<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
      <asp:View ID="vwDatos" runat="server">
                                     <table cellpadding="5" cellspacing="0" style="width: 100%">
                                         <tr>
                                             <td colspan="6" style="text-align: left; height: 23px; "><strong>Información del Contrato </strong>&nbsp;&nbsp;</td>
                                             <td colspan="2" style="text-align: left; height: 23px;">Código Ingreso </td>
                                             <td style="text-align: left; height: 23px; width: 25%;">
                                                 <asp:TextBox ID="txtCodigoIngreso" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: left; height: 23px; width: 28%;">Identificación </td>
                                             <td style="text-align: left; height: 23px; width: 15%;">
                                                 <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%" OnTextChanged="txtIdentificacionn_TextChanged"></asp:TextBox>
                                             </td>
                                             <td style="text-align: left; height: 23px; width: 21%;">Tipo Identificación </td>
                                             <td colspan="3" style="text-align: left; height: 23px;">
                                                 <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td colspan="2" style="text-align: left; height: 23px;">Código Empleado</td>
                                             <td style="text-align: left; height: 23px; width: 25%;">
                                                 <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                                                 <asp:HiddenField ID="hiddenCodigoPersona" runat="server" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: left; height: 23px; width: 28%;">Nombres y Apellidos </td>
                                             <td colspan="5" style="text-align: left; height: 23px; ">
                                                 <asp:TextBox ID="txtNombreEmpleado" runat="server" CssClass="textbox" Enabled="false" Width="93%"></asp:TextBox>
                                             </td>
                                             <td colspan="2" style="text-align: left; height: 23px;">Cargo </td>
                                             <td style="text-align: left; height: 23px; width: 25%;">
                                                 <asp:DropDownList ID="ddlCargo" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: left; height: 19px; width: 28%;">Tipo Nómina </td>
                                             <td style="text-align: left; height: 19px; width: 15%;">
                                                 <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td colspan="2" style="text-align: left; height: 19px;">Centro de Costo </td>
                                             <td colspan="2" style="text-align: left; height: 19px;">
                                                 <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td colspan="2" style="text-align: left; height: 19px;">Area </td>
                                             <td style="text-align: left; height: 19px; width: 25%;">
                                                 <asp:DropDownList ID="ddlArea" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: left; height: 7px; width: 28%;">Contrato </td>
                                             <td style="text-align: left; height: 7px; width: 15%;">
                                                 <asp:DropDownList ID="ddlContrato" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td colspan="3" style="text-align: right; height: 7px;">
                                                 <div class="text-left">
                                                     Es Contrato Prestación de Servicios?
                                                 </div>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <div class="text-center">
                                                     <asp:CheckBoxList ID="chkEsContratoPrestacional" runat="server" RepeatDirection="Horizontal">
                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                     </asp:CheckBoxList>
                                                 </div>
                                             </td>
                                             <td style="text-align: right; height: 7px;">&nbsp;</td>
                                             <td colspan="2" style="text-align: left; height: 7px;">
                                                 <asp:Label ID="Lblpagaduria" runat="server" Text="Pagaduria" Visible="False"></asp:Label>
                                             </td>
                                             <td style="text-align: left; height: 7px; width: 25%;">
                                                 <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="222px" Visible="False">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: left; height: 23px; width: 28%;">Fecha Ingreso </td>
                                             <td style="text-align: left; height: 23px; width: 15%;">
                                                 <asp:TextBox ID="txtFechaIngreso" runat="server" AutoPostBack="true" CssClass="textbox" MaxLength="10" OnTextChanged="txtFechaIngreso_TextChanged" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicio" TargetControlID="txtFechaIngreso">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioInicio" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                             </td>
                                             <td style="text-align: center; width: 21%; padding: 10px">Fecha Periodo Prueba </td>
                                             <td colspan="2" style="text-align: left; width: 447px; height: 23px;">
                                                 <asp:TextBox ID="txtFechaInicioPrueba" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicioPrueba" TargetControlID="txtFechaInicioPrueba">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioInicioPrueba" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                             </td>
                                             <td colspan="2" style="text-align: left; height: 23px;">Fecha Fin Periodo Prueba</td>
                                             <td colspan="2" style="text-align: left; height: 23px; width: 32%;">
                                                 <asp:TextBox ID="txtFechaFinPrueba" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioFinPrueba" TargetControlID="txtFechaFinPrueba">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioFinPrueba" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: left; height: 23px; width: 28%;">Dia Habil/Sábado
                                                 <asp:DropDownList ID="ddldiahabil" runat="server" CssClass="textbox">
                                                     <asp:ListItem Value="SeleccioneUnItem">Seleccione Un Item</asp:ListItem>
                                                     <asp:ListItem Value="Si"> Si </asp:ListItem>
                                                     <asp:ListItem Value="No"> No </asp:ListItem>
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: left; height: 23px; width: 15%;">
                                                 <asp:CheckBoxList ID="chkAuxilioTransporte" runat="server" RepeatDirection="Horizontal" Visible="false"> 
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" Selected="True" />
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                 </asp:CheckBoxList>
                                             </td>
                                             <td style="text-align: center; width: 21%; padding: 10px">Ley 50? </td>
                                             <td colspan="2" style="text-align: left; width: 447px; height: 23px;">
                                                 <asp:RadioButton ID="rdButtonLey50" runat="server" Text="Si" />
                                             </td>
                                             <td colspan="2" style="text-align: left; height: 23px;">Extranjero </td>
                                             <td colspan="2" style="text-align: left; height: 23px; width: 32%;">
                                                 <asp:CheckBoxList ID="chkExtranjero" runat="server" RepeatDirection="Horizontal">
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                 </asp:CheckBoxList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="6">
                                                 <asp:UpdatePanel runat="server">
                                                     <ContentTemplate>
                                                         <table style="width: 117%">
                                                             <tr>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">Salario </td>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">
                                                                     <asp:TextBox ID="txtSalario" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" />
                                                                 </td>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">Forma Pago </td>
                                                                 <td style="text-align: center; padding: 10px; width: 15%;">
                                                                     <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Width="90%" />
                                                                 </td>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">Sueldo Variable </td>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">
                                                                     <asp:CheckBoxList ID="chkSalarioVariable" runat="server" RepeatDirection="Horizontal">
                                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                                     </asp:CheckBoxList>
                                                                 </td>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">Sueldo Integral</td>
                                                                 <td style="text-align: center; width: 15%; padding: 10px">
                                                                     <asp:CheckBoxList ID="chkSalarioIntegral" runat="server" RepeatDirection="Horizontal">
                                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                                     </asp:CheckBoxList>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td colspan="6" style="width: 100%">
                                                                     <asp:Panel ID="pnlInformacionBancaria" runat="server" Visible="false" Width="100%">
                                                                         <table style="width: 100%">
                                                                             <tr>
                                                                                 <td style="text-align: center; width: 15%; padding: 10px">Tipo de Cuenta </td>
                                                                                 <td style="text-align: center; width: 15%; padding: 10px">
                                                                                     <asp:CheckBoxList ID="chkTipoCuenta" runat="server" RepeatDirection="Horizontal" TextAlign="Right">
                                                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Ahorro" Value="0" />
                                                                                         <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Corriente" Value="1" />
                                                                                     </asp:CheckBoxList>
                                                                                 </td>
                                                                                 <td style="text-align: center; width: 15%; padding: 10px">Banco </td>
                                                                                 <td style="text-align: center; padding: 10px">
                                                                                     <asp:DropDownList ID="ddlBancoConsignacion" runat="server" CssClass="textbox" Width="90%">
                                                                                     </asp:DropDownList>
                                                                                 </td>
                                                                                 <td style="text-align: center; width: 15%; padding: 10px">Cuenta Bancaria </td>
                                                                                 <td style="text-align: center; width: 15%; padding: 10px">
                                                                                     <asp:TextBox ID="txtNumeroCuentaBancariaConsignacion" runat="server" CssClass="textbox" />
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </asp:Panel>
                                                                 </td>
                                                                 <td style="width: 100%">&nbsp;</td>
                                                                 <td style="width: 100%">&nbsp;</td>
                                                             </tr>
                                                         </table>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </td>
                                         </tr>
                                     </table>
                                     <table cellpadding="5" cellspacing="0" style="width: 100%">
                                         <tr style="text-align: left">
                                             <td colspan="6"><strong>Información Parafiscales</strong>
                                                 <hr />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fondo de Salud </td>
                                             <td style="text-align: center; padding: 10px; width: 18%">
                                                 <asp:DropDownList ID="ddlFondoSalud" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Ingreso </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaIngresoFondoSalud" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioIngresoSalud" TargetControlID="txtFechaIngresoFondoSalud">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioIngresoSalud" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Retiro </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaRetiroFondoSalud" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioRetiroSalud" TargetControlID="txtFechaRetiroFondoSalud">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioRetiroSalud" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fondo de Pensión </td>
                                             <td style="text-align: center; padding: 10px; width: 18%">
                                                 <asp:DropDownList ID="ddlFondoPension" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Ingreso </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaIngresoFondoPension" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioIngresoPension" TargetControlID="txtFechaIngresoFondoPension">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioIngresoPension" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Retiro </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaRetiroFondoPension" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioRetiroPension" TargetControlID="txtFechaRetiroFondoPension">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioRetiroPension" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fondo de Cesantias </td>
                                             <td style="text-align: center; padding: 10px; width: 18%">
                                                 <asp:DropDownList ID="ddlFondoCesantias" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Ingreso </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaIngresoFondoCesantias" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioIngresoCesantias" TargetControlID="txtFechaIngresoFondoCesantias">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioIngresoCesantias" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Retiro </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaRetiroFondoCesantias" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioRetiroCesantias" TargetControlID="txtFechaRetiroFondoCesantias">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioRetiroCesantias" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 12%; padding: 10px">Caja de Compensación </td>
                                             <td style="text-align: center; padding: 10px; width: 18%">
                                                 <asp:DropDownList ID="ddlCajaCompensacion" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Ingreso </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaIngresoCajaCompensacion" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioIngresoCajaCompensacion" TargetControlID="txtFechaIngresoCajaCompensacion">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioIngresoCajaCompensacion" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                             <td style="text-align: center; width: 12%; padding: 10px">Fecha Retiro </td>
                                             <td style="text-align: center; width: 18%; padding: 10px">
                                                 <asp:TextBox ID="txtFechaRetiroCajaCompensacion" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioRetiroCompensacion" TargetControlID="txtFechaRetiroCajaCompensacion">
                                                 </asp:CalendarExtender>
                                                 <img id="imagenCalendarioRetiroCompensacion" alt="Calendario"
                            src="../../../Images/iconCalendario.png" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 15%; padding: 10px">ARL </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:DropDownList ID="ddlARL" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">Pensión Voluntaria </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:DropDownList ID="ddlPensionVoluntaria" runat="server" CssClass="textbox" Width="90%">
                                                     <asp:ListItem Text="Seleccione un Item" Value=" " />
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">Tipo de Cotizante </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:DropDownList ID="ddlTipoCotizante" runat="server" CssClass="textbox" Width="90%">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 15%; padding: 10px">Clase Riesgo&nbsp; Arl</td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:DropDownList ID="ddlTipoRiesgoARL" runat="server" CssClass="textbox" Width="90%" OnSelectedIndexChanged="ddlTipoRiesgoARL_SelectedIndexChanged" AutoPostBack="True">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">Porcentaje arl </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" Enabled="False"/>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">&nbsp;</td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 &nbsp;</td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 15%; padding: 10px">Pensionado por Vejez </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:CheckBoxList ID="chkPensionadoPorVejez" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkPensionadoPorVejez_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                 </asp:CheckBoxList>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">Pensionado por Invalidez </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:CheckBoxList ID="chkPensionadoPorInvalidez" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkPensionadoPorInvalidez_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                 </asp:CheckBoxList>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">Procedimiento Retención En la Fuente</td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:CheckBoxList ID="chkProcRetencion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkPensionadoPorInvalidez_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="1" Value="1"  Selected="True"/>
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="2" Value="0" />
                                                 </asp:CheckBoxList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:Label ID="Lblinactivacion" runat="server" Text="Inactivación por Liquidación Contrato Pendiente"></asp:Label>
                                             </td>
                                             <td style="text-align: center; width: 15%; padding: 10px">
                                                 <asp:CheckBoxList ID="chkInactiviacion" runat="server" AutoPostBack="True"  RepeatDirection="Horizontal">
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Si" Value="1" />
                                                     <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="No" Value="0" />
                                                 </asp:CheckBoxList>
                                             </td>
                                         </tr>
                                     </table>
                                     <%--Me hicieron quitarlo luego de hacerlo porque ya no lo necesitaban--%>
                                     <asp:UpdatePanel runat="server" Visible="false">
                                         <ContentTemplate>
                                             <table cellpadding="5" cellspacing="0" style="width: 100%">
                                                 <tr>
                                                     <td>
                                                         <asp:Button ID="btnAgregarConcepto" runat="server" CssClass="btn8 " OnClick="btnAgregarConcepto_Click" Text="Agregar Concepto + " />
                                                         <asp:GridView ID="gvConceptosFijos" runat="server" AutoGenerateColumns="False" DataKeyNames="consecutivo" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" HorizontalAlign="Center" OnRowCommand="gvConceptosFijos_RowCommand" OnRowDataBound="gvConceptosFijos_RowDataBound" OnRowDeleting="gvConceptosFijos_RowDeleting" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: small" Width="40%">
                                                             <Columns>
                                                                 <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                                 <ItemStyle Width="16px" />
                                                                 </asp:CommandField>
                                                                 <asp:TemplateField HeaderText="Conceptos">
                                                                     <ItemTemplate>
                                                                         <asp:DropDownList ID="ddlConceptosGridView" runat="server" AppendDataBoundItems="true" CssClass="dropdown" Width="80%">
                                                                             <asp:ListItem Text="Selecciona un item" Value=" " />
                                                                         </asp:DropDownList>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                             </Columns>
                                                         </asp:GridView>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </ContentTemplate>
                                     </asp:UpdatePanel>
                                     <br />
                                     <br />
                                     <br />
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
