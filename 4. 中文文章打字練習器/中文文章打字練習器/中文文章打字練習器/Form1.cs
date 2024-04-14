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

namespace 中文文章打字練習器
{
    public partial class Form1 : Form
    {
        int supervisor_mode = -1;   //  0 = add, 1 = modify ,-1 = writer

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO: 這行程式碼會將資料載入 'articleDataSet.Article' 資料表。您可以視需要進行移動或移除。
            this.articleTableAdapter.Fill(this.articleDataSet.Article);
            textBox8.Text = textBox1.Text + textBox3.Text + textBox4.Text + textBox5.Text + textBox6.Text + textBox7.Text;
        }

        private void 增加題目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
 //           comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            supervisor_mode = 0;
            textBox8.ReadOnly = false;
            textBox14.ReadOnly = true;
            BeginInvoke(new Action(() => { textBox8.Clear(); }));
            textBox14.Clear();
        }

        private void 修改題目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            supervisor_mode = 1;
            textBox8.ReadOnly = false;
            textBox14.ReadOnly = true;
            textBox8.Text = textBox1.Text + textBox3.Text + textBox4.Text + textBox5.Text + textBox6.Text + textBox7.Text;
            textBox14.Clear();
        }

        private void 輸入完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(supervisor_mode==0)
            {
                question_divide();
                answer_divide();

                this.articleTableAdapter.Insert(comboBox1.Items.Count + 1, Convert.ToString(textBox8.Text.Length) + "字", textBox1.Text, "X", textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text);
                this.articleTableAdapter.Fill(this.articleDataSet.Article);
                supervisor_mode = -1;
                textBox8.ReadOnly = true;
                textBox14.ReadOnly = false;
            }
            else if(supervisor_mode==1)
            {
                label4.Text = Convert.ToString(textBox8.Text.Length) + "字";
                question_divide();

                this.articleBindingSource.EndEdit();
                this.articleTableAdapter.Update(this.articleDataSet);
                supervisor_mode = -1;
                textBox8.ReadOnly = true;
                textBox14.ReadOnly = false;
            }
            else if (supervisor_mode == -1)
            {
                this.articleBindingSource.EndEdit();
                this.articleTableAdapter.Update(this.articleDataSet);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox14.Text = textBox2.Text + textBox9.Text + textBox10.Text + textBox11.Text + textBox12.Text + textBox13.Text;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            answer_divide();

            compare(sender, e);

            textBox14.Text = textBox2.Text + textBox9.Text + textBox10.Text + textBox11.Text + textBox12.Text + textBox13.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.BeginInvoke(new Action(() => { textBox8.Text = textBox1.Text + textBox3.Text + textBox4.Text + textBox5.Text + textBox6.Text + textBox7.Text; }));
            textBox14.Clear();
        }

        void compare(object sender, EventArgs e)
        {
            if (textBox1.Text.CompareTo(textBox2.Text) == 0 && textBox3.Text.CompareTo(textBox9.Text) == 0 && textBox4.Text.CompareTo(textBox10.Text) == 0 && textBox5.Text.CompareTo(textBox11.Text) == 0 && textBox6.Text.CompareTo(textBox12.Text) == 0)
            {
                label3.Text = "O";
            }
            else
            {
                label3.Text = "X";
            }
            輸入完成ToolStripMenuItem_Click(sender, e);
        }

        void question_divide()
        {
            if (0 <= textBox8.Text.Length && textBox8.Text.Length < 249)
            {
                textBox1.Text = textBox8.Text.Substring(0, textBox8.Text.Length);
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
            else if (250 <= textBox8.Text.Length && textBox8.Text.Length < 499)
            {
                textBox1.Text = textBox8.Text.Substring(0, 250);
                textBox3.Text = textBox8.Text.Substring(250, textBox8.Text.Length-250);
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
            else if (500 <= textBox8.Text.Length && textBox8.Text.Length < 749)
            {
                textBox1.Text = textBox8.Text.Substring(0, 250);
                textBox3.Text = textBox8.Text.Substring(250, 250);
                textBox4.Text = textBox8.Text.Substring(500, textBox8.Text.Length-500);
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
            else if (750 <= textBox8.Text.Length && textBox8.Text.Length < 999)
            {
                textBox1.Text = textBox8.Text.Substring(0, 250);
                textBox3.Text = textBox8.Text.Substring(250, 250);
                textBox4.Text = textBox8.Text.Substring(500, 250);
                textBox5.Text = textBox8.Text.Substring(750, textBox8.Text.Length- 750);
                textBox6.Text = "";
                textBox7.Text = "";
            }
            else if (1000 <= textBox8.Text.Length && textBox8.Text.Length < 1249)
            {
                textBox1.Text = textBox8.Text.Substring(0, 250);
                textBox3.Text = textBox8.Text.Substring(250, 250);
                textBox4.Text = textBox8.Text.Substring(500, 250);
                textBox5.Text = textBox8.Text.Substring(750, 250);
                textBox6.Text = textBox8.Text.Substring(1000, textBox8.Text.Length-1000);
                textBox7.Text = "";
            }
            else if (1250 <= textBox8.Text.Length && textBox8.Text.Length < 1499)
            {
                textBox1.Text = textBox8.Text.Substring(0, 250);
                textBox3.Text = textBox8.Text.Substring(250, 250);
                textBox4.Text = textBox8.Text.Substring(500, 250);
                textBox5.Text = textBox8.Text.Substring(750, 250);
                textBox6.Text = textBox8.Text.Substring(1000, 250);
                textBox7.Text = textBox8.Text.Substring(1250, textBox8.Text.Length- 1250);
            }
            else
            {
                textBox1.Text = "不符限制規定";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        void answer_divide()
        {
            if (0 <= textBox14.Text.Length && textBox14.Text.Length < 249)
            {
                textBox2.Text = textBox14.Text.Substring(0, textBox14.Text.Length);
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
            }
            else if (250 <= textBox14.Text.Length && textBox14.Text.Length < 499)
            {
                textBox2.Text = textBox14.Text.Substring(0, 250);
                textBox9.Text = textBox14.Text.Substring(250, textBox14.Text.Length-250);
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
            }
            else if (500 <= textBox14.Text.Length && textBox14.Text.Length < 749)
            {
                textBox2.Text = textBox14.Text.Substring(0, 250);
                textBox9.Text = textBox14.Text.Substring(250, 250);
                textBox10.Text = textBox14.Text.Substring(500, textBox14.Text.Length-500);
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
            }
            else if (750 <= textBox14.Text.Length && textBox14.Text.Length < 999)
            {
                textBox2.Text = textBox14.Text.Substring(0, 250);
                textBox9.Text = textBox14.Text.Substring(250, 250);
                textBox10.Text = textBox14.Text.Substring(500, 250);
                textBox11.Text = textBox14.Text.Substring(750, textBox14.Text.Length-750);
                textBox12.Text = "";
                textBox13.Text = "";
            }
            else if (1000 <= textBox14.Text.Length && textBox14.Text.Length < 1249)
            {
                textBox2.Text = textBox14.Text.Substring(0, 250);
                textBox9.Text = textBox14.Text.Substring(250, 250);
                textBox10.Text = textBox14.Text.Substring(500, 250);
                textBox11.Text = textBox14.Text.Substring(750, 250);
                textBox12.Text = textBox14.Text.Substring(1000, textBox14.Text.Length-1000);
                textBox13.Text = "";
            }
            else if (1250 <= textBox14.Text.Length && textBox14.Text.Length < 1499)
            {
                textBox2.Text = textBox14.Text.Substring(0, 250);
                textBox9.Text = textBox14.Text.Substring(250, 250);
                textBox10.Text = textBox14.Text.Substring(500, 250);
                textBox11.Text = textBox14.Text.Substring(750, 250);
                textBox12.Text = textBox14.Text.Substring(1000, 250);
                textBox13.Text = textBox14.Text.Substring(1250, textBox14.Text.Length- 1250);
            }
            else
            {
                textBox1.Text = "不符限制規定";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            label6.Text = Convert.ToString(textBox14.Text.Length)+"字";
        }

        private void 字型大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox8.Font = fontDialog1.Font;
                textBox14.Font = fontDialog1.Font;
            }
        }

        private void 插入特殊符號ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (supervisor_mode == -1) 
            {
                textBox14.Focus();
                textBox14.Select(textBox14.Text.Length, 0);
                this.textBox14.ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            }
            else
            {
                textBox8.Focus();
                textBox8.Select(textBox8.Text.Length, 0);
                this.textBox8.ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            }

            System.Windows.Forms.SendKeys.Send("^%{,}");
        }
    }
}
