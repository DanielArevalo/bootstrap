<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Fabrica de Creditos :." %>

<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<br />
<br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />

</asp:Content>
