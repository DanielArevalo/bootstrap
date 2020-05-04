<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td colspan="4" style="text-align: left; width:250px;">Código del factor de riesgo<br />
                        <asp:TextBox ID="txtCodigoFactorRiesgo" runat="server" CssClass="textbox"
                            MaxLength="20"  Width="250px" Enabled="false"/>
                        <asp:HiddenField ID="hdCodigo" runat="server" />
                    </td>
                    <td colspan="4" style="text-align: left">Descripción del factor de riesgo<br />
                        <asp:TextBox ID="txtDescripcionFactor" runat="server" CssClass="textbox"  Width="550px" />
                    </td>
                </tr>
<%--            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="95%">--%>
                <tr>
                    <td colspan="4" style="text-align: left">Procedimiento
                        <br />
                        <asp:DropDownList ID="ddlProcedimiento" runat="server" CssClass="textbox" Width="260px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td colspan="3" style="text-align: left">Factor de riesgo
                        <br />
                        <asp:DropDownList ID="ddlFactorRiesgo" runat="server" CssClass="textbox" Width="260px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td colspan="3" style="text-align: left">Sistema de riesgo
                        <br />
                        <asp:DropDownList ID="ddlSistemaRiesgo" runat="server" CssClass="textbox" Width="260px" OnSelectedIndexChanged="ddlSistemaRiesgo_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

