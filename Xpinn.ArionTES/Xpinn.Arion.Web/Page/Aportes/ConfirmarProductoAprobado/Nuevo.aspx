<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlDocumentosAnexo.ascx" TagName="ctlDocumentosAnexo" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%" cellspacing="2" cellpadding="2">
                <tr>
                    <td style="text-align: left; width:30%;">Código<br />                        
                        <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                    <td style="text-align: left; width:30%;">Identificación<br />                        
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                    <td style="text-align: left; width:30%;">Nombre<br />                        
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                </tr>
                <tr>
                    <td style="text-align: left; width:30%;">Num. solicitud<br />                        
                        <asp:TextBox ID="txtIdSol" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                    <td style="text-align: left; width:30%;">Producto<br />                        
                        <asp:TextBox ID="txtProducto" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                    <td style="text-align: left; width:30%;">Estado<br />                        
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                </tr>
                <tr>
                    <td style="text-align: left; width:30%;">Valor<br />                        
                        <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                    <td style="text-align: left; width:30%;">Plazo<br />                        
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                    <td style="text-align: left; width:30%;">Fecha Solicitud<br />
                        <asp:TextBox ID="txtFechaSol" runat="server" CssClass="textbox" Width="88%" />
                    </td>                                    
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <uc5:ctlDocumentosAnexo runat="server" ID="DocumentosAnexos" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>