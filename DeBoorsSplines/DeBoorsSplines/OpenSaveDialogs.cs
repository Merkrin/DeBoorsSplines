using Microsoft.Win32;
using PointsLibrary;
using System;
using System.IO;
using System.Windows;

namespace DeBoorsSplines
{
    public delegate void OnPointsRenewed(MainWindow mainWindow, SplineCollection splineCollection);

    class OpenSaveDialogs
    {
        private static PointsListDialogs pointsListDialogs = new PointsListDialogs();
        public OnPointsRenewed OnPointsRenewer;
        private static DrawingClass drawingClass = new DrawingClass();

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
                catch(FileException e)
                {
                    OnPointsRenewer = null;
                    ShowErrorMessage(e.Message);
                }catch(IOException e)
                {
                    OnPointsRenewer = null;
                    ShowErrorMessage(e.Message);
                }catch(Exception e)
                {
                    OnPointsRenewer = null;
                    ShowErrorMessage(e.Message);
                }
            }
        }

        public void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Произошла ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
