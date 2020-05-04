<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CuotasExtras :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_cuota&nbsp;*&nbsp;<br />
                       <asp:TextBox ID="txtCod_cuota" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" />
                       </td>
                       <td class="tdD">
                       Numero_radicacion&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNUMERO_RADICACION" runat="server" ControlToValidate="txtNumero_radicacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server" ControlToValidate="txtNumero_radicacion" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Fecha_pago&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvFECHA_PAGO" runat="server" ControlToValidate="txtFecha_pago" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvFECHA_PAGO" runat="server" ControlToValidate="txtFecha_pago" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtFecha_pago" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Valor&nbsp;&nbsp;<asp:CompareValidator ID="cvVALOR" runat="server" ControlToValidate="txtValor" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valor_capital&nbsp;&nbsp;<asp:CompareValidator ID="cvVALOR_CAPITAL" runat="server" ControlToValidate="txtValor_capital" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValor_capital" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Valor_interes&nbsp;&nbsp;<asp:CompareValidator ID="cvVALOR_INTERES" runat="server" ControlToValidate="txtValor_interes" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValor_interes" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldo_capital&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDO_CAPITAL" runat="server" ControlToValidate="txtSaldo_capital" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldo_capital" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Saldo_interes&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDO_INTERES" runat="server" ControlToValidate="txtSaldo_interes" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldo_interes" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Forma_pago&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
 <%--   <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_CUOTA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>