<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register src="../../../../General/Controles/ctlTasa.ascx" tagname="ctltasa" tagprefix="ctl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="panelDatos" runat="server">
            <table style="width: 730px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left" colspan="3">
                        Fecha Desembolso<br />
                        <ucFecha:fecha ID="txtFechaDesem" runat="server" CssClass="textbox" Enabled="false" /> 
                        <span style="color: #FF0000">*</span>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="LblMensaje" runat="server" Style="color: #FF0000; font-weight: 700;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Número<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Linea<br />
                        <asp:TextBox ID="txtNomLinea" runat="server" CssClass="textbox" Width="90%" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Numero de Crédito<br />
                        <asp:TextBox ID="txtNumCredito" runat="server" CssClass="textbox" Width="90%" Enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 170px">
                        Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Width="90%" Enabled="false"/>
                    </td>                  
                </tr>
                <tr>
                    <td colspan="5" style="text-align: left">
                        <br />
                        <strong>Solicitante</strong>
                        <asp:TextBox ID="txtcodLineacredito" runat="server" CssClass="textbox" 
                            Enabled="false" Visible="False" Width="1px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" Enabled="false"/>
                    </td>
                    <td style="text-align: left" colspan="4">
                        Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" Enabled="false"/>                    
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <hr style="width: 100%" />
                        <br />
                    </td>
                </tr>
                </table>
               </asp:Panel>
                <table style="width: 700px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 140px">
                        Fec. Solicitud<br />
                        <ucFecha:fecha ID="txtFechaSoli" runat="server" CssClass="textbox" Enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Valor Solicitado<br />
                        <uc1:decimales ID="txtValorSoli" runat="server" Enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 140px" colspan="2">
                        Fec. Aprobación<br />
                        <ucFecha:fecha ID="txtFechaApro" runat="server" CssClass="textbox" Enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 75px">
                        Valor Aprobado<br />
                        <uc1:decimales ID="txtValorApro" runat="server" Enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 140px">Plazo
                        <br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" Width="43%" />
                    </td>
               </tr>
               <tr>
                    <td style="text-align: left; width: 140px">
                        Forma Pago<br />
                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" Enabled="false"/>
                    </td>
                   <td style="text-align: left;" colspan="2">
                       Observaciones<br />
                       <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="87%" TextMode="MultiLine" Enabled="false" Height="23px"/>
                   </td>                    
                    <td colspan="3" style="text-align: left;">
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
                                                            <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Enabled="true" Width="400px" />
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
                </tr>
                    <tr>
                        <td style="text-align: left; width: 140px">
                            &nbsp;</td>
                        <td colspan="5" style="text-align: left;">
                            &nbsp;</td>
                    </tr>
                <tr>
                    <td colspan="6" style="text-align: left">
                        <hr style="width:100%"/>
                    </td>
                </tr>           
                <tr>
                    <td colspan="6" style="text-align: left">
                     <asp:Panel ID="Panel5" runat="server" Width="100%">
                        <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
                <ContentTemplate>
                    <table style="width: 583px;">
                        <tr>
                            <td style="width: 179px; text-align: left">
                                <strong>Forma de Desembolso</strong>
                            </td>
                            <td colspan="3" style="width: 404px">
                                <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" Style="margin-left: 0px; text-align: left"
                                    Width="84%" Height="28px" CssClass="textbox" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownFormaDesembolso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <asp:Panel runat="server" ID="pnlCuentaAhorroVista">
                        <table style="width: 48%">
                            <tr>
                                <td style="text-align: left; width: 110px">
                                    <asp:Label ID="lblCuentaAhorroVista" runat="server" Text="Numero Cuenta" Style="text-align: left"></asp:Label>
                                </td>
                                <td style="width: 151px; text-align: left;">
                                    <asp:DropDownList ID="ddlCuentaAhorroVista" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="102%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlCuentasBancarias">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 179px; text-align: left">
                                    <asp:Label ID="lblEntidadOrigen" runat="server" Text="Banco de donde se Gira" Style="text-align: left"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlEntidadOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 179px; text-align: left; height: 25px;">
                                    <asp:Label ID="lblNumCuentaOrigen" runat="server" Text="Cuenta de donde se Gira"
                                        Style="text-align: left"></asp:Label>
                                </td>
                                <td colspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlCuentaOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px"></td>
                            </tr>
                            <tr>
                                <td style="width: 179px; text-align: left">
                                    <asp:Label ID="lblEntidad" runat="server" Text="Entidad" Style="text-align: left"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <%--<asp:DropDownList ID="DropDownEntidad" AutoPostBack="true" OnSelectedIndexChanged="DropDownEntidad_SelectedIndexChanged" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox">
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="DropDownEntidad" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 179px; text-align: left">
                                    <asp:Label ID="lblNumCuenta" runat="server" Text="Numero de Cuenta" Style="text-align: left"></asp:Label>
                                </td>
                                <td style="width: 143px">
                                    <%--<asp:DropDownList ID="ddlNumeroCuenta"  runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="102%" CssClass="textbox" OnSelectedIndexChanged="ddlNumeroCuenta_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                    <asp:TextBox runat="server" ID="txtNumeroCuenta" CssClass="textbox" />
                                </td>
                                <td style="width: 110px; text-align: left">
                                    <asp:Label ID="lblTipoCuenta" runat="server" Text="Tipo Cuenta" Style="text-align: left"></asp:Label>
                                </td>
                                <td style="width: 151px">
                                    <asp:DropDownList ID="ddlTipo_cuenta" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="102%" CssClass="textbox">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
                     </asp:Panel>
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
                            La solicitud de avance fue
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
