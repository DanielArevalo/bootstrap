<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Auditoria :." %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <br />
    <br />
    <br />
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 25%;">
                        <label>Tablas</label>
                    </td>
                    <td style="width: 25%;">
                        <asp:DropDownList ID="ddlTablas" runat="server" Width="100%" CssClass="dropdown" />
                    </td>
                    <td style="width: 25%;">
                        <label>Operación</label>
                    </td>
                    <td style="width: 25%;">
                        <asp:DropDownList ID="ddlOperacion" runat="server" Width="100%" CssClass="dropdown">
                            <asp:ListItem Text="INSERT=CREACION DE DATOS" Value="INSERT" />
                            <asp:ListItem Text="UPDATE=ACTUALIZACION DE DATOS" Value="UPDATE" />
                            <asp:ListItem Text="DELETE=BORRAR DATOS" Value="DELETE" />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Operacion Realizada Correctamente"
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

</asp:Content>