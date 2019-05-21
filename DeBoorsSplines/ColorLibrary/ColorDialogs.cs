using System.Collections.Generic;
using System.Windows.Media;

namespace ColorLibrary
{
    /// <summary>
    /// Класс взаимодействия с пользователем относительно градиента для окраски
    /// сплайна.
    /// </summary>
    public class ColorDialogs
    {
        // Экземпляр класса GradientColorClass.
        private static GradientColorClass gradientColorClass = new GradientColorClass();

        /// <summary>
        /// Проверка строки с цветом на соответствие правилам.
        /// </summary>
        /// <param name="line">
        /// Строка, содаержащая код цвета.
        /// </param>
        /// <returns>
        /// Соответствует ли строка коду цвета (без проверки чисел).
        /// </returns>
        public bool CheckColor(string line)
        {
            string[] colors = line.Split(',');

            return colors.Length == 3 && CheckNumber(colors[0]) &&
                CheckNumber(colors[1]) && CheckNumber(colors[2]);
        }

        // Проверка числа цветовой характеристики на соответствие правилу.
        private bool CheckNumber(string line)
        {
            return int.TryParse(line, out int x) && x >= 0 && x <= 255;
        }

        /// <summary>
        /// Метод получения списка цветов градиента.
        /// </summary>
        /// <param name="startingColor">
        /// Строка, содержащая код начального цвета.
        /// </param>
        /// <param name="endingColor">
        /// Строка, содержащая код конечного цвета.
        /// </param>
        /// <param name="elementarySplineAmount">
        /// Количество элементарных сплайнов в составном.
        /// </param>
        /// <returns>
        /// Список цветов градиента.
        /// </returns>
        public List<Color> SetColors(string startingColor, string endingColor,
            int elementarySplineAmount)
        {
            string[] rgbs = startingColor.Split(',');

            if (elementarySplineAmount < 3)
            {
                List<Color> returnedColors = new List<Color>
                {
                    Color.FromRgb(byte.Parse(rgbs[0]), byte.Parse(rgbs[1]),
                    byte.Parse(rgbs[2]))
                };

                rgbs = endingColor.Split(',');

                if (elementarySplineAmount == 2)
                {
                    returnedColors.Add(Color.FromRgb(byte.Parse(rgbs[0]),
                        byte.Parse(rgbs[1]), byte.Parse(rgbs[2])));
                }

                return returnedColors;
            }

            List<Color> colors = new List<Color>();

            rgbs = startingColor.Split(',');
            string[] secondRgbs = endingColor.Split(',');

            gradientColorClass.SetColorList(colors,
                Color.FromRgb(byte.Parse(rgbs[0]), byte.Parse(rgbs[1]),
                byte.Parse(rgbs[2])), Color.FromRgb(byte.Parse(secondRgbs[0]),
                byte.Parse(secondRgbs[1]), byte.Parse(secondRgbs[2])),
                elementarySplineAmount);

            return colors;
        }
    }
}
