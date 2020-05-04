<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvEstCueObl" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">
            <div id="gvDiv">
            <table cellpadding="5" cellspacing="0" style="width: 95%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                                        <strong>Estado de Cuenta de la Obligación</strong></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td> 
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width:95%;">
                                <tr valign="top">
                                 <td style="text-align: left">
                                        No Obligación<br/>
                                        <asp:TextBox ID="txtNroObligacion" runat="server" Enabled="false" 
                                            CssClass="textbox" Width="88px" ></asp:TextBox>
                                    </td>
                                     <td style="text-align: center">
                                        Pagaré<br/>
                                        <asp:TextBox ID="txtPagare" runat="server" Enabled="false" CssClass="textbox" 
                                             Width="84px" ></asp:TextBox>
                                    </td>
                                     <td style="text-align: center">
                                        Estado<br/>
                                        <asp:TextBox ID="txtEstado" runat="server" Enabled="false" CssClass="textbox" 
                                             Width="115px" ></asp:TextBox>
                                    </td>
                                    <td class="gridIco" style="text-align: center">
                                        Fecha Solicitud<br/>
                                        <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="textbox" Enabled="false"
                                            Width="90px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">
                                        Fecha Desembolso<br/>
                                        <asp:TextBox ID="txtFechaDesembolso" runat="server" CssClass="textbox" Enabled="false"
                                            MaxLength="12" Width="90px"></asp:TextBox>
                                    </td>
                                     <td style="text-align: center">
                                        Fecha Inicio<br/>
                                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="textbox" Enabled="false"
                                            MaxLength="12" Width="90px"></asp:TextBox>
                                    </td>
                                     <td style="text-align: center">
                                        Fecha Terminación<br/>
                                        <asp:TextBox ID="txtFechaTerminacion" runat="server" CssClass="textbox" Enabled="false"
                                            MaxLength="12" Width="90px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">
                                        F. Ult Pago<br/>
                                       <asp:TextBox ID="txtFechaUltPago" CssClass="textbox" MaxLength="10" 
                                              enabled="false" runat="server" Width="90px"></asp:TextBox>
                                    </td>
                                      <td style="text-align: center">
                                        F.Prox Pago<br/>
                                       <asp:TextBox ID="txtFechaProxPago" CssClass="textbox" MaxLength="10" 
                                              enabled="false" runat="server" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel4" runat="server">
                            <table style="width:90%;" border="0">
                                <tr valign="top">
                                    <td style="width: 460px; text-align: left">
                                        Entidad<br/>
                                         <asp:DropDownList ID="ddlEntidad" runat="server" 
                                            Height="29px" Width="139px" Enabled="false" CssClass="dropdown">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: center; width: 621px;">
                                        Monto Solicitado<br/>
                                        <uc1:decimales ID="txtMontoSol" enabled="false" runat="server" CssClass="textbox" width="95px"  />
                                    </td>
                                    <td class="gridIco" style="width: 300px; text-align: center">
                                        Monto Aprobado<br/>
                                         <uc1:decimales ID="txtMontoApro" runat="server" enabled="false" CssClass="textbox" width="95px"  />
                                    </td>
                                    <td style="width: 631px; text-align: center">
                                        Tipo Moneda<br/>
                                       <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="dropdown" enabled="false"
                                            Height="26px" Width="104px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 631px; text-align: center">
                                        Cuotas Pagadas<br/>
                                        <asp:TextBox ID="txtCuotasPag" CssClass="textbox" width="66px" Enabled="false" 
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 631px; text-align: center">
                                        Cuota<br/>
                                        <uc1:decimales ID="txtValCuota" runat="server" enabled="false" CssClass="textbox" width="95px"  /> 
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                    <td>
                                    </td>
                                 </tr>
                            </table>
                            <table style="width:100%;" border="0">
                                 <tr valign="top">
                                    <td style="width: 624px; text-align: left">
                                        Plazo<br/>
                                        <asp:TextBox ID="txtPlazo" CssClass="textbox" MaxLength="2" Width="53px" 
                                            runat="server" Enabled="False"></asp:TextBox> 
                                    </td>
                                     <td style="width: 972px; text-align: left">
                                        <asp:UpdatePanel ID="upGracia" runat="server">                                     
                                        <ContentTemplate>
                                             Plazo Gracia<br/>
                                            <asp:TextBox ID="txtGracia" CssClass="textbox" MaxLength="2" Width="36px" 
                                                 runat="server" Enabled="False"></asp:TextBox>                                            
                                            <asp:DropDownList ID="ddlTipoGracia" runat="server" AutoPostBack="True" 
                                                 CssClass="dropdown" Height="27px" Width="109px" Enabled="False">
                                                 <asp:ListItem Value="0">Sin Gracia</asp:ListItem>
                                                 <asp:ListItem Value="1">Sobre Capital</asp:ListItem>
                                                 <asp:ListItem Value="2">Muerta</asp:ListItem>
                                             </asp:DropDownList>
                                         </ContentTemplate>
                                         </asp:UpdatePanel>
                                    </td>
                                    <td class="gridIco" style="width: 199px; text-align: left">
                                        Periodicidad Cuotas<br/>
                                        <asp:DropDownList ID="ddlPeriodCuotas" runat="server" CssClass="textbox" 
                                            Height="27px" Width="143px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 901px; text-align: left">
                                        <asp:RadioButtonList ID="rbCalculoTasa" runat="server" 
                                            style="font-size: xx-small" AutoPostBack="True"  Enabled="False">
                                            <asp:ListItem Value="1">Tasa Fija</asp:ListItem>                                        
                                            <asp:ListItem Value="2">Tasa Variable</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 401px; text-align: left">
                                        Tipo de Tasa<br/>
                                        <asp:DropDownList ID="ddlTipoTasa" runat="server" CssClass="textbox" 
                                            Height="27px" Width="149px" AutoPostBack="True" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 271px; text-align: left">
                                        Valor Tasa (%)<br/>                            
                                        <asp:TextBox ID="txtValorTasa" runat="server" CssClass="textbox" enabled="False" 
                                            Height="22px" width="88px"></asp:TextBox>                          
                                    </td>                        
                                    <td style="width: 700px; text-align: left">
                                        Puntos Adicionales(%)<br/>
                                       <asp:TextBox ID="txtPuntosads" CssClass="textbox" width="100px" MaxLength="6" runat="server" Enabled="False"></asp:TextBox>                          
                                    </td>
                                     <td>
                                         Saldo Capital<br />
                                         <uc1:decimales ID="txtSaldo" runat="server" CssClass="textbox" enabled="false" 
                                             width="95px" />
                                     </td>
                                </tr>
                                <tr> 
                                     <td>
                                     </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="text-align: center">
                    <td>
                        <div id="DivButtons" style="text-align: left">
                            <asp:ImageButton ID="btnImprimir" runat="server" 
                                 ImageUrl="~/Images/btnImprimir.jpg" onclick="btnImprimir_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDetPagPend" runat="server" Text="Ir a Detalle Pagos Pendientes" 
                                 onclick="btnDetPagPend_Click" CssClass="btn8"/>
                        </div>
                    </td>            
                </tr>
                <tr style="text-align: left">
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <strong>Movimientos de la Obligación</strong>
                    </td>
                </tr>
                 <tr style="text-align: left">
                    <td>
                        <asp:GridView ID="gvMovimiento" runat="server"
                            AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical" style="font-size: x-small" 
                            Width="862px" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                    <asp:BoundField DataField="nrocuota"  HeaderText="No Cuota" />
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha Cuota" />
                                    <asp:BoundField DataField="fechapago" DataFormatString="{0:d}" HeaderText="Fecha Pago" />
                                    <asp:BoundField DataField="cod_ope"  HeaderText="No Operación" />
                                    <asp:BoundField DataField="tipo_ope"  HeaderText="Tipo Operación" />
                                     <asp:BoundField DataField="num_comp"  HeaderText="Num. Comp" />
                                      <asp:BoundField DataField="tipo_comp"  HeaderText="Tipo Comp" />
                                    <asp:BoundField DataField="tipo_mov"  HeaderText="Tipo Mov" />
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
                                <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}">
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
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <strong>Plan de Pagos</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:GridView ID="gvRelMovObl" runat="server"
                            AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical" style="font-size: x-small" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="nrocuota"  HeaderText="No Cuota" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha Cuota" />
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
                                <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}">
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="40%">
                            <tr valign="top">
                               <td align="center"> COMPONENTES ADICIONALES <br />
                                    <asp:GridView ID="gvComponente" runat="server" Width="100%" AutoGenerateColumns="False"  
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px"
                                        Style="font-size: xx-small; font-weight: 700; text-align: center;">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="nomcomponente"  HeaderText="Componente" />
                                            <asp:BoundField DataField="nomformula"  HeaderText="Formula" />
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
                               <td align="center">
                                    PAGOS EXTRAORDINARIOS<br/>
                                    <asp:GridView ID="gvPagosExt" runat="server" Width="100%" 
                                        AutoGenerateColumns="False"  BackColor="White" BorderColor="#DEDFDE" 
                                        BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px"
                                        Style="font-size: xx-small; font-weight: 700; text-align: center;">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="NOM_PERIODICIDAD"  HeaderText="Periodo" />
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

                    </td>
                </tr>
            </table>
            </div> 
        </asp:View>
        <asp:View ID="View1" runat="server" >
            <asp:Button ID="btnCerrar" runat="server" Text="Regresar al Estado de Cuenta" 
                onclick="btnCerrar_Click" CssClass="btn8"/>
            <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                <LocalReport ReportPath="Page\Obligaciones\EstadoCuenta\ReporteObligacion.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>

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
</asp:Content>