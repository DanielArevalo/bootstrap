<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Empleados :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlListarEmpleados.ascx" TagName="ctlListarEmpleados" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<script runat="server">

  
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <br />

    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewPrincipal">
            <table border="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <table id="tbCriterios" border="0" width="100%">
                                    <tr>
                                       
                                        <td style="width: 20%">Fecha Inicio<br />
                                            <asp:TextBox ID="txtFechaAnticipos" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="true" MaxLength="10" OnTextChanged="txtFechaAnticipos_TextChanged" Width="80%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtenderAnticipos" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioAnticipos" TargetControlID="txtFechaAnticipos">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioAnticipos" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="width: 20%; height: 49px;">
                                            <asp:TextBox ID="txtCodigoConsecutivo" Visible="false" runat="server" CssClass="textbox" />
                                        </td>
                                        <td style="width: 20%; height: 49px;"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">Tipo Nómina<br />
                                            <asp:DropDownList ID="ddlTipoNomina" runat="server" AppendDataBoundItems="true" Autopostback="true" CssClass="dropdown" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged" Width="70%">
                                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%">Fecha Inicio<br />
                                            <asp:TextBox ID="txtFechaInicio" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="false" MaxLength="10" OnTextChanged="txtFechaAnticipos_TextChanged" Width="80%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicio" TargetControlID="txtFechaInicio">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioInicio" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="width: 20%">Fecha Fin<br />
                                            <asp:TextBox ID="txtFechaFin" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="false" MaxLength="10" OnTextChanged="txtFechaAnticipos_TextChanged" Width="80%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioFin" TargetControlID="txtFechaFin">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioFin" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="width: 20%">Centro Costo<br />
                                            <asp:DropDownList ID="ddlCentroCosto" runat="server" AppendDataBoundItems="true" CssClass="dropdown" Width="70%">
                                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table id="tbBotones" border="0" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <table style="width: 100%; text-align: center">
                                        <tr>
                                            <td style="text-align: left; padding-right: 20px">
                                                <asp:Button runat="server" ID="btnLiquidacionDefinitiva" Width="200px" Text="Realizar la Liquidación" CssClass="btn8" OnClick="btnLiquidacionDefinitiva_Click" />
                                            </td>
                                            <td style="text-align: left; padding-left: 20px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel runat="server"  ID="updatePanelLiquidacionGeneradas" Visible="false">
                <ContentTemplate>
                    <table border="0" width="100%">
                        <tr>
                            <td style="text-align:left;">
                                <strong>Filtro Liquidaciones Generadas</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width:70%;">
                                    <tr>
                                        <td>
                                            <label>Nombre</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtNombre" CssClass="textbox"/>
                                        </td>
                                        <td>
                                            <label>Identificación</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="return isNumber(event)" ID="txtIdentificacion" CssClass="textbox"/>
                                        </td>
                                        <td>
                                            <label>Código Empleado</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCodigoEmpleado" onkeypress="return isNumber(event)" CssClass="textbox"/>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnFiltrar" runat="server" CssClass="btn8" Text="Filtrar" Width="100px" OnClick="btnFiltrar_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista"
                                    runat="server"
                                    AllowPaging="True"
                                    AutoGenerateColumns="False"
                                    GridLines="Horizontal"
                                    PageSize="50"
                                    HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True"
                                    Width="100%"
                                    OnPageIndexChanging="gvLista_PageIndexChanging"
                                    DataKeyNames="consecutivo"
                                    Style="font-size: x-small" OnRowDataBound="gvLista_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="codigoempleado" HeaderText="Cod.Empleado" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion_empleado" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="salario" HeaderText="Salario" ItemStyle-HorizontalAlign="Center" DataFormatString="${0:#,##0.00}" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dias" HeaderText="Dias Liquidados" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="porcentaje_anticipo" HeaderText="%Anticipo" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_anticipo" HeaderText="Valor Anticipo" ItemStyle-HorizontalAlign="Center" DataFormatString="${0:#,##0.00}" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                          <asp:BoundField DataField="porcentaje_anticipo_sub" HeaderText="%Anticipo Sub Trans." ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_anticipo_sub" HeaderText="Valor Anticipo Sub transp" ItemStyle-HorizontalAlign="Center" DataFormatString="${0:#,##0.00}" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                                <asp:Label ID="lblTotalNominaIdenti" Text="Total Nomina:" Visible="false" runat="server" />
                                <asp:Label ID="lblTotalNomina" Font-Bold="true" Visible="false" runat="server" />
                                <br />
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div style="text-align: center">
            </div>

            <div style="text-align: center">
            </div>

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
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
      <asp:View runat="server" ID="viewImprimir">
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnImprimirDesprendibles" runat="server" Height="30px" Text="Imprimir Desprendibles"
                                OnClick="btnImprimirDesprendibles_Click" CssClass="btn8" />
                            <asp:Button ID="btnImprimirPlanilla" runat="server" Height="30px" Text="Imprimir Planilla"
                                CssClass="btn8" Style="margin-left: 10px" OnClick="btnImprimirPlanilla_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr> 
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="pnlReporte" Visible="false">
                                <rsweb:ReportViewer ID="rvReporteDesprendible" runat="server" Font-Names="Verdana" Visible="false"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%">
                                    
                               
                                </rsweb:ReportViewer>
                                <rsweb:ReportViewer ID="rvReportePlanilla" runat="server" Font-Names="Verdana" Visible="false"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%">
                                    
                               
                                </rsweb:ReportViewer>
                            </asp:Panel>
                        </td>
                         <td>
                             &nbsp;</td>
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
          <asp:View runat="server" ID="viewComprobante">
            <asp:Panel ID="Panel2" runat="server">
                <table style="width: 100%;">     
                    <tr>
                        <td>
                         <asp:Panel ID="panelGeneral" runat="server">
</asp:Panel>
<asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

                        </td>
                   </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensajeGuardar" runat="server" />


</asp:Content>
