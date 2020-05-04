<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hiddenCodDeudor" runat="server" />
    <asp:MultiView ActiveViewIndex="0" ID="mvNuevo" runat="server">
        <asp:View runat="server">

            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel2" runat="server">
                            <table style="width: 90%;">
                                <tr>
                                    <td class="logo" colspan="3" style="text-align: left">
                                        <strong>DATOS DEL DEUDOR</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Identificación
                                    </td>
                                    <td style="text-align: left">Tipo Identificación
                                    </td>
                                    <td style="text-align: left">Nombre
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="350px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 99%;">
                                <tr>
                                    <td colspan="5"></td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align: left">
                                        <strong>DATOS DEL CRÉDITO</strong>&nbsp;&nbsp;
                                <asp:TextBox ID="txtNumero_solicitud" runat="server" CssClass="textbox"
                                    Enabled="False" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 278px;">
                                        <strong>Número Radicación</strong></td>
                                    <td style="text-align: left">Monto
                                    </td>
                                    <td style="text-align: left" colspan="2">Línea de Crédito</td>
                                    <td style="text-align: left">Fecha Solicitud</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 278px;">&nbsp;<asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"
                                        Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox"
                                            Enabled="false" />
                                    </td>
                                    <td style="text-align: left" colspan="2">
                                        <asp:DropDownList ID="ddlLineas" runat="server" CssClass="textbox"
                                            Enabled="False" Height="27px" Width="177px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="textbox"
                                            Enabled="false" OnTextChanged="txtPlazo_TextChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 278px; height: 21px;">Valor de la Cuota
                                    </td>
                                    <td style="text-align: left; height: 21px;">Periodicidad
                                    </td>
                                    <td style="text-align: left; height: 21px;">Plazo
                                    </td>
                                    <td style="text-align: left; height: 21px;">Forma de Pago
                                
                                    </td>
                                    <td style="text-align: left; height: 21px;">Estado</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 278px;">
                                        <uc2:decimales ID="txtValor_cuota" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox"
                                            Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false"
                                            OnTextChanged="txtPlazo_TextChanged" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox"
                                            Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"
                                            Style="margin-bottom: 0px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:Label ID="lbDocumentos" runat="server" Text="DOCUMENTOS REQUERIDOS"
                            Style="font-weight: 700"></asp:Label>
                        <br />
                        <hr />
                    </td>
                </tr>

                <tr>
                    <td class="tdI">
                        <asp:UpdatePanel ID="updategrilla" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="tipo_documento"
                                    ForeColor="Black" GridLines="Vertical"
                                    PageSize="20"
                                    Style="margin-right: 0px" Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="IdDoc" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Lblid" runat="server" Text='<%# Bind("iddocumento") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id" Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tipo_documento") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Lbldocumento" runat="server"
                                                    Text='<%# Bind("tipo_documento") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                        <asp:BoundField DataField="tipo" HeaderText="Requerido" />
                                        <asp:BoundField DataField="aplica_codeudor" HeaderText="Aplica al codeudor" />
                                        <asp:TemplateField HeaderText="Entregado">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="ChkEntregado" runat="server" Checked='<%# Eval("estado_doc") %>' AutoPostBack="true"
                                                    OnCheckedChanged="ChkEntregado_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observaciones">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtobservaciones" runat="server"
                                                    Text='<%# Bind("observaciones") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Anexo">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfechaanexo" runat="server" Text='<%# Bind("fecha_anexo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfechaanexo" runat="server"
                                                    Text='<%# Bind("fechaanexo") %>'></asp:TextBox>
                                                <asp:CalendarExtender ID="ceFechaAnexo" runat="server"
                                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtfechaanexo" TodaysDateFormat="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Posible Entrega">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfechaentrega" runat="server" Text='<%# Bind("fechaentrega") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfechaentrega" runat="server"
                                                    Text='<%# Bind("fechaentrega") %>'></asp:TextBox>
                                                <asp:CalendarExtender ID="ceFechaEntrega" runat="server"
                                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtfechaentrega" TodaysDateFormat="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                        <br />
                        <asp:UpdatePanel ID="updatenuevo" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvListaNuevo" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="tipo_documento"
                                    ForeColor="Black" GridLines="Vertical"
                                    PageSize="20"
                                    Style="font-size: x-small; margin-right: 0px" Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbldocumento" runat="server"
                                                    Text='<%# Bind("tipo_documento") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("tipo_documento") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                        <asp:BoundField DataField="tipo" HeaderText="Requerido" />
                                        <asp:BoundField DataField="aplica_codeudor" HeaderText="Aplica al codeudor" />
                                        <asp:TemplateField HeaderText="Entregado">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEntregado" runat="server" Text='<%# Bind("entregado") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkEntregadoNuevo" runat="server" Enabled="true"
                                                    OnCheckedChanged="ChkEntregadoNuevo_CheckedChanged" AutoPostBack="True" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observaciones">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtobservaciones" runat="server" Width="70px"
                                                    Text='<%# Bind("observaciones") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Anexo">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfechaanexo0" runat="server" Width="65px"
                                                    Text='<%# Bind("fecha_anexo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfechaanexo" runat="server" Width="65px"
                                                    Text='<%# Bind("observaciones") %>'></asp:TextBox>
                                                <asp:CalendarExtender ID="ceFechaAnexo0" runat="server"
                                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtfechaanexo" TodaysDateFormat="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvfecchaanexo" runat="server" Width="30px"
                                                    ControlToValidate="txtfechaanexo" Display="Dynamic"
                                                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                    ValidationGroup="vgGuardar" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Posible Entrega">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfechaentrega" runat="server" Width="65px"
                                                    Text='<%# Bind("fecha_entrega") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfechaentrega" runat="server" Width="65px"
                                                    Text='<%# Bind("observaciones") %>'></asp:TextBox>
                                                <asp:CalendarExtender ID="ceFechaEntrega" runat="server"
                                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtfechaentrega" TodaysDateFormat="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvfecchaentrega" runat="server" Width="30px"
                                                    ControlToValidate="txtfechaentrega" Display="Dynamic"
                                                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                    ValidationGroup="vgGuardar" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                <br />
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                <br />

                                <asp:Label ID="lblInfo" runat="server" Font-Bold="True"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td class="tdI"></td>
                        </tr>
            </table>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnCorreo" runat="server" Text="Enviar Correo" OnClick="btnCorreo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
