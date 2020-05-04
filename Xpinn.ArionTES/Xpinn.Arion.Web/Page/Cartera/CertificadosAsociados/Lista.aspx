<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register src="../../../General/Controles/ctlFormatoDocum.ascx" tagname="FormatoDocu" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
        <asp:View ID="vmirar" runat="server">
            <br />
            <br />
            <div style="text-align: left">
                Fecha proyectada<br />
                <ucFecha:fecha ID="txtfechaProy" runat="server" />
            </div>
            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
        </asp:View>
        <asp:View ID="vReporte" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            height="500px" runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="RpviewEstado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Height="500px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="100%">
                            <LocalReport ReportPath="Page/Cartera/CertificadosAsociados/Reportcertificadosasociados.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwfinal" runat="server">
            <table>
                <tr>
                    <td style="text-align: left">
                        <br /><br />
                        <strong>IDENTIFICACION:</strong>&nbsp;<asp:Label ID="lblIdentificacion" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        <strong>NOMBRES:</strong>&nbsp;<asp:Label ID="lblNombre" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <br />
                        <strong>CREDITOS ACTIVOS DE LA PERSONA </strong>
                        <br />
                        Seleccione los créditos a los cuales les desea generar el certificado.
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="gvListas" runat="server" AutoGenerateColumns="False" Style="text-align: left" 
                            Width="100%" Height="100%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Num Radicación">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="linea_credito" HeaderText="Línea">
                                    <HeaderStyle Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_prox_pago" HeaderText="Fecha Prox. Pago" DataFormatString="{0:d}">
                                    <HeaderStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_a_pagar" HeaderText="Vencimiento" DataFormatString="{0:n}">
                                    <HeaderStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto" HeaderText="Monto Aprobado" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_a_pagar_CE" HeaderText="Vr. a Pagar" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_a_pagar" HeaderText="Total a Pagar" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="valor_cuota" HeaderText="Vr. Cuota" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Seleccionados" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkManejaCP" runat="server" EnableViewState="true" Checked="true"
                                            Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblMensajeActivos" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">                       
                        <br /><br /><br />
                        <strong>CREDITOS TERMINADOS</strong>
                        <br />
                        Seleccione los créditos a los cuales desea generar paz y salvo.
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="gvTerminados" runat="server" AutoGenerateColumns="False" Style="text-align: left"
                            Width="100%" Height="100%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Num Radicación">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="linea_credito" HeaderText="Línea">
                                    <HeaderStyle Width="160px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_prox_pago" HeaderText="Fecha Proximo Pago" DataFormatString="{0:d}">
                                    <HeaderStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto_aprobado" HeaderText="Vencimiento" DataFormatString="{0:n}">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>      
                                <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:n}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                                     
                                <asp:BoundField DataField="estado" HeaderText="Estado">
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Seleccionados">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkselecciona" runat="server" EnableViewState="true" Checked="false"
                                            Enabled="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblMensajeTerminados" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                     <td>
                           Observaciones :   <br/>
                          <asp:TextBox ID="TxtObservaciones" Width ="800"  Height ="100" TextMode ="MultiLine"  MaxLength ="500" runat="server" Text="" />
                           <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
                     </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
