<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Usuario :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Tipo de Comprobante&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipocomprobante" runat="server" ControlToValidate="txtTipoComp" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtTipoComp" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                <asp:CheckBox ID="cbLocal" runat="server" Text="Local" Font-Size="X-Small" AutoPostBack="True" 
                    oncheckedchanged="cbLocal_CheckedChanged" />&nbsp;
                <asp:CheckBox ID="cbNif" runat="server" Text="NIF" Font-Size="X-Small" AutoPostBack="True"
                    oncheckedchanged="cbNif_CheckedChanged" />&nbsp;
                <asp:CheckBox ID="cbAmbos" runat="server" Text="Ambos" Font-Size="X-Small" AutoPostBack="True"
                    oncheckedchanged="cbAmbos_CheckedChanged" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>