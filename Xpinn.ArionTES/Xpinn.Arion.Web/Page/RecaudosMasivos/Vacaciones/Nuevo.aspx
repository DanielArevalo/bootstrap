<%@ Page Title=".: Recaudos Masivos :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <br />
    <br />
    <asp:MultiView ID="mvFinal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <table style="width: 80%;">
                <tr>
                    <td style="width: 250px; text-align: left">Tipo Identificación
                    </td>
                    <td style="text-align: left; width: 250px">
                        <asp:DropDownList ID="ddlTipoIdentificacion" Enabled="false" runat="server" CssClass="textbox" Height="27px"
                            Width="182px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 200px; text-align: left">Identificación
                    </td>
                    <td style="width: 260px; text-align: left">
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px"
                            MaxLength="12" ReadOnly="true"></asp:TextBox>
                        <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                            OnClick="btnConsultaPersonas_Click" />
                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodCliente" runat="server" CssClass="textbox"
                            Enabled="False" MaxLength="12" Visible="False" Width="10px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; text-align: left">Nombres y Apellidos
                    </td>
                    <td colspan="4" style="text-align: left">
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false"
                            Width="542px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; text-align: left">Número de Cuotas
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNumeroCuotas" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="110px"
                            MaxLength="12"></asp:TextBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
            </table>

            <table style="width: 100%;">
                <tr>
                    <td colspan="4" style="text-align: left; font-size: x-small">
                        <b>Tener en cuenta que el "Valor a Descontar" tiene mayor prioridad</b>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 30%" colspan="2">Empresa Recaudadora<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="94%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 15%">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha Periodo Novedad" /><br />
                        <ucFecha:fecha ID="txtFecha" runat="server" Enabled="true" Width="120px" />
                    </td>
                    <td style="text-align: left; width: 25%">Tipo Calculo
                        <br />
                        <asp:DropDownList ID="ddlTipoCalculo" runat="server" CssClass="dropdown" Width="94%">
                            <asp:ListItem Text="Calculo en Generacion" Value="0" />
                            <asp:ListItem Text="Calculo en Carga" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 15%">Fecha Inicial<br />
                        <ucFecha:fecha ID="txtFecIni" runat="server" Enabled="true" Width="120px" />
                    </td>
                    <td style="text-align: left; width: 15%">Fecha Final<br />
                        <ucFecha:fecha ID="txtFecFin" runat="server" Enabled="true" Width="120px" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server">
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
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
