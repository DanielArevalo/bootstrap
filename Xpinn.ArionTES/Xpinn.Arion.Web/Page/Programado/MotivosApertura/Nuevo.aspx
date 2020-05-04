<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvtermina" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwGenerar" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Codigo
                            <br />
                            <asp:TextBox ID="txtCodigo" CssClass="textbox" runat="server" Width="269px"></asp:TextBox>
                            <br />
                        </td>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Descripcion
                            <br />
                            <asp:TextBox ID="txtDescripcion" CssClass="textbox" runat="server" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    </table>
        </asp:View>
        <asp:View ID="View1" runat="server">
        <table>
            <tr>
                <td style="text-align: center; font-size: large; color: Red">
                    La linea fue
                    <asp:Label ID="lblmsj" runat="server"></asp:Label>
                    &nbsp;correctamente.
                </td>
                <td style="text-align: center; font-size: large;">
                    &nbsp;
                </td>
            </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
