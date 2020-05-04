<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - ProductosProceso :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%--  <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_prodproc&nbsp;<asp:CompareValidator ID="cvCOD_PRODPROC" runat="server" ControlToValidate="txtCOD_PRODPROC" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_prodproc" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_balance&nbsp;<asp:CompareValidator ID="cvCOD_BALANCE" runat="server" ControlToValidate="txtCOD_BALANCE" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_balance" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cantidad&nbsp;<asp:CompareValidator ID="cvCANTIDAD" runat="server" ControlToValidate="txtCANTIDAD" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCantidad" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Producto&nbsp;<br/>
                       <asp:TextBox ID="txtProducto" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Porcpd&nbsp;<asp:CompareValidator ID="cvPORCPD" runat="server" ControlToValidate="txtPORCPD" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtPorcpd" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Valunitario&nbsp;<asp:CompareValidator ID="cvVALUNITARIO" runat="server" ControlToValidate="txtVALUNITARIO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValunitario" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valortotal&nbsp;<asp:CompareValidator ID="cvVALORTOTAL" runat="server" ControlToValidate="txtVALORTOTAL" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValortotal" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
                </table>--%>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_PRODPROC">
                    <Columns>
                        <asp:BoundField DataField="COD_PRODPROC" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
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
                        <asp:BoundField DataField="COD_BALANCE" HeaderText="Cod balance" />
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="PRODUCTO" HeaderText="Producto" />
                        <asp:BoundField DataField="PORCPD" HeaderText="Porcpd" />
                        <asp:BoundField DataField="VALUNITARIO" HeaderText="V. unitario" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="VALORTOTAL" HeaderText="V. total" DataFormatString="{0:N0}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Total: &nbsp;<asp:TextBox ID="txtTotal" runat="server" Enabled="False"></asp:TextBox>
                <asp:MaskedEditExtender ID="msktxtTotal" runat="server" TargetControlID="txtTotal"
                    Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCod_prodproc" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI">
                Cantidad&nbsp;*&nbsp;<br />
                <uc1:decimales ID="txtCantidad" runat="server" />
            </td>
            <td class="tdD">
                Producto&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvPRODUCTO" runat="server"
                    ControlToValidate="txtProducto" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtProducto" runat="server" CssClass="textbox" 
                    MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Porcpd&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvPORCPD" runat="server" ControlToValidate="txtPorcpd"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" /><asp:CompareValidator ID="cvPORCPD" runat="server"
                        ControlToValidate="txtPorcpd" ErrorMessage="Solo se admiten n&uacute;meros enteros"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar"
                        ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtPorcpd" runat="server" CssClass="textbox" MaxLength="8" />
            </td>
            <td class="tdD">
                Valor Unitario&nbsp;*<br />
                <uc1:decimales ID="txtValunitario" runat="server" />
                <br />
            </td>
        </tr>
    </table>


    <%--  <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_PRODPROC').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>