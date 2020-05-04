<%@ Page Title=".: Consolidado Productos :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView runat="server" ID="mvConsolidado" ActiveViewIndex="0">
        <asp:View runat="server" ID="vDatosCliente">
            <asp:Panel runat="server">
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: left; width: 150px">Código Cliente<br />
                                <asp:TextBox ID="txtCodCliente" runat="server" BackColor="White" Enabled="false" Style="text-align: left; width: 100px"></asp:TextBox>
                            </td>

                            <td colspan="3" style="text-align: left;">Nombres<br />
                                <asp:TextBox ID="txtNombres" runat="server" BackColor="White" Enabled="false" Style="text-align: left; width: 500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 150px">Tipo Documento<br />
                                <asp:TextBox ID="txtTipoDoc" runat="server" BackColor="White" Enabled="false" Style="text-align: left; width:100px"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 100px">No. Documento<br />
                                <asp:TextBox ID="txtId" runat="server" BackColor="White" Enabled="false" Style="text-align: left"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 150px">Teléfono<br />
                                <asp:TextBox ID="txtTelefono" runat="server" BackColor="White" Enabled="false" Style="text-align: left"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 300px">Dirección<br />
                                <asp:TextBox ID="txtDireccion" runat="server" BackColor="White"  Enabled="false" Style="text-align: left; width: 280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="6">
                                <hr style="width: 100%" />
                                <strong>Fecha de Movimientos</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label ID="lblfechainicial" runat="server" Text="Fecha Inicial"></asp:Label>
                                <br />
                                <ucFecha:fecha ID="TxtFechaInicial" runat="server" style="text-align: center" />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblfechafinal" runat="server" Text="Fecha Final"></asp:Label>
                                <br />
                                <ucFecha:fecha ID="TxtFechaFinal" runat="server" style="text-align: center" />
                            </td>
                        </tr>
                    </table>

                    <asp:UpdatePanel ID="upAporte" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" Visible="false">
                        <ContentTemplate>
                            <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                                <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                    <table style="width: 100%;" id="TituloAportes" runat="server" visible="true">
                                        <tr style="text-align: center; color: #fff; font-weight: bold">
                                            <td>Movimientos de Aportes</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left" colspan="4">
                                        <div style="width: 100%;">
                                            <asp:GridView ID="gvMovAportes" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False" AllowPaging="true"
                                                OnPageIndexChanging="gvMovAportes_PageIndexChanging" Font-Size="XX-Small" RowStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_aporte" HeaderText="Nro. Aporte" />
                                                    <asp:BoundField DataField="nom_linea_aporte" HeaderText="Línea Aporte" />
                                                    <asp:BoundField DataField="FechaPago" HeaderText="Fecha" />
                                                    <asp:BoundField DataField="CodOperacion" HeaderText="No Operación" />
                                                    <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Operación" />
                                                    <asp:BoundField DataField="num_comp" HeaderText="Num.Comp" />
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp" />
                                                    <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo Mov" />
                                                    <asp:BoundField DataField="Capital" HeaderText="Valor" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                            </asp:GridView>
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:UpdatePanel ID="upCredito" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" Visible="false">
                        <ContentTemplate>

                            <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                                <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                    <table style="width: 100%;" id="TituloCreditos" runat="server" >
                                        <tr style="text-align: center; color: #fff; font-weight: bold">
                                            <td>Movimientos de Créditos</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="width: 100%;">
                                            <asp:GridView ID="gvMovCredito" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False" AllowPaging="true"
                                                OnPageIndexChanging="gvMovCredito_PageIndexChanging" Style="font-size: xx-small" RowStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundField DataField="NumeroRadicacion" HeaderText="Nro. Radicación" />
                                                    <asp:BoundField DataField="Linea" HeaderText="Línea Crédito" />
                                                    <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" />
                                                    <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" />
                                                    <asp:BoundField DataField="TipoOperacion" HeaderText="No Operac" />
                                                    <asp:BoundField DataField="num_comp" HeaderText="Num Comprobante" />
                                                    <asp:BoundField DataField="TIPO_COMP" HeaderText="Tipo Comprobante" />
                                                    <asp:BoundField DataField="Transaccion" HeaderText="Transacción" />
                                                    <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo  Mov" />
                                                    <asp:BoundField DataField="Capital" HeaderText="Capital" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="IntCte" HeaderText="Int Corriente" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="IntMora" HeaderText="Int Mora" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="Poliza" HeaderText="Poliza" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="Seguros" HeaderText="Seguros" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="Prejuridico" HeaderText="Pre-Juridico" />
                                                    <asp:BoundField DataFormatString="{0:c}" HeaderText="Valor" DataField="totalpago" />
                                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                            </asp:GridView>
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:UpdatePanel ID="upAhorroV" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" Visible="false">
                        <ContentTemplate>

                            <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                                <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                    <table style="width: 100%;" id="TituloAhorroV" runat="server" >
                                        <tr style="text-align: center; color: #fff; font-weight: bold">
                                            <td>Movimientos de Ahorros a la Vista</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">
                                        <div style="width: 100%;">
                                            <asp:GridView ID="gvMovAhorroVista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                                AllowPaging="True" OnPageIndexChanging="gvMovAhorroVista_PageIndexChanging" PageSize="5" Style="font-size: xx-small" RowStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_cuenta" HeaderText="Nro. Cuenta" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Línea Ahorro" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="cod_ope" HeaderText="Nro. Operación" />
                                                    <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Operación" />
                                                    <asp:BoundField DataField="tipo_tran" HeaderText="Transacción" />
                                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp" />
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp" />
                                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                                <RowStyle CssClass="gridItem"></RowStyle>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="upProgramado" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" Visible="false">
                        <ContentTemplate>

                            <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                                <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                    <table style="width: 100%;" id="TituloProgramado" runat="server" >
                                        <tr style="text-align: center; color: #fff; font-weight: bold">
                                            <td>Movimientos de Ahorro Programado</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">
                                        <div style="width: 100%;">
                                            <asp:GridView ID="gvMovProgramado" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
                                                AllowPaging="True" OnPageIndexChanging="gvMovProgramado_PageIndexChanging" PageSize="5" Style="font-size: xx-small" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_cuenta" HeaderText="Nro. Cuenta" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="cod_ope" HeaderText="Nro. Operación" />
                                                    <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Operación" />
                                                    <asp:BoundField DataField="tipo_tran" HeaderText="Transacción" />
                                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp" />
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp" />
                                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                                <RowStyle CssClass="gridItem"></RowStyle>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:UpdatePanel ID="upCDAT" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" Visible="false">
                        <ContentTemplate>

                            <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                                <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                    <table style="width: 100%;" id="TituloCDAT" runat="server" >
                                        <tr style="text-align: center; color: #fff; font-weight: bold">
                                            <td>Movimientos de CDATS</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">
                                        <div style="width: 100%;">
                                            <asp:GridView ID="gvMovCDAT" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
                                                AllowPaging="True" OnPageIndexChanging="gvMovCDAT_PageIndexChanging" PageSize="5" Style="font-size: xx-small" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_cdat" HeaderText="Nro. CDAT" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Línea CDAT" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="cod_ope" HeaderText="Nro. Operación" />
                                                    <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Operación" />
                                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp" />
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp" />
                                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                                <RowStyle CssClass="gridItem"></RowStyle>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="upServicio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" Visible="false">
                        <ContentTemplate>

                            <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                                <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                    <table style="width: 100%;" id="TituloServicio" runat="server">
                                        <tr style="text-align: center; color: #fff; font-weight: bold">
                                            <td>Movimientos de Servicios</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">
                                        <div style="width: 100%;">
                                            <asp:GridView ID="gvMovServicios" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5" RowStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                AllowPaging="true" GridLines="Horizontal" Style="font-size: xx-small" OnPageIndexChanging="gvMovServicios_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_servicio" HeaderText="Nro. Servicio" />
                                                    <asp:BoundField DataField="nom_linea" HeaderText="Línea Servicio" />
                                                    <asp:BoundField DataField="Fec_1Pago" HeaderText="Fecha" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="cod_persona" HeaderText="Nro. Operación" />
                                                    <asp:BoundField DataField="descripcion" HeaderText="Tipo Operación" />
                                                    <asp:BoundField DataField="numero_cuotas" HeaderText="Num.Comp" />
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp" />
                                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov" />
                                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n2}" />
                                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:n2}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="false" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </asp:Panel>
        </asp:View>
        <asp:View runat="server" ID="vReporte">
            <rsweb:ReportViewer ID="Rpview1" runat="server" Width="100%"
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Page\Asesores\EstadoCuenta\ConsolidadoProductos\ReportMovimiento.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>

    </asp:MultiView>
</asp:Content>


