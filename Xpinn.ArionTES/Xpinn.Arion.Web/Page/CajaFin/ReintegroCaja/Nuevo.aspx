<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvprincipal" runat="server">
        <asp:View ID="View1" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 100%">
             <tr>
                <td>
                    Fecha de Reintegro<br />
                    <asp:TextBox ID="txtFechaReintegro" Enabled="false" runat="server" CssClass="textbox" 
                        MaxLength="10" Width="150" style="text-align: center"></asp:TextBox>
                </td>
                 <td style="height: 15px; text-align: left; width: 150px;">
                            <br />
                            <ucFecha:fecha ID="txtfecha" runat="server" AutoPostBack="True" visible="false" 
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                 <td class="tdI">
                     &nbsp;</td>
            </tr>
             <tr>
                <td style="background-color: #3599F7; text-align: center;" colspan="3">
                    <strong style="color: #FFFFFF">
                    Información de Reintegro</strong>
                </td>
                <td style="background-color: #3599F7; text-align: center;">
                    &nbsp;</td>
            </tr>
            <tr>
              <td class="tdI">
                    Oficina <br/>
                    <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
                </td>
                 <td>
                    Caja<br/>
                    <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
                </td>
                 <td class="tdI">
                    Cajero <br/>
                    <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
             <td class="tdI">
                    Banco<br/>
                    <asp:DropDownList ID="ddlBancos" CssClass="dropdown"  runat="server" 
                        Height="27px" Width="155px">
                    </asp:DropDownList> 
                </td>
                <td>
                     Moneda<br/>
                     <asp:DropDownList ID="ddlMonedas" CssClass="dropdown"  runat="server" 
                        Height="27px" Width="155px">
                    </asp:DropDownList> 
                </td>
                <td>
                    Valor
                    <br/>           
                    <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Width="260px" 
                    MaxLength="9" style="text-align: right">
                    </uc1:decimales>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="View3" runat="server">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="789px">

            <LocalReport ReportPath="Page\CajaFin\ReintegroCaja\ReporteTraslado.rdlc">
            </LocalReport>

        </rsweb:ReportViewer>
    </asp:View>
    <asp:View ID="vwFinal" runat="server">
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
                            Se han guardado                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            correctamente los datos ingresados
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>        
    </asp:MultiView>
</asp:Content>