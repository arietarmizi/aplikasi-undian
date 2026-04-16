Public Class Form2
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Timer1.Enabled = True
    End Sub

    Private Sub Form2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Form2_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim F1 As Boolean
        Dim F2 As Boolean
        Dim F3 As Boolean
        Dim F5 As Boolean
        Dim F6 As Boolean
        F1 = GetAsyncKeyState(Keys.F1)
        F2 = GetAsyncKeyState(Keys.F2)
        F3 = GetAsyncKeyState(Keys.F3)
        F5 = GetAsyncKeyState(Keys.F5)
        F6 = GetAsyncKeyState(Keys.F6)
        If F5 = True Then
            Me.Hide()
            FrmGrandprize.ShowDialog()
        Else
            If F6 = True Then
                Me.Hide()
                FrmGrandprize.ShowDialog()
            Else
                If F1 = True Then
                    FrmCompany.ShowDialog()
                Else
                    If F2 = True Then
                        FrmMaster.ShowDialog()
                    Else
                        If F3 = True Then
                            FrmResult.ShowDialog()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = Format(Now, "dd")
        If Label6.Text >= Label7.Text Then
            Application.Exit()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub
End Class