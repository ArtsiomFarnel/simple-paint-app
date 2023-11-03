using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Paint
{
    public class Paint : Form
    {
        private TrackBar BrushBar;
        private PictureBox PictureBox;
        private Color MainColor = Color.Black;
        private Point FirstLocation = Point.Empty, SecondLocation = Point.Empty;
        private int NumberOfTool, BrushSize = 1;
        private bool IsMouseDown = new bool();
        List<Point> list = new List<Point>();
        Bitmap bmp;

        public Paint()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Console.WriteLine("Color and Brush values set as default - " + MainColor.Name + "; " + BrushSize);

            //Инициализация окна
            StartPosition = FormStartPosition.Manual;
            Location = new Point(10, 15);
            Size = new Size(1280, 720);
            Icon = new Icon("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\egor.ico");
            Name = "Paint";
            Text = "Paint";

            //Добавление цветов
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\brush.png"),
                            0, 0, 50, 50, "brush"));
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\erase.png"),
                            0, 50, 50, 50, "erase"));
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\line.png"),
                            0, 100, 50, 50, "line"));
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\square.png"),
                            0, 150, 50, 50, "square"));
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\oval.png"),
                            0, 200, 50, 50, "oval"));
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\fill.png"),
                            0, 250, 50, 50, "fill"));
            Controls.Add(AddToolButton(new Button(),
                            new Bitmap("C:\\Users\\Govnuck\\Desktop\\Paint\\Paint\\Paint\\src\\clean.png"),
                            0, 300, 50, 50, "clean"));

            //Добавление инструментов
            Controls.Add(AddColorButton(new Button(), Color.Gray, 300, 0, 30, 30, "gray"));
            Controls.Add(AddColorButton(new Button(), Color.Brown, 270, 0, 30, 30, "brown"));
            Controls.Add(AddColorButton(new Button(), Color.Pink, 240, 0, 30, 30, "pink"));
            Controls.Add(AddColorButton(new Button(), Color.Black, 210, 0, 30, 30, "black"));
            Controls.Add(AddColorButton(new Button(), Color.Blue, 180, 0, 30, 30, "blue"));
            Controls.Add(AddColorButton(new Button(), Color.Red, 150, 0, 30, 30, "red"));
            Controls.Add(AddColorButton(new Button(), Color.Yellow, 120, 0, 30, 30, "yellow"));
            Controls.Add(AddColorButton(new Button(), Color.Green, 90, 0, 30, 30, "green"));
            Controls.Add(AddColorButton(new Button(), Color.Orange, 60, 0, 30, 30, "red"));

            //Добавление панели для рисования
            PictureBox = new PictureBox()
            {
                BackColor = Color.White,
                //BorderStyle = BorderStyle.FixedSingle;
                Location = new Point(60, 40),
                Name = "pictureBox",
                Size = new Size(1200, 600),
                Image = null
            };

            PictureBox.MouseDown += MouseButtonIsDown;
            PictureBox.MouseMove += MouseIsMoved;
            PictureBox.MouseUp += MouseButtonIsUp;
            PictureBox.Paint += PictureBox_Paint;

            bmp = new Bitmap(PictureBox.Width, PictureBox.Height);
            PictureBox.Image = bmp;
            Graphics g = Graphics.FromImage(PictureBox.Image);
            g.FillRectangle(Brushes.White, 0, 0, PictureBox.Width, PictureBox.Height);

            Controls.Add(PictureBox);

            //Добавление ползунка
            BrushBar = new TrackBar
            {
                Location = new Point(330, 0),
                Name = "brushSize",
                Size = new Size(300, 60),
                Maximum = 30
            };
            BrushBar.Scroll += BrushBar_Scroll;
            Controls.Add(BrushBar);
        }

        //Обработка события ползунка
        private void BrushBar_Scroll(object sender, EventArgs e)
        {
            BrushSize = BrushBar.Value;
            Console.WriteLine("Now brush size is " + BrushSize);
        }

        //Добавление кнопки цвета
        private Button AddColorButton(Button button, Color color, int x, int y, int width, int height, string name)
        {
            button = new Button
            {
                BackColor = color,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Name = name
            };
            button.Click += Button_Click;

            return button;
        }

        //Добавление кнопки инструмента
        private Button AddToolButton(Button button, Image res, int x, int y, int width, int height, string name)
        {
            button = new Button
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Name = name,
                Image = res
            };
            button.Click += Button_Click;
            return button;
        }

        //Обработка события кнопок
        private void Button_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn.Name == "brush")
            {
                NumberOfTool = 0;
                Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool);
            }
            else if (btn.Name == "erase")
            {
                NumberOfTool = 1;
                Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool);
            }
            else if (btn.Name == "line")
            {
                NumberOfTool = 2;
                Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool);
            }
            else if (btn.Name == "square")
            {
                NumberOfTool = 3;
                Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool);
            }
            else if (btn.Name == "oval")
            {
                NumberOfTool = 4;
                Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool);
            }
            else if (btn.Name == "fill")
            {
                NumberOfTool = 5;
                Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool);
            }
            else if (btn.Name == "clean")
            {
                NumberOfTool = 6;
                if (PictureBox.Image != null)
                {
                    //PictureBox.BackColor = Color.White;
                    PictureBox.Image = null;
                    PictureBox.Invalidate();
                    Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool + ". Picture box will be cleaned!");
                }
                else Console.WriteLine("Now selected tool is " + btn.Name + ". His number is " + NumberOfTool + ". Picture box is already empty!");
            }
            else
            {
                MainColor = btn.BackColor;
                Console.WriteLine("Now main color is " + MainColor.Name);
            }
        }

        //Кнопка мыши нажата
        private void MouseButtonIsDown(object sender, MouseEventArgs e)
        {
            FirstLocation = e.Location;
            list.Add(FirstLocation);
            IsMouseDown = true;
            Console.WriteLine("Now mouse coordinates is " + FirstLocation);
            if (NumberOfTool == 5)
            {
                if (PictureBox.Image == null)
                {
                    bmp = new Bitmap(PictureBox.Width, PictureBox.Height);
                    PictureBox.Image = bmp;
                    Graphics g = Graphics.FromImage(PictureBox.Image);
                    g.FillRectangle(Brushes.White, 0, 0, PictureBox.Width, PictureBox.Height);
                }
                //Console.WriteLine("!");
                FloodFill(bmp, FirstLocation.X, FirstLocation.Y, MainColor);
                
                PictureBox.Invalidate();
            }
        }

        //Мышь двигается
        private void MouseIsMoved(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                SecondLocation = e.Location;
                if (FirstLocation != null)
                {
                    if (PictureBox.Image == null)
                    {
                        bmp = new Bitmap(PictureBox.Width, PictureBox.Height);
                        PictureBox.Image = bmp;
                        Graphics g = Graphics.FromImage(PictureBox.Image);
                        g.FillRectangle(Brushes.White, 0, 0, PictureBox.Width, PictureBox.Height);
                    }
                    if (NumberOfTool == 0) //кисть
                    {
                        Graphics g = Graphics.FromImage(PictureBox.Image);
                        list.Add(SecondLocation);
                        //g.DrawLine(new Pen(MainColor, BrushSize), FirstLocation, e.Location);
                        g.DrawLines(new Pen(MainColor, BrushSize), list.ToArray());
                        PictureBox.Invalidate();
                        FirstLocation = e.Location;
                    }
                    if (NumberOfTool == 1) //ластик
                    {
                        Graphics g = Graphics.FromImage(PictureBox.Image);
                        list.Add(SecondLocation);
                        g.DrawLines(new Pen(Color.White, BrushSize), list.ToArray());
                        PictureBox.Invalidate();
                        FirstLocation = e.Location;
                    }
                    //Фигуры для перерисовки
                    if (NumberOfTool == 2 || NumberOfTool == 3 || NumberOfTool == 4)
                    {
                        Refresh();
                    }
                    //Console.WriteLine("Now mouse coordinates is " + FirstLocation);
                }
            }
        }

        //Кнопка мыши отжата
        private void MouseButtonIsUp(object sender, MouseEventArgs e)
        {
            list.Clear();
            //Отображение фигур на битмапе
            if (NumberOfTool == 2) //линия
            {
                Graphics g = Graphics.FromImage(PictureBox.Image);
                g.DrawLine(new Pen(MainColor, BrushSize), FirstLocation, SecondLocation);
            }
            if (NumberOfTool == 3) //прямоугольник
            {
                Graphics g = Graphics.FromImage(PictureBox.Image);
                g.DrawRectangle(new Pen(MainColor, BrushSize), GetRect());
            }
            if (NumberOfTool == 4) //овал
            {
                Graphics g = Graphics.FromImage(PictureBox.Image);
                g.DrawEllipse(new Pen(MainColor, BrushSize), GetRect());
            }

            Console.WriteLine("Now mouse coordinates is " + FirstLocation);
            FirstLocation = Point.Empty;
            IsMouseDown = false;
        }

        //Перерисовка 
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (NumberOfTool == 2) //линия
                e.Graphics.DrawLine(new Pen(MainColor, BrushSize), FirstLocation, SecondLocation);
            if (NumberOfTool == 3) //прямоугольник
                e.Graphics.DrawRectangle(new Pen(MainColor, BrushSize), GetRect());
            if (NumberOfTool == 4) //овал
                e.Graphics.DrawEllipse(new Pen(MainColor, BrushSize), GetRect());
        }

        //Получение прямоугольника с необходимыми координатами
        private Rectangle GetRect()
        {
            return new Rectangle
            {
                X = Math.Min(FirstLocation.X, SecondLocation.X),
                Y = Math.Min(FirstLocation.Y, SecondLocation.Y),
                Width = Math.Abs(FirstLocation.X - SecondLocation.X),
                Height = Math.Abs(FirstLocation.Y - SecondLocation.Y)
            };
        }

        private void FloodFill(Bitmap bitmap, int x, int y, Color color)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int[] bits = new int[data.Stride / 4 * data.Height];
            Marshal.Copy(data.Scan0, bits, 0, bits.Length);

            LinkedList<Point> check = new LinkedList<Point>();
            int floodTo = color.ToArgb();
            int floodFrom = bits[x + y * data.Stride / 4];
            bits[x + y * data.Stride / 4] = floodTo;

            if (floodFrom != floodTo)
            {
                check.AddLast(new Point(x, y));
                while (check.Count > 0)
                {
                    Point cur = check.First.Value;
                    check.RemoveFirst();

                    foreach (Point off in new Point[] {
                new Point(0, -1), new Point(0, 1),
                new Point(-1, 0), new Point(1, 0)})
                    {
                        Point next = new Point(cur.X + off.X, cur.Y + off.Y);
                        if (next.X >= 0 && next.Y >= 0 &&
                            next.X < data.Width &&
                            next.Y < data.Height)
                        {
                            if (bits[next.X + next.Y * data.Stride / 4] == floodFrom)
                            {
                                check.AddLast(next);
                                bits[next.X + next.Y * data.Stride / 4] = floodTo;
                            }
                        }
                    }
                }
            }

            Marshal.Copy(bits, 0, data.Scan0, bits.Length);
            bitmap.UnlockBits(data);
        }
    }
}
