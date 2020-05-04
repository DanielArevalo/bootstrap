<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <%--<asp:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager2" />--%>
    <script type="text/javascript">
    </script>
    <asp:Panel ID="PanelSolicitud" runat="server" Width="650px">
        <table border="0" cellpadding="5" cellspacing="0" style="width: 128%">
            <tr>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align:left; width: 215px;">
                    &nbsp;</td>
                <td style="text-align: left; width: 178px;">
                    &nbsp;</td>
                <td style="text-align: left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    No.Solicitud<br />
                    <asp:TextBox ID="txtRadicacion" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
                <td style="text-align:left; width: 215px;">
                    Monto Solicitado<br />
                    <asp:TextBox ID="txtMonto" runat="server" BorderWidth="0px" CssClass="textbox" 
                        Enabled="False" Width="152px"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 178px;">
                    Cuota<br />
                    <asp:TextBox ID="txtCuota" runat="server" BorderWidth="0px" CssClass="textbox" 
                        Enabled="False" Width="122px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Plazo<br />
                    <asp:TextBox ID="txtPlazo" runat="server" BorderWidth="0px" CssClass="textbox" 
                        Enabled="False" Width="99px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelViabilidad" runat="server" Width="650px">
        <table border="0" cellpadding="5" cellspacing="0" style="width: 128%">
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>VIABILIDAD FINANCIERA</strong></td><asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
            </tr>
            <tr>
                <td class="columnForm50" style="font-size: small; width: 219px; text-align:left">
                    Rotación Cuentas por Pagar<br />
                    <span style="font-size: xx-small">(Activo Corriente - Inventarios / Pasivo Corriente)</span>
                </td>
                <td style="width: 110px">
                    <asp:TextBox ID="txtRotacionCP" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
                <td class="columnForm50" style="font-size: small; width: 219px; text-align:left">
                    Rotación Cuentas por Cobrar<br />
                    <span style="font-size: xx-small; width: 228px;">(Cuentas por Cobrar / Ventas 
                    Totales del mes)x30</span>
                </td>
                <td style="width: 110px">
                    <asp:TextBox ID="txtRotacionCC" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnForm50" style="font-size: small; width: 312px; text-align:left">
                    Relación Préstamo Patrimonio<br /> 
                    <span style="font-size: xx-small">Total Obigaciones/Patrimonio</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEF" runat="server" BorderWidth="0px" CssClass="textbox" 
                        Enabled="False" Width="99px"></asp:TextBox>
                </td>
                <td class="columnForm50" style="font-size: small; width: 312px; text-align:left">
                    Gastos Familiares<br /> 
                    <span style="font-size: xx-small">(Gastos Familiares - Otros Ingresos Familiares) / Utilidad Neta</span>
                </td>
                <td>
                    <asp:TextBox ID="txtGastosFamiliares" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnForm50" style="font-size: small; width: 219px; text-align:left">
                    Rotación de Inventarios<br />
                    <span style="font-size: xx-small">(Inventarios/Costo de Venta * 30 días)</span></td>
                <td style="width: 110px">
                    <asp:TextBox ID="txtRotacionInv" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
                <td class="columnForm50" style="font-size: small; width: 312px; text-align:left">
                    Rotación Capital de Trabajo<br /> 
                    <span style="font-size: xx-small">((Activo Corriente - Pasivo Corriente)/Costo de Ventas)x30</span>
                </td>
                <td>
                    <asp:TextBox ID="txtRotacionCap" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnForm50" style="font-size: small; width: 312px; text-align:left">
                    Endeudamiento Total<br /> 
                    <span style="font-size: xx-small">(Pasivo Total/Activo Total) x 100</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEndeudamientoTot" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
                <td class="columnForm50" style="font-size: small; width: 312px; text-align:left">
                    Punto de Equilibrio<br /> 
                    <span style="font-size: xx-small">(Costos Fijos Negocio + Gastos Fam. y Adm.)/Costo Ventas)x100</span>
                </td>
                <td>
                    <asp:TextBox ID="txtPuntoEquilibrio" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnForm50" 
                    style="font-size: small; width: 219px; text-align:left">
                    &nbsp;</td>
                <td style="width: 110px">
                    &nbsp;</td>
                <td style="font-size: small; width: 219px; text-align:left">
                    Valor Cuota / Disponible<br />                     
                </td>
                <td style="width: 110px">
                    <asp:TextBox ID="txtPruebaAcida" runat="server" BorderWidth="0px" 
                        CssClass="textbox" Enabled="False" Width="99px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnForm50" colspan="4" style="font-size: small; text-align:left">
                    <hr style="width: 828px" />
                </td>
            </tr>
        </table>
    </asp:Panel>                  
    <asp:Panel ID="Panel1" runat="server" Width="650px">
        <table border="0" cellpadding="5" cellspacing="0" style="width: 128%">
            <tr>
                <td class="columnForm50" style="font-size: small; text-align:left" colspan="2">
                    <strong>OPINION Y PROPUESTA DEL ANALISTA DE CREDITO</strong></td>
            </tr>
            <tr>
                <td class="columnForm50" style="font-size: small; text-align:left">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" 
                        Text="Se ha registrado la Opinion y Propuesta correctamente." Visible="False"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" 
                        ControlToValidate="txtObservaciones" Display="Dynamic" 
                        ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                        ValidationGroup="vgGuardar" />
                    <asp:Label ID="lblMensaje2" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td class="columnForm50" style="font-size: small; text-align:right">
                    <asp:ImageButton ID="btnGuardar" runat="server" 
                        ImageUrl="~/Images/btnGuardar.jpg" onclick="btnGuardar_Click" 
                        ValidationGroup="vgGuardar" />
                </td>
            </tr>
            <tr>
                <td class="columnForm50" style="font-size: small; text-align:left" colspan="2">
                    <asp:TextBox ID="txtObservaciones" runat="server" Height="103px" TextMode="MultiLine" 
                        Width="785px" MaxLength="250" ValidationGroup="vgGuardar"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>  
  </asp:Content>

