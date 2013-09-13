Imports System.IO
Imports System.Runtime.InteropServices

' Localisation
'0	 enUS / enGB	 English / Great British
'1	 koKR	 Korean
'2	 frFR	 French
'3	 deDE	 German
'4	 zhCN	 Chinese
'5	 zhTW	 Taiwan
'6	 esES / esMX	 Spanish / Mexican
'7	 ?	 ?
'8	 ruRU	 Russian

Structure DBC_Header
    Dim magic As UInt32 ' always 'WDBC'
    Dim record_count As UInt32 ' records per file
    Dim field_count As UInt32 ' fields per record
    Dim record_size As UInt32 ' size of record
    Dim string_block_size As UInt32 ' size of string block
End Structure

Structure DBC_Body
    Dim Header As DBC_Header ' Header structure
    Dim records() As SpellRecord ' Array of spell records
    Dim string_block() As Char ' string of the string block (null terminators throughout)
End Structure

Public Class DBC
    Private fsSource As FileStream
    Dim bytes() As Byte
    Dim numBytesRead As UInt32
    Dim numBytesToRead As UInt32

    Private SpellDBC As DBC_Body

    Private sizeofUint32 As UInt32
    Private sizeofUint64 As UInt32

    Private dict As Dictionary(Of Integer, String)
    Private dict2 As Dictionary(Of String, UInt32)
    Private StringBlockIndex As UInt32

    Dim writeZero As UInt32 = 0

    Public Sub OpenDBC(ByVal path As String)
        fsSource = New FileStream(path, FileMode.Open, FileAccess.Read)
        bytes = New Byte((fsSource.Length) - 1) {}
        numBytesToRead = CType(fsSource.Length, Integer)
        numBytesRead = 0
        StringBlockIndex = 1
        dict = New Dictionary(Of Integer, String)
    End Sub

    Public Sub CloseDBC()
        fsSource.Close()
    End Sub

    Public Function GetRecordCount() As UInt32
        Return SpellDBC.Header.record_count
    End Function

    Public Sub UpdateListBox()
        Dim i As Integer
        For i = 0 To SpellDBC.Header.record_count - 1
            Form1.ListBox1.Items.Add(SpellDBC.records(i).Id.ToString())
        Next i
        Form1.ListBox1.Update()
    End Sub

    Public Sub ReadHeader()
        Dim Header As New DBC_Header

        fsSource.Read(bytes, numBytesRead, Marshal.SizeOf(Header))

        Dim temp As UInt64
        sizeofUint32 = Marshal.SizeOf(Header.magic)
        sizeofUint64 = Marshal.SizeOf(temp)

        Header.magic = BitConverter.ToUInt32(bytes, numBytesRead)
        numBytesRead = numBytesRead + sizeofUint32
        Header.record_count = BitConverter.ToUInt32(bytes, numBytesRead)
        numBytesRead = numBytesRead + sizeofUint32
        Header.field_count = BitConverter.ToUInt32(bytes, numBytesRead)
        numBytesRead = numBytesRead + sizeofUint32
        Header.record_size = BitConverter.ToUInt32(bytes, numBytesRead)
        numBytesRead = numBytesRead + sizeofUint32
        Header.string_block_size = BitConverter.ToUInt32(bytes, numBytesRead)
        numBytesRead = numBytesRead + sizeofUint32

        SpellDBC.Header = Header

        SpellDBC.records = New SpellRecord(Header.record_count) {}
    End Sub

    Public Sub ReadBody()
        ' Load DBC's
        Dim i As UInt32
        For i = 0 To SpellDBC.Header.record_count - 1

            Dim Spell As New SpellRecord

            fsSource.Read(bytes, numBytesRead, SpellDBC.Header.record_size)

            ReadRecord(Spell, i)

            SpellDBC.records(i) = Spell

            If i Mod 100 = 0 Then
                Form1.prog.bar.PerformStep()
                Application.DoEvents()
            End If
        Next i
        Application.DoEvents()
        ' Load string block
        fsSource.Read(bytes, numBytesRead, SpellDBC.Header.string_block_size)
        numBytesRead = numBytesRead + 1 ' Skip over the first null character
        i = 1
        Dim offset As Integer = 0
        While numBytesRead < numBytesToRead
            Dim cha As Char = " "
            Dim str As String = ""
            cha = Convert.ToChar(bytes(numBytesRead))
            numBytesRead = numBytesRead + 1
            offset = offset + 1
            i = offset
            While cha <> Chr(0)
                str = str & cha
                cha = Convert.ToChar(bytes(numBytesRead))
                numBytesRead = numBytesRead + 1
                offset = offset + 1
            End While
            str = str & cha
            dict.Add(i, str)
        End While
        bytes = {} ' Remove ~50MB from memory
        ' Now to convert the existing spells into strings where needed
        For i = 0 To SpellDBC.Header.record_count - 1
            LoadRecordStrings(SpellDBC.records(i))
        Next i
    End Sub

    Private Sub LoadRecordStrings(ByRef SpellDBC As SpellRecord)
        If SpellDBC.SpellName <> 0 Then
            If dict.ContainsKey(SpellDBC.SpellName) Then
                SpellDBC.SpellName_String = dict.Item(SpellDBC.SpellName)
            End If
        End If
        If SpellDBC.Rank <> 0 Then
            If dict.ContainsKey(SpellDBC.Rank) Then
                SpellDBC.Rank_String = dict.Item(SpellDBC.Rank)
            End If
        End If
        If SpellDBC.Description <> 0 Then
            If dict.ContainsKey(SpellDBC.Description) Then
                SpellDBC.Description_String = dict.Item(SpellDBC.Description)
            End If
        End If
        If SpellDBC.ToolTip <> 0 Then
            If dict.ContainsKey(SpellDBC.ToolTip) Then
                SpellDBC.ToolTip_String = dict.Item(SpellDBC.ToolTip)
            End If
        End If
        If SpellDBC.Local1N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local1N) Then
                SpellDBC.Local1N_String = dict.Item(SpellDBC.Local1N)
            End If
        End If
        If SpellDBC.Local1R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local1R) Then
                SpellDBC.Local1R_String = dict.Item(SpellDBC.Local1R)
            End If
        End If
        If SpellDBC.Local1D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local1D) Then
                SpellDBC.Local1D_String = dict.Item(SpellDBC.Local1D)
            End If
        End If
        If SpellDBC.Local1T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local1T) Then
                SpellDBC.Local1T_String = dict.Item(SpellDBC.Local1T)
            End If
        End If
        If SpellDBC.Local2N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local2N) Then
                SpellDBC.Local2N_String = dict.Item(SpellDBC.Local2N)
            End If
        End If
        If SpellDBC.Local2R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local2R) Then
                SpellDBC.Local2R_String = dict.Item(SpellDBC.Local2R)
            End If
        End If
        If SpellDBC.Local2D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local2D) Then
                SpellDBC.Local2D_String = dict.Item(SpellDBC.Local2D)
            End If
        End If
        If SpellDBC.Local2T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local2T) Then
                SpellDBC.Local2T_String = dict.Item(SpellDBC.Local2T)
            End If
        End If
        If SpellDBC.Local3N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local3N) Then
                SpellDBC.Local3N_String = dict.Item(SpellDBC.Local3N)
            End If
        End If
        If SpellDBC.Local3R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local3R) Then
                SpellDBC.Local3R_String = dict.Item(SpellDBC.Local3R)
            End If
        End If
        If SpellDBC.Local3D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local3D) Then
                SpellDBC.Local3D_String = dict.Item(SpellDBC.Local3D)
            End If
        End If
        If SpellDBC.Local3T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local3T) Then
                SpellDBC.Local3T_String = dict.Item(SpellDBC.Local3T)
            End If
        End If
        If SpellDBC.Local4N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local4N) Then
                SpellDBC.Local4N_String = dict.Item(SpellDBC.Local4N)
            End If
        End If
        If SpellDBC.Local4R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local4R) Then
                SpellDBC.Local4R_String = dict.Item(SpellDBC.Local4R)
            End If
        End If
        If SpellDBC.Local4D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local4D) Then
                SpellDBC.Local4D_String = dict.Item(SpellDBC.Local4D)
            End If
        End If
        If SpellDBC.Local4T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local4T) Then
                SpellDBC.Local4T_String = dict.Item(SpellDBC.Local4T)
            End If
        End If
        If SpellDBC.Local5N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local5N) Then
                SpellDBC.Local5N_String = dict.Item(SpellDBC.Local5N)
            End If
        End If
        If SpellDBC.Local5R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local5R) Then
                SpellDBC.Local5R_String = dict.Item(SpellDBC.Local5R)
            End If
        End If
        If SpellDBC.Local5D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local5D) Then
                SpellDBC.Local5D_String = dict.Item(SpellDBC.Local5D)
            End If
        End If
        If SpellDBC.Local5T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local5T) Then
                SpellDBC.Local5T_String = dict.Item(SpellDBC.Local5T)
            End If
        End If
        If SpellDBC.Local6N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local6N) Then
                SpellDBC.Local6N_String = dict.Item(SpellDBC.Local6N)
            End If
        End If
        If SpellDBC.Local6R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local6R) Then
                SpellDBC.Local6R_String = dict.Item(SpellDBC.Local6R)
            End If
        End If
        If SpellDBC.Local6D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local6D) Then
                SpellDBC.Local6D_String = dict.Item(SpellDBC.Local6D)
            End If
        End If
        If SpellDBC.Local6T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local6T) Then
                SpellDBC.Local6T_String = dict.Item(SpellDBC.Local6T)
            End If
        End If
        If SpellDBC.Local7N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local7N) Then
                SpellDBC.Local7N_String = dict.Item(SpellDBC.Local7N)
            End If
        End If
        If SpellDBC.Local7R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local7R) Then
                SpellDBC.Local7R_String = dict.Item(SpellDBC.Local7R)
            End If
        End If
        If SpellDBC.Local7D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local7D) Then
                SpellDBC.Local7D_String = dict.Item(SpellDBC.Local7D)
            End If
        End If
        If SpellDBC.Local7T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local7T) Then
                SpellDBC.Local7T_String = dict.Item(SpellDBC.Local7T)
            End If
        End If
        If SpellDBC.Local8N <> 0 Then
            If dict.ContainsKey(SpellDBC.Local8N) Then
                SpellDBC.Local8N_String = dict.Item(SpellDBC.Local8N)
            End If
        End If
        If SpellDBC.Local8R <> 0 Then
            If dict.ContainsKey(SpellDBC.Local8R) Then
                SpellDBC.Local8R_String = dict.Item(SpellDBC.Local8R)
            End If
        End If
        If SpellDBC.Local8D <> 0 Then
            If dict.ContainsKey(SpellDBC.Local8D) Then
                SpellDBC.Local8D_String = dict.Item(SpellDBC.Local8D)
            End If
        End If
        If SpellDBC.Local8T <> 0 Then
            If dict.ContainsKey(SpellDBC.Local8T) Then
                SpellDBC.Local8T_String = dict.Item(SpellDBC.Local8T)
            End If
        End If
    End Sub

    Public Sub GenerateDataTable(ByVal index As Integer)
        Form1.dt = New DataTable
        For i = 0 To 204 ' Need to leave room for localisation
            Form1.dt.Columns.Add(i.ToString())
        Next i
        Dim dr As DataRow = Form1.dt.NewRow
        dr(0) = System.Guid.NewGuid()
        Form1.dt.Rows.Add(dr)

        PopulateDataTable(index)
    End Sub

    Private Sub GenerateStringBlock(ByRef Spell As SpellRecord, ByRef writer As BinaryWriter)
        ' ENGB/ENUS
        If (Spell.SpellName_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.SpellName_String) Then
                Spell.SpellName_String = dict2.Item(Spell.SpellName_String)
            Else
                Dim l As UInt32 = Spell.SpellName_String.Length
                writer.Write(Spell.SpellName_String.ToCharArray())
                dict2.Add(Spell.SpellName_String, StringBlockIndex)
                Spell.SpellName_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Rank_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Rank_String) Then
                Spell.Rank_String = dict2.Item(Spell.Rank_String)
            Else
                Dim l As UInt32 = Spell.Rank_String.Length
                writer.Write(Spell.Rank_String.ToCharArray())
                dict2.Add(Spell.Rank_String, StringBlockIndex)
                Spell.Rank_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Description_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Description_String) Then
                Spell.Description_String = dict2.Item(Spell.Description_String)
            Else
                Dim l As UInt32 = Spell.Description_String.Length
                writer.Write(Spell.Description_String.ToCharArray())
                dict2.Add(Spell.Description_String, StringBlockIndex)
                Spell.Description_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.ToolTip_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.ToolTip_String) Then
                Spell.ToolTip_String = dict2.Item(Spell.ToolTip_String)
            Else
                Dim l As UInt32 = Spell.ToolTip_String.Length
                writer.Write(Spell.ToolTip_String.ToCharArray())
                dict2.Add(Spell.ToolTip_String, StringBlockIndex)
                Spell.ToolTip_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local1N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local1N_String) Then
                Spell.Local1N_String = dict2.Item(Spell.Local1N_String)
            Else
                Dim l As UInt32 = Spell.Local1N_String.Length
                writer.Write(Spell.Local1N_String.ToCharArray())
                dict2.Add(Spell.Local1N_String, StringBlockIndex)
                Spell.Local1N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local1R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local1R_String) Then
                Spell.Local1R_String = dict2.Item(Spell.Local1R_String)
            Else
                Dim l As UInt32 = Spell.Local1R_String.Length
                writer.Write(Spell.Local1R_String.ToCharArray())
                dict2.Add(Spell.Local1R_String, StringBlockIndex)
                Spell.Local1R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local1D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local1D_String) Then
                Spell.Local1D_String = dict2.Item(Spell.Local1D_String)
            Else
                Dim l As UInt32 = Spell.Local1D_String.Length
                writer.Write(Spell.Local1D_String.ToCharArray())
                dict2.Add(Spell.Local1D_String, StringBlockIndex)
                Spell.Local1D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local1T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local1T_String) Then
                Spell.Local1T_String = dict2.Item(Spell.Local1T_String)
            Else
                Dim l As UInt32 = Spell.Local1T_String.Length
                writer.Write(Spell.Local1T_String.ToCharArray())
                dict2.Add(Spell.Local1T_String, StringBlockIndex)
                Spell.Local1T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local2N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local2N_String) Then
                Spell.Local2N_String = dict2.Item(Spell.Local2N_String)
            Else
                Dim l As UInt32 = Spell.Local2N_String.Length
                writer.Write(Spell.Local2N_String.ToCharArray())
                dict2.Add(Spell.Local2N_String, StringBlockIndex)
                Spell.Local2N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local2R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local2R_String) Then
                Spell.Local2R_String = dict2.Item(Spell.Local2R_String)
            Else
                Dim l As UInt32 = Spell.Local2R_String.Length
                writer.Write(Spell.Local2R_String.ToCharArray())
                dict2.Add(Spell.Local2R_String, StringBlockIndex)
                Spell.Local2R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local2D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local2D_String) Then
                Spell.Local2D_String = dict2.Item(Spell.Local2D_String)
            Else
                Dim l As UInt32 = Spell.Local2D_String.Length
                writer.Write(Spell.Local2D_String.ToCharArray())
                dict2.Add(Spell.Local2D_String, StringBlockIndex)
                Spell.Local2D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local2T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local2T_String) Then
                Spell.Local2T_String = dict2.Item(Spell.Local2T_String)
            Else
                Dim l As UInt32 = Spell.Local2T_String.Length
                writer.Write(Spell.Local2T_String.ToCharArray())
                dict2.Add(Spell.Local2T_String, StringBlockIndex)
                Spell.Local2T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local3N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local3N_String) Then
                Spell.Local3N_String = dict2.Item(Spell.Local3N_String)
            Else
                Dim l As UInt32 = Spell.Local3N_String.Length
                writer.Write(Spell.Local3N_String.ToCharArray())
                dict2.Add(Spell.Local3N_String, StringBlockIndex)
                Spell.Local3N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local3R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local3R_String) Then
                Spell.Local3R_String = dict2.Item(Spell.Local3R_String)
            Else
                Dim l As UInt32 = Spell.Local3R_String.Length
                writer.Write(Spell.Local3R_String.ToCharArray())
                dict2.Add(Spell.Local3R_String, StringBlockIndex)
                Spell.Local3R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local3D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local3D_String) Then
                Spell.Local3D_String = dict2.Item(Spell.Local3D_String)
            Else
                Dim l As UInt32 = Spell.Local3D_String.Length
                writer.Write(Spell.Local3D_String.ToCharArray())
                dict2.Add(Spell.Local3D_String, StringBlockIndex)
                Spell.Local3D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local3T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local3T_String) Then
                Spell.Local3T_String = dict2.Item(Spell.Local3T_String)
            Else
                Dim l As UInt32 = Spell.Local3T_String.Length
                writer.Write(Spell.Local3T_String.ToCharArray())
                dict2.Add(Spell.Local3T_String, StringBlockIndex)
                Spell.Local3T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local4N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local4N_String) Then
                Spell.Local4N_String = dict2.Item(Spell.Local4N_String)
            Else
                Dim l As UInt32 = Spell.Local4N_String.Length
                writer.Write(Spell.Local4N_String.ToCharArray())
                dict2.Add(Spell.Local4N_String, StringBlockIndex)
                Spell.Local4N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local4R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local4R_String) Then
                Spell.Local4R_String = dict2.Item(Spell.Local4R_String)
            Else
                Dim l As UInt32 = Spell.Local4R_String.Length
                writer.Write(Spell.Local4R_String.ToCharArray())
                dict2.Add(Spell.Local4R_String, StringBlockIndex)
                Spell.Local4R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local4D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local4D_String) Then
                Spell.Local4D_String = dict2.Item(Spell.Local4D_String)
            Else
                Dim l As UInt32 = Spell.Local4D_String.Length
                writer.Write(Spell.Local4D_String.ToCharArray())
                dict2.Add(Spell.Local4D_String, StringBlockIndex)
                Spell.Local4D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local4T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local4T_String) Then
                Spell.Local4T_String = dict2.Item(Spell.Local4T_String)
            Else
                Dim l As UInt32 = Spell.Local4T_String.Length
                writer.Write(Spell.Local4T_String.ToCharArray())
                dict2.Add(Spell.Local4T_String, StringBlockIndex)
                Spell.Local4T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local5N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local5N_String) Then
                Spell.Local5N_String = dict2.Item(Spell.Local5N_String)
            Else
                Dim l As UInt32 = Spell.Local5N_String.Length
                writer.Write(Spell.Local5N_String.ToCharArray())
                dict2.Add(Spell.Local5N_String, StringBlockIndex)
                Spell.Local5N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local5R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local5R_String) Then
                Spell.Local5R_String = dict2.Item(Spell.Local5R_String)
            Else
                Dim l As UInt32 = Spell.Local5R_String.Length
                writer.Write(Spell.Local5R_String.ToCharArray())
                dict2.Add(Spell.Local5R_String, StringBlockIndex)
                Spell.Local5R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local5D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local5D_String) Then
                Spell.Local5D_String = dict2.Item(Spell.Local5D_String)
            Else
                Dim l As UInt32 = Spell.Local5D_String.Length
                writer.Write(Spell.Local5D_String.ToCharArray())
                dict2.Add(Spell.Local5D_String, StringBlockIndex)
                Spell.Local5D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local5T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local5T_String) Then
                Spell.Local5T_String = dict2.Item(Spell.Local5T_String)
            Else
                Dim l As UInt32 = Spell.Local5T_String.Length
                writer.Write(Spell.Local5T_String.ToCharArray())
                dict2.Add(Spell.Local5T_String, StringBlockIndex)
                Spell.Local5T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local6N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local6N_String) Then
                Spell.Local6N_String = dict2.Item(Spell.Local6N_String)
            Else
                Dim l As UInt32 = Spell.Local6N_String.Length
                writer.Write(Spell.Local6N_String.ToCharArray())
                dict2.Add(Spell.Local6N_String, StringBlockIndex)
                Spell.Local6N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local6R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local6R_String) Then
                Spell.Local6R_String = dict2.Item(Spell.Local6R_String)
            Else
                Dim l As UInt32 = Spell.Local6R_String.Length
                writer.Write(Spell.Local6R_String.ToCharArray())
                dict2.Add(Spell.Local6R_String, StringBlockIndex)
                Spell.Local6R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local6D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local6D_String) Then
                Spell.Local6D_String = dict2.Item(Spell.Local6D_String)
            Else
                Dim l As UInt32 = Spell.Local6D_String.Length
                writer.Write(Spell.Local6D_String.ToCharArray())
                dict2.Add(Spell.Local6D_String, StringBlockIndex)
                Spell.Local6D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local6T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local6T_String) Then
                Spell.Local6T_String = dict2.Item(Spell.Local6T_String)
            Else
                Dim l As UInt32 = Spell.Local6T_String.Length
                writer.Write(Spell.Local6T_String.ToCharArray())
                dict2.Add(Spell.Local6T_String, StringBlockIndex)
                Spell.Local6T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local7N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local7N_String) Then
                Spell.Local7N_String = dict2.Item(Spell.Local7N_String)
            Else
                Dim l As UInt32 = Spell.Local7N_String.Length
                writer.Write(Spell.Local7N_String.ToCharArray())
                dict2.Add(Spell.Local7N_String, StringBlockIndex)
                Spell.Local7N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local7R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local7R_String) Then
                Spell.Local7R_String = dict2.Item(Spell.Local7R_String)
            Else
                Dim l As UInt32 = Spell.Local7R_String.Length
                writer.Write(Spell.Local7R_String.ToCharArray())
                dict2.Add(Spell.Local7R_String, StringBlockIndex)
                Spell.Local7R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local7D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local7D_String) Then
                Spell.Local7D_String = dict2.Item(Spell.Local7D_String)
            Else
                Dim l As UInt32 = Spell.Local7D_String.Length
                writer.Write(Spell.Local7D_String.ToCharArray())
                dict2.Add(Spell.Local7D_String, StringBlockIndex)
                Spell.Local7D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local7T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local7T_String) Then
                Spell.Local7T_String = dict2.Item(Spell.Local7T_String)
            Else
                Dim l As UInt32 = Spell.Local7T_String.Length
                writer.Write(Spell.Local7T_String.ToCharArray())
                dict2.Add(Spell.Local7T_String, StringBlockIndex)
                Spell.Local7T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        ' LOCALES
        If (Spell.Local8N_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local8N_String) Then
                Spell.Local8N_String = dict2.Item(Spell.Local8N_String)
            Else
                Dim l As UInt32 = Spell.Local8N_String.Length
                writer.Write(Spell.Local8N_String.ToCharArray())
                dict2.Add(Spell.Local8N_String, StringBlockIndex)
                Spell.Local8N_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local8R_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local8R_String) Then
                Spell.Local8R_String = dict2.Item(Spell.Local8R_String)
            Else
                Dim l As UInt32 = Spell.Local8R_String.Length
                writer.Write(Spell.Local8R_String.ToCharArray())
                dict2.Add(Spell.Local8R_String, StringBlockIndex)
                Spell.Local8R_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local8D_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local8D_String) Then
                Spell.Local8D_String = dict2.Item(Spell.Local8D_String)
            Else
                Dim l As UInt32 = Spell.Local8D_String.Length
                writer.Write(Spell.Local8D_String.ToCharArray())
                dict2.Add(Spell.Local8D_String, StringBlockIndex)
                Spell.Local8D_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
        If (Spell.Local8T_String.Length <> 0) Then
            If dict2.ContainsKey(Spell.Local8T_String) Then
                Spell.Local8T_String = dict2.Item(Spell.Local8T_String)
            Else
                Dim l As UInt32 = Spell.Local8T_String.Length
                writer.Write(Spell.Local8T_String.ToCharArray())
                dict2.Add(Spell.Local8T_String, StringBlockIndex)
                Spell.Local8T_String = StringBlockIndex
                StringBlockIndex = StringBlockIndex + l
            End If
        End If
    End Sub

    Private Sub ReadRecord(ByRef Spell As SpellRecord, ByRef i As UInt32)
        Try
            Spell.Id = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Category = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Dispel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Mechanic = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Attributes = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx4 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx5 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx6 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AttributesEx7 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Stances = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.unk_320_2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.StancesNot = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.unk_320_3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Targets = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.TargetCreatureType = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.RequiresSpellFocus = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.FacingCasterFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.CasterAuraState = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.TargetAuraState = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.CasterAuraStateNot = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.TargetAuraStateNot = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.casterAuraSpell = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.targetAuraSpell = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.excludeCasterAuraSpell = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.excludeTargetAuraSpell = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.CastingTimeIndex = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.RecoveryTime = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.CategoryRecoveryTime = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.InterruptFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AuraInterruptFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ChannelInterruptFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.procFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.procChance = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.procCharges = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.maxLevel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.baseLevel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.spellLevel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.DurationIndex = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.powerType = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.manaCost = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.manaCostPerlevel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.manaPerSecond = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.manaPerSecondPerLevel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.rangeIndex = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.speed = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.modalNextSpell = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.StackAmount = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Totem1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Totem2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent1 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent2 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent3 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent4 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent5 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent6 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent7 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Reagent8 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount4 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount5 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount6 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount7 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ReagentCount8 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EquippedItemClass = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EquippedItemSubClassMask = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EquippedItemInventoryTypeMask = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Effect1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Effect2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Effect3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectDieSides1 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectDieSides2 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectDieSides3 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectRealPointsPerLevel1 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectRealPointsPerLevel2 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectRealPointsPerLevel3 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectBasePoints1 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectBasePoints2 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectBasePoints3 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMechanic1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMechanic2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMechanic3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectImplicitTargetA1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectImplicitTargetA2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectImplicitTargetA3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectImplicitTargetB1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectImplicitTargetB2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectImplicitTargetB3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectRadiusIndex1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectRadiusIndex2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectRadiusIndex3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectApplyAuraName1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectApplyAuraName2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectApplyAuraName3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectAmplitude1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectAmplitude2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectAmplitude3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMultipleValue1 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMultipleValue2 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMultipleValue3 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectChainTarget1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectChainTarget2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectChainTarget3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectItemType1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectItemType2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectItemType3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMiscValue1 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMiscValue2 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMiscValue3 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMiscValueB1 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMiscValueB2 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectMiscValueB3 = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectTriggerSpell1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectTriggerSpell2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectTriggerSpell3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectPointsPerComboPoint1 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectPointsPerComboPoint2 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectPointsPerComboPoint3 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskA1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskA2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskA3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskB1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskB2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskB3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskC1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskC2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectSpellClassMaskC3 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellVisual1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellVisual2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellIconID = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.activeIconID = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.spellPriority = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellName = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local1N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local2N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local3N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local4N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local5N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local6N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local7N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local8N = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32 * 8 ' 8 more unknown bitmasks
            Spell.SpellNameFlag = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Rank = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local1R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local2R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local3R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local4R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local5R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local6R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local7R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local8R = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32 * 8 ' 8 more unknown bitmasks
            Spell.RankFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Description = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local1D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local2D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local3D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local4D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local5D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local6D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local7D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local8D = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32 * 8 ' 8 more unknown bitmasks
            Spell.DescriptionFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ToolTip = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local1T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local2T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local3T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local4T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local5T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local6T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local7T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Local8T = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32 * 8 ' 8 more unknown bitmasks
            Spell.ToolTipFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ManaCostPercentage = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.StartRecoveryCategory = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.StartRecoveryTime = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.MaxTargetLevel = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellFamilyName = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellFamilyFlags = BitConverter.ToUInt64(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint64
            Spell.SpellFamilyFlags2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.MaxAffectedTargets = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.DmgClass = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.PreventionType = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.StanceBarOrder = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.DmgMultiplier1 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.DmgMultiplier2 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.DmgMultiplier3 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.MinFactionId = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.MinReputation = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.RequiredAuraVision = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.TotemCategory1 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.TotemCategory2 = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.AreaGroupId = BitConverter.ToInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SchoolMask = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.runeCostID = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.spellMissileID = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.PowerDisplayId = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectBonusMultiplier1 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectBonusMultiplier2 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.EffectBonusMultiplier3 = BitConverter.ToSingle(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.spellDescriptionVariableID = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellDifficultyId = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.SpellName_String = ""
            Spell.Rank_String = ""
            Spell.ToolTip_String = ""
            Spell.Description_String = ""
            Spell.Local1N_String = ""
            Spell.Local1R_String = ""
            Spell.Local1D_String = ""
            Spell.Local1T_String = ""
            Spell.Local2N_String = ""
            Spell.Local2R_String = ""
            Spell.Local2D_String = ""
            Spell.Local2T_String = ""
            Spell.Local3N_String = ""
            Spell.Local3R_String = ""
            Spell.Local3D_String = ""
            Spell.Local3T_String = ""
            Spell.Local4N_String = ""
            Spell.Local4R_String = ""
            Spell.Local4D_String = ""
            Spell.Local4T_String = ""
            Spell.Local5N_String = ""
            Spell.Local5R_String = ""
            Spell.Local5D_String = ""
            Spell.Local5T_String = ""
            Spell.Local6N_String = ""
            Spell.Local6R_String = ""
            Spell.Local6D_String = ""
            Spell.Local6T_String = ""
            Spell.Local7N_String = ""
            Spell.Local7R_String = ""
            Spell.Local7D_String = ""
            Spell.Local7T_String = ""
            Spell.Local8N_String = ""
            Spell.Local8R_String = ""
            Spell.Local8D_String = ""
            Spell.Local8T_String = ""
        Catch ex As Exception
            Dim f As New StreamWriter("error_log.txt", True)
            Dim out As String = "Record ID Failed To Read: " & i.ToString() & ": " & ex.Message.ToString() & vbNewLine
            f.Write(out)
            f.Close()
        End Try
    End Sub

    Public Sub DumpRecordsDebug()
        dict2 = New Dictionary(Of String, UInt32)
        Form1.prog = New progress
        Form1.prog.bar.Maximum = SpellDBC.Header.record_count - 1
        Form1.prog.bar.Step = 100
        Form1.prog.Show()
        Dim writer As New BinaryWriter(File.Open("string_block.temp", FileMode.Create))
        Dim i As UInt32 = 0
        writer.Write(Chr(0))
        dict2.Add("", 0)
        For i = 0 To SpellDBC.Header.record_count - 1
            GenerateStringBlock(SpellDBC.records(i), writer)
            If i Mod 100 = 0 Then
                Form1.prog.bar.PerformStep()
                Application.DoEvents()
            End If
        Next i
        Dim index As UInt32 = 0
        writer.Close()
        OpenDBC("string_block.temp")
        SpellDBC.Header.string_block_size = fsSource.Length
        bytes = New Byte((fsSource.Length) - 1) {}
        fsSource.Read(bytes, 0, fsSource.Length)
        CloseDBC()
        writer = New BinaryWriter(File.Open("new_Spell.dbc", FileMode.Create))
        writer.Write(SpellDBC.Header.magic)
        writer.Write(SpellDBC.Header.record_count)
        writer.Write(SpellDBC.Header.field_count)
        writer.Write(SpellDBC.Header.record_size)
        writer.Write(SpellDBC.Header.string_block_size)
        For i = 0 To SpellDBC.Header.record_count - 1
            DumpRecord(SpellDBC.records(i), writer)
        Next i
        writer.Write(bytes) ' string block
        writer.Close()
        bytes = {}
        Form1.prog.Close()
        File.Delete("string_block.temp")
    End Sub

    Private Sub DumpRecord(ByRef spell As SpellRecord, ByRef writer As BinaryWriter)
        Dim i As UInt32
        Dim k As UInt32 = 0
        writer.Write(spell.Id)
        writer.Write(spell.Category)
        writer.Write(spell.Dispel)
        writer.Write(spell.Mechanic)
        writer.Write(spell.Attributes)
        writer.Write(spell.AttributesEx)
        writer.Write(spell.AttributesEx2)
        writer.Write(spell.AttributesEx3)
        writer.Write(spell.AttributesEx4)
        writer.Write(spell.AttributesEx5)
        writer.Write(spell.AttributesEx6)
        writer.Write(spell.AttributesEx7)
        writer.Write(spell.Stances)
        writer.Write(spell.unk_320_2)
        writer.Write(spell.StancesNot)
        writer.Write(spell.unk_320_3)
        writer.Write(spell.Targets)
        writer.Write(spell.TargetCreatureType)
        writer.Write(spell.RequiresSpellFocus)
        writer.Write(spell.FacingCasterFlags)
        writer.Write(spell.CasterAuraState)
        writer.Write(spell.TargetAuraState)
        writer.Write(spell.CasterAuraStateNot)
        writer.Write(spell.TargetAuraStateNot)
        writer.Write(spell.casterAuraSpell)
        writer.Write(spell.targetAuraSpell)
        writer.Write(spell.excludeCasterAuraSpell)
        writer.Write(spell.excludeTargetAuraSpell)
        writer.Write(spell.CastingTimeIndex)
        writer.Write(spell.RecoveryTime)
        writer.Write(spell.CategoryRecoveryTime)
        writer.Write(spell.InterruptFlags)
        writer.Write(spell.AuraInterruptFlags)
        writer.Write(spell.ChannelInterruptFlags)
        writer.Write(spell.procFlags)
        writer.Write(spell.procChance)
        writer.Write(spell.procCharges)
        writer.Write(spell.maxLevel)
        writer.Write(spell.baseLevel)
        writer.Write(spell.spellLevel)
        writer.Write(spell.DurationIndex)
        writer.Write(spell.powerType)
        writer.Write(spell.manaCost)
        writer.Write(spell.manaCostPerlevel)
        writer.Write(spell.manaPerSecond)
        writer.Write(spell.manaPerSecondPerLevel)
        writer.Write(spell.rangeIndex)
        writer.Write(spell.speed)
        writer.Write(spell.modalNextSpell)
        writer.Write(spell.StackAmount)
        writer.Write(spell.Totem1)
        writer.Write(spell.Totem2)
        writer.Write(spell.Reagent1)
        writer.Write(spell.Reagent2)
        writer.Write(spell.Reagent3)
        writer.Write(spell.Reagent4)
        writer.Write(spell.Reagent5)
        writer.Write(spell.Reagent6)
        writer.Write(spell.Reagent7)
        writer.Write(spell.Reagent8)
        writer.Write(spell.ReagentCount1)
        writer.Write(spell.ReagentCount2)
        writer.Write(spell.ReagentCount3)
        writer.Write(spell.ReagentCount4)
        writer.Write(spell.ReagentCount5)
        writer.Write(spell.ReagentCount6)
        writer.Write(spell.ReagentCount7)
        writer.Write(spell.ReagentCount8)
        writer.Write(spell.EquippedItemClass)
        writer.Write(spell.EquippedItemSubClassMask)
        writer.Write(spell.EquippedItemInventoryTypeMask)
        writer.Write(spell.Effect1)
        writer.Write(spell.Effect2)
        writer.Write(spell.Effect3)
        writer.Write(spell.EffectDieSides1)
        writer.Write(spell.EffectDieSides2)
        writer.Write(spell.EffectDieSides3)
        writer.Write(spell.EffectRealPointsPerLevel1)
        writer.Write(spell.EffectRealPointsPerLevel2)
        writer.Write(spell.EffectRealPointsPerLevel3)
        writer.Write(spell.EffectBasePoints1)
        writer.Write(spell.EffectBasePoints2)
        writer.Write(spell.EffectBasePoints3)
        writer.Write(spell.EffectMechanic1)
        writer.Write(spell.EffectMechanic2)
        writer.Write(spell.EffectMechanic3)
        writer.Write(spell.EffectImplicitTargetA1)
        writer.Write(spell.EffectImplicitTargetA2)
        writer.Write(spell.EffectImplicitTargetA3)
        writer.Write(spell.EffectImplicitTargetB1)
        writer.Write(spell.EffectImplicitTargetB2)
        writer.Write(spell.EffectImplicitTargetB3)
        writer.Write(spell.EffectRadiusIndex1)
        writer.Write(spell.EffectRadiusIndex2)
        writer.Write(spell.EffectRadiusIndex3)
        writer.Write(spell.EffectApplyAuraName1)
        writer.Write(spell.EffectApplyAuraName2)
        writer.Write(spell.EffectApplyAuraName3)
        writer.Write(spell.EffectAmplitude1)
        writer.Write(spell.EffectAmplitude2)
        writer.Write(spell.EffectAmplitude3)
        writer.Write(spell.EffectMultipleValue1)
        writer.Write(spell.EffectMultipleValue2)
        writer.Write(spell.EffectMultipleValue3)
        writer.Write(spell.EffectChainTarget1)
        writer.Write(spell.EffectChainTarget2)
        writer.Write(spell.EffectChainTarget3)
        writer.Write(spell.EffectItemType1)
        writer.Write(spell.EffectItemType2)
        writer.Write(spell.EffectItemType3)
        writer.Write(spell.EffectMiscValue1)
        writer.Write(spell.EffectMiscValue2)
        writer.Write(spell.EffectMiscValue3)
        writer.Write(spell.EffectMiscValueB1)
        writer.Write(spell.EffectMiscValueB2)
        writer.Write(spell.EffectMiscValueB3)
        writer.Write(spell.EffectTriggerSpell1)
        writer.Write(spell.EffectTriggerSpell2)
        writer.Write(spell.EffectTriggerSpell3)
        writer.Write(spell.EffectPointsPerComboPoint1)
        writer.Write(spell.EffectPointsPerComboPoint2)
        writer.Write(spell.EffectPointsPerComboPoint3)
        writer.Write(spell.EffectSpellClassMaskA1)
        writer.Write(spell.EffectSpellClassMaskA2)
        writer.Write(spell.EffectSpellClassMaskA3)
        writer.Write(spell.EffectSpellClassMaskB1)
        writer.Write(spell.EffectSpellClassMaskB2)
        writer.Write(spell.EffectSpellClassMaskB3)
        writer.Write(spell.EffectSpellClassMaskC1)
        writer.Write(spell.EffectSpellClassMaskC2)
        writer.Write(spell.EffectSpellClassMaskC3)
        writer.Write(spell.SpellVisual1)
        writer.Write(spell.SpellVisual2)
        writer.Write(spell.SpellIconID)
        writer.Write(spell.activeIconID)
        writer.Write(spell.spellPriority)
        If spell.SpellName_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.SpellName_String))
        End If
        If spell.Local1N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local1N_String))
        End If
        If spell.Local2N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local2N_String))
        End If
        If spell.Local3N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local3N_String))
        End If
        If spell.Local4N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local4N_String))
        End If
        If spell.Local5N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local5N_String))
        End If
        If spell.Local6N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local6N_String))
        End If
        If spell.Local7N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local7N_String))
        End If
        If spell.Local8N_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local8N_String))
        End If
        For i = 1 To 7
            writer.Write(k)
        Next
        writer.Write(spell.SpellNameFlag)
        If spell.Rank_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Rank_String))
        End If
        If spell.Local1R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local1R_String))
        End If
        If spell.Local2R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local2R_String))
        End If
        If spell.Local3R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local3R_String))
        End If
        If spell.Local4R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local4R_String))
        End If
        If spell.Local5R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local5R_String))
        End If
        If spell.Local6R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local6R_String))
        End If
        If spell.Local7R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local7R_String))
        End If
        If spell.Local8R_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local8R_String))
        End If
        For i = 1 To 7
            writer.Write(k)
        Next
        writer.Write(spell.RankFlags)
        If spell.Description_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Description_String))
        End If
        If spell.Local1D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local1D_String))
        End If
        If spell.Local2D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local2D_String))
        End If
        If spell.Local3D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local3D_String))
        End If
        If spell.Local4D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local4D_String))
        End If
        If spell.Local5D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local5D_String))
        End If
        If spell.Local6D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local6D_String))
        End If
        If spell.Local7D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local7D_String))
        End If
        If spell.Local8D_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local8D_String))
        End If
        For i = 1 To 7
            writer.Write(k)
        Next
        writer.Write(spell.DescriptionFlags)
        If spell.ToolTip_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.ToolTip_String))
        End If
        If spell.Local1T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local1T_String))
        End If
        If spell.Local2T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local2T_String))
        End If
        If spell.Local3T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local3T_String))
        End If
        If spell.Local4T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local4T_String))
        End If
        If spell.Local5T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local5T_String))
        End If
        If spell.Local6T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local6T_String))
        End If
        If spell.Local7T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local7T_String))
        End If
        If spell.Local8T_String.Length = 0 Then
            writer.Write(writeZero)
        Else
            writer.Write(UInt32.Parse(spell.Local8T_String))
        End If
        For i = 1 To 7
            writer.Write(k)
        Next
        writer.Write(spell.ToolTipFlags)
        writer.Write(spell.ManaCostPercentage)
        writer.Write(spell.StartRecoveryCategory)
        writer.Write(spell.StartRecoveryTime)
        writer.Write(spell.MaxTargetLevel)
        writer.Write(spell.SpellFamilyName)
        writer.Write(spell.SpellFamilyFlags)
        writer.Write(spell.SpellFamilyFlags2)
        writer.Write(spell.MaxAffectedTargets)
        writer.Write(spell.DmgClass)
        writer.Write(spell.PreventionType)
        writer.Write(spell.StanceBarOrder)
        writer.Write(spell.DmgMultiplier1)
        writer.Write(spell.DmgMultiplier2)
        writer.Write(spell.DmgMultiplier3)
        writer.Write(spell.MinFactionId)
        writer.Write(spell.MinReputation)
        writer.Write(spell.RequiredAuraVision)
        writer.Write(spell.TotemCategory1)
        writer.Write(spell.TotemCategory2)
        writer.Write(spell.AreaGroupId)
        writer.Write(spell.SchoolMask)
        writer.Write(spell.runeCostID)
        writer.Write(spell.spellMissileID)
        writer.Write(spell.PowerDisplayId)
        writer.Write(spell.EffectBonusMultiplier1)
        writer.Write(spell.EffectBonusMultiplier2)
        writer.Write(spell.EffectBonusMultiplier3)
        writer.Write(spell.spellDescriptionVariableID)
        writer.Write(spell.SpellDifficultyId)
    End Sub

    Sub PopulateDataTable(ByVal index As Integer)
        For i = 0 To SpellDBC.records.Count - 1
            If SpellDBC.records(i).Id = index Then
                index = i
                Exit For
            End If
        Next i
        Form1.dt.Rows(0).Item(0) = SpellDBC.records(index).Id
        Form1.dt.Rows(0).Item(1) = SpellDBC.records(index).Category
        Form1.dt.Rows(0).Item(2) = SpellDBC.records(index).Dispel
        Form1.dt.Rows(0).Item(3) = SpellDBC.records(index).Mechanic
        Form1.dt.Rows(0).Item(4) = SpellDBC.records(index).Attributes
        Form1.dt.Rows(0).Item(5) = SpellDBC.records(index).AttributesEx
        Form1.dt.Rows(0).Item(6) = SpellDBC.records(index).AttributesEx2
        Form1.dt.Rows(0).Item(7) = SpellDBC.records(index).AttributesEx3
        Form1.dt.Rows(0).Item(8) = SpellDBC.records(index).AttributesEx4
        Form1.dt.Rows(0).Item(9) = SpellDBC.records(index).AttributesEx5
        Form1.dt.Rows(0).Item(10) = SpellDBC.records(index).AttributesEx6
        Form1.dt.Rows(0).Item(11) = SpellDBC.records(index).AttributesEx7
        Form1.dt.Rows(0).Item(12) = SpellDBC.records(index).Stances
        Form1.dt.Rows(0).Item(13) = SpellDBC.records(index).unk_320_2
        Form1.dt.Rows(0).Item(14) = SpellDBC.records(index).StancesNot
        Form1.dt.Rows(0).Item(15) = SpellDBC.records(index).unk_320_3
        Form1.dt.Rows(0).Item(16) = SpellDBC.records(index).Targets
        Form1.dt.Rows(0).Item(17) = SpellDBC.records(index).TargetCreatureType
        Form1.dt.Rows(0).Item(18) = SpellDBC.records(index).RequiresSpellFocus
        Form1.dt.Rows(0).Item(19) = SpellDBC.records(index).FacingCasterFlags
        Form1.dt.Rows(0).Item(20) = SpellDBC.records(index).CasterAuraState
        Form1.dt.Rows(0).Item(21) = SpellDBC.records(index).TargetAuraState
        Form1.dt.Rows(0).Item(22) = SpellDBC.records(index).CasterAuraStateNot
        Form1.dt.Rows(0).Item(23) = SpellDBC.records(index).TargetAuraStateNot
        Form1.dt.Rows(0).Item(24) = SpellDBC.records(index).casterAuraSpell
        Form1.dt.Rows(0).Item(25) = SpellDBC.records(index).targetAuraSpell
        Form1.dt.Rows(0).Item(26) = SpellDBC.records(index).excludeCasterAuraSpell
        Form1.dt.Rows(0).Item(27) = SpellDBC.records(index).excludeTargetAuraSpell
        Form1.dt.Rows(0).Item(28) = SpellDBC.records(index).CastingTimeIndex
        Form1.dt.Rows(0).Item(29) = SpellDBC.records(index).RecoveryTime
        Form1.dt.Rows(0).Item(30) = SpellDBC.records(index).CategoryRecoveryTime
        Form1.dt.Rows(0).Item(31) = SpellDBC.records(index).InterruptFlags
        Form1.dt.Rows(0).Item(32) = SpellDBC.records(index).AuraInterruptFlags
        Form1.dt.Rows(0).Item(33) = SpellDBC.records(index).ChannelInterruptFlags
        Form1.dt.Rows(0).Item(34) = SpellDBC.records(index).procFlags
        Form1.dt.Rows(0).Item(35) = SpellDBC.records(index).procChance
        Form1.dt.Rows(0).Item(36) = SpellDBC.records(index).procCharges
        Form1.dt.Rows(0).Item(37) = SpellDBC.records(index).maxLevel
        Form1.dt.Rows(0).Item(38) = SpellDBC.records(index).baseLevel
        Form1.dt.Rows(0).Item(39) = SpellDBC.records(index).spellLevel
        Form1.dt.Rows(0).Item(40) = SpellDBC.records(index).DurationIndex
        Form1.dt.Rows(0).Item(41) = SpellDBC.records(index).powerType
        Form1.dt.Rows(0).Item(42) = SpellDBC.records(index).manaCost
        Form1.dt.Rows(0).Item(43) = SpellDBC.records(index).manaCostPerlevel
        Form1.dt.Rows(0).Item(44) = SpellDBC.records(index).manaPerSecond
        Form1.dt.Rows(0).Item(45) = SpellDBC.records(index).manaPerSecondPerLevel
        Form1.dt.Rows(0).Item(46) = SpellDBC.records(index).rangeIndex
        Form1.dt.Rows(0).Item(47) = SpellDBC.records(index).speed
        Form1.dt.Rows(0).Item(48) = SpellDBC.records(index).modalNextSpell
        Form1.dt.Rows(0).Item(49) = SpellDBC.records(index).StackAmount
        Form1.dt.Rows(0).Item(50) = SpellDBC.records(index).Totem1
        Form1.dt.Rows(0).Item(51) = SpellDBC.records(index).Totem2
        Form1.dt.Rows(0).Item(52) = SpellDBC.records(index).Reagent1
        Form1.dt.Rows(0).Item(53) = SpellDBC.records(index).Reagent2
        Form1.dt.Rows(0).Item(54) = SpellDBC.records(index).Reagent3
        Form1.dt.Rows(0).Item(55) = SpellDBC.records(index).Reagent4
        Form1.dt.Rows(0).Item(56) = SpellDBC.records(index).Reagent5
        Form1.dt.Rows(0).Item(57) = SpellDBC.records(index).Reagent6
        Form1.dt.Rows(0).Item(58) = SpellDBC.records(index).Reagent7
        Form1.dt.Rows(0).Item(59) = SpellDBC.records(index).Reagent8
        Form1.dt.Rows(0).Item(60) = SpellDBC.records(index).ReagentCount1
        Form1.dt.Rows(0).Item(61) = SpellDBC.records(index).ReagentCount2
        Form1.dt.Rows(0).Item(62) = SpellDBC.records(index).ReagentCount3
        Form1.dt.Rows(0).Item(63) = SpellDBC.records(index).ReagentCount4
        Form1.dt.Rows(0).Item(64) = SpellDBC.records(index).ReagentCount5
        Form1.dt.Rows(0).Item(65) = SpellDBC.records(index).ReagentCount6
        Form1.dt.Rows(0).Item(66) = SpellDBC.records(index).ReagentCount7
        Form1.dt.Rows(0).Item(67) = SpellDBC.records(index).ReagentCount8
        Form1.dt.Rows(0).Item(68) = SpellDBC.records(index).EquippedItemClass
        Form1.dt.Rows(0).Item(69) = SpellDBC.records(index).EquippedItemSubClassMask
        Form1.dt.Rows(0).Item(70) = SpellDBC.records(index).EquippedItemInventoryTypeMask
        Form1.dt.Rows(0).Item(71) = SpellDBC.records(index).Effect1
        Form1.dt.Rows(0).Item(72) = SpellDBC.records(index).Effect2
        Form1.dt.Rows(0).Item(73) = SpellDBC.records(index).Effect3
        Form1.dt.Rows(0).Item(74) = SpellDBC.records(index).EffectDieSides1
        Form1.dt.Rows(0).Item(75) = SpellDBC.records(index).EffectDieSides2
        Form1.dt.Rows(0).Item(76) = SpellDBC.records(index).EffectDieSides3
        Form1.dt.Rows(0).Item(77) = SpellDBC.records(index).EffectRealPointsPerLevel1
        Form1.dt.Rows(0).Item(78) = SpellDBC.records(index).EffectRealPointsPerLevel2
        Form1.dt.Rows(0).Item(79) = SpellDBC.records(index).EffectRealPointsPerLevel3
        Form1.dt.Rows(0).Item(80) = SpellDBC.records(index).EffectBasePoints1
        Form1.dt.Rows(0).Item(81) = SpellDBC.records(index).EffectBasePoints2
        Form1.dt.Rows(0).Item(82) = SpellDBC.records(index).EffectBasePoints3
        Form1.dt.Rows(0).Item(83) = SpellDBC.records(index).EffectMechanic1
        Form1.dt.Rows(0).Item(84) = SpellDBC.records(index).EffectMechanic2
        Form1.dt.Rows(0).Item(85) = SpellDBC.records(index).EffectMechanic3
        Form1.dt.Rows(0).Item(86) = SpellDBC.records(index).EffectImplicitTargetA1
        Form1.dt.Rows(0).Item(87) = SpellDBC.records(index).EffectImplicitTargetA2
        Form1.dt.Rows(0).Item(88) = SpellDBC.records(index).EffectImplicitTargetA3
        Form1.dt.Rows(0).Item(89) = SpellDBC.records(index).EffectImplicitTargetB1
        Form1.dt.Rows(0).Item(90) = SpellDBC.records(index).EffectImplicitTargetB2
        Form1.dt.Rows(0).Item(91) = SpellDBC.records(index).EffectImplicitTargetB3
        Form1.dt.Rows(0).Item(92) = SpellDBC.records(index).EffectRadiusIndex1
        Form1.dt.Rows(0).Item(93) = SpellDBC.records(index).EffectRadiusIndex2
        Form1.dt.Rows(0).Item(94) = SpellDBC.records(index).EffectRadiusIndex3
        Form1.dt.Rows(0).Item(95) = SpellDBC.records(index).EffectApplyAuraName1
        Form1.dt.Rows(0).Item(96) = SpellDBC.records(index).EffectApplyAuraName2
        Form1.dt.Rows(0).Item(97) = SpellDBC.records(index).EffectApplyAuraName3
        Form1.dt.Rows(0).Item(98) = SpellDBC.records(index).EffectAmplitude1
        Form1.dt.Rows(0).Item(99) = SpellDBC.records(index).EffectAmplitude2
        Form1.dt.Rows(0).Item(100) = SpellDBC.records(index).EffectAmplitude3
        Form1.dt.Rows(0).Item(101) = SpellDBC.records(index).EffectMultipleValue1
        Form1.dt.Rows(0).Item(102) = SpellDBC.records(index).EffectMultipleValue2
        Form1.dt.Rows(0).Item(103) = SpellDBC.records(index).EffectMultipleValue3
        Form1.dt.Rows(0).Item(104) = SpellDBC.records(index).EffectChainTarget1
        Form1.dt.Rows(0).Item(105) = SpellDBC.records(index).EffectChainTarget2
        Form1.dt.Rows(0).Item(106) = SpellDBC.records(index).EffectChainTarget3
        Form1.dt.Rows(0).Item(107) = SpellDBC.records(index).EffectItemType1
        Form1.dt.Rows(0).Item(108) = SpellDBC.records(index).EffectItemType2
        Form1.dt.Rows(0).Item(109) = SpellDBC.records(index).EffectItemType3
        Form1.dt.Rows(0).Item(110) = SpellDBC.records(index).EffectMiscValue1
        Form1.dt.Rows(0).Item(111) = SpellDBC.records(index).EffectMiscValue2
        Form1.dt.Rows(0).Item(112) = SpellDBC.records(index).EffectMiscValue3
        Form1.dt.Rows(0).Item(113) = SpellDBC.records(index).EffectMiscValueB1
        Form1.dt.Rows(0).Item(114) = SpellDBC.records(index).EffectMiscValueB2
        Form1.dt.Rows(0).Item(115) = SpellDBC.records(index).EffectMiscValueB3
        Form1.dt.Rows(0).Item(116) = SpellDBC.records(index).EffectTriggerSpell1
        Form1.dt.Rows(0).Item(117) = SpellDBC.records(index).EffectTriggerSpell2
        Form1.dt.Rows(0).Item(118) = SpellDBC.records(index).EffectTriggerSpell3
        Form1.dt.Rows(0).Item(119) = SpellDBC.records(index).EffectPointsPerComboPoint1
        Form1.dt.Rows(0).Item(120) = SpellDBC.records(index).EffectPointsPerComboPoint2
        Form1.dt.Rows(0).Item(121) = SpellDBC.records(index).EffectPointsPerComboPoint3
        Form1.dt.Rows(0).Item(122) = SpellDBC.records(index).EffectSpellClassMaskA1
        Form1.dt.Rows(0).Item(123) = SpellDBC.records(index).EffectSpellClassMaskA2
        Form1.dt.Rows(0).Item(124) = SpellDBC.records(index).EffectSpellClassMaskA3
        Form1.dt.Rows(0).Item(125) = SpellDBC.records(index).EffectSpellClassMaskB1
        Form1.dt.Rows(0).Item(126) = SpellDBC.records(index).EffectSpellClassMaskB2
        Form1.dt.Rows(0).Item(127) = SpellDBC.records(index).EffectSpellClassMaskB3
        Form1.dt.Rows(0).Item(128) = SpellDBC.records(index).EffectSpellClassMaskC1
        Form1.dt.Rows(0).Item(129) = SpellDBC.records(index).EffectSpellClassMaskC2
        Form1.dt.Rows(0).Item(130) = SpellDBC.records(index).EffectSpellClassMaskC3
        Form1.dt.Rows(0).Item(131) = SpellDBC.records(index).SpellVisual1
        Form1.dt.Rows(0).Item(132) = SpellDBC.records(index).SpellVisual2
        Form1.dt.Rows(0).Item(133) = SpellDBC.records(index).SpellIconID
        Form1.dt.Rows(0).Item(134) = SpellDBC.records(index).activeIconID
        Form1.dt.Rows(0).Item(135) = SpellDBC.records(index).spellPriority
        Form1.dt.Rows(0).Item(136) = SpellDBC.records(index).SpellName_String
        Form1.dt.Rows(0).Item(137) = SpellDBC.records(index).SpellNameFlag
        Form1.dt.Rows(0).Item(138) = SpellDBC.records(index).Rank_String
        Form1.dt.Rows(0).Item(139) = SpellDBC.records(index).RankFlags
        Form1.dt.Rows(0).Item(140) = SpellDBC.records(index).Description_String
        Form1.dt.Rows(0).Item(141) = SpellDBC.records(index).DescriptionFlags
        Form1.dt.Rows(0).Item(142) = SpellDBC.records(index).ToolTip_String
        Form1.dt.Rows(0).Item(143) = SpellDBC.records(index).ToolTipFlags
        Form1.dt.Rows(0).Item(144) = SpellDBC.records(index).ManaCostPercentage
        Form1.dt.Rows(0).Item(145) = SpellDBC.records(index).StartRecoveryCategory
        Form1.dt.Rows(0).Item(146) = SpellDBC.records(index).StartRecoveryTime
        Form1.dt.Rows(0).Item(147) = SpellDBC.records(index).MaxTargetLevel
        Form1.dt.Rows(0).Item(148) = SpellDBC.records(index).SpellFamilyName
        Form1.dt.Rows(0).Item(149) = SpellDBC.records(index).SpellFamilyFlags
        Form1.dt.Rows(0).Item(150) = SpellDBC.records(index).SpellFamilyFlags2
        Form1.dt.Rows(0).Item(151) = SpellDBC.records(index).MaxAffectedTargets
        Form1.dt.Rows(0).Item(152) = SpellDBC.records(index).DmgClass
        Form1.dt.Rows(0).Item(153) = SpellDBC.records(index).PreventionType
        Form1.dt.Rows(0).Item(154) = SpellDBC.records(index).StanceBarOrder
        Form1.dt.Rows(0).Item(155) = SpellDBC.records(index).DmgMultiplier1
        Form1.dt.Rows(0).Item(156) = SpellDBC.records(index).DmgMultiplier2
        Form1.dt.Rows(0).Item(157) = SpellDBC.records(index).DmgMultiplier3
        Form1.dt.Rows(0).Item(158) = SpellDBC.records(index).MinFactionId
        Form1.dt.Rows(0).Item(159) = SpellDBC.records(index).MinReputation
        Form1.dt.Rows(0).Item(160) = SpellDBC.records(index).RequiredAuraVision
        Form1.dt.Rows(0).Item(161) = SpellDBC.records(index).TotemCategory1
        Form1.dt.Rows(0).Item(162) = SpellDBC.records(index).TotemCategory2
        Form1.dt.Rows(0).Item(163) = SpellDBC.records(index).AreaGroupId
        Form1.dt.Rows(0).Item(164) = SpellDBC.records(index).SchoolMask
        Form1.dt.Rows(0).Item(165) = SpellDBC.records(index).runeCostID
        Form1.dt.Rows(0).Item(166) = SpellDBC.records(index).spellMissileID
        Form1.dt.Rows(0).Item(167) = SpellDBC.records(index).PowerDisplayId
        Form1.dt.Rows(0).Item(168) = SpellDBC.records(index).EffectBonusMultiplier1
        Form1.dt.Rows(0).Item(169) = SpellDBC.records(index).EffectBonusMultiplier2
        Form1.dt.Rows(0).Item(170) = SpellDBC.records(index).EffectBonusMultiplier3
        Form1.dt.Rows(0).Item(171) = SpellDBC.records(index).spellDescriptionVariableID
        Form1.dt.Rows(0).Item(172) = SpellDBC.records(index).SpellDifficultyId
        ' LOCALES
        Form1.dt.Rows(0).Item(173) = SpellDBC.records(index).Local1N_String
        Form1.dt.Rows(0).Item(174) = SpellDBC.records(index).Local1R_String
        Form1.dt.Rows(0).Item(175) = SpellDBC.records(index).Local1D_String
        Form1.dt.Rows(0).Item(176) = SpellDBC.records(index).Local1T_String
        Form1.dt.Rows(0).Item(177) = SpellDBC.records(index).Local2N_String
        Form1.dt.Rows(0).Item(178) = SpellDBC.records(index).Local2R_String
        Form1.dt.Rows(0).Item(179) = SpellDBC.records(index).Local2D_String
        Form1.dt.Rows(0).Item(180) = SpellDBC.records(index).Local2T_String
        Form1.dt.Rows(0).Item(181) = SpellDBC.records(index).Local3N_String
        Form1.dt.Rows(0).Item(182) = SpellDBC.records(index).Local3R_String
        Form1.dt.Rows(0).Item(183) = SpellDBC.records(index).Local3D_String
        Form1.dt.Rows(0).Item(184) = SpellDBC.records(index).Local3T_String
        Form1.dt.Rows(0).Item(185) = SpellDBC.records(index).Local4N_String
        Form1.dt.Rows(0).Item(186) = SpellDBC.records(index).Local4R_String
        Form1.dt.Rows(0).Item(187) = SpellDBC.records(index).Local4D_String
        Form1.dt.Rows(0).Item(188) = SpellDBC.records(index).Local4T_String
        Form1.dt.Rows(0).Item(189) = SpellDBC.records(index).Local5N_String
        Form1.dt.Rows(0).Item(190) = SpellDBC.records(index).Local5R_String
        Form1.dt.Rows(0).Item(191) = SpellDBC.records(index).Local5D_String
        Form1.dt.Rows(0).Item(192) = SpellDBC.records(index).Local5T_String
        Form1.dt.Rows(0).Item(193) = SpellDBC.records(index).Local6N_String
        Form1.dt.Rows(0).Item(194) = SpellDBC.records(index).Local6R_String
        Form1.dt.Rows(0).Item(195) = SpellDBC.records(index).Local6D_String
        Form1.dt.Rows(0).Item(196) = SpellDBC.records(index).Local6T_String
        Form1.dt.Rows(0).Item(197) = SpellDBC.records(index).Local7N_String
        Form1.dt.Rows(0).Item(198) = SpellDBC.records(index).Local7R_String
        Form1.dt.Rows(0).Item(199) = SpellDBC.records(index).Local7D_String
        Form1.dt.Rows(0).Item(200) = SpellDBC.records(index).Local7T_String
        Form1.dt.Rows(0).Item(201) = SpellDBC.records(index).Local8N_String
        Form1.dt.Rows(0).Item(202) = SpellDBC.records(index).Local8R_String
        Form1.dt.Rows(0).Item(203) = SpellDBC.records(index).Local8D_String
        Form1.dt.Rows(0).Item(204) = SpellDBC.records(index).Local8T_String
    End Sub

    Public Sub SaveCurrentSpell(ByVal index As Integer)
        For i = 0 To SpellDBC.records.Count - 1
            If SpellDBC.records(i).Id = index Then
                index = i
                Exit For
            End If
        Next i
        SpellDBC.records(index).Id = Form1.dt.Rows(0).Item(0)
        SpellDBC.records(index).Category = Form1.dt.Rows(0).Item(1)
        SpellDBC.records(index).Dispel = Form1.dt.Rows(0).Item(2)
        SpellDBC.records(index).Mechanic = Form1.dt.Rows(0).Item(3)
        SpellDBC.records(index).Attributes = Form1.dt.Rows(0).Item(4)
        SpellDBC.records(index).AttributesEx = Form1.dt.Rows(0).Item(5)
        SpellDBC.records(index).AttributesEx2 = Form1.dt.Rows(0).Item(6)
        SpellDBC.records(index).AttributesEx3 = Form1.dt.Rows(0).Item(7)
        SpellDBC.records(index).AttributesEx4 = Form1.dt.Rows(0).Item(8)
        SpellDBC.records(index).AttributesEx5 = Form1.dt.Rows(0).Item(9)
        SpellDBC.records(index).AttributesEx6 = Form1.dt.Rows(0).Item(10)
        SpellDBC.records(index).AttributesEx7 = Form1.dt.Rows(0).Item(11)
        SpellDBC.records(index).Stances = Form1.dt.Rows(0).Item(12)
        SpellDBC.records(index).unk_320_2 = Form1.dt.Rows(0).Item(13)
        SpellDBC.records(index).StancesNot = Form1.dt.Rows(0).Item(14)
        SpellDBC.records(index).unk_320_3 = Form1.dt.Rows(0).Item(15)
        SpellDBC.records(index).Targets = Form1.dt.Rows(0).Item(16)
        SpellDBC.records(index).TargetCreatureType = Form1.dt.Rows(0).Item(17)
        SpellDBC.records(index).RequiresSpellFocus = Form1.dt.Rows(0).Item(18)
        SpellDBC.records(index).FacingCasterFlags = Form1.dt.Rows(0).Item(19)
        SpellDBC.records(index).CasterAuraState = Form1.dt.Rows(0).Item(20)
        SpellDBC.records(index).TargetAuraState = Form1.dt.Rows(0).Item(21)
        SpellDBC.records(index).CasterAuraStateNot = Form1.dt.Rows(0).Item(22)
        SpellDBC.records(index).TargetAuraStateNot = Form1.dt.Rows(0).Item(23)
        SpellDBC.records(index).casterAuraSpell = Form1.dt.Rows(0).Item(24)
        SpellDBC.records(index).targetAuraSpell = Form1.dt.Rows(0).Item(25)
        SpellDBC.records(index).excludeCasterAuraSpell = Form1.dt.Rows(0).Item(26)
        SpellDBC.records(index).excludeTargetAuraSpell = Form1.dt.Rows(0).Item(27)
        SpellDBC.records(index).CastingTimeIndex = Form1.dt.Rows(0).Item(28)
        SpellDBC.records(index).RecoveryTime = Form1.dt.Rows(0).Item(29)
        SpellDBC.records(index).CategoryRecoveryTime = Form1.dt.Rows(0).Item(30)
        SpellDBC.records(index).InterruptFlags = Form1.dt.Rows(0).Item(31)
        SpellDBC.records(index).AuraInterruptFlags = Form1.dt.Rows(0).Item(32)
        SpellDBC.records(index).ChannelInterruptFlags = Form1.dt.Rows(0).Item(33)
        SpellDBC.records(index).procFlags = Form1.dt.Rows(0).Item(34)
        SpellDBC.records(index).procChance = Form1.dt.Rows(0).Item(35)
        SpellDBC.records(index).procCharges = Form1.dt.Rows(0).Item(36)
        SpellDBC.records(index).maxLevel = Form1.dt.Rows(0).Item(37)
        SpellDBC.records(index).baseLevel = Form1.dt.Rows(0).Item(38)
        SpellDBC.records(index).spellLevel = Form1.dt.Rows(0).Item(39)
        SpellDBC.records(index).DurationIndex = Form1.dt.Rows(0).Item(40)
        SpellDBC.records(index).powerType = Form1.dt.Rows(0).Item(41)
        SpellDBC.records(index).manaCost = Form1.dt.Rows(0).Item(42)
        SpellDBC.records(index).manaCostPerlevel = Form1.dt.Rows(0).Item(43)
        SpellDBC.records(index).manaPerSecond = Form1.dt.Rows(0).Item(44)
        SpellDBC.records(index).manaPerSecondPerLevel = Form1.dt.Rows(0).Item(45)
        SpellDBC.records(index).rangeIndex = Form1.dt.Rows(0).Item(46)
        SpellDBC.records(index).speed = Form1.dt.Rows(0).Item(47)
        SpellDBC.records(index).modalNextSpell = Form1.dt.Rows(0).Item(48)
        SpellDBC.records(index).StackAmount = Form1.dt.Rows(0).Item(49)
        SpellDBC.records(index).Totem1 = Form1.dt.Rows(0).Item(50)
        SpellDBC.records(index).Totem2 = Form1.dt.Rows(0).Item(51)
        SpellDBC.records(index).Reagent1 = Form1.dt.Rows(0).Item(52)
        SpellDBC.records(index).Reagent2 = Form1.dt.Rows(0).Item(53)
        SpellDBC.records(index).Reagent3 = Form1.dt.Rows(0).Item(54)
        SpellDBC.records(index).Reagent4 = Form1.dt.Rows(0).Item(55)
        SpellDBC.records(index).Reagent5 = Form1.dt.Rows(0).Item(56)
        SpellDBC.records(index).Reagent6 = Form1.dt.Rows(0).Item(57)
        SpellDBC.records(index).Reagent7 = Form1.dt.Rows(0).Item(58)
        SpellDBC.records(index).Reagent8 = Form1.dt.Rows(0).Item(59)
        SpellDBC.records(index).ReagentCount1 = Form1.dt.Rows(0).Item(60)
        SpellDBC.records(index).ReagentCount2 = Form1.dt.Rows(0).Item(61)
        SpellDBC.records(index).ReagentCount3 = Form1.dt.Rows(0).Item(62)
        SpellDBC.records(index).ReagentCount4 = Form1.dt.Rows(0).Item(63)
        SpellDBC.records(index).ReagentCount5 = Form1.dt.Rows(0).Item(64)
        SpellDBC.records(index).ReagentCount6 = Form1.dt.Rows(0).Item(65)
        SpellDBC.records(index).ReagentCount7 = Form1.dt.Rows(0).Item(66)
        SpellDBC.records(index).ReagentCount8 = Form1.dt.Rows(0).Item(67)
        SpellDBC.records(index).EquippedItemClass = Form1.dt.Rows(0).Item(68)
        SpellDBC.records(index).EquippedItemSubClassMask = Form1.dt.Rows(0).Item(69)
        SpellDBC.records(index).EquippedItemInventoryTypeMask = Form1.dt.Rows(0).Item(70)
        SpellDBC.records(index).Effect1 = Form1.dt.Rows(0).Item(71)
        SpellDBC.records(index).Effect2 = Form1.dt.Rows(0).Item(72)
        SpellDBC.records(index).Effect3 = Form1.dt.Rows(0).Item(73)
        SpellDBC.records(index).EffectDieSides1 = Form1.dt.Rows(0).Item(74)
        SpellDBC.records(index).EffectDieSides2 = Form1.dt.Rows(0).Item(75)
        SpellDBC.records(index).EffectDieSides3 = Form1.dt.Rows(0).Item(76)
        SpellDBC.records(index).EffectRealPointsPerLevel1 = Form1.dt.Rows(0).Item(77)
        SpellDBC.records(index).EffectRealPointsPerLevel2 = Form1.dt.Rows(0).Item(78)
        SpellDBC.records(index).EffectRealPointsPerLevel3 = Form1.dt.Rows(0).Item(79)
        SpellDBC.records(index).EffectBasePoints1 = Form1.dt.Rows(0).Item(80)
        SpellDBC.records(index).EffectBasePoints2 = Form1.dt.Rows(0).Item(81)
        SpellDBC.records(index).EffectBasePoints3 = Form1.dt.Rows(0).Item(82)
        SpellDBC.records(index).EffectMechanic1 = Form1.dt.Rows(0).Item(83)
        SpellDBC.records(index).EffectMechanic2 = Form1.dt.Rows(0).Item(84)
        SpellDBC.records(index).EffectMechanic3 = Form1.dt.Rows(0).Item(85)
        SpellDBC.records(index).EffectImplicitTargetA1 = Form1.dt.Rows(0).Item(86)
        SpellDBC.records(index).EffectImplicitTargetA2 = Form1.dt.Rows(0).Item(87)
        SpellDBC.records(index).EffectImplicitTargetA3 = Form1.dt.Rows(0).Item(88)
        SpellDBC.records(index).EffectImplicitTargetB1 = Form1.dt.Rows(0).Item(89)
        SpellDBC.records(index).EffectImplicitTargetB2 = Form1.dt.Rows(0).Item(90)
        SpellDBC.records(index).EffectImplicitTargetB3 = Form1.dt.Rows(0).Item(91)
        SpellDBC.records(index).EffectRadiusIndex1 = Form1.dt.Rows(0).Item(92)
        SpellDBC.records(index).EffectRadiusIndex2 = Form1.dt.Rows(0).Item(93)
        SpellDBC.records(index).EffectRadiusIndex3 = Form1.dt.Rows(0).Item(94)
        SpellDBC.records(index).EffectApplyAuraName1 = Form1.dt.Rows(0).Item(95)
        SpellDBC.records(index).EffectApplyAuraName2 = Form1.dt.Rows(0).Item(96)
        SpellDBC.records(index).EffectApplyAuraName3 = Form1.dt.Rows(0).Item(97)
        SpellDBC.records(index).EffectAmplitude1 = Form1.dt.Rows(0).Item(98)
        SpellDBC.records(index).EffectAmplitude2 = Form1.dt.Rows(0).Item(99)
        SpellDBC.records(index).EffectAmplitude3 = Form1.dt.Rows(0).Item(100)
        SpellDBC.records(index).EffectMultipleValue1 = Form1.dt.Rows(0).Item(101)
        SpellDBC.records(index).EffectMultipleValue2 = Form1.dt.Rows(0).Item(102)
        SpellDBC.records(index).EffectMultipleValue3 = Form1.dt.Rows(0).Item(103)
        SpellDBC.records(index).EffectChainTarget1 = Form1.dt.Rows(0).Item(104)
        SpellDBC.records(index).EffectChainTarget2 = Form1.dt.Rows(0).Item(105)
        SpellDBC.records(index).EffectChainTarget3 = Form1.dt.Rows(0).Item(106)
        SpellDBC.records(index).EffectItemType1 = Form1.dt.Rows(0).Item(107)
        SpellDBC.records(index).EffectItemType2 = Form1.dt.Rows(0).Item(108)
        SpellDBC.records(index).EffectItemType3 = Form1.dt.Rows(0).Item(109)
        SpellDBC.records(index).EffectMiscValue1 = Form1.dt.Rows(0).Item(110)
        SpellDBC.records(index).EffectMiscValue2 = Form1.dt.Rows(0).Item(111)
        SpellDBC.records(index).EffectMiscValue3 = Form1.dt.Rows(0).Item(112)
        SpellDBC.records(index).EffectMiscValueB1 = Form1.dt.Rows(0).Item(113)
        SpellDBC.records(index).EffectMiscValueB2 = Form1.dt.Rows(0).Item(114)
        SpellDBC.records(index).EffectMiscValueB3 = Form1.dt.Rows(0).Item(115)
        SpellDBC.records(index).EffectTriggerSpell1 = Form1.dt.Rows(0).Item(116)
        SpellDBC.records(index).EffectTriggerSpell2 = Form1.dt.Rows(0).Item(117)
        SpellDBC.records(index).EffectTriggerSpell3 = Form1.dt.Rows(0).Item(118)
        SpellDBC.records(index).EffectPointsPerComboPoint1 = Form1.dt.Rows(0).Item(119)
        SpellDBC.records(index).EffectPointsPerComboPoint2 = Form1.dt.Rows(0).Item(120)
        SpellDBC.records(index).EffectPointsPerComboPoint3 = Form1.dt.Rows(0).Item(121)
        SpellDBC.records(index).EffectSpellClassMaskA1 = Form1.dt.Rows(0).Item(122)
        SpellDBC.records(index).EffectSpellClassMaskA2 = Form1.dt.Rows(0).Item(123)
        SpellDBC.records(index).EffectSpellClassMaskA3 = Form1.dt.Rows(0).Item(124)
        SpellDBC.records(index).EffectSpellClassMaskB1 = Form1.dt.Rows(0).Item(125)
        SpellDBC.records(index).EffectSpellClassMaskB2 = Form1.dt.Rows(0).Item(126)
        SpellDBC.records(index).EffectSpellClassMaskB3 = Form1.dt.Rows(0).Item(127)
        SpellDBC.records(index).EffectSpellClassMaskC1 = Form1.dt.Rows(0).Item(128)
        SpellDBC.records(index).EffectSpellClassMaskC2 = Form1.dt.Rows(0).Item(129)
        SpellDBC.records(index).EffectSpellClassMaskC3 = Form1.dt.Rows(0).Item(130)
        SpellDBC.records(index).SpellVisual1 = Form1.dt.Rows(0).Item(131)
        SpellDBC.records(index).SpellVisual2 = Form1.dt.Rows(0).Item(132)
        SpellDBC.records(index).SpellIconID = Form1.dt.Rows(0).Item(133)
        SpellDBC.records(index).activeIconID = Form1.dt.Rows(0).Item(134)
        SpellDBC.records(index).spellPriority = Form1.dt.Rows(0).Item(135)
        SpellDBC.records(index).SpellName_String = Form1.dt.Rows(0).Item(136) & Chr(0)
        SpellDBC.records(index).SpellNameFlag = Form1.dt.Rows(0).Item(137)
        SpellDBC.records(index).Rank_String = Form1.dt.Rows(0).Item(138) & Chr(0)
        SpellDBC.records(index).RankFlags = Form1.dt.Rows(0).Item(139)
        SpellDBC.records(index).Description_String = Form1.dt.Rows(0).Item(140) & Chr(0)
        SpellDBC.records(index).DescriptionFlags = Form1.dt.Rows(0).Item(141)
        SpellDBC.records(index).ToolTip_String = Form1.dt.Rows(0).Item(142) & Chr(0)
        SpellDBC.records(index).ToolTipFlags = Form1.dt.Rows(0).Item(143)
        SpellDBC.records(index).ManaCostPercentage = Form1.dt.Rows(0).Item(144)
        SpellDBC.records(index).StartRecoveryCategory = Form1.dt.Rows(0).Item(145)
        SpellDBC.records(index).StartRecoveryTime = Form1.dt.Rows(0).Item(146)
        SpellDBC.records(index).MaxTargetLevel = Form1.dt.Rows(0).Item(147)
        SpellDBC.records(index).SpellFamilyName = Form1.dt.Rows(0).Item(148)
        SpellDBC.records(index).SpellFamilyFlags = Form1.dt.Rows(0).Item(149)
        SpellDBC.records(index).SpellFamilyFlags2 = Form1.dt.Rows(0).Item(150)
        SpellDBC.records(index).MaxAffectedTargets = Form1.dt.Rows(0).Item(151)
        SpellDBC.records(index).DmgClass = Form1.dt.Rows(0).Item(152)
        SpellDBC.records(index).PreventionType = Form1.dt.Rows(0).Item(153)
        SpellDBC.records(index).StanceBarOrder = Form1.dt.Rows(0).Item(154)
        SpellDBC.records(index).DmgMultiplier1 = Form1.dt.Rows(0).Item(155)
        SpellDBC.records(index).DmgMultiplier2 = Form1.dt.Rows(0).Item(156)
        SpellDBC.records(index).DmgMultiplier3 = Form1.dt.Rows(0).Item(157)
        SpellDBC.records(index).MinFactionId = Form1.dt.Rows(0).Item(158)
        SpellDBC.records(index).MinReputation = Form1.dt.Rows(0).Item(159)
        SpellDBC.records(index).RequiredAuraVision = Form1.dt.Rows(0).Item(160)
        SpellDBC.records(index).TotemCategory1 = Form1.dt.Rows(0).Item(161)
        SpellDBC.records(index).TotemCategory2 = Form1.dt.Rows(0).Item(162)
        SpellDBC.records(index).AreaGroupId = Form1.dt.Rows(0).Item(163)
        SpellDBC.records(index).SchoolMask = Form1.dt.Rows(0).Item(164)
        SpellDBC.records(index).runeCostID = Form1.dt.Rows(0).Item(165)
        SpellDBC.records(index).spellMissileID = Form1.dt.Rows(0).Item(166)
        SpellDBC.records(index).PowerDisplayId = Form1.dt.Rows(0).Item(167)
        SpellDBC.records(index).EffectBonusMultiplier1 = Form1.dt.Rows(0).Item(168)
        SpellDBC.records(index).EffectBonusMultiplier2 = Form1.dt.Rows(0).Item(169)
        SpellDBC.records(index).EffectBonusMultiplier3 = Form1.dt.Rows(0).Item(170)
        SpellDBC.records(index).spellDescriptionVariableID = Form1.dt.Rows(0).Item(171)
        SpellDBC.records(index).SpellDifficultyId = Form1.dt.Rows(0).Item(172)
        ' LOCALES
        SpellDBC.records(index).Local1N_String = Form1.dt.Rows(0).Item(173) & Chr(0)
        SpellDBC.records(index).Local1R_String = Form1.dt.Rows(0).Item(174) & Chr(0)
        SpellDBC.records(index).Local1D_String = Form1.dt.Rows(0).Item(175) & Chr(0)
        SpellDBC.records(index).Local1T_String = Form1.dt.Rows(0).Item(176) & Chr(0)
        SpellDBC.records(index).Local2N_String = Form1.dt.Rows(0).Item(177) & Chr(0)
        SpellDBC.records(index).Local2R_String = Form1.dt.Rows(0).Item(178) & Chr(0)
        SpellDBC.records(index).Local2D_String = Form1.dt.Rows(0).Item(179) & Chr(0)
        SpellDBC.records(index).Local2T_String = Form1.dt.Rows(0).Item(180) & Chr(0)
        SpellDBC.records(index).Local3N_String = Form1.dt.Rows(0).Item(181) & Chr(0)
        SpellDBC.records(index).Local3R_String = Form1.dt.Rows(0).Item(182) & Chr(0)
        SpellDBC.records(index).Local3D_String = Form1.dt.Rows(0).Item(183) & Chr(0)
        SpellDBC.records(index).Local3T_String = Form1.dt.Rows(0).Item(184) & Chr(0)
        SpellDBC.records(index).Local4N_String = Form1.dt.Rows(0).Item(185) & Chr(0)
        SpellDBC.records(index).Local4R_String = Form1.dt.Rows(0).Item(186) & Chr(0)
        SpellDBC.records(index).Local4D_String = Form1.dt.Rows(0).Item(187) & Chr(0)
        SpellDBC.records(index).Local4T_String = Form1.dt.Rows(0).Item(188) & Chr(0)
        SpellDBC.records(index).Local5N_String = Form1.dt.Rows(0).Item(189) & Chr(0)
        SpellDBC.records(index).Local5R_String = Form1.dt.Rows(0).Item(190) & Chr(0)
        SpellDBC.records(index).Local5D_String = Form1.dt.Rows(0).Item(191) & Chr(0)
        SpellDBC.records(index).Local5T_String = Form1.dt.Rows(0).Item(192) & Chr(0)
        SpellDBC.records(index).Local6N_String = Form1.dt.Rows(0).Item(193) & Chr(0)
        SpellDBC.records(index).Local6R_String = Form1.dt.Rows(0).Item(194) & Chr(0)
        SpellDBC.records(index).Local6D_String = Form1.dt.Rows(0).Item(195) & Chr(0)
        SpellDBC.records(index).Local6T_String = Form1.dt.Rows(0).Item(196) & Chr(0)
        SpellDBC.records(index).Local7N_String = Form1.dt.Rows(0).Item(197) & Chr(0)
        SpellDBC.records(index).Local7R_String = Form1.dt.Rows(0).Item(198) & Chr(0)
        SpellDBC.records(index).Local7D_String = Form1.dt.Rows(0).Item(199) & Chr(0)
        SpellDBC.records(index).Local7T_String = Form1.dt.Rows(0).Item(200) & Chr(0)
        SpellDBC.records(index).Local8N_String = Form1.dt.Rows(0).Item(201) & Chr(0)
        SpellDBC.records(index).Local8R_String = Form1.dt.Rows(0).Item(202) & Chr(0)
        SpellDBC.records(index).Local8D_String = Form1.dt.Rows(0).Item(203) & Chr(0)
        SpellDBC.records(index).Local8T_String = Form1.dt.Rows(0).Item(204) & Chr(0)
    End Sub
