using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WP_HW2_1061504
{
    public partial class Form1 : Form
    {
        Rectangle[] rect = new Rectangle[9];        //配置9個 Rectangle 給rect[]，獲得9個pointer
        Pen pen_black = new Pen(Color.Black, 3);    //框框
        Pen pen_blue = new Pen(Color.Blue, 3);      //圈叉
        Pen pen_red = new Pen(Color.Red, 5);        //遊戲結束
        int[] state = new int[9];                   //各個格子狀態，0=未,1=圈,2=叉
        bool over = false;                          //遊戲狀態，false=未結束，true=結束

                                                //電腦戰術：-1==未啟動，非-1==下一個位置
                                                //戰術優先度：強攻>防守>布局
        int attack = -1;                            //電腦戰術-強攻
        int defend = -1;                            //電腦戰術-防守
        int arrangement = -1;                       //電腦戰術-布局

        string[] record_array = new string[9] { (""), (""), (""), (""), (""), (""), (""), (""), ("") };
        int num_choose = 0;                     //紀錄位置順序，奇數是圈，偶數是叉
        string[] trap;                          //陷阱紀錄
        string[] Untie_trap;                    //開陷阱方法

        public Form1()
        {
            InitializeComponent();
            this.trap = new string[] { ("0"), ("2"), ("6"), ("8"),
                        ("045"),("047"),  ("243"),("247"),      ("641"),("645"),  ("841"),("843"),
                        ("048"),         ("246"),             ("840"),         ("642"),

                        ("4"),      ("408"),("426"),("462"),("480"),

                        ("1"), ("3"),("5"),("7")    };
            this.Untie_trap = new string[] { ("4"), ("4"), ("4"), ("4"),
                       ("1278"),("3568"),("1067"),("3568"),("0235"),("1278"),("0235"),("0167"),
                       ("1357"),        ("1357"),          ("1357"),        ("1357"),

                       ("0268"),        ("26"),("08"),("08"),("26"),

                       ("0247"),("0456"),("2348"),("1468")  };
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    rect[i + j * 3] = new Rectangle(i * 60 + 5, j * 60 + 30, 60, 60);
            //重新配置正確的 Rectangle 給rect[i] 指標

            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);                    //更新畫面

            for (int i = 0; i < 9; i++)
            {
                g.DrawRectangle(pen_black, rect[i]);    // 繪出矩形

                if (state[i] == 1)                      //依各個格子狀態，添加對應圖型
                    Draw_Circle(rect[i]);
                else if (state[i] == 2)
                    Draw_Cross(rect[i]);
            }

            Result(1);                                  //繪製三圈連線
            Result(2);                                  //繪製三叉連線
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.over == false) //當有遊戲結果產生，遊戲停止功能
            {
                for (int i = 0; i < 9; i++)
                    if (rect[i].Contains(e.Location) && state[i] == 0)
                    {
                        state[i] = 1;
                        this.record_array[this.num_choose]=Convert.ToString(i);
                        this.num_choose += 1;
                        Player2();  //不可寫在迴圈外，否則plyer1非在規定範圍內點擊也會觸發player2
                    }
                
                Invalidate();                           //更新
            }
        }

        void Player2()
        {
            Result(1);                  //Player1 下完後檢查 勝利或和局
                                        //若 已勝利 或 已和局，則不需執行 -> over=true
            if (this.over == false)
            {
                for (int a = 0; a < 3; a++)         //檢查雙子，設定 電腦戰術：強攻 & 防守
                {
                    Check_2(a * 3, a * 3 + 1, a * 3 + 2);
                    Check_2(a, a + 3, a + 6);
                }
                Check_2(0, 4, 8);
                Check_2(2, 4, 6);

                if (this.arrangement == -1)         //電腦戰術：布陣
                {
                    Random rand = new Random();     //使用亂數類別
                    int i = rand.Next(9);
                    while (state[i] != 0)
                    {
                        i = rand.Next(9);
                    }
                    this.arrangement = i;           //i=0~8，同時不為 已下過位置

                    //解除陷阱 //record 紀錄圈圈的每一步，arrang_array 解除陷阱的可能位置，k 可能位置的數量
                    string record = record_array[0] + record_array[1] + record_array[2] + record_array[3] +
                        record_array[4] + record_array[5] + record_array[6] + record_array[7] + record_array[8];

                    int[] arrang_array = new int[9];
                    int k = 0;
                    for (int j = 0; j < this.trap.Length; j++)
                        if (record == this.trap[j])
                        {
                            for (k = 0; k < this.Untie_trap[j].Length; k++)
                                arrang_array[k] = this.Untie_trap[j][k] - '0';

                            i = rand.Next(k);
                            this.arrangement = arrang_array[i];
                        }
                }

                //電腦戰術：強攻 > 防守 > 布陣
                if (this.attack != -1)
                {
                    state[this.attack] = 2;
                    this.record_array[this.num_choose] = Convert.ToString(this.attack);
                }
                else if (this.defend != -1)
                {
                    state[this.defend] = 2;
                    this.record_array[this.num_choose] = Convert.ToString(this.defend);
                }
                else                                //無需強攻，也無需防守，始可布陣
                {
                    state[this.arrangement] = 2;
                    this.record_array[this.num_choose] = Convert.ToString(this.arrangement);
                }  

                this.num_choose += 1;

                this.attack = -1;
                this.defend = -1;
                this.arrangement = -1;

                Result(2);              //Player2 下完，檢查失敗 or 和局
                Invalidate();
            }
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                state[i] = 0;

            label1.Text = "";

            this.over = false;

            this.attack = -1;
            this.defend = -1;
            this.arrangement = -1;

            this.record_array = new string[9] { (""), (""), (""), (""), (""), (""), (""), (""), ("") };
            this.num_choose = 0;

            Invalidate();
        }

        void Draw_Circle(Rectangle r)
        {
            Graphics g = this.CreateGraphics();
            g.DrawArc(pen_blue, r.X + 5, r.Y + 5, 50, 50, 0, 360);
        }

        void Draw_Cross(Rectangle r)
        {
            Graphics g = this.CreateGraphics();
            g.DrawLine(pen_blue, r.X + 5, r.Y + 5, r.X + 55, r.Y + 55);
            g.DrawLine(pen_blue, r.X + 55, r.Y + 5, r.X + 5, r.Y + 55);
        }

        void Result(int p)
        {
                            //檢查和局，若上次檢查已獲取遊戲結果則不執行，保留 繪製連線的功能
                                //若無此判斷，當和局和另一遊戲結果同時發生
            if (over == false)  //遊戲會不停刷新 和局&另一遊戲結果
            {
                int i;
                for (i = 0; i < 9; i++)
                    if (state[i] == 0)
                        break;
                if (i == 9)
                {
                    label1.Text = "Draw!";
                    this.over = true;
                }
            }

                                        //檢查勝利或失敗，當有遊戲結果產生時，則覆蓋和局結果
                                        //具有畫線功能，由 p 確認 目前檢查 哪一玩家遊戲結果
                                        //當有遊戲結果產生時，設定this.over=true;;
            for (int a = 0; a < 3; a++)
            {
                Check_3(a * 3, a * 3 + 1, a * 3 + 2, p);  //檢查橫線
                Check_3(a, a + 3, a + 6, p);              //檢查直線
            }
            Check_3(0, 4, 8, p);                          //檢查斜線
            Check_3(2, 4, 6, p);                          //檢查斜線
        }

        void Check_3(int a, int b, int c, int p)        //三子連線檢查 & 畫紅線功能
        {
            Graphics g = this.CreateGraphics();
            if (state[a] == state[b] && state[b] == state[c] && state[a] == p)
            {
                this.over = true;                   //設定得出遊戲結果

                g.DrawLine(pen_red, rect[a].X + 30, rect[a].Y + 30,
                                    rect[c].X + 30, rect[c].Y + 30);    //畫紅線

                if (p == 1)                         //顯示遊戲結果
                    label1.Text = "You win!";
                if (p == 2)
                    label1.Text = "You lose!";
            }
        }

        void Check_2(int a, int b, int c)       //雙子連線，player2預測
        {
            int next = -1;                     //下一步==-1，為 無發現雙子結構
            for (int i = 1; i <= 2; i++)       //i==1，為player1雙子檢查；i==2，為player2雙子檢查
            {
                if (state[a] == i && state[a] == state[b] && state[c] == 0) 
                {
                    next = c;
                }
                if (state[a] == i && state[a] == state[c] && state[b] == 0)
                {
                    next = b;
                }
                if (state[b] == i && state[b] == state[c] && state[a] == 0)
                {
                    next = a;
                }

                if (next != -1)                 //下一步==-1，為 無發現雙子結構
                {
                    if (i == 1)                 //player1 出現雙子，啟動 defend 戰術
                        this.defend = next;
                    else if (i == 2)            //player2 出現雙子，啟動 attack 戰術
                        this.attack = next;
                }

                next = -1;                      //初始化 for next player mode check
            }
        }
    }
}
