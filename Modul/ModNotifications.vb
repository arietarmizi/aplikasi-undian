

Imports MySql.Data.MySqlClient
Module ModNotifications

    Dim noMonth As String
    Public isNOtify As Boolean
    Private Sub IntMonth()

        noMonth = ""
        If Date.Now.ToString("MMMM") = "January" Then
            noMonth = "01"
        ElseIf Date.Now.ToString("MMMM") = "February" Then
            noMonth = "02"
        ElseIf Date.Now.ToString("MMMM") = "March" Then
            noMonth = "03"
        ElseIf Date.Now.ToString("MMMM") = "April" Then
            noMonth = "04"
        ElseIf Date.Now.ToString("MMMM") = "May" Then
            noMonth = "05"
        ElseIf Date.Now.ToString("MMMM") = "June" Then
            noMonth = "06"
        ElseIf Date.Now.ToString("MMMM") = "July" Then
            noMonth = "07"
        ElseIf Date.Now.ToString("MMMM") = "August" Then
            noMonth = "08"
        ElseIf Date.Now.ToString("MMMM") = "September" Then
            noMonth = "09"
        ElseIf Date.Now.ToString("MMMM") = "October" Then
            noMonth = "10"
        ElseIf Date.Now.ToString("MMMM") = "November" Then
            noMonth = "11"
        ElseIf Date.Now.ToString("MMMM") = "December" Then
            noMonth = "12"
        End If

    End Sub


    
End Module
