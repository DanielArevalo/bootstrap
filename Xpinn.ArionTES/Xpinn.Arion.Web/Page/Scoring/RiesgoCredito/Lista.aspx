<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>
    <asp:MultiView ID="mvScoringCreditos" runat="server">
        <asp:View ID="View1" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" Text="Cierres" runat="server" /><br />
                        <asp:DropDownList ID="ddlFechaCierre" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 309px">
                        <asp:Label ID="Label2" Text="Oficina" runat="server" /><br />
                        <asp:DropDownList ID="ddloficina" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label3" Text="Segmento" runat="server" /><br />
                        <asp:DropDownList ID="ddlSegmentoActual" AppendDataBoundItems="true" runat="server" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 309px">
                        No. Crédito
                        <asp:CompareValidator ID="cvNoCredito" runat="server" ControlToValidate="txtCredito"
                            Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                        <br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                    </td>
                    <td style="width: 309px">
                        Identificación<br />
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                        <br />
                    </td>
                    <td style="width: 309px">
                        Línes de Crédito<br />
                        <asp:DropDownList ID="ddlTipoCredito" runat="server" CssClass="dropdown">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="width: 309px">
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <hr width="100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" Style="text-align: left"
                            ShowHeaderWhenEmpty="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDataBound="gvLista_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="fecha_corte" HeaderText="F.Corte" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="oficina" HeaderText="Oficina" />                                
                                <asp:BoundField DataField="numero_radicacion" HeaderText="No.Obligaciòn" />
                                <asp:BoundField DataField="identificacion" HeaderText="NroDocumento" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombres y Apellidos" />
                                <asp:BoundField DataField="total_ingresos" HeaderText="Ingresos" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="total_activos" HeaderText="Activos" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="total_pasivos" HeaderText="Pasivos" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="linea" HeaderText="Línea" />
                                <asp:BoundField DataField="monto" HeaderText="Monto Aprobado" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo al corte" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />                                
                                <asp:BoundField DataField="numero_cuotas" HeaderText="#Cuotas" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas pagas" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="valor_cuota" HeaderText="Vr.Cuota" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                                <asp:BoundField DataField="cod_categoria" HeaderText="Calif.Actual" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="dias_mora" HeaderText="Dias Mora" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="aportes" HeaderText="Aportes" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="tipo_garantia" HeaderText="Tipo de Garantìa" />
                                <asp:BoundField DataField="valor_garantia" HeaderText="Vr.Garantia" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                                <asp:BoundField DataField="valor_avaluo" HeaderText="Vr.Avaluo" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                                <asp:BoundField DataField="reestructurado" HeaderText="Reestructurado" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="antiguedad" HeaderText="Antiguedad" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="provision" HeaderText="Provisiòn" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />                                 
                                <asp:BoundField DataField="capacidad_pago" HeaderText="Capacidad de Pago" />
                                <asp:BoundField DataField="solvencia" HeaderText="Solvencia" />
                                <asp:BoundField DataField="garantias" HeaderText="Garantias" />                                
                                <asp:BoundField DataField="servicio" HeaderText="Servicio" />
                                <asp:BoundField DataField="reestructurado" HeaderText="Reestructuraciones" />
                                <asp:BoundField DataField="antiguedad" HeaderText="Antiguedad" />
                                <asp:BoundField DataField="centrales_riesgo" HeaderText="Centrales Riesgo" />                                
                                <asp:BoundField DataField="score" HeaderText="Score" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Segmento">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCalificacion" runat="server" Text='<%# Bind("calificacion") %>' visible="true" />
                                        <asp:Label ID="lbSegmento" runat="server" Text='<%# Bind("segmento") %>' visible="true" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    <ItemStyle CssClass="gridIco"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="probabilidad_incumplimiento" HeaderText="Probabilidad de incumplimiento" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="cod_categoria_pro" HeaderText="Categoría por Probabilidad de incumplimiento" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="provision_riesgo" HeaderText="Provisiòn Riesgo" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                                <asp:BoundField DataField="segmento" HeaderText="Segmento" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <asp:Button ID="btnRegresarRep" runat="server" CssClass="btn8" 
                onclick="btnRegresar_Click" onclientclick="btnRegresar_Click" 
                Text="Regresar a Scoring" />
            <rsweb:ReportViewer ID="rvScoringCreditos" runat="server" Width="700px" 
                Font-Names="Verdana" Font-Size="8pt" Height="700px" 
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Page\Scoring\ScoringCreditos\Report.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>

</asp:Content>
