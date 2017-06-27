<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ingresar.aspx.vb" Inherits="Ingresar" %>
 
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
    <link href="css/UplFile.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
    <script lang="JavaScript" type="text/javascript">
        javascript: window.history.forward(1); 
    </script>
    
    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('< Response.Write(UpdateProgress1.ClientID) %>').style.display = "inline";
        }
    </script>

     <script lang="javascript" type="text/javascript">
         var currentIndex = 1;
         function addUploadField() {
             currentIndex++;

             if (currentIndex > 1) {
                 var iAnt = currentIndex - 1;

                 var sPaso = document.getElementById("ddlTipo" + iAnt).innerText;

                 if (sPaso.trim() == "") {
                     sPaso = document.getElementById("ddlTipo" + iAnt).textContent;
                 }

                 if (document.getElementById("ddlTipo" + iAnt).childElementCount == 1 && sPaso.trim() != "Comunicaciones del cliente") {
                     currentIndex--;
                     return;
                 }
             }

             var newRow = '<tr>';
             newRow += '<td><select id="ddlTipo' + currentIndex + '" name="ddlTipo' + currentIndex + '" class="form-control" /></td>';
             newRow += '<td><div class="fileUpload btn btn-danger"><span>Examinar...</span><input type="file" name="file' + currentIndex + '" id="file' + currentIndex + '" class="upload" onchange="nombre(this.value, this.id);load_image(this.id,this.value);"/></div><span id="lbl' + currentIndex + '"></span></td>';
             newRow += '<td><input type="button" value="Remove" onclick="return removeUploadField(this)" class="btn btn-danger"/></td>';
             newRow += '</tr>';
             newRow = $(newRow);
             newRow.insertBefore($('#trUploadRow'));
             var numeroant = currentIndex - 1;
             var nombre = "ddlTipo" + numeroant;
             var obj = document.getElementById(nombre).innerHTML;
             var second = document.getElementById("ddlTipo" + currentIndex);
             second.innerHTML = obj;
             var nueva = document.forms["form1"][nombre].options;
             var total = nueva.childElementCount;
             if (total == null)
             { total = nueva.length; }

             var aBorrar = document.getElementById("ddlTipo" + currentIndex);

             var nuevaB = document.forms["form1"]["ddlTipo" + currentIndex].options;
             var totalB = nueva.childElementCount;
             if (totalB == null)
             { totalB = nuevaB.length; }

             var bBorrar = document.getElementById("ddlTipo" + numeroant);

             bBorrar.disabled = true;

             for (var i = 0; i < totalB; i++) {
                 if (i == bBorrar.selectedIndex) {
                     if (bBorrar.value != 7)
                         aBorrar.remove(i);
                 }
             }
         }

         function removeUploadField(e) {
             document.getElementById("ddlTipo" + currentIndex).value = null;
             currentIndex--;
             $(e.parentNode.parentNode).remove();
         }
     </script>
    <script type="text/javascript">
        function HabiliarCombos() {

            for (var i = 1; i < currentIndex; i++) {
                document.getElementById("ddlTipo" + i.toString()).disabled = false;
            }

        }
    </script>
    <script type="text/javascript">
        function nombre(fic, name) {
            if (name == "")
                return;

            fic = fic.split('\\');
            var index = parseInt(name.replace("file", ""));
            //var indexBis = index.trim();
            //var numEtiqueta = indexBis + 1;
            //var nombreEtiqueta = "lbl" + numEtiqueta.toString(); //+ currentIndex.toString();
            //var nombreEtiqueta = index + 1;
            var nombreEtiqueta = "lbl" + index.toString(); //+ currentIndex.toString();
            document.getElementById(nombreEtiqueta).innerText = fic[fic.length - 1].toString();
        }
        function validaFiles() {
            var listaInputs = document.querySelectorAll("#tblArchivos input");
            var sw = true;
            for (var i = 0; i < listaInputs.length; i++) {
                var inputItem = listaInputs[i];
                if (inputItem.type == "file"){
                    var valorInput = inputItem.value;
                    if (valorInput == ""){
                        alert('falta ingresar archivos');
                        sw = false;
                        
                    }
                }
            }

            if (sw == false) {
                return false;
            }
            else {
                HabiliarCombos();
                ShowProgress();
            }

            
          
        }
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




    <script type="text/javascript">
        function load_image(id, ext) {

            if (id == "")
                return;

            var index = parseInt(id.replace("file", ""));
            var objeto = document.getElementById("ddlTipo" + index)
            var descripcion = objeto.children[objeto.selectedIndex].innerText;
            if (descripcion == "XML") {
                if (validateExtension(ext) == false) {
                    alert("Solo archivos XML");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                    return;
                }
            }
           
        }

        function validateExtension(v) {
            var allowedExtensions = new Array("XML", "xml", "Xml");
            for (var ct = 0; ct < allowedExtensions.length; ct++) {
                sample = v.lastIndexOf(allowedExtensions[ct]);
                if (sample != -1) { return true; }
            }
            return false;
        }
