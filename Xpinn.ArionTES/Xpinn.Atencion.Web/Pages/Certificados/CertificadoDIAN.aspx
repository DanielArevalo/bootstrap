<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CertificadoDIAN.aspx.cs" Inherits="CertificadoDIAN" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-group">
        <div class="form-group">
            <div class="col-sm-12">
                <div class="col-sm-9" style="font-size:x-small">
                    Seleccione un año para generar el Certificado.
                </div>
                <div class="col-sm-3 text-right">
                    &nbsp;
                </div>
            </div>
            <div class="col-sm-12">
                <br />
            </div>
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <asp:ListBox ID="lstAniosPersona" runat="server" CssClass="list-group" AutoPostBack="true" Height="350px"
                        Width="100%" onselectedindexchanged="lstAniosPersona_SelectedIndexChanged"></asp:ListBox>
                </div>
                <div class="col-sm-9">
                <asp:Panel ID="panelImpresion" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                                    height="550px" runat="server" style="border-style: groove; float: left;"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <rsweb:ReportViewer ID="RptCertificado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    Height="550px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                                    WaitMessageFont-Size="14pt" Width="100%">
                                    <LocalReport ReportPath="Pages\Certificados\rptCertificadoDian.rdlc">
                                    </LocalReport>
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                </div>                
            </div>
            <div class="col-sm-12">
                <br />
            </div>
        </div>
    </div>
</asp:Content>

