<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales"TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">


    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align:left;width:200px;" >
                                            Fecha Inicial:
                                        </td>
                                        <td style="text-align:left; width:200px">
                                            Fecha Final:
                                            &nbsp;</td>
                                        <td style="text-align:left">
                                             Oficina:
                                            &nbsp;</td>
                                        <td style="text-align:left"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left; ">
                                               <ucFecha:fecha ID="txt_fechainicial" runat="server" CssClass="textbox" />
                                        </td>
                                        <td style="text-align:left;">
                                            <ucFecha:fecha ID="txt_fechafinal" runat="server" CssClass="textbox" />
                                        </td>
                                        <td style="text-align:left">
                                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" 
                                                Width="230px" AppendDataBoundItems="True">
                                            
                                            </asp:DropDownList>
                                          
                                            
                                        </td>
                                        <td style="text-align:left"></td>

                                    </tr>
                                </table>
                           
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr width="100%" />
            <asp:Panel ID="pDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                             Text="Exportar a Excel" />
                        &nbsp;
                        <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                            Text="Visualizar Informe" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div style="max-width: 1200px;max-height:700px; overflow: scroll">
                            <asp:GridView ID="gvLista" Width="100%" runat="server" AutoGenerateColumns="False" 
                                 HeaderStyle-CssClass="gridHeader"  DataKeyNames="codigo_cdat" OnRowEditing="gvLista_RowEditing"
                                PagerStyle-CssClass="gridPager" Style="font-size: xx-small"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />  
                                    <asp:BoundField DataField="codigo_cdat" HeaderText="Id" />
                                    <asp:BoundField DataField="numero_cdat" HeaderText="CDAT" />
                                    <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}"/>
                                    <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}"/>
                                    <asp:BoundField DataField="valor" HeaderText="Valor CDAT" DataFormatString="{0:c}"/>
                                    <asp:BoundField DataField="plazo" HeaderText="Plazo" />
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                    <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                                    <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                    <asp:BoundField DataField="tasa_efectiva" HeaderText="Tasa Efectivo" />
                                    <asp:BoundField DataField="tasa_nominal" HeaderText="Tasa Nominal" />
                                    <asp:BoundField DataField="nom_modalidad" HeaderText="Modalidad" />
                                    <asp:BoundField DataField="nom_periodo" HeaderText="Periodo Int" />
                                    <asp:BoundField DataField="interes_causado" HeaderText="Int. Causado" />
                                    <asp:BoundField DataField="interes_mes" HeaderText="Int. Mes" />
                                    <asp:BoundField DataField="interes_retencion" HeaderText="Int. Retencion" />
                                    </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        
                        </div>
                        <table >
                                    <tr>
                               
                                         <td style="width:400px; text-align:left;">
                                            <asp:Label ID="lblTotalCDAT" runat="server" Text=" Total Valor CDAT " />
                                  <uc2:decimales ID="txtTotalCDAT" runat="server" style="text-align: right;" enabled="false"/>
                                         </td>
                                        <td style="width:400px; text-align:left;">
                                            <asp:Label ID="Label2" runat="server" Text=" Total Int. Causado " />
                                 <uc2:decimales ID="txtTotalCau" runat="server" style="text-align: right;" enabled="false"/>
                                         </td>
                                        <td style="width:400px; text-align:left;">
                                            <asp:Label ID="Label3" runat="server" Text=" Total Int. Mes " />
                                  <uc2:decimales ID="txtTotalMes" runat="server" style="text-align: right;" enabled="false"/>
                                         </td>
                                        <td style="width:420px; text-align:left;">
                                            <asp:Label ID="Label4" runat="server" Text=" Total Int. Retencion" />
                                 <uc2:decimales ID="txtTotalRet" runat="server" style="text-align: right;" enabled="false"/>
                                         </td>
                                    </tr>
                             </table>
                        
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnDatos_Click"
                        Text="Visualizar Datos" Height="30px"/>
                    <br />
                    <br />
                    <hr width="100%" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                        Width="100%">
                        <LocalReport ReportPath="Page\CDATS\ReporteVencimientos\rptCdatVencimiento.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
            </asp:View>
        <asp:View ID="View2" runat="server">
            <br /><br />
            <table width="100%">
                <tr>
                    <td style="text-align:left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8"  Height="25px" Width="130px"
                             Text="Visualizar Datos" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="130px"
                            Text="Imprimir"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            height="600px" runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>            
                <tr>
                    <td>                        
                                  
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>

