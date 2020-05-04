<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>
    <hr width="100%" noshade />
    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        OnRowDataBound="gvLista_RowDataBound" style="font-size: x-small">
        <Columns>                    
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
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
            <asp:BoundField DataField="tipo_liquidacion" HeaderText="Codigo"/>
            <asp:BoundField DataField="nombre" HeaderText="Descripción" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="cod_periodicidad" HeaderText="Periodicidad" />
            <asp:BoundField DataField="tipo_saldo_base" HeaderText="Tipo Saldo Base" />
            <asp:BoundField DataField="lineaaporte_base" HeaderText="Linea Aporte Base" />
            <asp:BoundField DataField="lineaaporte_afecta" HeaderText="Linea Aporte Afecta" />
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
