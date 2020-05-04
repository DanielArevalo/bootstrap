<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">

        <asp:UpdatePanel ID="updData" runat="server">
        <ContentTemplate>
            <table style="width: 750px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left">
                        Tipo de Calculo<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="150px" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="ddlTipoCalculo" runat="server" CssClass="textbox" Width="250px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Valor<br />
                        <uc1:decimales ID="txtValor" runat="server" />
                    </td>                    
                </tr>
                <tr>
                    <td style="text-align: left">
                        Número de Cuotas<br />
                        <asp:TextBox ID="txtNumeroCuotas" runat="server" CssClass="textbox" Width="130px" style="text-align:right"/>
                        <asp:FilteredTextBoxExtender ID="fteNumero" runat="server" Enabled="True" FilterType="Numbers, Custom"
                            TargetControlID="txtNumeroCuotas" ValidChars="-" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Periodicidad
                        <br />
                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="300px" />
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