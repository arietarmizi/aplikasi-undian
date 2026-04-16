Imports System.Drawing.Drawing2D

Public Class Form1
    Private lblTitle As New Label
    Private teksPemenang As String = ""

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.UserPaint Or
                    ControlStyles.OptimizedDoubleBuffer Or
                    ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True

        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Me.BackColor = Color.White
        Me.KeyPreview = True

        lblTitle.AutoSize = False
        lblTitle.Dock = DockStyle.Top
        lblTitle.TextAlign = ContentAlignment.BottomCenter
        lblTitle.ForeColor = Color.White
        lblTitle.BackColor = Color.Transparent
        lblTitle.Text = "SELAMAT KEPADA :"

        Dim h As Integer = Screen.PrimaryScreen.Bounds.Height
        lblTitle.Padding = New Padding(0, CInt(h * 0.15), 0, CInt(h * 0.1))

        Me.Controls.Add(lblTitle)
        lblTitle.BringToFront()

        AddHandler Me.Paint, AddressOf Form1_Paint

        Application.DoEvents()
        ReposisiTombol()
    End Sub

    Private Sub ReposisiTombol()
        Dim screenW As Integer = Me.ClientRectangle.Width
        Button2.Location = New Point(screenW - Button2.Width - 15, 15)
        Button2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button2.BringToFront()
    End Sub

    Public Sub ShowCenterScreen(teks As String)
        teksPemenang = teks
        Me.Invalidate()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs)
        If String.IsNullOrEmpty(teksPemenang) Then Return

        Dim screenW As Integer = Me.ClientRectangle.Width
        Dim screenH As Integer = Me.ClientRectangle.Height

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

        Dim baris() As String = teksPemenang.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        Dim jmlData As Integer = baris.Length
        Dim pakaiDuaKolom As Boolean = (jmlData > 10)

        Dim baseScale As Single = CSng(screenH / 1000)
        Dim fontSizeBase As Single = If(pakaiDuaKolom, 32 * baseScale, 48 * baseScale)

        Dim fontName As String = "Montserrat"
        Dim fontNama As Font = Nothing

        Dim barisTerpanjang As String = ""
        For Each b In baris
            If b.Trim().Length > barisTerpanjang.Length Then barisTerpanjang = b.Trim()
        Next

        Dim gapTengahMinimal As Integer = CInt(screenW * 0.08)
        Dim marginLayar As Integer = CInt(screenW * 0.05)
        Dim safeWidth As Integer = If(pakaiDuaKolom, CInt((screenW / 2) - (gapTengahMinimal / 2) - marginLayar), screenW - (marginLayar * 2))
        Dim safeHeight As Integer = screenH - lblTitle.Bottom - 50

        Do
            If fontNama IsNot Nothing Then fontNama.Dispose()
            fontNama = New Font(fontName, fontSizeBase, FontStyle.Bold)

            Dim sizeTerpanjang As Size = TextRenderer.MeasureText(barisTerpanjang, fontNama)
            Dim rowsPerCol As Integer = If(pakaiDuaKolom, CInt(Math.Ceiling(jmlData / 2)), jmlData)

            If (sizeTerpanjang.Width + 100 < safeWidth) AndAlso ((rowsPerCol * (sizeTerpanjang.Height + 25)) < safeHeight) Then
                Exit Do
            End If

            fontSizeBase -= 1
        Loop While fontSizeBase > 6

        lblTitle.Font = New Font(fontName, CSng(fontSizeBase * 1.5), FontStyle.Bold)
        lblTitle.Height = TextRenderer.MeasureText(lblTitle.Text, lblTitle.Font).Height + lblTitle.Padding.Top + lblTitle.Padding.Bottom

        Dim startY As Integer = lblTitle.Bottom
        Dim currentY As Integer = startY
        Dim gapAntarBar As Integer = CInt(screenH * 0.018) ' Jarak baris proporsional

        Dim sizeNamaMax As Size = TextRenderer.MeasureText(barisTerpanjang, fontNama)
        Dim xCenterAlignLeft As Integer = CInt((screenW - (sizeNamaMax.Width + 100)) / 2)

        Dim rowsDrawn As Integer = 0
        Dim rowsPerColLimit As Integer = If(pakaiDuaKolom, CInt(Math.Ceiling(jmlData / 2)), jmlData)

        For i As Integer = 0 To jmlData - 1
            Dim textSize As Size = TextRenderer.MeasureText(baris(i), fontNama)
            Dim barWidth As Integer = textSize.Width + 100
            Dim barHeight As Integer = textSize.Height + CInt(screenH * 0.02)

            If pakaiDuaKolom AndAlso i = rowsPerColLimit Then
                currentY = startY
            End If

            Dim currentX As Integer
            If pakaiDuaKolom Then
                currentX = If(i < rowsPerColLimit, marginLayar, CInt((screenW / 2) + (gapTengahMinimal / 2)))
            Else
                currentX = xCenterAlignLeft
            End If

            Dim rectBar As New Rectangle(currentX, currentY, barWidth, barHeight)
            Using br As New LinearGradientBrush(rectBar, Color.FromArgb(0, 160, 255), Color.FromArgb(0, 80, 190), LinearGradientMode.Vertical)
                FillRoundedRectangle(g, br, rectBar, 20)
            End Using

            g.DrawString(baris(i), fontNama, Brushes.White, currentX + 50, currentY + CSng(barHeight / 2 - textSize.Height / 2))

            currentY += barHeight + gapAntarBar
        Next

        fontNama.Dispose()
    End Sub

    Private Sub FillRoundedRectangle(g As Graphics, brush As Brush, rect As Rectangle, radius As Integer)
        If rect.Width <= 0 Or rect.Height <= 0 Then Return
        Dim diameter As Integer = radius * 2
        If diameter > rect.Height Then diameter = rect.Height
        If diameter > rect.Width Then diameter = rect.Width

        Using path As New GraphicsPath()
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
            path.CloseFigure()
            g.FillPath(brush, path)
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Button2.PerformClick()
    End Sub
End Class