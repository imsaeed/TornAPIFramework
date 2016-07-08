Public Class frmMain
    Public bLoaded As Boolean = False

    Public sBGWCategory As String = ""
    Public sBGWSelections As String = ""
    Public sBGWUseID As String = ""

    Public Sub New()
        InitializeComponent()
        bgwJSONUpdater.WorkerReportsProgress = True
        bgwJSONUpdater.WorkerSupportsCancellation = True
    End Sub

    Public Sub GoDoBackgroundJSON(ByVal sCategory As String, ByVal sSelections As String, ByVal sUseID As String)
        sBGWCategory = sCategory
        sBGWSelections = sSelections
        sBGWUseID = sUseID
        If bgwJSONUpdater.IsBusy <> True Then bgwJSONUpdater.RunWorkerAsync()
    End Sub

    Public Sub StopBackgroundJSON()
        If bgwJSONUpdater.WorkerSupportsCancellation Then bgwJSONUpdater.CancelAsync()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Enabled = False
        frmStart.Close()
        Me.Show()
        If Not bResetWindow Then Me.Location = My.Settings.WindowLocation
        Application.DoEvents()

        Me.Text = Application.ProductName.Replace("_", " ") & " v" & Application.ProductVersion & " - " & sName & " [" & sPlayerID & "]"
        If bDebug Then sslblShowDebugScreen.Visible = True

        AlwaysOnTopToolStripMenuItem.Checked = My.Settings.AlwaysOnTop
        Me.TopMost = AlwaysOnTopToolStripMenuItem.Checked
        CompactViewToolStripMenuItem.Checked = My.Settings.CompactView
        Call ChangeView(CompactViewToolStripMenuItem.Checked)

        'Do application-specific startup stuff here

        Me.Enabled = True
        Me.Show()
        Application.DoEvents()
        Me.TopMost = AlwaysOnTopToolStripMenuItem.Checked

        bLoaded = True
    End Sub

    Private Sub sslblShowDebugScreen_Click(sender As System.Object, e As System.EventArgs) Handles sslblShowDebugScreen.Click
        frmDebug.Show()
    End Sub

    Private Sub sslblByMcNeo_Click(sender As System.Object, e As System.EventArgs) Handles sslblByMcNeo.Click
        Process.Start("https://www.torn.com/profiles.php?XID=864688")
    End Sub

    Private Sub sslblOptions_Click(sender As System.Object, e As System.EventArgs) Handles sslblOptions.Click
        cmOptions.Show(MousePosition)
    End Sub

    Private Sub AlwaysOnTopToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AlwaysOnTopToolStripMenuItem.Click
        If AlwaysOnTopToolStripMenuItem.Checked = False Then
            AlwaysOnTopToolStripMenuItem.Checked = True
        Else
            AlwaysOnTopToolStripMenuItem.Checked = False
        End If
        Me.TopMost = AlwaysOnTopToolStripMenuItem.Checked

        My.Settings.AlwaysOnTop = AlwaysOnTopToolStripMenuItem.Checked
        My.Settings.Save()
    End Sub

    Private Sub CompactViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CompactViewToolStripMenuItem.Click
        If CompactViewToolStripMenuItem.Checked = False Then
            CompactViewToolStripMenuItem.Checked = True
        Else
            CompactViewToolStripMenuItem.Checked = False
        End If
        Call ChangeView(CompactViewToolStripMenuItem.Checked)

        My.Settings.CompactView = CompactViewToolStripMenuItem.Checked
        My.Settings.Save()
    End Sub

    Private Sub frmMain_Move(sender As Object, e As System.EventArgs) Handles Me.Move
        If Me.WindowState <> FormWindowState.Normal Then Exit Sub
        My.Settings.WindowLocation = Me.Location
        My.Settings.Save()
    End Sub

    Private Sub ChangeView(bCompact As Boolean)

    End Sub

    Private Sub bgwJSONUpdater_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwJSONUpdater.DoWork
        Call GoGetJSONData(sBGWCategory, sBGWSelections, sBGWUseID, False)
    End Sub

    Private Sub bgwJSONUpdater_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwJSONUpdater.RunWorkerCompleted
        If e.Cancelled = True Then
            'MsgBox("Canceled!")
        ElseIf e.Error IsNot Nothing Then
            'MsgBox("Error: " & e.Error.Message)
        Else
            'MsgBox("Done!")
            'Call DoScreenUpdate()
        End If
    End Sub
End Class