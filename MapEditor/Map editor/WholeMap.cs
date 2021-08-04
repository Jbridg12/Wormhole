using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_editor
{
    public partial class WholeMap : Form
    {

        string[] list = new string[26];

        public WholeMap()
        {
            InitializeComponent();
        }
        public string[] List
        {
            get { return list; }
        }

        public string room1
        {
            get { return Room1.Text; }
        }


        private void WholeMap_Load(object sender, EventArgs e)
        {
           
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
           
            // Close the Whole Map Form
            this.Close();
        }

        private void Bottom_Click(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            string a = string.Empty;
            int x = 0;

            for (int i = 0; i < pressed.Name.Length; i++)
            {
                if (Char.IsDigit(pressed.Name[i]))
                    a += pressed.Name[i];
            }

            if (a.Length > 0)
                x = int.Parse(a);

            switch (pressed.Text)
            {
                case "0":
                    pressed.BackColor = Color.Aqua; 
                    pressed.Text = "1";
                    list[x] = "1";
                    break;

                case "1":
                    pressed.BackColor = Color.Snow; 
                    pressed.Text = "0";
                    list[x] = "0";
                    break;
            }
        }

        private void Room1_Click(object sender, EventArgs e)
        {

        }
    }
}
