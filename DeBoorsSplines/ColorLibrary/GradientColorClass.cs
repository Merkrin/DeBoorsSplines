using System;
using System.Collections.Generic;
using System.Windows.Media;
//using System.Drawing;

namespace ColorLibrary
{
    class GradientColorClass
    {
        internal void SetColorList(List<Color> colorList, Color start,
            Color end, int iterations)
        {
            SetGradientColors(colorList, start, end, iterations, 0, iterations - 1);
        }

        public void SetGradientColors(List<Color> colorList, Color startingColor,
            Color endingColor, int iterations, int firstIteration, int lastIteration)
        {
            if (iterations <= 0 || firstIteration < 0 || lastIteration > iterations - 1)
            {
                throw new ArgumentOutOfRangeException("Непредвиднная ошибка" +
                    " вычисления градиента");
            }
            // Проверка на неправильные данные. На всякий случай.

            double aStep = (endingColor.A - startingColor.A) / iterations;
            double rStep = (endingColor.R - startingColor.R) / iterations;
            double gStep = (endingColor.G - startingColor.G) / iterations;
            double bStep = (endingColor.B - startingColor.B) / iterations;
            // Подсчёт шага цвета по каждому из разложений.

            for (int i = firstIteration; i < lastIteration; i++)
            {
                var a = startingColor.A + (int)(aStep * i);
                var r = startingColor.R + (int)(rStep * i);
                var g = startingColor.G + (int)(gStep * i);
                var b = startingColor.B + (int)(bStep * i);
                // Подсчёт каждого следующего цвета в разложении.

                try
                {
                    colorList.Add(Color.FromArgb(Convert.ToByte(a), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b)));
                    // Соединение цвета.
                }
                catch (ArgumentException)
                {
                    if (a > 255)
                    {
                        a = 255;
                    }
                    else if (a < 0)
                    {
                        a = 0;
                    }

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

                    colorList.Add(Color.FromArgb(Convert.ToByte(a), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b)));
                }
                // Если вдруг какое-то из разложений порождает исключение, оно
                // принимает ближайшее возможное значение.
            }

            colorList.Add(endingColor);
            // Добавление полученного цвета.
        }
    }
}
