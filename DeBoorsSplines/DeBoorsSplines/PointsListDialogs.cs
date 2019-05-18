using PointsLibrary;
using System.Windows;
using System.Windows.Controls;

namespace DeBoorsSplines
{
    /// <summary>
    /// Класс взаимодействия со списком контрольных точек.
    /// </summary>
    public class PointsListDialogs
    {
        public SplineCollection splineCollection { set; get; }
        public MainWindow mainWindow { set; get; }

        public PointsListDialogs(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Метод, устанавливающий список координат, если координаты посылаются
        /// одномоментно. То есть при загрузке координат из внешних источников.
        /// </summary>
        /// <param name="mainWindow">
        /// Объект класса MainWindow для работы с элементами интерфейса.
        /// </param>
        /// <param name="splineCollection">
        /// Объект класса-контейнера SplineCollection с информацией об элементах
        /// сплайна.
        /// </param>
        public void SetPointsList()
        {
            mainWindow.PointsListBox.Items.Clear();

            TextBlock header = new TextBlock
            {
                Text = "Список контрольных точек:",
                TextDecorations = TextDecorations.Underline,
                FontWeight = FontWeights.Bold
            };

            mainWindow.PointsListBox.Items.Add(header);

            for (int i = 0; i < splineCollection.PointsList.Count; i++)
            {
                mainWindow.PointsListBox.Items.Add($"{i}) " 
                    + splineCollection.PointsList[i]);
            }

            mainWindow.ParameterTextBox.Text = splineCollection.Parameter.ToString();
        }

        public void DeletePoint()
        {
            if (mainWindow.PointsListBox.Items.IndexOf(mainWindow.PointsListBox.SelectedItem) > 0)
            {
                splineCollection.PointsList.RemoveAt(mainWindow.PointsListBox.Items.IndexOf(mainWindow.PointsListBox.SelectedItem) - 1);

                mainWindow.PointsListBox.Items.RemoveAt(mainWindow.PointsListBox.Items.IndexOf(mainWindow.PointsListBox.SelectedItem));

                for (int i = 1; i < mainWindow.PointsListBox.Items.Count; i++)
                {
                    mainWindow.PointsListBox.Items[i] = $"{i - 1}) {splineCollection.PointsList[i - 1]}";
                }
            }
        }

        /// <summary>
        /// Метод добавления в список опорных точек новой точки по строке вида
        /// "x y".
        /// </summary>
        /// <param name="newPoint">
        /// Строка, содержащая координаты новой опрной точки.
        /// </param>
        public void AddNewPoint(string newPoint)
        {
            if (newPoint.Split(',').Length == 2)
            {
                string xCoordinate = newPoint.Split(',')[0],
                    yCoordinate = newPoint.Split(',')[1];

                if (int.TryParse(xCoordinate, out int temporaryX) && temporaryX > 0 &&
                    int.TryParse(yCoordinate, out int temporaryY) && temporaryY > 0)
                {
                    splineCollection.PointsList.Add(new PointsLibrary.Point(temporaryX, temporaryY));
                    SetPointsList();
                }
            }
        }

        public void EditPoint(string newPoint)
        {
            if (newPoint.Split(',').Length == 2)
            {
                string xCoordinate = newPoint.Split(',')[0],
                yCoordinate = newPoint.Split(',')[1];

                if (int.TryParse(xCoordinate, out int temporaryX) && temporaryX > 0 &&
                    int.TryParse(yCoordinate, out int temporaryY) && temporaryY > 0)
                {
                    int index = mainWindow.PointsListBox.Items.IndexOf(mainWindow.PointsListBox.SelectedItem);

                    splineCollection.PointsList[index - 1].PointX = temporaryX;
                    splineCollection.PointsList[index - 1].PointY = temporaryY;

                    SetPointsList();
                }
            }
        }
    }
}
