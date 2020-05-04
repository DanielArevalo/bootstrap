<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipos de Documento :." ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>--%>
<%@ Register Src="~/General/Controles/CtlEditDocument.ascx" TagPrefix="uc1" TagName="CtlEditDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScritpManager1" runat="server" EnablePageMethods="true" />

    <br /><br /> 
    <asp:MultiView ID="mvTipoDopcumento" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="1" cellspacing="0" width="100%" >                
                <tr>
                    <td class="tdI" style="text-align: left" colspan="2">
                        Código*&nbsp;<asp:RequiredFieldValidator ID="rfvtipoliq" runat="server" ControlToValidate="txtTipoDocumento"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" style="font-size:x-small"
                            ForeColor="Red" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revTipoDocumento" runat="server" style="font-size:x-small"
                            ErrorMessage="Ingrese solo números" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgGuardar"
                            ControlToValidate="txtTipoDocumento" Display="Dynamic" 
                            ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                        <br />
                        <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="textbox" MaxLength="128" />
                    </td>
                    <td class="tdD">
                    </td>
                    <td class="tdI" style="text-align: left" colspan="2">
                        Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" style="font-size:x-small"
                            ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" style="text-transform:uppercase"
                            Width="519px" />
                        <asp:FilteredTextBoxExtender ID="fte10" runat="server" Enabled="True" TargetControlID="txtDescripcion"
                            ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                    </td>
                    <td style="text-align: left">
                        Tipo de Formato<br />
                        <asp:DropDownList ID="ddlFormato" runat="server" CssClass="textbox" Width="180px">                           
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
           
            <div>    	    		
                <uc1:CtlEditDocument runat="server" ID="CtlEditDocument" />
	        </div>	
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
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
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Documento Grabado Correctamente"></asp:Label>
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
                        </td>
                    </tr>
                </table>
            
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
