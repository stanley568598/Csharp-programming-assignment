using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 影像壓縮
{
    public partial class Form2 : Form
    {
        List<List<int>> results = new List<List<int>>();
        List<List<List<double>>> tables = new List<List<List<double>>>();

        public Form2()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            List<List<double>> test_case = new List<List<double>>();    //二維
            List<double> temp;
            /*
            temp.Add(1); temp.Add(2); temp.Add(6); temp.Add(7); temp.Add(14);
            test_case.Add(temp);

            temp.Clear();
            temp.Add(3); temp.Add(5); temp.Add(8); temp.Add(13); temp.Add(15);
            test_case.Add(temp);

            temp.Clear();
            temp.Add(4); temp.Add(9); temp.Add(12); temp.Add(16); temp.Add(19);
            test_case.Add(temp);

            temp.Clear();
            temp.Add(10); temp.Add(11); temp.Add(17); temp.Add(18); temp.Add(20);
            test_case.Add(temp);

            tables.Add(test_case);
            */
            ////////////////////////////////////////////////////////////////////////

            temp = new List<double>();
            temp.Add(1); temp.Add(2); temp.Add(6);
            test_case.Add(temp);

            temp = new List<double>();
            temp.Add(3); temp.Add(5); temp.Add(7); 
            test_case.Add(temp);

            temp = new List<double>();
            temp.Add(4); temp.Add(8); temp.Add(9);
            test_case.Add(temp);

            tables.Add(test_case);

            Zig_zag();
        }

        void Zig_zag()
        {
            List<int> r_R;

            bool right_up;
            int i;
            int j;

            for (int blocks = 0; blocks < tables.Count; blocks++)
            {
                r_R = new List<int>();

                right_up = false;
                i = 0;
                j = 0;
                for (int count = 0; count < tables[blocks].Count * tables[blocks].Count; count++)
                {

                    r_R.Add(Convert.ToInt32(tables[blocks][i][j]));

                    if (right_up == true)
                    {
                        i++;
                        j--;

                        if (i >= 3)
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
                    else if (right_up == false)  // false == left_down
                    {
                        i--;
                        j++;

                        if (j >= 3)
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
                }
                results.Add(r_R);
            }

            string result;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            saveFileDialog1.ShowDialog();
            StreamWriter outFile = new StreamWriter(saveFileDialog1.FileName);

            result = "";
            result += ("TESTING");
            result += Environment.NewLine;
            save_txt(result, outFile);

            for (int blocks = 0; blocks < results.Count; blocks++)
            {
                result = "";
                result += ("Block " + Convert.ToString(blocks) + " - " + Environment.NewLine);
                result += "R : ";
                for (int index = 0; index <= getEOB(results[blocks]); index++)
                {
                    //if (results[blocks][index] != 0)
                    result += Convert.ToString(results[blocks][index]);
                    result += ",";
                }
                result += ("EOB" + Environment.NewLine);

                save_txt(result, outFile);
            }
            save_txt("", outFile);
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

        private void save_txt(String write_line, StreamWriter outFile)
        {
            if (write_line == "")
                outFile.Close();
            else
                outFile.Write(write_line);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            image2 = new Bitmap(8, 8);
            int[,] subGraph = new int[8, 8]
            {{ 52, 55, 61, 66, 70, 61, 64, 73 },
            { 63, 59, 55, 90, 109, 85, 69, 72 },
            { 62, 59, 68,113,144,104, 66, 73 },
            { 63, 58, 71,122,154,106, 70, 69 },
            { 67, 61, 68,104,126, 88, 68, 70 },
            { 79, 65, 60, 70, 77, 68, 58, 75 },
            { 85, 71, 64, 59, 55, 61, 65, 83 },
            { 87, 79, 69, 68, 65, 76, 78, 94 },
            };

            image = new Bitmap(16, 16);
            int[,] Graph = new int[16, 16]
            {{ 52, 55, 61, 66, 70, 61, 64, 73,52, 55, 61, 66, 70, 61, 64, 73 },
            { 63, 59, 55, 90, 109, 85, 69, 72,63, 59, 55, 90, 109, 85, 69, 72 },
            { 62, 59, 68,113,144,104, 66, 73, 62, 59, 68,113,144,104, 66, 73 },
            { 63, 58, 71,122,154,106, 70, 69,63, 58, 71,122,154,106, 70, 69 },
            { 67, 61, 68,104,126, 88, 68, 70,67, 61, 68,104,126, 88, 68, 70 },
            { 79, 65, 60, 70, 77, 68, 58, 75, 79, 65, 60, 70, 77, 68, 58, 75 },
            { 85, 71, 64, 59, 55, 61, 65, 83 ,85, 71, 64, 59, 55, 61, 65, 83 },
            { 87, 79, 69, 68, 65, 76, 78, 94, 87, 79, 69, 68, 65, 76, 78, 94 },
            { 52, 55, 61, 66, 70, 61, 64, 73,52, 55, 61, 66, 70, 61, 64, 73 },
            { 63, 59, 55, 90, 109, 85, 69, 72,63, 59, 55, 90, 109, 85, 69, 72 },
            { 62, 59, 68,113,144,104, 66, 73, 62, 59, 68,113,144,104, 66, 73 },
            { 63, 58, 71,122,154,106, 70, 69,63, 58, 71,122,154,106, 70, 69 },
            { 67, 61, 68,104,126, 88, 68, 70,67, 61, 68,104,126, 88, 68, 70 },
            { 79, 65, 60, 70, 77, 68, 58, 75, 79, 65, 60, 70, 77, 68, 58, 75 },
            { 85, 71, 64, 59, 55, 61, 65, 83 ,85, 71, 64, 59, 55, 61, 65, 83 },
            { 87, 79, 69, 68, 65, 76, 78, 94, 87, 79, 69, 68, 65, 76, 78, 94 }
            };

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    image2.SetPixel(i, j, Color.FromArgb(subGraph[i, j], 0, 0));

            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    image.SetPixel(i, j, Color.FromArgb(Graph[i, j], 0, 0));

            DCT();

            string result;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            saveFileDialog1.ShowDialog();
            StreamWriter outFile = new StreamWriter(saveFileDialog1.FileName);

            result = "";
            result += ("TESTING");
            result += Environment.NewLine;
            save_txt(result, outFile);

            int block_width_num = image.Width / 8;

            for (int k = 0; k < R_tables.Count; k++)
            {
                for (int i = 0; i < R_tables[k].Count; i++)
                {
                    result = "";
                    for (int j = 0; j < R_tables[k][i].Count; j++)
                    {
                        result += (Convert.ToString(R_tables[k][i][j]) + " ");
                    }
                    result += (Environment.NewLine);

                    save_txt(result, outFile);
                }
                if ((k + 1) % block_width_num == 0) 
                {
                    result = (Environment.NewLine);
                    save_txt(result, outFile);
                }
            }
            save_txt("", outFile);
        }

        Bitmap image2;
        Bitmap image;

        List<List<List<double>>> R_tables = new List<List<List<double>>>();

        void DCT()    //DCT for 1 8*8 block
        {
            int n = 8;
            for (int k = 0; k < image.Height; k += n)               // image.h_position
            {
                for (int l = 0; l < image.Width; l += n)            // image.w_position
                {
                    List<List<double>> DCT_coefficient_table_R = new List<List<double>>();

                    for (int i = 0; i < n; i++)    // make 8 *8 
                    {
                        DCT_coefficient_table_R.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
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

                                    //三角函數function 只處理弧度，不處理度數，需要用 pi 做轉換；弧度=角度 * pi/180
                                    // pi 的精確值 == 2 * Math.Acos(0)，Math.PI == 大約值
                                    DCT_coefficient_table_R[u][v] += (image.GetPixel(y, x).R - 128)
                                        * Math.Cos((2 * i + 1) * (Math.Acos(0) * u / n)) * Math.Cos((2 * j + 1) * Math.Acos(0) * v / n);
                                    j++;
                                }
                                i++;
                            }
                            DCT_coefficient_table_R[u][v] *= C(u) * C(v) / (n / 2);

                            v++;
                        }
                        u++;
                    }

                    R_tables.Add(DCT_coefficient_table_R);
                }   
            }
            /*
            for (int k = 0; k < image2.Height; k += n)                     //image.h_position
                for (int l = 0; l < image2.Width; l += n)                  //image.w_position
                {
                    List<List<double>> DCT_coefficient_table_R = new List<List<double>>();

                    for (int i = 0; i < n; i++)    // make 8 *8 
                    {
                        DCT_coefficient_table_R.Add(new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                    }

                    int u = 0;                                       // block.h_num  == u
                    for (int a = k; a < k + n; a++)                  //a == block.h_position
                    {
                        int v = 0;                                  //block.w_num == v
                        for (int b = l; b < l + n; b++)             //b == block.w_position
                        {
                            int i = 0;                          //other_pixel.w_num == i
                            for (int y = l; y < l + n; y++)      //y == other_pixel.w_position
                            {
                                int j = 0;                              //other_pixel.h_num == j
                                for (int x = k; x < k + n; x++)         //x == other_pixel.h_position
                                {

                                    //三角函數function 只處理弧度，不處理度數，需要用 pi 做轉換；弧度=角度 * pi/180
                                    // pi 的精確值 == 2 * Math.Acos(0)，Math.PI == 大約值
                                    DCT_coefficient_table_R[u][v] += (image2.GetPixel(y, x).R - 128)
                                        * Math.Cos((2 * i + 1) * (Math.Acos(0) * u / n)) * Math.Cos((2 * j + 1) * Math.Acos(0) * v / n);
                                    j++;
                                }
                                i++;
                            }
                            DCT_coefficient_table_R[u][v] *= C(u) * C(v) / (n / 2);

                            v++;
                        }
                        u++;
                    }

                    R_tables.Add(DCT_coefficient_table_R);
                }
                */
        }

        double C(int x)
        {
            if (x == 0)
                return 1.0 / Math.Sqrt(2.0);
            else
                return 1.0;
        }

        void inverse_DCT()
        {
            //List<List<int>> ans = new List<List<int>>();
            //List<int> snb_ans = new List<int>();
            int[,] subdct = new int[8, 8]
            {{-416, -33, -60, 32, 48, -40, 0, 0 },
            { 0, -24, -56, 19, 26, 0, 0, 0 },
            { -42, 13, 80,-24,-40,0, 0, 0 },
            { -42, 17, 44,-29,0,0, 0, 0 },
            { 18, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            };

            int[,] ans = new int[8, 8]
            {{ 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            };

            int n = 8;
            //int block_num_each_width = image2.Width / n;

            for (int k = 0; k < 8; k += n)                          // image.h_position
            {
                for (int l = 0; l < 8; l += n)                      // image.w_position
                {
                    double R = 0;

                    int i = 0;                                      // block.h_num  == i
                    for (int c = k; c < k + n; c++)                 // c == block.h_position
                    {
                        int j = 0;                                  // block.w_num == j
                        for (int d = l; d < l + n; d++)             // d == block.w_position
                        {
                            R = 0;

                            int u = 0;                              // other_pixel.h_num == u
                            for (int x = k; x < k + n; x++)         // x == other_pixel.h_position
                            {
                                int v = 0;                          // other_pixel.w_num == v
                                for (int y = l; y < l + n; y++)     // y == other_pixel.w_position
                                {
                                    R += subdct[v,u]
                                     * C(u) * C(v) * Math.Cos((2 * i + 1) * u * Math.Acos(0) / n) * Math.Cos((2 * j + 1) * v * Math.Acos(0) / n);

                                    v++;
                                }
                                u++;
                            }
                            R /= (n / 2);


                            R = Math.Round(R) + 128;

                            ans[d,c] = (int)R;

                            j++;
                        }
                        i++;
                    }
                }
            }

            string result;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            saveFileDialog1.ShowDialog();
            StreamWriter outFile = new StreamWriter(saveFileDialog1.FileName);

            result = "";
            result += ("TESTING");
            result += Environment.NewLine;
            save_txt(result, outFile);

            for (int i = 0; i < n; i++)
            {
                result = "";
                for (int j = 0; j < n; j++)
                {
                    result += (ans[i, j] + " ");
                }
                result += (Environment.NewLine);

                save_txt(result, outFile);
            }
            save_txt("", outFile);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inverse_DCT();
        }
    }
}
