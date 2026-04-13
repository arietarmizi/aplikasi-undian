
Imports System.Math

Public Class frmMain

    Public isLogout As Boolean
#Region ""
    Private Sub MouseLeaveEvent(ByVal sender As Object, ByVal e As System.EventArgs)


        ' A simple event handler that fires when the user's mouse arrow leaves the
        ' area of any of the buttons.  It passes the button's identity to the Glow sub.

        Dim ctrl As Control = CType(sender, Control)
        Glow(ctrl, False)

    End Sub



    Private Sub MouseEnterEvent(ByVal sender As Object, ByVal e As System.EventArgs)


        ' A simple event handler that fires when the user's mouse arrow rolls 
        ' over any of the buttons.  It passes the button's identity to the Glow sub.

        Dim ctrl As Control = CType(sender, Control)
        Glow(ctrl, True)

    End Sub
    Private Sub Glow(ByVal ctrl As Control, ByVal Hovering As Boolean)

        Dim Sb_R, Sb_G, Sb_B As Int16 '           <--- RGB values for the starting backcolor
        Dim Sf_R, Sf_G, Sf_B As Int16 '           <--- RGB values for the starting forecolor
        Dim Eb_R, Eb_G, Eb_B As Int16 '           <--- RGB values for the ending backcolor
        Dim Ef_R, Ef_G, Ef_B As Int16 '           <--- RGB values for the ending forecolor

        Select Case Hovering  ' True for MouseEnter, False for MouseLeave
            Case True
                Sb_R = SystemColors.Control.R           ' Assign the variables the 
                Sb_G = SystemColors.Control.G           ' appropriate values based
                Sb_B = SystemColors.Control.B           ' on system-defined colors
                '                                         for control and control
                Sf_R = SystemColors.ControlText.R       ' text.
                Sf_G = SystemColors.ControlText.G
                Sf_B = SystemColors.ControlText.B

                Eb_R = SystemColors.ActiveCaption.R     ' Assign the variables the
                Eb_G = SystemColors.ActiveCaption.G     ' appropriate values based
                Eb_B = SystemColors.ActiveCaption.B     ' on system-defined colors
                '                                         for the title bar and the
                Ef_R = SystemColors.ActiveCaptionText.R ' title bar's text.
                Ef_G = SystemColors.ActiveCaptionText.G
                Ef_B = SystemColors.ActiveCaptionText.B
            Case False
                Sb_R = SystemColors.ActiveCaption.R     ' Assign the variables the
                Sb_G = SystemColors.ActiveCaption.G     ' appropriate values based
                Sb_B = SystemColors.ActiveCaption.B     ' on system-defined colors
                '                                         for the title bar and the
                Sf_R = SystemColors.ActiveCaptionText.R ' title bar's text.
                Sf_G = SystemColors.ActiveCaptionText.G
                Sf_B = SystemColors.ActiveCaptionText.B

                Eb_R = SystemColors.Control.R           ' Assign the variables the 
                Eb_G = SystemColors.Control.G           ' appropriate values based
                Eb_B = SystemColors.Control.B           ' on system-defined colors
                '                                         for control and control
                Ef_R = SystemColors.ControlText.R       ' text.
                Ef_G = SystemColors.ControlText.G
                Ef_B = SystemColors.ControlText.B
        End Select

        Dim b_RIncrement As Int16 = Round(((Eb_R - Sb_R) / 16), 0)      ' Find the increments that
        Dim b_GIncrement As Int16 = Round(((Eb_G - Sb_G) / 16), 0)      ' the RGB values will take;
        Dim b_BIncrement As Int16 = Round(((Eb_B - Sb_B) / 16), 0)      ' 1/16th of the difference
        '                                                                 between the start and end
        Dim f_RIncrement As Int16 = Round(((Ef_R - Sf_R) / 16), 0)      ' values, rounded to the 
        Dim f_GIncrement As Int16 = Round(((Ef_G - Sf_G) / 16), 0)      ' nearest integer.
        Dim f_BIncrement As Int16 = Round(((Ef_B - Sf_B) / 16), 0)

        Dim bR As Int16 = Sb_R ' \
        Dim bG As Int16 = Sb_G '  }--  the RGB values for the backcolor as it changes.
        Dim bB As Int16 = Sb_B ' /

        Dim fR As Int16 = Sf_R ' \
        Dim fG As Int16 = Sf_G '  }--  the RGB values for the forecolor as it changes.
        Dim fB As Int16 = Sf_B ' /

        Dim count As Int16
        For count = 0 To 6
            bR += b_RIncrement          ' Add the appropriate increments to the 
            bG += b_GIncrement          ' RGB values.  The result is a nifty
            bB += b_BIncrement          ' "cross-fade" effect.
            fR += f_RIncrement
            fG += f_GIncrement
            fB += f_BIncrement

            ctrl.BackColor = Color.FromArgb(bR, bG, bB)   ' Repaint the button using the
            ctrl.ForeColor = Color.FromArgb(fR, fG, fB)   ' current RGB values, and refresh
            ctrl.Refresh()                                ' the button.

            Threading.Thread.Sleep(30 - (count * 2))      ' Wait a certain number of milliseconds,
            '                                               which is a factor of the current count.
            '                                               I tweaked these numbers to get a decent
            '                                               effect; feel free to adjust these 
            '                                               numbers to achieve an effect you like.
        Next
        Select Case Hovering
            Case True
                ctrl.BackColor = Color.White        ' Finish the effect by painting 
                ctrl.ForeColor = SystemColors.InactiveCaptionText      ' the control with it's final
            Case False                                              ' look, depending on whether
                ctrl.BackColor = Color.Maroon             ' we're fading in or out.
                ctrl.ForeColor = SystemColors.ControlLight
        End Select
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)

    End Sub
