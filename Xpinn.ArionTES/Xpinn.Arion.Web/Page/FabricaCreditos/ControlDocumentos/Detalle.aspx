<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<%@ Register src="../../../General/Controles/mensajeGrabar.ascx" tagname="mensajegrabar" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:90%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Identificación
                            </td>
                            <td style="text-align:left">
                                Tipo Identificación
                            </td>
                            <td style="text-align:left">
                                Nombre
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style=" text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left">
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
                    <table style="width:99%;">
                        <tr>
                            <td colspan="5">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align:left">
                                <strong>DATOS DEL CRÉDITO</strong>&nbsp;&nbsp;
                                <asp:TextBox ID="txtNumero_solicitud" runat="server" CssClass="textbox" 
                                    Enabled="False" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width: 278px;">
                                <strong>Número Radicación</strong></td>
                            <td style="text-align:left">
                                Monto
                            </td>
                            <td style="text-align:left" colspan="2">
                                Línea de Crédito</td>
                            <td style="text-align:left">
                                Fecha Solicitud</td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width: 278px;">
                                &nbsp;<asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left" colspan="2">
                                <asp:DropDownList ID="ddlLineas" runat="server" CssClass="textbox" 
                                    Enabled="False" Height="27px" Width="177px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="textbox" 
                                    Enabled="false" ontextchanged="txtPlazo_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width: 278px; height: 21px;">
                                Valor de la Cuota
                            </td>
                            <td style="text-align:left; height: 21px;">
                                Periodicidad
                            </td>
                            <td style="text-align: left; height: 21px;">
                                Plazo
                            </td>
                            <td style="text-align: left; height: 21px;">
                                
                                Forma de Pago
                                
                            </td>
                            <td style="text-align: left; height: 21px;">
                                
                                Estado</td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width: 278px;">
                                <uc2:decimales ID="txtValor_cuota" runat="server" CssClass="textbox" Enabled="false" />                                
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" 
                                    ontextchanged="txtPlazo_TextChanged" />
                            </td>                            
                            <td style="text-align: left">
                                <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>                           
                            <td style="text-align: left">
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True" 
                                    style="margin-bottom: 0px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Label ID="lbDocumentos" runat="server" Text="DOCUMENTOS REQUERIDOS" 
                    style="font-weight: 700"></asp:Label>
                <br />
                                <hr __designer:mapid="2b9" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="tipo_documento" 
                ForeColor="Black" GridLines="Vertical" 
                PageSize="20" 
                style="margin-right: 0px" Width="100%" 
                >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
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
                    <asp:BoundField DataField="tipo" HeaderText="Requerido"/>
                    <asp:BoundField DataField="aplica_codeudor" HeaderText="Aplica al codeudor"/>
                    <asp:TemplateField HeaderText="Entregado">
                        <EditItemTemplate>
                            
                        </EditItemTemplate>
                        <ItemTemplate>
                        
                                        <asp:CheckBox ID="ChkEntregado" runat="server" Checked='<%# Eval("estado_doc") %>' AutoPostBack="true" 
                                            oncheckedchanged="ChkEntregado_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />

                         
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
                            <asp:RequiredFieldValidator ID="rfvfecchaanexo" runat="server" 
                                ControlToValidate="txtfechaanexo" Display="Dynamic" 
                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                ValidationGroup="vgGuardar" />
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
                            <asp:RequiredFieldValidator ID="rfvfecchaentrega" runat="server" 
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
                <br />
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                <br />
                
                <asp:Label ID="lblInfo" runat="server" Font-Bold="True"></asp:Label> 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>        
        <tr>
            <td class="tdI">
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
            </td>
        </tr>        
    </table>
    
</asp:Content>