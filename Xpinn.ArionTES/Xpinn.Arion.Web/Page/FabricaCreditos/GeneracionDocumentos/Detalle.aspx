<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 152px">
                                <strong>Número Radicación</strong></td>
                            <td class="logo" style="width: 149px">
                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong></td>
                        </tr>
                        <tr>
                            <td style="width: 41px; text-align:left">
                                Identificación</td>
                            <td style="text-align:left">
                                Tipo Identificación</td>
                            <td style="text-align:left">
                                Nombre</td>
                        </tr>
                        <tr>
                            <td style="width: 41px; text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" 
                                    Width="504px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel3" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td colspan="5" style="text-align:left">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align:left">
                                <strong>DATOS DEL CRÉDITO</strong></td>
                        </tr>
                        <tr>
                            <td style="width: 132px; text-align:left">
                                Linea de Crédito&nbsp;&nbsp;</td>
                            <td class="logo" style="width: 141px; text-align:left">
                                Monto&nbsp;&nbsp;</td>
                            <td style="width: 136px; text-align:left">
                                Plazo</td>
                            <td style="text-align:left; width: 163px;">
                                Periodicidad</td>
                            <td style="text-align:left">
                                Valor de la Cuota</td>
                        </tr>
                        <tr>
                            <td style="width: 132px; text-align:left">
                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                                    Enabled="false" Width="237px" />
                            </td>
                            <td class="logo" style="width: 141px; text-align:left">
                                <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Enabled="false" style="text-align:right" />
                                <asp:MaskedEditExtender ID="MEMonto" runat="server" TargetControlID="txtMonto" Mask="9,999,999.99" MaskType="Number" />
                            </td>
                            <td style="width: 136px; text-align:left">
                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left; width: 163px;">
                                <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtValor_cuota" runat="server" CssClass="textbox" Enabled="false" style="text-align:right"/>
                                <asp:MaskedEditExtender ID="MECuota" runat="server" TargetControlID="txtValor_cuota" Mask="9,999,999.99" MaskType="Number" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Forma de Pago</td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td>
                            
                                <asp:Button ID="talonario" runat="server" CssClass="btn8" 
                                    Text="Talonario" Width="126px" onclick="talonario_Click" />
                            
                            </td>
                        </tr>
                    </table>


                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" InteractiveDeviceInfos="(Colección)"  
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1108px" 
                        BorderStyle="None" DocumentMapWidth="">
                        <LocalReport ReportPath="Page\FabricaCreditos\GeneracionDocumentos\Report2.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>



                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>