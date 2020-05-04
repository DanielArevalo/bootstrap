<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Conciliacion :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
   
   <script type="text/javascript">
       function ActiveTabChanged(sender, e) {
       }

       var HighlightAnimations = {};

       function Highlight(el) {
           if (HighlightAnimations[el.uniqueID] == null) {
               HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                   AnimationName: "color",
                   duration: 0.5,
                   property: "style",
                   propertyKey: "backgroundColor",
                   startValue: "#FFFF90",
                   endValue: "#FFFFFF"
               }, el);
           }
           HighlightAnimations[el.uniqueID].stop();
           HighlightAnimations[el.uniqueID].play();
       }
    </script>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

   <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
   <asp:View ID="vwPrincipal" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="560px">
        <tr>
            <td style="width: 140px; text-align: left">
                Cuenta Bancaria :<br />
                <asp:DropDownList ID="ddlCuentaBanc" runat="server" CssClass="textbox" Width="90%"
                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCuentaBanc_SelectedIndexChanged"
                    AutoPostBack="True" />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" />
            </td>
            <td colspan="2" style="width: 280px; text-align: left">
                Entidad
                <br />
                <asp:Label ID="lblCodEntidad" runat="server" Visible="false" />
                <asp:TextBox ID="txtEntidad" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="width: 140px; text-align: left">
                Tipo de Cta
                <br />
                <asp:TextBox ID="txtTipoCuenta" runat="server" CssClass="textbox" Width="90%" />
            </td>
        </tr>
        <tr>
            <td style="width: 140px; text-align: left">
                Cod. Cuenta Contable<br />
                <asp:TextBox ID="txtCodCuenta" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td colspan="3" style="text-align: left">
                Nombre de la Cuenta Contable<br />
                <asp:TextBox ID="txtNombreCta" runat="server" CssClass="textbox" Width="90%" />
            </td>
        </tr>
        <tr>
            <td style="width: 140px; text-align: left">
                Fecha Inicial<br />
                <ucFecha:Fecha ID="txtFechaIni" runat="server" Enabled="true" />
            </td>
            <td style="width: 140px; text-align: left">
                Fecha Final<br />
                <ucFecha:Fecha ID="txtFechaFin" runat="server" Enabled="true" />
            </td>
            <td style="width: 140px; text-align: left">
                #Extracto
                <br />
                <asp:DropDownList ID="ddlExtracto" runat="server" CssClass="textbox" Width="90%"
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlExtracto_SelectedIndexChanged" />
            </td>
            <td style="width: 140px; text-align: left">
                F. Extracto
                <br />
                <ucFecha:Fecha ID="txtFechaExtrac" runat="server" Enabled="true" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>


    <asp:MultiView ID="mvACH" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwResumen" runat="server">
            <table>
                <tr>
                    <td  colspan="2" style="text-align: left">
                        <strong style="text-align: left">Resumen de la Conciliación Bancaria:</strong>
                    </td>
                    
                    <td>
                       <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                            OnClientClick="btnDetalle_Click" Text="+ Ver Detalle" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="idresumen"
                            Style="font-size: x-small" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('idresumen')%>" /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text="<%# Bind('descripcion')%>" /></ItemTemplate>
                                   <ItemStyle
                                        HorizontalAlign="Left" Width="80%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemValor" runat="server" Text='<%# Bind("valor", "{0:n}") %>' /></ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <FooterStyle CssClass="gridHeader" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Elaborado Por<br />
                        <asp:TextBox ID="txtUsuElabora" runat="server" CssClass="textbox" Width="200px" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Revisado Por<br />
                        <asp:TextBox ID="txtUsuRevisado" runat="server" CssClass="textbox" Width="200px"
                            Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Aprobado por<br />
                        <asp:TextBox ID="txtUsuAprobado" runat="server" CssClass="textbox" Width="200px"
                            Enabled="false" />
                    </td>
                </tr>
            </table>
        </asp:View>
        
        <asp:View ID="vwDetalle" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" style="text-align: left">
                        <strong style="text-align: left">Detalle de la Conciliación Bancaria:</strong>
                    </td>
                    <td>
                        <asp:Button ID="btnVerResumen" runat="server" CssClass="btn8" OnClick="btnVerResumen_Click"
                            OnClientClick="btnVerResumen_Click" Text="+ Ver Resumen" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:TabContainer runat="server" ID="TabsDetalle" OnClientActiveTabChanged="ActiveTabChanged"
                            ActiveTabIndex="0" Style="margin-right: 5px" CssClass="CustomTabStyle" 
                            Width="100%">
                            <asp:TabPanel ID="tabDetalleContabilidad" runat="server">
                                <HeaderTemplate>
                                    Partidas en la Contabilidad y no en Extracto</HeaderTemplate>
                                <ContentTemplate>
                                    <table id="Table3" border="0" cellpadding="1" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Cheque Pendientes de Cobro</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel1" runat="server">
                                            <div style="overflow: scroll; width: 100% ; height:350px">
                                                <asp:GridView ID="gvChequePend1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                    
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField HeaderText="Codigo" DataField="iddetalle" Visible="False"/>--%>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="#Cheque" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Beneficiario"  DataField="beneficiario"/>
                                                        <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>                                                        
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView> 
                                                </div> 
                                                </asp:Panel>                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoCheque" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Consignaciones Pendientes de Registrar en Extracto</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel2" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvConsigPend1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha" />
                                                        <asp:BoundField HeaderText="Referencia"  DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Concepto" DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>   
                                                </div>
                                                </asp:Panel>                                             
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoConsig" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Notas de Crédito contables no Registradas en Extracto</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel3" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvNotasCred1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="Referencia" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Concepto" DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>  
                                                </div> 
                                                </asp:Panel>                                             
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoCredito" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Notas de Débito contables no Registradas en Extracto</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel4" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvNotasDeb1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="Referencia" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Concepto" DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>  
                                                </div>  
                                                </asp:Panel>                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoDebito" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="tabDetalleExtracto" runat="server">
                                <HeaderTemplate>
                                    Partida en Extracto y no en Contabilidad</HeaderTemplate>
                                <ContentTemplate>
                                    <table id="Table1" border="0" cellpadding="1" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Cheque Pendientes de Contabilizar</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel5" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvChequePend2" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                       <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="#Cheque" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Beneficiario"  DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView> 
                                                </div> 
                                                </asp:Panel>                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoCheque2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Consignaciones Pendientes de Contabilizar</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel6" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvConsigPend2" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="Referencia" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Concepto" DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>   
                                                </div>  
                                                </asp:Panel>                                           
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoConsig2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Notas de Crédito Bancarias pendientes de contabilizar</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel7" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvNotasCred2" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="Referencia" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Concepto" DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>
                                                </div>
                                                </asp:Panel>                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoCredito2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;
                                                width: 100%;">
                                                <strong>Notas de Débito Bancarias pendientes de contabilizar</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Panel id="panel8" runat="server">
                                            <div style="overflow:scroll ; width:100% ; height:350px;">
                                                <asp:GridView ID="gvNotasDeb2" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetalle"
                                                    Style="font-size: x-small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigo" runat="server" Text="<%# Bind('iddetalle')%>" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" DataField="fecha"/>
                                                        <asp:BoundField HeaderText="Referencia" DataField="referencia"/>
                                                        <asp:BoundField HeaderText="Concepto" DataField="beneficiario"/>
                                                         <asp:BoundField DataFormatString="{0:n}" HeaderText="Valor" DataField="valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Días" DataField="dias">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Num. Comp" DataField="num_comp">                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                       <asp:BoundField HeaderText="Tipo Comp"  DataField="tipo_comp">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Observación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtObservacion" runat="server" Text="<%# Bind('observacion')%>" Width="140px" /></ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>
                                                </div>  
                                                </asp:Panel>                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblInfoDebito2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                                    Visible="False" />
                                                    <br />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    </asp:View>
    </asp:MultiView>

     <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos0" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                        </td>
                </tr>
                <tr>
                    <td>                        
                        <rsweb:ReportViewer ID="rvReporteMensajeria" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\ConciliacionBancaria\Conciliacion\rptConciliacion.rdlc">
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

    <uc4:mensajegrabar ID="ctlMensaje" runat="server"/>

</asp:Content>