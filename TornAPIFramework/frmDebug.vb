Public Class frmDebug
    Private Sub DoRefreshList()
        lstDebug.Items.Clear()
        lstDebug.Items.Add("Application name=" & Application.ProductName.Replace("_", " "))
        lstDebug.Items.Add("Application version=" & Application.ProductVersion)
        lstDebug.Items.Add("Expiration=" & lEpochExpiration)
        lstDebug.Items.Add("Framework version=" & sFrameworkVersion)
        lstDebug.Items.Add("--")
        lstDebug.Items.Add("Use HTTPS=" & bUseHTTPS)
        lstDebug.Items.Add("UserAgent=""" & sUserAgent & """")
        lstDebug.Items.Add("--")
        lstDebug.Items.Add("sAPIKey=" & sAPIKey)
        lstDebug.Items.Add("Array totals:" & mField.GetUpperBound(0) & "/" & mValue.GetUpperBound(0))
        lstDebug.Items.Add("--")
        lstDebug.Items.Add("Index:Field=Value")
        For i = 0 To mField.GetUpperBound(0)
            lstDebug.Items.Add(i & ":" & mField(i) & "=""" & mValue(i) & """")
        Next
        lstDebug.Items.Add("-- END --")
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call DoRefreshList()
    End Sub

    Private Sub frmDebug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Application.ProductName.Replace("_", " ") & " v" & Application.ProductVersion & " - Debug"
        'Call GetJSONData("user", "profile", sPlayerID, False)
        'If GetValue("user/" & sPlayerID & "/property_id") <> "0" Then Call GetJSONData("property", "", GetValue("user/" & sPlayerID & "/property_id"), False)
        'If GetValue("user/" & sPlayerID & "/faction/faction_id") <> "0" Then Call GetJSONData("faction", "", GetValue("user/" & sPlayerID & "/faction/faction_id"), False)
        'If GetValue("user/" & sPlayerID & "/job/company_id") <> "0" Then Call GetJSONData("company", "", GetValue("user/" & sPlayerID & "/job/company_id"), False)
        Call DoRefreshList()
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim r

        r = MsgBox("Warning!  This will save your private Torn data as plain text.  Make sure you keep the saved file secure and only share it with trusted parties." & vbCrLf & vbCrLf & "Continue?", vbQuestion + vbYesNo, "Question")

        If r <> vbYes Then Exit Sub

        diaSave.Title = "Save user data - " & Application.ProductName.Replace("_", " ")
        diaSave.ShowDialog()

        If diaSave.FileName = "" Then Exit Sub

        Dim filOutput As System.IO.StreamWriter

        filOutput = My.Computer.FileSystem.OpenTextFileWriter(diaSave.FileName, False)
        For i = 0 To lstDebug.Items.Count - 1
            filOutput.WriteLine(lstDebug.Items(i))
        Next
        filOutput.Close()
    End Sub
End Class