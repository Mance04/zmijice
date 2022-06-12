using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zmijice
{
    public partial class Form1 : Form
    {
        private List<Krug> zmija = new List<Krug>();
        private Krug hrana = new Krug();
        int maxSirina;
        int maxVisina;
        int rezultat;
        int maxRezultat;
        Random r = new Random();
        bool levo, desno, gore, dole;
        public Form1()
        {
            InitializeComponent();
            new Podesavanja();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Restartuj();
            if (radioButton1.Checked)
            {
                timer1.Interval = 120;
            }
            if (radioButton2.Checked)
            {
                timer1.Interval = 80;
            }
            if (radioButton3.Checked)
            {
                timer1.Interval = 40;
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left && Podesavanja.smer!="desno")
            {
                levo = true;
            }
            if (e.KeyCode == Keys.Right && Podesavanja.smer != "levo")
            {
                desno = true;
            }
            if (e.KeyCode == Keys.Up && Podesavanja.smer != "dole")
            {
                gore = true;
            }
            if (e.KeyCode == Keys.Down && Podesavanja.smer != "gore")
            {
                dole = true;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                levo = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                desno = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                gore = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                dole = false;
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush boja;

            for(int i=0;i<zmija.Count;i++)
            {
                if(i==0)
                {
                    boja = Brushes.DarkBlue;
                }
                else
                {
                    boja = Brushes.LightBlue;
                }

                g.FillEllipse(boja, new Rectangle(zmija[i].X * Podesavanja.sirina, zmija[i].Y * Podesavanja.visina,Podesavanja.sirina,Podesavanja.visina));
            }
            g.FillEllipse(Brushes.Red, new Rectangle(hrana.X * Podesavanja.sirina, hrana.Y * Podesavanja.visina, Podesavanja.sirina, Podesavanja.visina));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(levo)
            {
                Podesavanja.smer = "levo";
            }
            if(desno)
            {
                Podesavanja.smer = "desno";
            }
            if(gore)
            {
                Podesavanja.smer = "gore";
            }
            if(dole)
            {
                Podesavanja.smer = "dole";
            }
            for(int i=zmija.Count-1;i>=0;i--)
            {
                if(i==0)
                {
                    switch(Podesavanja.smer)
                    {
                        case "levo":
                            zmija[i].X--;
                            break;
                        case "desno":
                            zmija[i].X++;
                            break;
                        case "gore":
                            zmija[i].Y--;
                            break;
                        case "dole":
                            zmija[i].Y++;
                            break;

                    }
                    /*if(zmija[i].X<0)
                    {
                        zmija[i].X = maxSirina;
                    }
                    if (zmija[i].X > maxSirina)
                    {
                        zmija[i].X = 0;
                    }
                    if (zmija[i].Y < 0)
                    {
                        zmija[i].Y = maxVisina;
                    }
                    if (zmija[i].Y > maxVisina)
                    {
                        zmija[i].Y = 0;
                    }*/
                    if(zmija[i].X==hrana.X&&zmija[i].Y==hrana.Y)
                    {
                        Hrana();
                    }
                    if (zmija[i].X == maxSirina+1 || zmija[i].X == 0||zmija[i].Y==maxVisina+1||zmija[i].Y==0)
                    {
                        Kraj();
                    }
                    for (int j=1;j<zmija.Count;j++)
                    {
                        if(zmija[i].X==zmija[j].X&&zmija[i].Y==zmija[j].Y)
                        {
                            Kraj();
                        }
                    }
                    
                }
                else
                {
                    zmija[i].X = zmija[i - 1].X;
                    zmija[i].Y = zmija[i - 1].Y;
                }
            }
            pictureBox1.Invalidate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void Restartuj()
        {
            maxSirina = pictureBox1.Width / Podesavanja.sirina - 1;
            maxVisina = pictureBox1.Height / Podesavanja.visina - 1;
            zmija.Clear();
            button1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            label1.Text = "Rezultat: " + rezultat;
            label4.Text = "";
            Krug glava = new Krug { X = 10, Y = 5 };
            zmija.Add(glava);
            
            for (int i=0;i<10;i++)
            {
                Krug telo = new Krug();
                zmija.Add(telo);
            }

            hrana = new Krug { X = r.Next(2, maxSirina), Y = r.Next(2, maxVisina) };

            timer1.Start();
        }
        private void Hrana()
        {
            rezultat++;

            label1.Text = "Rezultat: " + rezultat;
            Krug telo = new Krug { X = zmija[zmija.Count - 1].X, Y = zmija[zmija.Count-1].Y };
            zmija.Add(telo);
            hrana = new Krug { X = r.Next(2, maxSirina), Y = r.Next(2, maxVisina) };
        }
        public void Kraj()
        {
            timer1.Stop();
            button1.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;

            if (rezultat>maxRezultat)
            {
                maxRezultat = rezultat;
                label2.Text = "Najbolji rezultat: " + maxRezultat;
            }
            label4.Text = "Na žalost izgubili ste.";
        }
    }
}
