<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="80%">
                <tr>
                    <td colspan="2" style="text-align: left">Código de área<br />
                        <asp:TextBox ID="txtCodigoArea" runat="server" CssClass="textbox"
                            MaxLength="20" width="250px" Enabled="false"/>
                    </td>
                    <td colspan="5" style="text-align: left">Descripción de área<br />
                        <asp:TextBox ID="txtDescripcionArea" runat="server" CssClass="textbox" MaxLength="120" Width="400px" />
                    </td>
                    <td style="text-align: left">&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

