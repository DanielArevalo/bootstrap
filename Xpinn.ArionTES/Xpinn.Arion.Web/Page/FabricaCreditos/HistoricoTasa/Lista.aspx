<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Tasas Históricas :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                </asp:Panel>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                Tipo Historico<br />
                                <asp:DropDownList ID="ddlHistorico" runat="server" Height="25px" Width="250px" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
    <div style="overflow: scroll; height: 500px; width: 80%;">
        <div style="width: 100%;">
            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="False"
                OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="IDHISTORICO">
                <Columns>
                    <asp:BoundField DataField="IDHISTORICO" HeaderStyle-CssClass="gridColNo" 
                        ItemStyle-CssClass="gridColNo" >
                        <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                        <ItemStyle CssClass="gridColNo"></ItemStyle>
                    </asp:BoundField>
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
                            <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                ToolTip="Borrar" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        <ItemStyle CssClass="gI" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="DESCRIPCION" HeaderText = "Tipo de Histórico" >
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FECHA_INICIAL" HeaderText = "Fecha Inicial" 
                        DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FECHA_FINAL" HeaderText = "Fecha Final" 
                        DataFormatString="{0:d}" />
                    <asp:BoundField DataField="VALOR" HeaderText = "Valor" />
                </Columns>
                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                <PagerStyle CssClass="gridPager"></PagerStyle>
                <RowStyle CssClass="gridItem"></RowStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
