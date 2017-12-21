using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace chicken_game
{
    public partial class Form1 : Form
    {
        //sounds
        WMPLib.WindowsMediaPlayer highway = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer buck1 = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer buck2 = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer buck3 = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer buck4 = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer dead = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer point = new WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer gameover = new WindowsMediaPlayer();

        //random number object
        Random rnd = new Random();

        //default values for lives and score
        int lives = 3;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
            highway.URL = "highway.wav";
            highway.settings.setMode("loop", true);
            buck1.URL = "chicken_buck1.wav";
            buck1.controls.stop();
            buck2.URL = "chicken_buck2.wav";
            buck2.controls.stop();
            buck3.URL = "chicken_buck3.wav";
            buck3.controls.stop();
            buck4.URL = "chicken_buck4.wav";
            buck4.controls.stop();
            dead.URL = "chicken_dead.wav";
            dead.controls.stop();
            point.URL = "Coin_FX04.wav";
            point.controls.stop();
            gameover.URL = "Sad_Arp01.wav";
            gameover.controls.stop();

            //x,y coordinates
            int xpos = 0;
            int ypos = 0;

            //Left cars initial position
            xpos = randomX();
            ypos = randomY();
            RCL.Location = new Point(xpos, ypos);
            xpos = randomX();
            ypos = randomY();
            OCL.Location = new Point(xpos, ypos);
            xpos = randomX();
            ypos = randomY();
            GCL.Location = new Point(xpos, ypos);
            xpos = randomX();
            ypos = randomY();
            BCL.Location = new Point(xpos, ypos);
            xpos = randomX();
            ypos = randomY();
            PCL.Location = new Point(xpos, ypos);
            xpos = randomX();
            ypos = randomY();
            WCL.Location = new Point(xpos, ypos);

            //Right cars initial position
            xpos = randomXR();
            ypos = randomYR();
            RCR.Location = new Point(xpos, ypos);
            xpos = randomXR();
            ypos = randomYR();
            OCR.Location = new Point(xpos, ypos);
            xpos = randomXR();
            ypos = randomYR();
            GCR.Location = new Point(xpos, ypos);
            xpos = randomXR();
            ypos = randomYR();
            BCR.Location = new Point(xpos, ypos);
            xpos = randomXR();
            ypos = randomYR();
            PCR.Location = new Point(xpos, ypos);
            xpos = randomXR();
            ypos = randomYR();
            WCR.Location = new Point(xpos, ypos);
        }

        //moves cars
        private void timer1_Tick(object sender, EventArgs e)
        {
            //move left facing cars left
            RCL.Left += -5;
            OCL.Left += -2;
            GCL.Left += -3;
            BCL.Left += -4;
            PCL.Left += -1;
            WCL.Left += -6;

            //move right facing cars right
            RCR.Left += 3;
            OCR.Left += 1;
            GCR.Left += 6;
            BCR.Left += 4;
            PCR.Left += 5;
            WCR.Left += 2;

            //checks if new cars need to be generated
            generateCars();

            //checks if chicken is hit by car
            collision();
        }

        //chicken controls
        private void moveChicken(object sender, KeyEventArgs e)
        {
            int xpos;

            if (e.KeyCode == Keys.Left)
            {
                if (pbchickenleft.Location.X <= 5)
                    pbchickenleft.Left += 0;
                else
                {
                    chickenCluck();
                    pbchickenright.Visible = false;
                    pbchickenleft.Visible = true;
                    pbchickenright.Left += -75;
                    pbchickenleft.Location = pbchickenright.Location;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (pbchickenleft.Location.X >= 680)
                    pbchickenleft.Left += 0;
                else
                {
                    chickenCluck();
                    pbchickenleft.Visible = false;
                    pbchickenright.Visible = true;
                    pbchickenleft.Left += 75;
                    pbchickenright.Location = pbchickenleft.Location;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (pbchickenleft.Location.Y <= 0 || pbchickenright.Location.Y <= 0)
                {
                    pbchickenleft.Top += 0;
                    pbchickenright.Top += 0;
                }
                else
                {
                    chickenCluck();
                    pbchickenleft.Top += -75;
                    pbchickenright.Top += -75;
                    xpos = pbchickenleft.Location.X;

                    if (pbchickenleft.Top == 12 || pbchickenright.Top == 12)
                    {
                        point.controls.play();
                        pbchickenleft.Location = new Point(xpos, 537);
                        pbchickenright.Location = new Point(xpos, 537);
                        score++;
                        lblscore.Text = score.ToString();
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (pbchickenleft.Location.Y >= 537 || pbchickenright.Location.Y >= 537)
                {
                    pbchickenleft.Top += 0;
                    pbchickenright.Top += 0;
                }
                else
                {
                    chickenCluck();
                    pbchickenleft.Top += 75;
                    pbchickenright.Top += 75;
                }
            }
        }

        //random chicken cluck
        private void chickenCluck()
        {
            int i = rnd.Next(1, 5);

            if (i == 1)
                buck1.controls.play();
            else if (i == 2)
                buck2.controls.play();
            else if (i == 3)
                buck3.controls.play();
            else if (i == 4)
                buck4.controls.play();
        }

        //generates cars from random positions
        private void generateCars()
        {
            int xpos = 0;
            int ypos = 0;

            //left cars
            if (RCL.Right <= 0)
            {
                xpos = randomX();
                ypos = randomY();
                RCL.Location = new Point(xpos, ypos);
            }
            else if (OCL.Right <= 0)
            {
                xpos = randomX();
                ypos = randomY();
                OCL.Location = new Point(xpos, ypos);
            }
            else if (GCL.Right <= 0)
            {
                xpos = randomX();
                ypos = randomY();
                GCL.Location = new Point(xpos, ypos);
            }
            else if (BCL.Right <= 0)
            {
                xpos = randomX();
                ypos = randomY();
                BCL.Location = new Point(xpos, ypos);
            }
            else if (PCL.Right <= 0)
            {
                xpos = randomX();
                ypos = randomY();
                PCL.Location = new Point(xpos, ypos);
            }
            if (WCL.Right <= 0)
            {
                xpos = randomX();
                ypos = randomY();
                WCL.Location = new Point(xpos, ypos);
            }

            //right cars
            else if (RCR.Left >= 750)
            {
                xpos = randomXR();
                ypos = randomYR();
                RCR.Location = new Point(xpos, ypos);
            }
            else if (OCR.Left >= 750)
            {
                xpos = randomXR();
                ypos = randomYR();
                OCR.Location = new Point(xpos, ypos);
            }
            else if (GCR.Left >= 750)
            {
                xpos = randomXR();
                ypos = randomYR();
                GCR.Location = new Point(xpos, ypos);
            }
            else if (BCR.Left >= 750)
            {
                xpos = randomXR();
                ypos = randomYR();
                BCR.Location = new Point(xpos, ypos);
            }
            else if (PCR.Left >= 750)
            {
                xpos = randomXR();
                ypos = randomYR();
                PCR.Location = new Point(xpos, ypos);
            }
            else if (WCR.Left >= 750)
            {
                xpos = randomXR();
                ypos = randomYR();
                WCR.Location = new Point(xpos, ypos);
            }
        }

        //detects if chicken is hit by car
        private void collision()
        {
            if (pbchickenleft.Bounds.IntersectsWith(RCL.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(OCL.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(GCL.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(BCL.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(PCL.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(WCL.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(RCR.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(OCR.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(GCR.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(BCR.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(PCR.Bounds))
                lifeLost();
            else if (pbchickenleft.Bounds.IntersectsWith(WCR.Bounds))
                lifeLost();
        }

        //removes lives when hit by car
        private void lifeLost()
        {
            dead.controls.stop();
            dead.controls.play();
            lives--;

            if (lives == 2)
            {
                pbchickenleft.Location = new Point(352, 537);
                pbchickenright.Location = new Point(352, 537);
                life2.Visible = false;
            }
            else if (lives == 1)
            {
                pbchickenleft.Location = new Point(352, 537);
                pbchickenright.Location = new Point(352, 537);
                life3.Visible = false;
            }
            else if (lives == 0)
                gameOver();
        }

        //stop gameplay, display messagebox with score and reset 
        private void gameOver()
        {
            gameover.controls.play();
            timer1.Enabled = false;
            highway.controls.pause();
            MessageBox.Show("Your Score = " + score + " Click OK to play Again");
            highway.controls.play();
            timer1.Enabled = true;
            life2.Visible = true;
            life3.Visible = true;
            pbchickenleft.Location = new Point(352, 537);
            pbchickenright.Location = new Point(352, 537);
            lblscore.Text = "0";
            lives = 3;
            score = 0;
        }

        //random x start point for new leftward cars
        private int randomX()
        {
            int xpos = rnd.Next(760, 1160);
            return xpos;
        }

        //random y start point for new leftward cars
        private int randomY()
        {
            int yposn = rnd.Next(1, 4);
            int ypos = 0;

            if (yposn == 1)
                ypos = 463;
            else if (yposn == 2)
                ypos = 387;
            else if (yposn == 3)
                ypos = 314;

            return ypos;
        }

        //random x start point for new rightward cars
        private int randomXR()
        {
            int xpos = rnd.Next(-500, -100);
            return xpos;
        }

        //random y start point for new rightward cars
        private int randomYR()
        {
            int yposn = rnd.Next(1, 4);
            int ypos = 0;

            if (yposn == 1)
                ypos = 236;
            else if (yposn == 2)
                ypos = 163;
            else if (yposn == 3)
                ypos = 87;

            return ypos;
        }
    }
}