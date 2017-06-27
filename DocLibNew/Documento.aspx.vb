
Partial Class Documento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim url = Session("rutaVer").ToString()
        myPDF.Attributes.Add("src", url)
    End Sub
End Class
