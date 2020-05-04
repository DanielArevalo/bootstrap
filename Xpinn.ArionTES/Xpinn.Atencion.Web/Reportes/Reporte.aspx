<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reporte.aspx.cs" Inherits="Reporte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte</title>
    <style type="text/css">       
        @media print {
          size: letter landscape; 
        }
    </style>
</head>
<body">    
    <form id="form1" runat="server" method="post" name="Imprimir">                
    </form>
</body>
<script type="text/javascript">


    window.onload = function imprimir() {
        var pdfFile = 'Reportes/imprimir.pdf';
        window.open(pdfFile).print();

    }
</script>
</html>
