<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%" cellspacing="2" cellpadding="2">
                <tr>
                    <td style="text-align: left; width: 30%">
                        Código<br />
                        &nbsp;<asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 30%">
                        Cuenta Bancaria<br />
                        &nbsp;<asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" 
                            Width="80%" AutoPostBack="True" 
                            onselectedindexchanged="ddlCuenta_SelectedIndexChanged" 
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 40%">
                        Banco<br />
                        &nbsp;<asp:TextBox ID="txtBanco" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Prefijo<br />
                        &nbsp;<asp:TextBox ID="txtPrefijo" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        Cheque Inicial<br />
                        &nbsp;<asp:TextBox ID="txtChqInicial" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                         <asp:filteredtextboxextender id="fte5" runat="server" targetcontrolid="txtChqInicial"
                                                            filtertype="Custom, Numbers" validchars="+-=/*()." />
                    </td>
                    <td style="text-align: left">
                        Cheque Final<br />
                        &nbsp;<asp:TextBox ID="txtChqFinal" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                        <asp:filteredtextboxextender id="Filteredtextboxextender1" runat="server" targetcontrolid="txtChqFinal"
                                                            filtertype="Custom, Numbers" validchars="+-=/*()." />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Fecha Entrega<br />
                        &nbsp;
                        <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" MaxLength="1" Width="135px" />
                    </td>
                    <td style="text-align: left">
                        Num. Sig. Cheque<br />
                        &nbsp;<asp:TextBox ID="txtSigCheque" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                    </td>
                    <td style="text-align:left">
                    Estado<br />
                        <asp:RadioButtonList ID="rblEstado" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Activa</asp:ListItem>
                            <asp:ListItem Value="2">Inactiva</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente los datos ingresados</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" 
                                onclick="btnFinal_Click" />
                              &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
       <asp:View ID="vwReporte" runat="server">
            <br /><br />&nbsp;
         
            <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="500px"
                AsyncRendering="false" >
            <localreport reportpath="Page\Tesoreria\Chequeras\ReportChequera.rdlc">
                <DataSources>

                                    <rsweb:ReportDataSource />
                                </DataSources>
            </localreport>
       
            </rsweb:ReportViewer>    
       </asp:View>
    </asp:MultiView>
    
    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>