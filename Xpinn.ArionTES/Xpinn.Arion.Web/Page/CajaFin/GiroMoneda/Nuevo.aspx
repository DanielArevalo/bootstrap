<%@ Page Title=".: Registro de Operaciones :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="Panel2" runat="server">
        <table style="width: 100%;">
            <tr>
                <td colspan="8" style="text-align: center; color: #FFFFFF; background-color: #359af2">
                    <strong>Servicio de Giros</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 60px; text-align: left;">
                    <span style="font-size: x-small">Oficina</span>&nbsp;
                </td>
                <td class="logo" style="width: 181px">
                    <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False" Width="151px"></asp:TextBox>
                </td>
                <td style="width: 50px; font-size: x-small;">
                    Caja
                </td>
                <td class="logo" style="width: 186px">
                    <asp:TextBox ID="txtCaja" runat="server" CssClass="textbox" Enabled="False" Width="185px"></asp:TextBox>
                </td>
                <td style="width: 60px; font-size: x-small;">
                    Cajero
                </td>
                <td style="width: 113px">
                    <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" Enabled="False" Width="173px"></asp:TextBox>
                </td>
                <td style="font-size: x-small; width: 150px; text-align: right">
                    Fecha y Hora de Transacción
                </td>
                <td style="margin-left: 240px">
                    <asp:TextBox ID="txtFechaTransaccion" runat="server" CssClass="textbox" Enabled="false"
                        MaxLength="10" Width="132px"></asp:TextBox>
                    <asp:TextBox ID="txttransacciondia" runat="server" CssClass="textbox" Enabled="false"
                        Visible="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelCooperativa" runat="server" Width="100%">
        <table style="width: 100%; margin-bottom: 7px;">
            <tr>
                <td style="text-align: center; color: #FFFFFF; background-color: #359af2">
                    <strong>Datos del Cliente</strong>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td style="width: 250px; text-align: left">
                    Tipo Identificación
                </td>
                <td style="text-align: left; width: 250px">
                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Height="27px"
                        Width="182px">
                    </asp:DropDownList>
                </td>
                <td style="width: 200px; text-align: left">
                    Identificación
                </td>
                <td style="width: 260px; text-align: left">
                    <asp:TextBox ID="txtCodPersona" runat="server" Visible="false" />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px"
                        MaxLength="12"></asp:TextBox>
                    <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                        OnClick="btnConsultaPersonas_Click" />
                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                </td>
                <td style="width: 631px; text-align: left">
                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar" />
                </td>
            </tr>
            <tr>
                <td style="width: 250px; text-align: left">
                    Nombres y Apellidos
                </td>
                <td colspan="4" style="text-align: left">
                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false"
                        Width="582px"></asp:TextBox>
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <table style="width: 100%;">
        <tr>
            <td style="text-align: left; font-size: small;">
                <strong>Datos de la Transacción</strong>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="width: 50%; text-align: center;">
                <asp:RadioButtonList ID="rblRegistro" runat="server" Width="350px" RepeatDirection="Horizontal"
                AutoPostBack="true" onselectedindexchanged="rblRegistro_SelectedIndexChanged">
                    <asp:ListItem Value="I" Selected="True">Registro del Giro</asp:ListItem>
                    <asp:ListItem Value="E">Entrega del Giro</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="width: 50%; text-align: center;">
                &nbsp;
            </td>
        </tr>
    </table>    
    <asp:Panel ID="panelIngreso" runat="server">
        <table>
            <tr>
                <td style="width: 13%; text-align: left;">
                    Monto del Giro
                </td>
                <td style="width: 15%; text-align: left;">
                    <uc1:decimales ID="txtMontoGiro" runat="server" CssClass="textbox" Width="170px" />
                </td>
                <td style="width: 13%; text-align: center;">
                    Moneda
                </td>
                <td style="width: 20%; text-align: left;">
                    <asp:DropDownList ID="ddlMonedaEnvio" Width="100%" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
                <td style="width: 13%; text-align: center;">
                    Ofi. Entrega
                </td>
                <td style="width: 20%; text-align: left;">
                    <asp:DropDownList ID="ddlOficinaEntrega" Width="100%" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: left; font-size: small">
                    <strong>Persona Reclamo:</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 13%; text-align: left;">
                    Identificación:
                </td>
                <td style="width: 15%; text-align: left;">
                    <asp:TextBox ID="txtIdentificEnvio" runat="server" CssClass="textbox" Width="120px" />
                </td>
                <td style="width: 13%; text-align: center;">
                    Nombres y Apellidos:
                </td>
                <td colspan="3" style="text-align: left;">
                    <asp:TextBox ID="txtNomApe" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;vertical-align:top">
                    Observaciones:
                </td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="87%" TextMode="MultiLine"
                        Style="height: 35px;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelEgreso" runat="server">
        <table>
            <tr>
                <td style="text-align: left">
                    <strong>Listado de Giros</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvGiros" runat="server" Width="100%" GridLines="Both" AutoGenerateColumns="False"
                        Style="font-size: xx-small" CellPadding="0" AllowPaging="False" HeaderStyle-Font-Size="Small"
                        ShowFooter="true" DataKeyNames="idgiromoneda">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <cc1:CheckBoxGrid id="cbSeleccionar" runat="server" autopostback="True" commandargument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                        oncheckedchanged="cbSeleccionar_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="idgiromoneda" HeaderText="Código">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Monto del Giro">
                                <ItemTemplate>
                                    <uc1:decimales ID="txtMontoGiro" runat="server" Text='<%# Bind("valor") %>' Enabled="false"/>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <uc1:decimales ID="txtMontoTotalGiro" runat="server" Enabled="false" />
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="nom_moneda" HeaderText="Moneda">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombres y Apellidos">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <FooterStyle CssClass="gridHeader" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                <asp:Label ID="lblTotalReg" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>        
    </asp:Panel>
    
    <asp:Panel ID="panelFormaPago" runat="server" Width="100%">
    <hr />
        <table style="width: 100%;">
            <tr>
                <td class="logo" style="width: 120px">
                    <strong>Forma de Pago</strong>
                </td>
                <td style="width: 124px">
                    &nbsp;
                </td>
                <td class="logo" style="width: 132px">
                    &nbsp;
                </td>
                <td style="width: 33px">
                    &nbsp;
                </td>
                <td rowspan="5">
                    <asp:Panel ID="Panel8" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 87px">
                                    <strong>Cheques</strong>
                                </td>
                                <td style="width: 106px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px; font-size: x-small;">
                                    Núm. Cheque
                                </td>
                                <td style="width: 106px">
                                    E<span style="font-size: x-small">ntidad Bancaria</span>
                                </td>
                                <td style="font-size: x-small">
                                    Valor Cheque
                                </td>
                                <td style="font-size: x-small">
                                    Moneda
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 87px">
                                    <asp:TextBox ID="txtNumCheque" runat="server" CssClass="textbox" Width="94px" MaxLength="20"></asp:TextBox>
                                    <asp:Label ID="numchequevacio" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                                </td>
                                <td style="width: 106px">
                                    <asp:DropDownList ID="ddlBancos" runat="server" CssClass="textbox" Width="123px">
                                    </asp:DropDownList>
                                    <asp:Label ID="bancochquevacio" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                                </td>
                                <td>
                                    <uc1:decimales ID="txtValCheque" runat="server" CssClass="textbox" Width="100px">
                                    </uc1:decimales>
                                    <asp:Label ID="valorchequevacio" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonCheque" runat="server" CssClass="textbox" Width="82px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnGoCheque" runat="server" OnClick="btnGoCheque_Click" Text="&gt;&gt;"
                                        Width="22px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:GridView ID="gvCheques" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" Style="font-size: small"
                                        Width="79%" ShowHeaderWhenEmpty="True" OnRowDeleting="gvCheques_RowDeleting">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                        ToolTip="Eliminar" Width="16px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="entidad" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                            <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                            <asp:BoundField DataField="numcheque" HeaderText="Núm. Cheque" />
                                            <asp:BoundField DataField="nomentidad" HeaderText="Entidad" />
                                            <asp:BoundField DataField="valor" DataFormatString="{0:N0}" HeaderText="Valor">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
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
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right">
                                    Valor Total Cheques
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtValTotalCheque" runat="server" CssClass="textbox" Enabled="false"
                                        Width="171px" Style="text-align: right"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtValTotalCheque"
                                        Mask="999,999,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="logo" style="width: 120px">
                    Moneda
                </td>
                <td style="width: 124px">
                    Forma de Pago
                </td>
                <td class="logo" style="width: 132px; text-align: center">
                    Valor
                </td>
                <td style="width: 33px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="logo" style="width: 120px">
                    <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="textbox" Width="114px">
                    </asp:DropDownList>
                </td>
                <td style="width: 124px">
                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="133px">
                    </asp:DropDownList>
                </td>
                <td class="logo" style="width: 132px">
                    <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Width="120px"></uc1:decimales>
                </td>
                <td style="width: 33px">
                    <asp:Button ID="btnGoFormaPago" runat="server" OnClick="btnGoFormaPago_Click" Text="&gt;&gt;"
                        Width="21px" />
                </td>
            </tr>
            <tr>
                <td class="logo" colspan="3">
                    <asp:Panel ID="PanelForPago" runat="server">
                        <asp:GridView ID="gvFormaPago" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" PageSize="20" Width="98%" Style="text-align: left; font-size: small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fpago" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
                                <asp:BoundField DataField="nomfpago" HeaderText="F.Pago">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipomov" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
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
                    </asp:Panel>
                </td>
                <td class="logo">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="logo" style="width: 120px; text-align: right;">
                    Valor Total
                </td>
                <td style="width: 124px">
                    <asp:TextBox ID="txtValTotalFormaPago" runat="server" CssClass="textbox" Enabled="false"
                        Width="171px" Style="text-align: right"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtValTotalFormaPago"
                        Mask="999,999,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                    </asp:MaskedEditExtender>
                </td>
                <td class="logo" style="width: 132px">
                    &nbsp;
                </td>
                <td style="width: 33px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
</asp:Content>
