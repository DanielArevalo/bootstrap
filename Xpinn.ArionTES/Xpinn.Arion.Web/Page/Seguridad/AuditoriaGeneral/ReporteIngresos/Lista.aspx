<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwData" runat="server">
        <asp:Panel ID="pConsulta" runat="server" style="width: 70%;">
            <table style="width: 70%;">
                <tr>
                    <td style="font-size: x-small; text-align:left" colspan="5">
                        <strong>Criterios de Búsqueda :</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align:left">
                        Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" 
                            Width="121px"></asp:TextBox>
                     </td>
                    <td style="width: 120px; text-align:left">
                        Usuario<br />
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" 
                            Width="300px"></asp:TextBox>
                     </td>
                    <td style="text-align:left; width:140px ">
                        Fecha Inicial<br />
                        <ucFecha:fecha ID="txtFechaIni" runat="server" />
                    </td>
                    <td style="text-align:center; width:10px">
                        <br />
                        a
                    </td>
                    <td style="text-align:left; width:140px ">
                        Fecha Final<br />
                        <ucFecha:fecha ID="txtFechaFin" runat="server" />
                    </td>                    
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PanelListado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align:left">
                    <strong>Listado</strong><br />

                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                            ForeColor="Black" GridLines="Horizontal" PageSize="20"
                            DataKeyNames="cod_ingreso"
                            onpageindexchanging="gvLista_PageIndexChanging" style="font-size:small">                           
                            <Columns>                                                            
                                <asp:BoundField DataField="cod_ingreso" HeaderText="Codigo"><ItemStyle HorizontalAlign="Left" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ><ItemStyle HorizontalAlign="Left" Width="35%"/></asp:BoundField>
                                <asp:BoundField DataField="fecha_horaingreso" HeaderText="Fecha Ingreso" ><ItemStyle HorizontalAlign="Left" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="fecha_horasalida" HeaderText="Fecha Salida" ><ItemStyle HorizontalAlign="Left" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="direccionip" HeaderText="Direccion IP" ><ItemStyle HorizontalAlign="Left" Width="20%" /></asp:BoundField>                                                                
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px" OnClick="btnDatos_Click"
                Text="Visualizar Datos" />
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <rsweb:reportviewer id="rvIngresosUsuario" runat="server" font-names="Verdana" font-size="8pt"
                            enabled="false" interactivedeviceinfos="(Colección)" waitmessagefont-names="Verdana"
                            waitmessagefont-size="10pt" width="100%"><localreport reportpath="Page\Seguridad\Auditoria\ReporteIngresos\rptIngresosUsuarios.rdlc"><datasources><rsweb:ReportDataSource /></datasources></localreport></rsweb:reportviewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
