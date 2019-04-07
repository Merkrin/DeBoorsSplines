# DeBoorsSplines
Программа, позволяющая пользователю быстро построить сплайн на своём персональном компьютере, чтобы продолжить работу, имея полученную информацию.

## Как использовать программу
Есть несколько способов воспользоваться программой. Рассмотрим их.
1. Загрузить данные из файла;
   - Открыть в программе файл ```.txt``` вида:
   ```
      N:
      x1 y1
      x2 y2
      ...
      xn yn
      T:
      t
   ```
   (где "N" - число пар координат на плоскости, "xn yn" - целочисленные пары координат, а "t" - параметр, отвечающий за шаг вектора узлов) или сохранённый программой файл ```.dbspl```;
2. Воспользоваться возможностью добавлять точки на рабочем пространстве;
3. Воспользоваться возможностью добавлять точки в список по координатам.

Use:
Regex.IsMatch(s, "^\\d+:$");
