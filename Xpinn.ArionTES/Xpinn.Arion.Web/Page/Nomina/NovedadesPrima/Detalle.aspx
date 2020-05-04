<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 85%">
                <tr style="text-align: left">
                    <td>
                        <strong>Registro Novedad Primas</strong>
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">Codigo Novedad
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">
                        <asp:TextBox ID="txtCodigoNovedad" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px">Identificación
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtIdentificacionn" Enabled="false" Width="90%" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Tipo Identificación
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                        Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Código Empleado
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtCodigoEmpleado" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px">Nombres y Apellidos
                                </td>
                                <td style="text-align: center; width: 75%; padding: 10px" colspan="5">
                                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox"
                                        Enabled="false" Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px; height: 49px;">Tipo Nómina
                                </td>
                                <td style="text-align: center; padding: 10px; height: 49px;" colspan="2">
                                    <asp:DropDownList ID="ddlTipoNomina" runat="server"   AutoPostBack="true" autoCssClass="textbox" Width="90%" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px; height: 49px;">Tipo Novedad
                                </td>
                                <td style="text-align: center; padding: 10px; height: 49px;" colspan="2">
                                    <asp:DropDownList ID="ddlTipoNovedad" runat="server" CssClass="textbox" Width="90%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px; height: 49px;">Semestre
                                </td>
                                <td style="text-align: center; width: 20%; padding: 10px" colspan="1">
                                    <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="textbox" Width="100%">
                                        <asp:ListItem Text="Primer Semestre" Value="1" />
                                        <asp:ListItem Text="Segundo Semestre" Value="2" />
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Año
                                </td>
                                <td style="text-align: center; width: 20%; padding: 10px">
                                    <asp:TextBox ID="txtAño" MaxLength="10" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="70%"></asp:TextBox>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Valor
                                </td>
                                <td style="text-align: center; width: 20%; padding: 10px" colspan="1">
                                    <asp:TextBox ID="txtValor" MaxLength="10" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="70%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
