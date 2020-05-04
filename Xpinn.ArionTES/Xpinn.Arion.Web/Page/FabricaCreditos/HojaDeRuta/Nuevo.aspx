<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Lista" Title=".: Xpinn - HojaDeRuta :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvControlTiempos" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Width="756px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 76%">
                    <tr>
                        <td colspan="4">
                            <strong>Datos del Cliente</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            Código<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 29%">
                            Identificación<br />
                            <asp:TextBox ID="txtIdenti" runat="server" CssClass="textbox" Enabled="False" 
                                Width="213px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="False" 
                                Width="290px"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 9px; width: 20%">
                            Teléfono<br />
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="height: 9px; width: 29%">
                            Nombre del Negocio<br />
                            <asp:TextBox ID="txtNombreNegocio" runat="server" CssClass="textbox" 
                                Enabled="False" Width="213px"></asp:TextBox>
                        </td>
                        <td colspan="2" style="height: 9px">
                            Dirección<br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" 
                                Enabled="False" Width="290px"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr style="font-weight: 700" width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <strong>Datos del Crédito</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; height: 25px;">
                            Número de Radicación<br />
                            <asp:TextBox ID="txtNumeroRadicacion" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                            <br />
                        </td>
                        <td style="width: 29%; height: 25px;">
                            Línea de Crédito<br />
                            <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Enabled="False" Width="95%"></asp:TextBox>
                        </td>
                        <td style="width: 16%; height: 25px;">
                            Monto<br />
                            <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Enabled="False" 
                                Width="150px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtMonto_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="999,999,999" MaskType="Number" TargetControlID="txtMonto">
                            </asp:MaskedEditExtender>
                        </td>
                        <td style="width: 18%; height: 25px;">
                            Plazo<br />
                            <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="False" 
                                Width="86px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtPlazo_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="999,999,999" MaskType="Number" TargetControlID="txtPlazo">
                            </asp:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            Fecha Solicitud<br />
                            <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            Asesor<br />
                            <asp:TextBox ID="txtNombreAsesor" runat="server" CssClass="textbox" 
                                Enabled="False" Width="98%"></asp:TextBox>
                            <br />
                        </td>
                        <td style="width: 18%;">
                            Cuota<br />
                            <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" Enabled="False" 
                                Width="86px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtCuota_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="999,999,999" MaskType="Number" TargetControlID="txtCuota">
                            </asp:MaskedEditExtender>
                            <asp:TextBox ID="txtAsesor" runat="server" AutoCompleteType="Disabled" 
                                CssClass="textbox" Enabled="False" Visible="False" Width="48px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr style="font-weight: 700" width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <strong>Seguimiento</strong>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#CCCCFF" style="height: 31px">
                            PROCESO ACTUAL
                        </td>
                        <td style="width: 29%; height: 31px;">
                            <asp:TextBox ID="txtEstadoActual" runat="server" 
                                CssClass="textbox" Enabled="False" Width="95%" Height="25px" />
                        </td>
                        <td style="width: 16%; height: 31px;">
                            <asp:Label ID="lblfechadatacredito0" runat="server" 
                                Text="Fecha Consulta Datacredito" Width="95%" Visible="False"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFechaConsultaData" runat="server" AutoPostBack="True" 
                                CssClass="selectedrow" MaxLength="1" ValidationGroup="vgGuardar" 
                                Width="95%" BackColor="#66FFFF" Font-Bold="True" Visible="False" />
                            <asp:CalendarExtender ID="txtFechaConsultaData_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaConsultaData">
                            </asp:CalendarExtender>
                        </td>
                        <td style="text-align: right; width: 18%; height: 31px;">
                            <asp:ImageButton ID="btnGrabarSegumiento" runat="server" 
                                ImageUrl="~/Images/btnGuardar.jpg" OnClick="btnGrabarSeguimiento_Click" 
                                ValidationGroup="vgGuardar" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" bgcolor="#99CCFF">
                            NUEVO PROCESO</td>
                        <td style="width: 29%">
                            <asp:DropDownList ID="ddlAccion" runat="server" CssClass="textbox" 
                                Height="41px" Width="98%" onselectedindexchanged="ddlAccion_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 16%">
                            Fecha y Hora Actual<br />
                            <asp:TextBox ID="txtFechaActual" runat="server" CssClass="textbox" 
                                Enabled="False" ReadOnly="True" Width="95%"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 18%;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            Observaciones:&nbsp;
                            <br />
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Height="18px"
                                TextMode="MultiLine" Width="745px"></asp:TextBox>
                            <asp:Label ID="lblMensajeGrabar" runat="server" Font-Size="Larger" 
                                ForeColor="Red" Text="Crèdito Aprobado/Negado Correctamente" Visible="False"></asp:Label>
                            <hr style="font-weight: 700" width="100%" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:MultiView ID="MultiViewRuta" runat="server">
                <asp:View ID="ViewSeguimientos" runat="server">
                    <strong>Histórico de Seguimientos</strong>
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                        DataKeyNames="IDCONTROL" HeaderStyle-CssClass="gridHeader" 
                        OnPageIndexChanging="gvLista_PageIndexChanging" onrowdatabound="gvLista_RowDataBound" 
                        PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Width="98%">
                        <Columns>
                            <asp:BoundField DataField="IDCONTROL" HeaderStyle-CssClass="gridColNo" 
                                ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo" />
                            <ItemStyle CssClass="gridColNo" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IDCONTROL" HeaderText="Id Control" Visible="false" />
                            <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Numero Radicación" 
                                Visible="false" />
                            <asp:BoundField DataField="CODTIPOPROCESO" HeaderText="Tipo Proceso" 
                                Visible="false" />
                            <asp:BoundField DataField="FECHAPROCESO" HeaderText="Fecha Proceso">
                            <ItemStyle Width="170px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod. Persona" 
                                Visible="false" />
                            <asp:BoundField DataField="COD_MOTIVO" HeaderText="Cod. Motivo" 
                                Visible="false" />
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo Proceso" />
                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" />
                            <asp:BoundField DataField="NIVEL" HeaderText="Nivel" Visible="false" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Motivo" HeaderText="Motivo" Visible="false" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblTotalRegsH" runat="server" />
                </asp:View>
                <asp:View ID="ViewNegar" runat="server">
                    <asp:Accordion ID="Accordion1" runat="server" 
                        ContentCssClass="accordionContenido" FadeTransitions="True" 
                        FramesPerSecond="50" HeaderCssClass="accordionCabecera" Height="285px" 
                        SelectedIndex="-1" style="margin-right: 6px; font-size: xx-small;" 
                        TransitionDuration="200" Width="792px">
                        <Panes>
                            <asp:AccordionPane ID="AccordionPane6" runat="server">
                                <Header>
                                    NEGAR CRÈDITO
                                </Header>
                                <Content>
                                    <table style="width: 100%; text-align: left">
                                        <tr>
                                            <td colspan="2" style="text-align: center">
                                                NEGAR CRÈDITO
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                No. Crédito:
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtCredito5" runat="server" CssClass="textbox" Enabled="false" 
                                                    Width="145px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCredito5" runat="server" 
                                                    ControlToValidate="txtCredito5" Display="Dynamic" 
                                                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                                    ValidationGroup="vgNegar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Motivo de negación:
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlNegar" runat="server" CssClass="dropdown" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                                &nbsp;<asp:CompareValidator ID="cvNegar" runat="server" ControlToValidate="ddlNegar" 
                                                    Display="Dynamic" ErrorMessage="Seleccione un motivo de negacion" 
                                                    ForeColor="Red" Operator="GreaterThan" SetFocusOnError="true" 
                                                    Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" ValidationGroup="vgNegar" 
                                                    ValueToCompare="0">
                                                </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Observación:
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtObs5" runat="server" CssClass="textbox" MaxLength="250" 
                                                    Width="145px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                    DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" 
                                                    ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgNegar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnAcpNegar" runat="server" CssClass="btn8" 
                                                    OnClick="btnAcpNegar_Click" Text="Aceptar" ValidationGroup="vgNegar" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnCncNegar" runat="server" CssClass="btn8" 
                                                    OnClick="btnCncNegar_Click" Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                </asp:View>
            </asp:MultiView>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            //document.getElementById('cphMain_txtNumeroCredido').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
