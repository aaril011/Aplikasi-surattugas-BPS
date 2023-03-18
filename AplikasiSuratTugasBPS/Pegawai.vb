Public Class Pegawai

    Public id As String
    Sub tampil()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select IdPegawai,NIPPegawai,NamaPegawai,Alamat,Telepon,Email,Status from Pegawai", conn)
            ds = New DataSet
            adapter.Fill(ds, "Pegawai")
            DataGridView1.DataSource = ds.Tables("Pegawai")
        Catch ex As Exception

        End Try
    End Sub
    Sub caridata()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select IdPegawai,NIPPegawai,NamaPegawai,Alamat,Telepon,Email,Statusfrom Pegawai where IdPegawai like'%" &
                                                   TextBox1.Text & "%' or NamaPegawai like '%" & TextBox1.Text & "%'", conn)
            ds = New DataSet
            adapter.Fill(ds, "Pegawai")
            DataGridView1.DataSource = ds.Tables("Pegawai")
        Catch ex As Exception

        End Try
    End Sub
    Sub urutasc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select IdPegawai,NIPPegawai,NamaPegawai,Alamat,Telepon,Email,Status from Pegawai order by IdPegawai asc", conn)
            ds = New DataSet
            adapter.Fill(ds, "Pegawai")
            DataGridView1.DataSource = ds.Tables("Pegawai")
        Catch ex As Exception

        End Try
    End Sub
    Sub urutdesc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select IdPegawai,NIPPegawai,NamaPegawai,Alamat,Telepon,Email,Statusfrom Pegawai order by IdPegawai desc", conn)
            ds = New DataSet
            adapter.Fill(ds, "Pegawai")
            DataGridView1.DataSource = ds.Tables("Pegawai")
        Catch ex As Exception

        End Try
    End Sub
    Sub proses()
        TextBox1.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = True
        Button5.Enabled = True
    End Sub

    Private Sub User_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ambilkoneksi()
        Call tampil()
        Call proses()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call caridata()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Call urutdesc()
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            InputPegawai.id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            InputPegawai.TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            InputPegawai.TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            InputPegawai.TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            InputPegawai.TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            InputPegawai.TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            InputPegawai.ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value

            Button1.Enabled = False
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
        Catch ex As Exception
            MsgBox("Data yang Dipilih tidak ada", MsgBoxStyle.Information, "Informasi")
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub RadioButton1_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Call urutasc()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        InputPegawai.Show()
        InputPegawai.proses()
        InputPegawai.status = "Simpan"
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If InputPegawai.id = "" Then
            MsgBox("Pilih Data Yang Ingin Di Update atau Di Hapus", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            InputPegawai.Show()
            InputPegawai.status = "Edit"
        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If id = "" Then
            MsgBox("Pilih Data Yang Ingin Di Update atau Di Hapus", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            Dim a As String
            a = MsgBox("Apakah Anda Akan Menghapus Data Dengan Kode : " & id, MsgBoxStyle.YesNo, "Konfirmasi")
            If a = vbYes Then
                strsql = "delete from Pegawai where IdPegawai='" & id & "'"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
                Call tampil()
                Call proses()
                MsgBox("Data Berhasil dihapus", MsgBoxStyle.Information, "Informasi")
            Else
                Call tampil()
                Call proses()
            End If
        End If
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Call tampil()
        Call proses()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Dim a As String
        a = MsgBox("Apakah Anda Ingin Keluar?", MsgBoxStyle.YesNo, "Informasi")
        If a = vbYes Then
            MsgBox("Anda Berhasil Keluar", MsgBoxStyle.Information, "informasi")
            Me.Dispose()
        Else
            Call proses()
        End If
    End Sub
End Class