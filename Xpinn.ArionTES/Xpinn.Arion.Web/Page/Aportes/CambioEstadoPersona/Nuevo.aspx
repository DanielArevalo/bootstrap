<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cambio de Estado :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlDatosPersona.ascx" TagName="ddlPersona" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

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
    <asp:MultiView ID="mvCambioEstado" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewTitular" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong style="text-align: left">Seleccione la Persona para Cambio de Estado</strong>
                        <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pCuenta" runat="server" Width="100%">
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td style="text-align: left" colspan="3">
                            <strong>Datos Principales</strong>&nbsp;&nbsp;
                            <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Fecha de Cambio<br />
                            <uc1:fecha ID="txtFechaApertura" runat="server" Enabled="True" Habilitado="True"
                                style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <strong>Titular</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <asp:Panel ID="panelPersona" runat="server">
                                <ctl:ddlPersona ID="ctlPersona" runat="server" Width="400px" />
                            </asp:Panel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="display: block; text-align: left; padding-top: 15px;">
                        <td style="text-align: left" colspan="3">
                            Fecha Afilacion:
                            <br />
                            <asp:TextBox ID="txtFechaAfiliacion" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td>
                            Fecha Retiro:
                            <br />
                            <asp:TextBox ID="txtFechaRetiro" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                        <td>
                            Estado Actual:
                            <br />
                            <asp:TextBox ID="txtEstadoActual" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="text-align: left; display: block;">
                        <td style="text-align: left;" colspan="2">
                            Nuevo Estado:
                            <br />
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" />
                        </td>
                        <td>
                            Motivo Cambio:
                            <br />
                            <asp:DropDownList ID="ddlCambio" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            Observaciones<br />
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Width="685px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <hr style="text-align: left" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table id="Table1" border="0" cellpadding="1" cellspacing="0" width="50%">
                    <tr>
                        <td style="text-align: left">
                            <hr style="text-align: left" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pDatos" runat="server" Width="100%">
                </asp:Panel>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Datos Grabados Correctamente" Style="color: #FF3300"></asp:Label>
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
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
