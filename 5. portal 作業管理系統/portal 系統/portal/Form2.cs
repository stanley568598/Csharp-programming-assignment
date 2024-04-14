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

namespace portal
{
    public partial class Form2 : Form
    {
        string 作業名稱 = "";
        string 題目檔案 = "";
        string 題目位置 = "";
        string 截止日期 = "";
        int 成績 = -1;

        public Form2()
        {
            InitializeComponent();

            maskedTextBox3.ValidatingType = typeof(System.DateTime);

            DateTime new_default = DateTime.Now.Date.AddDays(1);
            dateTimePicker1.Value = (DateTime.Now.Add(new_default - DateTime.Now));

            maskedTextBox3.Text = "";
            maskedTextBox4.Text = "-1";
        }

        public Form2(string form1_作業名稱, string form1_繳交檔案, string form1_繳交位置, string form1_繳交日期, int form1_成績)
        {
            InitializeComponent();

            label11.Text = "繳交檔案";
            label10.Text = "繳交日期";
            button10.Visible = false;
            button10.Enabled = false;
            dateTimePicker1.Visible = false;
            dateTimePicker1.Enabled = false;

            maskedTextBox3.ValidatingType = typeof(System.DateTime);

            作業名稱 = form1_作業名稱;
            題目檔案 = form1_繳交檔案;
            題目位置 = form1_繳交位置;
            截止日期 = form1_繳交日期;
            成績 = form1_成績;

            DateTime new_default = DateTime.Now.Date.AddDays(1);
            dateTimePicker1.Value = (DateTime.Now.Add(new_default - DateTime.Now));

            maskedTextBox1.Text = 作業名稱;
            maskedTextBox2.Text = 題目檔案;
            maskedTextBox5.Text = 題目位置;
            maskedTextBox3.Text = 截止日期;
            maskedTextBox4.Text = Convert.ToString(成績);

            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = false;
            maskedTextBox5.Enabled = false;
            maskedTextBox3.Enabled = false;
        }

        public Form2(string form1_作業名稱, string form1_題目檔案, string form1_題目位置, string form1_截止日期)
        {
            InitializeComponent();

            maskedTextBox3.ValidatingType = typeof(System.DateTime);

            作業名稱 = form1_作業名稱;
            題目檔案 = form1_題目檔案;
            題目位置 = form1_題目位置;
            截止日期 = form1_截止日期;
            成績 = -1;

            DateTime new_default = DateTime.Now.Date.AddDays(1);
            dateTimePicker1.Value = (DateTime.Now.Add(new_default - DateTime.Now));

            maskedTextBox1.Text = 作業名稱;
            maskedTextBox2.Text = 題目檔案;
            maskedTextBox5.Text = 題目位置;
            maskedTextBox3.Text = 截止日期;
            maskedTextBox4.Text = Convert.ToString(成績);

            maskedTextBox4.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)     //題目上傳
        {
            string sourceFile = "";     //上傳檔案的 Path+名稱+副檔名

            string fileName = "";       //上傳檔案的名稱+副檔名
            string sourcePath = "";     //上傳檔案的 Path

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFile = openFileDialog1.FileName;                      //上傳檔案的 Path+名稱+副檔名
                fileName = Path.GetFileName(sourceFile);                    //設定目的地檔案的名稱+類型
                sourcePath = Path.GetDirectoryName(sourceFile);             //設定目的地檔案的 Path

                maskedTextBox2.Text = fileName;
                maskedTextBox5.Text = sourcePath;
            }
            else
            {
                MessageBox.Show("請選擇要上傳的檔案 !", "操作失敗");
            }
        }

        private void button1_Click(object sender, EventArgs e)      //操作完成，紀錄全部資料   //Form1 再確認
        {
            作業名稱 = maskedTextBox1.Text;
            題目檔案 = maskedTextBox2.Text;
            題目位置 = maskedTextBox5.Text;
            截止日期 = maskedTextBox3.Text;
            成績 = Convert.ToInt32(maskedTextBox4.Text);

            if (作業名稱 != "")
                Close();
            else
            {
                MessageBox.Show("作業名稱 不可為空", "操作錯誤");
            }

            if (截止日期 != "")
                Close();
            else
            {
                MessageBox.Show("請務必設定 deadline ！", "操作錯誤");
            }
        }

        public string get_作業名稱()
        {
            return 作業名稱;
        }

        public string get_題目檔案()
        {
            return 題目檔案;
        }

        public string get_題目位置()
        {
            return 題目位置;
        }

        public string get_截止日期()
        {
            return 截止日期;
        }

        public int get_成績()
        {
            return 成績;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 '作業資料表DataSet.作業資料表' 資料表。您可以視需要進行移動或移除。
            this.作業資料表TableAdapter.Fill(this.作業資料表DataSet.作業資料表);
        }

        private void maskedTextBox3_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (maskedTextBox3.Text == "") ;
            else
            {
                if (e.IsValidInput == false)
                {
                    MessageBox.Show("截止日期 寫法錯誤", "操作錯誤");
                    e.Cancel = true; // 不被接受的輸入，無法下個動作，
                                     // 例如按“操作完成”按鈕會跳出警告MessageBox，也無法關閉視窗應用程式
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox3.Text = dateTimePicker1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)  //下載作業
        {
            string fileName = "";   //檔案名稱+副檔名
            string sourcePath = ""; //檔案 Path
            string targetPath = @"C:\Users\USER\Downloads"; //目的地 Path

            string sourceFile = ""; //檔案 Path+名稱+副檔名
            string destFile = "";   //目的地 Path+名稱+副檔名

            fileName = this.題目檔案;
            sourcePath = this.題目位置;

            sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            destFile = System.IO.Path.Combine(targetPath, fileName);

            if (File.Exists(destFile))   //目標檔案已存在
            {
                if (MessageBox.Show(("檔案已存在,是否覆蓋"), "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)    //選擇Yes==確定覆蓋
                {
                    File.Copy(sourceFile, destFile, true);  //複製檔案
                    MessageBox.Show("作業下載完成", "操作成功");
                }
                else
                {
                    MessageBox.Show("作業下載失敗，請確認下載資料夾中不存在相同檔名之資料", "操作失敗");
                }
            }
            else                        //目標檔案不存在
            {
                File.Copy(sourceFile, destFile, true);  //開始複製
                MessageBox.Show("作業下載完成", "操作成功");
            }
        }
    }
}
