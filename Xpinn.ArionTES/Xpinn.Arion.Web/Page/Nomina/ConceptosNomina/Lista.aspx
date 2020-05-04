<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Nomina_ConceptosNomina_Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td style="width: 15%; text-align: left;">Consecutivo 
                    <br />
                    <asp:TextBox ID="txtConsecutivo" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="width: 15%; text-align: left;">Descripción 
                    <br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="width: 15%; text-align: left;">Tipo
                    <br />
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="dropdown" Width="100%">
                        <asp:ListItem Value=" " Text="Seleccione un Item"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Pago"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Descuento"></asp:ListItem>
                   
                    </asp:DropDownList>
                </td>
                <td style="width: 15%; text-align: left;">Tipo de Concepto<br />
                    <asp:DropDownList ID="ddlTipoConcepto" runat="server" CssClass="dropdown" Width="100%" AppendDataBoundItems="true">
                        <asp:ListItem Value=" " Text="Seleccione un Item"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 15%; text-align: left;">Clase<br />
                    <asp:DropDownList ID="ddlClase" runat="server" CssClass="dropdown" Width="100%">
                        <asp:ListItem Value=" " Text="Seleccione un Item"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Prestacional"></asp:ListItem>
                        <asp:ListItem Value="2" Text="No Prestacional"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Otros"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

    </asp:Panel>
    <hr width="100%" noshade />
    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        OnRowDataBound="gvLista_RowDataBound" Style="font-size: x-small">
        <Columns>
<%--            <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                <ItemTemplate>
                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                        ToolTip="Detalle" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                        ToolTip="Editar" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                        ToolTip="Eliminar" Width="16px" />
                </ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo" />
            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="desc_clase" HeaderText="Clase" />
            <asp:BoundField DataField="desc_tipoconcepto" HeaderText="Tipo Concepto" />
            <asp:BoundField DataField="desc_tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="desc_unidad_concepto" HeaderText="Unidad Concepto" />
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

