<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: <%=ancho%>,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }
 </script>

 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    

    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">        
        <asp:View ID="vwLista" runat="server">
            <table style="width: 95%" id="tabladesc" runat="server">
                <tr>
                    <td style="text-align: left">Empresa Recaudadora<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="280px"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha de Aplicacion" /><br />
                        <ucFecha:fecha ID="ucFechaAplicacion" runat="server" Enabled="False" Requerido="False" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumeroLista" runat="server" Visible="True" Text="Núm. Aplicación" /><br />
                        <asp:TextBox ID="txtNumeroLista" runat="server" Enabled="false" Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumeroNovedad" runat="server" Visible="True" Text="Número de Novedad" /><br />
                        <asp:TextBox ID="txtNumeroNovedad" runat="server" Enabled="false" Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPeriodo" runat="server" Visible="True" Text="Período" /><br />
                        <ucFecha:fecha ID="txtPeriodo" runat="server" Enabled="False" Requerido="False" Width="70px" />
                    </td>
                    <td>
                        <br />
                        <asp:CheckBox ID="cbDetallado" runat="server" Checked="false" Text="Detallado"
                            AutoPostBack="True" OnCheckedChanged="cbDetallado_CheckedChanged" Visible="false" />
                    </td>
                    <td>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="3" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;">
                            <Columns>
                                <asp:TemplateField>                                  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"  OnCheckedChanged="Check_Clicked"/>
                                    </HeaderTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxgv" runat="server" />
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                   
                                <asp:BoundField DataField="numero_recaudo" HeaderText="Num Novedad">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_producto" HeaderText="Cantidad de Productos" />                            
                                <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Periodo"  DataFormatString="{0:d}" />                   
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                        </asp:GridView>
                    </td>
                </tr>              
           
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px" OnClick="btnDatos_Click"
                Text="Visualizar Datos" />
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvConsultaRecaudo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Page\RecaudosMasivos\ConsultarRecaudo\rptConsultaRecaudo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Page\RecaudosMasivos\ConsultarRecaudo\rptReporte.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvConsolidado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Page\Reporteador\ExtractoRecaudoMasivo\rptDescuentos.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
    
</asp:Content>
