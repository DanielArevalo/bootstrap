<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvDetalle" runat="server">
                <asp:View ID="vwDetalleParametro" runat="server">
                    <table cellpadding="5" cellspacing="0" >
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Mínimo *<br />
                                <asp:Label ID="lblMinimo" runat="server" CssClass="textbox" Width="150px" style="text-align:right"></asp:Label>
                            </td>
                            <td style="text-align:left">
                                Máximo *<br />
                                <asp:Label ID="lblMaximo" runat="server" CssClass="textbox" Width="150px" style="text-align:right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Aprueba *<br />
                                <asp:RadioButtonList ID="chklAprueba" runat="server" RepeatDirection="Horizontal" Enabled="False" Width="300px">
                                    <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align:left">
                                Muestra *<br />
                                <asp:RadioButtonList ID="chklMuestra" runat="server" RepeatDirection="Horizontal" Enabled="False" Width="300px">
                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:left">
                                <asp:Label ID="Label1" runat="server" Height="25px" Text="Mensaje *"></asp:Label>
                                <br />
                                <asp:Label ID="lblMensaje" runat="server" CssClass="textbox" Width="450px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vwDetalleCentral" runat="server">
                    <table cellpadding="5" cellspacing="0">
                        <tr>
                            <td style="text-align:left">
                                Central *&nbsp;<asp:RadioButtonList ID="rblCentral" runat="server" RepeatDirection="Horizontal" Width="300px">
                                    <asp:ListItem Selected="True" Value="Cifin">Cifin</asp:ListItem>
                                    <asp:ListItem Value="Datacredito">Datacredito</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                            </td>
                            <td style="text-align:left">
                                Valor * <br />
                                <asp:Label ID="lblValor" runat="server" CssClass="textbox" Width="150px" style="text-align:right"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Cobra IVA<br />
                                <asp:RadioButtonList ID="rblCobra" runat="server" RepeatDirection="Horizontal" Enabled="False" Width="300px">
                                    <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align:left">
                                Porcentaje IVA<br />
                                <asp:Label ID="lblPorcentaje" runat="server" CssClass="textbox" Width="150px" style="text-align:right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Valor IVA<br />
                                <asp:Label ID="lblValoriva" runat="server" CssClass="textbox" Width="150px" style="text-align:right"></asp:Label>
                            </td>
                            <td>
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
