<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">

        <asp:UpdatePanel ID="updData" runat="server">
        <ContentTemplate>
            <table style="width: 70%; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 40%">
                        ID<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="95%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 60%">
                        Descripcion<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="100%" />
                    </td>
                </tr>
                <tr>                
                    <td style="text-align: left; width: 40%">
                        Textfield<br />
                        <asp:TextBox ID="txttextfield" runat="server" CssClass="textbox" Width="95%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 60%">
                        Valuefield<br />
                        <asp:TextBox ID="txtvaluefield" runat="server" CssClass="textbox" Width="63%"></asp:TextBox>
                    </td>
                </tr>
                <tr> 
                     <td style="text-align: left; width: 100%" colspan="2">
                        Sentencia<br />
                        <asp:TextBox ID="txtsentencia" runat="server" CssClass="textbox" Width="100%" 
                             TextMode="MultiLine"></asp:TextBox>
                         Nota: no repetir los nombres de las columnas
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large; color: Red">
                        <asp:Label ID="lblMsj" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
  
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
     
</asp:Content>