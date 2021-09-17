using SciChart.Charting.Model.DataSeries;
using System;

namespace SciChartSine.Model
{
    public class SineCurve
    {
        private double _phase;
        private double _amplitude;
        public SineCurve()
        {
            LineData = new XyDataSeries<double, double>() { SeriesName = "Sinusoid" };
            UpdateCurve();
        }

        public void UpdateCurve()
        {
            GenerateRandomAmplitudeAndPhase();
            GenerateSineData();
        }

        public XyDataSeries<double, double> LineData { get; private set; }

        public String Phase
        {
            get
            {
                double ph = Math.Round(_phase, 3);
                return ph.ToString();
            }
        }

        public String Amplitude
        {
            get
            {
                double amp = Math.Round(_amplitude, 3);
                return amp.ToString();
            }
        }

        #region HelperFunctions
        private void GenerateRandomAmplitudeAndPhase()
        {
            double minPhase = 0; // degrees
            double maxPhase = 180; // degrees
            double minAmplitude = 1;
            double maxAmplitude = 50;

            Random random = new Random();
            double ampRand = random.NextDouble();
            _amplitude = Math.Ceiling(minAmplitude + ampRand * (maxAmplitude - minAmplitude));

            double phaseRand = random.NextDouble();
            _phase = minPhase + phaseRand * (maxPhase - minPhase); // Degrees
            _phase *= Math.PI / 180.0; // Radians
        }

        private void GenerateSineData()
        {
            int noDivisions = 200;
            double totalRange = 4 * Math.PI;
            double step = totalRange / noDivisions;

            LineData.Clear();

            for (int i = 0; i <= noDivisions; ++i)
            {
                double x = i * step;
                double y = _amplitude * Math.Sin(x + _phase);
                LineData.Append(x, y);
            }
        }
        #endregion
    }
}
