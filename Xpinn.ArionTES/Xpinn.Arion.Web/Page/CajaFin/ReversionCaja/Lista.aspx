<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
     CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script src="../../../Scripts/PCLBryan.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <asp:MultiView ID="mvReversion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwTipoComprobante" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="4" >
                        <hr width="100%" />
                    </td>
                </tr>
                 <tr>
                    <td colspan="4"> 
                        Fecha Operación
                        <asp:TextBox ID="txtFecha" enabled="false" CssClass="textbox" runat="server" Width="70"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td> 
                        Oficina 
                        <br />
                        <asp:TextBox ID="txtOficina" enabled="false" CssClass="textbox" runat="server" Width="260px"></asp:TextBox>
                        &nbsp;
                    </td>
                     <td>
                         Saldo Caja Efectivo<br />
                         <asp:TextBox ID="txtSaldoCaja" runat="server" CssClass="textbox" 
                             enabled="false" Width="120px"></asp:TextBox>
                     </td>
                     <td>
                         Saldo Caja Cheque
                         <br />
                         <asp:TextBox ID="txtSaldoCajaCheque" runat="server" CssClass="textbox" 
                             enabled="false" Width="120px"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                </tr>
                <tr><td colspan="4"></td></tr>
                <tr>
                    <td colspan="4">
                    <div id="gvDiv">
                        <asp:GridView ID="gvOperacion" runat="server" Width="100%" 
                            AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Anular">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="chkAnula" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_movimiento" HeaderText="Cod Oper" />
                                <asp:BoundField DataField="fecha_movimiento" DataFormatString="{0:d}" HeaderText="Fecha Oper" />
                                <asp:BoundField DataField="nom_caja" HeaderText="Caja" />
                                <asp:BoundField DataField="nom_cajero" HeaderText="Cajero" />
                                <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Oper" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_pago_ing" HeaderText="Valor Ingreso" DataFormatString="{0:N0}">
                                  <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_pago_egr" HeaderText="Valor Egreso" DataFormatString="{0:N0}">
                                  <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomtipo_pago" HeaderText="Tipo Pago" >
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
                        </div>
                    </td>
                </tr>
                <tr><td colspan="4">&#160;</td></tr>
                <tr>
                    <td colspan="4">
                        Cajero que Anula
                        <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" Width="228px" enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Motivo de Anulación
                        <asp:DropDownList ID="ddlMotivoAnulacion" runat="server" Height="25px" Width="244px">
                        </asp:DropDownList>    
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Operaciones Reversadas Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnContinuar" runat="server" Text="Imprimir" onclick="btnContinuar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </asp:View>

        <asp:View ID="View2" runat="server">
            <asp:DropDownList ID="ddlMonedas" runat="server" AutoPostBack="true" 
                CssClass="dropdown" Height="27px" 
                onselectedindexchanged="ddlMonedas_SelectedIndexChanged" Width="162px">
            </asp:DropDownList>
            <asp:GridView ID="gvConsignacion" Visible="false" runat="server" AutoGenerateColumns="false" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                style="text-align: center">
                <AlternatingRowStyle BackColor="White" />
                <columns>
                    <asp:BoundField DataField="cod_movimiento" HeaderStyle-CssClass="gridColNo" 
                        ItemStyle-CssClass="gridColNo" />
                    <asp:BoundField DataField="fec_ope" DataFormatString="{0:d}" 
                        HeaderText="Fecha Operación" />
                    <asp:BoundField DataField="num_documento" HeaderText="Núm. Cheque" />
                    <asp:BoundField DataField="cod_banco" HeaderText="Banco" />
                    <asp:BoundField DataField="nom_banco" HeaderText="Nombre Banco" />
                    <asp:BoundField DataField="valor" DataFormatString="{0:N0}" HeaderText="Valor">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                    <asp:TemplateField HeaderText="Recibe">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRecibe" runat="server" AutoPostBack="true" 
                                />
                        </ItemTemplate>
                    </asp:TemplateField>
                </columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>

        </asp:View>
        

        <asp:View ID="ViewReporte" runat="server">
            <asp:Panel ID="Panel2" runat="server" ClientIDMode="Static">
                <table style="width: 100%">
                    <tr>
                        <td>
                              <asp:Literal ID="LiteralDcl" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="RpviewInfo" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="789px">
                                <LocalReport ReportPath="Page\CajaFin\ReversionCaja\Factura.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        

    </asp:MultiView>
</asp:Content>

