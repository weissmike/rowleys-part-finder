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

''' <summary>
''' enum representing possible Webcat errors
''' </summary>
Public Enum csapiError
    '	#Pragma warning disable 1591
	csapierrSuccess = 0
	csapierrInvalidParam = 101
	csapierrMissingParam
	csapierrFirstParam
	csapierrSecondParam
	csapierrNoRecords
	csapierrInvalidVehicle
	csapierrMclValidation
	csapierrTiffFile
	csapierrNoCategories
	csapierrNoVehicles
	csapierrNoPartDescs
	csapierrNoPartGrps
	csapierrNoPartTypes
	csapierrNoInit
	csapierrNoLaborDesc
	csapierrNoLaborGrps
	csapierrNoLaborTypes
	csapierrFuncNotAvail
	csapierrCdromIdErr
	csapierrMclCreation
	csapierrServiceIntervals
	csapierrSupplierNotSupported
	csapierrSupplierFileMissing
	csapierrMclInit
	csapierrCatGrpInit
	csapierrGrpInfoInit
	csapierrVolumeRestart
	csapierrParts
	csapierrLabor
	csapierrInterchange
	csapierrInvalidWildcard
	csapierrTooManyRecords
	csapierrPartNumberNotFound
	csapierrTireSizeNotFound
	csapierrInvalidLicense
	csapierrTypeMains
	csapierrNoPartsFound
	csapierrNoLaborFound
	csapierrPriceInit
	csapierrLaborInit
	csapierrGraphicsInit
	csapierrTiresInit
	csapierrNoGroupInfo
	csapierrTclInit
	csapierrMclUpload
	csapierrZapLoad
	csapierrMaclSave
	csapierrTires
	csapierrPlusSize
	csapierrPlusSizeAspect
	csapierrPlusSizePmetric
	csapierrServiceInit
	csapierrServices
	csapierrNoServiceIntervals
	csapierrMclEmpty
	csapierrGraphicsPath
	csapierrGraphicsFile
	csapierrMclSearch = 166
	csapierrSupplierNotInMem
	csapierrBuyers
	csapierrLaborDescBridge
	csapierrPartDescBridge
	csapierrDataExpired
	csapierrSupplierExists
	csapierrInit2
	csapierrInit3
	csapierrInit4
	csapierrInit5
	csapierrInit6
	csapierrInvalidDesc
	csapierrDataAccess
	csapierrInvalidUser
	csapierrSupplierData
	csapierrSupplierCreate
	csapierrInvalidExpiration
	csapierrDataNotFound
	csapierrNullsInFile
	csapierrRecordLimits
	csapierrEndLookups
	csapierrTooManyRatings
	csapierrVehicleNotSupported
	csapierrCoverageAdd
	csapierrInvalidVin
	csapierrVin
	csapierrMclDownload
	csapierrInvalidTireSize
	csapierrNoHpiData
	csapierrHpi
	csapierrInvalidLaserCatLicense
	csapierrCantDeleteTS
	csapierrTooManySpecs
	csapierrMclByMake
	csapierrMotorsSpecs
	csapierrSearsLabor
	csapierrVrmNotFound
	csapierrElvfFile
	csapierrInvalidLicense2
	csapierrC2cLink
	csapierrC2cFile
	csapierrC2cManufacturerConflict
	csapierrC2cLineCodeConflict
	csapierrC2CPartNumberConflict
	csapierrLocalC2c
	csapierrSearchFile
	csapierrVehicleCoverageConflict
	csapierrVehicleCoveragesMaxed
    '	#Pragma warning restore 1591
End Enum

Namespace ePeTest
	''' <summary>
	''' Exception class for server errors
	''' </summary>
	Public Class CatalogServerException
		Inherits ApplicationException
		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			Me.New("Unknown Error")
			csAPIError = csapiError.csapierrSuccess
			SupplierName = ""
			FunctionCall = ""
			Parameters = ""
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="message">Message</param>
		''' <param name="error">Error Id</param>
		''' <param name="suppliername">Supplier name</param>
		''' <param name="functioncall">Function call</param>
		''' <param name="parameters">Function parameters</param>
		Public Sub New(message As String, [error] As csapiError, suppliername__1 As String, functioncall__2 As String, parameters__3 As String)
			MyBase.New(message)
			csAPIError = [error]
			SupplierName = suppliername__1
			FunctionCall = functioncall__2
			Parameters = parameters__3
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="message">Message</param>
		Public Sub New(message As String)
			MyBase.New(message)
			csAPIError = csapiError.csapierrSuccess
			SupplierName = ""
			FunctionCall = ""
			Parameters = ""
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New(message As String, inner As System.Exception)
			MyBase.New(message, inner)
			csAPIError = csapiError.csapierrSuccess
			SupplierName = ""
			FunctionCall = ""
			Parameters = ""
		End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="info">Info</param>
		''' <param name="context">Context</param>
		Public Sub New(info As System.Runtime.Serialization.SerializationInfo, context As System.Runtime.Serialization.StreamingContext)
			MyBase.New(info, context)
			csAPIError = csapiError.csapierrSuccess
			SupplierName = ""
			FunctionCall = ""
			Parameters = ""
		End Sub

		''' <summary>
		''' Error Id
		''' </summary>
		Public csAPIError As csapiError

		''' <summary>
		''' Supplier name
		''' </summary>
		Public SupplierName As String

		''' <summary>
		''' Function call
		''' </summary>
		Public FunctionCall As String

		''' <summary>
		''' Function parameters
		''' </summary>
		Public Parameters As String
	End Class
End Namespace
