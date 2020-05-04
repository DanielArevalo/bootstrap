<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - CuotasExtras :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_cuota&nbsp;*<br />
                       <asp:TextBox ID="txtCod_cuota" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Numero_radicacion&nbsp;*<br />
                       <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Fecha_pago&nbsp;*<br />
                       <asp:TextBox ID="txtFecha_pago" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Valor&nbsp;<br />
                       <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valor_capital&nbsp;<br />
                       <asp:TextBox ID="txtValor_capital" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Valor_interes&nbsp;<br />
                       <asp:TextBox ID="txtValor_interes" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldo_capital&nbsp;<br />
                       <asp:TextBox ID="txtSaldo_capital" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Saldo_interes&nbsp;<br />
                       <asp:TextBox ID="txtSaldo_interes" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Forma_pago&nbsp;<br />
                       <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
</asp:Content>