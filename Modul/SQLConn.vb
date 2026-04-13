Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Module SQLConn
    Public ServerMySQL As String
    Public PortMySQL As String
    Public UserNameMySQL As String
    Public PwdMySQL As String
    Public DBNameMySQL As String

    Public sqL As String
    Public ds As New DataSet
    Public Con As OleDbConnection
    Public Connx As OleDbConnection
    Public Conn As OleDbConnection
    Public dax As OleDbDataAdapter
    Public cmdx As OleDbCommand
    Public rdx As OleDbDataReader
    Public rd As OleDbDataReader
    Public drx As OleDbDataReader
    Public lokasidb As String

    '-----------------------------------------------------
    Public cmdcccccccc As MySqlCommand
    Public drccccccccc As MySqlDataReader
    Public daccccccccc As MySqlDataAdapter
    Public connccccccccc As New MySqlConnection
    '------------------------------------------------------
    Public strSQL As String '// Major SQL
    Public strStmt As String    '// Minor SQL

    '// Data Path 
    Public strPathData As String = MyPath(Application.StartupPath)
    '// Images Path
    Public strPathImages As String = MyPath(Application.StartupPath)
    Function MyPath(AppPath As String) As String
        '/ MessageBox.Show(AppPath);
        AppPath = AppPath.ToLower()
        '/ Return Value
        MyPath = AppPath.Replace("\bin\debug", "\").Replace("\bin\release", "\")
        '// If not found folder then put the \ (BackSlash) at the end.
        If Right(MyPath, 1) <> "\" Then MyPath = MyPath & "\"
    End Function

    Public Sub Koneksi()
        lokasidb = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "/undian.mdb"
        'lokasidb = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=undian.mdb"
        Con = New OleDbConnection(lokasidb)
        If Con.State = ConnectionState.Closed Then Con.Open()
    End Sub
    Public Function ConString() As String
        ConString = ("Provider = Microsoft.ACE.OLEDB.12.0;" &
        "Data Source=" & Application.StartupPath & "/undian.mdb")
    End Function

    Sub getData()
        Dim AppName As String = Application.ProductName

        Try
            DBNameMySQL = GetSetting(AppName, "DBSection", "DB_Name", "temp")
            ServerMySQL = GetSetting(AppName, "DBSection", "DB_IP", "temp")
            PortMySQL = GetSetting(AppName, "DBSection", "DB_Port", "temp")
            UserNameMySQL = GetSetting(AppName, "DBSection", "DB_User", "temp")
            PwdMySQL = GetSetting(AppName, "DBSection", "DB_Password", "temp")
        Catch ex As Exception
            MsgBox("System registry was not established, you can set/save " & _
            "these settings by pressing F1", MsgBoxStyle.Information)
        End Try

    End Sub

    Public Sub ConnDB()

        'conn.Close()
        '
        '        Try
        '        conn.ConnectionString = "Server = '" & ServerMySQL & "';  " _
        '                                         & "Port = '" & PortMySQL & "'; " _
        '                                         & "Database = '" & DBNameMySQL & "'; " _
        '                                         & "user id = '" & UserNameMySQL & "'; " _
        '                                         & "password = '" & PwdMySQL & "'"
        '
        '        conn.Open()
        '
        '
        '        Catch ex As Exception
        ''        Try
        '       conn = New MySqlConnection("datasource = localhost; port=3306; username=root; password=; database=ardatabase;")
        '      conn.Open()
        '     Catch
        '    MsgBox("Please configure database.", MsgBoxStyle.Information, "Database")
        '   End Try
        '
        ' MsgBox("The system failed to establish a connection", MsgBoxStyle.Information, "Database Settings")
        'End Try

    End Sub


    Public Function SQLTable(ByVal Source As String) As DataTable
        Try
            Dim Adp As New OleDb.OleDbDataAdapter(Source, ConString)
            Dim DT As New DataTable

            Adp.Fill(DT)
            SQLTable = DT
        Catch ex As OleDb.OleDbException
            MsgBox(ex.Message)
            SQLTable = Nothing
        End Try
    End Function
    Public Sub DisconnMy()

        'conn.Close()
        'conn.Dispose()

    End Sub

    Sub SaveData()
        Dim AppName As String = Application.ProductName

        SaveSetting(AppName, "DBSection", "DB_Name", DBNameMySQL)
        SaveSetting(AppName, "DBSection", "DB_IP", ServerMySQL)
        SaveSetting(AppName, "DBSection", "DB_Port", PortMySQL)
        SaveSetting(AppName, "DBSection", "DB_User", UserNameMySQL)
        SaveSetting(AppName, "DBSection", "DB_Password", PwdMySQL)

        MsgBox("Database connection settings are saved.", MsgBoxStyle.Information)
    End Sub


    Public Sub Fadein()
        Dim fade As Double
        For fade = 0.0 To 1.1 Step 0.05
            'frmSplashScreen.Opacity = fade
            'frmSplashScreen.Refresh()
            Threading.Thread.Sleep(100)
        Next

    End Sub

    Public Sub ImportExcelToAccess(ByVal filePath As String)
        Try
            Dim dtExcel As New DataTable
            Dim connStringExcel As String = ""
            Dim extension As String = IO.Path.GetExtension(filePath).ToLower()
            If Con.State = ConnectionState.Open Then Con.Close()
            ' Pengaturan Koneksi Excel/CSV
            If extension = ".csv" Then
                connStringExcel = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & IO.Path.GetDirectoryName(filePath) & ";Extended Properties='Text;HDR=YES;FMT=Delimited'"
                Using connCSV As New OleDbConnection(connStringExcel)
                    Dim da As New OleDbDataAdapter("SELECT * FROM [" & IO.Path.GetFileName(filePath) & "]", connCSV)
                    da.Fill(dtExcel)
                End Using
            Else
                ' Untuk .xls atau .xlsx
                connStringExcel = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties='Excel 12.0 Xml;HDR=YES;'"
                Using connExcel As New OleDbConnection(connStringExcel)
                    Dim da As New OleDbDataAdapter("SELECT * FROM [Sheet1$]", connExcel)
                    da.Fill(dtExcel)
                End Using
            End If

            ' Proses Pemindahan Data ke Access
            If dtExcel.Rows.Count > 0 Then
                Koneksi()
                Dim sukses As Integer = 0
                For Each row As DataRow In dtExcel.Rows
                    ' Logika AutoNumber manual (Ambil ID terakhir + 1)
                    Dim cmdId As New OleDbCommand("SELECT MAX(VAL(id)) FROM company2", Con)
                    Dim lastId As Object = cmdId.ExecuteScalar()
                    Dim newId As Integer = If(IsDBNull(lastId), 1, Convert.ToInt32(lastId) + 1)

                    ' Insert ke Access
                    Dim sqlInsert As String = "INSERT INTO company2 (id, companies, tipe) VALUES (?, ?, ?)"
                    Using cmd As New OleDbCommand(sqlInsert, Con)
                        cmd.Parameters.AddWithValue("?", newId.ToString())
                        cmd.Parameters.AddWithValue("?", row(0).ToString()) ' Kolom 1 di Excel
                        cmd.Parameters.AddWithValue("?", row(1).ToString()) ' Kolom 2 di Excel
                        cmd.ExecuteNonQuery()
                    End Using
                    sukses += 1
                Next
                MsgBox(sukses & " Data berhasil diimport!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Proses Import Gagal: " & ex.Message)
        End Try
    End Sub
End Module

