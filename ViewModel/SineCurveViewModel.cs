using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;
using SciChartSine.Model;
using System.Collections.ObjectModel;
using System.Windows.Media;
using SciChartSine.Properties;

namespace SciChartSine.ViewModel
{
    public class SineCurveViewModel : BindableObject
    {
        private string _chartTitle = Strings.SineCurve_ChartTitle;
        private string _xAxisTitle = Strings.SineCurve_XaxisTitle;
        private string _yAxisTitle = Strings.SineCurve_YaxisTitle;
        private string _bnGenerateNew = Strings.SineCurve_Button_GenerateNew;
        private string _windowTitle = Strings.SineCurve_WindowTitle;
        private SineCurve _sine;
        private XyDataSeries<double, double> _sineData;
        private RelayCommand _generateCurveCommand;

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

        public string BnGenerateNew
        {
            get { return _bnGenerateNew; }
            set
            {
                _bnGenerateNew = value;
                OnPropertyChanged(nameof(BnGenerateNew));
            }
        }

        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged(nameof(WindowTitle));
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
        }

        bool CanGenerate
        {
            get { return true; }
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
