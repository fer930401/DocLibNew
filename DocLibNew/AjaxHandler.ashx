<%@ WebHandler Language="VB" Class="AjaxHandler" %>

Imports System
Imports System.Web

Public Class AjaxHandler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'context.Response.ContentType = "text/plain"
        'context.Response.Write("Hola a todos")
        System.Threading.Thread.Sleep(10000)
        context.Response.ContentType = "text/plain"
        context.Response.Write("Data processed successfully!")
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class