<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register src="../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register src="../../../../General/Controles/ctlTasa.ascx" tagname="ctltasa" tagprefix="ctl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="panelDatos" runat="server">
            <table style="width: 700px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 700px" colspan="5">
                        Número<br />
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Linea<br />
                        <asp:TextBox ID="txtNomLinea" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Numero de Crédito<br />
                        <asp:TextBox ID="txtNumCredito" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Cupo Total<br />
                        <uc1:decimales ID="txtCupoTotal" runat="server" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Cupó Disponible<br />
                        <uc1:decimales ID="txtCupoDisp" runat="server" />                    
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Width="220px" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Fecha Aprobación<br />
                        <ucFecha:fecha ID="txtFechaApro" runat="server" CssClass="textbox" />                        
                    </td>
                    <td style="text-align: left; width: 140px">
                        Fec. Ultimo Avance<br />
                        <ucFecha:fecha ID="txtFechaUlt" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Forma de Pago<br />
                        <asp:TextBox ID="txtnumFormPago" runat="server" CssClass="textbox" Width="30px" Visible="false"/>
                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: left">
                        <strong>Solicitante</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
                       
                    </td>
                    <td style="text-align: left; width: 140px">
                        <br />
                        <asp:TextBox ID="txtCuotasPagas" runat="server" CssClass="textbox" 
                            Visible="False" />
                        <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" FilterType="Numbers, Custom" 
                            TargetControlID="txtCuotasPagas" ValidChars=",." />
                    </td>
                    <td style="text-align: left; width: 140px">
                        <uc2:decimales ID="txtValor_cuota" runat="server" Enabled="false" 
                            Visible="False" />
                    </td>
                </tr>
                 <tr>
                    <td colspan="5">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                </table>
                </asp:Panel>

               <table style="width: 700px; text-align: center; height: 55px;" 
                cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 140px">
                        Fec. Solicitud<br />
                        <ucFecha:fecha ID="txtFechaSoli" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        Valor Solicitado<br />
                        <uc1:decimales ID="txtValorSoli" runat="server" />
                    </td>
                    <td style="text-align: left; width: 280px">
                        <br />
                    </td>
                    <td style="text-align: left; width: 130px; margin-left: 40px;">
                        Plazo
                        <br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="43%" Enabled ="false"/>
                        &nbsp;</td>
                    <td style="text-align: left; width: 130px">
                         <asp:Panel ID="panel1" runat="server">
                        <asp:Accordion ID="AccordionNomina" runat="server" ContentCssClass="accordionContenido" FadeTransitions="false" FramesPerSecond="50" HeaderCssClass="accordionCabecera" Height="240px" SelectedIndex="-1" Style="margin-right: 6px; font-size: xx-small;" SuppressHeaderPostbacks="true" TransitionDuration="200" Width="332px">
                            <Panes>
                                <asp:AccordionPane ID="AccordionPane4" runat="server" ContentCssClass="" HeaderCssClass="">
                                    <Header>
                                        <center>
                                            TASA DE INTERES</center>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td class="tdD" colspan="4">
                                                    <asp:Panel ID="panelTasa" runat="server">
                                                        <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" Enabled="true"/>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <caption>
                                                <hr style="text-align: left" />
                                            </caption>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                            </Panes>
                        </asp:Accordion>
                              </asp:Panel>
                    </td>
                    <td style="text-align: left; width: 130px">&nbsp;</td>
                </tr>
            </table>
            <asp:Panel ID="panelFormaPago" runat="server" Visible="false">
            <table style="width: 700px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align:left;width:250px">
                        <asp:Label ID="lblEntidadGiro" runat="server" Text="Entidad. de Giro"></asp:Label><br />
                        <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox" Width="90%" AutoPostBack="True" 
                            onselectedindexchanged="ddlEntidad_giro_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:left;" colspan="2">
                        <asp:Label ID="lblCuenta_Giro" runat="server" Text="Cuenta de Giro :"></asp:Label><br />
                        <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </td>
               </tr>
               <tr>
                    <td style="text-align:left;width:250px">
                        <asp:Label ID="lblNum_cuenta" runat="server" Text="Num. Cuenta :"></asp:Label><br />
                        <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="155px" />
                    </td>
                    <td style="text-align:left;width:250px">
                        <asp:Label ID="lblEntidad" runat="server" Text="Entidad :"></asp:Label><br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:left;width:200px">
                        <asp:Label ID="lblTipo_Cuenta" runat="server" Text="Tipo de Cuenta :"></asp:Label><br />
                        <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox" Width="191px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </asp:Panel>
            <table style="width: 700px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                 <td style="text-align: left; width: 140px">
                       Fec. Aprobación<br />
                        <ucFecha:fecha ID="txtFechaAproSoli" runat="server" CssClass="textbox" />
                    </td>
                     <td style="text-align: left; width: 150px">
                         Valor Aprobado<br />
                        <uc1:decimales ID="txtValorApro" runat="server" />
                    </td>
                    <td style="text-align: left; width: 280px">
                        Observaciones<br />
                        <asp:TextBox ID="txtObserva" runat="server" CssClass="textbox" Width="90%" TextMode="MultiLine" />
                    </td>   
                    <td style="text-align: left; width: 130px">
                        <asp:CheckBox ID="chkAprobar" runat="server" Text="Aprobar" AutoPostBack="True" 
                            oncheckedchanged="chkAprobar_CheckedChanged" /> <br/>
                        <asp:CheckBox ID="chkNegar" runat="server" Text="Negar" AutoPostBack="True" 
                            oncheckedchanged="chkNegar_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left; ">
                        <span style="font-size: small">
                        <asp:GridView ID="gvDeducciones" runat="server" AllowPaging="False" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" DataKeyNames="cod_atr" ForeColor="Black" 
                            ShowFooter="True" Style="font-size: x-small" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Cod.Atr">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodAtr" runat="server" CssClass="textbox" Enabled="false" 
                                            Text='<%# Bind("cod_atr") %>' Width="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nom_atr" HeaderStyle-Width="120px" 
                                    HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Tipo de Descuento">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTipoDescuento" runat="server" CssClass="textbox" 
                                            DataSource="<%#ListarTiposdeDecuento() %>" DataTextField="descripcion" 
                                            DataValueField="codigo" Enabled="false" 
                                            SelectedValue='<%# Bind("tipo_descuento") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Liquidación">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="textbox" 
                                            DataTextField="descripcion" DataValueField="codigo" Enabled="false" 
                                            Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Forma de Descuento">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlFormaDescuento" runat="server" CssClass="textbox" 
                                            DataSource="<%#ListaCreditoFormadeDescuento() %>" DataTextField="descripcion" 
                                            DataValueField="codigo" Enabled="false" 
                                            SelectedValue='<%# Bind("forma_descuento") %>' Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtvalor" runat="server" Style="font-size: x-small" 
                                            Text='<%# Bind("val_atr") %>' Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Num.Cuotas">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtnumerocuotas" runat="server" Enabled="false" 
                                            Style="font-size: x-small" Text='<%# Bind("numero_cuotas") %>' Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cobra Mora">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbCobraMora" runat="server" 
                                            Checked='<%# Convert.ToBoolean(Eval("cobra_mora")) %>' Enabled="false" 
                                            Text=" " />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Impuestos">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        </span>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
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
                        <td style="text-align: center; font-size: large; color: Red">
                            La aprobación&nbsp; de avance fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
