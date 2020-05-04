<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="logo" style="text-align: left" colspan="2">
                                                <asp:CompareValidator ID="cvnumero_radicacion1" runat="server"
                                                    ControlToValidate="txtnumero_radicacion" Display="Dynamic"
                                                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="text-align: left">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="logo" style="width: 195px; text-align: left">Número Radicación</td>
                                            <td style="width: 138px; text-align: left">Línea de Crédito</td>
                                            <td style="text-align: left">&nbsp;Oficina</td>
                                        </tr>
                                        <tr>
                                            <td class="logo" style="width: 195px; text-align: left">
                                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Width="180px"
                                                    MaxLength="128" />
                                            </td>
                                            <td style="width: 138px; text-align: left">
                                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox"
                                                    Width="327px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left">&nbsp;<asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" Width="191px">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                <asp:Panel ID="Panel2" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="logo" style="width: 197px; text-align: left">Identificación</td>
                                            <td style="width: 197px; text-align: left" class="logo">Nombre Completo</td>
                                            <td style="text-align: left">&nbsp;Código de nómina</td>
                                        </tr>
                                        <tr>
                                            <td class="logo" style="width: 197px; text-align: left">
                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="180px"/>
                                                <br />
                                            </td>
                                            <td style="width: 342px; text-align: left">
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="327px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="180px" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="logo" style="width: 197px; text-align: left; height: 25px;">
                                                <asp:CompareValidator ID="cvidentificacion" runat="server"
                                                    ControlToValidate="txtidentificacion" Display="Dynamic"
                                                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="width: 342px; text-align: left; height: 25px;"></td>
                                            <td style="text-align: left; height: 25px;"></td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                    <Columns>
                        <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_linea_credito" HeaderText="Línea">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="center" />
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
