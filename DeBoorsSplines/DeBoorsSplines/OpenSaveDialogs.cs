using Microsoft.Win32;
using PointsLibrary;
using System;
using System.IO;
using System.Windows;

namespace DeBoorsSplines
{
    class OpenSaveDialogs
    {
        private static PointsListDialogs pointsListDialogs = new PointsListDialogs();

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
                }
                catch(FileException e)
                {
                    ShowErrorMessage(e.Message);
                }catch(IOException e)
                {
                    ShowErrorMessage(e.Message);
                }catch(Exception e)
                {
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
