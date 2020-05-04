<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 90%">
                <tr style="text-align: left">
                    <td>
                        <strong>Registro Horas Extras</strong>
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">Código Hora Extra
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">
                        <asp:TextBox ID="txtCodigoInactividad" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 100%;">

                            <tr>
                            <td style="text-align: left; width: 308px; height: 34px;">Identificación </td>
                            <td style="text-align: left; width: 7%; height: 34px;">
                                <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 55px; height: 34px;" colspan="2">Tipo Identificación </td>
                            <td style="text-align: left; width: 140px; height: 34px;" colspan="2">
                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="190px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 194px; height: 34px;" colspan="2">Código Empleado </td>
                            <td style="text-align: left; width: 25%; height: 34px;">
                                <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hiddenCodigoPersona" runat="server" />
                            </td>
                      </tr>

                            <tr>
                                <td style="text-align: left; width: 308px; height: 34px;">Nombres y Apellidos
                                </td>
                                <td style="text-align: left; height: 34px;" colspan="8">
                                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false" Width="710px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 308px; height: 34px;">Tipo Nómina </td>
                                <td style="text-align: left; height: 34px;">
                                    <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="textbox" Width="250px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; height: 34px; width: 10%;">&nbsp;</td>
                                <td colspan="2" style="text-align: left; height: 34px;">&nbsp;</td>
                                <td colspan="2" style="text-align: left; height: 34px;">&nbsp;</td>
                                <td colspan="2" style="text-align: left; height: 34px;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 308px; height: 34px;">Fecha </td>
                                <td style="text-align: left; height: 34px;"><asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                    <img id="imagenCalendarioInicio" alt="Calendario"
                                        src="../../../Images/iconCalendario.png" />
                                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicio" TargetControlID="txtFecha">
                                    </asp:CalendarExtender></td>
                                <td style="text-align: left; height: 34px; width: 10%;">Cantidad Horas </td>
                                <td colspan="4" style="text-align: left; height: 34px;">
                                    <asp:TextBox ID="txtCantidadHoras" runat="server" CssClass="textbox" Visible="true"></asp:TextBox>
                                     </td>
                                <td colspan="2" style="text-align: left; height: 34px;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 308px; height: 34px;">Concepto Nómina </td>
                                <td style="text-align: left; height: 34px;">
                                    <asp:DropDownList ID="ddlConceptoNomina" runat="server" CssClass="textbox" Width="250px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; height: 34px; width: 10%;">
                                    <asp:Label ID="Lblpagadas" runat="server" Text="Pagadas"></asp:Label>
                                </td>
                                <td colspan="4" style="text-align: left; height: 34px;">
                                    <asp:CheckBoxList ID="chkPagadas" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                    </asp:CheckBoxList>
                                </td>
                                <td colspan="2" style="text-align: left; height: 34px;">&nbsp;</td>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información Guardada Correctamente"
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
