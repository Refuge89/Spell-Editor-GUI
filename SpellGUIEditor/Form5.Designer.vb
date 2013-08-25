<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form5
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
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(378, 362)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 35)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Done"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"Player or target ? Might also be none-individual target", "? (Not used in 3.3.5a on it's own)", "? (Not used in 3.3.5a on it's own)", "? (Not used in 3.3.5a on it's own)", "? (Not used in 3.3.5a on it's own)", "Enchantment (Item in inventory and equipment)", "Point-Blank AoE", "Targetable AoE ", "Targetable AoE ?(Some Stealth-revealing spell AoE has it)", "Teleport ? Seems general based (Seems to handle every Player in AoE individually)" & _
                "", "Player corpses selectable ? Used for looting insignias.", "Revives every player in Point-Blank AoE (Spirit Revival)", "Used for skinning (Target corpses ?)", "Spawns something on caster spot ? (Used in a spell that drops object at caster po" & _
                "sition)", "? (Not used in 3.3.5a on it's own)", "? (Not used in 3.3.5a on it's own)", "Target chests ? Used for Lock-opening spells", "? (Used with non-referenced, passive spells)", "? (Not used in 3.3.5a on it's own)", "Glyphs (Maybe change how another spell works ?)"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(12, 12)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(451, 344)
        Me.CheckedListBox1.TabIndex = 2
        '
        'Form5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 406)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Button2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form5"
        Me.ShowIcon = False
        Me.Text = "Targets Editor - Stoneharry"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
End Class
