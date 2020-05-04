<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master"  CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br/><br/>
    <table cellpadding="5" cellspacing="0" style="width: 95%">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                                <strong>Pago de Cuota de Obligaciones Financieras</strong></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td> 
        </tr>
        <tr style="text-align: left">
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width:100%;">
                        <tr valign="top">
                            <td style="text-align: left">
                                No Obligación<br/>
                                <asp:TextBox ID="txtNroObligacion" runat="server" Enabled="false" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                Pagaré<br/>
                                <asp:TextBox ID="txtPagare" runat="server" Enabled="false" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                Estado<br/>
                                <asp:TextBox ID="txtEstado" runat="server" Enabled="false" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td class="gridIco" style="text-align: left">
                                 Entidad<br/>
                                 <asp:DropDownList ID="ddlEntidad" runat="server" 
                                    Height="27px" Width="183px" Enabled="false" CssClass="dropdown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                               Monto Aprobado<br/>
                               <uc1:decimales ID="txtMontoApro" enabled="false" runat="server" CssClass="textbox" width="95px"  />
                            </td>
                            <td style="text-align: left">
                               Saldo<br/>
                                  <uc1:decimales ID="txtSaldo" enabled="false" runat="server" CssClass="textbox" width="95px"  />
                            </td>
                            <td style="text-align: left">
                                Fecha Desembolso<br/>
                                <asp:TextBox ID="txtFechaProxPago" runat="server" CssClass="textbox" Enabled="false"
                                    MaxLength="12" Width="149px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
         <tr>
             <td align="left">
                 <strong>Escoja la cuota que va a Pagar</strong><br /><br />
                 <asp:UpdatePanel ID="UPGrilla" runat="server">
                 <ContentTemplate>
                 <span style="font-size: x-small">Mostrar</span> 
                 <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" 
                     CssClass="dropdown" onselectedindexchanged="ddlPageSize_SelectedIndexChanged" 
                     Width="63px"></asp:DropDownList> <span style="font-size: x-small">Cuotas
                 </span>
                <asp:GridView ID="gvObPlanPago" runat="server" width="89%"
                    AutoGenerateColumns="False" PageSize="5" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" OnRowCommand="gvObPlanPago_RowCommand" style="font-size: small" AllowPaging="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDistPagos" runat="server" 
                                    CommandArgument="<%# Container.DataItemIndex%>" 
                                    CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg" 
                                    ToolTip="Dist Pagos" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nrocuota"  HeaderText="No Cuota" />
                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Cuota" />
                        <asp:BoundField DataField="amort_cap" HeaderText="Capital" DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_corriente" HeaderText="Interes Corriente" DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_mora" HeaderText="Interes Mora" DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="seguro" HeaderText="Seguro" DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
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
                </ContentTemplate>
                </asp:UpdatePanel>
             </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(".numeric").numeric();
        $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        $(".positive").numeric({ negative: false }, function () { alert("No negative values"); this.value = ""; this.focus(); });
        $(".positive-integer").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
        $("#remove").click(
		function (e) {
		    e.preventDefault();
		    $(".numeric,.integer,.positive").removeNumeric();
		}
	    );
    </script>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeRegObPlanPago" runat="server" Enabled="True"
       TargetControlID="HiddenField1" PopupControlID="Panels1"
       BackgroundCssClass="modalBackground" DropShadow="true"     
        CancelControlID="CancelButton" OkControlID=""
        OnCancelScript="onCancel()">
    </asp:ModalPopupExtender>

   <asp:Panel ID="Panels1" runat="server" BackColor="White" CssClass="modalPopup">
    <asp:UpdatePanel ID="UpdatePanels1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="AceptarButton"/>
            <asp:AsyncPostBackTrigger ControlID="ddlEntidad2" EventName="SelectedIndexChanged" />            
        </Triggers>
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0" style="border: medium groove #0000FF; width: 400px; " border="0">
                <tr>
                    <td class="tdI" colspan="2" style="text-align: left">
                        <div class="gridHeader" style="height: 22px; width: 100%; text-align: center;">
                            PAGO DE OBLIGACIONES
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>                        
                        Fecha de Pago
                    </td>
                    <td>                        
                        <asp:TextBox ID="txtFechaPago" runat="server" CssClass="textbox" MaxLength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaPago" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFechaPago" runat="server" ErrorMessage="Debe ingresar la fecha de pago"
                            ControlToValidate="txtFechaPago" Display="Dynamic" Style="font-size: xx-small;
                            color: #FF0000" ValidationGroup="vgGrabarPago"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Forma Pago
                    </td>
                    <td> 
                        <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" 
                            CssClass="textbox" Width="90%"
                            Style="text-align: left" 
                            onselectedindexchanged="ddlFormaPago_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="PanelFormaP" runat="server">
                            <table style="width:100%">
                                <tr>
                                    <td colspan="2">
                                        Entidad<br />
                                        <asp:DropDownList ID="ddlEntidad2" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlEntidad2_SelectedIndexChanged" Style="text-align: left"
                                            Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Cuenta
                                        <br />
                                        <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" Style="text-align: left"
                                            Width="90%" onselectedindexchanged="ddlCuenta_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                <td>
                                    Número de Cheque
                                </td>
                                <td>
                                <asp:TextBox ID="txtNumSop" runat="server" CssClass="textbox" Width="90px" Enabled="false"/>
                                </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr> 
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        No Cuota<br />
                        &nbsp;&nbsp;<asp:TextBox ID="txtNroCuota" runat="server" CssClass="textbox" Enabled="false" Width="55px"></asp:TextBox>
                    </td>
                    <td>
                        Fecha de la Cuota<br />
                        <asp:TextBox ID="txtFechaCuota" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr> 
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <b>Componente</b>
                    </td>
                    <td>
                        <b>Valor</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        Capital
                    </td>
                    <td>
                        <uc1:decimales ID="txtCapital" runat="server" CssClass="textbox" Width="163px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Int. Corriente
                    </td>
                    <td>
                        <uc1:decimales ID="txtIntCorr" runat="server" CssClass="textbox" Width="163px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Interes Mora
                    </td>
                    <td>
                        <uc1:decimales ID="txtIntMora" runat="server" CssClass="textbox" Width="163px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Seguro
                    </td>
                    <td>
                        <uc1:decimales ID="txtSeguro" runat="server" CssClass="textbox" Width="163px" />
                    </td>
                </tr>
                <tr> 
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <br />
                        <asp:Button ID="AceptarButton" runat="server" Text="Guardar" CssClass="btn8" OnClick="AceptarButton_Click" Height="25px" Width="80px"  ValidationGroup="vgGrabarPago" />&#160;
                        <asp:Button ID="CancelButton" runat="server" Text="Cancelar" CssClass="btn8" OnClick="CancelButton_Click"  Height="25px" Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>


