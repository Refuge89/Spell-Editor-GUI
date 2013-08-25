<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SName = New System.Windows.Forms.TextBox()
        Me.Description = New System.Windows.Forms.RichTextBox()
        Me.BuffDescription = New System.Windows.Forms.RichTextBox()
        Me.Rank = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.iconid = New System.Windows.Forms.Label()
        Me.Effect1 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Effect1Min = New System.Windows.Forms.TextBox()
        Me.Effect1Max = New System.Windows.Forms.TextBox()
        Me.Effect2Max = New System.Windows.Forms.TextBox()
        Me.Effect2Min = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Effect2 = New System.Windows.Forms.ComboBox()
        Me.Effect3Max = New System.Windows.Forms.TextBox()
        Me.Effect3Min = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Effect3 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.spellevel = New System.Windows.Forms.TextBox()
        Me.baselevel = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.maxlevel = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 16
        Me.ListBox1.Location = New System.Drawing.Point(12, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(73, 468)
        Me.ListBox1.TabIndex = 0
        '
        'SName
        '
        Me.SName.Location = New System.Drawing.Point(91, 35)
        Me.SName.Name = "SName"
        Me.SName.Size = New System.Drawing.Size(233, 22)
        Me.SName.TabIndex = 1
        '
        'Description
        '
        Me.Description.Location = New System.Drawing.Point(91, 80)
        Me.Description.Name = "Description"
        Me.Description.Size = New System.Drawing.Size(233, 101)
        Me.Description.TabIndex = 2
        Me.Description.Text = ""
        '
        'BuffDescription
        '
        Me.BuffDescription.Location = New System.Drawing.Point(91, 204)
        Me.BuffDescription.Name = "BuffDescription"
        Me.BuffDescription.Size = New System.Drawing.Size(233, 57)
        Me.BuffDescription.TabIndex = 3
        Me.BuffDescription.Text = ""
        '
        'Rank
        '
        Me.Rank.Location = New System.Drawing.Point(91, 284)
        Me.Rank.Name = "Rank"
        Me.Rank.Size = New System.Drawing.Size(233, 22)
        Me.Rank.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(91, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(91, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Description"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(91, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Buff Description"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(91, 264)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Rank"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(813, 450)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 30)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Save Spell"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(813, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 27)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "Delete Spell"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(330, 35)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(104, 42)
        Me.Button3.TabIndex = 11
        Me.Button3.Text = "Icon Editor"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'iconid
        '
        Me.iconid.AutoSize = True
        Me.iconid.Location = New System.Drawing.Point(331, 80)
        Me.iconid.Name = "iconid"
        Me.iconid.Size = New System.Drawing.Size(103, 17)
        Me.iconid.TabIndex = 12
        Me.iconid.Text = "Icon ID = NULL"
        '
        'Effect1
        '
        Me.Effect1.FormattingEnabled = True
        Me.Effect1.Items.AddRange(New Object() {"0 NONE", "1 INSTAKILL", "2 SCHOOL_DAMAGE", "3 DUMMY", "4 PORTAL_TELEPORT unused", "5 TELEPORT_UNITS", "6 APPLY_AURA", "7 ENVIRONMENTAL_DAMAGE", "8 POWER_DRAIN", "9 HEALTH_LEECH", "10 HEAL", "11 BIND", "12 PORTAL", "13 RITUAL_BASE unused", "14 RITUAL_SPECIALIZE unused", "15 RITUAL_ACTIVATE_PORTAL unused", "16 QUEST_COMPLETE", "17 WEAPON_DAMAGE_NOSCHOOL", "18 RESURRECT", "19 ADD_EXTRA_ATTACKS", "20 DODGE one spell: Dodge", "21 EVADE one spell: Evade (DND)", "22 PARRY", "23 BLOCK one spell: Block", "24 CREATE_ITEM", "25 WEAPON", "26 DEFENSE one spell: Defense", "27 PERSISTENT_AREA_AURA", "28 SUMMON", "29 LEAP", "30 ENERGIZE", "31 WEAPON_PERCENT_DAMAGE", "32 TRIGGER_MISSILE", "33 OPEN_LOCK", "34 SUMMON_CHANGE_ITEM", "35 APPLY_AREA_AURA_PARTY", "36 LEARN_SPELL", "37 SPELL_DEFENSE one spell: SPELLDEFENSE (DND)", "38 DISPEL", "39 LANGUAGE", "40 DUAL_WIELD", "41 JUMP", "42 JUMP_DEST", "43 TELEPORT_UNITS_FACE_CASTER", "44 SKILL_STEP", "45 ADD_HONOR honor/pvp related", "46 SPAWN clientside, unit appears as if it was just spawned", "47 TRADE_SKILL", "48 STEALTH one spell: Base Stealth", "49 DETECT one spell: Detect", "50 TRANS_DOOR", "51 FORCE_CRITICAL_HIT unused", "52 GUARANTEE_HIT one spell: zzOLDCritical Shot", "53 ENCHANT_ITEM", "54 ENCHANT_ITEM_TEMPORARY", "55 TAMECREATURE", "56 SUMMON_PET", "57 LEARN_PET_SPELL", "58 WEAPON_DAMAGE", "59 CREATE_RANDOM_ITEM create item base at spell specific loot", "60 PROFICIENCY", "61 SEND_EVENT", "62 POWER_BURN", "63 THREAT", "64 TRIGGER_SPELL", "65 APPLY_AREA_AURA_RAID", "66 CREATE_MANA_GEM (possibly recharge it, misc - is item ID)", "67 HEAL_MAX_HEALTH", "68 INTERRUPT_CAST", "69 DISTRACT", "70 PULL one spell: Distract Move", "71 PICKPOCKET", "72 ADD_FARSIGHT", "73 UNTRAIN_TALENTS", "74 APPLY_GLYPH", "75 HEAL_MECHANICAL one spell: Mechanical Patch Kit", "76 SUMMON_OBJECT_WILD", "77 SCRIPT_EFFECT", "78 ATTACK", "79 SANCTUARY", "80 ADD_COMBO_POINTS", "81 CREATE_HOUSE one spell: Create House (TEST)", "82 BIND_SIGHT", "83 DUEL", "84 STUCK", "85 SUMMON_PLAYER", "86 ACTIVATE_OBJECT", "87 GAMEOBJECT_DAMAGE", "88 GAMEOBJECT_REPAIR", "89 GAMEOBJECT_SET_DESTRUCTION_STATE", "90 KILL_CREDIT Kill credit but only for single person", "91 THREAT_ALL one spell: zzOLDBrainwash", "92 ENCHANT_HELD_ITEM", "93 FORCE_DESELECT", "94 SELF_RESURRECT", "95 SKINNING", "96 CHARGE", "97 CAST_BUTTON (totem bar since 3.2.2a)", "98 KNOCK_BACK", "99 DISENCHANT", "100 INEBRIATE", "101 FEED_PET", "102 DISMISS_PET", "103 REPUTATION", "104 SUMMON_OBJECT_SLOT1", "105 SUMMON_OBJECT_SLOT2", "106 SUMMON_OBJECT_SLOT3", "107 SUMMON_OBJECT_SLOT4", "108 DISPEL_MECHANIC", "109 SUMMON_DEAD_PET", "110 DESTROY_ALL_TOTEMS", "111 DURABILITY_DAMAGE", "112 112", "113 RESURRECT_NEW", "114 ATTACK_ME", "115 DURABILITY_DAMAGE_PCT", "116 SKIN_PLAYER_CORPSE one spell: Remove Insignia, bg usage, required special cor" & _
                "pse flags...", "117 SPIRIT_HEAL one spell: Spirit Heal", "118 SKILL professions and more", "119 APPLY_AREA_AURA_PET", "120 TELEPORT_GRAVEYARD one spell: Graveyard Teleport Test", "121 NORMALIZED_WEAPON_DMG", "122 122 unused", "123 SEND_TAXI taxi/flight related (misc value is taxi path id)", "124 PULL_TOWARDS", "125 MODIFY_THREAT_PERCENT", "126 STEAL_BENEFICIAL_BUFF spell steal effect?", "127 PROSPECTING Prospecting spell", "128 APPLY_AREA_AURA_FRIEND", "129 APPLY_AREA_AURA_ENEMY", "130 REDIRECT_THREAT", "131 PLAYER_NOTIFICATION sound id in misc value (SoundEntries.dbc)", "132 PLAY_MUSIC sound id in misc value (SoundEntries.dbc)", "133 UNLEARN_SPECIALIZATION unlearn profession specialization", "134 KILL_CREDIT misc value is creature entry", "135 CALL_PET", "136 HEAL_PCT", "137 ENERGIZE_PCT", "138 LEAP_BACK Leap back", "139 CLEAR_QUEST Reset quest status (miscValue - quest ID)", "140 FORCE_CAST", "141 FORCE_CAST_WITH_VALUE", "142 TRIGGER_SPELL_WITH_VALUE", "143 APPLY_AREA_AURA_OWNER", "144 KNOCK_BACK_DEST", "145 PULL_TOWARDS_DEST Black Hole Effect", "146 ACTIVATE_RUNE", "147 QUEST_FAIL quest fail", "148 TRIGGER_MISSILE_SPELL_WITH_VALUE", "149 CHARGE_DEST", "150 QUEST_START", "151 TRIGGER_SPELL_2", "152 SUMMON_RAF_FRIEND summon Refer-a-Friend", "153 CREATE_TAMED_PET misc value is creature entry", "154 DISCOVER_TAXI", "155 TITAN_GRIP Allows you to equip two-handed axes, maces and swords in one hand," & _
                " but you attack $49152s1% slower than normal.", "156 ENCHANT_ITEM_PRISMATIC", "157 CREATE_ITEM_2 create item or create item template and replace by some randon " & _
                "spell loot item", "158 MILLING milling", "159 ALLOW_RENAME_PET allow rename pet once again", "160 160 1 spell - 45534", "161 TALENT_SPEC_COUNT second talent spec (learn/revert)", "162 TALENT_SPEC_SELECT activate primary/secondary spec", "163 unused", "164 REMOVE_AURA"})
        Me.Effect1.Location = New System.Drawing.Point(502, 30)
        Me.Effect1.Name = "Effect1"
        Me.Effect1.Size = New System.Drawing.Size(288, 24)
        Me.Effect1.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(440, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 17)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Effect 1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(440, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 17)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Effect Base Value"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(441, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 17)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Effect Mod Value"
        '
        'Effect1Min
        '
        Me.Effect1Min.Location = New System.Drawing.Point(557, 61)
        Me.Effect1Min.Name = "Effect1Min"
        Me.Effect1Min.Size = New System.Drawing.Size(233, 22)
        Me.Effect1Min.TabIndex = 17
        '
        'Effect1Max
        '
        Me.Effect1Max.Location = New System.Drawing.Point(557, 89)
        Me.Effect1Max.Name = "Effect1Max"
        Me.Effect1Max.Size = New System.Drawing.Size(233, 22)
        Me.Effect1Max.TabIndex = 18
        '
        'Effect2Max
        '
        Me.Effect2Max.Location = New System.Drawing.Point(557, 176)
        Me.Effect2Max.Name = "Effect2Max"
        Me.Effect2Max.Size = New System.Drawing.Size(233, 22)
        Me.Effect2Max.TabIndex = 24
        '
        'Effect2Min
        '
        Me.Effect2Min.Location = New System.Drawing.Point(557, 148)
        Me.Effect2Min.Name = "Effect2Min"
        Me.Effect2Min.Size = New System.Drawing.Size(233, 22)
        Me.Effect2Min.TabIndex = 23
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(440, 122)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 17)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Effect 2"
        '
        'Effect2
        '
        Me.Effect2.FormattingEnabled = True
        Me.Effect2.Items.AddRange(New Object() {"0 NONE", "1 INSTAKILL", "2 SCHOOL_DAMAGE", "3 DUMMY", "4 PORTAL_TELEPORT unused", "5 TELEPORT_UNITS", "6 APPLY_AURA", "7 ENVIRONMENTAL_DAMAGE", "8 POWER_DRAIN", "9 HEALTH_LEECH", "10 HEAL", "11 BIND", "12 PORTAL", "13 RITUAL_BASE unused", "14 RITUAL_SPECIALIZE unused", "15 RITUAL_ACTIVATE_PORTAL unused", "16 QUEST_COMPLETE", "17 WEAPON_DAMAGE_NOSCHOOL", "18 RESURRECT", "19 ADD_EXTRA_ATTACKS", "20 DODGE one spell: Dodge", "21 EVADE one spell: Evade (DND)", "22 PARRY", "23 BLOCK one spell: Block", "24 CREATE_ITEM", "25 WEAPON", "26 DEFENSE one spell: Defense", "27 PERSISTENT_AREA_AURA", "28 SUMMON", "29 LEAP", "30 ENERGIZE", "31 WEAPON_PERCENT_DAMAGE", "32 TRIGGER_MISSILE", "33 OPEN_LOCK", "34 SUMMON_CHANGE_ITEM", "35 APPLY_AREA_AURA_PARTY", "36 LEARN_SPELL", "37 SPELL_DEFENSE one spell: SPELLDEFENSE (DND)", "38 DISPEL", "39 LANGUAGE", "40 DUAL_WIELD", "41 JUMP", "42 JUMP_DEST", "43 TELEPORT_UNITS_FACE_CASTER", "44 SKILL_STEP", "45 ADD_HONOR honor/pvp related", "46 SPAWN clientside, unit appears as if it was just spawned", "47 TRADE_SKILL", "48 STEALTH one spell: Base Stealth", "49 DETECT one spell: Detect", "50 TRANS_DOOR", "51 FORCE_CRITICAL_HIT unused", "52 GUARANTEE_HIT one spell: zzOLDCritical Shot", "53 ENCHANT_ITEM", "54 ENCHANT_ITEM_TEMPORARY", "55 TAMECREATURE", "56 SUMMON_PET", "57 LEARN_PET_SPELL", "58 WEAPON_DAMAGE", "59 CREATE_RANDOM_ITEM create item base at spell specific loot", "60 PROFICIENCY", "61 SEND_EVENT", "62 POWER_BURN", "63 THREAT", "64 TRIGGER_SPELL", "65 APPLY_AREA_AURA_RAID", "66 CREATE_MANA_GEM (possibly recharge it, misc - is item ID)", "67 HEAL_MAX_HEALTH", "68 INTERRUPT_CAST", "69 DISTRACT", "70 PULL one spell: Distract Move", "71 PICKPOCKET", "72 ADD_FARSIGHT", "73 UNTRAIN_TALENTS", "74 APPLY_GLYPH", "75 HEAL_MECHANICAL one spell: Mechanical Patch Kit", "76 SUMMON_OBJECT_WILD", "77 SCRIPT_EFFECT", "78 ATTACK", "79 SANCTUARY", "80 ADD_COMBO_POINTS", "81 CREATE_HOUSE one spell: Create House (TEST)", "82 BIND_SIGHT", "83 DUEL", "84 STUCK", "85 SUMMON_PLAYER", "86 ACTIVATE_OBJECT", "87 GAMEOBJECT_DAMAGE", "88 GAMEOBJECT_REPAIR", "89 GAMEOBJECT_SET_DESTRUCTION_STATE", "90 KILL_CREDIT Kill credit but only for single person", "91 THREAT_ALL one spell: zzOLDBrainwash", "92 ENCHANT_HELD_ITEM", "93 FORCE_DESELECT", "94 SELF_RESURRECT", "95 SKINNING", "96 CHARGE", "97 CAST_BUTTON (totem bar since 3.2.2a)", "98 KNOCK_BACK", "99 DISENCHANT", "100 INEBRIATE", "101 FEED_PET", "102 DISMISS_PET", "103 REPUTATION", "104 SUMMON_OBJECT_SLOT1", "105 SUMMON_OBJECT_SLOT2", "106 SUMMON_OBJECT_SLOT3", "107 SUMMON_OBJECT_SLOT4", "108 DISPEL_MECHANIC", "109 SUMMON_DEAD_PET", "110 DESTROY_ALL_TOTEMS", "111 DURABILITY_DAMAGE", "112 112", "113 RESURRECT_NEW", "114 ATTACK_ME", "115 DURABILITY_DAMAGE_PCT", "116 SKIN_PLAYER_CORPSE one spell: Remove Insignia, bg usage, required special cor" & _
                "pse flags...", "117 SPIRIT_HEAL one spell: Spirit Heal", "118 SKILL professions and more", "119 APPLY_AREA_AURA_PET", "120 TELEPORT_GRAVEYARD one spell: Graveyard Teleport Test", "121 NORMALIZED_WEAPON_DMG", "122 122 unused", "123 SEND_TAXI taxi/flight related (misc value is taxi path id)", "124 PULL_TOWARDS", "125 MODIFY_THREAT_PERCENT", "126 STEAL_BENEFICIAL_BUFF spell steal effect?", "127 PROSPECTING Prospecting spell", "128 APPLY_AREA_AURA_FRIEND", "129 APPLY_AREA_AURA_ENEMY", "130 REDIRECT_THREAT", "131 PLAYER_NOTIFICATION sound id in misc value (SoundEntries.dbc)", "132 PLAY_MUSIC sound id in misc value (SoundEntries.dbc)", "133 UNLEARN_SPECIALIZATION unlearn profession specialization", "134 KILL_CREDIT misc value is creature entry", "135 CALL_PET", "136 HEAL_PCT", "137 ENERGIZE_PCT", "138 LEAP_BACK Leap back", "139 CLEAR_QUEST Reset quest status (miscValue - quest ID)", "140 FORCE_CAST", "141 FORCE_CAST_WITH_VALUE", "142 TRIGGER_SPELL_WITH_VALUE", "143 APPLY_AREA_AURA_OWNER", "144 KNOCK_BACK_DEST", "145 PULL_TOWARDS_DEST Black Hole Effect", "146 ACTIVATE_RUNE", "147 QUEST_FAIL quest fail", "148 TRIGGER_MISSILE_SPELL_WITH_VALUE", "149 CHARGE_DEST", "150 QUEST_START", "151 TRIGGER_SPELL_2", "152 SUMMON_RAF_FRIEND summon Refer-a-Friend", "153 CREATE_TAMED_PET misc value is creature entry", "154 DISCOVER_TAXI", "155 TITAN_GRIP Allows you to equip two-handed axes, maces and swords in one hand," & _
                " but you attack $49152s1% slower than normal.", "156 ENCHANT_ITEM_PRISMATIC", "157 CREATE_ITEM_2 create item or create item template and replace by some randon " & _
                "spell loot item", "158 MILLING milling", "159 ALLOW_RENAME_PET allow rename pet once again", "160 160 1 spell - 45534", "161 TALENT_SPEC_COUNT second talent spec (learn/revert)", "162 TALENT_SPEC_SELECT activate primary/secondary spec", "163 unused", "164 REMOVE_AURA"})
        Me.Effect2.Location = New System.Drawing.Point(502, 117)
        Me.Effect2.Name = "Effect2"
        Me.Effect2.Size = New System.Drawing.Size(288, 24)
        Me.Effect2.TabIndex = 19
        '
        'Effect3Max
        '
        Me.Effect3Max.Location = New System.Drawing.Point(557, 263)
        Me.Effect3Max.Name = "Effect3Max"
        Me.Effect3Max.Size = New System.Drawing.Size(233, 22)
        Me.Effect3Max.TabIndex = 30
        '
        'Effect3Min
        '
        Me.Effect3Min.Location = New System.Drawing.Point(557, 235)
        Me.Effect3Min.Name = "Effect3Min"
        Me.Effect3Min.Size = New System.Drawing.Size(233, 22)
        Me.Effect3Min.TabIndex = 29
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(440, 209)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 17)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Effect 3"
        '
        'Effect3
        '
        Me.Effect3.FormattingEnabled = True
        Me.Effect3.Items.AddRange(New Object() {"0 NONE", "1 INSTAKILL", "2 SCHOOL_DAMAGE", "3 DUMMY", "4 PORTAL_TELEPORT unused", "5 TELEPORT_UNITS", "6 APPLY_AURA", "7 ENVIRONMENTAL_DAMAGE", "8 POWER_DRAIN", "9 HEALTH_LEECH", "10 HEAL", "11 BIND", "12 PORTAL", "13 RITUAL_BASE unused", "14 RITUAL_SPECIALIZE unused", "15 RITUAL_ACTIVATE_PORTAL unused", "16 QUEST_COMPLETE", "17 WEAPON_DAMAGE_NOSCHOOL", "18 RESURRECT", "19 ADD_EXTRA_ATTACKS", "20 DODGE one spell: Dodge", "21 EVADE one spell: Evade (DND)", "22 PARRY", "23 BLOCK one spell: Block", "24 CREATE_ITEM", "25 WEAPON", "26 DEFENSE one spell: Defense", "27 PERSISTENT_AREA_AURA", "28 SUMMON", "29 LEAP", "30 ENERGIZE", "31 WEAPON_PERCENT_DAMAGE", "32 TRIGGER_MISSILE", "33 OPEN_LOCK", "34 SUMMON_CHANGE_ITEM", "35 APPLY_AREA_AURA_PARTY", "36 LEARN_SPELL", "37 SPELL_DEFENSE one spell: SPELLDEFENSE (DND)", "38 DISPEL", "39 LANGUAGE", "40 DUAL_WIELD", "41 JUMP", "42 JUMP_DEST", "43 TELEPORT_UNITS_FACE_CASTER", "44 SKILL_STEP", "45 ADD_HONOR honor/pvp related", "46 SPAWN clientside, unit appears as if it was just spawned", "47 TRADE_SKILL", "48 STEALTH one spell: Base Stealth", "49 DETECT one spell: Detect", "50 TRANS_DOOR", "51 FORCE_CRITICAL_HIT unused", "52 GUARANTEE_HIT one spell: zzOLDCritical Shot", "53 ENCHANT_ITEM", "54 ENCHANT_ITEM_TEMPORARY", "55 TAMECREATURE", "56 SUMMON_PET", "57 LEARN_PET_SPELL", "58 WEAPON_DAMAGE", "59 CREATE_RANDOM_ITEM create item base at spell specific loot", "60 PROFICIENCY", "61 SEND_EVENT", "62 POWER_BURN", "63 THREAT", "64 TRIGGER_SPELL", "65 APPLY_AREA_AURA_RAID", "66 CREATE_MANA_GEM (possibly recharge it, misc - is item ID)", "67 HEAL_MAX_HEALTH", "68 INTERRUPT_CAST", "69 DISTRACT", "70 PULL one spell: Distract Move", "71 PICKPOCKET", "72 ADD_FARSIGHT", "73 UNTRAIN_TALENTS", "74 APPLY_GLYPH", "75 HEAL_MECHANICAL one spell: Mechanical Patch Kit", "76 SUMMON_OBJECT_WILD", "77 SCRIPT_EFFECT", "78 ATTACK", "79 SANCTUARY", "80 ADD_COMBO_POINTS", "81 CREATE_HOUSE one spell: Create House (TEST)", "82 BIND_SIGHT", "83 DUEL", "84 STUCK", "85 SUMMON_PLAYER", "86 ACTIVATE_OBJECT", "87 GAMEOBJECT_DAMAGE", "88 GAMEOBJECT_REPAIR", "89 GAMEOBJECT_SET_DESTRUCTION_STATE", "90 KILL_CREDIT Kill credit but only for single person", "91 THREAT_ALL one spell: zzOLDBrainwash", "92 ENCHANT_HELD_ITEM", "93 FORCE_DESELECT", "94 SELF_RESURRECT", "95 SKINNING", "96 CHARGE", "97 CAST_BUTTON (totem bar since 3.2.2a)", "98 KNOCK_BACK", "99 DISENCHANT", "100 INEBRIATE", "101 FEED_PET", "102 DISMISS_PET", "103 REPUTATION", "104 SUMMON_OBJECT_SLOT1", "105 SUMMON_OBJECT_SLOT2", "106 SUMMON_OBJECT_SLOT3", "107 SUMMON_OBJECT_SLOT4", "108 DISPEL_MECHANIC", "109 SUMMON_DEAD_PET", "110 DESTROY_ALL_TOTEMS", "111 DURABILITY_DAMAGE", "112 112", "113 RESURRECT_NEW", "114 ATTACK_ME", "115 DURABILITY_DAMAGE_PCT", "116 SKIN_PLAYER_CORPSE one spell: Remove Insignia, bg usage, required special cor" & _
                "pse flags...", "117 SPIRIT_HEAL one spell: Spirit Heal", "118 SKILL professions and more", "119 APPLY_AREA_AURA_PET", "120 TELEPORT_GRAVEYARD one spell: Graveyard Teleport Test", "121 NORMALIZED_WEAPON_DMG", "122 122 unused", "123 SEND_TAXI taxi/flight related (misc value is taxi path id)", "124 PULL_TOWARDS", "125 MODIFY_THREAT_PERCENT", "126 STEAL_BENEFICIAL_BUFF spell steal effect?", "127 PROSPECTING Prospecting spell", "128 APPLY_AREA_AURA_FRIEND", "129 APPLY_AREA_AURA_ENEMY", "130 REDIRECT_THREAT", "131 PLAYER_NOTIFICATION sound id in misc value (SoundEntries.dbc)", "132 PLAY_MUSIC sound id in misc value (SoundEntries.dbc)", "133 UNLEARN_SPECIALIZATION unlearn profession specialization", "134 KILL_CREDIT misc value is creature entry", "135 CALL_PET", "136 HEAL_PCT", "137 ENERGIZE_PCT", "138 LEAP_BACK Leap back", "139 CLEAR_QUEST Reset quest status (miscValue - quest ID)", "140 FORCE_CAST", "141 FORCE_CAST_WITH_VALUE", "142 TRIGGER_SPELL_WITH_VALUE", "143 APPLY_AREA_AURA_OWNER", "144 KNOCK_BACK_DEST", "145 PULL_TOWARDS_DEST Black Hole Effect", "146 ACTIVATE_RUNE", "147 QUEST_FAIL quest fail", "148 TRIGGER_MISSILE_SPELL_WITH_VALUE", "149 CHARGE_DEST", "150 QUEST_START", "151 TRIGGER_SPELL_2", "152 SUMMON_RAF_FRIEND summon Refer-a-Friend", "153 CREATE_TAMED_PET misc value is creature entry", "154 DISCOVER_TAXI", "155 TITAN_GRIP Allows you to equip two-handed axes, maces and swords in one hand," & _
                " but you attack $49152s1% slower than normal.", "156 ENCHANT_ITEM_PRISMATIC", "157 CREATE_ITEM_2 create item or create item template and replace by some randon " & _
                "spell loot item", "158 MILLING milling", "159 ALLOW_RENAME_PET allow rename pet once again", "160 160 1 spell - 45534", "161 TALENT_SPEC_COUNT second talent spec (learn/revert)", "162 TALENT_SPEC_SELECT activate primary/secondary spec", "163 unused", "164 REMOVE_AURA"})
        Me.Effect3.Location = New System.Drawing.Point(502, 204)
        Me.Effect3.Name = "Effect3"
        Me.Effect3.Size = New System.Drawing.Size(288, 24)
        Me.Effect3.TabIndex = 25
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(440, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(115, 17)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Effect Mod Value"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(439, 151)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 17)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "Effect Base Value"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(441, 266)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(115, 17)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Effect Mod Value"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(440, 238)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(120, 17)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Effect Base Value"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Enabled = False
        Me.RichTextBox1.Location = New System.Drawing.Point(796, 45)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(115, 240)
        Me.RichTextBox1.TabIndex = 36
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(671, 295)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(240, 32)
        Me.Button4.TabIndex = 37
        Me.Button4.Text = "Spell Effect Editor"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(442, 295)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(223, 32)
        Me.Button5.TabIndex = 38
        Me.Button5.Text = "Attributes Editor"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(91, 312)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(233, 26)
        Me.Button6.TabIndex = 39
        Me.Button6.Text = "Targets Editor"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(327, 117)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 17)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "Spell Level"
        '
        'spellevel
        '
        Me.spellevel.Location = New System.Drawing.Point(327, 137)
        Me.spellevel.Name = "spellevel"
        Me.spellevel.Size = New System.Drawing.Size(109, 22)
        Me.spellevel.TabIndex = 41
        '
        'baselevel
        '
        Me.baselevel.Location = New System.Drawing.Point(327, 182)
        Me.baselevel.Name = "baselevel"
        Me.baselevel.Size = New System.Drawing.Size(109, 22)
        Me.baselevel.TabIndex = 43
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(327, 162)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 17)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "Base Level"
        '
        'maxlevel
        '
        Me.maxlevel.Location = New System.Drawing.Point(327, 233)
        Me.maxlevel.Name = "maxlevel"
        Me.maxlevel.Size = New System.Drawing.Size(109, 22)
        Me.maxlevel.TabIndex = 45
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(327, 213)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(71, 17)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Max Level"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(923, 489)
        Me.Controls.Add(Me.maxlevel)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.baselevel)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.spellevel)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Effect3Max)
        Me.Controls.Add(Me.Effect3Min)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Effect3)
        Me.Controls.Add(Me.Effect2Max)
        Me.Controls.Add(Me.Effect2Min)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Effect2)
        Me.Controls.Add(Me.Effect1Max)
        Me.Controls.Add(Me.Effect1Min)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Effect1)
        Me.Controls.Add(Me.iconid)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Rank)
        Me.Controls.Add(Me.BuffDescription)
        Me.Controls.Add(Me.Description)
        Me.Controls.Add(Me.SName)
        Me.Controls.Add(Me.ListBox1)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.Text = "Basic Spell Editor - Stoneharry"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents SName As System.Windows.Forms.TextBox
    Friend WithEvents Description As System.Windows.Forms.RichTextBox
    Friend WithEvents BuffDescription As System.Windows.Forms.RichTextBox
    Friend WithEvents Rank As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents iconid As System.Windows.Forms.Label
    Friend WithEvents Effect1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Effect1Min As System.Windows.Forms.TextBox
    Friend WithEvents Effect1Max As System.Windows.Forms.TextBox
    Friend WithEvents Effect2Max As System.Windows.Forms.TextBox
    Friend WithEvents Effect2Min As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Effect2 As System.Windows.Forms.ComboBox
    Friend WithEvents Effect3Max As System.Windows.Forms.TextBox
    Friend WithEvents Effect3Min As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Effect3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents spellevel As System.Windows.Forms.TextBox
    Friend WithEvents baselevel As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents maxlevel As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label

End Class
