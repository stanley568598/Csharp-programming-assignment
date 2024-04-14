using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Specialized;

namespace 影像壓縮
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap image2;
        Dictionary<Color, int> history = new Dictionary<Color, int>();
        Dictionary<Color, int> mapping = new Dictionary<Color, int>();


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Bitmap文件(*.bmp)|*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)        // 開啟影像檔
            {
                String input = openFileDialog1.FileName;
                image = (Bitmap)Bitmap.FromFile(input);                    // 產生一個Image物件
                image2 = new Bitmap(image);

                pictureBox1.Size = new Size(image.Width, image.Height);
                pictureBox2.Size = new Size(image.Width, image.Height);
                pictureBox1.Image = image;
                pictureBox1.Refresh(); // 要求重畫

                Invalidate(); // 要求重畫
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Bitmap文件(*.bmp)|*.bmp";

            if (pictureBox2.Image != null)
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Bmp;
                    String output = saveFileDialog1.FileName;
                    image2.Save(output, format);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image!=null)
            {
                for (int x = 0; x < image.Size.Width; x++)
                    for (int y = 0; y < image.Size.Height; y++)
                    {
                        Color c = image.GetPixel(x, y);
                        if (history.ContainsKey(c))
                            history[c] = history[c] + 1;
                        else
                            history.Add(c, 1);
                    }

                var result = history.OrderByDescending(a => a.Value);
                /* 等價於 var result = from b in history
                              orderby b.Value descending
                              select b; */
                int number = 256;
                var mostusedcolor = result.Select(x => x.Key).Take(number).ToList();

                Bitmap bmpTemp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed); //原圖格式未提供調色盤空間
                ColorPalette new_palette = bmpTemp.Palette;   //無法直接修改 bmp.palette ，必須新創 ColorPalette
                for (int i = 0; i < 256; i++)
                {
                    Color palette_used = new Color();
                    palette_used = Color.FromArgb(mostusedcolor[i].R, mostusedcolor[i].G, mostusedcolor[i].B);
                    new_palette.Entries.SetValue(palette_used, i);
                }
                image2.Palette = new_palette;

                for (int i = 0; i < 256; i++)   //輸出調色盤
                {
                    textBox1.Text += Convert.ToString(i);
                    textBox1.Text += ".";
                    textBox1.Text += Convert.ToString(image2.Palette.Entries[i]);
                    textBox1.Text += " ";
                    if (i % 2 == 1)
                        textBox1.Text += Environment.NewLine;
                }

                Double temp;
                Dictionary<Color, Double> dist = new Dictionary<Color, double>(); //紀錄原本顏色與調色盤各顏色差值
                Dictionary<int, Double> index = new Dictionary<int, double>();
                foreach (var r in result)
                {
                    dist.Clear();
                    index.Clear();
                    for (int i = 0; i < 256; i++)
                    {
                        Color u = mostusedcolor[i]; 
                        temp = Math.Abs(r.Key.R - u.R) + Math.Abs(r.Key.B - u.B) + Math.Abs(r.Key.G - u.G); //計算原本顏色與調色盤各顏色差值
                        dist.Add(u, temp);
                        index.Add(i, temp);
                    }
                    var min_dist = dist.OrderBy(k => k.Value).FirstOrDefault();  //找到最小差值
                    var min_index = index.OrderBy(n => n.Value).FirstOrDefault();  //找到最小差值
                    mapping.Add(r.Key, min_index.Key);    //建立原本各顏色的轉換對象
                }

                for (int x = 0; x < image2.Size.Width; x++)
                    for (int y = 0; y < image2.Size.Height; y++)
                    {
                        Color c = image2.GetPixel(x, y);
                        image2.SetPixel(x, y, image2.Palette.Entries[mapping[c]]);
                    }

                Bitmap bmpNew = image2.Clone(new Rectangle(0, 0, image2.Width, image2.Height), PixelFormat.Format8bppIndexed);  //轉換儲存方式
                image2 = bmpNew;
                pictureBox2.Image = image2;

                count_PSNR();
            }
        }

        void count_PSNR()
        {
            double sumR = 0;
            double sumG = 0;
            double sumB = 0;
            for (int i = 0; i < image.Width - 1; i++)
            {
                for (int j = 0; j < image2.Height - 1; j++)
                {
                    Color color1 = image.GetPixel(i, j);
                    Color color2 = image2.GetPixel(i, j);

                    int retR = color1.R - color2.R;
                    int retG = color1.G - color2.G;
                    int retB = color1.B - color2.B;

                    sumR += Math.Pow(retR, 2);
                    sumG += retG * retG;
                    sumB += retB * retB;
                }
            }
            double sum = sumR + sumG + sumB;

            double MSE = ((sum / 3) / (image.Width * image.Height));

            int B = 8;              //the pixels are represented using 8 bits per sample
            double psnr = 20 * Math.Log10(Math.Pow(2, B) - 1) - 10 * Math.Log10(MSE);

            label1.Text += (Convert.ToString(psnr) + " dB");
        }
    }
}
