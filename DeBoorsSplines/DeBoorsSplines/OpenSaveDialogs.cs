using Microsoft.Win32;
using PointsLibrary;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private static PointsListDialogs pointsListDialogs;
        public OnPointsRenewed OnPointsRenewer;
        //private static DrawingClass drawingClass = new DrawingClass();
        public OpenFileDialog openFileDialog;

        internal string Path { set; get; }

        private MainWindow mainWindow { set; get; }
        private DrawingClass drawingClass { set; get; }
        private SplineCollection splineCollection { set; get; }

        public OpenSaveDialogs (MainWindow mainWindow, DrawingClass drawingClass,
            SplineCollection splineCollection)
        {
            this.mainWindow = mainWindow;
            this.drawingClass = drawingClass;
            this.splineCollection = splineCollection;
            pointsListDialogs = new PointsListDialogs(mainWindow);
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
            pointsListDialogs.splineCollection = splineCollection;

            openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|DeBoorsSplines" +
                " files (*.dbsp)|*.dbsp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Path = openFileDialog.FileName;

                    fileParser.ReadFile(Path, splineCollection);

                    pointsListDialogs.SetPointsList();
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

        private void SaveTextFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                int pointsAmount = splineCollection.PointsList.Count;

                streamWriter.WriteLine($"{pointsAmount}:");

                for (int i = 0; i < pointsAmount; i++)
                {
                    streamWriter.WriteLine(
                        $"{splineCollection.PointsList[i].PointX} " +
                        $"{splineCollection.PointsList[i].PointY}");
                }

                streamWriter.WriteLine("t:");

                streamWriter.Write(splineCollection.Parameter);

                MessageBox.Show("Файл успешно сохранён", "Сохранение",
            MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SavePicture(string path)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)
                (mainWindow.DeBoorsSplinesAppWindow.ActualWidth / 100 * 70),
                (int)(mainWindow.DeBoorsSplinesAppWindow.ActualHeight),
                96d, 96d, PixelFormats.Pbgra32);

            mainWindow.DrawingCanvas.Measure(new Size((int)
                (mainWindow.DeBoorsSplinesAppWindow.ActualWidth / 100 * 70),
                (int)(mainWindow.DeBoorsSplinesAppWindow.ActualHeight)));
            mainWindow.DrawingCanvas.Arrange(new Rect(new Size((int)
                (mainWindow.DeBoorsSplinesAppWindow.ActualWidth / 100 * 70),
                (int)(mainWindow.DeBoorsSplinesAppWindow.ActualHeight))));

            renderBitmap.Render(mainWindow.DrawingCanvas);

            if (path.Substring(path.Length - 4) == "png")
            {
                PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
                pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (FileStream file = File.Create(path))
                {
                    pngBitmapEncoder.Save(file);
                }
            }
            else
            {
                JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (FileStream file = File.Create(path))
                {
                    jpegBitmapEncoder.Save(file);
                }
            }

            MessageBox.Show("Файл успешно сохранён", "Сохранение",
            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SaveOpenedFile()
        {
            try
            {
                SaveTextFile(Path);
            }catch(IOException e)
            {
                ShowErrorMessage(e.Message);
            }catch(Exception e)
            {
                ShowErrorMessage(e.Message);
            }
        }

        public void SaveNewFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt|DeBoorsSplines" +
                " files (*.dbsp)|*.dbsp|PNG image (*.png)|*.png|JPEG image (*.jpeg)|" +
                "*.jpeg"
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    if (saveFileDialog.FilterIndex < 2)
                    {
                        SaveTextFile(saveFileDialog.FileName);
                    }
                    else
                    {
                        SavePicture(saveFileDialog.FileName);
                    }
                }
                catch (IOException e)
                {
                    ShowErrorMessage(e.Message);
                }
                catch (Exception e)
                {
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
