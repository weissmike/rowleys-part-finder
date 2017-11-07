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
    ''' <summary>
    ''' Class to form parameter inputs for csAPI calls
    ''' </summary>
    ''' <remarks>LaserCat4 Automotive Catalog Program, Copyright © 2009 Activant Solutions Inc.</remarks>
    Public Class ParameterString
        ' String that will become formatted parameters
        Private params As String

        ' RS character
        Private Const Rs As String = ChrW(30)

        ' GS character
        Private Const Gs As String = ChrW(29)

        ' ETB character
        Private Const Etb As String = ChrW(23)

        ''' <summary>
        ''' Get the formatted return string
        ''' </summary>
        ''' <returns></returns>
        Public Function Parameters() As String
            Return params
        End Function

        ''' <summary>
        ''' Add a string to the parameters 
        ''' </summary>
        ''' <param name="param"></param>
        Public Sub AddString(param As String)
            params += param

            ' Add GS
            params += Gs
        End Sub

        ''' <summary>
        ''' Add a number to the parameters 
        ''' </summary>
        ''' <param name="param"></param>
        Public Sub AddNumber(param As Integer)
            params += param.ToString()

            ' Add GS
            params += Gs
        End Sub

        ''' <summary>
        ''' Add a long number to the parameters 
        ''' </summary>
        ''' <param name="param"></param>
        Public Sub AddLongNumber(param As Long)
            params += param.ToString()

            ' Add GS
            params += Gs
        End Sub

        ''' <summary>
        ''' End a record 
        ''' </summary>
        Public Sub EndRecord()
            ' Add RS
            params += Rs
        End Sub

        ''' <summary>
        ''' End the string 
        ''' </summary>
        Public Sub EndBlock()
            params += Etb
        End Sub

        ''' <summary>
        ''' Clear the string
        ''' </summary>
        Public Sub Clear()
            params = ""
        End Sub
    End Class
End Namespace
