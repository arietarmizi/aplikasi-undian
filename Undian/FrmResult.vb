Imports System.Data.OleDb

Public Class FrmResult
    Sub loadDataGP()
        Try
            Call Koneksi()
            If Con.State = ConnectionState.Closed Then Con.Open()

            ' Get data grandprize
            Dim query As String = "SELECT * FROM pemenang WHERE kategori LIKE '" & lblGrandprize.Text & "' ORDER BY Indeks DESC, ID DESC"
            Dim dax As New OleDbDataAdapter(query, Con)
            Dim dt As New DataTable()

            dax.Fill(dt)

            dgv2.DataSource = dt
            If dgv2.Columns.Count >= 6 Then
                dgv2.Columns(0).HeaderText = "ID"
                dgv2.Columns(0).Width = 50
                dgv2.Columns(0).Visible = False

                dgv2.Columns(1).HeaderText = "NAMA DISTRIBUTOR"
                dgv2.Columns(1).Width = 350

                dgv2.Columns(2).HeaderText = "TYPE"
                dgv2.Columns(2).Width = 100
                dgv2.Columns(2).Visible = False

                dgv2.Columns(3).HeaderText = "KATEGORI"
                dgv2.Columns(3).Width = 100
                dgv2.Columns(3).Visible = False

                dgv2.Columns(4).HeaderText = "HADIAH"
                dgv2.Columns(4).Width = 200

                dgv2.Columns(5).HeaderText = "PENGUNDIAN KE"
                dgv2.Columns(5).Width = 135
                'dgv2.Columns(5).Visible = False
            End If

            dgv2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            dgv2.AllowUserToAddRows = False
            dgv2.RowsDefaultCellStyle.BackColor = Color.LightYellow
            dgv2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            dgv2.Refresh()

        Catch ex As Exception
            MsgBox("Gagal memuat data Grandprize: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub loadDataGPbyIndeks(ByVal lastIndex As Integer)
        Try
            Call Koneksi()
            If Con.State = ConnectionState.Closed Then Con.Open()

            Dim query As String = "SELECT * FROM pemenang WHERE kategori LIKE '" & lblGrandprize.Text & "' AND Indeks = " & lastIndex & " ORDER BY ID DESC"
            Dim dax As New OleDbDataAdapter(query, Con)
            Dim dt As New DataTable()

            dax.Fill(dt)
            dgv2.DataSource = dt

            ' Format Header
            If dgv2.Columns.Count >= 6 Then
                dgv2.Columns(0).HeaderText = "ID"
                dgv2.Columns(0).Width = 50
                dgv2.Columns(0).Visible = False

                dgv2.Columns(1).HeaderText = "NAMA DISTRIBUTOR"
                dgv2.Columns(1).Width = 300

                dgv2.Columns(2).HeaderText = "TYPE"
                dgv2.Columns(2).Width = 100
                dgv2.Columns(2).Visible = False

                dgv2.Columns(3).HeaderText = "KATEGORI"
                dgv2.Columns(3).Width = 100
                dgv2.Columns(3).Visible = False

                dgv2.Columns(4).HeaderText = "HADIAH"
                dgv2.Columns(4).Width = 150

                dgv2.Columns(5).HeaderText = "INDEKS"
                dgv2.Columns(5).Width = 60
                dgv2.Columns(5).Visible = False
            End If

            ' Styling
            dgv2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            dgv2.AllowUserToAddRows = False
            dgv2.RowsDefaultCellStyle.BackColor = Color.LightYellow
            dgv2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            dgv2.Refresh()

        Catch ex As Exception
            MsgBox("Gagal memuat data pemenang terbaru: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub FrmResult_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'loadDataDP()
        loadDataGP()
        ' CheckboxColumnDP() 
    End Sub

    Private Sub BtnHapusGP_Click(sender As Object, e As EventArgs) Handles BtnHapusGP.Click
        Try
            If MessageBox.Show("Yakin akan Hapus Semua Data Grandprize?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Call Koneksi()
                Dim query As String = "DELETE FROM pemenang WHERE kategori LIKE ?"
                Using cmdx As New OleDbCommand(query, Con)
                    cmdx.Parameters.AddWithValue("?", lblGrandprize.Text)
                    cmdx.ExecuteNonQuery()
                End Using

                MsgBox("Data berhasil dihapus", MsgBoxStyle.Information, "Perhatian")
                Call loadDataGP()
            End If
        Catch ex As Exception
            MsgBox("Cek Kembali Inputan Anda: " & ex.Message, vbInformation, "Informasi")
        End Try
    End Sub

    Private Sub BtnShowAll_Click(sender As Object, e As EventArgs) Handles BtnShowAll.Click
        loadDataGP()
    End Sub
End Class