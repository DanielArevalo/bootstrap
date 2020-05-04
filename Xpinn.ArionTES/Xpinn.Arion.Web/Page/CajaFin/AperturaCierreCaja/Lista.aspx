<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
               <br /><br /><br />    

                <asp:GridView ID="gvLista" runat="server" Width="70%"  GridLines="Horizontal" 
                    AutoGenerateColumns="False" onrowediting="gvLista_RowEditing"
                    PageSize="20" HeaderStyle-CssClass="gridHeader" 
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  
                    DataKeyNames="cod_caja" >
                    <Columns>                 
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="cod_caja" HeaderText="Código Caja"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="cod_oficina" HeaderText="Codigo Oficina"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="nombre_ofi" HeaderText="Nombre Oficina"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre Caja"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="fecha_Creacion" HeaderText="Fecha Creacion"  DataFormatString="{0:d}">  <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="state" HeaderText="Estado"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                      </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

