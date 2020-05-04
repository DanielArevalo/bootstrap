<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Ahorros a la Vista :." %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTipoMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvAhorros" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <br />
            <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align:left" colspan="3">
                        <strong>Datos Principales</strong>&nbsp;&nbsp;
                        <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Código<br/>
                        <asp:TextBox ID="txtCodLineaAhorro" runat="server" CssClass="textbox" Width="90px" />
                        <asp:RequiredFieldValidator ID="rfvCodLinea" runat="server" ErrorMessage="Debe ingresar el código de la línea" 
                            ControlToValidate="txtCodLineaAhorro" Display="Dynamic" 
                            ValidationGroup="vgGuardar" Font-Size="X-Small" 
                            style="color: Red; font-size: xx-small;">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                    <td style="text-align:left">
                        Nombre<br/>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="400px" />
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción de la línea" 
                            ControlToValidate="txtDescripcion" Display="Dynamic" 
                            ValidationGroup="vgGuardar" Font-Size="X-Small" 
                            style="color: Red; font-size: xx-small;">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                    <td style="text-align:left">
                        Estado:<br />
                        <asp:CheckBox ID="cbEstado" runat="server" Text="Activa" Width="80px" />
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                    <td style="text-align:left">
                        Moneda<br />
                        <ctl:ddlMoneda ID="ddlMoneda" runat="server" Width="100px" />
                    </td>
                    <td style="text-align:left">
                        <asp:CheckBox ID="chkDebitoAutomatico" runat="server" Style="font-size: x-small" Text="Deb. Automático" />
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                    <td style="text-align:left">
                        Prioridad<br />
                        <asp:TextBox ID="txtPrioridad" runat="server" CssClass="textbox" Width="50px" MaxLength="4"/>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtPrioridad" ValidChars="" />
                    </td>
                </tr>
            </table>
            <br />
            <ajaxToolkit:TabContainer runat="server" ID="tcLineaAhorro" 
                OnClientActiveTabChanged="ActiveTabChanged" ActiveTabIndex="1" Width="99%" 
                style="text-align:left; margin-right: 5px" CssClass="CustomTabStyle" >
                <ajaxToolkit:TabPanel runat="server" ID="tpGeneral" HeaderText="Tablas" >
                    <HeaderTemplate>Datos Generales</HeaderTemplate>
                    <ContentTemplate>
                        <table id="tbGeneral" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align:left;" colspan="7">
                                    <strong>Requisitos</strong>
                                </td>                                
                            </tr>
                            <tr>
                                <td style="text-align:left;">
                                    Valor Apertura<br />
                                    <uc1:decimales ID="txtValorApertura" runat="server" 
                                        style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                        Habilitado="True" Enabled="True" Width_="80" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    Saldo Mínimo<br />
                                    <uc1:decimales ID="txtSaldoMinimo" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />                                    
                                </td>
                                <td style="">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    Movimiento Mínimo<br />
                                    <uc1:decimales ID="txtMovimientoMinimo" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />                                    
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left;">
                                    Vr.Máximo Retiros x Día<br />
                                    <uc1:decimales ID="txtMaximoRetiroDiario" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    Vr.Retiro Máx. Efectivo<br />
                                    <uc1:decimales ID="txtRetiroMaxEfectivo" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    Vr.Retiro Mínimo Cheque<br />
                                    <uc1:decimales ID="txtRetiroMinCheque" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align:left;" colspan="7">
                                    <hr />
                                    <strong>Libreta</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left;">
                                    <br />
                                    <asp:CheckBox ID="cbRequiereLibreta" runat="server" Width="150px" 
                                        Text="Requiere Libreta" AutoPostBack="True" 
                                        oncheckedchanged="cbRequiereLibreta_CheckedChanged" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    Valor Libreta<br />
                                    <uc1:decimales ID="txtValorLibreta" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />
                                </td>                                
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    Número de Desprendibles<br />
                                    <asp:TextBox ID="txtNumDesprendiblesLib" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    <asp:CheckBox ID="cbCobraPrimeraLibreta" runat="server" Text="Cobra Primera Libreta" /><br />
                                    <asp:CheckBox ID="cbCobraPerdidaLibreta" runat="server" Text="Cobra Perdida de Libreta" />
                                </td>                                
                            </tr>
                            <tr>                                                                
                                <td style="text-align:left;" colspan="7">
                                    <hr />
                                    <strong>Canje</strong>
                                </td>                                                        
                            </tr>  
                            <tr>
                                <td style="text-align:left;">
                                    <asp:CheckBox ID="cbCanjeAutomatico" runat="server" Text="Canje Automático" 
                                        AutoPostBack="True" oncheckedchanged="cbCanjeAutomatico_CheckedChanged" />
                                    <br />                                    
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    Días para Canje<br /> 
                                    <asp:TextBox ID="txtDiasCanje" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>   
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>                                                                
                                <td style="text-align:left;" colspan="7">
                                    <hr />
                                    <strong>Inactivación</strong>
                                </td>                                                        
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    <asp:CheckBox ID="cbInactivacionAutomatica" runat="server" 
                                        Text="Inactivación Automática" AutoPostBack="True" 
                                        oncheckedchanged="cbInactivacionAutomatica_CheckedChanged" />
                                    <br />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    Días Inactivación
                                    <br />
                                    <asp:TextBox ID="txtDiasInactiva" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>                                                                
                                <td style="text-align:left;" colspan="7">
                                    <hr />
                                    <strong>Cierre</strong>
                                </td>                                                        
                            </tr>
                            <tr>
                                <td style="text-align:left;">
                                     <br />
                                    <asp:CheckBox ID="cbCobroCierre" runat="server" Text="Se cobra Cierre" 
                                         AutoPostBack="True" oncheckedchanged="cbCobroCierre_CheckedChanged" />                                    
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    Vr Cobro por Cierre<br />
                                    <uc1:decimales ID="txtCierreValor" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left;">
                                    Días para Cobro Cierre<br />
                                    <asp:TextBox ID="txtCierreDias" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                                <td style="text-align:left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="Liquidación Intereses" >
                    <HeaderTemplate>Liquidación Intereses</HeaderTemplate>
                    <ContentTemplate>
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                            <tr>                            
                                <td style="text-align:left" colspan="8">
                                    <strong>Parámetros de Liquidación de Intereses:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    Saldo Base de Cálculo<br />
                                    <asp:DropDownList ID="ddlTipoSaldoInt" runat="server" CssClass="textbox" Width="200px">
                                        <asp:ListItem Value="1" Text="Saldo Mínimo" />
                                        <asp:ListItem Value="2" Text="Saldo Promedio" />
                                        <asp:ListItem Value="3" Text="Saldo Final" />
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left">
                                    &nbsp;&nbsp;&nbsp;                      
                                </td>
                                <td style="text-align:left">
                                    Periodicidad Liquidación<br />
                                    <ctl:ddlPeriodicidad ID="ddlPeriodicidad" runat="server" Requerido="False" AutoPostBack="False" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;&nbsp;&nbsp;                      
                                </td>
                                <td style="text-align:left">
                                    Días de Gracia<br />
                                    <asp:TextBox ID="txtDiasGracia" runat="server" CssClass="textbox" Width="90px" />
                                </td>
                                <td style="text-align:left">
                                    &nbsp;&nbsp;&nbsp;                      
                                </td>
                                <td style="text-align:left">
                                    <br />
                                    <asp:CheckBox ID="cbRealizaProvision" runat="server" Text="Realiza Causación" Width="150px" />
                                </td>    
                                <td style="text-align:left">
                                  Saldo Mínimo Liquidación<br />
                                    <asp:TextBox ID="txtsaldominimoliqu" runat="server" CssClass="textbox" Width="90px" />                    
                                </td>                            
                            </tr>
                            <tr>                            
                                <td style="text-align:left" colspan="8">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    Interés Diario Cobro Retención<br />
                                    <uc1:decimales ID="txtInteresDia" runat="server" Enabled="True" 
                                        Habilitado="True" style="font-size:xx-small; text-align:right" 
                                        TipoLetra="XX-Small" Width_="80" />
                                </td>  
                                  
                                <td style="text-align:left">
                                    &nbsp;&nbsp;&nbsp;                      
                                </td>     
                                <td style="text-align:left" colspan="5">
                                    <br />
                                    <asp:CheckBox ID="cbInteresPorCuenta" runat="server" Text="Manejar Tasa de Interés por Cada Cuenta" Width="100%" />
                                </td>  
                                <td style="text-align:left">
                                    <br />
                                    <asp:CheckBox ID="cbCobraRetencion" runat="server" Text="Cobra Retención por Cada Cuenta" Width="100%" />
                                </td>                                    
                                <td style="text-align:left">
                                    &nbsp;&nbsp;&nbsp;                      
                                </td>     
                            </tr>
                            <tr>                            
                                <td style="text-align:left" colspan="8">
                                    <hr />
                                </td>
                            </tr>
                            <tr>                            
                                <td style="text-align:left" colspan="8">
                                    <strong>Tasa de Intereses:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" colspan="8">
                                    <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
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
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Datos Grabados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
