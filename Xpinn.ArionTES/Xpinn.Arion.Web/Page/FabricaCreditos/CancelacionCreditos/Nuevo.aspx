<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<%@ Register src="../../../General/Controles/mensajeGrabar.ascx" tagname="mensajegrabar" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel2" runat="server">
                            <table style="width: 80%;">
                                <tr>
                                    <td style="text-align: left">
                                        Fecha
                                        <br />
                                        <ucFecha:fecha ID="txtFecha" runat="server" AutoPostBack="True" CssClass="textbox"
                                            MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" colspan="3" style="text-align: left">
                                        <strong>DATOS DEL DEUDOR</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Identificación
                                    </td>
                                    <td style="text-align: left">
                                        Tipo Identificación
                                    </td>
                                    <td style="text-align: left">
                                        Nombres y Apellidos
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" 
                                            Width="329px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table style="width: 80%;">
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 50%;">
                                <tr>
                                    <td colspan="5" style="text-align: left">
                                        <strong>DATOS DEL CRÉDITO</strong>&nbsp;&nbsp;
                                        <asp:TextBox ID="txtNumero_solicitud" runat="server" CssClass="textbox" Enabled="False"
                                            Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 166px;">
                                        Número Radicación
                                    </td>
                                    <td style="text-align: left" colspan="2">
                                        Línea de Crédito
                                    </td>
                                    <td style="text-align: left">
                                        Fecha Solicitud
                                    </td>
                                    <td style="text-align: left">
                                        Fecha Aprobacion
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 166px;">
                                        &nbsp;<asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left" colspan="2">
                                        <asp:DropDownList ID="ddlLineas" runat="server" CssClass="textbox" Enabled="False" Width="98%"
                                            Height="27px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 166px;">
                                        Monto Solicitado
                                    </td>
                                    <td style="text-align: left; width: 166px;">
                                        Monto Aprobado
                                    </td>
                                    <td style="text-align: left; height: 21px;">
                                        Valor de la Cuota
                                    </td>
                                    <td style="text-align: left; height: 21px;">
                                        Num.Cuotas
                                    </td>
                                    <td style="text-align: left; height: 21px;">
                                        Periodicidad
                                    </td>
                                    <td style="text-align: left; height: 21px;">
                                        Forma de Pago
                                    </td>
                                </tr>
                                <tr>                                
                                    <td style="text-align: left; width: 166px;">
                                        <uc2:decimales ID="txtMontoSolicitado" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left; width: 166px;">
                                        <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtValor_cuota" runat="server" CssClass="textbox" Enabled="false" style="text-align: right" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtNumCuotas" runat="server" CssClass="textbox" 
                                            Enabled="false" Width="72px" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                                            Enabled="false" Width="120px" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" 
                                            Enabled="false" Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table style="width: 80%;">
                <tr>
                    <td colspan="2" style="text-align: left">
                        <strong>TIPO DE CANCELACIÓN</strong>&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" width="55%">
                        Observaciones<br />
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Width="100%"
                            Enabled="true" />
                    </td>
                    <td style="text-align: left" width="5%">
                        &nbsp;&nbsp;
                    </td>
                    <td style="text-align: left" width="45%">
                        <br />
                        <asp:RadioButtonList ID="rbEstado" runat="server" RepeatDirection="Horizontal" Width="100%">
                            <asp:ListItem Selected="True" Value="1">Cancelar</asp:ListItem>
                            <asp:ListItem Value="2">A Solicitar</asp:ListItem>
                            <asp:ListItem Value="3">Refencias Verificadas</asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:Label ID="lblInfo" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 80%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Datos Modificados Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>
