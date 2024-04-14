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
using System.IO;
using System.Globalization;

namespace 影像壓縮
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap image2;

        int compression_factor;
        int[,] Quantization_table = new int[16, 16]
        {{ 10, 20, 30, 40, 50, 60, 70, 80, 90,100,110,120,130,140,150,160 },
         { 20, 30, 40, 50, 60, 70, 80, 90,100,110,120,130,140,150,160,170 },
         { 30, 40, 50, 60, 70, 80, 90,100,110,120,130,140,150,160,170,180 },
         { 40, 50, 60, 70, 80, 90,100,110,120,130,140,150,160,170,180,190 },
         { 50, 60, 70, 80, 90,100,110,120,130,140,150,160,170,180,190,200 },
         { 60, 70, 80, 90,100,110,120,130,140,150,160,170,180,190,200,210 },
         { 70, 80, 90,100,110,120,130,140,150,160,170,180,190,200,210,220 },
         { 80, 90,100,110,120,130,140,150,160,170,180,190,200,210,220,230 },
         { 90,100,110,120,130,140,150,160,170,180,190,200,210,220,230,240 },
         {100,110,120,130,140,150,160,170,180,190,200,210,220,230,240,250 },
         {110,120,130,140,150,160,170,180,190,200,210,220,230,240,250,260 },
         {120,130,140,150,160,170,180,190,200,210,220,230,240,250,260,270 },
         {130,140,150,160,170,180,190,200,210,220,230,240,250,260,270,280 },
         {140,150,160,170,180,190,200,210,220,230,240,250,260,270,280,290 },
         {150,160,170,180,190,200,210,220,230,240,250,260,270,280,290,300 },
         {160,170,180,190,200,210,220,230,240,250,260,270,280,290,300,310 },
        };

        List<List<List<double>>> R_tables;
        List<List<List<double>>> G_tables;
        List<List<List<double>>> B_tables;

        List<List<int>> result_R;
        List<List<int>> result_G;
        List<List<int>> result_B;

        int inverse_compression_factor;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            /*
                Form2 x = new Form2();
                x.TopMost = true;
                x.Text = "Testing";
                x.Show();
            */            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            if (comboBox1.Text != "")
            {
                if (pictureBox1.Image != null)
                {
                    R_tables = new List<List<List<double>>>();
                    G_tables = new List<List<List<double>>>();
                    B_tables = new List<List<List<double>>>();

                    result_R = new List<List<int>>();
                    result_G = new List<List<int>>();
                    result_B = new List<List<int>>();

                    compression_factor = Convert.ToInt32(comboBox1.Text);

                    image2 = new Bitmap(image);

                    DCT();
                    quantize();
                    Zig_zag();
                    statistical_report();
                    output_result();
                }
            }

            button2.Enabled = true;
        }

        void DCT()    // DCT for 1 image
        {
            int n = 16;

            for (int k = 0; k < image2.Height; k += n)                     // image.h_position
                for (int l = 0; l < image2.Width; l += n)                  // image.w_position
                {
                    List<List<double>> DCT_coefficient_table_R = new List<List<double>>();
                    List<List<double>> DCT_coefficient_table_G = new List<List<double>>();
                    List<List<double>> DCT_coefficient_table_B = new List<List<double>>();

                    for (int i = 0; i < n; i++)    // make 16 * 16
                    {
                        DCT_coefficient_table_R.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                        DCT_coefficient_table_G.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                        DCT_coefficient_table_B.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                    }

                    int u = 0;                                          // block.h_num  == u
                    for (int a = k; a < k + n; a++)                     // a == block.h_position
                    {
                        int v = 0;                                      // block.w_num == v
                        for (int b = l; b < l + n; b++)                 // b == block.w_position
                        {
                            int i = 0;                                  // other_pixel.w_num == i
                            for (int y = l; y < l + n; y++)             // y == other_pixel.w_position
                            {
                                int j = 0;                              // other_pixel.h_num == j
                                for (int x = k; x < k + n; x++)         // x == other_pixel.h_position
                                {
                                    // 三角函數function 只處理弧度，不處理度數，需要用 pi 做轉換；弧度=角度 * pi/180
                                    // pi 的精確值 == 2 * Math.Acos(0)，Math.PI == 大約值
                                    DCT_coefficient_table_R[u][v] += (image2.GetPixel(y, x).R - 128)
                                        * Math.Cos((2 * i + 1) * (Math.Acos(0) * u / n)) * Math.Cos((2 * j + 1) * Math.Acos(0) * v / n);
                                    DCT_coefficient_table_G[u][v] += (image2.GetPixel(y, x).G - 128)
                                        * Math.Cos((2 * i + 1) * (Math.Acos(0) * u / n)) * Math.Cos((2 * j + 1) * Math.Acos(0) * v / n);
                                    DCT_coefficient_table_B[u][v] += (image2.GetPixel(y, x).B - 128)
                                        * Math.Cos((2 * i + 1) * (Math.Acos(0) * u / n)) * Math.Cos((2 * j + 1) * Math.Acos(0) * v / n);
                                    j++;
                                }
                                i++;
                            }
                            DCT_coefficient_table_R[u][v] *= C(u) * C(v) / (n / 2);
                            DCT_coefficient_table_G[u][v] *= C(u) * C(v) / (n / 2);
                            DCT_coefficient_table_B[u][v] *= C(u) * C(v) / (n / 2);

                            v++;
                        }
                        u++;
                    }

                    R_tables.Add(DCT_coefficient_table_R);
                    G_tables.Add(DCT_coefficient_table_G);
                    B_tables.Add(DCT_coefficient_table_B);
                }
        }

        double C(int x)
        {
            if (x == 0)
                return 1.0 / Math.Sqrt(2.0);
            else
                return 1.0;
        }

        void quantize()
        {
            for (int blocks=0; blocks < R_tables.Count; blocks++)
                for (int j = 0; j < R_tables[blocks].Count; j++)
                    for (int i = 0; i < R_tables[blocks][j].Count; i++)
                    {
                        R_tables[blocks][i][j] /= (Quantization_table[i, j] * compression_factor);
                        G_tables[blocks][i][j] /= (Quantization_table[i, j] * compression_factor);
                        B_tables[blocks][i][j] /= (Quantization_table[i, j] * compression_factor);

                        R_tables[blocks][i][j] = Math.Round(R_tables[blocks][i][j]);
                        G_tables[blocks][i][j] = Math.Round(G_tables[blocks][i][j]);
                        B_tables[blocks][i][j] = Math.Round(B_tables[blocks][i][j]);
                    }
        }

        void Zig_zag()
        {
            List<int> r_R;
            List<int> r_G;
            List<int> r_B;

            bool right_up;
            int i;
            int j;

            for (int blocks = 0; blocks < R_tables.Count; blocks++)
            {
                r_R = new List<int>();
                r_G = new List<int>();
                r_B = new List<int>();

                right_up = false;
                i = 0;
                j = 0;
                for (int count = 0; count < R_tables[blocks].Count * R_tables[blocks].Count; count++)
                {
                    r_R.Add(Convert.ToInt32(R_tables[blocks][i][j]));
                    r_G.Add(Convert.ToInt32(G_tables[blocks][i][j]));
                    r_B.Add(Convert.ToInt32(B_tables[blocks][i][j]));

                    if (right_up == false)  // false == left_down
                    {
                        i--;
                        j++;

                        if(j>=16)
                        {
                            i++;
                            i++;
                            j--;
                            right_up = true;
                        }
                        else if (i < 0)
                        {
                            i++;
                            right_up = true;
                        }

                    }
                    else if (right_up == true)
                    {
                        i++;
                        j--;
                        
                        if (i >= 16)
                        {
                            j++;
                            j++;
                            i--;
                            right_up = false;
                        }
                        else if (j < 0)
                        {
                            j++;
                            right_up = false;
                        }
                    }
                }
                result_R.Add(r_R);
                result_G.Add(r_G);
                result_B.Add(r_B);
            }
        }

        void statistical_report()
        {
            string report;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            saveFileDialog1.FileName = "report";
            saveFileDialog1.ShowDialog();
            StreamWriter outFile = new StreamWriter(saveFileDialog1.FileName);

            report = "";
            report += ("factor : " + Convert.ToString(compression_factor));
            report += ("  each block have R,G,B with 16*16 elements, and \" EOB \" means 0's left.");
            report += (Environment.NewLine + Environment.NewLine);
            save_txt(report, outFile);

            int count_R_0 = 0;
            int count_G_0 = 0;
            int count_B_0 = 0;

            for (int blocks = 0; blocks < result_R.Count; blocks++)
            {
                report = "";
                report += ("Block " + Convert.ToString(blocks) + " - " + Environment.NewLine);
                report += "R : ";
                for (int index = 0; index <= getEOB(result_R[blocks]); index++)
                {   
                    //if (result_R[blocks][index] != 0)
                    report += Convert.ToString(result_R[blocks][index]);
                    report += ",";
                }
                if (getEOB(result_R[blocks]) != result_R[blocks].Count - 1)
                {
                    report += ("EOB" + Environment.NewLine);

                    count_R_0 += (result_R[blocks].Count - 1 - getEOB(result_R[blocks]));
                }

                report += "G : ";
                for (int index = 0; index <= getEOB(result_G[blocks]); index++)
                {
                    //if (result_G[blocks][index] != 0)
                    report += Convert.ToString(result_G[blocks][index]);
                    report += ",";
                }
                if (getEOB(result_G[blocks]) != result_G[blocks].Count - 1)
                {
                    report += ("EOB" + Environment.NewLine);

                    count_G_0 += (result_G[blocks].Count - 1 - getEOB(result_G[blocks]));
                }

                report += "B : ";
                for (int index = 0; index <= getEOB(result_B[blocks]); index++)
                {
                    //if (result_B[blocks][index] != 0)
                    report += Convert.ToString(result_B[blocks][index]);
                    report += ",";
                }
                if (getEOB(result_B[blocks]) != result_B[blocks].Count - 1)
                {
                    report += ("EOB" + Environment.NewLine);

                    count_B_0 += (result_B[blocks].Count - 1 - getEOB(result_B[blocks]));
                }

                report += Environment.NewLine;
                save_txt(report, outFile);
            }

            report = "";
            report += Environment.NewLine;
            label2.Text = "所有block 總共省略 R,G,B 之零的數目：";
            label2.Text += (Convert.ToString(count_R_0) + "," + Convert.ToString(count_G_0) + "," + Convert.ToString(count_B_0));
            report += (label2.Text + Environment.NewLine);
            save_txt(report, outFile);

            save_txt("", outFile);  //關閉寫入檔
        }

        int getEOB(List<int> result)
        {
            int index;
            for (index = result.Count - 1; index >= 0; index--)
            {
                if (result[index] != 0)
                    break;
            }
            return index;
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

            label1.Text = "PSNR：";
            label1.Text += (Convert.ToString(psnr) + " dB");
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Bitmap文件(*.bmp)|*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)        // 開啟影像檔
            {
                String input = openFileDialog1.FileName;
                image = (Bitmap)Bitmap.FromFile(input);                    // 產生一個Image物件
                image2 = new Bitmap(image, change_size());

                image = image2;
                //save_new_size(input);
                
                image2 = image;

                pictureBox1.Size = new Size(image.Width, image.Height);
                pictureBox2.Size = new Size(image.Width, image.Height);
                pictureBox1.Image = image;
                pictureBox1.Refresh(); // 要求重畫

                Invalidate(); // 要求重畫
            }
        }

        /*
        void save_new_size(String filename)
        {
            ImageFormat format = ImageFormat.Bmp;
            String output = "";
            char symbol = '/';
            for (int i = 1; i < filename.Length; i++)
                if (filename[i - 1] == symbol && filename[i] == symbol) ;
                else
                    output += filename[i - 1];

            output += filename[filename.Length - 1];

            image2.Save(output, format);
        }
        */

        Size change_size()
        {
            int x = image.Width;
            int y = image.Height;

            int count = 0;
            while (count <= x - 16)
                count += 16;
            x = count;

            count = 0;
            while (count <= y - 16)
                count += 16;
            y = count;

            return new Size(x, y);

            //int min = Math.Min(x, y);
            //return new Size(min, min);
        }

        //Save (picturebox2.Image)
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog3.Filter = "Bitmap文件(*.bmp)|*.bmp";

            if (pictureBox2.Image != null)
                if (saveFileDialog3.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Bmp;
                    String output = saveFileDialog3.FileName;
                    image2.Save(output, format);
                }
        }

        void output_result()
        {
            string result;
            saveFileDialog2.Filter = "txt files (*.txt)|*.txt";

            saveFileDialog2.FileName = "result";
            saveFileDialog2.ShowDialog();
            StreamWriter output = new StreamWriter(saveFileDialog2.FileName);

            //存檔格式：(compression_factor+".") + R-256 + G-256 + B-256 ； *-EOB or END
            result = "";
            result += (Convert.ToString(compression_factor)+".");
            save_txt(result, output);

            for (int blocks = 0; blocks < result_R.Count; blocks++)
            {
                result = "";

                for (int index = 0; index <= getEOB(result_R[blocks]); index++)
                {
                    result += Convert.ToString(result_R[blocks][index]);
                    result += " ";
                }
                result += "*";

                for (int index = 0; index <= getEOB(result_G[blocks]); index++)
                {
                    result += Convert.ToString(result_G[blocks][index]);
                    result += " ";
                }
                result += "*";

                for (int index = 0; index <= getEOB(result_B[blocks]); index++)
                {
                    result += Convert.ToString(result_B[blocks][index]);
                    result += " ";
                }
                result += "*";

                save_txt(result, output);
            }
            save_txt("", output);
        }

        private void save_txt(String write_line, StreamWriter outFile)
        {
            if (write_line == "")
                outFile.Close();
            else
                outFile.Write(write_line);
        }
        
        /*
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8) 
            {
                e.Handled = true;
            }
        }
        */

        private void button1_Click(object sender, EventArgs e)      //解碼//
        {
            button1.Enabled = false;

            input_result_file();
            inverse_Zig_zag();
            inverse_quantize();

            image2 = new Bitmap(image);     // 執行前，確保 image 存在
            Bitmap bmpNew = image2.Clone(new Rectangle(0, 0, image2.Width, image2.Height), PixelFormat.Format24bppRgb);  ////轉換儲存方式//24bppRgb//16bppRgb555//
            image2 = bmpNew;

            inverse_DCT();
            
            pictureBox2.Image = image2;
            count_PSNR();

            button1.Enabled = true;
        }

        void input_result_file()
        {
            openFileDialog2.FileName = "result";
            openFileDialog2.Filter = "txt files (*.txt)|*.txt";
            openFileDialog2.ShowDialog();

            result_R = new List<List<int>>();
            result_G = new List<List<int>>();
            result_B = new List<List<int>>();

            inverse_compression_factor = 0;

            FileStream input = new FileStream(openFileDialog2.FileName, FileMode.Open);

            StreamReader Reader = new StreamReader(input);   //讀取文件(讀取大文件時，最好不要用此方法)
            string read_all = "";

            read_all = Reader.ReadToEnd();

            Reader.Close();
            input.Close();

            string[] header = read_all.Split('.');
            inverse_compression_factor = Convert.ToInt32(header[0]);

            string[] channels = header[1].Split('*');

            for (int i = 0; i < channels.Length - 1; i += 3)
            {
                string[] R_values = channels[i].Split(' ');
                List<int> R_channel_value = new List<int>();
                for (int j = 0; j < R_values.Length - 1; j++) 
                    R_channel_value.Add(Convert.ToInt32(R_values[j]));
                for (int j = R_values.Length; j < 256; j++)
                    R_channel_value.Add(0);
                result_R.Add(R_channel_value);

                string[] G_values = channels[i + 1].Split(' ');
                List<int> G_channel_value = new List<int>();
                for (int j = 0; j < G_values.Length - 1; j++) 
                    G_channel_value.Add(Convert.ToInt32(G_values[j]));
                for (int j = G_values.Length; j < 256; j++)
                    G_channel_value.Add(0);
                result_G.Add(G_channel_value);

                string[] B_values = channels[i + 2].Split(' ');
                List<int> B_channel_value = new List<int>();
                for (int j = 0; j < B_values.Length - 1; j++) 
                    B_channel_value.Add(Convert.ToInt32(B_values[j]));
                for (int j = B_values.Length; j < 256; j++)
                    B_channel_value.Add(0);
                result_B.Add(B_channel_value);
            }
        }

        void inverse_Zig_zag()
        {
            R_tables = new List<List<List<double>>>();
            G_tables = new List<List<List<double>>>();
            B_tables = new List<List<List<double>>>();

            bool right_up;
            int i;
            int j;
            for (int blocks = 0; blocks < result_R.Count; blocks++)
            {
                List<List<double>> IDCT_coefficient_table_R = new List<List<double>>();
                List<List<double>> IDCT_coefficient_table_G = new List<List<double>>();
                List<List<double>> IDCT_coefficient_table_B = new List<List<double>>();

                int n = 16;
                for (int k = 0; k < n; k++)    // make 16 * 16
                {
                    IDCT_coefficient_table_R.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                    IDCT_coefficient_table_G.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                    IDCT_coefficient_table_B.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                }

                right_up = false;
                i = 0;
                j = 0;
                for (int count = 0; count < result_R[blocks].Count; count++)
                {
                    IDCT_coefficient_table_R[i][j] = result_R[blocks][count];
                    IDCT_coefficient_table_G[i][j] = result_G[blocks][count];
                    IDCT_coefficient_table_B[i][j] = result_B[blocks][count];

                    if (right_up == false)  // false == left_down
                    {
                        i--;
                        j++;

                        if (j >= 16)
                        {
                            i++;
                            i++;
                            j--;
                            right_up = true;
                        }
                        else if (i < 0)
                        {
                            i++;
                            right_up = true;
                        }

                    }
                    else if (right_up == true)
                    {
                        i++;
                        j--;

                        if (i >= 16)
                        {
                            j++;
                            j++;
                            i--;
                            right_up = false;
                        }
                        else if (j < 0)
                        {
                            j++;
                            right_up = false;
                        }
                    }
                }
                R_tables.Add(IDCT_coefficient_table_R);
                G_tables.Add(IDCT_coefficient_table_G);
                B_tables.Add(IDCT_coefficient_table_B);
            }
        }

        void inverse_quantize()
        {
            for (int blocks = 0; blocks < R_tables.Count; blocks++)
                for (int j = 0; j < R_tables[blocks].Count; j++)
                    for (int i = 0; i < R_tables[blocks][j].Count; i++)
                    {
                        R_tables[blocks][i][j] *= (Quantization_table[i, j] * inverse_compression_factor);
                        G_tables[blocks][i][j] *= (Quantization_table[i, j] * inverse_compression_factor);
                        B_tables[blocks][i][j] *= (Quantization_table[i, j] * inverse_compression_factor);
                    }
        }

        void inverse_DCT()
        {
            int n = 16;
            int block_num_each_width = image2.Width / n;

            for (int k = 0; k < image2.Height; k += n)                     //image.h_position
                for (int l = 0; l < image2.Width; l += n)                  //image.w_position
                {
                    List<List<double>> Block_R = new List<List<double>>();
                    List<List<double>> Block_G = new List<List<double>>();
                    List<List<double>> Block_B = new List<List<double>>();

                    for (int b = 0; b < n; b++)    // make 16 * 16
                    {
                        Block_R.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                        Block_G.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                        Block_B.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                    }

                    int i = 0;                                          // block.h_num  == i
                    for (int c = k; c < k + n; c++)                     // c == block.h_position
                    {
                        int j = 0;                                      // block.w_num == j
                        for (int d = l; d < l + n; d++)                 // d == block.w_position
                        {
                            int u = 0;                                  // other_pixel.h_num == u
                            for (int x = k; x < k + n; x++)             // x == other_pixel.h_position
                            {
                                int v = 0;                              // other_pixel.w_num == v
                                for (int y = l; y < l + n; y++)         // y == other_pixel.w_position
                                {
                                    // 三角函數function 只處理弧度，不處理度數，需要用 pi 做轉換；弧度=角度 * pi/180
                                    // pi 的精確值 == 2 * Math.Acos(0)，Math.PI == 大約值
                                    Block_R[i][j] += C(u) * C(v) / (n / 2) * R_tables[(k / 16) * block_num_each_width + (l / 16)][v][u]
                                     * Math.Cos((2 * i + 1) * u * Math.Acos(0) / n) * Math.Cos((2 * j + 1) * v * Math.Acos(0) / n);

                                    Block_G[i][j] += C(u) * C(v) / (n / 2) * G_tables[(k / 16) * block_num_each_width + (l / 16)][v][u]
                                     * Math.Cos((2 * i + 1) * u * Math.Acos(0) / n) * Math.Cos((2 * j + 1) * v * Math.Acos(0) / n);

                                    Block_B[i][j] += C(u) * C(v) / (n / 2) * B_tables[(k / 16) * block_num_each_width + (l / 16)][v][u]
                                     * Math.Cos((2 * i + 1) * u * Math.Acos(0) / n) * Math.Cos((2 * j + 1) * v * Math.Acos(0) / n);

                                    v++;
                                }
                                u++;
                            }

                            Block_R[i][j] = Math.Round(Block_R[i][j]) + 128;
                            Block_G[i][j] = Math.Round(Block_G[i][j]) + 128;
                            Block_B[i][j] = Math.Round(Block_B[i][j]) + 128;

                            if (Block_R[i][j] < 0)
                                Block_R[i][j] = 0;
                            else if (Block_R[i][j] > 255)
                                Block_R[i][j] = 255;

                            if (Block_G[i][j] < 0)
                                Block_G[i][j] = 0;
                            else if (Block_G[i][j] > 255)
                                Block_G[i][j] = 255;

                            if (Block_B[i][j] < 0)
                                Block_B[i][j] = 0;
                            else if (Block_B[i][j] > 255)
                                Block_B[i][j] = 255;

                            j++;
                        }
                        i++;
                    }

                    for (int b_x = 0; b_x < n; b_x++)
                        for (int b_y = 0; b_y < n; b_y++)
                            image2.SetPixel(b_y + l, b_x + k, Color.FromArgb((int)Block_R[b_x][b_y], (int)Block_G[b_x][b_y], (int)Block_B[b_x][b_y]));
                }
        }
             
        /*
        public static double[] Transform(double[] vector)   //1-D DCT
        {
            if (vector == null)
                throw new NullReferenceException();
            double[] result = new double[vector.Length];
            double factor = 2 * Math.Acos(0) / vector.Length;
            for (int i = 0; i < vector.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < vector.Length; j++)
                    sum += vector[j] * Math.Cos((j + 0.5) * i * factor);
                result[i] = sum;
            }
            return result;
        }

        public static double[] InverseTransform(double[] vector)   //1-D IDCT
        {
            if (vector == null)
                throw new NullReferenceException();
            double[] result = new double[vector.Length];
            double factor = 2 * Math.Acos(0) / vector.Length;
            for (int i = 0; i < vector.Length; i++)
            {
                double sum = vector[0] / 2;
                for (int j = 1; j < vector.Length; j++)
                    sum += vector[j] * Math.Cos(j * (i + 0.5) * factor);
                result[i] = sum;
            }
            return result;
        }
        */

        /*
          參考資料
        void function(double[][] I, bool t, int Ia, int Ib)    //t==inverse or not
        {

            if(t==true) //dct
            {
                double[][] J = zeros(Ia, Ib);
                for (int k = 1; k <= Ia; k += 8)
                    for (int l = 1; l <= Ib; l += 8)
                    {
                        int z = 1;
                        for (int i = k; i <= k + 7; i++)
                        {
                            int c = 1;
                            for (int j = l; j <= l + 7; j++)
                            {
                                int v = 1;
                                for (int x = k; x <= k + 7; x++)
                                {
                                    int b = 1;
                                    for (int y = l; y <= l + 7; y++)
                                    {
                                        J[i][j] = J[i][j] + I[x][y] * Math.Cos((2 * (b - 1) + 1) * (c - 1) * 2 * Math.Acos(0) / 16) * Math.Cos((2 * (v - 1) + 1) * (z - 1) * 2 * Math.Acos(0) / 16);
                                        b = b + 1;
                                    }
                                    v++;
                                }
                                double Ci;
                                if ((z - 1) == 0)
                                    Ci = 1 / Math.Sqrt(2.0);
                                else
                                    Ci = 1;

                                double Cj;
                                if ((c - 1) == 0)
                                    Cj = 1 / Math.Sqrt(2.0);
                                else
                                    Cj = 1;

                                J[i][j] = J[i][j] * Ci * Cj / 4;
                                c = c + 1;
                            }
                            z++;
                        }
                    }
            }
            else        //idct
            {
                double[][] J = zeros(Ia, Ib);
                for (int k = 1; k <= Ia; k += 8)
                    for (int l = 1; l <= Ib; l += 8)
                    {
                        int z = 1;
                        for (int i = k; i <= k + 7; i++)
                        {
                            int c = 1;
                            for (int j = l; j <= l + 7; j++)
                            {
                                int v = 1;
                                for (int x = k; x <= k + 7; x++)
                                {
                                    int b = 1;
                                    for (int y = l; y <= l + 7; y++)
                                    {
                                        double Ci;
                                        if ((v - 1) == 0)
                                            Ci = 1 / Math.Sqrt(2.0);
                                        else
                                            Ci = 1;

                                        double Cj;
                                        if ((b - 1) == 0)
                                            Cj = 1 / Math.Sqrt(2.0);
                                        else
                                            Cj = 1;

                                        J[i][j] = J[i][j] + I[x][y] * Ci * Cj * Math.Cos((2 * (c - 1) + 1) * (b - 1) * 2 * Math.Acos(0) / 16) * Math.Cos((2 * (z - 1) + 1) * (v - 1) * 2 * Math.Acos(0) / 16);
                                        b = b + 1;
                                    }
                                    v++;
                                }
                                J[i][j] = J[i][j] / 4;
                                c = c + 1;
                            }
                            z++;
                        }
                    }
            }
        }
        */
    }
}
