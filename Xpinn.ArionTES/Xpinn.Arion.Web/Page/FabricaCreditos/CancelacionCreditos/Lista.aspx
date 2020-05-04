<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:panel id="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="95%">
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
                                            <td style="text-align: left">Identificación
                                            </td>
                                            <td style="text-align: left">Nombre Completo
                                            </td>
                                            <td style="text-align: left">Código de nómina<br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="12" Width="167px" />
                                                <br />
                                                <asp:CompareValidator ID="cvnumero_radicacion" runat="server" ControlToValidate="txtnumero_radicacion"
                                                    Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Style="font-size: x-small" Type="Double" ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlLineas" runat="server" CssClass="textbox"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                                <br />
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="177px" />
                                                    <br />
                                                </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="177px" />
                                                <br />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Oficina
                                            </td>
                                            <td style="text-align: left">Estado
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox"
                                                    Width="167px">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox"
                                                    Width="128px">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Analizado" Value="L"></asp:ListItem>
                                                    <asp:ListItem Text="Aprobado" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="Generado" Value="G"></asp:ListItem>
                                                    <asp:ListItem Text="Anulado/Borrado" Value="B"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">Listado de Créditos<br />
                            </td>
                        </tr>
                    </table>
                </asp:panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:gridview id="gvLista" runat="server" width="100%" gridlines="Horizontal" autogeneratecolumns="False"
                    onrowdatabound="gvLista_RowDataBound" onrowdeleting="gvLista_RowDeleting" allowpaging="True"
                    onpageindexchanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged"
                    onrowediting="gvLista_RowEditing" pagesize="20" headerstyle-cssclass="gridHeader"
                    pagerstyle-cssclass="gridPager" rowstyle-cssclass="gridItem"
                    datakeynames="numero_radicacion" style="font-size: x-small">
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
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Credito">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="linea_credito" HeaderText="Linea Credito" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombres">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_aproba" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:gridview>
                <asp:label id="lblTotalRegs" runat="server" visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>



