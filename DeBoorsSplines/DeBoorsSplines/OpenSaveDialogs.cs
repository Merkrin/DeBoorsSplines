using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointsLibrary;

namespace DeBoorsSplines
{
    class OpenSaveDialogs
    {
        public List<Point> OpenFile(FileParser fileParser)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                return fileParser.ReadFile(openFileDialog.FileName);
            }

            return null;
        }
    }
}
