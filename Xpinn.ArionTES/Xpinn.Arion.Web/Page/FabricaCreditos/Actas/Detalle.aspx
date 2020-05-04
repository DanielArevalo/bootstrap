<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="logo" style="width: 148px; text-align: left">Número Acta </td>
                            <td style="width: 148px; text-align: left" class="logo">Fecha Acta </td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 148px; text-align: left">
                                <asp:TextBox ID="txtacta" runat="server" AutoPostBack="True" CssClass="textbox"
                                    Enabled="False" ValidationGroup="vgGuardar" Width="148px"></asp:TextBox>
                            </td>
                            <td style="width: 138px; text-align: left">
                                <asp:TextBox ID="txtFechaacta" runat="server" AutoPostBack="True" CssClass="textbox"
                                    MaxLength="1" ValidationGroup="vgGuardar" Width="148px" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFechaacta_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtFechaacta"></asp:CalendarExtender>
                                <br />
                            </td>
                            <td style="text-align: left">&nbsp;
                                <strong>
                                    <asp:TextBox ID="txtusuario" runat="server" AutoPostBack="True"
                                        CssClass="textbox" Enabled="False" Height="16px" MaxLength="1"
                                        ValidationGroup="vgGuardar"
                                        Width="42px" Visible="False"></asp:TextBox>

                                    <asp:TextBox ID="txtperfil" runat="server" AutoPostBack="True"
                                        CssClass="textbox" Enabled="False" Height="16px" MaxLength="1"
                                        ValidationGroup="vgGuardar"
                                        Width="42px" Visible="False"></asp:TextBox>

                                    <asp:Button ID="btnInforme" runat="server" CssClass="btn8"
                                        OnClick="btnInforme_Click" OnClientClick="btnInforme_Click"
                                        Style="height: 22px" Text="Ver Acta" />
                                </strong>
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal"
                    AutoGenerateColumns="False" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                    DataKeyNames="numero_radicacion">
                    <Columns>
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodicidad" HeaderText="Amortización">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreAsesor" HeaderText="Asesor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codeudor" HeaderText="Identificación Codeudor" />
                        <asp:BoundField DataField="nombrecodeudor" HeaderText="Nombre Codeudor" />
                        <asp:BoundField DataField="nomestado" HeaderText="Estado" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:MultiView ID="mvReporte" runat="server">
                    <asp:View ID="vReporte" runat="server">
                        <rsweb:ReportViewer ID="ReportViewActa" runat="server" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" Width="100%"
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="361px">
                            <LocalReport ReportPath="Page\FabricaCreditos\Actas\ReporteActas.rdlc"></LocalReport>
                        </rsweb:ReportViewer>
                        <br />
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>
