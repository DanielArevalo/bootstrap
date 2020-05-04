<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
 <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
   <hr width="100%" noshade>
   <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="Horizontal" PageSize="5" 
                            ShowHeaderWhenEmpty="True" Width="100%" 
            onpageindexchanging="gvLista_PageIndexChanging" 
            onrowdeleting="gvLista_RowDeleting" onrowdatabound="gvLista_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" 
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Código del motivo" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo de motivo" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>

     <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
     <asp:Label ID="lblInfo" runat="server" 
         Text="Su consulta no obtuvo ningun resultado." Visible="False" />

    </asp:Panel>
</asp:Content>

