<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Parametros Contables Inventarios :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="../../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="../../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvImpuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="text-align: left">
                        Código&nbsp;*&nbsp;
                        <asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtCategoria"
                            Style="font-size: x-small" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtCategoria" runat="server" CssClass="textbox" MaxLength="128" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: left">
                        Descripción&nbsp;*&nbsp;
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                            Style="font-size: x-small" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128"
                            Width="519px" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuenta_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlan2_Click" />
                                </td>
                                <td style="width: 230px; text-align: left">
                                    Nombre de la Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                        Width="200px" CssClass="textbox" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                    Tipo Mov.<br />
                                    <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" Width="170px" CssClass="dropdown"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Value="1">Débito</asp:ListItem>
                                        <asp:ListItem Value="2">Crédito</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta NIIF
                                    <br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas id="ctlListadoPlanNif" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlanNIF_Click" />
                                </td>
                                <td style="width: 230px; text-align: left">
                                    Nombre de la Cuenta
                                    <br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" CssClass="textbox"
                                        Width="200px" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta Ingreso
                                    <br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaIngreso" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaIngreso_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas id="ctlListadoPlanIngreso" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlanIngreso" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlanIngreso_Click" />
                                </td>
                                <td style="width: 230px; text-align: left">
                                    Nombre de la Cuenta
                                    <br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaIngreso" runat="server" Style="text-align: left" CssClass="textbox"
                                        Width="200px" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta Gasto
                                    <br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaGasto" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaGasto_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc2:listadoplannif id="ctlListadoPlanGasto" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlanGasto" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlanGasto_Click" />
                                </td>
                                <td style="width: 230px; text-align: left">
                                    Nombre de la Cuenta
                                    <br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaGasto" runat="server" Style="text-align: left" CssClass="textbox"
                                        Width="200px" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
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
                        <td style="text-align: center; font-size: large;color:Red">
                            Parametrizaciòn contable de categoria de inventarios &nbsp;<asp:Label ID="lblMsj" runat="server"></asp:Label>&nbsp;Correctamente
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
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
