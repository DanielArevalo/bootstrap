<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<%@ Register src="../../../../General/Controles/ctlPeriodicidad.ascx" tagname="ddlperiodicidad" tagprefix="ctl" %>
<%@ Register src="../../../../General/Controles/decimalesGridRow.ascx" tagname="decimalesgridrow" tagprefix="uc1" %>
<%@ Register src="../../../../General/Controles/ctlTasa.ascx" tagname="ctltasa" tagprefix="ctl" %>
<%@ Register src="../../../../General/Controles/fechaeditable.ascx" tagname="fecha" tagprefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="panelDatos" runat="server">
                <table style="width: 700px; text-align: center" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="text-align: left; width: 700px" colspan="6">
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
                            <asp:TextBox ID="txtNumCredito" runat="server" CssClass="textbox" Width="100px" />
                        </td>
                        <td style="text-align: left; width: 150px">
                            Cupo Total<br />
                            <uc1:decimales ID="txtCupoTotal" runat="server" />
                        </td>
                        <td style="text-align: left; width: 150px" colspan="2">
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
                            <asp:TextBox ID="txtfechaDesem" runat="server" CssClass="textbox" Width="100px" Visible="false"/>
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
                        <td style="text-align: left; width: 140px"><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: left">
                            <br />
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
                            <asp:TextBox ID="txtCodCliente" runat="server" CssClass="textbox" 
                                Visible="False" Width="90%" />
                        </td>
                        <td style="text-align: left; width: 140px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 1000px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 140px">
                        Fec. Solicitud<br />
                        <ucFecha:fecha ID="txtFechaSoli" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Valor Solicitado<br />
                        <uc1:decimales ID="txtValorSoli" runat="server" />
                    </td>                    
                    <td style="text-align: left; width: 140px">Plazo
                        <br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="43%" />
                    </td>
                    <td style="text-align: left; width: 140px"> 
                        <asp:Panel ID="panel1" runat="server" Enabled="false">                       
                        <asp:Accordion ID="AccordionNomina" runat="server" ContentCssClass="accordionContenido" FadeTransitions="false" FramesPerSecond="50" HeaderCssClass="accordionCabecera" Height="231px" SelectedIndex="-1" Style="margin-right: 6px; font-size: xx-small;" SuppressHeaderPostbacks="true" TransitionDuration="200" Width="355px">
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
                                                        <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
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
                  <asp:Panel ID="panel2" visible="false"  runat="server">              
                <tr>
                  
                    <td style="text-align: left; width: 140px" colspan="2">
                        Forma Desembolso<br />
                        <asp:DropDownList ID="ddlForma_Desem" visible="false" runat="server" CssClass="textbox" Width="191px"
                            OnSelectedIndexChanged="ddlForma_Desem_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>                                     
                    <td style="text-align: left; width: 140px">
                        <br />
                    </td>
                    <td style="text-align: left; width: 140px">                        
                    </td>
                </tr>
                   </asp:Panel>
            </table>
            
            <asp:Panel ID="panelFormaPago" visible="false"  runat="server">
                <table style="width: 700px; text-align: left" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblEntidadGiro" runat="server" Text="Entidad. de Giro"></asp:Label><br />
                            <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox"
                                Width="90%" AutoPostBack="True" 
                                onselectedindexchanged="ddlEntidad_giro_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 450px" colspan="2">
                            <asp:Label ID="lblCuenta_Giro" runat="server" Text="Cuenta de Giro :"></asp:Label><br />
                            <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>                    
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblNum_cuenta" runat="server" Text="Num. Cuenta :"></asp:Label><br />
                            <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="155px" />
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblEntidad" runat="server" Text="Entidad :"></asp:Label><br />
                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblTipo_Cuenta" runat="server" Text="Tipo de Cuenta :"></asp:Label><br />
                            <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox" Width="191px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 700px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="3">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left; width: 420px">
                        Observaciones<br />
                        <asp:TextBox ID="txtObserva" runat="server" CssClass="textbox" Width="90%" TextMode="MultiLine" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 140px">
                        &nbsp;
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
