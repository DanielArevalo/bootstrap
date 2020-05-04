<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - InventarioActivoFijo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">                   
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_ACTIVO">
                    <Columns>                        
                        <asp:BoundField DataField="COD_ACTIVO" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_INFFIN" HeaderText="Cod_inffin" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion" />
                        <asp:BoundField DataField="MARCA" HeaderText="Marca" />
                        <asp:BoundField DataField="VALOR" HeaderText="Valor" DataFormatString="{0:N0}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <strong>Totales
                </strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Total Activo Fijo:
                <strong>
                    <asp:TextBox ID="txtTotalFijo" runat="server" Enabled="False"></asp:TextBox>
                    <asp:MaskedEditExtender ID="msktxtTotalFijo" runat="server" TargetControlID="txtTotalFijo"
                        Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
                </strong>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCod_activo" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdD">
                <asp:TextBox ID="txtCod_inffin" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI">
                Descripcion&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvDESCRIPCION" runat="server"
                    ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="100" />
            </td>
            <td class="tdD">
                Marca&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtMarca" runat="server" CssClass="textbox" MaxLength="50" />
            </td>
            <td class="tdD">
                Valor&nbsp;<br />
                <uc1:decimales ID="txtValor" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
