<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaCreditoPagoDetalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../../../Scripts/PCLBryan.js"></script>
    <table style="width: 100%">
        <tr>
            <td colspan="3" style="text-align: center">&nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 223px">Tipo Identificación<br />
                <asp:TextBox ID="txtTipoDoc" runat="server"
                    CssClass="textbox" Enabled="false" Width="151px"></asp:TextBox></td>
            <td style="text-align: center; width: 179px">Número Identificación<br />
                <asp:TextBox ID="txtNumeIdentificacion"
                    runat="server" CssClass="textbox" Enabled="false" Width="151px"></asp:TextBox></td>
            <td style="text-align: center">Nombres<br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox"
                    Enabled="false" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; width: 223px">No. Crédito<br />
                <asp:TextBox ID="txtNoCredito" runat="server"
                    CssClass="textbox" Enabled="false" Width="151px"></asp:TextBox></td>
            <td style="text-align: center; width: 179px">Estado Crédito<br />
                <asp:TextBox ID="txtEstaCredito" runat="server"
                    CssClass="textbox" Enabled="false" Width="151px"></asp:TextBox></td>
            <td style="text-align: center">Nombre Línea<br />
                <asp:TextBox ID="txtNombLinea" runat="server"
                    CssClass="textbox" Enabled="false" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; width: 223px">Fecha Pago<br />
                <asp:TextBox ID="txtFechapago" runat="server" CssClass="textbox"
                    MaxLength="128" Enabled="true" />
                <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server"
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechapago"></asp:CalendarExtender>
                <asp:CompareValidator ID="cvFecha_pago" runat="server"
                    ControlToValidate="txtFechapago" Display="Dynamic"
                    ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small"
                    ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar"
                    Width="200px" />
            </td>
            <td style="width: 179px; text-align: center">Estado de Cobro<br />
                <asp:TextBox ID="txtEstado" runat="server"
                    CssClass="textbox" Enabled="false" Width="151px"></asp:TextBox>
            </td>
            <td>Fecha Proximo Pago
                <br />
                <asp:TextBox ID="txtFechaProximoPago" runat="server" CssClass="textbox"
                    MaxLength="128" Enabled="false" Width="151px" />

            </td>
        </tr>
    </table>

    <%--asp:UpdatePanel runat="server" UpdateMode="" ChildrenAsTriggers="true" RenderMode="">
        <ContentTemplate>--%>
    <asp:Panel ID="panelfijo" runat="server" Height="30px" Width="100%">
        <div style="border-style: none; border-width: medium; background-color: #f5f5f5;">
            <table>
                <tr>
                    <td style="margin-left: 40px">
                        <asp:Button ID="btnCalcular" runat="server" CssClass="btn8 BotonCambiarClass"
                            Text="Calcular pago hasta la fecha" OnClientClick="changeColorByEvent('BotonCambiarClass', this.id, '#E8254C', '#359af2')" OnClick="btnCalcular_Click" />
                    </td>
                    <td style="margin-left: 40px">
                        <asp:Button ID="btnCalcularPagoTotal" runat="server" CssClass="btn8 BotonCambiarClass"
                            Text="Calcular pago total" OnClientClick="changeColorByEvent('BotonCambiarClass', this.id, '#E8254C', '#359af2')" OnClick="btnCalcularPagoTotal_Click" />
                    </td>
                    <td style="margin-left: 40px">
                        <asp:Button ID="btnProyeccionTotal" CssClass="btn8 BotonCambiarClass" OnClientClick="changeColorByEvent('BotonCambiarClass', this.id, '#E8254C', '#359af2')" runat="server"
                            Text="Proyección Total" OnClick="btnProyeccionTotal_Click" />
                    </td>
                    <td style="margin-left: 40px">
                        <asp:Button ID="btnProximaCuota" CssClass="btn8 BotonCambiarClass" OnClientClick="changeColorByEvent('BotonCambiarClass', this.id, '#E8254C', '#359af2')" runat="server"
                            Text="Proxima Cuota" OnClick="btnProximaCuota_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox runat="server" ID="ChkCuotasExtras" class="ChkBoxCuotas" Text="Mostrar Cuotas Extras" AutoPostBack="True" OnCheckedChanged="ChkCuotasExtras_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                    OnClick="btnExportar_Click" Text="Exportar a excel" /><br />
                <asp:GridView ID="gvDistPagosPendCuotas" AllowPaging="True" runat="server"
                    Width="100%" PageSize="20" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvDistPagosPendCuotas_PageIndexChanging"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:BoundField DataField="NumCuota" HeaderText="No.Cuota" />
                        <asp:BoundField DataField="IdAvance" HeaderText="IdAvance" />
                        <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" />
                        <asp:BoundField DataField="Capital" HeaderText="Capital">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IntCte" HeaderText="Int Corriente" />
                        <asp:BoundField DataField="IntMora" HeaderText="Int Mora" />
                        <%--<asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" />
                                <asp:BoundField DataField="iva_leymipyme" HeaderText="Iva Ley MiPyme" />--%>
                        <asp:BoundField DataField="Poliza" HeaderText="Póliza" />
                        <asp:BoundField DataField="Garantias_Comunitarias" HeaderText="Gar.Comunitarias" />
                        <asp:BoundField DataField="Cobranzas" HeaderText="Cobranza" />
                        <asp:BoundField DataField="Otros" HeaderText="Otros" />
                        <asp:BoundField DataField="totalconhonorarios" HeaderText="Total a Pagar" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegPendCuotas" runat="server" Visible="False" />
                <asp:Label ID="lblInfoPendCuotas" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <hr />
    <table width="90%">
        <tr>
            <td align="left">Total Capital</td>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Int. Cte </td>
            <td align="right">
                <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Int. Mora </td>
            <td align="right">
                <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Ley MiPyme </td>
            <td align="right">
                <asp:Label ID="Label4" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">Total Iva Ley MiPyme</td>
            <td align="right">
                <asp:Label ID="Label5" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Póliza</td>
            <td align="right">
                <asp:Label ID="Label6" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Cobranza</td>
            <td align="right">
                <asp:Label ID="Label7" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Valor a Pagar</td>
            <td align="right">
                <asp:Label ID="Label8" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">Total Garant.Comunitarias</td>
            <td align="right">
                <asp:Label ID="Label9" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left">Total Otros</td>
            <td align="right">

                <asp:Label ID="Label10" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>

            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left" runat="server" visible="False" id ="lblTotCuotasExtras">Total Cuotas Extras</td>
            <td align="right" runat="server" visible="False" id ="txtTotCuotasExtras">
                <asp:Label ID="Label11" runat="server" Text="Label" ForeColor="#0066FF"></asp:Label>
            </td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left"></td>
            <td align="right"></td>
            <td align="right">&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <style>
        .ChkBoxCuotas {
            display: flex;
        }
    </style>
</asp:Content>
