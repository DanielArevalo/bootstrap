<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Tipo Soporte :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Tipo de Soporte<br />
                <asp:TextBox ID="txtTipSopCaj" runat="server" CssClass="textbox" MaxLength="128" Enabled="False" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Descripción<br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" Enabled="False" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Cod.Cuenta<br />            
                <asp:DropDownList ID="ddlCodCuenta" runat="server" AppendDataBoundItems="True"  Enabled="False"
                    CssClass="dropdown" style="font-size:xx-small; text-align:left" Height="24" 
                    Width="120px">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>