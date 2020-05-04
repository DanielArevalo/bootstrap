<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="AseMoviGralCredito" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha0" %>
<%@ Register Src="~/General/Controles/ctlListarCreditosPorFiltro.ascx" TagName="ListarCreditos" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="<%=Page.ResolveUrl("~/Scripts/jquery-1.7.1.min.js") %>" type="text/javascript"></script>

    <uc3:ListarCreditos ID="ctlListarCreditos" runat="server" style="text-align: right;" />

</asp:Content>
