<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<%@ Register src="../../../General/Controles/ctlGiro.ascx" tagname="ctlgiro" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 822px">
                      <tr>
                        <td style="text-align: left; width: 280px" colspan="2">
                            <asp:Label ID="lblError" runat="server" style="text-align: right" Visible="False"  ForeColor="Red"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                            <td style="text-align: left; height: 23px;" colspan="2">                             
                                <strong>Registro Liquidación de Contrato</strong>&nbsp;</td>
                            <td style="text-align: left; height: 23px;"></td>
                            <td style="text-align: left; height: 23px;">
                                </td>
                            <td style="text-align: left; width: 194px; height: 23px;">Código Liquidación</td>
                            <td style="text-align: left; height: 23px;">
                                <asp:TextBox ID="txtCodigoLiquidacion" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; height: 23px;" colspan="2">
                                &nbsp;</td>
                            <td style="text-align: left; height: 23px;">
                                &nbsp;</td>
                            <td style="text-align: left; height: 23px;">&nbsp;</td>
                            <td style="text-align: left; width: 194px; height: 23px;">
                                &nbsp;</td>
                            <td style="text-align: left; height: 23px;">&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td style="text-align: left; width: 280px; height: 34px;">Identificación </td>
                            <td style="text-align: left; width: 280px; height: 34px;">
                                <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 140px; height: 34px;">Tipo Identificación </td>
                            <td style="text-align: left; width: 140px; height: 34px;">
                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 194px; height: 34px;">Código Empleado </td>
                            <td style="text-align: left; width: 140px; height: 34px;">
                                <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                      </tr>
                        
                        <tr>
                            <td style="text-align: left; width: 150px; height: 38px;">
                                Nombres y Apellidos
                               
                            </td>
                            <td style="text-align: left; height: 38px;" colspan="5">
                                <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false" Width="755px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; " colspan="6">
                                <hr />
                            </td>
                        </tr>
                      <tr>
                          <td style="text-align: left; width: 140px">Tipo Nómina </td>
                          <td style="text-align: left; width: 140px">
                              <asp:DropDownList ID="ddlTipoNomina" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged" Width="90%">
                                  <asp:ListItem Text="Seleccione un Item" Value=" " />
                              </asp:DropDownList>
                          </td>
                          <td style="text-align: left; width: 160px">
                              <asp:Label ID="labelCentrodeCosto" runat="server" Text="Centro de Costo" Visible="false" />
                          </td>
                          <td style="text-align: left; width: 160px">
                              <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Enabled="false" Visible="false" Width="90%">
                              </asp:DropDownList>
                          </td>
                          <td style="text-align: left; width: 194px">&nbsp;</td>
                          <td style="text-align: left; width: 160px">
                              <asp:TextBox ID="txtCodigoPersona" runat="server" Visible ="false" CssClass="textbox" Enabled="false"></asp:TextBox>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: left; width: 140px">&nbsp;</td>
                          <td style="text-align: left; width: 140px">&nbsp;</td>
                          <td style="text-align: left; width: 160px">&nbsp;</td>
                          <td style="text-align: left; width: 160px">&nbsp;</td>
                          <td style="text-align: left; width: 194px">&nbsp;</td>
                          <td style="text-align: left; width: 160px">&nbsp;</td>
                      </tr>
                    </table>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlLiquidacion" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 822px">
                            <tr>
                                <td style="text-align: left; width: 280px; height: 34px;">Tipo de Contrato </td>
                                <td style="text-align: left; width: 280px; height: 34px;">
                                    <asp:DropDownList ID="ddlContrato" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 140px; height: 34px;">fecha de Ingreso</td>
                                <td style="text-align: left; width: 140px; height: 34px;">
                                    <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="textbox" Enabled="false" MaxLength="10" Width="70%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 194px; height: 34px;">Código Ingreso Personal </td>
                                <td style="text-align: left; width: 140px; height: 34px;">
                                    <asp:TextBox ID="txtCodigoIngresoPersonal" runat="server" CssClass="textbox" Enabled="false" MaxLength="10" Width="70%"></asp:TextBox>
                                </td>
                            </tr>
                    </table>

                        <table cellpadding="5" cellspacing="0" style="width: 86%; height: 100px;">
                            <tr>
                                <td colspan="6">
                                    <hr />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Fecha de Retiro
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtFechaRetiro" runat="server" AutoPostBack="true" CssClass="textbox" MaxLength="10" OnTextChanged="txtFechaRetiro_TextChanged" Width="70%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioRetiro" TargetControlID="txtFechaRetiro">
                                        
                     </asp:CalendarExtender>
                                    <img id="imagenCalendarioRetiro" alt="Calendario"
                                        src="../../../Images/iconCalendario.png" />
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">&nbsp;</td>
                                <td style="text-align: left">Motivo de Retiro </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlMotivoRetiro" runat="server" CssClass="textbox" Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnLiquidar" runat="server" CssClass="btn8" OnClick="btnLiquidar_Click" Text="Liquidar" Width="150px" />
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnAgregarNovedades" runat="server" Visible="false" CssClass="btn8" OnClick="btnAgregarNovedades_Click" Text="Agregar Novedad" Width="150px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: left">
                                    <strong>Resumen Liquidación</strong>
                                    <hr />
                                    </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                        </table>
                        <asp:HiddenField runat="server" ID="hiddenPrimaValorPagado" />
                        <asp:HiddenField runat="server" ID="hiddenPrimaDiasPagado" />
                        <asp:HiddenField runat="server" ID="hiddenCesantiasValorPagado" />
                        <asp:HiddenField runat="server" ID="hiddenCesantiasDiasPagado" />
                        <asp:HiddenField runat="server" ID="hiddenVacacionesValorPagado" />
                        <asp:HiddenField runat="server" ID="hiddenVacacionesDiasPagado" />
                        <asp:HiddenField ID="hiddenDiasLiquidacion" runat="server" />
                        <asp:Panel runat="server" ID="pnlLiquidacionHecha" Visible="false">
                            <table cellpadding="5" cellspacing="0" style="width: 90%">
                                <tr>
                                    <td colspan="6" style="padding: 10px">
                                        <asp:GridView ID="gvLiquidacion" runat="server" Width="60%" GridLines="Horizontal" AutoGenerateColumns="False"
                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                                            <Columns>
                                                <asp:BoundField DataField="desc_conceptoNomina" HeaderText="Descripcion">
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valorPago" HeaderText="Devengado" DataFormatString="${0:#,##0.00}">
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valorDescuento" HeaderText="Deducido" DataFormatString="${0:#,##0.00}">
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px; width: 15%;">
                                        <strong>Valor Total</strong>
                                    </td>
                                    <td style="padding: 10px; width: 15%;">
                                        <asp:TextBox runat="server" ID="txtValorTotal" Enabled="false" CssClass="textbox" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="padding: 10px; ">
                                        <uc3:ctlgiro ID="ctlGiro" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
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
        <asp:View runat="server" ID="vReportes">
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
                            <asp:Button ID="btnImprimirDesprendibles" runat="server" Height="30px" Text="Imprimir Formato Liquidación"
                                OnClick="btnImprimirDesprendibles_Click" CssClass="btn8" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="pnlReporte" Visible="false">
                                <rsweb:ReportViewer ID="rvReporteDesprendible" runat="server" Font-Names="Verdana" Visible="false"
                                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Height="600px" Width="100%">
                                    
                </rsweb:ReportViewer>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <br />
        <asp:View ID="viewComprobante" runat="server">
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
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
      <asp:ModalPopupExtender ID="mpeAgregarNovedades" runat="server" PopupControlID="panelActualizarNovedad"
                TargetControlID="hfFechaIni" BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
       <asp:HiddenField ID="hfFechaIni" runat="server" />
            <div style="text-align: center">
                <asp:Panel ID="panelActualizarNovedad" runat="server" BackColor="White" Style="text-align: left" Width="550px">
                    <div style="border: 2px groove #2E9AFE; width: 550px; background-color: #FFFFFF; padding: 10px">
                        <asp:UpdatePanel ID="upModificacionTasa" runat="server">
                            <ContentTemplate>
                                <table width="550px">
                                    <tr>
                                        <td class="gridHeader" style="text-align: center;" colspan="3">Agregar Novedad
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Codigo Empleado :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtCodigoEmpleadoModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                            <asp:HiddenField runat="server" ID="hiddenIndexSeleccionado" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Identificacion :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtIdentificacionModal" Enabled="false" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Nombres :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtNombresModal" Enabled="false" runat="server" CssClass="textbox" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Concepto :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlConceptoModal" CssClass="dropdown" AppendDataBoundItems="true" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlConceptoModal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px; height: 32px;">Valor :
                                        </td>
                                        <td style="text-align: left; width: 400px; height: 32px;" colspan="2">
                                            <asp:TextBox ID="txtValorModal" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="80%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Descripcion :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:TextBox ID="txtDescripcionModal" runat="server" CssClass="textbox" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Centro Costo :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlCentroCostoModal" CssClass="dropdown" AppendDataBoundItems="true" Width="80%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Nomina :
                                        </td>
                                        <td style="text-align: left; width: 400px;" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlNominaModal" Enabled="false" CssClass="dropdown" AppendDataBoundItems="true" Width="80%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Tipo:
                                        </td>
                                        <td style="text-align: left; width: 150px;" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkBoxValorModal" Text="Valor" TextAlign="Left" runat="server" Font-Size="Small" Width="100px" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkBoxCantidadModal" Text="Cantidad" TextAlign="Left" runat="server" Visible="false" Font-Size="Small" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            <asp:Label ID="lblmsjModalNovedadNueva" runat="server" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnAgregarNovedadModal" runat="server" Height="30px" Text="Guardar" Width="100px"
                                        OnClick="btnAgregarNovedadModal_Click" CssClass="btn8" />
                                    &#160;&#160;&#160;&#160;
                        <asp:Button ID="btnCloseReg1" runat="server" CssClass="btn8"
                            Height="30px" Text="Cancelar" Width="100px" OnClick="btnCloseReg1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>


</asp:Content>
