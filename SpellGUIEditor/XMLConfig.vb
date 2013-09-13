Imports System.Xml

Public Class XMLConfig
    Public iconfilepath As String = ""

    Public Sub LoadVars()
        Dim config As XmlDocument = New XmlDocument
        config.Load("config.xml")

        iconfilepath = xmlRead(config, "iconfilepath", "gen")
    End Sub

    Public Function xmlRead(xmldoc As XmlDocument, strKey As String, strElem As String) As String
        Dim xmlElem As XmlElement
        Dim RetVal As String = ""

        Try
            xmlElem = xmldoc.DocumentElement

            If (strElem <> "") Then
                xmlElem = xmlElem.Item(strElem)
            End If

            RetVal = xmlElem.Item(strKey).InnerText
        Catch ex As Exception
            RetVal = ""
        End Try
        Return RetVal
    End Function
End Class