
Imports System.Security.Policy

Partial Class Archivo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Dim ruta = Session("rutaImagen").ToString()

        'If ruta = "" Then
        '    Dim url = Session("rutaVer").ToString()
        '    myPDF.Attributes.Add("src", url)
        'Else
        '    Dim rutaConArchivo As String = CType(Session("rutaImagen"), String)
        '    Image1.ImageUrl = rutaConArchivo
        'End If


        Dim rutaConArchivo As String = CType(Session("rutaImagen"), String)
        Image1.ImageUrl = rutaConArchivo
    End Sub
    Public Sub OpenNewWindow(url As String)
        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "newWindow", String.Format("<script>  window.location('{0}'); window.print();</script>", url))
    End Sub
End Class