#End Region

    Dim LL, II, PP As Integer
    Dim TXT As String



    Private Sub StudentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StudentToolStripMenuItem.Click
        If MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Close Application") = MsgBoxResult.Yes Then
            Application.Exit()
        End If

    End Sub


    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Close Application") = MsgBoxResult.Yes Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLogout = False
        frmLogo.MdiParent = Me
        frmLogo.Show()

        LoadShortcut()
        getData()
        tstDate.Text = Format(Date.Now, "MMMM dd, yyyy")


        TXT = "Undian"
        LL = Len(TXT)
        II = 1
        PP = 1

        ' If isNotified() = True Then
        ' lblNotification.Text = "Clients notified"
        ' Else
        '  LoadNotifications()
        'End If

        'frmLogin.ShowDialog()
    End Sub

    Private Sub ToolStripContainer1_ContentPanel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripContainer1.ContentPanel.Load

    End Sub

    Private Sub SystemFlowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ClientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientToolStripMenuItem.Click

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        txtTime.Text = Format(Date.Now, "Long Time")
    End Sub


    Private Sub PaymentsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentsToolStripMenuItem1.Click
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label1.Text = Label1.Text + Mid(TXT, II, 1)

        If II > LL Then
            II = 0
            Label1.Text = ""
        End If

        II = II + 1
    End Sub

    Private Sub PaymentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentsToolStripMenuItem.Click
        'frmPayment.ShowDialog()
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Randomize()
        lblN.ForeColor = Color.FromArgb(255, Rnd() * 255, Rnd() * 255, Rnd() * 255)

        lblN.Left = lblN.Left + 5
        If lblN.Left > Panel2.Width Then lblN.Left = 0

    End Sub

    Private Sub LoadShortcut()
        ListView1.Items.Add("Doorprize", 21)
        ListView1.Items.Add("Grandprize", 22)
        ListView1.Items.Add("Company", 6)
        ListView1.Items.Add("Add Doorprize", 1)
        ListView1.Items.Add("Result", 3)
        'ListView1.Items.Add("Order Masuk", 10)
        'ListView1.Items.Add("Master Barang", 8)
        'ListView1.Items.Add("Produksi", 2)
        'ListView1.Items.Add("Stock Barang", 11)
        'ListView1.Items.Add("Sold Lots", 20)
        'ListView1.Items.Add("Transfer Barang", 19)
        'ListView1.Items.Add("Pengguna", 0)
        'ListView1.Items.Add("SMS Notifications", 12)
        '  ListView1.Items.Add("Matured Accounts", 22)
        'ListView1.Items.Add("Penalty", 3)
        'ListView1.Items.Add("Summary", 21)
    End Sub


    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        If ListView1.FocusedItem.Text = "Doorprize" Then
            Me.Hide()
            FrmDoorprize.ShowDialog()
            'frmMCClient.MdiParent = Me
            'frmMCClient.Show()
        ElseIf ListView1.FocusedItem.Text = "Company" Then
            FrmCompany.ShowDialog()
            ' frmMCLot.MdiParent = Me
            ' frmMCLot.Show()
        ElseIf ListView1.FocusedItem.Text = "Add Doorprize" Then
            FrmMaster.ShowDialog()
            ' frmMCContract.MdiParent = Me
            ' frmMCContract.Show()
        ElseIf ListView1.FocusedItem.Text = "Result" Then
            FrmResult.ShowDialog()
        ElseIf ListView1.FocusedItem.Text = "Grandprize" Then
            Me.Hide()
            FrmGrandprize.ShowDialog()

        End If
    End Sub



    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Randomize()
        If isNOtify = True Then
            lblNotification.ForeColor = Color.FromArgb(255, Rnd() * 255, Rnd() * 255, Rnd() * 255)
        Else
            lblNotification.ForeColor = Color.Black
        End If
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        frmLogo.MdiParent = Me
        frmLogo.Show()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        frmLock.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Close()


    End Sub


    Private Sub SoldLotsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoldLotsToolStripMenuItem.Click

    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Shell("calc.exe")
    End Sub

    Private Sub NotepadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotepadToolStripMenuItem.Click
        Shell("notepad.exe")
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click

    End Sub

    Private Sub AboutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub CollectablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub SMSNotiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMSNotiToolStripMenuItem.Click
        MsgBox("Please visit https://www.facebook.com/CyberVSS for the full feature of the system")
    End Sub

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserToolStripMenuItem.Click

    End Sub

    Private Sub PaidAccountBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaidAccountBalanceToolStripMenuItem.Click

    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click


    End Sub

    Private Sub SummaryToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryToolStripMenuItem.Click

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        isLogout = True
        'frmLogin.ShowDialog()
    End Sub
End Class
