<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Libreria de Documentos</title>
    <div data-role="header">
        <h1>Libreria de Documentos</h1>
    </div>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/css/custom.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
    <script lang="JavaScript">
        javascript: window.history.forward(1);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div data-role="content" data-inset="true">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional" Visible="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <fieldset>
                            <div id="page-wrapper">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Literal runat="server" ID="litMessage"></asp:Literal>
                                        <label for="entidad">
                                            Entidad:</label>
                                        <asp:DropDownList ID="ddlEntidad" runat="server" class="form-control" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <label for="usuario">
                                            Usuarios:</label>
                                        <asp:DropDownList ID="ddlUsuarios" runat="server" class="form-control">
                                        </asp:DropDownList>
                                        <label for="password">
                                            Password:</label>
                                        <asp:TextBox ID="txtPassword" Type="password" runat="server" class="form-control" required="" title="Inserta la contraseña por favor" placeholder="Password"></asp:TextBox>
                                        <hr />
                                        <asp:Button ID="btnLogin" runat="server" Text="Entrar" class="btn btn-danger btn-lg btn-block" />
                                        <br />
                                        <asp:Label ID="lblError" runat="server" Text="" class="icon-bar"></asp:Label><!-- agregar un alert con icono -->
                                    </div>
                                </div>
                            </div>

                        </fieldset>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEntidad" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
