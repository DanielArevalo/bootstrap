<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - CosteoProductos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_margen&nbsp;*<br />
                       <asp:TextBox ID="txtCod_margen" runat="server" CssClass="textbox" Enabled="false"/>
                           <br />
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtCod_costeo" runat="server" CssClass="textbox" Enabled="false" 
                               Visible="False"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Materiaprima&nbsp;*<br />
                       <asp:TextBox ID="txtMateriaprima" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Unidadcompra&nbsp;<br />
                       <asp:TextBox ID="txtUnidadcompra" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Costounidad&nbsp;<br />
                       <asp:TextBox ID="txtCostounidad" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Cantidad&nbsp;<br />
                       <asp:TextBox ID="txtCantidad" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Costo&nbsp;<br />
                       <asp:TextBox ID="txtCosto" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
</asp:Content>