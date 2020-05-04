<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="70%">
            <tr>
                <td class="tdI" style="text-align:left;">
                    Cod.Categoria<br/>
                    <asp:TextBox ID="txtProducto" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left">
                    Descripcion<br/>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="393px" />
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <hr />
        <asp:GridView ID="gvLista" runat="server" Width="90%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="id_categoria">
            <Columns>                   
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
                <asp:BoundField DataField="id_categoria" HeaderText="Id.Categ." >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="codigo" HeaderText="Còdigo" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="estado" HeaderText="Estado" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="nombre_categoria" HeaderText="Categorìa Padre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod Cta" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod Cta Niif" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_cuenta_ingreso" HeaderText="Cod Cta Ingreso" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_cuenta_gasto" HeaderText="Cod Cta Gasto" >
                    <ItemStyle HorizontalAlign="Left" />
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
