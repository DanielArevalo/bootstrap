<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cdat.aspx.cs" 
    Inherits="Cdat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
     namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .fontSizeXSmall {
            font-size: x-small;
        }

        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="form-group">

        <asp:FormView ID="frmVista" runat="server" Width="100%" Visible="false">
                <ItemTemplate>
                    <table class="col-sm-12 tableNormal">
                        <tr>
                            <td style="display: flex; justify-content: space-between">
                                <h4 class="text-primary text-left" style="padding-left:25px">Información del Producto</h4>
                                <a class="btn btn-primary" href="../EstadoCuenta/Detalle.aspx" style="height: fit-content;">Volver</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>                                             
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Línea
                                    </div>
                                    <div class="col-sm-10 text-left">
                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("nomlinea") %>' />
                                    </div>                                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Número
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("numero_cdat") %>' />
                                    </div>                                    
                                    <div class="col-sm-2 text-left text-primary">
                                        Estado
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nom_estado") %>' />
                                    </div>
                                    <div class="col-sm-4 text-left text-primary">
                                        &nbsp;
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Valor
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("valor" , "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                       Plazo
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("plazo")+" días" %>' />
                                    </div>  
                                    <div class="col-sm-2 text-left text-primary">
                                        Tasa interés
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("tasa_interes", "{0:N}%") %>' />
                                    </div>                                                                        
                                </div>
                            </td>
                        </tr>  
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Fecha apertura
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("fecha_apertura" , "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Fecha inicio
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("fecha_inicio", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                       Vencimiento
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("fecha_vencimiento", "{0:d}") %>' />
                                    </div>                                    
                                </div>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Moneda
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("nommoneda") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                       Retención
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("retencion")%>' />
                                    </div>  
                                    <div class="col-sm-2 text-left text-primary">
                                        Periodicidad
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("nomperiodicidad") %>' />
                                    </div>                                                                        
                                </div>
                            </td>
                        </tr>  
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left text-primary">
                                        Saldo total
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("valor_parcial", "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Rendimientos
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("valor_acumulado", "{0:c0}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left text-primary">
                                        Saldo + rendimientos
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("valor_total_acumu" , "{0:c0}") %>' />
                                    </div>  
                                </div>
                            </td>
                        </tr>    
                        <tr>
                            <td>
                                <hr style="border-color: #2780e3;" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>

        <asp:Panel runat="server" ID="pnlFiltros" Visible="false">
        <table style="width: 100%">
            <tr>
                <td class="text-left text-primary fontSizeXSmall text-danger">
                    <div class="col-sm-12" style="padding-left:20px">
                        <strong>Seleccione un rango diferente a consultar</strong>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td>
                    <div class="col-md-12">
                        <div class="col-md-1 text-left text-bold fontSizeXSmall">
                            Fecha Inicial:
                        </div>
                        <div class="col-md-2">
                            <ucFecha:fecha ID="txtFecIni" runat="server" />
                        </div>
                        <div class="col-md-1 text-left text-bold fontSizeXSmall">
                            Fecha Final:
                        </div>
                        <div class="col-md-2">
                            <ucFecha:fecha ID="txtFecFin" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultar" OnClick="btnConsultar_Click1" CssClass="btn btn-primary" Text="Consultar" />
                        </div>
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="pGrilla" runat="server">
            <hr />
            <div style="overflow: scroll; max-height: 550px; max-width: 100%;">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                                                AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="cod_ope"
                                                CssClass="table table-striped" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small">
                                                <Columns>
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_ope" HeaderText="Nro Operación">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_ope" HeaderText="Descripción">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                <br />                
            </div>

            <table style="width: 100%">
                <tr>
                    <td class="text-center">
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>       

        <asp:Panel ID="panelReport" runat="server" Visible="false">
            <rsweb:reportviewer id="Rpview1" runat="server" width="100%"
                    font-names="Verdana" font-size="8pt" interactivedeviceinfos="(Colección)"
                    waitmessagefont-names="Verdana" waitmessagefont-size="14pt" height="450px">
                    <LocalReport ReportPath="Pages\Asociado\EstadoCuenta\ReportMovimiento.rdlc">
                    </LocalReport>
                </rsweb:reportviewer>
        </asp:Panel>
    </div>
</asp:Content>

<%--<asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                                <Columns>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_ope" HeaderText="Nro Operación"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Operación"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_tran" HeaderText="Transacción"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="soporte" HeaderText="Doc.Soporte"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>--%>