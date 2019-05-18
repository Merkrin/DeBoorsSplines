using ColorLibrary;
using PointsLibrary;
using SplineMathsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DeBoorsSplines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileParser fileParser = new FileParser();
        private OpenSaveDialogs openSaveDialogs;
        private SplineCollection splineCollection = new SplineCollection();
        private KnotsClass knotsClass = new KnotsClass();
        private SplineMaker splineMaker;
        private DrawingClass drawingClass;
        private PointsListDialogs pointsListDialogs;
        private ColorDialogs colorDialogs = new ColorDialogs();
        private double OldCoordinateX { set; get; }
        private double OldCoordinateY { set; get; }
        private bool mouseClicked = false;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            drawingClass = new DrawingClass(this, splineCollection);
            openSaveDialogs = new OpenSaveDialogs(this, drawingClass, splineCollection);
            splineMaker = new SplineMaker(splineCollection);
            pointsListDialogs = new PointsListDialogs(this)
            {
                splineCollection = splineCollection
            };

            DeBoorsSplinesAppWindow.MinHeight =
                SystemParameters.PrimaryScreenHeight;
            DeBoorsSplinesAppWindow.MinWidth =
                SystemParameters.PrimaryScreenWidth;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainGrid.Height = ButtonsGrid.Height = DrawingCanvas.Height
                = DeBoorsSplinesAppWindow.ActualHeight;

            MainGrid.Width = DeBoorsSplinesAppWindow.ActualWidth;

            // Канвас во весь экран по высоте и на 70% по ширине
            int canvasWidth =
                (int)(DeBoorsSplinesAppWindow.ActualWidth / 100 * 70);
            DrawingCanvas.Width = canvasWidth;
            DrawingCanvas.Margin =
                new Thickness(0, 0, MainGrid.Width - canvasWidth, 0);

            ButtonsGrid.Width = MainGrid.Width - canvasWidth;
            ButtonsGrid.Margin = new Thickness(canvasWidth + 5, 0, 0, 0);
        }

        private void AddPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (splineCollection.PointsList == null)
            {
                splineCollection.PointsList = new List<PointsLibrary.Point>();
            }

            if (double.TryParse(ParameterTextBox.Text, out double t) && t > 0
                && t < 1)
            {
                DrawingCanvas.Children.Clear();

                splineCollection.Parameter = t;

                if (!string.IsNullOrWhiteSpace(AddPointTextBox.Text))
                {
                    pointsListDialogs.AddNewPoint(AddPointTextBox.Text);
                }

                if (ShowControlCheckBox.IsChecked == true)
                {
                    drawingClass.DrawControlLines();
                }

                if (splineCollection.PointsList.Count() >= 4)
                {
                    MakeSpline();
                }
            }
            else
            {
                openSaveDialogs.ShowErrorMessage("Неправильный параметр!");
            }
        }

        private void MakeSpline()
        {
            knotsClass.CalculateKnotsVektor(splineCollection.PointsList.Count(), splineCollection);
            splineMaker.SetSplineCurve(splineCollection.PointsList.Count());
            drawingClass.DrawSpline();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
            openSaveDialogs.OpenFile(fileParser, splineCollection);

            if (openSaveDialogs.OnPointsRenewer != null)
            {
                Visualize();
            }
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OldCoordinateX = e.GetPosition(DrawingCanvas).X;
            OldCoordinateY = e.GetPosition(DrawingCanvas).Y;
            mouseClicked = true;

            if (AddPointRadioButton.IsChecked == true)
            {
                if (splineCollection.PointsList == null)
                {
                    splineCollection.PointsList = new List<PointsLibrary.Point>();
                }

                if (double.TryParse(ParameterTextBox.Text, out double t) && t > 0
                    && t < 1)
                {
                    splineCollection.Parameter = double.Parse(ParameterTextBox.Text);
                    pointsListDialogs.AddNewPoint(e.GetPosition(DrawingCanvas).X.ToString() + "," + e.GetPosition(DrawingCanvas).Y.ToString());

                    Visualize();
                }
                else
                {
                    openSaveDialogs.ShowErrorMessage("Неправильный параметр!");
                }
            }
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            double xPosition = e.GetPosition(DrawingCanvas).X,
                    yPosition = e.GetPosition(DrawingCanvas).Y,
                    xChanges = OldCoordinateX - xPosition,
                    yChanges = OldCoordinateY - yPosition;

            if (mouseClicked && RelocateSplineRadioButton.IsChecked == true)
            {
                for (int i = 0; i < DrawingCanvas.Children.Count; i++)
                {
                    if (DrawingCanvas.Children[i] is Line)
                    {
                        Line line = DrawingCanvas.Children[i] as Line;
                        line.X1 -= xChanges;
                        line.X2 -= xChanges;
                        line.Y1 -= yChanges;
                        line.Y2 -= yChanges;
                        DrawingCanvas.Children[i] = line;
                    }
                    else if (DrawingCanvas.Children[i] is Ellipse)
                    {
                        Ellipse ellipse = DrawingCanvas.Children[i] as Ellipse;
                        Canvas.SetLeft(ellipse, Canvas.GetLeft(ellipse) - xChanges);
                        Canvas.SetTop(ellipse, Canvas.GetTop(ellipse) - yChanges);
                        DrawingCanvas.Children[i] = ellipse;
                    }
                    else
                    {
                        Label label = DrawingCanvas.Children[i] as Label;
                        Canvas.SetLeft(label, Canvas.GetLeft(label) - xChanges);
                        Canvas.SetTop(label, Canvas.GetTop(label) - yChanges);
                        DrawingCanvas.Children[i] = label;
                    }
                }

                if (splineCollection.PointsList != null)
                {
                    for (int i = 0; i < splineCollection.PointsList.Count(); i++)
                    {
                        splineCollection.PointsList[i].PointX -= xChanges;
                        splineCollection.PointsList[i].PointY -= yChanges;
                    }

                    pointsListDialogs.SetPointsList();
                }
            }

            if (mouseClicked && RelocatePointRadioButton.IsChecked == true &&
                splineCollection.PointsList != null && ShowControlCheckBox.IsChecked == true)
            {
                bool pointChecker = false;
                int relocatedPointIndex = 0;

                for (int i = 0; i < splineCollection.PointsList.Count(); i++)
                {
                    if (OldCoordinateX >= splineCollection.PointsList[i].PointX - 5 &&
                        OldCoordinateX <= splineCollection.PointsList[i].PointX + 5 &&
                        OldCoordinateY >= splineCollection.PointsList[i].PointY - 5 &&
                        OldCoordinateY <= splineCollection.PointsList[i].PointY + 5)
                    {
                        pointChecker = true;
                        relocatedPointIndex = i;
                        break;
                    }
                }

                if (pointChecker)
                {
                    splineCollection.PointsList[relocatedPointIndex].PointX -= xChanges;
                    splineCollection.PointsList[relocatedPointIndex].PointY -= yChanges;

                    Visualize();
                }
            }

            mouseClicked = false;
        }

        private void ParameterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (double.TryParse(ParameterTextBox.Text, out double t) && t > 0
                    && t < 1)
                {
                    Visualize();
                }
                else
                {
                    openSaveDialogs.ShowErrorMessage("Введённый параметр " +
                        "не является положительным числом!");
                }
            }
        }

        private SolidColorBrush SetColor(string line)
        {
            string[] colors = line.Split(',');

            return new SolidColorBrush
            {
                Color = Color.FromRgb(byte.Parse(colors[0]), byte.Parse(colors[1]), byte.Parse(colors[2]))
            };
        }

        private void StartingColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(StartingColorTextBox.Text) &&
                    colorDialogs.CheckColor(StartingColorTextBox.Text))
                {
                    drawingClass.StartingColor = StartingColorTextBox.Text;

                    StartingColorExample.Fill = SetColor(StartingColorTextBox.Text);

                    ShowControlRadioButton_Click(sender, e);
                }
                else
                {
                    openSaveDialogs.ShowErrorMessage("Введите правильный цвет " +
                        "в формате RGB: \"r,g,b\"!");
                }
            }
        }

        private void DeletePointButton_Click(object sender, RoutedEventArgs e)
        {
            pointsListDialogs.DeletePoint();

            Visualize();
        }

        private void EditPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EditPointTextBox.Text))
            {
                pointsListDialogs.EditPoint(EditPointTextBox.Text);
            }

            Visualize();
        }

        private void AddPointTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddPointButton_Click(sender, e);
            }
        }

        private void ShowControlRadioButton_Click(object sender, RoutedEventArgs e)
        {
            Visualize();
        }

        private void EndingColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(StartingColorTextBox.Text) &&
                    colorDialogs.CheckColor(EndingColorTextBox.Text))
                {
                    drawingClass.EndingColor = EndingColorTextBox.Text;

                    EndingColorExample.Fill = SetColor(EndingColorTextBox.Text);

                    ShowControlRadioButton_Click(sender, e);
                }
                else
                {
                    openSaveDialogs.ShowErrorMessage("Введите правильный цвет " +
                        "в формате RGB: \"r,g,b\"!");
                }
            }
        }

        private void GenerationButton_Click(object sender, RoutedEventArgs e)
        {
            if (splineCollection.PointsList == null)
            {
                splineCollection.PointsList = new List<PointsLibrary.Point>();
            }

            if (double.TryParse(ParameterTextBox.Text, out double t) && t > 0
                && t < 1)
            {
                DrawingCanvas.Children.Clear();

                splineCollection.PointsList.Clear();

                splineCollection.Parameter = t;

                if (!string.IsNullOrWhiteSpace(NPointsTextBox.Text))
                {
                    if (int.TryParse(NPointsTextBox.Text, out int n) && n >= 4)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            pointsListDialogs.AddNewPoint(random.Next(0,
                                (int)DrawingCanvas.ActualWidth).ToString() + "," +
                                random.Next(0,
                                (int)DrawingCanvas.Height - 65).ToString());
                        }
                    }
                }

                Visualize();
            }
            else
            {
                openSaveDialogs.ShowErrorMessage("Неправильный параметр!");
            }
        }

        private void Visualize()
        {
            DrawingCanvas.Children.Clear();

            if (splineCollection.PointsList != null)
            {

                if (ShowControlCheckBox.IsChecked == true)
                {
                    try
                    {
                        drawingClass.DrawControlLines();
                    }
                    catch (Exception e)
                    {
                        openSaveDialogs.ShowErrorMessage(e.Message);
                    }
                }

                if (ShowIndexCheckBox.IsChecked == true)
                {
                    try
                    {
                        drawingClass.DrawIndexes();
                    }
                    catch (Exception e)
                    {
                        openSaveDialogs.ShowErrorMessage(e.Message);
                    }
                }

                if (double.TryParse(ParameterTextBox.Text, out double t) && t > 0
                    && t < 1)
                {
                    splineCollection.Parameter = t;

                    try
                    {
                        pointsListDialogs.SetPointsList();

                        if (splineCollection.PointsList.Count() >= 4)
                        {
                            MakeSpline();
                        }
                    }
                    catch (Exception e)
                    {
                        openSaveDialogs.ShowErrorMessage(e.Message);
                    }
                }
            }
        }

        private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            splineCollection.PointsList = null;
            PointsListBox.Items.Clear();

            TextBlock header = new TextBlock
            {
                Text = "Список контрольных точек:",
                TextDecorations = TextDecorations.Underline,
                FontWeight = FontWeights.Bold
            };

            PointsListBox.Items.Add(header);

            openSaveDialogs.Path = null;

            Visualize();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (openSaveDialogs.Path != null)
            {
                openSaveDialogs.SaveOpenedFile();
            }
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (splineCollection.PointsList != null)
            {
                openSaveDialogs.SaveNewFile();
            }
        }

        private void InfoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.Help.ShowHelp(null, @"../../Help/help.chm");
            }
            catch (Exception exception)
            {
                openSaveDialogs.ShowErrorMessage(exception.Message);
            }
        }

        private void NPointsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GenerationButton_Click(sender, e);
            }
        }

        private void EditPointTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EditPointButton_Click(sender, e);
        }

        private void ShowIndexCheckBox_Click(object sender, RoutedEventArgs e)
        {
            Visualize();
        }
    }
}
