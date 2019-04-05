using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PointsLibrary
{
    public class FileParser
    {
        private const string allowedCharacters = "1234567890 ";

        public List<Point> ReadFile(string filePath)
        {
            List<Point> points = new List<Point>();

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line = streamReader.ReadLine();

                if (line == null)
                {
                    throw new FileException("Файл пуст!");
                }

                if (line != "N:")
                {
                    throw new FileException("Файл должен начинаться якорем \"N:\"!");
                }

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (CheckLine(line))
                    {
                        string xCoordinate = line.Split(' ')[0],
                            yCoordinate = line.Split(' ')[1];
                        if (CheckCoordinates(xCoordinate, yCoordinate))
                        {
                            points.Add(new Point(int.Parse(xCoordinate), int.Parse(yCoordinate)));
                        }
                        else
                        {
                            throw new FileException("Координаты некорректны!");
                        }
                    }
                    else
                    {
                        throw new FileException("Строка координат некорректна!");
                    }
                }
            }

            return points;
        }

        private bool CheckLine(string line)
        {
            return line.All(c => allowedCharacters.Contains(c)) &&
                line.Split(' ').Length == 2;
        }

        private bool CheckCoordinates(string x, string y)
        {
            return int.TryParse(x, out int temporary) && int.TryParse(y, out temporary);
        }
    }
}
