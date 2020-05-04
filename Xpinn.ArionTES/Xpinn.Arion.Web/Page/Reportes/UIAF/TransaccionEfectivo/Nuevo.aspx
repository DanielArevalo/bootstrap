<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvConfAporte" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="PanelDatos" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left;">Fecha Inicial<br />
                            <ucFecha:fecha ID="txtFecIni" runat="server" style="text-align: center" />
                        </td>
                        <td style="text-align: left; width: 150px;">Fecha de Corte<br />
                            <ucFecha:fecha ID="ucFecha" runat="server" style="text-align: center" />
                        </td>
                        <td style="text-align: left; width: 284px;">&nbsp;
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;"></td>
                    </tr>
                    <tr display="Dynamic">
                        <td style="text-align: left; width: 150px;">Tipo de Archivo<br />
                            <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                Width="222px">
                                <asp:ListItem Value=",">CSV</asp:ListItem>
                                <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                                <asp:ListItem Value="|">EXCEL</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="2" style="text-align: left">Nombre del Archivo<br />
                            <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="Ingrese una Ruta del Archivo a Generar : C:\Users\..."
                                ValidationGroup="vgConsultar" Display="Dynamic" ControlToValidate="txtArchivo"
                                ForeColor="Red" Style="font-size: x-small;"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px;">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left">
                            <div style="overflow: scroll; width: 1100px; height: 400px;">
                                <asp:GridView ID="gvLista" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;"
                                    AllowPaging="false" DataKeyNames="consecutivo">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Exonerado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxSeleccion" runat="server" />
                                                <asp:HiddenField runat="server" ID="consecutivo" Value='<%# Bind("consecutivo") %>' />
                                                <itemstyle horizontalalign="Center" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fecha_tran" HeaderText="Fecha Transacción" DataFormatString="{0:d}"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_tran" HeaderText="Valor Transacción" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_moneda" HeaderText="Tipo Moneda" DataFormatString="{0:n0}"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_oficina" HeaderText="Código Oficina" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_ciudad" HeaderText="Código Departamento/Municipio" DataFormatString="{0:n0}"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_tran" HeaderText="Tipo Transacción" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_producto" HeaderText="Nro. Cuenta o Producto" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_identificacion1" HeaderText="Tipo Identificación del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion1" HeaderText="Nro. Identificación del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_apellido1" HeaderText="1er. Apellido del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_apellido1" HeaderText="2do. Apellido del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_nombre1" HeaderText="1er. Nombre del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_nombre1" HeaderText="Otros Nombres del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_social1" HeaderText="Razón Social del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad_economica" HeaderText="Actividad Económica del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ingresos" HeaderText="Ingreso Mensual del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_identificacion2" HeaderText="Tipo Identificación persona que realiza la transacción individual" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion2" HeaderText="Nro. Identificación persona que realiza la transacción individual" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_apellido2" HeaderText="1er. Apellido persona que realiza la transacción individual" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_apellido2" HeaderText="2do. Apellido persona que realiza la transacción individual" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_nombre2" HeaderText="1er. Nombre persona que realiza la transacción individual" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_nombre2" HeaderText="Otros Nombres persona que realiza la transacción individual" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Style="text-align: center" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblResultado" runat="server" Text="Archivo de Centrales de Riesgo Generado Correctamente"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
