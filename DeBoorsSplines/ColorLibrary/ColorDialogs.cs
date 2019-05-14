using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
//using System.Drawing;

namespace ColorLibrary
{
    public class ColorDialogs
    {
        private static GradientColorClass gradientColorClass = new GradientColorClass();

        public bool CheckColor(string line)
        {
            string[] colors = line.Split(',');

                return colors.Length == 3 && CheckNumber(colors[0]) && CheckNumber(colors[1]) && CheckNumber(colors[2]);
        }

        private bool CheckNumber(string line)
        {
            return int.TryParse(line, out int x) && x >= 0 && x <= 255;
        }

        public List<Color> SetColors(string startingColor, string endingColor, int partsAmount)
        {
            string[] rgbs = startingColor.Split(',');

            if (partsAmount < 3)
            {
                List<Color> returnedColors = new List<Color>
                {
                    Color.FromRgb(byte.Parse(rgbs[0]), byte.Parse(rgbs[1]), byte.Parse(rgbs[2]))
                };

                rgbs = endingColor.Split(',');

                returnedColors.Add(Color.FromRgb(byte.Parse(rgbs[0]), byte.Parse(rgbs[1]), byte.Parse(rgbs[2])));

                return returnedColors;
            }

            List<Color> colors = new List<Color>();

            rgbs = startingColor.Split(',');
            string[] secondRgbs = endingColor.Split(',');

            gradientColorClass.SetColorList(colors, Color.FromRgb(byte.Parse(rgbs[0]), byte.Parse(rgbs[1]), byte.Parse(rgbs[2])),
                    Color.FromRgb(byte.Parse(secondRgbs[0]), byte.Parse(secondRgbs[1]), byte.Parse(secondRgbs[2])), partsAmount);

            return colors;
        }
    }
}
