Imports System.IO

Public Class Form6

    Private vars As New SpellAttributes
    Public interrupts() As Integer = {0, 0, 0}
    Private loading As Boolean = False

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        loading = True
        If Not Me.Visible Then
            Return
        End If
        Dim i As Integer
        Dim state As Boolean
        For i = 0 To CheckedListBox1.Items.Count - 1
            state = False
            If (interrupts(0) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox1.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox2.Items.Count - 1
            state = False
            If (interrupts(1) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox2.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox3.Items.Count - 1
            state = False
            If (interrupts(2) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox3.SetItemChecked(i, state)
        Next i
        loading = False
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If loading Then
            Return
        End If
        interrupts(0) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) Then
                interrupts(0) = interrupts(0) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox2_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox2.ItemCheck
        If loading Then
            Return
        End If
        interrupts(1) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox2.Items.Count - 1
            If CheckedListBox2.GetItemChecked(i) Then
                interrupts(1) = interrupts(1) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox3_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox3.ItemCheck
        If loading Then
            Return
        End If
        interrupts(2) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox3.Items.Count - 1
            If CheckedListBox3.GetItemChecked(i) Then
                interrupts(2) = interrupts(2) + vars.Attributes0(i)
            End If
        Next i
    End Sub
End Class