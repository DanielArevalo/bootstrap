<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <%--<asp:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager2" />--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script type="text/javascript">
        function PanelClick(sender, e) {
        }

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

        function ToggleHidden(value) {
            $find('<%=Tabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }

        function doPrint(theForm) {
            document.all.item("noprint").style.visibility = 'hidden'
            window.print()
            document.all.item("noprint").style.visibility = 'visible'
        }

        function imprSelec(muestra) {
            var ficha = document.getElementById(muestra);
            var ventimp = window.open(' ', 'popimpr');
            ventimp.document.write(ficha.innerHTML);
            ventimp.document.close();
            ventimp.print();
            ventimp.close();
        }
    </script>
    <div id="siprint">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tdI" style="width: 854px; font-size: x-small;" align="center">
                    <strong>Referenciador</strong>
                    <asp:Label ID="lblReferenciador" runat="server"></asp:Label>
                </td>
                <td class="tdI" colspan="3">&nbsp;
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel2" runat="server" Width="650px">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 133%">
                <tr>
                    <td style="width: 117px; font-size: x-small; text-align: left">No.Radicación
                    </td>
                    <td style="width: 112px; text-align: left">
                        <asp:TextBox ID="txtRadicacion" runat="server" BorderWidth="0px" CssClass="textbox"
                            Enabled="False" Width="173px"></asp:TextBox>
                    </td>
                    <td class="tdI" style="font-size: x-small; width: 80px;">&nbsp;
                    </td>
                    <td class="tdI" style="width: 179px">&nbsp;
                    <strong>
                        <asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown"
                            Enabled="False" Height="32px" Width="290px" Visible="False">
                        </asp:DropDownList>
                    </strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 117px; font-size: x-small; text-align: left">
                        <strong>Solicitante</strong>
                    </td>
                    <td class="logo" style="width: 112px">&nbsp;
                    </td>
                    <td class="tdI" style="font-size: x-small; width: 80px;">&nbsp;
                    </td>
                    <td class="tdI" style="width: 179px">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 117px; font-size: x-small; text-align: left">Nombre
                    </td>
                    <td class="logo" style="width: 112px; text-align: left">
                        <asp:TextBox ID="txtNombre" runat="server" BorderWidth="0px" CssClass="textbox" Enabled="False"
                            Width="346px"></asp:TextBox>
                    </td>
                    <td class="tdI" style="font-size: x-small; width: 80px; text-align: left">Identificación
                    </td>
                    <td style="width: 121px; text-align: left">
                        <asp:TextBox ID="txtIdentificacion" runat="server" BorderWidth="0px" CssClass="textbox"
                            Enabled="False" Width="183px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelReferencias" runat="server" Width="860px" Height="762px">
            <asp:TabContainer runat="server" ID="Tabs" ActiveTabIndex="2" Width="860">
                <asp:TabPanel runat="server" ID="PanelDatos" HeaderText="Datos">
                    <HeaderTemplate>
                        Datos
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table border="0" cellpadding="5" cellspacing="0" style="width: 21%">
                            <tr>
                                <td style="font-weight: bold; width: 1063px;">
                                    <asp:Label ID="Label40" runat="server" Text="Datos Solicitante:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1100px; text-align: left">Fecha nacimiento&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtFecha_nacimientopersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Estado civil&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtEstadocivilpersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Dirección
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtDirecciónPersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Teléfono&nbsp;&nbsp;&nbsp;
                                </td>
                                <td class="tdI" style="width: 116px; text-align: left">
                                    <asp:TextBox ID="txtTelefonopersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Barrio
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtbarriopersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Tipo de Vivienda
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtTipoviviendapersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Nombre Arrendatario
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtNombreArrendatariopersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Valor Arriendo
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtValorArriendo" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Teléfono Arrendatario
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtTelefonoarrendatario" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Personas a cargo
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtPersonasacargo" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">&nbsp;Actividad Económica
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtActividadpersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Ubicacion del Negocio
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtUbiacion_negocio" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Antigüedad del Negocio
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtAntiguedad_negocioperosna" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Nombre Empresa
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtNombre_empresapersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Actividad Empresa donde labora
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtActividad_empresapersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">NIT Empresa
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtNit_empresapersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Cargo
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txt_Cargopersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Salario
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtSalariopersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">Antigüedad en la Empresa
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtAntiguedad_empresapersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">Dirección de la Empresa
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtDreccion_Empresapersona" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; width: 1063px;">
                                    <asp:Label ID="Labeldaosconyuge" runat="server" Text="Datos Cónyuge:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">
                                    <asp:Label ID="lblTitNombreConyuge" runat="server" Text="Nombre Cónyuge"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtNombreconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td class="tdI" style="font-size: x-small; width: 1042px; text-align: left">
                                    <asp:Label ID="lblTitCedulaConyuge" runat="server" Text="Cédula Cónyuge"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtIdentificacionconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">
                                    <asp:Label ID="lblTitTelefonoConyuge" runat="server" Text="Teléfono"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtTelefonoconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">
                                    <asp:Label ID="lblTitDireccionConyuge" runat="server" Text="Dirección"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtDirecciónconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">
                                    <asp:Label ID="lblTitActividadConyuge" runat="server"
                                        Text="Actividad Económica"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtActividadconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="width: 1064px; font-size: x-small; text-align: left">
                                    <asp:Label ID="lblTitEmpresaConyuge" runat="server" Text="Nombre de la Empresa"></asp:Label>
                                </td>
                                <td style="width: 116px; text-align: left; text-align: left;">
                                    <asp:TextBox ID="txtNombre_empresaconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1063px; font-size: x-small; text-align: left;">&nbsp;<asp:Label ID="lblTitActEmpresaConyuge" runat="server"
                                    Text="Actividad Empresa donde trabaja"></asp:Label>
                                </td>
                                <td style="width: 172px;">
                                    <asp:TextBox ID="txtActividad_emprsaconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="font-size: x-small; width: 1064px; text-align: left;">
                                    <asp:Label ID="lblTitNitEmpresaConyuge" runat="server" Text="NIT de la Empresa"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 116px">
                                    <asp:TextBox ID="txtNit_empresaconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; width: 1063px; text-align: left">
                                    <asp:Label ID="lblTitCargoConyuge" runat="server" Text="Cargo de la Empresa"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtCargoconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="font-size: x-small; width: 1064px; text-align: left">
                                    <asp:Label ID="lblTitSalarioConyuge" runat="server" Text="Salario"></asp:Label>
                                </td>
                                <td style="font-size: x-small; text-align: left; width: 116px; text-align: left">
                                    <asp:TextBox ID="txtSalarioconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="font-size: x-small; width: 1063px; text-align: left">
                                    <asp:Label ID="lblTitAntiguedadConyuge" runat="server"
                                        Text="Antigüedad de la Empresa"></asp:Label>
                                </td>
                                <td class="tdI" style="width: 172px">
                                    <asp:TextBox ID="txtAntiguedad_empresaconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                                <td style="font-size: x-small; width: 1064px; text-align: left">
                                    <asp:Label ID="lblTitDirEmpresaConyuge" runat="server"
                                        Text="Dirección de la Empresa"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 116px; text-align: left">
                                    <asp:TextBox ID="txtDireccion_empresaconyuge" runat="server" BorderWidth="0px" CssClass="textbox"
                                        Enabled="False" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; width: 1063px; text-align: left">&nbsp;
                                </td>
                                <td style="font-size: x-small; width: 172px; text-align: left">&nbsp;
                                </td>
                                <td style="font-size: x-small; text-align: left; width: 1064px;">&nbsp;
                                </td>
                                <td style="font-size: x-small; text-align: left; width: 116px; text-align: left">&nbsp;
                                </td>
                            </tr>
                        </table>
                        <input id="btnImprimirDatos" type="button" value="Imprimir" onclick="imprSelec('siprint');" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="PanelReferenciacion" HeaderText="Confirmación de Datos">
                    <HeaderTemplate>
                        Confirmación de Datos
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="upSolicitante" runat="server">
                            <ContentTemplate>
                                <table style="width: 850">
                                    <tr>
                                        <td style="font-size: x-small">&nbsp;&nbsp;
                                        </td>
                                        <td style="text-align: center; background-color: #808080; color: #FFFFFF;">Solicitante
                                        </td>
                                        <td style="text-align: center; background-color: #808080; color: #FFFFFF;">
                                            <asp:Label ID="lblTitConyuge" runat="server" Text="Cónyuge"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small">&nbsp;</td>
                                        <td style="text-align: center; background-color: #808080; color: #FFFFFF;">&nbsp;</td>
                                        <td style="text-align: center; background-color: #808080; color: #FFFFFF;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta1" runat="server"
                                                Text="¿Por favor me confirma el número de su cédula?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP1" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP1" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta2" runat="server"
                                                Text="¿Me indica cuál su fecha de nacimiento?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP2" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP2" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta3" runat="server" Text="¿Cuál es su estado civil?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP3" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP3" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta4" runat="server"
                                                Text="¿Me confirma por favor la dirección de su residencia?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP4" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP4" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta5" runat="server"
                                                Text="¿Me indica su número de teléfono?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP5" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP5" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta6" runat="server" Text="¿A qué se dedica usted?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP6" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP6" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta7" runat="server"
                                                Text="¿Su vivienda es propia, familiar ó vive en arriendo?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP7" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP7" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta8" runat="server"
                                                Text="¿Me indica por favor el nombre de su arrendatario?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP8" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP8" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta9" runat="server"
                                                Text="¿Me puede dar un número de contacto de su arrendatario por favor?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP9" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyuge9" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta10" runat="server"
                                                Text="¿Cuál es el valor que paga por su arriendo?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP10" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP10" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:Label ID="lblPregunta11" runat="server"
                                                Text="¿Cuántas pesonas tiene a cargo?"></asp:Label>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlDeudorP11" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; background-color: #E8E7E6;">
                                            <asp:DropDownList ID="ddlConyugeP11" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: x-small; background-color: #E8E7E6;">Observaciones
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: x-small; background-color: #E8E7E6;">
                                            <asp:TextBox ID="txtObservacionDeudor" runat="server" BorderWidth="0px" Height="21px"
                                                Width="584px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <input id="btnImprimirConf" type="button" value="Imprimir" onclick="imprSelec('siprint');" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnGuardarSolicitante" runat="server" OnClick="btnGuardarSolicitante_Click"
                            Text="Guardar" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnGuardarParcial" runat="server" OnClick="btnGuardarParcial_Click" Text="Guardar Parcial" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="PanelMicrocredito" HeaderText="Confirmación de Referencias-Microcrédito">
                    <HeaderTemplate>
                        Confirmación Referencias-Microcrédito
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="PanelReferencia" runat="server" Width="450px">
                            <table width="850">
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Nombre Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:DropDownList ID="ddlNombreReferencia" runat="server" AutoPostBack="True" CssClass="textbox"
                                                Height="26px" OnSelectedIndexChanged="ddlNombreReferencia_SelectedIndexChanged"
                                                Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Tipo de Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtTipoReferencia" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Dirección Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtDireccionReferencia" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Teléfono Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtTelefonoReferencia" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Celular Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtCelularReferencia" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Teléfono Oficina Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtTelOficinaReferencia" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelPreguntas" runat="server" Width="650px">
                            <asp:UpdatePanel ID="upPreguntas" runat="server">
                                <ContentTemplate>
                                    <table width="850">
                                        <tr>
                                            <td style="font-size: x-small; background-color: #808080; width: 434px; color: #FFFFFF;">&#160;&#160;
                                            </td>
                                            <td style="text-align: center; background-color: #808080; color: #FFFFFF;">
                                                <strong>Respuestas</strong>
                                            </td>
                                            <td style="text-align: center; background-color: #808080; color: #FFFFFF;">
                                                <strong>Alertas</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito1" runat="server" Text="Referencia localizada"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito1" runat="server"
                                                    AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito1_SelectedIndexChanged"
                                                    Width="51px">
                                                    <asp:ListItem Value="0">Si</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta1" runat="server" BorderWidth="0px" CssClass="textbox" Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito2" runat="server" Text="¿Conoce al(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito2" runat="server"
                                                    AutoPostBack="True" Height="17px"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito2_SelectedIndexChanged"
                                                    Width="51px">
                                                    <asp:ListItem Value="0">Si</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta2" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito3" runat="server"
                                                    Text="¿Qué relación o parentesco tiene con el(la) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito3" runat="server"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito3_SelectedIndexChanged"
                                                    Style="height: 22px" AutoPostBack="True">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta3" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito4" runat="server"
                                                    Text="¿Hace cuántos años conoce al(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito4" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito4_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1"> Menos de 3 Meses</asp:ListItem>
                                                    <asp:ListItem Value="2">3 Meses a 1 Año</asp:ListItem>
                                                    <asp:ListItem Value="3">1 a 3 Años</asp:ListItem>
                                                    <asp:ListItem Value="4">3 a Mas Años</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta4" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito5" runat="server"
                                                    Text="¿Sabe en qué trabaja el(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito5" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito5_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta5" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito6" runat="server"
                                                    Text="¿Sabe dónde está ubicado el negocio del(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito6" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito6_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta6" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito7" runat="server"
                                                    Text="¿Sabe hace cuánto tiene el negocio el(la) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito7" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito7_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta7" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito8" runat="server"
                                                    Text="¿Sabe en qué barrio vive el(la) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito8" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlRespuestaMicrocredito8_SelectedIndexChanged"
                                                    Style="height: 22px">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta8" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito9" runat="server"
                                                    Text="¿La vivienda dónde él(ella) vive es: propia, familiar o arrendada?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito9" runat="server"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito9_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta9" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito10" runat="server"
                                                    Text="¿Sabe cuál es el estado civil del(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito10" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito10_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta10" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito11" runat="server"
                                                    Text="¿Sabe cuántas personas tiene a cargo el(la) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito11" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito11_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta11" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito12" runat="server"
                                                    Text="¿Considera una persona honesta al(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito12" runat="server"
                                                    AutoPostBack="True" Height="17px"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito12_SelectedIndexChanged"
                                                    Width="50px">
                                                    <asp:ListItem Value="0">Si</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta12" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaMicrocredito13" runat="server"
                                                    Text="¿Podría confirmarme su dirección?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaMicrocredito13" runat="server"
                                                    AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaMicrocredito13_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlerta13" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: x-small; background-color: #E8E7E6;">¿Desea agregar alguna observación particular acerca del al(a) Sr(a)?
                                            </td>
                                            <td style="font-size: x-small; background-color: #E8E7E6;">&#160;&#160;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-size: x-small; background-color: #E8E7E6; text-align: center;">&nbsp;
                                                <asp:TextBox ID="txtObservacionMicrocredito" runat="server" Height="21px"
                                                    Width="644px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <input id="btnImprimirMicro" type="button" value="Imprimir" onclick="imprSelec('siprint');" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <div id="Div1">
                                <asp:Button ID="btnGuardarMicro" runat="server" OnClick="btnGuardarReferencia_Click"
                                    Text="Guardar" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="PanelConsumo" HeaderText="Confirmación de Referencias-Consumo">
                    <HeaderTemplate>
                        Confirmación Referencias-ConsumoDatos
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="PanelReferenciaConsumo" runat="server" Width="450px">
                            <table width="850">
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Nombre Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:DropDownList ID="ddlNombreReferenciaCon" runat="server"
                                                AutoPostBack="True" CssClass="textbox"
                                                Height="26px" OnSelectedIndexChanged="ddlNombreReferenciaCon_SelectedIndexChanged"
                                                Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Tipo de Referencia
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtTipoReferenciaCon" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Dirección
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtDireccionReferenciaCon" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; background-color: #808080; text-align: right; width: 429px; color: #FFFFFF;">Teléfono
                                        <td style="background-color: #808080; text-align: center;">
                                            <asp:TextBox ID="txtTelefonoReferenciaCon" runat="server" Width="205px"></asp:TextBox>
                                        </td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelPreguntasConsumo" runat="server" Width="650px">
                            <asp:UpdatePanel ID="upPreguntasConsumo" runat="server">
                                <ContentTemplate>
                                    <table width="850">
                                        <tr>
                                            <td style="font-size: x-small; background-color: #808080; width: 434px; color: #FFFFFF;">&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: center; background-color: #808080; color: #FFFFFF;">
                                                <strong>Respuestas</strong>
                                            </td>
                                            <td style="text-align: center; background-color: #808080; color: #FFFFFF;">
                                                <strong>Alertas</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo1" runat="server" Text="¿Conoce al(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo1" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo1_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Si</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo1" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo2" runat="server"
                                                    Text="¿Cuál es la actividad principal de la empresa?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo2" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo2_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo2" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo3" runat="server"
                                                    Text="¿Me confirma por favor el nombre de la empresa?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo3" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo3_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo3" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo4" runat="server"
                                                    Text="¿Me confirma el número de NIT por favor?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo4" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo4_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo4" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo5" runat="server"
                                                    Text="¿Cuál es el cargo que desempeña el(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo5" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo5_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo5" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo6" runat="server"
                                                    Text="¿Me confirma por favor el salario del(a) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo6" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo6_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo6" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo7" runat="server"
                                                    Text="¿Cuánto tiempo lleva trabajando en su empresa el(la) Sr(a)? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo7" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo7_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo7" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo8" runat="server"
                                                    Text="¿Me confirma la dirección de la empresa por favor?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo8" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo8_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
                                                    <asp:ListItem Value="1">Coincide</asp:ListItem>
                                                    <asp:ListItem Value="2">No Coincide</asp:ListItem>
                                                    <asp:ListItem Value="3">No Sabe</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo8" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo9" runat="server"
                                                    Text="¿Considera una persona responsable al(a) Sr(a)?"></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo9" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo9_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Si</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo9" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: x-small; background-color: #E8E7E6; width: 434px;">
                                                <asp:Label ID="lblPreguntaConsumo10" runat="server"
                                                    Text="¿Considera  una persona honesta al Sr? "></asp:Label>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:DropDownList ID="ddlRespuestaConsumo10" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlRespuestaConsumo10_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Si</asp:ListItem>
                                                    <asp:ListItem Value="1">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtAlertaConsumo10" runat="server" BorderWidth="0px" CssClass="textbox"
                                                    Enabled="False" Width="116px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: x-small; background-color: #E8E7E6;">¿Desea agregar alguna observación particular acerca del sr.(a)?
                                            </td>
                                            <td style="font-size: x-small; background-color: #E8E7E6;">&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-size: x-small; background-color: #E8E7E6;">
                                                <asp:TextBox ID="txtObservacionConsumo" runat="server" Height="21px"
                                                    Width="644px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <input id="btnImprimirCon" type="button" value="Imprimir" onclick="imprSelec('siprint');" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnGuardarConsumo" runat="server" OnClick="btnGuardarReferencia_Click"
                                Text="Guardar" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

            <asp:Label ID="lblCodPersona" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblNumeroRadicacion" runat="server" Text="Label"
                Visible="False"></asp:Label>
            <asp:Label ID="lblNoExisteReferencias" runat="server" Text="Label"
                Visible="False"></asp:Label>
            <asp:Label ID="lblExisteConyuge" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblEsMicrocredito" runat="server" Text="Label" Visible="False"></asp:Label>

        </asp:Panel>

    </div>

</asp:Content>
