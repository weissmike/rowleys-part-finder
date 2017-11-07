Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Script.Serialization
Imports System.ServiceModel.Activation
Imports System.ServiceModel.Web
Imports System.Web.Script.Services
Imports ePeTest

<ServiceContract()> _
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class csApiService

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetYears() As String
        Dim years As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim vehicleParamString As New ParameterString()
        Dim vehicleResponseString As New ResponseString("")
        Dim yearText As [String] = ""
        Dim status As ParseStatus

        vehicleParamString.AddString("")
        vehicleParamString.EndRecord()
        vehicleParamString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "VY", vehicleParamString, vehicleResponseString, True)

        If [error] = csapiError.csapierrSuccess Then
            Do
                Do
                    status = vehicleResponseString.GetTextField(yearText)
                    If (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse) Then
                        vehicleResponseString.GetEndOfRecord()
                        years.Add(yearText)
                    End If

                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                vehicleResponseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(years))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetMakes(ByVal year As String) As String
        Dim makes As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim vehicleParamString As New ParameterString()
        Dim vehicleResponseString As New ResponseString("")
        Dim makeName As [String] = ""
        Dim makeCode As [String] = ""
        Dim status As ParseStatus

        vehicleParamString.AddString(year)
        vehicleParamString.EndRecord()
        vehicleParamString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "VM", vehicleParamString, vehicleResponseString, True)

        If [error] = csapiError.csapierrSuccess Then

            ' Take out the leading 'makeDetailsAvail'
            vehicleResponseString.GetTextField(makeName)
            vehicleResponseString.GetEndOfRecord()

            Do
                Do
                    status = vehicleResponseString.GetTextField(makeName)
                    If (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse) Then
                        vehicleResponseString.GetTextField(makeCode)
                        vehicleResponseString.GetEndOfRecord()

                        makes.Add(New With { _
                             Key .Code = makeCode, _
                             Key .Name = makeName _
                            })
                    End If
                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                vehicleResponseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(makes))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetModels(ByVal year As String, ByVal makeCode As String) As String
        Dim models As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim vehicleParamString As New ParameterString()
        Dim vehicleResponseString As New ResponseString("")
        Dim modelName As [String] = ""
        Dim modelCode As [String] = ""
        Dim status As ParseStatus

        vehicleParamString.AddString(year)
        vehicleParamString.AddNumber(makeCode)
        vehicleParamString.AddNumber(0)
        vehicleParamString.EndRecord()
        vehicleParamString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "Vm", vehicleParamString, vehicleResponseString, True)

        If [error] = csapiError.csapierrSuccess Then

            ' Take out the leading 'makeDetailsAvail'
            vehicleResponseString.GetTextField(modelName)
            vehicleResponseString.GetEndOfRecord()

            Do
                Do
                    status = vehicleResponseString.GetTextField(modelName)
                    If (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse) Then
                        vehicleResponseString.GetTextField(modelCode)
                        vehicleResponseString.GetEndOfRecord()

                        models.Add(New With { _
                             Key .Code = modelCode, _
                             Key .Name = modelName _
                            })
                    End If
                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                vehicleResponseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(models))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetEngines(ByVal year As String, ByVal makeCode As String, ByVal modelCode As String) As String
        Dim engines As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim vehicleParamString As New ParameterString()
        Dim vehicleResponseString As New ResponseString("")
        Dim engineName As [String] = ""
        Dim engineCode As [String] = ""
        Dim status As ParseStatus

        vehicleParamString.AddString(year)
        vehicleParamString.AddNumber(makeCode)
        vehicleParamString.AddNumber(modelCode)
        vehicleParamString.AddNumber(0)
        vehicleParamString.EndRecord()
        vehicleParamString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "VE", vehicleParamString, vehicleResponseString, True)

        If [error] = csapiError.csapierrSuccess Then

            ' Take out the leading 'makeDetailsAvail'
            vehicleResponseString.GetTextField(engineName)
            vehicleResponseString.GetEndOfRecord()

            Do
                Do
                    status = vehicleResponseString.GetTextField(engineName)
                    If (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse) Then
                        vehicleResponseString.GetTextField(engineCode)
                        vehicleResponseString.GetEndOfRecord()

                        engines.Add(New With { _
                             Key .Code = engineCode, _
                             Key .Name = engineName _
                            })
                    End If
                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                vehicleResponseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(engines))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetTireSizes(ByVal year As String, ByVal makeCode As String, ByVal modelCode As String, ByVal engineCode As String) As String
        Dim tireSizes As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim vehicleParamString As New ParameterString()
        Dim vehicleResponseString As New ResponseString("")
        Dim tireSize As [String] = ""
        Dim tireCode As [String] = ""
        Dim status As ParseStatus

        vehicleParamString.AddString(year)
        vehicleParamString.AddNumber(makeCode)
        vehicleParamString.AddNumber(modelCode)
        vehicleParamString.AddNumber(0)
        vehicleParamString.AddNumber(engineCode)
        vehicleParamString.EndRecord()
        vehicleParamString.EndRecord()
        vehicleParamString.EndRecord()
        vehicleParamString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "P7", vehicleParamString, vehicleResponseString, True)

        If [error] = csapiError.csapierrSuccess Then

            ' Take out the leading 'PD'
            vehicleResponseString.GetTextField(tireSize)
            vehicleResponseString.GetEndOfRecord()

            Do
                Do
                    status = vehicleResponseString.GetTextField(tireCode)
                    If tireCode = "TS" Then
                        status = vehicleResponseString.GetTextField(tireSize)
                        If (tireSizes.Contains(tireSize) = False) Then
                            tireSizes.Add(tireSize)
                        End If
                    End If
                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                vehicleResponseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(tireSizes))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetCategories() As String
        Dim categories As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim paramString As New ParameterString()
        Dim responseString As New ResponseString("")
        Dim categoryCode As [String] = ""
        Dim categoryText As [String] = ""
        Dim status As ParseStatus

        paramString.EndRecord()
        paramString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "CT", paramString, responseString, True)

        If [error] = csapiError.csapierrSuccess Then

            Do
                Do
                    status = responseString.GetTextField(categoryText)
                    If (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse) Then
                        responseString.GetTextField(categoryCode)
                        responseString.GetEndOfRecord()

                        categories.Add(New With { _
                             Key .Code = categoryCode, _
                             Key .Text = categoryText _
                            })
                    End If
                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                responseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(categories))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetPartGroups(ByVal categoryCode As String) As String
        Dim groups As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim paramString As New ParameterString()
        Dim responseString As New ResponseString("")
        Dim groupsCode As [String] = ""
        Dim groupsText As [String] = ""
        Dim status As ParseStatus

        paramString.AddNumber(categoryCode)
        paramString.EndRecord()
        paramString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "PG", paramString, responseString, True)

        If [error] = csapiError.csapierrSuccess Then

            Do
                Do
                    status = responseString.GetTextField(groupsText)
                    If (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse) Then
                        responseString.GetTextField(groupsCode)
                        responseString.GetEndOfRecord()

                        groups.Add(New With { _
                            Key .Code = groupsCode, _
                            Key .Text = groupsText _
                        })
                    End If
                Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                responseString.GetEndOfRecord()
            Loop While status <> ParseStatus.EndResponse

        End If
        Return (New JavaScriptSerializer().Serialize(groups))
    End Function

    <OperationContract()> _
    <WebGet(ResponseFormat:=WebMessageFormat.Json)> _
    Public Function GetPartNumbers(ByVal year As String, ByVal makeCode As String, ByVal modelCode As String, ByVal engineCode As String, ByVal groupCode As String, ByVal conditions As String) As String
        Dim parts As New List(Of Object)()
        Dim specificConditions As New List(Of Object)()
        Dim specifiedOptions As New List(Of Object)()

        csAPIClient.Initialize()

        Dim [error] As csapiError
        Dim paramString As New ParameterString()
        Dim responseString As New ResponseString("")
        Dim partCode As [String] = ""
        Dim partManufacturer As [String] = ""
        Dim partNumber As [String] = ""
        Dim specificConditionCode As [String] = ""
        Dim specificConditionString As [String] = ""
        Dim status As ParseStatus
        Dim conditionsArr() As String = Split(conditions,",")
        Dim conditionResp() As String

        paramString.AddString(year)
        paramString.AddNumber(makeCode)
        paramString.AddNumber(modelCode)
        paramString.AddNumber(0)
        paramString.AddNumber(engineCode)
        paramString.AddNumber(groupCode)
        paramString.EndRecord()
        paramString.EndRecord()
        paramString.EndRecord()
        For i As Integer = 0 To conditionsArr.Length - 1
            conditionResp = Split(conditionsArr(i),"|")
            For j As Integer = 0 To conditionResp.Length - 1
                paramString.AddString(conditionResp(j))
            Next
        Next         
        paramString.EndRecord()
        paramString.EndBlock()

        ' Make the csAPI call
        [error] = csAPIClient.CallcsAPI("RWS", "P1", paramString, responseString, True)

        If [error] = csapiError.csapierrSuccess Then

            ' Take out the leading 'PD'
            responseString.GetTextField(partCode)
            responseString.GetEndOfRecord()

            'status = responseString.GetAllText(partCode)
            'return partCode

            If partCode = "SR" Then
            
                Do
                    status = responseString.GetTextField(specificConditionCode)
                    Do
                        If specificConditionCode = "BO" Then
                            status = responseString.GetTextField(specificConditionString)
                            status = responseString.GetTextField(specificConditionCode)
                            specifiedOptions.Add(New With { _
                                Key .Option = "Yes", _
                                Key .Value = "Y" _
                            })
                            specifiedOptions.Add(New With { _
                                Key .Option = "No", _
                                Key .Value = "N" _
                            })
                            specifiedOptions.Add(New With { _
                                Key .Option = "Don't Know", _
                                Key .Value = "D" _
                            })
                            
                            If (specificConditionCode <> "") And (specificConditionString <> "") Then
                                specificConditions.Add(New With { _
                                    Key .Code = specificConditionCode, _
                                    Key .String = specificConditionString, _
                                    Key .Options = specifiedOptions _
                                })
        
                                specificConditionCode = ""
                                specificConditionString = ""
                                specifiedOptions = New List(Of Object)()
                            End If
                        Else
                            status = responseString.GetTextField(specificConditionString)
                            status = responseString.GetTextField(specificConditionCode)
                            
                             If (specificConditionCode <> "") And (specificConditionString <> "") Then
                                specifiedOptions.Add(New With { _
                                    Key .Option = specificConditionString, _
                                    Key .Value = specificConditionCode _
                                })
        
                                specificConditionCode = ""
                                specificConditionString = ""
                            End If
                       End If
                    Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)

                    If (specifiedOptions IsNot Nothing And specifiedOptions.Count > 0) Then
                        specificConditions.Add(New With { _
                            Key .Code = "MC", _
                            Key .String = "QUESTION", _
                            Key .Options = specifiedOptions _
                        })

                        specifiedOptions = New List(Of Object)()
                    End If
    
                    responseString.GetEndOfRecord()
                Loop While status <> ParseStatus.EndResponse
                
                Return (New JavaScriptSerializer().Serialize(specificConditions))
            Else

                Do
                    Do
                        status = responseString.GetTextField(partCode)
                        If partCode = "CC" Then
                            status = responseString.GetTextField(partManufacturer)
                        End If
                        If partCode = "PN" Then
                            status = responseString.GetTextField(partNumber)
                        End If
                        If partCode = "XN" Then
                            status = responseString.GetTextField(partNumber)
                        End If
                        
                        If (partManufacturer <> "") And (partNumber <> "") Then
                            parts.Add(New With { _
                                 Key .Manufacturer = partManufacturer, _
                                 Key .Number = partNumber _
                            })
    
                            partManufacturer = ""
                            partNumber = ""
                        End If
                    Loop While (status <> ParseStatus.RsFound) AndAlso (status <> ParseStatus.EndResponse)
    
                    responseString.GetEndOfRecord()
                Loop While status <> ParseStatus.EndResponse
            
            End If
            
            Return (New JavaScriptSerializer().Serialize(parts))
        End If
    End Function

End Class
