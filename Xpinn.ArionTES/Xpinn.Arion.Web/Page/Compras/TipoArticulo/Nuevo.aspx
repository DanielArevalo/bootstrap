<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Areas :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Id Tipo Articulo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipocomprobante" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
            Descripción :&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>


          <tr>
            <td class="tdI" style="text-align:left">
            Dias Periodicidad:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCentroCosto" runat="server" ControlToValidate="txtDias" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDias" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>