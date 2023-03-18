Public Class MenuUtama
        Public namauser As String
        Sub proseslogout()
            ToolStripMenuItem1.Visible = True
            ToolStripMenuItem1.Enabled = True
            ToolStripMenuItem2.Enabled = True
            ToolStripMenuItem3.Enabled = False
            ToolStripMenuItem4.Enabled = True
        ToolStripMenuItem5.Visible = False
        ToolStripMenuItem7.Visible = False
        ToolStripMenuItem9.Visible = True
        ToolStripMenuItem10.Visible = True
            ToolStripStatusLabel2.Text = ""
            ToolStripStatusLabel3.Text = ""
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        GroupBox3.Visible = False
        GroupBox4.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
        End Sub
        Sub loginadmin()
            ToolStripMenuItem1.Visible = True
            ToolStripMenuItem1.Enabled = True
            ToolStripMenuItem2.Enabled = False
            ToolStripMenuItem3.Enabled = True
        ToolStripMenuItem4.Enabled = True
        ToolStripMenuItem7.Visible = True
        ToolStripMenuItem5.Visible = True
        ToolStripMenuItem9.Visible = True
        ToolStripMenuItem10.Visible = True
        GroupBox1.Visible = True
        GroupBox2.Visible = True
        GroupBox3.Visible = True
        GroupBox4.Visible = True
            Button1.Visible = True
            Button2.Visible = True
            Button3.Visible = True
        End Sub
        Sub loginuser()
            ToolStripMenuItem1.Visible = True
            ToolStripMenuItem1.Enabled = True
            ToolStripMenuItem2.Enabled = False
            ToolStripMenuItem3.Enabled = True
            ToolStripMenuItem4.Enabled = True
        ToolStripMenuItem5.Visible = True
        ToolStripMenuItem7.Visible = False
        ToolStripMenuItem9.Visible = False
        ToolStripMenuItem10.Visible = False
        GroupBox1.Visible = True
        GroupBox2.Visible = True
        GroupBox3.Visible = True
        GroupBox4.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
    End Sub
    Sub hitungpegawai()
        Try
            Call ambilkoneksi()
            strsql = "Select count(IdPegawai) AS TotalPegawai FROM Pegawai"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                Label2.Text = rd("TotalPegawai")
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub hitungsurat()
        Try
            Call ambilkoneksi()
            strsql = "Select count(NomorSurat) AS TotalSurat FROM SuratTugas"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                Label1.Text = rd("TotalSurat")
            End If
            rd.Close()
        Catch ex As Exception

        End Try
    End Sub
        Private Sub MenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Call proseslogout()
            ToolStripStatusLabel4.Text = Format(Today(), "MM/dd/yyyy")
        ToolStripStatusLabel5.Text = Now.ToString("hh:mm:ss tt")
        Call hitungpegawai()
        Call hitungsurat()
        End Sub
        Private Sub FormUtama(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            Dim result As MsgBoxResult = MsgBox("Apakah Anda Ingin Keluar ?", MsgBoxStyle.Information + vbYesNo, "Keluar")
            If result = MsgBoxResult.Ok Or result = MsgBoxResult.Yes Then
            MsgBox("Anda Berhasil Keluar", MsgBoxStyle.Information, "Informasi")
                End
            Else
                e.Cancel = True
            End If
        End Sub

        Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
            MsgBox("Logout Dari Aplikasi Berhasil", MsgBoxStyle.Information, "Informasi")

            Call proseslogout()
        End Sub

        Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
            Dim a As String
            a = MsgBox("Apakah Anda Ingin Keluar ?", MsgBoxStyle.YesNo, "Konfirmasi")
            If a = vbYes Then
                MsgBox("Anda Berhasil Keluar", MsgBoxStyle.Information, "Informasi")
                End
            Else
                Me.Show()
            End If
        End Sub

        Private Sub Timer1_Tick_2(sender As Object, e As EventArgs) Handles Timer1.Tick
            ToolStripStatusLabel5.Text = Now.ToString("hh:mm:ss tt")
        End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Login.Show()
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        SuratTugas.Show()
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.Click
        FormUser.Show()
    End Sub

    Private Sub ToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem10.Click
        AboutProgram.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Jabatan.Show()
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Pegawai.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TampilDepan.TopLevel = False
        TampilDepan.Parent = Me.PictureBox2
        TampilDepan.Show()
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        WilayahTugas.Show()
    End Sub
End Class