<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipo Liquidaciòn :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
            Código*&nbsp;<asp:RequiredFieldValidator ID="rfvtipoliq" runat="server" ControlToValidate="txtTipoLiquidacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtTipoLiquidacion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
            Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Tipo de Cuota&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTipoCuota" runat="server" ControlToValidate="ddlTipoCuota" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="ddlTipoCuota" runat="server" CssClass="dropdown" 
                    Height="26px" Width="240px" AutoPostBack="True" 
                    onselectedindexchanged="ddlTipoCuota_SelectedIndexChanged">
                    <asp:ListItem Value="1">Pago Unico</asp:ListItem>
                    <asp:ListItem Value="2">Serie Uniforme</asp:ListItem>
                    <asp:ListItem Value="3">Gradiente</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left; height: 58px;" colspan="2">
                Tipo de Pago&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvTipoPago" runat="server" ControlToValidate="ddlTipoPago" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="dropdown" OnSelectedIndexChanged="ddlTipoPago_SelectedIndexChanged"
                    Height="26px" Width="240px">
                    <asp:ListItem Value="1">Anticipado</asp:ListItem>
                    <asp:ListItem Value="2">Vencido</asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD" style="height: 58px">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Tipo de Interés&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvTipoInt" runat="server" ControlToValidate="ddlTipoInteres" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlTipoInteres" runat="server" Height="26px" 
                    Width="240px" CssClass="dropdown">
                    <asp:ListItem Value="1">Simple</asp:ListItem>
                    <asp:ListItem Value="2">Compuesto</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Tipo de Amortización&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvTipAmo" runat="server" ControlToValidate="ddlTipoAmo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />
                <asp:DropDownList ID="ddlTipoAmo" runat="server" Height="26px" Width="240px" 
                    CssClass="dropdown">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left; height: 50px;" colspan="2">
            Tipo de Interés Anticipado Ajuste&nbsp;*&nbsp;
            <asp:RequiredFieldValidator ID="rfvIntAnt" runat="server" ControlToValidate="ddlTipoIntAnt" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" InitialValue="0" /><br />            
            <asp:DropDownList ID="ddlTipoIntAnt" runat="server" CssClass="dropdown" 
                    Height="26px" Width="240px">
                <asp:ListItem Value="0">Seleccione un Item..</asp:ListItem>
                <asp:ListItem Value="1">No Utiliza</asp:ListItem>
                <asp:ListItem Value="2">Descuento del Desembolso</asp:ListItem>
                <asp:ListItem Value="3">Lo Antes Posible</asp:ListItem>
                <asp:ListItem Value="4">Sumado al Monto</asp:ListItem>                
                <asp:ListItem Value="5">Financiado</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="tdD" style="height: 50px">
            </td>
        </tr>
        <tr>
            <td style="text-align:left; width: 144px;">
                Tipo de Gradiente<br />
                <asp:DropDownList ID="ddlTipoGra" runat="server" Height="26px" Width="240px">
                    <asp:ListItem Value="1">No Utiliza</asp:ListItem>
                    <asp:ListItem Value="2">Escalonado</asp:ListItem>
                    <asp:ListItem Value="3">Geométrico</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdI" style="text-align:left">
                Valor del Gradiente<br />
                <asp:TextBox ID="txtGradiente" runat="server" Height="22px" Width="120px"></asp:TextBox>
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left; width: 144px;" colspan="3">
                <asp:CheckBox ID="cbCobraIntDesembolso" runat="server" Text="Cobra todo el interes en el desembolso" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>