Imports csImageFileTrial
Imports System.Globalization
Imports System.Data
Imports System.Drawing
Imports GhostscriptSharp
Imports GhostscriptSharp.Settings
Imports System.Threading
Partial Class Ingresar
    Inherits System.Web.UI.Page
    Private ReadOnly _da As New Datos
    ReadOnly _transac As New Doclib.libreria.Transacciones
    Private Const Swlocal As Integer = 0 'cambiar a 0 para produccion o para el server configurado
    Private _httpRuta As String = "http://websrv1"
    Private _libreriaLoc As String = "\\A201302635486\archivosLibreriasWeb\"
    'Private _libreriaLoc As String = "\\archivosLibreriasWeb\"
    Private _libreria As String = _da.RutaDocumentos
    Private _swConfig As Integer = 0
    Private _libreriaTmp As String = ""
    Protected Sub btnRegistrar_Click(sender As Object, e As System.EventArgs) Handles btnRegistrar.Click
        Thread.Sleep(6000)
        Dim iError As Integer = 0

        iError = ValidaArchivos() ' que contengan archivos todos los input files

        If iError = 0 And Page.Session("ArchivoMismoNombre") = 0 Then
            iError = GrabarImagenes(iError)
        End If


        If iError = 0 And Page.Session("ArchivoMismoNombre") = 1 Then
            iError = ValidaXml()
        End If

        If iError = 0 And Page.Session("ArchivoMismoNombre") = 1 Then
            iError = GrabarImagenesMismoNombre(iError)
        End If

        If iError = 0 Then
            lblError.Visible = False
            lblError.Text = ""
            ddlTipo1.DataSource = Nothing
            txtFolio.Text = ""
            btnRegistrar.Visible = False
            lblRegistrar.Visible = False
            'Else
            '    lblError.Visible = True
            '    lblError.Text = "Ocurrio un error al subir archivos por favor verificar en la consulta"
        End If
    End Sub
    Private Function ValidaXml() As Integer
        Dim iError = 0
        Dim j = 1
        Dim uploadFilCol As HttpFileCollection = Request.Files
        For i As Integer = 0 To uploadFilCol.Count - 1
            Dim file As HttpPostedFile = uploadFilCol(i)
            Dim FileName = System.IO.Path.GetFileName(file.FileName)
            Dim consecutivo As Integer = CType(Request.Form("ddlTipo" + j.ToString()), Integer)
            Dim extension = FileName.Substring(FileName.LastIndexOf(".", System.StringComparison.Ordinal) + 1).ToLower()
            Dim dt = New DataTable
            Dim extensionDB As String = ""
            dt = Session("dtCbo")

            Dim query = _
            From consul In dt.AsEnumerable() _
            Where consul.Field(Of Short)("clave") = consecutivo _
            Select consul

            For Each itm As Object In query
                extensionDB = itm("extension").ToString.Trim
            Next

            If extensionDB <> "*" Then
                If extension.Trim <> extensionDB.ToLower Then
                    lblError.Text = "Extencion de archivo incorrecta, verificar"
                    lblError.Visible = True
                    iError = 1
                End If
            End If


            j += 1

        Next
        Return iError
    End Function
    Private Function ValidaArchivos() As Integer
        Dim iError = 0
        Dim uploadFilCol As HttpFileCollection = Request.Files
        For i As Integer = 0 To uploadFilCol.Count - 1
            Dim file As HttpPostedFile = uploadFilCol(i)
            Dim FileName = IO.Path.GetFileNameWithoutExtension(file.FileName)
            If FileName = String.Empty Then
                lblError.Text = "Debe seleccionar archivos"
                lblError.Visible = True
                iError = 1
            End If
        Next
        Return iError
    End Function

    Protected Sub SalirInicio(sender As Object, e As EventArgs) Handles lnkSalir.ServerClick
        Response.Redirect("Login.aspx")
    End Sub
    Protected Sub btnConsultarIr(sender As Object, e As EventArgs) Handles lnkConsulta.ServerClick
        Response.Redirect("Consulta.aspx")
    End Sub
    Protected Sub btnIngresarIr(sender As Object, e As EventArgs) Handles lnkIngreso.ServerClick
        Response.Redirect("Ingresar.aspx")
    End Sub
    Private Function RevisarImagenesYaExisten(ByRef iError As Integer) As Integer
        Dim datDoc = _transac.ConsultaImagenes(Session("efCve").ToString(), ddlDocumentos.SelectedValue.Trim, Integer.Parse(txtFolio.Text.Trim()))
        Return iError
    End Function
    Private Function GrabarImagenes(ByRef iError As Integer) As Integer
        Dim j As Integer = 0
        _httpRuta = Page.Session("httpRuta").ToString()
        _libreriaTmp = Page.Session("libreriaTmp").ToString()
        _swConfig = Integer.Parse(Page.Session("swConfig").ToString())
        For i As Integer = 1 To Request.Files.Count

            Dim postedFile As HttpPostedFile = Request.Files(j)
            If postedFile.ContentLength > 0 Then
                Dim fn = System.IO.Path.GetFileName(postedFile.FileName)
                Dim nombreArchivo = postedFile.FileName
                Dim nomFinalArchivo = ""
                Dim consecutivo As Integer = CType(Request.Form("ddlTipo" + i.ToString()), Integer)
                Dim ultimo = ""
                If consecutivo.ToString() = "7" Then
                    Session("numCom") = CType(Session("numCom").ToString(), Integer) + 1
                    ultimo = "7" + "@" + Session("numCom").ToString().Trim()
                Else
                    ultimo = consecutivo.ToString()
                End If
                If consecutivo = 1 Then
                    nomFinalArchivo = Session("efCve").ToString() + ddlDocumentos.SelectedValue.Trim() + txtFolio.Text.ToString().Trim
                Else
                    nomFinalArchivo = Session("efCve").ToString() + ddlDocumentos.SelectedValue.Trim() + txtFolio.Text.ToString().Trim & "@" & ultimo
                End If
                Dim extension = nombreArchivo.Substring(nombreArchivo.LastIndexOf(".", System.StringComparison.Ordinal) + 1).ToLower()
                GrabarDatos(iError, consecutivo, extension, nomFinalArchivo)
                Dim rutaGrabar As String = ""
                Dim rutaGrabarBis As String = "" 'para ruta alterna y no de libreria
                If Swlocal = 1 Then
                    rutaGrabar = _libreriaLoc & "documentos\upload\"
                Else
                    If _swConfig = 1 Then
                        rutaGrabarBis = Server.MapPath(_libreriaTmp)
                    End If
                    rutaGrabar = Server.MapPath(_libreria & "documentos/upload/")
                End If
                If _swConfig = 1 Then
                    postedFile.SaveAs(rutaGrabarBis & nomFinalArchivo & "." & extension)
                Else
                    postedFile.SaveAs(rutaGrabar & nomFinalArchivo & "." & extension)
                End If
                If extension = "pdf" Then
                    Dim origenRuta As String = ""
                    If _swConfig = 1 Then
                        origenRuta = rutaGrabarBis & nomFinalArchivo & "." & extension
                    Else
                        origenRuta = rutaGrabar & nomFinalArchivo & "." & extension
                    End If
                    Dim destinoRuta = rutaGrabar + "thumbnail\" + nomFinalArchivo & ".gif"
                    iError = GetPdfThumbnail(origenRuta, destinoRuta)
                Else
                    If _swConfig = 1 Then
                        iError = ThumbsImagen(rutaGrabarBis, nomFinalArchivo & "." & extension)
                    Else
                        iError = ThumbsImagen(rutaGrabar, nomFinalArchivo & "." & extension)
                    End If
                End If
            End If
            j += 1
        Next
        Return iError
    End Function
    Private Function GrabarImagenesMismoNombre(ByRef iError As Integer) As Integer
        Dim j As Integer = 0
        Dim nomFinalArchivo = ""

        _httpRuta = Page.Session("httpRuta").ToString()
        _libreriaTmp = Page.Session("libreriaTmp").ToString()
        _swConfig = Integer.Parse(Page.Session("swConfig").ToString())
        Dim consecutivo As Integer = 0 'CType(Request.Form("ddlTipo1"), Integer)
        'Dim ultimo = consecutivo.ToString()
        nomFinalArchivo = Session("efCve").ToString() + ddlDocumentos.SelectedValue.Trim() + txtFolio.Text.ToString().Trim
        Dim extension As String = ""


        For i As Integer = 1 To Request.Files.Count

            Dim postedFile As HttpPostedFile = Request.Files(j)
            If postedFile.ContentLength > 0 Then
                Dim fn = System.IO.Path.GetFileName(postedFile.FileName)
                Dim nombreArchivo = postedFile.FileName
                extension = nombreArchivo.Substring(nombreArchivo.LastIndexOf(".", System.StringComparison.Ordinal) + 1).ToLower()

                Dim rutaGrabar As String = ""
                Dim rutaGrabarBis As String = "" 'para ruta alterna y no de libreria

                If Swlocal = 1 Then
                    rutaGrabar = _libreriaLoc & "documentos\upload\"
                Else
                    If _swConfig = 1 Then
                        rutaGrabarBis = Server.MapPath(_libreriaTmp)
                    End If
                    rutaGrabar = Server.MapPath(_libreria & "documentos/upload/")
                End If

                If _swConfig = 1 Then
                    postedFile.SaveAs(rutaGrabarBis & nomFinalArchivo & "." & extension)
                Else
                    postedFile.SaveAs(rutaGrabar & nomFinalArchivo & "." & extension)
                End If

                If extension = "pdf" Then
                    Dim origenRuta As String = ""
                    If _swConfig = 1 Then
                        origenRuta = rutaGrabarBis & nomFinalArchivo & "." & extension
                    Else
                        origenRuta = rutaGrabar & nomFinalArchivo & "." & extension
                    End If
                    Dim destinoRuta = rutaGrabar + "thumbnail\" + nomFinalArchivo & ".gif"
                    iError = GetPdfThumbnail(origenRuta, destinoRuta)
                End If

                If extension <> "xml" And extension <> "pdf" Then

                    If _swConfig = 1 Then
                        iError = ThumbsImagen(rutaGrabarBis, nomFinalArchivo & "." & extension)
                    Else
                        iError = ThumbsImagen(rutaGrabar, nomFinalArchivo & "." & extension)
                    End If
                End If




            End If
            j += 1
        Next

        If extension.ToLower = "xml" Or extension.ToLower = "pdf" Then
            extension = "pdf"
        End If

        GrabarDatos(iError, consecutivo, extension, nomFinalArchivo)

        Return iError
    End Function

    Private Function GetPdfThumbnail(ByRef sourcePdfFilePath As String, ByRef destinationPngFilePath As String) As Integer
        Dim iError As Integer = 0
        Try
            Dim mPage As New GhostscriptPages()
            mPage.Start = 1
            mPage.End = 1
            mPage.AllPages = True
            Dim mResolution As New Size
            mResolution.Height = 310
            mResolution.Width = 140
            Dim mSize As New GhostscriptPageSize
            mSize.Native = GhostscriptPageSizes.letter
            Dim mSettings As New GhostscriptSettings()
            mSettings.Device = GhostscriptDevices.pngalpha
            mSettings.Page = mPage
            mSettings.Resolution = mResolution
            mSettings.Size = mSize
            GhostscriptWrapper.GenerateOutput(sourcePdfFilePath, destinationPngFilePath, mSettings)
        Catch ex As Exception
            Me.lblError.Text = "Archivo guardado pero no valido para generar thumbnail"
            Me.lblError.Visible = True
            Response.Write(ex.Message)
        Finally
        End Try
        Return iError
    End Function
    Private Function ThumbsImagen(ByVal ruta As String, ByVal nombreArch As String) As Integer
        Dim iError As Integer = 0
        Try
            Dim imagen As New Manage
            imagen.ReadFile(ruta + nombreArch)
            Dim sNewFile As String
            Dim indexW As Integer
            indexW = nombreArch.LastIndexOf(".", System.StringComparison.Ordinal)
            sNewFile = nombreArch.Substring(0, indexW)
            imagen.JpegQuality = 30
            imagen.Scale(50)
            imagen.ColorDepth = 12
            imagen.Resize(0, 200)
            imagen.WriteFile(ruta + "thumbnail/" + sNewFile & ".gif")
            imagen = Nothing
            ruta = "ArchivoCreado"
        Catch ex As Exception
            Me.lblError.Text = "Archivo guardado pero no valido para generar thumbnail"
            Me.lblError.Visible = True
        Finally
        End Try
        Return iError
    End Function
    Private Function GrabarDatos(ByRef iError As Integer, ByRef consecutivo As Integer, ByRef extension As String, ByRef archivo As String) As Integer
        Dim resultado = _transac.AgregarImagen(Session("efCve").ToString(), ddlDocumentos.SelectedValue, CType(txtFolio.Text.ToString(), Integer),
                                               consecutivo, Session("usrCve").ToString(), extension, archivo, lblFecha.Text)
        For Each o As DataRow In resultado.Rows
            Dim imgId = CType(o.Item("imgId"), Double)
            Dim swActualizar = CType(o.Item("swActualizar"), Integer)
        Next
        Return iError
    End Function
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            'Session("efCve") = "001"
            'Session("usrCve") = "RGM"
            'Session("nomUsuario") = "Ricardo Garcia"
            'Session("empresa") = "Skytex Mexico SA de CV."
            'Page.Session("ruta") = "\\websrv1\ArchivosLibreriasWeb\documentos\upload\"
            'Page.Session("ruta") = "\\fsb\Documentos\"

            LlenaDocumentos()
            Page.Session("ArchivoMismoNombre") = 0
            Page.Session("swParXmlPdf") = 0
            ''Dim dt = Date.Today.ToString("dd/mm/yyyy")
            lblFecha.Text = Year(Now).ToString("0000") & "/" & Month(Now).ToString("00") & "/" & Day(Now).ToString("00") 'dt
            lblEmpresa.Text = Session("empresa").ToString()
            lblNombre.Text = Session("nomUsuario").ToString()
            Dim configuracion = Page.Session("ruta").ToString()
            If Swlocal = 0 Then
                If configuracion.IndexOf("ArchivosLibreriasWeb", StringComparison.CurrentCulture) < 0 Then
                    _swConfig = 1
                    _httpRuta = "http://websrv1/Documentos"
                    _libreriaTmp = "/Documentos/"

                Else
                    _swConfig = 0
                End If
            End If
            Page.Session("swConfig") = _swConfig
            Page.Session("httpRuta") = _httpRuta
            Page.Session("libreriaTmp") = _libreriaTmp
        End If
        If IsPostBack Then
            Dim ctrlName = Request.Params(Page.postEventSourceID)
            Dim args = Request.Params(Page.postEventArgumentID)
            HandleCustomPostbackEvent(ctrlName, args)
        End If
        If form1.Enctype <> "multipart/form-data" Then
            form1.Enctype = "multipart/form-data"
        End If
    End Sub
    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim onBlurScript = Page.ClientScript.GetPostBackEventReference(txtFolio, "OnBlur")
        txtFolio.Attributes.Add("onblur", onBlurScript)
    End Sub
    Private Sub LlenaDocumentos()
        Dim dtTrans = _transac.ConsultaDocumentos(Session("efCve").ToString(), Session("usrCve").ToString())
        ddlDocumentos.DataSource = Nothing
        ddlDocumentos.DataSource = dtTrans
        ddlDocumentos.DataTextField = "nombre"
        ddlDocumentos.DataValueField = "tipdoc_cve"
        ddlDocumentos.DataBind()
        ddlDocumentos.Items(0).Selected = True
    End Sub
    Private Sub LlenaComboTipo()
        Dim dtCbo As New DataTable
        If Session("swValPersona") = 0 Then
            dtCbo = _transac.ObtCboConfiguracion(Session("efCve").ToString(), ddlDocumentos.SelectedValue, CType(txtFolio.Text.ToString(), Integer))
        Else
            dtCbo = _transac.ObtCboBis(Session("efCve").ToString(), ddlDocumentos.SelectedValue, CType(txtFolio.Text.ToString(), Integer))
        End If

        If dtCbo.Rows.Count > 0 Then
            ddlTipo1.DataSource = Nothing
            ddlTipo1.DataSource = dtCbo
            ddlTipo1.DataTextField = "dato"
            ddlTipo1.DataValueField = "clave"
            ddlTipo1.DataBind()
            ddlTipo1.Items(0).Selected = True
            For Each o As DataRow In dtCbo.Rows
                If o.Item("clave").ToString() = "7" Then
                    Session("numCom") = CType(o.Item("numero"), Integer)
                End If
                If o.Item("prm1").ToString().Trim = "Only" Then
                    Session("ArchivoMismoNombre") = 1
                    Page.Session("swParXmlPdf") = Integer.Parse(o.Item("requerido").ToString())
                    Session("dtCbo") = dtCbo
                End If
            Next
        End If
    End Sub
    Private Sub HandleCustomPostbackEvent(ctrlName As String, args As String)
        If ctrlName = txtFolio.UniqueID AndAlso args = "OnBlur" Then
            If txtFolio.Text.ToString().Trim() <> "" Then

                Session("FolioInsert") = CType(txtFolio.Text.ToString(), Integer)
                ExisteDocumento()
                'LlenaComboTipo()
            End If
        End If
    End Sub
    Private Sub ExisteDocumento()
        'Dim iExiste = _transac.ValidaExisteFolio(Session("efCve").ToString(), ddlDocumentos.SelectedValue, CType(txtFolio.Text.ToString(), Integer))
        Dim dtRes = _transac.ValidaExisteFolio(Session("efCve").ToString(), ddlDocumentos.SelectedValue, CType(Session("FolioInsert").ToString(), Integer))
        Dim iExiste As Integer

        If dtRes.Rows.Count > 0 Then
            iExiste = CType(dtRes.Rows(0).Item("existe"), Integer)
        End If

        'Dim iExiste = _transac.ValidaExisteFolio(Session("efCve").ToString(), ddlDocumentos.SelectedValue, Session("FolioInsert"))

        If iExiste = 1 Then
            lblRegistrar.Visible = True
            btnRegistrar.Visible = True
            btnRegistrar.Focus()
            lblError.Visible = False
            lblError.Text = ""

            'Session("tipoPersona") = CType(dtRes.Rows(0).Item("tipo_persona"), Integer)
            'Session("swImpDia") = CType(dtRes.Rows(0).Item("sw_impdia"), Boolean)
            'Session("FechaMov") = CType(dtRes.Rows(0).Item("fecha_mov"), Date)
            Session("swValPersona") = CType(dtRes.Rows(0).Item("sw_val_persona"), Integer)
            'Session("FechaValidacion") = CType(dtRes.Rows(0).Item("fecha_validacion"), Date)
            LlenaComboTipo()
        ElseIf iExiste = 2 Then
            lblError.Visible = True
            lblError.Text = "El documento ya tiene un archivo registrado, por favor consultar"
            lblRegistrar.Visible = False
            btnRegistrar.Visible = False
        Else
            lblError.Visible = True
            lblError.Text = "No existe folio en base de datos"
            lblRegistrar.Visible = False
            btnRegistrar.Visible = False
        End If
    End Sub
    Protected Sub txtFolio_TextChanged(sender As Object, e As System.EventArgs) Handles txtFolio.TextChanged
    End Sub
End Class
