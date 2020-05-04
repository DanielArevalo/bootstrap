<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFinanciera_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <p>
    <iframe frameborder="0" height="300px" src="EstacionalidadSemanal/Lista.aspx" 
            width="100%" scrolling="no">
    
    </iframe>
        <br />
    </p>
    <p>
    <iframe frameborder="0" height="350px" src="EstacionalidadMensual/Lista.aspx" 
            width="100%" scrolling="no">
    
    </iframe>
    </p>
</asp:Content>
