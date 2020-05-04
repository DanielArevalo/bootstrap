<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 740px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 150px">
                        Número<br />
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Concepto<br />
                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" Width="270px"></asp:TextBox>                       
                    </td>                    
                    <td style="text-align: left; width: 140px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 160px">
                        &nbsp;
                    </td>                    
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px">
                        Identificación<br />
                        <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="50px" Visible="false" />
                        <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                            Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                        <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                            OnClick="btnConsultaPersonas_Click" Text="..." />
                    </td>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Nombre<br />
                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                        <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="270px" />
                        <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                            Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                            Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 160px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px">
                        Fec. Devolución<br />
                        <ucFecha:fecha ID="txtFechaDev" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Valor<br />
                        <uc1:decimales ID="txtValor" runat="server" />                       
                    </td>
                    <td style="text-align: left; width: 140px">
                        Saldo<br />
                        <uc1:decimales ID="txtSaldo" runat="server" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Origen<br />
                         <asp:TextBox ID="txtOrigen" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Estado<br />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="90%" 
                            AppendDataBoundItems="True" /> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px">
                        Fec. Descuento<br />
                        <ucFecha:fecha ID="txtFecDescuento" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Num. Recaudo<br />
                        <asp:TextBox ID="txtNumRecaudo" runat="server" CssClass="textbox" Width="90%" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtNumRecaudo"
                            FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                    </td>
                    <td style="text-align: left; width: 140px">
                       ID. Detalle
                        <asp:TextBox ID="txtIdDetalle" runat="server" CssClass="textbox" Width="90%" 
                            AutoPostBack="True" ontextchanged="txtIdDetalle_TextChanged" />
                        <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtIdDetalle"
                            FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                    </td>
                    <td style="text-align: left; width: 300px" colspan="2">
                        Detalle Recaudo<br />
                        <asp:TextBox ID="txtDetalleRec" runat="server" CssClass="textbox" ReadOnly="true"
                            Width="230px" />
                    </td>
                </tr>              
            </table>

        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color:Red">
                            La Devolución fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
  
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
     
</asp:Content>