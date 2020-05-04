<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="5" width="100%">
                <tr>
                    <td style="text-align: left; width: 170px">Código de Inversión</td>
                    <td style="text-align: left">
                        <asp:Label ID="lblCodigo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Número título&nbsp;</td>
                    <td style="text-align: left">                        
                        <asp:TextBox ID="txtNroTitulo" runat="server" CssClass="textbox" MaxLength="128" Width="250px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Valor Capital
                    </td>
                    <td style="text-align: left">
                        <uc1:decimales ID="txtVrCapital" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Tasa Interes
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtTasaInteres" runat="server" CssClass="textbox" Style="text-align:right" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="rfvTasa" runat="server" Enabled="True" FilterType="Numbers, Custom"
                        TargetControlID="txtTasaInteres" ValidChars="," />&nbsp;Nominal Anual
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Valor Interes</td>
                    <td style="text-align: left">
                        <uc1:decimales ID="txtVrInteres" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Plazo
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="120px" Style="text-align:right" OnTextChanged="txtPlazo_TextChanged" AutoPostBack="True" />
                        <asp:FilteredTextBoxExtender ID="rfvPlazo" runat="server" Enabled="True" FilterType="Numbers, Custom"
                        TargetControlID="txtPlazo" ValidChars=".," />&nbsp;Dìas
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Fecha Emision
                    </td>
                    <td style="text-align: left">
                        <uc1:fecha ID="txtFechaEmi" runat="server" OnTextChanged="txtFechaEmi_TextChanged" AutoPostBack="True" />                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Tipo Calendario
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlTipoCalendario" runat="server" AutoPostBack="true" CssClass="textbox" 
                            onselectedindexchanged="ddlTipoCalendario_SelectedIndexChanged"  Enabled="True" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Fecha Vencimiento
                    </td>
                    <td style="text-align: left">
                        <uc1:fecha ID="txtFechaVenc" runat="server" Enabled="False" />                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Tipo de Inversión
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoInv" runat="server" CssClass="textbox" Width="250px"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 170px">Entidad
                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCodPersona" runat="server" Visible="false" />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px"
                            Style="text-align: left" AutoPostBack="True" OnTextChanged="txtIdentificacion_TextChanged" />
                        <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                            OnClick="btnConsultaPersonas_Click" />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="380px" Style="text-align: left"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="text-align: left; width: 170px">Código de Cuenta
                        <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                    </td>
                    <td style="text-align: left; margin-left: 40px;">
                        <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                            Style="text-align: left" BackColor="#F4F5FF" Width="100px" OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                        <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btn8" runat="server" Text="..." Height="26px"
                            OnClick="btnListadoPlan_Click" />                        
                        <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                            Width="380px" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;color: #FF3300">
                        Se&nbsp;<asp:Label ID="lblMsj" runat="server" ></asp:Label>&nbsp; correctamente los datos
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>

