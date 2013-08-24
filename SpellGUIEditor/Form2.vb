Imports System.IO

Public Class Form2
    Public originalID As String = ""
    Public iconID As String = ""
    Public iconFile As String = ""
    ' column 133 = icon ID
    Private iconpaths As ArrayList = New ArrayList
    Private iconids As ArrayList = New ArrayList
    Private paths(55) As String
    Dim currentIndex As Integer = -1
    Dim ctrlDict As New Dictionary(Of String, PictureBox)
    Dim isLoading As Boolean = False

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Set new icon ID
        Me.Hide()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ctrlDict.Add("0", PictureBox0)
        ctrlDict.Add("1", PictureBox1)
        ctrlDict.Add("2", PictureBox2)
        ctrlDict.Add("3", PictureBox3)
        ctrlDict.Add("4", PictureBox4)
        ctrlDict.Add("5", PictureBox5)
        ctrlDict.Add("6", PictureBox6)
        ctrlDict.Add("7", PictureBox7)
        ctrlDict.Add("8", PictureBox8)
        ctrlDict.Add("9", PictureBox9)
        ctrlDict.Add("10", PictureBox10)
        ctrlDict.Add("11", PictureBox11)
        ctrlDict.Add("12", PictureBox12)
        ctrlDict.Add("13", PictureBox13)
        ctrlDict.Add("14", PictureBox14)
        ctrlDict.Add("15", PictureBox15)
        ctrlDict.Add("16", PictureBox16)
        ctrlDict.Add("17", PictureBox17)
        ctrlDict.Add("18", PictureBox18)
        ctrlDict.Add("19", PictureBox19)
        ctrlDict.Add("20", PictureBox20)
        ctrlDict.Add("21", PictureBox21)
        ctrlDict.Add("22", PictureBox22)
        ctrlDict.Add("23", PictureBox23)
        ctrlDict.Add("24", PictureBox24)
        ctrlDict.Add("25", PictureBox25)
        ctrlDict.Add("26", PictureBox26)
        ctrlDict.Add("27", PictureBox27)
        ctrlDict.Add("28", PictureBox28)
        ctrlDict.Add("29", PictureBox29)
        ctrlDict.Add("30", PictureBox30)
        ctrlDict.Add("31", PictureBox31)
        ctrlDict.Add("32", PictureBox32)
        ctrlDict.Add("33", PictureBox33)
        ctrlDict.Add("34", PictureBox34)
        ctrlDict.Add("35", PictureBox35)
        ctrlDict.Add("36", PictureBox36)
        ctrlDict.Add("37", PictureBox37)
        ctrlDict.Add("38", PictureBox38)
        ctrlDict.Add("39", PictureBox39)
        ctrlDict.Add("40", PictureBox40)
        ctrlDict.Add("41", PictureBox41)
        ctrlDict.Add("42", PictureBox42)
        ctrlDict.Add("43", PictureBox43)
        ctrlDict.Add("44", PictureBox44)
        ctrlDict.Add("45", PictureBox45)
        ctrlDict.Add("46", PictureBox46)
        ctrlDict.Add("47", PictureBox47)
        ctrlDict.Add("48", PictureBox48)
        ctrlDict.Add("49", PictureBox49)
        ctrlDict.Add("50", PictureBox50)
        ctrlDict.Add("51", PictureBox51)
        ctrlDict.Add("52", PictureBox52)
        ctrlDict.Add("53", PictureBox53)
        ctrlDict.Add("54", PictureBox54)

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(iconFile)

        Dim line As String = ""
        Dim i As Integer = 0
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
                    iconpaths.Add(currentp)
                    iconids.Add(currentId.ToString())
                    currentp = ""
                    currentId = 0
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try

        Try
            For i = 0 To iconids.Count - 1
                If iconids(i).Equals(originalID) Then
                    currentbox.Image = Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location) & "\" & iconpaths.Item(i) & ".png")
                    currentbox.Update()
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show("Unable to find original icon image.")
        End Try

        LoadNext55Images()

        For i = 0 To 54
            AddHandler ctrlDict(i).MouseClick, AddressOf OnClicked
        Next i

    End Sub

    Private Sub LoadNext55Images()
        'Button3.Enabled = False
        isLoading = True
        Dim apppath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        For i = 0 To 54
            Try
                currentIndex = currentIndex + 1
                If currentIndex > iconids.Count - 1 Then
                    currentIndex = -1
                End If
                ctrlDict(i).Image = Image.FromFile(apppath & "\" & iconpaths.Item(currentIndex) & ".png")
                paths(i) = iconpaths.Item(currentIndex)
            Catch ex As Exception
                i = i - 1
            End Try
        Next i
        Label1.Text = "Loaded icons " & currentIndex - 55 & " to " & currentIndex & ". Total icons: " & iconpaths.Count & "."
        isLoading = False
        If Not isLoading Then
            For i = 0 To 54
                ctrlDict(i).Update()
            Next
        End If
        'Button3.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadNext55Images()
    End Sub

    Private Sub OnClicked(sender As Object, e As EventArgs)
        Try
            Dim pic As PictureBox = DirectCast(sender, PictureBox)
            Dim name As String = pic.Name.ToString()
            Dim impath As String = paths(Integer.Parse(name.Chars(name.Length - 1)))
            Dim i As Integer
            For i = 0 To iconpaths.Count - 1
                If impath.Equals(iconpaths.Item(i)) Then
                    Exit For
                End If
            Next i

            iconID = iconids.Item(i).ToString()
            Form1.iconid.Text = "Icon ID: " & iconID
            originalID = iconID
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form2_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Not Me.Visible() Then
            Return
        End If
        Try
            For i = 0 To iconids.Count - 1
                If iconids(i).Equals(originalID) Then
                    currentbox.Image = Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location) & "\" & iconpaths.Item(i) & ".png")
                    currentbox.Update()
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show("Unable to find original icon image.")
        End Try
    End Sub
End Class