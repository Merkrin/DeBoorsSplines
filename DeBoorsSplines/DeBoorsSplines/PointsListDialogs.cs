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

        /// <summary>
        /// Конструктор класса PointsListDialogs.
        /// </summary>
        /// <param name="mainWindow">
        /// Экземпляр класса <see cref="MainWindow"/>.
        /// </param>
        public PointsListDialogs(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Метод, устанавливающий список координат.
        /// </summary>
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

        /// <summary>
        /// Метод удаления опорной точки.
        /// </summary>
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
        /// Метод добавления в список опорных точек новой точки.
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

                if (int.TryParse(xCoordinate, out int temporaryX) &&
                    int.TryParse(yCoordinate, out int temporaryY))
                {
                    splineCollection.PointsList.Add(new PointsLibrary.Point(temporaryX, temporaryY));
                    SetPointsList();
                }
            }
        }

        /// <summary>
        /// Метод изменения опорной точки.
        /// </summary>
        /// <param name="newPoint">
        /// Строка, содержащая координаты новой опрной точки.
        /// </param>
        public void EditPoint(string newPoint)
        {
            if (newPoint.Split(',').Length == 2)
            {
                string xCoordinate = newPoint.Split(',')[0],
                yCoordinate = newPoint.Split(',')[1];

                if (int.TryParse(xCoordinate, out int temporaryX) &&
                    int.TryParse(yCoordinate, out int temporaryY))
                {
                    int index = mainWindow.PointsListBox.Items.IndexOf(mainWindow.PointsListBox.SelectedItem);

                    splineCollection.PointsList[index].PointX = temporaryX;
                    splineCollection.PointsList[index].PointY = temporaryY;

                    SetPointsList();
                }
            }
        }
    }
}
