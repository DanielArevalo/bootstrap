<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 75%">
                <tr>
                    <td style="text-align: left; width: 90%; padding: 10px" colspan="4">
                        <strong>Datos de Tipo de Nomina</strong>
                    </td>
                    <td></td>
                </tr>
                <tr style="text-align: left">
                    <td style="text-align: Left; width: 15%; padding: 10px">                        
                        Codigo
                    </td>
                    <td style="text-align: Left; width: 35%; padding: 10px">
                         <asp:TextBox ID="txtCodigoNomina" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: Left; width: 15%; padding: 10px">
                        Tipo de Contrato
                    </td>
                    <td style="text-align: Left; width: 35%; padding: 10px">
                        <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="textbox" Width="95%" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px">
                        Descripción
                    </td>
                    <td style="text-align: Left; padding: 10px" colspan="3">
                        <asp:TextBox ID="txtNombreNomina" runat="server" CssClass="textbox" Width="95%" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px" colspan="4">
                        <strong>Periodicidad</strong>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px" colspan="2">
                        <asp:CheckBoxList ID="checkBoxTipoNomina" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3">
                            <asp:ListItem Text="Semanal"            Value="3" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Bisemanal"          Value="5" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Decadal"            Value="4" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Mensual"            Value="1" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Quincenal"          Value="2" onclick="ExclusiveCheckBoxList(this);" />
                        </asp:CheckBoxList>
                    </td>
                    <td style="text-align: Left; padding: 10px">
                        Fecha Ult.Liquidación
                    </td>
                    <td style="text-align: Left; padding: 10px">
                        <uc:fecha ID="txtFechaCorte" runat="server" CssClass="textbox" Width="85px" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px" colspan="4">
                        <strong>Otros Datos</strong>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px">
                        Oficina
                    </td>
                    <td style="text-align: Left; padding: 10px">
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" AutoPostBack="true"
                            Width="95%" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: Left; padding: 10px">
                        Ciudad de la Oficina
                    </td>
                    <td style="text-align: Left; padding: 10px">
                        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Width="95%" Enabled="false">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left; padding: 10px">
                        Direccion de la Oficina
                    </td>
                    <td style="text-align: left; padding: 10px" colspan="3">
                        <asp:TextBox runat="server" ID="txtDireccionOficina" Width="100%" Enabled="false"/>
                    </td>
                    <td></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </asp:View>
        <asp:View ID="vFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
