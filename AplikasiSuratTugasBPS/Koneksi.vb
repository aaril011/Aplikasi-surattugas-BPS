Imports MySql.Data.MySqlClient
Module koneksi
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public ds As DataSet
    Public rd As MySqlDataReader
    Public adapter As MySqlDataAdapter
    Public lokasidb As String
    Public strsql As String
    Public Sub ambilkoneksi()
        lokasidb = "Server=localhost;user id=royandi;password=;database=dbsurattugas"
        conn = New MySqlConnection(lokasidb)
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Public Sub jalankanSQL(ByVal sql As String)
        Dim objcmd As New MySqlCommand
        Try
            objcmd.Connection = conn
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = sql
            objcmd.ExecuteNonQuery()
            objcmd.Dispose()
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Module