using System.Windows.Forms;
using System.Drawing;

namespace mastermind
{
    partial class Mastermind
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartNewGame = new System.Windows.Forms.Button();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.lblAttempts = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // btnStartNewGame
            // 
            this.btnStartNewGame.Location = new System.Drawing.Point(10, 640);
            this.btnStartNewGame.Name = "btnStartNewGame";
            this.btnStartNewGame.Size = new System.Drawing.Size(80, 24);
            this.btnStartNewGame.TabIndex = 1;
            this.btnStartNewGame.Text = "Novo Jogo";
            this.btnStartNewGame.UseVisualStyleBackColor = false;
            this.btnStartNewGame.BackColor = ColorTranslator.FromHtml("#121212"); // Set background color
            this.btnStartNewGame.ForeColor = Color.White; // Set text color
            this.btnStartNewGame.Click += new System.EventHandler(this.btnStartNewGame_Click);
            // 
            // btnStartNewGame
            // 
            this.btnInstructions.Location = new System.Drawing.Point(10, 670);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(80, 24);
            this.btnInstructions.TabIndex = 2;
            this.btnInstructions.Text = "Instruções";
            this.btnInstructions.UseVisualStyleBackColor = false;
            this.btnInstructions.BackColor = ColorTranslator.FromHtml("#121212"); // Set background color
            this.btnInstructions.ForeColor = Color.White; // Set text color
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // lblAttempts
            // 
            this.lblAttempts.AutoSize = true;
            this.lblAttempts.Location = new System.Drawing.Point(10, 620);
            this.lblAttempts.Name = "lblAttempts";
            this.lblAttempts.Size = new System.Drawing.Size(82, 13);
            this.lblAttempts.TabIndex = 3;
            this.lblAttempts.Text = "Tentativas: 0";
            this.lblAttempts.ForeColor = Color.White; // Define a cor do texto como branco
            // 
            // 
            // Mastermind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400,700);
            this.Controls.Add(this.btnStartNewGame);
            this.Controls.Add(this.btnInstructions);
            this.Controls.Add(this.lblAttempts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Mastermind";
            this.Text = "Mastermind";
            this.AcceptButton = null;
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnStartNewGame;
        private System.Windows.Forms.Button btnInstructions;
        private System.Windows.Forms.Label lblAttempts;

        #endregion

        private void btnStartNewGame_Click(object sender, System.EventArgs e)
        {
            StartNewGame();
        }

        private void btnInstructions_Click(object sender, System.EventArgs e)
        {
            string instructions = @"Instruções do Mastermind:

Objetivo do Jogo:

O objetivo do Mastermind é adivinhar a combinação secreta de quatro bolas coloridas. Você tem um número limitado (10) de tentativas para acertar a combinação secreta. Após cada tentativa, o jogo fornecerá feedback para ajudá-lo a descobrir a combinação correta.

Como Jogar:

1. Fazer uma Jogada:
Selecione bolas coloridas: À direita você tem uma linha de bolas coloridas. Cada bola representa uma cor (vermelho, verde, azul, amarelo, roxo e laranja).
Adicione bolas coloridas até ter selecionado 4 (uma linha).

2. Feedback:
Bolas Vermelhas: Cada uma significa que há uma bola com a cor  e posição correta.
Bolas Brancas: Cada uma significa que há uma bola com a cor correta, mas a posição errada.
Nenhum Feedback: Cor e posição incorrectas.

3. Repetir até vitória ou derrota:
Voltar a fazer uma jogada, quando tiver feito 10 tentativas o jogo acaba em derrota, se adivinhar a combinação correcta antes então ganha.

Botões:
Botão ""Novo Jogo"": Inicia um novo jogo, gerando uma nova combinação secreta e reiniciando as tentativas.
Botão ""Instruções"": Exibe estas instruções sobre como jogar o Mastermind.

            Boa sorte e divirta-se a jogar Mastermind!";

            MessageBox.Show(instructions, "Instruções");
        }

    }
}

