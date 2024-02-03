Imports System.Text, System.Runtime.InteropServices

Public Class IniFile
    Public path As String
    Public Sub New(ByVal INIPath As String)
        path = INIPath
    End Sub

    <DllImport("kernel32", CharSet:=CharSet.None, ExactSpelling:=False)>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    Public Function ReadValue(ByVal Section As String, ByVal Key As String) As String
        Dim temp As StringBuilder = New StringBuilder(255)
        GetPrivateProfileString(Section, Key, "", temp, 255, path)
        Return temp.ToString()
    End Function
    <DllImport("kernel32", CharSet:=CharSet.None, ExactSpelling:=False)>
    Private Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
    End Function

    Public Sub WriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, path)
    End Sub
End Class