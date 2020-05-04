<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - SolicitudCreditosRecogidos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Idsolicitudrecoge&nbsp;*<br />
                       <asp:TextBox ID="txtIdsolicitudrecoge" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Numerosolicitud&nbsp;*<br />
                       <asp:TextBox ID="txtNumerosolicitud" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Numero_recoge&nbsp;*<br />
                       <asp:TextBox ID="txtNumero_recoge" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fecharecoge&nbsp;<br />
                       <asp:TextBox ID="txtFecharecoge" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorrecoge&nbsp;<br />
                       <asp:TextBox ID="txtValorrecoge" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Fechapago&nbsp;<br />
                       <asp:TextBox ID="txtFechapago" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldocapital&nbsp;<br />
                       <asp:TextBox ID="txtSaldocapital" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Saldointcorr&nbsp;<br />
                       <asp:TextBox ID="txtSaldointcorr" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldointmora&nbsp;<br />
                       <asp:TextBox ID="txtSaldointmora" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Saldomipyme&nbsp;<br />
                       <asp:TextBox ID="txtSaldomipyme" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldoivamipyme&nbsp;<br />
                       <asp:TextBox ID="txtSaldoivamipyme" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Saldootros&nbsp;<br />
                       <asp:TextBox ID="txtSaldootros" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
    </table>
</asp:Content>