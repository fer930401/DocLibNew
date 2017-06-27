
Partial Class Ver
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim rutaConArchivo As String = CType(Session("rutaImagen"), String)


        MostrarImagen(rutaConArchivo)
    End Sub
    Private Sub MostrarImagen(ByRef url As String)
        'Dim url = "\\websrv1\ArchivosLibreriasWeb\documentos\upload\001ITRMN76638.pdf"
        Response.Clear()

        If url.IndexOf(".pdf", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "application/pdf"
        ElseIf url.IndexOf(".xml", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "text/xml"
        ElseIf url.IndexOf(".html", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "text/html"
        ElseIf url.IndexOf(".txt", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "text/plain"
        ElseIf url.IndexOf(".jpeg", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "image/jpeg"
        ElseIf url.IndexOf(".pjpeg", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "image/pjpeg"
        ElseIf url.IndexOf(".png", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "image/x-png"
        ElseIf url.IndexOf(".tiff", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "image/tiff"
        ElseIf url.IndexOf(".bmp", System.StringComparison.Ordinal) > 0 Then
            Response.ContentType = "image/bmp"
        End If

        Response.AddHeader("Content-disposition", "inline; filename=" & url)
        Response.TransmitFile(url)
        Response.Flush()
        Response.End()
    End Sub
End Class
