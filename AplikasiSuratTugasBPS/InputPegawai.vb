Public Class InputPegawai
    Public status As String
    Public id As String = ""
    Sub simpanpegawai()
        Try
            strsql = "insert into Pegawai values('" & id & "','" & TextBox1.Text & "','" &
                    TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & ComboBox1.Text & "')"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil Diproses", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Sub editpegawai()
        Try
            strsql = "update Pegawai set NIPPegawai='" & TextBox1.Text & "'," &
                    "NamaPegawai='" & TextBox2.Text & "'," &
                    "Alamat='" & TextBox3.Text & "'," &
                    "Telepon='" & TextBox4.Text & "'," &
                    "Email='" & TextBox5.Text & "'," &
                    "Status='" & ComboBox1.Text & "'" &
                    "Where IdPegawai='" & id & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil Diproses", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
        Sub proses()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
        End Sub
        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            If status = "Simpan" Then
                Call simpanpegawai()
                Me.Dispose()
                Pegawai.tampil()
                Pegawai.proses()
            ElseIf status = "Edit" Then
                Call editpegawai()
                Me.Dispose()
                Pegawai.tampil()
                Pegawai.proses()
            End If
        End If
        End Sub
        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            Dim a As String
        a = MsgBox("Apakah Anda Ingin Keluar?", MsgBoxStyle.YesNo, "Informasi")
            If a = vbYes Then
                MsgBox("Anda Berhasil Keluar", MsgBoxStyle.Information, "Informasi")
                Me.Dispose()
            Pegawai.tampil()
            Pegawai.proses()
            Else
                Call proses()
            End If
        End Sub

    Private Sub InputPegawai_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class