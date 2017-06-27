Imports System.IO
Imports System.Data
Imports System.IO.Compression
Imports System.Activities.Statements
Imports System.Net
Imports System.Web.UI.WebControls.Expressions
Partial Class Consulta
    Inherits System.Web.UI.Page
    Private ReadOnly _da As New Datos
    ReadOnly _transac As New Doclib.libreria.Transacciones
    Private _libreria As String = _da.RutaDocumentos
    Private Const Swlocal As Integer = 0 'cambiar a 0 para produccion o para el server configurado
    Private _httpRuta As String = "http://websrv1"
    Private _libreriaLoc As String = "\\A201302635486\archivosLibreriasWeb\"
    Private _libreriaTmp As String = "\ArchivosLibreriasWeb\"
    Private _swConfig As Integer = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then


            'Response.Cache.SetExpires(DateTime.Now.AddSeconds(5))
            'Response.Cache.SetCacheability(HttpCacheability.Public)
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)
            'Response.Cache.SetNoServerCaching()

            'Session("efCve") = "001"
            'Session("usrCve") = "RGM"
            'Session("nomUsuario") = "Ricardo Garcia"
            'Session("empresa") = "Skytex Mexico SA de CV."
            'Page.Session("ruta") = "\\websrv1\ArchivosLibreriasWeb\documentos\upload\"
            ''Page.Session("ruta") = "\\fsb\Documentos\"


            LlenaDocumentos()
            lblEmpresa.Text = Session("empresa").ToString()
            lblNombre.Text = Session("nomUsuario").ToString()
            Page.Session("swRegIguales") = 0
            Dim configuracion = Page.Session("ruta").ToString()
            If Swlocal = 0 Then
                If configuracion.IndexOf("ArchivosLibreriasWeb", StringComparison.CurrentCulture) < 0 Then
                    _swConfig = 1
                    '_httpRuta = "http://websrv1/Documentos"
                    _httpRuta = "http://websrv1/documentos"
                    _libreriaTmp = Page.Session("ruta") '"/Documentos/"
                Else
                    _swConfig = 0
                End If
            End If
            Page.Session("swConfig") = _swConfig
            Page.Session("httpRuta") = _httpRuta
            Page.Session("LibreriaTmp") = _libreriaTmp
        End If
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
    Protected Sub btnConsultar_Click(sender As Object, e As System.EventArgs) Handles btnConsultar.Click
        DsTmp()
        If txtFolio.Text.Trim <> "" Then
            'DsTmp()
            MostrarImg()
        Else
            grdDocDescarga.Visible = False
        End If
    End Sub
    Private Sub DsTmp()
        Dim dsV, dsC As New Data.DataSet()
        Dim tabla2 As DataTable = dsV.Tables.Add("Tabla")
        tabla2.Columns.Add("id", Type.GetType("System.Int32"))
        tabla2.Columns.Add("s_nombre", Type.GetType("System.String"))
        tabla2.Columns.Add("s_tipo", Type.GetType("System.String"))
        tabla2.Columns.Add("s_archivo", Type.GetType("System.Object"))
        tabla2.Columns.Add("s_fname", Type.GetType("System.String"))
        tabla2.Columns.Add("s_name2", Type.GetType("System.String"))
        tabla2.Columns.Add("file_ext", Type.GetType("System.String"))
        tabla2.Columns.Add("fec_design", Type.GetType("System.String"))
        tabla2.Columns.Add("consecutivo", Type.GetType("System.Int32"))
        Session("dstemp2") = dsV
    End Sub
    Protected Sub SalirInicio(sender As Object, e As EventArgs) Handles lnkSalir.ServerClick
        Response.Redirect("Login.aspx")
    End Sub
    Protected Sub btnConsultarIr(sender As Object, e As EventArgs) Handles lnkConsulta.ServerClick
        Response.Redirect("Consulta.aspx")
    End Sub
    Protected Sub btnIngresarIr(sender As Object, e As EventArgs) Handles lnkIngreso.ServerClick
        Response.Redirect("Ingresar.aspx")
    End Sub
    Private Sub MostrarImg()


        Dim datDoc As New DataTable
        Dim swImagenes As Integer
        Dim archivo As String
        Dim nomArchivo As String = ""
        Dim rutaF As String
        Dim rutaUpl As String = ""
        Dim myDataTable As New DataTable
        Dim myDataRow As DataRow
        Dim myDs As DataSet
        'Dim swRegIguales As Integer = 0
        _httpRuta = Page.Session("httpRuta").ToString()
        _swConfig = Integer.Parse(Page.Session("swConfig").ToString())
        '_libreriaTmp = Page.Session("libreriaTmp").ToString()

        myDataTable.Columns.Add(New DataColumn("archivo", System.Type.GetType("System.String")))
        myDataTable.Columns.Add(New DataColumn("sw", System.Type.GetType("System.Int16")))
        myDataTable.Columns.Add(New DataColumn("URL", System.Type.GetType("System.String")))
        myDataTable.Columns.Add(New DataColumn("pathImage", System.Type.GetType("System.String")))
        myDataTable.Columns.Add(New DataColumn("archivoBis", System.Type.GetType("System.String")))
        myDataTable.Columns.Add(New DataColumn("file_ext", System.Type.GetType("System.String")))
        myDataTable.Columns.Add(New DataColumn("fec_design", System.Type.GetType("System.String")))
        myDataTable.Columns.Add(New DataColumn("consecutivo", System.Type.GetType("System.Int32")))
        datDoc = _transac.ConsultaImagenes(Session("efCve").ToString(), ddlDocumentos.SelectedValue.Trim, Integer.Parse(txtFolio.Text.Trim()))

        Dim productsQuery = From product In datDoc.AsEnumerable() _
               Select product
        Dim largeProducts = _
              productsQuery.Where(Function(p) p.Field(Of String)("cve_tipo") = 1)

        If largeProducts.Count() > 1 Then
            Page.Session("swRegIguales") = 1
        End If

        If datDoc.Rows.Count <> 0 Then
            swImagenes = datDoc.Rows.Count()
            Dim i As Integer = 0
            For Each r As DataRow In datDoc.Rows
                If swImagenes = 1 And r.Item("cve_tipo").ToString().Trim = "1" Then
                    nomArchivo = r.Item("nomUnico").ToString().Trim
                End If
                If swImagenes >= 1 Then
                    If Not IsDBNull(r.Item("nomArchivo")) Then
                        nomArchivo = r.Item("nomArchivo").ToString().Trim()
                    Else
                        nomArchivo = r.Item("nomUnico").ToString().Trim()
                    End If
                End If



                
                If Swlocal = 1 Then
                    archivo = _libreriaLoc & "documentos\upload\thumbnail\" & nomArchivo & ".gif"
                Else
                    archivo = ""
                    archivo = Server.MapPath(_libreria & "documentos/upload/thumbnail/" & nomArchivo & ".gif")
                End If
                If _swConfig = 1 Then
                    rutaUpl = _httpRuta & "/"
                Else
                    rutaUpl = _httpRuta & _libreria & "documentos/upload/"
                End If
                'If File.Exists(archivo) Then
                '    rutaF = _httpRuta & _libreria & "documentos/upload/thumbnail/" & nomArchivo & ".gif"
                'Else
                '    rutaF = _httpRuta & _libreria & "documentos/upload/thumbnail/GaleriaNoDisponible.jpg"
                'End If
                myDataRow = myDataTable.NewRow
                If swImagenes = 1 Then
                    myDataRow(0) = ddlDocumentos.SelectedItem.Text & " Folio: " & txtFolio.Text & " Tipo: " & datDoc.Rows(i).Item("tipo").ToString()
                    myDataRow(4) = nomArchivo & "." & datDoc.Rows(i).Item("file_ext").ToString().Trim()

                Else
                    myDataRow(0) = ddlDocumentos.SelectedItem.Text & " Folio: " & txtFolio.Text & " Tipo: " & datDoc.Rows(i).Item("tipo").ToString()
                    If Not IsDBNull(r.Item("nomArchivo")) Then
                        'myDataRow(4) = r.Item("nomArchivo").ToString().Trim()
                        myDataRow(4) = r.Item("nomArchivo").ToString().Trim() & "." & datDoc.Rows(i).Item("file_ext").ToString().Trim()
                    Else
                        myDataRow(4) = r.Item("nomUnico").ToString().Trim() & "." & datDoc.Rows(i).Item("file_ext").ToString().Trim()
                    End If
                End If
                myDataRow(1) = 1
                myDataRow(2) = rutaUpl
                myDataRow(3) = "" 'rutaF
                myDataRow(5) = datDoc.Rows(i).Item("file_ext").ToString().Trim()
                myDataRow(6) = datDoc.Rows(i).Item("fec_design").ToString().Trim()
                myDataRow(7) = datDoc.Rows(i).Item("cve_tipo").ToString().Trim()
                myDataTable.Rows.Add(myDataRow)
                i += 1
            Next
            myDs = New DataSet()
            myDs.Tables.Add(myDataTable)
            If myDs.Tables(0).Rows.Count > 0 Then
                ProcesoDesc(myDs)
            End If
        Else
            grdDocDescarga.DataSource = Nothing
            grdDocDescarga.Visible = False
        End If
    End Sub
    Sub ProcesoDesc(ByRef ds As DataSet)
        grdDocDescarga.Visible = True
        grdDocDescarga.DataSource = Nothing
        Dim drfila As DataRow
        grdDocDescarga.DataSource = ds.Tables(0)
        grdDocDescarga.DataBind()
        Dim ds2 = CType(Session("dstemp2"), DataSet)
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            drfila = ds2.Tables("Tabla").NewRow
            drfila("id") = 1
            drfila("s_nombre") = ds.Tables(0).Rows(i).Item("archivo").ToString()
            drfila("s_tipo") = ds.Tables(0).Rows(i).Item("sw").ToString()
            drfila("s_archivo") = ds.Tables(0).Rows(i).Item("archivoBis").ToString()
            drfila("s_fname") = ds.Tables(0).Rows(i).Item("pathImage").ToString()
            drfila("file_ext") = ds.Tables(0).Rows(i).Item("file_ext").ToString()
            drfila("fec_design") = ds.Tables(0).Rows(i).Item("fec_design").ToString()
            drfila("s_name2") = ds.Tables(0).Rows(i).Item("URL").ToString()
            drfila("consecutivo") = ds.Tables(0).Rows(i).Item("consecutivo").ToString()
            ds2.Tables("Tabla").Rows.Add(drfila)
        Next
        Session("dstemp2") = ds2
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim BOTON = DirectCast(e.CommandSource, Button)
        Dim a = grdDocDescarga.Rows()
        Dim opcionGrid As String = e.CommandName
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim selectedRow As GridViewRow = grdDocDescarga.Rows(index)

        _libreriaTmp = Page.Session("libreriaTmp").ToString()
        _swConfig = Integer.Parse(Page.Session("swConfig").ToString())
        Dim ds2 = CType(Session("dstemp2"), DataSet)
        Dim archivo As TableCell = selectedRow.Cells(1)
        Dim productsQuery = From product In ds2.Tables(0).AsEnumerable() _
               Select product

        

        If opcionGrid = "Ver" Then
            Dim largeProducts = _
                productsQuery.Where(Function(p) p.Field(Of String)("s_archivo") = archivo.Text)


            'Dim paso = CType(largeProducts, DataRow)

            For Each product In largeProducts
                Dim ruta = product.Field(Of String)("s_name2")
                'Dim ext = product.Field(Of String)("file_ext")
                'OpenNewWindow(ruta & archivo.Text & "." & ext)

                OpenNewWindow(ruta & archivo.Text, archivo.Text)

                Exit For
            Next
        End If
        If opcionGrid = "Imprimir" Then
        
            Dim largeProducts = _
                productsQuery.Where(Function(p) p.Field(Of String)("s_archivo") = archivo.Text)
            For Each product In largeProducts
                Dim archivoBis = archivo.Text.Replace(".pdf", ".gif")
                archivoBis = archivoBis.Replace(".xml", ".gif")

                Dim ruta = product.Field(Of String)("s_name2")
                'Dim ext = product.Field(Of String)("file_ext")
                'If ext.ToLower() = "pdf" Then
                '    Session("rutaImagen") = _httpRuta & _libreria & "documentos/upload/thumbnail/" & archivo.Text & ".gif"
                'Else
                '    Session("rutaImagen") = _httpRuta & _libreria & "documentos/upload/" & archivo.Text & "." & ext
                'End If
                If archivo.Text.IndexOf("pdf") >= 0 Or archivo.Text.IndexOf("xml") >= 0 Then
                    Session("rutaImagen") = _httpRuta & _libreria & "documentos/upload/thumbnail/" & archivoBis
                Else
                    Session("rutaImagen") = _httpRuta & _libreria & "documentos/upload/" & archivoBis
                End If
            Next
            Response.Write("<script type='text/javascript'>window.open('Archivo.aspx','cal');</script>")
        End If
        If opcionGrid = "Borrar" Then

            Dim largeProducts As EnumerableRowCollection(Of DataRow)
            If Page.Session("swRegIguales") = 1 Then
                largeProducts = _
               productsQuery.Where(Function(p) p.Field(Of String)("s_tipo") = 1)
            Else
                largeProducts = _
                productsQuery.Where(Function(p) p.Field(Of String)("s_archivo") = archivo.Text)
            End If

            'Dim largeProducts = _
            '    productsQuery.Where(Function(p) p.Field(Of String)("s_archivo") = archivo.Text)
            For Each product As DataRow In largeProducts
                Dim consecutivo = product.Field(Of Integer)("consecutivo")
                Dim archivoPaso = product("s_archivo").ToString()
                BorrarDocumento(consecutivo, archivoPaso)
                'BorrarDocumento(consecutivo, archivo.Text.Substring(0, archivo.Text.IndexOf(".", System.StringComparison.Ordinal)))
                'Response.Write(Server.MapPath(_libreriaTmp & archivo.Text))
                'Response.Write(Server.MapPath(_libreriaTmp & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & archivo.Text))
                Dim ruta = product.Field(Of String)("s_name2")
                If _swConfig = 1 Then
                    'If File.Exists(_libreriaTmp & archivo.Text) = True Then
                    If File.Exists(_libreriaTmp & archivoPaso) = True Then
                        'File.Delete(Server.MapPath(_libreriaTmp & archivo.Text))
                        'FileSystem.Rename(_libreriaTmp & archivo.Text, _libreriaTmp & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & "_DELDOCLIB_" & archivo.Text)

                        Dim fi As FileInfo
                        fi = New FileInfo(_libreriaTmp & archivoPaso)
                        If File.Exists(_libreriaTmp & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & "_DELDOCLIB_" & archivoPaso) = False Then
                            fi.CopyTo(_libreriaTmp & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & "_DELDOCLIB_" & archivoPaso)
                            File.Delete(_libreriaTmp & archivoPaso)
                        End If

                    End If
                Else
                    'If File.Exists(Server.MapPath(ruta & archivo.Text)) = True Then
                    '    FileSystem.Rename(Server.MapPath(ruta & archivo.Text), Server.MapPath(ruta & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & archivo.Text))
                    '    'File.Delete(ruta & archivo.Text))
                    'End If
                    _libreriaTmp = Page.Session("ruta").ToString()

                    Dim fi As FileInfo
                    fi = New FileInfo(_libreriaTmp & "/" & archivoPaso)
                    If File.Exists(_libreriaTmp & "/" & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & "_DELDOCLIB_" & archivoPaso) = False Then
                        fi.CopyTo(_libreriaTmp & "/" & Year(Now).ToString() & Month(Now).ToString() & Day(Now) & Minute(Now).ToString() & "_DELDOCLIB_" & archivoPaso)
                        File.Delete(_libreriaTmp & "/" & archivoPaso)
                    End If
                End If


                'MostrarImg()
                'Exit For
            Next
            MostrarImg()
        End If
    End Sub
    Public Sub BorrarDocumento(ByRef consecutivo As Integer, ByRef archivo As String)
        Dim resultado = _transac.BorrarImagen(Session("efCve").ToString(), ddlDocumentos.SelectedValue, CType(txtFolio.Text.ToString(), Integer),
                                              consecutivo, Session("usrCve").ToString(), archivo)
        For Each o As DataRow In resultado.Rows
            Dim imgId = CType(o.Item("imgId"), Double)
            Dim swActualizar = CType(o.Item("swBorrado"), Short)
        Next
    End Sub
    Public Sub MuestraPdf()
        Dim Path As String = "http://a201302635486/archivosLibreriasWeb/documentos/upload/001ITVS062.pdf"
        Response.Redirect(Path)
    End Sub
    Public Sub OpenNewWindow(url As String, archivo As String)
        'Session("rutaImagen") = url
        _libreriaTmp = Page.Session("ruta").ToString()
        Session("rutaImagen") = _libreriaTmp & "\" & archivo
        Response.Write("<script type='text/javascript'>window.open('Ver.aspx','cal');</script>")

        ''Page.Response.Flush()
        ''Page.Response.Clear()
        ''Page.Response.End()
        ''Response.BufferOutput = False
        ''HttpResponse.RemoveOutputCacheItem(Server.MapPath(url))
        ''HttpResponse.RemoveOutputCacheItem(Server.MapPath(url))
        ''HttpResponse.RemoveOutputCacheItem("/ArchivosLibreriasWeb/documentos/upload/001ITRMN76638.pdf")
        ''HttpResponse.RemoveOutputCacheItem("/websrv1/ArchivosLibreriasWeb/documentos/upload/001ITRMN76638.pdf")
        ''HttpResponse.RemoveOutputCacheItem("/Consulta.aspx/websrv1/ArchivosLibreriasWeb/documentos/upload/001ITRMN76638.pdf")
        ''HttpResponse.RemoveOutputCacheItem("/Consulta.aspx//ArchivosLibreriasWeb/documentos/upload/001ITRMN76638.pdf")
        'ClearCache()
        ''Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Page.ClientScript.RegisterStartupScript(Me.[GetType](), "newWindow", String.Format("<script>window.open('{0}');</script>", url))
        'Response.Flush()
        'Response.Clear()
        ''Response.Close()
        ''Response.End()
        ''Response.Write("<script type='text/javascript'>window.open('" + url + "','cal','width=400,height=250,left=270,top=180');</script>")
        ''Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>")
        ''Response.Clear()
        ''Session("rutaImagen") = ""
        ''Session("rutaVer") = url
        ''Response.Write("<script type='text/javascript'>window.open('Documento.aspx','cal','width=400,height=250,left=270,top=180');</script>")
        ''Response.Write("<script type='text/javascript'>window.open('Documento.aspx','','width=1200,height=900');</script>")
        ''Response.Write("<script type='text/javascript'>window.open('" + url + "', '' ,'width=1000,height=800');</script>")
        ''Response.Redirect(url)
    End Sub

    Private Sub ClearCache()
        'Dim Cache = HttpRuntime.Cache

        ''Dim dictionaryEnumerators As IDictionaryEnumerator
        ''Dim dictionaryEnumerators = Cache.GetEnumerator()

        'For Each key As Object In Cache.GetEnumerator().Value
        '    Cache.Remove(key.ToString())
        'Next

        Dim itemsInCache = HttpContext.Current.Cache.GetEnumerator()

        While itemsInCache.MoveNext()
            HttpContext.Current.Cache.Remove(itemsInCache.Key)
        End While




    End Sub


End Class
