<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">


            <div class="col-md-12" id="datos">
                <div class="col-lg-2 col-md-2 col-sm-12">
                </div>
                <div class="col-lg-8 col-md-8 col-sm-12">
                    <table class="tableNormal" width="100%">
                        <tr>                            
                            <td style="text-align: center;">
                                Fecha
                            </td>
                            <td style="text-align: center;">
                                <asp:DropDownList runat="server" ID="ddlFecha" DataFormatString="{0:d}">                                  
                                </asp:DropDownList>                                
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-info" style="height: 30px;" Text="consultar" OnClick="btnConsultar_Click" />
                            </td>                           
                        </tr>
                                <asp:CheckBox ID="NomGene" runat="server" Checked="false" Text="Nominas generadas sin aplicar"
                                AutoPostBack="True" OnCheckedChanged="cbNoGeneradas_CheckedChanged" Visible="false" />
                    </table>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-12">
                </div>
            </div>   

            <table style="width: 95%" id="tabladesc" runat="server">
                <tr>
                    <td style="text-align: left">Empresa Recaudadora<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="280px"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha de Aplicacion" /><br />
                        <ucFecha:fecha ID="ucFechaAplicacion" runat="server" Enabled="False" Requerido="False" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumeroLista" runat="server" Visible="True" Text="Núm. Aplicación" /><br />
                        <asp:TextBox ID="txtNumeroLista" runat="server" Enabled="false" Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumeroNovedad" runat="server" Visible="True" Text="Número de Novedad" /><br />
                        <asp:TextBox ID="txtNumeroNovedad" runat="server" Enabled="false" Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPeriodo" runat="server" Visible="True" Text="Período" /><br />
                        <ucFecha:fecha ID="txtPeriodo" runat="server" Enabled="False" Requerido="False" Width="70px" />
                    </td>
                    <td>
                        <br />
                        <asp:CheckBox ID="cbDetallado" runat="server" Checked="false" Text="Detallado"
                            AutoPostBack="True" OnCheckedChanged="cbDetallado_CheckedChanged" Visible="true" />
                    </td>
                    <td>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>

            <div class="col-md-12">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="3" ShowHeaderWhenEmpty="True" CssClass="table"
                                AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;"
                                OnRowEditing="gvLista_RowEditing">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderText="Exportar" EditImageUrl="~/Imagenes/gr_edit.jpg" ShowEditButton="True"/>
                                    <asp:BoundField DataField="numero_recaudo" HeaderText="Num Novedad" visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit" visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombres" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_producto" HeaderText="Cantidad de Productos" />
                                    <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Periodo" DataFormatString="{0:d}" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos" runat="server" CssClass="btn btn-primary" Height="28px" OnClick="btnDatos_Click"
                Text="Regresar" />
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvConsultaRecaudo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Pages/Certificados/ExtractoRecaudoMasivo/rptDescuentos.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Pages/Certificados/ExtractoRecaudoMasivo/rptDescuentos.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvConsolidado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Pages/Certificados/ExtractoRecaudoMasivo/rptDescuentos.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>
