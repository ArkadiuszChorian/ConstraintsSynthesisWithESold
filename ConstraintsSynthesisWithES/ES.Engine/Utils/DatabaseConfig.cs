using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ES.Engine.Utils
{
    public static class DatabaseConfig
    {        
        static DatabaseConfig()
        {
            DriveToStore = DriveInfo.GetDrives().Any(x => x.Name.Contains("F"))
                ? DriveInfo.GetDrives().First(d => d.Name.Contains("F")).Name
                : DriveInfo.GetDrives().First(d => d.Name.Contains("C")).Name;

            ColumnsNames = new List<string>()
            {
                "Kolumna_1",
                "Kolumna_2"
            };
        }

        private static string DriveToStore { get; }
        private static string InnerPath = "OneDrive\\MGR Database\\Database v1.db";
        public static string DbFullPath => Path.GetFullPath(DriveToStore + InnerPath);
        public static List<string> ColumnsNames { get; }

        public static readonly Type[] SerializableTypes = {
            typeof(bool).BaseType,
            typeof(int).BaseType,
            typeof(long).BaseType,
            typeof(double).BaseType,
            typeof(decimal).BaseType,
            typeof(float).BaseType,
            typeof(Enum)
        };
    }
}
