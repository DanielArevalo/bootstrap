<%@ Page Title=".: Xpinn - Flujo de Caja :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs"
    Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<script runat="server">    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelConsulta" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: center; width: 125px">Código Concepto</td>
                <td style="text-align: center; width: 125px">Concepto</td>
                <td style="text-align: center; width: 125px">Tipo de Concepto</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtCodConcepto" runat="server" CssClass="textbox" MaxLength="20" />
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlTipoConcepto" runat="server" CssClass="textbox" Width="150px" AutoPostBack="True" >
                        <asp:ListItem Text="Seleccione un item" Value="0" />
                        <asp:ListItem Text="Ingreso" Value="1" />
                        <asp:ListItem Text="Egreso" Value="2" />
                        <asp:ListItem Text="Otros Egresos" Value="3" />
                        <asp:ListItem Text="Saldo Caja" Value="4" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:center"><br/></td>
            </tr>
            <tr>                
                <td colspan="4" style="text-align: center">
                    <asp:GridView ID="gvCuentas" HorizontalAlign="Center" DataKeyNames="cod_concepto"
                        runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                        OnRowEditing="gvCuentas_RowEditing" OnRowDeleting="gvCuentas_RowDeleting" PageSize="10" ShowFooter="True"
                        Style="font-size: small" Width="70%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                        ToolTip="Eliminar" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="cod_concepto" HeaderText="Código Concepto">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_tipo_concepto" HeaderText="Tipo Concepto">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

