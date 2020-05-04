<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="80%">
                <tr>
                    <td colspan="4" style="text-align: left; width:300px;">Código de causa<br />
                        <asp:TextBox ID="txtCodigoCausa" runat="server" CssClass="textbox"
                            MaxLength="20"  Width="250px" Enabled="false"/>
                    </td>
                    <td colspan="4" style="text-align: left">Descripción de causa<br />
                        <asp:TextBox ID="txtDescripcionCausa" runat="server" CssClass="textbox" MaxLength="150" Width="450px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">Área
                        <br />
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="textbox" Width="260px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td colspan="7" style="text-align: left">Potencial Responsable
                        <br />
                        <asp:DropDownList ID="ddlPotencialResponsable" runat="server" CssClass="textbox" Width="260px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

