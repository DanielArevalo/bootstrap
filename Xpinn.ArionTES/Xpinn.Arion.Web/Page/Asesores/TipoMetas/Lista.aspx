<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/Imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table cellpadding="5" cellspacing="0" style="width: 100%; text-align:left">
                        <tr>
                            <td class="tdI" colspan="3" style="font-size: x-small">
                                <strong>Ingresar datos de búsqueda y presione botón de consultar o presione 
                                botón NUEVO si desea crear una meta</strong></td>
                            <td class="tdI" style="font-size: x-small">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 328px">
                                Codigo&nbsp; Meta<br />
                                <asp:TextBox ID="IdMeta" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="tdI" style="width: 328px">
                                Nombre Meta<br />
                                <asp:TextBox ID="txtNombreMeta" runat="server" CssClass="textbox" 
                                    Width="306px"></asp:TextBox>
                            </td>
                            <td class="tdD" style="width: 221px">
                                &nbsp;</td>
                            <td class="tdD">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 328px">
                                &nbsp;</td>
                            <td class="tdI" style="width: 328px">
                                <br />
                            </td>
                            <td class="tdD" style="width: 221px"></td>
                            <td class="tdD">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">
                <strong>Listado de Metas:</strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="IdMeta" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo">
<HeaderStyle CssClass="gridColNo"></HeaderStyle>

<ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>                        
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar"/>
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"  ToolTip="Borrar" />
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdMeta" HeaderText="IdMeta" />
                        <asp:BoundField DataField="NombreMeta" HeaderText="Nombre Meta" />
                        <asp:BoundField DataField="formatoMeta" HeaderText="Formatometa" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>