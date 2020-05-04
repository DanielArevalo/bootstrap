<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="ProvisionCartera.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Comprobante Provisión :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br /><br />
    <asp:Panel ID="panelGeneral" runat="server">
        <table border="0" cellpadding="5" cellspacing="0" width="60%">
            <tr>
                <td>
                    <asp:Label ID="LabelFecha" runat="server" Text="Fecha" /><br />
                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" Height="20px" MaxLength="10" Width="188px" />
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaIni" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
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
        <br /><br />
        <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="PanelReporte" runat="server">
                            <%--<asp:TabContainer ID="tcInfFinNeg" runat="server" ActiveTabIndex="0" Height="338px"
                                ScrollBars="Auto" Width="80%">--%>
                                <%--<asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Detallado" ScrollBars="Auto">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Button ID="Button2" runat="server" CssClass="btn8" Height="28px"
                                                        OnClick="btnExportarExcelCon_Click" Text="Exportar a Excel" Width="124px" />
                                                    &nbsp;
                                                    <asp:GridView ID="gvdetallado" runat="server" Width="80%" PageSize="3"
                                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: x-small"
                                                        Height="187px">
                                                        <Columns>
                                                            <asp:BoundField DataField="FECHA_HISTORICO" HeaderText="Fecha Historico" />
                                                            <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Numero Radicación" />                                                        
                                                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina" />
                                                            <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Linea" />
                                                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre Linea" >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificación" />
                                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificacion" />
                                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma Pago" />
                                                            <asp:BoundField DataField="TIPO_GARANTIA" HeaderText="Tipo Garantia" />
                                                            <asp:BoundField DataField="COD_CATEGORIA" HeaderText="Categoria" />
                                                            <asp:BoundField DataField="COD_ATR" HeaderText="Atr" />
                                                            <asp:BoundField DataField="NOMBRE_ATRIBUTO" HeaderText="Nombre Atributo"> 
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PORC_PROVISION" HeaderText="Porcent Provisión" />
                                                            <asp:BoundField DataField="VALOR_PROVISION" HeaderText="Valor Provisión" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="DIFERENCIA_PROVISION" HeaderText="Diferencia Provisión" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="DIFERENCIA_ACTUAL" HeaderText="Diferencia Actual" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="DIFERENCIA_ANTERIOR" HeaderText="Diferencia Anterior" DataFormatString="{0:N}" />                                                                                                                                         
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
                                </asp:TabPanel>--%>
                                <%--<asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Consolidado" Visible="true" Width="100%">
                                    <ContentTemplate>--%>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Button ID="btnExportarExcelCon" runat="server" CssClass="btn8" Height="28px"
                                                        Text="Exportar a Excel" Width="124px" OnClick="btnExportarExcel_Click" />
                                                    &nbsp;
                                                    <asp:GridView ID="gvConsolidado" runat="server" Width="100%" PageSize="3" 
                                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                        Style="font-size: x-small">
                                                        <Columns>
                                                            <asp:BoundField DataField="FECHA_HISTORICO" HeaderText="Fecha Historico" />    
                                                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Linea" />
                                                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre Linea" />
                                                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina" />
                                                            <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" />
                                                            <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificación" />
                                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                                            <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificacion" />
                                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma Pago" />
                                                            <asp:BoundField DataField="TIPO_GARANTIA" HeaderText="Tipo Garantia" />
                                                            <asp:BoundField DataField="COD_CATEGORIA" HeaderText="Categoria" />
                                                            <asp:BoundField DataField="COD_ATR" HeaderText="Atr" />
                                                            <asp:BoundField DataField="NOMBRE_ATRIBUTO" HeaderText="Nombre Atributo" />
                                                            <asp:BoundField DataField="PORC_PROVISION" HeaderText="Porcent Provisión" />
                                                            <asp:BoundField DataField="VALOR_PROVISION" HeaderText="Valor Provisión" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="DIFERENCIA_PROVISION" HeaderText="Diferencia Provisión" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="DIFERENCIA_ACTUAL" HeaderText="Diferencia Actual" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="DIFERENCIA_ANTERIOR" HeaderText="Diferencia Anterior" DataFormatString="{0:N}" />
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
                                                </td>
                                            </tr>
                                        </table>
                                    <%--</ContentTemplate>
                                </asp:TabPanel>--%>
                            <%--</asp:TabContainer>--%>
                    </asp:Panel>
                    <asp:Panel ID="panelErrores" runat="server" Visible="false">
                        &nbsp;
                        <asp:Label ID="lblErrores" runat="server" Text=""></asp:Label>
                       <div style="overflow:scroll;max-height:500px; width:100%; text-align:center " >
                        <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: x-small">
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
            <tr>
                <td>
                    <asp:Label ID="lblTotRegs" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblTotErrores" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>

</asp:Content>
