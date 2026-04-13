Imports System.Data.OleDb

Public Class FrmDoorprize
    Dim DT As DataTable
    Dim DR As DataRow

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Randomize()
        DR = DT.Rows(CInt(Rnd() * (DT.Rows.Count - 1)))
        lblCompany.Text = DR("companies")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim s As String, i As Integer

        If Timer1.Enabled = False Then

            If lvlist.Items.Count <> 0 Then
                s = " where Companies <> '" & lvlist.Items(0) & "'"
                For i = 1 To lvlist.Items.Count - 1
                    s &= " and tipe <> '" & lvlist.Items(i) & "'"
                Next
            End If

            DT = SQLTable("select * from company" & s)

            If DT.Rows.Count = 0 Then
                MsgBox("Data Habis !")
                Exit Sub
            End If


            Timer1.Enabled = True
            Button1.Text = "Berhenti"
            lblCompany.Font = New Font(lblCompany.Font.Name, 12)
            lblTipe.Text = ""


        Else
            Timer1.Enabled = False
            Button1.Text = "START"
            lblCompany.Font = New Font(lblCompany.Font.Name, 24)
            lblTipe.Text = DR("tipe")
            simpanPemenang()
            loadList()
        End If
    End Sub

    Private Sub simpanPemenang()
        On Error GoTo salah
        If cmbDoorprize.Text = "" Then
            MsgBox("Pilih Jenis Hadiah Yang Akan Diundi", vbInformation)
        Else
            Call Koneksi()
            Dim simpan As String = "insert into pemenang (companies,tipe,kategori,hadiah) values ( '" & lblCompany.Text &
                                   "','" & lblTipe.Text & "', '" & Label4.Text & "','" & cmbDoorprize.Text & "')"
            cmdx = New OleDbCommand(simpan, Con)
            cmdx.ExecuteNonQuery()

            Form1.lblCompany.Text = lblCompany.Text
            Form1.lblTipe.Text = lblTipe.Text
            Form1.ShowDialog()
        End If
        Exit Sub
salah:
        MsgBox("Periksa Kembali Apakah Nama PT Terdapat tanda koma atas(')", vbInformation)
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCombo()
        loadList()
        cmbDoorprize.Select()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        rolling()
    End Sub

    Private Sub rolling()
        Dim s As String, i As Integer

        If Timer1.Enabled = False Then

            If lvlist.Items.Count <> 0 Then
                s = " where Companies <> '" & lvlist.Items(0) & "'"
                For i = 1 To lvlist.Items.Count - 1
                    s &= " and tipe <> '" & lvlist.Items(i) & "'"
                Next
            End If

            DT = SQLTable("select * from company" & s)

            If DT.Rows.Count = 0 Then
                MsgBox("Data Peserta Habis!")
                Exit Sub
            End If


            Timer1.Enabled = True
            Button1.Text = "STOP"
            lblCompany.Font = New Font(lblCompany.Font.Name, 12)
            lblTipe.Text = ""

        Else
            Timer1.Enabled = False
            Button1.Text = "START"
            lblCompany.Font = New Font(lblCompany.Font.Name, 24)
            lblTipe.Text = DR("tipe")
            simpanPemenang()
            loadList()
        End If
    End Sub

    Private Sub loadList()
        Try
            sqL = "select * from pemenang order by id"
            Koneksi()
            cmdx = New OleDbCommand(sqL, Con)
            drx = cmdx.ExecuteReader
            lvlist.Items.Clear()

            Do While drx.Read = True
                lvlist.Items.Add(drx.Item("companies"))

            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub loadCombo()
        Try
            sqL = "select no, jenis, picturename, kategori from doorprize where kategori like '" & Label4.Text &
                  "' order by jenis asc"
            Koneksi()
            cmdx = New OleDbCommand(sqL, Con)
            drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection)

            cmbDoorprize.Items.Clear()
            Do While drx.Read = True
                cmbDoorprize.Items.Add(drx("jenis"))
            Loop

        Catch ex As Exception

        End Try
    End Sub

    Private Sub loadPic()
        Try
            sqL = "Select * from doorprize where jenis='" & cmbDoorprize.Text & "'"
            Koneksi()
            cmdx = New OleDbCommand(sqL, Con)
            drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection)
            While drx.Read
                Dim myphoto As Byte() = drx.Item("picturename")
                If myphoto.Length = 0 Then
                    picPhoto.Image = Nothing
                Else
                    picPhoto.Image = Image.FromStream(New IO.MemoryStream(myphoto))
                End If
            End While

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbDoorprize_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cmbDoorprize.SelectedIndexChanged
        loadPic()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub lblCompany_Click(sender As Object, e As EventArgs) Handles lblCompany.Click
    End Sub

    Private Sub lblCompany_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lblCompany.KeyPress
        If (e.KeyChar = Chr(13)) Then
            rolling()
        End If
    End Sub

    Private Sub cmbDoorprize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDoorprize.KeyPress
        If (e.KeyChar = Chr(13)) Then
            rolling()
            cmbDoorprize.Select()
        End If
    End Sub

    Private Sub FrmDoorprize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
            Form2.Show()
        End If
    End Sub

    Private Sub FrmDoorprize_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        cmbDoorprize.Select()
        lblCompany.Text = "-"
        lblTipe.Text = "-"
    End Sub

    Private Sub FrmDoorprize_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim widthform As Integer = Me.Width
        Dim heightform As Integer = Me.Height
    End Sub
End Class