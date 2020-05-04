<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register src="../../../../General/Controles/ctlTasa.ascx" tagname="ctltasa" tagprefix="ctl" %>
<script runat="server">

   
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="panelDatos" runat="server">
            <table style="width: 730px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 140px;">
                        Número<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="false" Width="100px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 280px">Linea<br />
                        <asp:TextBox ID="txtNomLinea" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">Numero de Crédito<br />
                        <asp:TextBox ID="txtNumCredito" runat="server" CssClass="textbox" Enabled="false" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 170px">Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="false" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; " colspan="4">
                        <br />
                        <strong>Solicitante</strong>
                        <asp:TextBox ID="txtcodLineacredito" runat="server" CssClass="textbox" Width="1px" Enabled="false" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px;">
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                            Enabled="false" Width="90%" />
                    </td>
                    <td colspan="3" style="text-align: left">Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
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
                        <td style="text-align: left; " colspan="6">
                            <asp:HiddenField ID="hiddenOperacionAvance" runat="server" />
                            <table style="width: 100%; text-align: left">
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <hr style="width: 100%" />
                                        <strong>NEGAR AVANCES</strong> 
                                        <hr style="width: 100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">No. Avance: </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtNumavance" runat="server" CssClass="textbox" Enabled="false" Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito5" runat="server" ControlToValidate="txtNumavance" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgNegar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Motivo de negación: </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlNegar" runat="server" CssClass="dropdown" Width="150px">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvNegar" runat="server" ControlToValidate="ddlNegar" Display="Dynamic" ErrorMessage="Seleccione un motivo de negacion" ForeColor="Red" Operator="GreaterThan" SetFocusOnError="true" Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" ValidationGroup="vgNegar" ValueToCompare="0">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Observación: </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtObs" runat="server" CssClass="textbox" MaxLength="250" Width="145px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgNegar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpNegar" runat="server" CssClass="btn8" OnClick="btnAcpNegar_Click" Text="Aceptar" ValidationGroup="vgNegar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCncNegar" runat="server" CssClass="btn8" OnClick="btnCncNegar_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                </tr>           
                <tr>
                    <td colspan="6" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
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
                            El&nbsp; &nbsp;<asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.
                            <br />
                            <asp:Button ID="btnContinuar" runat="server" OnClick="btnContinuar_Click" Text="Continuar" />
                            <br />
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
