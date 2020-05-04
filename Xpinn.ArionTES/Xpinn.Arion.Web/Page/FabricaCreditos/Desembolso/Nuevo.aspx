<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Desembolso de Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctrCuotasExtras" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#demo7').click(function () {
                $.blockUI({ message: null });
                setTimeout($.unblockUI, 2000);
            });
        });
    </script>
    <span class="badge badge-warning" runat="server" Visible="True" id="labelWarning" style="font-size: 9pt;"></span>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:Panel ID="Panel1" runat="server">
            <table style="width: 70%;">
                <tr>
                    <td style="text-align: left">Fecha de Desembolso
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Style="text-align: left"></asp:TextBox>
                        <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtFecha" Format="dd/MM/yyyy" StartDate="2019-08-22">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                            ForeColor="Red" Display="Dynamic" Style="font-size: x-small" />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="logo" style="text-align: left">N�mero de Cr�dito
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false"
                            Style="text-align: left" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Width="80%">
            <table>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <strong>DATOS DEL DEUDOR:</strong> <strong>
                            <asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown" Enabled="False"
                                Height="25px" Visible="False" Width="186px">
                            </asp:DropDownList>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 200px;">C�digo Cliente
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="False" Style="text-align: left"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 200px;">Identificaci�n
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false"
                            Style="text-align: left" />
                    </td>
                    <td style="text-align: right; width: 120px;">Tipo Identificaci�n
                    </td>
                    <td style="text-align: left; width: 140px;">
                        <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Style="text-align: left"
                            Enabled="false" Width="140px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 200px;">Nombre
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Style="text-align: left"
                            Width="410px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="Panel3" runat="server" Width="80%">
            <table>
                <tr>
                    <td style="text-align: left;" colspan="6">
                        <strong>DATOS DEL CREDITO:</strong>
                    </td>
                    <td style="text-align: left;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px;">L�nea de cr�dito
                    </td>
                    <td colspan="6" style="text-align: left; width: 600px;">
                        <asp:TextBox ID="txtCod_Linea_credito" runat="server" CssClass="textbox" Style="text-align: left"
                            Enabled="false" Width="40px" />
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" Style="text-align: left"
                            Enabled="false" Width="420px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px;">Monto
                    </td>
                    <td style="text-align: left">
                        <uc1:decimales ID="txtMonto" runat="server" CssClass="textbox" Enabled="false" style="text-align: left" />
                    </td>
                    <td style="text-align: left">Forma de Pago
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Style="text-align: left"
                            Enabled="false" Width="90px" />
                    </td>
                    <td style="text-align: right;">Plazo
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" Style="text-align: left"
                            Width="50px" />
                    </td>
                    <td style="text-align: left">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px;">Periodicidad
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Style="text-align: left"
                            Enabled="false" Width="129px" />
                    </td>
                    <td style="text-align: left; width: 200px;">Valor Cuota
                    </td>
                    <td style="text-align: left">
                        <uc1:decimales ID="txtValor_cuota" runat="server" CssClass="textbox" style="text-align: left"
                            Enabled="false" Width="100px" />
                    </td>
                    <td style="text-align: right;">&nbsp;
                    </td>
                    <td style="text-align: left">&nbsp;
                    </td>
                    <td style="text-align: left;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px;">Observaciones:
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtObs_solici" runat="server" CssClass="textbox" Enabled="false" Style="text-align: left"
                            Width="410px" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 385px; text-align: left" colspan="6">
                        <strong>
                            <asp:Label ID="lblTextoDeduc" runat="server" Text="DEDUCCIONES DEL CREDITO:" /></strong>
                    </td>
                    <td style="width: 446px; text-align: left">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="width: 385px; text-align: left">
                        <asp:GridView ID="gvLista0" runat="server" AutoGenerateColumns="False" DataKeyNames="tipo_movimiento"
                            HeaderStyle-CssClass="gridHeader" OnPageIndexChanging="gvLista_PageIndexChanging"
                            PagerStyle-CssClass="gridPager" PageSize="30" RowStyle-CssClass="gridItem" Width="75%">
                            <Columns>
                                <asp:BoundField DataField="tipo_movimiento" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_TRAN" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" Visible="False" />
                                <asp:TemplateField HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="descrpcion" HeaderText="Descripci�n" />
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Valor">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VALOR") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <uc1:decimales ID="TextBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colecci�n)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="false">
                            <LocalReport ReportPath="Page\FabricaCreditos\PlanPagos\ReportePlan.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                    <td style="width: 446px; text-align: left">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="width: 385px; text-align: left">
                        <asp:TextBox ID="txttotal" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td style="width: 446px; text-align: left">&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <hr style="width: 100%" />
        <asp:Panel ID="Panel4" runat="server" Width="90%">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 155px; text-align: left;">Fecha de Aprobaci�n
                    </td>
                    <td class="logo" style="text-align: left; width: 220px">
                        <asp:TextBox ID="Textfecha" runat="server" CssClass="textbox" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <strong>Fecha Primer Pago</strong>
                    </td>
                    <td style="text-align: left">
                        <ucFecha:fecha ID="txtPrimerPago" runat="server" style="text-align: left" />
                    </td>
                </tr>
            </table>
            <hr style="width: 110%" />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 161px; text-align: left;">
                        <span>
                            <asp:CheckBox ID="chkTasa" runat="server" AutoPostBack="true" OnCheckedChanged="chkTasa_CheckedChanged"
                                Text="Actualizar Tasa" />
                        </span>
                    </td>
                    <td class="logo" colspan="2" style="text-align: left">Tasa Int.Cte &nbsp;
                        <asp:TextBox ID="txttasa" runat="server" CssClass="textbox" MaxLength="5" Width="86px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Tipo Tasa &nbsp;
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddltipotasa"
                            Display="Dynamic" ErrorMessage="Seleccione una Tipo Tasa" ForeColor="Red" Operator="GreaterThan"
                            SetFocusOnError="true" Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" ValidationGroup="vgAcpAproModif"
                            ValueToCompare="0"> </asp:CompareValidator>
                        <asp:DropDownList ID="ddltipotasa" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div style="text-align: left">
                <strong><span style="font-size: small; text-align: left"><asp:Label ID="lblAtrDesc" runat="server" Text="ATRIBUTOS DESCONTADOS/FINANCIADOS" Visible="false" /></strong><br />
                <span style="font-size: small">
                    <asp:GridView ID="gvDeducciones" runat="server" AllowPaging="False"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" DataKeyNames="cod_atr" ForeColor="Black"
                        ShowFooter="True" Style="font-size: x-small" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Cod.Atr">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodAtr" runat="server" CssClass="textbox" Enabled="false"
                                        Text='<%# Bind("cod_atr") %>' Width="30px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nom_atr" HeaderStyle-Width="120px"
                                HeaderText="Descripción" />
                            <asp:TemplateField HeaderText="Tipo de Descuento">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTipoDescuento" runat="server" CssClass="textbox"
                                        DataSource="<%#ListarTiposdeDecuento() %>" DataTextField="descripcion"
                                        DataValueField="codigo" Enabled="false"
                                        SelectedValue='<%# Bind("tipo_descuento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Liquidación">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="textbox"
                                        DataSource="<%#ListaCreditoTipoDeLiquidacion() %>" DataTextField="descripcion"
                                        DataValueField="codigo" Enabled="false"
                                        SelectedValue='<%# Bind("tipo_liquidacion") %>' Width="100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Forma de Descuento">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlFormaDescuento" runat="server" CssClass="textbox"
                                        DataSource="<%#ListaCreditoFormadeDescuento() %>" DataTextField="descripcion"
                                        DataValueField="codigo" Enabled="false"
                                        SelectedValue='<%# Bind("forma_descuento") %>' Width="100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtvalor" runat="server" Style="font-size: x-small"
                                        Text='<%# Bind("val_atr") %>' Width="60px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Num.Cuotas">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtnumerocuotas" runat="server" Enabled="false"
                                        Style="font-size: x-small" Text='<%# Bind("numero_cuotas") %>' Width="60px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cobra Mora">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbCobraMora" runat="server"
                                        Checked='<%# Convert.ToBoolean(Eval("cobra_mora")) %>' Enabled="false"
                                        Text=" " />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Impuestos">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlimpuestos" runat="server" CssClass="textbox"
                                        DataSource="<%#ListaImpuestos() %>" DataTextField="descripcion"
                                        DataValueField="codigo" Enabled="false"
                                        SelectedValue='<%# Bind("tipo_impuesto") %>' Width="100px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </span>
            </div>
        </asp:Panel>
        <hr style="width: 100%" />
        <asp:Panel ID="Panel6" runat="server" Width="100%">
            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                <tr>
                    <td style="text-align: left">
                        <strong>CREDITOS A RECOGER: </strong>
                        <asp:TextBox ID="txtorden" runat="server" CssClass="textbox" Style="text-align: left"
                            Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" HeaderStyle-CssClass="gridHeader"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_credito, valor_total">
                            <Columns>
                                <asp:BoundField DataField="numero_credito" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_credito" HeaderText="N�mero de cr�dito" />
                                <asp:BoundField DataField="linea_credito" HeaderText="L�nea" />
                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo capital" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="interes_corriente" HeaderText="Interes corriente" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="interes_mora" HeaderText="Interes mora" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuo.Pag">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Recoger">
                                    <ItemTemplate>
                                        <cc1:CheckBoxGrid ID="chkRecoger" runat="server" Checked='<%# Eval("Recoger") %>'
                                            AutoPostBack="true" OnCheckedChanged="chkRecoger_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="valor_total" HeaderText="Total" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantidad_nominas" HeaderText="Cant.Nominas">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_nominas" HeaderText="Val.Nominas">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong>SERVICIOS A RECOGER: </strong>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Style="text-align: left"
                            Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:GridView ID="gvServiciosRecogidos" runat="server" Width="100%"
                            GridLines="Horizontal" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="gvServiciosRecogidos_PageIndexChanging"
                            DataKeyNames="consecutivo" Style="font-size: x-small">
                            <Columns>
                                <asp:BoundField DataField="numeroservicio" HeaderText="Num. Servicio">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="saldoservicio" HeaderText="Saldo" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="interessevicio" HeaderText="Interes" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="valorrecoger" HeaderText="Valor a Pagar" DataFormatString="{0:n0}" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblTotRec" runat="server" Text="Total por Recoger :" Style="font-size: x-small" />
                        &#160;&#160;
                        <uc1:decimales ID="txtVrTotRecoger" runat="server" CssClass="textbox" Width="120px"
                            Enabled="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="text-align: center;" colspan="4">
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <br />
                        <asp:Label ID="lblordenservicios" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelDatosEducativo" runat="server">
                <hr style="width: 100%" />
                <table width="100%">
                    <tr>
                        <td style="text-align: left; width: 30%">Universidad<br />
                            <asp:TextBox ID="txtUniversidad" runat="server" CssClass="textbox" Enabled="false"
                                Width="95%" MaxLength="200" />
                        </td>
                        <td style="text-align: left; width: 13%">Semestre<br />
                            <asp:TextBox ID="txtSemestre" runat="server" CssClass="textbox" Enabled="false" Width="90%"
                                MaxLength="200" />
                        </td>
                        <td style="text-align: left; width: 15%">Vr. Matricula<br />
                            <asp:TextBox ID="txtVrMatricula" runat="server" CssClass="textbox" Enabled="false"
                                Width="90%" />
                        </td>
                        <td style="text-align: left; width: 15%">Vr. Auxilio<br />
                            <asp:TextBox ID="txtVrAuxilio" runat="server" CssClass="textbox" Enabled="false"
                                Width="90%" />
                        </td>
                        <td style="text-align: left; width: 15%"># Auxilio<br />
                            <asp:TextBox ID="txtNumAuxilio" runat="server" CssClass="textbox" Enabled="false"
                                Width="90%" />
                        </td>
                        <td style="text-align: left; width: 12%">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelProveedor" runat="server">
                <table border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td style="text-align: left;" colspan="6">
                            <span style="font-weight: bold">
                                <asp:Label ID="lblTitOrden" runat="server" Text="PROVEEDOR PARA LA ORDEN DE SERVICIO:" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="3">
                            <asp:Label ID="lblTitIdentific" runat="server" Text="Identificaci�n Proveedor" /><br />
                            <asp:TextBox ID="txtIdentificacionprov" runat="server" CssClass="textbox" Width="150px"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblTitNombre" runat="server" Text="Nombre Proveedor" /><br />
                            <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="textbox" Width="450px"
                                MaxLength="200" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblPreImpreso" runat="server" Text="Nro Orden" /><br />
                            <asp:TextBox ID="txtPreImpreso" runat="server" CssClass="textbox" Width="120px" MaxLength="100" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr style="width: 100%" />
        </asp:Panel>
        <asp:Panel ID="Panel5" runat="server" Width="100%">
            <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
                <ContentTemplate>
                    <table style="width: 583px;">
                        <tr>
                            <td style="width: 179px; text-align: left">
                                <strong>Forma de Desembolso</strong>
                            </td>
                            <td colspan="3" style="width: 404px">
                                <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" Style="margin-left: 0px; text-align: left"
                                    Width="84%" Height="28px" CssClass="textbox" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownFormaDesembolso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="pnlCuentaAhorroVista">
                        <table style="width: 48%">
                            <tr>
                                <td style="text-align: left; width: 110px">
                                    <asp:Label ID="lblCuentaAhorroVista" runat="server" Text="Numero Cuenta" Style="text-align: left"></asp:Label>
                                </td>
                                <td style="width: 151px; text-align: left;">
                                    <asp:DropDownList ID="ddlCuentaAhorroVista" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="102%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlCuentasBancarias">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 179px; text-align: left">
                                    <asp:Label ID="lblEntidadOrigen" runat="server" Text="Banco de donde se Gira" Style="text-align: left"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlEntidadOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 179px; text-align: left; height: 25px;">
                                    <asp:Label ID="lblNumCuentaOrigen" runat="server" Text="Cuenta de donde se Gira"
                                        Style="text-align: left"></asp:Label>
                                </td>
                                <td colspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlCuentaOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px"></td>
                            </tr>
                            <tr>
                                <td style="width: 179px; text-align: left">
                                    <asp:Label ID="lblEntidad" runat="server" Text="Entidad" Style="text-align: left"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="DropDownEntidad" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 179px; text-align: left">
                                    <asp:Label ID="lblNumCuenta" runat="server" Text="Numero de Cuenta" Style="text-align: left"></asp:Label>
                                </td>
                                <td style="width: 143px">
                                    <asp:TextBox runat="server" ID="txtNumeroCuenta" CssClass="textbox" />
                                </td>
                                <td style="width: 110px; text-align: left">
                                    <asp:Label ID="lblTipoCuenta" runat="server" Text="Tipo Cuenta" Style="text-align: left"></asp:Label>
                                </td>
                                <td style="width: 151px">
                                    <asp:DropDownList ID="ddlTipo_cuenta" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="102%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <div style="text-align: left">
            <br />
            <strong>Cuotas Extras:</strong>
            <uc1:ctrCuotasExtras runat="server" ID="CuotasExtras" />
        </div>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
    <uc2:validarBiometria ID="ctlValidarBiometria" runat="server" />

</asp:Content>
