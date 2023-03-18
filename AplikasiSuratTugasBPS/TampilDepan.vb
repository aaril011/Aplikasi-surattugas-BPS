
Public Class TampilDepan
    Public tampiltahun As String = ""
    Sub tampilchart()
        tampiltahun = Format(Today(), "yyyy")
        Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select concat(month(TanggalBerangkat),'/',Year(TanggalBerangkat)) as Bulan, count(NomorSurat) as Total from SuratTugas2 where Year(TanggalBerangkat)='" & tampiltahun & "' group by concat(month(TanggalBerangkat),'/',year(TanggalBerangkat))", conn)
            ds = New DataSet
            adapter.Fill(ds, "SuratTugas2")
            Chart1.Titles.Add("Surat Jadi")
            Chart1.Series("Surat Jadi").XValueMember = "Bulan"
            Chart1.Series("Surat Jadi").YValueMembers = "Total"
            Chart1.DataSource = ds.Tables("SuratTugas2")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TampilDepan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ambilkoneksi()
        Call tampilchart()
    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs)
        ' Call tampilchartsurattugas()
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            Me.Dispose()
    End Sub

    Private Sub Chart1_Click_1(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub
End Class