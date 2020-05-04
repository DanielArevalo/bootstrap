<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Style="margin-right: 0px"
        Width="1087px">
        <table cellpadding="5" cellspacing="0" style="width: 77%">
            <tr>
                <td style="text-align: left; font-size: x-small;" colspan="4">
                    <span style="color: #0099FF;">
                        <strong style="text-align: center; background-color: #FFFFFF;">
                            <asp:Label ID="LblMensaje" runat="server"
                                Style="color: #FF0000; font-weight: 700;"></asp:Label>
                        </strong></span></td>
            </tr>
            <tr>
                <td style="text-align: left; width: 188px;">Linea Aporte
                    <br />
                    <asp:DropDownList ID="DdlLineaAporte" runat="server" Width="280px" Enabled="False" CssClass="dropdown">
                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 134px;">Número Aporte
                    <asp:TextBox ID="txtgrupoaporte" runat="server" CssClass="textbox"
                        Enabled="False" Visible="False" Width="16px"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="192px"
                        Enabled="False"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 125px;">Oficina
                    <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox"
                        Enabled="False" Visible="False"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtOficinaNombre" runat="server" CssClass="textbox"
                        Enabled="False" Width="192px"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 180px;">Fecha Apertura&nbsp;<span style="font-size: xx-small">
                    <asp:RequiredFieldValidator
                        ID="rfvFechaApertura0" runat="server" ControlToValidate="txtFecha_apertura"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red"
                        SetFocusOnError="True" Style="font-size: x-small"
                        ValidationGroup="vgConsultar" />
                    <asp:CompareValidator ID="cvFecha_apertura" runat="server"
                        ControlToValidate="txtFecha_apertura" Display="Dynamic"
                        ErrorMessage="Formato de Fecha (dd/MM/yyyy)" lForeColor="Red"
                        Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small"
                        ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Width="133px" />
                </span>
                    <br />
                    <asp:TextBox ID="txtFecha_apertura" runat="server" CssClass="textbox"
                        MaxLength="128" Width="132px" AutoPostBack="True" Enabled="False" />
                    <asp:CalendarExtender ID="txtFecha_aperturaCalendarExtender" runat="server"
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_apertura">
                    </asp:CalendarExtender>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 188px;">Identificación<asp:TextBox ID="txtCodigoCliente" runat="server"
                    CssClass="textbox" Visible="False" Width="35px" Enabled="False"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox"
                        Width="275px" onkeyup="Actualizar()" Enabled="False"></asp:TextBox>
                    <span style="font-size: xx-small">
                        <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server"
                            ControlToValidate="txtNumeIdentificacion" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small" ValidationGroup="vgConsultar" />
                    </span>

                </td>

                <td style="text-align: left; width: 134px;">Tipo Identificacion<br />
                    <asp:DropDownList ID="DdlTipoIdentificacion" runat="server" Width="202px" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left;">Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="342px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 188px;">
                    <br />
                </td>
                <td style="text-align: left; width: 134px;">Valor Cuota<br />
                    <span style="font-size: xx-small">
                        <asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" Width="192px" Enabled="False"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtValorCuota_MaskedEditExtender" runat="server"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"
                            InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtValorCuota" />
                    </span>
                </td>
                <td style="text-align: left; width: 125px;">Periodicicidad<br />
                    <asp:DropDownList ID="DdlPeriodicidad" runat="server" Width="192px" Enabled="False">
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="text-align: left; width: 180px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="4">
                    <asp:MultiView ID="MvDistribucion" runat="server">
                        <asp:View ID="VDistribucion" runat="server">
                            <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" GridLines="Horizontal" PageSize="5"
                                ShowFooter="True" ShowHeaderWhenEmpty="True" Width="836px" OnRowDataBound="gvLista_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="porcentaje" HeaderText="%Distribución">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cuota" DataFormatString="{0:C}"
                                        HeaderText="Valor Base" SortExpression="cuota">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Principal">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chkprincipal" runat="server"
                                                Checked='<%#Convert.ToBoolean(Eval("principal")) %>' Enabled="False"
                                                EnableViewState="true" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pendiente Crear">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPendiente" runat="server"
                                                Checked='<%#Convert.ToBoolean(Eval("pendiente_crear")) %>' Enabled="False"
                                                EnableViewState="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 188px;">&nbsp;</td>
                <td style="text-align: center; width: 134px;">&nbsp;</td>
                <td style="text-align: center; font-weight: 700; width: 125px;">&nbsp;</td>
                <td style="text-align: left; width: 180px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; width: 188px; vertical-align: top">Forma Pago<br />
                    <asp:DropDownList ID="DdlFormaPago" runat="server" CssClass="dropdown"
                        Width="95%" OnSelectedIndexChanged="DdlFormaPago_SelectedIndexChanged" Enabled="false">
                        <asp:ListItem Value="1">Caja</asp:ListItem>
                        <asp:ListItem Value="2">Nomina</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                    <asp:DropDownList ID="ddlEmpresa" runat="server" Width="95%" CssClass="dropdown"
                        Enabled="false">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 134px; vertical-align: top">Fecha Prox. Pago<br />
                    <asp:TextBox ID="txtFecha_Proxppago" runat="server" CssClass="textbox"
                        Enabled="False" MaxLength="120" Width="120px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFecha_Proxppago_CalendarExtender" runat="server"
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_Proxppago">
                    </asp:CalendarExtender>
                    <span style="font-size: xx-small">
                        <br />
                        <asp:RequiredFieldValidator ID="rfvFechaProxPago" runat="server"
                            ControlToValidate="txtFecha_Proxppago" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small" ValidationGroup="vgConsultar" />
                    </span>
                    <asp:CompareValidator ID="cvFecha_proxpago" runat="server"
                        ControlToValidate="txtFecha_Proxppago" Display="Dynamic"
                        ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                        Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small"
                        ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Width="105px" />
                </td>
                <td style="text-align: left; width: 125px; vertical-align: top">Fecha Interes&nbsp;<br />
                    <asp:TextBox ID="txtFecha_interes" runat="server" CssClass="textbox"
                        Enabled="False" MaxLength="128" Width="120px" />
                    <asp:CalendarExtender ID="txtFecha_interes_CalendarExtender" runat="server"
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_interes">
                    </asp:CalendarExtender>
                    <br />
                    <span style="font-size: xx-small">
                        <asp:RequiredFieldValidator ID="rfvFechaInteres" runat="server"
                            ControlToValidate="txtFecha_interes" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small" ValidationGroup="vgConsultar" />
                    </span>
                    <asp:CompareValidator ID="cvFecha_interes" runat="server"
                        ControlToValidate="txtFecha_interes" Display="Dynamic"
                        ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red" Height="16px"
                        Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small"
                        ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Width="93px" />
                </td>
                <td style="text-align: left; width: 180px; vertical-align: top">Estado
                    <br />
                    <asp:DropDownList ID="DdlEstado" runat="server" Enabled="False" Width="139px">
                        <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                        <asp:ListItem Value="2">INACTIVO</asp:ListItem>
                        <asp:ListItem Value="3">CERRADO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 88%">
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>
    </table>

</asp:Content>
