using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ColorLibrary
{
    public class ColorDialogs
    {
        private static GradientColorClass gradientColorClass = new GradientColorClass();

        public bool CheckColor(string line)
        {
            return (line[0] == '#' && line.Length == 7 
                && int.TryParse(line.Substring(1), 
                System.Globalization.NumberStyles.HexNumber,
                null, out int color)) || (line.Length == 6 && int.TryParse(line,
                System.Globalization.NumberStyles.HexNumber,
                null, out color));
        }

        public List<Color> SetColors(string startingColor, string endingColor, int partsAmount)
        {
            if(partsAmount < 3)
            {
                return new List<Color>
                {
                    ColorTranslator.FromHtml(startingColor),
                    ColorTranslator.FromHtml(endingColor)
                };
            }

            List<Color> colors = new List<Color>();

            gradientColorClass.SetColorList(colors, ColorTranslator.FromHtml(startingColor),
                    ColorTranslator.FromHtml(endingColor), partsAmount);

            return colors;
        }
    }
}
