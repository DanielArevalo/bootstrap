<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0">
                        <tr>
                            <td style="font-size: small; text-align: left" colspan="5">
                                <strong>Críterios de Búsqueda</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                Moneda Origen<br />
                                <asp:DropDownList ID="ddlOrigen" runat="server" CssClass="dropdown" Width="180px" />
                            </td>
                            <td style="text-align: left">
                                Moneda Destino<br />
                                <asp:DropDownList ID="ddlDestino" runat="server" CssClass="dropdown" Width="180px" />
                            </td>
                            <td style="text-align: left">
                                Fecha<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" style="text-align: center" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="panelGrilla" runat="server">
                    <asp:GridView ID="gvLista" runat="server" Width="90%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnRowEditing="gvLista_RowEditing" AllowPaging="True" PageSize="20" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="idcambiomoneda"
                        OnPageIndexChanging="gvLista_PageIndexChanging" 
                        onrowdeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Modificar" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                        ToolTip="Eliminar" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="idcambiomoneda" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_monedaOrigen" HeaderText="Moneda Origen">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_monedaDestino" HeaderText="Moneda Destino">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_venta" HeaderText="Valor Venta">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_compra" HeaderText="Valor Compra">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>

     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
