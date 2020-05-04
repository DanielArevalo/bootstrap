<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Src="~/General/Controles/PlanPagos.ascx" TagName="planpagos" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PanelClick(sender, e) {
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

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

        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }
    </script>

    <asp:MultiView ID="mvCredito" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="panelEncabezado" runat="server">
                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="logo" colspan="3" style="text-align: left">
                                            <strong>DATOS DEL DEUDOR</strong>
                                        </td>
                                        <td class="logo" style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 150px; text-align: left">Identificación
                                        </td>
                                        <td style="text-align: left">Tipo Identificación
                                        </td>
                                        <td style="text-align: left">Nombre
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 150px; text-align: left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                                                Enabled="false" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>

                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <strong>DATOS DEL CRÉDITO</strong>
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">Número Radicación<br />
                            <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td colspan="3" style="text-align: left">Línea de crédito<br />
                            <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox"
                                Enabled="false" Width="347px" />
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">Monto<br />
                            <uc2:decimales ID="txtMonto" runat="server" Enabled="true" />
                        </td>
                        <td style="text-align: left">Plazo<br />
                            <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td style="text-align: left">Periodicidad<br />
                            <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td style="text-align: left">Valor de la Cuota<br />
                            <uc2:decimales ID="txtValor_cuota" runat="server" Enabled="false" />
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">Forma de Pago<br />
                            <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td style="text-align: left">Moneda<br />
                            <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td style="text-align: left">F.Aprobación<br />
                            <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td style="text-align: left">Estado<br />
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">Saldo Capital<br />
                            <uc2:decimales ID="txtSaldoCapital" runat="server" Enabled="false" />
                        </td>
                        <td style="text-align: left">F. Ult Pago<br />
                            <uc1:fecha ID="txtFechaUltimoPago" Enabled="false" runat="server" />
                        </td>
                        <td style="text-align: left">&nbsp;                                         
                        </td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Modificación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
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
    <uc4:mensajegrabar ID="MensajeGrabar1" runat="server" />

</asp:Content>
