Imports System.IO

Public Class Form4

    Private loading As Boolean = False
    Private radiusi() As Integer = {7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65}

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form4_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Not Me.Visible() Then
            Return
        End If
        loading = True

        Dim index As Integer = 71
        ComboBox1.SelectedIndex = Form1.dt.Rows(0).Item(index)
        ComboBox12.SelectedIndex = Form1.dt.Rows(0).Item(index + 1)
        ComboBox18.SelectedIndex = Form1.dt.Rows(0).Item(index + 2)
        TextBox1.Text = Form1.dt.Rows(0).Item(index + 3)
        TextBox28.Text = Form1.dt.Rows(0).Item(index + 4)
        TextBox42.Text = Form1.dt.Rows(0).Item(index + 5)
        TextBox2.Text = Form1.dt.Rows(0).Item(index + 6)
        TextBox27.Text = Form1.dt.Rows(0).Item(index + 7)
        TextBox41.Text = Form1.dt.Rows(0).Item(index + 8)
        TextBox3.Text = Form1.dt.Rows(0).Item(index + 9)
        TextBox26.Text = Form1.dt.Rows(0).Item(index + 10)
        TextBox40.Text = Form1.dt.Rows(0).Item(index + 11)
        ComboBox2.SelectedIndex = Form1.dt.Rows(0).Item(index + 12)
        ComboBox11.SelectedIndex = Form1.dt.Rows(0).Item(index + 13)
        ComboBox17.SelectedIndex = Form1.dt.Rows(0).Item(index + 14)
        Dim id As UInt32 = Form1.dt.Rows(0).Item(index + 15)
        If id < ComboBox3.Items.Count - 1 Then
            ComboBox3.SelectedIndex = id
        End If
        id = Form1.dt.Rows(0).Item(index + 16)
        If id < ComboBox10.Items.Count - 1 Then
            ComboBox10.SelectedIndex = id
        End If
        id = Form1.dt.Rows(0).Item(index + 17)
        If id < ComboBox16.Items.Count - 1 Then
            ComboBox16.SelectedIndex = id
        End If
        ComboBox4.SelectedIndex = Form1.dt.Rows(0).Item(index + 18)
        ComboBox9.SelectedIndex = Form1.dt.Rows(0).Item(index + 19)
        ComboBox15.SelectedIndex = Form1.dt.Rows(0).Item(index + 20)
        TextBox4.Text = Form1.dt.Rows(0).Item(index + 24)
        TextBox25.Text = Form1.dt.Rows(0).Item(index + 25)
        TextBox39.Text = Form1.dt.Rows(0).Item(index + 26)
        TextBox5.Text = Form1.dt.Rows(0).Item(index + 27)
        TextBox24.Text = Form1.dt.Rows(0).Item(index + 28)
        TextBox38.Text = Form1.dt.Rows(0).Item(index + 29)
        TextBox6.Text = Form1.dt.Rows(0).Item(index + 30)
        TextBox23.Text = Form1.dt.Rows(0).Item(index + 31)
        TextBox37.Text = Form1.dt.Rows(0).Item(index + 32)
        ComboBox6.SelectedIndex = Form1.dt.Rows(0).Item(index + 33)
        ComboBox7.SelectedIndex = Form1.dt.Rows(0).Item(index + 34)
        ComboBox13.SelectedIndex = Form1.dt.Rows(0).Item(index + 35)
        TextBox7.Text = Form1.dt.Rows(0).Item(index + 36)
        TextBox22.Text = Form1.dt.Rows(0).Item(index + 37)
        TextBox36.Text = Form1.dt.Rows(0).Item(index + 38)
        TextBox8.Text = Form1.dt.Rows(0).Item(index + 39)
        TextBox21.Text = Form1.dt.Rows(0).Item(index + 40)
        TextBox35.Text = Form1.dt.Rows(0).Item(index + 41)
        TextBox9.Text = Form1.dt.Rows(0).Item(index + 42)
        TextBox20.Text = Form1.dt.Rows(0).Item(index + 43)
        TextBox34.Text = Form1.dt.Rows(0).Item(index + 44)
        TextBox10.Text = Form1.dt.Rows(0).Item(index + 45)
        TextBox19.Text = Form1.dt.Rows(0).Item(index + 46)
        TextBox33.Text = Form1.dt.Rows(0).Item(index + 47)
        TextBox11.Text = Form1.dt.Rows(0).Item(index + 48)
        TextBox18.Text = Form1.dt.Rows(0).Item(index + 49)
        TextBox32.Text = Form1.dt.Rows(0).Item(index + 50)
        TextBox12.Text = Form1.dt.Rows(0).Item(index + 51)
        TextBox17.Text = Form1.dt.Rows(0).Item(index + 52)
        TextBox31.Text = Form1.dt.Rows(0).Item(index + 53)
        TextBox13.Text = Form1.dt.Rows(0).Item(index + 54)
        TextBox16.Text = Form1.dt.Rows(0).Item(index + 55)
        TextBox30.Text = Form1.dt.Rows(0).Item(index + 56)
        TextBox14.Text = Form1.dt.Rows(0).Item(index + 57)
        TextBox15.Text = Form1.dt.Rows(0).Item(index + 58)
        TextBox29.Text = Form1.dt.Rows(0).Item(index + 59)

        Dim rangedex As Integer = 0
        For i = 0 To radiusi.Count - 1
            If radiusi(i) = Form1.dt.Rows.Item(0).Item(index + 21) Then
                rangedex = i
                Exit For
            End If
        Next
        ComboBox5.SelectedIndex = rangedex
        rangedex = 0
        For i = 0 To radiusi.Count - 1
            If radiusi(i) = Form1.dt.Rows.Item(0).Item(index + 22) Then
                rangedex = i
                Exit For
            End If
        Next
        ComboBox8.SelectedIndex = rangedex
        rangedex = 0
        For i = 0 To radiusi.Count - 1
            If radiusi(i) = Form1.dt.Rows.Item(0).Item(index + 23) Then
                rangedex = i
                Exit For
            End If
        Next
        ComboBox14.SelectedIndex = rangedex
        rangedex = 0

        loading = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged, TextBox8.TextChanged, TextBox7.TextChanged, TextBox6.TextChanged, TextBox5.TextChanged, TextBox4.TextChanged, TextBox3.TextChanged, TextBox2.TextChanged, TextBox14.TextChanged, TextBox13.TextChanged, TextBox12.TextChanged, TextBox11.TextChanged, TextBox10.TextChanged, TextBox1.TextChanged
        LazyHackfix_UpdateItems()
    End Sub

    Private Sub TextBox28_TextChanged(sender As Object, e As EventArgs) Handles TextBox28.TextChanged, TextBox27.TextChanged, TextBox26.TextChanged, TextBox25.TextChanged, TextBox24.TextChanged, TextBox23.TextChanged, TextBox22.TextChanged, TextBox21.TextChanged, TextBox20.TextChanged, TextBox19.TextChanged, TextBox18.TextChanged, TextBox17.TextChanged, TextBox16.TextChanged, TextBox15.TextChanged
        LazyHackfix_UpdateItems()
    End Sub

    Private Sub TextBox42_TextChanged(sender As Object, e As EventArgs) Handles TextBox42.TextChanged, TextBox41.TextChanged, TextBox40.TextChanged, TextBox39.TextChanged, TextBox38.TextChanged, TextBox37.TextChanged, TextBox36.TextChanged, TextBox35.TextChanged, TextBox34.TextChanged, TextBox33.TextChanged, TextBox32.TextChanged, TextBox31.TextChanged, TextBox30.TextChanged, TextBox29.TextChanged
        LazyHackfix_UpdateItems()
    End Sub

    Private Sub LazyHackfix_UpdateItems()
        If loading Then
            Return
        End If
        Dim index As Integer = 71
        Form1.dt.Rows(0).Item(index) = ComboBox1.SelectedIndex
        Form1.dt.Rows(0).Item(index + 1) = ComboBox12.SelectedIndex
        Form1.dt.Rows(0).Item(index + 2) = ComboBox18.SelectedIndex
        Form1.dt.Rows(0).Item(index + 3) = TextBox1.Text
        Form1.dt.Rows(0).Item(index + 4) = TextBox28.Text
        Form1.dt.Rows(0).Item(index + 5) = TextBox42.Text
        Form1.dt.Rows(0).Item(index + 6) = TextBox2.Text
        Form1.dt.Rows(0).Item(index + 7) = TextBox27.Text
        Form1.dt.Rows(0).Item(index + 8) = TextBox41.Text
        Form1.dt.Rows(0).Item(index + 9) = TextBox3.Text
        Form1.dt.Rows(0).Item(index + 10) = TextBox26.Text
        Form1.dt.Rows(0).Item(index + 11) = TextBox40.Text
        Form1.dt.Rows(0).Item(index + 12) = ComboBox2.SelectedIndex
        Form1.dt.Rows(0).Item(index + 13) = ComboBox11.SelectedIndex
        Form1.dt.Rows(0).Item(index + 14) = ComboBox17.SelectedIndex
        Form1.dt.Rows(0).Item(index + 15) = ComboBox3.SelectedIndex
        Form1.dt.Rows(0).Item(index + 16) = ComboBox10.SelectedIndex
        Form1.dt.Rows(0).Item(index + 17) = ComboBox16.SelectedIndex
        Form1.dt.Rows(0).Item(index + 18) = ComboBox4.SelectedIndex
        Form1.dt.Rows(0).Item(index + 19) = ComboBox9.SelectedIndex
        Form1.dt.Rows(0).Item(index + 20) = ComboBox15.SelectedIndex
        Form1.dt.Rows(0).Item(index + 24) = TextBox4.Text
        Form1.dt.Rows(0).Item(index + 25) = TextBox25.Text
        Form1.dt.Rows(0).Item(index + 26) = TextBox39.Text
        Form1.dt.Rows(0).Item(index + 27) = TextBox5.Text
        Form1.dt.Rows(0).Item(index + 28) = TextBox24.Text
        Form1.dt.Rows(0).Item(index + 29) = TextBox38.Text
        Form1.dt.Rows(0).Item(index + 30) = TextBox6.Text
        Form1.dt.Rows(0).Item(index + 31) = TextBox23.Text
        Form1.dt.Rows(0).Item(index + 32) = TextBox37.Text
        Form1.dt.Rows(0).Item(index + 33) = ComboBox6.SelectedIndex
        Form1.dt.Rows(0).Item(index + 34) = ComboBox7.SelectedIndex
        Form1.dt.Rows(0).Item(index + 35) = ComboBox13.SelectedIndex
        Form1.dt.Rows(0).Item(index + 36) = TextBox7.Text
        Form1.dt.Rows(0).Item(index + 37) = TextBox22.Text
        Form1.dt.Rows(0).Item(index + 38) = TextBox36.Text
        Form1.dt.Rows(0).Item(index + 39) = TextBox8.Text
        Form1.dt.Rows(0).Item(index + 40) = TextBox21.Text
        Form1.dt.Rows(0).Item(index + 41) = TextBox35.Text
        Form1.dt.Rows(0).Item(index + 42) = TextBox9.Text
        Form1.dt.Rows(0).Item(index + 43) = TextBox20.Text
        Form1.dt.Rows(0).Item(index + 44) = TextBox34.Text
        Form1.dt.Rows(0).Item(index + 45) = TextBox10.Text
        Form1.dt.Rows(0).Item(index + 46) = TextBox19.Text
        Form1.dt.Rows(0).Item(index + 47) = TextBox33.Text
        Form1.dt.Rows(0).Item(index + 48) = TextBox11.Text
        Form1.dt.Rows(0).Item(index + 49) = TextBox18.Text
        Form1.dt.Rows(0).Item(index + 50) = TextBox32.Text
        Form1.dt.Rows(0).Item(index + 51) = TextBox12.Text
        Form1.dt.Rows(0).Item(index + 52) = TextBox17.Text
        Form1.dt.Rows(0).Item(index + 53) = TextBox31.Text
        Form1.dt.Rows(0).Item(index + 54) = TextBox13.Text
        Form1.dt.Rows(0).Item(index + 55) = TextBox16.Text
        Form1.dt.Rows(0).Item(index + 56) = TextBox30.Text
        Form1.dt.Rows(0).Item(index + 57) = TextBox14.Text
        Form1.dt.Rows(0).Item(index + 58) = TextBox15.Text
        Form1.dt.Rows(0).Item(index + 59) = TextBox29.Text

        Form1.dt.Rows(0).Item(index + 21) = ComboBox5.SelectedIndex
        Form1.dt.Rows(0).Item(index + 22) = ComboBox8.SelectedIndex
        Form1.dt.Rows(0).Item(index + 23) = ComboBox14.SelectedIndex
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged, ComboBox8.SelectedIndexChanged, ComboBox7.SelectedIndexChanged, ComboBox6.SelectedIndexChanged, ComboBox5.SelectedIndexChanged, ComboBox4.SelectedIndexChanged, ComboBox3.SelectedIndexChanged, ComboBox2.SelectedIndexChanged, ComboBox18.SelectedIndexChanged, ComboBox17.SelectedIndexChanged, ComboBox16.SelectedIndexChanged, ComboBox15.SelectedIndexChanged, ComboBox14.SelectedIndexChanged, ComboBox13.SelectedIndexChanged, ComboBox12.SelectedIndexChanged, ComboBox11.SelectedIndexChanged, ComboBox10.SelectedIndexChanged, ComboBox1.SelectedIndexChanged
        LazyHackfix_UpdateItems()
    End Sub
End Class