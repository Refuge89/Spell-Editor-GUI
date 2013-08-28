Imports System.IO

Public Class Form4

    Private loading As Boolean = False

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
        ComboBox3.SelectedIndex = Form1.dt.Rows(0).Item(index + 15)
        ComboBox10.SelectedIndex = Form1.dt.Rows(0).Item(index + 16)
        ComboBox16.SelectedIndex = Form1.dt.Rows(0).Item(index + 17)
        ComboBox4.SelectedIndex = Form1.dt.Rows(0).Item(index + 18)
        ComboBox9.SelectedIndex = Form1.dt.Rows(0).Item(index + 19)
        ComboBox15.SelectedIndex = Form1.dt.Rows(0).Item(index + 20)
        ComboBox5.SelectedIndex = Form1.dt.Rows(0).Item(index + 18)
        ComboBox8.SelectedIndex = Form1.dt.Rows(0).Item(index + 19)
        ComboBox14.SelectedIndex = Form1.dt.Rows(0).Item(index + 20)
        TextBox4.Text = Form1.dt.Rows(0).Item(index + 21)
        TextBox25.Text = Form1.dt.Rows(0).Item(index + 22)
        TextBox39.Text = Form1.dt.Rows(0).Item(index + 23)
        TextBox5.Text = Form1.dt.Rows(0).Item(index + 24)
        TextBox24.Text = Form1.dt.Rows(0).Item(index + 25)
        TextBox38.Text = Form1.dt.Rows(0).Item(index + 26)
        TextBox6.Text = Form1.dt.Rows(0).Item(index + 27)
        TextBox23.Text = Form1.dt.Rows(0).Item(index + 28)
        TextBox37.Text = Form1.dt.Rows(0).Item(index + 29)
        ComboBox6.SelectedIndex = Form1.dt.Rows(0).Item(index + 30)
        ComboBox7.SelectedIndex = Form1.dt.Rows(0).Item(index + 31)
        ComboBox13.SelectedIndex = Form1.dt.Rows(0).Item(index + 32)
        TextBox7.Text = Form1.dt.Rows(0).Item(index + 33)
        TextBox22.Text = Form1.dt.Rows(0).Item(index + 34)
        TextBox36.Text = Form1.dt.Rows(0).Item(index + 35)
        TextBox8.Text = Form1.dt.Rows(0).Item(index + 36)
        TextBox21.Text = Form1.dt.Rows(0).Item(index + 37)
        TextBox35.Text = Form1.dt.Rows(0).Item(index + 38)
        TextBox9.Text = Form1.dt.Rows(0).Item(index + 39)
        TextBox20.Text = Form1.dt.Rows(0).Item(index + 40)
        TextBox34.Text = Form1.dt.Rows(0).Item(index + 41)
        TextBox10.Text = Form1.dt.Rows(0).Item(index + 42)
        TextBox19.Text = Form1.dt.Rows(0).Item(index + 43)
        TextBox33.Text = Form1.dt.Rows(0).Item(index + 44)
        TextBox11.Text = Form1.dt.Rows(0).Item(index + 42)
        TextBox18.Text = Form1.dt.Rows(0).Item(index + 43)
        TextBox32.Text = Form1.dt.Rows(0).Item(index + 44)
        TextBox12.Text = Form1.dt.Rows(0).Item(index + 45)
        TextBox17.Text = Form1.dt.Rows(0).Item(index + 46)
        TextBox31.Text = Form1.dt.Rows(0).Item(index + 47)
        TextBox13.Text = Form1.dt.Rows(0).Item(index + 48)
        TextBox16.Text = Form1.dt.Rows(0).Item(index + 49)
        TextBox30.Text = Form1.dt.Rows(0).Item(index + 50)
        TextBox14.Text = Form1.dt.Rows(0).Item(index + 51)
        TextBox15.Text = Form1.dt.Rows(0).Item(index + 52)
        TextBox29.Text = Form1.dt.Rows(0).Item(index + 53)

        loading = False
    End Sub
End Class