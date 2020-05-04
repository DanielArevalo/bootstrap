<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="align-items: center">
            <tr>
                <td colspan="2" style="text-align: center">Código de área<br />
                    <asp:TextBox ID="txtCodigoArea" runat="server" CssClass="textbox"
                        MaxLength="20" Width="250px"/>
                </td>
                <td colspan="5" style="text-align: center">Descripción de área<br />
                    <asp:TextBox ID="txtDescripcionArea" runat="server" CssClass="textbox" MaxLength="120" Width="400px" />
                </td>
                <td style="text-align: left">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Panel ID="panelGrilla" runat="server" Width="100%">
                    <br />
                    <asp:GridView runat="server" ID="gvAreaFuncional" AllowPaging="true" HorizontalAlign="Center" Width="90%" AutoGenerateColumns="false"
                        Style="font-size: small" OnPageIndexChanging="gvAreaFuncional_PageIndexChanging" OnRowDeleting="gvAreaFuncional_RowDeleting"
                        OnRowEditing="gvAreaFuncional_RowEditing" DataKeyNames="cod_area" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Modificar" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                        ToolTip="Borrar" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="cod_area" HeaderText="Código de Área">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
            </td>
        </tr>
    </table>

</asp:Content>

