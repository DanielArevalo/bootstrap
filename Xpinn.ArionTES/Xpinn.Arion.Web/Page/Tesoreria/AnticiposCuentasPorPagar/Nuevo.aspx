<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Plan de Cuentas :." %>
    <%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvCuentasxPagar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="700px">
                        <tr>
                            <td style="text-align: left; width: 130px;">
                                <asp:Label ID="Label2" runat="server" Text="Codigo" /><br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" Style="text-align: right"
                                    OnTextChanged="txtValor_TextChanged" />
                            </td>

                            <td style="text-align: left; width: 140px">
                                <asp:Label ID="lblNumFactura" runat="server" Text="Num. Factura" /><br />
                                <asp:TextBox ID="txtNumFactura" runat="server" CssClass="textbox" Width="90%" Style="text-align: right"
                                    Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                <asp:Label ID="lblFechaFact" runat="server" Text="Fecha Factura" /><br />
                                <uc2:fecha ID="txtFechaFact" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 130px;">
                                Valor Neto
                                <br />
                                <asp:TextBox ID="txtvalorneto" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 130px;">
                                Saldo
                                <br />
                                <asp:TextBox ID="txtsaldo" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 130px;">
                                Estado
                                <br />
                                <asp:TextBox ID="txtestado" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                            </td>
                        </tr>
                         <caption>
                             <br />
                             <tr>
                                 <td style="text-align: left; width: 130px;">
                                     Cód.Persona<br />
                                     <asp:TextBox ID="txtcodpersona" runat="server" CssClass="textbox" 
                                         Enabled="false" Width="90%" />
                                 </td>
                                 <td style="text-align: left; width: 130px;">
                                     Identificación<br />
                                     <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" 
                                         Enabled="false" Width="90%" />
                                 </td>
                                 <td colspan="2" style="text-align: left">
                                     Tipo identificación<br />
                                     <asp:DropDownList ID="ddltipoidentificacion" runat="server" AutoPostBack="true" 
                                         CssClass="textbox" Enabled="false" Width="230px" />
                                 </td>
                                 <td colspan="2" style="text-align: left; width: 130px;">
                                     Nombres<br />
                                     <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Enabled="false" 
                                         Width="230px" />
                                 </td>
                             </tr>
                        </caption>
                    </table>
                    <table>
                        <tr>
                            <td style="text-align: left;" colspan="5">
                                <strong>Datos Del Anticipo</strong>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label ID="Label1" runat="server" Text="Fecha Anticipa" /><br />
                                <uc2:fecha ID="txtfechaanticipa" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left;">
                                Valor Anticipo
                                <br />
                                <asp:TextBox ID="txtvalor" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 130px;">
                                Observaciones
                                <br />
                                <asp:TextBox ID="txtobservaciones" runat="server" CssClass="textbox" Width="320px" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="text-align: left;">
                                <strong>Datos para el Giro</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <uc3:ctlgiro ID="ctlGiros" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
