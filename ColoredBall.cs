﻿using mastermind;
using System;
using System.Drawing;
using System.Windows.Forms;

// classe para uma bola de uma cor
public class ColoredBall : Control
{
    private Color ballColor; // cor da bola.
    private Mastermind parentForm; // parent form onde está a logica do jogo.
    private bool isControlBall; // se faz parte dos controles ou não.
    private int ballPosition; // Posição da bola (de 1 a 4).

    // constructor para preencher a bola de uma cor, dar-lhe um certo tamanho e dar funcionalidade de controle se necessário.
    public ColoredBall(Color color, bool isControlBall, Mastermind form, int position = 0)
    {
        parentForm = form;
        ballColor = color;
        ballPosition = position;
        this.Size = new Size(40, 40);
        this.isControlBall = isControlBall;
        if (isControlBall)
        {
            this.MouseEnter += ColoredBall_MouseEnter;
            this.MouseLeave += ColoredBall_MouseLeave;
            this.Click += ColoredBall_Click;
        }
    }

    // Getter para obter a cor da bola.
    public Color BallColor
    {
        get { return ballColor; }
    }

    // método que desenha a bola do tamanho e da cor desejado.
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (SolidBrush brush = new SolidBrush(ballColor))
        {
            e.Graphics.FillEllipse(brush, 0, 0, this.Width, this.Height);
        }
    }

    // event handler para mudar o cursor quando se faz hover.
    private void ColoredBall_MouseEnter(object sender, EventArgs e)
    {
        this.Cursor = Cursors.Hand;
    }

    // event handler para mudar o cursor quando se deixa de fazer hover.
    private void ColoredBall_MouseLeave(object sender, EventArgs e)
    {
        this.Cursor = Cursors.Default;
    }

    // event handler para quando se faz click na bola.
    private void ColoredBall_Click(object sender, EventArgs e)
    {
        parentForm.AddColoredBall(ballColor);
    }

}
