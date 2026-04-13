Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblCompany.AutoSize = False
        lblCompany.Dock = DockStyle.Fill

        lblCompany.TextAlign = ContentAlignment.MiddleLeft

        lblCompany.Padding = New Padding(200, 0, 0, 0)
    End Sub

    Public Sub ShowCenterScreen(teks As String)
        lblCompany.Text = teks

        Dim ukuranFont As Single = 36
        Dim fontUji As New Font(lblCompany.Font.Name, ukuranFont, FontStyle.Bold)
        Dim ukuranTeks As SizeF

        Using g As Graphics = lblCompany.CreateGraphics()
            ukuranTeks = g.MeasureString(lblCompany.Text, fontUji)

            While (ukuranTeks.Width > (lblCompany.Width - 300) OrElse ukuranTeks.Height > lblCompany.Height) AndAlso ukuranFont > 10
                ukuranFont -= 1
                fontUji = New Font(lblCompany.Font.Name, ukuranFont, FontStyle.Bold)
                ukuranTeks = g.MeasureString(lblCompany.Text, fontUji)
            End While
        End Using

        lblCompany.Font = fontUji
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class