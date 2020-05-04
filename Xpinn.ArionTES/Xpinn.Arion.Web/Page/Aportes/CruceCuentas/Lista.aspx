<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="listadopersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="100%">
        <table>
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="5">
                    <strong>Criterios de Búsqueda:</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" width="22%">Identificación<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left" width="22%">Tipo Identificación
                    <br />
                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox"
                        Style="text-align: left" Height="26px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left" width="22%">Nombres<br />
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left" width="22%">Apellidos<br />
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left" width="22%">Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" width="20%">Fecha Retiro<br />
                    <ucFecha:fecha ID="txtFecharetiro" runat="server" />
                </td>
                <td style="text-align: left" width="20%">Motivo del Retiro<br />
                    <asp:DropDownList ID="DdlMotRetiro" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left" width="20%">Número Acta<br />
                    <asp:TextBox ID="txtActa" runat="server" CssClass="textbox" />
                </td>
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <br />
    <div id="GridScroll" style="overflow-x: auto; height: 30pc">
        <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Panel ID="panelGrilla" runat="server">
                        <strong>Listado de cruces de cuentas:</strong><br />
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            DataKeyNames="idretiro" Style="margin-top: 0px">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idretiro" HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombres" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cod_nomina" HeaderText="Código de nómina" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_retiro" HeaderText="Fec. Retiro" HeaderStyle-Width="10%" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="motivoretiro" HeaderText="Motivo retiro" HeaderStyle-Width="10%">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="acta" HeaderText="Número Acta" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="Saldos" HeaderText="Saldo">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estadocruce" HeaderText="Estado Cruce" HeaderStyle-Width="7%">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            </table>
    </div>
    <td style="text-align: center">
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
    </td>
</asp:Content>
