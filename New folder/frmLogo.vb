Public Class frmLogo

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        PictureBox1.SizeMode = Zoom
        'PictureBox1.Height = Me.Height
    End Sub

  
    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Left = Label1.Left + 5

        If Label1.Left > Panel1.Width Then Label1.Left = 0
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Randomize()
        Label1.ForeColor = Color.FromArgb(255, Rnd() * 255, Rnd() * 255, Rnd() * 255)

    End Sub
End Class