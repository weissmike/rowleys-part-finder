'''//////////////////////////////////////////////////////////////////////////
'
' ePeTest Application
' .NET Sample Code for ePartExpert
' Copyright 2011 by Epicor Software Inc. = All Rights Reserved
'
'''//////////////////////////////////////////////////////////////////////////

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ePeTest
    '#Pragma warning disable 1591
    Public Enum ParseStatus
        NoStatus = -4
        BadType = -3
        RsFound = -2
        BadGs = -1
        Success = 0
        EndResponse = 1
    End Enum
    '#Pragma warning restore 1591

    ''' <summary>
    ''' This class is used to extract fields from a csAPI response string.
    ''' String is separated by GS characters to distinguish fields and RS characters to distinguish records
    ''' It is initialized with the full response string and repeated calls are made to extract the fields.
    ''' </summary>
    Public Class ResponseString
        ' This is the string with the full csAPI response
        Private response As String

        ' Current position of the extraction
        Private position As Int32

        ''' <summary>
        ''' Constructor with response string as input
        ''' </summary>
        ''' <param name="resp"></param> 
        Public Sub New(resp As String)
            response = resp
            position = 0
        End Sub

        ''' <summary>
        ''' Extract all text
        ''' </summary>
        ''' <param name="text">Returned text</param>
        ''' <returns>ParseStatus</returns>
        Public Function GetAllText(ByRef text As String) As ParseStatus
            Dim ret As ParseStatus = ParseStatus.Success
            Dim index As Integer

            ' Assign return string to default value
            text = ""
            text = response

            ' Check for ETB
            If response.IndexOf(ChrW(23), position, 1) = -1 Then
                ' Check for RS character at start which is unexpected
                index = response.IndexOf(ChrW(30), position, 1)

                ' RS not found so continue
                If index = -1 Then
                    ' Find next GS starting at 'position'
                    index = response.IndexOf(ChrW(29), position)

                    ' GS was found so extract field
                    If index > -1 Then
                        ' Get the string
                        'text = response.Substring(position, index - position)

                        ' Update position
                        position = index + 1
                    Else

                        ' GS not found    
                        ret = ParseStatus.BadGs
                    End If
                Else

                    ' RS was found
                    ret = ParseStatus.RsFound
                End If
            Else
                ret = ParseStatus.EndResponse
            End If

            Return ret
        End Function

        ''' <summary>
        ''' Extract a text field and return it in 'text' parameter
        ''' </summary>
        ''' <param name="text">Returned text</param>
        ''' <returns>ParseStatus</returns>
        Public Function GetTextField(ByRef text As String) As ParseStatus
            Dim ret As ParseStatus = ParseStatus.Success
            Dim index As Integer

            ' Assign return string to default value
            text = ""

            ' Check for ETB
            If response.IndexOf(ChrW(23), position, 1) = -1 Then
                ' Check for RS character at start which is unexpected
                index = response.IndexOf(ChrW(30), position, 1)

                ' RS not found so continue
                If index = -1 Then
                    ' Find next GS starting at 'position'
                    index = response.IndexOf(ChrW(29), position)

                    ' GS was found so extract field
                    If index > -1 Then
                        ' Get the string
                        text = response.Substring(position, index - position)

                        ' Update position
                        position = index + 1
                    Else

                        ' GS not found    
                        ret = ParseStatus.BadGs
                    End If
                Else

                    ' RS was found
                    ret = ParseStatus.RsFound
                End If
            Else
                ret = ParseStatus.EndResponse
            End If

            Return ret
        End Function

        ''' <summary>
        ''' Extract a number field and return it in 'number' parameter
        ''' </summary>
        ''' <param name="number">Returned number</param>
        ''' <returns>ParseStatus</returns>
        Public Function GetNumberField(ByRef number As Integer) As ParseStatus
            Dim ret As ParseStatus = ParseStatus.Success
            Dim index As Integer
            Dim text As String

            ' Assign return number to default value
            number = 0

            ' Check for ETB
            If response.IndexOf(ChrW(23), position, 1) = -1 Then
                ' Check for RS character at start which is unexpected
                index = response.IndexOf(ChrW(30), position, 1)

                ' RS not found so continue
                If index = -1 Then
                    ' Find next GS starting at 'position'
                    index = response.IndexOf(ChrW(29), position)

                    ' GS was found so extract field
                    If index > -1 Then
                        ' Get the string
                        text = response.Substring(position, index - position)

                        ' Determine if this is number or white space
                        If [Char].IsNumber(text, 0) Then
                            ' Convert it to a number
                            number = Convert.ToInt32(text)

                            ' Update position
                            position = index + 1
                        ElseIf [Char].IsWhiteSpace(text, 0) Then
                            number = 0

                            ' Update position
                            position = index + 1
                        Else
                            ret = ParseStatus.BadType
                        End If
                    Else

                        ' GS not found    
                        ret = ParseStatus.BadGs
                    End If
                Else
                    ' RS was found
                    ret = ParseStatus.RsFound
                End If
            Else
                ret = ParseStatus.EndResponse
            End If

            Return ret
        End Function

        ''' <summary>
        ''' Extract a long number field and return it in 'number' parameter
        ''' </summary>
        ''' <param name="number">Returned number</param>
        ''' <returns>ParseStatus</returns>
        Public Function GetLongNumberField(ByRef number As Long) As ParseStatus
            Dim ret As ParseStatus = ParseStatus.Success
            Dim index As Integer
            Dim text As String

            ' Assign return number to default value
            number = 0

            ' Check for ETB
            If response.IndexOf(ChrW(23), position, 1) = -1 Then
                ' Check for RS character at start which is unexpected
                index = response.IndexOf(ChrW(30), position, 1)

                ' RS not found so continue
                If index = -1 Then
                    ' Find next GS starting at 'position'
                    index = response.IndexOf(ChrW(29), position)

                    ' GS was found so extract field
                    If index > -1 Then
                        ' Get the string
                        text = response.Substring(position, index - position)

                        ' Determine if this is number or white space
                        If [Char].IsNumber(text, 0) Then
                            ' Convert it to a number
                            number = Convert.ToInt64(text)

                            ' Update position
                            position = index + 1
                        ElseIf [Char].IsWhiteSpace(text, 0) Then
                            number = 0

                            ' Update position
                            position = index + 1
                        Else
                            ret = ParseStatus.BadType
                        End If
                    Else

                        ' GS not found    
                        ret = ParseStatus.BadGs
                    End If
                Else
                    ' RS was found
                    ret = ParseStatus.RsFound
                End If
            Else
                ret = ParseStatus.EndResponse
            End If

            Return ret
        End Function

        ''' <summary>
        ''' Extract a decimal field and return it in 'decnumber' parameter
        ''' </summary>
        ''' <param name="decnumber">Returned number</param>
        ''' <returns>ParseStatus</returns>
        Public Function GetDecimalField(ByRef decnumber As Decimal) As ParseStatus
            Dim ret As ParseStatus = ParseStatus.Success
            Dim index As Integer
            Dim text As String

            ' Assign return number to default value
            decnumber = 0D

            ' Check for ETB
            If response.IndexOf(ChrW(23), position, 1) = -1 Then
                ' Check for RS character at start which is unexpected
                index = response.IndexOf(ChrW(30), position, 1)

                ' RS not found so continue
                If index = -1 Then
                    ' Find next GS starting at 'position'
                    index = response.IndexOf(ChrW(29), position)

                    ' GS was found so extract field
                    If index > -1 Then
                        ' Get the string
                        text = response.Substring(position, index - position)

                        ' Determine if this is number or white space
                        If ([Char].IsNumber(text, 0)) OrElse ([Char].IsWhiteSpace(text, 0)) Then
                            ' Convert it to a number
                            decnumber = Convert.ToDecimal(text)

                            ' Update position
                            position = index + 1
                        Else
                            ret = ParseStatus.BadType
                        End If
                    Else

                        ' GS not found    
                        ret = ParseStatus.BadGs
                    End If
                Else

                    ' RS was found
                    ret = ParseStatus.RsFound
                End If
            Else
                ret = ParseStatus.EndResponse
            End If

            Return ret
        End Function

        ''' <summary>
        ''' Find the next RS and advance the pointer just past it
        ''' </summary>
        ''' <returns>RS found or not</returns>
        Public Function GetEndOfRecord() As Boolean
            Dim success As Boolean = False

            ' Look for RS starting at 'position'
            Dim index As Integer = response.IndexOf(ChrW(30), position)

            ' RS found
            If index > -1 Then
                ' Advance the pointer
                position = index + 1
                success = True
            End If

            Return success
        End Function
    End Class
End Namespace
