
Public Class Form1

    Public Shared mydb As mysqldb = New mysqldb
    Public Shared f As Form2 = New Form2
    Public Shared f2 As Form3 = New Form3
    Public Shared f3 As Form4 = New Form4
    Public Shared f4 As Form5 = New Form5
    Public Shared f5 As Form6 = New Form6
    Public Shared f6 As Form7 = New Form7
    Public Shared prog As progress = New progress
    Private Shared DBC As New DBC
    Public Shared dt As DataTable
    Private rangedindexes() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 34, 35, 36, 37, 38, 54, 74, 94, 95, 96, 114, 134, 135, 136, 137, 139, 140, 141, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 176, 177, 179, 180, 181, 184, 187}
    Private castindexes() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 50, 70, 90, 91, 110, 130, 150, 151, 152, 153, 170, 171, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209}
    Private durationindexes() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 35, 36, 37, 38, 39, 40, 41, 42, 62, 63, 64, 65, 66, 85, 86, 105, 106, 125, 145, 165, 185, 186, 187, 205, 225, 245, 265, 285, 305, 325, 326, 327, 328, 347, 367, 387, 407, 427, 447, 467, 468, 487, 507, 508, 527, 547, 548, 549, 550, 551, 552, 553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 577, 578, 579, 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590, 591, 592, 593, 594, 596, 597, 598, 600, 602}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            prog.Show()

            mydb.LoadVars()
            f.iconFile = mydb.iconfilepath

            DBC.OpenDBC("spell.DBC")
            DBC.ReadHeader()
            prog.bar.Step = 100
            prog.bar.Maximum = DBC.GetRecordCount() - 1
            DBC.ReadBody()
            DBC.CloseDBC()
            prog.Close()

            DBC.UpdateListBox()

            'DBC.DumpRecordsDebug()

            ToggleButtons(False)

            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(f.iconFile)

            Dim line As String = ""
            Dim currentId As Integer = 0
            Dim currentp As String = ""
            Dim path As Boolean = False
            Try
                For i = 0 To fileReader.Length() - 1
                    Dim c As String = fileReader.Chars(i)
                    If path Then
                        ' read path
                        If c = """" Then
                            currentp = line
                            line = ""
                            path = False
                            i = i + 3 ' Read ,
                            ' Read \
                            ' read n
                        Else
                            line = line & c
                        End If
                    Else
                        ' Read ID
                        If c = "," Then
                            currentId = Integer.Parse(line)
                            line = ""
                            path = True
                            i = i + 1
                        Else
                            line = line & c
                        End If
                    End If
                    ' save info
                    If (currentId <> 0 And currentp.Length <> 0) Then
                        f.iconpaths.Add(currentp)
                        f.iconids.Add(currentId.ToString())
                        currentp = ""
                        currentId = 0
                    End If
                Next i
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return
            End Try

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

            'mydb.executeSQL("SELECT * FROM dbc_spell WHERE Id = '" & ListBox1.Items.Item(ListBox1.SelectedIndex).ToString() & "'", dt)

            'If dt.Rows.Count <> 1 Then
            '    MessageBox.Show("Spell does not exist!")
            '    Return
            'End If

            DBC.GenerateDataTable(ListBox1.SelectedItem)

            Category.Text = dt.Rows.Item(0).Item(1).ToString()
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
            TextBox3.Text = dt.Rows.Item(0).Item(42)
            TextBox4.Text = dt.Rows.Item(0).Item(43)
            TextBox5.Text = dt.Rows.Item(0).Item(44)
            TextBox6.Text = dt.Rows.Item(0).Item(45)
            powertype.SelectedIndex = dt.Rows.Item(0).Item(41)

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
            For i = 0 To rangedindexes.Count - 1
                If rangedindexes(i) = dt.Rows.Item(0).Item(46) Then
                    rangedex = i
                    Exit For
                End If
            Next
            range.SelectedIndex = rangedex
            rangedex = 0
            For i = 0 To castindexes.Count - 1
                If castindexes(i) = dt.Rows.Item(0).Item(28) Then
                    rangedex = i
                    Exit For
                End If
            Next
            casttime.SelectedIndex = rangedex
            rangedex = 0
            For i = 0 To durationindexes.Count - 1
                If durationindexes(i) = dt.Rows.Item(0).Item(40) Then
                    rangedex = i
                    Exit For
                End If
            Next
            durationind.SelectedIndex = rangedex
            TextBox1.Text = dt.Rows.Item(0).Item(29)
            TextBox2.Text = dt.Rows.Item(0).Item(30)
            f5.interrupts(0) = dt.Rows.Item(0).Item(31)
            f5.interrupts(1) = dt.Rows.Item(0).Item(32)
            f5.interrupts(2) = dt.Rows.Item(0).Item(33)
            f6.values(0) = dt.Rows.Item(0).Item(34)
            f6.values(1) = dt.Rows.Item(0).Item(35)
            f6.values(2) = dt.Rows.Item(0).Item(36)
            TextBox7.Text = dt.Rows.Item(0).Item(131)
            TextBox8.Text = dt.Rows.Item(0).Item(144)
            TextBox9.Text = dt.Rows.Item(0).Item(147)
            TextBox10.Text = dt.Rows.Item(0).Item(148)
            TextBox11.Text = dt.Rows.Item(0).Item(152)
            TextBox12.Text = dt.Rows.Item(0).Item(153)
            TextBox13.Text = dt.Rows.Item(0).Item(167)
            ComboBox2.SelectedIndex = dt.Rows.Item(0).Item(154)

            Dim state As Boolean
            For i = 0 To CheckedListBox1.Items.Count - 1
                state = False
                If (dt.Rows.Item(0).Item(165) And f5.vars.Attributes0(i)) Then
                    state = True
                End If
                CheckedListBox1.SetItemChecked(i, state)
            Next i

            updateCurrentImage()

            ToggleButtons(True)

        Catch ex As Exception

            MessageBox.Show(ex.Message.ToString())
            ToggleButtons(False)

        End Try

    End Sub

    Public Sub updateCurrentImage()
        Try
            For i = 0 To f.iconids.Count - 1
                If f.iconids(i).Equals(f.originalID) Then
                    currentbox.Image = Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location) & "\" & f.iconpaths.Item(i) & ".png")
                    currentbox.Update()
                End If
            Next i
        Catch ex As Exception
            Try
                currentbox.Image = Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location) & "\" & "Interface\Icons\questionmark.png")
                currentbox.Update()
            Catch ex2 As Exception
            End Try
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

        'Try
        '    If Not mydb.UpdateSQL("DELETE FROM dbc_spell WHERE Id = '" & ListBox1.Items.Item(ListBox1.SelectedIndex).ToString() & "'") Then
        '        MessageBox.Show("Deletion failed.")
        '        Return
        '    End If

        '    ToggleButtons(False)

        'Catch ex As Exception

        '    MessageBox.Show(ex.Message.ToString())

        'End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try

            'Dim row As Integer = Integer.Parse(ListBox1.Items.Item(ListBox1.SelectedIndex))

            'If Not mydb.UpdateSQL("DELETE FROM dbc_spell WHERE Id = '" & row.ToString() & "'") Then
            '    MessageBox.Show("Preparing to insert failed because old record deleted badly. If no other error shows, then inserted correctly anyway.")
            'End If

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

            'Dim insertstring As String = "INSERT INTO dbc_spell VALUES ("

            'For i = 0 To 173
            '    insertstring = insertstring & "'" & dt.Rows.Item(0).Item(i).ToString & "',"
            'Next i

            'Dim temp() As Char = insertstring.ToCharArray()
            'Dim t As Char = ")"
            'temp(temp.Length - 1) = t
            'insertstring = New String(temp)

            'If Not mydb.UpdateSQL(insertstring) Then
            '    MessageBox.Show("Saving failed.")
            'End If
            DBC.SaveCurrentSpell(dt.Rows.Item(0).Item(0))

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

    Private Sub Category_TextChanged(sender As Object, e As EventArgs) Handles Category.TextChanged
        dt.Rows.Item(0).Item(1) = Category.Text
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles durationind.SelectedIndexChanged
        dt.Rows.Item(0).Item(40) = castindexes(durationind.SelectedIndex)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        dt.Rows.Item(0).Item(42) = TextBox3.Text
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        dt.Rows.Item(0).Item(43) = TextBox4.Text
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        dt.Rows.Item(0).Item(44) = TextBox5.Text
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        dt.Rows.Item(0).Item(45) = TextBox6.Text
    End Sub

    Private Sub powertype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles powertype.SelectedIndexChanged
        dt.Rows.Item(0).Item(41) = powertype.SelectedIndex
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        dt.Rows.Item(0).Item(131) = TextBox7.Text
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        dt.Rows.Item(0).Item(144) = TextBox8.Text
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        dt.Rows.Item(0).Item(147) = TextBox9.Text
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        dt.Rows.Item(0).Item(152) = TextBox11.Text
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        dt.Rows.Item(0).Item(148) = TextBox10.Text
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        dt.Rows.Item(0).Item(153) = TextBox12.Text
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        dt.Rows.Item(0).Item(154) = ComboBox2.SelectedIndex
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If Button1.Enabled Then
            dt.Rows.Item(0).Item(165) = 0
            Dim i As Integer
            For i = 0 To CheckedListBox1.Items.Count - 1
                If CheckedListBox1.GetItemChecked(i) Or i = e.Index Then
                    dt.Rows.Item(0).Item(165) = dt.Rows.Item(0).Item(165) + f5.vars.Attributes0(i)
                End If
            Next i
        End If
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        dt.Rows.Item(0).Item(167) = TextBox13.Text
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        DBC.DumpRecordsDebug()
        MessageBox.Show("Created new DBC file!")
    End Sub
End Class
