<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">         
        <table style="width: 100%;">
            <tr>
                <td style="width: 95px" >
                    &nbsp;&nbsp;Código<br />
                    &nbsp;&nbsp;<asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="75px"></asp:TextBox>                    
                </td>
                <td style="width: 320px">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="300px"></asp:TextBox> 
                </td>
                <td colspan="2">                    
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr width="100%" />
    <div style="font-size: x-small">
    <asp:GridView ID="gvLista" runat="server" Width="60%" 
        AutoGenerateColumns="False"  AllowPaging="True" PageSize="20" ShowHeaderWhenEmpty="True" 
        onrowediting="gvLista_RowEditing" onrowdeleting="gvLista_RowDeleting"   onpageindexchanging="gvLista_PageIndexChanging" 
            onrowdatabound="gvLista_RowDataBound" 
            onselectedindexchanged="gvLista_SelectedIndexChanged">
        <Columns>                    
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                        ToolTip="Detalle" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                <ItemStyle VerticalAlign="Top" Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                        ToolTip="Editar" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                <ItemStyle VerticalAlign="Top" Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                        ToolTip="Eliminar" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                <ItemStyle VerticalAlign="Top" Width="20px" />
            </asp:TemplateField>
            <asp:BoundField DataField="codperfil" HeaderText="Id.">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="nombreperfil" HeaderText="Descripción" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />   
        <RowStyle CssClass="gridItem" />     
    </asp:GridView>
    </div>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
    <br />
    <br />
    <br />
</asp:Content>

