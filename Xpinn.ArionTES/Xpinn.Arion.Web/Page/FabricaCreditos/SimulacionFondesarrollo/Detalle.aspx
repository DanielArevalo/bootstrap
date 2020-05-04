﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="Numero" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctrCuotasExtras" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvLista" runat="server">

        <asp:View ID="Pantalla" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="3" style="color: #0033CC; font-size: medium; text-align: left">
                            <strong>Condiciones Deseadas del Crédito:</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel4" runat="server" Width="435px">
                                <table style="width: 75%;">
                                    <tr>
                                        <td style="width: 235px; text-align: left">Monto Deseado :&nbsp;
                                        </td>
                                        <td>
                                            <uc2:Numero ID="TextBox1" runat="server" AutoPostBack_="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>


                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel5" runat="server" Width="435px">
                                <table style="width: 75%;">
                                    <tr>
                                        <td style="width: 235px; text-align: left">Maneja Intereses Anticipados ?&nbsp;
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="TextBox2" CssClass="textbox" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>



                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel6" runat="server" Width="435px">
                                <table style="width: 75%;">
                                    <tr>
                                        <td style="width: 235px; text-align: left">Valor Confe&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextBox3" onkeypress="return isNumber(event)" CssClass="textbox" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel3" runat="server" Width="435px">
                                <table style="width: 75%;">
                                    <tr>
                                        <td style="width: 235px; text-align: left">Fecha Simulación:&nbsp;
                                        </td>
                                        <td>
                                            <ucFecha:fecha ID="txtFecha" runat="server" style="text-align: center" Width="130px" Enabled="True" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="font-size: xx-small;"></td>
                    </tr>
                    <tr>
                        <td style="height: 56px;" colspan="3">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 65%;">
                                    <tr>
                                        <td style="width: 167px; text-align: left">Tipo de Crédito:
                                        </td>
                                        <td style="width: 180px; text-align: left" colspan="1">
                                            <asp:DropDownList ID="lstLinea" runat="server" CssClass="textbox" DataTextField="&lt;Seleccione Un Item&gt;"
                                                Width="100%" AutoPostBack="True"
                                                OnSelectedIndexChanged="lstLinea_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 167px; text-align: left">Identificación de la Persona:
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox runat="server" ID="txtIdentificacionPersona" onkeypress="return isNumber(event)" CssClass="textbox" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Monto Solicitado:
                                        </td>
                                        <td style="text-align: left">
                                            <uc2:Numero ID="txtMonto" runat="server" AutoPostBack_="false" />
                                        </td>
                                        <td style="text-align: left">Monto Máximo:
                                        </td>
                                        <td style="text-align: left">
                                            <uc2:Numero ID="txtMontoMaximo" runat="server" Habilitado="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Plazo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="text-align: left">
                                            <uc2:Numero ID="txtPlazo" runat="server" AutoPostBack_="false" />
                                        </td>
                                        <td style="text-align: left">Plazo Máximo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="text-align: left">
                                            <uc2:Numero ID="txtPlazoMaximo" runat="server" Habilitado="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Periodicidad:
                                        </td>
                                        <td style="text-align: left" colspan="3">
                                            <asp:DropDownList ID="lstPeriodicidad" runat="server" CssClass="textbox" Width="158px">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="1">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Tipo de Liquidación:</td>
                                        <td style="text-align: left" colspan="3">
                                            <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="textbox"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="1">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="Lbltasa" runat="server" Text="Tasa Interes:"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left" colspan="1">
                                            <uc2:Numero ID="txttasa" runat="server" AutoPostBack_="False" />
                                        </td>
                                        <td style="text-align: left" colspan="2">
                                            <asp:Label ID="lblTipoTasa" runat="server" Text=" "></asp:Label>
                                        </td>
                                        <td colspan="1">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px; text-align: left">Fecha de Primer Pago:</td>
                                        <td style="text-align: left" colspan="3">
                                            <ucFecha:fecha ID="txtFechaPrimerPago" runat="server" style="text-align: left" Width="130px" Enabled="True" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="panelComision" runat="server">
                                    <table style="width: 65%;">
                                        <tr>
                                            <td style="width: 167px; text-align: left">
                                                <asp:Label ID="LblComision" runat="server" Text="Comisión" />
                                            </td>
                                            <td style="text-align: left" colspan="3">
                                                <asp:TextBox ID="txtComision" runat="server" CssClass="textbox" Width="145px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="meeComision" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                    TargetControlID="txtComision" />
                                            </td>
                                            <td colspan="1">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelAporte" runat="server">
                                    <table style="width: 65%;">
                                        <tr>
                                            <td style="width: 167px; text-align: left">
                                                <asp:Label ID="lblAporte" runat="server" Text="Aportes" />
                                            </td>
                                            <td style="text-align: left" colspan="3">
                                                <asp:TextBox ID="txtAporte" runat="server" CssClass="textbox" Width="145px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="meeAporte" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                    TargetControlID="txtAporte" />
                                            </td>
                                            <td colspan="1">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelSeguro" runat="server">
                                    <table style="width: 65%;">
                                        <tr>
                                            <td style="width: 167px; text-align: left">
                                                <asp:Label ID="lblSeguro" runat="server" Text="Seguro" />
                                            </td>
                                            <td style="text-align: left" colspan="3">
                                                <asp:TextBox ID="txtSeguro" runat="server" CssClass="textbox" Width="145px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                    TargetControlID="txtSeguro" />
                                            </td>
                                            <td colspan="1">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <uc1:ctrCuotasExtras runat="server" ID="CuotasExtras" />
                            </asp:Panel>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnCalcular0" runat="server"
                                ImageUrl="~/Images/btnAceptar.jpg" OnClick="btnCalcular_Click" Width="140px" />
                            &nbsp;
                        </td>
                        <td style="text-align: left" colspan="3">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px; text-align: left">Cuota Calculada:</td>
                        <td style="text-align: left" colspan="3">
                            <uc2:Numero ID="txtCuota" runat="server" Enabled="false" />
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left">&nbsp;
                        </td>
                        <td colspan="3">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnPlanPagos" runat="server" ImageUrl="~/Images/btnPlanPagos.jpg"
                                Width="140px" OnClick="btnPlanPagos_Click" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Accordion ID="Accordion1" runat="server"
                FadeTransitions="True" FramesPerSecond="50" Width="947px"
                TransitionDuration="200" HeaderCssClass="accordionCabecera" SuppressHeaderPostbacks="true"
                ContentCssClass="accordionContenido" Height="350px">
                <Panes>
                    <asp:AccordionPane ID="AccordionPane7" runat="server" ContentCssClass="" HeaderCssClass="">
                        <Header>
                            <a>PLAN DE PAGOS</a>
                        </Header>

                        <Content>
                            Atributos Descontados : 
                              <div id="divDescontados" style="overflow: scroll; width: 440px">
                                  <asp:DataList ID="lbDescontados" runat="server" ShowFooter="False" ShowHeader="False"
                                      Width="425px" BorderStyle="Solid" BorderWidth="1px">
                                      <AlternatingItemStyle Wrap="False" />
                                      <ItemTemplate>
                                          <asp:Label ID="lblNomAtr" runat="server" Text='<%# Bind("nom_atr") %>'
                                              Style="font-size: small" Width="250px"></asp:Label>
                                          <asp:Label ID="lblValAtr" runat="server" Text='<%# Bind("val_atr","{0:C}") %>'
                                              Style="font-size: small; text-align: right" Width="150px"></asp:Label>
                                      </ItemTemplate>
                                  </asp:DataList>
                              </div>
                            Valor a Desembolsar :
                            <asp:TextBox ID="txtVrDesembolsado" runat="server" CssClass="textbox2"
                                Enabled="False" Style="text-align: right; margin-left: 0px" Width="150px"></asp:TextBox>
                            <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                                OnClick="btnExportar_Click" OnClientClick="btnExportar_Click"
                                Text="Exportar a Excel" />
                            <div id="Layer1" style="width: 927px; height: 300px; overflow: scroll;">
                                <asp:GridView ID="gvPlanPagos" runat="server" AllowPaging="false"
                                    AutoGenerateColumns="False" GridLines="Horizontal"
                                    ShowHeaderWhenEmpty="True" Height="292px"
                                    OnPageIndexChanging="gvPlanPagos_PageIndexChanging" Style="font-size: small"
                                    OnSelectedIndexChanged="gvPlanPagos_SelectedIndexChanged" Width="927px">
                                    <Columns>
                                        <asp:BoundField DataField="numerocuota" HeaderText="No"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechacuota" HeaderText="Fecha"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:dd-MM-yyyy}">
                                            <ItemStyle HorizontalAlign="Right" Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sal_ini" HeaderText="Saldo Inicial"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="4px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="capital" HeaderText="Capital"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_1" HeaderText="Int_1"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_2" HeaderText="Int_2"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_3" HeaderText="Int_3"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_4" HeaderText="Int_4"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_5" HeaderText="Int_5"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_6" HeaderText="Int_6"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_7" HeaderText="Int_7"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_8" HeaderText="Int_8"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_9" HeaderText="Int_9"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_10" HeaderText="Int_10"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_11" HeaderText="Int_11"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_12" HeaderText="Int_12"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_13" HeaderText="Int_13"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_14" HeaderText="Int_14"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_15" HeaderText="Int_15"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:c}"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Width="3px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sal_fin" HeaderText="Saldo Final"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>



                            </div>
                        </Content>
                    </asp:AccordionPane>
                </Panes>
            </asp:Accordion>
            <br />
        </asp:View>
        <asp:View ID="vReportePlan" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            OnClientClick="btnRegresar_Click" Text="Regresar" OnClick="btnRegresar_Click" />
                        &#160;&#160;
                        <asp:Button ID="btnImprimiendose" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            Text="Imprimir" OnClick="btnImprimiendose_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\Simulacion\ReportePlan.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="Iframe1" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="600px"
                            runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
</asp:Content>
