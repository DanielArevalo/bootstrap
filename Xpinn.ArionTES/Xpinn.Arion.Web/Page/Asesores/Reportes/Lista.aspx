<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 100%;">
            <tr>
                <td align="center" colspan="3">Consultar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True"
                        CssClass="textbox" OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">CARTERA EN MORA A LA FECHA POR EJECUTIVO</asp:ListItem>
                        <asp:ListItem Value="2">REPORTE POLIZAS POR OFICINA</asp:ListItem>
                        <asp:ListItem Value="3">REPORTE COLOCACION POR EJECUTIVO</asp:ListItem>
                        <asp:ListItem Value="4">CARTERA TOTAL POR EJECUTIVO</asp:ListItem>
                        <asp:ListItem Value="5">REPORTE CARTERA ACTIVA POR OFICINA</asp:ListItem>
                        <asp:ListItem Value="6">CARTERA AL CIERRE POR OFICINA</asp:ListItem>
                        <asp:ListItem Value="7"> REPORTE CREDITOS PAGO ESPECIAL</asp:ListItem>
                        <asp:ListItem Value="8">AFILIACIÓN POR EJECUTIVO</asp:ListItem>
                        <asp:ListItem Value="9">DEPOSITOS POR EJECUTIVO</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="cvConsultar" runat="server"
                        ControlToValidate="ddlConsultar" Display="Dynamic"
                        ErrorMessage="Seleccione un tipo de consulta" ForeColor="Red"
                        Operator="GreaterThan" SetFocusOnError="true" Type="Integer"
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                    </asp:CompareValidator>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Labelejecutivos" runat="server" Text="Ejecutivos"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlAsesores" runat="server" CssClass="textbox" Width="220px" />
                    <asp:HiddenField ID="hiddenAsesor" runat="server" />
                </td>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="100px">
                        <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Inicial"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox"
                            MaxLength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image1" TargetControlID="txtFechaIni">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label4" runat="server" Style="color: #FF3300" Text=""></asp:Label>
                    </asp:Panel>
                </td>
                <td align="center">
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" Width="100px">
                        <asp:Label ID="LabelFecha_gara0" runat="server" Text="Fecha Final"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textbox"
                            MaxLength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image2" TargetControlID="txtFechaFin">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label3" runat="server" Style="color: #FF3300" Text=""></asp:Label>
                    </asp:Panel>
                </td>
                <td style="margin-left: 60px"></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Panel ID="Panel2" runat="server" HorizontalAlign="center" Width="60%">
                        <table>
                            <tr>
                                <td style="text-align: center;">Fecha<br />
                                    <ucFecha:fecha ID="txtFechagenerada" runat="server" CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                                </td>
                                <td style="text-align: center;">Línea Crédito<br />
                                    <asp:DropDownList ID="ddllineacredito" runat="server" CssClass="textbox"
                                        Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center;">Oficina<br />
                                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="180px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="margin-left: 60px"></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReporteMora" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvReoirtemora" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True" OnRowDataBound="gvReoirtemora_RowDataBound"
                        Width="100%" Style="font-size: x-small">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="NumRadicacion"
                                DataNavigateUrlFormatString="..//../Recuperacion/Detalle.aspx?radicado={0}"
                                DataTextField="NumRadicacion" HeaderText="Radicado" Text="Radicado"
                                Target="_blank" />
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Fecha Acuerdo" DataField="fecha_acuerdo"
                                DataFormatString="{0:M}" />
                            <asp:BoundField HeaderText="Valor Acuerdo" DataField="valor_acuerdo" DataFormatString="{0:N}" />
                            <asp:BoundField HeaderText="Respuesta cliente" DataField="respuesta">
                                <ItemStyle Width="190px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usuario" HeaderText="Persona Diligencia" />
                            <asp:BoundField DataField="observacion" HeaderText="Observación" />
                            <asp:BoundField DataField="icodigo" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres Cliente">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pagare" HeaderText="Pagare">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" DataFormatString="{0:N}"
                                HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Días en Mora">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora_u_cierre" HeaderText="DiasMoraCierre" />
                            <asp:BoundField DataField="saldo_capital" DataFormatString="{0:N}"
                                HeaderText="Saldo a Capital">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="garantia_comunitaria"
                                HeaderText="Valor G.Comunitaria" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pendite_cuota" DataFormatString="{0:N}"
                                HeaderText="Valor Pendiente">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_cuota" HeaderText="Fecha Cuota Pendiente"
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_a_pagar" DataFormatString="{0:N}" HeaderText="valor_a_pagar">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_oficina"
                                HeaderText="Dirección Empresa">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono_empresa" HeaderText="Telefono Empresa">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_negocio" HeaderText="Direccion Negocio">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_negocio" HeaderText="Barrio Negocio" />
                            <asp:BoundField DataField="telefono_negocio" HeaderText="Telefono Negocio">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_residencia"
                                HeaderText="Dirección Residencia">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_residencia" HeaderText="Barrio Residencia">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono_residencia"
                                HeaderText="Telefono Residencia" />
                            <asp:BoundField DataField="celular" HeaderText="Celular" />
                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                            <asp:BoundField DataField="idpromotor" HeaderText="Cod Ejec." />
                            <asp:BoundField DataField="nombre_asesor" HeaderText="Nombre Ejecutivo" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>

        <asp:View ID="VGreportepolisas" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">
                    <asp:Button ID="btnExportarpoliza" runat="server" CssClass="btn8"
                        OnClick="btnExportarpoliza_Click" Text="Exportar a excel" CommandArgument="VGreportepolisas" />
                    <asp:GridView ID="Gvpreportepolizas" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="identificacion"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="103%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="porcentaje" HeaderText="Porcetaje">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_prima" HeaderText="Valor Prima" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Numero de Radicación">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipoplan" HeaderText="Tipo del Plan">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_pago" HeaderText="Monto" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cedula_asesor" HeaderText="Cedula Asesor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_asesor" HeaderText="Nombre Asesor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fehca_pago" HeaderText="Fehca Pago">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_vigencia" HeaderText="Fecha Vigencia">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label2" runat="server" />
            <br />
            <br />
            &nbsp;
        </asp:View>

        <asp:View ID="Vreportecolocacionejecutivo" runat="server">
            <div style="overflow: scroll; height: 50%; width: 100%;">
                <div style="width: 100%;">
                    <asp:Button ID="btnExportarcolocacion" runat="server" CssClass="btn8"
                        OnClick="btnExportarcolocacion_Click" Text="Exportar a excel" CommandArgument="VGreportepolisas" />
                    <asp:GridView ID="Gvcolocacionejecutivo" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="codigo"
                        GridLines="Both" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="100%"
                        OnRowDataBound="Gvcolocacionejecutivo_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_pago" HeaderText="Valor Desembolsado"
                                DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuenta" HeaderText="Conteo">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label5" runat="server" />
            <br />
            <br />
            &nbsp;
        </asp:View>

        <asp:View ID="Vreportecarteraejecutivo" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">
                    <asp:Button ID="btnExportarcarteraejecutivo" runat="server" CssClass="btn8"
                        OnClick="btnExportarcarteraejecutivo_Click" Text="Exportar a excel"
                        CommandArgument="VGreportepolisas" />
                    <asp:GridView ID="Gvcarteraejecutivo" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="codigo"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Cedula Cliente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="credito" HeaderText="Crédito">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="linea">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_pago" HeaderText="Monto de Pago" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_desenbolso" HeaderText="fecha Desenbolso">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha Proximo Pago">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Dias en Mora">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_asesor" HeaderText="Nombre asesor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label6" runat="server" />
            <br />
            <br />
            &nbsp;
        </asp:View>

        <asp:View ID="Vreporteactivo" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">
                    <asp:Button ID="btnExportarreporactivo" runat="server" CssClass="btn8"
                        OnClick="btnExportarreporactivo_Click" Text="Exportar a excel" />
                    <asp:GridView ID="Gvreporactivo" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="NumRadicacion"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres Cliente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha Proximo Pago">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cuotas" HeaderText="Número Cuota">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagadas">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_asesor" HeaderText="Codigo Asesor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pagare" HeaderText="Pagare">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_oficina"
                                HeaderText="Dirección Empresa">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_empresa" HeaderText="Barrio Empresa">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono_empresa" HeaderText="Telefono Empresa">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_negocio"
                                HeaderText="Dirección Negocio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_negocio" HeaderText="Barrio Negocio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono_negocio" HeaderText="Telefono Negocio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_residencia"
                                HeaderText="Direccion Residencia">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_residencia"
                                HeaderText="Barrio Residencia">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="telefono_residencia"
                                HeaderText="Telefono Residencia" />
                            <asp:BoundField DataField="celular" HeaderText="Celular" />
                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" />

                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label7" runat="server" />
            <br />
            <br />
            &nbsp;                                    
        </asp:View>

        <asp:View ID="Carteracierre" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">
                    <asp:Button ID="Button1" runat="server" CssClass="btn8"
                        OnClick="btnExportarCarteracierre_Click" Text="Exportar a excel" />
                    <asp:GridView ID="GvCarteracierre" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="NumRadicacion"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="100%" OnRowDataBound="GvCarteracierre_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                <ControlStyle Font-Bold="False" />
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Asesor" />
                            <asp:BoundField DataField="codigo" HeaderText="Codigo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechacierre" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="fecha Cierre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_credito" HeaderText="Num.Creditos">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_cierre" HeaderText="Saldo Cierre"
                                DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_colocacion_mes" HeaderText="Colocación Mes">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_colocacion_mes"
                                HeaderText="Monto Colocación Mes" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_mora" HeaderText="Mora Total" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_mora" HeaderText="Saldo Mora"
                                DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mora_menor_30" HeaderText="Mora Menor 30" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_menor_30" HeaderText="Monto Menor 30"
                                DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mora_mayor_30" HeaderText="Mora Mayor 30" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_mayor_30" HeaderText="Monto Mayor 30"
                                DataFormatString="{0:N}" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label1" runat="server" />
            <br />
            <br />
            &nbsp;                                    
        </asp:View>

        <asp:View ID="gvpagoespecial" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 97%;">
                    <asp:Button ID="Button2" runat="server" CssClass="btn8"
                        Text="Exportar a excel" OnClick="btn_export_gvpagos" />
                    <asp:GridView ID="gvpagosespeciales" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="numero_radicacion"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="100%" ShowFooter="False" Style="font-size: x-small">
                        <Columns>
                            <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Radicación" ItemStyle-Width="50px">
                                <ControlStyle Font-Bold="False" />
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombress" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Línea" />
                            <asp:BoundField DataField="fecha_solicitud" DataFormatString="{0:d}" HeaderText="Fecha Solicitud">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Aprob.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="plazo" HeaderText="Plazo" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo"
                                HeaderText="Saldo" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="Fec.Prox.Pago">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label8" runat="server" />
            <br />
            <br />
            &nbsp;                                    
        </asp:View>

        <asp:View ID="vAfiliacionAsesor" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 97%;">
                    <asp:Button ID="btnAfiliacionAsesor" runat="server" CssClass="btn8"
                        Text="Exportar a excel" OnClick="btn_export_gvAfiliacionAsesor" />
                    <asp:GridView ID="gvAfiliacionAsesor" runat="server"
                        AutoGenerateColumns="False"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="100%" ShowFooter="False" Style="font-size: x-small"
                        HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="gvAfiliacionAsesor_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Num. Afiliación" ItemStyle-Width="50px">
                                <ControlStyle Font-Bold="False" />
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_afiliacion" DataFormatString="{0:d}" HeaderText="Fecha Afiliación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Cliente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="razon_social" HeaderText="Razon Social">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" DataFormatString="${0:#,##0.00}" HeaderText="Cuota Aporte">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblAfiliacionAsesor" runat="server" />
                </div>
            </div>
            <asp:Label ID="Label9" runat="server" />
            <br />
            <br />
            &nbsp;                                    
        </asp:View>
        
        <asp:View ID="vAsesoresProductos" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 97%;">
                    <asp:Button ID="btnAsesoresProductos" runat="server" CssClass="btn8"
                        Text="Exportar a excel" OnClick="btn_export_gvAsesoresProductos" />
                    <asp:GridView ID="gvAsesoresProductos" runat="server"
                        AutoGenerateColumns="False"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Width="100%" ShowFooter="False" Style="font-size: x-small"
                        HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="gvAsesoresProductos_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Numero Producto" ItemStyle-Width="50px">
                                <ControlStyle Font-Bold="False" />
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fecha Apertura">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Cliente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="razon_social" HeaderText="Razon Social">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="${0:#,##0.00}" HeaderText="Monto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblAsesoresProductos" runat="server" />
                </div>
            </div>
            <br />
            <br />
            &nbsp;                                    
        </asp:View>

    </asp:MultiView>

</asp:Content>

