<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="reporte.aspx.cs" Inherits="Reporte" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <rsweb:ReportViewer ID="rvVisor" runat="server" Width="100%">
    </rsweb:ReportViewer>
</asp:Content>

