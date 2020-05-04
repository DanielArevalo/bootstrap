<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Procesos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvProcesos" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <br /><br />
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="70%">
                    <tr>
                        <td style="width: 30%">
                            Cod Proceso<br />
                            <asp:TextBox ID="txtCodProceso" runat="server" CssClass="textbox" MaxLength="130"  Width="98%"/>
                            <asp:FilteredTextBoxExtender ID="txtCodProceso_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtCodProceso">
                            </asp:FilteredTextBoxExtender>
                            &nbsp;
                        </td>
                        <td style="width: 30%">
                            Tipo Proceso<br />
                            <asp:DropDownList ID="ddlTipoProceso" runat="server" CssClass="textbox" AutoPostBack="True" 
                                onselectedindexchanged="ddlTipoProceso_SelectedIndexChanged" Width="100%">
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                <asp:ListItem Value="2">Automatico</asp:ListItem>
                                <asp:ListItem Value="1">Manual</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td style="width: 30%">
                            Proceso Antecesor<br />
                            <asp:DropDownList ID="ddlAntecesor" runat="server" CssClass="textbox" AutoPostBack="True" 
                                onselectedindexchanged="ddlAntecesor_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                GridLines="Vertical" onpageindexchanging="gvLista_PageIndexChanging" 
                onrowediting="gvLista_RowEditing" OnRowDeleting="gvLista_OnRowDeleted"
                onselectedindexchanged="gvLista_SelectedIndexChanged" PageSize="20" 
                style="margin-right: 0px" Width="100%" DataKeyNames="cod_proceso">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="cod_proceso" Visible="False" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" 
                                ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" 
                                ImageUrl="~/Images/gr_elim.jpg" ToolTip="Editar" Width="16px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cod_proceso" HeaderText="Código Proceso" HeaderStyle-Width="15%" />
                    <asp:BoundField DataField="descripcion" HeaderText="Proceso" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="nom_tipo_proceso" HeaderText="Tipo Proceso" HeaderStyle-Width="20%" />
                    <asp:BoundField DataField="cod_proceso_antec" HeaderText="Cod.Antecesor" HeaderStyle-Width="5%" />
                    <asp:BoundField DataField="antecede" HeaderText="Antecesor" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblTotalRegs" runat="server"  />
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            //document.getElementById('cphMain_txtNumeroCredido').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
