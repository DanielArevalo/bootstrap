<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Concepto :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="60%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="logo" style="text-align: left">
                           Código<br/>   
                           <asp:TextBox ID="txtCentroCosto" runat="server" CssClass="textbox" />
                           <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ControlToValidate="txtCentroCosto"
                               Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                               Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                       </td>
                       <td style="text-align: left">
                           <br/>
                           <asp:CheckBox ID="chbPrincipal" runat="server" Text="Principal" />
                       </td>
                       <td>
                           &nbsp;
                       </td>
                       <td>
                           &nbsp;
                       </td>
                       <td class="tdI">
                       </td>
                       <td class="tdI">
                           &nbsp;
                       </td>
                       <td class="tdI">
                           &nbsp;</td>
                       <td class="tdI">
                           &nbsp;</td>
                       <td class="tdD">
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="8" style="text-align: left">
                           Nombre<br />
                           <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" 
                               Width="574px" />
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                               Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                               Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                           <br/>
                       </td>
                       <td class="tdD">
                           <br/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="8" style="text-align: left">
                           Descripción<br />
                           <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" 
                               Width="574px" />
                           <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                               Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                               Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                           <br/>
                       </td>
                       <td class="tdD">
                           <br/>
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>