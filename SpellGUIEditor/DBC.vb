Imports System.IO
Imports System.Runtime.InteropServices

Structure DBC_Header
    Dim magic As UInt32 ' always 'WDBC'
    Dim record_count As UInt32 ' records per file
    Dim field_count As UInt32 ' fields per record
    Dim record_size As UInt32 ' sum (sizeof (field_type_i)) | 0 <= i < field_count. field_type_i is NOT defined in the files.
    Dim string_block_size As UInt32
End Structure

Structure DBC_Body
    Dim Header As DBC_Header
    Dim records() As SpellRecord
    Dim string_block() As Char
End Structure

Public Class DBC
    Private fsSource As FileStream
    Dim bytes() As Byte
    Dim numBytesToRead As UInt32
    Dim numBytesRead As UInt32

    Private SpellDBC As DBC_Body

    Private sizeofUint32 As UInt32
    Private sizeofUint64 As UInt32

    Public Sub OpenDBC(ByVal path As String)
        fsSource = New FileStream(path, FileMode.Open, FileAccess.Read)
        bytes = New Byte((fsSource.Length) - 1) {}
        numBytesToRead = CType(fsSource.Length, Integer)
        numBytesRead = 0
    End Sub

    Public Sub CloseDBC()
        fsSource.Close()
    End Sub

    Public Function GetRecordCount() As UInt32
        Return SpellDBC.Header.record_count
    End Function

    Public Sub ReadHeader()
        Dim Header As New DBC_Header

        Dim n As UInt32 = fsSource.Read(bytes, numBytesRead, Marshal.SizeOf(Header))
        numBytesToRead = (numBytesToRead - n)

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

        Dim i As UInt32
        For i = 0 To SpellDBC.Header.record_count - 1

            Dim Spell As New SpellRecord

            Dim n As UInt32 = fsSource.Read(bytes, numBytesRead, Marshal.SizeOf(Spell))
            numBytesToRead = (numBytesToRead - n)

            ReadRecord(Spell, i)

            SpellDBC.records(i) = Spell

            If i Mod 100 = 0 Then
                Form1.prog.bar.PerformStep()
                Application.DoEvents()
            End If
        Next i
        i = 0
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
            Spell.SpellName = BitConverter.ToString(bytes, numBytesRead, 16)
            numBytesRead = numBytesRead + 16
            Spell.SpellNameFlag = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Rank = BitConverter.ToString(bytes, numBytesRead, 16)
            numBytesRead = numBytesRead + 16
            Spell.RankFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.Description = BitConverter.ToString(bytes, numBytesRead, 16)
            numBytesRead = numBytesRead + 16
            Spell.DescriptionFlags = BitConverter.ToUInt32(bytes, numBytesRead)
            numBytesRead = numBytesRead + sizeofUint32
            Spell.ToolTip = BitConverter.ToString(bytes, numBytesRead, 16)
            numBytesRead = numBytesRead + 16
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
            Spell.AreaGroupId = BitConverter.ToUInt32(bytes, numBytesRead)
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
        Catch ex As Exception
            Dim f As New StreamWriter("log.txt", True)
            Dim out As String = "Record ID Failed To Read: " & i.ToString() & ": " & ex.Message.ToString() & vbNewLine
            f.Write(out)
            f.Close()
        End Try
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
    <VBFixedString(16)> Dim SpellName As String                                  ' 136-151  m_name_lang
    Dim SpellNameFlag As UInt32                                ' 152 
    <VBFixedString(16)> Dim Rank As String                                       ' 153-168  m_nameSubtext_lang
    Dim RankFlags As UInt32                                  ' 169 
    <VBFixedString(16)> Dim Description As String                            ' 170-185  m_description_lang 
    Dim DescriptionFlags As UInt32                              ' 186 
    <VBFixedString(16)> Dim ToolTip As String                               ' 187-202  m_auraDescription_lang 
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
End Structure