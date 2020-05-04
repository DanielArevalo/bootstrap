<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="text-align: left">
        <br />
        <br />
        <strong>Seleccione la Persona para la legalización del giro</strong>
        <br />
    </div>
    <asp:Panel ID="pConsulta" runat="server"> 
        <uc1:ListadoPersonas id="ctlBusquedaPersonas" runat="server" />
    </asp:Panel>
    <br />
</asp:Content>
