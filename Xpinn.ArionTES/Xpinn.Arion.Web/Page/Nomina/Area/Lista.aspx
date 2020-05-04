<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Nomina_Area_Lista" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:GridView ID="gvContent" runat="server" 
                        AllowPaging="True"
                        AutoGenerateColumns="False" 
                        GridLines="Horizontal"
                        PageSize="20" 
                        HorizontalAlign="Center"
                        ShowHeaderWhenEmpty="True" 
                        Width="100%"
                        DataKeyNames="IdArea"
                        OnRowEditing="gvLista_RowEditing"
                        OnRowDeleting="gvLista_RowDeleting"
                        Style="font-size: x-small" 
                        OnRowDataBound="gvLista_RowDataBound" 
                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged" >
                     <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnElim" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdArea" HeaderText="Consecutivo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CodEmpresa" HeaderText="Cod_Empresa" ItemStyle-HorizontalAlign="Center" />
                       
                    </Columns>
                 <HeaderStyle CssClass="gridHeader" />
                 <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                 <RowStyle CssClass="gridItem" />
        </asp:GridView>
        <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
     <asp:Label ID="lblTotalRegs" runat="server" />
</asp:Content>

