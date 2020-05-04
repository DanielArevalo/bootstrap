<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI" style="text-align: left">
                                <strong>Ingresar Criterios de Búsqueda</strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table style="width: 90%;">
                                        <tr>
                                            <td style="text-align: left">Número Radicación
                                            </td>
                                            <td style="text-align: left">Línea de Crédito
                                            </td>
                                            <td style="text-align: left">Oficina
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" />
                                                <br />
                                                <asp:CompareValidator ID="cvnumero_radicacion" runat="server" ControlToValidate="txtnumero_radicacion"
                                                    Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Style="font-size: x-small" Type="Double" ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" Width="250px"/>
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" Width="210px">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Identificación
                                            </td>
                                            <td style="text-align: left">Primer Apellido
                                            </td>
                                            <td style="text-align: left">Segundo Apellido
                                            </td>
                                            <td style="text-align: left">Código de nómina
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                                <br />
                                                <asp:CompareValidator ID="cvidentificacion" runat="server" ControlToValidate="txtidentificacion"
                                                    Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Style="font-size: x-small" Type="Double" ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPrimer_apellido" runat="server" CssClass="textbox" MaxLength="150"
                                                    Width="250px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtSegundo_apellido" runat="server" CssClass="textbox" Width="200px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Nombre Completo
                                            </td>
                                            <td style="text-align: left">&nbsp;
                                            </td>
                                            <td style="text-align: left">&nbsp; Estado
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="445px" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkAprobados" runat="server" Text="Créditos para Generación de Documentos" Checked="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;
                                            </td>
                                            <td style="text-align: left">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                <strong>Créditos Aprobados/Generados</strong><br />
                            </td>
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
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                    DataKeyNames="numero_radicacion" Style="font-size: x-small">
                    <Columns>
                        <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
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
                            <ItemStyle HorizontalAlign="center" />
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
                        <asp:BoundField DataField="estado" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="linea_credito" HeaderText="Línea">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>


