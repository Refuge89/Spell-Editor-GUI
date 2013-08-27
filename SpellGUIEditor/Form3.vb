Imports System.IO

Public Class Form3

    Public setAttributes(8) As String
    Public attributes(8) As Integer
    Private vars As New SpellAttributes
    Private loading As Boolean = False

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load, MyBase.VisibleChanged
        If Not Me.Visible Then
            Return
        End If
        loading = True
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
        loading = False
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(0) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) Then
                setAttributes(0) = setAttributes(0) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox2_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox2.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(1) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox2.Items.Count - 1
            If CheckedListBox2.GetItemChecked(i) Then
                setAttributes(1) = setAttributes(1) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox3_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox3.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(2) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox3.Items.Count - 1
            If CheckedListBox3.GetItemChecked(i) Then
                setAttributes(2) = setAttributes(2) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox4_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox4.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(3) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox4.Items.Count - 1
            If CheckedListBox4.GetItemChecked(i) Then
                setAttributes(3) = setAttributes(3) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox5_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox5.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(4) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox5.Items.Count - 1
            If CheckedListBox5.GetItemChecked(i) Then
                setAttributes(4) = setAttributes(4) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox6_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox6.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(5) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox6.Items.Count - 1
            If CheckedListBox6.GetItemChecked(i) Then
                setAttributes(5) = setAttributes(5) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox7_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox7.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(6) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox7.Items.Count - 1
            If CheckedListBox7.GetItemChecked(i) Then
                setAttributes(6) = setAttributes(6) + vars.Attributes0(i)
            End If
        Next i
    End Sub

    Private Sub CheckedListBox8_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox8.ItemCheck
        If loading Then
            Return
        End If
        setAttributes(7) = 0
        Dim i As Integer
        For i = 0 To CheckedListBox8.Items.Count - 1
            If CheckedListBox8.GetItemChecked(i) Then
                setAttributes(7) = setAttributes(7) + vars.Attributes0(i)
            End If
        Next i
    End Sub
End Class