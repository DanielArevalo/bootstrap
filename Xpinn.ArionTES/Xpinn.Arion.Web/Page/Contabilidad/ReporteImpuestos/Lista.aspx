<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        }); 

        function gridviewScroll() { 
            $('#<%=gvLista.ClientID%>').gridviewScroll({ 
                width: CalcularAncho(),
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {            
            if (navigator.platform == 'Win32') {
                return screen.width - 350;
            }
            else {
                return 950;
            }
        }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvImpuestos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="2" cellspacing="0" width="700px">
                    <tr>
                        <td class="tdI" colspan="2" style="text-align: left; width: 500px">
                            Tipo de Impuesto<br />
                            <asp:DropDownList ID="ddlTipoImpuesto" runat="server" CssClass="textbox" Width="250px"
                                onselectedindexchanged="ddlTipoImpuesto_SelectedIndexChanged" AutoPostBack="True"/>
                        </td>
                        <td>
                          &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align: left; width: 140px">                            
                            Cuenta Contable<br />
                            <asp:DropDownList ID="ddlCodCuenta" runat="server"  AutoPostBack="True" CssClass="textbox"
                                Style="text-align: left" BackColor="#F4F5FF" Width="120px" 
                                onselectedindexchanged="ddlCodCuenta_SelectedIndexChanged"></asp:DropDownList>                                                        
                       </td>
                       <td class="tdI" style="text-align: left; width: 360px">    
                            Nombre de la cuenta<br />                       
                            <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                Width="90%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                        </td>
                        <td class="tdD" style="width: 200px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align: left; width: 140px">
                            Identificación<br />
                            <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                Width="50px" Visible="false" />
                            <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                            <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                OnClick="btnConsultaPersonas_Click" Text="..." />
                         </td>
                         <td class="tdI" style="text-align: left; width: 360px">
                            Nombres<br />
                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                Width="90%" />                           
                         </td>
                     </tr>
                     <tr>
                       <td class="tdI" colspan="2" style="text-align: left; width: 500px">
                            Período<br />
                            <uc:fecha ID="txtFecIni" runat="server" CssClass="textbox" Width="85px" />
                            &nbsp;a&nbsp;
                            <uc:fecha ID="txtFecFin" runat="server" CssClass="textbox" Width="85px" />
                        </td>                       
                        <td class="tdD" style="text-align: left">
                            Agrupado por<br />
                            <asp:RadioButtonList ID="rbAgrupacion" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Cuenta</asp:ListItem>
                                <asp:ListItem Value="2">Persona</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>                       
                    </tr>
                    <tr>
                        <td class="tdI" colspan="3">
                            <hr width="100%" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
           
           <asp:Panel ID ="panelGrilla" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td >
                    <br />
                       <strong>Detalle de movimientos</strong>
                    </td>
                </tr>               
                <tr>
                    <td>
                     <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />
                        &nbsp;
                        <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                            onclick="btnInforme_Click" Text="Visualizar Informe" />
                           <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
                               OnPageIndexChanging="gvLista_PageIndexChanging" HeaderStyle-CssClass="gridHeader"
                               PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: xx-small"
                               ShowHeaderWhenEmpty="True">
                               <Columns>
                                   <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                   <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                       <ItemStyle HorizontalAlign="Left" Width="250px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                   <asp:BoundField DataField="direccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Left" />
                                   <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                   <asp:BoundField DataField="email" HeaderText="E-Mail" />
                                   <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                   <asp:BoundField DataField="num_comprobante" HeaderText="Num. Comprobante">
                                       <ItemStyle HorizontalAlign="Right" />
                                       <ItemStyle HorizontalAlign="Left" Width="80px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="tipo_comprobante" HeaderText="Tipo Comprobante" />
                                   <asp:BoundField DataField="cod_cuenta" HeaderText="Cod. Cuenta"></asp:BoundField>
                                   <asp:BoundField DataField="nombre_cuenta" HeaderText="Nombre Cuenta"></asp:BoundField>
                                   <asp:BoundField DataField="baseimp" HeaderText="Base" DataFormatString="{0:c}">
                                   </asp:BoundField>
                                   <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje" DataFormatString="{0:N2}">
                                       <ItemStyle HorizontalAlign="Right" />
                                       <ItemStyle HorizontalAlign="Right" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="valor" DataFormatString="{0:c}" HeaderText="Valor" />
                               </Columns>
                               <HeaderStyle CssClass="gridHeader" />
                               <PagerStyle CssClass="gridPager" />
                               <RowStyle CssClass="gridItem" />
                           </asp:GridView>
                       
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                            onclick="btnDatos_Click" Text="Visualizar Datos" />
                    </td>
                </tr>
                <tr>
                    <td>                        
                        <rsweb:ReportViewer ID="rvImpuestos" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <localreport reportpath="Page\Contabilidad\ReporteImpuestos\ReporteImpuestos.rdlc">
                            <datasources>
                            <rsweb:ReportDataSource />
                            </datasources>
                            </localreport>
                        </rsweb:ReportViewer>                    
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>