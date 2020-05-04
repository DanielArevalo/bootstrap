<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Crédito Educativo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; margin-bottom: 0px;">
        <tr>
            <td>
                <br />
            </td>
        </tr>        
        <tr>
            <td style="text-align: left">
                <strong style="text-align: left">Seleccionar la Persona a Modificar</strong>
                <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="text-align: left;">
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
