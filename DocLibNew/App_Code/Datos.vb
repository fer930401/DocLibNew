
Imports System.Data.SqlClient

Public Class Datos
    Public Function MiConexion() As SqlConnection
        Dim conexion As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("developConnectionString")
        Dim myConnection As New SqlConnection(conexion.ConnectionString)
        Return myConnection
    End Function

    Public Function time_out() As Integer
        Dim time As Integer = Int16.Parse(ConfigurationManager.AppSettings("time"))
        Return time
    End Function

    Public Function RutaDocumentos() As String
        Dim ruta As String = ConfigurationManager.AppSettings("ruta")
        Return ruta
    End Function

    Public Function ObtieneDb() As String
        Dim cadena As String = ConfigurationManager.ConnectionStrings("developConnectionString").ToString
        Dim partes() As String = cadena.Split(New Char() {";"c})
        Dim base As String = ""

        For index = 0 To partes.Count - 1
            Dim i As Integer = partes(index).IndexOf("Initial Catalog", StringComparison.Ordinal)
            If i > -1 Then
                base = partes(index).Substring(partes(index).IndexOf("=", StringComparison.Ordinal) + 1, partes(index).Length - partes(index).IndexOf("=", StringComparison.Ordinal) - 1)
            End If
        Next
        Return base.ToUpper
    End Function
End Class

