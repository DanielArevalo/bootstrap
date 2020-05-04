<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" MasterPageFile="~/General/Master/site.master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlDocumentosAnexo.ascx" TagPrefix="uc1" TagName="DocumentosAnexo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function PanelClick(sender, e) {
        }

        function ActiveTabChanged(sender, e) {
        }

        function mpeSeleccionOnOk() {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

        function mpeSeleccionOnCancel() {
        }



    </script>
    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridPlan" runat="server">
            <hr style="width: 98%" />
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;">
                            <table style="width: 50%;">
                                <tr>
                                    <td colspan="6">
                                        <div style="font-size: xx-small">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px">No. Crédito:
                                        <br />
                                    </td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtNumRadic" runat="server" CssClass="textbox2"
                                            Enabled="False" Width="139px" OnTextChanged="txtNumRadic_TextChanged"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 60px;">LíneaCrédito:</td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox2" Enabled="False"
                                            Style="margin-left: 0px" Width="60px"></asp:TextBox>
                                        <asp:TextBox ID="txtNombreLinea" runat="server" CssClass="textbox2"
                                            Width="270px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px">Identific.:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtTipoIdentific" runat="server" CssClass="textbox2"
                                            Enabled="False" Width="21px" Height="21px"></asp:TextBox>
                                        <asp:TextBox ID="txtIdentific" runat="server" CssClass="textbox2"
                                            Enabled="False" Width="110px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 60px;">Nombres:</td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:TextBox ID="txtNombre" runat="server" Enabled="False" Width="335px"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px">Plazo:</td>
                                    <td style="width: 60px; text-align: left">
                                        <asp:TextBox ID="txtPlazo" runat="server" Enabled="False" Width="43px" CssClass="textbox2"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 60px;" colspan="2">Monto&nbsp; Crédito:&nbsp;&nbsp;
                                        <br />
                                    </td>
                                    <td style="width: 150px"></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtMonto" runat="server"
                                            Style="text-align: right; margin-left: 0px" Width="150px" Enabled="False"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px; height: 30px;">Periodicidad:</td>
                                    <td style="width: 60px; height: 30px;">
                                        <asp:TextBox ID="txtPeriodicidad" runat="server" Enabled="False" Width="139px"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left" colspan="4" rowspan="2">
                                        <div id="div1" style="overflow: scroll; width: 440px">
                                            <asp:DataList ID="lbSumados" runat="server" ShowFooter="False" ShowHeader="False"
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
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px">For.Pago:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtFormaPago" runat="server" Width="139px" Enabled="False"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>


                                </tr>
                                <tr>
                                    <td style="width: 97px">Fec.Aprobacion:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="TxtFechaApro" runat="server" Width="139px" Enabled="False"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>


                                </tr>
                                <tr>
                                    <td style="width: 97px; height: 32px;">F.Desembolso:</td>
                                    <td style="width: 60px; height: 32px;">
                                        <asp:TextBox ID="txtFechaSolicitud" runat="server" Enabled="False" Width="139px"
                                            CssClass="textbox2" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtFechaInicial" runat="server" Enabled="False" Width="139px"
                                            CssClass="textbox2" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtFechaDesembolso" runat="server" Enabled="False" Width="139px"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 60px;" colspan="2">&nbsp;Monto&nbsp; Total:
                                    </td>
                                    <td style="width: 150px"></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtMontoCalculado" runat="server" Width="150px" Style="text-align: right; margin-left: 0px"
                                            Enabled="False" CssClass="textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px">Vr.Cuota:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtCuota" runat="server" Enabled="False" Width="139px"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left" colspan="4" rowspan="2">
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
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 97px">Dias Ajuste:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtDiasAjuste" runat="server" Enabled="False" Width="139px"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>


                                </tr>
                                <tr>
                                    <td style="width: 97px">Tasa Int.Nom:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtTasaInteres" runat="server" Enabled="False" Width="65px"
                                            CssClass="textbox2"></asp:TextBox>
                                        <asp:TextBox ID="txtTasaPeriodica" runat="server" Enabled="False" Width="65px"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px">Tasa de Usura:</td>
                                    <td style="width: 60px">
                                        <asp:TextBox ID="txtTasaUsura" runat="server" Enabled="False" Width="65px"></asp:TextBox>
                                        <asp:TextBox ID="txtTasaPeriodicaUsu" runat="server" Enabled="False" Width="65px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px; " ><asp:Label runat="server"></asp:Label>TIR:</td>
                                    <td style="width: 60px;">
                                        <asp:TextBox ID="txtTIR" runat="server" Width="139px" Enabled="False"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left" colspan="4" rowspan="2">
                                        <div id="divListRecogidos" style="overflow: scroll; width: 440px">
                                            <asp:DataList ID="dtListRecogidos" runat="server" ShowFooter="False" ShowHeader="False"
                                                Width="425px" BorderStyle="Solid" BorderWidth="1px">
                                                <AlternatingItemStyle Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomAtr" runat="server" Text='<%# Bind("numero_radicacion") %>'
                                                        Style="font-size: small" Width="80px"></asp:Label>
                                                    <asp:Label ID="Linea" runat="server" Text='<%# Bind("linea_Credito") %>'
                                                        Style="font-size: small" Width="180px"></asp:Label>
                                                    <asp:Label ID="lblValAtr" runat="server" Text='<%# Bind("valor_recoge","{0:C}") %>'
                                                        Style="font-size: small; text-align: right" Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 97px; height: 26px;">Moneda:</td>
                                    <td style="width: 60px; height: 26px;">
                                        <asp:TextBox ID="txtMoneda" runat="server" Width="139px" Enabled="False"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="TxtCodOficina" runat="server" CssClass="textbox2"
                                            Enabled="False" Visible="False" Width="43px"></asp:TextBox>
                                        <asp:TextBox ID="TxtNomOficina" runat="server" CssClass="textbox2"
                                            Enabled="False" Visible="False" Width="43px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 60px;" colspan="2">&nbsp;Vr.Desembolsado
                                    </td>
                                    <td style="width: 150px"></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtVrDesembolsado" runat="server" CssClass="textbox2"
                                            Enabled="False" Style="text-align: right; margin-left: 0px" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttipodoc" runat="server" Width="139px" Enabled="False" Visible="false"
                                            CssClass="textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 4px">
                                        <asp:TextBox ID="Txtdireccion" runat="server" Width="56px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="Txtciudad" runat="server" Width="44px" Visible="False">  </asp:TextBox>
                                        <asp:TextBox ID="Txtprimerpago" runat="server" Width="57px" Visible="False">  </asp:TextBox>
                                        <asp:TextBox ID="Txtgeneracion" runat="server" Width="43px" Visible="False">  </asp:TextBox>
                                        <asp:TextBox ID="Txtcuotas" runat="server" Width="43px" Visible="False">  </asp:TextBox>
                                        <asp:TextBox ID="Txtpagare" runat="server" Width="61px" Visible="False">  </asp:TextBox>
                                        <asp:TextBox ID="Txtinteresefectiva" runat="server" Width="53px" Visible="False">  </asp:TextBox>
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>


                            </table>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvPlanPagos0" runat="server"
                            AutoGenerateColumns="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" Height="292px"
                            Style="font-size: x-small" Width="927px" OnPageIndexChanging="gvPlanPagos_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="numerocuota" HeaderText="No"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechacuota" DataFormatString="{0:dd-MM-yyyy}"
                                    HeaderText="Fecha" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sal_ini" DataFormatString="{0:c}"
                                    HeaderText="Saldo Inicial" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="capital" DataFormatString="{0:c}"
                                    HeaderText="Capital" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_1" DataFormatString="{0:c}" HeaderText="Int_1"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_2" DataFormatString="{0:c}" HeaderText="Int_2"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_3" DataFormatString="{0:c}" HeaderText="Int_3"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_4" DataFormatString="{0:c}" HeaderText="Int_4"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_5" DataFormatString="{0:c}" HeaderText="Int_5"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_6" DataFormatString="{0:c}" HeaderText="Int_6"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_7" DataFormatString="{0:c}" HeaderText="Int_7"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_8" DataFormatString="{0:c}" HeaderText="Int_8"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_9" DataFormatString="{0:c}" HeaderText="Int_9"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_10" DataFormatString="{0:c}" HeaderText="Int_10"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_11" DataFormatString="{0:c}" HeaderText="Int_11"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_12" DataFormatString="{0:c}" HeaderText="Int_12"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_13" DataFormatString="{0:c}" HeaderText="Int_13"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_14" DataFormatString="{0:c}" HeaderText="Int_14"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="int_15" DataFormatString="{0:c}" HeaderText="Int_15"
                                    ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Total Cuota"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sal_fin" DataFormatString="{0:c}"
                                    HeaderText="Saldo Final" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Label ID="lblPlanPagos" runat="server" Text="PLAN DE PAGOS"></asp:Label>
            <asp:GridView ID="gvPlanPagos" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Height="300px" Style="font-size: x-small"
                Width="927px" OnPageIndexChanging="gvPlanPagos_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="numerocuota" HeaderText="No"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechacuota" HeaderText="Fecha"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:dd-MM-yyyy}">
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <ControlStyle Width="100px" />
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
                    <asp:BoundField DataField="total" HeaderText="Total Cuota" DataFormatString="{0:c}"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sal_fin" HeaderText="Saldo Final"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                        <ItemStyle HorizontalAlign="Right" Width="130px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
            <asp:Label ID="LblError" runat="server"
                Style="color: #FF0000; font-weight: 700;"></asp:Label>
            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            <br />
            <div style="text-align: left">
                <table style="width: 90%; text-align: center">
                    <tr>
                        <td>
                            <asp:Button ID="btnInformePlan" runat="server" CssClass="btn8"
                                OnClick="btnInformePlan_Click" OnClientClick="btnInformePlan_Click"
                                Text="Visualizar informe" />
                        </td>
                        <td>
                            <asp:Button ID="btnExportar1" runat="server" CssClass="btn8"
                                OnClick="btnExportar0_Click" OnClientClick="btnExportar0_Click"
                                Text="Exportar a excel" />
                        </td>
                        <td>
                            <asp:Button ID="btnTalonario" runat="server" CssClass="btn8"
                                OnClick="btnTalonario_Click" Text="Carta Desembolso"
                                Width="151px" Visible="False" />
                        </td>
                        <td>
                            <asp:Button ID="BtnPagare0" runat="server" CssClass="btn8"
                                OnClick="BtnPagare_Click" Text="Exportar Pagaré" />
                        </td>
                        <td>
                            <asp:Button ID="btnVerSubirPagare" runat="server" CssClass="btn8"
                                Text="Ver/Subir Pagaré" Width="110px" OnClick="btnVerSubirPagare_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDocumentosAnexos" runat="server" CssClass="btn8"
                                Text="Ver Anexos" Width="110px" OnClick="btnDocumentosAnexo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Btnautorizacion0" runat="server" CssClass="btn8"
                                OnClick="Btnautorizacion_Click" Text="Ver Autorización" Width="110px" />
                        </td>
                        <td>
                            <asp:Button ID="btnInformeSolicitud" runat="server" CssClass="btn8"
                                OnClick="btnInformeSolicitud_Click"
                                Text="Informe Solicitud" Width="110px" />
                        </td>
                        <td>
                            <asp:Button ID="btnInformeConsumo" runat="server" CssClass="btn8"
                                OnClick="btnInformeConsumo_Click"
                                Text="Solicitud Crédito" Width="109px" />
                        </td>
                        <td>
                            <asp:Button ID="btnImprimirOrden" runat="server" CssClass="btn8"
                                OnClick="btnImprimirOrden_Click"
                                Text="Imprimir Orden" Width="109px" />
                        </td>
                        <td>
                            <asp:Button ID="mpePopUp" runat="server" CssClass="btn8"
                                OnClick="btnAtributosCredito_Click"
                                Text="Ver Atributos" Width="109px" />
                        </td>
                        <td>
                            <asp:Button ID="btnPlanPagosOriginal" runat="server" CssClass="btn8"
                                Text="Plan Pagos Original" Width="150px" Visible="false" OnClick="btnPlanPagosOriginal_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="vReportePlan" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnInforme5" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            OnClientClick="btnInforme5_Click" Text="Regresar" OnClick="btnInforme5_Click" />
                        &#160;&#160;
                        <asp:Button ID="BtnImprimir" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            Text="Imprimir" OnClick="btnImprimiendose_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\PlanPagos\ReportePlan.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>

                </tr>
                <tr>
                    <td>
                        <iframe id="Iframe1" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>

            </table>
        </asp:View>
        <asp:View ID="vReporteSolicitud" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnInforme4" runat="server" CssClass="btn8" OnClientClick="btnInforme4_Click"
                            Text="Regresar" OnClick="btnInforme4_Click" />
                        <asp:Button ID="txtimprimesolicitud" runat="server" CssClass="btn8" Text="Imprimir"
                            OnClick="btnimprimesolicitud" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewersolicitud" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="100%" Height="100%" SizeToReportContent="True"
                            DocumentMapWidth="25%">
                            <LocalReport ReportPath="Page\FabricaCreditos\Solicitud\PlanPagos\ReportSolicitud.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="framesolicitud" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px" visible="false"
                            runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vGridDocumentos" runat="server">
            <hr style="width: 98%" />
            <asp:Panel ID="pConsulta0" runat="server">
                <table style="width: 101%;">
                    <tr>
                        <td>
                            <div style="font-size: xx-small">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 4px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" DataKeyNames="cod_linea_credito"
                                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                                        OnPageIndexChanging="gvLista_PageIndexChanging" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="cod_linea_credito" HeaderStyle-CssClass="gridColNo"
                                                ItemStyle-CssClass="gridColNo"></asp:BoundField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="tipo_documento" HeaderText="Tipo documento">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion_documento" HeaderText="Descripción" />
                                            <asp:BoundField DataField="requerido" HeaderText="Requerido" />
                                            <asp:TemplateField HeaderText="Referencia">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferencia" runat="server"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvResultado" runat="server"
                                                        ControlToValidate="txtReferencia" Display="Dynamic"
                                                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="vgGuardar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ruta" HeaderText="Ruta" />
                                            <asp:TemplateField HeaderText="Seleccionar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbx" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Label ID="lbDocumentos" runat="server" Style="font-weight: 700"
                Text="DOCUMENTOS"></asp:Label>
            <asp:GridView ID="gvLista2" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" DataKeyNames="iddocumento" GridLines="Horizontal"
                HeaderStyle-CssClass="gridHeader"
                OnPageIndexChanging="gvLista_PageIndexChanging" PagerStyle-CssClass="gridPager"
                RowStyle-CssClass="gridItem" Width="100%">
                <Columns>
                    <asp:BoundField DataField="iddocumento" HeaderStyle-CssClass="gridColNo"
                        ItemStyle-CssClass="gridColNo">
                        <HeaderStyle CssClass="gridColNo" />
                        <ItemStyle CssClass="gridColNo" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo0" runat="server" CommandName="Select"
                                ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnImprimir" runat="server" CommandName="Print"
                                ImageUrl="~/Images/gr_imp.gif" OnClick="btnImprimir_Click" ToolTip="Imprimir"
                                Width="16px" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="tipo_documento" HeaderText="Tipo documento" />
                    <asp:BoundField DataField="descripcion_documento" HeaderText="Descripción" />
                    <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                    <asp:TemplateField HeaderText="Generar">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkGenerar" runat="server" Checked="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ruta" HeaderStyle-CssClass="gridColNo"
                        HeaderText="Ruta" ItemStyle-CssClass="gridColNo">
                        <HeaderStyle CssClass="gridColNo" />
                        <ItemStyle ForeColor="#CCCCCC" Width="1px" Wrap="False" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridPager" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
            <asp:Label ID="LblError0" runat="server"
                Style="color: #FF0000; font-weight: 700;"></asp:Label>
            <br />
            <asp:Button ID="btnGenerar" runat="server" CssClass="btn8" Height="32px"
                Text="Generar" ValidationGroup="vgGuardar"
                Width="94px" />
            <br />
            <asp:Label ID="lblInfo" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <div style="text-align: left">
            </div>
        </asp:View>
        <asp:View ID="vReporteOrden" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnRegresarOrden" runat="server" CssClass="btn8" OnClientClick="btnInforme4_Click"
                            Text="Regresar" OnClick="btnInforme4_Click" Height="25px" Width="120px" />
                        &#160;&#160;
                            <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="overflow: scroll; max-width: 100%; max-height: 600px">
                            <rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                                WaitMessageFont-Size="14pt" Width="100%" Height="100%" SizeToReportContent="True"
                                DocumentMapWidth="25%">
                                <LocalReport ReportPath="Page\FabricaCreditos\PlanPagos\rptOrden.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="vVerSubirPagare">
            <hr style="width: 98%" />
            <br />
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; width: 50%">
                        <strong>Vizualización de Documento</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; margin-bottom: 20px; width: 100%; margin: auto;">
                        <asp:Literal ID="ltPagare" runat="server" />
                    </td>
                </tr>
                <tr style="width: 30%">
                    <td>
                        <asp:Label ID="lblNotificacionGuardado" BorderColor="Red" BorderWidth="1px" Style="padding: 2px 20px" Visible="false" ForeColor="Red" runat="server" Text=" "></asp:Label>
                        <asp:Label ID="lblIDImagen" BorderColor="Red" BorderWidth="1px" Style="padding: 2px 20px" Visible="false" ForeColor="Red" runat="server" Text=" "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="avatarUpload" type="file" name="file" runat="server" />
                        <br />
                        <br />
                        <asp:Button ID="btnPrevisualizar" runat="server" CssClass="btn8" OnClick="btnPrevisualizar_Click" Text="Previsualizar Documento" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Size="XX-Small" Text="Archivo Máx. 20MB."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Size="XX-Small" Text="Extensiones Validas *.pdf"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="vwPlanPagosOriginal">
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="PLAN DE PAGOS ORIGINAL"></asp:Label>
            <hr style="width: 98%" />
            <asp:GridView ID="gvPlanPagosOriginal" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" HorizontalAlign="Center" Height="300px" Style="font-size: x-small"
                Width="927px" OnPageIndexChanging="gvPlanPagos_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="numerocuota" HeaderText="No"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechacuota" HeaderText="Fecha"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:dd-MM-yyyy}">
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <ControlStyle Width="100px" />
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
                    <asp:BoundField DataField="total" HeaderText="Total Cuota" DataFormatString="{0:c}"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sal_fin" HeaderText="Saldo Final"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                        <ItemStyle HorizontalAlign="Right" Width="130px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="vGridAnexos" runat="server">
            <hr style="width: 98%" />
            <asp:Panel ID="panelAnexos" runat="server">
                <uc1:DocumentosAnexo runat="server" ID="DocumentosAnexo" />
            </asp:Panel>
            <br />
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hfDocAnexo" runat="server" Visible="True" />
    <asp:ModalPopupExtender ID="mpeDocAnexo" runat="server" Enabled="True" BackgroundCssClass="backgroundColor"
        PopupControlID="Panl1" TargetControlID="hfDocAnexo" CancelControlID="btnCloseDocAnexo">
    </asp:ModalPopupExtender>
    <asp:ResizableControlExtender ID="rceDocAnexo" runat="server" TargetControlID="Panl1" OnClientResizing="rceDocAnexoResize"
        HandleCssClass="handle" ResizableCssClass="resizing" MinimumHeight="150" MinimumWidth="550" HandleOffsetY="20"></asp:ResizableControlExtender>
    <asp:DragPanelExtender ID="dpeDocAnexo" runat="server"
        Enabled="True" TargetControlID="Panl1" DragHandleID="Panl1"></asp:DragPanelExtender>


    <asp:Panel ID="Panl1" runat="server" CssClass="modalPopup ext" Style="background: rgba(184, 180, 185, .5); padding: 50px;">
        <div runat="server" id="Campos" style="background: rgba(184, 180, 185, .5); margin: 0 auto; margin-top: -40px">
            <strong><span style="font-size: small">Atributos Del Crédito</span></strong>
            <br />
            <asp:GridView ID="gvAtributos" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="black" AllowPaging="True" Width="550" Height="150"
                BorderWidth="1px" ForeColor="Black" GridLines="Both" Visible="False"
                CellPadding="0" ShowFooter="True" Style="font-size: x-small; background: #fff;"
                AutoPostBack="false" PageSize="10" OnPageIndexChanging="gvLista_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="cod_atr" ReadOnly="true" HeaderText="Codigo Atributo" />
                    <asp:BoundField DataField="descripcion" ReadOnly="true" HeaderText="Nombre Atributo" />
                    <asp:BoundField DataField="tasa" ReadOnly="true" HeaderText="Tasa (Mensual)" />
                </Columns>
                <HeaderStyle BackColor="lightseagreen" />
            </asp:GridView>
            <asp:Button ID="btnCloseDocAnexo" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseDocAnexo_Click" CausesValidation="false" />
            &nbsp;&nbsp;&nbsp;    
        </div>
    </asp:Panel>

    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 11px 23px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 15px;
            margin: 4px 2px;
            cursor: pointer;
        }

        .button2 {
            background-color: #008CBA;
        }

        .resizing {
            padding: 0px;
            border-style: solid;
            border-width: 1px;
            border-color: #7391BA;
            top: 400px;
        }

        .dragpanel {
            background-color: #FFC0FF;
            height: 400px;
            width: 400px;
            border-bottom-color: black;
        }
    </style>
    <script>
        function rceDocAnexoResize(sender, e) {
            var panelDocAnexo = document.getElementById('<%= Panl1.ClientID %>');
            var imgDocAnexo = document.getElementById('<%= Campos.ClientID %>');
            if (imgDocAnexo !== null) {
                imgDocAnexo.width = panelDocAnexo.width;
                imgDocAnexo.height = panelDocAnexo.height;
            }

        }
    </script>
</asp:Content>
