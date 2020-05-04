<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../../General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="../../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="../../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc3" %>
<%@ Register src="../../../../General/Controles/fechaeditable.ascx" tagname="Fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Style="margin-right: 0px" Width="866px">
        <table cellpadding="5" cellspacing="0" style="width: 98%">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;">
                    <span style="color: #0099FF;"><strong style="text-align: center; background-color: #FFFFFF;">
                        <asp:Label ID="LblMensaje" runat="server" Style="color: #FF0000; font-weight: 700;"
                            Font-Size="Larger"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:MultiView ID="MvAfiliados" runat="server">
                        <asp:View ID="vwSolicitudCredito" runat="server">
                            <table cellpadding="5" cellspacing="0" width="80%">
                                <tr>
                                    <td class="tdI" style="text-align: center">
                                        <asp:Panel ID="Panel4" runat="server" Width="822px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="4" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                                                        height: 20px;">
                                                        <strong style="text-align: center">SOLICITUD DE CREDITO ROTATIVO</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcredito" runat="server" Text="Número Crédito"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="lblNumeroCreditoRad" runat="server" CssClass="textbox" 
                                                            Enabled="False" Visible="False" Width="153px"></asp:TextBox>
                                                        <br />
                                                    </td>
                                                    <td>
                                                        Fecha de Solicitud *<br />
                                                        <ucFecha:Fecha ID="txfechasolicitud" runat="server" Enabled="false"/>
                                                    </td>
                                                    <td style="width: 203px">
                                                        Oficina<br />
                                                        <asp:TextBox ID="lblOficina" runat="server" CssClass="textbox" Enabled="False" 
                                                            Width="153px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table style="width: 825px;">
                                                <tr>
                                                    <td colspan="2" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                                                        height: 20px;">
                                                        &nbsp;<strong style="text-align: center">SOLICITANTE</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Identificación<br />
                                                        <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Enabled="false"
                                                            Width="153px"></asp:TextBox>
                                                        <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" 
                                                            Visible="False" Width="35px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Nombre<br />
                                                        <asp:TextBox ID="txtNombreCLiente" runat="server" CssClass="textbox" 
                                                            Enabled="False" OnTextChanged="txtNombreCLiente_TextChanged" Width="282px"></asp:TextBox>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table style="width: 100%; text-align: left;">
                                                            <tr>
                                                                <td align="left" colspan="3" 
                                                                    style="color: #FFFFFF; background-color: #0066FF; height: 25px;">
                                                                    <strong>Condiciones Solicitadas Del Crédito</strong>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Tipo de Crédito<br />
                                                                    <asp:DropDownList ID="ddlTipoCredito" runat="server" AutoPostBack="True" 
                                                                        CssClass="textbox" OnSelectedIndexChanged="ddlTipoCredito_TextChanged" 
                                                                        OnTextChanged="ddlTipoCredito_TextChanged" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Monto Máximo<br />
                                                                    <asp:TextBox ID="txtMontoMaximo" runat="server" CssClass="textbox" 
                                                                        Enabled="False" Width="153px"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 249px">
                                                                    Plazo Máximo<br />
                                                                    <asp:TextBox ID="txtPlazoMaximo" runat="server" CssClass="textbox" 
                                                                        Enabled="False" Width="136px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Monto Solicitado<br />
                                                                    <uc2:decimales ID="txtMonto" runat="server" />
                                                                </td>
                                                                <td>
                                                                    Plazo<br />
                                                                    <uc2:decimales ID="txtPlazo" runat="server" />
                                                                    <br />
                                                                </td>
                                                                <td style="width: 249px">
                                                                    Periodicidad<br />
                                                                    <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" 
                                                                        OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" Width="148px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    Observaciones<br />
                                                                    <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" MaxLength="50" 
                                                                        Width="803px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Asesor Comercial<br />
                                                                    <asp:DropDownList ID="Ddlusuarios" runat="server" CssClass="dropdown" 
                                                                        Width="260px">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <span style="font-size: xx-small">
                                                                    <asp:RequiredFieldValidator ID="rfvAsesor0" runat="server" 
                                                                        ControlToValidate="Ddlusuarios" Display="Dynamic" 
                                                                        ErrorMessage="Campo Requerido" ForeColor="Red" 
                                                                        InitialValue="&lt;Seleccione un Item&gt;" SetFocusOnError="True" 
                                                                        Style="font-size: x-small" ValidationGroup="vgGuardar" />
                                                                    </span>
                                                                </td>
                                                                <td>
                                                                    Forma de Pago<br />
                                                                    <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" 
                                                                        CssClass="dropdown" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" 
                                                                        Width="225px">
                                                                        <asp:ListItem Value="1">Caja</asp:ListItem>
                                                                        <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 249px">
                                                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="dropdown" 
                                                                        Width="222px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblTipoLiquidacion" runat="server" Text="Tipo de Liquidacion" 
                                                                        Visible="False"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="dropdown" 
                                                                        Width="260px" Visible="false">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTipoLiquidacion0" runat="server" 
                                                                        ControlToValidate="ddlTipoLiquidacion" Display="Dynamic" 
                                                                        ErrorMessage="Campo Requerido" ForeColor="Red" 
                                                                        InitialValue="&lt;Seleccione un Item&gt;" SetFocusOnError="True" 
                                                                        Style="font-size: x-small" ValidationGroup="vgGuardar" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 249px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                        <uc2:decimales ID="txtMontomax" runat="server" Visible="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="vwMensajes" runat="server">
                            <table style="width: 816px">
                                <tr>
                                    <td style="text-align: center; width: 239px; height: 49px;">
                                    </td>
                                    <td style="text-align: center; width: 239px; height: 49px;">
                                        <asp:Label ID="lblMensaje2" runat="server" Width="228px"></asp:Label>
                                    </td>
                                    <td style="text-align: center; width: 74%; height: 49px;">
                                        Número Crédito<br />
                                        <asp:TextBox ID="lblNumeroCredito" runat="server" CssClass="textbox" 
                                            Enabled="False" Width="153px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center; width: 74%; height: 49px;">
                                        Número de Solicitud
                                        <br />
                                        <asp:TextBox ID="lblNumeroSolicitud" runat="server" CssClass="textbox" 
                                            Enabled="False" Width="153px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        &nbsp;
                                        <asp:Button ID="btnPlanPagos" runat="server" OnClick="btnPlanPagosClick" 
                                            Text="Ir a Plan de Pagos" visible="false" />
                                        &nbsp;<asp:Button ID="btnAprobacion" runat="server" OnClick="btnAprobacionClick" 
                                            Text="ir a Aprobación" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            </asp:Panel>
</asp:Content>
