<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Detalle" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js"></script>
    <script type="text/javascript">

        var arrayIDControlesCalcular = ['<%= txtValorTotal.ClientID %>', '<%= txtNumCuotas.ClientID %>'];
        var IDControlDestino = '<%= txtValorCuota.ClientID %>';

    </script>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pDatos" runat="server">
                        <table style="text-align: center" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="text-align: left; width: 140px">
                                    Num. Servicio<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 140px">
                                    Fec. Solicitud<br />
                                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 140px">
                                    Fec. Aprobación<br />
                                    <ucFecha:fecha ID="txtFechaAproba" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 320px" colspan="2">
                                    Fec. Primera Cuota<br />
                                    <ucFecha:fecha ID="txtFechaPrimeraCuota" Enabled="false" runat="server" CssClass="textbox" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 140px">
                                    Solicitante<br />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="50px" Visible="false" />
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                        Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                                </td>
                                <td style="text-align: left; width: 280px" colspan="2">
                                    Nombre<br />
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="90%" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                        Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 280px" colspan="2">
                                    Linea de Servicio<br />
                                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="260px" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td style="text-align: left; width: 300px" colspan="2">
                                    Plan<br />
                                    <asp:DropDownList ID="ddlPlan" runat="server" CssClass="textbox" Width="240px" AppendDataBoundItems="True" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                            <td style="text-align: left" colspan="5">
                                <strong>Titular del Servicio :</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 140px">
                                Identificación<br />
                                <asp:TextBox ID="txtIdentificacionTitu" runat="server" CssClass="textbox" Width="90%" />
                            </td>
                            <td style="text-align: left;" colspan="3">
                                Nombre<br />
                                <asp:TextBox ID="txtNombreTit" runat="server" CssClass="textbox" Width="350px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        </table>
                    </asp:Panel>
                    <table style="text-align: center" cellspacing="0" cellpadding="0">                       
                        <tr>
                            <td colspan="5" style="text-align: left">
                                <hr style="width: 100%" /><br />
                                <strong>Datos a Modificar :</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 140px">
                                Fec. Inicio Vigencia<br />
                                <ucFecha:fecha ID="txtFecIni" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fec. Final Vigencia<br />
                                <ucFecha:fecha ID="txtFecFin" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Nro Poliza/Contrato
                                <asp:TextBox ID="txtNroPoliza" runat="server" CssClass="textbox" Width="90%" />
                                <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtNroPoliza"
                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                            </td>
                            <td style="text-align: left; width: 160px">
                                Valor Total<br />
                                <%--<uc1:decimales ID="txtValorTotal" runat="server" />--%>
                                <asp:TextBox ID="txtValorTotal" onkeyup="CalcularDivisionTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino)" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="100%" />
                            </td>
                            <td style="text-align: left; width: 160px">
                                Saldo<br />
                                <uc1:decimales ID="txtSaldo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 140px">
                                Fec. Prox Pago<br />
                                <ucFecha:fecha ID="txtFecProxPago" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Num. Cuotas<br />
                                <asp:TextBox ID="txtNumCuotas" onkeyup="CalcularDivisionTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino)" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" Width="60%" AutoPostBack="true" OnTextChanged="CalcularFechaTerminacion_TextChanged" />
<%--                                <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtNumCuotas"
                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />--%>
                            </td>
                            <td style="text-align: left; width: 140px">
                                Valor de la Cuota<br />
                                <%--<asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtValorCuota"
                                    FilterType="Custom, Numbers" ValidChars="+-=/*().," />--%>
                                <%--<uc1:decimales ID="txtValorCuota" runat="server" />--%>
                                <asp:TextBox ID="txtValorCuota" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="90%" />
                            </td>
                            <td style="text-align: left; width: 160px">
                                Periodicidad<br />
                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="90%" OnSelectedIndexChanged="CalcularFechaTerminacion_TextChanged" />
                            </td>
                            <td style="text-align: left; width: 160px">
                                Forma de Pago<br />
                                <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="90%" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <strong>Beneficiarios</strong><br />
                                    <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Beneficiario" Height="22px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvBeneficiarios" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                        margin-bottom: 0px;" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                                        DataKeyNames="codserbeneficiario" GridLines="Horizontal">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codserbeneficiario") %>' /></ItemTemplate>
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
                                                        Visible="false" />
                                                    <cc1:DropDownListGrid ID="ddlParentesco" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" Width="170px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%Beneficiario">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtPorcBene" runat="server" Text='<%# Bind("porcentaje") %>'
                                                        Width="100px" Style="text-align: right" MaxLength="8"/>
                                                    <asp:FilteredTextBoxExtender ID="fte3" runat="server" TargetControlID="txtPorcBene"
                                                        FilterType="Custom, Numbers" ValidChars=".," />
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
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
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
                        <td style="text-align: center; font-size: large; color: Red">
                            Datos del servicio modificado correctamente.<br />
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
        <asp:View ID="mvFinal" runat="server">
            <uc2:procesoContable ID="ctlproceso" runat="server" />
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
