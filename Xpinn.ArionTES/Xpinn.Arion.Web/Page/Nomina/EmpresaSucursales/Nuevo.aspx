<%@ Page Title=".: Empleados :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                            <br />
                            <table>
                                <tr>
                                    <td style="text-align: left;">Código:
                                    </td>
                                    <td style="text-align: left; width: 200px;">
                                        <asp:TextBox ID="txtcodigo" runat="server" CssClass="textbox" Enabled="false" Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">Nombre:
                                    </td>
                                    <td style="text-align: left; width: 250px;">
                                        <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">E-mail:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revTxtEmail" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="E-Mail no valido!" ForeColor="Red"
                                            Style="font-size: x-small"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Teléfono:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txttelefono" runat="server" CssClass="textbox" Width="150px" onkeypress="return isNumber()"></asp:TextBox>
                                    </td>

                                    <td style="text-align: left;">Ciudad:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddciudad" runat="server" CssClass="textbox" Width="210px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="text-align: left;">Dirección:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:direccion ID="txtdir" runat="server" Width="150px"></uc1:direccion>
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
