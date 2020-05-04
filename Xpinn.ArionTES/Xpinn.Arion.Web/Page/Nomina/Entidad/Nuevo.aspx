<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="NuevoEn" Title=".: Xpinn - Entidad" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register assembly="Xpinn.Util" namespace="Xpinn.Util" tagprefix="cc1" %>
<%@ Register src="../../../General/Controles/ctlPlanCuentas.ascx" tagname="listadoplanctas" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server" ID="viewDatos">
            <table style="width: 100%">
                <tr>
                    <td class="tdI" colspan="4" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Datos Básicos</strong>
                    </td>
                </tr>
                <tr>
                    <td style="color: #359af2; text-align: center; font-size: 24px; font-family: Segoe UI; width: 60px;">
                        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 60px;">Consecutivo:
                  <br />
                        <asp:TextBox ID="txtconsecutivo" runat="server" CssClass="textbox" Width="130px" Enabled="false"></asp:TextBox>

                    </td>
                    <td style="text-align: left;">Identificación:
                   <br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="130px" AutoPostBack="true" onkeypress="return isNumber(event)" OnTextChanged="txtIdentificacion_TextChanged"></asp:TextBox>
                        <br />
                    </td>
                    <td style="text-align: left;">Nombre:
                <br />
                        <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>

                    </td>
                    <td style="text-align: left;">Clase: 
                  <br />
                        <asp:DropDownList ID="ddlclase" runat="server" Width="140px" CssClass="textbox">
                            <asp:ListItem Value="1">Fondo De Salud</asp:ListItem>
                            <asp:ListItem Value="2">Pension</asp:ListItem>
                            <asp:ListItem Value="3">Cesantías</asp:ListItem>
                            <asp:ListItem Value="4">ARL</asp:ListItem>
                            <asp:ListItem Value="5">ICBF</asp:ListItem>
                            <asp:ListItem Value="6">SENA</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 60px;">Responsable:
             <br />
                        <asp:TextBox ID="txtrespon" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>

                    </td>
                    <td style="text-align: left;">Código Pila:
          <br />
                        <asp:TextBox ID="txtcodpila" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>

                    </td>
                    <td style="text-align: left;">Actividad CIIU:
                <br />
                        <asp:DropDownList ID="ddlcodciiu" runat="server" Width="140px" CssClass="textbox"></asp:DropDownList>

                    </td>
                    <td style="text-align: left;">E-mail:
             <br />
                        <asp:TextBox ID="txtmail" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtmail" ErrorMessage="E-Mail no valido!" ForeColor="Red"
                            Style="font-size: x-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: left; width: 60px;">Teléfono:
              <br />
                        <asp:TextBox ID="txttel" runat="server" CssClass="textbox" Width="130px" onkeypress="return isNumber(event)"></asp:TextBox>

                    </td>
                    <td style="text-align: left;">Ciudad:
              <br />
                        <asp:DropDownList ID="ddlciudad" runat="server" Width="140px" CssClass="textbox"></asp:DropDownList>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 60px;">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 60px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="4">Dirección:
                        <uc1:direccion ID="txtdir" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left;">
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
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left;">
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
                <tr>
                    <td style="width: 60px">
                        &nbsp;</td>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información  Guardada Correctamente"
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

