<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - BackUps :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" width="90%">
        <tr>
            <td>
                <table id="tbCriterios" border="0" width="100%">
                    <tr>
                        <td>
                            <label>Script a Ejecutar: </label>
                        </td>
                        <td style="width: 80%">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtScriptParaEjecutar" Width="90%" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>Recordad que el script para que se vea en el resultado debe tener " | Out-String" al final </strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button Text="Ejecutar Script" runat="server" ID="btnEjecutarScript" CssClass="btn8" Font-Bold="true" OnClick="btnEjecutarScript_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <h2>Resultado Script</h2>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="ResultBox" TextMode="MultiLine" Width="90%" Height="300px" />
            </td>
        </tr>
    </table>
</asp:Content>
