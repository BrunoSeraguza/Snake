using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    internal class Game
    {
        public Keys direction { get; set; }
        public Keys arrow { get; set; }

        private Timer frame { get; set; }
        private Label LbPontuacao { get; set; }
        private Panel PnTela { get; set; }

        private int pontos  = 0;
        private Food Food;
        private Snake Snake;
        private Bitmap offScreen;
        private Graphics bitMapGraph;
        private Graphics screenGraph;

        public Game(ref Timer timer, ref Label label, ref Panel panel)
        {
            PnTela = panel;
            frame =  timer;
            LbPontuacao = label;
            offScreen = new Bitmap(428, 428);
            Snake = new Snake();
            Food = new Food();
            direction = Keys.Left;
            arrow = direction;
        }

        public void StartGame() 
        {
            Snake.Reset();
            Food.CreateFood();
            direction= Keys.Left;
            bitMapGraph = Graphics.FromImage(offScreen);
            screenGraph = PnTela.CreateGraphics();
            frame.Enabled = true;
        }

        public void tick()
        {
            if( ((arrow == Keys.Left) && (direction != Keys.Right)) ||
            ((arrow == Keys.Right) && (direction != Keys.Left)) ||
            ((arrow == Keys.Up) && (direction != Keys.Down)) ||
            ((arrow == Keys.Down) && (direction != Keys.Up)) )
            {
                direction = arrow;
            }
            switch(direction) 
            {
                case Keys.Left:
                    Snake.Left();
                    break;
                case Keys.Right:
                    Snake.Right();
                    break;
                case Keys.Up:
                    Snake.Up();
                    break;
                case Keys.Down:
                    Snake.Down();
                    break;                    
            }

            bitMapGraph.Clear(Color.White);
            bitMapGraph.DrawImage(Properties.Resources.raspberry_icon, (Food.Location.X * 15), (Food.Location.Y * 15) ,15,15);
            bool gameOver = false;

            for (int i = 0; i < Snake.Lenght; i++)
            {
                if(i == 0)
                {
                    bitMapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);

                }
                else 
                {
                    bitMapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#006400")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }

                if ((Snake.Location[i] == Snake.Location[0]) && ( i > 0))
                {
                    gameOver = true;
                }

            }
            screenGraph.DrawImage(offScreen,0,0);
            CheckCollision();
            if (gameOver)
            {
                GameOver();
            }
        }

        public void CheckCollision()
        {
            if (Snake.Location[0] == Food.Location )
            {
                Snake.Eat();
                Food.CreateFood();
                pontos += 9;
                LbPontuacao.Text = "PONTOS: " + pontos;
            }
        }
        public void GameOver()
        {
            frame.Enabled= false;
            bitMapGraph.Dispose();
            screenGraph.Dispose();
            LbPontuacao.Text = "PONTOS: 0 ";
            pontos= 0;
            MessageBox.Show("Fim de jogo!");
        }


    }
}
