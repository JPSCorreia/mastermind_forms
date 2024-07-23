using System;
using System.Drawing;
using System.Windows.Forms;

public class ColoredBall : Control
{
    private Color ballColor;

    public Color BallColor
    {
        get => ballColor;
        set
        {
            ballColor = value;
            Invalidate(); // Triggers a repaint
        }
    }



    public ColoredBall(int colorNumber)
    {
        BallColor = GetColorFromNumber(colorNumber);
        this.Size = new Size(30, 30); // Default size of the ball
    }


    // Transforma um numero numa cor.
    private Color GetColorFromNumber(int number)
    {
        if (number == 1) return Color.Red;
        if (number == 2) return Color.Green;
        if (number == 3) return Color.Blue;
        if (number == 4) return Color.Yellow;
        if (number == 5) return Color.Purple;
        if (number == 6) return Color.Orange;
        return Color.Black; // Default color
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (SolidBrush brush = new SolidBrush(ballColor))
        {
            e.Graphics.FillEllipse(brush, 0, 0, this.Width, this.Height);
        }
    }
}
