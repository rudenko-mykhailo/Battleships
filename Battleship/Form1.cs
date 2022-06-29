using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace Battleship
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; 
                return cp;
            }
        }
        SoundPlayer miss = new SoundPlayer("Sounds\\Sound_Miss.wav"), hit = new SoundPlayer("Sounds\\Sound_Hit.wav"), win = new SoundPlayer("Sounds\\Victory.wav"),loss=new SoundPlayer("Sounds\\Loss.wav");
        int planI=0,planJ=0,S_corner=4,Net_Start=0;
        public bool fgame = false,method=false;
        bool prepare = true;
        Button[,] player;
        Form2 Choice = new Form2();
        Button[,] enemy;
        private void A1_Click(object sender, EventArgs e)
        {
            Cl(0,0);
        }

        void Cl(int i, int j)
        {
            if(prepare)
            {
                if(player[i, j].BackColor == Color.Lime)
                {
                    player[i, j].BackColor = Color.Transparent;
                    player[i, j].ForeColor = Color.Blue;
                }
                else if ((i == 0 && j == 0) || (i == 0 && j == 9) || (i == 9 && j == 9) || (i == 9 && j == 0))
                {
                    if (player[Math.Abs(i - 1), Math.Abs(j - 1)].BackColor != Color.Lime)
                    {
                        player[i, j].BackColor = Color.Lime;
                        player[i, j].ForeColor = Color.Red;
                    }
                }
                else if (i == 0 || i== 9)
                {
                    if (player[Math.Abs(i - 1), j - 1].BackColor != Color.Lime&& player[Math.Abs(i - 1), j + 1].BackColor != Color.Lime)
                    {
                        player[i, j].BackColor = Color.Lime;
                        player[i, j].ForeColor = Color.Red;
                    }
                }
                else if(j == 0 || j == 9)
                {
                    if (player[i - 1, Math.Abs(j - 1)].BackColor != Color.Lime && player[i + 1, Math.Abs(j - 1)].BackColor != Color.Lime)
                    {
                        player[i, j].BackColor = Color.Lime;
                        player[i, j].ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (player[i - 1, j - 1].BackColor != Color.Lime && player[i + 1, j - 1].BackColor != Color.Lime && player[i + 1, j + 1].BackColor != Color.Lime && player[i - 1, j + 1].BackColor != Color.Lime)
                    {
                        player[i, j].BackColor = Color.Lime;
                        player[i, j].ForeColor = Color.Red;
                    }
                }
                Check_field(false,true);
            }
        }

        bool Check_field(bool game,bool defense)
        {
            int boat = 0, corvette = 0, line = 0, carrier = 0, counter = 0, eboat = 0, ecorvette = 0, eline = 0, ecarrier = 0, counter2 = 0,wounds=0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (defense)
                    {
                        if (player[i, j].BackColor == Color.Lime)
                        {
                            wounds = 0;
                            if (player[i, j].BackgroundImage != null)
                                wounds = 1;
                            counter = 1;
                            if (i != 0)
                            {
                                if (player[i - 1, j].BackColor == Color.Lime)
                                    continue;
                            }
                            if (j != 0)
                            {
                                if (player[i, j - 1].BackColor == Color.Lime)
                                    continue;
                            }
                            int temp1 = j;
                            while (counter != 5 && temp1 != 9)
                            {
                                temp1++;
                                if (player[i, temp1].BackColor == Color.Lime)
                                {
                                    counter++;
                                    if (player[i, temp1].BackgroundImage != null)
                                        wounds++;
                                }
                                else
                                {
                                    if (counter == 1)
                                        temp1--;
                                    break;
                                }
                            }
                            int temp = i;
                            while (counter != 5 && temp != 9)
                            {
                                temp++;
                                if (player[temp, j].BackColor == Color.Lime)
                                {
                                    counter++;
                                    if (player[temp, j].BackgroundImage != null)
                                        wounds++;
                                }
                                else
                                {
                                    if (temp - 1 == i)
                                        temp--;
                                    break;
                                }
                            }
                            if (wounds != counter)
                                switch (counter)
                                {
                                    case 1: boat++; break;
                                    case 2: corvette++; break;
                                    case 3: line++; break;
                                    case 4: carrier++; break;
                                    case 5: player[temp, temp1].BackColor = Color.Transparent; player[temp, temp1].ForeColor = Color.Blue; carrier++; break;
                                }
                        }
                    }
                    else
                    {
                        if (enemy[i, j].BackColor == Color.Lime)
                        {
                            counter2 = 1;
                            if (i != 0)
                            {
                                if (enemy[i - 1, j].BackColor == Color.Lime)
                                    continue;
                            }
                            if (j != 0)
                            {
                                if (enemy[i, j - 1].BackColor == Color.Lime)
                                    continue;
                            }
                            if (!Check(i, j, enemy))
                            {
                                int temp1 = j;
                                while (temp1 != 9)
                                {
                                    temp1++;
                                    if (enemy[i, temp1].BackColor == Color.Lime)
                                        counter2++;
                                    else
                                    {
                                        break;
                                    }
                                }
                                int temp = i;
                                while (temp != 9)
                                {
                                    temp++;
                                    if (enemy[temp, j].BackColor == Color.Lime)
                                        counter2++;
                                    else
                                    {
                                        break;
                                    }
                                }
                                switch (counter2)
                                {
                                    case 1: eboat++; break;
                                    case 2: ecorvette++; break;
                                    case 3: eline++; break;
                                    case 4: ecarrier++; break;
                                }
                            }
                        }
                    }
                }
            if (defense)
            {
                label27.Text = $"x {carrier}/1";
                label26.Text = $"x {line}/2";
                label25.Text = $"x {corvette}/3";
                label24.Text = $"x {boat}/4";
                if (boat == 4 && corvette == 3 && line == 2 && carrier == 1 && !game)
                {
                    if (prepare)
                        Begin.Enabled = true;
                    pictureBox31.BackgroundImage = Battleship.Properties.Resources._13__LED_GREEN;
                    return true;
                }
                else
                {
                    Begin.Enabled = false;
                    if (prepare)
                        pictureBox31.BackgroundImage = Battleship.Properties.Resources._12__LED_RED;
                    return false;
                }
            }
            else
            {
                label2.Text = $"x {1 - ecarrier}/1";
                label5.Text = $"x {2 - eline}/2";
                label3.Text = $"x {3 - ecorvette}/3";
                label4.Text = $"x {4 - eboat}/4";
                return true;
            }
            
        }

        void Random_Formation(Button[,] field)
        {
            int carrier = 0, line = 0, corvette = 0, boat = 0;
            var rand = new Random();
            int temp1 = rand.Next(10);
            int temp2 = rand.Next(10);
            bool set = false;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    field[i, j].Text = "";
                    field[i, j].BackColor = Color.Transparent;
                    field[i, j].ForeColor = Color.Blue;
                    field[i, j].BackgroundImage = null;
                }
            while (carrier != 1)
            {
                temp1 = rand.Next(10);
                temp2 = rand.Next(10);
                if (temp1 <= 6 && temp2 <= 6)
                {
                    int cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        field[temp1, temp2].ForeColor = Color.Red;
                        field[temp1 + 1, temp2].ForeColor = Color.Red;
                        field[temp1 + 2, temp2].ForeColor = Color.Red;
                        field[temp1 + 3, temp2].ForeColor = Color.Red;
                    }
                    else
                    {
                        field[temp1, temp2].ForeColor = Color.Red;
                        field[temp1, temp2 + 1].ForeColor = Color.Red;
                        field[temp1, temp2 + 2].ForeColor = Color.Red;
                        field[temp1, temp2 + 3].ForeColor = Color.Red;
                    }
                    carrier++;
                }
                else if (temp1 <= 6)
                {
                    field[temp1, temp2].ForeColor = Color.Red;
                    field[temp1 + 1, temp2].ForeColor = Color.Red;
                    field[temp1 + 2, temp2].ForeColor = Color.Red;
                    field[temp1 + 3, temp2].ForeColor = Color.Red;
                    carrier++;
                }
                else if (temp2 <= 6)
                {
                    field[temp1, temp2].ForeColor = Color.Red;
                    field[temp1, temp2 + 1].ForeColor = Color.Red;
                    field[temp1, temp2 + 2].ForeColor = Color.Red;
                    field[temp1, temp2 + 3].ForeColor = Color.Red;
                    carrier++;
                }
            }
            while (line != 2)
            {
                set = false;
                temp1 = rand.Next(10);
                temp2 = rand.Next(10);
                if (temp1 != 0 && temp1 != 9 && temp2 != 0 && temp2 != 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[temp1 + 1, temp2].ForeColor == Color.Red || field[temp1 - 1, temp2].ForeColor == Color.Red || field[temp1, temp2 + 1].ForeColor == Color.Red || field[temp1 + 1, temp2 + 1].ForeColor == Color.Red || field[temp1 - 1, temp2 + 1].ForeColor == Color.Red || field[temp1, temp2 - 1].ForeColor == Color.Red || field[temp1 + 1, temp2 - 1].ForeColor == Color.Red || field[temp1 - 1, temp2 - 1].ForeColor == Color.Red)
                        continue;
                }
                else if ((temp1 == 0 && temp2 == 0) || (temp1 == 0 && temp2 == 9) || (temp1 == 9 && temp2 == 9) || (temp1 == 9 && temp2 == 0))
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1, Math.Abs(temp2 - 1)].ForeColor == Color.Red)
                        continue;
                }
                else if (temp1 == 0 || temp1 == 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2 - 1].ForeColor == Color.Red || field[temp1, temp2 - 1].ForeColor == Color.Red || field[temp1, temp2 + 1].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2 + 1].ForeColor == Color.Red)
                        continue;
                }
                else if (temp2 == 0 || temp2 == 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[temp1 - 1, temp2].ForeColor == Color.Red || field[temp1 + 1, temp2].ForeColor == Color.Red || field[temp1, Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1 + 1, Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1 - 1, Math.Abs(temp2 - 1)].ForeColor == Color.Red)
                        continue;
                }
                if (temp1 <= 7 && temp2 <= 7)
                {
                    int cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 + 1].ForeColor != Color.Red && field[temp1 + 2, Math.Abs(temp2 - 1)].ForeColor != Color.Red)
                        {
                            if (temp1 == 7)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                field[temp1 + 2, temp2].ForeColor = Color.Red;
                                set = true;
                                line++;
                            }
                            else if (field[temp1 + 3, temp2].ForeColor != Color.Red && field[temp1 + 3, temp2 + 1].ForeColor != Color.Red && field[temp1 + 3, Math.Abs(temp2 - 1)].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                field[temp1 + 2, temp2].ForeColor = Color.Red;
                                set = true;
                                line++;
                            }
                        }
                        if (!set)
                        {
                            if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 + 1, temp2 + 2].ForeColor != Color.Red && field[Math.Abs(temp1 - 1), temp2 + 2].ForeColor != Color.Red)
                            {
                                if (temp2 == 7)
                                {
                                    field[temp1, temp2].ForeColor = Color.Red;
                                    field[temp1, temp2 + 1].ForeColor = Color.Red;
                                    field[temp1, temp2 + 2].ForeColor = Color.Red;
                                    line++;
                                }
                                else if (field[temp1, temp2 + 3].ForeColor != Color.Red && field[temp1 + 1, temp2 + 3].ForeColor != Color.Red && field[Math.Abs(temp1 - 1), temp2 + 3].ForeColor != Color.Red)
                                {
                                    field[temp1, temp2].ForeColor = Color.Red;
                                    field[temp1, temp2 + 1].ForeColor = Color.Red;
                                    field[temp1, temp2 + 2].ForeColor = Color.Red;
                                    line++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 + 1, temp2 + 2].ForeColor != Color.Red && field[Math.Abs(temp1 - 1), temp2 + 2].ForeColor != Color.Red)
                        {
                            if (temp2 == 7)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                field[temp1, temp2 + 2].ForeColor = Color.Red;
                                set = true;
                                line++;
                            }
                            else if (field[temp1, temp2 + 3].ForeColor != Color.Red && field[temp1 + 1, temp2 + 3].ForeColor != Color.Red && field[Math.Abs(temp1 - 1), temp2 + 3].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                field[temp1, temp2 + 2].ForeColor = Color.Red;
                                set = true;
                                line++;
                            }
                        }
                        if (!set)
                        {
                            if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 + 1].ForeColor != Color.Red && field[temp1 + 2, Math.Abs(temp2 - 1)].ForeColor != Color.Red)
                            {
                                if (temp1 == 7)
                                {
                                    field[temp1, temp2].ForeColor = Color.Red;
                                    field[temp1 + 1, temp2].ForeColor = Color.Red;
                                    field[temp1 + 2, temp2].ForeColor = Color.Red;
                                    line++;
                                }
                                else if (field[temp1 + 3, temp2].ForeColor != Color.Red && field[temp1 + 3, temp2 + 1].ForeColor != Color.Red && field[temp1 + 3, Math.Abs(temp2 - 1)].ForeColor != Color.Red)
                                {
                                    field[temp1, temp2].ForeColor = Color.Red;
                                    field[temp1 + 1, temp2].ForeColor = Color.Red;
                                    field[temp1 + 2, temp2].ForeColor = Color.Red;
                                    line++;
                                }
                            }
                        }
                    }
                }
                else if (temp1 <= 7)
                {
                    if (temp2 == 8)
                    {
                        if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 + 1].ForeColor != Color.Red && field[temp1 + 2, temp2 - 1].ForeColor != Color.Red)
                        {
                            if (temp1 == 7)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                field[temp1 + 2, temp2].ForeColor = Color.Red;
                                line++;
                            }
                            else if (field[temp1 + 3, temp2].ForeColor != Color.Red && field[temp1 + 3, temp2 + 1].ForeColor != Color.Red && field[temp1 + 3, temp2 - 1].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                field[temp1 + 2, temp2].ForeColor = Color.Red;
                                line++;
                            }
                        }
                    }
                    else
                    {
                        if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 - 1].ForeColor != Color.Red)
                        {
                            if (temp1 == 7)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                field[temp1 + 2, temp2].ForeColor = Color.Red;
                                line++;
                            }
                            else if (field[temp1 + 3, temp2].ForeColor != Color.Red && field[temp1 + 3, temp2 - 1].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                field[temp1 + 2, temp2].ForeColor = Color.Red;
                                line++;
                            }
                        }
                    }
                }
                else if (temp2 <= 7)
                {
                    if (temp1 == 8)
                    {
                        if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 + 1, temp2 + 2].ForeColor != Color.Red && field[temp1 - 1, temp2 + 2].ForeColor != Color.Red)
                        {
                            if (temp2 == 7)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                field[temp1, temp2 + 2].ForeColor = Color.Red;
                                line++;
                            }
                            else if (field[temp1, temp2 + 3].ForeColor != Color.Red && field[temp1 + 1, temp2 + 3].ForeColor != Color.Red && field[temp1 - 1, temp2 + 3].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                field[temp1, temp2 + 2].ForeColor = Color.Red;
                                line++;
                            }
                        }
                    }
                    else
                    {
                        if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 - 1, temp2 + 2].ForeColor != Color.Red)
                        {
                            if (temp2 == 7)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                field[temp1, temp2 + 2].ForeColor = Color.Red;
                                line++;
                            }
                            else if (field[temp1, temp2 + 3].ForeColor != Color.Red && field[temp1 - 1, temp2 + 3].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                field[temp1, temp2 + 2].ForeColor = Color.Red;
                                line++;
                            }
                        }
                    }
                }
            }
            while (corvette != 3)
            {
                set = false;
                temp1 = rand.Next(10);
                temp2 = rand.Next(10);
                if (temp1 != 0 && temp1 != 9 && temp2 != 0 && temp2 != 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[temp1 + 1, temp2].ForeColor == Color.Red || field[temp1 - 1, temp2].ForeColor == Color.Red || field[temp1, temp2 + 1].ForeColor == Color.Red || field[temp1 + 1, temp2 + 1].ForeColor == Color.Red || field[temp1 - 1, temp2 + 1].ForeColor == Color.Red || field[temp1, temp2 - 1].ForeColor == Color.Red || field[temp1 + 1, temp2 - 1].ForeColor == Color.Red || field[temp1 - 1, temp2 - 1].ForeColor == Color.Red)
                        continue;
                }
                else if ((temp1 == 0 && temp2 == 0) || (temp1 == 0 && temp2 == 9) || (temp1 == 9 && temp2 == 9) || (temp1 == 9 && temp2 == 0))
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1, Math.Abs(temp2 - 1)].ForeColor == Color.Red)
                        continue;
                }
                else if (temp1 == 0 || temp1 == 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2 - 1].ForeColor == Color.Red || field[temp1, temp2 - 1].ForeColor == Color.Red || field[temp1, temp2 + 1].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2 + 1].ForeColor == Color.Red)
                        continue;
                }
                else if (temp2 == 0 || temp2 == 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[temp1 - 1, temp2].ForeColor == Color.Red || field[temp1 + 1, temp2].ForeColor == Color.Red || field[temp1, Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1 + 1, Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1 - 1, Math.Abs(temp2 - 1)].ForeColor == Color.Red)
                        continue;
                }
                if (temp1 <= 8 && temp2 <= 8)
                {
                    int cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        if (temp1 == 8)
                        {
                            field[temp1, temp2].ForeColor = Color.Red;
                            field[temp1 + 1, temp2].ForeColor = Color.Red;
                            set = true;
                            corvette++;
                        }
                        else if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 + 1].ForeColor != Color.Red && field[temp1 + 2, Math.Abs(temp2 - 1)].ForeColor != Color.Red)
                        {
                            field[temp1, temp2].ForeColor = Color.Red;
                            field[temp1 + 1, temp2].ForeColor = Color.Red;
                            set = true;
                            corvette++;
                        }
                        if (!set)
                        {
                            if (temp2 == 8)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                corvette++;
                            }
                            else if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 + 1, temp2 + 2].ForeColor != Color.Red && field[Math.Abs(temp1 - 1), temp2 + 2].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1, temp2 + 1].ForeColor = Color.Red;
                                corvette++;
                            }
                        }
                    }
                    else
                    {
                        if (temp2 == 8)
                        {
                            field[temp1, temp2].ForeColor = Color.Red;
                            field[temp1, temp2 + 1].ForeColor = Color.Red;
                            set = true;
                            corvette++;
                        }
                        else if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 + 1, temp2 + 2].ForeColor != Color.Red && field[Math.Abs(temp1 - 1), temp2 + 2].ForeColor != Color.Red)
                        {
                            field[temp1, temp2].ForeColor = Color.Red;
                            field[temp1, temp2 + 1].ForeColor = Color.Red;
                            set = true;
                            corvette++;
                        }
                        if (!set)
                        {
                            if (temp1 == 8)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                corvette++;
                            }
                            else if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 + 1].ForeColor != Color.Red && field[temp1 + 2, Math.Abs(temp2 - 1)].ForeColor != Color.Red)
                            {
                                field[temp1, temp2].ForeColor = Color.Red;
                                field[temp1 + 1, temp2].ForeColor = Color.Red;
                                corvette++;
                            }
                        }
                    }
                }
                else if (temp1 <= 8)
                {
                    if (temp1 == 8)
                    {
                        field[temp1, temp2].ForeColor = Color.Red;
                        field[temp1 + 1, temp2].ForeColor = Color.Red;
                        corvette++;
                    }
                    else if (field[temp1 + 2, temp2].ForeColor != Color.Red && field[temp1 + 2, temp2 - 1].ForeColor != Color.Red)
                    {
                        field[temp1, temp2].ForeColor = Color.Red;
                        field[temp1 + 1, temp2].ForeColor = Color.Red;
                        corvette++;
                    }
                }
                else if (temp2 <= 8)
                {
                    if (temp2 == 8)
                    {
                        field[temp1, temp2].ForeColor = Color.Red;
                        field[temp1, temp2 + 1].ForeColor = Color.Red;
                        corvette++;
                    }
                    else if (field[temp1, temp2 + 2].ForeColor != Color.Red && field[temp1 - 1, temp2 + 2].ForeColor != Color.Red)
                    {
                        field[temp1, temp2].ForeColor = Color.Red;
                        field[temp1, temp2 + 1].ForeColor = Color.Red;
                        corvette++;
                    }
                }
            }
            while (boat != 4)
            {
                temp1 = rand.Next(10);
                temp2 = rand.Next(10);
                if (temp1 != 0 && temp1 != 9 && temp2 != 0 && temp2 != 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[temp1 + 1, temp2].ForeColor == Color.Red || field[temp1 - 1, temp2].ForeColor == Color.Red || field[temp1, temp2 + 1].ForeColor == Color.Red || field[temp1 + 1, temp2 + 1].ForeColor == Color.Red || field[temp1 - 1, temp2 + 1].ForeColor == Color.Red || field[temp1, temp2 - 1].ForeColor == Color.Red || field[temp1 + 1, temp2 - 1].ForeColor == Color.Red || field[temp1 - 1, temp2 - 1].ForeColor == Color.Red)
                        continue;
                }
                else if ((temp1 == 0 && temp2 == 0) || (temp1 == 0 && temp2 == 9) || (temp1 == 9 && temp2 == 9) || (temp1 == 9 && temp2 == 0))
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1, Math.Abs(temp2 - 1)].ForeColor == Color.Red)
                        continue;
                }
                else if (temp1 == 0 || temp1 == 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2 - 1].ForeColor == Color.Red || field[temp1, temp2 - 1].ForeColor == Color.Red || field[temp1, temp2 + 1].ForeColor == Color.Red || field[Math.Abs(temp1 - 1), temp2 + 1].ForeColor == Color.Red)
                        continue;
                }
                else if (temp2 == 0 || temp2 == 9)
                {
                    if (field[temp1, temp2].ForeColor == Color.Red || field[temp1 - 1, temp2].ForeColor == Color.Red || field[temp1 + 1, temp2].ForeColor == Color.Red || field[temp1, Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1 + 1, Math.Abs(temp2 - 1)].ForeColor == Color.Red || field[temp1 - 1, Math.Abs(temp2 - 1)].ForeColor == Color.Red)
                        continue;
                }
                field[temp1, temp2].ForeColor = Color.Red;
                boat++;
            }
        }

        private void J1_Click(object sender, EventArgs e)
        {
            Cl(0, 9);
        }

        private void J10_Click(object sender, EventArgs e)
        {
            Cl(9, 9);
        }

        private void A10_Click(object sender, EventArgs e)
        {
            Cl(9, 0);
        }

        private void B1_Click(object sender, EventArgs e)
        {
            Cl(0, 1);
        }

        private void C1_Click(object sender, EventArgs e)
        {
            Cl(0, 2);
        }

        private void D1_Click(object sender, EventArgs e)
        {
            Cl(0, 3);
        }

        private void E1_Click(object sender, EventArgs e)
        {
            Cl(0, 4);
        }

        private void F1_Click(object sender, EventArgs e)
        {
            Cl(0, 5);
        }

        private void G1_Click(object sender, EventArgs e)
        {
            Cl(0, 6);
        }

        private void H1_Click(object sender, EventArgs e)
        {
            Cl(0, 7);
        }

        private void I1_Click(object sender, EventArgs e)
        {
            Cl(0, 8);
        }

        private void J2_Click(object sender, EventArgs e)
        {
            Cl(1, 9);
        }

        private void J3_Click(object sender, EventArgs e)
        {
            Cl(2, 9);
        }

        private void J4_Click(object sender, EventArgs e)
        {
            Cl(3, 9);
        }

        private void J5_Click(object sender, EventArgs e)
        {
            Cl(4, 9);
        }

        private void J6_Click(object sender, EventArgs e)
        {
            Cl(5, 9);
        }

        private void J7_Click(object sender, EventArgs e)
        {
            Cl(6, 9);
        }

        private void J8_Click(object sender, EventArgs e)
        {
            Cl(7, 9);
        }

        private void J9_Click(object sender, EventArgs e)
        {
            Cl(8, 9);
        }

        private void I10_Click(object sender, EventArgs e)
        {
            Cl(9, 8);
        }

        private void H10_Click(object sender, EventArgs e)
        {
            Cl(9, 7);
        }

        private void G10_Click(object sender, EventArgs e)
        {
            Cl(9, 6);
        }

        private void F10_Click(object sender, EventArgs e)
        {
            Cl(9, 5);
        }

        private void E10_Click(object sender, EventArgs e)
        {
            Cl(9, 4);
        }

        private void D10_Click(object sender, EventArgs e)
        {
            Cl(9, 3);
        }

        private void C10_Click(object sender, EventArgs e)
        {
            Cl(9, 2);
        }

        private void B10_Click(object sender, EventArgs e)
        {
            Cl(9, 1);
        }

        private void A2_Click(object sender, EventArgs e)
        {
            Cl(1, 0);
        }

        private void A3_Click(object sender, EventArgs e)
        {
            Cl(2, 0);
        }

        private void A4_Click(object sender, EventArgs e)
        {
            Cl(3, 0);
        }

        private void A5_Click(object sender, EventArgs e)
        {
            Cl(4, 0);
        }

        private void A6_Click(object sender, EventArgs e)
        {
            Cl(5, 0);
        }

        private void A7_Click(object sender, EventArgs e)
        {
            Cl(6, 0);
        }

        private void A8_Click(object sender, EventArgs e)
        {
            Cl(7, 0);
        }

        private void A9_Click(object sender, EventArgs e)
        {
            Cl(8, 0);
        }

        private void B2_Click(object sender, EventArgs e)
        {
            Cl(1, 1);
        }

        private void B3_Click(object sender, EventArgs e)
        {
            Cl(2, 1);
        }

        private void B4_Click(object sender, EventArgs e)
        {
            Cl(3, 1);
        }

        private void B5_Click(object sender, EventArgs e)
        {
            Cl(4, 1);
        }

        private void B6_Click(object sender, EventArgs e)
        {
            Cl(5, 1);
        }

        private void B7_Click(object sender, EventArgs e)
        {
            Cl(6, 1);
        }

        private void B8_Click(object sender, EventArgs e)
        {
            Cl(7, 1);
        }

        private void B9_Click(object sender, EventArgs e)
        {
            Cl(8, 1);
        }

        private void C2_Click(object sender, EventArgs e)
        {
            Cl(1, 2);
        }

        private void C3_Click(object sender, EventArgs e)
        {
            Cl(2, 2);
        }

        private void C4_Click(object sender, EventArgs e)
        {
            Cl(3, 2);
        }

        private void C5_Click(object sender, EventArgs e)
        {
            Cl(4, 2);
        }

        private void C6_Click(object sender, EventArgs e)
        {
            Cl(5, 2);
        }

        private void C7_Click(object sender, EventArgs e)
        {
            Cl(6, 2);
        }

        private void C8_Click(object sender, EventArgs e)
        {
            Cl(7, 2);
        }

        private void C9_Click(object sender, EventArgs e)
        {
            Cl(8, 2);
        }

        private void D2_Click(object sender, EventArgs e)
        {
            Cl(1, 3);
        }

        private void D3_Click(object sender, EventArgs e)
        {
            Cl(2, 3);
        }

        private void D4_Click(object sender, EventArgs e)
        {
            Cl(3, 3);
        }

        private void D5_Click(object sender, EventArgs e)
        {
            Cl(4, 3);
        }

        private void D6_Click(object sender, EventArgs e)
        {
            Cl(5, 3);
        }

        private void D7_Click(object sender, EventArgs e)
        {
            Cl(6, 3);
        }

        private void D8_Click(object sender, EventArgs e)
        {
            Cl(7, 3);
        }

        private void D9_Click(object sender, EventArgs e)
        {
            Cl(8, 3);
        }

        private void E2_Click(object sender, EventArgs e)
        {
            Cl(1, 4);
        }

        private void E3_Click(object sender, EventArgs e)
        {
            Cl(2, 4);
        }

        private void E4_Click(object sender, EventArgs e)
        {
            Cl(3, 4);
        }

        private void E5_Click(object sender, EventArgs e)
        {
            Cl(4, 4);
        }

        private void E6_Click(object sender, EventArgs e)
        {
            Cl(5, 4);
        }

        private void E7_Click(object sender, EventArgs e)
        {
            Cl(6, 4);
        }

        private void E8_Click(object sender, EventArgs e)
        {
            Cl(7, 4);
        }

        private void E9_Click(object sender, EventArgs e)
        {
            Cl(8, 4);
        }

        private void F2_Click(object sender, EventArgs e)
        {
            Cl(1, 5);
        }

        private void F3_Click(object sender, EventArgs e)
        {
            Cl(2, 5);
        }

        private void F4_Click(object sender, EventArgs e)
        {
            Cl(3, 5);
        }

        private void F5_Click(object sender, EventArgs e)
        {
            Cl(4, 5);
        }

        private void F6_Click(object sender, EventArgs e)
        {
            Cl(5, 5);
        }

        private void F7_Click(object sender, EventArgs e)
        {
            Cl(6, 5);
        }

        private void F8_Click(object sender, EventArgs e)
        {
            Cl(7, 5);
        }

        private void F9_Click(object sender, EventArgs e)
        {
            Cl(8, 5);
        }

        private void G2_Click(object sender, EventArgs e)
        {
            Cl(1, 6);
        }

        private void G3_Click(object sender, EventArgs e)
        {
            Cl(2, 6);
        }

        private void G4_Click(object sender, EventArgs e)
        {
            Cl(3, 6);
        }

        private void G5_Click(object sender, EventArgs e)
        {
            Cl(4, 6);
        }

        private void G6_Click(object sender, EventArgs e)
        {
            Cl(5, 6);
        }

        private void G7_Click(object sender, EventArgs e)
        {
            Cl(6, 6);
        }

        private void G8_Click(object sender, EventArgs e)
        {
            Cl(7, 6);
        }

        private void G9_Click(object sender, EventArgs e)
        {
            Cl(8, 6);
        }

        private void H2_Click(object sender, EventArgs e)
        {
            Cl(1, 7);
        }

        private void H3_Click(object sender, EventArgs e)
        {
            Cl(2, 7);
        }

        private void H4_Click(object sender, EventArgs e)
        {
            Cl(3, 7);
        }

        private void H5_Click(object sender, EventArgs e)
        {
            Cl(4, 7);
        }

        private void H6_Click(object sender, EventArgs e)
        {
            Cl(5, 7);
        }

        private void H7_Click(object sender, EventArgs e)
        {
            Cl(6, 7);
        }

        private void H8_Click(object sender, EventArgs e)
        {
            Cl(7, 7);
        }

        private void H9_Click(object sender, EventArgs e)
        {
            Cl(8, 7);
        }

        private void I2_Click(object sender, EventArgs e)
        {
            Cl(1, 8);
        }

        private void I3_Click(object sender, EventArgs e)
        {
            Cl(2, 8);
        }

        private void I4_Click(object sender, EventArgs e)
        {
            Cl(3, 8);
        }

        private void I5_Click(object sender, EventArgs e)
        {
            Cl(4, 8);
        }

        private void I6_Click(object sender, EventArgs e)
        {
            Cl(5, 8);
        }

        private void I7_Click(object sender, EventArgs e)
        {
            Cl(6, 8);
        }

        private void I8_Click(object sender, EventArgs e)
        {
            Cl(7, 8);
        }

        private void I9_Click(object sender, EventArgs e)
        {
            Cl(8, 8);
        }

        private void Begin_Click(object sender, EventArgs e)
        {
            prepare = false;
            Random_Formation(enemy);
            Begin.Enabled = false;
            Check_field(false, false);
            label28.Text = "YOUR TURN";
            label28.Refresh();
        }

        private void N_game_Click(object sender, EventArgs e)
        {
            label28.Text = "DEPLOY";
            label28.Refresh();
            label27.Enabled = true;
            label26.Enabled = true;
            label25.Enabled = true;
            label24.Enabled = true;
            label27.Visible = true;
            label26.Visible = true;
            label25.Visible = true;
            label24.Visible = true;
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            pictureBox2.Visible = true;
            pictureBox2.Enabled = true;
            pictureBox3.Visible = true;
            pictureBox3.Enabled = true;
            pictureBox4.Visible = true;
            pictureBox4.Enabled = true;
            Begin.Enabled = true;
            prepare = true;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    player[i, j].Enabled = true;
                    player[i, j].Text = "";
                    player[i, j].BackColor = Color.Transparent;
                    player[i, j].ForeColor = Color.Blue;
                    player[i, j].BackgroundImage = null;
                    enemy[i, j].Enabled = true;
                    enemy[i, j].BackColor = Color.Transparent;
                    enemy[i, j].ForeColor = Color.Blue;
                    enemy[i, j].BackgroundImage = null;
                    enemy[i, j].Text = "";
                }
            S_corner = 4;
            Check_field(false,true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var pos = this.PointToScreen(Begin.Location);
            pos = pictureBox30.PointToClient(pos);
            Begin.Parent = pictureBox30;
            Begin.Location = pos;
            pos = this.PointToScreen(N_game.Location);
            pos = pictureBox30.PointToClient(pos);
            N_game.Parent = pictureBox30;
            N_game.Location = pos;
            pos = this.PointToScreen(Random.Location);
            pos = pictureBox30.PointToClient(pos);
            Random.Parent = pictureBox30;
            Random.Location = pos;
            pos = this.PointToScreen(Settings.Location);
            pos = pictureBox30.PointToClient(pos);
            Settings.Parent = pictureBox30;
            Settings.Location = pos;
            pos = this.PointToScreen(pictureBox31.Location);
            pos = pictureBox30.PointToClient(pos);
            pictureBox31.Parent = pictureBox30;
            pictureBox31.Location = pos;
            player = new Button[10, 10] { { A1, B1, C1, D1, E1, F1, G1, H1, I1, J1 }, { A2, B2, C2, D2, E2, F2, G2, H2, I2, J2 }, { A3, B3, C3, D3, E3, F3, G3, H3, I3, J3 }, { A4, B4, C4, D4, E4, F4, G4, H4, I4, J4 }, { A5, B5, C5, D5, E5, F5, G5, H5, I5, J5 }, { A6, B6, C6, D6, E6, F6, G6, H6, I6, J6 }, { A7, B7, C7, D7, E7, F7, G7, H7, I7, J7 }, { A8, B8, C8, D8, E8, F8, G8, H8, I8, J8 }, { A9, B9, C9, D9, E9, F9, G9, H9, I9, J9 }, { A10, B10, C10, D10, E10, F10, G10, H10, I10, J10 } };
            enemy = new Button[10, 10] { { EA1, EB1, EC1, ED1, EE1, EF1, EG1, EH1, EI1, EJ1 }, { EA2, EB2, EC2, ED2, EE2, EF2, EG2, EH2, EI2, EJ2 }, { EA3, EB3, EC3, ED3, EE3, EF3, EG3, EH3, EI3, EJ3 }, { EA4, EB4, EC4, ED4, EE4, EF4, EG4, EH4, EI4, EJ4 }, { EA5, EB5, EC5, ED5, EE5, EF5, EG5, EH5, EI5, EJ5 }, { EA6, EB6, EC6, ED6, EE6, EF6, EG6, EH6, EI6, EJ6 }, { EA7, EB7, EC7, ED7, EE7, EF7, EG7, EH7, EI7, EJ7 }, { EA8, EB8, EC8, ED8, EE8, EF8, EG8, EH8, EI8, EJ8 }, { EA9, EB9, EC9, ED9, EE9, EF9, EG9, EH9, EI9, EJ9 }, { EA10, EB10, EC10, ED10, EE10, EF10, EG10, EH10, EI10, EJ10 } };
            pos = this.PointToScreen(label28.Location);
            pos = pictureBox13.PointToClient(pos);
            label28.Parent = pictureBox13;
            label28.Location = pos;
            label28.BackColor = Color.Transparent;
            pos = this.PointToScreen(label24.Location);
            pos = pictureBox14.PointToClient(pos);
            label24.Parent = pictureBox14;
            label24.Location = pos;
            label24.BackColor = Color.Transparent;
            pos = this.PointToScreen(label25.Location);
            pos = pictureBox14.PointToClient(pos);
            label25.Parent = pictureBox14;
            label25.Location = pos;
            label25.BackColor = Color.Transparent;
            pos = this.PointToScreen(label26.Location);
            pos = pictureBox14.PointToClient(pos);
            label26.Parent = pictureBox14;
            label26.Location = pos;
            label26.BackColor = Color.Transparent;
            pos = this.PointToScreen(label27.Location);
            pos = pictureBox14.PointToClient(pos);
            label27.Parent = pictureBox14;
            label27.Location = pos;
            label27.BackColor = Color.Transparent;
            pos = this.PointToScreen(label2.Location);
            pos = pictureBox25.PointToClient(pos);
            label2.Parent = pictureBox25;
            label2.Location = pos;
            label2.BackColor = Color.Transparent;
            pos = this.PointToScreen(label3.Location);
            pos = pictureBox25.PointToClient(pos);
            label3.Parent = pictureBox25;
            label3.Location = pos;
            label3.BackColor = Color.Transparent;
            pos = this.PointToScreen(label4.Location);
            pos = pictureBox25.PointToClient(pos);
            label4.Parent = pictureBox25;
            label4.Location = pos;
            label4.BackColor = Color.Transparent;
            pos = this.PointToScreen(label5.Location);
            pos = pictureBox25.PointToClient(pos);
            label5.Parent = pictureBox25;
            label5.Location = pos;
            label5.BackColor = Color.Transparent;
        }

        void Shoot(int i,int j)
        {
            if (!prepare)
            {
                if (enemy[i, j].ForeColor == Color.Blue)
                {
                    enemy[i, j].Text = "O";
                    enemy[i, j].Enabled = false;
                    miss.Play();
                    label28.Text = "ENEMY TURN";
                    label28.Refresh();
                    if(!fgame)
                        Thread.Sleep(1000);
                    EShoot();
                }
                else
                {
                    enemy[i, j].BackColor = Color.Lime;
                    enemy[i, j].BackgroundImage = Battleship.Properties.Resources.hittran3;
                    hit.Play();
                    enemy[i, j].Enabled = false;
                    Check(i, j,enemy);
                    Check_field(true, false);
                }
                if (End() == 1)
                {
                    label28.Text = "VICTORY";
                    win.Play();
                    label28.Refresh();
                    for (int m = 0; m < 10; m++)
                        for (int n = 0; n < 10; n++)
                        {
                            enemy[m, n].Enabled = false;
                        }
                }
            }
        }

        int End()
        {
            int p = 0, e = 0;
            for(int i=0;i<10;i++)
                for(int j=0;j<10;j++)
                {
                    if (player[i, j].BackgroundImage != null)
                        p++;
                    if (enemy[i, j].BackgroundImage != null)
                        e++;
                }
            if (p == 20)
                return 0;
            else if (e == 20)
                return 1;
            else
                return 2;
        }

        bool Check(int i, int j,Button[,] field)
        {
            int count = 0;
            bool alive = false;
            if (i != 0 && i != 9 && j != 0 && j != 9)
            {
                if (field[i + 1, j].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i + 1, j, field);
                    count++;
                }
                if (field[i - 1, j].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i - 1, j, field);
                    count++;
                }
                if (field[i, j + 1].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i, j + 1, field);
                    count++;
                }
                if (field[i, j - 1].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i, j - 1, field);
                    count++;
                }
                if (!alive)
                {
                    Bury(i, j, field);
                    if (count != 0)
                    {
                        if (field[i + 1, j].ForeColor == Color.Red)
                            Bury(i, j, i + 1, j, field);
                        if (field[i - 1, j].ForeColor == Color.Red)
                            Bury(i, j, i - 1, j, field);
                        if (field[i, j + 1].ForeColor == Color.Red)
                            Bury(i, j, i, j + 1, field);
                        if (field[i, j - 1].ForeColor == Color.Red)
                            Bury(i, j, i, j - 1, field);
                    }
                }
            }
            else if ((i == 0 && j == 0) || (i == 0 && j == 9) || (i == 9 && j == 9) || (i == 9 && j == 0))
            {
                if (field[Math.Abs(i - 1), j].ForeColor == Color.Red)
                {
                    alive = IsAlive(i, j, Math.Abs(i - 1), j, field);
                    count++;
                }
                else if (field[i, Math.Abs(j - 1)].ForeColor == Color.Red)
                {
                    alive = IsAlive(i, j, i, Math.Abs(j - 1), field);
                    count++;
                }
                if (!alive)
                {
                    Bury(i, j, field);
                    if (count != 0)
                    {
                        if (field[Math.Abs(i - 1), j].ForeColor == Color.Red)
                            Bury(i, j, Math.Abs(i - 1), j, field);
                        else if (field[i, Math.Abs(j - 1)].ForeColor == Color.Red)
                            Bury(i, j, i, Math.Abs(j - 1), field);
                    }
                }
            }
            else if (i == 0 || i == 9)
            {
                if (field[Math.Abs(i - 1), j].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, Math.Abs(i - 1), j, field);
                    count++;
                }
                if (field[i, j - 1].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i, j - 1, field);
                    count++;
                }
                if (field[i, j + 1].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i, j + 1, field);
                    count++;
                }
                if (!alive)
                {
                    Bury(i, j, field);
                    if(count!=0)
                    {
                        if (field[Math.Abs(i - 1), j].ForeColor == Color.Red)
                            Bury(i, j, Math.Abs(i - 1), j, field);
                        if (field[i, j - 1].ForeColor == Color.Red)
                            Bury(i, j, i, j - 1, field);
                        if (field[i, j + 1].ForeColor == Color.Red)
                            Bury(i, j, i, j + 1, field);
                    }
                }
            }
            else if (j == 0 || j == 9)
            {
                if (field[i - 1, j].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i - 1, j, field);
                    count++;
                }
                if(field[i + 1, j].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i + 1, j, field);
                    count++;
                }
                if(field[i, Math.Abs(j - 1)].ForeColor == Color.Red)
                {
                    if (!alive)
                        alive = IsAlive(i, j, i, Math.Abs(j - 1), field);
                    count++;
                }
                if (!alive)
                {
                    Bury(i, j, field);
                    if (count != 0)
                    {
                        if (field[i, Math.Abs(j - 1)].ForeColor == Color.Red)
                            Bury(i, j, i, Math.Abs(j - 1), field);
                        if (field[i + 1, j].ForeColor == Color.Red)
                            Bury(i, j, i + 1, j, field);
                        if (field[i - 1, j].ForeColor == Color.Red)
                            Bury(i, j, i - 1, j, field);
                    }
                }
            }
                return alive;
        }

        bool IsAlive(int i, int j, int i1, int j1,Button[,] field)
        {
            if (field[i1, j1].BackgroundImage == null)
                return true;
            if (i1 > i && i1 != 9)
            {
                if (field[i1 + 1, j1].ForeColor == Color.Red)
                    return IsAlive(i1, j1, i1 + 1, j1, field);
            }
            else if (i1 < i && i1 != 0)
            {
                if (field[i1 - 1, j1].ForeColor == Color.Red)
                    return IsAlive(i1, j1, i1 - 1, j1, field);
            }
            else if (j1 < j && j1 != 0)
            {
                if (field[i1, j1 - 1].ForeColor == Color.Red)
                    return IsAlive(i1, j1, i1, j1 - 1, field);
            }
            else if (j1 > j && j1 != 9)
            {
                if (field[i1, j1 + 1].ForeColor == Color.Red)
                    return IsAlive(i1, j1, i1, j1 + 1, field);
            }
            return false;
        }

        void Bury(int i, int j, Button[,] field)
        {
            field[i, j].BackgroundImage = Battleship.Properties.Resources.hit1;
            field[i, j].Text = "_";
            if (i != 0 && i != 9 && j != 0 && j != 9)
            {
                if(field[i + 1, j].BackgroundImage == null)
                    field[i + 1, j].Text = "O";
                field[i + 1, j].Enabled = false;
                if (field[i + 1, j+1].BackgroundImage == null)
                    field[i + 1, j + 1].Text = "O";
                field[i + 1, j + 1].Enabled = false;
                if (field[i + 1, j-1].BackgroundImage == null)
                    field[i + 1, j - 1].Text = "O";
                field[i + 1, j - 1].Enabled = false;
                if (field[i, j+1].BackgroundImage == null)
                    field[i, j + 1].Text = "O";
                field[i, j + 1].Enabled = false;
                if (field[i, j-1].BackgroundImage == null)
                    field[i, j - 1].Text = "O";
                field[i, j - 1].Enabled = false;
                if (field[i - 1, j+1].BackgroundImage == null)
                    field[i - 1, j + 1].Text = "O";
                field[i - 1, j + 1].Enabled = false;
                if (field[i - 1, j].BackgroundImage == null)
                    field[i - 1, j].Text = "O";
                field[i - 1, j].Enabled = false;
                if (field[i - 1, j-1].BackgroundImage == null)
                    field[i - 1, j - 1].Text = "O";
                field[i - 1, j - 1].Enabled = false;
            }
            else if ((i == 0 && j == 0) || (i == 0 && j == 9) || (i == 9 && j == 9) || (i == 9 && j == 0))
            {
                if (field[Math.Abs(i - 1), j].BackgroundImage == null)
                    field[Math.Abs(i - 1), j].Text = "O";
                field[Math.Abs(i - 1), j].Enabled = false;
                if (field[Math.Abs(i - 1), Math.Abs(j-1)].BackgroundImage == null)
                    field[Math.Abs(i - 1), Math.Abs(j - 1)].Text = "O";
                field[Math.Abs(i - 1), Math.Abs(j - 1)].Enabled = false;
                if (field[i, Math.Abs(j-1)].BackgroundImage == null)
                    field[i, Math.Abs(j - 1)].Text = "O";
                field[i, Math.Abs(j - 1)].Enabled = false;
            }
            else if (i == 0 || i == 9)
            {
                if (field[Math.Abs(i - 1), j].BackgroundImage == null)
                    field[Math.Abs(i - 1), j].Text = "O";
                field[Math.Abs(i - 1), j].Enabled = false;
                if (field[Math.Abs(i - 1), j+1].BackgroundImage == null)
                    field[Math.Abs(i - 1), j + 1].Text = "O";
                field[Math.Abs(i - 1), j + 1].Enabled = false;
                if (field[Math.Abs(i - 1), j-1].BackgroundImage == null)
                    field[Math.Abs(i - 1), j - 1].Text = "O";
                field[Math.Abs(i - 1), j - 1].Enabled = false;
                if (field[i, j + 1].BackgroundImage == null)
                    field[i, j + 1].Text = "O";
                field[i, j + 1].Enabled = false;
                if (field[i, j - 1].BackgroundImage == null)
                    field[i, j - 1].Text = "O";
                field[i, j - 1].Enabled = false;
            }
            else if (j == 0 || j == 9)
            {
                if (field[i, Math.Abs(j - 1)].BackgroundImage == null)
                    field[i, Math.Abs(j - 1)].Text = "O";
                field[i, Math.Abs(j - 1)].Enabled = false;
                if (field[i-1, Math.Abs(j - 1)].BackgroundImage == null)
                    field[i - 1, Math.Abs(j - 1)].Text = "O";
                field[i - 1, Math.Abs(j - 1)].Enabled = false;
                if (field[i+1, Math.Abs(j - 1)].BackgroundImage == null)
                    field[i + 1, Math.Abs(j - 1)].Text = "O";
                field[i + 1, Math.Abs(j - 1)].Enabled = false;
                if (field[i - 1, j].BackgroundImage == null)
                    field[i-1, j].Text = "O";
                field[i-1, j].Enabled = false;
                if (field[i + 1, j].BackgroundImage == null)
                    field[i+1, j].Text = "O";
                field[i+1, j].Enabled = false;
            }
        }

        void Bury(int i, int j, int i1, int j1,Button[,] field)
        {
            Bury(i1, j1, field);
            if (i1 > i && i1 != 9)
            {
                if (field[i1 + 1, j1].ForeColor == Color.Red)
                    Bury(i1, j1, i1 + 1, j1, field);
            }
            else if (i1 < i && i1 != 0)
            {
                if (field[i1 - 1, j1].ForeColor == Color.Red)
                    Bury(i1, j1, i1 - 1, j1, field);
            }
            else if (j1 < j && j1 != 0)
            {
                if (field[i1, j1-1].ForeColor == Color.Red)
                    Bury(i1, j1, i1, j1-1, field);
            }
            else if (j1 > j && j1 != 9)
            {
                if (field[i1, j1 + 1].ForeColor == Color.Red)
                    Bury(i1, j1, i1, j1 + 1, field);
            }
        }

        void Aim(int i, int j, Button[,] field)
        {
            var rand = new Random();
            int cointoss;
            if ((i == 0 && j == 0) || (i == 0 && j == 9) || (i == 9 && j == 9) || (i == 9 && j == 0))
            {
                if (field[Math.Abs(i - 1), j].BackgroundImage != null)
                {
                    Aim(i, j, Math.Abs(i - 1), j, field);
                    return;
                }
                else if (field[i, Math.Abs(j - 1)].BackgroundImage != null)
                {
                    Aim(i, j, i, Math.Abs(j - 1), field);
                    return;
                }
                if (field[Math.Abs(i - 1), j].Text == "" && field[i, Math.Abs(j - 1)].Text == "")
                {
                    rand = new Random();
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, Math.Abs(i - 1), j, field);
                            else
                                Aim(i, j, i, Math.Abs(j - 1), field);
                        }
                        else if (void_size(i, j, true) > 2)
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        else
                            Aim(i, j, Math.Abs(i - 1), j, field);
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        else
                            Aim(i, j, i, Math.Abs(j - 1), field);
                    }
                }
                else if(field[Math.Abs(i - 1), j].Text == "")
                    Aim(i, j, Math.Abs(i - 1), j, field);
                else if(field[i, Math.Abs(j - 1)].Text == "")
                    Aim(i, j, i, Math.Abs(j - 1), field);
            }
            else if (i == 0 || i == 9)
            {
                if (field[Math.Abs(i - 1), j].BackgroundImage != null)
                    Aim(i, j, Math.Abs(i - 1), j, field);
                else if (field[i, j-1].BackgroundImage != null && field[i, j+1].Text == "O")
                    Aim(i, j, i, j-1, field);
                else if (field[i, j+1].BackgroundImage != null && field[i, j-1].Text == "O")
                    Aim(i, j, i, j+1, field);
                else if ((field[i, j + 1].BackgroundImage != null && field[i, j - 1].Text == "")|| (field[i, j - 1].BackgroundImage != null && field[i, j + 1].Text == "")|| (field[i, j + 1].Text=="" && field[i, j - 1].Text == ""&& field[Math.Abs(i - 1), j].Text == "O"))
                {
                    bool shot = false;
                    cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        shot = Aim(i, j, i, j + 1, field);
                        if (!shot)
                            Aim(i, j, i, j - 1, field);
                    }
                    else
                    {
                        shot = Aim(i, j, i, j - 1, field);
                        if (!shot)
                            Aim(i, j, i, j + 1, field);
                    }
                }
                else if (field[Math.Abs(i - 1), j].Text == "" && field[i, j + 1].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(3);
                            if (cointoss == 0)
                                Aim(i, j, Math.Abs(i - 1), j, field);
                            else if (cointoss == 1)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else
                        {
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(3);
                        if (cointoss == 0)
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        else if (cointoss == 1)
                            Aim(i, j, i, j + 1, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if(field[Math.Abs(i - 1), j].Text == "" && field[i, j + 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, Math.Abs(i - 1), j, field);
                            else
                                Aim(i, j, i, j + 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, j + 1, field);
                        }
                        else
                        {
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        else
                            Aim(i, j, i, j + 1, field);
                    }
                }
                else if(field[Math.Abs(i - 1), j].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, Math.Abs(i - 1), j, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, j - 1, field);
                        }
                        else
                        {
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, Math.Abs(i - 1), j, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if(field[i, j - 1].Text == "")
                    Aim(i, j, i, j - 1, field);
                else if(field[i, j + 1].Text == "")
                    Aim(i, j, i, j + 1, field);
                else
                    Aim(i, j, Math.Abs(i - 1), j, field);
            }
            else if (j == 0 || j == 9)
            {
                if (field[i, Math.Abs(j-1)].BackgroundImage != null)
                    Aim(i, j, i, Math.Abs(j - 1), field);
                else if (field[i+1, j].BackgroundImage != null && field[i-1, j].Text == "O")
                    Aim(i, j, i+1, j, field);
                else if (field[i-1, j].BackgroundImage != null && field[i+1, j].Text == "O")
                    Aim(i, j, i-1, j, field);
                else if ((field[i + 1, j].BackgroundImage != null && field[i - 1, j].Text == "") || (field[i - 1, j].BackgroundImage != null && field[i + 1, j].Text == "") || (field[i + 1, j].Text == "" && field[i - 1, j].Text == ""&& field[i, Math.Abs(j - 1)].Text=="O"))
                {
                    bool shot = false;
                    cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        shot = Aim(i, j, i + 1, j, field);
                        if (!shot)
                            Aim(i, j, i - 1, j, field);
                    }
                    else
                    {
                        shot = Aim(i, j, i - 1, j, field);
                        if (!shot)
                            Aim(i, j, i + 1, j, field);
                    }
                }
                else if (field[i, Math.Abs(j - 1)].Text == "" && field[i + 1, j].Text == "" && field[i - 1, j].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(3);
                            if (cointoss == 0)
                                Aim(i, j, i, Math.Abs(j - 1), field);
                            else if (cointoss == 1)
                                Aim(i, j, i + 1, j, field);
                            else
                                Aim(i, j, i - 1, j, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        }
                        else
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i + 1, j, field);
                            else
                                Aim(i, j, i - 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(3);
                        if (cointoss == 0)
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        else if (cointoss == 1)
                            Aim(i, j, i + 1, j, field);
                        else
                            Aim(i, j, i - 1, j, field);
                    }
                }
                else if (field[i, Math.Abs(j - 1)].Text == "" && field[i + 1, j].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i, Math.Abs(j - 1), field);
                            else
                                Aim(i, j, i + 1, j, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        }
                        else
                        {
                            Aim(i, j, i + 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        else
                            Aim(i, j, i + 1, j, field);
                    }
                }
                else if (field[i, Math.Abs(j-1)].Text == "" && field[i - 1, j].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i, Math.Abs(j - 1), field);
                            else
                                Aim(i, j, i - 1, j, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        }
                        else
                        {
                            Aim(i, j, i - 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, i, Math.Abs(j - 1), field);
                        else
                            Aim(i, j, i - 1, j, field);
                    }
                }
                else if (field[i - 1, j].Text == "")
                    Aim(i, j, i - 1, j, field);
                else if (field[i + 1, j].Text == "")
                    Aim(i, j, i + 1, j, field);
                else
                    Aim(i, j, i, Math.Abs(j - 1), field);
            }
            else
            {
                if (field[i, j + 1].BackgroundImage != null && field[i, j - 1].Text == "O")
                    Aim(i, j, i, j + 1, field);
                else if (field[i, j - 1].BackgroundImage != null && field[i, j + 1].Text == "O")
                    Aim(i, j, i, j - 1, field);
                else if (field[i + 1, j].BackgroundImage != null && field[i - 1, j].Text == "O")
                    Aim(i, j, i + 1, j, field);
                else if (field[i - 1, j].BackgroundImage != null && field[i + 1, j].Text == "O")
                    Aim(i, j, i - 1, j, field);
                else if ((field[i + 1, j].BackgroundImage != null && field[i - 1, j].Text == "") || (field[i - 1, j].BackgroundImage != null && field[i + 1, j].Text == "") || (field[i + 1, j].Text == "" && field[i - 1, j].Text == "" && field[i, j + 1].Text == "O" && field[i, j - 1].Text == "O"))
                {
                    bool shot = false;
                    cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        shot = Aim(i, j, i + 1, j, field);
                        if (!shot)
                            Aim(i, j, i - 1, j, field);
                    }
                    else
                    {
                        shot = Aim(i, j, i - 1, j, field);
                        if (!shot)
                            Aim(i, j, i + 1, j, field);
                    }
                }
                else if ((field[i, j + 1].BackgroundImage != null && field[i, j - 1].Text == "") || (field[i, j - 1].BackgroundImage != null && field[i, j + 1].Text == "") || (field[i + 1, j].Text == "O" && field[i - 1, j].Text == "O" && field[i, j + 1].Text == "" && field[i, j - 1].Text == ""))
                {
                    bool shot = false;
                    cointoss = rand.Next(2);
                    if (cointoss == 0)
                    {
                        shot = Aim(i, j, i, j + 1, field);
                        if (!shot)
                            Aim(i, j, i, j - 1, field);
                    }
                    else
                    {
                        shot = Aim(i, j, i, j - 1, field);
                        if (!shot)
                            Aim(i, j, i, j + 1, field);
                    }
                }
                else if (field[i + 1, j].Text == "" && field[i - 1, j].Text == "" && field[i, j + 1].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(4);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else if (cointoss == 1)
                                Aim(i, j, i, j + 1, field);
                            else if (cointoss == 2)
                                Aim(i, j, i, j - 1, field);
                            else
                                Aim(i, j, i + 1, j, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i + 1, j, field);
                            else
                                Aim(i, j, i - 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(4);
                        if (cointoss == 0)
                            Aim(i, j, i - 1, j, field);
                        else if (cointoss == 1)
                            Aim(i, j, i, j + 1, field);
                        else if (cointoss == 2)
                            Aim(i, j, i, j - 1, field);
                        else
                            Aim(i, j, i + 1, j, field);
                    }
                }
                else if (field[i - 1, j].Text == "" && field[i, j + 1].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(3);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else if (cointoss == 1)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if(void_size(i, j, true) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else
                        {
                            Aim(i, j, i - 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(3);
                        if (cointoss == 0)
                            Aim(i, j, i - 1, j, field);
                        else if (cointoss == 1)
                            Aim(i, j, i, j + 1, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if (field[i + 1, j].Text == "" && field[i, j + 1].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(3);
                            if (cointoss == 0)
                                Aim(i, j, i + 1, j, field);
                            else if (cointoss == 1)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else
                        {
                            Aim(i, j, i + 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(3);
                        if (cointoss == 0)
                            Aim(i, j, i + 1, j, field);
                        else if (cointoss == 1)
                            Aim(i, j, i, j + 1, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if (field[i + 1, j].Text == "" && field[i - 1, j].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(3);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else if (cointoss == 1)
                                Aim(i, j, i + 1, j, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, j - 1, field);
                        }
                        else
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else
                                Aim(i, j, i + 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(3);
                        if (cointoss == 0)
                            Aim(i, j, i - 1, j, field);
                        else if (cointoss == 1)
                            Aim(i, j, i + 1, j, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if (field[i + 1, j].Text == "" && field[i - 1, j].Text == "" && field[i, j + 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(3);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else if (cointoss == 1)
                                Aim(i, j, i, j + 1, field);
                            else
                                Aim(i, j, i + 1, j, field);
                        }
                        else if (void_size(i, j, true) > 2)
                        {
                            Aim(i, j, i, j + 1, field);
                        }
                        else
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else
                                Aim(i, j, i + 1, j, field);
                        }
                    }
                    else
                    {
                        cointoss = rand.Next(3);
                        if (cointoss == 0)
                            Aim(i, j, i - 1, j, field);
                        else if (cointoss == 1)
                            Aim(i, j, i, j + 1, field);
                        else
                            Aim(i, j, i + 1, j, field);
                    }
                }
                else if (field[i + 1, j].Text == "" && field[i, j + 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i + 1, j, field);
                            else
                                Aim(i, j, i, j + 1, field);
                        }
                        else if(void_size(i, j, true) > 2)
                            Aim(i, j, i, j + 1, field);
                        else
                            Aim(i, j, i + 1, j, field);
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, i + 1, j, field);
                        else
                            Aim(i, j, i, j + 1, field);
                    }
                }
                else if (field[i - 1, j].Text == "" && field[i, j + 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else
                                Aim(i, j, i, j + 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                            Aim(i, j, i, j + 1, field);
                        else
                            Aim(i, j, i - 1, j, field);
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, i - 1, j, field);
                        else
                            Aim(i, j, i, j + 1, field);
                    }
                }
                else if (field[i + 1, j].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i + 1, j, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                            Aim(i, j, i, j - 1, field);
                        else
                            Aim(i, j, i + 1, j, field);
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, i + 1, j, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if (field[i - 1, j].Text == "" && field[i, j - 1].Text == "")
                {
                    if (label25.Text == "x 0/3")
                    {
                        if (void_size(i, j, true) > 2 && void_size(i, j, false) > 2)
                        {
                            cointoss = rand.Next(2);
                            if (cointoss == 0)
                                Aim(i, j, i - 1, j, field);
                            else
                                Aim(i, j, i, j - 1, field);
                        }
                        else if (void_size(i, j, true) > 2)
                            Aim(i, j, i, j - 1, field);
                        else
                            Aim(i, j, i - 1, j, field);
                    }
                    else
                    {
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            Aim(i, j, i - 1, j, field);
                        else
                            Aim(i, j, i, j - 1, field);
                    }
                }
                else if (field[i - 1, j].Text == "")
                    Aim(i, j, i - 1, j, field);
                else if (field[i, j - 1].Text == "")
                    Aim(i, j, i, j - 1, field);
                else if (field[i + 1, j].Text == "")
                    Aim(i, j, i + 1, j, field);
                else
                    Aim(i, j, i, j + 1, field);
            }
        }

        bool Aim(int i,int j,int i1,int j1,Button[,] field)
        {
            if(field[i1,j1].BackgroundImage==null)
            {
                if(field[i1, j1].ForeColor==Color.Blue)
                {
                    miss.Play();
                    field[i1, j1].Text = "O";
                    field[i1, j1].Enabled = false;
                    field[i1, j1].ForeColor = Color.Blue;
                }
                else
                {
                    hit.Play();
                    field[i1, j1].BackgroundImage = Battleship.Properties.Resources.hittran3;
                    field[i1, j1].Enabled = false;
                    if (!fgame)
                        Thread.Sleep(1000);
                    if (Check(i1, j1, field))
                        Aim(i1, j1, field);
                    else
                    {
                        Check_field(true, true);
                        Check_field(true, false);
                        EShoot();
                    }
                }
                return true;
            }
            if (i1 > i && i1 != 9)
            {
                if (field[i1 + 1, j1].Text == "")
                    return Aim(i1, j1, i1 + 1, j1, field);
            }
            else if (i1 < i && i1 != 0)
            {
                if (field[i1 - 1, j1].Text == "")
                    return Aim(i1, j1, i1 - 1, j1, field);
            }
            else if (j1 < j && j1 != 0)
            {
                if (field[i1, j1 - 1].Text == "")
                    return Aim(i1, j1, i1, j1 - 1, field);
            }
            else if (j1 > j && j1 != 9)
            {
                if (field[i1, j1 + 1].Text == "")
                    return Aim(i1, j1, i1, j1 + 1, field);
            }
            return false;
        }

        void EShoot()
        {
            if (End() == 0)
            {
                loss.Play();
                label28.Text = "DEFEAT";
                label28.Refresh();
                for (int m = 0; m < 10; m++)
                    for (int n = 0; n < 10; n++)
                    {
                        enemy[m, n].Enabled = false;
                        if (enemy[m, n].ForeColor == Color.Red && enemy[m, n].BackgroundImage == null)
                            enemy[m, n].BackColor = Color.Lime;
                    }
                return;
            }
            else
            {
                var rand = new Random();
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        if (player[i, j].BackgroundImage != null && player[i, j].Text != "_")
                        {
                            if (Check(i, j, player))
                                Aim(i, j, player);
                            else
                                EShoot();
                            label28.Text = "YOUR TURN";
                            label28.Refresh();
                            if (End() == 0)
                            {
                                label28.Text = "DEFEAT";
                                Check_field(true, true);
                                label28.Refresh();
                                for (int m = 0; m < 10; m++)
                                    for (int n = 0; n < 10; n++)
                                    {
                                        enemy[m, n].Enabled = false;
                                        if (enemy[m, n].ForeColor == Color.Red && enemy[m, n].BackgroundImage == null)
                                            enemy[m, n].BackColor = Color.Lime;
                                    }
                                return;
                            }
                            return;
                        }
                    }
                int temp1, temp2;
                if (!method)
                {
                    while (true)
                    {
                        temp1 = rand.Next(10);
                        temp2 = rand.Next(10);
                        while (player[temp1, temp2].Text == "O" || player[temp1, temp2].BackgroundImage != null)
                        {
                            temp1 = rand.Next(10);
                            temp2 = rand.Next(10);
                        }
                        if (label27.Text != "x 0/1")
                        {
                            if (void_size(temp1, temp2, true) >= 4 || void_size(temp1, temp2, false) >= 4)
                                break;
                        }
                        else if (label26.Text != "x 0/2")
                        {
                            if (void_size(temp1, temp2, true) >= 3 || void_size(temp1, temp2, false) >= 3)
                                break;
                        }
                        else if (label25.Text != "x 0/3")
                        {
                            if (void_size(temp1, temp2, true) >= 2 || void_size(temp1, temp2, false) >= 2)
                                break;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    if (player[0, 0].Text == "" && player[0, 0].BackgroundImage == null && player[9, 9].Text == "" && player[9, 9].BackgroundImage == null && player[0, 9].Text == "" && player[0, 9].BackgroundImage == null && player[9, 0].Text == "" && player[9, 0].BackgroundImage == null)
                    {
                        int cointoss = rand.Next(2);
                        if (cointoss == 0)
                            temp2 = 0;
                        else
                            temp2 = 9;
                        cointoss = rand.Next(2);
                        if (cointoss == 0)
                            temp1 = 0;
                        else
                            temp1 = 9;
                    }
                    else if (player[0, 0].Text == "" && player[0, 0].BackgroundImage == null && player[9, 9].Text == "" && player[9, 9].BackgroundImage == null && (player[0, 9].Text == "O" || player[0, 9].BackgroundImage != null) && (player[9, 0].Text == "O" || player[9, 0].BackgroundImage != null))
                    {
                        int cointoss = rand.Next(2);
                        if (cointoss == 0)
                        {
                            temp2 = 0;
                            temp1 = 0;
                        }
                        else
                        {
                            temp2 = 9;
                            temp1 = 9;
                        }
                    }
                    else if ((player[0, 0].Text == "O" || player[0, 0].BackgroundImage != null) && (player[9, 9].Text == "O" || player[9, 9].BackgroundImage != null) && player[0, 9].Text == "" && player[0, 9].BackgroundImage == null && player[9, 0].Text == "" &&player[9, 0].BackgroundImage == null)
                    {
                        int cointoss = rand.Next(2);
                        if (cointoss == 0)
                        {
                            temp2 = 0;
                            temp1 = 9;
                        }
                        else
                        {
                            temp2 = 9;
                            temp1 = 0;
                        }
                    }
                    else if ((player[0, 0].Text == "O" || player[0, 0].BackgroundImage != null) && (player[9, 9].Text == "O" || player[9, 9].BackgroundImage != null) && (player[0, 9].Text == "O" || player[0, 9].BackgroundImage != null) && (player[9, 0].Text == "O" || player[9, 0].BackgroundImage != null) && S_corner==4)
                    {
                        int cointoss = rand.Next(4);
                        int cointoss2 = rand.Next(2);
                        if (cointoss == 0)
                        {
                            temp1 = 1 + cointoss2;
                            temp2 = 0;
                        }
                        else if (cointoss == 1)
                        {
                            temp1 = 9;
                            temp2 = 1 + cointoss2;
                        }
                        else if (cointoss == 2)
                        {
                            temp1 = 8 - cointoss2;
                            temp2 = 9;
                        }
                        else
                        {
                            temp1 = 0;
                            temp2 = 8 - cointoss2;
                        }
                        S_corner = cointoss;
                        Net_Start = cointoss2;
                    }
                    else
                    {
                        temp1 = planI;
                        temp2 = planJ;
                    }
                    while (player[temp1, temp2].Text == "O" || player[temp1, temp2].BackgroundImage != null)
                    {
                        if ((player[0, 0].Text == "O" || player[0, 0].BackgroundImage != null) && player[9, 9].Text == "" && player[9, 9].BackgroundImage == null)
                        {
                            temp1++;
                            temp2++;
                        }
                        else if ((player[9, 0].Text == "O" || player[9, 0].BackgroundImage != null) && player[0, 9].Text == "" && player[0, 9].BackgroundImage == null)
                        {
                            temp1--;
                            temp2++;
                        }
                        else if (player[0, 0].Text == "" && player[0, 0].BackgroundImage == null && (player[9, 9].Text == "O" || player[9, 9].BackgroundImage != null))
                        {
                            temp1--;
                            temp2--;
                        }
                        else if (player[9, 0].Text == "" && player[9, 0].BackgroundImage == null && (player[0, 9].Text == "O" || player[0, 9].BackgroundImage != null))
                        {
                            temp1++;
                            temp2--;
                        }
                        else if ((temp1 == 0 && temp2 == 1) || (temp1 == 1 && temp2 == 2) || (temp1 == 2 && temp2 == 3) || (temp1 == 3 && temp2 == 4))
                        {
                            if (S_corner == 0)
                            {
                                temp1++;
                            }
                            else
                            {
                                temp1 += 2;
                                temp2--;
                            }
                        }
                        else if ((temp1 == 8 && temp2 == 0) || (temp1 == 7 && temp2 == 1) || (temp1 == 6 && temp2 == 2) || (temp1 == 5 && temp2 == 3))
                        {
                            if (S_corner == 1)
                            {
                                temp2++;
                            }
                            else
                            {
                                temp1++;
                                temp2 += 2;
                            }
                        }
                        else if ((temp1 == 9 && temp2 == 8) || (temp1 == 8 && temp2 == 7) || (temp1 == 7 && temp2 == 6) || (temp1 == 6 && temp2 == 5))
                        {
                            if (S_corner == 2)
                            {
                                temp1--;
                            }
                            else
                            {
                                temp1 -= 2;
                                temp2++;
                            }
                        }
                        else if ((temp1 == 1 && temp2 == 9) || (temp1 == 2 && temp2 == 8) || (temp1 == 3 && temp2 == 7) || (temp1 == 4 && temp2 == 6))
                        {
                            if (S_corner == 3)
                            {
                                temp2--;
                            }
                            else
                            {
                                temp2 -= 2;
                                temp1--;
                            }
                        }
                        else if ((temp1 == 0 && temp2 == 2) || (temp1 == 1 && temp2 == 3) || (temp1 == 2 && temp2 == 4) || (temp1 == 3 && temp2 == 5))
                        {
                            if (S_corner == 0)
                            {
                                temp2--;
                                temp1 += 2;
                            }
                            else
                            {
                                temp2 -= 2;
                                temp1++;
                            }
                        }
                        else if ((temp1 == 7 && temp2 == 0) || (temp1 == 6 && temp2 == 1) || (temp1 == 5 && temp2 == 2) || (temp1 == 4 && temp2 == 3))
                        {
                            if (S_corner == 1)
                            {
                                temp1++;
                                temp2 += 2;
                            }
                            else
                            {
                                temp1 += 2;
                                temp2++;
                            }
                        }
                        else if ((temp1 == 9 && temp2 == 7) || (temp1 == 8 && temp2 == 6) || (temp1 == 7 && temp2 == 5) || (temp1 == 6 && temp2 == 4))
                        {
                            if (S_corner == 2)
                            {
                                temp2++;
                                temp1 -= 2;
                            }
                            else
                            {
                                temp2 += 2;
                                temp1--;
                            }
                        }
                        else if ((temp1 == 2 && temp2 == 9) || (temp1 == 3 && temp2 == 8) || (temp1 == 4 && temp2 == 7) || (temp1 == 5 && temp2 == 6))
                        {
                            if (S_corner == 3)
                            {
                                temp1--;
                                temp2 -= 2;
                            }
                            else
                            {
                                temp1 -= 2;
                                temp2--;
                            }
                        }
                        else if ((temp1 == 0 && temp2 > 2)|| (temp1 == 1 && temp2 > 3)||(temp1 == 2 && temp2 > 4)||(temp1 == 3 && temp2 > 5&&temp2<8))
                            temp2 -= 2;
                        else if ((temp2 == 0 && temp1 < 7)|| (temp2 == 1 && temp1 < 6) || (temp2 == 2 && temp1 < 5) || (temp2 == 3 && temp1 < 4))
                            temp1 += 2;
                        else if ((temp1 == 9 && temp2 < 7)|| (temp1 == 8 && temp2 < 6) || (temp1 == 7 && temp2 < 5) || (temp1 == 6 && temp2 < 4))
                            temp2 += 2;
                        else if ((temp2 == 9 && temp1 > 2)|| (temp2 == 8 && temp1 > 3) || (temp2 == 7 && temp1 > 4) || (temp2 == 6 && temp1 > 5))
                            temp1 -= 2;
                        else if((temp1==4||temp1==5)&&(temp2==4||temp2==5))
                        {
                            if (Net_Start == 0)
                            {
                                if(S_corner==0)
                                {
                                    temp1 = 2;
                                    temp2 = 0;
                                }
                                else if (S_corner == 1)
                                {
                                    temp1 = 9;
                                    temp2 = 2;
                                }
                                else if (S_corner == 2)
                                {
                                    temp1 = 7;
                                    temp2 = 9;
                                }
                                else
                                {
                                    temp1 = 0;
                                    temp2 = 7;
                                }
                            }
                            else
                            {
                                if (S_corner == 0)
                                {
                                    temp1 = 1;
                                    temp2 = 0;
                                }
                                else if (S_corner == 1)
                                {
                                    temp1 = 9;
                                    temp2 = 1;
                                }
                                else if (S_corner == 2)
                                {
                                    temp1 = 8;
                                    temp2 = 9;
                                }
                                else
                                {
                                    temp1 = 0;
                                    temp2 = 8;
                                }
                            }
                        }
                    }
                    planI = temp1;
                    planJ = temp2;
                }
                if (player[temp1, temp2].ForeColor == Color.Blue)
                {
                    miss.Play();
                    player[temp1, temp2].Text = "O";
                    player[temp1, temp2].Enabled = false;
                    player[temp1, temp2].ForeColor = Color.Blue;
                }
                else
                {
                    hit.Play();
                    player[temp1, temp2].BackgroundImage = Battleship.Properties.Resources.hittran3;
                    player[temp1, temp2].Enabled = false;
                    if (!fgame)
                        Thread.Sleep(1000);
                    EShoot();
                }
                label28.Text = "YOUR TURN";
                label28.Refresh();
                if (End() == 0)
                {
                    label28.Text = "DEFEAT";
                    Check_field(true,true);
                    label28.Refresh();
                    for (int m = 0; m < 10; m++)
                        for (int n = 0; n < 10; n++)
                        {
                            enemy[m, n].Enabled = false;
                            if (enemy[m, n].ForeColor == Color.Red && enemy[m, n].BackgroundImage == null)
                                enemy[m, n].BackColor = Color.Lime;
                        }
                    return;
                }
            }
        }

        int void_size(int i, int j,bool horiz)
        {
            int size = 1;
            if(horiz)
            {
                if(j!=9)
                    if (player[i, j + 1].BackgroundImage == null && player[i, j + 1].Text != "O")
                        size = void_size(i, j + 1, i, j, size);
                if(j!=0)
                    if (player[i, j - 1].BackgroundImage == null && player[i, j - 1].Text != "O")
                        size = void_size(i, j - 1, i, j, size);
                return size;
            }
            else
            {
                if(i!=9)
                    if (player[i+1, j].BackgroundImage == null && player[i+1, j].Text != "O")
                        size = void_size(i+1,j, i, j, size);
                if(i!=0)
                    if (player[i-1, j].BackgroundImage == null && player[i-1, j].Text != "O")
                        size = void_size(i-1, j, i, j, size);
                return size;
            }
        }

        int void_size(int i, int j,int i1,int j1, int size)
        {
            size++;
            if((i == 0 && j == 0) || (i == 0 && j == 9) || (i == 9 && j== 9) || (i== 9 && j == 0))
            {
                return size;
            }
            else if(i==0||i==9)
            {
                if (j > j1 && player[i, j + 1].BackgroundImage == null && player[i, j + 1].Text != "O")
                    return void_size(i, j + 1, i, j, size);
                else if (j < j1 && player[i, j - 1].BackgroundImage == null && player[i, j - 1].Text != "O")
                    return void_size(i, j - 1, i, j, size);
                else
                    return size;
            }
            else if (j == 0 || j == 9)
            {
                if (i > i1 && player[i+1, j].BackgroundImage == null && player[i+1, j].Text != "O")
                    return void_size(i+1, j , i, j, size);
                else if (i < i1 && player[i-1, j].BackgroundImage == null && player[i-1, j].Text != "O")
                    return void_size(i-1, j, i, j, size);
                else
                    return size;
            }
            else
            {
                if (i > i1 && player[i + 1, j].BackgroundImage == null && player[i + 1, j].Text != "O")
                    return void_size(i + 1, j, i, j, size);
                else if (i < i1 && player[i - 1, j].BackgroundImage == null && player[i - 1, j].Text != "O")
                    return void_size(i - 1, j, i, j, size);
                else if (j > j1 && player[i, j + 1].BackgroundImage == null && player[i, j + 1].Text != "O")
                    return void_size(i, j + 1, i, j, size);
                else if (j < j1 && player[i, j - 1].BackgroundImage == null && player[i, j - 1].Text != "O")
                    return void_size(i, j - 1, i, j, size);
                else
                    return size;
            }
        }

        private void EA1_Click(object sender, EventArgs e)
        {
            Shoot(0, 0);
        }

        private void EA2_Click(object sender, EventArgs e)
        {
            Shoot(1, 0);
        }

        private void EA3_Click(object sender, EventArgs e)
        {
            Shoot(2, 0);
        }

        private void EA4_Click(object sender, EventArgs e)
        {
            Shoot(3, 0);
        }

        private void EA5_Click(object sender, EventArgs e)
        {
            Shoot(4, 0);
        }

        private void EA6_Click(object sender, EventArgs e)
        {
            Shoot(5, 0);
        }

        private void EA7_Click(object sender, EventArgs e)
        {
            Shoot(6, 0);
        }

        private void EA8_Click(object sender, EventArgs e)
        {
            Shoot(7, 0);
        }

        private void EA9_Click(object sender, EventArgs e)
        {
            Shoot(8, 0);
        }

        private void EA10_Click(object sender, EventArgs e)
        {
            Shoot(9, 0);
        }

        private void EB1_Click(object sender, EventArgs e)
        {
            Shoot(0, 1);
        }

        private void EB2_Click(object sender, EventArgs e)
        {
            Shoot(1, 1);
        }

        private void EB3_Click(object sender, EventArgs e)
        {
            Shoot(2, 1);
        }

        private void EB4_Click(object sender, EventArgs e)
        {
            Shoot(3, 1);
        }

        private void EB5_Click(object sender, EventArgs e)
        {
            Shoot(4, 1);
        }

        private void EB6_Click(object sender, EventArgs e)
        {
            Shoot(5, 1);
        }

        private void EB7_Click(object sender, EventArgs e)
        {
            Shoot(6, 1);
        }

        private void EB8_Click(object sender, EventArgs e)
        {
            Shoot(7, 1);
        }

        private void EB9_Click(object sender, EventArgs e)
        {
            Shoot(8, 1);
        }

        private void EB10_Click(object sender, EventArgs e)
        {
            Shoot(9, 1);
        }

        private void EC1_Click(object sender, EventArgs e)
        {
            Shoot(0, 2);
        }

        private void EC2_Click(object sender, EventArgs e)
        {
            Shoot(1, 2);
        }

        private void EC3_Click(object sender, EventArgs e)
        {
            Shoot(2, 2);
        }

        private void EC4_Click(object sender, EventArgs e)
        {
            Shoot(3, 2);
        }

        private void EC5_Click(object sender, EventArgs e)
        {
            Shoot(4, 2);
        }

        private void EC6_Click(object sender, EventArgs e)
        {
            Shoot(5, 2);
        }

        private void EC7_Click(object sender, EventArgs e)
        {
            Shoot(6, 2);
        }

        private void EC8_Click(object sender, EventArgs e)
        {
            Shoot(7, 2);
        }

        private void EC9_Click(object sender, EventArgs e)
        {
            Shoot(8, 2);
        }

        private void EC10_Click(object sender, EventArgs e)
        {
            Shoot(9, 2);
        }

        private void ED1_Click(object sender, EventArgs e)
        {
            Shoot(0, 3);
        }

        private void ED2_Click(object sender, EventArgs e)
        {
            Shoot(1, 3);
        }

        private void ED3_Click(object sender, EventArgs e)
        {
            Shoot(2, 3);
        }

        private void ED4_Click(object sender, EventArgs e)
        {
            Shoot(3, 3);
        }

        private void ED5_Click(object sender, EventArgs e)
        {
            Shoot(4, 3);
        }

        private void ED6_Click(object sender, EventArgs e)
        {
            Shoot(5, 3);
        }

        private void ED7_Click(object sender, EventArgs e)
        {
            Shoot(6, 3);
        }

        private void ED8_Click(object sender, EventArgs e)
        {
            Shoot(7, 3);
        }

        private void ED9_Click(object sender, EventArgs e)
        {
            Shoot(8, 3);
        }

        private void ED10_Click(object sender, EventArgs e)
        {
            Shoot(9, 3);
        }

        private void EE1_Click(object sender, EventArgs e)
        {
            Shoot(0, 4);
        }

        private void EE2_Click(object sender, EventArgs e)
        {
            Shoot(1, 4);
        }

        private void EE3_Click(object sender, EventArgs e)
        {
            Shoot(2, 4);
        }

        private void EE4_Click(object sender, EventArgs e)
        {
            Shoot(3, 4);
        }

        private void EE5_Click(object sender, EventArgs e)
        {
            Shoot(4, 4);
        }

        private void EE6_Click(object sender, EventArgs e)
        {
            Shoot(5, 4);
        }

        private void EE7_Click(object sender, EventArgs e)
        {
            Shoot(6, 4);
        }

        private void EE8_Click(object sender, EventArgs e)
        {
            Shoot(7, 4);
        }

        private void EE9_Click(object sender, EventArgs e)
        {
            Shoot(8, 4);
        }

        private void EE10_Click(object sender, EventArgs e)
        {
            Shoot(9, 4);
        }

        private void EF1_Click(object sender, EventArgs e)
        {
            Shoot(0, 5);
        }

        private void EF2_Click(object sender, EventArgs e)
        {
            Shoot(1, 5);
        }

        private void EF3_Click(object sender, EventArgs e)
        {
            Shoot(2, 5);
        }

        private void EF4_Click(object sender, EventArgs e)
        {
            Shoot(3, 5);
        }

        private void EF5_Click(object sender, EventArgs e)
        {
            Shoot(4, 5);
        }

        private void EF6_Click(object sender, EventArgs e)
        {
            Shoot(5, 5);
        }

        private void EF7_Click(object sender, EventArgs e)
        {
            Shoot(6, 5);
        }

        private void EF8_Click(object sender, EventArgs e)
        {
            Shoot(7, 5);
        }

        private void EF9_Click(object sender, EventArgs e)
        {
            Shoot(8, 5);
        }

        private void EF10_Click(object sender, EventArgs e)
        {
            Shoot(9, 5);
        }

        private void EG1_Click(object sender, EventArgs e)
        {
            Shoot(0, 6);
        }

        private void EG2_Click(object sender, EventArgs e)
        {
            Shoot(1, 6);
        }

        private void EG3_Click(object sender, EventArgs e)
        {
            Shoot(2, 6);
        }

        private void EG4_Click(object sender, EventArgs e)
        {
            Shoot(3, 6);
        }

        private void EG5_Click(object sender, EventArgs e)
        {
            Shoot(4, 6);
        }

        private void EG6_Click(object sender, EventArgs e)
        {
            Shoot(5, 6);
        }

        private void EG7_Click(object sender, EventArgs e)
        {
            Shoot(6, 6);
        }

        private void EG8_Click(object sender, EventArgs e)
        {
            Shoot(7, 6);
        }

        private void EG9_Click(object sender, EventArgs e)
        {
            Shoot(8, 6);
        }

        private void EG10_Click(object sender, EventArgs e)
        {
            Shoot(9, 6);
        }

        private void EH1_Click(object sender, EventArgs e)
        {
            Shoot(0, 7);
        }

        private void EH2_Click(object sender, EventArgs e)
        {
            Shoot(1, 7);
        }

        private void EH3_Click(object sender, EventArgs e)
        {
            Shoot(2, 7);
        }

        private void EH4_Click(object sender, EventArgs e)
        {
            Shoot(3, 7);
        }

        private void EH5_Click(object sender, EventArgs e)
        {
            Shoot(4, 7);
        }

        private void EH6_Click(object sender, EventArgs e)
        {
            Shoot(5, 7);
        }

        private void EH7_Click(object sender, EventArgs e)
        {
            Shoot(6, 7);
        }

        private void EH8_Click(object sender, EventArgs e)
        {
            Shoot(7, 7);
        }

        private void EH9_Click(object sender, EventArgs e)
        {
            Shoot(8, 7);
        }

        private void EH10_Click(object sender, EventArgs e)
        {
            Shoot(9, 7);
        }

        private void EI1_Click(object sender, EventArgs e)
        {
            Shoot(0, 8);
        }

        private void EI2_Click(object sender, EventArgs e)
        {
            Shoot(1, 8);
        }

        private void EI3_Click(object sender, EventArgs e)
        {
            Shoot(2, 8);
        }

        private void EI4_Click(object sender, EventArgs e)
        {
            Shoot(3, 8);
        }

        private void EI5_Click(object sender, EventArgs e)
        {
            Shoot(4, 8);
        }

        private void EI6_Click(object sender, EventArgs e)
        {
            Shoot(5, 8);
        }

        private void EI7_Click(object sender, EventArgs e)
        {
            Shoot(6, 8);
        }

        private void EI8_Click(object sender, EventArgs e)
        {
            Shoot(7, 8);
        }

        private void EI9_Click(object sender, EventArgs e)
        {
            Shoot(8, 8);
        }

        private void EI10_Click(object sender, EventArgs e)
        {
            Shoot(9, 8);
        }

        private void EJ1_Click(object sender, EventArgs e)
        {
            Shoot(0, 9);
        }

        private void EJ2_Click(object sender, EventArgs e)
        {
            Shoot(1, 9);
        }

        private void EJ3_Click(object sender, EventArgs e)
        {
            Shoot(2, 9);
        }

        private void EJ4_Click(object sender, EventArgs e)
        {
            Shoot(3, 9);
        }

        private void EJ5_Click(object sender, EventArgs e)
        {
            Shoot(4, 9);
        }

        private void EJ6_Click(object sender, EventArgs e)
        {
            Shoot(5, 9);
        }

        private void EJ7_Click(object sender, EventArgs e)
        {
            Shoot(6, 9);
        }

        private void EJ8_Click(object sender, EventArgs e)
        {
            Shoot(7, 9);
        }

        private void EJ9_Click(object sender, EventArgs e)
        {
            Shoot(8, 9);
        }

        private void EJ10_Click(object sender, EventArgs e)
        {
            Shoot(9, 9);
        }

        private void Random_Click(object sender, EventArgs e)
        {
            if(prepare)
            {
                Random_Formation(player);
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        if (player[i, j].ForeColor == Color.Red)
                            player[i, j].BackColor = Color.Lime;
                    }
                Check_field(false,true);
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Choice.Owner = this;
            Choice.Fgame = fgame;
            Choice.Net = method;
            if (DialogResult.OK == Choice.ShowDialog())
            {
                fgame = Choice.Fgame;
                method = Choice.Net;
            }
        }
    }
}
