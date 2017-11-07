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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace ePeTest
	''' <summary>
	''' Class to talk to the WebCat catalog engine through NetCatService
	''' Depends upon a Reference to NETCATSERVICE
	''' </summary>
	Public NotInheritable Class csAPIClient
		Private Sub New()
		End Sub
		Private Shared csAPIConnection As NETCATSERVICELib.CatalogServer
		Private Shared errorcode As csapiError = 0
		Private Shared errormsg As String = ""
		Shared bInitialized As Boolean = False

		''' <summary>
		''' Make the initial connection
		''' </summary>
		''' <returns></returns>
		Public Shared Function Initialize() As Boolean
			Dim ret As Boolean = False
			Dim message As [String]

			' If we are not already initialized
			If Not bInitialized Then
				' Make the connection
				Try
					csAPIConnection = New NETCATSERVICELib.CatalogServer()
				Catch ce As COMException
					message = ce.Message
                    'MessageBox.Show(message)
				End Try
				If csAPIConnection IsNot Nothing Then
					bInitialized = True
					ret = True
				End If
			End If

			Return ret
		End Function

		''' <summary>
		''' Make a call to the csAPI
		''' </summary>
		''' <param name="supplier">Supplier name</param>
		''' <param name="funcstr">Function ID</param>
		''' <param name="paramstr">Parameters</param>
		''' <param name="response">Response String</param>
		''' <param name="displayError">Display error or not</param>
		''' <returns>csapiError</returns>
		Public Shared Function CallcsAPI(supplier As String, funcstr As String, paramstr As ParameterString, ByRef response As ResponseString, displayError As Boolean) As csapiError
			Dim respstr As String
			response = Nothing

			If Not bInitialized Then
				Initialize()
			End If

			If bInitialized Then
				errorcode = DirectCast(csAPIConnection.csapiFunction(funcstr, supplier, paramstr.Parameters(), respstr), csapiError)

				If errorcode = 0 Then
					response = New ResponseString(respstr)
					errormsg = ""
				Else
					errormsg = respstr
                    Throw New CatalogServerException(respstr, errorcode, supplier, funcstr, paramstr.Parameters())
				End If
			End If

			Return errorcode
		End Function
	End Class
End Namespace
