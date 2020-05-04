<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Concepto :." %>

<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas"
    TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvParametros" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="650px">
                    <tr>
                        <td style="text-align: left; width: 76px;">Código<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="width: 500;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">Estructura<br />
                            <asp:DropDownList ID="ddlEstructura" runat="server" Width="90%" CssClass="textbox"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 76px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 130px; text-align: left">Cuenta Contable<br />
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



                        <td style="width: 76px">
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
                        <td colspan="2" style="text-align: left">Tipo Impuesto<br />
                            <asp:DropDownList ID="ddlTipoImpuesto" runat="server" Width="200px" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left; width: 200px; padding-right: 60px;">Tipo de Movimiento<br />
                                        <asp:DropDownList ID="ddlTipoMov" runat="server" Width="100%" CssClass="textbox" />
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
        </asp:View>
        <asp:View ID="vwFin" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblmsj" runat="server" Style="color: Red" Font-Bold="True" /><br />
                        <asp:Button ID="btnFin" runat="server" CssClass="btn8" Height="28px"
                            Text="  Regresar  " OnClick="btnFin_Click" />
                    </td>
                </tr>
            </table>

        </asp:View>
    </asp:MultiView>

</asp:Content>
