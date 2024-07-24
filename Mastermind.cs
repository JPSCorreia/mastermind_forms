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
    // lógica do jogo
    public partial class Mastermind : Form
    {

        private int[] secretCode; // código secreto.
        private int attempts; // numero de tentativas feitas.
        private bool winner; // se o jogador é vencedor.
        private List<ColoredBall> addedBalls; // lista das bolas clickadas pelo jogador.
        private int rowPosition = 450; // linha da tentativa correspondente.

        // constructor que inicializa um novo jogo.
        public Mastermind()
        {
            this.BackColor = ColorTranslator.FromHtml("#121212"); // muda cor de fundo
            InitializeComponent();
            StartNewGame();
        }

        // função que gera um array com 4 numeros aleatórios de 1 a 6 (sem repetição) para ser usado no código secreto.
        private static int[] GenerateSecretCode()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            Random random = new Random(); // nova instancia de Random.
            int[] randomArray = numbers.OrderBy(x => random.Next()).Take(4).ToArray();
            return randomArray;
        }

        // função que começa um jogo novo.
        private async void StartNewGame()
        {
            secretCode = GenerateSecretCode();
            attempts = 0;
            winner = false;
            // MessageBox.Show("Starting new game.");
            GenerateColoredBallsControls();
            GenerateColoredBallsRow(50);
            await StartNewTurn();

        }

        // função assincrona que inicia um novo turno.
        private async Task StartNewTurn()
        {
            addedBalls = new List<ColoredBall>();

            // Application.DoEvents(); // processar eventos pendentes
            // continuar enquanto não houver vencedor e ainda haver tentativas.
            while (!winner && attempts < 10)
            {

                if (addedBalls.Count == 4)
                {
                    CheckGuess();
                    if (!winner)
                    {
                        attempts++;
                        rowPosition -= 50;
                        await StartNewTurn();

                    }
                }
                await Task.Delay(100); // aguardar um curto período para permitir actualizações.
            }
        }


        // função para verificar se a tentativa do jogador foi correcta.
        private void CheckGuess()
        {
            bool correctGuess = false;
            // comparar seleção do utilizador com o codigo secreto.
            for (int i = 0; i < 4; i++)
            {
                if (GetNumberFromColor(addedBalls[i].BackColor) != secretCode[i])
                {
                    correctGuess = false;
                    break;
                }
            }
            // acabar o jogo se o utilizador for o vencedor ou se ultrapassar o numero máximo de tentativas.
            if (correctGuess)
            {
                winner = true;
                MessageBox.Show("Parabéns! É o vencedor!");
            }
            else if (attempts >= 9)
            {
                MessageBox.Show("Alcançou o número máximo de tentativas. Game Over!");
            }
        }

        // função que gera uma linha de bolas (com o código secreto de momento).
        private void GenerateColoredBallsRow(int positionY)
        {
            for (int i = 0; i <= 3; i++)
            {
                // Criar nova bola com cor random.
                ColoredBall ball = new ColoredBall(secretCode[i], false, this); 
                ball.Location = new Point(100 + i * 50, positionY);
                this.Controls.Add(ball);
            }
        }

        // função que gera os controles.
        private void GenerateColoredBallsControls()
        {
            for (int i = 1; i <= 6; i++)
            {
                ColoredBall ball = new ColoredBall(i, true, this);
                ball.Location = new Point(400, 350 + i * 50);
                this.Controls.Add(ball);
            }
        }

        // função que adiciona uma nova instancia da classe ColoredBall ao jogo. 
        public void AddColoredBall(Color color)
        {
            if (addedBalls.Count < 4)
            {
                ColoredBall newBall = new ColoredBall(GetNumberFromColor(color), false, this);
                int xOffset = 100 + addedBalls.Count * 50;
                newBall.Location = new Point(xOffset, rowPosition + 100);
                addedBalls.Add(newBall);
                this.Controls.Add(newBall);
            }
        }


        // função que devolve um numero associado a uma cor.
        public int GetNumberFromColor(Color color)
        {
            if (color == Color.Red) return 1;
            if (color == Color.Green) return 2;
            if (color == Color.Blue) return 3;
            if (color == Color.Yellow) return 4;
            if (color == Color.Purple) return 5;
            if (color == Color.Orange) return 6;
            return 0;
        }

        // função que devolve uma cor associado a um numero.
        public Color GetColorFromNumber(int number)
        {
            if (number == 1) return Color.Red;
            if (number == 2) return Color.Green;
            if (number == 3) return Color.Blue;
            if (number == 4) return Color.Yellow;
            if (number == 5) return Color.Purple;
            if (number == 6) return Color.Orange;
            return Color.Black;
        }



    }


}
