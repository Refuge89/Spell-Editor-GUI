<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form6
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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckedListBox3 = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(429, 638)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 35)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Done"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"NULL", "ON_MOVEMENT", "PUSHBACK // whether or not the spell is pushed back on dmg", "ON_INTERRUPT_CAST // ? probably interrupt only cast", "ON_INTERRUPT_SCHOOL // interrupts only 1 school, like counterspell", "ON_DAMAGE_TAKEN", "ON_INTERRUPT_ALL // guessed"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(11, 29)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(501, 123)
        Me.CheckedListBox1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Interrupt Flags"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 154)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Aura Interrupt Flags"
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.FormattingEnabled = True
        Me.CheckedListBox2.Items.AddRange(New Object() {"NULL", "HOSTILE_SPELL_INFLICTED", "ANY_DAMAGE_TAKEN", "UNK1", "MOVEMENT", "TURNING", "ENTER_COMBAT", "DISMOUNT", "ENTER_WATER", "LEAVE_WATER", "UNUSED2", "UNK4", "UNK5", "START_ATTACK", "UNK6", "UNUSED3", "CAST_SPELL", "UNK7", "MOUNT", "STAND_UP", "LEAVE_AREA", "INVINCIBLE", "STEALTH", "UNK8", "PVP_ENTER", "DIRECT_DAMAGE", "LANDING", "AFTER_CAST_SPELL"})
        Me.CheckedListBox2.Location = New System.Drawing.Point(11, 174)
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.Size = New System.Drawing.Size(501, 259)
        Me.CheckedListBox2.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 436)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Channel Interrupt Flags"
        '
        'CheckedListBox3
        '
        Me.CheckedListBox3.FormattingEnabled = True
        Me.CheckedListBox3.Items.AddRange(New Object() {"NULL", "ON_1", "ON_2", "ON_3", "ON_4", "ON_5", "ON_6", "ON_7", "ON_8", "ON_9", "ON_10", "ON_11", "ON_12", "ON_13", "ON_14", "ON_15", "ON_16", "ON_17", "ON_18"})
        Me.CheckedListBox3.Location = New System.Drawing.Point(11, 458)
        Me.CheckedListBox3.Name = "CheckedListBox3"
        Me.CheckedListBox3.Size = New System.Drawing.Size(501, 174)
        Me.CheckedListBox3.TabIndex = 6
        '
        'Form6
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 685)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckedListBox3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckedListBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Button2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form6"
        Me.ShowIcon = False
        Me.Text = "Interrupt Editor - Stoneharry"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox3 As System.Windows.Forms.CheckedListBox
End Class
