<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" 
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="70%">
            <tr>
                <td class="tdI" style="text-align:left;">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left">
                    Descripcion<br/>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="393px" Visible="True" />
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <hr />
        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="idarea">
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
                <asp:BoundField DataField="idarea" HeaderText="Código" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="fecha_constitucion" HeaderText="Fecha de Constitución"  ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>               
                <asp:BoundField DataField="base_valor" HeaderText="Monto máximo"  ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>   
                <asp:BoundField DataField="valor_minimo" HeaderText="Monto Mínimo"  ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField> 
                <asp:BoundField DataField="saldo_caja" HeaderText="Saldo"  ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                     <asp:BoundField DataField="nom_usuario" HeaderText="Responsable"  ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>    
                     <asp:BoundField DataField="nombre" HeaderText="Descripción"  ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
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
