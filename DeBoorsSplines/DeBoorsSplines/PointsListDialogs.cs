using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointsLibrary;

namespace DeBoorsSplines
{
    public class PointsListDialogs
    {
        public void SetPointsList(MainWindow mainWindow, FileParser fileParser)
        {
            for (int i = 0; i < fileParser.PointsList.Count; i++)
            {
                mainWindow.PointsListBox.Items.Add(fileParser.PointsList[i]);
            }
        }

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
