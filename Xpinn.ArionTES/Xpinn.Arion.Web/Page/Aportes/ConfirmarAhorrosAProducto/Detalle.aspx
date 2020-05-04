<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <br />
    <asp:FormView ID="frmDatos" runat="server" Width="100%">
        <ItemTemplate>
            <asp:Panel ID="panelDataBasic" runat="server" BorderWidth="2" BorderColor="#0066FF">
                <div style="text-align: center; color: #FFFFFF; background-color: #0066FF; width: 100%">
                    <strong>Datos de la solicitud</strong>
                </div>
                <table cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="text-align: left; width: 50%">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: left; width: 180px"><b>Fecha de Solicitud</b>
                                    </td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:Label ID="lblFechaSoli" runat="server" Text='<%# Eval("fecha_pago","{0:d}") %>' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left; width: 50%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100%" colspan="2">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: left; width: 180px"><b>Identificación</b>
                                    </td>
                                    <td colspan="2" style="text-align: left; width: 180px">
                                        <asp:Label ID="lblIdentificacion" runat="server" Text='<%# Eval("identificacion") %>' />
                                    </td>
                                    <td style="text-align: left; width: 180px"><b>Nombres</b>
                                    </td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:Label ID="lblNombres" runat="server" Text='<%# Eval("nombre") %>' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="text-align: left; width: 13%">Código
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("idcruceahorro") %>' />
                        </td>
                        <td style="text-align: left; width: 12%">Tipo de Producto
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblTipoProducto" runat="server" Text='<%# Eval("nom_tipo_producto") %>' />
                        </td>
                        <td style="text-align: left; width: 12%">Num Producto
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblNumProducto" runat="server" Text='<%# Eval("num_producto") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 13%">Tipo de Transacción
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblTipoTran" runat="server" Text='<%# Eval("nom_tipo_tran") %>' />
                        </td>
                        <td style="text-align: left; width: 12%">Número de Cuenta
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblNumeroCuenta" runat="server" Text='<%# Eval("numero_cuenta") %>' />
                        </td>
                        <td style="text-align: left; width: 12%">Valor a Cruzar
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblValorCruce" runat="server" Text='<%# Eval("valor_pago", "{0:n0}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 13%">Oficina
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblFechaNac" runat="server" Text='<%# Eval("nom_oficina") %>' />
                        </td>
                        <td style="text-align: left; width: 12%">Generado por
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblCiudadNac" runat="server" Text='<%# Eval("concepto") %>' />
                        </td>
                        <td style="text-align: left; width: 12%">Estado
                        </td>
                        <td style="text-align: left; width: 21%">
                            <asp:Label ID="lblCabezaFam" runat="server" Text='<%# Eval("nom_estado") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ItemTemplate>
    </asp:FormView>

</asp:Content>
