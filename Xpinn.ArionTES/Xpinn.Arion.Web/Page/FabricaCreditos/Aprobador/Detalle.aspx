<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
    <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    Línea de crédito<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="dropdown" 
                        Width="250px" Enabled="False">
                    </asp:DropDownList>
                    &nbsp;</td>
                <td>
                    &nbsp; Usuario aprobador<br />
                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="dropdown" 
                        Width="250px" Enabled="False">
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Nivel<br />
                    <asp:DropDownList ID="ddlNivel" runat="server" CssClass="dropdown" 
                        Width="250px" Enabled="False">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">Nivel 1</asp:ListItem>
                        <asp:ListItem Value="2">Nivel 2</asp:ListItem>
                        <asp:ListItem Value="3">Nivel 3</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;</td>
                <td>
                    Aprueba<br />
                    <asp:CheckBox ID="chkAprueba" runat="server" Enabled="False" Width="250px" />
                </td>
            </tr>
            <tr>
                <td>
                    Monto mínimo<br />
                    <asp:TextBox ID="txtMinimo" runat="server" CssClass="textbox" Width="250px" 
                        Enabled="False" />
                    &nbsp;</td>
                <td>
                    Monto máximo<br />
                    <asp:TextBox ID="txtMaximo" runat="server" CssClass="textbox" Width="250px" 
                        Enabled="False" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Oficina<br />
                    <asp:TextBox ID="txtNombreOficina" runat="server" CssClass="textbox" 
                        Enabled="False" Width="250px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" 
                        ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgGuardar" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </asp:Content>

