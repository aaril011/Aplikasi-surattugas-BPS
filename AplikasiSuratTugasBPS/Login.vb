Public Class Login
    Dim user, password, level As String
    Sub login()
        Try
            user = TextBox1.Text
            password = TextBox2.Text
            strsql = "select*from TbUser where UserName='" & user & "'AND Password='" & password & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
            rd = Cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                level = rd("Role")
                If level = "Admin" Then
                    MsgBox("Login Ke Aplikasi Berhasil", MsgBoxStyle.Information, "Informasi")
                    Me.Close()
                    MenuUtama.Show()
                    MenuUtama.loginadmin()
                    MenuUtama.ToolStripStatusLabel2.Visible = True
                    MenuUtama.ToolStripStatusLabel3.Visible = True
                    MenuUtama.ToolStripStatusLabel2.Text = "User Aktif" & " " & ":" & " " & rd("NamaUser")
                    MenuUtama.ToolStripStatusLabel3.Text = "Level" & " " & ":" & " " & rd("Role")
                    MenuUtama.GroupBox1.Visible = True
                    MenuUtama.Button1.Visible = True
                    MenuUtama.Label1.Text = rd("Foto")
                    MenuUtama.namauser = rd("NamaUser")
                    MenuUtama.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                    MenuUtama.PictureBox1.Image = Image.FromFile(MenuUtama.Label1.Text)
                    If MenuUtama.Label1.Text = "" Then
                        MenuUtama.PictureBox1.Image = Nothing
                    End If
                ElseIf level = "User" Then
                    MsgBox("Login Ke Aplikasi Berhasil", MsgBoxStyle.Information, "Informasi")
                    Me.Close()
                    MenuUtama.Show()
                    MenuUtama.loginuser()
                    MenuUtama.ToolStripStatusLabel2.Visible = True
                    MenuUtama.ToolStripStatusLabel3.Visible = True
                    MenuUtama.ToolStripStatusLabel2.Text = "User Aktif" & " " & ":" & " " & rd("NamaUser")
                    MenuUtama.ToolStripStatusLabel3.Text = "Level" & " " & ":" & " " & rd("Role")
                    MenuUtama.GroupBox1.Visible = True
                    MenuUtama.Button1.Visible = True
                    MenuUtama.Label1.Text = rd("Foto")
                    MenuUtama.namauser = rd("NamaUser")
                    MenuUtama.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                    MenuUtama.PictureBox1.Image = Image.FromFile(MenuUtama.Label1.Text)
                    If MenuUtama.Label1.Text = "" Then
                        MenuUtama.PictureBox1.Image = Nothing
                    End If
                End If
            Else
                MessageBox.Show("Kombinasi Username dan Password Salah", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Focus()
                MenuUtama.proseslogout()
            End If
            rd.Close()
            Cmd.Dispose()
        Catch ex As Exception

        End Try
    End Sub
    Sub proses()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Focus()
    End Sub
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ambilkoneksi()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            ErrorProvider1.SetError(TextBox1, "Silahkan Isi Username Anda !!!")
            Exit Sub
        ElseIf TextBox2.Text = "" Then
            ErrorProvider1.SetError(TextBox2, "Silahkan Isi Password Anda !!!")
            Exit Sub
        Else
            ErrorProvider1.Clear()
            Call login()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
        MenuUtama.proseslogout()
    End Sub
    Private Sub Login_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Call proses()
    End Sub
End Class