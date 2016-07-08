Public Class frmStart
    Private Sub lblHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHelp.Click
        Dim sMessage As String

        sMessage = "Your Torn API Key is required to use this program.  It can be found on the Torn website by clicking the gear in the top-right to access your preferences, then under the ""API Key"" section." & vbCrLf & vbCrLf
        sMessage &= "Your email address, Torn username, and Torn password are NOT required to use this program; please do not input this information anywhere in this program." & vbCrLf & vbCrLf
        sMessage &= "You can choose for this program to remember your Torn API Key if you do not want to enter it every time.  If you select this option, your Torn API Key is stored as plain text on this computer in your unique Windows user folder.  Please do not select this option if you are using a public computer or shared Windows user account, unless you trust any other users of your Windows user account.  Your Torn API Key can be forgotten by selecting ""Forget"" on this initial screen, or elsewhere in the main program screen." & vbCrLf & vbCrLf
        sMessage &= "If you choose for this program to remember your Torn API Key, you can also choose to auto launch the program, which will skip this initial screen and bring you directly to the main program screen." & vbCrLf & vbCrLf
        sMessage &= "Your Torn API Key is only used to authenticate you to the Torn servers and gather the data this program needs; your Torn API Key and any data gathered is not transmitted to any other server or service or logged in any way (unless you choose for this program to remember your Torn API Key, in which case it is stored as described above)."

        MsgBox(sMessage, vbInformation + vbOKOnly, "Help")
    End Sub

    Private Sub lblForgetAPI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblForgetAPI.Click
        Call PromptForgetAPI()
    End Sub

    Private Sub frmStart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo lblStartError

        Me.Enabled = False
        Me.Show()
        Application.DoEvents()

        For Each sArgument As String In My.Application.CommandLineArgs
            If InStr(sArgument, "http") > 0 Then bUseHTTPS = False
            If InStr(sArgument, "debug") > 0 Then bDebug = True
            If InStr(sArgument, "resetwindow") > 0 Then
                bResetWindow = True
                frmMain.StartPosition = FormStartPosition.CenterScreen
            End If
            If InStr(sArgument, "resetsettings") > 0 Then
                MsgBox("All settings have be reset, please re-launch Torn Tools without the /resetsettings argument.", vbInformation + vbOKOnly, "Torn Tools Reset")
                My.Settings.Reset()
                My.Settings.SettingsResetToDefault = True
                My.Settings.Save()
                End
            End If
        Next

        If My.Settings.SettingsUpgradeRequired Then
            If My.Settings.SettingsResetToDefault Then
                My.Settings.Reset()
                My.Settings.SettingsUpgradeRequired = False
                My.Settings.SettingsResetToDefault = False
                My.Settings.Save()
            Else
                My.Settings.Upgrade()
                My.Settings.SettingsUpgradeRequired = False
                My.Settings.SettingsResetToDefault = False
                My.Settings.Save()
            End If
        End If

        Me.Text = Application.ProductName.Replace("_", " ") & " Launcher"
        txtAPIKey.Text = ""

        'Clear options on frmStart
        chkRememberKey.Checked = False
        chkRememberKey.Enabled = True
        chkAutoLaunch.Checked = False
        chkAutoLaunch.Enabled = False

        If My.Settings.TornAPIKey <> "" Then
            txtAPIKey.Text = My.Settings.TornAPIKey
            chkRememberKey.Checked = True
            chkAutoLaunch.Enabled = True
            chkAutoLaunch.Checked = My.Settings.AutoLaunch
        End If

        Call DoDataStartup()

        'Done loading

        Me.Enabled = True
        Me.Show()
        Application.DoEvents()

        If chkAutoLaunch.Checked Then Call GoLaunch()
        Exit Sub

lblStartError:
        Call DoErrorHandler("An unexpected error occured while starting the program.", True)

    End Sub

    Private Sub chkRememberKey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRememberKey.CheckedChanged
        If chkRememberKey.Checked = True Then
            chkAutoLaunch.Enabled = True
        Else
            chkAutoLaunch.Checked = False
            chkAutoLaunch.Enabled = False
        End If
    End Sub

    Private Sub cmdLaunch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLaunch.Click
        Call GoLaunch()
    End Sub


    Private Sub GoLaunch()
        'On Error GoTo GoLaunchError

        If txtAPIKey.Text = "" Then
            Call DoErrorHandler("Please enter your Torn API Key.", False)
            Exit Sub
        End If

        Me.Enabled = False
        Me.Show()
        Application.DoEvents()

        sAPIKey = txtAPIKey.Text

        'Get JSON data from API
        Call GetJSONData("user", "", "", False)

        'Data looks good, check for player name and ID
        sPlayerID = GetValue("player_id", True)
        sName = GetValue("name", True)
        sFactionID = GetValue("faction_id", True)

        Call UpdateBlankFeildUserID()

        'Everything looks good, it's on like Donkey Kong
        'Save settings
        If chkRememberKey.Checked Then My.Settings.TornAPIKey = sAPIKey
        My.Settings.AutoLaunch = chkAutoLaunch.Checked
        My.Settings.Save()

        Call CheckExpiration()
        'Call DoVerifyUsage() 'Omitted due to virus warnings, but should be working

        Application.DoEvents()
        frmMain.Show()
        Exit Sub
GoLaunchError:
        Call DoErrorHandler("An unexpected error occured while launching the program.", True)
    End Sub

    Private Sub lblByMcNeo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblByMcNeo.Click
        Process.Start("https://www.torn.com/profiles.php?XID=864688")
    End Sub
End Class
