Imports System.Data.OleDb
Public Class FrmCompany

    Sub Initial()
        Koneksi()
        Da = New OleDbDataAdapter("Select * from company2", Con)
        ds = New DataSet
        ds.Clear()
        Da.Fill(ds, "company2")
        DataGridView1.DataSource = (ds.Tables("company2"))
        DataGridView1.Columns(0).Width = 70
        DataGridView1.Columns(1).Width = 275
        DataGridView1.Columns(2).Width = 150
        'dgv.Columns(3).Width = 100
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.LightYellow
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        txtNo.Text = ""
        txtCompany.Text = ""
        txtType.Text = ""

        Call AutoNumber()

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Initial()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Call Koneksi()
        Dim simpan As String = "insert into company2 values ('" & txtNo.Text & "','" & txtCompany.Text & "','" & txtType.Text & "')"
        CMD = New OleDbCommand(simpan, Con)
        CMD.ExecuteNonQuery()
        MsgBox("Data saved succesfully.")
        Call Initial()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtNo.Text = "" Then
            MsgBox("Choose data to delete")
        Else
            If MessageBox.Show("Surely delete this data..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call Koneksi()
                Dim CMD As OleDbCommand
                Dim hapus As String = "delete From company2  where id='" & txtNo.Text & "'"
                CMD = New OleDbCommand(hapus, Con)
                CMD.ExecuteNonQuery()
                MsgBox("Data deleted successfully.")
                Call Initial()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer
        i = Me.DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Me.txtNo.Text = .Cells(0).Value
            Me.txtCompany.Text = .Cells(1).Value
            Me.txtType.Text = .Cells(2).Value
        End With
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Call Koneksi()
        Dim edit As String = "update company2 set companies='" & txtCompany.Text & "',tipe='" & txtType.Text & "' where id='" & txtNo.Text & "'"
        CMD = New OleDbCommand(edit, Con)
        CMD.ExecuteNonQuery()
        MsgBox("Data updated successfully.")
        Call Initial()
    End Sub

    Private Sub txtInput_TextChanged(sender As Object, e As EventArgs) Handles txtInput.TextChanged
        On Error GoTo salah
        Koneksi()
        Da = New OleDbDataAdapter("Select * from company2 where companies like '%" & txtInput.Text & "%'", Con)
        ds = New DataSet
        ds.Clear()
        Da.Fill(ds, "company2")
        DataGridView1.DataSource = (ds.Tables("company2"))
        DataGridView1.Columns(0).Width = 70
        DataGridView1.Columns(1).Width = 275
        DataGridView1.Columns(2).Width = 150
        'dgv.Columns(3).Width = 100
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.LightYellow
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        txtNo.Text = ""
        txtCompany.Text = ""
        txtType.Text = ""
        Exit Sub
salah:
        MsgBox("Check inputted data", vbInformation, "Information")
        txtInput.Text = ""
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        txtCompany.Text = ""
        txtType.Text = ""

        txtCompany.Focus()
    End Sub

    Sub AutoNumber()
        Call Koneksi()
        CMD = New OleDbCommand("Select id from company2 order by val(id) desc", Con)
        rd = CMD.ExecuteReader
        rd.Read()

        If Not rd.HasRows Then
            txtNo.Text = "1"
        Else
            txtNo.Text = Val(rd.Item("id")) + 1
        End If

        txtNo.Enabled = False
    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Excel/CSV Files|*.xls;*.xlsx;*.csv"
        If ofd.ShowDialog() = DialogResult.OK Then
            ImportExcelToAccess(ofd.FileName)
            Call Initial()
        End If
    End Sub

    Private Sub btnBulkDelete_Click(sender As Object, e As EventArgs) Handles btnBulkDelete.Click
        Dim Tanya As DialogResult = MessageBox.Show("Apakah Anda yakin ingin MENGHAPUS SEMUA DATA?",
                                                    "Konfirmasi",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning)

        If Tanya = DialogResult.Yes Then
            If MessageBox.Show("Data yang dihapus tidak bisa dikembalikan. Lanjutkan?", "Confirmation",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = DialogResult.Yes Then
                Try
                    Call Koneksi()
                    Dim hapusSemua As String = "DELETE * FROM company2"

                    Using cmd As New OleDbCommand(hapusSemua, Con)
                        cmd.ExecuteNonQuery()
                    End Using

                    MsgBox("Seluruh data telah berhasil dikosongkan!", MsgBoxStyle.Information)

                    Call Initial()

                Catch ex As Exception
                    MsgBox("Gagal menghapus data: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
        End If
    End Sub
End Class
