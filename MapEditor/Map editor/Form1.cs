using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_editor
{
    //Create by Zejun Meng
    /*
     * I think this program may not be easy to maintain. I don’t think of other ways to simplify them at the moment, I will find a way to simplify these buttons later
    */
    public partial class Form1 : Form
    {
        //Door color is Aqua, Wall color is Lime

        Dictionary<char, int> floorX;

        string[,] form = new string[12, 20]; // Record the input of the rows and columns of the map , the map size is 12 X 20
        string[] rooms = new string[25]; // array of saved room strings

       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            floorX = new Dictionary<char, int>
            {
                {'A', 1 },
                {'B', 2 },
                {'C', 3 },
                {'D', 4 },
                {'E', 5 },
                {'F', 6 },
                {'G', 7 },
                {'H', 8 },
                {'I', 9 },
                {'J', 10 }//,
                //{'K', 11 },
                //{'L', 12 },
                //{'M', 13 }

            };
            //Set the text of the four corners
            form[0, 0] = "C";
            form[0, 19] = "C";
            form[11, 0] = "C";
            form[11, 19] = "C";

            //Set map
            //Top Wall

            form[0, 1] = "*";
            form[0, 2] = "*";
            form[0, 3] = "*";
            form[0, 4] = "d";//Door
            form[0, 5] = "D";//Door
            form[0, 6] = "*";
            form[0, 7] = "*";
            form[0, 8] = "*";
            form[0, 9] = "*";
            form[0, 10] = "*";
            form[0, 11] = "*";
            form[0, 12] = "*";
            form[0, 13] = "*";
            form[0, 14] = "*";
            form[0, 15] = "*";
            form[0, 16] = "*";
            form[0, 17] = "*";
            form[0, 18] = "*";

            //Bottom wall
            for (int i = 1; i < 19; i++)
            {
                form[11, i] = "*";
            }


            //Left wall
            for (int i = 1; i < 11; i++)
            {
                form[i, 0] = "*";
            }

            //Right wall
            for (int i = 1; i < 11; i++)
            {
                form[i, 19] = "*";
            }

            //Floor A
            for (int i = 1; i < 19; i++)
            {
                form[1, i] = "-";
            }
            //Floor B
            form[2, 1] = "-";
            form[2, 2] = "-";
            form[2, 3] = "-";
            form[2, 4] = "-";
            form[2, 5] = "-";
            form[2, 6] = "E";
            form[2, 7] = "-";
            form[2, 8] = "-";
            form[2, 9] = "-";
            form[2, 10] = "-";
            form[2, 11] = "-";
            form[2, 12] = "-";
            form[2, 13] = "-";
            form[2, 14] = "-";
            form[2, 15] = "-";
            form[2, 16] = "-";
            form[2, 17] = "-";
            form[2, 18] = "-";
            //Floor C
            form[3, 1] = "-";
            form[3, 2] = "E";
            form[3, 3] = "-";
            form[3, 4] = "-";
            form[3, 5] = "-";
            form[3, 6] = "-";
            form[3, 7] = "-";
            form[3, 8] = "-";
            form[3, 9] = "-";
            form[3, 10] = "-";
            form[3, 11] = "E";
            form[3, 12] = "-";
            form[3, 13] = "-";
            form[3, 14] = "-";
            form[3, 15] = "-";
            form[3, 16] = "-";
            form[3, 17] = "-";
            form[3, 18] = "-";
            //Floor D
            for (int i = 1; i < 19; i++)
            {
                form[4, i] = "-";
            }

            //Floor E
            form[5, 1] = "-";
            form[5, 2] = "-";
            form[5, 3] = "*";
            form[5, 4] = "*";
            form[5, 5] = "-";
            form[5, 6] = "-";
            form[5, 7] = "-";
            form[5, 8] = "-";
            form[5, 9] = "-";
            form[5, 10] = "-";
            form[5, 11] = "-";
            form[5, 12] = "-";
            form[5, 13] = "-";
            form[5, 14] = "-";
            form[5, 15] = "-";
            form[5, 16] = "-";
            form[5, 17] = "-";
            form[5, 18] = "-";
            //Floor F
            form[6, 1] = "-";
            form[6, 2] = "-";
            form[6, 3] = "*";
            form[6, 4] = "*";
            form[6, 5] = "-";
            form[6, 6] = "-";
            form[6, 7] = "-";
            form[6, 8] = "-";
            form[6, 9] = "-";
            form[6, 10] = "-";
            form[6, 11] = "-";
            form[6, 12] = "-";
            form[6, 13] = "-";
            form[6, 14] = "-";
            form[6, 15] = "-";
            form[6, 16] = "-";
            form[6, 17] = "-";
            form[6, 18] = "-";
            //Floor G
            for (int i = 1; i < 19; i++)
            {
                form[7, i] = "-";
            }
            //Floor H
            for (int i = 1; i < 19; i++)
            {
                form[8, i] = "-";
            }

            //Floor I
            form[9, 1] = "-";
            form[9, 2] = "-";
            form[9, 3] = "-";
            form[9, 4] = "-";
            form[9, 5] = "-";
            form[9, 6] = "-";
            form[9, 7] = "-";
            form[9, 8] = "-";
            form[9, 9] = "-";
            form[9, 10] = "-";
            form[9, 11] = "-";
            form[9, 12] = "-";
            form[9, 13] = "-";
            form[9, 14] = "-";
            form[9, 15] = "-";
            form[9, 16] = "-";
            form[9, 17] = "-";
            form[9, 18] = "-";

            //Floor J
            for (int i = 1; i < 19; i++)
            {
                form[10, i] = "-";
            }
            ////Floor K
            //form[11, 1] = "-";
            //form[11, 2] = "-";
            //form[11, 3] = "-";
            //form[11, 4] = "-";
            //form[11, 5] = "-";
            //form[11, 6] = "-";
            //form[11, 7] = "P";
            //form[11, 8] = "-";
            //form[11, 9] = "-";
            //form[11, 10] = "-";
            //form[11, 11] = "-";
            //form[11, 12] = "-";
            //form[11, 13] = "-";

            ////Floor L
            //for (int i = 1; i < 14; i++)
            //{
            //    form[12, i] = "-";
            //}
            ////Floor M
            //for (int i = 1; i < 14; i++)
            //{
            //    form[13, i] = "-";
            //}
        }

        private void WholeMap_Click(object sender, EventArgs e)
        {
            WholeMap WholeMap = new WholeMap();
            WholeMap.Show();

        }

        //Create Map !
        private void Create_Click(object sender, EventArgs e)
        {
            //From HW3
            //create the text file
            // Dump out the array to a text file named Map.txt. 


            WholeMap wholeMap = new WholeMap();
            

            try
            {
               

                StreamWriter output = new StreamWriter("Map.txt");
                string room = "";
                string No = "";
                // write out a comma delimited lines of text
                for(int i = 0; i < 25; i++)
                {
                    //rooms[i] = wholeMap.List[i];
                   // rooms[i] = ""+i;
                    No = rooms[i];
                    output.Write(No);
                }
                
                output.Write(wholeMap.room1);

                for (int i = 0; i < 12; i++)
                {
                    // which Line                 
                   // output.Write("L"+(i+1));

                    for (int j = 0; j < 20; j++)
                    {
                        room = form[i, j];
                        output.Write(room);
                        
                        //if (j < 20 - 1)  // comma separated on same line
                        //{
                        //    //output.Write(form[i, j] + ",");
                        //    output.Write(form[i, j]);
                        //}
                        //else // no comma after number, start a new line
                        //{
                        //    output.WriteLine(form[i, j]);
                        //}
                    }
                    output.Write(",");
                }
                // done - so close the file
                output.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception detected: " + ex.Message);
                Console.WriteLine("Exiting program");
                return;
            }
         
           
        }

        // Top
        private void WallTop_Click(object sender, EventArgs e)
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
                case "D":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "d"; //Door2  
                    form[0, x] = "d";
                    break;

                case "d":
                    pressed.BackColor = Color.Lime; //Wall color
                    pressed.Text = "*"; //Wall 
                    form[0, x] = "*";
                    break;

                case "*":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door1                   
                    form[0, x] = "D";
                    break;

            }
        }
        //Bottom
        private void WallBottom_Click(object sender, EventArgs e)
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
                case "D":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "d"; //Door2
                    form[11, x] = "d";
                    break;

                case "d":
                    pressed.BackColor = Color.Lime; //Wall color
                    pressed.Text = "*"; //Wall 
                    form[11, x] = "*";
                    break;

                case "*":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door 1                   
                    form[11, x] = "D";
                    break;

            }
        }
        //Left
        private void WallLeft_Click(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            string a = string.Empty;
            int y = 0;

            for (int i = 0; i < pressed.Name.Length; i++)
            {
                if (Char.IsDigit(pressed.Name[i]))
                    a += pressed.Name[i];
            }

            if (a.Length > 0)
                y = int.Parse(a);

            switch (pressed.Text)
            {
                case "D":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "d"; //Wall
                    form[0, y] = "d";
                    break;

                case "d":
                    pressed.BackColor = Color.Lime; //Wall color
                    pressed.Text = "*"; //Wall 
                    form[0, y] = "*";
                    break;

                case "*":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door 1                 
                    form[0, y] = "D";
                    break;

            }
        }
        //Right
        private void WallRight_Click(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            string a = string.Empty;
            int y = 0;

            for (int i = 0; i < pressed.Name.Length; i++)
            {
                if (Char.IsDigit(pressed.Name[i]))
                    a += pressed.Name[i];
            }

            if (a.Length > 0)
                y = int.Parse(a);

            switch (pressed.Text)
            {
                case "D":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "d"; //Door 2
                    form[11, y] = "d";
                    break;

                case "d":
                    pressed.BackColor = Color.Lime; //Wall color
                    pressed.Text = "*"; //Wall 
                    form[11, y] = "*";
                    break;

                case "*":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door 1                  
                    form[11, y] = "D";
                    break;

            }
        }
        // Floor part
        private void Floor_Click(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            char x = pressed.Name.ToCharArray()[5];
            string a = string.Empty;
            int y = 0;

            for (int i = 0; i < pressed.Name.Length; i++)
            {
                if (Char.IsDigit(pressed.Name[i]))
                    a += pressed.Name[i];
            }

            if (a.Length > 0)
                y = int.Parse(a);

            switch (pressed.Text)
            {
                case "-":
                    pressed.BackColor = Color.Red; //E1 color
                    pressed.Text = "1"; //Enemy1                   
                    form[floorX[x], y] = "E";
                    break;
                //case "P":
                //    pressed.BackColor = Color.Red; //E1 color
                //    pressed.Text = "E1"; //Enemy1                   
                //    form[floorX[x], y] = "E1";
                //    break;
                case "1":
                    pressed.BackColor = Color.Fuchsia; //E2 color
                    pressed.Text = "2"; //Enemy2 
                    form[floorX[x], y] = "E";
                    break;
                case "2":
                    pressed.BackColor = Color.SlateBlue; //E3 color
                    pressed.Text = "3"; //Enemy3
                    form[floorX[x], y] = "E";
                    break;
                case "3":
                    pressed.BackColor = Color.Lime; //Wall color
                    pressed.Text = "*"; //Wall
                    form[floorX[x], y] = "*";
                    break;
                case "*":
                    pressed.BackColor = Color.Snow; //blank color
                    pressed.Text = "-"; //blank                   
                    form[floorX[x], y] = "-";
                    break;
            }
        }

        //private void Save_Room(object sender, EventArgs e)
        //{
        //  rooms[][] = form;
        //}

        private void Write(object sender, EventArgs e)
        {
            //foreach (string str in rooms)
            //{
            //    //write ...
            //}
        }

        private void FloorA15_Click(object sender, EventArgs e)
        {

        }

        private void WallTop15_Click(object sender, EventArgs e)
        {

        }

        private void FloorB16_Click(object sender, EventArgs e)
        {

        }

        private void WallTop14_Click(object sender, EventArgs e)
        {

        }

       
    }
}
