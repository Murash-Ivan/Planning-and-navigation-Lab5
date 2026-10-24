using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace Lab5
{
    public partial class Form1 : Form
    {
        //параметры карты
        private const float MAP_WIDTH_METERS = 16.0f;  // 16 метров
        private const float MAP_HEIGHT_METERS = 10.0f; // 10 метров
        private const int SCALE = 30;                  // 1 метр = 30 пикселей

        //список вершин препядствия
        private List<List<PointF>> obstacles = new List<List<PointF>>();

        //точки старта и финиша
        private PointF? startPoint = null;
        private PointF? endPoint = null;

        //параметры робота
        private double robotDiameter = 0.6; //диаметр в метрах
        private PointF? robotPosition = null; //координаты робота (X, Y) в метрах
        private float robotCourse = 0.0f;     //курс в градусах

        //данные для графа видимости
        private List<PointF> graphNodes = new List<PointF>(); //все узлы графа
        private List<List<float>> adjacencyMatrix; //матрица смежности (расстояния между узлами)
        private List<List<bool>> visibilityMatrix; //матрица видимости (true = видны друг другу)
        private List<int> shortestPath; //индексы узлов найденного пути
        private List<int> touchedObstacles = new List<int>(); //индексы препятствий, с которыми был контакт
        public Form1()
        {
            InitializeComponent();

            pictureBoxMap.Width = (int)(MAP_WIDTH_METERS * SCALE);
            pictureBoxMap.Height = (int)(MAP_HEIGHT_METERS * SCALE);
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            if (openFileDialogMap.ShowDialog() == DialogResult.OK)
            {
                LoadMapFromFile(openFileDialogMap.FileName);
                DrawMap();
            }
        }

        //чтение карты из файла
        private void LoadMapFromFile(string filePath)
        {
            obstacles.Clear();
            listBoxRawData.Items.Clear();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // вывод строки в листбокс
                    listBoxRawData.Items.Add(line);

                    List<PointF> polygon = new List<PointF>();

                    // разделение строки по точке с запятой
                    string[] pointsStr = line.Split(';');

                    foreach (string pStr in pointsStr)
                    {
                        //разделение координаты X и Y по запятой
                        string[] coords = pStr.Split(',');
                        if (coords.Length == 2)
                        {
                            float x = float.Parse(coords[0].Trim(), CultureInfo.InvariantCulture);
                            float y = float.Parse(coords[1].Trim(), CultureInfo.InvariantCulture);

                            polygon.Add(new PointF(x, y));
                        }
                    }

                    if (polygon.Count > 0)
                    {
                        obstacles.Add(polygon);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //отрисовка карты
        private void DrawMap()
        {
            int imgWidth = (int)(MAP_WIDTH_METERS * SCALE);
            int imgHeight = (int)(MAP_HEIGHT_METERS * SCALE);
            Bitmap bmp;

            //нужно ли строить пространство конфигураций
            if (chkShowConfigSpace.Checked && obstacles.Count > 0)
            {
                bmp = BuildConfigSpaceMap();
            }
            else
            {
                bmp = new Bitmap(imgWidth, imgHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    using (SolidBrush grayBrush = new SolidBrush(Color.Gray))
                    using (Pen blackPen = new Pen(Color.Black, 1))
                    {
                        foreach (var polygon in obstacles)
                        {
                            PointF[] screenPolygon = new PointF[polygon.Count];
                            for (int i = 0; i < polygon.Count; i++)
                            {
                                screenPolygon[i] = ToScreenCoordinates(polygon[i]);
                            }
                            g.FillPolygon(grayBrush, screenPolygon);
                            g.DrawPolygon(blackPen, screenPolygon);
                        }
                    }
                }
            }

            //рисую старт, финиш и робота поверх карты
            using (Graphics g = Graphics.FromImage(bmp))
            {
                if (startPoint.HasValue) DrawCircle(g, startPoint.Value, Color.Green);
                if (endPoint.HasValue) DrawCircle(g, endPoint.Value, Color.Blue);
                DrawRobot(g);

                DrawVisibilityGraph(g);
            }

            pictureBoxMap.Image = bmp;
        }

        private PointF ToScreenCoordinates(PointF p)
        {
            float screenX = p.X * SCALE;
            float screenY = (MAP_HEIGHT_METERS - p.Y) * SCALE;

            return new PointF(screenX, screenY);
        }

        private PointF ToMapCoordinates(PointF screenPoint)
        {
            float mapX = screenPoint.X / SCALE;
            float mapY = MAP_HEIGHT_METERS - (screenPoint.Y / SCALE);
            return new PointF(mapX, mapY);
        }

        //отрисовка кругов старта и финиша
        private void DrawCircle(Graphics g, PointF mapPoint, Color color)
        {
            PointF screenPoint = ToScreenCoordinates(mapPoint);
            int radius = 5;

            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, screenPoint.X - radius, screenPoint.Y - radius, radius * 2, radius * 2);
            }
            using (Pen pen = new Pen(Color.Black, 1))
            {
                g.DrawEllipse(pen, screenPoint.X - radius, screenPoint.Y - radius, radius * 2, radius * 2);
            }
        }
        private void pictureBoxMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointF mapPoint = ToMapCoordinates(new PointF(e.X, e.Y));

            if (mapPoint.X < 0 || mapPoint.X > MAP_WIDTH_METERS ||
                mapPoint.Y < 0 || mapPoint.Y > MAP_HEIGHT_METERS)
                return;

            if (e.Button == MouseButtons.Left)
            {
                startPoint = mapPoint;
                txtStartX.Text = mapPoint.X.ToString("F2", CultureInfo.InvariantCulture);
                txtStartY.Text = mapPoint.Y.ToString("F2", CultureInfo.InvariantCulture);
            }
            else if (e.Button == MouseButtons.Right)
            {
                endPoint = mapPoint;
                txtEndX.Text = mapPoint.X.ToString("F2", CultureInfo.InvariantCulture);
                txtEndY.Text = mapPoint.Y.ToString("F2", CultureInfo.InvariantCulture);
            }

            DrawMap();
        }

        private void btnApplyStart_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtStartX.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float x) &&
                float.TryParse(txtStartY.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float y))
            {
                startPoint = new PointF(x, y);
                DrawMap();
            }
        }

        private void btnApplyEnd_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtEndX.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float x) &&
                float.TryParse(txtEndY.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float y))
            {
                endPoint = new PointF(x, y);
                DrawMap();
            }
        }

        //отрисовка робота на карте
        private void DrawRobot(Graphics g)
        {
            if (!robotPosition.HasValue) return;

            PointF screenPos = ToScreenCoordinates(robotPosition.Value);

            float radiusPx = (float)(robotDiameter / 2.0 * SCALE);

            using (SolidBrush brush = new SolidBrush(Color.Orange))
            {
                g.FillEllipse(brush, screenPos.X - radiusPx, screenPos.Y - radiusPx, radiusPx * 2, radiusPx * 2);
            }
            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawEllipse(pen, screenPos.X - radiusPx, screenPos.Y - radiusPx, radiusPx * 2, radiusPx * 2);
            }

            double rad = robotCourse * Math.PI / 180.0;
            float dx = (float)Math.Sin(rad);
            float dy = (float)-Math.Cos(rad);

            float arrowLen = radiusPx * 1.5f;
            float endX = screenPos.X + dx * arrowLen;
            float endY = screenPos.Y + dy * arrowLen;

            using (Pen arrowPen = new Pen(Color.Black, 3))
            {
                g.DrawLine(arrowPen, screenPos.X, screenPos.Y, endX, endY);
            }
        }

        private void btnApplyRobot_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtRobotDiameter.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double d) && d > 0)
            {
                robotDiameter = d;
            }
            else
            {
                MessageBox.Show("Неверный диаметр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (float.TryParse(txtRobotX.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float x) &&
                float.TryParse(txtRobotY.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float y) &&
                float.TryParse(txtRobotCourse.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float course))
            {
                if (x >= 0 && x <= MAP_WIDTH_METERS && y >= 0 && y <= MAP_HEIGHT_METERS)
                {
                    robotPosition = new PointF(x, y);
                    robotCourse = course;
                    DrawMap();
                }
                else
                {
                    MessageBox.Show($"Координаты вне поля X: 0-{MAP_WIDTH_METERS}, Y: 0-{MAP_HEIGHT_METERS}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Неверный формат чисел", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //находится ли точка внутри полигона
        private bool IsPointInPolygon(PointF p, List<PointF> polygon)
        {
            bool result = false;
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < p.Y && polygon[j].Y >= p.Y || polygon[j].Y < p.Y && polygon[i].Y >= p.Y)
                {
                    if (polygon[i].X + (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < p.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        //расстояние от точки до отрезка
        private float DistancePointToSegment(PointF p, PointF v, PointF w)
        {
            float l2 = (v.X - w.X) * (v.X - w.X) + (v.Y - w.Y) * (v.Y - w.Y);
            if (l2 == 0) return (float)Math.Sqrt((p.X - v.X) * (p.X - v.X) + (p.Y - v.Y) * (p.Y - v.Y));

            float t = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / l2;
            t = Math.Max(0, Math.Min(1, t)); // Ограничиваем проекцию пределами отрезка

            PointF projection = new PointF(v.X + t * (w.X - v.X), v.Y + t * (w.Y - v.Y));
            return (float)Math.Sqrt((p.X - projection.X) * (p.X - projection.X) + (p.Y - projection.Y) * (p.Y - projection.Y));
        }

        private Bitmap BuildConfigSpaceMap()
        {
            int imgWidth = (int)(MAP_WIDTH_METERS * SCALE);
            int imgHeight = (int)(MAP_HEIGHT_METERS * SCALE);
            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            float robotRadius = (float)(robotDiameter / 2.0);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                //заливаю всё зеленым
                g.Clear(Color.LightGreen);

                //рисую препятствия серым цветом
                using (SolidBrush grayBrush = new SolidBrush(Color.Gray))
                using (Pen blackPen = new Pen(Color.Black, 1))
                {
                    foreach (var poly in obstacles)
                    {
                        PointF[] screenPolygon = new PointF[poly.Count];
                        for (int i = 0; i < poly.Count; i++)
                        {
                            screenPolygon[i] = ToScreenCoordinates(poly[i]);
                        }
                        g.FillPolygon(grayBrush, screenPolygon);
                        g.DrawPolygon(blackPen, screenPolygon);
                    }
                }

                //прохожу по каждому пикселю и ищем раздутую зону
                for (int py = 0; py < imgHeight; py++)
                {
                    for (int px = 0; px < imgWidth; px++)
                    {
                        PointF mapPoint = ToMapCoordinates(new PointF(px, py));

                        //находится ли точка внутри исходного препятствия
                        bool isInsideObstacle = false;
                        foreach (var poly in obstacles)
                        {
                            if (IsPointInPolygon(mapPoint, poly))
                            {
                                isInsideObstacle = true;
                                break;
                            }
                        }

                        //если точка внутри препятствия  пропускаю
                        if (isInsideObstacle) continue;

                        //проверяю расстояние до препятствия
                        bool isInExpandedZone = false;
                        foreach (var poly in obstacles)
                        {
                            for (int i = 0; i < poly.Count; i++)
                            {
                                PointF p1 = poly[i];
                                PointF p2 = poly[(i + 1) % poly.Count];

                                float dist = DistancePointToSegment(mapPoint, p1, p2);

                                //если расстояние меньше радиуса робота это раздутая зона
                                if (dist < robotRadius)
                                {
                                    isInExpandedZone = true;
                                    break;
                                }
                            }
                            if (isInExpandedZone) break;
                        }

                        //закрашиваю раздутую зону красным
                        if (isInExpandedZone)
                        {
                            bmp.SetPixel(px, py, Color.FromArgb(200, Color.Red));
                        }
                    }
                }
            }
            return bmp;
        }

        private void chkShowConfigSpace_CheckedChanged(object sender, EventArgs e)
        {
            if (obstacles.Count > 0)
            {
                DrawMap();
            }
        }

        // Сбор всех узлов графа вершины препятствий старт финиш
        private void CollectGraphNodes()
        {
            graphNodes.Clear();

            //добавляю все вершины препятствий
            foreach (var poly in obstacles)
            {
                foreach (var vertex in poly)
                {
                    //проверяю нет ли уже такой точки
                    bool exists = false;
                    foreach (var node in graphNodes)
                    {
                        if (Math.Abs(node.X - vertex.X) < 0.01f && Math.Abs(node.Y - vertex.Y) < 0.01f)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists) graphNodes.Add(vertex);
                }
            }

            //добавляю старт и финиш
            if (startPoint.HasValue)
            {
                graphNodes.Add(startPoint.Value);
            }
            if (endPoint.HasValue)
            {
                graphNodes.Add(endPoint.Value);
            }
        }

        //пересекает ли отрезок препятствия
        private bool SegmentIntersectsObstacle(PointF a, PointF b, List<PointF> obstacle)
        {
            //если середина отрезка внутри препятствия, отрезок проходит сквозь.
            PointF mid = new PointF((a.X + b.X) / 2f, (a.Y + b.Y) / 2f);
            if (IsPointInPolygon(mid, obstacle))
            {
                return true;
            }

            //пересечение с гранями, игнорирую случаи когда отрезок просто касается вершины
            for (int i = 0; i < obstacle.Count; i++)
            {
                PointF p1 = obstacle[i];
                PointF p2 = obstacle[(i + 1) % obstacle.Count];

                //если конец отрезка совпадает с вершиной препятствия это не считается пересечением
                if (IsPointsEqual(a, p1) || IsPointsEqual(a, p2) || IsPointsEqual(b, p1) || IsPointsEqual(b, p2))
                {
                    continue;
                }

                if (SegmentsIntersect(a, b, p1, p2))
                {
                    return true;
                }
            }
            return false;
        }

        //проверка совпадения точек с учётом погрешности
        private bool IsPointsEqual(PointF p1, PointF p2)
        {
            return Math.Abs(p1.X - p2.X) < 0.01f && Math.Abs(p1.Y - p2.Y) < 0.01f;
        }

        //проверяю пересечения двух отрезков
        private bool SegmentsIntersect(PointF a, PointF b, PointF c, PointF d)
        {
            float d1 = Direction(c, d, a);
            float d2 = Direction(c, d, b);
            float d3 = Direction(a, b, c);
            float d4 = Direction(a, b, d);

            float eps = 0.001f;

            if (((d1 > eps && d2 < -eps) || (d1 < -eps && d2 > eps)) &&
                ((d3 > eps && d4 < -eps) || (d3 < -eps && d4 > eps)))
            {
                return true;
            }

            return false;
        }


        private float Direction(PointF pi, PointF pj, PointF pk)
        {
            return (pk.X - pi.X) * (pj.Y - pi.Y) - (pj.X - pi.X) * (pk.Y - pi.Y);
        }


        // Построение матриц смежности и видимости
        private void BuildGraphMatrices()
        {
            int n = graphNodes.Count;
            adjacencyMatrix = new List<List<float>>();
            visibilityMatrix = new List<List<bool>>();

            //получаю текущий радиус робота
            float robotRadius = (float)(robotDiameter / 2.0);

            for (int i = 0; i < n; i++)
            {
                adjacencyMatrix.Add(new List<float>());
                visibilityMatrix.Add(new List<bool>());

                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        adjacencyMatrix[i].Add(0);
                        visibilityMatrix[i].Add(false);
                    }
                    else
                    {
                        //вычисляю расстояние между узлами i и j
                        float dist = DistanceBetween(graphNodes[i], graphNodes[j]);
                        adjacencyMatrix[i].Add(dist);

                        //проверяю видимость в зависимости от выбранного режима
                        bool isVisible;

                        if (chkUseConfigSpace.Checked)
                        {
                            //режим пространста конфигурации проверяю с учётом радиуса робота
                            isVisible = IsSegmentSafeInCSpace(graphNodes[i], graphNodes[j], robotRadius);
                        }
                        else
                        {
                            //обычный режим проверяю пересечение с препятствиями
                            isVisible = true;
                            foreach (var obstacle in obstacles)
                            {
                                if (SegmentIntersectsObstacle(graphNodes[i], graphNodes[j], obstacle))
                                {
                                    isVisible = false;
                                    break;
                                }
                            }
                        }

                        visibilityMatrix[i].Add(isVisible);
                    }
                }
            }
        }

        //расстояние между двумя точками
        private float DistanceBetween(PointF a, PointF b)
        {
            return (float)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        private void btnBuildGraph_Click(object sender, EventArgs e)
        {
            if (!startPoint.HasValue || !endPoint.HasValue)
            {
                MessageBox.Show("Сначала укажите старт и финиш", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CollectGraphNodes();
            BuildGraphMatrices();

            MessageBox.Show($"Граф построен: {graphNodes.Count} узлов", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cmbVisualizationStep.SelectedIndex = 0;
            DrawMap();
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            if (graphNodes.Count == 0)
            {
                MessageBox.Show("Сначала постройте граф", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //находжу индексы старта и финиша
            int startIndex = graphNodes.Count - 2;
            int endIndex = graphNodes.Count - 1;

            //запускаю алгоритм Дейкстры
            Dijkstra(startIndex, endIndex);

            cmbVisualizationStep.SelectedIndex = 2;
            DrawMap();
        }

        private void Dijkstra(int start, int end)
        {
            int n = graphNodes.Count;
            float[] dist = new float[n];
            int[] prev = new int[n];
            bool[] visited = new bool[n];

            for (int i = 0; i < n; i++)
            {
                dist[i] = float.MaxValue;
                prev[i] = -1;
                visited[i] = false;
            }

            dist[start] = 0;

            for (int i = 0; i < n; i++)
            {
                //нахожу узел с минимальным расстоянием
                int u = -1;
                float minDist = float.MaxValue;
                for (int j = 0; j < n; j++)
                {
                    if (!visited[j] && dist[j] < minDist)
                    {
                        minDist = dist[j];
                        u = j;
                    }
                }

                if (u == -1) break;
                visited[u] = true;

                //обновляю расстояния до соседей
                for (int v = 0; v < n; v++)
                {
                    if (!visited[v] && visibilityMatrix[u][v])
                    {
                        float cost = adjacencyMatrix[u][v];

                        //если включен штраф за близость к препятствиям
                        if (chkUsePenalty.Checked)
                        {
                            //проверяю проходит ли ребро вдоль препятствия
                            bool nearObstacle = false;
                            foreach (var obstacle in obstacles)
                            {
                                for (int k = 0; k < obstacle.Count; k++)
                                {
                                    PointF p1 = obstacle[k];
                                    PointF p2 = obstacle[(k + 1) % obstacle.Count];
                                    float distToSegment = DistancePointToSegment(
                                        new PointF((graphNodes[u].X + graphNodes[v].X) / 2,
                                                  (graphNodes[u].Y + graphNodes[v].Y) / 2),
                                        p1, p2);
                                    if (distToSegment < 0.2f)
                                    {
                                        nearObstacle = true;
                                        break;
                                    }
                                }
                                if (nearObstacle) break;
                            }

                            if (nearObstacle) cost *= 15f;
                        }

                        float newDist = dist[u] + cost;
                        if (newDist < dist[v])
                        {
                            dist[v] = newDist;
                            prev[v] = u;
                        }
                    }
                }
            }

            //восстанавливаю путь
            shortestPath = new List<int>();
            int current = end;
            while (current != -1)
            {
                shortestPath.Add(current);
                current = prev[current];
            }
            shortestPath.Reverse();

            //определяю препятствия с которыми был контакт
            touchedObstacles.Clear();
            float robotRadius = (float)(robotDiameter / 2.0);
            float tolerance = 0.15f;

            //прохожу по всем сегментам найденного пути
            for (int i = 0; i < shortestPath.Count - 1; i++)
            {
                PointF p1 = graphNodes[shortestPath[i]];
                PointF p2 = graphNodes[shortestPath[i + 1]];

                //беру середину отрезка пути для проверки
                PointF midPath = new PointF((p1.X + p2.X) / 2f, (p1.Y + p2.Y) / 2f);

                for (int obsIdx = 0; obsIdx < obstacles.Count; obsIdx++)
                {
                    for (int k = 0; k < obstacles[obsIdx].Count; k++)
                    {
                        PointF v1 = obstacles[obsIdx][k];
                        PointF v2 = obstacles[obsIdx][(k + 1) % obstacles[obsIdx].Count];

                        //считаю расстояние от середины пути до препятствия
                        float distToWall = DistancePointToSegment(midPath, v1, v2);

                        if (distToWall <= robotRadius + tolerance)
                        {
                            if (!touchedObstacles.Contains(obsIdx))
                            {
                                touchedObstacles.Add(obsIdx);
                            }
                            break;
                        }
                    }
                }
            }
        }

        //отрисовка графа и пути
        private void DrawVisibilityGraph(Graphics g)
        {
            if (graphNodes.Count == 0) return;

            int step = cmbVisualizationStep.SelectedIndex;

            // все связи
            if (step == 0)
            {
                using (Pen bluePen = new Pen(Color.Blue, 1))
                {
                    for (int i = 0; i < graphNodes.Count; i++)
                    {
                        for (int j = i + 1; j < graphNodes.Count; j++)
                        {
                            PointF p1 = ToScreenCoordinates(graphNodes[i]);
                            PointF p2 = ToScreenCoordinates(graphNodes[j]);
                            g.DrawLine(bluePen, p1, p2);
                        }
                    }
                }
            }
            //только видимые связи
            else if (step == 1 && visibilityMatrix != null)
            {
                using (Pen bluePen = new Pen(Color.Blue, 1))
                {
                    for (int i = 0; i < graphNodes.Count; i++)
                    {
                        for (int j = i + 1; j < graphNodes.Count; j++)
                        {
                            //рисую линию если есть видимость между узлами
                            if (visibilityMatrix[i][j])
                            {
                                PointF p1 = ToScreenCoordinates(graphNodes[i]);
                                PointF p2 = ToScreenCoordinates(graphNodes[j]);
                                g.DrawLine(bluePen, p1, p2);
                            }
                        }
                    }
                }
            }
            //найденный путь
            else if (step == 2 && shortestPath != null && shortestPath.Count > 1)
            {
                if (touchedObstacles.Count > 0)
                {
                    using (SolidBrush orangeBrush = new SolidBrush(Color.Orange))
                    using (Pen blackPen = new Pen(Color.Black, 1))
                    {
                        foreach (int obsIdx in touchedObstacles)
                        {
                            PointF[] screenPolygon = new PointF[obstacles[obsIdx].Count];
                            for (int i = 0; i < obstacles[obsIdx].Count; i++)
                            {
                                screenPolygon[i] = ToScreenCoordinates(obstacles[obsIdx][i]);
                            }
                            g.FillPolygon(orangeBrush, screenPolygon);
                            g.DrawPolygon(blackPen, screenPolygon);
                        }
                    }
                }

                using (Pen redPen = new Pen(Color.Red, 3))
                {
                    for (int i = 0; i < shortestPath.Count - 1; i++)
                    {
                        PointF p1 = ToScreenCoordinates(graphNodes[shortestPath[i]]);
                        PointF p2 = ToScreenCoordinates(graphNodes[shortestPath[i + 1]]);
                        g.DrawLine(redPen, p1, p2);
                    }
                }
            }
        }

        private void cmbVisualizationStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawMap();
        }

        //расчет курса от точки к точке
        private float GetCompassCourse(PointF from, PointF to)
        {
            float dx = to.X - from.X;
            float dy = to.Y - from.Y;

            double mathAngleRad = Math.Atan2(dy, dx);
            double mathAngleDeg = mathAngleRad * 180.0 / Math.PI;

            double compassAngle = (90.0 - mathAngleDeg) % 360.0;
            if (compassAngle < 0) compassAngle += 360.0;

            return (float)compassAngle;
        }

        private void btnSaveRoute_Click(object sender, EventArgs e)
        {
            // Проверяем, что маршрут найден
            if (shortestPath == null || shortestPath.Count < 2)
            {
                MessageBox.Show("Сначала найдите маршрут!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Открываем диалог сохранения файла
            if (saveFileDialogCSV.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialogCSV.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("Номер точки;Угол поворота;Пробег до след. точки (м);X (м);Y (м);Курс");

                        float previousCourse = 0f;

                        for (int i = 0; i < shortestPath.Count; i++)
                        {
                            int currentNodeIdx = shortestPath[i];
                            PointF currentPoint = graphNodes[currentNodeIdx];

                            float turnAngle = 0f;
                            float distance = 0f;
                            float currentCourse = previousCourse;

                            if (i < shortestPath.Count - 1)
                            {
                                int nextNodeIdx = shortestPath[i + 1];
                                PointF nextPoint = graphNodes[nextNodeIdx];

                                distance = DistanceBetween(currentPoint, nextPoint);

                                float targetCourse = GetCompassCourse(currentPoint, nextPoint);

                                if (i == 0)
                                {
                                    turnAngle = targetCourse;
                                }
                                else
                                {
                                    turnAngle = targetCourse - previousCourse;

                                    if (turnAngle > 180f) turnAngle -= 360f;
                                    if (turnAngle < -180f) turnAngle += 360f;
                                }

                                currentCourse = targetCourse;
                                previousCourse = currentCourse;
                            }
                            else
                            {
                                turnAngle = 0f;
                                distance = 0f;
                            }

                            string line = string.Format(CultureInfo.InvariantCulture,
                                "{0};{1:F2};{2:F2};{3:F2};{4:F2};{5:F2}",
                                i, turnAngle, distance, currentPoint.X, currentPoint.Y, currentCourse);

                            sw.WriteLine(line);
                        }
                    }
                    MessageBox.Show("Маршрут успешно сохранен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkUseConfigSpace_CheckedChanged(object sender, EventArgs e)
        {
            if (graphNodes.Count > 0 && startPoint.HasValue && endPoint.HasValue)
            {
                BuildGraphMatrices();
                DrawMap();
            }
        }

        //проверка безопасен ли отрезок с учетом радиуса робота
        private bool IsSegmentSafeInCSpace(PointF a, PointF b, float robotRadius)
        {
            //если отрезок пересекает внутренность любого препятствия он недопустим
            foreach (var obs in obstacles)
            {
                if (SegmentIntersectsObstacle(a, b, obs))
                {
                    return false;
                }
            }

            // если отрезок не пересекает препятствия, проверяю не проходит ли он ближе чем радиус робота
            PointF[] checkPoints = new PointF[]
            {
                a,
                new PointF((a.X + b.X) / 2f, (a.Y + b.Y) / 2f),
                b
            };

            foreach (PointF pt in checkPoints)
            {
                foreach (var obs in obstacles)
                {
                    for (int k = 0; k < obs.Count; k++)
                    {
                        PointF p1 = obs[k];
                        PointF p2 = obs[(k + 1) % obs.Count];

                        //считаю расстояние от точки до препятствия
                        float dist = DistancePointToSegment(pt, p1, p2);

                        //проверяю является ли точка вершиной препятствия
                        bool isEndpoint = IsPointsEqual(pt, a) || IsPointsEqual(pt, b);
                        bool isObstacleVertex = IsPointsEqual(pt, p1) || IsPointsEqual(pt, p2);

                        if (isEndpoint && isObstacleVertex)
                        {
                            continue;
                        }

                        if (dist < robotRadius - 0.05f)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
