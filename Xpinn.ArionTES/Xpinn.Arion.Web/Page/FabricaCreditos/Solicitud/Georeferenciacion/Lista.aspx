<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI" style="width: 439px">&nbsp;</td>
                            <td class="tdD">&nbsp;</td>
                            <td class="tdD">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 439px; text-align: left">Número radicación<asp:CompareValidator ID="cvNumero_radicacion" runat="server"
                                ControlToValidate="txtnumero_radicacion" Display="Dynamic"
                                ErrorMessage=" Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"
                                    MaxLength="128" OnTextChanged="txtNumero_radicacion_TextChanged"
                                    Width="158px" />
                                <br />
                            </td>

                            <td class="tdD" style="width: 439px; text-align: left">Oficina<br />
                                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="dropdown"
                                    Width="174px">
                                </asp:DropDownList>
                                <br />
                                <br />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdD" style="width: 439px; text-align: left">Nombre<br />
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox"
                                    MaxLength="128" Width="300px" />
                            </td>
                            <td class="tdD" style="width: 439px; text-align: left">Identificación&nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server"
                                ControlToValidate="txtIdentificacion" Display="Dynamic"
                                ErrorMessage=" Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                                    Width="197px" />
                                <br />
                            </td>
                            <td style="text-align: left; width: 439px;">Código de nómina<br />
                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 439px; text-align: left">Línea de crédito&nbsp;<br />
                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox"
                                    MaxLength="128" Width="300px" />
                            </td>
                            <td class="tdI" style="width: 439px; text-align: left">Monto solicitado &nbsp;<br />
                                <asp:TextBox ID="Txtmonto_solicitado" runat="server" CssClass="textbox"
                                    MaxLength="128" Width="196px" />
                            </td>
                            <td class="tdD">&nbsp;</td>
                            <td class="tdD">&nbsp;</td>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_referencia">
                    <Columns>
                        <asp:BoundField DataField="COD_PERSONA" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <%--      <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate> 
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>--%>
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
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre completo">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Direccion">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>

                        <%--     <asp:BoundField DataField="estado" HeaderText="Estado" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>

                        <%--
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToString(resultado["FECHA_SOLICITUD"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);--%>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
