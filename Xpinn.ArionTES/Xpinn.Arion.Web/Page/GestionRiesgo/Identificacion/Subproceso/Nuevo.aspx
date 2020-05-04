<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-bottom: 0px; font-size:small">
                <tr>
                    <td style="text-align: left; width:200px;">Código de subproceso<br />
                        <asp:TextBox ID="txtCodigoSubproceso" runat="server" CssClass="textbox"
                            MaxLength="20"  Width="180px" Enabled="false"/>
                    </td>
                    <td style="text-align: left;  width:280px;" >Descripción del subproceso<br />
                        <asp:TextBox ID="txtDescripcionSubproceso" runat="server" CssClass="textbox" MaxLength="120" Width="250px" />
                    </td>
                    <td style="text-align: left">Proceso<br />
                        <asp:DropDownList ID="ddlProceso" runat="server" CssClass="textbox" Width="260px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

