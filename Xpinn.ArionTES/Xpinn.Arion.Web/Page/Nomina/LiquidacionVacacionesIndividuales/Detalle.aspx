<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register src="../../../General/Controles/ctlGiro.ascx" tagname="ctlgiro" tagprefix="uc3" %>
<%@ Register src="../../../General/Controles/ctlProcesoContable.ascx" tagname="procesocontable" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    51<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table cellpadding="5" cellspacing="0" style="width: 90%">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="4" style="text-align: left; padding: 10px">
                                            <asp:Label ID="lblMensajenoliquida" runat="server" Style="color: #FF3300" ></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 15%; padding: 10px">&nbsp;</td>
                                        <td style="text-align: center; width: 15%; padding: 10px">
                                            &nbsp;</td>
                                    </tr>
                                     
                                    <tr>
                                        <td colspan="4" style="text-align: left; padding: 10px"><strong>Registro Liquidación Vacaciones Individuales</strong> 
                                            <asp:TextBox ID="txtCodigoPersona" runat="server" CssClass="textbox" Enabled="false" Visible="false"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center; width: 15%; padding: 10px">Código Liquidación</td>
                                        <td style="text-align: center; width: 15%; padding: 10px">
                                            <asp:TextBox ID="txtCodigoLiquidacion" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                     
                        <tr>
                            <td style="text-align: left; width: 442px; height: 34px;">Identificación </td>
                            <td style="text-align: left; width: 689px; height: 34px;">
                                <asp:TextBox ID="txtIdentificacionn" runat="server" CssClass="textbox" Enabled="false" Width="90%"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 739px; height: 34px;">Tipo Identificación </td>
                            <td style="text-align: left; width: 884px; height: 34px;">
                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 194px; height: 34px;">Código Empleado </td>
                            <td style="text-align: left; width: 140px; height: 34px;">
                                <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox"  Width="90%" Enabled="false"></asp:TextBox>
                            </td>
                      </tr>

                                    <tr>
                                        <td style="text-align: left; width: 442px; height: 34px;">Nombres y Apellidos </td>
                                        <td colspan="5" style="text-align: left; height: 34px;">
                                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false" Width="98%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 442px; height: 34px;">Nómina </td>
                                        <td style="text-align: left; width: 689px; height: 34px;">
                                            <asp:DropDownList ID="ddlTipoNomina" runat="server" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged" Width="90%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 739px; height: 34px;">Centro de Costo </td>
                                        <td style="text-align: left; width: 884px; height: 34px;">
                                            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Width="90%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 194px; height: 34px;">
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 140px; height: 34px;">
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left; height: 31px;">Fecha Inicio Periodo&nbsp; Liquidar</td>
                                        <td style="text-align: left; width: 739px; height: 31px;">
                                            <asp:TextBox ID="txtFechaInicioPeriodo" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                     
                                            &nbsp;</td>
                                        <td colspan="2" style="text-align: left; height: 31px;">Fecha Terminación Periodo Liquidar</td>
                                        <td style="text-align: left; width: 140px; height: 31px;">
                                            <asp:TextBox ID="txtFechaTerminacionPeriodo" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>

                                            &nbsp;</td>
                                    </tr>
                                    </table>
                                   
                                    <tr>
                                        <td style="text-align: left; height: 34px;">
                                            <hr />
                                        </td>
                                    </tr>
                                    </table>
              <asp:Panel runat="server" ID="PaneldatosLiquida" Visible="false" Width="710px">
                                 <table cellpadding="5" cellspacing="0" style="width: 105%">
                                    <tr>
                                        <td style="text-align: left; width: 280px; height: 34px;">
                                            <asp:Label ID="LabelFechaInicio" runat="server" Text="Fecha Inicio"></asp:Label>
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 280px; height: 34px;">
                                            <asp:TextBox ID="txtFechaInicio" runat="server" AutoPostBack="true" CssClass="textbox" MaxLength="10" Width="70%" OnTextChanged="txtFechas_TextChanged" ></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioInicio" TargetControlID="txtFechaInicio">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioInicio" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="text-align: left; width: 140px; height: 34px;">
                                            <asp:Label ID="LabelFechaTerminacion" runat="server" Text="Fecha Terminación"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 1027px; height: 34px;">
                                            <asp:TextBox ID="txtFechaTerminacion" runat="server" AutoPostBack="true" CssClass="textbox" MaxLength="10"  Width="70%"  ></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioTerminacion" TargetControlID="txtFechaTerminacion">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioTerminacion" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="text-align: left; width: 194px; height: 34px;">
                                            <asp:Label ID="LabelDias" runat="server" Text="Dias"></asp:Label>
                                            &nbsp;legales</td>
                                        <td style="text-align: left; width: 140px; height: 34px;">
                                            <asp:TextBox ID="txtDias" runat="server" CssClass="textbox" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 280px; height: 34px;">
                                            <asp:Label ID="LabelDiasDisfrutar" runat="server" Text="Dias a Disfrutar"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 280px; height: 34px;">
                                            <asp:TextBox ID="txtDiasDisfrutar" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="true" OnTextChanged="txtFechas_TextChanged" />
                                        </td>
                                        <td style="text-align: left; width: 140px; height: 34px;">
                                            <asp:Label ID="LabelFechaRegreso" runat="server" Text="Fecha Regreso"></asp:Label>
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 1027px; height: 34px;">
                                            <asp:TextBox ID="txtFechaRegreso" runat="server" AutoPostBack="true" CssClass="textbox" MaxLength="10" Enabled="false" Width="70%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtenderRegreso" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioRegreso" TargetControlID="txtFechaRegreso">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioRegreso" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                        </td>
                                        <td style="text-align: left; height: 34px;">
                                            <asp:Label ID="LabelFechaPago" runat="server" Text=" Fecha Pago"></asp:Label>
                                        </td>
                                        <td style="text-align: left; height: 34px;">
                                            <asp:TextBox ID="txtFechaPago" runat="server" CssClass="textbox" MaxLength="10" Width="70%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendarioFechaPago" TargetControlID="txtFechaPago">
                                            </asp:CalendarExtender>
                                            <img id="imagenCalendarioFechaPago" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                        </td>
                                       
                                    </tr>
                                     <tr>
                                         <td style="text-align: left; width: 280px; height: 34px;">
                                             <asp:Label ID="LabelDiaspendxpagar0" runat="server" Text="Dias Pendientes"></asp:Label>
                                             &nbsp;X Liquidar
                                         </td>
                                         <td style="text-align: left; width: 280px; height: 34px;">
                                             <asp:TextBox ID="txtDiaspendientes" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="true" OnTextChanged="txtDiaspendientes_TextChanged" />
                                         </td>
                                         <td style="text-align: left; width: 140px; height: 34px;">Dias Pagados</td>
                                         <td style="text-align: left; width: 1027px; height: 34px;">
                                             <asp:TextBox ID="txtDiasPagados"  Enabled="false"  runat="server" AutoPostBack="true" CssClass="textbox" OnTextChanged="txtDiasPagados_TextChanged"  />
                                             <asp:Label ID="lblMensajeDias" Visible="false" runat="server" Style="color: #FF3300"></asp:Label>
                                         </td>
                                         <td colspan="2" style="text-align: left; height: 34px;">
                                             <asp:Button ID="btnLiquidar" runat="server" CssClass="btn8" OnClick="btnLiquidar_Click" Text="Liquidar" Width="150px" />
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; width: 280px; height: 34px;">
                                             <asp:Label ID="LabelDiasPendientes" runat="server" Text="Dias Novedades"></asp:Label>
                                         </td>
                                         <td style="text-align: left; width: 280px; height: 34px;">
                                             <asp:TextBox ID="txtDiasNovedades" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="false" />
                                         </td>
                                         <td style="text-align: left; width: 140px; height: 34px;">
                                             <asp:TextBox ID="txtSalario" runat="server" CssClass="textbox" Enabled="false" Visible="false" />
                                         </td>
                                         <td style="text-align: left; width: 1027px; height: 34px;">Total Dias Liquidados</td>
                                         <td colspan="2" style="text-align: left; height: 34px;">
                                             <asp:TextBox ID="txtDiastotalpagados" runat="server" CssClass="textbox" Enabled="false" />
                                             <asp:TextBox ID="txtvacacantic" runat="server" CssClass="textbox" Enabled="false" Visible="false" />
                                             <br />
                                             <asp:TextBox ID="txtSalarioAdicional" runat="server" CssClass="textbox" Enabled="false" Visible="false" />
                                         </td>
                                     </tr>
                                </table>
                            </td>
                        </tr>
             </table>
 </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLiquidacionHecha" Visible="false">
                        <table cellpadding="5" cellspacing="0" style="width: 89%">
                            <tr>
                                <td colspan="6" style="padding: 10px">
                                    <strong>Resumen Liquidación</strong>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="padding: 10px">
                                    <asp:GridView ID="gvVacaciones" runat="server" Width="60%" GridLines="Horizontal" AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                                        <Columns>
                                            <asp:BoundField DataField="desc_concepto" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="${0:#,##0.00}">
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
                                <td>
                                    <asp:TextBox ID="txtusuariocreacion" runat="server" CssClass="textbox" Enabled="false" Visible="false" />
                                </td>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 89%;">
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
                <table style="width: 89%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnImprimirDesprendibles" runat="server" Height="30px" Text="Imprimir Desprendible"
                                        OnClick="btnImprimirDesprendibles_Click" CssClass="btn8" />
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
                            </asp:Panel>
                        </td>
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
</asp:Content>
