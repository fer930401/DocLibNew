<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Consulta.aspx.vb" Inherits="Consulta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Libreria de Documentos</title>
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/css/custom.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />

    <script lang="JavaScript">
        javascript: window.history.forward(1);
    </script>
    
    <script type="text/javascript">
        /*solo enteros*/
        function onlyNumbersF(e) {
            var val = (document.all);
            var key = val ? e.keyCode : e.which;
            if (key > 31 && (key < 48 || key > 57)) {
                if (val)
                    window.event.keyCode = 0;
                else {
                    e.stopPropagation();
                    e.preventDefault();
                }
            }
        }

 </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        </asp:ScriptManager>
        <div id="wrapper">
            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="adjust-nav">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand"><i class="fa fa-square-o "></i>&nbsp;LIBRERIA DE DOCUMENTOS</a>


                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a id="lnkSalir" runat="server" onclic="SalirInicio">SALIR</a></li>
                        </ul>
                    </div>

                </div>
            </div>
            <nav class="navbar-default navbar-side" role="navigation">
                <div class="sidebar-collapse">
                    <ul class="nav" id="main-menu">
                        <li class="text-center user-image-back">

                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label><br />
                            <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>
                            <img src="assets/img/find_user.png" class="img-responsive" />

                        </li>
                        <li>
                            <a id="lnkConsulta" runat="server" onclic="btnConsultarIr"><i class="fa fa-edit "></i>Consultar Documento</a>
                        </li>
                        <li>
                            <a id="lnkIngreso" runat="server" onclic="btnIngresarIr"><i class="fa fa-edit "></i>Ingresar Documento</a>
                        </li>

                    </ul>

                </div>

            </nav>
        </div>
        <asp:UpdatePanel ID="upnl2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div id="page-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <h2>Recuperar Imagen</h2>
                            </div>
                        </div>
                        <div class="row">
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Tipo Documento</label>
                                    <asp:DropDownList ID="ddlDocumentos" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Folio</label>
                                    <asp:TextBox ID="txtFolio" runat="server" class="form-control"  onkeypress="onlyNumbersF(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Da click en el boton</label>
                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" class="btn btn-danger btn-lg btn-block" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="lblError" runat="server" Text="Label" Visible="False"></asp:Label>
                                <asp:GridView ID="grdDocDescarga" Visible="False" runat="server" AutoGenerateColumns="False"
                                    class="table table-striped table-bordered table-hover"
                                    OnRowCommand="GridView1_RowCommand"
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="archivo" HeaderText="Documento" ItemStyle-Width="300px" />
                                        <asp:BoundField DataField="archivoBis" HeaderText="Archivo" />
                                        <asp:BoundField DataField="fec_design" HeaderText="Fecha" />
                                        <asp:TemplateField HeaderText="Ver" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnVer" runat="server" Text="Ver" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="Ver" class="btn btn-danger btn-lg btn-block"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Imprimir" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnImprimir" Text="Imprimir" runat="server" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="Imprimir" class="btn btn-danger btn-lg btn-block"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Borrar" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="Borrar" class="btn btn-danger btn-lg btn-block"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <script src="assets/js/jquery-1.10.2.js"></script>
        <script src="assets/js/bootstrap.min.js"></script>
        <script src="assets/js/jquery.metisMenu.js"></script>
        <script src="assets/js/custom.js"></script>
        <script src="assets/js/jquery.printPage.js" type="text/javascript"></script>

    </form>
</body>
</html>
