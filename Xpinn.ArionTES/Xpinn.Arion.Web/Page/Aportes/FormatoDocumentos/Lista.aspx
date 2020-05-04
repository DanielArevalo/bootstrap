<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table>
            <tr>
                <td style="text-align: left; font-size: x-small" colspan="3">
                    <strong>Criterios de búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" />
                </td>
                <td style="text-align: left;">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="300px" />
                </td>
                <td style="text-align: left;">
                    Tipo<br />
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="textbox" Width="200px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr width="100%" noshade />
    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        OnRowDataBound="gvLista_RowDataBound" style="font-size: x-small" DataKeyNames="cod_documento">
        <Columns>                    
            <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                <ItemTemplate>
                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                        ToolTip="Detalle" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                        ToolTip="Editar" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                        ToolTip="Eliminar" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="cod_documento" HeaderText="Codigo"/>
            <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="nomtipo" HeaderText="Tipo"/>
        </Columns>
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
    </asp:GridView>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    <br />
</asp:Content>
