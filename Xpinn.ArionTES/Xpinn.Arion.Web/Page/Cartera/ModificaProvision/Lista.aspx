<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Modificación Provisión :."%>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="2">
                    <tr>
                        <td colspan="6" style="text-align: left; font-size: x-small">
                            <strong>Criterios de búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Fecha de corte<br />
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="textbox" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">
                            Oficina:<br />
                            <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Width="210px"></ucDrop:dropdownmultiple>
                        </td>
                        <td style="text-align: left;">
                            Linea de Crédito:<br />
                            <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Width="250px" cssClass="textbox"></ucDrop:dropdownmultiple>
                        </td>
                        <td style="text-align: left;">
                            Categoria:<br />
                            <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Width="150px" cssClass="textbox"></ucDrop:dropdownmultiple>
                        </td>
                        <td style="text-align: left;">
                            Num. Radicación:<br />
                            <asp:TextBox ID="txtNumRadicacion" runat="server" Width="110px" CssClass="textbox" />
                            <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtNumRadicacion" ValidChars=".">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left;">
                            Identificación:<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" Width="110px" CssClass="textbox" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <asp:Panel ID="panelGrid" runat="server">
                <table cellpadding="2" width="100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Listado de Provisión</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: scroll; max-height: 500px; width: 100%">
                                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" DataKeyNames="idprovision"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                                    Style="font-size: x-small" Width="100%" GridLines="Horizontal">
                                    <Columns>
                                        <asp:BoundField HeaderText="Id Provision" DataField="idprovision" Visible="false">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Num. Radicación" DataField="numero_radicacion">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Oficina" DataField="nom_oficina">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cod Cliente" DataField="cod_cliente">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Identificación" DataField="identificacion">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Nombre" DataField="nombres">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Linea Credito" DataField="linea">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Categoria" DataField="cod_categoria">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Atributo" DataField="nom_atr">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Base Provisión" DataField="base_provision" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="% Prov" DataField="porc_provision" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor Provisión">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVrProvision" runat="server" Text='<%# Bind("valor_provision") %>'
                                                    Visible="false" />
                                                <uc1:decimalesGridRow ID="txtVrProvision" runat="server" Text='<%# Bind("valor_provision") %>' Width_="100px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Vr Provisión Anterior" DataField="aporte_resta" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Diferencia" DataField="diferencia_provision" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Dif Actual" DataField="diferencia_actual" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Dif Anterior" DataField="diferencia_anterior" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblTotalRegs" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color: Red">
                            Se realizó
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp; modificacion(es) correcta(s) de la(s) provision(es).<br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>

