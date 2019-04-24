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
        public void SetPointsList(MainWindow mainWindow, SplineCollection splineCollection)
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
                mainWindow.PointsListBox.Items.Add(splineCollection.PointsList[i]);
            }

            mainWindow.ParameterTextBox.Text = splineCollection.Parameter.ToString();
        }

        /// <summary>
        /// Метод добавления в список опорных точек новой точки по строке вида
        /// "x y".
        /// </summary>
        /// <param name="mainWindow">
        /// Объект класса MainWindow для работы с элементами интерфейса.
        /// </param>
        /// <param name="newPoint">
        /// Строка, содержащая координаты новой опрной точки.
        /// </param>
        public void AddNewPoint(MainWindow mainWindow, string newPoint)
        {
            string xCoordinate = newPoint.Split(' ')[0],
                yCoordinate = newPoint.Split(' ')[1];

            if (int.TryParse(xCoordinate, out int temporary) &&
                int.TryParse(yCoordinate, out temporary))
            {

                //mainWindow.PointsListBox.Items.Add();
            }
        }
    }
}
