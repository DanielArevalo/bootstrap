<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" MasterPageFile="~/General/Master/site.master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphMain">

<table style="width:100%;">
            <tr>
                <td colspan="4">
                    DATOS DEL CLIENTE:<br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    Código<br />
                    <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Tipo<br />
                    <asp:TextBox ID="txtTipoIdCliente" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False"></asp:TextBox>
&nbsp;No.</td>
                <td>
                    Identificación<br />
                    <asp:TextBox ID="txtIdentificacionCliente" runat="server" CssClass="textbox" 
                        Width="238px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    Estado<br />
                    <asp:TextBox ID="txtEstadoCliente" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Primer nombre<br />
                    <asp:TextBox ID="txtPrimerNombreCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Segundo nombre<br />
                    <asp:TextBox ID="txtSegundoNombreCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    Primer apellido<br />
                    <asp:TextBox ID="txtPrimerApellidoCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    Segundo apellido<br />
                    <asp:TextBox ID="txtSegundoApellidoCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Dirección<br />
                    <asp:TextBox ID="txtDireccionCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Teléfono<br />
                    <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    E-mail<br />
                    <asp:TextBox ID="txtEmailCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    Calificación cliente<br />
                    <asp:TextBox ID="txtCalificacionCliente" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Zona<br />
                    <asp:TextBox ID="txtZonaCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Oficina<br />
                    <asp:TextBox ID="txtOficinaCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td colspan="2">
                    Ejecutivo<br />
                    <asp:TextBox ID="txtEjecutivoCliente" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="width: 216px">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                    <td >
                        &nbsp;</td>
            </tr>            
            <tr>
                <td colspan="4">
                    DATOS DEL CRÉDITO:<br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    No. Crédito<br />
                    <asp:TextBox ID="txtIdCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Línea<br />
                    <asp:TextBox ID="txtLineaCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td >
                    Fecha solicitud<br />
                    <asp:TextBox ID="txtFechaSolicitudCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                    <td >
                        Monto crédito<br />
                    <asp:TextBox ID="txtMontoCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Proceso<br />
                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                <asp:CompareValidator ID="cvProceso" runat="server" 
                    ControlToValidate="ddlProceso" Display="Dynamic" 
                    ErrorMessage="Seleccione un proceso" ForeColor="Red" 
                    Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                    ValidationGroup="vgGuardar" ValueToCompare="0">
                </asp:CompareValidator>
                    <br />
                </td>
                <td style="width: 216px">
                    Usuario<br />
                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                     <asp:CompareValidator ID="cvUsuario" runat="server" 
                    ControlToValidate="ddlUsuario" Display="Dynamic" 
                    ErrorMessage="Seleccione un usuario" ForeColor="Red" 
                    Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                    ValidationGroup="vgGuardar" ValueToCompare="0">
                </asp:CompareValidator>
                    <br />
                </td>
                <td >
                    Motivo<br />
                    <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="dropdown">
                    </asp:DropDownList>
                    <br />
                     <asp:CompareValidator ID="cvMotivo" runat="server" 
                    ControlToValidate="ddlMotivo" Display="Dynamic" 
                    ErrorMessage="Seleccione un motivo" ForeColor="Red" 
                    Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                    ValidationGroup="vgGuardar" ValueToCompare="0">
                </asp:CompareValidator>
                    <br />
                </td>
                    <td >
                    Ejecutivo<br />
                    <asp:TextBox ID="txtEjecutivoCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Plazo<br />
                    <asp:TextBox ID="txtPlazoCredito" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Cuota<br />
                    <asp:TextBox ID="txtCuotaCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td >
                    Cuotas pagadas<br />
                    <asp:TextBox ID="txtCuotasPagadasCredito" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False"></asp:TextBox>
                </td>
                    <td >
                        Calificación<br />
                    <asp:TextBox ID="txtCalificacionCredito" runat="server" CssClass="textbox" Width="89px" 
                            Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Fecha terminación<asp:TextBox ID="txtFechaTerminacionCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 216px">
                    Fecha ultimo pago<br />
                    <asp:TextBox ID="txtFechaUltimoPagoCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                <td >
                    Ultimo valor pagado<br />
                    <asp:TextBox ID="txtUltimoValorPagadoCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
                    <td >
                    Fecha próximo pago<br />
                    <asp:TextBox ID="txtFechaProximoPagoCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                        Saldo<br />
                    <asp:TextBox ID="txtSaldoCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                    </td>
                <td style="width: 216px; height: 25px;">
                        Valor a pagar<asp:TextBox ID="txtValorPagarCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                    </td>
                <td style="height: 25px" >
                    Saldo en mora<asp:TextBox ID="txtSaldoMoraCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="height: 25px" >
                    Valor total a pagar<asp:TextBox ID="txtValorTotalPagarCredito" runat="server" CssClass="textbox" 
                        Width="216px" Enabled="False"></asp:TextBox>
                    </td>
            </tr>
            </table>
</asp:Content>
