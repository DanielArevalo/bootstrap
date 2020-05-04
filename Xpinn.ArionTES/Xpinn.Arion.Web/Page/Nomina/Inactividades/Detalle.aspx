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
                <tr>

                    <td style="text-align: left; height: 23px;" colspan="8">
                        <strong>Registro Inactividades</strong>
                    </td>
                    <td colspan="2" style="text-align: left; height: 23px;">
                        Código Inactividad</td>
                    <td colspan="4" style="text-align: left; height: 23px;"><asp:TextBox ID="txtCodigoInactividad" runat="server" CssClass="textbox" Enabled="false" Width="87%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px; width: 16%;">Identificación </td>
                    <td style="text-align: left; height: 23px; width: 24%;">
                        <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                    </td>
                    <td colspan="3" style="text-align: left; height: 23px;">Tipo Identificación </td>
                    <td colspan="3" style="text-align: left; width: 447px; height: 23px;">
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="5" style="text-align: left; height: 23px;">Código Empleado</td>
                    <td style="text-align: left; height: 23px; width: 32%;">
                        <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false" Width="87%"></asp:TextBox>
                        <asp:HiddenField ID="hiddenCodigoPersona" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px; width: 16%;">Nombres y Apellidos </td>
                    <td colspan="7" style="text-align: left; height: 23px;">
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false" Width="98%"></asp:TextBox>
                    </td>
                    <td colspan="4" style="text-align: left; height: 23px;">Tipo Nómina </td>
                    <td colspan="2" style="text-align: left; height: 23px;">
                        <asp:DropDownList ID="ddlTipoNomina" runat="server" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged" Width="90%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px;" colspan="14"><strong>Tipos</strong>&nbsp;<hr /> </td>
                </tr>
                <tr>
                    <td colspan="14" style="text-align: left; height: 23px;">
                        <asp:CheckBoxList ID="checkBoxListTipos" runat="server" RepeatDirection="Horizontal" Width="100%">
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Reconoce Todo la empresa" Value="1" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Paga  dias corr. emp. y resto porcentaje" Value="2" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Utiliza porcentaje todos los dias" Value="3" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Porcentaje 50%" Value="4" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Prorroga" Value="5" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="&gt; Mas 180 días" Value="6" />
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px;" colspan="14"><strong>Clases</strong>&nbsp;<hr /> </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left; height: 23px;">
                        <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="textbox" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td colspan="4" style="text-align: left; height: 23px;">&nbsp;</td>
                    <td colspan="4" style="text-align: left; height: 23px;">&nbsp;</td>
                    <td colspan="3" style="text-align: left; height: 23px;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="14" style="text-align: left; height: 23px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px; width: 16%;">Fecha Inicio </td>
                    <td colspan="3" style="text-align: left; height: 23px;">
                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicio" 
                            TargetControlID="txtFechaInicio">
                        </asp:CalendarExtender>
                        <img id="imagenCalendarioInicio" alt="Calendario" src="../../../Images/iconCalendario.png" />
                    </td>
                    <td colspan="5" style="text-align: right; height: 23px;">Fecha Terminación </td>
                    <td colspan="5" style="text-align: left; height: 23px;">
                        <asp:TextBox ID="txtFechaTerminacion" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioTerminacion"
                             TargetControlID="txtFechaTerminacion">
                        </asp:CalendarExtender>
                        <img id="imagenCalendarioTerminacion" alt="Calendario" src="../../../Images/iconCalendario.png" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px; width: 16%;">Descripción </td>
                    <td colspan="13" style="text-align: left; height: 23px;">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" TextMode="MultiLine" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px; width: 16%;">
                        Remunerada
                    </td>
                    <td colspan="5" style="text-align: left; height: 23px;">
                        <asp:CheckBoxList ID="checkBoxListRemunerada" runat="server" RepeatDirection="Horizontal" Width="90%">
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                        </asp:CheckBoxList>
                    </td>
                    <td colspan="3" style="text-align: left; height: 23px;">Contrato </td>
                    <td colspan="5" style="text-align: left; height: 23px;">
                        <asp:DropDownList ID="ddlContrato" runat="server" CssClass="textbox" Width="90%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 23px; width: 16%;">&nbsp;</td>
                    <td colspan="5" style="text-align: left; height: 23px;">
                        &nbsp;</td>
                    <td colspan="3" style="text-align: left; height: 23px;">Aplicada&nbsp;</td>
                    <td colspan="5" style="text-align: left; height: 23px;">
                        <asp:CheckBoxList ID="chkaplicada" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                            <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                        </asp:CheckBoxList>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información grabada Correctamente"
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
