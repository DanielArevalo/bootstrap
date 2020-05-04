<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlProveedor.ascx.cs"
    Inherits="ctlProveedor" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Panel ID="panelCtl" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfCargar" runat="server" Visible="False" />
            <div style="text-align: left; font-size: x-small">
                <asp:CheckBox ID="chkManeja_Orden" runat="server" Text="Maneja orden de servicio"
                    AutoPostBack="true" OnCheckedChanged="chkManeja_Orden_CheckedChanged" />
            </div>
            <asp:Panel ID="panelProveedor" runat="server">
                <table>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            <span style="font-weight: bold">
                            <asp:Label ID="lblTitOrden" runat="server" Text="Proveedor para La Orden de Servicio:" /></span>
                                &nbsp;
                            <asp:TextBox ID="txtOrdenCred" runat="server" CssClass="textbox" Width="30px" Visible="false"
                                Style="text-align: left" />&nbsp;
                            <asp:TextBox ID="txtOrdenAux" runat="server" CssClass="textbox" Width="30px" Visible="false"
                                Style="text-align: left" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left;vertical-align:top">  
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblTitIdenProveedor" runat="server" Text="Identificación Proveedor" />
                                        <asp:TextBox ID="txtcodProveedor" runat="server" Text="" Width="1px" BorderColor="White" BackColor="White" BorderWidth="0" Enabled="False" Font-Size="XX-Small" ForeColor="White" Height="1px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTitNomProveedor" runat="server" Text="Nombre Proveedor" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="150px"
                                            AutoPostBack="true" Style="text-align: left" OnTextChanged="txtIdentificacion_TextChanged" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                                            OnClick="btnConsultaPersonas_Click" />
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                    <td style="text-align: left;vertical-align:top"> 
                                        <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="textbox" Enabled="false"
                                            Width="455px" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>                          
                        </td>                        
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkManeja_Orden" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
