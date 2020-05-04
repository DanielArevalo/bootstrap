<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Proceso :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                C&oacute;digo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcod_proceso" runat="server" ControlToValidate="txtCod_proceso" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvcod_proceso" runat="server" ControlToValidate="txtCod_proceso" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtCod_proceso" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvnombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                M&oacute;dulo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcod_modulo" runat="server" ControlToValidate="txtCod_modulo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="txtCod_modulo" runat="server" CssClass="dropdown" />
            </td>
            <td class="tdD">
                &nbsp;
            </td>
    </table>
  <%--  <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCod_proceso').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>