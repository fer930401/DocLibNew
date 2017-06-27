<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Archivo.aspx.vb" Inherits="Archivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
     <!-- BOOTSTRAP STYLES-->
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <!-- FONTAWESOME STYLES-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <!-- CUSTOM STYLES-->
    <link href="assets/css/custom.css" rel="stylesheet" />
    <!-- GOOGLE FONTS-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />

    <script src="assets/js/jquery-1.10.2.js"></script>
    <SCRIPT LANGUAGE="JavaScript" TYPE="text/javascript">
	<!--

        /* The actual print function */

//        function prePrint() {
//            VBS = true;
//            if (window.print) window.print();
//            else if (VBS) printIt();
//            else alert('This script does not work in your browser');
//        }

        $(window).load(function () {
            window.print();
        });
        
	// -->
</SCRIPT>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" />
<%--        <iframe runat="server" id="myPDF" src="#"></iframe>--%>
    </div>
    
     <script src="assets/js/jquery-1.10.2.js"></script>
    <!-- BOOTSTRAP SCRIPTS -->
    <script src="assets/js/bootstrap.min.js"></script>
    <!-- METISMENU SCRIPTS -->
    <script src="assets/js/jquery.metisMenu.js"></script>
    <!-- CUSTOM SCRIPTS -->
    <script src="assets/js/custom.js"></script>
    </form>
</body>
</html>

