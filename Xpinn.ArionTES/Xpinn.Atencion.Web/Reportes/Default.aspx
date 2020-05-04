<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_Reportes_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <iframe id="frmPrint" name="IframeName" width="100%" src="../Reportes/imprimir.pdf"
                                                        height="500px" runat="server" style="border-style: groove; float: left;"></iframe>
    </div>
    </form>
</body>


<script type="text/javascript">


    window.onload = function imprimir() {
        var pdfFile = document.getElementById('frmPrint').contentWindow.print();

    }
</script>
</html>
