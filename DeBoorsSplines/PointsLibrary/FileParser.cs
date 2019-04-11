using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PointsLibrary
{
    public class FileParser
    {
        public double Parameter { private set; get; }

        private const string allowedCharacters = "1234567890 ";

        public List<Point> PointsList { set; get; }

        public void ReadFile(string filePath)
        {
            PointsList = new List<Point>();

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line = streamReader.ReadLine();

                if (line == null)
                {
                    throw new FileException("Файл пуст!");
                }

                if (!Regex.IsMatch(line, "^[0-9]+:$"))
                {
                    throw new FileException("Файл должен начинаться якорем \"N:\"!");
                }

                int coordinatesAmount = int.Parse(line.Substring(0, line.IndexOf(':')));

                if (coordinatesAmount < 4)
                {
                    throw new FileException("Для построения кубического сплайна" +
                        " требуется не менее 4 пар координат!");
                }

                for (int i = 0; i < coordinatesAmount; i++)
                {
                    line = streamReader.ReadLine();

                    if (line != null)
                    {
                        if (CheckLine(line))
                        {
                            string xCoordinate = line.Split(' ')[0],
                                yCoordinate = line.Split(' ')[1];

                            if (CheckCoordinates(xCoordinate, yCoordinate))
                            {
                                try
                                {
                                    PointsList.Add(new Point(int.Parse(xCoordinate), int.Parse(yCoordinate)));
                                }
                                catch (OutOfMemoryException)
                                {
                                    throw new FileException("Too many coordinates!");
                                }
                            }
                            else
                            {
                                throw new FileException(i + 1 + "-ая пара координат некорректна!");
                            }
                        }
                        else
                        {
                            throw new FileException(i + 2 + "-ая строка некорректна!");
                        }
                    }
                }

                line = streamReader.ReadLine();

                if (line != "T:")
                {
                    throw new FileException("Требуется якорь \"T:\"!");
                }

                line = streamReader.ReadLine();

                if (double.TryParse(line, out double t))
                {
                    Parameter = t;
                }
                else
                {
                    throw new FileException("Значение параметра некорректно!");
                }
            }
        }

        private bool CheckLine(string line)
        {
            return line.All(c => allowedCharacters.Contains(c)) &&
                line.Split(' ').Length == 2;
        }

        public bool CheckCoordinates(string x, string y)
        {
            return int.TryParse(x, out int temporary) && int.TryParse(y, out temporary);
        }
    }
}
