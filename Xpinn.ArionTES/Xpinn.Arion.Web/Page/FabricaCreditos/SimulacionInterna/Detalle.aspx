<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>    
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
    function ValidaDecimal(dato) {
        var valor = dato.indexOf(",");
        if ((window.event.keyCode > 47 && window.event.keyCode < 58) || window.event.keyCode == 44) {
            
            if (window.event.keyCode == 44) {
                if (valor >= 0) {
                    window.event.returnValue = false;
                }
            }
        }
        else {
            window.event.returnValue = false;
        }
    }
</script> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
  
    </asp:ScriptManager>
    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="Pantalla" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                &nbsp;<table style="width: 100%;">
                    <tr>
                        <td colspan="3">                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel3" runat="server" Width="435px">
                                <table style="width: 75%;">
                                    <tr>
                                        <td style="width: 235px; text-align: left">
                                            Fecha Simulación:&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Enabled="False" Style="background-color: #99CCFF" Width="154px" />
                                            <asp:MaskedEditExtender ID="txtFecha_MaskedEditExtender" runat="server" Mask="99/99/9999"
                                                MaskType="Date" TargetControlID="txtFecha" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="color: #0033CC; font-size: medium; text-align: left">
                            <strong>Condiciones Deseadas del Crédito:</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-size: xx-small;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 56px;" colspan="3">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 167px; text-align: left">
                                            Monto Solicitado:
                                        </td>
                                        <td style="width: 180px; text-align: left">
                                            <uc2:decimales ID="txtMonto" runat="server" AutoPostBack_="false" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px; text-align: left">
                                            Plazo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 180px; text-align: left">
                                            <uc2:decimales ID="txtPlazo" runat="server" AutoPostBack_="false" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px; text-align: left">
                                            <asp:Label ID="Lbltasa" runat="server" Text="Tasa"></asp:Label>
                                            &nbsp;Interés Mensual:</td>
                                        <td ;="" align="left" style="width: 180px">
                                            <asp:TextBox ID="txttasa" runat="server" text-align= "left"
                                                onkeypress="return ValidaDecimal(this.value);" onunload="txtSeguro_Unload" 
                                                Width="132px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px; text-align: left">
                                            Periodicidad: </td>
                                        <td style="width: 180px; text-align: left;">
                                            <asp:DropDownList ID="lstPeriodicidad" runat="server" CssClass="textbox" 
                                                Width="139px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px; text-align: left">
                                            Tasa Seguro Ej.(0,030)</td>
                                        <td style="width: 180px; text-align: left">
                                            <asp:TextBox ID="txtSeguro" text-align= "left" onkeypress ="return ValidaDecimal(this.value);" 
                                                runat="server" Width="131px"></asp:TextBox>
                                            
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px; text-align: left">
                                           Fecha de Primer Pago</td>
                                        <td style="width: 180px; text-align: left">
                                            <ucFecha:fecha ID="txtFechaPrimerPago" runat="server" style="text-align: center" Width="130px"  Enabled="True" />                                    
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 180px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnPlanPagos" runat="server" 
                                ImageUrl="~/Images/btnPlanPagos.jpg" onclick="btnPlanPagos_Click" 
                                Width="140px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
             
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             
              <asp:accordion ID="acordionPlanPagos" runat="server" 
                FadeTransitions="True" FramesPerSecond="50" Width="947px" 
                TransitionDuration="200" HeaderCssClass="accordionCabecera" SuppressHeaderPostbacks="true"
                ContentCssClass="accordionContenido" Height="350px" >
                <Panes>
                    <asp:AccordionPane ID="AccordionPane7" runat="server" ContentCssClass="" HeaderCssClass="">
                     <Header>
                        <a>PLAN DE PAGOS</a>
                     </Header>
                     <Content>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" onclientclick="btnExportar_Click" 
                            Text="Exportar a Excel" />  
                        <asp:GridView ID="gvPlanPagos" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="Horizontal" 
                            ShowHeaderWhenEmpty="True" Height="292px" 
                            onpageindexchanging="gvPlanPagos_PageIndexChanging" style="font-size: small"
                            onselectedindexchanged="gvPlanPagos_SelectedIndexChanged" Width="927px" >
                            <Columns>
                                <asp:BoundField DataField="numerocuota" HeaderText="No. Cuota"  
                                    ItemStyle-HorizontalAlign="Left" >
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="valorcuota" HeaderText="Valor Cuota"  
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                </asp:BoundField>                                                              
                                <asp:BoundField DataField="capital" HeaderText="Capital"  
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="interes" HeaderText="Interes" DataFormatString="{0:c}"  
                                    ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                </asp:BoundField>
                                   <asp:BoundField DataField="seguro" HeaderText="Seguro"   
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="sal_pendiente" HeaderText="Saldo Pendiente"   
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                                    <ItemStyle HorizontalAlign="Right" Width="150px"/>
                                </asp:BoundField>
                              
                                 <asp:BoundField HeaderText=""   
                                    ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" Width="20px"/>
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                      </Content>
                    </asp:AccordionPane>
                </Panes>
              </asp:accordion>
            <br />
        </asp:View>

    </asp:MultiView>
</asp:Content>
