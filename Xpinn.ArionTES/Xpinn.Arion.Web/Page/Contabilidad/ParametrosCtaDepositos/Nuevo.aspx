<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
    <asp:Panel ID="panelInfor" runat="server">
        <table>
            <tr>
                <td colspan="2" style="text-align: left;">Codigo<br />
                    <asp:TextBox ID="txtCodigo" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">Tipo Ahorro<br />
                    <asp:DropDownList ID="ddlTipoAhorro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoAhorro_SelectedIndexChanged"
                        Height="26px" CssClass="dropdown" Width="318px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; width: 320px">Linea de Ahorro<br />
                    <ctl:ctlListarCodigo ID="ctlListarCodigoAhorro" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 130px; text-align: left">Cod Cuenta<br />
                                        <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" BackColor="#F4F5FF"
                                            CssClass="textbox" OnTextChanged="txtCodCuenta_TextChanged" Style="text-align: left"
                                            Width="120px"></cc1:TextBoxGrid>
                                        <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                    </td>
                                    <td style="width: 25px; text-align: center">
                                        <br />
                                        <cc1:ButtonGrid ID="btnListadoPlan" runat="server" CssClass="btnListado" OnClick="btnListadoPlan_Click"
                                            Width="95%" Text="..." />
                                    </td>
                                    <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                        <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" CssClass="textbox" Enabled="false"
                                            Width="300px" />
                                    </td>
                                    <td style="width: 190px; text-align: left">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 130px; text-align: left">Cod Cuenta NIIF
                                <br />
                                        <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                                            CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                                        </cc1:TextBoxGrid>
                                        <uc2:ListadoPlanNif ID="ctlListadoPlanNif" runat="server" />
                                    </td>
                                    <td style="width: 25px; text-align: center">
                                        <br />
                                        <cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server" Text="..."
                                            Width="95%" OnClick="btnListadoPlanNIF_Click" />
                                    </td>
                                    <td style="width: 230px; text-align: left">Nombre de la Cuenta
                                <br />
                                        <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" CssClass="textbox"
                                            Width="300px" Enabled="False">
                                        </cc1:TextBoxGrid>
                                    </td>
                                    <td style="width: 190px; text-align: left">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 130px; text-align: left">Tipo de Movimiento<br />
                                <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" Width="160px" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 350px">Tipo de Transaccion<br />
                                <ctl:ctlListarCodigo ID="ctlListarCodigoTransaccion" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
