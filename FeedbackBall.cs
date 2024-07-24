using mastermind;
using System.Drawing;
using System.Windows.Forms;

// classe para uma bola de feedback.
public class FeedbackBall : Control
{
    private Color ballColor; // cor da bola.
    private Mastermind parentForm; // parent form onde está a logica do jogo.

    // constructor para preencher a bola de uma cor e dar-lhe um certo tamanho.
    public FeedbackBall(Color color, Mastermind form)
    {
        parentForm = form;
        ballColor = color;
        this.Size = new Size(10, 10);
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
}
