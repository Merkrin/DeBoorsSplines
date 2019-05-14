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
                return line[0] == '#' && line.Length == 7 &&
                int.TryParse(line.Substring(1),
                    System.Globalization.NumberStyles.HexNumber,
                    null, out int color) || line.Length == 6 && int.TryParse(line,
                    System.Globalization.NumberStyles.HexNumber,
                    null, out color);
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
