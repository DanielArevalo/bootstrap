<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pDatos" runat="server">
                        <table style="text-align: center" cellspacing="0" cellpadding="1">
                            <tr>
                                <td style="text-align: left; width: 150px">Num. Servicio<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Solicitud<br />
                                    <ucFecha:fecha ID="txtFecha" runat="server" />
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Aprobación<br />
                                    <ucFecha:fecha ID="txtFechaAproba" runat="server" />
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Inicio Vigencia<br />
                                    <ucFecha:fecha ID="txtFecIni" runat="server" />
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Final Vigencia<br />
                                    <ucFecha:fecha ID="txtFecFin" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">Identificación<br />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="50px" Visible="false" />
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                        Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                                </td>
                                <td style="text-align: left; width: 280px" colspan="2">Nombre<br />
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="95%" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                        Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                                </td>
                                <td style="text-align: left; width: 140px">Valor Total<br />
                                    <uc1:decimales ID="txtValorTotal" runat="server" />
                                </td>
                                <td style="text-align: left; width: 100px">
                                    <table>
                                        <tr>
                                            <td>Saldo<br />
                                                <uc1:decimales ID="txtSaldo" runat="server" />
                                            </td>
                                            <td>Tasa<br />
                                                <asp:TextBox ID="txtTasa" CssClass="textbox" ReadOnly="True" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 430px" colspan="3">
                                    <table>
                                        <tr>
                                            <td>Linea de Servicio<br />
                                                <asp:TextBox ID="ddlLinea" runat="server" CssClass="textbox" ReadOnly="True" Width="160px" />
                                            </td>
                                            <td>Plan<br />
                                                <asp:TextBox ID="ddlPlan" runat="server" CssClass="textbox" ReadOnly="True" Width="160px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left; width: 140px">Fec. Prox Pago<br />
                                    <ucFecha:fecha ID="txtFecProxPago" runat="server" />
                                </td>
                                <td style="text-align: left; width: 140px">
                                    <table>
                                        <tr>
                                            <td>#Cuotas<br />
                                                <asp:TextBox ID="txtNumCuotas" runat="server" CssClass="textbox" Width="40px" />
                                            </td>
                                            <td>#Cuotas Faltantes
                                                <br />
                                                <asp:TextBox ID="txtCutasFaltantes" runat="server" CssClass="textbox" Width="100px" />
                                            </td>
                                            <td>Valor de la Cuota<br />
                                                <uc1:decimales ID="txtValorCuota" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <hr style="width: 100%" />
                    <asp:Panel ID="pMovimientos" runat="server">
                        <table style="text-align: center" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="text-align: left; width: 140px">Fecha Inicial<br />
                                    <ucFecha:fecha ID="txtFechaini" runat="server" />
                                </td>
                                <td style="text-align: left; width: 140px">Fecha Final<br />
                                    <ucFecha:fecha ID="txtFechaFin" runat="server" />
                                </td>
                                <td style="text-align: left; width: 140px">
                                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                                        OnClick="btnExportar_Click" Text="Exportar a Excel" Height="28px" Width="124px" />
                                </td>
                                <td style="text-align: left; width: 140px">
                                    <asp:Button ID="btnVisualizar" runat="server" CssClass="btn8"
                                        OnClick="btn_visualizar_click" Text="Vizualizar informe" Height="28px" Width="124px" />
                                </td>
                                <td style="text-align: left; width: 220px"></td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: left">
                                    <br />
                                    <asp:Panel ID="panelGrilla" runat="server">
                                        <div style="overflow: scroll; max-width: 100%; max-height: 450px">
                                            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                                                AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="cod_persona"
                                                Style="font-size: xx-small">
                                                <Columns>
                                                    <asp:BoundField DataField="Fec_1Pago" HeaderText="F.Pago" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Operación">
                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="numero_cuotas" HeaderText="Num.Comprobante">
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comprobante">
                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="descripcion" HeaderText="Transaccion">
                                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="capital" HeaderText="Capital" DataFormatString="{0:n2}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="interes_corriente" HeaderText="Interes Corriente" DataFormatString="{0:n2}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n2}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:n2}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 100%" colspan="5">
                                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                        Visible="False" />
                                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <hr style="width: 100%" />

        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
