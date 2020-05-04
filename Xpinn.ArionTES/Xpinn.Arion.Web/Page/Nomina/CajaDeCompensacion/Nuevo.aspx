<%@ Page Title=".: Empleados :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Xpinn.Util" namespace="Xpinn.Util" tagprefix="cc1" %>
<%@ Register src="../../../General/Controles/ctlPlanCuentas.ascx" tagname="listadoplanctas" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server" ID="viewDatos">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Datos Básicos</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server">
                            <table>
                                <tr>
                                    <td style="height: 50px; text-align: left">Consecutivo: </td>
                                    <td style="text-align: left; width: 200px">
                                        <asp:TextBox ID="txtconsecutivo" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="height: 50px; text-align: left">Identificación: </td>
                                    <td style="text-align: left; width: 200px">
                                        <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" AutoPostback="true" OnTextChanged="txtidentificacion_TextChanged"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">Nombre: </td>
                                    <td style="text-align: left; width: 250px">
                                        <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Teléfono: </td>
                                    <td style="text-align: left; width: 200px">
                                        <asp:TextBox ID="txttelefono" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </td>
                                    <td style="height: 50px; text-align: left">Código Pila: </td>
                                    <td style="text-align: left; width: 200px">
                                        <asp:TextBox ID="txtcodpila" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px;"></td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="text-align: left;">Dirección: </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        <uc1:direccion ID="txtdir" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 130px; text-align: left">Cuenta Contable<br />
                                                            <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" BackColor="#F4F5FF" CssClass="textbox" OnTextChanged="txtCodCuenta_TextChanged" Style="text-align: left" Width="120px"></cc1:TextBoxGrid>
                                                            <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                                        </td>
                                                        <td style="width: 25px; text-align: center">
                                                            <br />
                                                            <cc1:ButtonGrid ID="btnListadoPlan" runat="server" CssClass="btnListado" OnClick="btnListadoPlan_Click" Text="..." Width="95%" />
                                                        </td>
                                                        <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                                            <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" CssClass="textbox" Enabled="false" Width="300px" />
                                                        </td>
                                                        <td style="width: 190px; text-align: left">&nbsp; </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 130px; text-align: left">Cod Cuenta Nomina<br />
                                                            <cc1:TextBoxGrid ID="txtCodCuentaNomina" runat="server" AutoPostBack="True" BackColor="#F4F5FF" CssClass="textbox" OnTextChanged="txtCodCuentaNomina_TextChanged" Style="text-align: left" Width="120px"></cc1:TextBoxGrid>
                                                            <uc1:ListadoPlanCtas ID="ctlCuentasNomina" runat="server" />
                                                        </td>
                                                        <td style="width: 25px; text-align: center">
                                                            <br />
                                                            <cc1:ButtonGrid ID="btnListarCuentaNomina" runat="server" CssClass="btnListado" OnClick="btnListadoPlanNomina_Click" Text="..." Width="95%" />
                                                        </td>
                                                        <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                                            <cc1:TextBoxGrid ID="txtNombreCuentaNomina" runat="server" CssClass="textbox" Enabled="false" Width="300px" />
                                                        </td>
                                                        <td style="width: 190px; text-align: left">&nbsp; </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="viewGuardado">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información Guardada Correctamente"
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
</asp:Content>
