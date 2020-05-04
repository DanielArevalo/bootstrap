<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Soporte :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc2" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagPrefix="rsweb" %>
<script runat="server">


</script>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:MultiView ID="mvCajaMenor" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="1" cellspacing="0" width="100%" >
                <tr>
                    <td style="text-align: left; width: 160px">Número&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="135px" Enabled="false" />
                        <asp:RequiredFieldValidator ID="rfvcodigo" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                    </td>
                    <td class="tdI" style="text-align:left;" colspan="5">Fecha&nbsp;*&nbsp;<br />
                        <uc1:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="True" Requerido="True" />
                    </td>
                    <td class="tdI" style="text-align:left">Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False" Width="150px" />
                    <td class="tdI" style="text-align:left">Area&nbsp;*&nbsp;<br />
                        <asp:DropDownList ID="ddlArea" runat="server" AppendDataBoundItems="True" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                            Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align:left" colspan="6">
                        <strong>Datos de la Persona</strong>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: left; width: 160px">Identificación<br />
                                <asp:TextBox ID="txtCod_Persona" runat="server" Visible="false" />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="135px"
                                    Style="text-align: left" AutoPostBack="True" OnTextChanged="txtIdentificacion_TextChanged" />
                            </td>
                            <td style="text-align: left">Tipo Identificación<br />
                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="156px" Style="text-align: left">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">Nombres y Apellidos<br />
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="474px" Style="text-align: left"
                                    Enabled="False"></asp:TextBox>
                                <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px" Width="26px"
                                    OnClick="btnConsultaPersonas_Click" />
                                <uc2:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            </td>
                        </tr>
                    </table>
           
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="text-align:left" colspan="6">
                        <strong>Información del Recibo de Caja</strong>
                    </td>
                    <td class="tdD"></td>
                </tr>
                <tr>
                    <td style="text-align: left; Width:135px">Vale provisional&nbsp;*&nbsp;<br />

                        <asp:DropDownList ID="ddlVale" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox" Style="font-size:xx-small; text-align: left" Width="135px" OnSelectedIndexChanged="ddlVale_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="Legalizado"></asp:ListItem>
                            <asp:ListItem Value="No Legalizado"></asp:ListItem>
                            <asp:ListItem Value="No Aplica"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvValeP" runat="server" ControlToValidate="ddlVale" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                    </td>
                    <td style="text-align: left; Width:300px">Concepto&nbsp;<br />
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; Width:200px">Valor&nbsp;*&nbsp;<br />
                        <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" MaxLength="128" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;"colspan="6">Detalle&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Style="overflow: hidden; resize: none"
                            MaxLength="128" Width="490px" Height="50px" TextMode="MultiLine" />
                    </td>
                    <td class="tdD">

                    </td>
                </tr>
                <tr>

                    <td class="tdD">

                    </td>
                </tr>
<%--            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">--%>
                <tr>
                    <td class="tdD" style="text-align:left; Width:200px">Estado<br />
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="False"
                            Width="130px" />
                    </td>
                    <td class="tdD" colspan="2" style="text-align:left; width: 350px">Elaborado por<br />
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" Enabled="False" Width="300px" />
                    </td>
                    <td class="tdD" style="text-align:left; Width:200px">
                        <asp:Label ID="lblNumComp" runat="server" Text="Num.Comp" Visible="true"></asp:Label><br />
                        <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Enabled="False" />
                    </td>
                    <td class="tdD" style="text-align:left; Width:200px">
                        <asp:Label ID="lblTipoComp" runat="server" Text="Tipo Comp." Visible="true"></asp:Label><br />
                        <asp:TextBox ID="txtTipoComp" runat="server" CssClass="textbox" Enabled="False" />
                    </td>

                </tr>
                <tr>
                    <td class="tdD" style="text-align:left;">&nbsp;</td>
                </tr>
            </table>
                         </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server"
                                Text="Datos Grabados Correctamente" style="color: #FF3300"></asp:Label>
                            <br />
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" Text="Imprimir" style="padding: 5px 15px"
                                OnClick="btnInforme_Click"/>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br/>
                            <br/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwReciboImpr" runat="server">
            <br/>
            <br/>
            <br/>
            <asp:Panel id="PanelReciboImpr" runat="server">
                <asp:Button ID="btnRegresarComp" runat="server" CssClass="btn8" style="padding: 5px 15px"
                    onclick="btnRegresarComp_Click" Text="Regresar al Recibo" />
                <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" Text="Imprimir" style="padding: 5px 15px"
                    onclick="btnImprimir_Click"/>
                <br /><br />
                <rsweb:ReportViewer ID="RpviewRecibo" runat="server" Font-Names="Verdana"
                    Font-Size="8pt" Height="450px" InteractiveDeviceInfos="(Colección)"
                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                    <localreport reportpath="Page\Tesoreria\SoporteCajaMenor\ReporteReciboCaja.rdlc"></localreport>
                </rsweb:ReportViewer>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc1:mensajegrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
