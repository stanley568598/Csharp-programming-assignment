using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WP_7_5
{
    public partial class Form1 : Form
    {
        Bitmap image;
        TextureBrush textureBrush;
        Pen myPen;
        int x, y;　// 紀錄上一個筆畫的起始點
        Graphics G; // 畫布物件
        Rectangle rectDest, backcolor_rect_0, backcolor_rect_1, backcolor_rect_2, backcolor_rect_3;
        Boolean pen_down;

        public Form1()
        {
            InitializeComponent();

            Image img = Properties.Resources.back_page;
            
            image = new Bitmap(img, img.Width / 2, img.Height / 2);

            ClientSize = new Size(image.Width + 200, image.Height + 200);
            backcolor_rect_0 = new Rectangle(0, 0, image.Width + 200, image.Height + 200);

            rectDest = new Rectangle(110, 160, image.Width - 20, image.Height - 20);
            backcolor_rect_1 = new Rectangle(95, 145, image.Width + 10, image.Height + 10);
            backcolor_rect_2 = new Rectangle(80, 130, image.Width + 40, image.Height + 40);
            backcolor_rect_3 = new Rectangle(70, 120, image.Width + 60, image.Height + 60);

            label1.Location = new Point(0, 20);
            label1.Width = image.Width + 200;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            textureBrush = new TextureBrush(image);
            textureBrush.TranslateTransform(100, 150);
            myPen = new Pen(textureBrush, 20);  //畫刷的色塊大小與原圖相同，但畫刷大小小於原圖
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // 滑鼠的左鍵
            {
                if (pen_down == true && rectDest.Contains(e.Location) == true)
                {
                    G = CreateGraphics();
                    G.DrawLine(myPen, x, y, e.X, e.Y); // 寫到　buffer

                    x = e.X; // 結束點 就是 下一次的 開始點
                    y = e.Y;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Brush red = new SolidBrush(Color.Red);
            Brush yellow = new SolidBrush(Color.Gold);
            Brush gray = new SolidBrush(Color.Silver);
            Brush white = new SolidBrush(Color.White);

            e.Graphics.FillRectangle(red, backcolor_rect_0);
            e.Graphics.FillRectangle(white, backcolor_rect_3);
            e.Graphics.FillRectangle(yellow, backcolor_rect_2);
            e.Graphics.FillRectangle(gray, backcolor_rect_1);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            pen_down = false;

            if (rectDest.Contains(e.Location) == true) {
                x = e.X; // 紀錄筆畫的起始點
                y = e.Y;
                pen_down = true;
            }
        }
    }
}
