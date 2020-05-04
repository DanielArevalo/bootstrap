<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table cellpadding="5" cellspacing="0" style="width: 80%">
        <tr>
            <td class="tdI">
            </td>
        </tr> 
        <tr>
            <td class="tdI" style="text-align:left">
                C&oacute;digo
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" enabled="false" 
                    Width="134px"></asp:TextBox>
            </td>
            <td class="tdD" style="text-align:left">
                 Tipo Cuota
                <asp:DropDownList ID="ddlTipoCuota" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="163px" AutoPostBack="True" 
                     onselectedindexchanged="ddlTipoCuota_SelectedIndexChanged">
                    <asp:ListItem value="1">Pago Único</asp:ListItem>
                    <asp:ListItem value="2">Serie Uniforme</asp:ListItem>
                    <asp:ListItem value="3">Gradiente</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Tipo Liquidación * 
                <asp:TextBox ID="txtTipLiq" runat="server" MaxLength="50" CssClass="textbox" Width="206px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLinea" runat="server" 
                    ErrorMessage="Campo Requerido" ControlToValidate="txtTipLiq" Display="Dynamic" 
                    ForeColor="Red" ValidationGroup="vgGuardar"/>
            </td>
            <td style="text-align:left">
                Tipo Amortización &#160;&#160;
                <asp:DropDownList ID="ddlTipoAmortizacion" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                       <asp:ListItem value="1">Pago Único</asp:ListItem>
                </asp:DropDownList> 
            </td>
        </tr>

           <tr>
            <td class="tdI" style="text-align:left">
               Tipo Interés &#160;&#160;
                <asp:DropDownList ID="ddlTipoInteres" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                       <asp:ListItem value="1">Simple</asp:ListItem>
                    <asp:ListItem value="2">Compuesto</asp:ListItem>
                </asp:DropDownList> 
            </td>
            <td style="text-align:left">
                Tipo Pago &#160;&#160;
                <asp:DropDownList ID="ddlTipoPago" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                      <asp:ListItem value="1">Anticipado</asp:ListItem>
                    <asp:ListItem value="2">Vencido</asp:ListItem>
                </asp:DropDownList> 
            </td>
        </tr>

    
           <tr>
            <td class="tdI" style="text-align:left">
               Cobro Interés Ajuste&#160;&#160;
                <asp:DropDownList ID="ddlCobroIntAju" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                     <asp:ListItem value="1">Descuento Desembolso</asp:ListItem>
                    <asp:ListItem value="2">Financiado</asp:ListItem>
                    <asp:ListItem value="3">Primera Cuota</asp:ListItem>
                </asp:DropDownList> 
            </td>
            <td style="text-align:left">
                Tipo Cuotas Extra &#160;&#160;
                <asp:DropDownList ID="ddlTipoCuotaExtra" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                      <asp:ListItem value="1">Si</asp:ListItem>
                    <asp:ListItem value="2">No</asp:ListItem>
                </asp:DropDownList> 
            </td>
        </tr>

         <tr>
            <td class="tdI" style="text-align:left">
               Tipo Interés Extra&#160;&#160;
                <asp:DropDownList ID="ddlTipoIntExtra" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                     <asp:ListItem value="1">Simple</asp:ListItem>
                    <asp:ListItem value="2">Compuesto</asp:ListItem>
                </asp:DropDownList> 
            </td>
            <td style="text-align:left">
                Tipo Pagos Extra &#160;&#160;
                <asp:DropDownList ID="ddlTipoPagoExtra" CssClass="dropdown"  runat="server" 
                    Height="25px" Width="238px">
                      <asp:ListItem value="1">Anticipado</asp:ListItem>
                    <asp:ListItem value="2">Vencido</asp:ListItem>
                </asp:DropDownList> 
            </td>
        </tr>
    
    </table>

</asp:Content>



