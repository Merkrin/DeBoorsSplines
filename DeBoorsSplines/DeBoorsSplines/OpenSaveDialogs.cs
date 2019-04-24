﻿using Microsoft.Win32;
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
    public delegate void OnPointsRenewed(MainWindow mainWindow, SplineCollection splineCollection);

    /// <summary>
    /// Класс взаимодействия с диалоговыми окнами.
    /// </summary>
    public class OpenSaveDialogs
    {
        private static PointsListDialogs pointsListDialogs = new PointsListDialogs();
        public OnPointsRenewed OnPointsRenewer;
        private static DrawingClass drawingClass = new DrawingClass();

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
        public void OpenFile(MainWindow mainWindow, FileParser fileParser, SplineCollection splineCollection)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|DeBoorsSplines" +
                " Files (*.dbsp)|*.dbsp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileParser.ReadFile(openFileDialog.FileName, splineCollection);
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
