<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - LineasCredito :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_linea_credito&nbsp;*<br />
                       <asp:TextBox ID="txtCod_linea_credito" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Nombre&nbsp;<br />
                       <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo_linea&nbsp;<br />
                       <asp:TextBox ID="txtTipo_linea" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Tipo_liquidacion&nbsp;*<br />
                       <asp:TextBox ID="txtTipo_liquidacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo_cupo&nbsp;<br />
                       <asp:TextBox ID="txtTipo_cupo" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Recoge_saldos&nbsp;<br />
                       <asp:TextBox ID="txtRecoge_saldos" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cobra_mora&nbsp;<br />
                       <asp:TextBox ID="txtCobra_mora" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Tipo_refinancia&nbsp;<br />
                       <asp:TextBox ID="txtTipo_refinancia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Minimo_refinancia&nbsp;<br />
                       <asp:TextBox ID="txtMinimo_refinancia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Maximo_refinancia&nbsp;<br />
                       <asp:TextBox ID="txtMaximo_refinancia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Maneja_pergracia&nbsp;<br />
                       <asp:TextBox ID="txtManeja_pergracia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Periodo_gracia&nbsp;<br />
                       <asp:TextBox ID="txtPeriodo_gracia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo_periodic_gracia&nbsp;<br />
                       <asp:TextBox ID="txtTipo_periodic_gracia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Modifica_datos&nbsp;<br />
                       <asp:TextBox ID="txtModifica_datos" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Modifica_fecha_pago&nbsp;<br />
                       <asp:TextBox ID="txtModifica_fecha_pago" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Garantia_requerida&nbsp;<br />
                       <asp:TextBox ID="txtGarantia_requerida" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo_capitalizacion&nbsp;<br />
                       <asp:TextBox ID="txtTipo_capitalizacion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Cuotas_extras&nbsp;<br />
                       <asp:TextBox ID="txtCuotas_extras" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cod_clasifica&nbsp;<br />
                       <asp:TextBox ID="txtCod_clasifica" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Numero_codeudores&nbsp;<br />
                       <asp:TextBox ID="txtNumero_codeudores" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cod_moneda&nbsp;<br />
                       <asp:TextBox ID="txtCod_moneda" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Porc_corto&nbsp;<br />
                       <asp:TextBox ID="txtPorc_corto" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo_amortiza&nbsp;*<br />
                       <asp:TextBox ID="txtTipo_amortiza" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Estado&nbsp;<br />
                       <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
    </table>
</asp:Content>