using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        public bool Net { get { return Netter.Checked; } set { Netter.Checked = value; } }
        public bool Fgame { get { return Fast.Checked; } set { Fast.Checked = value; } }
        private void Netter_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Battleship.Properties.Resources._23__Handle_3;
        }

        private void Lucky_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Battleship.Properties.Resources._21__Handle_1;
        }

        private void Normal_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = Battleship.Properties.Resources._21__Handle_1;
        }

        private void Fast_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = Battleship.Properties.Resources._23__Handle_3;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            if(Net)
                pictureBox2.BackgroundImage = Battleship.Properties.Resources._23__Handle_3;
            else
                pictureBox2.BackgroundImage = Battleship.Properties.Resources._21__Handle_1;
            if(Fgame)
                pictureBox3.BackgroundImage = Battleship.Properties.Resources._23__Handle_3;
            else
                pictureBox3.BackgroundImage = Battleship.Properties.Resources._21__Handle_1;
        }
    }
}
