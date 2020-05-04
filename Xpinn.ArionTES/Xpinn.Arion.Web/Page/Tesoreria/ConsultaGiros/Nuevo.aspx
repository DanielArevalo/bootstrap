<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register src="../../../General/Controles/ctlGiro.ascx" tagname="ctlgiro" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panelEncabezado" runat="server">
                        <table style="width: 730px; text-align: center" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="text-align: left; width: 150px">
                                    <asp:Label ID="lblCodigo" runat="server" Text="Código Giro" />
                                    <br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Fecha<br />
                                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    <asp:Label ID="lblFechaGiro" runat="server" Text="Fecha Giro" />
                                    <br />
                                    <ucFecha:fecha ID="txtFechaGiro" runat="server" CssClass="textbox" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 280px">
                                    Tipo Acto Giro<br />
                                    <asp:DropDownList ID="ddlTipoGiro" runat="server" CssClass="textbox" Width="90%"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">
                                    Código Persona<br />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Identificación
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                                        Enabled="false" Width="110px" OnTextChanged="txtIdPersona_TextChanged" />
                                    <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                        Enabled="false" OnClick="btnConsultaPersonas_Click" Text="..." />
                                </td>
                                <td style="text-align: left;" colspan="2">
                                    Nombre<br />
                                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" Enabled="false"
                                        Width="90%" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                        Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">
                                    <asp:Label ID="lblCodOperacion" runat="server" Text="Cod. Operación" />
                                    <br />
                                    <asp:TextBox ID="txtCodOperacion" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    <asp:Label ID="Label1" runat="server" Text="Num. Radicación" />
                                    <br />
                                    <asp:TextBox ID="txtNumRadicacion" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Num Comprobante<br />
                                    <asp:TextBox ID="txtNumComprobante" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left">
                                    Tipo Comprobante<br />
                                    <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" Width="90%"
                                        AutoPostBack="True" />
                                </td>
                            </tr>                            
                        </table>
                    </asp:Panel>
                    <table style="width: 730px; text-align: center" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: left;width:300px;" colspan="2" >
                                Valor<br />
                                <uc1:decimales ID="txtValor" runat="server"/>
                            </td>
                            <td style="text-align: left;width:150px">
                                <asp:CheckBox ID="chkCobraComision" runat="server" TextAlign="Left" Text="Cobra Comisión" Enabled="false"/>
                            </td>
                            <td style="text-align: left;width:280px">
                                Estado<br />
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="90%" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelFormaPago" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    Forma de Pago<br />
                                    <asp:DropDownList ID="ddlForma_Desem" runat="server" CssClass="textbox" Width="191px"
                                        OnSelectedIndexChanged="ddlForma_Desem_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="PanelCheque" runat="server">
                                        <table width="700px">
                                            <tr>
                                                <td style="text-align: left; width: 250px">
                                                    Banco de donde se Gira<br />
                                                    <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox" Width="90%"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlEntidad_giro_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 250px">
                                                    Cuenta de donde se Gira<br />
                                                    <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox" Width="220px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelTransfe" runat="server">
                                        <table width="700px">
                                            <tr>
                                                <td style="text-align: left; width: 250px">
                                                    Entidad Bancaria<br />
                                                    <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Width="90%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 250px">
                                                    Num. Cuenta<br />
                                                    <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="220px" />
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    Tipo de Cuenta<br />
                                                    <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox" Width="90%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: left; ">
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <uc3:ctlgiro ID="ctlGiro" runat="server" />
                    <asp:Panel ID="panelPiePagina" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="4">
                                    <table style="width: 90%">
                                        <tr>
                                            <td style="width: 32%; text-align: left">
                                                <asp:Label ID="lblUsuarioGenera" runat="server" Text="Usuario Genera" /><br />
                                                <asp:DropDownList ID="ddlUsuarioGenera" runat="server" CssClass="textbox" Width="95%" />
                                            </td>
                                            <td style="width: 33%; text-align: left">
                                                <asp:Label ID="lblbUsuarioAproba" runat="server" Text="Usuario Aprueba" />
                                                <br />
                                                <asp:DropDownList ID="ddlUsuarioAproba" runat="server" CssClass="textbox" Width="95%" />
                                            </td>
                                            <td style="width: 30%; text-align: left">
                                                <asp:Label ID="lblUsuarioAplica" runat="server" Text="Usuario Aplica" />
                                                <br />
                                                <asp:DropDownList ID="ddlUsuarioAplica" runat="server" CssClass="textbox" Width="95%" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
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
                        <td style="text-align: center; font-size: large;">
                            El giro&nbsp;<asp:Label ID="lblMsj" runat="server" />&nbsp;fue&nbsp;<asp:Label ID="lblOperacion" runat="server" />&nbsp;correctamente.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
