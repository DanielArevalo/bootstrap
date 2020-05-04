<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pDatos" runat="server">
                        <table style="text-align: center" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="text-align: left; width: 140px">Fecha Cancelación<br />
                                    <ucFecha:fecha ID="txtFechaCancelacion" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 150px">Num. Servicio<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Solicitud<br />
                                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Aprobación<br />
                                    <ucFecha:fecha ID="txtFechaAproba" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 320px" colspan="2" rowspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">Solicitante<br />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="50px" Visible="false" />
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                        Width="100px"  />
                                </td>
                                <td style="text-align: left; width: 280px" colspan="2">Nombre<br />
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="90%" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                        Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 280px" colspan="2">Linea de Servicio<br />
                                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="260px" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td style="text-align: left; width: 300px" colspan="2">Plan<br />
                                    <asp:DropDownList ID="ddlPlan" runat="server" CssClass="textbox" Width="240px" AppendDataBoundItems="True" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">Fec. Inicio Vigencia<br />
                                    <ucFecha:fecha ID="txtFecIni" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Final Vigencia<br />
                                    <ucFecha:fecha ID="txtFecFin" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 140px">Nro Poliza/Contrato
                                    <asp:TextBox ID="txtNroPoliza" runat="server" CssClass="textbox" Width="90%" />
                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtNroPoliza"
                                        FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                </td>
                                <td style="text-align: left; width: 160px">Valor Total<br />
                                    <uc1:decimales ID="txtValorTotal" runat="server" />
                                </td>
                                <td style="text-align: left; width: 160px">Saldo<br />
                                    <uc1:decimales ID="txtSaldo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 140px">Fec. Prox Pago<br />
                                    <ucFecha:fecha ID="txtFecProxPago" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 215px">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 48%">
                                                Num. Cuotas<br />
                                                <asp:TextBox ID="txtNumCuotas" runat="server" CssClass="textbox" Width="60%" />
                                                <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtNumCuotas"
                                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                            </td>
                                            <td style="width: 52%">
                                                Ctas Pendientes<br />
                                                <asp:TextBox ID="txtCuotasPendientes" runat="server" CssClass="textbox" Width="60%" />
                                                <asp:FilteredTextBoxExtender ID="fte3" runat="server" TargetControlID="txtCuotasPendientes"
                                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left; width: 140px">Valor Cuota<br />
                                    <uc1:decimales ID="txtValorCuota" runat="server" />
                                </td>
                                <td style="text-align: left; width: 160px">Periodicidad<br />
                                    <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 160px">Forma de Pago<br />
                                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <hr style="width: 100%" />
            
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color: Red">
                            <asp:Label ID="lblMensajeConfirmacion" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>

