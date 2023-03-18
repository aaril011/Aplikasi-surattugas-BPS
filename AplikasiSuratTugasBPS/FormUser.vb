Public Class FormUser
    Public id As String = ""
        Sub tampil()
            Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select*from TbUser", conn)
                ds = New DataSet
            adapter.Fill(ds, "TbUser")
            DataGridView1.DataSource = ds.Tables("TbUser")
            Catch ex As Exception

            End Try
        End Sub

        Sub caridata()
            Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from TbUser where NamaUser like '%" &
                                                      TextBox1.Text & "%' or UserName  like '%" & TextBox1.Text & "%'", conn)
                ds = New DataSet
            adapter.Fill(ds, "TbUser")
            DataGridView1.DataSource = ds.Tables("TbUser")
            Catch ex As Exception
            End Try
        End Sub
        Sub urutasc()
            Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from TbUser order by IdUser asc", conn)
                ds = New DataSet
            adapter.Fill(ds, "TbUser")
            DataGridView1.DataSource = ds.Tables("TbUser")
            Catch ex As Exception
            End Try
        End Sub
        Sub urutdesc()
            Try
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from TbUser order by IdUser desc", conn)
                ds = New DataSet
            adapter.Fill(ds, "TbUser")
            DataGridView1.DataSource = ds.Tables("TbUser")
            Catch ex As Exception
            End Try
        End Sub

        Sub proses()
            TextBox1.Text = ""
            RadioButton1.Checked = False
            RadioButton2.Checked = False
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

        ComboBox1.Text = ""
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False

        ComboBox1.Enabled = False
            Button1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = True
            Button6.Enabled = True
        End Sub
        Sub proses1()
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True

        ComboBox1.Enabled = True
            Button1.Enabled = False
            Button2.Enabled = True
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = True
            Button6.Enabled = True
        End Sub

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Call ambilkoneksi()
            Call tampil()
            Call proses()
        End Sub

        Private Sub button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("data tidak boleh kosong", MsgBoxStyle.Information, "informasi")
            Exit Sub
        Else
            Try
                strsql = "insert into TbUser values('" & id & "','" & TextBox2.Text & "','" &
                    TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox1.Text & "')"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil diproses", MsgBoxStyle.Information, "informasi")
                Call tampil()
                Call proses()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
            End Try
        End If
        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("data tidak boleh kosong", MsgBoxStyle.Information, "informasi")
            Exit Sub
        Else
            Try
                strsql = "update Tbuser set NamaUser='" & TextBox2.Text & "'," &
                   "UserName='" & TextBox3.Text & "'," &
                   "Password='" & TextBox4.Text & "'," &
                   "Role='" & ComboBox1.Text & "'" &
                   "where IdUser='" & id & "'"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil diproses", MsgBoxStyle.Information, "informasi")
                Call tampil()
                Call proses()
            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
            End Try
        End If
        End Sub

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
            Dim a As String
            a = MsgBox("anda ingin menghapus data ini", MsgBoxStyle.YesNo, "konfirmasi")
            If a = vbYes Then
            strsql = "delete from TbUser where IdUser='" & id & "'"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strsql, conn)
                cmd.ExecuteNonQuery()
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
            a = MsgBox("apakah anda ingin keluar ?", MsgBoxStyle.YesNo, "konfirmasi")
            If a = vbYes Then
                MsgBox("anda berhasil keluar", MsgBoxStyle.Information, "informasi")
                Me.Dispose()
            Else
                Call proses()
            End If
        End Sub

        Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
            Try
                Call proses1()
            id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = True
                Button4.Enabled = True
                Button5.Enabled = True
                Button6.Enabled = True
            Catch ex As Exception
                MsgBox("data yang dipilih tidak ada", MsgBoxStyle.Information, "informasi")
            End Try
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call proses1()
        TextBox2.Focus()
        End Sub

        Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
            Call caridata()
        End Sub

        Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
            Call urutasc()
        End Sub

        Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
            Call urutdesc()
        End Sub

        Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button3_ClientSizeChanged(sender As Object, e As EventArgs) Handles Button3.ClientSizeChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class