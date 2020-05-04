<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="gvDiv">
        <table cellpadding="0" cellspacing="0" style="width: 97%" border="0">
             <tr>
                <td  class="tdI">
                    <table width="58%">
                        <tr>
                            <td align="left" colspan="3">
                                <strong>Ingrese el perìodo de fechas para las cuales desea consultar el reporte:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 210px">
                                Fecha Inicial<br/>
                                <asp:TextBox ID="txtFechaIni"  CssClass="textbox"  maxlength="10" runat="server"></asp:TextBox>
                                 <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        PopupButtonID="Image1" 
                                        TargetControlID="txtFechaIni"
                                        Format="dd/MM/yyyy" >
                                    </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtFechaIni" runat="server" ValidationGroup="valida" ForeColor="Red" ControlToValidate="txtFechaIni"  ErrorMessage="*"></asp:RequiredFieldValidator><br/>
                                <asp:RegularExpressionValidator ID="revtxtFechaIni" runat="server" ValidationGroup="valida" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaIni" ForeColor="Red" ErrorMessage="Formato Fecha Invalida"></asp:RegularExpressionValidator>
                            </td>
                            <td width="5%">&#160;
                            </td>
                            <td align="left">
                                Fecha Final<br/>
                                <asp:TextBox ID="txtFechaFin"   CssClass="textbox" maxlength="10" runat="server"></asp:TextBox>
                                <img id="Image2" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                    PopupButtonID="Image2" 
                                    TargetControlID="txtFechaFin"
                                    Format="dd/MM/yyyy" >
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtFechaFin" runat="server" ValidationGroup="valida" ControlToValidate="txtFechaFin" ForeColor="Red"  ErrorMessage="*"></asp:RequiredFieldValidator><br/>
                                <asp:RegularExpressionValidator ID="revtxtFechaFin" runat="server" ValidationGroup="valida" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaFin" ForeColor="Red" ErrorMessage="Formato Fecha Invalida"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>
             <tr>
                <td  class="tdI"> 
                    <table>
                        <tr>
                            <td align="left" colspan="3">
                                <strong>Ingrese criterios de bùsqueda:</strong></td>
                            <td align="left" style="width: 16px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left">
                                Oficina <br/>
                                <asp:DropDownList ID="ddlOficinas" CssClass="textbox" runat="server" Height="22px" Width="170px" style="font-size: x-small" AutoPostBack="True" OnSelectedIndexChanged="ddlOficinas_SelectedIndexChanged">
                                </asp:DropDownList>  
                            </td>
                             <td style="width: 16px">&nbsp;</td>
                             <td>
                                Caja<br/>
                                    <asp:DropDownList ID="ddlCajas"   CssClass="textbox" runat="server" Height="22px" Width="170px" AutoPostBack="true" 
                                     onselectedindexchanged="ddlCajas_SelectedIndexChanged" 
                                     style="font-size: x-small">
                                    </asp:DropDownList>  
                             </td>
                            <td style="width: 16px">&nbsp;</td>
                             <td>
                                Cajero<br />
                                    <asp:DropDownList ID="ddlCajeros" CssClass="textbox"  runat="server" Height="22px" 
                                     Width="200px" style="font-size: x-small">
                                    </asp:DropDownList>  
                             </td>
                            <td style="width: 16px">&nbsp;</td>
                            <td>
                                 Moneda<br />
                                <asp:DropDownList ID="ddlMonedas" CssClass="textbox"  runat="server" Height="22px" Width="120px" 
                                     style="font-size: x-small">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="DivButtons">
                        <asp:Button ID="btnImprimir" runat="server" ValidationGroup="valida" Text="Imprimir" />&#160;
                        <asp:Button ID="btnExportarExcel" ValidationGroup="valida" runat="server" Text="Exportar" 
                          onclick="btnExportarExcel_Click" />&#160;
                        <asp:Button ID="btnGenerar" runat="server" Text="Generar" ValidationGroup="valida"
                        onclick="btnGenerar_Click" />&#160;
                        <asp:Button ID="btnReporte" runat="server" Text="Reporte" ValidationGroup="valida"
                        onclick="btnReporte_Click" />
                    </div>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td align="left">
                    <asp:GridView ID="gvMovimiento" runat="server"
                        AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Vertical" 
                        onpageindexchanging="gvMovimiento_PageIndexChanging" 
                        style="font-size: small">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" />
                            <asp:BoundField DataField="nom_caja" HeaderText="Caja" />
                            <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                              <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_comp" HeaderText="Num.Comp">
                              <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_comp" HeaderText="T.Comp">
                              <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_movimiento"  HeaderText="Fecha" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="tipo_movimiento" HeaderText="Tipo Movimiento" />
                            <asp:BoundField DataField="iden_cliente" HeaderText="Identificacion" />
                            <asp:BoundField DataField="nom_cliente" HeaderText="Nombre Cliente" />
                            <asp:BoundField DataField="nom_tipo_producto" HeaderText="Producto" />
                              <asp:BoundField DataField="num_producto" HeaderText="Num. Producto" />
                            <asp:BoundField DataField="valor_pago" HeaderText="Valor" DataFormatString="{0:N0}">                                 
                              <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_tipo_tran" HeaderText="Tipo Transacción" />
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
                <td align="right">
                    <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                    <asp:TextBox ID="txtTotalMovs" runat="server" Width="80px" Enabled="False" 
                        style="direction:rtl"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >
                &#160;
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="HiddenField1" runat="server" />    
        <asp:ModalPopupExtender ID="mpeNuevo" runat="server" 
            PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
             BackgroundCssClass="backgroundColor" >        
        </asp:ModalPopupExtender>     

        <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" 
            style="text-align: left" BorderStyle="Solid" Width="800px">
    
            <div id="popupcontainer" style="width:792px">                    
                <div class="row popupcontainertitle">
                    <div class="cell popupcontainercell" style="text-align: center">
                       Información
                    </div>  
                </div>
                <div class="row">
                    <div class="cell popupcontainercell">
                        <div id="ordereditcontainer">
                            <asp:UpdatePanel ID="upActividadReg" runat="server">
                                <ContentTemplate>   
                                <div class="row">
                                    <div class="cell ordereditcell">
                                    <br />
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                                        Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="763px">
                                        <LocalReport ReportPath="Page\CajaFin\RepMovimientoCaja\ReporteMovimietosCaja.rdlc">
                                        </LocalReport>
                                    </rsweb:ReportViewer>
                                    <br>
                                    </div>
                                </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <div class="row">
                        <div class="cell" style="text-align:right">
                        <asp:Button ID="btnCloseReg" runat="server" Text="Cerrar" 
                            CssClass="button"  CausesValidation="false" 
                            Height="20px" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>   
    </asp:Panel>
    </div>
</asp:Content>
