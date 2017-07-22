using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ES.Engine.Constraints;
using ES.Engine.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace ES.Engine.Utils
{
    public class Visualization
    {
        private readonly RangeColorAxis _colorAxis;
        private readonly Dictionary<OxyColor, double> _colorKey;
        private const string ColorAxisName = "ColorAxis";

        public Visualization()
        {
            Plots = new List<PlotView>();

            _colorKey = new Dictionary<OxyColor, double>();
            _colorAxis = new RangeColorAxis {Key = ColorAxisName};

            var fieldInfos = typeof(OxyColors).GetFields(BindingFlags.Static | BindingFlags.Public);
            var rangeStart = 0.0;

            foreach (var fieldInfo in fieldInfos)
            {
                var oxyColor = (OxyColor)fieldInfo.GetValue(null);

                if (_colorKey.ContainsKey(oxyColor)) continue;

                _colorAxis.AddRange(rangeStart, rangeStart + 0.1, oxyColor);
                _colorKey.Add(oxyColor, rangeStart);
                rangeStart++;
            }

            Application.EnableVisualStyles();
        }

        public List<PlotView> Plots { get; set; }
      
        public Thread Show()
        {

            var c = Plots.Count;

            var h = 500 + ((c - 1) / 3) * 400;
            int w;
            if (c > 2) w = 1300;
            else w = 100 + c * 400;

            var form = new Form()
            {
                Text = "Thesis",
                Height = h,
                Width = w,
                AutoScroll = true
            };

            var i = 0;

            foreach (var plot in Plots)
            {
                plot.Location = new System.Drawing.Point((i % 3) * 400, 20 + (i / 3) * 400);
                form.Controls.Add(plot);
                i++;
            }

            var plotThread = new Thread(() =>
            {
                Application.Run(form);
            });

            plotThread.SetApartmentState(ApartmentState.STA);
            plotThread.Start();
            
            return plotThread;
        }

        public Visualization AddNextPlot(string title = "Plot", int width = 400, int height = 400, int yAxisMin = -100, int yAxisMax = 100, int xAxisMin = -100, int xAxisMax = 100)
        {
            var plot = new PlotView { Size = new System.Drawing.Size(width, height) };

            var model = new PlotModel { Title = title };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = xAxisMin, Maximum = xAxisMax });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = yAxisMin, Maximum = yAxisMax });
            model.Axes.Add(_colorAxis.DeepCopyByExpressionTree());
            plot.Model = model;

            Plots.Add(plot);

            return this;
        }

        public Visualization AddPoints(IEnumerable<Point> points, OxyColor color, MarkerType markerType = MarkerType.Circle, double pointSize = 3)
        {
            var plot = Plots.Last();

            var series = new ScatterSeries { MarkerType = markerType, ColorAxisKey = ColorAxisName };

            foreach (var point in points)
            {
                series.Points.Add(new ScatterPoint(point.Coordinates[0], point.Coordinates[1], pointSize, _colorKey[color]));
            }

            plot.Model.Series.Add(series);

            return this;
        }

        //public Visualization AddConstraint(Constraint constraint, int xMin = -100, int xMax = 100, double step = 0.5)
        //{
        //    var plot = Plots.Last();

        //    plot.Model.Series.Add(
        //        new FunctionSeries(x => (constraint.LimitingValue / constraint.TermsCoefficients[1]) - ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), 
        //        xMin, xMax, step));

        //    return this;
        //}

        public Visualization AddConstraints(IList<Constraint> constraints, Func<int, OxyPalette> paletteInitializer = null, OxyColor color = default(OxyColor), double xMin = -100, double xMax = 100, double step = 0.5)
        {
            var plot = Plots.Last();

            OxyPalette palette = null;        

            if (paletteInitializer != null)
            {
                palette = paletteInitializer.Invoke(constraints.Count == 1 ? constraints.Count + 1 : constraints.Count);
            }
            else
            {
                color = color == default(OxyColor) ? OxyColors.Black : color;
            }

            for (var i = 0; i < constraints.Count; i++)
            {
                //var series =
                //    new FunctionSeries(
                //        x =>
                //            (constraints[i].LimitingValue / constraints[i].TermsCoefficients[1]) -
                //            ((constraints[i].TermsCoefficients[0] / constraints[i].TermsCoefficients[1]) * x), xMin, xMax, step)
                //    {
                //        Color = palette?.Colors[i] ?? color
                //    };

                //FunctionSeries series;
                //var constraintTypeName = constraints[i].GetType().FullName;

                //switch (constraintTypeName)
                //{
                //    case nameof(LinearConstraint):
                //        series = GetLinearSeries(constraints[i], palette?.Colors[i] ?? color, xMin, xMax, step);
                //        break;
                //    case nameof(BallConstraint):
                //        break;
                //    default:
                //        series = GetLinearSeries(constraints[i], palette?.Colors[i] ?? color, xMin, xMax, step);
                //        break;
                //}

                //plot.Model.Series.Add(series);

                plot.Model.Series.Add(GetSeries(constraints[i], palette?.Colors[i] ?? color, xMin, xMax, step));
            }

            //foreach (var constraint in constraints)
            //{
            //    var series =
            //        new FunctionSeries(
            //            x =>
            //                (constraint.LimitingValue / constraint.TermsCoefficients[1]) -
            //                ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), xMin, xMax, step)
            //        {
            //            Color = OxyColors.Aqua
            //        };

            //    plot.Model.Series.Add(series);
            //}

            return this;
        }

        public Visualization AddClusters(IEnumerable<Point> positivePoints, IEnumerable<Point> negativePoints, string title = "", int? plotNumber = null)
        {
            //PlotView plot;

            var plot = plotNumber.HasValue
                ? Plots[plotNumber.Value]
                : new PlotView { Size = new System.Drawing.Size(400, 400) };

            //var plot = new PlotView {Size = new System.Drawing.Size(400, 400)};

            var model = new PlotModel { Title = title };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -100, Maximum = 100 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -100, Maximum = 100 });
            plot.Model = model;

            var zeroOneAxis = new RangeColorAxis { Key = "zeroOneColors" };
            zeroOneAxis.AddRange(0, 0.1, OxyColors.Red);
            zeroOneAxis.AddRange(1, 1.1, OxyColors.ForestGreen);

            var clustersAxis = new RangeColorAxis { Key = "clustersColors" };
            clustersAxis.AddRange(0, 0.1, OxyColors.Red);
            clustersAxis.AddRange(1, 1.1, OxyColors.Orange);
            clustersAxis.AddRange(2, 2.1, OxyColors.OrangeRed);
            clustersAxis.AddRange(3, 3.1, OxyColors.DarkOrange);
            clustersAxis.AddRange(4, 4.1, OxyColors.DarkRed);
            clustersAxis.AddRange(5, 5.1, OxyColors.IndianRed);

            plot.Model.Axes.Add(zeroOneAxis);
            plot.Model.Axes.Add(clustersAxis);
            plot.Model.Axes.Add(_colorAxis);

            //var dataPositiveSeries = new ScatterSeries { MarkerType = MarkerType.Circle, ColorAxisKey = "zeroOneColors" };
            var dataPositiveSeries = new ScatterSeries { MarkerType = MarkerType.Circle, ColorAxisKey = ColorAxisName };
            foreach (var point in positivePoints)
            {
                //dataPositiveSeries.Points.Add(new ScatterPoint(point.Coordinates[0], point.Coordinates[1], 3, 1));
                dataPositiveSeries.Points.Add(new ScatterPoint(point.Coordinates[0], point.Coordinates[1], 3, _colorKey[OxyColors.Aqua]));
            }

            //var dataPositiveSeries = new ScatterSeries { MarkerType = MarkerType.Circle, TextColor = OxyColors.AliceBlue };
            //foreach (var point in positivePoints)
            //{
            //    dataPositiveSeries.Points.Add(new ScatterPoint(point.Coordinates[0], point.Coordinates[1], 3));
            //}

            plot.Model.Series.Add(dataPositiveSeries);

            var dataNegativeSeries = new ScatterSeries { MarkerType = MarkerType.Circle, ColorAxisKey = "clustersColors" };

            foreach (var point in negativePoints)
            {
                dataNegativeSeries.Points.Add(new ScatterPoint(point.Coordinates[0], point.Coordinates[1], 3, 0));
            }

            plot.Model.Series.Add(dataNegativeSeries);

            Plots.Add(plot);

            return this;
        }       

        
        public Visualization AddModelPlot(List<Constraint> constraints, string title)
        {

            var plot = new PlotView
            {
                Location = new System.Drawing.Point(450, 20),
                Size = new System.Drawing.Size(400, 400)
            };

            var plotModel = new PlotModel { Title = title };
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 100 });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });
            plot.Model = plotModel;

            var zeroOneAxis = new RangeColorAxis { Key = "zeroOneColors" };
            zeroOneAxis.AddRange(0, 0.1, OxyColors.Red);
            zeroOneAxis.AddRange(1, 1.1, OxyColors.ForestGreen);

            plot.Model.Axes.Add(zeroOneAxis);

            //var positiveSeries = new ScatterSeries { MarkerType = MarkerType.Circle, ColorAxisKey = "zeroOneColors" };
            //var negativeSeries = new ScatterSeries { MarkerType = MarkerType.Circle, ColorAxisKey = "zeroOneColors" };

            foreach (var constraint in constraints)
            {
                plot.Model.Series.Add(new FunctionSeries(x => (constraint.LimitingValue / constraint.TermsCoefficients[1]) - ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), -100, 100, 0.2));
            }

            Plots.Add(plot);

            return this;
        }

        private Series GetSeries(Constraint constraint, OxyColor color = default(OxyColor), double xMin = -100, double xMax = 100,
            double step = 0.5)
        {
            var constraintTypeName = constraint.GetType().Name;

            var denominator = constraint.TermsCoefficients[1];
            var aNominator = constraint.TermsCoefficients[0];
            var bNominator = constraint.LimitingValue;

            if (denominator == 0)
            {
                denominator = 1;
                aNominator *= 10000;
                bNominator *= 10000;
            }
            
            var a = aNominator / denominator;
            var b = bNominator / denominator;
            return new FunctionSeries(x => b - a * x, xMin, xMax, step)
            {
                Color = color.Equals(default(OxyColor)) ? OxyColors.Black : color,
            };

            switch (constraintTypeName)
            {
                case nameof(LinearConstraint):
                    return new FunctionSeries(x => b - a * x, xMin, xMax, step)
                    {
                        Color = color.Equals(default(OxyColor)) ? OxyColors.Black : color,                       
                    };
                case nameof(BallConstraint):
                    //return new FunctionSeries(
                    //    x =>
                    //        (constraint.LimitingValue / constraint.TermsCoefficients[1]) -
                    //        ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), xMin, xMax, step)
                    //{
                    //    Color = color.Equals(default(OxyColor)) ? OxyColors.Black : color
                    //};
                    return new ScatterSeries();
                default:
                    return new FunctionSeries(
                        x =>
                            (constraint.LimitingValue / constraint.TermsCoefficients[1]) -
                            ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), xMin, xMax, step)
                    {
                        Color = color.Equals(default(OxyColor)) ? OxyColors.Black : color
                    };
            }

            //return new FunctionSeries(
            //            x =>
            //                (constraint.LimitingValue / constraint.TermsCoefficients[1]) -
            //                ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), xMin, xMax, step)
            //{
            //    Color = color.Equals(default(OxyColor)) ? OxyColors.Black : color
            //};
        }

        private FunctionSeries GetBallSeries(Constraint constraint, OxyColor color = default(OxyColor), int xMin = -100, int xMax = 100,
            double step = 0.5)
        {
            return new FunctionSeries(
                        x =>
                            (constraint.LimitingValue / constraint.TermsCoefficients[1]) -
                            ((constraint.TermsCoefficients[0] / constraint.TermsCoefficients[1]) * x), xMin, xMax, step)
            {
                Color = color.Equals(default(OxyColor)) ? OxyColors.Black : color
            };
        }
    }
}
