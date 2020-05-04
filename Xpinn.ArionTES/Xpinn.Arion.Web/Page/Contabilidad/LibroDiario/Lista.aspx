<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="../../../General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

    $(document).ready(function () {
        gridviewScroll();
    });

    function gridviewScroll() {
        $('#<%=gvLista.ClientID%>').gridviewScroll({
            width: CalcularAncho(),
            height: 500,
            arrowsize: 30,
            varrowtopimg: "../../../Images/arrowvt.png",
            varrowbottomimg: "../../../Images/arrowvb.png",
            harrowleftimg: "../../../Images/arrowhl.png",
            harrowrightimg: "../../../Images/arrowhr.png"
        });
        $('#<%=gvListaO.ClientID%>').gridviewScroll({
            width: CalcularAncho(),
            height: 500,
            arrowsize: 30,
            varrowtopimg: "../../../Images/arrowvt.png",
            varrowbottomimg: "../../../Images/arrowvb.png",
            harrowleftimg: "../../../Images/arrowhl.png",
            harrowrightimg: "../../../Images/arrowhr.png"
        });
    } 

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvLibroDiario" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="70%">
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align:left">                          
                           <tr>
                               <td class="tdI" colspan="3" style="text-align:left">
                                   Período</td>
                               <td class="tdD" style="text-align:left">
                                   <asp:Label ID="lblCentroCosto" runat="server" Text="Centro de Costo"></asp:Label> </td>
                               <td class="tdD" colspan="2">
                                   Moneda</td>
                               <td class="tdD" colspan="2" style="text-align:left">
                                   &nbsp;</td>
                               <td class="tdD">
                                   &nbsp;</td>
                               <td class="tdD">
                                   &nbsp;</td>
                           </tr>
                            <tr>
                                <td class="tdI" style="text-align:left">                                    
                                    <uc:fecha ID="txtFecIni" runat="server" CssClass="textbox" />
                                </td>
                                <td class="tdI" style="text-align:center">
                                    a</td>
                                <td class="tdI" style="text-align:left">                                    
                                    <uc:fecha ID="txtFecFin" runat="server" Width="85px" />
                                </td>
                                <td class="tdD" style="text-align:left">
                                    <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="dropdown" Width="200px">
                                    </asp:DropDownList>                                  
                                    <br/>
                                </td>
                                <td class="tdD" colspan="2" style="width: 226px">
                                    <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" 
                                Width="151px" /></td>
                                <td class="tdD" colspan="2">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdI" colspan="11" style="text-align:left; font-size: x-small;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkFecha"  runat="server" Text="Agrupado por Fecha" 
                                            AutoPostBack="True" oncheckedchanged="chkFecha_CheckedChanged" 
                                            style="font-size: x-small" />&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkCuenta" runat="server" Text="Agrupado por Cuenta" 
                                            AutoPostBack="True" oncheckedchanged="chkCuenta_CheckedChanged" 
                                            style="font-size: x-small" />
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br/>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td><hr width="100%" /></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />
                        &nbsp;
                        <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                            onclick="btnInforme_Click" Text="Visualizar Informe" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>                        
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="False" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" style="font-size: small" 
                            ShowHeaderWhenEmpty="True" >
                            <Columns>                        
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" />
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta"><ItemStyle Width="250px" HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="debito" HeaderText="Débito" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="credito" HeaderText="Crédito" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:GridView ID="gvListaO" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="False" Visible="false"
                            OnPageIndexChanging="gvListaO_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" style="font-size: small" 
                            ShowHeaderWhenEmpty="True" >
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" />
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta"><ItemStyle Width="250px" HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="debito" HeaderText="Débito" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="credito" HeaderText="Crédito" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>                        
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align:left">
                        <hr width="100%" />
                        &nbsp;
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                            onclick="btnDatos_Click" Text="Visualizar Datos" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvLibAux" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%" Height="600">
                            <LocalReport ReportPath="Page\Contabilidad\LibroDiario\ReporteLibroDiario.rdlc">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">    
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>