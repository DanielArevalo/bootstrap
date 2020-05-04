<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="80%">
                <tr>
                    <td colspan="4" style="text-align: left; width:300px;">Código del cargo<br />
                        <asp:TextBox ID="txtCodCargo" runat="server" CssClass="textbox"
                            MaxLength="20"  Width="250px" Enabled="false"/>
                    </td>
                    <td colspan="4" style="text-align: left">Descripción del cargo<br />
                        <asp:TextBox ID="txtDescripcionCargo" runat="server" CssClass="textbox" MaxLength="120" Width="450px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

