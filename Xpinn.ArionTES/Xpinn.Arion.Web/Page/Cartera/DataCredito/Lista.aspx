<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="DataCredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlFechaCierre.ascx" TagName="fechaC" TagPrefix="fcFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvActivosFijos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="PanelDatos" runat="server">
                <div style="text-align: right">
                    <asp:ImageButton ID="btnGenerar" runat="server" ImageUrl="~/Images/btnConsultar.jpg"
                        OnClick="btnGenerar_Click" Visible="False" />
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 150px;">
                                    Fecha de Corte<br />
                                    <fcFecha:fechaC ID="fechaCierre" runat="server" style="text-align: center" />
                                </td>
                                <td style="text-align: left">
                                    <br />
                                </td>
                                <td style="text-align: left; width: 284px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 284px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;" colspan="4">
                                    <strong>Entidad a Reportar</strong>
                                    <br />
                                    <asp:RadioButtonList ID="rblEntidadReporta" runat="server" RepeatDirection="Horizontal"
                                        Width="222px" AutoPostBack="true" OnSelectedIndexChanged="rblEntidadReporta_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">DataCrédito</asp:ListItem>
                                        <asp:ListItem Value="2">CIFIN</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="4">
                                    <asp:Panel ID="panelDataCred" runat="server">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; width: 150px;">
                                                    Tipo Cuenta<br />
                                                    <asp:TextBox ID="txttipocuenta" runat="server" Width="147px" CssClass="textbox"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left">
                                                    Nombre de la Oficina de Radicación<br />
                                                    <asp:TextBox ID="txtoficina" runat="server" Width="346px" CssClass="textbox"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px;">
                                                    Código del Suscriptor<br />
                                                    <asp:TextBox ID="txtsuscriptor" runat="server" Width="145px" CssClass="textbox"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left">
                                                    Nombre de la Ciudad de Radicación<br />
                                                    <asp:TextBox ID="txtCiudad" runat="server" Width="347px" CssClass="textbox"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="panelCifin" runat="server">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; width: 150px;">
                                                    Código Paquete<br />
                                                    <asp:DropDownList ID="ddlCodPaquete" runat="server" Width="147px" CssClass="textbox" />
                                                </td>
                                                <td style="text-align: left">
                                                    Tipo Entidad<br />
                                                    <asp:DropDownList ID="ddlTipoEntidad" runat="server" Width="346px" CssClass="textbox" />
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px;">
                                                    Código Entidad<br />
                                                    <asp:TextBox ID="txtCodEntidad" runat="server" Width="145px" CssClass="textbox"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left">
                                                    Probabilidad No Pago<br />
                                                    <asp:TextBox ID="txtProbabilidad" runat="server" Width="270px" CssClass="textbox"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; width: 284px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Tipo de Entrega<br />
                                    <asp:RadioButtonList ID="rbTipoEntrega" runat="server" RepeatDirection="Horizontal"
                                        Width="222px">
                                        <asp:ListItem Value="T" Selected="True">Total</asp:ListItem>
                                        <asp:ListItem Value="F">Parcial</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left">
                                    Nombre del Archivo<br />
                                    <asp:TextBox ID="txtArchivo" runat="server" Width="346px"></asp:TextBox>
                                    <br />
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Tipo de Archivo<asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                        Width="222px">
                                        <asp:ListItem Selected="True">Texto</asp:ListItem>
                                        <asp:ListItem>CSV</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left;">
                                    <br />                                    
                                    <asp:CheckBox ID="cbEmpleados" runat="server" Text="No Reportar Créditos de Empleados" />
                                </td>                                
                                <td style="text-align: left;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblEntidadReporta" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </asp:Panel>
            <asp:Panel ID="PanelFinal" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblResultado" runat="server" Text="Archivo de Centrales de Riesgo Generado Correctamente"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
