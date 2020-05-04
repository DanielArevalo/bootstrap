<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlOficinaCon.ascx" TagName="oficina" TagPrefix="ucofi" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
          <table style="width: 1003px">
                <tr>
                    <td colspan="3" style="text-align:left">
                        <strong>Ingrese el periodo de fechas para las cuales desea consultar el reporte</strong>
                    </td>
                </tr>        
                <tr>
                    <td style="text-align: left; width:10%">
                        Fecha Inicial<br />
                        <ucFecha:fecha ID="txtFechaIni" runat="server" CssClass="textbox"/>                
                    </td>
                    <td style="text-align: left; width:10%">
                       Fecha Final<br />
                       <ucFecha:fecha ID="txtFechaFin" runat="server" CssClass="textbox"/>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:left">
                        <strong>Ingrese criterios de Búsqueda</strong>
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="text-align: left;width:100%" colspan="3">
                        <table width="80%">
                            <tr>
                                <td style="text-align: left; width: 35%">
                                    Oficina<br />
                                    <ucofi:oficina ID="ddlofi" runat="server" CssClass="textbox" Width="98%" />
                                </td>
                                <td style="text-align: left; width: 35%">
                                    Usuario<br />
                                    <asp:DropDownList ID="ddlCajero" runat="server" CssClass="textbox" Width="98%" />
                                </td>
                                <td style="text-align: left; width: 15%">
                                    Moneda<br />
                                    <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="textbox" Width="98%" />
                                </td>
                                <td style="text-align: left; width: 15">
                                    Forma Pago<br />
                                    <asp:DropDownList ID="ddlTipoPago" runat="server" AutoPostBack="True" 
                                        CssClass="textbox" onselectedindexchanged="ddlTipoPago_SelectedIndexChanged" 
                                        Width="98%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr style="width: 100%" />
                    </td>
                </tr>
            </table>   
            
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div id="divLista" runat="server" style="overflow: scroll; max-height:500px">
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvLista_PageIndexChanging" AllowPaging="false" HeaderStyle-Height="28px"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-Font-Size="x-small"
                                RowStyle-CssClass="gridItem" style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>                           
                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod. Ope">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomtipo_comp" HeaderText="T. Comp">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomTipo_Ope" HeaderText="Tipo Operacion">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomTipo_Pago" HeaderText="Tipo Pago">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Cliente">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_documento" HeaderText="Documento">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor Neto" DataFormatString="{0:n0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_moneda" HeaderText="Moneda">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>              
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            </div>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                            <asp:TextBox ID="txtTotalMovs" runat="server" Width="90px" Enabled="False" 
                                style="direction:rtl"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </asp:View>
        <asp:View ID="vwReporte" runat="server">
          <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px"
            onclick="btnDatos_Click" Text="Visualizar Datos" />
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt" Enabled="false"
                            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                            Width="100%">
                            <localreport reportpath="Page\Tesoreria\ReporteMovimientos\rptMovimientos.rdlc">
                            <datasources>
                            <rsweb:ReportDataSource />
                            </datasources>
                            </localreport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
  
</asp:Content>
