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
                        <strong>Registro Retroactivos</strong>
                    </td>
                    <td style="text-align: center; padding: 10px">Código Retroactivo
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigoRetroactivo" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width: 100%;">

                             <tr>
                            <td style="text-align: left; height: 23px;">                             
                                Identificación </td>
                            <td style="text-align: left; height: 23px;">
                                <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                                 </td>
                            <td style="text-align: left; height: 23px;">
                                Tipo Identificación
                                </td>
                            <td style="text-align: left; width: 194px; height: 23px;">
                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                                </asp:DropDownList>
                                 </td>
                            <td style="text-align: left; height: 23px;">
                                Código Empleado
                            </td>
                                 <td style="text-align: left; height: 23px;">
                                     <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                     <asp:HiddenField ID="hiddenCodigoPersona" runat="server" />
                                 </td>
                        </tr>

                             <tr>
                                 <td style="text-align: left; height: 23px;">Nombres y Apellidos </td>
                                 <td style="text-align: left; height: 23px;" colspan="5">
                                     <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false" Width="740px"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align: left; height: 23px;">Tipo Nómina </td>
                                 <td style="text-align: left; height: 23px;">
                                     <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="textbox" Width="90%">
                                     </asp:DropDownList>
                                 </td>
                                 <td style="text-align: left; height: 23px;">&nbsp;</td>
                                 <td style="text-align: left; width: 194px; height: 23px;">&nbsp;</td>
                                 <td colspan="2" style="text-align: left; height: 23px;">&nbsp;</td>
                             </tr>

                             <tr>
                                 <td style="text-align: left; height: 23px;">Fecha Inicio </td>
                                 <td style="text-align: left; height: 23px;">
                                     <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                     <asp:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendario" TargetControlID="txtFechaInicio">
                                     </asp:CalendarExtender>
                                     <img id="imagenCalendario" alt="Calendario"
                                        src="../../../Images/iconCalendario.png" />
                                 </td>
                                 <td style="text-align: left; height: 23px;">Fecha Final </td>
                                 <td style="text-align: left; width: 194px; height: 23px;">
                                         <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                    <img id="imagenCalendarioFinal" alt="Calendario"
                                        src="../../../Images/iconCalendario.png" />
                                    <asp:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioFinal" TargetControlID="txtFechaFinal">
                                    </asp:CalendarExtender>

                                 </td>
                                 <td style="text-align: left; height: 23px;">Fecha Pago</td>
                                 <td style="text-align: left; height: 23px;">  <asp:TextBox ID="txtFechaPago" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaPago_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioPago" TargetControlID="txtFechaPago">
                                    </asp:CalendarExtender>
                                    <img id="imagenCalendarioPago" alt="Calendario"
                                        src="../../../Images/iconCalendario.png" /></td>
                             </tr>

                             <tr>
                                 <td style="text-align: left; height: 23px;">Valor </td>
                                 <td style="text-align: left; height: 23px;">
                                     <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" Width="90%"></asp:TextBox>
                                 </td>
                                 <td style="text-align: left; height: 23px;">Número Pagos </td>
                                 <td style="text-align: left; width: 194px; height: 23px;">
                                     <asp:TextBox ID="txtNumeroPagos" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" Width="90%"></asp:TextBox>
                                 </td>
                                 <td style="text-align: left; height: 23px;">Centro de Costo </td>
                                 <td style="text-align: left; height: 23px;">
                                     <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Width="90%">
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align: left; padding: 10px; height: 31px;" colspan="6"><strong>Periodicidad</strong>&nbsp;<hr /></td>
                             </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px" colspan="6">
                                    <asp:CheckBoxList ID="checkBoxListaPeriodicidad" runat="server" Width="100%" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="1er Periodo" Value="1" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="2do Periodo" Value="2" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="3er Periodo" Value="3" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="Último Periodo" Value="4" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="Todos los Periodos" Value="5" onclick="ExclusiveCheckBoxList(this);" />
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="padding: 10px">
                                    <div style="text-align: left">
                                        <strong style="text-align: left">Concepto por el cual se paga el retroactivo</strong>
                                    </div>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px" colspan="6">
                                    <asp:CheckBoxList ID="checkBoxListaConcepto" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3">
                                        <asp:ListItem Text="Sueldo" Value="39" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="Inactividad" Value="23" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="Cesantias" Value="7" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="Vacaciones" Value="43" onclick="ExclusiveCheckBoxList(this);" />
                                        <asp:ListItem Text="Prima" Value="32" onclick="ExclusiveCheckBoxList(this);" />
                                    </asp:CheckBoxList>
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
                            &nbsp;</td>
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
