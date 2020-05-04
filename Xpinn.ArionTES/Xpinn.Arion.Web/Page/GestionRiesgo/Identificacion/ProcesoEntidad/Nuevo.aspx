<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="80%" style="font-size:small">
                <tr>
                    <td colspan="2" style="text-align: left">Código del proceso<br />
                        <asp:TextBox ID="txtCodigoProceso" runat="server" CssClass="textbox" MaxLength="20" Enabled="false" />
                    </td>
                    <td colspan="2" style="text-align: left">Descripción<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="120" Width="500px" />
                    </td>
                    <td style="text-align: left">&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

