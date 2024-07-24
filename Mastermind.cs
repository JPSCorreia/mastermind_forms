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

        private Color[] secretCode; // código secreto.
        private int attempts; // numero de tentativas feitas.
        public bool gameover; // se o jogo acabou.
        private List<ColoredBall> addedBalls; // lista das bolas clickadas pelo jogador.
        private int rowPosition; // linha da tentativa correspondente.

        // constructor que inicializa um novo jogo.
        public Mastermind()
        {
            this.BackColor = ColorTranslator.FromHtml("#121212"); // muda cor de fundo
            InitializeComponent();
            StartNewGame();
        }

        // função que gera um array com 4 cores aleatórias para ser usado como código secreto.
        private static Color[] GenerateSecretCode()
        {
                List<Color> colors = new List<Color> 
                { 
                    Color.Red, 
                    Color.Green, 
                    Color.Blue, 
                    Color.Yellow, 
                    Color.Purple, 
                    Color.Orange 
                };
            Random random = new Random(); // nova instancia de Random.
            Color[] randomArray = colors.OrderBy(x => random.Next()).Take(4).ToArray(); // baralhar a lista de cores e selecionar as primeiras 4
            return randomArray;
        }

        // função que começa um jogo novo.
        private async void StartNewGame()
        {
            RemoveAllColoredBalls();
            secretCode = GenerateSecretCode();
            attempts = 0;
            gameover = false;
            UpdateAttemptsLabel();
            rowPosition = 450;
            GenerateColoredBallsControls(); // mostrar controles.
            // GenerateColoredBallsSolution(); // mostrar solução no ecran para debugging.
            await StartNewTurn();
        }

        // função que inicia um novo turno.
        private async Task StartNewTurn()
        {
            addedBalls = new List<ColoredBall>();

            while (!gameover)
            {

                if (addedBalls.Count == 4)
                {
                    attempts++;
                    UpdateAttemptsLabel();
                    GenerateFeedbackBalls();
                    CheckGuess();
                    if (!gameover)
                    {
                        rowPosition -= 50;
                        await StartNewTurn();
                    }
                }
                await Task.Delay(10); // aguardar um curto período para permitir actualizações.
            }
        }

        // função que remove todas as bolas do ecran.
        private void RemoveAllColoredBalls()
        {
            var controlsToRemove = this.Controls.OfType<ColoredBall>().ToList();
            var othersToRemove = this.Controls.OfType<FeedbackBall>().ToList();


            foreach (var ball in controlsToRemove)
            {
                this.Controls.Remove(ball);
                ball.Dispose();
            }
            foreach (var ball in othersToRemove)
            {
                this.Controls.Remove(ball);
                ball.Dispose();
            }
        }

        // função para verificar se a tentativa do jogador foi correcta.
        private void CheckGuess()
        {
            Color[] choiceArray = { addedBalls[0].BallColor, addedBalls[1].BallColor, addedBalls[2].BallColor , addedBalls[3].BallColor };

            if (choiceArray[0] == secretCode[0] && choiceArray[1] == secretCode[1] && choiceArray[2] == secretCode[2] && choiceArray[3] == secretCode[3])
            {
                GameOver();
                MessageBox.Show("Ganhou! Acertou no código secreto.", "Game Over!");
            }
            else if (attempts >= 10)
            {
                GameOver();
                MessageBox.Show("Perdeu! Alcançou o número máximo de tentativas.", "Game Over!");
            }
        }

        // função que gera a linha de bolas com a solução.
        private void GenerateColoredBallsSolution()
        {
            for (int i = 0; i <= 3; i++)
            {
                ColoredBall ball = new ColoredBall(secretCode[i], false, this); 
                ball.Location = new Point(100 + i * 50, 30);
                this.Controls.Add(ball);
            }
        }

        // função que gera os controles.
        private void GenerateColoredBallsControls()
        {
            List<Color> colors = new List<Color>
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Purple,
                Color.Orange
            };

            for (int i = 0; i <= 5; i++)
            {
                ColoredBall ball = new ColoredBall(colors[i], true, this);
                ball.Location = new Point(100 + i * 50, 647);
                this.Controls.Add(ball);
            }
        }

        // função que adiciona uma nova ColoredBall ao jogo. 
        public void AddColoredBall(Color color)
        {
            if ((addedBalls.Count < 4) && (gameover == false))
            {
                ColoredBall newBall = new ColoredBall(color, false, this, addedBalls.Count + 1);
                int xOffset = 100 + addedBalls.Count * 50;
                newBall.Location = new Point(xOffset, rowPosition + 100);
                addedBalls.Add(newBall);
                this.Controls.Add(newBall);
            }
        }

        // função que gera uma linha de bolas de feedback.
        private void GenerateFeedbackBalls()
        {
            
            int[] feedback = GetFeedback(); // obter o feedback da linha actual.
            int feedbackStartX = 300; // posição inicial da primeira bola de feedback
            int feedbackSpacing = 15; // espaçamento entre bolas de feedback

            int redCount = feedback.Count(f => f == 2); // contar bolas vermelhas.
            int whiteCount = feedback.Count(f => f == 1); // contar bolas brancas.

            // criar bolas de feedback vermelhas
            for (int i = 0; i < redCount; i++)
            {
                FeedbackBall newBall = new FeedbackBall(Color.Red, this);
                newBall.Location = new Point(feedbackStartX + i * feedbackSpacing, rowPosition + 115);
                this.Controls.Add(newBall);
            }

            // criar bolas de feedback brancas
            for (int i = 0; i < whiteCount; i++)
            {
                FeedbackBall newBall = new FeedbackBall(Color.White, this);
                newBall.Location = new Point(feedbackStartX + (redCount + i) * feedbackSpacing, rowPosition + 115);
                this.Controls.Add(newBall);
            }
        }

        // função que termina o jogo.
        private void GameOver()
        {
            GenerateColoredBallsSolution();
            gameover = true;
        }

        // função que devolve o feedback sobre a linha jogada.
        private int[] GetFeedback()
        {
            int[] feedback = new int[4]; // 0 = não correto, 1 = cor correta, 2 = cor e posição correctas.
            Color[] choiceArray = { addedBalls[0].BallColor, addedBalls[1].BallColor, addedBalls[2].BallColor, addedBalls[3].BallColor };
            bool[] usedInSecretCode = new bool[4];
            bool[] usedInGuess = new bool[4];

            for (int i = 0; i < 4; i++)
            {
                if (choiceArray[i] == secretCode[i])
                {
                    feedback[i] = 2;
                    usedInSecretCode[i] = true;
                    usedInGuess[i] = true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (feedback[i] != 2)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (!usedInGuess[j] && choiceArray[i] == secretCode[j] && !usedInSecretCode[j])
                        {
                            feedback[i] = 1;
                            usedInSecretCode[j] = true;
                            break;
                        }
                    }
                }
            }
            return feedback;
        }

        // função que faz update á label de tentativas.
        private void UpdateAttemptsLabel()
        {
            lblAttempts.Text = $"Tentativas: {attempts}";
        }

    }

}
