using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace RaindropsWinForms
{
    public class TextDrop
    {
        private int X;
        private int Y;

        public string InnerText;

        private Brush[] brushes;
        private int currentBrush = 0;

        /// <summary>
        /// Создаёт каплю
        /// </summary>
        /// <param name="x">Координаты по оси X</param>
        /// <param name="y">Координаты по оси Y</param>
        /// <param name="TailSize">Длина хвоста</param>
        public TextDrop(int x, int y, string innerText)
        {
            this.X = x;
            this.Y = y;
            this.InnerText = innerText;

            brushes = new Brush[20];
            for (int i = 0; i < 20; i++)
                brushes[i] = new SolidBrush(Color.FromArgb(12 * i, 255, 255, 255));
        }

        /// <summary>
        /// Отрисовывает каплю
        /// </summary>
        /// <param name="g">Ссылка на Graphics, который отвечает за отрисовку</param>
        public void Draw(Graphics g)
        {
            g.DrawString(InnerText, new Font(FontFamily.GenericSerif, 30), brushes[currentBrush], new Point(X, Y));
            if (currentBrush < brushes.Length - 1)
                currentBrush++;
        }

        /// <summary>
        /// Обновить положение капли
        /// </summary>
        public void Update()
        {
            Y += 5;
        }

        /// <summary>
        /// Проверка на выход капли за границы окна
        /// </summary>
        /// <param name="formSize">Размеры формы</param>
        /// <returns>Возвращает результат - вышла ли капля полностью за границы окна</returns>
        public bool CheckCollision(Size formSize)
        {
            if (Y + 40 > formSize.Height)
                return true;
            else
                return false;
        }
    }
}
