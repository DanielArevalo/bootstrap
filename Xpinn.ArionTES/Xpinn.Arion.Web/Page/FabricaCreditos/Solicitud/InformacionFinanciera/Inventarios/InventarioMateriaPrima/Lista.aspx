<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - InventarioMateriaPrima :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="FALSE">
                    <%--  <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_matprima&nbsp;<asp:CompareValidator ID="cvCOD_MATPRIMA" runat="server" ControlToValidate="txtCOD_MATPRIMA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_matprima" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_inffin&nbsp;<asp:CompareValidator ID="cvCOD_INFFIN" runat="server" ControlToValidate="txtCOD_INFFIN" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_inffin" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Descripcion&nbsp;<br/>
                       <asp:TextBox ID="txtDescripcion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Valor&nbsp;<asp:CompareValidator ID="cvVALOR" runat="server" ControlToValidate="txtVALOR" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValor" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                </table>--%>
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
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_MATPRIMA">
                    <Columns>
                        <asp:BoundField DataField="COD_MATPRIMA" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_INFFIN" HeaderText="Cod inffin" Visible="false" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci�n" />
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
                <strong>Totales</strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Total materia prima y/o mercancia:
                <asp:TextBox ID="txtTotalMateriaPrima" runat="server" Enabled="False"></asp:TextBox>
                <asp:MaskedEditExtender ID="msktxtTotalFijo" runat="server" TargetControlID="txtTotalMateriaPrima"
                        Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCod_matprima" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI">
                Descripci�n&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDESCRIPCION" runat="server"
                    ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                    MaxLength="100" />
            </td>
            <td class="tdD">
                Valor&nbsp;*<br />
                <uc1:decimales ID="txtValor" runat="server" />
            </td>
        </tr>
    </table>
    <%--    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_MATPRIMA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
