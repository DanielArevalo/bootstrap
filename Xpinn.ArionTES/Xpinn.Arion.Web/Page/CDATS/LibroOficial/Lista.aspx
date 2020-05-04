<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Libro Oficial :." %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">    
    <asp:View ID="vwData" runat="server">

        <table style="width: 800px">
            <tr>
                <td colspan="4" style="text-align: left">
                    <strong>Criterios de Busqueda :</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; text-align: left">
                    Fecha Inicial<br />
                    <ucFecha:fecha ID="txtFechaIni" runat="server" />
                </td>
                <td style="text-align: left; width: 140px">
                    Fecha Final<br />
                    <ucFecha:fecha ID="txtFechaFin" runat="server" />
                </td>
                <td style="text-align: left; width: 200px">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="95%" AppendDataBoundItems="True" />
                </td>
                <td style="width: 320px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr style="width: 800px" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="panelGrilla" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <strong>Listado de Servicios :</strong>
                        <br />
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="codigo_cdat,cod_oficina" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                            OnPageIndexChanging="gvLista_PageIndexChanging" PagerStyle-CssClass="gridPager"
                            PageSize="20" RowStyle-CssClass="gridItem">
                            <Columns>
                                <asp:BoundField DataField="codigo_cdat" HeaderText="Cod">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_cdat" HeaderText="Num. CDAT">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fec Apertura">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_vencimiento" DataFormatString="{0:d}" HeaderText="Fec Vencimiento">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" DataFormatString="{0:n}" HeaderText="Valor">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tasa_efectiva" HeaderText="Tasa Efectiva">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tasa_nominal" HeaderText="Tasa Nominal">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nommodalidadint" HeaderText="Modalidad Int">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomperiodicidad" HeaderText="Period">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="apellidos" HeaderText="Apellidos">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="modalidad" HeaderText="Conjunción">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomestado" HeaderText="nomestado">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomoficina" HeaderText="oficina" Visible="false">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <div style="overflow: scroll; width: 100%">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:View>
    <asp:View ID="vwReporte" runat="server">
        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="btnDatos" runat="server" CssClass="btn8" OnClick="btnDatos_Click"
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
                        <LocalReport ReportPath="Page\CDATS\LibroOficial\rptLibroOficial.rdlc">
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

    <br />
    <br />

     <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
