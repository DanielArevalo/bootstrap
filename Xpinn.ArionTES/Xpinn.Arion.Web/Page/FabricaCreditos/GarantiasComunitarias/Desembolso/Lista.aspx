<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="garantiascomunitarias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="cuFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="gvDiv">
        <table style="width: 100%">
            <tr>
                <td colspan="3" style="text-align:left">
                    <asp:Label ID="Label1" runat="server" Text="Seleccione un Rango de Fechas" 
                        Visible="true" style="font-weight: 700"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Inicial"></asp:Label><br />
                    <ucFecha:fecha ID="ucFecha" runat="server" style="text-align: center" />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="Fecha Final"></asp:Label><br />
                    <ucFecha:fecha ID="ucFecha0" runat="server" style="text-align: center" />
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:left">
                    <strong>Escoja la oficina para la cual desea generar el reporte:
                    </strong>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="True" 
                        CssClass="dropdown" Height="20px" Width="344px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>

        <table width="100%">
            <tr>
                <td style="text-align:left">
                    <div id="DivButtons">
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportarExcel_Click"
                            Text="Exportar a Excel" Width="124px" Height="28px" Visible="false"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
        </table>

        <table style="width: 100%">
            <tr>
                <td>
                     <div style="overflow:scroll;height:500px;width:1000px;">
                        <div style="width: 100%;">
                            <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="3"
                                GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvMovGeneral_PageIndexChanging" SelectedRowStyle-Font-Size="XX-Small"
                                Style="font-size: XX-Small; margin-bottom: 0px;">
                                <Columns>
                                    <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Número del Crédito" />
                                    <asp:BoundField DataField="CONVENIO" HeaderText="Convenio" />
                                    <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="línea de Crédito" />
                                    <asp:BoundField DataField="ENTIDAD" HeaderText="Nombre de la Entidad" />
                                    <asp:BoundField DataField="NITENTIDAD" HeaderText="Nit de la Entidad" />
                                    <asp:BoundField DataField="OFICINA" HeaderText="Sucursal" />
                                    <asp:BoundField DataField="DESEMBOLSOFECHA" HeaderText="Mes de Reporte" DataFormatString="{0:MMM-yyyy}" />
                                    <asp:BoundField DataField="NOMBRES" HeaderText="Nombres/Razon social" />
                                    <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                    <asp:BoundField DataField="IDENTIFICACION" HeaderText="Cedula/Nit" />
                                    <asp:BoundField DataField="SUCURSALOFICINA" HeaderText="Sucursal de Desembolso" />
                                    <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                                    <asp:BoundField DataField="NOMCIUDAD" HeaderText="Direccion del Cliente" />
                                    <asp:BoundField DataField="BARRIO" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="ACTIVIDAD" HeaderText="Departamento" />
                                    <asp:BoundField DataField="PAGARE" HeaderText="Ciiu" />
                                    <asp:BoundField DataField="PAGARE2" HeaderText="Numero de Pagaré" />
                                    <asp:BoundField DataField="MONTO_APROBADO" HeaderText="Valor  del Credito" />
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Destino del Crédito" />
                                    <asp:BoundField DataField="VALOR_CUOTA" HeaderText="Valor Cuota" />
                                    <asp:BoundField DataField="NUMERO_CUOTAS" HeaderText="Plazo" />
                                    <asp:BoundField DataField="FECHA_DESEMBOLSO" HeaderText="Fecha de Desembolso" DataFormatString="{0:dd-MM-yyyy}"  />
                                    <asp:BoundField DataField="FECHA_VENCIMIENTO" HeaderText="Fecha de Vencimiento" DataFormatString="{0:dd-MM-yyyy}"  />
                                    <asp:BoundField DataField="TIPOPRODUCTO" HeaderText="Tipo Producto" />
                                    <asp:BoundField DataField="PERIODO_GRACIA" HeaderText="Periodo Gracia" />
                                    <asp:BoundField DataField="CAPITAL" HeaderText="Amortización Capital" />
                                    <asp:BoundField DataField="PERIODO" HeaderText="Amortización Periodo" />
                                    <asp:BoundField DataField="ASMODALIDAD" HeaderText="Amortización Modalidad" />
                                    <asp:BoundField DataField="TASA" HeaderText="Tasa Interés" />
                                    <asp:BoundField DataField="PORCENTAJE" HeaderText=" Porcentaje comisión" />
                                    <asp:BoundField DataField="MODADLIDADCOMISION" HeaderText="Modalidad Comisión" />
                                    <asp:BoundField DataField="TIPOCOMISION" HeaderText="Tipo Comisión" />
                                    <asp:BoundField DataField="VALORCOMISION" HeaderText="Valor Comisión" />
                                    <asp:BoundField DataField="VALORIVACOMISION" HeaderText="Valor iva Comisión" />
                                    <asp:BoundField DataField="VALORTOTALCOMISION" HeaderText="Valor Total Comisión" />
                                    <asp:BoundField HeaderText="Espesificacion de la Garantia" />
                                    <asp:BoundField DataField="NOMCODEUDOR" HeaderText="Garantia Adicional Nombres Persona" />
                                    <asp:BoundField DataField="APELLIDOCODEUDOR" HeaderText="Garantia Adicional Apellidos Persona" />
                                    <asp:BoundField DataField="IDENTIFICACIONCODEUDOR" HeaderText="Garantia Adicional Nit/CC" />
                                    <asp:BoundField DataField="SUCURSAL" HeaderText="Garantia Adicional Sucursal Persona" />
                                    <asp:BoundField DataField="TELEFONOCODEUDOR" HeaderText="Garantia Adicional Telefono Persona" />
                                    <asp:BoundField DataField="DIRECCIONCODEUDOR" HeaderText="Garantia Adicional Direccion Persona" />
                                    <asp:BoundField DataField="CIUCODEUDOR" HeaderText="Garantia Adicional Ciudad Persona" />
                                    <asp:BoundField HeaderText="Garantia Adicional Departamento" />
                                    <asp:BoundField HeaderText="Observaciones" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />                                                             
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
