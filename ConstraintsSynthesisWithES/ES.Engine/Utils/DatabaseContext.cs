using System;
using System.Collections.Generic;
using System.Linq;
using ES.Engine.Benchmarks;
using ES.Engine.Constraints;
using ES.Engine.Models;
using ExperimentDatabase;

namespace ES.Engine.Utils
{
    public class DatabaseContext
    {
        private readonly Database _database;
        private readonly DatabaseEngine _databaseEngine;
        private readonly DataSet _experiment;
        private readonly DataSet _error;
        
        private readonly int _version;
        private bool _anyErrors;

        public DatabaseContext(string dbFilePath, int version)
        {
            _database = new Database(dbFilePath);
            _databaseEngine = new DatabaseEngine(dbFilePath);

            _experiment = _database.NewExperiment();
            _error = _experiment.NewChildDataSet("errors");

            _version = version;
            _anyErrors = false;
        }
        
        public void Initialize()
        {
            _experiment.Add("StartDateTime", DateTime.Now);
            _experiment.Add("Version", _version);
        }

        public void Insert(ExperimentParameters experimentParameters)
        {
            Insert<ExperimentParameters>(experimentParameters);   
        }

        public void Insert(Statistics statistics)
        {
            Insert<Statistics>(statistics);
        }

        public void Insert(IList<Constraint> constraints, IBenchmark benchmark)
        {
            _experiment.Add("SynthesizedLpModel", constraints.ToLpFormat(benchmark.Domains));
            _experiment.Add("ReferenceLpModel", benchmark.Constraints.ToLpFormat(benchmark.Domains));
        }

        public void Insert(Exception exception)
        {
            _error.Add(nameof(exception.HResult), exception.HResult);
            _error.Add(nameof(exception.Message), exception.Message);
            _error.Add(nameof(exception.StackTrace), exception.StackTrace);

            _anyErrors = true;
        }

        public void Save()
        {
            _experiment.Save(false);

            if (!_anyErrors) return;

            _error.Save();
            _anyErrors = false;
        }

        public void Dispose()
        {
            _experiment.Dispose();
            _database.Dispose();
        }

        private void Insert<T>(T objectToInsert)
        {
            var propertyInfos = objectToInsert.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                if (!DatabaseConfig.SerializableTypes.Contains(propertyInfo.PropertyType.BaseType)) continue;

                var valueToInsert = propertyInfo.GetValue(objectToInsert, null);

                if (propertyInfo.PropertyType.IsEnum)
                {
                    var numericValue = propertyInfo.GetValue(objectToInsert, null);
                    var stringValue = Enum.GetName(propertyInfo.PropertyType, numericValue);

                    valueToInsert = stringValue;
                }

                _experiment.Add(propertyInfo.Name, valueToInsert);
            }
        }
    }
}
