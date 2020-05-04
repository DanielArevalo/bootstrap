<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Libro Auxiliar :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register Src="~/General/Controles/ctlMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvLibroAuxiliar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
            <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" style="width: 865px;">
                <tr>
                    <td colspan="2" style="text-align: left;width:285px">
                        Centro de Costo<br />
                        <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Width="280px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 180px">
                        <br />
                        <asp:CheckBox ID="cbRango" runat="server" Text="Rango de Cuentas" Width="95%" AutoPostBack="True"
                            OnCheckedChanged="cbRango_CheckedChanged" />
                    </td>
                    <td style="width: 200px; text-align: left">
                        <asp:Label ID="lblCuentaContable" runat="server" Text="Cuenta Contable"></asp:Label><br />
                        <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                            Style="text-align: left" BackColor="#F4F5FF" Width="80px" OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                        <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                            OnClick="btnListadoPlan_Click" />
                        <br />
                        <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                        <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                            Width="95%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                    </td>
                    <td style="width: 200px; text-align: left">
                       <asp:Panel ID="PanelCuentaFin" runat="server">
                        <asp:Label ID="lblCuentaContableFin" runat="server" Text="Hasta Cuenta Contable"></asp:Label><br />
                        <cc1:TextBoxGrid ID="txtCodCuentaFin" runat="server" AutoPostBack="True" CssClass="textbox"
                            Style="text-align: left" BackColor="#F4F5FF" Width="80px" OnTextChanged="txtCodCuentaFin_TextChanged"></cc1:TextBoxGrid>
                        <cc1:ButtonGrid ID="btnListadoPlan1" CssClass="btnListado" runat="server" Text="..."
                            OnClick="btnListadoPlan1_Click" /><br />
                        <uc1:ListadoPlanCtas ID="ListadoPlanCtas1" runat="server" />
                        <cc1:TextBoxGrid ID="txtNomCuenta1" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                            Width="95%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                       </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 145px">
                        Período<br />
                        <uc:fecha ID="txtFecIni" runat="server" CssClass="textbox" Width="85px" />
                        a
                    </td>
                    <td style="text-align: left; width: 140px">
                        <br />
                        <uc:fecha ID="txtFecFin" runat="server" CssClass="textbox" Width="85px" />
                    </td>
                    <td style="text-align: left; width: 180px">
                        <br />
                        <asp:CheckBox ID="chkAgrupado" runat="server" Text="Agrupado por Cuentas" Width="95%" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Ordenado Por<br />
                        <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="textbox" Width="200px">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="0">Fecha</asp:ListItem>
                            <asp:ListItem Value="1">Tipo de Comprobante</asp:ListItem>
                            <asp:ListItem Value="2">Número Comprobante</asp:ListItem>
                            <asp:ListItem Value="3">Tercero</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdD" style="text-align: left; width: 200px">
                        Moneda<br />
                        <ctl:ddlMoneda ID="ddlTipoMoneda" runat="server" Width="100px"/>
                    </td>
                </tr>
            </table>
            </asp:Panel>
            <hr width="100%" />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
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
                        <div style="max-width: 1200px;max-height:700px; overflow: scroll">
                            <asp:GridView ID="gvLista" Width="100%" runat="server" AutoGenerateColumns="False"
                                 OnRowDataBound="gvLista_RowDataBound"
                                OnPageIndexChanging="gvLista_PageIndexChanging"  PageIndex="5"
                                HeaderStyle-CssClass="gridHeader"
                                PagerStyle-CssClass="gridPager" Style="font-size: xx-small"
                                ShowHeaderWhenEmpty="True" AllowPaging="True">
                                <Columns>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" />
                                    <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Num.Comp." />
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp." />
                                    <asp:BoundField DataField="num_sop" HeaderText="Nro Soporte" />
                                    <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo Mov" />
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c2}" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="debito" HeaderText="Débito" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="credito" HeaderText="Crédito" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identific_tercero" HeaderText="Ident.Tercero" />
                                    <asp:BoundField DataField="nombre_tercero" HeaderText="Nombre Tercero" />
                                    <asp:BoundField DataField="naturaleza" HeaderText="Naturaleza" />
                                    <asp:BoundField DataField="depende_de" HeaderText="Depende" />
                                    <asp:BoundField DataField="centro_costo" HeaderText="C/C" />
                                    <asp:BoundField DataField="centro_gestion" HeaderText="C/G" />
                                    <asp:BoundField DataField="regimen" HeaderText="Regimen" />
                                    <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo" />
                                    <asp:BoundField DataField="base_minima" HeaderText="Base" />
                                    <asp:BoundField DataField="porcentaje_impuesto" HeaderText="Porcentaje" />
                                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br /><br />
            <table width="100%">
                <tr>
                    <td style="text-align:left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8"  Height="25px" Width="130px"
                            onclick="btnDatos_Click" Text="Visualizar Datos" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="130px"
                            Text="Imprimir" OnClick="btnImprime_Click" />
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
                        <rsweb:ReportViewer ID="rvLibAux" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\Contabilidad\LibroAuxiliar\ReporteLibroAuxiliar.rdlc">
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

</asp:Content>