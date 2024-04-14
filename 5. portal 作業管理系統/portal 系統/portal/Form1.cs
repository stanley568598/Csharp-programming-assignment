using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace portal
{
    public partial class Form1 : Form
    {
        int list_select_homework = -1;
        int list_select_submit = -1;

        bool login_success = false;

        帳號密碼 user;
        作業繳交 homework;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 '帳號密碼資料表DataSet.帳號密碼資料表' 資料表。您可以視需要進行移動或移除。
            this.帳號密碼資料表TableAdapter.Fill(this.帳號密碼資料表DataSet.帳號密碼資料表);
            // TODO: 這行程式碼會將資料載入 '導生資料表DataSet.導生資料表' 資料表。您可以視需要進行移動或移除。
            this.導生資料表TableAdapter.Fill(this.導生資料表DataSet.導生資料表);
            // TODO: 這行程式碼會將資料載入 '作業資料表DataSet.作業資料表' 資料表。您可以視需要進行移動或移除。
            this.作業資料表TableAdapter.Fill(this.作業資料表DataSet.作業資料表);
            // TODO: 這行程式碼會將資料載入 '繳交作業資料表DataSet.繳交作業資料表' 資料表。您可以視需要進行移動或移除。
            this.繳交作業資料表TableAdapter.Fill(this.繳交作業資料表DataSet.繳交作業資料表);

            panel5.BringToFront();
            panel1.BringToFront();
            panel2.BringToFront();
            panel4.BringToFront();

            panel6.SendToBack();


            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;

            login_success = false;

            timer1.Start();
        }

        private void 登入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (login_success == false)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel4.Visible = false;

                textBox1.Text = "";
                textBox2.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
        }

        private void 修改帳密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (login_success == true)
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = true;

                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void 登出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;

            login_success = false;
            user = new 帳號密碼();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = -1;

            i = 帳號密碼資料表BindingSource.Find("帳號", textBox1.Text);     //老師與學生不可有相同帳號

            if (i != -1)
            {
                listBox12.SelectedIndex = i;
                //MessageBox.Show("密碼為" + listBox9.Text);

                if (listBox9.Text == textBox2.Text)
                {
                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                    {
                        MessageBox.Show("請選擇使用者身分 !", "登入失敗");
                    }
                    else
                    {
                        if (listBox7.Text == radioButton1.Checked.ToString())
                        {
                            login_success = true;           //成功登入
                            user = new 帳號密碼();

                            user.帳號 = listBox11.Text;
                            user.密碼 = listBox9.Text;
                            user.姓名 = listBox10.Text;

                            if (radioButton1.Checked == true)
                                user.身分 = true;
                            else
                                user.身分 = false;
                        }
                        else
                            MessageBox.Show("查無此身分使用者 !", "登入失敗");
                    }
                }
                else
                    MessageBox.Show("密碼不正確 !", "登入失敗");
            }
            else
                MessageBox.Show("Not found 帳號 !", "登入失敗");

            if (login_success == true)
                MessageBox.Show("歡迎 " + user.姓名 + " 登入 portal 系統", "登入成功");

            if (login_success == true)
            {
                panel2.Visible = false;
                panel1.Visible = true;

                list_select_homework = -1;
                list_select_submit = -1;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //bool success = false;
            if (textBox5.Text != user.密碼)
                MessageBox.Show("舊密碼錯誤!", "修改密碼失敗");
            else
            {
                if (textBox3.Text != textBox4.Text)
                    MessageBox.Show("新密碼確認時不一致，請重新輸入!", "修改密碼失敗");
                else
                {
                    user.密碼 = textBox3.Text;
                    listBox9.Text = user.密碼;
                    textBox6.Text = user.密碼;
                    this.帳號密碼資料表BindingSource.EndEdit();
                    this.帳號密碼資料表TableAdapter.Update(this.帳號密碼資料表DataSet);

                    //success = true;
                    MessageBox.Show("新密碼修改成功!", "修改密碼成功");
                }
            }

            //if (success == true)
            {
                panel4.Visible = false;
                panel1.Visible = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
            if (login_success == true)
            {
                toolStripStatusLabel1.Text = user.姓名;
                if (user.身分 == true)
                    toolStripStatusLabel3.Text = "Student";
                else
                    toolStripStatusLabel3.Text = "Teacher";
            }
            else 
            {
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel3.Text = "";
            }
        }

        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox5.Items.Clear();
            listBox3.Items.Clear();

            listBox6.Items.Clear();
            listBox15.Items.Clear();
            listBox4.Items.Clear();

            listBox16.Items.Clear();
            listBox17.Items.Clear();

            if (panel1.Visible == true)
            {
                getData_personal_work();

                listBox1.BeginUpdate();     // Shutdown the painting of the ListBox as items are added.
                listBox2.BeginUpdate();
                listBox5.BeginUpdate();
                listBox3.BeginUpdate();
                for (int i = 0; i < user.作業ID.Count; i++)
                {
                    listBox1.Items.Add(i + 1);
                    listBox8.SelectedIndex = 作業資料表BindingSource.Find("作業ID", user.作業ID[i]);
                    listBox2.Items.Add(textBox8.Text);
                    listBox5.Items.Add(textBox10.Text);
                    listBox3.Items.Add(textBox9.Text);
                }
                listBox1.EndUpdate();       // Allow the ListBox to repaint and display the new items.
                listBox2.EndUpdate();
                listBox5.EndUpdate();
                listBox3.EndUpdate();

                if (user.身分 == true)
                {
                    label6.Text = "成績";
                    label23.Visible = false;
                    listBox16.Visible = false;

                    button1.Visible = true;
                    button5.Visible = false;
                    button6.Visible = false;
                    button9.Visible = false;
                    button4.Visible = false;
                }
                else
                {
                    label6.Text = "學生ID";
                    label23.Text = "成績";
                    label23.Visible = true;
                    listBox16.Visible = true;

                    button1.Visible = false;
                    button5.Visible = true;
                    button6.Visible = true;
                    button9.Visible = true;
                    button4.Visible = true;
                }
            }
        }

        void getData_personal_work()
        {
            user.作業ID.Clear();
            user.學生ID.Clear();
            user.老師ID.Clear();

            if (user.身分 == true)   //student
            {
                for (int i = 0; i < 導生資料表BindingSource.Count; i++)
                {
                    listBox13.SelectedIndex = i;
                    if (textBox17.Text == user.帳號)
                        user.老師ID.Add(textBox18.Text);
                }
                for (int j = 0; j < 作業資料表BindingSource.Count; j++)
                {
                    listBox8.SelectedIndex = j;
                    if(user.老師ID.Contains(textBox12.Text))
                        user.作業ID.Add(textBox7.Text);
                }
            }
            else                    //teacher
            {
                for (int i = 0; i < 導生資料表BindingSource.Count; i++)
                {
                    listBox13.SelectedIndex = i;
                    if (textBox18.Text == user.帳號)
                        user.學生ID.Add(textBox17.Text);
                }
                for (int j = 0; j < 作業資料表BindingSource.Count; j++)
                {
                    listBox8.SelectedIndex = j;

                    if (textBox12.Text == user.帳號)
                        user.作業ID.Add(textBox7.Text);
                }
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            list_select_homework = listBox1.SelectedIndex;
            move_all_index_in_homework();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_homework = listBox2.SelectedIndex;
            move_all_index_in_homework();
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_homework = listBox5.SelectedIndex;
            move_all_index_in_homework();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_homework = listBox3.SelectedIndex;
            move_all_index_in_homework();
        }

        void move_all_index_in_homework()
        {
            listBox1.SelectedIndex = list_select_homework;
            listBox2.SelectedIndex = list_select_homework;
            listBox5.SelectedIndex = list_select_homework;
            listBox3.SelectedIndex = list_select_homework;

            homework = new 作業繳交();
            getData_work_done();

            listBox6.Items.Clear();
            listBox15.Items.Clear();
            listBox4.Items.Clear();
            listBox16.Items.Clear();
            listBox17.Items.Clear();

            listBox6.BeginUpdate();
            listBox15.BeginUpdate();
            listBox4.BeginUpdate();
            listBox16.BeginUpdate();
            listBox17.BeginUpdate();
            if (user.身分 == true)
            {
                for (int i = 0; i < homework.繳交檔案.Count; i++)
                {
                    listBox6.Items.Add(homework.繳交檔案[i]);
                    listBox15.Items.Add(homework.繳交時間[i]);
                    listBox4.Items.Add(homework.成績[i]);

                    DateTime deadline = Convert.ToDateTime(listBox3.Text);
                    DateTime submitTime = Convert.ToDateTime(homework.繳交時間[i]);
                    TimeSpan ts = submitTime.Subtract(deadline);    //計算時間差 
                    if (ts.TotalDays > 0)
                        listBox17.Items.Add(Convert.ToString(Math.Ceiling(ts.TotalDays)));
                    else
                        listBox17.Items.Add("");
                }
            }
            else
            {
                for (int i = 0; i < homework.繳交檔案.Count; i++)
                {
                    listBox6.Items.Add(homework.繳交檔案[i]);
                    listBox15.Items.Add(homework.繳交時間[i]);
                    listBox4.Items.Add(homework.學生ID[i]);
                    listBox16.Items.Add(homework.成績[i]);

                    DateTime deadline = Convert.ToDateTime(listBox3.Text);
                    DateTime submitTime = Convert.ToDateTime(homework.繳交時間[i]);
                    TimeSpan ts = submitTime.Subtract(deadline);    //計算時間差                                                                                                                                                                                                                                           //計算時間差
                    if (ts.TotalDays > 0)
                        listBox17.Items.Add(Convert.ToString(Math.Ceiling(ts.TotalDays)));
                    else
                        listBox17.Items.Add("");
                }
            }
            listBox6.EndUpdate();
            listBox15.EndUpdate();
            listBox4.EndUpdate();
            listBox16.EndUpdate();
            listBox17.EndUpdate();
        }

        void getData_work_done()
        {
            listBox8.SelectedIndex = 作業資料表BindingSource.Find("作業ID", user.作業ID[list_select_homework]);

            if (user.身分 == true)   //student
            {
                for (int i = 0; i < 繳交作業資料表BindingSource.Count; i++)
                {
                    listBox14.SelectedIndex = i;
                    if (textBox20.Text == textBox7.Text)
                    {
                        if (textBox19.Text == user.帳號)
                        {
                            homework.識別碼.Add(listBox14.Text);
                            homework.繳交檔案.Add(textBox16.Text);
                            homework.作業位置.Add(textBox15.Text);
                            homework.繳交時間.Add(textBox14.Text);
                            homework.成績.Add(textBox13.Text);
                        }
                    }
                }
            }
            else                    //teacher
            {
                for (int i = 0; i < 繳交作業資料表BindingSource.Count; i++)
                {
                    listBox14.SelectedIndex = i;

                    if (textBox20.Text == textBox7.Text)
                    {
                        bool exist = false;
                        for (int j = 0; j < homework.學生ID.Count; j++)
                        {
                            if (homework.學生ID[j] == textBox19.Text)     //每位學生只以最新資料為其最終評分檔案
                            {
                                if (Convert.ToDateTime(homework.繳交時間[j]) > Convert.ToDateTime(textBox14.Text))
                                    exist = true;
                                else
                                {
                                    homework.識別碼[j] = listBox14.Text;
                                    homework.學生ID[j] = textBox19.Text;
                                    homework.繳交檔案[j] = textBox16.Text;
                                    homework.作業位置[j] = textBox15.Text;
                                    homework.繳交時間[j] = textBox14.Text;
                                    homework.成績[j] = textBox13.Text;
                                    exist = true;
                                }
                                break;
                            }
                        }
                        if (exist == false)
                        {
                            homework.識別碼.Add(listBox14.Text);
                            homework.學生ID.Add(textBox19.Text);
                            homework.繳交檔案.Add(textBox16.Text);
                            homework.作業位置.Add(textBox15.Text);
                            homework.繳交時間.Add(textBox14.Text);
                            homework.成績.Add(textBox13.Text);
                        }
                    }
                }
            }
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_submit = listBox6.SelectedIndex;
            move_all_index_in_submit();
        }

        private void listBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_submit = listBox15.SelectedIndex;
            move_all_index_in_submit();
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_submit = listBox4.SelectedIndex;
            move_all_index_in_submit();
        }

        private void listBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_submit = listBox16.SelectedIndex;
            move_all_index_in_submit();
        }

        private void listBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_select_submit = listBox17.SelectedIndex;
            move_all_index_in_submit();
        }

        void move_all_index_in_submit()
        {
            listBox6.SelectedIndex = list_select_submit;
            listBox15.SelectedIndex = list_select_submit;
            listBox4.SelectedIndex = list_select_submit;
            listBox17.SelectedIndex = list_select_submit;

            if (user.身分 != true)                        //teacher才有這欄資料
                listBox16.SelectedIndex = list_select_submit;
        }
        
        private void button1_Click(object sender, EventArgs e)  //作業上傳
        {
            string fileName = "";       //上傳檔案的名稱+副檔名
            string targetName = "";     //目的地檔案的名稱+副檔名

            string fileLocation = Directory.GetCurrentDirectory() + "\\作業檔案";  //預設相對位置
            string targetPath = "";

            string sourceFile = "";     //上傳檔案的 Path+名稱+副檔名
            string destFile = "";       //目的檔案的 Path+名稱+副檔名

            if (list_select_homework != -1)     //確定為存在的作業
            {
                listBox8.SelectedIndex = 作業資料表BindingSource.Find("作業ID", user.作業ID[list_select_homework]);
                targetName = "作業 ID-" + textBox7.Text + "\\學生 ID-" + user.帳號;

                Random rd = new Random();
                int ver = rd.Next();
                while (homework.識別碼.Contains(Convert.ToString(ver)))
                {
                    ver = rd.Next();
                }

                targetName += ("\\版本-" + Convert.ToString(ver));
                targetPath = System.IO.Path.Combine(fileLocation, targetName);

                System.IO.Directory.CreateDirectory(targetPath);                    //建立 題目-目的地資料夾

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sourceFile = openFileDialog1.FileName;                      //上傳檔案的 Path+名稱+副檔名
                    fileName = Path.GetFileName(sourceFile);                    //設定目的地檔案的名稱+類型
                    destFile = System.IO.Path.Combine(targetPath, fileName);    //設定目的地檔案的 Path+名稱+類型

                    File.Copy(sourceFile, destFile, true);  //開始複製
                    MessageBox.Show("作業上傳完成", "操作成功");

                    listBox8.SelectedIndex = 作業資料表BindingSource.Find("作業ID", user.作業ID[list_select_homework]);

                    listBox14.ClearSelected();
                    textBox20.Text = textBox7.Text;                         //作業ID
                    textBox19.Text = user.帳號;                             //學生ID
                    textBox16.Text = fileName;                              //作業檔案名稱
                    textBox15.Text = targetName;                            //檔案位置
                    textBox14.Text = DateTime.Now.ToString();               //繳交時間
                    textBox13.Text = "-1";                                  //成績
                    textBox21.Text = Convert.ToString(ver);                 //識別碼

                    this.繳交作業資料表TableAdapter.Insert(Convert.ToInt32(textBox21.Text), textBox20.Text, textBox19.Text, textBox15.Text, Convert.ToInt32(textBox13.Text), DateTime.Parse(textBox14.Text), textBox16.Text);
                    this.繳交作業資料表TableAdapter.Fill(this.繳交作業資料表DataSet.繳交作業資料表);

                    DateTime deadline = DateTime.Parse(listBox3.Text);
                    DateTime submitTime = DateTime.Parse(textBox14.Text);
                    TimeSpan ts = submitTime.Subtract(deadline);    //計算時間差 
                    if (ts.TotalDays > 0) 
                    {
                        MessageBox.Show("已超過作業繳交期限，將註記遲交天數 " + Convert.ToString(Math.Ceiling(ts.TotalDays)) + " !", "遲交通知");
                    }
                    panel1.Visible = false; //刷新
                    panel1.Visible = true;
                }
                else
                {
                    MessageBox.Show("請選擇要上傳的檔案 !", "操作失敗");
                }
            }
            else
            {
                MessageBox.Show("請先選擇作業區 !", "操作失敗");
            }
        }
        private void button2_Click(object sender, EventArgs e)  //作業下載
        {
            string fileLocation = Directory.GetCurrentDirectory() + "\\作業檔案";  //預設相對位置

            string fileName = "";   //檔案名稱+副檔名
            string sourcePath = ""; //檔案 Path
            string targetPath = @"C:\Users\USER\Downloads"; //目的地 Path

            string sourceFile = ""; //檔案 Path+名稱+副檔名
            string destFile = "";   //目的地 Path+名稱+副檔名

            if (list_select_submit != -1)
            {
                fileName = homework.繳交檔案[list_select_submit];
                sourcePath = homework.作業位置[list_select_submit];

                sourceFile = System.IO.Path.Combine(fileLocation, sourcePath, fileName);
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
            else
            {
                MessageBox.Show("請先選擇要下載的作業欄位 !", "操作失敗");
            }
        }

        private void button3_Click(object sender, EventArgs e)  //題目下載
        {
            string fileLocation = Directory.GetCurrentDirectory() + "\\作業檔案";  //預設相對位置

            string fileName = "";       //題目檔案名稱+副檔名
            string sourcePath = "";     //題目檔案 Path
            string targetPath = @"C:\Users\USER\Downloads"; //目的地 Path

            string sourceFile = ""; //題目檔案 Path+名稱+副檔名
            string destFile = "";   //目的地 Path+名稱+副檔名

            if (list_select_homework != -1)
            {
                listBox8.SelectedIndex = 作業資料表BindingSource.Find("作業ID", user.作業ID[list_select_homework]);

                fileName = textBox10.Text;
                sourcePath = textBox11.Text;

                sourceFile = System.IO.Path.Combine(fileLocation, sourcePath, fileName);
                destFile = System.IO.Path.Combine(targetPath, fileName);

                if (File.Exists(destFile))   //目標檔案已存在
                {
                    if (MessageBox.Show(("檔案已存在,是否覆蓋"), "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)    //選擇Yes==確定覆蓋
                    {
                        File.Copy(sourceFile, destFile, true);  //複製檔案
                        MessageBox.Show("題目下載完成", "操作成功");
                    }
                    else
                    {
                        MessageBox.Show("題目下載失敗，請確認下載資料夾中不存在相同檔名之資料", "操作失敗");
                    }
                }
                else                        //檔案不存在
                {
                    File.Copy(sourceFile, destFile, true);  //開始複製
                    MessageBox.Show("題目下載完成", "操作成功");
                }
            }
            else
            {
                MessageBox.Show("請先選擇要下載的題目欄位 !", "操作失敗");
            }
        }

        private void button5_Click(object sender, EventArgs e)  //增新作業
        {
            // 作業id不可相同
            Random rd = new Random();
            int different_作業ID_變數 = 0;
            bool exist = true;
            while (exist == true)
            {
                different_作業ID_變數 = rd.Next();
                exist = false;
                for (int i = 0; i < 作業資料表BindingSource.Count; i++)
                {
                    listBox8.SelectedIndex = i;
                    if (textBox7.Text == user.帳號 + "-" + Convert.ToString(different_作業ID_變數))
                        exist = true;
                }
            }
            //get_different作業id

            Form2 x = new Form2();
            x.Text = "增新作業";
            x.TopMost = true;  //移到最上層
            x.ShowDialog();    // Show Form2

            if (x.get_作業名稱() == "" || x.get_截止日期() == "") 
            {
                MessageBox.Show("無獲取得足夠新增作業的相關資料 ! \n請再次操作新增作業流程，並且確認 作業名稱/截止日期 是否皆輸入完整", "操作失敗");
            }
            else
            {
                string fileName = x.get_題目檔案();       //上傳題目檔案的名稱+副檔名
                string sourcePath = x.get_題目位置();

                string fileLocation = Directory.GetCurrentDirectory() + "\\作業檔案";  //預設相對位置
                string targetName = "作業 ID-" + user.帳號 + "-" + Convert.ToString(different_作業ID_變數);

                string targetPath = System.IO.Path.Combine(fileLocation, targetName);

                System.IO.Directory.CreateDirectory(targetPath);                    //建立 題目-目的地資料夾

                if (x.get_題目檔案() != "")
                {
                    string sourceFile = System.IO.Path.Combine(sourcePath, fileName);     //上傳檔案的 Path+名稱+副檔名
                    string destFile = System.IO.Path.Combine(targetPath, fileName);       //目的檔案的 Path+名稱+副檔名
                    File.Copy(sourceFile, destFile, true);  //開始複製
                    //MessageBox.Show("題目上傳完成", "操作成功");
                }

                listBox8.ClearSelected();
                textBox7.Text = user.帳號 + "-" + Convert.ToString(different_作業ID_變數);
                textBox8.Text = x.get_作業名稱();
                textBox9.Text = x.get_截止日期();

                textBox10.Text = x.get_題目檔案();
                textBox11.Text = targetName;
                textBox12.Text = user.帳號;

                this.作業資料表TableAdapter.Insert(textBox7.Text, textBox8.Text, DateTime.Parse(textBox9.Text), textBox10.Text, textBox11.Text, textBox12.Text);
                this.作業資料表TableAdapter.Fill(this.作業資料表DataSet.作業資料表);

                string new_student_homework_path = "";
                for (int i = 0; i < user.學生ID.Count; i++)
                {
                    new_student_homework_path = targetPath + "\\學生 ID-" + user.學生ID[i];
                    System.IO.Directory.CreateDirectory(new_student_homework_path);
                }

                MessageBox.Show("新增作業完成 !", "操作成功");

                panel1.Visible = false; //刷新
                panel1.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)  //刪除作業
        {
            if (list_select_homework != -1)
            {
                string 作業ID = user.作業ID[list_select_homework];              //用於後面刪除其他資料表中相關的資料
                //string question_Path = textBox11.Text;      //題目檔案 Path -> 用於後面刪除存在檔案夾中資料
                string question_Path = Directory.GetCurrentDirectory() + "\\作業檔案" + "\\作業 ID-" + textBox7.Text;

                this.作業資料表TableAdapter.Delete(Convert.ToInt32(listBox8.Text), textBox7.Text, textBox8.Text, DateTime.Parse(textBox9.Text), textBox10.Text, textBox11.Text, textBox12.Text);
                this.作業資料表TableAdapter.Fill(this.作業資料表DataSet.作業資料表);

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(question_Path);
                dir.Delete(true);    // Delete this dir and all subdirs.

                for (int i = 0; i < homework.識別碼.Count; i++)
                {
                    listBox14.SelectedIndex = 繳交作業資料表BindingSource.Find("識別碼", homework.識別碼[i]);
                    if (textBox20.Text == 作業ID)
                    {
                        this.繳交作業資料表TableAdapter.Delete(Convert.ToInt32(listBox14.Text), textBox20.Text, textBox19.Text, textBox15.Text, Convert.ToInt32(textBox13.Text), DateTime.Parse(textBox14.Text), textBox16.Text);
                        this.繳交作業資料表TableAdapter.Fill(this.繳交作業資料表DataSet.繳交作業資料表);
                    }
                }

                MessageBox.Show("作業刪除完成 !", "操作成功");

                panel1.Visible = false; //刷新
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("請先選擇作業區 !", "操作失敗");
            }
        }

        private void button4_Click(object sender, EventArgs e)  //修改作業
        {
            Form2 x;
            if (list_select_homework != -1)
            {
                listBox8.SelectedIndex = 作業資料表BindingSource.Find("作業ID", user.作業ID[list_select_homework]);
                x = new Form2(textBox8.Text, textBox10.Text, textBox11.Text, textBox9.Text);
                x.TopMost = true;  //移到最上層
                x.ShowDialog();    // Show Form2

                textBox8.Text = x.get_作業名稱();

                string targetPath = Directory.GetCurrentDirectory() + "\\作業檔案" + "\\作業 ID-" + textBox7.Text;
                if (textBox10.Text != "")
                    System.IO.File.Delete(System.IO.Path.Combine(targetPath, textBox10.Text));  //刪除原本的資料

                if (x.get_題目檔案() != "")
                    File.Copy(System.IO.Path.Combine(x.get_題目位置(), x.get_題目檔案()), System.IO.Path.Combine(targetPath, x.get_題目檔案()), true);  //加入更新資料

                textBox10.Text = x.get_題目檔案();
                textBox9.Text = x.get_截止日期();

                this.作業資料表BindingSource.EndEdit();
                this.作業資料表TableAdapter.Update(this.作業資料表DataSet);

                if (MessageBox.Show(("已修改作業內容，是否將已批改成績全部歸零?"), "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < 繳交作業資料表BindingSource.Count; i++)
                    {
                        listBox14.SelectedIndex = i;
                        if (textBox20.Text == textBox7.Text)
                        {
                            if (user.學生ID.Contains(textBox19.Text))
                            {
                                textBox13.Text = "-1";
                                this.繳交作業資料表BindingSource.EndEdit();
                                this.繳交作業資料表TableAdapter.Update(this.繳交作業資料表DataSet);
                            }
                        }
                    }
                    MessageBox.Show("成績皆已歸零", "操作成功");
                }

                MessageBox.Show("作業內容 修改完成", "操作成功");
                panel1.Visible = false; //刷新
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("請先選擇作業區 !", "操作失敗");
            }
        }

        private void button6_Click(object sender, EventArgs e)  //成績批改
        {
            Form2 x;
            if (list_select_submit != -1)
            {
                listBox14.SelectedIndex = 繳交作業資料表BindingSource.Find("識別碼", homework.識別碼[list_select_submit]);
                x = new Form2(listBox2.Text, textBox16.Text, textBox15.Text, textBox14.Text, Convert.ToInt32(textBox13.Text));
                x.TopMost = true;  //移到最上層
                x.ShowDialog();    // Show Form2

                textBox13.Text = Convert.ToString(x.get_成績());

                this.繳交作業資料表BindingSource.EndEdit();
                this.繳交作業資料表TableAdapter.Update(this.繳交作業資料表DataSet);

                MessageBox.Show("成績批改完成", "操作成功");
                panel1.Visible = false; //刷新
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("請先選擇要下載的作業欄位 !", "操作失敗");
            }
        }
    }

    public class 帳號密碼
    {
        public string 帳號 = "";
        public string 密碼 = "";
        public string 姓名 = "";
        public bool 身分 = true; // true = student  false = teacher

        public List<string> 作業ID = new List<string>();
        public List<string> 老師ID = new List<string>(); //學生有不同老師ID
        public List<string> 學生ID = new List<string>(); //老師有不同學生ID
    }

    public class 作業繳交
    {
        public List<string> 繳交檔案 = new List<string>();
        public List<string> 作業位置 = new List<string>();
        public List<string> 繳交時間 = new List<string>(); 
        public List<string> 學生ID = new List<string>();      //學生介面不用
        public List<string> 成績 = new List<string>();
        public List<string> 識別碼 = new List<string>();
    }
}
