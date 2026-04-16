Imports System.Data.OleDb

Public Class FrmGrandprize
    Dim DT As DataTable
    Dim DR As DataRow

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
            Randomize()
            DR = DT.Rows(CInt(Rnd() * (DT.Rows.Count - 1)))
            lblCompany.Text = DR("companies").ToString()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        shuffle()
    End Sub

    Private Sub shuffle()
        Dim s As String = ""
        Dim jumlahPemenang As Integer = 1

        If txtJumlahPemenang.Text <> "" Then
            If Not Integer.TryParse(txtJumlahPemenang.Text, jumlahPemenang) OrElse jumlahPemenang < 1 Then
                MsgBox("Masukkan jumlah pemenang yang valid!", vbExclamation)
                txtJumlahPemenang.Focus()
                Exit Sub
            End If
        End If

        If Timer1.Enabled = False Then
            If cmbDoorprize.Text = "" Then
                MsgBox("Pilih Jenis Hadiah Yang Akan Diundi", vbInformation)
                Exit Sub
            End If

            ' Filter anti duplicate
            If lvlist.Items.Count > 0 Then
                Dim listNama As New List(Of String)
                For i As Integer = 0 To lvlist.Items.Count - 1
                    listNama.Add("'" & lvlist.Items(i).ToString().Replace("'", "''") & "'")
                Next
                s = " WHERE Companies NOT IN (" & String.Join(",", listNama) & ")"
            End If

            DT = SQLTable("SELECT * FROM company" & s)

            ' Cek remaining quota winner
            If DT.Rows.Count = 0 Then
                MsgBox("Semua peserta sudah mendapatkan hadiah (Data Habis)!", vbInformation)
                Exit Sub
            ElseIf DT.Rows.Count < jumlahPemenang Then
                MsgBox("Sisa peserta tinggal " & DT.Rows.Count & ". Tidak cukup untuk " & jumlahPemenang & " pemenang.", vbExclamation)
                Exit Sub
            End If

            Timer1.Enabled = True
            Button1.Text = "STOP"
            lblCompany.Font = New Font(lblCompany.Font.Name, 12)
            lblTipe.Text = ""

        Else
            ' --- Winner determination Stage (on STOP Button) ---
            Timer1.Enabled = False
            Button1.Text = "START"

            Dim rnd As New Random()
            Dim indexTerpilih As New List(Of Integer)

            ' Randomize indexes without duplicates in one pull
            While indexTerpilih.Count < jumlahPemenang
                Dim acak As Integer = rnd.Next(0, DT.Rows.Count)
                If Not indexTerpilih.Contains(acak) Then
                    indexTerpilih.Add(acak)
                End If
            End While

            Dim companyList As New List(Of String)
            Dim typeList As New List(Of String)

            Call Koneksi()

            Dim lastIndex As Integer = GetNextIndeks()

            For Each idx As Integer In indexTerpilih
                Dim row As DataRow = DT.Rows(idx)
                Dim companyName As String = row("companies").ToString()
                Dim companyType As String = row("tipe").ToString()

                multipleSaveWinner(companyName, companyType, lastIndex)

                companyList.Add(companyName)
                typeList.Add(companyType)
            Next

            If Con.State = ConnectionState.Open Then Con.Close()

            lblCompany.Text = "SELAMAT KEPADA PEMENANG."
            AutoAdjustFont(lblCompany)

            loadList()

            Application.DoEvents()
            System.Threading.Thread.Sleep(500) ' sleep for ensure data record show

            ' --- Auto Number ---
            Dim formatPemenang As New List(Of String)
            Dim nomorUrut As Integer = 1

            For Each namaPT In companyList
                ' if there are multiple winners, add numbering, otherwise just show the name without number
                If jumlahPemenang > 1 Then
                    formatPemenang.Add(nomorUrut.ToString() & ". " & namaPT)
                Else
                    formatPemenang.Add(namaPT)
                End If
                nomorUrut += 1
            Next

            Form1.ShowCenterScreen(String.Join(Environment.NewLine, formatPemenang))
            Form1.ShowDialog()
        End If
    End Sub

    Private Sub AutoAdjustFont(lbl As Label)
        ' Set allowed font size
        Dim testFont As Font = New Font(lbl.Font.Name, 20, FontStyle.Bold)
        Dim testSize As SizeF

        Using g As Graphics = lbl.CreateGraphics()
            testSize = g.MeasureString(lbl.Text, testFont)

            ' font reducement loop until text fits within label bounds or minimum font size is reached
            While (testSize.Width > lbl.Width Or testSize.Height > lbl.Height) AndAlso testFont.Size > 8
                testFont = New Font(testFont.Name, testFont.Size - 1, FontStyle.Bold)
                testSize = g.MeasureString(lbl.Text, testFont)
            End While
        End Using

        ' implement adjusted font to label
        lbl.Font = testFont
    End Sub

    ' --- Get Indeks ---
    Private Function GetNextIndeks() As Integer
        Dim nextIdx As Integer = 1
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()

            Dim query As String = "SELECT MAX(Indeks) FROM pemenang"
            Using cmd As New OleDbCommand(query, Con)
                Dim result = cmd.ExecuteScalar()

                ' Check if result is DBNull or Nothing, if so start from 1, otherwise increment the max index by 1
                If IsDBNull(result) OrElse result Is Nothing Then
                    nextIdx = 1 ' Table is empty, start indexing from 1
                Else
                    ' Table data exists, increment the max index by 1 for the next entry
                    nextIdx = Convert.ToInt32(result) + 1
                End If
            End Using

        Catch ex As Exception
            nextIdx = 1
        End Try

        Return nextIdx
    End Function

    Private Sub multipleSaveWinner(companyName As String, tipeName As String, indexKe As Integer)
        On Error GoTo salah

        Dim simpan As String = "INSERT INTO pemenang (companies, tipe, kategori, hadiah, indeks) VALUES (?, ?, ?, ?, ?)"

        Using cmd As New OleDbCommand(simpan, Con)
            cmd.Parameters.AddWithValue("?", companyName)
            cmd.Parameters.AddWithValue("?", tipeName)
            cmd.Parameters.AddWithValue("?", Label4.Text)
            cmd.Parameters.AddWithValue("?", cmbDoorprize.Text)
            cmd.Parameters.AddWithValue("?", indexKe)
            cmd.ExecuteNonQuery()
        End Using

        Exit Sub
