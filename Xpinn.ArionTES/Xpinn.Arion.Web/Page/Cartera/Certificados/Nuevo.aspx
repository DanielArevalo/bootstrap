<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="~/General/Controles/fechaeditable.ascx" tagname="fecha" tagprefix="uc1" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register src="~/General/Controles/PlanPagos.ascx" tagname="planpagos" tagprefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 150px; text-align:left">
                                Identificación
                            </td>
                            <td style="text-align:left">
                                Tipo Identificación
                            </td>
                            <td style="text-align:left">
                                Nombre
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 150px; text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <asp:MultiView ID="mvCertificado" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                <tr>
                    <td colspan="4" style="text-align:left">
                        <strong>DATOS DEL CRÉDITO</strong>
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">
                        Número Radicación<br />
                        <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"  Enabled="false" />
                    </td>
                    <td colspan="3" style="text-align:left">
                        Línea de crédito<br />
                        <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                            Enabled="false" Width="347px" />
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">
                        Monto<br />
                        <uc2:decimales ID="txtMonto" runat="server" Enabled="false" />                                
                    </td>
                    <td style="text-align:left">
                        Plazo<br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align:left">
                        Periodicidad<br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align:left">
                        Valor de la Cuota<br />
                        <uc2:decimales ID="txtValor_cuota" runat="server" Enabled="false" />                                
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">
                        Forma de Pago<br />
                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">         
                        Moneda<br />
                        <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Enabled="false" />                       
                    </td>
                    <td style="text-align: left">    
                        F.Proximo Pago<br />
                        <asp:TextBox ID="txtFechaProximoPago" runat="server" CssClass="textbox" Enabled="false" />                               
                    </td>
                    <td style="text-align: left">    
                        F.Ultimo Pago <br />
                        <asp:TextBox ID="txtFechaUltimoPago" runat="server" CssClass="textbox" Enabled="false" />                              
                    </td>
                    <td style="text-align: left">     
                        &nbsp;</td>
                    <td style="text-align: left">     
                        &nbsp;</td>
                    <td style="text-align: left">     
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">     
                        Saldo Capital<br />
                        <uc2:decimales ID="txtSaldoCapital" runat="server" Enabled="false" />                              
                    </td>
                    <td style="text-align: left">
                        Estado<br />
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left">   
                        F.Aprobación<br />                         
                        <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />                                         
                    </td>
                    <td style="text-align: left">                                
                    </td>
                    <td style="text-align: left">                                
                        &nbsp;</td>
                    <td style="text-align: left">                                
                        &nbsp;</td>
                    <td style="text-align: left">                                
                        &nbsp;</td>
                </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="0" width="70%" >
                <tr>                    
                    <td colspan="2" style="text-align:left">
                        <strong>DATOS DE LA CERTIFICACION</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Valores Adeudados
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" 
                            Width="70%" DataKeyNames="cod_atr" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" style="font-size: small" 
                            ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Cod.Atr.">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodAtr" runat="server" Text='<%# Eval("cod_atr") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="Descripción" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="280px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("nom_atr") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblNomTotal" runat="server" Text='Total'/>
                                    </FooterTemplate>
                                </asp:TemplateField>                                   
                                <asp:TemplateField HeaderText="Valor"  >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Text='<%# Bind("valor", "{0:N}") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="textboxAlineadoDerecha" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" HorizontalAlign="Right"  />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>          
                    </td>
                    <td style="text-align:left">
                        <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                            <tr>
                                <td style="text-align: left">
                                    Fecha de la Certificación<br />
                                    <uc1:fecha ID="txtFechaCertificado" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    Valor Total a Certificar<br />
                                    <uc2:decimales ID="txtValorCertificado" runat="server" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>          
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" 
                onclientclick="btnRegresar_Click" 
                Text="Regresar" onclick="btnRegresar_Click" />
            <rsweb:ReportViewer ID="rvCertificado" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" AsyncRendering="false"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                <LocalReport ReportPath="Page\Cartera\Certificados\RepCertificado.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>


    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>