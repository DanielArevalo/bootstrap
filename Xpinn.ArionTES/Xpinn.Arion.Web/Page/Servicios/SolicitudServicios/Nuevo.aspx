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

        var arrayIDControlesCalcular = ['<%= txtValorTotal.ClientID %>', '<%= txtNumCuotas.ClientID %>'];
        var IDControlDestino = '<%= txtValorCuota.ClientID %>';

    </script>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table style="width: 740px; text-align: center" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: left; width: 140px">Num. Servicio<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 140px">Fec. Solicitud<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                            </td>

                         <%--   <td style="text-align: left; width: 140px">oficina<br />
                                <asp:TextBox ID="txtcodoficina" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                            </td--%>
                          
                                        <td style="text-align: left;" colspan="2">oficina<br />
                                <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox" Width="260px" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                                    AutoPostBack="True" />
                            </td>
                        

                            <td style="text-align: left; width: 140px">&nbsp;
                            </td>
                            <td style="text-align: left; width: 160px">&nbsp;
                            </td>
                            <td style="text-align: left; width: 160px">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 140px">Solicitante<br />
                                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                                <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                    Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="fte121" runat="server" TargetControlID="txtIdPersona"
                                    FilterType="Custom, Numbers" ValidChars="-" />
                                <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                    OnClick="btnConsultaPersonas_Click" Text="..." />
                            </td>
                            <td style="text-align: left; width: 420px" colspan="3">Nombre<br />
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" OneventotxtIdentificacion_TextChanged="txtIdPersona_TextChanged" />
                                <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="350px" />
                                <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                    Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                    Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                            </td>
                            <td style="text-align: left; width: 140px">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="2">Linea de Servicio<br />
                                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="260px" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td style="text-align: left; width: 300px" colspan="2">
                                <asp:Label ID="lblPlan" runat="server" Text="Plan" /><br />
                                <asp:DropDownList ID="ddlPlan" runat="server" CssClass="textbox" Width="240px" AppendDataBoundItems="True" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="5">
                                <table>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:Panel ID="panelPrimFila" runat="server">
                                                <table width="420px">
                                                    <tr>
                                                        <td>
                                                        <td style="text-align: left; width: 140px">Fec. Inicio Vigencia<br />
                                                            <ucFecha:fecha ID="txtFecIni" runat="server" CssClass="textbox" />
                                                        </td>
                                                        <td style="text-align: left; width: 140px">Fec. Final Vigencia<br />
                                                            <ucFecha:fecha ID="txtFecFin" Enabled="false" runat="server" CssClass="textbox" />
                                                        </td>
                                                        <td style="text-align: left; width: 140px">Nro Poliza/Contrato
                                                                <asp:TextBox ID="txtNroPoliza" runat="server" CssClass="textbox" Width="90%" />
                                                            <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtNroPoliza"
                                                                FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="text-align: left; width: 160px">Valor Total<br />
                                            <asp:TextBox ID="txtValorTotal" onkeyup="CalcularDivisionTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino)" runat="server" CssClass="textbox" Width="60%" onkeypress="return isNumber(event)" />
                                        </td>
                                        <td style="text-align: left; width: 160px">Forma de Pago<br />
                                            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" AutoPostBack="true"
                                                Width="90%" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="5">
                                <table>
                                    <tr>
                                        <td style="text-align: left;" colspan="4">
                                            <asp:Panel ID="panelSegFila" runat="server">
                                                <table width="580px">
                                                    <tr>
                                                        <td style="text-align: left; width: 140px">Fec. Primera Cuota<br />
                                                            <ucFecha:fecha ID="txtFec1ercuota" eventoCambiar="CalcularFechaTerminacion_TextChanged" runat="server" CssClass="textbox" />
                                                        </td>
                                                        <td style="text-align: left; width: 140px">Num. Cuotas<br />
                                                            <asp:TextBox ID="txtNumCuotas" onkeyup="CalcularDivisionTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino)" AutoPostBack="true" runat="server" CssClass="textbox" Width="60%" onkeypress="return isNumber(event)" OnTextChanged="CalcularFechaTerminacion_TextChanged" />
                                                            <asp:Label ID="lblCuotasPendientes" runat="server" />
                                                        </td>
                                                        <td style="text-align: left; width: 140px">Valor Cuota Aprox.<br />
                                                            <asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" />
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtValorCuota"
                                                                FilterType="Custom, Numbers" ValidChars="." />
                                                        </td>
                                                        <td style="text-align: left; width: 160px">Periodicidad<br />
                                                            <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="CalcularFechaTerminacion_TextChanged" />
                                                        </td>
                                                        <td style="text-align: left; width: 160px">Destinación<br />
                                                           <asp:DropDownList ID="ddlDestino" runat="server" CssClass="textbox" Width="225px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="text-align: left; width: 160px">
                                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label><br />
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="90%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 740px; text-align: center" cellspacing="0" cellpadding="0">
                        <caption>
                            <tr>
                                <td colspan="5">
                                    <hr style="width: 100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left;">
                                    <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
                                </td>
                                <%--<td style="text-align: left; width: 140px">
                                Identificación<br />
                                <asp:TextBox ID="txtProvIdentificacion" runat="server" CssClass="textbox" Width="90%" Enabled="true" />
                                <asp:Button ID="btnConsultaProveedor" CssClass="btn8" runat="server" Text="..." Height="26px"
                                    OnClick="btnConsultaProveedor_Click" /><br />
                                <uc1:ListadoPersonas ID="ctlBusquedaProveedor" runat="server" />
                            </td>
                            <td style="text-align: left; width: 420px" colspan="3">
                                Nombre<br />
                                <asp:TextBox ID="txtProvNombre" runat="server" CssClass="textbox" Width="350px" Enabled="true" />
                            </td>--%>
                                <td>&nbsp; </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: left"><strong>Titular del Servicio :</strong> </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 140px">Identificación<br />
                                    <asp:TextBox ID="txtIdentificacionTitu" runat="server" CssClass="textbox" Width="90%" />
                                    <asp:FilteredTextBoxExtender ID="fte120" runat="server" FilterType="Custom, Numbers" TargetControlID="txtIdentificacionTitu" ValidChars="-" />
                                </td>
                                <td colspan="3" style="text-align: left; width: 420px">Nombre<br />
                                    <asp:TextBox ID="txtNombreTit" runat="server" CssClass="textbox" Width="350px" />
                                </td>
                                <td>&nbsp; </td>
                            </tr>
                        </caption>
                    </table>
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table>
                            <tr>
                                <td style="text-align: left">
                                    <strong>Beneficiarios</strong><br />
                                    <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Beneficiario" Height="22px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvBeneficiarios" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;"
                                        OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                                        DataKeyNames="codserbeneficiario" GridLines="Horizontal">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codserbeneficiario") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtIdenti_Grid" runat="server" Text='<%# Bind("identificacion") %>'
                                                        Width="110px"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtNombreComple" runat="server" Text='<%# Bind("nombre") %>'
                                                        Width="250px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parentesco">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("codparentesco") %>'
                                                        Visible="false" /><cc1:DropDownListGrid ID="ddlParentesco" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                            CssClass="textbox" Width="170px">
                                                        </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%Beneficiario">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtPorcBene" runat="server" Text='<%# Bind("porcentaje") %>'
                                                        OnTextChanged="txtNumBeneficiario_TextChanged" Width="100px" Style="text-align: right" /><asp:FilteredTextBoxExtender
                                                            ID="fte3" runat="server" TargetControlID="txtPorcBene" FilterType="Custom, Numbers"
                                                            ValidChars="+-=/*()." />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width: 740px">
                        <tr>
                            <td style="width: 740px; text-align: center">
                                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 740px; text-align: center">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 740px; text-align: left">Observaciones<br />
                                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Width="500px"
                                    TextMode="MultiLine" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
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
                        <td style="text-align: center; font-size: large; color: Red">Solicitud de Servicio
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.<br />
                            Nro. de Servicio :
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