salah:
        MsgBox("Gagal menyimpan data pemenang: " & companyName & vbCrLf & Err.Description, vbInformation)
    End Sub

    Private Sub FrmGrandprize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCombo()
        loadList()
        cmbDoorprize.Select()
    End Sub

    Private Sub loadList()
        Try
            Dim sqL As String = "select * from pemenang order by id"
            Koneksi()
            Dim cmdx As New OleDbCommand(sqL, Con)
            Dim drx As OleDbDataReader = cmdx.ExecuteReader()

            lvlist.Items.Clear()

            Do While drx.Read = True
                lvlist.Items.Add(drx.Item("companies").ToString())
            Loop
            drx.Close()
        Catch ex As Exception
            MsgBox("Error LoadList: " & ex.Message)
        End Try
    End Sub

    Private Sub loadCombo()
        Try
            Dim sqL As String = "select no, jenis, picturename, kategori from doorprize where kategori like '" & Label4.Text & "' order by ID asc"
            Koneksi()
            Dim cmdx As New OleDbCommand(sqL, Con)
            Dim drx As OleDbDataReader = cmdx.ExecuteReader(CommandBehavior.CloseConnection)

            cmbDoorprize.Items.Clear()
            Do While drx.Read = True
                cmbDoorprize.Items.Add(drx("jenis").ToString())
            Loop
            drx.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub loadPic()
        Try
            Dim sqL As String = "Select * from doorprize where jenis='" & cmbDoorprize.Text & "'"
            Koneksi()
            Dim cmdx As New OleDbCommand(sqL, Con)
            Dim drx As OleDbDataReader = cmdx.ExecuteReader(CommandBehavior.CloseConnection)
            While drx.Read
                If Not IsDBNull(drx.Item("picturename")) Then
                    Dim myphoto As Byte() = DirectCast(drx.Item("picturename"), Byte())
                    If myphoto.Length = 0 Then
                        picPhoto.Image = Nothing
                    Else
                        picPhoto.Image = Image.FromStream(New IO.MemoryStream(myphoto))
                    End If
                End If
            End While
            drx.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbDoorprize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDoorprize.SelectedIndexChanged
        loadPic()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub FrmGrandprize_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        lblCompany.Text = "-"
        lblTipe.Text = "-"
    End Sub

    Private Sub Button2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Button2.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
            Form2.Show()
        End If
    End Sub

    Private Sub cmbDoorprize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDoorprize.KeyPress
        If (e.KeyChar = Chr(13)) Then
            shuffle()
            cmbDoorprize.Select()
        End If
    End Sub

    Private Sub FrmGrandprize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If (e.KeyChar = Chr(Keys.Escape)) Then
            Me.Close()
            Form2.Show()
        End If
    End Sub

End Class