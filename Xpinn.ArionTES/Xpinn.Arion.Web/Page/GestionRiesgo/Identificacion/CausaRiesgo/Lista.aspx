<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pConsulta" runat="server" HorizontalAlign="Center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align:center">
            <tr>
                <td colspan="2" style="text-align: center; width: 250px;">Código de causa<br />
                    <asp:TextBox ID="txtCodigoCausa" runat="server" CssClass="textbox"
                        MaxLength="20" Width="200px" />
                </td>
                <td colspan="2" style="text-align: center">Descripción de causa<br />
                    <asp:TextBox ID="txtDescripcionCausa" runat="server" CssClass="textbox" MaxLength="120" Width="350px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">Área<br />
                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="textbox" Width="200px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="3" style="text-align: center">Potencial Responsable<br />
                    <asp:DropDownList ID="ddlPotencialResponsable" runat="server" CssClass="textbox" Width="200px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Panel ID="panelGrilla" runat="server"><br/>
                    <asp:GridView runat="server" ID="gvCausaRiesgo" HorizontalAlign="Center" Width="90%" AutoGenerateColumns="false"
                        Style="font-size: x-small" OnRowDeleting="gvCausaRiesgo_RowDeleting" OnPageIndexChanging="gvCausaRiesgo_PageIndexChanging"
                        OnRowEditing="gvCausaRiesgo_RowEditing" PageSize="20" AllowPaging="true" DataKeyNames="cod_causa">
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
                            <asp:BoundField DataField="cod_causa" HeaderText="Código de causa">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="Center" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_area" HeaderText="Área">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_cargo" HeaderText="Potencial Responsable">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="se han encontrado un total de: ### registros. " />
            </td>
        </tr>
    </table>
</asp:Content>

