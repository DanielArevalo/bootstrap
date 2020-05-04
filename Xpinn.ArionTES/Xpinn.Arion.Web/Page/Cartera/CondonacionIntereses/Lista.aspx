<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" style="width: 100%;" cellpadding="0">
            <tr>
                <td class="tdI" style="text-align: left">
                    <strong>Ingresar Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 199px; text-align: left">Número Radicación
                </td>
                <td style="width: 163px; text-align: left">Línea de Crédito
                </td>
                <td style="width: 377px; text-align: left">Oficina
                </td>
            </tr>
            <tr>
                <td style="width: 199px; text-align: left">
                    <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="12" />
                    <br />
                    <asp:CompareValidator ID="cvnumero_radicacion" runat="server" ControlToValidate="txtnumero_radicacion"
                        Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                        SetFocusOnError="True" Style="font-size: x-small" Type="Double" ValidationGroup="vgGuardar" />
                </td>
                <td style="width: 163px; text-align: left">
                    <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" />
                    <br />
                </td>
                <td style="width: 377px; text-align: left">
                    <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox"
                        Width="221px">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 199px; text-align: left">Identificación
                </td>
                <td style="width: 163px; text-align: left">Primer Apellido
                </td>
                <td style="width: 377px; text-align: left">Segundo Apellido
                </td>
            </tr>
            <tr>
                <td style="width: 199px; text-align: left;">
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                    <br />
                    <asp:CompareValidator ID="cvidentificacion" runat="server" ControlToValidate="txtidentificacion"
                        Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                        SetFocusOnError="True" Style="font-size: x-small" Type="Double" ValidationGroup="vgGuardar" />
                </td>
                <td style="width: 163px; text-align: left;">
                    <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox" MaxLength="128"
                        Width="310px" />
                    <br />
                </td>
                <td style="width: 377px; text-align: left;">
                    <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox"
                        Width="288px" />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 199px; text-align: left">Monto<br />
                    <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Enabled="False" />
                </td>
                <td style="width: 199px; text-align: left">Nombre Completo
                    <br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="310px" />
                </td>
                <td style="width: 199px; text-align: left">Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="288px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 199px;">Plazo<br />
                    <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="False" />
                </td>
                <td style="text-align: left;">Periodicidad<br />
                    <asp:TextBox ID="txtPlazo0" runat="server" CssClass="textbox" Enabled="False" />
                </td>
                <td style="width: 377px; text-align: left;">Foma de pago<br />
                    <asp:TextBox ID="txFormaPago" runat="server" CssClass="textbox"
                        Enabled="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
                <td style="text-align: left; width: 377px">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
        OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
        <Columns>
            <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
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
            <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre" HeaderText="Nombre Completo">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="oficina" HeaderText="Oficina">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:c}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="plazo" HeaderText="Plazo">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
        <PagerStyle CssClass="gridPager"></PagerStyle>
        <RowStyle CssClass="gridItem"></RowStyle>
    </asp:GridView>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
</asp:Content>


