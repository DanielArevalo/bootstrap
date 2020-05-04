<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <table width="50%" cellspacing="10" style="text-align:left">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Codigo
                        </td>
                        <td>
                            <asp:TextBox ID="txtcodigo" runat="server" Width="190px" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tipo Historico</td>
                        <td>
                            <asp:DropDownList ID="ddlHistorico" runat="server" Width="200px" CssClass="textbox">
                             </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelFecha" runat="server" Text="Fecha Inicial"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox"
                                MaxLength="10" Width="190px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                PopupButtonID="Image1" TargetControlID="txtFechaIni">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelFecha0" runat="server" Text="Fecha Final"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textbox"
                                MaxLength="10" Width="190px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                PopupButtonID="Image1" TargetControlID="txtFechaFin">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Valor
                        </td>
                        <td>
                            <asp:TextBox ID="txtvalor" runat="server" Width="190px" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                 
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
