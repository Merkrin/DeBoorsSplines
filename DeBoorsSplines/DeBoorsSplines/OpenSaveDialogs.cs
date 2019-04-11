using Microsoft.Win32;
using PointsLibrary;

namespace DeBoorsSplines
{
    class OpenSaveDialogs
    {
        private static PointsListDialogs pointsListDialogs = new PointsListDialogs();

        public void OpenFile(MainWindow mainWindow, FileParser fileParser)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|DeBoorsSplines" +
                " Files (*.dbsp)|*.dbsp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                fileParser.ReadFile(openFileDialog.FileName);

                pointsListDialogs.SetPointsList(mainWindow, fileParser);
            }
        }
    }
}
