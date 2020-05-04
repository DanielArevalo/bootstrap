<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <asp:Panel ID="Panel1" runat="server">
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td class="tdI" style="text-align:left; width: 162px;">
                    Código<br/>
                    <asp:TextBox ID="txtCodPeriodicidad" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left">
                    Descripcion<br/>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="393px" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <hr />
        <asp:GridView ID="gvLista" runat="server" Width="70%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="Codigo">
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
                <asp:BoundField DataField="Codigo" HeaderText="Código" >
                <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="numero_dias" HeaderText="# Días" >
                <ItemStyle HorizontalAlign="Right" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="numero_meses" HeaderText="# Meses" >
                <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="periodos_anuales" HeaderText="Periodos Anuales" >
                <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="calendario" HeaderText="Tipo Calendario" >
                <ItemStyle HorizontalAlign="Right" Width="100px" />
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
