<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaCreditoDetalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="cuFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 100%">
        <tr>
            <td style="text-align: center">&nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
    </table>
    <div id="printTable">
        <table style="width: 100%">
            <tr>
                <td>No. Crédito<br />
                    <asp:TextBox ID="txtNoCredito" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                </td>
                <td>Nro.Solicitud<br />
                    <asp:TextBox ID="txtPagare" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Cliente<br />
                    <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Style="text-align: center"
                        Width="100%" Enabled="False"></asp:TextBox>
                </td>
                <td>Estado<br />
                    <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Estado del Crédito<br />
                    <asp:TextBox ID="txtEstadoCredito" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Oficina<br />
                    <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Línea<br />
                    <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False" Width="100%"></asp:TextBox>
                </td>
                <td>Calif. Promedio<br />
                    <asp:TextBox ID="txtCalifPromedio" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Plazo<br />
                    <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Periodicidad<br />
                    <asp:TextBox ID="txtPeriocidad" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Destinación<br />
                    <asp:TextBox ID="txtDestinacion" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox><br />
                    <asp:TextBox ID="txtFormatoPago" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False" Visible="false"></asp:TextBox>
                </td>
                <td>Cuotas Pagadas<br />
                    <asp:TextBox ID="txtCuotasPagas" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Tasa de Interés<br />
                    <asp:TextBox ID="txtTasaNM" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Forma de Pago<br />
                    <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                </td>
                <td>Monto Solicitado<br />
                    <asp:TextBox ID="txtMontoSolicitado" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                        Mask="999,999,999" MaskType="Number" TargetControlID="txtMontoSolicitado" />
                </td>
                <td>Monto Aprobado<br />
                    <asp:TextBox ID="txtMontoAprobado" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                        Mask="999,999,999" MaskType="Number" TargetControlID="txtMontoAprobado" />
                </td>
            </tr>
            <tr>
                <td>Saldo Capital<br />
                    <asp:TextBox ID="txtSaldoCapital" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                        Mask="999,999,999" MaskType="Number" TargetControlID="txtSaldoCapital" />
                </td>
                <td>Cuota<br />
                    <asp:TextBox ID="txtCuotas" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" DisplayMoney="Left"
                        Mask="999,999,999" MaskType="Number" TargetControlID="txtCuotas" />
                </td>
                <td>Atributos<br />
                    <asp:TextBox ID="txtAtributos" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                            DisplayMoney="Left" Mask="999,999,999" MaskType="Number" TargetControlID="txtAtributos" />
                </td>
                <td>Saldo Pendiente<br />
                    <asp:TextBox ID="txtSaldoPendiente" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                            DisplayMoney="Left" Mask="999,999,999" MaskType="Number" TargetControlID="txtSaldoPendiente" />
                </td>
            </tr>
            <tr>
                <td>Valor a Pagar<br />
                    <asp:TextBox ID="txtVlrAPagar" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                            DisplayMoney="Left" Mask="999,999,999" MaskType="Number" TargetControlID="txtVlrAPagar" />
                </td>
                <td>Total a Pagar<br />
                    <asp:TextBox ID="txtVlrTotalAPagar" runat="server" Style="text-align: center" CssClass="textbox"
                        Enabled="False"></asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                            DisplayMoney="Left" Mask="999,999,999" MaskType="Number" TargetControlID="txtVlrTotalAPagar" />
                </td>
                <td>Fecha Solicitud<br />
                    <asp:TextBox ID="TxtFechaSolicitud" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                </td>
                <td>Fecha Aprobación<br />
                    <asp:TextBox ID="TxtFechaAprobacion" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Fecha Desembolso<br />
                    <asp:TextBox ID="TxtFechaDesembolso" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False" Width="126px"></asp:TextBox>
                </td>
                <td>Fecha Terminación<br />
                    <asp:TextBox ID="TxtFechaTerminacion" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False" Width="126px"></asp:TextBox>
                </td>
                <td>F Últ Pago<br />
                    <asp:TextBox ID="TxtFechaUltimoPago" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False" Width="126px"></asp:TextBox>
                    <br />
                </td>
                <td>F Próx Pago<br />
                    <asp:TextBox ID="TxtFechaProximoPago" runat="server" CssClass="textbox" Style="text-align: center"
                        Enabled="False" Width="126px"></asp:TextBox>
                    <br />
                </td>
            </tr>
        </table>
        <div style="display: flex; vertical-align: baseline;">
            <table style="width: 100%; text-align: left">
                <tr>
                    <td style="width: 35%">
                        <div style="text-align: left">
                            <strong>Cuotas Extras</strong><br />
                        </div>
                    </td>
                    <td style="width: 35%">
                        <div style="text-align: left">
                            <strong>Garantías</strong><br />
                        </div>
                    </td>
                    <td style="width: 30%">
                        <strong>Empresas Recaudadoras</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 35%">
                        <asp:GridView ID="gvCuotas" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="Horizontal"
                            PageSize="10" OnPageIndexChanging="gvCuotas_PageIndexChanging"
                            ShowHeaderWhenEmpty="True" Width="100%"
                            Style="font-size: x-small">
                            <Columns>
                                <asp:BoundField DataField="fecha_pago" HeaderText="Fecha_Pago" HtmlEncode="false" DataFormatString="{0:d}"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_capital" HeaderText="Valor Pago" />
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo" />
                                <asp:BoundField DataField="des_forma_pago" HeaderText="Forma Pago" />
                                <asp:BoundField DataField="des_tipo_cuota" HeaderText="Tipo_Cuota" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>


                    </td>
                    <td style="text-align: left; width: 35%">
                        <asp:GridView ID="gvGarantias" runat="server" Width="95%" AllowPaging="True" PageSize="20"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                            Style="text-align: left; font-size: x-small;">
                            <Columns>
                                <asp:BoundField DataField="NumeroRadicacion" HeaderText="Número Garantía" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="FechaGarantia" HeaderText="Fecha Garantía" />
                                <asp:BoundField DataField="Valor" HeaderText="Vlr Garantía" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="Estado" runat="server" Text='<%# (Convert.ToDecimal(Eval("Estado")) == 1) ? "Activo" : "Terminada"   %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="false" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegGtia" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoGtia" runat="server" Text="El crédito no tiene garantías."
                            Visible="False" />
                    </td>
                    <td style="width: 30%; text-align: left; vertical-align: sub">
                        <asp:GridView ID="gvEmpresaRecaudora" runat="server" Width="95%" AllowPaging="True" PageSize="20"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                            Style="text-align: left; font-size: x-small;">
                            <Columns>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("cod_empresa") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nom_empresa") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Bind("porcentaje") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvalor" runat="server" Text='<%# Bind("valor") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalEmpresas" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoEmpresas" runat="server" Text="El crédito no tiene empresas recaudadoras."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 35%">
                        <div style="text-align: left">
                            <strong>Codeudores</strong><br />
                        </div>
                    </td>
                    <td style="width: 35%">
                        <div style="text-align: left">
                            <strong>Referencias</strong><br />
                        </div>
                    </td>
                    <td style="width: 30%">
                        <strong>Observaciones</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 35%">
                        <asp:GridView ID="gvCodeudores" runat="server" AllowPaging="True" PageSize="5" GridLines="Horizontal"
                            ShowHeaderWhenEmpty="True" Width="95%" AutoGenerateColumns="False" Style="text-align: center; font-size: x-small;">
                            <Columns>
                                <asp:BoundField DataField="IdCodeudor" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"
                                    HeaderText="Código">
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NumeroDocumento" HeaderText="Número Documento">
                                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NombreCodeudor" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DireccionCodeudor" HeaderText="Dirección">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TelefonoCodeudor" HeaderText="Teléfono">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegCodeudores" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoCodeudores" runat="server" Text="El crédito no tiene codeudores."
                            Visible="False" />
                    </td>
                    <td style="text-align: left; width: 35%">
                        <asp:GridView ID="gvReferencias" runat="server" AllowPaging="True" PageSize="5" ShowHeaderWhenEmpty="True"
                            Width="95%" AutoGenerateColumns="False" Style="text-align: center; font-size: x-small;">
                            <Columns>
                                <asp:BoundField DataField="tipo_referencia" HeaderText="Tipo Referencia">
                                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="vinculo" HeaderText="Parentesco">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DIRECCION" HeaderText="Dirección">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="telefonos" HeaderText="Telefóno">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TELOFICINA" HeaderText="Tel.Oficina">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Celular" HeaderText="Celular">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegReferencias" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoReferencias" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                    <td style="width: 30%; text-align: left; vertical-align: sub">
                        <asp:GridView ID="gvObservaciones" runat="server" AllowPaging="True"
                            PageSize="5" ShowHeaderWhenEmpty="True"
                            Width="95%" AutoGenerateColumns="False"
                            Style="text-align: center; font-size: x-small;"
                            OnPageIndexChanging="gvObservaciones_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="idcontrol" HeaderText="Código" />
                                <asp:BoundField DataField="fechaproceso" HeaderText="Fecha Generación" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observacion" />
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegObser" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoObser" runat="server" Text="El crédito no tiene observaciones registradas."
                            Visible="False" />
                    </td>
                </tr>
                <tr>


                    <td style="width: 35%">
                        <strong>Documentos :</strong><br />
                        <asp:GridView ID="gvDocumentos" runat="server" Width="95%" AllowPaging="True" PageSize="20"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                            Style="text-align: left; font-size: x-small;">
                            <Columns>
                                <asp:BoundField DataField="NumeroRadicacion" HeaderText="Número Radicación" />
                                <asp:BoundField DataField="Referencia" HeaderText="Referencia" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Nombre" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalDocs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoDocs" runat="server" Text="El crédito no tiene documentos registrados."
                            Visible="False" />
                    </td>

                </tr>

            </table>
        </div>
    </div>
    <script>
        var parser, xmlDoc;
        function Imprimir() {
            var txt = document.getElementsByTagName('head')[0].innerHTML;
            var divToPrint = document.getElementById("printTable");
            newWin = window.open("");
            newWin.document.write('<html>' + txt + '<body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.print();
            newWin.close();
        }



        $('#cphMain_ucImprimir_ImgBtn').on('click',
            function () {
                Imprimir();
            });
    </script>
</asp:Content>
