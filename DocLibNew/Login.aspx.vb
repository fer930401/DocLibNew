Imports System.Data
Imports Doclib.libreria
Partial Class Login
    Inherits System.Web.UI.Page
    ReadOnly _transac As New Doclib.libreria.Transacciones
    Protected Sub ddlEntidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEntidad.SelectedIndexChanged
        LlenaUsuarios()
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            LlenaEntidad()
            LlenaConfiguracion()
        End If
    End Sub
    Private Sub LlenaConfiguracion()
        Dim dtConf = _transac.ConsultaConfiguraciones()
        For Each o As DataRow In dtConf.Rows
            Page.Session("ruta") = o.Item("ruta").ToString()
        Next
    End Sub

    Private Sub LlenaEntidad()
        Dim dtTrans = _transac.ConsultaEntidad()
        ddlEntidad.DataSource = Nothing
        ddlEntidad.DataSource = dtTrans
        ddlEntidad.DataTextField = "nom1"
        ddlEntidad.DataValueField = "ef_cve"
        ddlEntidad.DataBind()
        ddlEntidad.Items(0).Selected = True
        LlenaUsuarios()
    End Sub
    Private Sub LlenaUsuarios()
        Dim dtUsr = _transac.ConsultaUsuarios(ddlEntidad.SelectedValue)
        ddlUsuarios.DataSource = Nothing
        ddlUsuarios.DataSource = dtUsr
        ddlUsuarios.DataTextField = "nombre"
        ddlUsuarios.DataValueField = "user_cve"
        ddlUsuarios.DataBind()
        ddlUsuarios.Items(1).Selected = True
        ddlUsuarios.Focus()
    End Sub
    Protected Sub btnLogin_Click(sender As Object, e As System.EventArgs) Handles btnLogin.Click
        Dim valida As Int16 = _transac.ValidaUsuario(ddlEntidad.SelectedValue, ddlUsuarios.SelectedValue, txtPassword.Text)
        If valida = 1 Then
            Session("efCve") = ddlEntidad.SelectedValue.ToString()
            Session("nomUsuario") = ddlUsuarios.SelectedItem.ToString()
            Session("usrCve") = ddlUsuarios.SelectedValue.ToString()
            Session("empresa") = ddlEntidad.SelectedItem.Text
            Response.Redirect("Consulta.aspx")
            DsTmp()
        Else
            lblError.Text = "El password es incorrecto"
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
        Session("dstemp2") = dsV
    End Sub
End Class
