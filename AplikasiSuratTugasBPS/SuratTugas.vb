Imports CrystalDecisions.Shared
Public Class SuratTugas
    Public NamaBulan As String() = {"Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember"}
    Public RomawiBulan As String() = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"}
    Public WilayahTugas As String = ""
    Public idjabatan As String = ""
    Public kode As String = ""

    Sub tampil()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from surattugas", conn)
            ds = New DataSet
            adapter.Fill(ds, "Surattugas")
            DataGridView1.DataSource = ds.Tables("surattugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub caridata()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from surattugas where NomorSurat like'%" &
                                                   TextBox9.Text & "%' or Nama like '%" & TextBox9.Text & "%'", conn)
            ds = New DataSet
            adapter.Fill(ds, "surattugas")
            DataGridView1.DataSource = ds.Tables("surattugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub urutasc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from surattugas order by NomorSurat asc", conn)
            ds = New DataSet
            adapter.Fill(ds, "surattugas")
            DataGridView1.DataSource = ds.Tables("surattugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub urutdesc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from surattugas order by NomorSurat desc", conn)
            ds = New DataSet
            adapter.Fill(ds, "surattugas")
            DataGridView1.DataSource = ds.Tables("surattugas")
        Catch ex As Exception
        End Try
    End Sub
    Sub tampiljabatan()
        Try
            Call ambilkoneksi()
            strsql = "select*from Jabatan where IdJabatan='" & ComboBox1.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                ComboBox1.Text = rd("Jabatan")
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub tampilwilayah()
        Try
            Call ambilkoneksi()
            strsql = "select*from WilayahTugas where KodeWilayah='" & ComboBox2.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                ComboBox2.Text = rd("NamaWilayah")
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub kodeotomatis()
        TextBox2.Text = "7322/ST/" & RomawiBulan(Now.Month - 1) & "/" & Now.Year
       
    End Sub
    Sub cetak()
        Try
            Dim rpt As New LpSuratTugas
            Dim a As New LapSuratTugas
            a.CrystalReportViewer1.ReportSource = rpt
            a.CrystalReportViewer1.SelectionFormula = "{viewsurattugas1.nomorsurat}='" & TextBox1.Text + TextBox2.Text & "'"
            a.CrystalReportViewer1.RefreshReport()
            a.WindowState = FormWindowState.Maximized
            a.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Sub cetak1()
        Try
            Dim rpt As New LpSuratTugas
            Dim a As New LapSuratTugas
            a.CrystalReportViewer1.ReportSource = rpt
            a.CrystalReportViewer1.SelectionFormula = "{viewsurattugas1.nomorsurat}='" & kode & "'"
            a.CrystalReportViewer1.RefreshReport()
            a.WindowState = FormWindowState.Maximized
            a.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Sub combowilayah()
        Call ambilkoneksi()
        strsql = "select * from WilayahTugas"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
        rd = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While rd.Read
            ComboBox2.Items.Add(rd("NamaWilayah"))
        Loop
        rd.Close()
    End Sub
    Sub combojabatan()
        Call ambilkoneksi()
        strsql = "select * from Jabatan"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
        rd = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While rd.Read
            ComboBox1.Items.Add(rd("Jabatan"))
        Loop
        rd.Close()
    End Sub
    Sub proses()
        TextBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Text = ""
        TextBox2.Enabled = False
        TextBox3.Text = ""
        TextBox3.Enabled = False
        TextBox4.Text = ""
        TextBox4.Enabled = False
        TextBox5.Text = ""
        TextBox5.Enabled = False
        TextBox6.Text = ""
        TextBox6.Enabled = False
        ComboBox1.Text = ""
        ComboBox1.Enabled = False
        ComboBox2.Text = ""
        ComboBox2.Enabled = False
        Button1.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = True
        Button8.Enabled = True
    End Sub
    Sub proses1()

        TextBox1.Enabled = True
        TextBox2.Enabled = False
        ComboBox1.Text = ""
        ComboBox1.Enabled = True
        ComboBox2.Text = ""
        ComboBox2.Enabled = True
        TextBox3.Text = ""
        TextBox3.Enabled = True
        TextBox4.Text = ""
        TextBox4.Enabled = True
        TextBox5.Text = ""
        TextBox5.Enabled = True
        TextBox6.Text = ""
        TextBox6.Enabled = True
        Button1.Enabled = True
        Button3.Enabled = False
        Button4.Enabled = True
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = True
        Button8.Enabled = True
    End Sub

    Private Sub SuratTugas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ambilkoneksi()
        Call tampil()
        Call proses()
        Call combowilayah()
        Call combojabatan()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim a As String
        a = MsgBox("apakah anda ingin keluar ?", MsgBoxStyle.YesNo, "konfirmasi")
        If a = vbYes Then
            MsgBox("anda berhasil keluar", MsgBoxStyle.Information, "informasi")
            Me.Dispose()
        Else
            Call proses()
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SuratTugas2.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If kode = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Or TextBox4.Text = "" Or ComboBox2.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("data tidak boleh kosong", MsgBoxStyle.Information, "informasi")
            Exit Sub
        Else
            Try
                strsql = "update surattugas set Nama ='" & TextBox3.Text & "'," &
                    "IdJabatan='" & idjabatan & "'," &
                    "Anggota='" & TextBox4.Text & "'," &
                    "Tugas='" & TextBox5.Text & "'," &
                    "KodeWilayah='" & WilayahTugas & "'," &
                    "JangkaWaktu='" & TextBox6.Text & "'" &
                    "where NomorSurat ='" & kode & "'"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil diproses", MsgBoxStyle.Information, "informasi")
                Call tampil()
                Call proses()
                Call cetak1()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call proses1()
        Call kodeotomatis()
        TextBox1.Focus()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If kode = "" Then
            MsgBox("Pilih Data Yang Ingin Di Update atau Di Hapus", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            Dim a As String
            a = MsgBox("Apakah Anda Akan Menghapus Data Dengan Kode : " & kode, MsgBoxStyle.YesNo, "Konfirmasi")
            If a = vbYes Then
                strsql = "delete from surattugas where NomorSurat='" & kode & "'"
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            Try
                strsql = "insert into surattugas values('" & TextBox1.Text + TextBox2.Text & "','" & TextBox3.Text & "','" & idjabatan & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & WilayahTugas & "','" & TextBox6.Text & "')"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Diproses", MsgBoxStyle.Information, "Informasi")
                Call tampil()
                Call cetak()
                Call proses()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Call tampil()
        Call proses()
    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            Call proses1()
            kode = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox1.Text = Microsoft.VisualBasic.Left(DataGridView1.Rows(e.RowIndex).Cells(0).Value, 3)
            TextBox2.Text = Microsoft.VisualBasic.Right(DataGridView1.Rows(e.RowIndex).Cells(0).Value, 16)
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            TextBox6.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
            Call tampiljabatan()
            Call tampilwilayah()
            Button1.Enabled = True
            Button3.Enabled = False
            Button5.Enabled = True
            Button4.Enabled = False
            Button6.Enabled = True
            Button7.Enabled = True
            Button8.Enabled = False
            TextBox1.Enabled = False
        Catch ex As Exception
            MsgBox("Data Yang Dipilih Tidak Ada", MsgBoxStyle.Information, "Informasi")
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Call ambilkoneksi()
            strsql = "select*from Jabatan where Jabatan='" & ComboBox1.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                idjabatan = rd("idjabatan")
                TextBox4.Focus()
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            Call ambilkoneksi()
            strsql = "select*from WilayahTugas where NamaWilayah='" & ComboBox2.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                WilayahTugas = rd("KodeWilayah")
                TextBox6.Focus()
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Call caridata()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Call urutasc()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Call urutdesc()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox5.Text = (TextBox5.Text.ToUpper)
    End Sub
End Class