<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - ProcesosCobro :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
            Código proceso precede&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtCodprocesoprecede" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
                <asp:CompareValidator ID="cvcodprocesoprecede" runat="server" ControlToValidate="txtCodprocesoprecede" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
            </td>
            <td class="tdD">
            Descripción&nbsp;*&nbsp;<br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" />
                <asp:RequiredFieldValidator ID="rfvdescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/>
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Rango inicial&nbsp;*&nbsp;<br />
            <asp:TextBox ID="txtRangoinicial" runat="server" CssClass="textbox" MaxLength="128" />
                <asp:RequiredFieldValidator ID="rfvrangoinicial" runat="server" ControlToValidate="txtRangoinicial" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/>
                <br />
                <asp:CompareValidator ID="cvrangoinicial" runat="server" ControlToValidate="txtRangoinicial" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
            </td>
            <td class="tdD">
            Rango final&nbsp;*&nbsp;<br />
            <asp:TextBox ID="txtRangofinal" runat="server" CssClass="textbox" MaxLength="128" />
                <asp:RequiredFieldValidator ID="rfvrangofinal" runat="server" ControlToValidate="txtRangofinal" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/>
                <br />
                <asp:CompareValidator ID="cvrangofinal" runat="server" ControlToValidate="txtRangofinal" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
    </table>
   <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtcodprocesocobro').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>