<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFamiliar_GrupoFamiliar_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <p>
        <iframe id="ifrLista" scrolling="no" src="Lista.aspx" width="100%" 
            runat="server" style="height: 462px" >
        
        
        </iframe></p>
    <p>
        <iframe id="ifrNuevo" scrolling="no" src="Lista.aspx" width="100%" name="I1" 
            style="height: 419px">
        
        
        </iframe></p>
    <p>
        &nbsp;</p>
</asp:Content>

