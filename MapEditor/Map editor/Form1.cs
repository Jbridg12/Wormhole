﻿using System;
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
        enum Wall { Door, Wall }; //change the wall mode
        enum floor { O, Player, Enemy1, Enemy2, Enemy3, Wall };//change the floor mode

        string[,][] rooms = new string[,][25]; // array of saved room strings Added by Josh

        int currentIndex = 12; // Added by Josh

        Dictionary<char, int> floorX;
        //int rows; //the rows of the map
        //int cols; //the columns of the map
        string[,] form = new string[12, 20]; // Record the input of the rows and columns of the map , the map size is 12 X 20

        //Wall myWall;

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

            form[0, 1] = "W";
            form[0, 2] = "W";
            form[0, 3] = "W";
            form[0, 4] = "d";//Door
            form[0, 5] = "D";//Door
            form[0, 6] = "W";
            form[0, 7] = "W";
            form[0, 8] = "W";
            form[0, 9] = "W";
            form[0, 10] = "W";
            form[0, 11] = "W";
            form[0, 12] = "W";
            form[0, 13] = "W";
            form[0, 14] = "W";
            form[0, 15] = "W";
            form[0, 16] = "W";
            form[0, 17] = "W";
            form[0, 18] = "W";

            //Bottom wall
            for (int i = 1; i < 19; i++)
            {
                form[11, i] = "W";
            }


            //Left wall
            for (int i = 1; i < 11; i++)
            {
                form[i, 0] = "W";
            }

            //Right wall
            for (int i = 1; i < 11; i++)
            {
                form[i, 19] = "W";
            }

            //Floor A
            for (int i = 1; i < 19; i++)
            {
                form[1, i] = "O";
            }
            //Floor B
            form[2, 1] = "O";
            form[2, 2] = "O";
            form[2, 3] = "O";
            form[2, 4] = "O";
            form[2, 5] = "O";
            form[2, 6] = "E3";
            form[2, 7] = "O";
            form[2, 8] = "O";
            form[2, 9] = "O";
            form[2, 10] = "O";
            form[2, 11] = "O";
            form[2, 12] = "O";
            form[2, 13] = "O";
            form[2, 14] = "O";
            form[2, 15] = "O";
            form[2, 16] = "O";
            form[2, 17] = "O";
            form[2, 18] = "O";
            //Floor C
            form[3, 1] = "O";
            form[3, 2] = "E2";
            form[3, 3] = "O";
            form[3, 4] = "O";
            form[3, 5] = "O";
            form[3, 6] = "O";
            form[3, 7] = "O";
            form[3, 8] = "O";
            form[3, 9] = "O";
            form[3, 10] = "O";
            form[3, 11] = "E1";
            form[3, 12] = "O";
            form[3, 13] = "O";
            form[3, 14] = "O";
            form[3, 15] = "O";
            form[3, 16] = "O";
            form[3, 17] = "O";
            form[3, 18] = "O";
            //Floor D
            for (int i = 1; i < 19; i++)
            {
                form[4, i] = "O";
            }

            //Floor E
            form[5, 1] = "O";
            form[5, 2] = "O";
            form[5, 3] = "W";
            form[5, 4] = "W";
            form[5, 5] = "O";
            form[5, 6] = "O";
            form[5, 7] = "O";
            form[5, 8] = "O";
            form[5, 9] = "O";
            form[5, 10] = "O";
            form[5, 11] = "O";
            form[5, 12] = "O";
            form[5, 13] = "O";
            form[5, 14] = "O";
            form[5, 15] = "O";
            form[5, 16] = "O";
            form[5, 17] = "O";
            form[5, 18] = "O";
            //Floor F
            form[6, 1] = "O";
            form[6, 2] = "O";
            form[6, 3] = "W";
            form[6, 4] = "W";
            form[6, 5] = "O";
            form[6, 6] = "O";
            form[6, 7] = "O";
            form[6, 8] = "O";
            form[6, 9] = "O";
            form[6, 10] = "O";
            form[6, 11] = "O";
            form[6, 12] = "O";
            form[6, 13] = "O";
            form[6, 14] = "O";
            form[6, 15] = "O";
            form[6, 16] = "O";
            form[6, 17] = "O";
            form[6, 18] = "O";
            //Floor G
            for (int i = 1; i < 19; i++)
            {
                form[7, i] = "O";
            }
            //Floor H
            for (int i = 1; i < 19; i++)
            {
                form[8, i] = "O";
            }

            //Floor I
            form[9, 1] = "O";
            form[9, 2] = "O";
            form[9, 3] = "O";
            form[9, 4] = "O";
            form[9, 5] = "O";
            form[9, 6] = "O";
            form[9, 7] = "O";
            form[9, 8] = "P";
            form[9, 9] = "O";
            form[9, 10] = "O";
            form[9, 11] = "O";
            form[9, 12] = "O";
            form[9, 13] = "O";
            form[9, 14] = "O";
            form[9, 15] = "O";
            form[9, 16] = "O";
            form[9, 17] = "O";
            form[9, 18] = "O";

            //Floor J
            for (int i = 1; i < 19; i++)
            {
                form[10, i] = "O";
            }
            ////Floor K
            //form[11, 1] = "O";
            //form[11, 2] = "O";
            //form[11, 3] = "O";
            //form[11, 4] = "O";
            //form[11, 5] = "O";
            //form[11, 6] = "O";
            //form[11, 7] = "P";
            //form[11, 8] = "O";
            //form[11, 9] = "O";
            //form[11, 10] = "O";
            //form[11, 11] = "O";
            //form[11, 12] = "O";
            //form[11, 13] = "O";

            ////Floor L
            //for (int i = 1; i < 14; i++)
            //{
            //    form[12, i] = "O";
            //}
            ////Floor M
            //for (int i = 1; i < 14; i++)
            //{
            //    form[13, i] = "O";
            //}
        }

        //Create Map !
        private void Create_Click(object sender, EventArgs e)
        {
            //From HW3
            //create the text file
            // Dump out the array to a text file named Map.txt. 
            try
            {
                StreamWriter output = new StreamWriter("Map.txt");
                string room = "";
                // write out a comma delimited lines of text
                for (int i = 0; i < 12; i++)
                {
                    // which Line
                    output.Write("L"+(i+1)+"{");

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
                    output.Write("},");
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


        //// Wall click method
        //public void WallClick()
        //{
        //    //Check the Text
        //    switch (WallTop1.Text)
        //    {
        //        case "D":

        //            WallTop1.BackColor = Color.Lime; //Wall color
        //            WallTop1.Text = "W"; //Wall                    
        //            break;

        //        case "W":

        //            WallTop1.BackColor = Color.Aqua; //Door color
        //            WallTop1.Text = "D"; //Door
        //            break;

        //    }
        //}


        ////Check floor block status 
        //public void CheckFloorBlock()
        //{
        //    switch (FloorA3.Text)
        //    {
        //        case "O":
        //            FloorA3.BackColor = Color.Yellow; //Player color
        //            FloorA3.Text = "P"; //Player
        //            break;
        //        case "P":
        //            FloorA3.BackColor = Color.Red; //E1 color
        //            FloorA3.Text = "E1"; //Enemy1                   
        //            break;
        //        case "E1":
        //            FloorA3.BackColor = Color.Fuchsia; //E2 color
        //            FloorA3.Text = "E2"; //Enemy2 
        //            break;
        //        case "E2":
        //            FloorA3.BackColor = Color.SlateBlue; //E3 color
        //            FloorA3.Text = "E3"; //Enemy3
        //            break;
        //        case "E3":
        //            FloorA3.BackColor = Color.Lime; //Wall color
        //            FloorA3.Text = "W"; //Wall
        //            break;
        //        case "W":
        //            FloorA3.BackColor = Color.Snow; //blank color
        //            FloorA3.Text = "O"; //blank                   
        //            break;
        //    }
        //}


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
                    pressed.Text = "W"; //Wall 
                    form[0, x] = "W";
                    break;

                case "W":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door1                   
                    form[0, x] = "D";
                    break;

            }
        }
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
                    pressed.Text = "W"; //Wall 
                    form[11, x] = "W";
                    break;

                case "W":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door 1                   
                    form[11, x] = "D";
                    break;

            }
        }

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
                    pressed.Text = "W"; //Wall 
                    form[0, y] = "W";
                    break;

                case "W":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door 1                 
                    form[0, y] = "D";
                    break;

            }
        }

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
                    pressed.Text = "W"; //Wall 
                    form[11, y] = "W";
                    break;

                case "W":

                    pressed.BackColor = Color.Aqua; //Door color
                    pressed.Text = "D"; //Door 1                  
                    form[11, y] = "D";
                    break;

            }
        }

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
                case "O":
                    pressed.BackColor = Color.Yellow; //Player color
                    pressed.Text = "P"; //Player

                    form[floorX[x], y] = "P";
                    break;
                case "P":
                    pressed.BackColor = Color.Red; //E1 color
                    pressed.Text = "E1"; //Enemy1                   
                    form[floorX[x], y] = "E1";
                    break;
                case "E1":
                    pressed.BackColor = Color.Fuchsia; //E2 color
                    pressed.Text = "E2"; //Enemy2 
                    form[floorX[x], y] = "E2";
                    break;
                case "E2":
                    pressed.BackColor = Color.SlateBlue; //E3 color
                    pressed.Text = "E3"; //Enemy3
                    form[floorX[x], y] = "E3";
                    break;
                case "E3":
                    pressed.BackColor = Color.Lime; //Wall color
                    pressed.Text = "W"; //Wall
                    form[floorX[x], y] = "W";
                    break;
                case "W":
                    pressed.BackColor = Color.Snow; //blank color
                    pressed.Text = "O"; //blank                   
                    form[floorX[x], y] = "O";
                    break;
            }
        }

        private void Save_Room(object sender, EventArgs e)  //Added by Josh
        {
            rooms[currentIndex] = form;
        }

        private void Write(object sender, EventArgs e)  // Added by Josh
        {
            foreach (string str in rooms)
            {
                
            }
        }

        private void ChangeRoom(object sender, EventArgs e) // Added by Josh
        {
            currentIndex = Int32.Parse(sender.Name);
            Form1_Load();
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
