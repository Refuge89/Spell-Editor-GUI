Imports System.IO

Public Class Form3

    Public Shared at0 As String = ""
    Public Shared at1 As String = ""
    Public Shared at2 As String = ""
    Public Shared at3 As String = ""
    Public Shared at4 As String = ""
    Public Shared at5 As String = ""
    Public Shared at6 As String = ""
    Public Shared at7 As String = ""
    Public attributes(8) As Integer
    Private vars As New SpellAttributes

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load, MyBase.VisibleChanged

        Dim i As Integer
        Dim state As Boolean
        For i = 0 To CheckedListBox1.Items.Count - 1
            state = False
            If (attributes(0) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox1.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox2.Items.Count - 1
            state = False
            If (attributes(1) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox2.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox3.Items.Count - 1
            state = False
            If (attributes(2) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox3.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox4.Items.Count - 1
            state = False
            If (attributes(3) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox4.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox5.Items.Count - 1
            state = False
            If (attributes(4) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox5.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox6.Items.Count - 1
            state = False
            If (attributes(5) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox6.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox7.Items.Count - 1
            state = False
            If (attributes(6) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox7.SetItemChecked(i, state)
        Next i
        For i = 0 To CheckedListBox8.Items.Count - 1
            state = False
            If (attributes(7) And vars.Attributes0(i)) Then
                state = True
            End If
            CheckedListBox8.SetItemChecked(i, state)
        Next i
    End Sub

End Class