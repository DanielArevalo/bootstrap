<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="CalificacionCartera.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Comprobante Clasificación :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#demo7').click(function () {
                $.blockUI({ message: null });
                setTimeout($.unblockUI, 2000);
            });
        });
    </script>      
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
                    <asp:Button ID="btnValidar" runat="server" CssClass="btn8" Text="Validar Parametrización" Width="182px"
                        Height="26px" OnClick="btnValidar_Click"/>
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
                   <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>--%>
                            <asp:TabContainer ID="tcInfFinNeg" runat="server" ActiveTabIndex="0" Height="338px"
                                ScrollBars="Auto" Width="100%">
                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Detallado" ScrollBars="Auto">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn8" 
                                                        Height="28px" OnClick="btnExportarExcel_Click" Text="Exportar a Excel" 
                                                        Width="124px" />
                                                    &nbsp;
                                                    <asp:GridView ID="gvdetallado" runat="server" Width="100%" PageSize="3"
                                                        GridLines="Horizontal" ShowHeaderWhenEmpty="True" 
                                                        AutoGenerateColumns="False" style="font-size:x-small" 
                                                        Height="187px">
                                                        <Columns>
                                                            <asp:BoundField DataField="FECHA_HISTORICO" HeaderText="Fecha" />
                                                            <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Radicación" />
                                                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Linea " />
                                                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre de la Linea" >
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina" />
                                                            <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" />
                                                            <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificación" />
                                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                                            <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificación" />
                                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma de Pgo" />
                                                            <asp:BoundField DataField="TIPO_GARANTIA" HeaderText="Tipo Garantia" />
                                                            <asp:BoundField DataField="COD_CATEGORIA_CLI" HeaderText="Categoria" />
                                                            <asp:BoundField DataField="COD_ATR" HeaderText="Atr" />
                                                            <asp:BoundField DataField="NOMBRE_ATRIBUTO" HeaderText="Atributo" />
                                                            <asp:BoundField DataField="SALDO" HeaderText="Saldo" DataFormatString="{0:N}" />
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
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Consolidado" Visible="true"
                                    Width="100%">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnExportarExcelCon" runat="server" CssClass="btn8" 
                                                        Height="28px" Text="Exportar a Excel" 
                                                        Width="124px" onclick="btnExportarExcelCon_Click" />
                                                    &nbsp;
                                                    <asp:GridView ID="gvConsolidado" runat="server" Width="100%" PageSize="3" ShowHeaderWhenEmpty="True" 
                                                        AutoGenerateColumns="False" style="font-size:x-small">
                                                        <Columns>
                                                            <asp:BoundField DataField="FECHA_HISTORICO" HeaderText="Fecha" />
                                                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Linea " />
                                                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre de la Linea" >
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina" />
                                                            <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" />
                                                            <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificación" />
                                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma de Pgo" />
                                                            <asp:BoundField DataField="TIPO_GARANTIA" HeaderText="Tipo Garantia" />
                                                            <asp:BoundField DataField="COD_CATEGORIA_CLI" HeaderText="Categoria" />
                                                            <asp:BoundField DataField="COD_ATR" HeaderText="Atr" />
                                                            <asp:BoundField DataField="NOMBRE_ATRIBUTO" HeaderText="Atributo" />
                                                            <asp:BoundField DataField="SALDO" HeaderText="Saldo"  DataFormatString="{0:N}" />
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
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                    <%--    </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </asp:Panel>
                   
                   <asp:Label ID="lblErrores" runat="server" Text=""></asp:Label>
                   <asp:Panel ID="panelErrores" runat="server" Visible="false">
                        &nbsp;                       
                       <div style="overflow:scroll;max-height:500px; width:100%; text-align:center " >
                        <asp:GridView ID="gvErrores" runat="server" Width="98%" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: x-small" HorizontalAlign="Center"
                            PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                            <Columns>
                                <asp:TemplateField HeaderText="Error">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDato" Enabled="false" runat="server" Width="98%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle Font-Size="XX-Small" />
                        </asp:GridView>
                       </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

</asp:Content>
