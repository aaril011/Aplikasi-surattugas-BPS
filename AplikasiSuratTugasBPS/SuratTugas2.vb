Imports CrystalDecisions.Shared
Public Class SuratTugas2
    Public NamaBulan As String() = {"Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember"}
    Public RomawiBulan As String() = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"}
    Public WilayahTugas As String = ""
    Public kode As String = ""

    Sub tampil()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from SuratTugas2", conn)
            ds = New DataSet
            adapter.Fill(ds, "SuratTugas2")
            DataGridView1.DataSource = ds.Tables("SuratTugas2")
        Catch ex As Exception
        End Try
    End Sub
    Sub kodeotomatis()
        TextBox2.Text = "7322/ST/" & RomawiBulan(Now.Month - 1) & "/" & Now.Year

    End Sub
    Sub caridata()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from SuratTugas2 where NomorSurat like'%" &
                                                   TextBox6.Text & "%' or Nama like '%" & TextBox6.Text & "%'", conn)
            ds = New DataSet
            adapter.Fill(ds, "SuratTugas2")
            DataGridView1.DataSource = ds.Tables("SuratTugas2")
        Catch ex As Exception
        End Try
    End Sub
    Sub urutasc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from SuratTugas2 order by NomorSurat asc", conn)
            ds = New DataSet
            adapter.Fill(ds, "SuratTugas2")
            DataGridView1.DataSource = ds.Tables("SuratTugas2")
        Catch ex As Exception
        End Try
    End Sub
    Sub urutdesc()
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from SuratTugas2 order by NomorSurat desc", conn)
            ds = New DataSet
            adapter.Fill(ds, "SuratTugas2")
            DataGridView1.DataSource = ds.Tables("SuratTugas2")
        Catch ex As Exception
        End Try
    End Sub
 
    Sub tampilwilayah()
        Try
            Call ambilkoneksi()
            strsql = "select*from WilayahTugas where KodeWilayah='" & ComboBox1.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                ComboBox1.Text = rd("NamaWilayah")
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub cetak()
        Try
            Dim rpt As New LpSuratTugas2
            Dim a As New LapSuratTugas2
            a.CrystalReportViewer1.ReportSource = rpt
            a.CrystalReportViewer1.SelectionFormula = "{viewsurattugas21.NomorSurat}='" & TextBox1.Text + TextBox2.Text & "'"
            a.CrystalReportViewer1.RefreshReport()
            a.WindowState = FormWindowState.Maximized
            a.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Sub cetak1()
        Try
            Dim rpt As New LpSuratTugas2
            Dim a As New LapSuratTugas2
            a.CrystalReportViewer1.ReportSource = rpt
            a.CrystalReportViewer1.SelectionFormula = "{viewsurattugas21.NomorSurat}='" & kode & "'"
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
            ComboBox1.Items.Add(rd("NamaWilayah"))
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
        DateTimePicker1.Format = DateTimePickerFormat.Long
        DateTimePicker1.Text = Format(Today())
        DateTimePicker2.Format = DateTimePickerFormat.Long
        DateTimePicker2.Text = Format(Today())
        ComboBox1.Text = ""
        ComboBox1.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = True
    End Sub
    Sub proses1()

        TextBox1.Enabled = True
        TextBox2.Enabled = False
        ComboBox1.Text = ""
        ComboBox1.Enabled = True
        TextBox3.Text = ""
        TextBox3.Enabled = True
        DateTimePicker1.Format = DateTimePickerFormat.Long
        DateTimePicker1.Text = Format(Today())
        DateTimePicker2.Format = DateTimePickerFormat.Long
        DateTimePicker2.Text = Format(Today())
        Button3.Enabled = False
        Button4.Enabled = True
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = True
    End Sub

    Private Sub SuratTugas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ambilkoneksi()
        Call tampil()
        Call proses()
        Call combowilayah()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim tanggal As String = Format(CDate(DateTimePicker1.Value.ToString), "yyyy-MM-dd")
        Dim tanggal1 As String = Format(CDate(DateTimePicker2.Value.ToString), "yyyy-MM-dd")
        If kode = "" Or TextBox3.Text = "" Or tanggal = "" Or tanggal1 = "" Or ComboBox1.Text = "" Then
            MsgBox("data tidak boleh kosong", MsgBoxStyle.Information, "informasi")
            Exit Sub
        Else
            Try
                strsql = "update SuratTugas2 set Nama='" & TextBox3.Text & "'," &
                   "KodeWilayah='" & WilayahTugas & "'," &
                   "TanggalBerangkat='" & tanggal & "'," &
                   "TanggalKembali='" & tanggal1 & "'" &
                   "where NomorSurat='" & kode & "'"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil diproses", MsgBoxStyle.Information, "informasi")
                Call tampil()
                Call cetak1()
                Call proses()
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
        Dim a As String
        a = MsgBox("anda ingin menghapus data ini?", MsgBoxStyle.YesNo, "Konfirmasi")
        If a = vbYes Then
            strsql = "delete from surattugas2 where NomorSurat='" & kode & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            cmd.ExecuteNonQuery()
            Call tampil()
            Call proses()
            MsgBox("data berhasil dihapus", MsgBoxStyle.Information, "informasi")
        Else
            Call tampil()
            Call proses()
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim tanggal As String = Format(CDate(DateTimePicker1.Value.ToString), "yyyy-MM-dd")
        Dim tanggal1 As String = Format(CDate(DateTimePicker2.Value.ToString), "yyyy-MM-dd")
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or tanggal = "" Or tanggal1 = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            Try
                strsql = "insert into SuratTugas2 values('" & TextBox1.Text + TextBox2.Text & "','" & TextBox3.Text & "','" & WilayahTugas & "','" & tanggal & "','" & tanggal1 & "')"
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Call ambilkoneksi()
            strsql = "select*from WilayahTugas where NamaWilayah='" & ComboBox1.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                WilayahTugas = rd("KodeWilayah")
                DateTimePicker1.Focus()
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellMouseClick1(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim tanggal As String = Format(CDate(DateTimePicker1.Value.ToString), "yyyy-MM-dd")
        Dim tanggal1 As String = Format(CDate(DateTimePicker2.Value.ToString), "yyyy-MM-dd")
        Try
            Call proses1()
            kode = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox1.Text = Microsoft.VisualBasic.Left(DataGridView1.Rows(e.RowIndex).Cells(0).Value, 3)
            TextBox2.Text = Microsoft.VisualBasic.Right(DataGridView1.Rows(e.RowIndex).Cells(0).Value, 16)
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            tanggal = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            tanggal1 = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            Call tampilwilayah()
            Button3.Enabled = False
            Button5.Enabled = True
            Button4.Enabled = False
            Button6.Enabled = True
            Button7.Enabled = True
        Catch ex As Exception
            MsgBox("Data Yang Dipilih Tidak Ada", MsgBoxStyle.Information, "Informasi")
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Call ambilkoneksi()
            strsql = "select*from WilayahTugas where NamaWilayah='" & ComboBox1.Text & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                WilayahTugas = rd("KodeWilayah")
                TextBox1.Focus()
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Call caridata()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Call urutasc()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Call urutdesc()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.Dispose()
    End Sub
End Class