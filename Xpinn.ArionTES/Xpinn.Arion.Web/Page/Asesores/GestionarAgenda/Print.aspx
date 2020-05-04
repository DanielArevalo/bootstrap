<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retencion en la Fuente</title>
</head>
<body>
    <form id="form1" runat="server" method="post" name="Imprimir">
    <center>
        <asp:Label ID="Label1" runat="server" Text="Su Mensaje se ha Enviado Exitosamente Cierre Esta Pantalla o Presione Aceptar Para Continuar"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Aceptar" OnClick="Button1_Click" /></center>
    </form>
</body>
</html>
