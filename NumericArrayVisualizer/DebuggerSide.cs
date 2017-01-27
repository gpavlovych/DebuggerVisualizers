using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(NumericArrayVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(double[]),
Description = "Plot")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(NumericArrayVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(float[]),
Description = "Plot")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(NumericArrayVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(double[][]),
Description = "Image")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(NumericArrayVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(double[,]),
Description = "Image")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(NumericArrayVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(float[][]),
Description = "Image")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(NumericArrayVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(float[,]),
Description = "Image")]
namespace NumericArrayVisualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        [STAThread]
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var window = new MainWindow()
            {


            };

            window.Points = window.Points ?? (objectProvider.GetObject() as double[])?.Select((item,index)=>new Point(){X = index, Y = item}).ToArray();

            window.Points = window.Points ?? (objectProvider.GetObject() as float[])?.Select((item, index) => new Point() { X = index, Y = item }).ToArray();
            
            window.Points2D = window.Points2D ??
                              (objectProvider.GetObject() as double[][])?.SelectMany((row, rowIndex) => row.Select((item, columnIndex) => new Point3D(rowIndex, columnIndex, item)))
                                  .ToArray();

            window.Points2D = window.Points2D ??
                              (objectProvider.GetObject() as float[][])?.SelectMany((row, rowIndex) => row.Select((item, columnIndex) => new Point3D(rowIndex, columnIndex, item)))
                                  .ToArray();

            var array2DDouble = objectProvider.GetObject() as double[,];
            window.Points2D = window.Points2D ??
                              Enumerable.Range(0, array2DDouble.GetLength(0))
                                  .SelectMany(
                                      row =>
                                          Enumerable.Range(0, array2DDouble.GetLength(1))
                                              .Select(column => new Point3D(row, column, array2DDouble[row, column])))
                                  .ToArray();

            var array2DFloat = objectProvider.GetObject() as float[,];
            window.Points2D = window.Points2D ??
                              Enumerable.Range(0, array2DFloat.GetLength(0))
                                  .SelectMany(
                                      row =>
                                          Enumerable.Range(0, array2DFloat.GetLength(1))
                                              .Select(column => new Point3D(row, column, array2DFloat[row, column])))
                                  .ToArray();
            window.ShowDialog();
        }
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DebuggerSide));
            visualizerHost.ShowVisualizer();
        }
    }
}
