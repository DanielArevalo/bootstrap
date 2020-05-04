<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pConsulta" runat="server" HorizontalAlign="Center"><br/>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2" style="width: 200px; align-items:center">Código del cargo<br />
                    <asp:TextBox ID="txtCodCargo" runat="server" CssClass="textbox" MaxLength="20" Width="200px" />
                </td>
                <td colspan="2" style="align-items:center">Descripción del cargo<br />
                    <asp:TextBox ID="txtDescripcionCargo" runat="server" CssClass="textbox" MaxLength="120" Width="350px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td style="text-align: center">
                <asp:Panel ID="panelGrilla" runat="server">
                    <br />
                    <asp:GridView runat="server" ID="gvCargo" HorizontalAlign="Center" Width="80%" AutoGenerateColumns="false" AllowPaging="true"
                        Style="font-size: x-small" OnRowEditing="gvCargo_RowEditing" OnPageIndexChanging="gvCargo_PageIndexChanging" PageSize="20"
                        OnRowDeleting="gvCargo_RowDeleting" DataKeyNames="cod_cargo">
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
                            <asp:BoundField DataField="cod_cargo" HeaderText="Código de Cargo">
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
                </asp:Panel><br/>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" />
            </td>
        </tr>
    </table>
</asp:Content>

