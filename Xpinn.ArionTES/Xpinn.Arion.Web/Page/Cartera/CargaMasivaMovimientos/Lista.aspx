<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%">
                <tr style="text-align: left">
                    <td class="logo" style="text-align: left; font-size: small;"
                        colspan="3">
                        <strong>
                            <asp:Label ID="lblInicial" runat="server" Text="Seleccion el Archivo con los Movimientos "></asp:Label>
                        </strong>
                        <br />
                        <asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="#359AF2"
                            Text="Label" Visible="False"></asp:Label>
                        &nbsp;     
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td class="logo" colspan="4" style="text-align: left; font-size: xx-small;">Para carga del comprobante el sistema cojera desde la 2da Fila del archivo.<br />
                        Estructura del archivo: Tipo Producto, N°Producto, Cedula, Valor, Tipo Movimiento.
                        <br />
                        Para el tipo de producto: 1	Aportes	2 Creditos 3 Ahorros Vista 4 Servicios 6 Afiliacion 7 Otros 8 Devoluciones 9 Ahorros Programado        
                </tr>
                <tr style="text-align: left">
                    <td class="logo" style="width: 350px; text-align: left; font-size: x-small;"
                        colspan="2">
                        <asp:FileUpload ID="FileUploadMovimientos" runat="server" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="width: 160px; text-align: left" colspan="2">
                        <asp:Panel ID="pConsulta" runat="server">
                            <asp:Button ID="btnCargarComp" runat="server" CssClass="btn8"
                                OnClick="btnCargarComp_Click" Text="Cargar Movimientos"
                                Width="145px" />
                        </asp:Panel>
                    </td>
                    <td></td>
                </tr>
            </table>
            &nbsp;
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="2">
                        <div style="overflow: scroll; height: 500px; width: 100%;">
                            <div style="width: 100%;">
                                <asp:UpdatePanel ID="upComprobante" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="3"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                            SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;">
                                            <Columns>
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TipoProducto" HeaderText="Tipo Producto">
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NumeroProducto" HeaderText="Numero Producto">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IdentificacionPer" HeaderText="Identificacion">
                                                    <ItemStyle Width="180px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="{0:N2}">
                                                    <ItemStyle Width="140px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo Movimiento">
                                                    <ItemStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CodPersona" HeaderText="Codigo Persona">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                        <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <asp:ModalPopupExtender ID="mpeNuevo" runat="server"
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelGeneral" runat="server">
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>

    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px">
        <div id="popupcontainer" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">Esta Seguro de Realizar la carga de los datos</td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8" Width="182px" OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8"
                            Width="182px" OnClick="btnParar_Click" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />

    <asp:ModalPopupExtender ID="mpePegar" runat="server"
        PopupControlID="panelClipboard" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:View ID="vwMensaje" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>

        </table>
    </asp:View>
    <asp:Panel ID="panelClipboard" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px">
        <div id="Div1" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3"
                        style="font-size: xx-small; text-decoration: underline; color: #00CC99">
                        <em><strong>Pegue el Texto a Cargar Aqui y Presione el Botón de Continuar</strong></em></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
