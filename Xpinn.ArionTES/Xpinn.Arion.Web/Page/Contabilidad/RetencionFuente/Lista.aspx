<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Certificado Impuesto :." Culture="es-CO" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register TagPrefix="ucDrop" TagName="dropdownmultiple" Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" %>

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
                return screen.width - 320;
            }
            else {
                return 1000;
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
                            <ucDrop:dropdownmultiple ID="ddlTipoImpuesto" runat="server" CssClass="textbox" Width="250px" ></ucDrop:dropdownmultiple>
                        </td>
                        <td style="text-align: left; width: 200px">
                          &nbsp;
                        </td>
                    </tr>
                     <tr>
                       <td class="tdI" colspan="2" style="text-align: left; width: 500px">
                            Período<br />
                            <uc:fecha ID="txtFecIni" runat="server" CssClass="textbox" Width="85px" />
                            &nbsp;a&nbsp;
                            <uc:fecha ID="txtFecFin" runat="server" CssClass="textbox" Width="85px" />
                        </td>                       
                        <td class="tdD" style="text-align: left; width:200px">
                            &nbsp;
                        </td>                       
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align: left; width: 140px">
                            Persona<br />
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
                         <td class="tdD" style="text-align: left; width:200px">
                            &nbsp;
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
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td style="text-align:left">
                    <br />
                       <strong>Detalle de movimientos</strong>
                    </td>
                </tr>               
                <tr>
                    <td style="text-align:left">
                     <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />
                        &nbsp;
                           <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
                               OnPageIndexChanging="gvLista_PageIndexChanging" HeaderStyle-CssClass="gridHeader"
                               PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: xx-small"
                               ShowHeaderWhenEmpty="True" AllowPaging="False" PageSize="20">
                               <Columns>
                                   <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco"><HeaderTemplate><asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged" AutoPostBack="True" />
                                       </HeaderTemplate><ItemTemplate><asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" /></ItemTemplate><HeaderStyle CssClass="gridIco"></HeaderStyle><ItemStyle CssClass="gridIco"></ItemStyle>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                   <asp:BoundField DataField="nombre" HeaderText="Nombre"><ItemStyle HorizontalAlign="Left" Width="250px" /></asp:BoundField>
                                   <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                   <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                   <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                   <asp:BoundField DataField="email" HeaderText="E-Mail" />
                                   <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha Expedicion">
                                     <ItemStyle HorizontalAlign="center" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="baseimp" HeaderText="Base" DataFormatString="{0:n}">
                                      <ItemStyle HorizontalAlign="Right" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje" DataFormatString="{0:N2}">
                                      <ItemStyle HorizontalAlign="Right" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="valor" DataFormatString="{0:N2}" HeaderText="Valor" >
                                     <ItemStyle HorizontalAlign="Right" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="cod_cuenta" HeaderText="Cod. Cuenta" >
                                     <ItemStyle HorizontalAlign="left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="nom_cuenta" HeaderText="Nom. Cuenta" >
                                     <ItemStyle HorizontalAlign="left" />
                                   </asp:BoundField>                                  
                               </Columns>
                               <HeaderStyle CssClass="gridHeader" />
                               <PagerStyle CssClass="gridPager" />
                               <RowStyle CssClass="gridItem" />
                               <FooterStyle CssClass="gridHeader" />
                           </asp:GridView>                       
                    </td>
                </tr>                
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
            </asp:Panel>
             <asp:Panel ID="PanelImpresion" runat="server">
               
            </asp:Panel>            
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td style="text-align:left">
                        <hr width="100%" />
                        &nbsp;
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8"  Height="25px" Width="120px"
                            onclick="btnDatos_Click" Text="Visualizar Datos" />
                             &#160;&#160;
                                <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                    Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                         <iframe id="frmPrint" name="IframeName" width="100%" src="Retencion.aspx" height="500px"
                               runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvRetencion" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                            Width="100%">
                            <LocalReport ReportPath="Page\Contabilidad\RetencionFuente\rptRetencionFuente.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>