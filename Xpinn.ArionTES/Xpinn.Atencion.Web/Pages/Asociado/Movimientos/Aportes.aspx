<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Aportes.aspx.cs" 
    Inherits="Aportes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
     namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .fontSizeXSmall {
            font-size: x-small;
        }

        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="form-group">
        
         <asp:FormView ID="frvData" runat="server" Width="100%">
                <ItemTemplate>
                    <table class="col-sm-12 tableNormal">
                        <tr>
                            <td style="display: flex; justify-content: space-between">
                                <h4 class="text-primary text-left" style="padding-left:25px">Información del Producto</h4>
                                <%--<a class="btn btn-primary" href="#" style="height: fit-content;">Volver</a>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Código
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("cod_persona") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Identificación
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Label ID="lblIdentificacion" runat="server" Text='<%# Eval("identificacion") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Nombre
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Dirección
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblDireccion" runat="server" Text='<%# Eval("direccion") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Teléfono
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblTelefono" runat="server" Text='<%# Eval("telefono") %>' />
                                    </div>
                                    <div class="col-sm-2">&nbsp;</div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Tipo de Producto
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblTipoProducto" runat="server" Text='<%# Eval("tipocontrato") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Num. Producto
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblNumProducto" runat="server" Text='<%# Eval("descripcion") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Estado Producto
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("estado") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Fecha Apertura
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblFecApertura" runat="server" Text='<%# Eval("fechacreacion", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left fontSizeXSmall text-primary">
                                        Linea Producto
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblLinea" runat="server" Text='<%# Eval("medio") %>' />
                                    </div>
                                    <div class="col-sm-2">
                                        &nbsp;
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="border-color: #2780e3;" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>


        <asp:FormView ID="frmAportes" runat="server" Width="100%" Visible="false">
                <ItemTemplate>
                    <table class="col-sm-12 tableNormal">
                        <tr>
                            <td style="display: flex; justify-content: space-between">
                                <h4 class="text-primary text-left" style="padding-left:25px">Información del Producto</h4>
                                <a class="btn btn-primary"  href="../EstadoCuenta/Detalle.aspx" style="height: fit-content;">Volver</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Número
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("numero_aporte") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Código linea
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="lblIdentificacion" runat="server" Text='<%# Eval("cod_linea_aporte") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Nombre
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nom_linea_aporte") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Fecha apertura
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("fecha_apertura", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Valor cuota
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("cuota" , "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Forma de pago
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("nom_forma_pago") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Valor en mora
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("valor_a_pagar" , "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Próximo pago
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("fecha_proximo_pago", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Último pago
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("fecha_ultimo_pago" , "{0:d}") %>' />
                                    </div>                                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Saldo
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Saldo" , "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Rendimientos
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("valor_acumulado", "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-4 text-left text-primary">
                                    </div>                                  
                                </div>
                            </td>
                        </tr>

                        
                        <tr>
                            <td>
                                <hr style="border-color: #2780e3;" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>

        <asp:Panel runat="server" ID="pnlFiltros">
        <table style="width: 100%">
            <tr>
                <td class="text-left text-primary fontSizeXSmall text-danger">
                    <div class="col-sm-12" style="padding-left:20px">
                        <strong>Seleccione un rango diferente a consultar</strong>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td>
                    <div class="col-md-12">
                        <div class="col-md-1 text-left text-bold fontSizeXSmall">
                            Fecha Inicial:
                        </div>
                        <div class="col-md-2">
                            <ucFecha:fecha ID="txtFecIni" runat="server" />
                        </div>
                        <div class="col-md-1 text-left text-bold fontSizeXSmall">
                            Fecha Final:
                        </div>
                        <div class="col-md-2">
                            <ucFecha:fecha ID="txtFecFin" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultar" OnClick="btnConsultar_Click1" CssClass="btn btn-primary" Text="Consultar" />
                        </div>
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="pGrilla" runat="server">
            <hr />
            <div style="overflow: scroll; max-height: 550px; max-width: 100%;">
                <asp:GridView ID="gvMovimiento" runat="server" Width="100%"
                    CssClass="table table-striped" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                    SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small">
                    <Columns>
                        <asp:BoundField DataField="num_producto" HeaderText="Número de Produc" Visible="false" />
                        <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" Visible="false"/>
                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago"></asp:BoundField>
                        <asp:BoundField DataField="dias_mora" HeaderText="Días Mora">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoOperacion" HeaderText="No Operac" />
                        <asp:BoundField DataField="num_comp" HeaderText="Num Comprobante">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comprobante">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Transaccion" HeaderText="Transacción" />
                        <asp:BoundField DataField="tipomovimiento" HeaderText="Tipo  Mov" />
                        <asp:BoundField DataField="capital" HeaderText="Capital" DataFormatString="{0:c0}" ItemStyle-Width="90">
                            <ItemStyle HorizontalAlign="Right" Width="95" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IntCte" HeaderText="Int Corriente" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IntMora" HeaderText="Int Mora" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="seguros" HeaderText="Seguros" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ivaMiPyme" HeaderText="Iva MiPyme" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Prejuridico" HeaderText="Pre-Juridico" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataFormatString="{0:c0}" HeaderText="Total Pagado">
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="center" Width="100" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>
                <br />                
            </div>

            <table style="width: 100%">
                <tr>
                    <td class="text-center">
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>       

        <asp:Panel ID="panelReport" runat="server" Visible="false">
            <rsweb:reportviewer id="Rpview1" runat="server" width="100%"
                    font-names="Verdana" font-size="8pt" interactivedeviceinfos="(Colección)"
                    waitmessagefont-names="Verdana" waitmessagefont-size="14pt" height="450px">
                    <LocalReport ReportPath="Pages\Asociado\EstadoCuenta\ReportMovimiento.rdlc">
                    </LocalReport>
                </rsweb:reportviewer>
        </asp:Panel>
    </div>
</asp:Content>

