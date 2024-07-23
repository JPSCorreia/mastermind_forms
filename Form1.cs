using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mastermind
{
    public partial class Mastermind : Form
    {
        private int[] secretCode; // código secreto.
        private int attempts; // numero de tentativas feitas.
        private bool winner; // se o jogador ganhou ou não.

        public Mastermind()
        {
            InitializeComponent();
            StartNewGame();
        }

        // função que devolve um array com 4 numeros random de 1 a 6 (sem repetição) para ser usado no código secreto.
        private static int[] GenerateSecretCode()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            Random random = new Random();
            int[] randomArray = numbers.OrderBy(x => random.Next()).Take(4).ToArray();
            return randomArray;
        }

        // função que começa um jogo novo.
        private void StartNewGame()
        {
            secretCode = GenerateSecretCode();
            attempts = 0;
            winner = false;
            GenerateColoredBallsRow(50);
            GenerateColoredBallsRow(100);
            GenerateColoredBallsRow(150);

        }


        // função que gera uma linha de bolas (com o código secreto de momento).
        private void GenerateColoredBallsRow(int positionY )
        {
            for (int i = 0; i <= 3; i++)
            {
                // Criar nova bola com cor random.
                ColoredBall ball = new ColoredBall(secretCode[i]); 
                ball.Location = new Point(50 + i * 40, positionY);
                this.Controls.Add(ball);
            }
        }

    }
}
