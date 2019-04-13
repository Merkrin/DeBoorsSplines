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
        public void SetPointsList(MainWindow mainWindow, SplineCollection splineCollection)
        {
            for (int i = 0; i < splineCollection.PointsList.Count; i++)
            {
                mainWindow.PointsListBox.Items.Add(splineCollection.PointsList[i]);
            }

            mainWindow.ParameterTextBox.Text = splineCollection.Parameter.ToString();
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
