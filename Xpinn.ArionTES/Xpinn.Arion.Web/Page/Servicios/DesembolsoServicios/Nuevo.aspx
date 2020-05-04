<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Detalle" %>  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:Panel ID="panel1" runat="server">
        <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwLista" runat="server">
            
                <asp:Panel ID="panelGeneral" runat="server">
                <table style="width: 740px; text-align: center" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="text-align: left; width: 140px">
                            Num. Servicio<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 140px">
                            Fec. Solicitud<br />
                            <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            &nbsp;
                        </td>
                        <td style="text-align: left; width: 320px" colspan="2">
                           &nbsp;
                        </td>                   
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 140px">
                            Solicitante<br />
                            <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                Width="0px" Visible="false" />
                            <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                            <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                OnClick="btnConsultaPersonas_Click" Text="..." visible="false"/>
                        </td>
                        <td style="text-align: left; width: 440px" colspan="3">
                            Nombre<br />
                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                Width="90%" />
                            <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left; width: 160px">
                            &nbsp;                                        
                        </td>                   
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 280px" colspan="2">
                            Linea de Servicio<br />                        
                            <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="260px" 
                                onselectedindexchanged="ddlLinea_SelectedIndexChanged" 
                                AutoPostBack="True" />
                        </td>
                        <td style="text-align: left; width: 300px" colspan="2">
                            <asp:Label ID="lblPlan" runat="server" Text="Plan"/><br />
                            <asp:DropDownList ID="ddlPlan" runat="server" CssClass="textbox" Width="240px" 
                                AppendDataBoundItems="True" />
                        </td>
                        <td style="text-align: left; width: 160px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="5">
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        Valor Total<br />
                                        <uc1:decimales ID="txtValorTotal" runat="server" Width="140px"/>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblValorCuota" runat="server" Text="Valor de la Cuota" /><br />
                                        <asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" width="140px" Style="text-align: right" />
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtValorCuota"
                                            FilterType="Custom, Numbers" ValidChars="+-=/*().," />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblNumCuotas" runat="server" Text="Num. Cuotas" /><br />
                                        <asp:TextBox ID="txtNumCuotas" runat="server" CssClass="textbox" Width="140px" />
                                        <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtNumCuotas"
                                            FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblPeriodicidad" runat="server" Text="Periodicidad" /><br />
                                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="160px" />
                                    </td>
                                    <td style="text-align: left;">
                                        Forma de Pago<br />
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="160px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>             
                    <tr>
                        <td style="text-align: left; vertical-align:top">
                            Proveedor<br />
                            <asp:Label id="lblCodProveedor" runat="server"/>
                        </td>
                        <td style="text-align: left;">
                            <br />
                            <asp:TextBox ID="txtIdentificacionTitu" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td colspan="2" style="text-align: left;">
                            Nombre<br />
                            <asp:TextBox ID="txtNombreTit" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="text-align: left; width: 160px">
                            <asp:Label ID="lblNumPreImpreso" runat="server" Visible="false" Text="Nro. Oden" /><br />
                            <asp:TextBox ID="txtNumPreImpreso" runat="server" CssClass="textbox" Width="90%" Visible="false" />
                        </td>
                    </tr>               
                </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
                    <asp:Panel id="PanelFinal" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br /><br /><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large; color:Red">
                                Solicitud de servicio Aprobada&nbsp;correctamente.<br /> 
                                </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

    <asp:HiddenField ID="HiddenField1" runat="server" />      
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
     
</asp:Content>