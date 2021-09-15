# sciChartSine - Simple WPF MVVM Application to display a Sine Curve
Simple WPF MVVM application to display a Sine Curve. 
- Model generates the Sine Curve, and exposes the points generated
- ViewModel reads the point list generated by the Model, and exposes it to the View
- View consumes the point list, and other view-related strings through binding with the ViewModel
- Properties contains a resource file having a list of all strings displayed in the View
- Uses the SciChart library for displaying charts, graphs
- Clicking on the Button generates a new Sine Curve on each click
