<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" colspan="3">
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td style="text-align:left" colspan="2">
                                <strong>CONDONACIÓN TOTAL DEUDA</strong>
                                </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 180px; text-align:left">
                                <strong>Fecha de Condonación</strong></td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtFechaCondonacion" runat="server" cssClass="textbox" 
                                    Height="23px" maxlength="10"  
                                    Width="106px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="Image1" TargetControlID="txtFechaCondonacion" 
                                    ViewStateMode="Enabled">
                                </asp:CalendarExtender>
                                <asp:Label ID="Label4" runat="server" style="color: #FF3300" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="3">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                Identificación
                            </td>
                            <td style="text-align:left">
                                Tipo Identificación
                            </td>
                            <td style="text-align:left">
                                Nombre
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="3">
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width:100%; height: 202px;">
                        <tr>
                            <td colspan="5" style="text-align:left">
                                <strong>DATOS DEL CRÉDITO</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <strong>Número Radicación</strong>
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td colspan="2" style="text-align:left">
                                Línea de crédito</td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                                    Enabled="false" Width="252px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 261px; text-align:left" colspan="2">
                                &nbsp;Monto
                            </td>
                            <td style="width: 134px; text-align:left" colspan="2">
                                Plazo
                            </td>
                            <td style="text-align:left">
                                Periodicidad
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 261px; text-align:left; height: 38px;" colspan="2">
                                <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="width: 134px; text-align:left; height: 38px;" colspan="2">
                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left; height: 38px;">
                                <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                                    Enabled="false" Width="180px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                Valor de la Cuota
                            </td>
                            <td style="text-align:left" colspan="2">
                                Forma de Pago
                            </td>
                            <td style="text-align: left">
                                Estado
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                <asp:TextBox ID="txtValor_cuota" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                                <br />
                            </td>
                            <td style="text-align:left" colspan="2">
                                <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                Saldo de Capital
                            </td>
                            <td style="text-align:left">
                                Fecha Próximo Pago</td>
                            <td colspan="2" style="text-align:left">
                                Fecha Último Pago</td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                <asp:TextBox ID="txtSaldoCapital" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left" colspan="2">
                                <asp:TextBox ID="txtFechaProxPago" runat="server" cssClass="textbox" 
                                    Height="23px" maxlength="10" 
                                    Width="106px" Enabled="False"></asp:TextBox>
                             
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtFechaUltPago" runat="server" cssClass="textbox" 
                                    Height="23px" maxlength="10"                                    
                                    Width="106px" Enabled="False"></asp:TextBox>
                               
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="height: 9px" colspan="3">
                <strong>DATOS DE LA CONDONACIÓN</strong></td>
        </tr>
        <tr>
            <td class="tdI" style="height: 67px">
                <strong>Valores adeudados del Crédito<asp:GridView ID="gvDistPagosPendCuotas" 
                    AllowPaging="True" runat="server" 
                    Width="100%" PageSize="20"
                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"                   
                    style="font-size: small" 
                    onrowdatabound="gvDistPagosPendCuotas_RowDataBound" ShowFooter="True">
                    <Columns>
                        <asp:BoundField DataField="NumCuota" HeaderText="No.Cuota" />
                        <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" />
                        <asp:BoundField DataField="Capital" HeaderText="Capital" />
                        <asp:BoundField DataField="IntCte" HeaderText="Int Corriente" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="IntMora" HeaderText="Int Mora" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" />                        
                        <asp:BoundField DataField="iva_leymipyme" HeaderText="Iva Ley MiPyme" />
                        <asp:BoundField DataField="" HeaderText="Póliza" />
                        <asp:BoundField DataField="otros" HeaderText="Otros" />
                        <asp:BoundField DataField="total" HeaderText="Valor Pagar sin Cobr." />
                        <asp:BoundField DataField="cobranzas" HeaderText="Cobranza" />
                         <asp:BoundField DataField="totalconhonorarios" 
                            HeaderText="Total a Pagar con Cobr." />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <div class="align-rt">
                    Valor A Condonar</strong>
                <strong>
                                <asp:TextBox ID="txttotal" runat="server" 
                    CssClass="textbox" />
                </strong>&nbsp;&nbsp;
                </div>
            </td>
            <td class="tdI" style="height: 67px">
                &nbsp;</td>
            <td class="tdI" rowspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" style="height: 20px" colspan="2">
                <strong>
                <asp:Label ID="lblTotalRegPendCuotas" runat="server" Visible="False" />
                <asp:Label ID="lblInfoPendCuotas" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                </strong>
            </td>
        </tr>
        </table>

</asp:Content>