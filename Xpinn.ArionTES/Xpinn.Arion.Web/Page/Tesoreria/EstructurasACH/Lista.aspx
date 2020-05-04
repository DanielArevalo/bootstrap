<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="50%">
            <tr>
                <td class="tdI" style="text-align:left;">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left">
                    Nombre<br/>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="200px" />
                </td>
                <td class="tdD" style="text-align:left">                   
                </td>
                <td class="tdD" style="text-align:left">
                </td>
                <td class="tdD" style="text-align:left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:Panel ID="pLista" runat="server" Width="60%">
        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
            onclick="btnExportar_Click" Text="Exportar a Excel" />
        <br />
        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="codigo" 
            style="font-size: x-small">
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
                <asp:BoundField DataField="codigo" HeaderText="No." >
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="activo" HeaderText="Estado">
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
    </asp:Panel>
</asp:Content>
