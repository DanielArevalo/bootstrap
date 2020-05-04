<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="tasa" TagPrefix="uctasa" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <style type="text/css">
        .ccsla, #rbCalculoTasa
        {
            display: block;
            width: 393px;
        }
        .style1
        {
            width: 321px;
        }
        .style3
        {
            width: 118px;
            }
        .style5
        {
            width: 321px;
            height: 2px;
        }
        .style10
        {
            height: 1px;
        }
        .style11
        {
            width: 140px;
            height: 1px;
        }
        </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="text-align: center; cellspacing="0" 
                cellpadding="0">
                <tr>
                    <td style="text-align: left">
                        Fec. Cierre<br />
                        <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True" Visible="False" Width="1px" />
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td style="text-align: left; " class="style1">
                        &nbsp;</td>
                    <td class="style1" style="text-align: left; ">
                        &nbsp;</td>
                    <td style="text-align: left; width: 140px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; ">
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                            ReadOnly="True" Visible="true" Width="150px" />
                        </td>
                    <td style="text-align: left; ">
                        &nbsp;</td>
                    <td style="text-align: left; ">Tipo. Identificación<br />
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" ClientIDMode="Static" CssClass="textbox" ReadOnly="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; ">&nbsp;</td>
                    <td style="text-align: left; ">&nbsp;</td>
                    <td style="text-align: left; ">
                        &nbsp;</td>
                    <td style="text-align: left; ">
                        Nombre<br />
                        <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" 
                            ReadOnly="True" Width="250px" />
                    </td>
                    <td class="style1" style="text-align: left; ">
                        <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" 
                            ControlToValidate="txtNomPersona" Display="Dynamic" 
                            ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0" 
                            Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="text-align: left; ">
                        &nbsp;</td>
                    <td style="text-align: left; width: 140px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="10" style="text-align: left; ">
                        <strong>Datos de la cuenta:</strong><hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; display: table">
                        Cuenta
                        <br />
                        <asp:TextBox ID="txtCuenta" runat="server" AutoPostBack="True" 
                            CssClass="textbox" EnableTheming="False" ReadOnly="True" Width="160px" />
                    </td>
                    <td style="text-align: left; ">
                        &nbsp;</td>
                    <td style="text-align: left; ">Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" ClientIDMode="Static" CssClass="textbox" Enabled="False" ReadOnly="true" Width="140px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; ">&nbsp;</td>
                    <td style="text-align: left; ">&nbsp;</td>
                    <td class="style3" style="text-align: left; ">
                    </td>
                    <td style="text-align: left; ">
                        Linea<br />
                        <asp:DropDownList ID="ddLinea" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" Enabled="False" ReadOnly="true" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td class="style1" style="text-align: left; ">
                    </td>
                    <td style="text-align: left; ">
                        Fecha Apertura<ucFecha:fecha ID="txtFechaApertura" runat="server" 
                            CssClass="textbox" Width="240px" />
                    </td>
                    <td style="text-align: left; ">
                        Fecha Próx. Pago<br />
                        <ucFecha:fecha ID="txtFechaProximoPago" runat="server" CssClass="textbox" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; display: table">
                        Saldo Total<br />
                        <asp:TextBox ID="txtSaldoTotal" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" ReadOnly="true" Width="140px" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="text-align: left; ">
                        &nbsp;</td>
                    <td style="text-align: left; ">Cuota
                        <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" Enabled="False" ReadOnly="True" Width="90%" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtCuota" ValidChars="+-=/*()." />
                    </td>
                    <td style="text-align: left; ">&nbsp;</td>
                    <td style="text-align: left; ">&nbsp;</td>
                    <td style="text-align: left; ">
                        </td>
                    <td style="text-align: left; ">
                        Plazo<br />
                        <asp:TextBox ID="txtPlazo" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" ReadOnly="True" Width="140px" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="text-align: left; ">
                        </td>
                    <td style="text-align: left; ">
                        Periodicidad<br />
                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" 
                            ReadOnly="True" Enabled="False" />
                    </td>
                    <td style="text-align: left; ">
                        Fecha Vencimiento<uc2:fecha ID="txtfechaVenci" runat="server" CssClass="textbox" enabled="false" />
                        </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 198px; display: table">
                        Fec. Ult. Liquidación Int.<ucFecha:fecha ID="txtFechaInteres" runat="server" 
                            CssClass="textbox" />
                    </td>
                    <td style="text-align: left; " rowspan="3">
                        &nbsp;</td>
                    <td colspan="5" rowspan="3" style="text-align: left; ">
                        <asp:UpdatePanel ID="updtipoTasa" runat="server">
                            <ContentTemplate>
                                Tasa de Interes<asp:RadioButtonList ID="rbCalculoTasa" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbCalculoTasa_SelectedIndexChanged" RepeatDirection="Horizontal" Style="font-size: small" Width="350px">
                                    <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                                    <asp:ListItem Value="3">Histórico Fijo</asp:ListItem>
                                    <asp:ListItem Value="5">Histórico Variable</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Panel ID="PanelFija" runat="server" Width="181px">
                            <table style="width: 124%; height: 17px;">
                                <tr>
                                    <td style="text-align: left; width: 50%">Tasa<br />
                                        <asp:TextBox ID="txtTasa" runat="server" AutoPostBack="True" CssClass="textbox" OnTextChanged="txtTasa_TextChanged" Width="100px" />
                                        <asp:FilteredTextBoxExtender ID="ftb15" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTasa" ValidChars="," />
                                    </td>
                                    <td style="text-align: left; width: 50%">Tipo de Tasa<br />
                                        <asp:DropDownList ID="ddlTipoTasa" runat="server" CssClass="textbox"  Width="224px" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PanelHistorico" runat="server" Width="230px">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left; width: 50%">Tipo Histórico<br />
                                            <asp:DropDownList ID="ddlHistorico" runat="server" CssClass="textbox" Enabled="False" Width="224px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 50%">Spread<br />
                                            <asp:TextBox ID="txtDesviacion" runat="server" CssClass="textbox" Enabled="False" Width="100px" />
                                            <asp:FilteredTextBoxExtender ID="fte18" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtDesviacion" ValidChars="," />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                    <td class="style1" style="text-align: left; " rowspan="3">
                        &nbsp;</td>
                    <td style="text-align: left; ">
                        <br />
                        <asp:TextBox ID="txtTotInteres" Visible="false" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left; ">
                        <br />
                        <asp:TextBox ID="txtTotRetencion" Visible="false" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 198px; display: table">&nbsp;</td>
                    <td style="text-align: left; ">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; " colspan="10">
                        <strong>Datos de la Renovación:</strong><hr />
                        </td>
                </tr>
                    
                <tr>
                    
                    <td class="style10" style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkCapitalizaInteres" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="chkCapitalizaInteres_CheckedChanged" Text="Capitaliza Interes" />
                        <br />
                        Total Renovación
                        <br />
                        <asp:TextBox ID="txtTotalRenovar" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                  
            </table>    
            
                        <asp:Panel ID="PanelLiquidacion" Visible="true" runat="server" Width="100%">
                            <strong>Datos de la Liquidación</strong><br />Interes&nbsp;<br /><asp:TextBox ID="txtInteres" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            <br />
                            Interes Causado&nbsp;<br /><asp:TextBox ID="txtInteresCausado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            <br />
                            Menos Retencion:<br />&nbsp;&nbsp;<asp:TextBox ID="txtMenosRetencion" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            <br />
                            &nbsp;<asp:Label ID="lblmgf" runat="server" Text="Menos GMF"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtGmf" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblMenosDescuento" runat="server" Text="Menos Descuento"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtMenosDescuento" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblTotalPagar" runat="server" Text="Total a Pagar"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtTotalPagar" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                            </td>
                            &nbsp;</asp:Panel>    
            <asp:Panel ID="Panel5" runat="server" Width="100%">
                <table style="width: 583px;">
                    <tr>
                        <td style="width: 175px; text-align: left">
                            <strong>Forma de Pago</strong>
                        </td>
                        <td colspan="3" style="text-align: left">
                           <asp:DropDownList ID="ddlForma_Desem" runat="server" CssClass="textbox" Width="320px"
                            OnSelectedIndexChanged="ddlForma_Desem_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="panelCheque" runat="server">
                    <table>
                        <tr>
                            <td style="width: 175px; text-align: left">
                                <asp:Label ID="lblEntidadGiro" runat="server" Text="Entidad. de Giro"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlEntidad_giro" runat="server" AutoPostBack="True"
                                    CssClass="textbox"
                                    OnSelectedIndexChanged="ddlEntidad_giro_SelectedIndexChanged" Width="320px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 175px; text-align: left; height: 25px;">
                                <asp:Label ID="lblCuenta_Giro" runat="server" Text="Cuenta de Giro :"></asp:Label>
                            </td>
                            <td style="height: 25px">
                                <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox"
                                    Width="320px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelTrans" runat="server">
                    <table>
                        <tr>
                            <td style="width: 175px; text-align: left">Entidad
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox"
                                    Width="320px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 175px; text-align: left">Numero de Cuenta
                            </td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="125px" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtNum_cuenta" ValidChars="-,.()" />
                            </td>
                            <td style="width: 100px; text-align: center">Tipo Cuenta
                            </td>
                            <td style="height: 25px">
                                <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox"
                                    Width="191px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlCuentaAhorroVista" runat="server">
                    <table>
                        <tr>
                            <td style="text-align: left; width: 175px;">
                                <asp:Label ID="lblCuentaAhorroVista" runat="server" Style="text-align: left" Text="Numero Cuenta"></asp:Label>
                            </td>
                            <td style="width: 180px; text-align: left;">
                                <asp:DropDownList ID="ddlCuentaAhorroVista" runat="server" CssClass="textbox" Style="margin-left: 0px; text-align: left" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left; ">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                
            </asp:Panel>        
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                <%----%>
                    <td style="text-align: left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px" Width="120px"
                            OnClick="btnDatos_Click" Text="Visualizar Datos" />
                        &#160;&#160;&#160;&#160;&#160;
                        <asp:Button ID="btnImpresion" runat="server" CssClass="btn8" Height="28px" Width="120px"
                           OnClick="btnImpresion_Click" Text="Imprimir" /><%-- --%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: groove; float: left;" onclick="return frmPrint_onclick()"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:reportviewer id="rvReportMov" runat="server" font-names="Verdana" font-size="8pt"
                            enabled="false" interactivedeviceinfos="(Colección)" waitmessagefont-names="Verdana"
                            waitmessagefont-size="10pt" width="100%" height="500px"><localreport reportpath="Page\Programado\CierreCuentas\rptCierre.rdlc"><datasources><rsweb:ReportDataSource /></datasources></localreport></rsweb:reportviewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    
                               
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function frmPrint_onclick() {

        }

// ]]>
    </script>


</asp:Content>
