﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table style="width: 740px; text-align: center">
                        <tr>
                            <td style="text-align: left; width: 140px" colspan="4">Fec. Solicitud<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 246px">Solicitante<br />
                                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                                <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                    Width="80%" OnTextChanged="txtIdPersona_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="fte121" runat="server" TargetControlID="txtIdPersona"
                                    FilterType="Custom, Numbers" ValidChars="-" />
                                <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                    OnClick="btnConsultaPersonas_Click" Text="..." />
                            </td>
                            <td style="text-align: left; width: 370px" colspan="2">Nombre<br />
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" OneventotxtIdentificacion_TextChanged="txtIdPersona_TextChanged" />
                                <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="350px" />
                                <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                    Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                    Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                            </td>

                        </tr>
                        <tr>

                            <td style="text-align: center; width: 185px">Número Linea Telefónica<br />
                                <asp:TextBox ID="txtNumeroLineaTel" runat="server" CssClass="textbox" Width="80%" Enabled="true" />
                            </td>
                            <td style="text-align: center; width: 185px">Linea de Servicio<br />
                                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="80%" 
                                    AutoPostBack="True" />
                            </td>
                            <td style="text-align: center; width: 185px">Tipo Plan<br />
                                <asp:DropDownList ID="ddlPlan" runat="server" CssClass="textbox" Width="80%" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td style="text-align: center; width: 185px">Valor Cuota Aprox.<br />
                                <asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtValorCuota"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                        </tr>
                        <tr>

                            <td style="text-align: center; width: 185px">Fec. Primer Cuota<br />
                                <ucFecha:fecha ID="txtFecPrimercuota" runat="server" CssClass="textbox" Width="80%" />
                            </td>
                            <td style="text-align: center; width: 185px">Fec. Vencimiento<br />
                                <ucFecha:fecha ID="txtFecVencimiento" runat="server" CssClass="textbox" Width="80%" />
                            </td>
                            <td style="text-align: center; width: 185px">Periodicidad<br />
                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" AutoPostBack="true" CssClass="textbox" Width="80%" />
                            </td>
                            <td style="text-align: center; width: 185px">Forma de Pago<br />
                              <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Width="80%" />                                
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="text-align: center; width: 185px; display:none">Valor Compra Entidad<br />
                                <asp:TextBox ID="textvalorentidad" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" OnTextChanged="txtcalcular_TextChanged" AutoPostBack="true"/>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtValorCuota"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                            <td style="text-align: center; width: 185px; display:none">Valor Mercado<br />
                                <asp:TextBox ID="textvalormercado" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" OnTextChanged="txtcalcular_TextChanged" AutoPostBack="true"/>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtValorCuota"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                            <td style="text-align: center; width: 185px; display:none">Valor Beneficio<br />
                                <asp:TextBox ID="txtbeneficio" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtValorCuota"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                             <td style="text-align: center; width: 185px">
                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label><br />
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="80%">
                                </asp:DropDownList>
                            </td>
                        </tr>                    

                        <tr>
                            <td>
                                <asp:TextBox ID="txtIdentificacionTitu" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreTit" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                            </td>

                        </tr>

                    </table>

                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged"/>--%>
                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlPlan" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="textvalorentidad" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="textvalormercado" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
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
                        <td style="text-align: center; font-size: large; color: Red">Linea Telefónica
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.<br />
                            Nro. de linea telefónica :
                            <asp:Label ID="lblNroMsj" runat="server"></asp:Label>
                            <br />
                            <asp:Button ID="btnDesembolso" runat="server" Text="ir a Desembolso"
                                OnClick="btnDesembolso_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
