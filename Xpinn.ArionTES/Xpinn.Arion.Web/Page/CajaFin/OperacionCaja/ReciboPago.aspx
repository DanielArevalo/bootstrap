<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="ReciboPago.aspx.cs" Inherits="ReciboPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <input type="button" style="font-size:16px" onclick="javascript: doClientPrint();" value="Imprimir..." />
    <br />
    <asp:Label ID="lblruta" Text="" runat="server" Font-Size="6" />
    <input type="hidden" id="sid" name="sid" value="<%= HttpContext.Current.Session.SessionID %>" />
    <%=Neodynamic.SDK.Web.WebClientPrint.CreateScript(lblruta.Text + "WebClientPrintAPI.ashx", lblruta.Text + "PrintHandler.ashx", HttpContext.Current.Session.SessionID)%>
    <script type="text/javascript">
        function doClientPrint() {
            // Launch WCPP at the client side for printing...
            var sessionId = $("#sid").val();
            var lblope = txttotalING_solijq = document.getElementById('<%= lblCodOpera.ClientID %>');
            var ope = lblope.value;
            jsWebClientPrint.print('sid=' + sessionId + '&ope=' + ope);
        }
    </script>   
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintGridView() {
            var inlineMediaStyle = null;
            var gridInsideDiv = document.getElementById('cphMain_DivFactura');
            var printWindow = window.open('gview.htm', 'PrintWindow', 'letf=0,top=0,width=1000,height=500,toolbar=1,scrollbars=1,status=1');
            printWindow.document.write(gridInsideDiv.innerHTML);
            printWindow.document.close();
            var newStyle = document.createElement('style');
            newStyle.setAttribute('type', 'text/css');
            newStyle.setAttribute('width', '21cm');
            newStyle.setAttribute('height', '14,85cm');
            newStyle.setAttribute('rel', 'stylesheet');
            printWindow.document.head.appendChild(newStyle);
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <div id="DivFactura" runat="server">
        <table style="width: 300px; font-family:'Courier New'; font-size:smaller; ">
            <tr>
                <td rowspan="1" class="align-rt" style="width: 66px; height: 10px;">&nbsp;<asp:Image ID="imgLogo" runat="server" Height="53px"
                    ImageUrl="~/Images/LogoEmpresa.jpg" Width="55px" />
                </td>
                <td align="left" style="width: 47%; height: 10px;">
                    <asp:Label ID="lblEmpresa" runat="server" Style="font-family: Arial"></asp:Label><br />
                    <span style="font-family: Arial">NIT:</span><asp:Label ID="lblNit"
                        runat="server" Style="font-family: Arial"></asp:Label>
                </td>
                <td colspan="1" class="space" style="height: 10px; text-align: center;">
                    <table>
                        <tr>
                            <td>
                                <span style="font-family: Arial">
                                    <%--<b>Número de Factura:</b></span>&nbsp;--%>
                                    <b>Recibo de Caja:</b></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFactura" runat="server" Font-Size="14"
                                    Style="font-family: Arial"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    <strong>Fecha</strong>:
                    <asp:Label ID="fechahoy" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td colspan="3">
                    <asp:Label ID="lblDir" runat="server" Style="font-family: Arial"></asp:Label>
                    <span style="font-family: Arial">Tel:</span>
                    <asp:Label ID="lblTel" runat="server" Style="font-family: Arial"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td colspan="3">
                    <span style="font-family: Arial">
                        <%--<em>Resolución DIAN 21 del 7 de Agosto de 2012</em>--%>
                    </span>
                </td>
            </tr>
        </table>

        <table style="width: 300px; font-family:'Courier New'; font-size:smaller;">
            <tr>
                <td style="width: 72px; font-family: Times New Roman"><span><b>Operac.:</b></span></td>
                <td colspan="3" style="font-family: Times New Roman">
                    <asp:TextBox ID="lblCodOpera" runat="server" ReadOnly="True" BorderStyle="None" BorderWidth="0px"/></td>
            </tr>
            <tr align="left">
                <td style="width: 72px; height: 8px; font-family: Times New Roman;"><b>Oficina:</b></td>
                <td style="width: 28%; height: 8px; font-family: Times New Roman;">
                    <asp:Label ID="lblOficina" runat="server"></asp:Label></td>
                <td style="width: 72px; height: 6px; font-family: Times New Roman;"><b>Caja:</b></td>
                <td style="width: 28%; height: 6px; font-family: Times New Roman;">
                    <asp:Label ID="lblCaja" runat="server"></asp:Label></td>
            </tr>
            <tr align="left">
                <td style="height: 8px; width: 72px; font-family: Times New Roman;"><span><b>Cajero:</b></span></td>
                <td style="height: 8px; width: 37%; font-family: Times New Roman;" colspan="3">
                    <asp:Label ID="lblCajero" runat="server"></asp:Label></td>
            </tr>
            <tr align="left">
                <td style="width: 72px; font-family: Times New Roman"><b>Ciudad:</b></td>
                <td style="font-family: Times New Roman">
                    <asp:Label ID="lblCiudad" runat="server"></asp:Label></td>
                <td style="height: 6px; width: 207px; font-family: Times New Roman"><b>Fecha:</b></td>
                <td style="height: 6px; width: 37%; font-family: Times New Roman">
                    <asp:Label ID="lblFecha" runat="server"></asp:Label></td>
            </tr>
            <tr align="left">
                <td rowspan="1"><b><span style="font-family: Times New Roma">Cliente:</span>&nbsp;</b></td>
                <td rowspan="1" colspan="3">
                    <asp:Label ID="lblCliente" runat="server"></asp:Label></td>
                <td rowspan="1">&nbsp;</td>
            </tr>
            <tr align="left">
                <td><b><span style="font-family: Times New Roma">Identifica:</span></b></td>
                <td colspan="3">
                    <asp:Label ID="lblIdentific" runat="server"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr align="left">
                <td style="width: 72px">&nbsp;</td>
                <td colspan="3">&nbsp;</td>
            </tr>
        </table>

        <table style="width: 300px; font-family:'Courier New'; font-size:smaller;">
            <tr align="left">
                <td colspan="2">
                    <asp:GridView ID="gvDetalle" runat="server" Width="100%"
                        AutoGenerateColumns="False" PageSize="20" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical"
                        Style="font-size: small; font-family: Arial;">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                            <asp:BoundField DataField="nro_producto" HeaderText="Nro Ref" />
                            <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" Visible="False" />
                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </td>
            </tr>
        </table>

        <table style="width: 250px; margin-left: 25px; font-family:'Courier New'; font-size:smaller;">
            <tr align="left">
                <td style="text-align: right; width: 90%; font-family: Times New Roman"><b>SUBTOTAL</b></td>
                <td style="width: 10%"></td>
                <td align="right" style="width: 39px; font-family: Times New Roman">
                    <asp:Label ID="lblSubTotal" runat="server"></asp:Label></td>
            </tr>
            <tr align="left">
                <td style="text-align: right; width: 90%; font-family: Times New Roman"><b>BASE IVA</b></td>
                <td style="width: 10%"></td>
                <td align="right" style="width: 39px; height: 12px; font-family: Times New Roman">
                    <asp:Label ID="lblBaseIva" runat="server"></asp:Label></td>
            </tr>
            <tr align="left">
                <td style="text-align: right; width: 90%; font-family: Times New Roman"><b>IVA</b></td>
                <td style="width: 10%"></td>
                <td align="right" style="width: 39px; font-family: Times New Roman">
                    <asp:Label ID="lblIva" runat="server"></asp:Label></td>
            </tr>
            <tr style="text-align: left">
                <td style="text-align: right; width: 90%; font-family: Times New Roman"><b>TOTAL</b></td>
                <td style="width: 10%"></td>
                <td align="right" style="width: 39px; font-family: Times New Roman">
                    <asp:Label ID="lblTotal" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="width: 117px; font-family: Times New Roman"><b>EFECTIVO:</b></td>
                <td align="right" style="font-family: Times New Roman" />
                <td>
                    <asp:Label ID="lblEfectivo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="width: 117px; font-family: Times New Roman"><b>CHEQUE:</b>&#160;&#160;&#160;&#160;</td>
                <td style="width: 10%"></td>
                <td>
                    <asp:Label ID="lblCheque" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 117px; font-family: Times New Roman"><b>OTROS:</b></td>
                <td style="width: 10%"></td>
                <td>
                    <asp:Label ID="lblOtros" runat="server"></asp:Label>
                </td>
            </tr>
            </table>

        <table style="width: 300px; font-family:'Courier New'; font-size:smaller;">
            <tr align="left">
                <td colspan="2">
                     <asp:Label ID="lblsaldos" Text="Saldos-Productos:" Visible="False" runat="server" width= "186px" style="text-align: left; font-weight: 700;"></asp:Label>
              
                    
                </td>
            </tr>
            <tr align="left">
                <td colspan="2">
                    <asp:GridView ID="gvSaldos" runat="server" Width="100%"
                        AutoGenerateColumns="False" PageSize="20" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical"
                        Style="font-size: small; font-family: Arial;">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="nom_tipo_producto" HeaderText="Producto" />
                            <asp:BoundField DataField="nro_producto" HeaderText="Nro Ref" />
                            <asp:BoundField DataField="saldo" HeaderText="Valor" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; text-align: left" colspan="2">
                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="2">
                    <asp:TextBox ID="txtObservaciones" runat="server" Width="95%" Style="height: auto;" CssClass="TextBox" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                     <asp:Label ID="lblnumboucher" Text="N. Boucher:" Visible="false" runat="server" width= "90px" style="text-align: left; font-weight: 700;"></asp:Label>
              
                    
                </td>
                <td style="width: 100%;">
                    <asp:Label ID="lblBoucher" runat="server" width= "133px" Visible="false"  style="text-align: center"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvCheques" runat="server" Width="100%"
                        AutoGenerateColumns="False" PageSize="20" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical"
                        Style="font-size:  xx-small; font-family: Arial;">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="nom_banco" HeaderText="Banco" />
                            <asp:BoundField DataField="num_documento" HeaderText="N.Cheque" />
                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
              
                    
                </td>
            </tr>
        </table>

    </div>
    <div id="DivButtons" runat="server">
        <table style="width: 300px">
            <tr>
                <td>
                    <input id="btnImprimir" onclick="javascript: PrintGridView();" type="button" value="Imprimir Factura" style="width: 130px;" />
                </td>
            </tr>

        </table>
    </div>
</asp:Content>
