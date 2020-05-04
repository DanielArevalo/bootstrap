<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Concepto :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="60%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="logo" style="text-align: left">
                           Código<br/>   
                           <asp:TextBox ID="txtCentroCosto" runat="server" CssClass="textbox" Enabled="False" />
                       </td>
                       <td style="text-align: left">
                           <br/>
                           <asp:CheckBox ID="chbPrincipal" runat="server" Text="Principal" Enabled="False" />
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
                               Width="574px" Enabled="False" />
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
                               Width="574px" Enabled="False" />
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