
Public Class Form1

    Public Shared mydb As mysqldb = New mysqldb
    Public Shared f As Form2 = New Form2
    Public Shared f2 As Form3 = New Form3
    Public Shared f3 As Form4 = New Form4
    Public Shared f4 As Form5 = New Form5
    Public Shared f5 As Form6 = New Form6
    Public Shared f6 As Form7 = New Form7
    Public Shared dt As DataTable
    Private rangedindexes() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 34, 35, 36, 37, 38, 54, 74, 94, 95, 96, 114, 134, 135, 136, 137, 139, 140, 141, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 176, 177, 179, 180, 181, 184, 187}
    Private castindexes() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 50, 70, 90, 91, 110, 130, 150, 151, 152, 153, 170, 171, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209}

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

            ToggleButtons(False)

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())

        End Try

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Try
            f.Hide()
            f2.Hide()
            f3.Hide()
            f4.Hide()
            ToggleButtons(False)
            Tabs.SelectedIndex = 0

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

            Dim pos As Integer = 0
            For i = 0 To 8
                pos = i + 4
                f2.attributes(i) = dt.Rows.Item(0).Item(pos)
                f2.setAttributes(i) = dt.Rows.Item(0).Item(pos)
            Next i

            f4.targets = dt.Rows.Item(0).Item(16)
            maxlevel.Text = dt.Rows.Item(0).Item(37)
            baselevel.Text = dt.Rows.Item(0).Item(38)
            spellevel.Text = dt.Rows.Item(0).Item(39)
            dispeltype.SelectedIndex = dt.Rows.Item(0).Item(2) + 1
            If dt.Rows.Item(0).Item(3) = -1 Then
                dt.Rows.Item(0).Item(3) = 0
            End If
            mechanics.SelectedIndex = dt.Rows.Item(0).Item(3)
            Dim rangedex As Integer = 0
            For i = 0 To rangedindexes.Count
                If rangedindexes(i) = dt.Rows.Item(0).Item(46) Then
                    rangedex = i
                    Exit For
                End If
            Next
            range.SelectedIndex = rangedex
            rangedex = 0
            For i = 0 To castindexes.Count
                If castindexes(i) = dt.Rows.Item(0).Item(28) Then
                    rangedex = i
                    Exit For
                End If
            Next
            casttime.SelectedIndex = rangedex
            TextBox1.Text = dt.Rows.Item(0).Item(29)
            TextBox2.Text = dt.Rows.Item(0).Item(30)
            f5.interrupts(0) = dt.Rows.Item(0).Item(31)
            f5.interrupts(1) = dt.Rows.Item(0).Item(32)
            f5.interrupts(2) = dt.Rows.Item(0).Item(33)
            f6.values(0) = dt.Rows.Item(0).Item(34)
            f6.values(1) = dt.Rows.Item(0).Item(35)
            f6.values(2) = dt.Rows.Item(0).Item(36)

            ToggleButtons(True)

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())
            ToggleButtons(False)

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            If Not mydb.UpdateSQL("DELETE FROM dbc_spell WHERE Id = '" & ListBox1.Items.Item(ListBox1.SelectedIndex).ToString() & "'") Then
                MessageBox.Show("Deletion failed.")
                Return
            End If

            ToggleButtons(False)

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

            dt.Rows.Item(0).Item(16) = f4.targets
            dt.Rows.Item(0).Item(31) = f5.interrupts(0)
            dt.Rows.Item(0).Item(32) = f5.interrupts(1)
            dt.Rows.Item(0).Item(33) = f5.interrupts(2)
            dt.Rows.Item(0).Item(34) = f6.values(0)
            dt.Rows.Item(0).Item(35) = f6.values(1)
            dt.Rows.Item(0).Item(36) = f6.values(2)

            Dim i As Integer
            For i = 0 To 8
                dt.Rows.Item(0).Item(4 + i) = f2.setAttributes(i)
            Next i

            Dim insertstring As String = "INSERT INTO dbc_spell VALUES ("

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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        f4.show()
    End Sub

    Private Sub spellevel_TextChanged(sender As Object, e As EventArgs) Handles spellevel.TextChanged
        dt.Rows.Item(0).Item(39) = spellevel.Text
    End Sub

    Private Sub baselevel_TextChanged(sender As Object, e As EventArgs) Handles baselevel.TextChanged
        dt.Rows.Item(0).Item(38) = baselevel.Text
    End Sub

    Private Sub maxlevel_TextChanged(sender As Object, e As EventArgs) Handles maxlevel.TextChanged
        dt.Rows.Item(0).Item(37) = maxlevel.Text
    End Sub

    Private Sub ToggleButtons(ByVal e As Boolean)
        Button1.Enabled = e
        Button2.Enabled = e
        Button3.Enabled = e
        Button4.Enabled = e
        Button5.Enabled = e
        Button6.Enabled = e
        Button7.Enabled = e
        Button8.Enabled = e
    End Sub

    Private Sub dispeltype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dispeltype.SelectedIndexChanged
        Dim t As Integer = dispeltype.SelectedIndex
        If t <> 0 Then
            t = t - 1
        End If
        dt.Rows.Item(0).Item(2) = t
    End Sub

    Private Sub mechanics_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mechanics.SelectedIndexChanged
        dt.Rows.Item(0).Item(3) = mechanics.SelectedIndex
    End Sub

    Private Sub range_SelectedIndexChanged(sender As Object, e As EventArgs) Handles range.SelectedIndexChanged
        dt.Rows.Item(0).Item(46) = rangedindexes(range.SelectedIndex)
    End Sub

    Private Sub casttime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles casttime.SelectedIndexChanged
        dt.Rows.Item(0).Item(28) = castindexes(casttime.SelectedIndex)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        dt.Rows.Item(0).Item(29) = TextBox1.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        dt.Rows.Item(0).Item(30) = TextBox2.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        f5.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        f6.Show()
    End Sub
End Class
