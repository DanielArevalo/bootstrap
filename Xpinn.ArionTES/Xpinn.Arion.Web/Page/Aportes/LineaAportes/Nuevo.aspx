<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" 
    AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="../../../General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../../General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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

    <asp:MultiView ID="mvAportes" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 15px; text-align: left;" colspan="6">
                        <strong>Datos de la Línea de Aporte</strong>
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px; text-align: left;">Cod Linea
                        <br />
                        <asp:TextBox ID="TxtCodLineaApo" runat="server" CssClass="textbox" Width="76px" AutoPostBack="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvcodLineaAporte" runat="server"
                            ControlToValidate="TxtCodLineaApo" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small;" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="height: 15px; text-align: left;">&nbsp;&nbsp;
                    </td>
                    <td style="height: 15px; text-align: left;" colspan="2">Nombre de Línea                    
                        <br />
                        <asp:TextBox ID="TxtLinea" runat="server" CssClass="textbox"
                            Width="331px" AutoPostBack="True" OnTextChanged="TxtLinea_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLineaAporte"
                            runat="server" ControlToValidate="TxtLinea" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small;" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="height: 15px; text-align: left;">&nbsp;&nbsp;
                    </td>
                    <td style="height: 15px; text-align: left;">Tipo de Aporte
                        <asp:RequiredFieldValidator ID="rfvTipoAporte" runat="server"
                            ControlToValidate="DdlTipoProducto" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red"
                            InitialValue="Seleccione un item" SetFocusOnError="True"
                            Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                        <br />
                        <asp:DropDownList ID="DdlTipoProducto" runat="server" AutoPostBack="True"
                            Width="190px" CssClass="textbox">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="height: 15px; text-align: left;">Estado<br />
                        <asp:CheckBox ID="Ckhactiva" runat="server" AutoPostBack="True"
                            Style="font-size: x-small; text-align: left;"
                            Text="Activa" OnCheckedChanged="Ckhactiva_CheckedChanged" />
                        <asp:CheckBox ID="Ckhinactiva" runat="server" AutoPostBack="True"
                            Style="font-size: x-small; text-align: left;" Text="Inactiva"
                            OnCheckedChanged="Ckhinactiva_CheckedChanged" />
                        <asp:CheckBox ID="Ckhcerrada" runat="server" AutoPostBack="True"
                            Style="font-size: x-small; text-align: left;" Text="Cerrada"
                            OnCheckedChanged="Ckhcerrada_CheckedChanged" />
                    </td>
                    <td style="height: 15px; text-align: left;">&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <ajaxToolkit:TabContainer runat="server" ID="tcLineaAhorro"
                OnClientActiveTabChanged="ActiveTabChanged" ActiveTabIndex="0" Width="99%"
                Style="text-align: left; margin-right: 5px; padding: 10px;" CssClass="CustomTabStyle">
                <ajaxToolkit:TabPanel runat="server" ID="tpGeneral" HeaderText="Tablas">
                    <HeaderTemplate>Datos Generales</HeaderTemplate>
                    <ContentTemplate>
                        <table id="tbGeneral" border="0" cellpadding="0" cellspacing="0" style="padding:10px; margin:10px">
                            <tr>
                                <td style="height: 15px; text-align: left;" colspan="7"><strong>Condiciones</strong><br /><br /></td>
                            </tr>
                            <tr>
                                <asp:Panel runat="server" ID="pnlRetiros" Visible="true">
                                    <td style="height: 15px; text-align: left;">Retiro Mínimo<br />
                                        <uc2:decimales ID="Txtretirominimo" runat="server" /></td>
                                    <td style="height: 15px; text-align: left;">Retiro Máximo<br />
                                        <uc2:decimales ID="Txtretiromaximo" runat="server" /></td>
                                    <td style="height: 15px; text-align: left;">% Max. a Retirar <br />
                                        <asp:TextBox ID="txtPorcentajeRetiro" runat="server" CssClass="textbox" Width="130px"></asp:TextBox></td>
                                    <td style="height: 15px; text-align: left;"><br />
                                        <asp:CheckBox ID="chbPermiteRetiros" runat="server" AutoPostBack="True" Style="font-size: x-small; text-align: left;" Text="Permite Retiros" /><br /></br></td>                                
                                </asp:Panel>
                            </tr>
                            <tr>
                                <td style="height: 15px; text-align: left;">Saldo Mínimo Cuenta<br />
                                    <uc2:decimales ID="TxtSaldoMinimo" runat="server" /></td>
                                <td style="height: 15px; text-align: left;"><br />
                                    <asp:CheckBox ID="chbPermiteTraslados" runat="server" AutoPostBack="True" Style="font-size: x-small; text-align: left;" Text="Permite Traslados" /></td>
                                <td style="height: 15px; text-align: left;"><br />
                                    <asp:CheckBox ID="chbPermitePagoProductos" runat="server" AutoPostBack="True"
                                        Style="font-size: x-small; text-align: left;" Text="Permite Pago a Productos" /></td>                                
                                <td style="height: 15px; text-align: left;"><br />
                                    <asp:CheckBox ID="ChkBeneficiarios" runat="server" AutoPostBack="True" Style="font-size: x-small; text-align: left;" Text="Beneficiarios" /></td>
                                <td style="height: 15px; text-align: left;"><br />
                                    <asp:CheckBox ID="ChkAlerta" runat="server" Style="font-size: x-small; text-align: left;" Text="No Mostrar Alerta" /></td>
                            </tr>
                            <tr>
                                <td style="height: 15px; text-align: left;" colspan="7"><br /><br /><strong>Parámetros para Cruce de Cuentas</strong><br /><br /></td>
                            </tr>
                            <tr>
                                <td style="height: 15px; text-align: left;">Cruzar<br />
                                    <asp:CheckBox ID="ChkcruceSI" runat="server" AutoPostBack="True" Style="font-size: x-small; text-align: left;" Text="SI"
                                        OnCheckedChanged="ChkcruceSI_CheckedChanged" />
                                    <asp:CheckBox ID="ChkcruceNO" runat="server" AutoPostBack="True"
                                        Style="font-size: x-small; text-align: left;" Text="NO"
                                        OnCheckedChanged="ChkcruceNO_CheckedChanged" />
                                    <asp:CheckBox ID="ChkcruceCOBRAR" runat="server" AutoPostBack="True"
                                        Style="font-size: x-small; text-align: left;" Text="NO y Cobrar"
                                        OnCheckedChanged="ChkcruceCOBRAR_CheckedChanged" />
                                </td>
                                <td style="height: 15px; text-align: left;">% Cruce <br />
                                    <asp:TextBox ID="txtCruce" runat="server" CssClass="textbox" Width="130px">
                                    </asp:TextBox>
                                </td>

                                <asp:Panel runat="server" ID="pnlCierre">
                                    <td style="text-align: left;">Días Cobro Cierre <br />
                                        <asp:TextBox ID="txtDiasCierre" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td style="height: 15px; text-align: left;">&nbsp;&nbsp; </td>
                                    <td style="text-align: left; height: 15px;">Valor por Cierre<br />
                                        <uc2:decimales ID="txtValorCierre" runat="server" />
                                    </td>
                                </asp:Panel>
                            </tr>
                            <caption>-- 
                                <tr>
                                    <td colspan="6" style="height: 15px; text-align: left;"><br /><br /><strong>Parámetros de la Cuota</strong><br /><br /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Tipo De Cuota
                                        <asp:RequiredFieldValidator ID="rfvCuota" runat="server"
                                         ControlToValidate="DdlTipoCuota" Display="Dynamic"
                                         ErrorMessage="Campo Requerido" ForeColor="Red"
                                         InitialValue="Seleccione un item" SetFocusOnError="True"
                                         Style="font-size: xx-small" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator><br />
                                        <asp:DropDownList ID="DdlTipoCuota" runat="server" AutoPostBack="True"
                                            CssClass="textbox" OnSelectedIndexChanged="DdlTipoCuota_SelectedIndexChanged"
                                            Width="190px">
                                            <asp:ListItem Value="1">CUOTA FIJA</asp:ListItem>
                                            <asp:ListItem Value="2">RANGOS DE SALARIOS</asp:ListItem>
                                            <asp:ListItem Value="3">DISTRIBUCION</asp:ListItem>
                                            <asp:ListItem Value="4">% DEL SUELDO</asp:ListItem>
                                            <asp:ListItem Value="5">% SMLMV</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 15px; text-align: left;">
                                        <asp:Label ID="lblCuotaMinima" runat="server" Text="Valor Cuota Mínima" Width="166px"></asp:Label><br />
                                        <asp:TextBox ID="Txtcuotaminima" runat="server" CssClass="textbox" Width="150px"/>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="Txtcuotaminima" Enabled="True" />                                                                                                                                                                                                                                                                                                                                               
                                            </td><td style="height: 15px; text-align: left;"><asp:Label ID="lblCuotaMaxima" runat="server" Text="Valor Cuota Máxima" Width="166px">                                            
                                        </asp:Label><br /><asp:TextBox ID="Txtcuotamaxima" runat="server" CssClass="textbox" Width="150px"/>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="Txtcuotamaxima" Enabled="True" />
                                                 </td><td style="height: 15px; text-align: left;"></td><td style="height: 15px; text-align: left;">&nbsp;</td><td style="height: 15px; text-align: left;"></td></tr><tr><td style="height: 15px; text-align: left;"><br /><br /><strong>Parámetros NIIF</strong><br /><br /></td><td></td><td style="height: 15px; text-align: left;" colspan="2"><strong>Parámetros de Clasificación</strong> </td></tr><tr><td style="height: 15px; text-align: left;">Capital Mínimo Irreductible<br /> <uc2:decimales ID="txtCapMinIrreduc" runat="server" /></td><td style="text-align: left; height: 15px;">&nbsp; </td><td style="text-align: left; height: 15px;">Clasificación <br /><asp:DropDownList ID="ddlClasificacion" runat="server" CssClass="textbox"></asp:DropDownList></td><td style="height: 15px; text-align: left;"></td><td style="height: 15px; text-align: left;">Prioridad<br /> <asp:TextBox ID="txtPrioridad" runat="server" CssClass="textbox" MaxLength="10" Width="60px" /></td><td style="height: 15px; text-align: left;"></td></tr></caption></table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="TabPanelLiquid" HeaderText="Tablas">
                    <HeaderTemplate>Liquidación Intereses</HeaderTemplate>
                    <ContentTemplate><table id="Table1" border="0" cellpadding="0" cellspacing="0"><tr><td style="text-align: left;"><asp:CheckBox ID="chkDistribuye" runat="server" AutoPostBack="True"
                                        Style="font-size: 7pt; text-align: left;" Text="Distribuye" />% Distribución&#160;&#160;&#160;&#160; <br /><asp:TextBox ID="txtDistribucion" runat="server" CssClass="textbox" Width="150px"></asp:TextBox></td><td colspan="4" style="text-align: left;">Linea de Liquidación / Revalorización<br /> <asp:DropDownList ID="ddlLineaRevaloriza" runat="server" CssClass="textbox" Width="240px"></asp:DropDownList></td></tr><tr><td style="text-align: left" colspan="8"><strong>Parámetros de Liquidación de Intereses:</strong> </td></tr><tr><td style="text-align: left">Saldo Base de Cálculo<br /> <asp:DropDownList ID="ddlTipoSaldoInt" runat="server" CssClass="textbox" Width="200px"><asp:ListItem Value="1" Text="Saldo Mínimo" /><asp:ListItem Value="2" Text="Saldo Promedio" /><asp:ListItem Value="3" Text="Saldo Final" /></asp:DropDownList></td><td colspan="2" style="text-align: left">Periodicidad Liquidación<br /> <ctl:ddlPeriodicidad ID="ddlPeriodicidad" runat="server" Requerido="False" AutoPostBack="False" /></td><td style="text-align: left">&nbsp;&nbsp;&nbsp; </td><td style="text-align: left">Días de Gracia<br /> <asp:TextBox ID="txtDiasGracia" runat="server" CssClass="textbox" Width="90px" /></td><td style="text-align: left">&nbsp;&nbsp;&nbsp; </td><td style="text-align: left"><br /><asp:CheckBox ID="cbRealizaProvision" runat="server" Text="Realiza Causación" Width="150px" /></td><td style="text-align: left">Saldo Mínimo Liquidación<br /> <asp:TextBox ID="txtsaldominimoliqu" runat="server" CssClass="textbox" Width="90px" /></td></tr><tr><td style="text-align: left" colspan="8"><hr /></td></tr><tr><td style="text-align: left; width: 225px;">Interés Diario Cobro Retención<br /> <uc1:decimales ID="txtInteresDia" runat="server" Enabled="True"
                                        Habilitado="True" style="font-size: xx-small; text-align: right"
                                        TipoLetra="XX-Small" Width_="80" /></td><td style="text-align: left; width: 211px;">Forma Pago Intereses <br /><asp:DropDownList ID="ddlformapago" runat="server" CssClass="textbox" Width="200px"><asp:ListItem Value="0" Text="Seleccione un Item" /><asp:ListItem Value="1" Text="A Cuenta" /><asp:ListItem Value="2" Text="Por Nomina" /></asp:DropDownList></td><td style="text-align: left; width: 310px;" colspan="5"><br /><asp:CheckBox ID="cbInteresPorCuenta" runat="server" Text="Manejar Tasa de Interés por Cada Cuenta" Width="100%" /></td><td style="text-align: left"><br /><asp:CheckBox ID="cbCobraRetencion" runat="server" Text="Cobra Retención por Cada Cuenta" Width="100%" /></td><td style="text-align: left">&nbsp;&nbsp;&nbsp; </td></tr><tr><td style="text-align: left" colspan="8"><hr /></td></tr><tr><td style="text-align: left" colspan="8"><strong>Tasa de Intereses:</strong> </td></tr><tr><td style="text-align: left" colspan="8"><ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" /></td></tr></table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>

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
                            <asp:Label ID="lblMensaje" runat="server"
                                Text="Datos Grabados Correctamente" Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>


</asp:Content>
