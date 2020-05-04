<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Perfil :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="90%" >
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI">
                C&oacute;digo&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvcodperfil" runat="server" ControlToValidate="txtCodperfil" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvcodperfil" runat="server" ControlToValidate="txtCodperfil" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtCodperfil" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Nombre&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvnombreperfil" runat="server" ControlToValidate="txtNombreperfil"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic"/>
                <br />
                <asp:TextBox ID="txtNombreperfil" runat="server" CssClass="textbox" MaxLength="128" Width="300px" />
            </td>
            <td class="tdD">
                Periodicidad de Cambio de Clave&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreperfil" 
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/>
                <br />
                <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="220px" CssClass="textbox" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4" style="text-align:left">
                <br />
                <asp:CheckBox ID="cbAdmin" runat="server" Checked="false" AutoPostBack="true" Text="Es administrador"></asp:CheckBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="90%" >
        <tr>
            <td colspan="4">
                <br /><span><strong>Parámetros de Claves:</strong></span>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <label>Longitud de Clave</label><br />
                <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdCC"><br/>
                <asp:CheckBox ID="cbCaracter" runat="server" Checked="false" AutoPostBack="false" Text="Caracter especial en la clave"></asp:CheckBox>
            </td>
            <td class="tdBC"><br/>
                <asp:CheckBox ID="cbNumero" runat="server" Checked="false" AutoPostBack="false" Text="Número en la clave"></asp:CheckBox>
            </td>
            <td class="tdMC"><br/>
                <asp:CheckBox ID="cbMayuscula" runat="server" Checked="false" AutoPostBack="false" Text="Mayuscula en la clave"></asp:CheckBox>
            </td>
        </table>
    <%--   <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCodperfil').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>