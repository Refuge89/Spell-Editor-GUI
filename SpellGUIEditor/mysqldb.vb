Imports MySql.Data.MySqlClient
Imports System.Xml


Public Class mysqldb
    'MySQL

    Public iconfilepath As String = ""
    Public connString As String = ""
    Public conn As MySqlConnection
    Public da As MySqlDataAdapter

    Public Sub LoadVars()
        Dim config As XmlDocument = New XmlDocument
        config.Load("config.xml")

        Dim mysqlhost As String = xmlRead(config, "mysqlhost", "mysql")
        Dim mysqldb As String = xmlRead(config, "mysqldb", "mysql")
        Dim mysqluser As String = xmlRead(config, "mysqluser", "mysql")
        Dim mysqlpass As String = xmlRead(config, "mysqlpass", "mysql")
        connString = "Server=" & mysqlhost & ";Database=" & mysqldb & ";Uid=" & mysqluser & ";Pwd=" & mysqlpass & ";"
        iconfilepath = xmlRead(config, "iconfilepath", "gen")

    End Sub

    Public Sub executeSQL(ByVal sSql As String, ByRef dt As DataTable)
        'Dim sr As SqlDataReader = Nothing
        dt = New DataTable
        conn = New MySqlConnection
        da = New MySqlDataAdapter
        Try
            conn.ConnectionString = connString
            conn.Open()
            Dim sComm As New MySqlCommand
            sComm.CommandText = sSql
            sComm.Connection = conn
            da.SelectCommand = sComm
            da.Fill(dt)
            conn.Close()
        Catch ex As Exception
            If (conn.State = Data.ConnectionState.Open) Then
                conn.Close()
            End If
        End Try
        conn = Nothing
    End Sub
    Public Function UpdateSQL(ByVal sSQL As String) As Boolean
        Dim result As Boolean
        Dim da As New MySqlDataAdapter
        conn = New MySqlConnection
        Try
            conn.ConnectionString = connString
            conn.Open()
            Dim sComm As New MySqlCommand
            sComm.CommandText = sSQL
            sComm.Connection = conn
            sComm.ExecuteNonQuery()
            conn.Close()
            result = True
        Catch ex As Exception
            result = False
            If (conn.State = Data.ConnectionState.Open) Then
                conn.Close()
            End If
        End Try
        conn = Nothing
        Return result
    End Function
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