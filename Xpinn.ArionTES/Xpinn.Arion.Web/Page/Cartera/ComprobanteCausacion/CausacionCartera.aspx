<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="CausacionCartera.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Comprobante Causación :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="panelGeneral" runat="server">
        <br />
        <table border="0" cellpadding="5" cellspacing="0" width="60%">
            <tr>
                <td class="columnForm50" style="width: 312px; text-align: left; font-size: large;" colspan="3">
                    <asp:Label ID="LabelFecha" runat="server" Text="Fecha de Causaciòn a Contabilizar" Font-Size="Large" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="txtFechaIni" runat="server" CssClass="dropdown" Width="160px" />
               <%-- <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" Height="20px" MaxLength="10"
                        Width="188px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                        TargetControlID="txtFechaIni">
                    </asp:CalendarExtender>--%>
                    <br />
                    Presione el botòn de grabar para generar el comprobante.
                </td>
            </tr>
            <tr>
                <td class="columnForm50" style="width: 550px">
                    <br /> 
                    <asp:Button ID="btnComprobante" runat="server" CssClass="btn8" Text=" Generar Comprobante " Width="192px"
                        Height="26px" OnClick="btnGuardar_Click" />&nbsp;&nbsp;
                    <br />
                </td>
                <td class="columnForm50" style="width: 312px">
                    <br /> 
                    <asp:Button ID="btnInforme" runat="server" CssClass="btn8" Text=" Consultar Datos Causaciòn " Width="182px"
                        Height="26px" OnClick="btnInforme_Click" />&nbsp;&nbsp;
                    <br />
                </td>
                <td class="tdI">
                    <br />
                    <asp:Button ID="btnValidar" runat="server" CssClass="btn8" Text=" Validar Parametrización " Width="182px"
                        Height="26px" OnClick="btnValidar_Click"/>&nbsp;&nbsp;
                </td>
                <td class="tdI">
                    <br />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TabContainer ID="tcInfFinNeg" runat="server" ActiveTabIndex="0" Height="338px"
                                ScrollBars="Auto" Width="100%">
                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Detallado" ScrollBars="Auto">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn8" Height="28px"
                                                    OnClick="btnExportarExcel_Click" Text="Exportar a Excel" Width="124px" />
                                                    &nbsp;<asp:GridView ID="gvdetallado" runat="server" Width="100%" PageSize="3"
                                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: x-small"
                                                        Height="187px">
                                                        <Columns>
                                                            <asp:BoundField DataField="FECHA_HISTORICO" HeaderText="fecha">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Radicación">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Linea ">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre de la Linea">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificación">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COD_ATR" HeaderText="Atr">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ATRIBUTO" HeaderText="Atributo">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VALOR_CAUSADO" HeaderText="Valor Causado" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VALOR_ORDEN" HeaderText="Valor Orden" DataFormatString="{0:N}" />
                                                            <asp:BoundField DataField="SALDO_CAUSADO" HeaderText="Saldo Causado" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SALDO_ORDEN" HeaderText="Saldo Orden" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DIAS_MORA" HeaderText="Dias Mora">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gridHeader" />
                                                        <PagerStyle CssClass="gridPager" />
                                                        <RowStyle CssClass="gridItem" />
                                                        <SelectedRowStyle Font-Size="XX-Small" />
                                                    </asp:GridView>
                                                    <asp:Label ID="lblTotRegs" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
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
                                                <td>&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" CssClass="btn8" Height="28px"
                                                    Text="Exportar a Excel" Width="124px" OnClick="Button3_Click" />
                                                    &nbsp;<asp:GridView ID="gvConsolidado" runat="server" Width="100%" PageSize="3"
                                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                        Style="font-size: x-small" BorderWidth="1px" CellSpacing="2">
                                                        <Columns>
                                                            <asp:BoundField DataField="FECHA_HISTORICO" HeaderText="fecha">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Linea">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre de la Linea">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COD_ATR" HeaderText="Atr">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ATRIBUTO" HeaderText="Atributo">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VALOR_CAUSADO" HeaderText="Valor Causado" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VALOR_ORDEN" HeaderText="Valor Orden" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SALDO_CAUSADO" HeaderText="Saldo Causado" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SALDO_ORDEN" HeaderText="Saldo Orden">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gridHeader" />
                                                        <PagerStyle CssClass="gridPager" />
                                                        <RowStyle CssClass="gridItem" />
                                                        <SelectedRowStyle Font-Size="XX-Small" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="tcInfFinNeg" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                    <asp:Label ID="lblErrores" runat="server" Text=""></asp:Label>                        
                    <asp:Panel ID="panelErrores" runat="server" Visible="false">
                        &nbsp;
                        <div style="overflow:scroll;max-height:500px; width:100%; text-align:center " >
                        <asp:GridView ID="gvErrores" runat="server" Width="98%" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: x-small"
                             HorizontalAlign="Center" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
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