End Class

Structure SpellRecord
    Dim Id As UInt32                                             ' 0        m_ID
    Dim Category As UInt32                                       ' 1        m_category
    Dim Dispel As UInt32                                        ' 2        m_dispelType
    Dim Mechanic As UInt32                                   ' 3        m_mechanic
    Dim Attributes As UInt32                                   ' 4        m_attribute
    Dim AttributesEx As UInt32                              ' 5        m_attributesEx
    Dim AttributesEx2 As UInt32                             ' 6        m_attributesExB
    Dim AttributesEx3 As UInt32                           ' 7        m_attributesExC
    Dim AttributesEx4 As UInt32                            ' 8        m_attributesExD
    Dim AttributesEx5 As UInt32                             ' 9        m_attributesExE
    Dim AttributesEx6 As UInt32                            ' 10       m_attributesExF
    Dim AttributesEx7 As UInt32                             ' 11       3.2.0 (0x20 - totems, 0x4 - paladin auras, etc...) --- Even more attributes needed, thanks MrLama for this
    Dim Stances As UInt32                               ' 12       m_shapeshiftMask
    Dim unk_320_2 As UInt32                             ' 13       3.2.0
    Dim StancesNot As UInt32                              ' 14       m_shapeshiftExclude
    Dim unk_320_3 As UInt32                              ' 15       3.2.0
    Dim Targets As UInt32                             ' 16       m_targets
    Dim TargetCreatureType As UInt32                         ' 17       m_targetCreatureType
    Dim RequiresSpellFocus As UInt32                       ' 18       m_requiresSpellFocus
    Dim FacingCasterFlags As UInt32                        ' 19       m_facingCasterFlags
    Dim CasterAuraState As UInt32                        ' 20       m_casterAuraState
    Dim TargetAuraState As UInt32                         ' 21       m_targetAuraState
    Dim CasterAuraStateNot As UInt32                      ' 22       m_excludeCasterAuraState
    Dim TargetAuraStateNot As UInt32                        ' 23       m_excludeTargetAuraState
    Dim casterAuraSpell As UInt32                         ' 24       m_casterAuraSpell
    Dim targetAuraSpell As UInt32                      ' 25       m_targetAuraSpell
    Dim excludeCasterAuraSpell As UInt32                      ' 26       m_excludeCasterAuraSpell
    Dim excludeTargetAuraSpell As UInt32                     ' 27       m_excludeTargetAuraSpell
    Dim CastingTimeIndex As UInt32                     ' 28       m_castingTimeIndex
    Dim RecoveryTime As UInt32                     ' 29       m_recoveryTime
    Dim CategoryRecoveryTime As UInt32                      ' 30       m_categoryRecoveryTime
    Dim InterruptFlags As UInt32                        ' 31       m_interruptFlags
    Dim AuraInterruptFlags As UInt32                          ' 32       m_auraInterruptFlags
    Dim ChannelInterruptFlags As UInt32                          ' 33       m_channelInterruptFlags
    Dim procFlags As UInt32                           ' 34       m_procTypeMask
    Dim procChance As UInt32                           ' 35       m_procChance
    Dim procCharges As UInt32                      ' 36       m_procCharges
    Dim maxLevel As UInt32                          ' 37       m_maxLevel
    Dim baseLevel As UInt32                          ' 38       m_baseLevel
    Dim spellLevel As UInt32                           ' 39       m_spellLevel
    Dim DurationIndex As UInt32                         ' 40       m_durationIndex
    Dim powerType As UInt32                           ' 41       m_powerType
    Dim manaCost As UInt32                           ' 42       m_manaCost
    Dim manaCostPerlevel As UInt32                        ' 43       m_manaCostPerLevel
    Dim manaPerSecond As UInt32                        ' 44       m_manaPerSecond
    Dim manaPerSecondPerLevel As UInt32                        ' 45       m_manaPerSecondPerLeve
    Dim rangeIndex As UInt32                         ' 46       m_rangeIndex
    Dim speed As Single                                        ' 47       m_speed
    Dim modalNextSpell As UInt32                              ' 48       m_modalNextSpell 
    Dim StackAmount As UInt32                              ' 49       m_cumulativeAura
    Dim Totem1 As UInt32
    Dim Totem2 As UInt32  ' 50-51    m_totem
    Dim Reagent1 As Int32
    Dim Reagent2 As Int32
    Dim Reagent3 As Int32
    Dim Reagent4 As Int32
    Dim Reagent5 As Int32
    Dim Reagent6 As Int32
    Dim Reagent7 As Int32
    Dim Reagent8 As Int32  ' 52-59    m_reagent
    Dim ReagentCount1 As UInt32
    Dim ReagentCount2 As UInt32
    Dim ReagentCount3 As UInt32
    Dim ReagentCount4 As UInt32
    Dim ReagentCount5 As UInt32
    Dim ReagentCount6 As UInt32
    Dim ReagentCount7 As UInt32
    Dim ReagentCount8 As UInt32 ' 60-67    m_reagentCount
    Dim EquippedItemClass As Int32                         ' 68       m_equippedItemClass (value)
    Dim EquippedItemSubClassMask As Int32                ' 69       m_equippedItemSubclass (mask)
    Dim EquippedItemInventoryTypeMask As Int32               ' 70       m_equippedItemInvTypes (mask)
    Dim Effect1 As UInt32
    Dim Effect2 As UInt32
    Dim Effect3 As UInt32 ' 71-73    m_effect
    Dim EffectDieSides1 As Int32
    Dim EffectDieSides2 As Int32
    Dim EffectDieSides3 As Int32 ' 74-76    m_effectDieSides
    Dim EffectRealPointsPerLevel1 As Single
    Dim EffectRealPointsPerLevel2 As Single
    Dim EffectRealPointsPerLevel3 As Single ' 77-79    m_effectRealPointsPerLevel
    Dim EffectBasePoints1 As Int32
    Dim EffectBasePoints2 As Int32
    Dim EffectBasePoints3 As Int32  ' 80-82    m_effectBasePoints (don't must be used in spell/auras explicitly, must be used cached Spell::m_currentBasePoints)
    Dim EffectMechanic1 As UInt32
    Dim EffectMechanic2 As UInt32
    Dim EffectMechanic3 As UInt32 ' 83-85    m_effectMechanic
    Dim EffectImplicitTargetA1 As UInt32
    Dim EffectImplicitTargetA2 As UInt32
    Dim EffectImplicitTargetA3 As UInt32  ' 86-88    m_implicitTargetA
    Dim EffectImplicitTargetB1 As UInt32
    Dim EffectImplicitTargetB2 As UInt32
    Dim EffectImplicitTargetB3 As UInt32  ' 89-91    m_implicitTargetB
    Dim EffectRadiusIndex1 As UInt32
    Dim EffectRadiusIndex2 As UInt32
    Dim EffectRadiusIndex3 As UInt32    ' 92-94    m_effectRadiusIndex - spellradius.dbc
    Dim EffectApplyAuraName1 As UInt32
    Dim EffectApplyAuraName2 As UInt32
    Dim EffectApplyAuraName3 As UInt32 ' 95-97    m_effectAura
    Dim EffectAmplitude1 As UInt32
    Dim EffectAmplitude2 As UInt32
    Dim EffectAmplitude3 As UInt32 ' 98-100   m_effectAuraPeriod
    Dim EffectMultipleValue1 As Single
    Dim EffectMultipleValue2 As Single
    Dim EffectMultipleValue3 As Single ' 101-103  m_effectAmplitude
    Dim EffectChainTarget1 As UInt32
    Dim EffectChainTarget2 As UInt32
    Dim EffectChainTarget3 As UInt32  ' 104-106  m_effectChainTargets
    Dim EffectItemType1 As UInt32
    Dim EffectItemType2 As UInt32
    Dim EffectItemType3 As UInt32 ' 107-109  m_effectItemType
    Dim EffectMiscValue1 As Int32
    Dim EffectMiscValue2 As Int32
    Dim EffectMiscValue3 As Int32 ' 110-112  m_effectMiscValue
    Dim EffectMiscValueB1 As Int32
    Dim EffectMiscValueB2 As Int32
    Dim EffectMiscValueB3 As Int32  ' 113-115  m_effectMiscValueB
    Dim EffectTriggerSpell1 As UInt32
    Dim EffectTriggerSpell2 As UInt32
    Dim EffectTriggerSpell3 As UInt32 ' 116-118  m_effectTriggerSpell
    Dim EffectPointsPerComboPoint1 As Single
    Dim EffectPointsPerComboPoint2 As Single
    Dim EffectPointsPerComboPoint3 As Single ' 119-121  m_effectPointsPerCombo
    Dim EffectSpellClassMaskA1 As UInt32
    Dim EffectSpellClassMaskA2 As UInt32
    Dim EffectSpellClassMaskA3 As UInt32 ' 122-124  m_effectSpellClassMaskA, effect 0
    Dim EffectSpellClassMaskB1 As UInt32
    Dim EffectSpellClassMaskB2 As UInt32
    Dim EffectSpellClassMaskB3 As UInt32  ' 125-127  m_effectSpellClassMaskB, effect 1
    Dim EffectSpellClassMaskC1 As UInt32
    Dim EffectSpellClassMaskC2 As UInt32
    Dim EffectSpellClassMaskC3 As UInt32 ' 128-130  m_effectSpellClassMaskC, effect 2
    Dim SpellVisual1 As UInt32
    Dim SpellVisual2 As UInt32 ' 131-132  m_spellVisualID
    Dim SpellIconID As UInt32                                   ' 133      m_spellIconID
    Dim activeIconID As UInt32                                  ' 134      m_activeIconID
    Dim spellPriority As UInt32                                 ' 135      m_spellPriority     
    Dim SpellName As UInt32 ' 136-151  m_name_lang
    Dim SpellName_String As String
    Dim SpellNameFlag As UInt32                                ' 152 
    Dim Rank As UInt32 ' 153-168  m_nameSubtext_lang
    Dim Rank_String As String
    Dim RankFlags As UInt32                                  ' 169 
    Dim Description As UInt32 ' 170-185  m_description_lang 
    Dim Description_String As String
    Dim DescriptionFlags As UInt32                              ' 186 
    Dim ToolTip As UInt32 ' 187-202  m_auraDescription_lang 
    Dim ToolTip_String As String
    Dim ToolTipFlags As UInt32                               ' 203 
    Dim ManaCostPercentage As UInt32                            ' 204      m_manaCostPct
    Dim StartRecoveryCategory As UInt32                        ' 205      m_startRecoveryCategory
    Dim StartRecoveryTime As UInt32                           ' 206      m_startRecoveryTime
    Dim MaxTargetLevel As UInt32                            ' 207      m_maxTargetLevel
    Dim SpellFamilyName As UInt32                           ' 208      m_spellClassSet
    Dim SpellFamilyFlags As UInt64                              ' 209-210  m_spellClassMask NOTE: size is 12 bytes!!!
    Dim SpellFamilyFlags2 As UInt32                        ' 211      addition to m_spellClassMask
    Dim MaxAffectedTargets As UInt32                        ' 212      m_maxTargets
    Dim DmgClass As UInt32                        ' 213      m_defenseType
    Dim PreventionType As UInt32                          ' 214      m_preventionType
    Dim StanceBarOrder As UInt32                         ' 215      m_stanceBarOrder 
    Dim DmgMultiplier1 As Single
    Dim DmgMultiplier2 As Single
    Dim DmgMultiplier3 As Single ' 216-218  m_effectChainAmplitude
    Dim MinFactionId As UInt32                              ' 219      m_minFactionID 
    Dim MinReputation As UInt32                              ' 220      m_minReputation 
    Dim RequiredAuraVision As UInt32                           ' 221      m_requiredAuraVision 
    Dim TotemCategory1 As UInt32
    Dim TotemCategory2 As UInt32 ' 222-223  m_requiredTotemCategoryID
    Dim AreaGroupId As Int32                             ' 224      m_requiredAreaGroupId
    Dim SchoolMask As UInt32                              ' 225      m_schoolMask
    Dim runeCostID As UInt32                              ' 226      m_runeCostID
    Dim spellMissileID As UInt32                             ' 227      m_spellMissileID 
    Dim PowerDisplayId As UInt32                           ' 228      PowerDisplay.dbc, new in 3.1
    Dim EffectBonusMultiplier1 As Single
    Dim EffectBonusMultiplier2 As Single
    Dim EffectBonusMultiplier3 As Single ' 229-231  3.2.0
    Dim spellDescriptionVariableID As UInt32                  ' 232      3.2.0
    Dim SpellDifficultyId As UInt32                    ' 233      3.3.0
    ' Localisation values
    Dim Local1N As UInt32
    Dim Local1N_String As String
    Dim Local1R As UInt32
    Dim Local1R_String As String
    Dim Local1D As UInt32
    Dim Local1D_String As String
    Dim Local1T As UInt32
    Dim Local1T_String As String
    Dim Local2N As UInt32
    Dim Local2N_String As String
    Dim Local2R As UInt32
    Dim Local2R_String As String
    Dim Local2D As UInt32
    Dim Local2D_String As String
    Dim Local2T As UInt32
    Dim Local2T_String As String
    Dim Local3N As UInt32
    Dim Local3N_String As String
    Dim Local3R As UInt32
    Dim Local3R_String As String
    Dim Local3D As UInt32
    Dim Local3D_String As String
    Dim Local3T As UInt32
    Dim Local3T_String As String
    Dim Local4N As UInt32
    Dim Local4N_String As String
    Dim Local4R As UInt32
    Dim Local4R_String As String
    Dim Local4D As UInt32
    Dim Local4D_String As String
    Dim Local4T As UInt32
    Dim Local4T_String As String
    Dim Local5N As UInt32
    Dim Local5N_String As String
    Dim Local5R As UInt32
    Dim Local5R_String As String
    Dim Local5D As UInt32
    Dim Local5D_String As String
    Dim Local5T As UInt32
    Dim Local5T_String As String
    Dim Local6N As UInt32
    Dim Local6N_String As String
    Dim Local6R As UInt32
    Dim Local6R_String As String
    Dim Local6D As UInt32
    Dim Local6D_String As String
    Dim Local6T As UInt32
    Dim Local6T_String As String
    Dim Local7N As UInt32
    Dim Local7N_String As String
    Dim Local7R As UInt32
    Dim Local7R_String As String
    Dim Local7D As UInt32
    Dim Local7D_String As String
    Dim Local7T As UInt32
    Dim Local7T_String As String
    Dim Local8N As UInt32
    Dim Local8N_String As String
    Dim Local8R As UInt32
    Dim Local8R_String As String
    Dim Local8D As UInt32
    Dim Local8D_String As String
    Dim Local8T As UInt32
    Dim Local8T_String As String
End Structure
