<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td style="text-align: center; width: 300px;">Código de subproceso<br />
                    <asp:TextBox ID="txtCodigoSubproceso" runat="server" CssClass="textbox"
                        MaxLength="20" Width="180px" />
                </td>
                <td style="text-align: center">Descripción del subproceso<br />
                    <asp:TextBox ID="txtDescripcionSubproceso" runat="server" CssClass="textbox" MaxLength="120" Width="250px" />
                </td>
                <td style="text-align: center">Proceso<br />
                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="textbox" Width="260px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; text-align:center">
        <tr>
            <td colspan="3">
                <asp:Panel ID="panelListado" runat="server" ChildrenAsTriggers="true"><br/>
                    <asp:GridView runat="server" ID="gvSubproceso" HorizontalAlign="Center" Width="90%" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
                        Style="font-size: small" OnRowDeleting="gvSubproceso_RowDeleting" OnRowEditing="gvSubproceso_RowEditing"
                        OnPageIndexChanging="gvSubproceso_PageIndexChanging" DataKeyNames="cod_subproceso">
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
                            <asp:BoundField DataField="cod_subproceso" HeaderText="Código Subproceso" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField DataField="nom_proceso" HeaderText="Proceso" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
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

