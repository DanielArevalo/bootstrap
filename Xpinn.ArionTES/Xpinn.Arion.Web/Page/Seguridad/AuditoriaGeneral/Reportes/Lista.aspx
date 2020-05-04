<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 100%;">
            <tr>
                <td align="center">Consultar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server"
                        CssClass="dropdown"
                        OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged"
                        Width="371px" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="cvConsultar" runat="server"
                        ControlToValidate="ddlConsultar" Display="Dynamic"
                        ErrorMessage="Seleccione un tipo de consulta" ForeColor="Red"
                        Operator="GreaterThan" SetFocusOnError="true" Type="Integer"
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                    </asp:CompareValidator>
                </td>
                <td align="center">
                    <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" Width="250px">
                        <asp:Label ID="LabelCliente" runat="server" Text="Identificación Cliente"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                            MaxLength="128" />
                    </asp:Panel>
                    <br />
                </td>
                <td>
                    <asp:Panel ID="Panel5" runat="server" HorizontalAlign="Center" Width="250px">
                        <asp:Label ID="LabelRadicado" runat="server" Text="Número Crédito"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtRadicado" runat="server" CssClass="textbox"
                            MaxLength="128" />
                    </asp:Panel>
                    &nbsp;
                </td>
                <td>
                    <asp:Panel ID="Panel6" runat="server" HorizontalAlign="Center" Width="150px">
                        <asp:Label ID="LabelOperacion" runat="server" Text="Codigo operación"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOperacion" runat="server" CssClass="textbox"
                            MaxLength="128" />
                    </asp:Panel>
                    &nbsp;
                </td>
                <td>
                    <asp:Panel ID="Panel7" runat="server" HorizontalAlign="Center" Width="250px">
                        <table style="width: 313px">
                            <tr>
                                <td style="text-align: left">

                                    <asp:Label ID="LabelComprobante" runat="server" Text="Numero Comprobante"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtComprobante" runat="server" CssClass="textbox"
                                        MaxLength="128" Width="132px" />

                                </td>
                                <td style="text-align: left">Tipo de Comprobante<br />
                                    <asp:DropDownList ID="ddlTipoComprobante" runat="server"
                                        AppendDataBoundItems="True" CssClass="textbox" Width="163%">
                                        <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
                <td align="center">&nbsp;</td>
                <td style="margin-left: 60px"></td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReportePersonas" runat="server">
            <div style="overflow: scroll; height: 222px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvReportePersonas" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="cod_persona" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        ShowFooter="True" Width="100%"
                        Style="font-size: x-small">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Cod Persona" DataField="cod_persona" />
                            <asp:BoundField HeaderText="tipo_persona" DataField="tipo_persona" />
                            <asp:BoundField HeaderText="Identificacion" DataField="identificacion" />
                            <asp:BoundField HeaderText="Dig. Verif" DataField="digito_verificacion" />

                            <asp:BoundField DataField="fechaexpedicion" DataFormatString="{0:d}"
                                HeaderText="F.Exp.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Genero" DataField="sexo" />
                            <asp:BoundField HeaderText="Prim.Nomb" DataField="primer_nombre" />
                            <asp:BoundField HeaderText="Seg.Nomb" DataField="segundo_nombre" />
                            <asp:BoundField HeaderText="Prim. Ape" DataField="primer_apellido" />
                            <asp:BoundField HeaderText="Seg. Ape" DataField="segundo_apellido" />
                            <asp:BoundField DataField="fechanacimiento" DataFormatString="{0:d}"
                                HeaderText="F.Nac.">

                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="celular" DataField="celular" />

                            <asp:BoundField HeaderText="dircorrespondencia" DataField="dircorrespondencia" />

                            <asp:BoundField DataField="fechacreacion" HeaderText="fechacreacion" />

                            <asp:BoundField DataField="fecultmod" HeaderText="fecultmod" />
                            <asp:BoundField DataField="usuultmod" HeaderText="usuultmod">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_cambio" HeaderText="tipo_cambio">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_cambio" HeaderText="fecha_cambio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usuario_cambio" HeaderText="usuario_cambio"></asp:BoundField>

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

        <br />
        <asp:View ID="VGreporteCreditos" runat="server">
            <div style="overflow: scroll; height: 225px; width: 1000px;">
                <div style="width: 1500px;">
                    <asp:Button ID="btnExportarpoliza" runat="server"
                        CommandArgument="VGreportepolisas" CssClass="btn8"
                        Text="Exportar a excel" OnClick="btnExportarpoliza_Click" />
                    <asp:GridView ID="GvpreporteCreditos" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="identificacion"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        OnSelectedIndexChanged="Gvpreportepolizas_SelectedIndexChanged1"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="103%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="numero_radicacion" HeaderText="Crédito">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_obligacion" HeaderText="Solicitud">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_deudor" HeaderText="Deudor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea_credito" HeaderText="Lin.Crédito">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_solicitado" DataFormatString="{0:N}"
                                HeaderText="M.Solicitado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_aprobado" DataFormatString="{0:N}"
                                HeaderText="M. Aprobado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_desembolsado" DataFormatString="{0:N}"
                                HeaderText="M. Desemb.">
                                <ItemStyle HorizontalAlign="center" />

                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_solicitud" DataFormatString="{0:d}"
                                HeaderText="F. Solicitud">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:d}"
                                HeaderText="F. Aprob">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_desembolso" DataFormatString="{0:d}"
                                HeaderText="F. Desemb">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_primerpago" DataFormatString="{0:d}"
                                HeaderText="F.Primer Pag.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="numero_cuotas"
                                HeaderText="# Cuotas">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cuotas_pagadas"
                                HeaderText="# Cuotas Pag">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cuotas_pendientes"
                                HeaderText="# Cuotas Pend">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cod_periodicidad"
                                HeaderText="Periodicidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="tipo_liquidacion"
                                HeaderText="T.Liquid.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_obligacion" HeaderText="Solicitud">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_deudor" HeaderText="Deudor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea_credito" HeaderText="Lin.Crédito">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_solicitado" DataFormatString="{0:N}"
                                HeaderText="M.Solicitado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_aprobado" DataFormatString="{0:N}"
                                HeaderText="M. Aprobado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_desembolsado" DataFormatString="{0:N}"
                                HeaderText="M. Desemb.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_solicitud" DataFormatString="{0:d}"
                                HeaderText="F. Solicitud">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:d}"
                                HeaderText="F. Aprob">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_desembolso" DataFormatString="{0:d}"
                                HeaderText="F. Desemb">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_primerpago" DataFormatString="{0:d}"
                                HeaderText="F.Primer Pag">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="numero_cuotas"
                                HeaderText="Cuotas">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cuotas_pagadas"
                                HeaderText="Cuotas Pag">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cuotas_pendientes"
                                HeaderText="Cuotas Pend">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cod_periodicidad"
                                HeaderText="Periodicidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="tipo_liquidacion"
                                HeaderText="T.Liquid">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>


                            <asp:BoundField DataField="valor_cuota" DataFormatString="{0:N}"
                                HeaderText="V.Cuota">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_ultimo_pago" DataFormatString="{0:d}"
                                HeaderText="F. Ult Pag">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}"
                                HeaderText="F. Prox Pag.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="saldo_capital" DataFormatString="{0:N}"
                                HeaderText="S.Capital">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fechacreacion" HeaderText="fechacreacion" />
                            <asp:BoundField DataField="fecultmod" HeaderText="fecultmod" />
                            <asp:BoundField DataField="usuultmod" HeaderText="usuultmod">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_cambio" HeaderText="tipo_cambio">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_cambio" HeaderText="fecha_cambio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usuario_cambio" HeaderText="usuario_cambio"></asp:BoundField>


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

        <br />
        <asp:View ID="VGridReporteOperacion" runat="server">
            <div style="overflow: scroll; height: 222px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportarOperacion" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvReporteOperacion" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="cod_ope" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        ShowFooter="True" Width="100%"
                        Style="font-size: x-small">
                        <Columns>
                            <%-- <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                        <HeaderStyle CssClass="gridIco" />
                        <ItemStyle CssClass="gridIco" />
                    </asp:TemplateField>--%>
                            <asp:BoundField HeaderText="Cod. Operación" DataField="cod_ope" />
                            <asp:BoundField HeaderText="Tipo Operacion" DataField="tipo_ope" />
                            <asp:BoundField HeaderText="Cod. Usuario" DataField="cod_usu" />
                            <asp:BoundField HeaderText="Cod. Oficina" DataField="cod_ofi" />
                            <asp:BoundField HeaderText="Cod. Caja" DataField="cod_caja" />
                            <asp:BoundField HeaderText="Cod. Cajero" DataField="cod_cajero" />
                            <asp:BoundField HeaderText="Numero Recibo" DataField="num_recibo" />
                            <asp:BoundField DataField="fecha_real" DataFormatString="{0:d}" HeaderText="Fecha Real">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="hora" DataFormatString="{0:hh\:mm}" HeaderText="Hora">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_oper" DataFormatString="{0:d}" HeaderText="Fecha Operacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_calc" DataFormatString="{0:d}" HeaderText="Fecha calculado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Numero Comprobante" DataField="num_comp" />
                            <asp:BoundField HeaderText="Tipo de Comprobante" DataField="tipo_comp" />
                            <asp:BoundField HeaderText="Estado" DataField="estado_ope" />
                            <asp:BoundField HeaderText="Numero Lista" DataField="num_lista" />
                            <asp:BoundField HeaderText="Tipo Cambio" DataField="tipo_cambio_ope" />
                            <asp:BoundField DataField="fecha_cambio_ope" DataFormatString="{0:d}" HeaderText="Fecha Cambio" />
                            <asp:BoundField DataField="usuario_cambio_ope" HeaderText="Usuario Cambio" />
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

        <br />
        <asp:View ID="VGridReporteComprobante" runat="server">
            <div style="overflow: scroll; height: 222px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportarComprobante" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvReporteComprobante" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="cod_persona" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        ShowFooter="True" Width="100%"
                        Style="font-size: x-small">
                        <Columns>
                            <asp:BoundField HeaderText="Numero Comprobante" DataField="num_compr" />
                            <asp:BoundField HeaderText="Tipo Comprobante" DataField="tipo_compr" />
                            <asp:BoundField HeaderText="Fecha operacion" DataField="fecha_oper" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Hora" DataField="hora" DataFormatString="{0:hh\:mm}" />
                            <asp:BoundField HeaderText="Ciudad" DataField="ciudad" />
                            <asp:BoundField DataField="tipo_pago_compr" HeaderText="Tipo de pago" />
                            <asp:BoundField DataField="numero_pago_compr" HeaderText="Numero de Consignacion" />
                            <asp:BoundField DataField="numero_documento_compr" HeaderText="Numero de Documento" />
                            <asp:BoundField DataField="entidad" HeaderText="Entidad" />
                            <asp:BoundField HeaderText="Concepto" DataField="concepto" />
                            <asp:BoundField HeaderText="Total Comprobante" DataField="totalcom" />
                            <asp:BoundField HeaderText="Tipo Beneficio" DataField="tipo_benef" />
                            <asp:BoundField HeaderText="Cod. Beneficio" DataField="cod_benef" />
                            <asp:BoundField HeaderText="Cod. Elaborado" DataField="cod_elaboro" />
                            <asp:BoundField HeaderText="Cod. Aprobo" DataField="cod_aprobo" />
                            <asp:BoundField HeaderText="Estado" DataField="estado_compr" />
                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                            <asp:BoundField DataField="tipo_cambio_compr" HeaderText="tipo_cambio">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_cambio_compr" HeaderText="fecha_cambio" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usuario_cambio_compr" HeaderText="usuario_cambio"></asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="Label3" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>

        <asp:View ID="viewAuditoriaUsuarios" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportarAuditoriaUsuarios" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvAuditoriaUsuarios" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="cod_auditoria" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        ShowFooter="false" Width="100%"
                        Style="font-size: x-small">
                        <Columns>
                            <asp:BoundField HeaderText="Observacion" DataField="observacion" />
                            <asp:BoundField HeaderText="Fecha operacion" DataField="fecha" />
                            <asp:BoundField HeaderText="Cod. Usuario" DataField="codusuario" />
                            <asp:BoundField HeaderText="Usuario" DataField="nombre_usuario" />
                            <asp:BoundField HeaderText="Cod. Opcion" DataField="codopcion" />
                            <asp:BoundField HeaderText="Opcion" DataField="nombre_opcion" />
                            <asp:BoundField HeaderText="IP" DataField="ip" />
                            <asp:BoundField HeaderText="Accion" DataField="accion" />
                            <asp:BoundField HeaderText="Informacion Enviada" DataField="detalle" />
                            <asp:BoundField HeaderText="Informacion Anterior Que Fue Modificada" DataField="informacionanterior" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="lblNumeroRegistrosEncontradosAuditoriaUsuarios" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
        <asp:View ID="ViewDinamica" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="bntDinamica" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvDinamica" runat="server"  AutoGenerateColumns="False" HeaderStyle-CssClass="gridHeader"  PagerStyle-CssClass="gridPager" 
                        RowStyle-CssClass="gridItem" ShowFooter="True" Width="100%" Style="font-size: x-small">
                        <Columns></Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="LblDinamica" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView>

</asp:Content>

