<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStart
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAPIKey = New System.Windows.Forms.TextBox()
        Me.chkRememberKey = New System.Windows.Forms.CheckBox()
        Me.chkAutoLaunch = New System.Windows.Forms.CheckBox()
        Me.cmdLaunch = New System.Windows.Forms.Button()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.lblForgetAPI = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblByMcNeo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please enter your Torn API Key:"
        '
        'txtAPIKey
        '
        Me.txtAPIKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPIKey.Location = New System.Drawing.Point(12, 25)
        Me.txtAPIKey.Name = "txtAPIKey"
        Me.txtAPIKey.Size = New System.Drawing.Size(158, 29)
        Me.txtAPIKey.TabIndex = 1
        Me.txtAPIKey.Text = "txtAPIKey"
        '
        'chkRememberKey
        '
        Me.chkRememberKey.AutoSize = True
        Me.chkRememberKey.Location = New System.Drawing.Point(179, 25)
        Me.chkRememberKey.Name = "chkRememberKey"
        Me.chkRememberKey.Size = New System.Drawing.Size(98, 17)
        Me.chkRememberKey.TabIndex = 2
        Me.chkRememberKey.Text = "Remember Key"
        Me.chkRememberKey.UseVisualStyleBackColor = True
        '
        'chkAutoLaunch
        '
        Me.chkAutoLaunch.AutoSize = True
        Me.chkAutoLaunch.Location = New System.Drawing.Point(179, 40)
        Me.chkAutoLaunch.Name = "chkAutoLaunch"
        Me.chkAutoLaunch.Size = New System.Drawing.Size(87, 17)
        Me.chkAutoLaunch.TabIndex = 3
        Me.chkAutoLaunch.Text = "Auto Launch"
        Me.chkAutoLaunch.UseVisualStyleBackColor = True
        '
        'cmdLaunch
        '
        Me.cmdLaunch.Location = New System.Drawing.Point(12, 60)
        Me.cmdLaunch.Name = "cmdLaunch"
        Me.cmdLaunch.Size = New System.Drawing.Size(298, 29)
        Me.cmdLaunch.TabIndex = 4
        Me.cmdLaunch.Text = "Launch"
        Me.cmdLaunch.UseVisualStyleBackColor = True
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp.ForeColor = System.Drawing.Color.Blue
        Me.lblHelp.Location = New System.Drawing.Point(12, 92)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(29, 13)
        Me.lblHelp.TabIndex = 5
        Me.lblHelp.Text = "Help"
        '
        'lblForgetAPI
        '
        Me.lblForgetAPI.AutoSize = True
        Me.lblForgetAPI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblForgetAPI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForgetAPI.ForeColor = System.Drawing.Color.Blue
        Me.lblForgetAPI.Location = New System.Drawing.Point(273, 26)
        Me.lblForgetAPI.Name = "lblForgetAPI"
        Me.lblForgetAPI.Size = New System.Drawing.Size(37, 13)
        Me.lblForgetAPI.TabIndex = 6
        Me.lblForgetAPI.Text = "Forget"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Options:"
        '
        'lblByMcNeo
        '
        Me.lblByMcNeo.AutoSize = True
        Me.lblByMcNeo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblByMcNeo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblByMcNeo.ForeColor = System.Drawing.Color.Blue
        Me.lblByMcNeo.Location = New System.Drawing.Point(253, 92)
        Me.lblByMcNeo.Name = "lblByMcNeo"
        Me.lblByMcNeo.Size = New System.Drawing.Size(57, 13)
        Me.lblByMcNeo.TabIndex = 8
        Me.lblByMcNeo.Text = "By McNeo"
        '
        'frmStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 114)
        Me.Controls.Add(Me.lblByMcNeo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblForgetAPI)
        Me.Controls.Add(Me.lblHelp)
        Me.Controls.Add(Me.cmdLaunch)
        Me.Controls.Add(Me.chkAutoLaunch)
        Me.Controls.Add(Me.chkRememberKey)
        Me.Controls.Add(Me.txtAPIKey)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmStart"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAPIKey As System.Windows.Forms.TextBox
    Friend WithEvents chkRememberKey As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoLaunch As System.Windows.Forms.CheckBox
    Friend WithEvents cmdLaunch As System.Windows.Forms.Button
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    Friend WithEvents lblForgetAPI As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblByMcNeo As System.Windows.Forms.Label

End Class
