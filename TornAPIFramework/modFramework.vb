Module modFramework
    Public sFrameworkVersion As String = "3.0.0.3"
    Public bUseHTTPS As Boolean = True
    Public iTimeoutLimit As Integer = 10000
    Public bDebug As Boolean = False
    Public bResetWindow As Boolean = False
    Public bIsExpired As Boolean = False
    Public sUserAgent As String = Trim(Application.ProductName.Replace(" ", ""))
    Public bIsInError As Boolean = False
    Public bCheckInternet As Boolean = False

    Public Sub CheckExpiration()
        If lEpochExpiration = 0 Then Exit Sub

        Call GetJSONData("torn", "stats", sPlayerID, False)
        If CLng(GetValue("torn/" & sPlayerID & "/timestamp")) > lEpochExpiration Then
            bIsExpired = True
            Call DoErrorHandler("You are not authorized to use this program.", True)
        End If
    End Sub

    Public Sub DoVerifyUsage() 'Omitted due to a/v warnings, but should be working
        '        On Error GoTo lblDoVerifyUsageError

        '        'Check version
        '        Dim sResult As String = New System.Net.WebClient().DownloadString("http://torn.daveeverett.net/APIappVerifyUsage/" & sUserAgent & "/version.dat")
        '        If InStr(sResult, Application.ProductVersion) <= 0 Then Call DoErrorHandler("This is an invalid or outdated version.  Please seek an update.", True)

        '        'Check blacklist
        '        sResult = New System.Net.WebClient().DownloadString("http://torn.daveeverett.net/APIappVerifyUsage/" & sUserAgent & "/blacklist.dat")
        '        If InStr(sResult, sPlayerID) > 0 Then Call DoErrorHandler("You are blacklisted from using this program.", True)
        '        If InStr(sResult, sFactionID) > 0 Then Call DoErrorHandler("Your faction is blacklisted from using this program.", True)

        '        'Check player id
        '        sResult = New System.Net.WebClient().DownloadString("http://torn.daveeverett.net/APIappVerifyUsage/" & sUserAgent & "/players.dat")
        '        If (InStr(sResult, "ALLPLAYERS") <= 0 And InStr(sResult, sPlayerID) <= 0) Then Call DoErrorHandler("You are not authorized to use this program.", True)

        '        'Check faction id
        '        sResult = New System.Net.WebClient().DownloadString("http://torn.daveeverett.net/APIappVerifyUsage/" & sUserAgent & "/factions.dat")
        '        If (InStr(sResult, "ALLFACTIONS") <= 0 And InStr(sResult, sFactionID) <= 0) Then Call DoErrorHandler("Your faction is not authorized to use this program.", True)

        '        Exit Sub

        'lblDoVerifyUsageError:
        '        Call DoErrorHandler("Failed to authenticate app.", True)
    End Sub

    Public Sub DoErrorHandler(ByVal sErrorMessage As String, ByVal bKill As Boolean)
        Dim sMessage As String
        Dim tMessageType
        Dim tButtons
        Dim r

        If bIsInError Then Exit Sub
        bIsInError = True

        If bDebug And Not bIsExpired And frmMain.bLoaded Then bKill = False

        sMessage = sErrorMessage & vbCrLf & vbCrLf
        sMessage &= "Debug info follows..." & vbCrLf
        sMessage &= "Date and time: " & DateTime.Now.ToString("G") & vbCrLf
        sMessage &= "Application name: " & Application.ProductName.Replace("_", " ") & vbCrLf
        sMessage &= "Application version: " & Application.ProductVersion & vbCrLf
        sMessage &= "Framework version: " & sFrameworkVersion & vbCrLf
        sMessage &= "Use HTTPS: " & bUseHTTPS & vbCrLf
        sMessage &= "User-Agent: " & sUserAgent & vbCrLf & vbCrLf
        sMessage &= "The above error has occured"
        If bKill Then sMessage &= " and " & Application.ProductName.Replace("_", " ") & " must now close"
        sMessage &= ".  This error has not been logged in any way.  Please take note of the above information and seek support if needed."
        If bDebug And Not bIsExpired And frmMain.bLoaded Then sMessage &= vbCrLf & vbCrLf & "NOTICE: This program is running in debug mode so it will not be closed.  However, depending on the error, this may cause more errors.  To close the program click NO, or to continue running, click YES."

        tMessageType = vbExclamation
        If bKill Then tMessageType = vbCritical

        tButtons = vbOKOnly
        If bDebug And Not bIsExpired And frmMain.bLoaded Then tButtons = vbYesNo

        r = MsgBox(sMessage, tMessageType + tButtons, "Error")

        bIsInError = False
        If bIsExpired Then End

        If r = vbNo Then End

        If bKill Then End
    End Sub

    Public Sub PromptForgetAPI()
        Dim r

        r = MsgBox("Forget your Torn API Key?", vbYesNo + vbQuestion, "Forget Torn API Key")

        If r = vbNo Then Exit Sub

        frmStart.chkRememberKey.Checked = False
        frmStart.chkAutoLaunch.Checked = False
        frmStart.chkAutoLaunch.Enabled = False

        My.Settings.AutoLaunch = False
        My.Settings.TornAPIKey = ""
        My.Settings.Save()
    End Sub
End Module
