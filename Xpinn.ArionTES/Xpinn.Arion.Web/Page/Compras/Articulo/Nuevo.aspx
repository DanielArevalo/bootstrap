<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Areas :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Id Articulo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipocomprobante" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
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
            Serial:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtxtSerial" runat="server" ControlToValidate="txtSerial" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtSerial" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>

           <tr>
            <td class="tdI" style="text-align:left">
            Marca:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtxtMarca" runat="server" ControlToValidate="txtMarca" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtMarca" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>

           <tr>
            <td class="tdI" style="text-align:left">
            Referencia:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvReferencia" runat="server" ControlToValidate="txtReferencia" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtReferencia" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>

           <tr>
            <td class="tdI" style="text-align:left">
            Tipo Articulo:&nbsp;*&nbsp;<br />
            <asp:DropDownList ID="ddlTipoArticulo" runat="server" AutoPostBack="True" CssClass="textbox"
            OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged" Style="text-align: left"
                                                            Width="170px">
                                                        </asp:DropDownList>

            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>