Public Class WilayahTugas
    Sub tampil()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from WilayahTugas", conn)
            ds = New DataSet
            adapter.Fill(ds, "WilayahTugas")
            DataGridView1.DataSource = ds.Tables("WilayahTugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub caridata()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from WilayahTugas where KodeWilayah like'%" &
                                                   TextBox1.Text & "%' or NamaWilayah like '%" & TextBox1.Text & "%'", conn)
            ds = New DataSet
            adapter.Fill(ds, "WilayahTugas")
            DataGridView1.DataSource = ds.Tables("WilayahTugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub urutasc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from WilayahTugas order by KodeWilayah asc", conn)
            ds = New DataSet
            adapter.Fill(ds, "WilayahTugas")
            DataGridView1.DataSource = ds.Tables("WilayahTugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub urutdesc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from WilayahTugas order by KodeWilayah desc", conn)
            ds = New DataSet
            adapter.Fill(ds, "WilayahTugas")
            DataGridView1.DataSource = ds.Tables("WilayahTugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub kodeotomatis()
        Try
            Dim strSementara As String = ""
            Dim strIsi As String = ""
            cmd = New MySql.Data.MySqlClient.MySqlCommand("select KodeWilayah from WilayahTugas where KodeWilayah in(select max(KodeWilayah) from WilayahTugas)", conn)
            rd = Cmd.ExecuteReader
            If rd.Read Then
                strSementara = Microsoft.VisualBasic.Right(rd("KodeWilayah"), 4) + 1
                strIsi = Val(strSementara) + 1
                TextBox2.Text = Microsoft.VisualBasic.Right("0000" & strSementara, 4)
            Else
                TextBox2.Text = "0001"
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub proses()
        TextBox1.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox2.Text = ""
        TextBox2.Enabled = False
        TextBox3.Text = ""
        TextBox3.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = True
        Button6.Enabled = True
    End Sub
    Sub proses1()
        TextBox2.Enabled = False
        TextBox3.Text = ""
        TextBox3.Enabled = True
        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = True
        Button6.Enabled = True
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Call urutasc()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call caridata()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Call urutdesc()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ambilkoneksi()
        Call tampil()
        Call proses()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call proses1()
        Call kodeotomatis()
        TextBox3.Focus()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox3.Text = "" Then
            MsgBox("data tidak boleh kosong", MsgBoxStyle.Information, "informasi")
            Exit Sub
        Else
            Try
                strsql = "insert into WilayahTugas values('" & TextBox2.Text & "','" &
                                                        TextBox3.Text & "')"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                Cmd.ExecuteNonQuery()
                MsgBox("data berhasil diproses", MsgBoxStyle.Information, "informasi")
                Call tampil()
                Call proses()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "error")
            End Try
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox3.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", MsgBoxStyle.Information, "Information")
            Exit Sub
        Else
            Try
                strsql = "update WilayahTugas Set NamaWilayah='" & TextBox3.Text & "'" &
                "where KodeWilayah='" & TextBox2.Text & "'"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil diproses", MsgBoxStyle.Information, "Informasi")
                Call tampil()
                Call proses()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim a As String
        a = MsgBox("Anda Ingin Menghapus Data Ini", MsgBoxStyle.YesNo, "Konfirmasi")
        If a = vbYes Then
            strsql = "delete from WilayahTugas where KodeWilayah='" & TextBox2.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            Cmd.ExecuteNonQuery()
            Call tampil()
            Call proses()
            MsgBox("Data Berhasil dihapus", MsgBoxStyle.Information, "Informasi")
        Else
            Call tampil()
            Call proses()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call tampil()
        Call proses()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim a As String
        a = MsgBox("Apakah Anda Ingin Keluar", MsgBoxStyle.YesNo, "informasi")
        If a = vbYes Then
            MsgBox("Anda Berhasil Keluar", MsgBoxStyle.Information, "informasi")
            Me.Dispose()
        Else
            Call proses()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            Call proses1()
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
        Catch ex As Exception
            MsgBox("Data yang Dipilih tidak ada", MsgBoxStyle.Information, "Informasi")
        End Try
    End Sub
End Class










