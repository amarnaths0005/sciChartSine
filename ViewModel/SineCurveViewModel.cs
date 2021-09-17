using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;
using SciChartSine.Model;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SciChartSine.ViewModel
{
    public class SineCurveViewModel : BindableObject
    {
        private string _chartTitle = "Sine Curve";
        private string _xAxisTitle = "Independent Variable x";
        private string _yAxisTitle = "Sine Value y";
        private SineCurve _sine;
        private XyDataSeries<double, double> _sineData;
        private RelayCommand _generateCurveCommand;
        private string _amp;
        private string _phas;

        private ObservableCollection<IRenderableSeriesViewModel> _renderableSeries;

        public SineCurveViewModel()
        {
            _renderableSeries = new ObservableCollection<IRenderableSeriesViewModel>();
            _sine = new SineCurve();
            _sineData = new XyDataSeries<double, double>();
            GenerateNewCurve();
        }

        public ObservableCollection<IRenderableSeriesViewModel> RenderableSeries
        {
            get { return _renderableSeries; }
            set { _renderableSeries = value;
                OnPropertyChanged(nameof(RenderableSeries));
            }
        }
        public string ChartTitle
        {
            get { return _chartTitle; }
            set
            {
                _chartTitle = value;
                OnPropertyChanged(nameof(ChartTitle));
            }
        }
        public string XAxisTitle
        {
            get { return _xAxisTitle; }
            set
            {
                _xAxisTitle = value;
                OnPropertyChanged(nameof(XAxisTitle));
            }
        }
        public string YAxisTitle
        {
            get { return _yAxisTitle; }
            set
            {
                _yAxisTitle = value;
                OnPropertyChanged(nameof(YAxisTitle));
            }
        }

        public string Amplitude
        {
            get
            {
                return "Amplitude = " + _amp;
            }
            set
            {
                _amp = value;
                OnPropertyChanged(nameof(Amplitude));
            }
        }

        public string Phase
        {
            get
            {
                return "Phase = " + _phas + " radians"; 
            }
            set
            {
                _phas = value;
                OnPropertyChanged(nameof(Phase));
            }
        }

        public void GenerateNewCurve()
        {
            _sine.UpdateCurve();
            _sineData = _sine.LineData;
            _sineData.InvalidateParentSurface(RangeMode.ZoomToFit);

            RenderableSeries.Clear();
            RenderableSeries.Add(new LineRenderableSeriesViewModel()
            {
                StrokeThickness = 2,
                Stroke = Colors.Azure,
                DataSeries = _sineData,
            }
                );
            _amp = _sine.Amplitude;
            _phas = _sine.Phase;
            Amplitude = _amp;
            Phase = _phas;
        }

        bool CanGenerate
        {
            get { return _sine.LineData.Count > 0; }
        }

        public RelayCommand GenerateCurveCommand
        {
            get
            {
                if (_generateCurveCommand == null)
                {
                    _generateCurveCommand = new RelayCommand(
                        param => GenerateNewCurve(),
                        param => CanGenerate
                        );
                }
                return _generateCurveCommand;
            }
            set => _generateCurveCommand = value;
        }

    }
}
