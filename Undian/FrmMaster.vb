Imports System.Data.OleDb
Public Class FrmMaster
    Dim Picture As Integer
    Dim P As Integer
    Sub Clear()
        txtName.Clear()
        picPhoto.Image = Nothing
        cboKat.SelectedIndex = -1

        Call AutoNumber()
    End Sub
    Public Function ConvertImage(ByVal myImage As Image) As Byte()
        On Error Resume Next
        Dim mstream As New IO.MemoryStream
        myImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Png)

        Dim myBytes(mstream.Length - 1) As Byte
        mstream.Position = 0
        mstream.Read(myBytes, 0, mstream.Length)

        Return myBytes

    End Function

    Private Sub frmDoorprize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilGrid()
        Call Clear()
    End Sub

    Sub TampilGrid()
        Koneksi()
        dax = New OleDbDataAdapter("select * from doorprize", Con)
        ds = New DataSet
        ds.Clear()
        dax.Fill(ds, "doorprize")
        datagridview1.DataSource = (ds.Tables("doorprize"))
        datagridview1.Columns(0).Width = 75
        datagridview1.Columns(1).Width = 150
        datagridview1.Columns(2).Width = 150
        datagridview1.Columns(3).Width = 150
        'datagridview1.AutoResizeRows = fill
        datagridview1.AllowUserToAddRows = False
        datagridview1.AllowUserToOrderColumns = True
        datagridview1.RowsDefaultCellStyle.BackColor = Color.LightYellow
        datagridview1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub datagridview1_CellContentClick_2(sender As Object, e As DataGridViewCellEventArgs) Handles datagridview1.CellContentClick
        'On Error Resume Next
        Dim i As Integer
        i = Me.datagridview1.CurrentRow.Index
        With datagridview1.Rows.Item(i)
            Me.txtNo.Text = .Cells(0).Value
            Me.txtName.Text = .Cells(1).Value
            Me.cboKat.Text = .Cells(3).Value
            Dim c As New IO.MemoryStream(CType(datagridview1.Item(2, datagridview1.CurrentRow.Index).Value, Byte()))
            picPhoto.SizeMode = PictureBoxSizeMode.StretchImage
            picPhoto.Image = Image.FromStream(c)
        End With
    End Sub
    Private Function GetFileImages() As String
        '// Get the Filename + Extension only
        Dim iArr() As String
        iArr = Split(newFileName, "\")
        GetFileImages = iArr(UBound(iArr))
        '//
        '// If same original and new
        If orgPicName = newFileName Then Return GetFileImages
        '// Remove original picture
        If orgPicName IsNot Nothing Or orgPicName <> "" Then
            If System.IO.File.Exists(orgPicName) = True Then
                System.IO.File.Delete(orgPicName)
            End If
        End If
        '// ------------- Copy File -------------
        ' Determine whether the source file is real or not.
        If System.IO.File.Exists(newFileName) = True Then
            ' Trap Error in the case source = destination
            If LCase(strPathImages + GetFileImages) <> LCase(newFileName) Then
                ' Copy the Source file (newFileName) to the Destination (DestFile). 
                ' If the same file is found, overwrite (OverWrite = True).
                System.IO.File.Copy(newFileName, strPathImages + GetFileImages, True)
            End If
        End If
    End Function

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        On Error GoTo salah
        If txtName.Text = "" Or txtNo.Text = "" Then
            MsgBox("Complete the data first", vbCritical)
        Else
            Dim CMDx As OleDbCommand
            Dim vImage As Byte() = ConvertImage(picPhoto.Image)
            Call Koneksi()

            Dim Simpan As String = "insert into doorprize values ('" & txtNo.Text & "','" & txtName.Text & "',@picturename,'" & cboKat.Text & "')"

            CMDx = New OleDbCommand(Simpan, Con)
            CMDx.Parameters.Add(New OleDb.OleDbParameter("@picturename", vImage))
            CMDx.ExecuteNonQuery()

            MsgBox("Data saved successfully!", MsgBoxStyle.Information, "Information")
            TampilGrid()
            Clear()
        End If
        Exit Sub
salah:
        MsgBox("Check your input", vbInformation, "Confirmation")
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Call Koneksi()
        If txtNo.Text = "" Then
            MsgBox("Fill code first!")
            txtNo.Focus()
        Else
            If MessageBox.Show("Surely to delete the data?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call Koneksi()
                cmdx = New OleDbCommand("Delete * From doorprize where id='" & txtNo.Text & "'", Con)
                cmdx.ExecuteNonQuery()
                MsgBox("Data successfully deleted.!", MsgBoxStyle.Information, "Information")
                Call TampilGrid()
                Call Clear()
                txtNo.Focus()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ofdphoto.Filter = "JPG|*.JPG|BMP|*.BMP|GIF|*.GIF|PNG|*.PNG"
        ofdphoto.RestoreDirectory = True
        ofdphoto.ShowDialog()
        If ofdphoto.FileName = "" Then Exit Sub
        picPhoto.ImageLocation = ofdphoto.FileName
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtName.Text = "" Or txtNo.Text = "" Or picPhoto.Image Is Nothing Then
            MsgBox("Lengkapi data dan foto", vbCritical)
        Else
            Dim vImage As Byte() = ConvertImage(picPhoto.Image)
            Call Koneksi()

            Dim sql As String = "UPDATE doorprize SET picturename = ?, jenis = ?, kategori = ? WHERE id = ?"
            cmdx = New OleDbCommand(sql, Con)

            cmdx.Parameters.AddWithValue("?", vImage)
            cmdx.Parameters.AddWithValue("?", txtName.Text)
            cmdx.Parameters.AddWithValue("?", cboKat.Text)
            cmdx.Parameters.AddWithValue("?", txtNo.Text)

            cmdx.ExecuteNonQuery()

            MsgBox("Data updated successfully!", MsgBoxStyle.Information, "Information")
            TampilGrid()
            Clear()
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Call Clear()
        txtName.Focus()
    End Sub

    Sub AutoNumber()
        Call Koneksi()
        Dim sql As String = "SELECT id FROM doorprize ORDER BY VAL(id) DESC"
        cmdx = New OleDbCommand(sql, Con)
        Dim rd As OleDbDataReader = cmdx.ExecuteReader
        rd.Read()

        If Not rd.HasRows Then
            txtNo.Text = "1"
        Else
            ' get last ID and increment by 1 for the new entry
            txtNo.Text = Val(rd.Item("id")) + 1
        End If

        rd.Close()
        txtNo.Enabled = False
    End Sub
End Class