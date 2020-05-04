<%@ Page Title="Expinn - Estructura Archivos" Language="C#" MasterPageFile="~/General/Master/site.master"
    AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="80%">
        <table cellspacing="5">
            <tr>
                <td colspan="2" style="font-size:x-small">
                    <strong>Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Código
                    <br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de Lineas de Auxilio:</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="15" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                        margin-bottom: 0px;" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                        DataKeyNames="Codigo" OnRowEditing="gvLista_RowEditing" GridLines="Horizontal"
                        OnRowDeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
                                <ItemStyle HorizontalAlign="center" Width="4%" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="true">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:CommandField>
                            <asp:BoundField DataField="Codigo" HeaderText="Código" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Código" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Style="text-align: center" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
