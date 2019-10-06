using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace RaindropsWinForms
{
    public class RainDrop
    {
        private int X;
        private int Y;       

        public int TailSize;

        private Pen pen = Pens.White;

        /// <summary>
        /// Создаёт каплю
        /// </summary>
        /// <param name="x">Координаты по оси X</param>
        /// <param name="y">Координаты по оси Y</param>
        /// <param name="TailSize">Длина хвоста</param>
        public RainDrop(int x, int y, int TailSize)
        {
            this.X = x;
            this.Y = y;
            this.TailSize = TailSize;
        }

        /// <summary>
        /// Отрисовывает каплю
        /// </summary>
        /// <param name="g">Ссылка на Graphics, который отвечает за отрисовку</param>
        public void Draw(Graphics g)
        {
            g.DrawLine(pen, X, Y, X, Y + TailSize);
        }

        /// <summary>
        /// Обновить положение капли
        /// </summary>
        public void Update()
        {
            Y+= 10;
        }

        /// <summary>
        /// Проверка на выход капли за границы окна
        /// </summary>
        /// <param name="formSize">Размеры формы</param>
        /// <returns>Возвращает результат - вышла ли капля полностью за границы окна</returns>
        public bool CheckCollision(Size formSize)
        {
            if (Y + TailSize > formSize.Height)
                return true;
            else
                return false;
        }
    }
}
