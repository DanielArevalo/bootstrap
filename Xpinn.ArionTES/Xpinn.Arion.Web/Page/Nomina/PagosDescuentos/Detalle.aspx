<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 90%">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="5" style="text-align: left; height: 34px;"><strong>Información Empleado</strong> </td>
                                <td colspan="2" style="text-align: left; width: 194px; height: 34px;">Código Pago </td>
                                <td style="text-align: right; width: 140px; height: 34px;">
                                    <asp:TextBox ID="txtCodigoPago" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 280px; height: 34px;">Identificación </td>
                                <td style="text-align: left; width: 280px; height: 34px;">
                                    <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 140px; height: 34px;">Tipo Identificación </td>
                                <td style="text-align: left; width: 140px; height: 34px;" colspan="2">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2" style="text-align: left; width: 194px; height: 34px;">Código Empleado </td>
                                <td style="text-align: right; width: 140px; height: 34px;">
                                    <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false" Width="90%" ></asp:TextBox>
                                    <asp:HiddenField ID="hiddenCodigoPersona" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 280px; height: 36px;">Nombres y Apellidos </td>
                                <td colspan="4" style="text-align: left; height: 36px;">
                                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false" Width="94%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; height: 36px;">Tipo Nomina </td>
                                <td colspan="2" style="text-align: right; height: 36px;">
                                    <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="textbox" Width="95%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; height: 34px;">Valor Cuota</td>
                                <td style="text-align: left; width: 280px; height: 34px;">
                                    <asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" Width="90%" onkeypress="return isNumber(event)"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 15%; padding: 10px">Valor Total </td>
                                <td style="text-align: center; width: 15%; padding: 10px" colspan="2">
                                    <asp:TextBox ID="txtValorTotal" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" Width="90%"></asp:TextBox>
                                </td>
                                <td colspan="2" style="width: 15%; padding: 10px; text-align: left;">
                                    <asp:Label ID="lblacumulado" runat="server" Text="Acumulado" Visible="false"></asp:Label>
                                </td>
                               
                                  <td style="width: 140px; height: 34px;">
                           <asp:TextBox ID="txtAcumulado" runat="server" CssClass="textbox" Enabled="false" onkeypress="return isNumber(event)" Width="92%"></asp:TextBox>
                             </td>

                            </tr>
                            <tr>
                               <td style="text-align: left; height: 34px;">Fecha</td>
                                  <td style="text-align: left; width: 280px; height: 34px;">
                                  <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendario" TargetControlID="txtFecha">
                                    </asp:CalendarExtender>
                                    <img id="imagenCalendario" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                      </td>

                                <td style="text-align: left; width: 15%; padding: 10px">
                                    <asp:Label ID="lblcontrolsaldos" runat="server" Text="Control de saldos" Visible="false"></asp:Label>
                                    &nbsp;</td>
                                <td style="text-align: center; width: 15%; padding: 10px" colspan="2">
                                    <asp:TextBox ID="txtControlSaldos" Enabled="false" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" Width="90%"></asp:TextBox>
                                </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    &nbsp;</td>
                                <td style="text-align: center; width: 15%; padding: 10px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center; padding: 10px">Liquidar hasta que acumulado sea igual al valor </td>
                                <td style="text-align: center; width: 15%; padding: 10px" colspan="2">
                                    <asp:CheckBoxList ID="checkBoxListPagoDefinitivo" runat="server" Width="90%">
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                    </asp:CheckBoxList>
                                </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    Liquidar como pago en la periocidad</td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:CheckBoxList ID="checkBoxListPagoPeriodica" runat="server" Width="90%">
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding: 10px" colspan="8"><strong>Descuento en Periodicidad</strong><hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:CheckBoxList ID="checkBoxListPeriocidad" runat="server" RepeatDirection="Horizontal" Width="90%">
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="1er Periodo" Value="1" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="2do Periodo" Value="2" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="3er Periodo" Value="3" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="4to Periodo" Value="4" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Todos los Periodos" Value="5" />
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">Concepto </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:DropDownList ID="ddlConcepto" AutoPostBack="true" runat="server" CssClass="textbox" Width="90%" OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged" style="height: 22px">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:Label ID="LblIdentificacion" runat="server" Visible="false" Text="Identificación"></asp:Label>
                                    <br />
                                    </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtIdPersona" runat="server" AutoPostBack="true" Visible="false" CssClass="textbox" Enabled="true" OnTextChanged="txtIdPersona_TextChanged" Width="110px" />
                                    <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Visible="false" Enabled="true" Height="26px" OnClick="btnConsultaPersonas_Click" Text="..." Width="22px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">Centro de Costo</td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2" style="text-align: center; padding: 10px; width: 15%;">
                                    <asp:Label ID="LblNombre" Visible="false" runat="server" Text="Nombre"></asp:Label>
                                    <br />
                                </td>
                                <td colspan="2" style="text-align: left; padding: 10px">
                                    <asp:TextBox ID="txtNomPersona" runat="server" Visible="false" CssClass="textbox" Enabled="false" Width="90%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">Motivos </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtMotivos" runat="server" CssClass="textbox" TextMode="MultiLine" Text=" " Width="90%"></asp:TextBox>
                                </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:Label ID="LblCodPersona" runat="server" Visible="false" Text="Cod.Persona"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtCodPersona" Visible="false" runat="server" CssClass="textbox" Enabled="false" Width="70%" />
                                    <br />
                                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
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
