<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.sslblUpdateTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslblShowDebugScreen = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslblOptions = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslblByMcNeo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.cmOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AlwaysOnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompactViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bgwJSONUpdater = New System.ComponentModel.BackgroundWorker()
        Me.ssStatus.SuspendLayout()
        Me.cmOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'ssStatus
        '
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sslblUpdateTime, Me.ssSpring, Me.sslblShowDebugScreen, Me.sslblOptions, Me.sslblByMcNeo})
        Me.ssStatus.Location = New System.Drawing.Point(0, 314)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ssStatus.Size = New System.Drawing.Size(586, 22)
        Me.ssStatus.SizingGrip = False
        Me.ssStatus.TabIndex = 29
        Me.ssStatus.Text = "ssStatus"
        '
        'sslblUpdateTime
        '
        Me.sslblUpdateTime.Name = "sslblUpdateTime"
        Me.sslblUpdateTime.Size = New System.Drawing.Size(95, 17)
        Me.sslblUpdateTime.Text = "sslblUpdateTime"
        '
        'ssSpring
        '
        Me.ssSpring.Name = "ssSpring"
        Me.ssSpring.Size = New System.Drawing.Size(365, 17)
        Me.ssSpring.Spring = True
        '
        'sslblShowDebugScreen
        '
        Me.sslblShowDebugScreen.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sslblShowDebugScreen.ForeColor = System.Drawing.Color.Blue
        Me.sslblShowDebugScreen.Name = "sslblShowDebugScreen"
        Me.sslblShowDebugScreen.Size = New System.Drawing.Size(112, 17)
        Me.sslblShowDebugScreen.Text = "Show Debug Screen"
        Me.sslblShowDebugScreen.Visible = False
        '
        'sslblOptions
        '
        Me.sslblOptions.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sslblOptions.ForeColor = System.Drawing.Color.Blue
        Me.sslblOptions.Name = "sslblOptions"
        Me.sslblOptions.Size = New System.Drawing.Size(49, 17)
        Me.sslblOptions.Text = "Options"
        '
        'sslblByMcNeo
        '
        Me.sslblByMcNeo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sslblByMcNeo.ForeColor = System.Drawing.Color.Blue
        Me.sslblByMcNeo.Name = "sslblByMcNeo"
        Me.sslblByMcNeo.Size = New System.Drawing.Size(62, 17)
        Me.sslblByMcNeo.Text = "By McNeo"
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 5000
        '
        'cmOptions
        '
        Me.cmOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlwaysOnTopToolStripMenuItem, Me.CompactViewToolStripMenuItem})
        Me.cmOptions.Name = "cmOptions"
        Me.cmOptions.Size = New System.Drawing.Size(152, 48)
        Me.cmOptions.Text = "Options"
        '
        'AlwaysOnTopToolStripMenuItem
        '
        Me.AlwaysOnTopToolStripMenuItem.Name = "AlwaysOnTopToolStripMenuItem"
        Me.AlwaysOnTopToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.AlwaysOnTopToolStripMenuItem.Text = "Always on Top"
        '
        'CompactViewToolStripMenuItem
        '
        Me.CompactViewToolStripMenuItem.Name = "CompactViewToolStripMenuItem"
        Me.CompactViewToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.CompactViewToolStripMenuItem.Text = "Compact View"
        '
        'bgwJSONUpdater
        '
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 336)
        Me.Controls.Add(Me.ssStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(318, 29)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMain"
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
        Me.cmOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ssStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents sslblUpdateTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ssSpring As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslblShowDebugScreen As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslblByMcNeo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslblOptions As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
    Friend WithEvents cmOptions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AlwaysOnTopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompactViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bgwJSONUpdater As System.ComponentModel.BackgroundWorker
End Class
