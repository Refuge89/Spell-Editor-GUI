
Public Class Form1

    Public Shared mydb As mysqldb = New mysqldb
    Public Shared f As Form2 = New Form2
    Public Shared f2 As Form3 = New Form3
    Public Shared f3 As Form4 = New Form4
    Public Shared dt As DataTable

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            mydb.LoadVars()
            f.iconFile = mydb.iconfilepath

            mydb.executeSQL("SELECT Id FROM dbc_spell", dt)

            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                ListBox1.Items.Add(dt.Rows.Item(i).Item(0).ToString())
            Next i
            ListBox1.Update()

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())

        End Try

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Try
            mydb.executeSQL("SELECT * FROM dbc_spell WHERE Id = '" & ListBox1.Items.Item(ListBox1.SelectedIndex).ToString() & "'", dt)

            If dt.Rows.Count <> 1 Then
                MessageBox.Show("Spell does not exist!")
                Return
            End If

            SName.Text = dt.Rows.Item(0).Item(136).ToString()
            Description.Text = dt.Rows.Item(0).Item(140).ToString()
            Rank.Text = dt.Rows.Item(0).Item(138).ToString()
            BuffDescription.Text = dt.Rows.Item(0).Item(142).ToString()
            iconid.Text = "Icon ID: " & dt.Rows.Item(0).Item(133).ToString()
            f.originalID = dt.Rows.Item(0).Item(133).ToString()

            Effect1.SelectedIndex = Integer.Parse(dt.Rows.Item(0).Item(71))
            Effect2.SelectedIndex = Integer.Parse(dt.Rows.Item(0).Item(72))
            Effect3.SelectedIndex = Integer.Parse(dt.Rows.Item(0).Item(73))
            Effect1Min.Text = dt.Rows.Item(0).Item(80)
            Effect2Min.Text = dt.Rows.Item(0).Item(81)
            Effect3Min.Text = dt.Rows.Item(0).Item(82)
            Effect1Max.Text = dt.Rows.Item(0).Item(74)
            Effect2Max.Text = dt.Rows.Item(0).Item(75)
            Effect3Max.Text = dt.Rows.Item(0).Item(76)

            For i = 0 To 8
                f2.attributes(i) = dt.Rows.Item(0).Item(4 + i)
                f2.setAttributes(i) = dt.Rows.Item(0).Item(4 + i)
            Next i

            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            If Not mydb.UpdateSQL("DELETE FROM dbc_spell WHERE Id = '" & ListBox1.Items.Item(ListBox1.SelectedIndex).ToString() & "'") Then
                MessageBox.Show("Deletion failed.")
                Return
            End If

            Button2.Enabled = False
            Button1.Enabled = False
            Button3.Enabled = False

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try

            Dim row As Integer = Integer.Parse(ListBox1.Items.Item(ListBox1.SelectedIndex))

            If Not mydb.UpdateSQL("DELETE FROM dbc_spell WHERE Id = '" & row.ToString() & "'") Then
                MessageBox.Show("Preparing to insert failed because old record deleted badly. If no other error shows, then inserted correctly anyway.")
            End If

            If f.iconID.Length <> 0 Then
                dt.Rows.Item(0).Item(133) = f.iconID
                f.iconID = ""
            End If

            Dim i As Integer
            For i = 0 To 8
                dt.Rows.Item(0).Item(4 + i) = f2.setAttributes(i)
            Next i

            Dim insertstring As String = "INSERT INTO dbc_spell VALUES ("

            Dim i As Integer
            For i = 0 To 173
                insertstring = insertstring & "'" & dt.Rows.Item(0).Item(i).ToString & "',"
            Next i

            Dim temp() As Char = insertstring.ToCharArray()
            Dim t As Char = ")"
            temp(temp.Length - 1) = t
            insertstring = New String(temp)

            If Not mydb.UpdateSQL(insertstring) Then
                MessageBox.Show("Saving failed.")
            End If

            Button2.Enabled = False
            Button1.Enabled = False
            Button3.Enabled = False

            ListBox1_SelectedIndexChanged(sender, e)

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())

        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        f.Show()
    End Sub

    Private Sub SName_TextChanged(sender As Object, e As EventArgs) Handles SName.TextChanged
        dt.Rows.Item(0).Item(136) = SName.Text
    End Sub

    Private Sub Description_TextChanged(sender As Object, e As EventArgs) Handles Description.TextChanged
        dt.Rows.Item(0).Item(140) = Description.Text
    End Sub

    Private Sub BuffDescription_TextChanged(sender As Object, e As EventArgs) Handles BuffDescription.TextChanged
        dt.Rows.Item(0).Item(142) = BuffDescription.Text
    End Sub

    Private Sub Rank_TextChanged(sender As Object, e As EventArgs) Handles Rank.TextChanged
        dt.Rows.Item(0).Item(138) = Rank.Text
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Effect1.SelectedIndexChanged
        dt.Rows.Item(0).Item(71) = Effect1.SelectedIndex.ToString()
    End Sub

    Private Sub Effect2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Effect2.SelectedIndexChanged
        dt.Rows.Item(0).Item(72) = Effect2.SelectedIndex.ToString()
    End Sub

    Private Sub Effect3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Effect3.SelectedIndexChanged
        dt.Rows.Item(0).Item(73) = Effect3.SelectedIndex.ToString()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        f2.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        f3.Show()
    End Sub
End Class
