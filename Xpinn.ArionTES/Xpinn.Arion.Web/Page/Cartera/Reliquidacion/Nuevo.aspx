<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Reliquidación :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc1" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register src="../../../General/Controles/PlanPagos.ascx" tagname="planpagos" tagprefix="uc3" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctrCuotasExtras" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel id="panelEncabezado" runat="server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                            <td class="logo" style="text-align:left">
                                &nbsp;</td>
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
                            <td style="text-align:left">
                                &nbsp;</td>
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
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

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
        <tr>
            <td colspan="4" style="text-align:left">
                <strong>DATOS DE LAS CUOTAS EXTRAS</strong>
            </td>
            <td style="text-align:left">
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
    </table>     
    <table border="0" cellpadding="5" cellspacing="0" width="80%">
        <tr>
            <td style="text-align: left; width: 151px;"></td>
            <td style="text-align: left; width: 151px;"></td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
        </tr>            
        <tr>
            <td style="text-align: left;" colspan="4">                                  
                <div>
                    <uc1:ctrCuotasExtras runat="server" ID="CuotasExtras" />
                </div>
            </td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
        </tr>
    </table>
    </asp:Panel>

    <asp:MultiView ID="mvReliquidacion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel ID="UpdReliquidacion" runat="server">        
            <ContentTemplate>
            <table border="0" cellpadding="5" cellspacing="0" width="80%" style="font-size: xx-small" >
                <tr>                    
                    <td colspan="5" style="text-align:left">
                        <strong><span style="font-size: small">DATOS DE LA RELIQUIDACION</span></strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="font-size: small">Fecha Reliquidación</span><br />
                        <uc1:fecha ID="txtFechaReliquidacion" runat="server" />
                    </td>
                    <td>
                        <span style="font-size: small">Valor a Reliquidar</span><br />
                        <uc2:decimales ID="txtValorReliquidar" runat="server" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        <span style="font-size: small">Plazo</span><br />
                        <uc2:decimales ID="txtNuePlazo" runat="server"  />
                    </td>
                    <td style="text-align: left">                                
                        <span style="font-size: small">Periodicidad</span><br />
                        <asp:DropDownList ID="ddlNuePeriodicidad" runat="server" CssClass="dropdown" Width="161px" Height="23" />
                    </td>
                    <td style="text-align: left; font-size: small;">                                            
                        F.Proximo Pago<br />
                        <uc1:fecha ID="txtNueFechaProximoPago" runat="server" Enabled="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 80%">
                            <tr>
                                <td style="text-align: left">
                                    <span style="font-size: small">Atributo</span>
                                    <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlAtributo" runat="server" 
                                                onselectedindexchanged="ddlAtributo_SelectedIndexChanged" 
                                                ontextchanged="ddlAtributo_TextChanged" Width="224px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 223px">
                                    <br />
                                    <asp:CheckBox ID="chkCobraMora" runat="server" style="font-size: small" 
                                        Text="Cobrar Mora" />
                                </td>
                                <td style="width: 165px">
                                    &nbsp;</td>
                                <td style="width: 165px">
                                    &nbsp;</td>
                                <td style="width: 165px">
                                    &nbsp;</td>
                                <td style="width: 165px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="3">
                                        <asp:RadioButtonList ID="rbCalculoTasa" runat="server" 
                                            RepeatDirection="Horizontal" style="font-size: small" AutoPostBack="True" 
                                            onselectedindexchanged="rbCalculoTasa_SelectedIndexChanged" >
                                            <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                                            <asp:ListItem Value="3">Histórico Fijo</asp:ListItem>
                                            <asp:ListItem Value="5">Histórico Variable</asp:ListItem>
                                        </asp:RadioButtonList>
                                </td>
                                <td style="width: 165px">
                                </td>
                                <td style="width: 165px">
                                    &nbsp;
                                </td>
                                <td style="width: 165px">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:MultiView ID="mvAtributo" runat="server" ActiveViewIndex="0">
                            <asp:View ID="vwHistorico" runat="server">
                                <table style="width: 80%">
                                    <tr>
                                        <td style="text-align: left">
                                            Tipo Histórico<br />
                                            <asp:DropDownList ID="ddlHistorico" runat="server" Width="224px">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2">
                                            Spread<br />
                                            <ucDecimal:Decimal ID="txtDesviacion" runat="server"/>
                                        </td>
                                        <td style="width: 165px">
                                            &nbsp;</td>
                                        <td style="width: 165px">
                                            &nbsp;</td>
                                        <td style="width: 165px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="vwFija" runat="server">                                
                                <table style="width: 80%">
                                    <tr>
                                        <td style="text-align: left">
                                            Tasa<br />
                                            <%--<ucDecimal:Decimal ID="txtTasa" runat="server" />--%>      
                                            <asp:TextBox ID="txtTasa" runat="server" Style="font-size: x-small;
                                                text-align: right" TipoLetra="XX-Small" Habilitado="True" AutoPostBack="True"
                                                onblur="Page_Load(this)" OnPreRender="txtTasa_PreRender" Width="80px" 
                                                Enabled="True" Width_="80" />                                          
                                        </td>
                                        <td colspan="2">
                                            Tipo de Tasa<br />
                                            <asp:DropDownList ID="ddlTipoTasa" runat="server" Width="224px" />
                                        </td>
                                        <td style="width: 165px">
                                            &nbsp;</td>
                                        <td style="width: 165px">
                                            &nbsp;</td>
                                        <td style="width: 165px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                        </asp:View>
                    </asp:MultiView>
                    </td>
                </tr>
            </table> 
            </ContentTemplate>    
            </asp:UpdatePanel>     
            <table border="0" width="100%" >
                <tr>
                    <td style="text-align:center">
                        <asp:ImageButton ID="btnAdelante" runat="server" 
                            ImageUrl="~/Images/btnPlanPagos.jpg" onclick="btnAdelante_Click" 
                            ValidationGroup="vgGuardar" Width="100px" Height="25px" />                 
                    </td>
                </tr>
            </table>        
        </asp:View>
        <asp:View ID="vwPlanPagos" runat="server">
            <div style="text-align: left">
                &nbsp;&nbsp;Nueva Cuota<br />            
                &nbsp;<uc2:decimales ID="txtCuota" runat="server" Enabled="false" />
            </div>
            <br />
            PLAN DE PAGOS
            <uc3:planpagos ID="gvPlanPagos" runat="server" />
            <table border="0" width="100%" >
                <tr>
                    <td style="text-align:center">
                        <asp:ImageButton ID="btnRegresar" runat="server" 
                            ImageUrl="~/Images/btnRegresar.jpg" onclick="btnRegresar_Click" 
                            ValidationGroup="vgGuardar" Width="100px" Height="25px" />                 
                    </td>
                </tr>
            </table>   
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Reliquidación Realizada Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>