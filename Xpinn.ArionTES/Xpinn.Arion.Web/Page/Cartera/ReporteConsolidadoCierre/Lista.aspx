<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Cartera :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="~/General/Controles/ctlFechaCierre.ascx" TagName="ddlCierreFecha" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
     <asp:Panel ID="pConsulta" runat="server">
       <table style="width: 90%;">
                <tr>
                    <td align="center">Consultar<br />
                        <asp:DropDownList ID="ddlConsultar" AutoPostBack="true" runat="server" CssClass="textbox" Width="200px" OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged">
                            <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                            <asp:ListItem Value="1">CIERRE DE CARTERA</asp:ListItem>
                            <asp:ListItem Value="2">CAUSACION DE CARTERA</asp:ListItem>
                            <asp:ListItem Value="3">PROVISION DE CARTERA</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="text-align: left">Fecha de Corte:
                    <br />
                        <ctl:ddlCierreFecha ID="ddlFechaCorte" runat="server" Requerido="True" />
                    </td>
                    <td style="text-align: left">Clasificacion:
                    <br />
                        <ucDrop:dropdownmultiple ID="ddlClasificacion" runat="server" Height="24px" Width="160px"></ucDrop:dropdownmultiple>
                    </td>
                    <td style="text-align: left">Forma de Pago:<br />
                        <ucDrop:dropdownmultiple ID="ddlFormaPago" runat="server" Height="24px" Width="160px" cssClass="textbox"></ucDrop:dropdownmultiple>
                    </td>
                    <td style="text-align: left">Tipo Garantía:<br />
                        <ucDrop:dropdownmultiple ID="ddlTipoGarantia" runat="server" Height="24px" Width="160px" cssClass="textbox"></ucDrop:dropdownmultiple>
                    </td>
                    <td style="text-align: left">Categoria:<br />
                        <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="160px" cssClass="textbox"></ucDrop:dropdownmultiple>
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="center">&nbsp;</td>
                    <td style="text-align: left">Linea<ucDrop:dropdownmultiple ID="ddlLinea" runat="server" cssClass="textbox" Height="24px" Width="160px" />
                    </td>
                    <td style="text-align: left">Oficina:<ucDrop:dropdownmultiple ID="ddlOficina" runat="server" cssClass="textbox" Height="24px" Width="160px" />
                    </td>
                    <td style="text-align: left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" onclick="btnDatos_Click" Text="Visualizar Datos" />
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
            </table>
    </asp:Panel>


 
    <br />
    <asp:Panel ID="panelReporte" Visible="false" runat="server">
        <asp:MultiView  ID="mvReporte" runat="server">
            <asp:View runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                                WaitMessageFont-Size="10pt" Width="100%" Height="700">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="Datos" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:GridView ID="gvReportecierre" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"  PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True" Style="font-size: x-small" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Clasificacion" HeaderText="Clasificación" />
                                    <asp:BoundField DataField="CodigoLinea" HeaderText="Linea Crédito"></asp:BoundField>

                                    <asp:BoundField DataField="FormaPago" HeaderText="Forma Pago">
                                    <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoGarantia" HeaderText="Tipo Garantía">
                                    <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCategoria" HeaderText="Código Categoría">
                                    <ItemStyle HorizontalAlign="left" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoría">
                                    <ItemStyle HorizontalAlign="left" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SaldoCapital" HeaderText="Saldo Capital">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroCreditos" DataFormatString="{0:d}" HeaderText="Número Creditos ">
                                    <ItemStyle HorizontalAlign="center" Width="90" />
                                    </asp:BoundField>
                                   
                               
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            <br />
                            <asp:GridView ID="gvReportecausacion" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True" Style="font-size: x-small" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Clasificacion" HeaderText="Clasificación" />
                                    <asp:BoundField DataField="CodigoLinea" HeaderText="Linea Crédito" />                                                                   
                                    <asp:BoundField DataField="CodigoCategoria" HeaderText="Código Categoría">
                                    <ItemStyle HorizontalAlign="left" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoría">
                                    <ItemStyle HorizontalAlign="left" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atributo" HeaderText="Atributo">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SaldoCausado"   HeaderText="Saldo Causado">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="SaldoOrden"  HeaderText="Saldo Orden">
                                    <ItemStyle HorizontalAlign="center" Width="90" />
                                    </asp:BoundField>

                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            <br />
                            <asp:GridView ID="gvReporteProvision" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True" Style="font-size: x-small" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Clasificacion" HeaderText="Clasificación" />
                                    <asp:BoundField DataField="CodigoLinea" HeaderText="Linea Crédito" />
                                    <asp:BoundField DataField="CodigoCategoria" HeaderText="Código Categoría">
                                    <ItemStyle HorizontalAlign="left" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoría">
                                    <ItemStyle HorizontalAlign="left" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoGarantia" HeaderText="Tipo Garantía">
                                    <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atributo" HeaderText="Atributo">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValorProvision" HeaderText="Valor Provision">           
                                                                 <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                  
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>

        </asp:MultiView>
        <br />
    </asp:Panel>
</asp:Content>
