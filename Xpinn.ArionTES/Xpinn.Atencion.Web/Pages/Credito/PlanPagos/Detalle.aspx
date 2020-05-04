<%@ Page Title=".: Plan Pagos :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableNormal
        {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btnImprimir" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" />
    <asp:Button ID="btnregresar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
    <br />
    <br />
    <div class="form-group">
        <asp:Panel ID="panelGeneral" runat="server">
            <div class="col-sm-12">
                <asp:FormView ID="frvData" runat="server" Width="100%" OnDataBound="frvData_DataBound">
                    <ItemTemplate>
                        <table class="col-sm-12 tableNormal">
                            <tr>
                                <td class="text-left" colspan="5">
                                    <strong>Información del Crédito</strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="col-xs-2 text-left">
                                    No. Crédito:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblNumRadic" runat="server" Text='<%# Eval("Numero_radicacion") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                    Línea Crédito:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblCodLinea" runat="server" Text='<%# Eval("LineaCredito") %>' />&nbsp;-&nbsp;
                                    <asp:Label ID="lblLinea" runat="server" Text='<%# Eval("Linea") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                </td>
                            </tr>
                            <tr>
                                <td class="col-xs-2 text-left">
                                    Plazo:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblPlazo" runat="server" Text='<%# Eval("Plazo") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                    Monto&nbsp; Crédito:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblMontoCredito" runat="server" Text='<%# Eval("Monto", "{0:n0}") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                </td>
                            </tr>
                            <tr>
                                <td class="col-xs-2 text-left">
                                    Periodicidad:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblPeriodicidad" runat="server" Text='<%# Eval("Periodicidad") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                    F.Desembolso:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblFecDesembolso" runat="server" Text='<%# Eval("FechaDesembolso", "{0:d}") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                </td>
                            </tr>
                            <tr>
                                <td class="col-xs-2 text-left">
                                    Vr Cuota:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblVrCuota" runat="server" Text='<%# Eval("Cuota", "{0:n0}") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                    Tasa Int.Nom:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblTasaInteres" runat="server" Text='<%# Eval("TasaNom", "{0:P2}") %>' />&nbsp;
                                    -&nbsp;
                                    <asp:Label ID="lblTasaPeriodica" runat="server" Text='<%# Eval("TasaInteres", "{0:P2}") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                </td>
                            </tr>
                             <tr>
                                <td class="col-xs-2 text-left">
                                    Cuotas Pagadas:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblCuotasPagadas" runat="server" Text='<%# Eval("cuotas_pagadas") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                    Cuotas Pendientes:
                                </td>
                                <td class="col-xs-3 text-left">
                                    <asp:Label ID="lblCuotasPendientes" runat="server" Text='<%# Eval("cuotas_pendientes") %>' />
                                </td>
                                <td class="col-xs-2 text-left">
                                </td>
                            </tr>
                            <tr>
                                <td class="col-xs-2 text-left">
                                    <asp:Button runat="server" ID="VerPagare" OnClick="btnVerPagare_Click" CssClass="btn btn-primary" style="padding: 3px 15px;border-radius:0px" Text="Ver Pagaré" Visible="false"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" colspan="5">
                                    <asp:TextBox ID="txtFormaPago" runat="server" Visible="false" Text='<%# Eval("FormaPago") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtdireccion" runat="server" Visible="False" Text='<%# Eval("Direccion") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtciudad" runat="server" Visible="False" Text='<%# Eval("ciudad") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtprimerpago" runat="server" Visible="False" Text='<%# Eval("FechaPrimerPago" , "{0:d}") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtgeneracion" runat="server" Visible="False" Text='<%# Eval("FechaSolicitud" , "{0:d}") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtcuotas" runat="server" Visible="False" Text='<%# Eval("numero_cuotas") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtpagare" runat="server" Visible="False" Text='<%# Eval("pagare") %>'></asp:TextBox>
                                    <asp:TextBox ID="Txtinteresefectiva" runat="server" Visible="False" Text='<%# Eval("TasaEfe", "{0:P2}") %>'></asp:TextBox>
                                    <asp:TextBox ID="txtEstado" runat="server" Visible="False" Text='<%# Eval("Estado") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <hr style="width: 100%" />
                <div class="col-sm-12" style="overflow: scroll; padding: 0px">
                    <table width="100%" class="tableNormal">
                        <tr>
                            <td class="text-left">
                                <strong>Listado Plan de Pagos</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    GridLines="Horizontal" CssClass="table table-hover table-inverse" ShowHeaderWhenEmpty="True"
                                    Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="numerocuota" HeaderText="No" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechacuota" HeaderText="Fecha" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:dd-MM-yyyy}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ControlStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sal_ini" HeaderText="Saldo Inicial" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="capital" HeaderText="Capital" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_1" HeaderText="Int_1" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_2" HeaderText="Int_2" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_3" HeaderText="Int_3" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_4" HeaderText="Int_4" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_5" HeaderText="Int_5" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_6" HeaderText="Int_6" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_7" HeaderText="Int_7" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_8" HeaderText="Int_8" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_9" HeaderText="Int_9" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_10" HeaderText="Int_10" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_11" HeaderText="Int_11" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_12" HeaderText="Int_12" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_13" HeaderText="Int_13" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_14" HeaderText="Int_14" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="int_15" HeaderText="Int_15" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" HeaderText="Total Cuota" DataFormatString="{0:c}"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sal_fin" HeaderText="Saldo Final" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerTemplate>
                                        &nbsp;
                                        <asp:ImageButton ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                            CommandArgument="First" ImageUrl="~/Imagenes/first.png" />
                                        <asp:ImageButton ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                            CommandArgument="Prev" ImageUrl="~/Imagenes/previous.png" />
                                        <asp:ImageButton ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                            CommandArgument="Next" ImageUrl="~/Imagenes/next.png" />
                                        <asp:ImageButton ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pag"
                                            CommandArgument="Last" ImageUrl="~/Imagenes/last.png" />
                                    </PagerTemplate>
                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="panelImprimir" runat="server" Visible="false">
            <div class="col-sm-12">
                <rsweb:ReportViewer ID="rptPlanPagos" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="10pt" Width="100%">
                    <LocalReport ReportPath="Pages\Credito\PlanPagos\rptPlanPagos.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
            <div class="col-sm-12">
                <asp:Literal ID="ltReport" runat="server" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlVerPagare" runat="server" Visible="false">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; width: 50%">
                        <strong>Vizualización de Documento</strong>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; margin-bottom: 20px; width: 100%; margin: auto;">
                        <asp:Literal ID="ltPagare" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
