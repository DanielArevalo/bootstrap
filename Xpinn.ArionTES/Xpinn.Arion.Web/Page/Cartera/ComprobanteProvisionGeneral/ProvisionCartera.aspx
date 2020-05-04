<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="ProvisionCartera.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Comprobante Provisión General :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td class="columnForm50" style="width: 312px">
                    <asp:Label ID="LabelFecha" runat="server" Text="Fecha"></asp:Label>
                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" Height="20px" MaxLength="10"
                        Width="188px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                        TargetControlID="txtFechaIni">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="columnForm50" style="width: 312px">
                    <asp:Button ID="btnInforme" runat="server" CssClass="btn8" Text="Consultar" Width="182px"
                        Height="26px" OnClick="btnInforme_Click" />
                    <br />
                </td>
                <td class="tdI">
                    &nbsp;
                </td>
                <td class="tdI">
                    <br />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="UpdatePanel3" runat="server">                        
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;
                                        <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn8" Height="28px"
                                            OnClick="btnExportarExcel_Click" Text="Exportar a Excel" Width="124px" />
                                        &nbsp;
                                        <asp:GridView ID="gvdetallado" runat="server" Width="80%" PageSize="3"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: x-small"
                                            Height="187px">
                                            <Columns>
                                                <asp:BoundField DataField="fecha_corte" HeaderText="Fecha Historico" DataFormatString="{0:d}" />                                                     
                                                <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina" />
                                                <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" >
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                                                                                         
                                                <asp:BoundField DataField="valor_sinlibranza" HeaderText="Cartera Total Sin Libranza" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="total_provision_sinlibranza" HeaderText="Total Provision Sin Libranza" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="provision_sinlibranza" HeaderText="Provision Sin Libranza" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="valor_conlibranza" HeaderText="Cartera Total Con Libranza" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="total_provision_conlibranza" HeaderText="Total Provision Con Libranza" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="provision_conlibranza" HeaderText="Provision Con Libranza" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridPager" />
                                            <RowStyle CssClass="gridItem" />
                                            <SelectedRowStyle Font-Size="XX-Small" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTotRegs" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel> 
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel>      
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
