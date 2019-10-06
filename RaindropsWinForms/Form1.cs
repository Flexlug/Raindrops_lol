using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic;

namespace RaindropsWinForms
{
    public partial class Form1 : Form
    {
        private const int START_DROPS_COUNT = 150;

        private volatile Bitmap mainCanvas;
        private volatile Graphics g;

        private List<RainDrop> rainDrops;

        private Random rnd;

        string str;

        private List<TextDrop> textDrops;

        public Form1()
        {
            InitializeComponent();

            InitBackground();

            rnd = new Random();

            rainDrops = new List<RainDrop>();
            for (int i = 0; i < START_DROPS_COUNT; i++)
            {
                rainDrops.Add(CreateRaindrop());
            }

            textDrops = new List<TextDrop>();

            str = Interaction.InputBox("Enter text", "Name");

            Timer t = new Timer();
            t.Interval = 10;
            t.Tick += new EventHandler(PaintDrops);
            t.Start();
        }

        /// <summary>
        /// Создаёт каплю со случайными координатами по оси X и случайной длиной хвоста
        /// </summary>
        public RainDrop CreateRaindrop()
        {
            return new RainDrop(rnd.Next(5, Width), rnd.Next(2, Height), 5);
        }

        /// <summary>
        /// Инициализирует фон и инструменты для рисования
        /// </summary>
        public void InitBackground()
        {
            mainCanvas = new Bitmap(Width, Height);
            g = Graphics.FromImage(mainCanvas);
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            MainPictureBox.Image = mainCanvas;

        }

        /// <summary>
        /// Рисует все капли по КД
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void PaintDrops(object obj, EventArgs e)
        {
            g.Clear(Color.Black);

            g.DrawString(str, new Font(FontFamily.GenericSerif, 25), Brushes.White, new Point(Width / 2, 50));

            for (int i = 0; i < rainDrops.Count; i++)
            {
                rainDrops[i].Draw(g);
                rainDrops[i].Update();

                if (rainDrops[i].CheckCollision(Size))
                {
                    rainDrops.Remove(rainDrops[i]);
                    rainDrops.Add(CreateRaindrop());
                }
            }

            for (int i = 0; i < textDrops.Count; i++)
            {
                textDrops[i].Draw(g);
                textDrops[i].Update();

                if (textDrops[i].CheckCollision(Size))
                {
                    textDrops.Remove(textDrops[i]);
                }
            }

            MainPictureBox.Refresh();
        }

        public void AddTextToScreen(string text)
        {
            textDrops.Add(new TextDrop(Width / 2, 100, text));
        }

        /// <summary>
        /// Вызывается, если форма изменила свой размер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            InitBackground();
            for (int i = 0; i < rainDrops.Count; i++)
                if (rainDrops[i].CheckCollision(this.Size))
                {
                    rainDrops[i] = CreateRaindrop();
                }
        }

        /// <summary>
        /// Обработик событий для textBox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddTextToScreen(textBox1.Text);
                textBox1.Text = string.Empty;
            }
        }
    }
}
