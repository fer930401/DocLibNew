<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Documento.aspx.vb" Inherits="Documento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="css/jquery-1.4.3.min.js"></script>
    <%--<script src="css/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="css/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="css/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen"/>--%>
  <%--  <script type="text/javascript" language = "javascript" > 
    function doIframe(){ 
        o = document.getElementsByTagName("iframe"); 
        for(i=0;i<o.length;i++){ <script type="text/javascript" src="css/bootstrap.file-input.js"></script>
            if (/\bautoHeight\b/.test(o[i].className)){
                removeEvent(o[i],'load', doIframe);
                setHeight(o[i]); 
                addEvent(o[i],"load", doIframe); 
            } 
        } 
    } 
    function setHeight(e){ 
        if(e.contentDocument){ 
            e.height = e.contentDocument.body.offsetHeight + 35; 
        } else { 
            e.height = e.contentWindow.document.body.scrollHeight; 
        } 
    }
    function addEvent(obj, evType, fn, useCapture) {
        if (isNaN(useCapture)) useCapture = false;
        if (obj.addEventListener) {
            obj.addEventListener(evType, fn, useCapture);
            return true;
        } else if (obj.attachEvent) {
            var r = obj.attachEvent("on" + evType, fn);
            return r;
        } else {
            return false; 
        }
    }
    function removeEvent(obj, evType, fn, useCapture) {
        if (isNaN(useCapture)) useCapture = false;
        if (obj.removeEventListener) {
            obj.removeEventListener(evType, fn, useCapture);
            return true;
        } else if (obj.detachEvent) {
            var r = obj.detachEvent("on" + evType, fn);
            return r;
        } else {
            return false; 
        }
    }
    </script> --%>
    
    <script type="text/javascript">
        function redimensionaPadre(){
            $('#myPDF', window.parent.document).css('height', jQuery('body').css('height'));
            }

            $(document).ready(function() {
            redimensionaPadre();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <iframe runat="server" id="myPDF" width="900px" height="1000px" src="#"></iframe>
           <%--<iframe id = "myPDF" frameborder = "0" scrolling = "no" runat ="server" class = "IframeCarga autoHeight" visible = "false" src = "/dirección" ></iframe> --%>
    </div>
    </form>
</body>
</html>
