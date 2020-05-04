<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Cuadre Operativo vs Contable :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 85%;">
            <tr>
                <td>
                    <label><b>Número Comprobante:</b></label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNumeroComprobante" ReadOnly="true" />
                </td>
                <td>
                    <label><b>Tipo Comprobante:</b></label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTipoComprobante" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Cod. Atributo:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCodAtributo" ReadOnly="true" />
                </td>
                <td>
                    <label>Nom. Atributo:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNombreAtributo" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Descripción</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescripcion" ReadOnly="true" />
                </td>
                <td>
                    <label>Fecha:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtFecha" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Valor Contable:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtValorContable" ReadOnly="true" />
                </td>
                <td>
                    <label>Valor Operativo:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtValorOperativo" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Diferencia</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDiferencia" ReadOnly="true" />
                </td>
                <td>
                    <label>Tipo Diferencia:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTipoDiferencia" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Tipo Producto</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTipoProducto" ReadOnly="true" />
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
    <table style="width: 90%;">
        <tr>
            <td>
                <h4>Cuadre Operativo vs Contable</h4>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:GridView ID="gvOperativo" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" HorizontalAlign="Center" RowStyle-CssClass="gridItem" Style="font-size: x-small" GridLines="Horizontal"
                    AllowPaging="false" OnRowCreated="gvOperativo_RowCreated">
                    <Columns>
                         <asp:BoundField DataField="cod_cuenta" HeaderText="Cód.Cuenta">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="detalle" HeaderText="detalle">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_mov" HeaderText="Tip. Mov">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_centro_costo" HeaderText="Cod. Centro Costo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_contable" HeaderText="Vr.Contable" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_moneda" HeaderText="Moneda">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_centro_gestion" HeaderText="Cod. Centro Gestión">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_ope" HeaderText="Cód.Ope.">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea" HeaderText="Nom.Línea">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_atr" HeaderText="No.Atributo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_tipo_tran" HeaderText="Tip. Tran">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_mov_operativo" HeaderText="Tip. Mov">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_operativo" HeaderText="Vr.Operativo" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diferencia" HeaderText="Diferencia" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegsOperativo" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
