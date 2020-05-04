<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="100%">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align: left">
            <tr>
                <td style="text-align: center; width: 200px">Código del proceso<br />
                    <asp:TextBox ID="txtCodigoProceso" runat="server" CssClass="textbox" MaxLength="20" />
                </td>
                <td style="text-align: center">Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="120" Width="500px" />
                </td>
                <td style="text-align: left">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="90%" style="text-align: center">
        <tr>
            <td colspan="2">
                <asp:Panel ID="pLista" runat="server">
                    <br />
                    <asp:GridView runat="server" ID="gvProcesoEntidad" HorizontalAlign="Center" Width="80%" AutoGenerateColumns="false"  PageSize="20"
                        Style="font-size: small" OnRowDeleting="gvProcesoEntidad_RowDeleting" OnRowEditing="gvProcesoEntidad_RowEditing"
                        AllowPaging="true" OnPageIndexChanging="gvProcesoEntidad_PageIndexChanging" DataKeyNames="cod_proceso">
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
                            <asp:BoundField DataField="cod_proceso" HeaderText="Código de proceso" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </asp:Panel><br/>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
            </td>
        </tr>
    </table>
</asp:Content>

