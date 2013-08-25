Imports System.IO

Public Class Form5

    Private vars As New SpellAttributes
    Public targets As Integer = 0

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Not Me.Visible Then
            Return
        End If
        Dim i As Integer
        Dim state As Boolean
        For i = 0 To CheckedListBox1.Items.Count - 1
            state = False
            If (targets And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox1.SetItemChecked(i, state)
        Next i
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        targets = 0
        Dim i As Integer
        For i = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) Then
                targets = targets + vars.Attributes0(i)
            End If
        Next i
    End Sub
End Class