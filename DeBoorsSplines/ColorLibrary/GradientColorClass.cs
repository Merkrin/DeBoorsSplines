using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ColorLibrary
{
    /// <summary>
    /// Класс математической логики, связанной с градиентом цвета.
    /// </summary>
    internal class GradientColorClass
    {
        // Метод перехода к получению списка цветов.
        internal void SetColorList(List<Color> colorList, Color start,
            Color end, int iterations)
        {
            SetGradientColors(colorList, start, end, iterations, 0, iterations - 1);
        }

        // Метод получения списка цветов.
        private void SetGradientColors(List<Color> colorList, Color startingColor,
            Color endingColor, int iterations, int firstIteration, int lastIteration)
        {
            // Проверка на неправильные данные на всякий случай.
            if (iterations <= 0 || firstIteration < 0 || lastIteration > iterations - 1)
            {
                throw new ArgumentOutOfRangeException("Непредвиднная ошибка" +
                    " вычисления градиента");
            }

            // Подсчёт шага для каждого разложения цвета.
            double rStep = (endingColor.R - startingColor.R) / iterations;
            double gStep = (endingColor.G - startingColor.G) / iterations;
            double bStep = (endingColor.B - startingColor.B) / iterations;

            for (int i = firstIteration; i < lastIteration; i++)
            {
                // Подсчёт следующего цвета.
                var r = startingColor.R + (int)(rStep * i);
                var g = startingColor.G + (int)(gStep * i);
                var b = startingColor.B + (int)(bStep * i);

                try
                {
                    colorList.Add(Color.FromRgb(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b)));
                }
                catch (ArgumentException)
                {
                    // Если вдруг какое-то из разложений порождает исключение,
                    // ему присваивается ближайшее краевое значение.

                    if (r > 255)
                    {
                        r = 255;
                    }
                    else if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    else if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    else if (b < 0)
                    {
                        b = 0;
                    }

                    colorList.Add(Color.FromRgb(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b)));
                }
            }

            colorList.Add(endingColor);
        }
    }
}
