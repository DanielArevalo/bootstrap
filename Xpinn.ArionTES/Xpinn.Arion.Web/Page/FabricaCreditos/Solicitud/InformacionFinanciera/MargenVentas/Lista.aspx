<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - MargenVentas :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="5">
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_MARGEN"
                    OnRowCommand="gvLista_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="COD_MARGEN" HeaderText="Cod Margen" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_VENTAS" HeaderText="Cod ventas" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:TemplateField HeaderText="Tipo Producto" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <ItemTemplate>                                
                                <asp:LinkButton ID="Button1" CommandName="TipoProd" Text='<%# Eval("TIPOPRODUCO") %>'
                                    CommandArgument='<%# Container.DataItemIndex %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NOMBREPRODUCTO" HeaderText="Nombre Producto" />
                        <asp:BoundField DataField="UNIVENDIDA" HeaderText="Unidad Pendida" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="COSTOUNIDVEN" HeaderText="Costo Unidad" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="PRECIOUNIDVEN" HeaderText="Precio de Venta" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="COSTOVENTA" HeaderText="Costo Venta" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="VENTATOTAL" HeaderText="Venta Total" DataFormatString="{0:N0}" />
                        <asp:TemplateField HeaderText="Utilidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUtilidad" runat="server" ReadOnly="true" Text='<%# String.Format("{0, 0:N0}", Convert.ToInt64(Eval("VENTATOTAL")) - Convert.ToInt64(Eval("COSTOVENTA")))   %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MARGEN" HeaderText="Margen" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="ctrlLogin" style="width: 372px">
                &nbsp;<br />
            </td>
            <td>
                &nbsp;</td>
            <td style="width: 363px; text-align:right">
                <strong style="text-align: right">&nbsp;&nbsp;</strong></td>
            <td style="width: 171px; text-align:center">
                Total Costo de Venta</td>
            <td style="text-align:center">
                Venta Total</td>
        </tr>
        <tr>
            <td class="ctrlLogin" style="width: 372px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="width: 363px; text-align:right">
                <strong style="text-align: right">Totales&nbsp;&nbsp;</strong></td>
            <td style="width: 171px">
                <asp:TextBox ID="lblCostoVenta" runat="server" ReadOnly="True" Enabled="False" 
                    CssClass="textbox" Height="20px" Width="145px"></asp:TextBox>
                <asp:MaskedEditExtender runat="server" TargetControlID="lblCostoVenta" Mask="999,999,999"
                    MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" ID="MaskedEditExtender1" />
            </td>
            <td>
                <asp:TextBox ID="lblVentaTotal" runat="server" ReadOnly="True" Enabled="False" 
                    CssClass="textbox"></asp:TextBox>
                <asp:MaskedEditExtender ID="msklblVentaTotal" runat="server" TargetControlID="lblVentaTotal" 
                    Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 90%">
        <tr>
            <td style="text-align: left">
                % Costo Venta = (Costo de Venta/Venta Total)*100 :<br />
            </td>
            <td>
                <asp:TextBox ID="lblPorcentajeCostoVenta" runat="server" ReadOnly="True" 
                    Enabled="False" Width="66px"></asp:TextBox>
                %</td>
            <td style="text-align: right">
                Dias Laborados de la Semana =&nbsp; 
                </td>
            <td>
                <asp:TextBox ID="lblDiasLaborados" runat="server" ReadOnly="True" 
                    Enabled="False" Width="49px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
               Margen de Rentabilidad = (Venta Total - Costo de Venta)/Venta Total*100) :
                </td>
            <td>
                <asp:TextBox ID="lblMargenVenta" runat="server" ReadOnly="True" Enabled="False" 
                    Width="66px"></asp:TextBox>
                %</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
    </table>

    <table border="0" cellpadding="5" cellspacing="0" style="width: 90%">
        <tr>
            <td class="columnForm50" style="color: #33CC33; font-size: x-small; text-align: left" colspan="2">
                <strong><em style="font-size: small">Ingrese los Datos del Producto y Presione el Botón de Guardar</em></strong></td>
            <td class="tdD">
                &nbsp;</td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="columnForm50" style="width: 314px">
                Nombre de Producto *<br />
            </td>
            <td class="tdD">
                1.Unidades Vendidas *<br />
            </td>
            <td class="tdD">
                2.Costo por Unidad Vendida&nbsp;*</td>
            <td class="tdD">
                3.
                Precio de Venta por Unidad*</td>
        </tr>
        <tr>
            <td class="columnForm50" style="width: 314px">
                <asp:TextBox ID="txtNombreproducto" runat="server" MaxLength="128"  style="text-transform :uppercase"
                    Height="16px" />
            </td>
            <td class="tdD">
                <uc1:decimales ID="txtUnivendida" runat="server" />
            </td>
            <td class="tdD">
                <uc1:decimales ID="txtCostounidven" runat="server" />
                

            </td>
            <td class="tdD">
                <uc1:decimales ID="txtPreciounidven" runat="server" />
                
            </td>
        </tr>
        <tr>
            <td class="columnForm50" style="width: 314px">
                <asp:RequiredFieldValidator ID="rfvNOMBREPRODUCTO" runat="server"
                    ControlToValidate="txtNombreproducto" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
            </td>
            <td class="tdD">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Cod ventas&nbsp;*&nbsp;<br />
                    <asp:TextBox ID="txtCod_ventas" runat="server" CssClass="textbox" MaxLength="128" Enabled="False" />
                    <br />
                    Tipo produco&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTIPOPRODUCO" runat="server" 
                        ControlToValidate="txtTipoproduco" Display="Dynamic" 
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                        ValidationGroup="vgGuardar" />
                    <br />
                    <asp:TextBox ID="txtTipoproduco" runat="server" CssClass="textbox" 
                        MaxLength="128">1</asp:TextBox>
                </asp:Panel>
                </td>
            <td class="tdD">
                &nbsp;</td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        </table>
    <%--<asp:TemplateField HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>--%>
</asp:Content>
