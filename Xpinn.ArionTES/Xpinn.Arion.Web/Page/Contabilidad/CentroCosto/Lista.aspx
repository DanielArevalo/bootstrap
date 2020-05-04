<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table style="width: 50%;">
            <tr>
                <td>
                    Centro Costo<br/>
                    <asp:TextBox ID="txtCentroCosto" runat="server" CssClass="textbox" />
                </td>
                <td>
                    Nombre<br/>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
        <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="centro_costo">
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
                <asp:BoundField DataField="centro_costo" HeaderText="Código" >
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="nom_centro" HeaderText="Nombre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="principal" HeaderText="Principal" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
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
