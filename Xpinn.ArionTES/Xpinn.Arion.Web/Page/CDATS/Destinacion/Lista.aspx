<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <hr />
    <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
       AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
       OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
       OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
       OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_destino"
       style="font-size: xx-small">
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
           <asp:BoundField DataField="cod_destino" HeaderText="Código" DataFormatString="{0:n0}" >
               <ItemStyle HorizontalAlign="Center" Width="50px" />
           </asp:BoundField>
           <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
               <ItemStyle HorizontalAlign="Left" />
           </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
    </asp:GridView>
    <asp:Label ID="Label1" runat="server" Visible="False" />
    <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    <br />
</asp:Content>
