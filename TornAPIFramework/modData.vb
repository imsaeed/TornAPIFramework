Module modData
    Public mField(1) As String
    Public mValue(1) As String

    Public sAPIKey As String = ""
    Public sPlayerID As String = ""
    Public sName As String = ""
    Public sFactionID As String = ""

    Public Sub DoDataStartup()
        mField = {"x"}
        mValue = {"x"}
    End Sub

    Public Function GetValue(ByVal sField As String, Optional bJustGetIt As Boolean = False, Optional bGetLast As Boolean = False, Optional bGetSecondToLast As Boolean = False) As String
        'Returns the value for the requested field, or "" if not found
        'If bJustGetIt is true, then returns the first thing that even remotely matches sField
        Dim iTimeout As Integer = 0
        Dim sCheckLastField As String = ""
        Dim sCheckSecondLastField As String = ""

        For i = 0 To mField.GetUpperBound(0)
            iTimeout += 1
            If iTimeout > iTimeoutLimit Then Call DoErrorHandler("An infinite loop or timeout has been detected while attempting to read the local data matrix.", True)

            If bJustGetIt And InStr(mField(i), sField) Then Return mValue(i).Replace("*&^%", """")
            If (bGetLast Or bGetSecondToLast) And InStr(mField(i), sField) Then
                sCheckSecondLastField = sCheckLastField
                sCheckLastField = mValue(i).Replace("*&^%", """")
            End If
            If mField(i) = sField Then Return mValue(i).Replace("*&^%", """")
        Next

        If bGetLast Then Return sCheckLastField
        If bGetSecondToLast Then Return sCheckSecondLastField

        Return ""
    End Function

    Public Sub UpdateBlankFeildUserID()
        Dim sHolder As String

        For i = 0 To mField.GetUpperBound(0)
            If Left(mField(i), 6) = "user//" Then
                sHolder = mField(i).Substring(6, mField(i).Length - 6)
                mField(i) = "user/" & sPlayerID & "/" & sHolder
            End If
        Next
    End Sub

    Public Sub SetValue(ByVal sField As String, ByVal sValue As String)
        'Check if field exists.  If so, update it, and exit
        For i = 0 To mField.GetUpperBound(0)
            If mField(i) = sField Then
                mValue(i) = sValue
                Exit Sub
            End If
        Next

        'Still here?  Field must not have been updated because it doesn't exist.  So, add it.
        Dim j = mField.GetUpperBound(0) + 1
        ReDim Preserve mField(j)
        ReDim Preserve mValue(j)
        mField(j) = sField
        mValue(j) = sValue
    End Sub

    Public Sub GetJSONData(ByVal sCategory As String, ByVal sSelections As String, ByVal sUseID As String, Optional bBackground As Boolean = True)
        If bBackground Then
            frmMain.sBGWCategory = sCategory
            frmMain.sBGWSelections = sSelections
            frmMain.sBGWUseID = sUseID
            Call frmMain.GoDoBackgroundJSON(sCategory, sSelections, sUseID)
        Else
            Call GoGetJSONData(sCategory, sSelections, sUseID, True)
        End If
    End Sub

    Public Sub GoGetJSONData(ByVal sCategory As String, ByVal sSelections As String, ByVal sUseID As String, bNeedIt As Boolean)
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        frmMain.tmrUpdate.Enabled = False

        On Error GoTo GetJSONDataError 'Second error handler about 20 lines down

        Dim sQueryPath As String

        sQueryPath = "api.torn.com/" & sCategory & "/" & sUseID & "?selections=" & sSelections & "&key=" & sAPIKey
        If bUseHTTPS Then
            sQueryPath = "https://" & sQueryPath
        Else
            sQueryPath = "http://" & sQueryPath
        End If
        'If sUserAgent <> "" Then sQueryPath &= "&useragent=" & sUserAgent

        Dim oWebClient As New System.Net.WebClient
        Dim sJSONResult As String = ""
        oWebClient.Headers("UserAgent") = sUserAgent
        oWebClient.Headers("User-Agent") = sUserAgent

        On Error Resume Next
        For iTestResult = 1 To 10
            sJSONResult = oWebClient.DownloadString(sQueryPath)
            If sJSONResult <> "" Then Exit For
            Threading.Thread.Sleep(500)
            If iTestResult = 10 Then
                If bNeedIt Then
                    Call DoErrorHandler("Failed to reach the Torn servers.  Check your internet connection.", True)
                Else
                    bCheckInternet = True
                    Exit Sub
                End If
            End If
        Next
        bCheckInternet = False
        frmMain.tmrUpdate.Enabled = True
        On Error GoTo GetJSONDataError

        Dim sParse As String = ""
        Dim iFieldStart As Integer = 0
        Dim iFieldEnd As Integer = 0
        Dim iValueStart As Integer = 0
        Dim iValueEnd As Integer = 0
        Dim sField As String = ""
        Dim sFieldHeader As String = ""
        Dim sFieldSubHeader As String = ""
        Dim sFieldMicroHeader As String = ""
        Dim iFieldHeaderClear As Integer = 0
        Dim sValue As String = ""
        Dim sValueList As String = ""
        Dim iTimeout As Integer = 0

        'Check for Torn API errors
        If sJSONResult.Substring(0, 17) = "{""error"":{""code"":" Then
            Select Case sJSONResult.Substring(17, 1)
                Case "1"
                    Call DoErrorHandler("Torn API error 1: Key is empty.  Please enter your Torn API Key.", True)
                Case "2"
                    My.Settings.AutoLaunch = False
                    My.Settings.TornAPIKey = ""
                    My.Settings.Save()
                    Call DoErrorHandler("Torn API error 2: Incorrect key.  Please enter your Torn API Key.", True)
                Case "5"
                    Call DoErrorHandler("Torn API error 5: Too many requests.  Your Torn API Key has made too many requests.  Please try again in 5 minutes.", True)
                Case "8"
                    Call DoErrorHandler("Torn API error 8: IP Block.  Your IP is temporarily banned from accessing the Torn API.  Please try again in 5 minutes.", True)
                Case "9"
                    Call DoErrorHandler("Torn API error 9: API Unavailable.  The Torn API is not available to handle requests.  Please try again in 5 minutes.", True)
                Case Else
                    Call DoErrorHandler("Torn API error: An unexpected error has occured.  Please try again in 5 minutes.", True)
            End Select
            Exit Sub
        ElseIf sJSONResult.Length < 50 Then
            Call DoErrorHandler("An invalid response was received from the Torn API server.  Please try again in 5 minutes.", True)
        End If

        sParse = Trim(sJSONResult)
        sParse = sRemoveHTML(sParse)
        sParse = sParse.Replace("\""", "*&^%")

        'Parse Data
        While sParse.Length > 1
            iTimeout += 1
            If iTimeout > iTimeoutLimit Then Call DoErrorHandler("An infinite loop or timeout has been detected while parsing JSON data.", True)

            If InStr(sParse, "education_completed") > 0 And InStr(sParse, "education_completed") < 3 Then
                sParse = sParse.Substring(InStr(sParse, "]"))
            End If

            iFieldStart = InStr(sParse, """")
            iFieldEnd = InStr(sParse.Substring(iFieldStart), """") - 1

            If iFieldStart = 0 And iFieldEnd = -1 Then Exit While 'Assume end of data

            sField = sParse.Substring(iFieldStart, iFieldEnd)
            sParse = sParse.Substring(iFieldStart + iFieldEnd + 2)

            Select Case sParse.Substring(0, 1)
                Case "{"
                    If sFieldHeader = "" Then
                        sFieldHeader = sField & "/"
                    ElseIf sFieldSubHeader = "" Then
                        sFieldSubHeader = sField & "/"
                    ElseIf sFieldMicroHeader = "" Then
                        sFieldMicroHeader = sField & "/"
                    Else
                        Call DoErrorHandler("Too many nests in JSON data.", True)
                    End If

                    Continue While
                Case "["
                    sValueList = ""
                    sParse = sParse.Substring(1, sParse.Length - 1)
                    Do Until sParse.Substring(0, 1) = ","
                        iTimeout += 1
                        If iTimeout > iTimeoutLimit Then Call DoErrorHandler("An infinite loop or timeout has been detected while parsing JSON data.", True)

                        If sValueList = "" And sParse.Substring(0, 1) = "]" Then
                            sParse = sParse.Substring(1, sParse.Length - 1)
                            Exit Select
                        End If
                        If sParse.Substring(0, 1) = """" Then
                            iValueStart = 1
                            iValueEnd = InStr(sParse.Substring(iValueStart), """") - 1

                            sValueList &= sParse.Substring(iValueStart, iValueEnd) & ", "

                            sParse = sParse.Substring(iValueStart + iValueEnd + 2)

                            If sParse.Length < 3 Then Exit Do
                        Else
                            iValueStart = 0
                            iValueEnd = InStr(sParse.Substring(iValueStart), "]") - 1

                            sValueList = sParse.Substring(iValueStart, iValueEnd)

                            sParse = sParse.Substring(iValueStart + iValueEnd + 2)

                            If sParse.Length < 1 Then
                                Call SetValue(sField, sValueList)
                                Exit Sub
                            End If
                        End If
                    Loop

                    sValueList = sValueList.Substring(0, sValueList.Length - 2)

                    sParse = sParse.Substring(1, sParse.Length - 1)
                Case """"
                    iValueStart = 1
                    iValueEnd = InStr(sParse.Substring(iValueStart), """") - 1

                    sValue = sParse.Substring(iValueStart, iValueEnd)

                    On Error Resume Next
                    If InStr(sParse.Substring(iValueEnd, 2), "}") Then iFieldHeaderClear = 1
                    If InStr(sParse.Substring(iValueEnd, 3), "}") Then iFieldHeaderClear = 1
                    If InStr(sParse.Substring(iValueEnd, 4), "}") Then iFieldHeaderClear = 1
                    If InStr(sParse.Substring(iValueEnd, 5), "}") Then iFieldHeaderClear = 1
                    If InStr(sParse.Substring(iValueEnd, 3), "}}") Then iFieldHeaderClear = 2
                    If InStr(sParse.Substring(iValueEnd, 4), "}}") Then iFieldHeaderClear = 2
                    If InStr(sParse.Substring(iValueEnd, 5), "}}") Then iFieldHeaderClear = 2
                    If InStr(sParse.Substring(iValueEnd, 4), "}}}") Then iFieldHeaderClear = 3
                    If InStr(sParse.Substring(iValueEnd, 5), "}}}") Then iFieldHeaderClear = 3
                    On Error GoTo GetJSONDataError

                    sParse = sParse.Substring(iValueStart + iValueEnd + 2)
                Case Else
                    iValueStart = 0
                    iValueEnd = InStr(sParse.Substring(iValueStart), ",") - 1
                    If iValueEnd < iValueStart Then iValueEnd = InStr(sParse.Substring(iValueStart), "}") - 1

                    sValue = sParse.Substring(iValueStart, iValueEnd)

                    If sValue.Length >= 3 Then
                        If sValue.Substring(sValue.Length - 3, 3) = "}}}" Then
                            iFieldHeaderClear = 3
                            sValue = sParse.Substring(iValueStart, iValueEnd - 1)
                        End If
                    End If
                    If sValue.Length >= 2 Then
                        If sValue.Substring(sValue.Length - 2, 2) = "}}" Then
                            iFieldHeaderClear = 2
                            sValue = sParse.Substring(iValueStart, iValueEnd - 1)
                        End If
                    End If
                    If sValue.Length >= 1 Then
                        If sValue.Substring(sValue.Length - 1, 1) = "}" Then
                            iFieldHeaderClear = 1
                            sValue = sParse.Substring(iValueStart, iValueEnd - 1)
                        End If
                    End If

                    If InStr(sValue, "}") > 0 Then
                        iFieldHeaderClear += 1
                        sValue = sValue.Substring(0, sValue.Length - 1)
                    End If
                    'Yes, do it twice...
                    If InStr(sValue, "}") > 0 Then
                        iFieldHeaderClear += 1
                        sValue = sValue.Substring(0, sValue.Length - 1)
                    End If

                    sParse = sParse.Substring(iValueStart + iValueEnd + 1)
            End Select

            sField = sCategory & "/" & sUseID & "/" & sFieldHeader & sFieldSubHeader & sFieldMicroHeader & sField
            If sValueList = "" Then
                Call SetValue(sField, sValue)
            Else
                Call SetValue(sField, sValueList)
            End If

            sField = ""
            sValue = ""
            sValueList = ""
            If iFieldHeaderClear <> 0 Then
                Select Case iFieldHeaderClear
                    Case 1
                        If sFieldMicroHeader <> "" Then
                            sFieldMicroHeader = ""
                        ElseIf sFieldSubHeader <> "" Then
                            sFieldSubHeader = ""
                        Else
                            sFieldMicroHeader = ""
                            sFieldSubHeader = ""
                            sFieldHeader = ""
                        End If
                    Case 2
                        If sFieldMicroHeader <> "" Then
                            sFieldMicroHeader = ""
                            sFieldSubHeader = ""
                        ElseIf sFieldSubHeader <> "" Then
                            sFieldSubHeader = ""
                            sFieldHeader = ""
                        Else
                            sFieldMicroHeader = ""
                            sFieldSubHeader = ""
                            sFieldHeader = ""
                        End If
                    Case 3
                        sFieldMicroHeader = ""
                        sFieldSubHeader = ""
                        sFieldHeader = ""
                End Select
                iFieldHeaderClear = 0
            End If
        End While

        If sPlayerID <> "" Then
            dtTornLocal = DateTime.Parse("January 1 1970 12:00:00 am").AddSeconds(CDbl(GetValue("user/" & sPlayerID & "/server_time"))).ToLocalTime
            'If Math.Abs(lTimeComp) + 1 < Math.Abs(DateDiff(DateInterval.Second, dtTornLocal, Now)) Or Math.Abs(lTimeComp) - 1 > Math.Abs(DateDiff(DateInterval.Second, dtTornLocal, Now)) Then
            '    iCompAvgCount = 0
            '    lCompAvg = 0
            'End If
            'If iCompAvgCount < 25 Then
            '    lCompAvg += DateDiff(DateInterval.Second, dtTornLocal, Now)
            '    iCompAvgCount += 1
            'End If
            'lTimeComp = CLng(Math.Round(lCompAvg / iCompAvgCount))
            'If lTimeComp = 1 Then lTimeComp = 0
            lTimeComp = DateDiff(DateInterval.Second, dtTornLocal, Now)
            Application.DoEvents()
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
GetJSONDataError:
        Call DoErrorHandler("An unexpected error occured while parsing JSON data.", True)
    End Sub

    Public Function sRemoveHTML(sTheString As String) As String
        Dim iStart As Integer
        Dim iEnd As Integer
        Dim sRemove As String = ""

        If InStr(sTheString, "<") <= 0 Then Return sTheString

        While InStr(sTheString, "<") > 0
            If InStr(sTheString, "<") <= 0 Then Return sTheString

            iStart = sTheString.IndexOf("<")
            iEnd = sTheString.IndexOf(">")
            If iEnd < iStart Then iEnd = sTheString.Substring(iStart, sTheString.Length - iStart - 1).IndexOf(">") + iStart

            sRemove = sTheString.Substring(iStart, iEnd - iStart + 1)
            sTheString = sTheString.Replace(sRemove, "")
        End While

        Return sTheString
    End Function
End Module
