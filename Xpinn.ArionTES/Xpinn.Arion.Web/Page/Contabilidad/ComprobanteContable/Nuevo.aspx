<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <asp:Label ID="lablerror" runat="server" Visible="false" 
        style="font-size: x-small; text-align: left" ></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false" 
        style="font-size: x-small; text-align: left" ></asp:Label>            
    <asp:MultiView ID="mvComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwTipoComprobante" runat="server">
            <asp:Panel id="PanelTipoComprobante" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: center;color: #FFFFFF; background-color: #0066FF">
                            TIPO DE COMPROBANTE
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="text-align: center" >
                        <td>
                            Escoja el tipo de comprobante a generar
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RadioButton ID="rbIngreso" runat="server" Text="Comprobante de Ingreso" 
                                AutoPostBack="True" oncheckedchanged="rbIngreso_CheckedChanged" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RadioButton ID="rbEgreso" runat="server" Text="Comprobante de Egreso" 
                                AutoPostBack="True" oncheckedchanged="rbEgreso_CheckedChanged" />
                         </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                           <asp:RadioButton ID="rbContable" runat="server" Text="Comprobante Contable" 
                                AutoPostBack="True" oncheckedchanged="rbContable_CheckedChanged" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:ImageButton ID="imgAceptar" runat="server" 
                                ImageUrl="~/Images/btnAceptar.jpg" onclick="imgAceptar_Click" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

        <asp:View ID="vwDatosComprobante" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                       <td class="align-rt" colspan="8" style="font-size: medium; height: 21px; text-align: left;">
                            <strong><asp:Label ID="Lblerror" runat="server" ForeColor="Red" CssClass="align-rt"></asp:Label></strong>
                       </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" Width="815px">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="font-size: medium; width: 301px; text-align:left">
                                            <b>Comprobante No.</b></td>
                                        <td class="columnForm50" style="width: 158px; text-align:left">
                                            <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" 
                                                enabled="false" Width="139px"></asp:TextBox>
                                        </td>
                                        <td class="gridIco" style="width: 204px">
                                            &nbsp;</td>
                                        <td class="logo" style="width: 89px">
                                            Fecha</td>
                                        <td class="gridIco" style="width: 141px">
                                            <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" enabled="false" 
                                                Width="120px"></asp:TextBox>
                                            <asp:MaskedEditExtender ID="mskFecha" runat="server" TargetControlID="txtFecha"
                                                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                        ErrorTooltipEnabled="True" />
                                            <asp:MaskedEditValidator ID="mevFecha" runat="server" ControlExtender="mskFecha"
                                                        ControlToValidate="txtFecha" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                                                        Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                                                        InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgGuardar" ForeColor="Red" />
                                            <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td class="gridIco" style="width: 4px">
                                            &nbsp;</td>
                                        <td style="width: 125px; text-align: right;">                                            
                                            <asp:Label ID="lblSoporte" runat="server" Text="Soporte" style="text-align:left"></asp:Label>                               
                                            <br />          
                                            <br />
                                        </td>
                                        <td style="width: 100px; text-align:left">
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtNumSop" runat="server" CssClass="textbox" Width="105px" 
                                                    AutoPostBack="True" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldNumSop" runat="server" 
                                                        ErrorMessage="Se debe ingresar el número de soporte" 
                                                        ControlToValidate="txtNumSop" Font-Italic="True" Font-Size="XX-Small" 
                                                        ForeColor="#FF0066" ValidationGroup="vgGuardar">
                                                </asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" 
                                Width="815px">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="logo" style="width: 303px; text-align:left">
                                            <asp:Label ID="lblTipoComp" runat="server" Text="Tipo de Comprobante" style="text-align:left"></asp:Label></td>
                                        <td style="text-align:left">
                                            <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" 
                                                Width="190px" style="text-align:left" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTipoComprobante_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 84px;text-align:left">
                                            &nbsp;</td>
                                        <td style="text-align:left">
                                            Oficina</td>
                                        <td style="text-align:left">
                                            <asp:TextBox ID="tbxOficina" runat="server" CssClass="textbox" enabled="false" 
                                                Width="315px" style="text-align:left"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 303px; text-align:left">
                                            <asp:Label ID="lblFormaPago" runat="server" Text="Forma de Pago" style="text-align:left"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" 
                                                Width="190px" style="text-align:left" AutoPostBack="True" 
                                                onselectedindexchanged="ddlFormaPago_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 84px; text-align:left">
                                            &nbsp;
                                        </td>
                                        <td style="text-align:left">
                                            <asp:Label ID="lblEntidad" runat="server" Text="Entidad" style="text-align:left"></asp:Label>
                                        </td>
                                        <td style="text-align:left">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" 
                                                        Width="320px" style="text-align:left" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                <asp:DropDownList ID="ddlEntidad5" runat="server" CssClass="textbox" 
                                                    style="text-align:left" Width="320px" 
                                                    onselectedindexchanged="ddlEntidad5_SelectedIndexChanged" 
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>                                                                                                                                 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 303px; text-align:left">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td style="width: 84px; text-align:left">
                                            &nbsp;</td>
                                        <td style="text-align:left">
                                            <asp:Label ID="lblCuenta" runat="server" Text="Cuenta" style="text-align:left"></asp:Label></td>
                                        <td style="text-align:left">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" 
                                                        style="text-align:left" Width="320px" onselectedindexchanged="ddlCuenta_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>     
                                                </ContentTemplate>   
                                            </asp:UpdatePanel>                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 303px;text-align:left">
                                            Ciudad</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" 
                                                Width="190px" style="text-align:left">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 84px;text-align:left">
                                            &nbsp;</td>
                                        <td style="text-align:left">
                                            Concepto</td>
                                        <td style="text-align:left">
                                            <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="textbox" 
                                                Width="320px" style="text-align:left">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" Width="815px">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="4" style="text-align:left">
                                            <strong>Beneficiario</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 108px; text-align:left">
                                            Identificación</td>
                                        <td style="width: 237px; text-align:left">
                                            Tipo Identificación</td>
                                        <td colspan="2" style="text-align:left">
                                            Nombres y Apellidos</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 108px; text-align:left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                                Width="125px" style="text-align:left" AutoPostBack="True" 
                                                ontextchanged="txtIdentificacion_TextChanged"></asp:TextBox>
                                        </td>
                                        <td style="width: 237px; text-align:left">
                                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" 
                                                Width="156px" style="text-align:left">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align:left">
                                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="474px" 
                                                style="text-align:left" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="text-align:left">
                                            <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Panel ID="Panel5" runat="server" Width="820px">
                            <table style="width:100%">
                                <tr>
                                    <td style="font-size: xx-small">
                                        <asp:accordion ID="Accordion1" runat="server" 
                                            FadeTransitions="True" FramesPerSecond="50" Width="820px" 
                                            TransitionDuration="200" HeaderCssClass="accordionCabecera" 
                                            ContentCssClass="accordionContenido" Height="312px" 
                                            style="font-size: x-small">
                                            <Panes>
                                              <asp:AccordionPane ID="AccordionPane7" runat="server" ContentCssClass="" 
                                                HeaderCssClass="">
                                                <Header>
                                                DETALLE DE MOVIMIENTOS
                                                </Header>
                                                <Content>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvDetMovs" runat="server" AutoGenerateColumns="false" AllowPaging="True" 
                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"  GridLines="Horizontal" 
                                                        CellPadding="4" ForeColor="Black"  Height="131px" ShowFooter="True" 
                                                        PageSize="30" style="font-size: xx-small; font-weight: 700" Width="100%" DataKeyNames="codigo" 
                                                        OnRowDeleting="gvDetMovs_RowDeleting" onpageindexchanging="gvDetMovs_PageIndexChanging" OnRowCommand="gvDetMovs_RowCommand">                                                       
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                                                                <ItemTemplate>
                                                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Nuevo"/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"   ShowDeleteButton="True"/>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" CommandArgument="<%# Container.DataItemIndex %>"
                                                                        ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cod. Cuenta" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodCuenta" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("cod_cuenta") %>' Width="70"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Nom. Cuenta">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNomCuenta" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("nombre_cuenta") %>' Width="120"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMoneda" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("nom_moneda") %>' Width="60"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="C/C" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCentroCosto" runat="server" style="font-size:xx-small; text-align:center" Text='<%# Bind("centro_costo") %>' Width="20"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="C/G" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCentroGestion" runat="server" style="font-size:xx-small; text-align:center" Text='<%# Bind("centro_gestion") %>' Width="10"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Detalle" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDetalle" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("detalle") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo Movimiento" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipo" runat="server" style="font-size:xx-small; text-align:center" Text='<%# Bind("tipo") %>' Width="40"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValor" runat="server" style="font-size:xx-small; text-align:right" Text='<%# Eval("valor", "{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cod.Ter" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTercero" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("tercero") %>' Width="40"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdentificacion" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("identificacion") %>' Width="40"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nombre Tercero" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNombre" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("nom_tercero") %>' > </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="gridHeader" />
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
                                                </Content>
                                              </asp:AccordionPane>
                                            </Panes>
                                        </asp:accordion>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </td></tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelObservaciones" runat="server" Width="80%">
                                <table>
                                    <tr>
                                        <td valign="top" rowspan="2" style="text-align:left">
                                            <strong>Observaciones</strong>
                                        </td>
                                        <td valign="top" rowspan="2" style="text-align:left; width:300px" width="70">
                                            <asp:TextBox ID="tbxObservaciones" runat="server" Height="57px" 
                                                TextMode="MultiLine" Width="460px"></asp:TextBox>
                                        </td>
                                        <td style="text-align:center">
                                            Total Debitos</td>
                                        <td style="text-align:center">
                                            Total Creditos</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">
                                            <uc1:decimales ID="tbxTotalDebitos" runat="server" Enabled="false" 
                                                CssClass="textbox" style="text-align:right"></uc1:decimales>                                        
                                        </td>                                    
                                        <td style="text-align:left">
                                            <uc1:decimales ID="tbxTotalCreditos" runat="server" Enabled="false" 
                                                CssClass="textbox" style="text-align:right"></uc1:decimales>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr />                               
                                        </td>
                                    </tr>                         
                                    <tr>
                                        <td style="text-align:left">
                                            Elaborado Por
                                        </td>
                                        <td style="text-align:left">
                                            <asp:TextBox ID="txtCodElabora" runat="server" CssClass="textbox" Visible="False" 
                                                Width="23px"></asp:TextBox>
                                            <asp:TextBox ID="txtElaboradoPor" runat="server" CssClass="textbox" 
                                                Width="477px" style="text-align:left"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Visible="False" 
                                                Width="23px" Enabled="False" style="text-align:left"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodAprobo" runat="server" CssClass="textbox" Visible="False" 
                                                Width="23px" Enabled="False" style="text-align:left"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelFooter" runat="server" Width="815px">
                                <table>
                                    <tr>
                                        <td style="font-weight: 700" colspan="3">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: 700; text-align:left">BENEFICIARIO DEL CHEQUE</td>
                                        <td colspan="2">                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">Identificación</td>
                                        <td colspan="2" style="text-align:left">
                                            <asp:TextBox ID="txtidenti" runat="server" 
                                                CssClass="textbox" style="text-align:left" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">Tipo Identificaciòn</td>
                                        <td colspan="2" style="text-align:left">                              
                                              <asp:DropDownList ID="ddlTipoIdentificacion0" runat="server" CssClass="textbox" 
                                                  style="text-align:left" Width="156px">
                                              </asp:DropDownList>                              
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">Nombre Completo</td>
                                        <td colspan="2" style="text-align:left">                                                            
                                            <asp:TextBox ID="txtnom" runat="server" CssClass="textbox" 
                                                style="text-align:left" Width="477px"></asp:TextBox>                            
                                        </td>
                                    </tr>    
                                </table> 
                            </asp:Panel>
                            <asp:Panel ID="PanelImprimir" runat="server" Width="815px">
                                <table>
                                    <tr>
                                        <td style="text-align:left">
                                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                                onclick="btnInforme_Click" onclientclick="btnInforme_Click" 
                                                Text="Imprimir" />
                                        </td>
                                        <td style="text-align:left">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:ModalPopupExtender ID="mpeNuevo" runat="server" PopupControlID="panelDetalle"
                TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
            <asp:ModalPopupExtender ID="mpePersonas" runat="server" TargetControlID="btnConsultaPersonas"
                PopupControlID="PanelListadoPersonas" BackgroundCssClass="modalBackground" DropShadow="true"
                CancelControlID="CancelButton" OkControlID = "OkButton">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelListadoPersonas" runat="server" Width="683px" BorderColor="#0033CC"
                BorderWidth="2px" Style="display: none;">
                <table cellpadding="5" cellspacing="0" style="width: 100%; background-color: #DEDFDE"
                    border="0">
                    <tr style="background-color: #DEDFDE">
                        <td class="style1">
                            <uc1:BusquedaRapida ID="ListadoPersonas" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="background-color: #DEDFDE">
                            <asp:Button ID="CancelButton" runat="server" Text="Cancelar" CssClass="btn8" />
                            <asp:Button ID="OkButton" runat="server" Text="Aceptar" CssClass="btn8" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="panelDetalle" runat="server" BackColor="#ccccff" Style="text-align: left; width:420px">
                <asp:UpdatePanel ID="upActividadReg" runat="server">
                <ContentTemplate>                  
                    <div id="popupcontainer" style="width: 420px">
                    <div class="row popupcontainertitle">
                        <div class="cell popupcontainercell" style="text-align: center">
                            DETALLE DEL COMPROBANTE
                            <table border="1" width="350px">
                                <tr>
                                    <td style="width: 120px; text-align:left; font-weight: 700; font-size: x-small;">
                                        Cod. Cuenta
                                    </td>
                                    <td style="width: 150px; text-align:left">
                                        <asp:DropDownList ID="dropcuenta" runat="server" AutoPostBack="True" 
                                            CssClass="dropdown" Width="119px" 
                                            onselectedindexchanged="dropcuenta_SelectedIndexChanged" >
                                        </asp:DropDownList>

                                    </td>
                                    <td style="width: 150px; text-align:left">
                                        <asp:Label ID="lblNomCuenta" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">
                                        Moneda
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">
                                        <asp:DropDownList ID="dropmoneda" runat="server" CssClass="dropdown" 
                                            Width="100px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">
                                        C/C
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">
                                        <asp:DropDownList ID="dropcc" runat="server" CssClass="dropdown" 
                                            AutoPostBack="True" Width="70px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">
                                        C/G
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">
                                        <asp:TextBox ID="cg" runat="server" Width="70px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">                                
                                        Detalle
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">                                
                                        <asp:TextBox ID="detalle" runat="server" Width="300px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">                                
                                        Tipo Movimiento
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">                                
                                        <asp:DropDownList MaxLength="10" ID="ddlTipo" runat="server" Style="font-size: x-small" Width="50px" CssClass="dropdown">
                                                <asp:ListItem Value="D">D</asp:ListItem>
                                                <asp:ListItem Value="C">C</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">                                
                                        Valor
                                    </td>
                                    <td style="width: 120px; text-align:left" colspan="2">                                
                                        <asp:TextBox MaxLength="10" ID="txtValor1" runat="server" Text='<%# Bind("valor") %>'
                                                Width="150px" Style="text-align:right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">                                     
                                        Cod.Tercero
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">                                                                                 
                                        <asp:Label ID="txtTercero1" runat="server" Text="" style="font-size: x-small;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">  
                                        Identificacion
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">  
                                        <asp:TextBox ID="txtIdentificD" runat="server" Width="150px" 
                                            ontextchanged="txtIdentificD_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align:left; font-weight: 700; font-size: x-small;">                                
                                        Nombre
                                    </td>
                                    <td style="width: 150px; text-align:left" colspan="2">                                
                                        <asp:Label ID="txtNombreTercero1" runat="server" Text="" style="font-size: x-small;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left" colspan="3">
                                        <asp:Button ID="Guardar_modi" runat="server" Text="Guardar" CssClass="button" 
                                            CausesValidation="false" OnClick="Guardar_modi_Click" style="height: 26px" />
                                        <asp:Button ID="btnCloseReg0" runat="server" CausesValidation="false" CssClass="button" OnClick="btnCloseReg_Click" Text="Cerrar" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>          
        </asp:View>

        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Comprobante Grabado Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar" 
                                onclick="btnContinuar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

        <asp:View ID="mvProceso" runat="server">
            <asp:Panel id="PanelProceso" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        <br /><br /><br /><br />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        Por favor seleccione el tipo de comprobante y concepto deseado para el proceso
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="lblProceso" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">                        
                        <asp:ListBox ID="lstProcesos" runat="server" Width="396px" Height="143px">
                        </asp:ListBox>                        
                        <br />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center; font-weight: 700;">
                        <asp:ImageButton ID="imgAceptarProceso" runat="server" 
                            ImageUrl="~/Images/btnAceptar.jpg" onclick="imgAceptarProceso_Click"  />                        
                        &nbsp;&nbsp;&nbsp;
                        </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </asp:View>
        
        <asp:View ID="ViewComprobanteImpr" runat="server">
            <asp:Panel id="PanelComprobanteImpr" runat="server">
                <rsweb:ReportViewer ID="RpviewComprobante" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" Height="361px" InteractiveDeviceInfos="(Colección)" 
                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">   
                    <localreport reportpath="Page\Contabilidad\Comprobante\ReportComprobante.rdlc">
                    </localreport>
                </rsweb:ReportViewer>
            </asp:Panel>
        </asp:View>

        <asp:View ID="mvError" runat="server">
            <asp:Panel id="Panel6" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblErrorGenerar" runat="server" 
                                Text="Se presento error al generar el comprobante" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
                                onclick="btnRegresar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

        <asp:View ID="mvProcesoContable" runat="server">
            <asp:Panel id="Panel7" runat="server">
                <table style="width: 50%;">
                    <tr>
                        <td colspan="4" style="text-align:left">
                            Período de Vigencia
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <uc1:fecha ID="txtFechaIni" runat="server"></uc1:fecha>                            
                        </td>
                        <td style="text-align:left">
                            &nbsp;&nbsp;a&nbsp;&nbsp;
                        </td>
                        <td style="text-align:left">
                            <uc1:fecha ID="txtFechaFin" runat="server"></uc1:fecha>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="text-align:left">
                        </td>
                        <td>                            
                        </td>
                        <td>                                                        
                        </td>
                    </tr>
                </table>
                <table style="width: 70%;">
                    <tr>
                        <td style="text-align: left;">
                            Tipo de Operación&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlTipoOperacion" runat="server" CssClass="dropdown" 
                                Width="240px">
                            </asp:DropDownList>  
                        </td>
                        <td>                                                  
                        </td>
                        <td>                                                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Tipo de Comprobante&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlTipoComp" runat="server" Width="240px" CssClass="dropdown" >
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>                                                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Concepto&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlConcep" runat="server" Width="240px" CssClass="dropdown" >
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>                                                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Cuenta Contable&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:UpdatePanel ID="upCodCuet" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddlCodCuent" runat="server" CssClass="dropdown" 
                                Width="100px" onselectedindexchanged="ddlCodCuent_SelectedIndexChanged" 
                                AutoPostBack="True" ontextchanged="ddlCodCuent_TextChanged" >
                            </asp:DropDownList>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlNomCuent" runat="server" CssClass="dropdown" 
                                Width="240px" AutoPostBack="True" 
                                onselectedindexchanged="ddlNomCuent_SelectedIndexChanged" >
                            </asp:DropDownList>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                        </td>
                        <td>                                                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Estructura de Detalle&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlEstructura" runat="server" Width="240px" CssClass="dropdown" >
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>                                                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Tipo de Movimiento&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlTipoMov" runat="server" Width="240px" CssClass="dropdown" >
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="1">Débito</asp:ListItem>
                                <asp:ListItem Value="2">Crédito</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>                                                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            <asp:Button ID="btnAceptarProceso" runat="server" Text="Aceptar" 
                                onclick="btnAceptarProceso_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>
    
    <script language="javascript" type="text/javascript">
        function mpeSeleccionOnOk() {
        }
        function mpeSeleccionOnCancel() {
        }    
    </script>

</asp:Content>
