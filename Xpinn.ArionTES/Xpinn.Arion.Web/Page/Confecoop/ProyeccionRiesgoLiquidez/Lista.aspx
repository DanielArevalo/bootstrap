<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="2" cellspacing="0" width="50%">
            <tr>
                <td class="tdI" style="text-align: left">Fecha Corte<br />
                    <uc:fecha ID="txtFechaCorte" runat="server" />
                </td>
                <td class="tdD" style="text-align: left">Tipo Reporte<br />
                    <asp:DropDownList ID="ddlTipoReporte" runat="server" AutoPostBack="True" CssClass="textbox" Width="250px" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                        <asp:ListItem Text="Disponible" Value="1" />
                        <asp:ListItem Text="Ahorro Permanente" Value="2" />
                        <asp:ListItem Text="Cartera" Value="3" />
                        <asp:ListItem Text="Aporte" Value="4" />
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="text-align: left">
                    <asp:Panel runat="server" Visible="false" ID="pnlClasificacion">
                        Clasificación Cartera<br />
                        <asp:DropDownList ID="ddlClasificacionCartera" runat="server" CssClass="textbox" Width="250px">
                        </asp:DropDownList>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr style="width: 100%; text-align: left" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="panelReporte" Visible="false" runat="server">
                    <asp:MultiView ActiveViewIndex="0" ID="mvReporte" runat="server">
                        <asp:View runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                            InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                                            WaitMessageFont-Size="10pt" Width="100%" Height="700">
                                        </rsweb:ReportViewer>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: center; font-size: large;">
                                        <asp:Label ID="lblMensajeError" runat="server" Text="No se encontraron registros"
                                            Style="color: #FF3300"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
