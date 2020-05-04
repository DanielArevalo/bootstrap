<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="../../../General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" colspan="6">
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td style="text-align:left" colspan="2">
                                <strong>CONDONACIÓN DE INTERES</strong>
                                </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 180px; text-align:left">
                                <strong>Fecha de Condonación</strong></td>                              
                            <td style="text-align:left">                                  
                                <uc1:fecha ID="txtFechaCondonacion" runat="server" OneventoCambiar="txtFechaCondonacion_eventoCambiar" />                                
                              <%--  <asp:TextBox ID="txtFechaCondonacion" runat="server" cssClass="textbox" 
                                    Height="23px" maxlength="10"  
                                    Width="106px"  OnTextChanged="txtFechaCondonacion_eventoCambiar"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="Image1" TargetControlID="txtFechaCondonacion" 
                                    ViewStateMode="Enabled">
                                </asp:CalendarExtender>--%>
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
            <td class="tdI" colspan="6">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:60%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                Cod.Persona
                            </td>
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
                                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
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
            <td class="tdI" colspan="6">
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width:100%; height: 202px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="5" style="text-align:left">
                                <strong>DATOS DEL CRÉDITO</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" >
                                <strong>Número Radicación</strong>
                            </td>
                            <td style="text-align:left" >
                                Línea de crédito
                            </td>
                            <td style="text-align:left">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" >
                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left" >
                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                                    Enabled="false" Width="252px" />
                            </td>
                            <td style="text-align:left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 261px; text-align:left" >
                                &nbsp;Monto
                            </td>
                            <td style="width: 134px; text-align:left" >
                                Plazo
                            </td>
                            <td style="text-align:left">
                                Periodicidad
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 261px; text-align:left; height: 38px;" >
                                <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="width: 134px; text-align:left; height: 38px;" >
                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left; height: 38px;">
                                <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                                    Enabled="false" Width="180px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" >
                                Valor de la Cuota
                            </td>
                            <td style="text-align:left" >
                                Forma de Pago
                            </td>
                            <td style="text-align: left">
                                Estado
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" >
                                <uc2:decimales ID="txtValor_cuota" runat="server" CssClass="textbox" 
                                    Enabled="false" />                                
                            </td>
                            <td style="text-align:left" >
                                <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" >
                                Saldo de Capital
                            </td>
                            <td style="text-align:left">
                                Fecha Próximo Pago</td>
                            <td colspan="2" style="text-align:left">
                                Fecha Último Pago</td>
                        </tr>
                        <tr>
                            <td style="text-align:left" >
                                <uc2:decimales ID="txtSaldoCapital" runat="server" CssClass="textbox" 
                                    Enabled="false" />  
                            </td>
                            <td style="text-align:left" >
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
            <td class="tdI" colspan="6" style="height: 9px; text-align:left">
                <strong>DATOS DE LA CONDONACIÓN</strong>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="6" style="height: 81px">
                <strong>Valores adeudados del Crédito</strong>
                <div id="divValores" runat="server" style="overflow: scroll; height: 300px">
                    <asp:GridView ID="gvDistPagosPendCuotas" 
                        AllowPaging="False" runat="server" Width="90%" PageSize="20"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"                   
                        style="font-size: small" 
                        onrowdatabound="gvDistPagosPendCuotas_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:BoundField DataField="NumCuota" HeaderText="No.Cuota" />
                            <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" />
                            <asp:BoundField DataField="Capital" HeaderText="Capital" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="IntCte" HeaderText="Int Corriente" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="IntMora" HeaderText="Int Mora" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" ItemStyle-HorizontalAlign="Right" />                        
                            <asp:BoundField DataField="iva_leymipyme" HeaderText="Iva Ley MiPyme" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Poliza" HeaderText="Póliza" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="otros" HeaderText="Otros" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="total" HeaderText="Valor Pagar sin Cobr." ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="cobranzas" HeaderText="Cobranza" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="totalconhonorarios" HeaderText="Total a Pagar con Cobr." ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
                <asp:Label ID="lblTotalRegPendCuotas" runat="server" Visible="False" />                
                <asp:Label ID="lblInfoPendCuotas" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 267px">
                <strong>Valor a Condonar Interes Corriente</strong></td>
            <td class="tdI">
                <strong>Valor a Condonar Interes Mora</strong></td>
            <td class="tdI">
                &nbsp;</td>
            <td class="tdI">
                &nbsp;</td>
            <td class="tdI">
                &nbsp;</td>
            <td class="tdI">
                &nbsp;</td>
        </tr>        
        <tr>
            <td class="tdI" bgcolor="#0099FF" style="height: 35px">
                <asp:TextBox ID="txtvalinterescte" runat="server" 
                    CssClass="textbox" />
                <asp:Label ID="Lblinfointeres" runat="server" Visible="False" />
            </td>
            <td class="tdI" bgcolor="#0099FF" style="height: 35px">
                <asp:TextBox ID="txtvalinteresmora" runat="server" 
                    CssClass="textbox" />
                <asp:Label ID="Lblinfomora" runat="server" Visible="False" />
            </td>
            <td class="tdI" style="height: 35px">
                </td>
            <td class="tdI" style="height: 35px">
                </td>
            <td class="tdI" style="height: 35px">
                </td>
            <td class="tdI" style="height: 35px">
                </td>
        </tr>
    </table>
</asp:Content>