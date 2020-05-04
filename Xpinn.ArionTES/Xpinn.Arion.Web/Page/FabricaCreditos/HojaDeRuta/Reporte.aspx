<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Reporte.aspx.cs" Inherits="Lista" Title=".: Xpinn - HojaDeRuta :." %>

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
                        <td class="tdD" style="font-weight: 700; width: 223px;">
                            &nbsp;</td>
                        <td class="tdD" style="text-align: left; margin-left: 40px;">
                           
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="width: 230px">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="dropdown" 
                                Height="24px" AutoPostBack="True" 
                                onselectedindexchanged="ddlOficina_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 138px; text-align: left;">
                            <span style="font-weight: normal">Estado</span>&nbsp;<br />
                            <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" 
                                CssClass="dropdown" Height="23px" 
                                onselectedindexchanged="ddlEstado_SelectedIndexChanged" Width="193px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="font-weight: 700; width: 223px;">
                            Fecha Proceso
                            <br />
                            <asp:TextBox ID="txtFechaProceso" runat="server" AutoPostBack="True" 
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="178px" />
                            <asp:CalendarExtender ID="txtFechaProceso_CalendarExtender" runat="server" 
                                Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtFechaProceso">
                            </asp:CalendarExtender>
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="width: 230px">
                            &nbsp;
                        </td>
                        <td class="tdI" style="width: 278px">
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
            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="numerocredito" 
                ForeColor="Black" GridLines="Vertical" 
                onpageindexchanging="gvLista_PageIndexChanging" 
                onrowediting="gvLista_RowEditing" 
                onselectedindexchanged="gvLista_SelectedIndexChanged" PageSize="20" 
                style="margin-right: 0px" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="NumeroCredito" HeaderText="Número Crédito" />
                    <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                    <asp:BoundField DataField="NombreDeudor" HeaderText="Nombre Deudor" />
                    <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" />
                    <asp:BoundField DataField="asesor" HeaderText="Ejecutivo" />
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
            <asp:Button ID="btnDatos0" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />
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
                        <rsweb:ReportViewer ID="rvReporteMensajeria" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\HojaDeRuta\ReporteMensajeria.rdlc">
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
