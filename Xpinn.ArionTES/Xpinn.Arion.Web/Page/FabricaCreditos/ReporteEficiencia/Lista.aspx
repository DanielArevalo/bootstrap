<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - HojaDeRuta :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvControlTiempos" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI" style="width: 230px">
                            &nbsp; Número de Crédito<br />
                            <asp:TextBox ID="txtNumeroCredito" runat="server" CssClass="textbox" 
                                MaxLength="128" />
                        </td>
                        <td class="tdI" style="width: 292px">
                            Proceso Inicial&nbsp;<br /><asp:DropDownList ID="ddlEstado" runat="server" 
                                CssClass="dropdown" Height="24px" 
                                onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td class="tdI" style="width: 292px">
                            Proceso Final<br />
                            <asp:DropDownList ID="ddlEstado2" runat="server" CssClass="dropdown" 
                                Height="24px" onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="font-weight: 700; width: 223px;">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="dropdown" AutoPostBack="True" 
                                onselectedindexchanged="ddlOficina_SelectedIndexChanged">
                                <asp:ListItem Value="0">Seleccione una Oficina</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align: left">
                           
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="width: 230px">
                            Ejecutivo<br />
                            <asp:DropDownList ID="DdlEjecutivo" runat="server" CssClass="dropdown" 
                                Height="24px" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 292px; text-align: left;">
                            Fecha Proceso Inicial
                            <asp:TextBox ID="txtFechaProcesoIn" runat="server" AutoPostBack="True" 
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                        <td style="width: 292px; text-align: left; font-weight: 700;">
                            Fecha Proceso Final
                            <asp:TextBox ID="txtFechaProcesoFin" runat="server" AutoPostBack="True" 
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                            <asp:CalendarExtender ID="txtFechaProcesoFin_CalendarExtender" runat="server" 
                                Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtFechaProcesoFin">
                            </asp:CalendarExtender>
                        </td>
                        <td class="tdD" style="font-weight: 700; width: 223px;">
                            <asp:CalendarExtender ID="txtFechaProcesoIn_CalendarExtender" runat="server" 
                                Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtFechaProcesoIn">
                            </asp:CalendarExtender>
                        </td>
                        <td class="tdD">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="width: 230px">
                            &nbsp;
                        </td>
                        <td class="tdI" style="width: 292px" colspan="2">
                            &nbsp;
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                onclick="btnInforme_Click" Text="Visualizar Informe" />
                        </td>
                        <td class="tdD" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                onclick="btnExportar_Click" Text="Exportar a excel" />
            <asp:GridView ID="gvLista" runat="server" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="numerocredito" 
                ForeColor="Black" GridLines="Vertical" 
                onpageindexchanging="gvLista_PageIndexChanging" 
                onrowediting="gvLista_RowEditing" 
                onselectedindexchanged="gvLista_SelectedIndexChanged" PageSize="20" 
                style="margin-right: 0px" Width="100%" 
                onrowdatabound="gvLista_RowDataBound" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="numerocredito" Visible="False" />
                    <asp:BoundField DataField="NumeroCredito" HeaderText="Número Crédito" />
                    <asp:BoundField DataField="Identificacion" 
                        HeaderText="Identificación Cliente" />
                    <asp:BoundField DataField="monto" HeaderText="MontoAprobado" 
                        DataFormatString="{0:d}" />
                    <asp:BoundField DataField="nombreaprobador" HeaderText="Aprobador" />
                    <asp:BoundField DataField="proceso1" HeaderText="Proceso1" />
                    <asp:BoundField DataField="fecha1" HeaderText="Fecha Proceso 1" />
                    <asp:BoundField DataField="proceso2" HeaderText="Proceso 2" />
                    <asp:BoundField DataField="fecha2" HeaderText="Fecha_Proceso2" />
                    <asp:BoundField DataField="tiempototal" HeaderText="Tiempo">
                    <ControlStyle BackColor="#FF66CC" />
                    <ItemStyle Font-Bold="True" Font-Overline="False" Font-Size="Smaller" 
                        ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tiempodatacredito" 
                        HeaderText="Dif Fecha Solicitud-fecha Datacredito" />
                    <asp:BoundField DataField="cod_oficina" HeaderText="Oficina" />
                    <asp:BoundField DataField="asesor" HeaderText="Asesor" />
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
            <br />
            <asp:Label ID="lblTotalRegs" runat="server"  />
            <br />
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                        </td>
                </tr>
                <tr>
                    <td>                        
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\ReporteEficiencia\ReporteEficiencia.rdlc">
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
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            //document.getElementById('cphMain_txtNumeroCredido').focus();
        }
        window.onload = SetFocus;

    function OpenPage() {

        window.open(url), '_blank';
    }

    </script>
</asp:Content>