</script>


</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
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
            <div id="page-wrapper">
                <div id="page-inner">
                    <div class="row">
                        <div class="col-md-12">
                            <h2>Ingresar Imagen</h2>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <hr />
                    <div class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upnl1" runat="server">
                            <ContentTemplate>


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Tipo Documento</label>
                                        <asp:DropDownList ID="ddlDocumentos" runat="server" class="form-control" AutoPostBack="False">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Folio</label>
                                        <asp:TextBox ID="txtFolio" runat="server" placeholder="Introduzca el número de folio" class="form-control" onblur="__doPostBack('txtFolio','OnBlur');"
                                            onkeypress="onlyNumbersF(event);"></asp:TextBox>
                                        
                                    </div>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="txtFolio" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Fecha    dd/mm/yyyy</label>
                                <asp:Label ID="lblFecha" runat="server" Text="" class="form-control"></asp:Label>
                            </div>

                        </div>


                    </div>


                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblRegistrar" runat="server" Text="" Visible="False">Registrar documento</asp:Label>
                                <asp:UpdatePanel runat="server" ID="upUfiles" ChildrenAsTriggers="" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnRegistrar" OnClientClick="return validaFiles()" runat="server" Text="Registrar" class="btn btn-danger btn-lg btn-block"
                                            Visible="false" />
                                        <%--<asp:Button ID="btnRegistrar" OnClientClick="return validaFiles(); HabiliarCombos(); ShowProgress();" runat="server" Text="Registrar" class="btn btn-danger btn-lg btn-block"
                                            Visible="false" />--%>
                                     <%--   validaFiles();--%>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnRegistrar" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblError" runat="server" Text="Label" Visible="False"></asp:Label>

                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">

                                <br />

                                <table id="tblArchivos" class="table table-striped table-bordered table-hover">
                                    <tr>
                                        <td colspan="3">
                                            <input type="button" value="Clic para agregar otro archivo" onclick="return addUploadField()" class="btn btn-danger btn-lg btn-block" />
                                        </td>
                                    </tr>
                                    <tr>

                                        <td style="width: 57%"> <!-- class="style2">-->
                                            <select id="ddlTipo1" runat="server" name="ddlTipo1" class="form-control">
                                               <%-- <option default>Seleccione una opcion</option>--%>
                                            </select> 

                                        </td>
                                        <td style="width: 43%">

                                            <div class="fileUpload btn btn-danger">
                                                <span>Examinar...</span>
                                                <input type="file" name="file1" id="file1" class="upload" onchange="nombre(this.value, this.id);load_image(this.id,this.value);" />

                                            </div>
                                            <span id="lbl1"></span>
                                            <%--runat="server"--%>
                                            <%--<asp:RequiredFieldValidator 
                                             id="RequiredFieldValidator1" runat="server" 
                                             ErrorMessage="This is a required field!" 
                                             ControlToValidate="file1"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td class="style1"></td>
                                    </tr>
                                    <tr id="trUploadRow">
                                        <td colspan="3" />

                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upUfiles">
            <ProgressTemplate>
                <div class="overlay" />
                <div class="overlayContent">
                    <h2>Subiendo archivos espere un momento...</h2>
                    <img src="assets/img/loadingEngranes.gif" alt="Loading" border="0" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <script src="assets/js/jquery-1.10.2.js"></script>
        <script src="assets/js/bootstrap.min.js"></script>
        <script src="assets/js/jquery.metisMenu.js"></script>
        <script src="assets/js/custom.js"></script>



    </form>    
</body>
</html>

