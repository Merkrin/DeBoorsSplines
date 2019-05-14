using Microsoft.Win32;
using PointsLibrary;
using System;
using System.IO;
using System.Windows;

namespace DeBoorsSplines
{
    /// <summary>
    /// Делегат, содержащий методы, вызываемые сразу после обновления
    /// опорных точек в соответствующем списке.
    /// </summary>
    /// <param name="mainWindow">
    /// Объект класса MainWindow для работы с элементами интерфейса.
    /// </param>
    /// <param name="splineCollection">
    /// Объект класса-контейнера SplineCollection с информацией об элементах
    /// сплайна.
    /// </param>
    public delegate void OnPointsRenewed();

    /// <summary>
    /// Класс взаимодействия с диалоговыми окнами.
    /// </summary>
    public class OpenSaveDialogs
    {
        private static PointsListDialogs pointsListDialogs = new PointsListDialogs();
        public OnPointsRenewed OnPointsRenewer;
        //private static DrawingClass drawingClass = new DrawingClass();
        public OpenFileDialog openFileDialog;

        private string Path { set; get; }

        private MainWindow mainWindow { set; get; }
        private DrawingClass drawingClass { set; get; }

        public OpenSaveDialogs (MainWindow mainWindow, DrawingClass drawingClass)
        {
            this.mainWindow = mainWindow;
            this.drawingClass = drawingClass;
        }

        /// <summary>
        /// Метод взаимодействия с диалоговым окном открытия файла. Открывается
        /// файл ".txt" или ".dbsp".
        /// </summary>
        /// <param name="mainWindow">
        /// Объект класса MainWindow для работы с элементами интерфейса.
        /// </param>
        /// <param name="fileParser">
        /// Объект класса чтения файла.
        /// </param>
        /// <param name="splineCollection">
        /// Объект класса-контейнера SplineCollection с информацией об элементах
        /// сплайна.
        /// </param>
        public void OpenFile(FileParser fileParser, SplineCollection splineCollection)
        {
            openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|DeBoorsSplines" +
                " Files (*.dbsp)|*.dbsp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Path = openFileDialog.FileName;

                    fileParser.ReadFile(Path, splineCollection);

                    //if(scaleClass.CheckScaling(mainWindow, splineCollection))
                    //{
                    //    scaleClass.ScaleSpline(mainWindow, splineCollection);

                    //    MessageBox.Show("Для налядной визуализации опорная кривая" +
                    //        " была отмасштабирована под размер рабочей поверхности",
                    //        "Проведено масштабирование", MessageBoxButton.OK,
                    //        MessageBoxImage.Information);
                    //}

                    pointsListDialogs.SetPointsList(mainWindow, splineCollection);
                    OnPointsRenewer = drawingClass.DrawControlLines;
                }
                catch (FileException e)
                {
                    OnPointsRenewer = null;
                    ShowErrorMessage(e.Message);
                }
                catch (IOException e)
                {
                    OnPointsRenewer = null;
                    ShowErrorMessage(e.Message);
                }
                catch (Exception e)
                {
                    OnPointsRenewer = null;
                    ShowErrorMessage(e.Message);
                }
            }
        }

        /// <summary>
        /// Метод, показывающий окно с информацией о возникшей ошибке.
        /// </summary>
        /// <param name="errorMessage">Текст исключения.</param>
        public void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Произошла ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
