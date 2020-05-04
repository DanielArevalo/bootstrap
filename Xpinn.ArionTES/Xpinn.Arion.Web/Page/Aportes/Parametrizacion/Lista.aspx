<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:90%">
        <tr>
            <td colspan="3" style="text-align:left">
                <strong>Editar parametros por :</strong>
            </td>
        </tr>
        <tr>
            <td style="width:25%; text-align:left">
                <asp:Button ID="btnEditaNatural" runat="server" CssClass="btn8" 
                    Text="Personas Naturales" Height="30px" onclick="btnEditaNatural_Click"/>
                    <asp:ImageButton ID="btnVerNatural" runat="server" Text="Ver" Height="20px" 
                    ImageUrl="~/Images/gr_edit.jpg" onclick="btnVerNatural_Click"/>
            </td>
            <td style="width:25%; text-align:left">
                <asp:Button ID="btnEditaJuridico" runat="server" CssClass="btn8" 
                    Text="Personas Juridicas" Height="30px" onclick="btnEditaJuridico_Click"/>                    
                    <asp:ImageButton ID="btnVerJuridica" runat="server" Text="Ver" Height="20px" 
                    ImageUrl="~/Images/gr_edit.jpg" onclick="btnVerJuridica_Click"/>
            </td>
            <td style="width:25%; text-align:left">
                <asp:Button ID="btnEditaMenores" runat="server" CssClass="btn8" 
                    Text="Menores de Edad" Height="30px" onclick="btnEditaMenores_Click"/>                    
                    <asp:ImageButton ID="btnVerMenores" runat="server" Text="Ver" Height="20px" 
                    ImageUrl="~/Images/gr_edit.jpg" onclick="btnVerMenores_Click"/>
            </td>
            <td style="width:40%; text-align:left">
                 <asp:Button ID="btnEditaGeneral" runat="server" CssClass="btn8" 
                    Text="Personas en General" Height="30px" onclick="btnEditaGeneral_Click"/>                    
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr width="100%" noshade />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    Style="font-size: small">
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
                        <asp:BoundField DataField="cod_infadicional" HeaderText="Codigo" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="items_lista" HeaderText="Lista de Items" />
                        <asp:BoundField DataField="tipo_persona" HeaderText="Tipo Persona" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    
</asp:Content>
