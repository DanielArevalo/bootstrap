<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" MasterPageFile="~/General/Master/site.master"%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvLista" runat="server">  
        <asp:View ID="vGridPlan" runat="server">
        <hr noshade style="width: 98%">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width:101%;">
                    <tr>
                        <td colspan="6">
                            <div style="font-size: xx-small">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            No. Crédito:
                            <br />
                        </td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtNumRadic" runat="server" CssClass="textbox2" 
                                Enabled="False" Width="139px" ontextchanged="txtNumRadic_TextChanged"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 61px;">
                            LíneaCrédito:</td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox2" Enabled="False" 
                                style="margin-left: 0px" Width="54px"></asp:TextBox>
                            <asp:TextBox ID="txtNombreLinea" runat="server" CssClass="textbox2" 
                                Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            Identific.:</td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtIdentific" runat="server" Enabled="False" Width="139px" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 61px;">
                            Nombres:</td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtNombre" runat="server" Enabled="False" Width="326px" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            Plazo:</td>
                        <td style="width: 29px; text-align:left">
                            <asp:TextBox ID="txtPlazo" runat="server" Enabled="False" Width="43px" 
                                CssClass="textbox2" Height="22px"></asp:TextBox>
                        </td>
                        <td style="text-align: left" colspan="2">
                            Monto&nbsp; Crédito:&nbsp;&nbsp;
                            <br />
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMonto" runat="server" 
                                style="text-align: right; margin-left: 0px" Width="150px" Enabled="False" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 97px; height: 30px;">
                            Periodicidad:</td>
                        <td style="width: 29px; height: 30px;">
                            <asp:TextBox ID="txtPeriodicidad" runat="server" Enabled="False" Width="139px" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                        <td style="text-align: left" colspan="4" rowspan="3">
                            <asp:ListBox ID="lbSumados" runat="server" Width="397px" Height="88px" 
                                Enabled="False"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            For.Pago:</td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtFormaPago" runat="server" Width="139px" Enabled="False" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px; height: 32px;">
                            Fec. Inicial:</td>
                        <td style="width: 29px; height: 32px;">
                            <asp:TextBox ID="txtFechaInicial" runat="server" Enabled="False" Width="139px" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            Vr.Cuota:</td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtCuota" runat="server" Enabled="False" Width="139px" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                        <td style="text-align: left" colspan="2">
                            &nbsp;Monto&nbsp; Total:</td>
                        <td style="text-align: left" colspan="2">
                            <asp:TextBox ID="txtMontoCalculado" runat="server" Width="150px" style="text-align: right; margin-left: 0px"
                                Enabled="False" CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            Dias Ajuste:</td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtDiasAjuste" runat="server" Enabled="False" Width="139px" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                        <td style="text-align: left" colspan="4" rowspan="3">
                            <asp:ListBox ID="lbDescontados0" runat="server" Width="397px" Enabled="False" 
                                Height="81px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            Tasa Int.Nom:</td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtTasaInteres" runat="server" Enabled="False" Width="139px" 
                                Height="22px" CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            LeyMiPyme:</td>
                        <td style="width: 29px">
                            <asp:TextBox ID="txtLeyMiPyme" runat="server" Width="139px" Enabled="False" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px; height: 26px;">
                            Moneda:</td>
                        <td style="width: 29px; height: 26px;">
                            <asp:TextBox ID="txtMoneda" runat="server" Width="139px" Enabled="False" 
                                CssClass="textbox2"></asp:TextBox>
                        </td>
                        <td style="text-align: left" colspan="2">
                            &nbsp;Vr.Desembolsado</td>
                        <td style="text-align: left; margin-left: 40px;" colspan="2">
                            <asp:TextBox ID="txtVrDesembolsado" runat="server" Width="150px" style="text-align: right; margin-left: 0px"
                                Enabled="False" CssClass="textbox2"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="6" style="height: 4px">
                            <div style="font-size: xx-small">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 4px">
                            <asp:TextBox ID="Txtdireccion" runat="server" Width="56px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="Txtciudad" runat="server" Width="44px"  Visible="False">  </asp:TextBox>
                            <asp:TextBox ID="Txtprimerpago" runat="server" Width="57px" Visible="False">  </asp:TextBox>
                            <asp:TextBox ID="Txtgeneracion" runat="server" Width="43px" Visible="False">  </asp:TextBox>
                            <asp:TextBox ID="Txtcuotas" runat="server" Width="43px" Visible="False">  </asp:TextBox>
                            <asp:TextBox ID="Txtpagare" runat="server" Width="61px" Visible="False">  </asp:TextBox>
                            <asp:TextBox ID="Txtinteresefectiva" runat="server" Width="53px" Visible="False">  </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 4px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                            <ContentTemplate>
                       
                            <asp:GridView ID="gvPlanPagos0" runat="server"  
                                AutoGenerateColumns="False" GridLines="Horizontal" 
                                ShowHeaderWhenEmpty="True" Height="292px" 
                                style="font-size: small"
                                Width="927px" onpageindexchanging="gvPlanPagos_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="numerocuota" HeaderText="No" 
                                        ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechacuota" DataFormatString="{0:dd-MM-yyyy}" 
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sal_ini" DataFormatString="{0:c}" 
                                        HeaderText="Saldo Inicial" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="capital" DataFormatString="{0:c}" 
                                        HeaderText="Capital" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_1" DataFormatString="{0:c}" HeaderText="Int_1" 
                                        ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_2" DataFormatString="{0:c}" HeaderText="Int_2" 
                                        ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"  />
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
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_6" DataFormatString="{0:c}" HeaderText="Int_6" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_7" DataFormatString="{0:c}" HeaderText="Int_7" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_8" DataFormatString="{0:c}" HeaderText="Int_8" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_9" DataFormatString="{0:c}" HeaderText="Int_9" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_10" DataFormatString="{0:c}" HeaderText="Int_10" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_11" DataFormatString="{0:c}" HeaderText="Int_11" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_12" DataFormatString="{0:c}" HeaderText="Int_12" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_13" DataFormatString="{0:c}" HeaderText="Int_13" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_14" DataFormatString="{0:c}" HeaderText="Int_14" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="int_15" DataFormatString="{0:c}" HeaderText="Int_15" 
                                        ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Total" 
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

                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblPlanPagos" runat="server" Text="PLAN DE PAGOS"></asp:Label>
            <asp:GridView ID="gvPlanPagos" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True" Height="292px" 
                style="font-size: small"
                Width="927px" onpageindexchanging="gvPlanPagos_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="numerocuota" HeaderText="No"  
                        ItemStyle-HorizontalAlign="Left" >
                    <ItemStyle HorizontalAlign="Left" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechacuota" HeaderText="Fecha"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:dd-MM-yyyy}" >
                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sal_ini" HeaderText="Saldo Inicial"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" Width="4px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="capital" HeaderText="Capital"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="int_1" HeaderText="Int_1"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="int_2" HeaderText="Int_2"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="int_3" HeaderText="Int_3"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="int_4" HeaderText="Int_4"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="int_5" HeaderText="Int_5"  
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
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
                        ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle HorizontalAlign="Right" Width="3px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sal_fin" HeaderText="Saldo Final"   
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
            <asp:Label ID="LblError" runat="server" 
                style="color: #FF0000; font-weight: 700; "></asp:Label>
            <br />
            <asp:MultiView ID="mvLista0" runat="server">
                <asp:View ID="vGridPlan0" runat="server">
                    &nbsp;<asp:Button ID="btnInforme2" runat="server" CssClass="btn8" 
                        onclick="btnInforme0_Click" onclientclick="btnInforme0_Click" 
                        Text="Visualizar informe" />
                    &nbsp;
                    <asp:Button ID="btnExportar1" runat="server" CssClass="btn8" 
                        onclick="btnExportar0_Click" onclientclick="btnExportar0_Click" 
                        Text="Exportar a excel" />
                    &nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="btn8" onclick="Button1_Click" 
                        style="height: 22px" Text="Generar Talonario" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnPagare0" runat="server" CssClass="btn8" 
                        onclick="BtnPagare_Click" Text="Ver Pagaré" />
                    &nbsp;<asp:Button ID="Btnautorizacion0" runat="server" CssClass="btn8" 
                        onclick="Btnautorizacion_Click" style="height: 22px" Text="Ver Autorización" />
                    &nbsp;
                    <asp:Button ID="btnInforme3" runat="server" CssClass="btn8" 
                        onclick="btnInforme1_Click" onclientclick="btnInforme0_Click" 
                        Text="Visualizar informe Solicitud" />
                </asp:View>
                <asp:View ID="vReportePlan0" runat="server">
                    <br />
                    <asp:Button ID="btnInforme4" runat="server" CssClass="btn8" 
                        onclientclick="btnInforme0_Click" 
                        Text="Regresar" onclick="btnInforme4_Click" />
                    <rsweb:ReportViewer ID="ReportViewersolicitud" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="963px">
                        <LocalReport ReportPath="Page\FabricaCreditos\Solicitud\PlanPagos\ReportSolicitud.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </asp:View>
            </asp:MultiView>
        </asp:View>
        <asp:View ID="vReportePlan" runat="server">
            <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                <LocalReport ReportPath="Page\FabricaCreditos\PlanPagos\ReportePlan.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView> 
</asp:Content>
