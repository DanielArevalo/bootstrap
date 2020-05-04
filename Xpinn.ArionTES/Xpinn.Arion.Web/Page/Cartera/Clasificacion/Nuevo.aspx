<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Clasificacion :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="80%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcodigo" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="8" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2" style="text-align:left">
                Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Tipo Histórico&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTipoHistorico" runat="server" ControlToValidate="ddlTipoHistorico" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />            
                <asp:DropDownList ID="ddlTipoHistorico" runat="server" CssClass="textbox"></asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Estado&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="txtEstado" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />            
                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" MaxLength="1" 
                    Width="50px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                <asp:CheckBox ID="cbLocal" runat="server" Text="Aportes son garantía en provisión" Font-Size="Small" AutoPostBack="True"  />
            </td>
            <td class="tdI" style="text-align:left">
                <asp:CheckBox ID="cbAporteClasif" runat="server" Text="Aportes son garantía en clasificación" Font-Size="Small" AutoPostBack="True"  />
            </td>
        </tr>
    </table>
</asp:Content>