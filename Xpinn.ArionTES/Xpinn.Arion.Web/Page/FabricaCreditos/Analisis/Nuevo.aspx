<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctlCuotasExtras" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlCodeudores.ascx" TagName="ctlCodeudores" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlDocumentosAnexo.ascx" TagName="ctlDocumentosAnexo" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />

    <script src="../../../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    <script src="../../../Scripts/JScript.js" type="text/javascript"></script>


    <style>
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 11px 23px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 15px;
            margin: 4px 2px;
            cursor: pointer;
        }

        .button2 {
            background-color: #008CBA;
        }

    </style>

    <!---->
    <input type="hidden" id="postbackControl" value="<%=Page.IsPostBack.ToString()%>" />
    <asp:MultiView ID="mvCredito" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <div>
                <asp:Label ID="lblgeneral" runat="server" Text="" Style="display: none;"></asp:Label>
            </div>
            <div>
                <asp:Label ID="MensajeViabilidad" runat="server" Style="font-weight: 700" Text="" ForeColor="#FF3300" />
                <br />
                <asp:Label ID="detalleValidacion" runat="server" Style="color: #FF3300" Text="" />
            </div>
            <asp:Panel ID="PTituloAnalisis" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px;">
                <asp:Label ID="LblTutiloAnalisis" runat="server" Text="ANÁLISIS DE LAS SOLICITUDES DE CRÉDITO" Style="color: #FFF; font-weight: bold"></asp:Label>
            </asp:Panel>
            <table id="TblAnalisisSolicitudes" class="tableNormal" runat="server" style="width: 100%; margin: 2% 0px">
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label runat="server" Text="Fecha Analisis:" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtFechaRecibido" runat="server" Width="100%" Enabled="false" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblNumeroSolicitud" runat="server" Style="text-align: left; margin-left: 10%" Text="N° Crédito"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtNumeroSolicitud" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                        <asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown"
                            Enabled="False" Height="25px" Width="186px" Visible="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblSolicitante" runat="server" Text="Solicitante" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtSolicitante" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblValorSolicitado" runat="server" Style="text-align: left; margin-left: 10%" Text="Valor solicitado"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtValorSolicitado" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; height: 26px;">
                        <asp:Label ID="LblDocumentoIdentidad" runat="server" Text="Documento de identidad" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%; height: 26px;">
                        <asp:TextBox ID="TxtDocumentoIdentidad" Enabled="false" runat="server" Width="100%"
                            CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left; height: 26px;">
                        <asp:Label ID="LblPlazo" runat="server" Style="text-align: left; margin-left: 10%" Text="Plazo"></asp:Label>
                    </td>
                    <td style="width: 25%; height: 26px;">
                        <asp:TextBox ID="TxtPlazo" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblEdad" runat="server" Text="Edad:" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtEdad" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblFrecuenciaCuota" runat="server" Style="text-align: left; margin-left: 10%" Text="Frecuencia cuota"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtFrecuenciaCuota" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblFormaPago" runat="server" Text="Forma de pago" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtFormaPago" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblValorGirar" runat="server" Style="text-align: left; margin-left: 10%" Text="Valor a Girar "></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtValorGirar" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblAntAsociado" runat="server" Text="Antiguedad Asociado" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtAntAsociado" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="lblfecSolici" runat="server" Style="text-align: left; margin-left: 10%" Text="Fecha Solicitud"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtFechaSoli" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="lblAntLAb" runat="server" Text="Antiguedad Laboral" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtAntLaboral" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="lblTasa" runat="server" Style="text-align: left; margin-left: 10%" Text="Tasa"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtTasa" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="lblTipoContrato" runat="server" Text="Tipo Contrato" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtTipoContrato" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="LblModalidadCredito" runat="server" Style="text-align: left; margin-left: 10%" Text="Modalidad de crédito"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtModalidadCredito" Enabled="false" runat="server" Width="100%"
                            CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label runat="server" Style="text-align: left;" Text="Empresa Recaudo"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtEmpresaReca" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="Label1" runat="server" Style="text-align: left; margin-left: 10%" Text="Total Cuota a Descontar"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtVrCuotaDescontar" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; display: none">
                        <asp:Label ID="lblDeudas" runat="server" Text="Deudas" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%; display: none">
                        <asp:Label ID="lblDeudasActuales" runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtDeudasActuales" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>

                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="Label2" runat="server" Style="text-align: left;" Text="% Salud/Pension"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtPorcentaje" Enabled="true" runat="server" Width="100%" CssClass="textbox" Text="8" onkeypress="return isNumber(event)"
                            onblur="calcular('Egreso', 0, 0)"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="Label6" runat="server" Style="text-align: left; margin-left: 10%" Text="Destinación"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="TxtDestinacion" Enabled="false" runat="server" Width="100%" CssClass="textbox" Text="8"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">
                        <asp:Label ID="lblZona" runat="server" Text="Zona" Style="text-align: left;"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtZona" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left">Observación:
                    </td>
                    <td colspan="3" style="text-align: left;">
                        <asp:TextBox ID="txtObservacion" Enabled="false" runat="server" TextMode="multiline" Width="100%" CssClass="textbox"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="width: 100%; display: none" colspan="4">
                        <asp:Label ID="LblActividadProfesion" runat="server" Text="Actividad (ES) o profesión del solicitante:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; display: none" colspan="4">
                        <asp:TextBox ID="TxtActividadProfesion" runat="server" Width="90%" Style="height: 35px;" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>

            </table>
            <div>
                <button class="button button2" id="btnCuotasExtras" onclick="Toogle()">Cuotas Extras</button>
                <button class="button button2" id="btnCodeudores" onclick="Toogleco()">Codeudores</button>
                <button class="button button2" id="btnDocAnexos" onclick="ToogleDoc()">Documentos Anexos</button>
                <asp:CheckBox ID="CkcAfiancol" runat="server" Enabled="True" OnCheckedChanged="CkcAfiancol_OnCheckedChanged" AutoPostBack="True" />
                <asp:Label ID="Label5" runat="server" Text="Afiancol "></asp:Label>
            </div>
            <div>
            </div>
            <div id="divCodeudores" style="display: none">
                <uc1:ctlCodeudores runat="server" ID="Codeudores" />
            </div>
            <div id="divCuotasExtras" style="display: none">
                <uc1:ctlCuotasExtras runat="server" ID="CuotasExtras" />
            </div>
            <div id="divDocAnexos" style="display: none">
                <uc1:ctlDocumentosAnexo runat="server" ID="DocumentosAnexo" />
            </div>

            <br />
            <asp:Panel ID="PTutuloAnalisisPromedio" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px;">
                <asp:Label ID="LblAnalisisInterno" runat="server" Text="ANÁLISIS INTERNO" Style="color: #FFF; font-weight: bold"></asp:Label>
            </asp:Panel>

            <table id="TblAnalisisInterno" class="tableNormal" runat="server" style="width: 100%; margin-bottom: 1%; text-align: center; font-weight: bold">
                <tr>
                    <td style="width: 100%; text-align: center; font-weight: bold">
                        <asp:Label ID="LblAnalisisPromedios" runat="server" Text="Análisis de promedios"></asp:Label>
                    </td>
                </tr>
            </table>

            <%--Inicia GridView--%>
            <asp:GridView ID="gvAnalisisPromedio" runat="server" CellPadding="4"
                GridLines="None" Width="100%" AutoGenerateColumns="False" ShowFooter="true">
                <Columns>
                    <asp:BoundField HeaderText="Vínculo con la Cooperativa" DataField="producto" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Fecha Apertura" DataFormatString="{0:d}" DataField="fecha_apertura" />
                    <asp:TemplateField HeaderText="Saldo">
                        <ItemTemplate>
                            <asp:Label ID="lblSaldoPromedio" runat="server" Text='<%# Eval("saldo", "{0:N}") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTotalSaldoPromedio" Style="text-align: right" Width="112px" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Reciprocidad" DataFormatString="{0:N}" DataField="reciprocidad" />
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" />
                <RowStyle HorizontalAlign="right" CssClass="gridItem" />
                <FooterStyle HorizontalAlign="right" />
            </asp:GridView>

            <table>
                <tr style="text-align: right">
                    <td style="padding-right: 30px; font-weight: 600">
                        <asp:Label ID="lblSaldoAportes" runat="server" Visible="false"></asp:Label>
                        Cupo Disponible:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCupoDisponible" Style="text-align: right" Width="200px" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>

            </table>

            <table id="TblSubtituloEstadosCuenta" class="tableNormal" runat="server" style="width: 100%; margin-top: 3%; text-align: center;">
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Label ID="LblEstadosCuenta" runat="server" Text="Estados de cuenta" Style="font-weight: bold"></asp:Label>
                    </td>
                </tr>
            </table>

            <%--Inicia GridView--%>
            <asp:GridView ID="gvEstadoCuenta" runat="server" CellPadding="4"
                GridLines="None" Width="100%" AutoGenerateColumns="False" ShowFooter="true">
                <Columns>
                    <asp:BoundField HeaderText="Fecha Desembolso" DataFormatString="{0:d}" DataField="fecha_desembolso_nullable" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Linea" DataField="linea_credito" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="No.Crédito" DataField="numero_radicacion" />
                    <asp:BoundField HeaderText="Forma Pago" DataField="forma_pago" />
                    <asp:TemplateField HeaderText="Valor Inicial">
                        <ItemTemplate>
                            <asp:Label ID="lblValorInicial" runat="server" Text='<%# Eval("monto_aprobado", "{0:N}") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTotalValorInicial" Style="text-align: right" Width="112px" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saldo">
                        <ItemTemplate>
                            <asp:Label ID="lblSaldo" runat="server" Text='<%# Eval("saldo_capital", "{0:N}") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTotalSaldo" Width="112px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor cuota">
                        <ItemTemplate>
                            <asp:Label ID="lblValorCuota" runat="server" Text='<%# Eval("valor_cuota", "{0:N}") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTotalValorCuota" Width="112px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" />
                <RowStyle HorizontalAlign="right" CssClass="gridItem" />
                <FooterStyle HorizontalAlign="right" />
            </asp:GridView>

            <asp:Panel ID="PAnalisisOtorgamiento" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: center;">
                <asp:Label ID="LblAnalisisOtrogamiento" runat="server" Text="ANÁLISIS PARA EL OTORGAMIENTO DEL CRÉDITO" Style="color: #FFF; font-weight: bold"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="panelCapacidad" Width="100%" runat="server">
                <asp:Table ID="TblCapacidadDepago" class="tableNormal" runat="server" Style="margin: 2% 0px 0px 0px;" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell Style="width: 25%; background-color: #0099FF; color: #FFF; text-align: center; font-weight: bold; max-width: 300px;">
                            <asp:Label ID="LblConcepto" runat="server" Text="Concepto"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%; background-color: #0099FF; text-align: center; color: #FFF; font-weight: bold;">
                            <asp:Label ID="LblDeudor" runat="server" Text="Deudor"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%; background-color: #0099FF; text-align: center; color: #FFF; font-weight: bold;">
                            <asp:Label ID="LblCopdeudorUno" runat="server" Text="Codeudor 1"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%; background-color: #0099FF; text-align: center; color: #FFF; font-weight: bold;">
                            <asp:Label ID="LblCodeudorDos" runat="server" Text="Codeudor 2"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%; background-color: #0099FF; text-align: center; color: #FFF; font-weight: bold;">
                            <asp:Label ID="LblCodeudorTres" runat="server" Text="Codeudor 3"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblIngresos" runat="server" Text="Ingresos"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresos1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 0, 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresos2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 1, 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresos3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 2, 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresos4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 3, 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblOtrosIngresos" runat="server" Text="Otros ingresos" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtOtrosIngresos1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 0,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtOtrosIngresos2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 1,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtOtrosIngresos3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 2,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtOtrosIngresos4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 3,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="Arriendo" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblArrendamiento" runat="server" Text="Arrendamientos" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtArrendamiento1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 0,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtArrendamiento2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 1,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtArrendamiento3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 2,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtArrendamiento4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 3,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="Honorarios" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblHonorario" runat="server" Text="Honorarios" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHonorario1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 0,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHonorario2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 1,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHonorario3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 2,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHonorario4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Ingreso', 3,0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblIngresosBrutos" runat="server" Text="A. Total ingresos brutos" Style="text-align: left; font-weight: bold"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosBrutos1" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosBrutos2" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosBrutos3" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosBrutos4" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblDeduccionesSegSocial" runat="server" Text="Salud / Pensión" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtDeduccionesSocial1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onblur="calcular('Egreso', 0, 0, this.id)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtDeduccionesSocial2" autocomplete="off"
                                onkeyup="calcular('Egreso', 1, 0, this.id)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtDeduccionesSocial3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2, 0, this.id)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtDeduccionesSocial4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3, 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblCuotasFinanPrincipal" runat="server" Text="Cuotas Oblig. Finan (principal)" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanPrincipal1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanPrincipal2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanPrincipal3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanPrincipal4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="ObliCode" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblCuotasFinanCodeudor" runat="server" Text="Cuotas Oblig. Finan(codeudor)" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanDeudor1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanDeudor2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanDeudor3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtCuotasFinanDeudor4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblGastosFamiliares" runat="server" Text="Gastos familiares" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="TxtGastosFamiliares1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="TxtGastosFamiliares2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="TxtGastosFamiliares3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="TxtGastosFamiliares4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblAportes" runat="server" Text="Aportes" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtAporte1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtAporte2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtAporte3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="txtAporte4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="otrDesc" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblOtrosDcstos" runat="server" Text="Otros Descuentos" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtOtrosDsctos1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtOtrosDsctos2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtOtrosDsctos3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="txtOtrosDsctos4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="CredVig" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblCreditosVig" runat="server" Text="Créditos vigentes" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtCreditoVig1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtCreditoVig2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtCreditoVig3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="txtCreditoVig4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="DescSer" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblServicios" runat="server" Text="Dctos Servicios" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtServicio1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtServicio2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtServicio3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="txtServicio4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="DeudTerc" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblDeudasTerceros" runat="server" Text="Deudas Terceros" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtDeudasTer1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtDeudasTer2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtDeudasTer3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="txtDeudasTer4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="ProtSal" runat="server">
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="lblProteccionSalario" runat="server" Text="Protección Salarial" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtProtSalarial1" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox" onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtProtSalarial2" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox" onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; max-width: 200px;">
                            <asp:TextBox ID="txtProtSalarial3" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox" onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; max-width: 200px;">
                            <asp:TextBox ID="txtProtSalarial4" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox" onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblB" runat="server" Text="B- Total Egresos" Style="text-align: left; font-weight: bold;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtB1" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtB2" runat="server" BackColor="#F4F5FF" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtB3" runat="server" BackColor="#F4F5FF" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtB4" runat="server" BackColor="#F4F5FF" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblIngresoNetoMensual" runat="server" Text="Ingreso Neto Mensual(A-B)" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosMensual1"
                                autocomplete="off"
                                BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosMensual2"
                                autocomplete="off"
                                onkeypress="return isNumber(event)"
                                BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosMensual3"
                                autocomplete="off"
                                onkeypress="return isNumber(event)"
                                BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresosMensual4"
                                autocomplete="off"
                                onkeypress="return isNumber(event)"
                                BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="text-align: left">
                            <asp:Label ID="LblIngresoNetoTrimestral" runat="server" Text="Ingreso Neto Frecuencia" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresoTrimestral1"
                                autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="CalcularCuotaSobreIngreso()"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresoTrimestral2"
                                autocomplete="off"
                                onkeypress="return isNumber(event)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresoTrimestral3"
                                autocomplete="off"
                                onkeypress="return isNumber(event); "
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="TxtIngresoTrimestral4"
                                autocomplete="off"
                                onkeypress="return isNumber(event)"
                                runat="server" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <table id="TblCuota1" runat="server" style="width: 100%;">
                    <tr>
                        <td style="text-align: left; width: 25%; height: 26px;">
                            <asp:Label ID="LblCuotaCreditoSolicitado" runat="server" Text="Cuota Crédito Solicitado" Style="text-align: left; font-weight: bold;"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 19%; height: 26px;">
                            <asp:TextBox ID="TxtCuotaCredito1" Enabled="False" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox"></asp:TextBox>
                        </td>
                        <td style="width: 56%; height: 26px;"></td>
                    </tr>

                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="LblCuotaIngresoNeto" runat="server" Text="% Cuota Sobre Ingreso Neto" Style="text-align: left;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TxtCuotaIngresosNeto1" BackColor="#F4F5FF" runat="server" Width="100%"
                                CssClass="textBox" step="any"></asp:TextBox>
                        </td>
                        <td style="width: 55%"></td>
                    </tr>
                </table>

                <br />
                <asp:Panel ID="Panel1" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: center;">
                    <asp:Label ID="lblCapacidadDctoPago" runat="server" Text="CAPACIDAD DE DESCUENTO POR NOMINA Y DE PAGO" Style="color: #FFF; font-weight: bold;"></asp:Label>
                </asp:Panel>
                <asp:Table ID="tblCapacidadDstoPago" class="tableNormal" runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell Style="width: 25%; text-align: left; max-width: 300px">
                            <asp:Label ID="lblCapDesc" runat="server" Text="Capacidad de Descuentos" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; margin-left: 1%;">
                            <asp:TextBox ID="txtCapDesc1" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 0)"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; margin-left: 1%;">
                            <asp:TextBox ID="txtCapDesc2" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 1)"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 19%; margin-left: 1%;">
                            <asp:TextBox ID="txtCapDesc3" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 2)"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="width: 18%; margin-left: 1%;">
                            <asp:TextBox ID="txtCapDesc4" autocomplete="off"
                                onkeypress="return isNumber(event)"
                                onkeyup="calcular('Egreso', 3)"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="width: 25%; text-align: left; max-width: 300px">
                            <asp:Label ID="lblCapPago" runat="server" Text="Capacidad de Pago" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:TextBox ID="txtCapPago1" autocomplete="off"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:TextBox ID="txtCapPago2" autocomplete="off"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:TextBox ID="txtCapPago3" autocomplete="off"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:TextBox ID="txtCapPago4" autocomplete="off"
                                runat="server" Width="100%" BackColor="#F4F5FF"
                                CssClass="TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="width: 25%; text-align: left; max-width: 300px">
                            <asp:Label ID="lblEspacio" runat="server" Text="." Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="width: 25%; text-align: left; max-width: 300px">
                            <asp:Label ID="lblCalif" runat="server" Text="Calif.Centrales Riesgo" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:DropDownList ID="ddlCalifica1" runat="server" CssClass="dropdown" Height="25px" Width="100%" >
                                <asp:ListItem Value="0" Text="" />
                                <asp:ListItem Value="1" Text="No tiene obligaciones en otras entidades" />
                                <asp:ListItem Value="2" Text="Tiene obligaciones sin reporte negativo" />
                                <asp:ListItem Value="3" Text="Tiene obligaciones en mora" />
                                <asp:ListItem Value="4" Text="Tiene obligaciones castigadas" />
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:DropDownList ID="ddlCalifica2" runat="server" CssClass="dropdown" Height="25px" Width="100%" >
                                <asp:ListItem Value="0" Text="" />
                                <asp:ListItem Value="1" Text="No tiene obligaciones en otras entidades" />
                                <asp:ListItem Value="2" Text="Tiene obligaciones sin reporte negativo" />
                                <asp:ListItem Value="3" Text="Tiene obligaciones en mora" />
                                <asp:ListItem Value="4" Text="Tiene obligaciones castigadas" />
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:DropDownList ID="ddlCalifica3" runat="server" CssClass="dropdown" Height="25px" Width="100%" >
                                <asp:ListItem Value="0" Text="" />
                                <asp:ListItem Value="1" Text="No tiene obligaciones en otras entidades" />
                                <asp:ListItem Value="2" Text="Tiene obligaciones sin reporte negativo" />
                                <asp:ListItem Value="3" Text="Tiene obligaciones en mora" />
                                <asp:ListItem Value="4" Text="Tiene obligaciones castigadas" />
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <asp:DropDownList ID="ddlCalifica4" runat="server" CssClass="dropdown" Height="25px" Width="100%" >
                                <asp:ListItem Value="0" Text="" />
                                <asp:ListItem Value="1" Text="No tiene obligaciones en otras entidades" />
                                <asp:ListItem Value="2" Text="Tiene obligaciones sin reporte negativo" />
                                <asp:ListItem Value="3" Text="Tiene obligaciones en mora" />
                                <asp:ListItem Value="4" Text="Tiene obligaciones castigadas" />
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="width: 25%; text-align: left; max-width: 300px">
                            <asp:Label ID="lblFechaConsulta" runat="server" Text="Fecha Consulta" Style="text-align: left;"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <ucFecha:fecha ID="ucFechaConsulta1" runat="server" style="text-align: left" />
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <ucFecha:fecha ID="ucFechaConsulta2" runat="server" style="text-align: left" />
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <ucFecha:fecha ID="ucFechaConsulta3" runat="server" style="text-align: left" />
                        </asp:TableCell>
                        <asp:TableCell Style="margin-left: 1%;">
                            <ucFecha:fecha ID="ucFechaConsulta4" runat="server" style="text-align: left" />
                        </asp:TableCell>                        
                    </asp:TableRow>
                </asp:Table>
                <br />
                <table style="width: 100%" id="TabCupo" runat="server">
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <b>ANALISIS DE CUPO</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70%; text-align: left">Cupo de endeudamiento
                                    </td>
                                    <td style="width: 30%; text-align: right; margin-right: 10px">
                                        <asp:Label ID="lblCupoEndeuda" runat="server" Style="font-weight: 700" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70%; text-align: left">Cupo disponible
                                    </td>
                                    <td style="width: 30%; text-align: right; margin-right: 10px">
                                        <asp:Label ID="lblCupoDispo" runat="server" Style="font-weight: 700" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70%; text-align: left">Nivel de Endeudamiento
                                    </td>
                                    <td style="width: 30%; text-align: right; margin-right: 10px">
                                        <asp:Label ID="lblNivelEndeudamiento" runat="server" Style="font-weight: 700" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%; text-align: center; vertical-align: top">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <b>DESCUBIERTO</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70%; text-align: left">Descubierto pago por caja    
                                    </td>
                                    <td style="width: 30%; text-align: right">
                                        <asp:Label ID="lblDescuCaja" runat="server" Style="font-weight: 700" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70%; text-align: left">Total Obligaciones - Aportes
                                    </td>
                                    <td style="width: 30%; text-align: right">
                                        <asp:Label ID="lblTotObliAportes" runat="server" Style="font-weight: 700" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <asp:Label ID="lblMensajeEndeudamiento" runat="server" Style="font-weight: 700" Text="" ForeColor="#FF3300" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />

            <asp:Panel ID="Panel2" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: center;">
                <asp:Label ID="Label4" runat="server" Text="HISTORIAL CALIFICACIONES" Style="color: #FFF; font-weight: bold;"></asp:Label>
            </asp:Panel>

            <table id="Table1" class="tableNormal" runat="server" style="width: 100%; margin-top: 3%; text-align: center;">
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Label ID="Label3" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                    </td>
                </tr>
            </table>

            <%--Inicia GridView--%>
            <asp:GridView ID="gvCalificacion" runat="server" CellPadding="4"
                GridLines="None" Width="100%" AutoGenerateColumns="False" ShowFooter="true">
                <Columns>
                    <asp:BoundField HeaderText="Periodo" DataField="Año" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Ene" DataField="Ene" />
                    <asp:BoundField HeaderText="Feb" DataField="Feb" />
                    <asp:BoundField HeaderText="Mar" DataField="Mar" />
                    <asp:BoundField HeaderText="Abr" DataField="Abr" />
                    <asp:BoundField HeaderText="May" DataField="May" />
                    <asp:BoundField HeaderText="Jun" DataField="Jun" />
                    <asp:BoundField HeaderText="Jul" DataField="Jul" />
                    <asp:BoundField HeaderText="Ago" DataField="Ago" />
                    <asp:BoundField HeaderText="Sep" DataField="Sep" />
                    <asp:BoundField HeaderText="Oct" DataField="Oct" />
                    <asp:BoundField HeaderText="Nov" DataField="Nov" />
                    <asp:BoundField HeaderText="Dic" DataField="Dic" />
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" />
                <RowStyle HorizontalAlign="right" CssClass="gridItem" />
                <FooterStyle HorizontalAlign="right" />
            </asp:GridView>

            <br />
            <asp:Panel ID="Ptitulo3" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: center;">
                <asp:Label ID="LblGarantiasOfrecidas" runat="server" Text="GARANTÍAS OFRECIDAS" Style="color: #FFF; font-weight: bold;"></asp:Label>
            </asp:Panel>
            <table id="Table3" class="tableNormal" runat="server" style="width: 100%; margin: 1% 0px;">
                <tr>
                    <td style="width: 100%">
                        <asp:TextBox ID="TxtGarantiasOfrecidas" runat="server" CssClass="TextBox" Style="margin-left: 0px; margin-left: 2%;" Width="95%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Ptitulo4" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: center;">
                <asp:Label ID="LblDocumentosPrevistos" runat="server" Text="DOCUMENTOS PROVISTOS POR EL SOLICITANTE" Style="color: #FFF; font-weight: bold;"></asp:Label>
            </asp:Panel>
            <table id="TblDocumentosPrevistos" runat="server" style="width: 100%; margin: 1% 0px;">
                <tr>
                    <td style="width: 100%">
                        <asp:TextBox ID="TxtDocumentosPrevistos" runat="server" Width="95%"
                            Style="margin-left: 2%;" CssClass="TextBox" TextMode="MultiLine"
                            Height="35px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Ptitulo5" runat="server" Style="background-color: #0099FF; height: 20px; font-weight: bold; text-align: center;">
                <asp:Label ID="LblCodeudores" runat="server" Text="Codeudores" Style="color: #fff;"></asp:Label>
            </asp:Panel>
            <asp:Table ID="TblCoudeudores" class="tableNormal" runat="server" Style="width: 100%;">
                <asp:TableRow Width="100%">
                    <asp:TableCell ID="cellLblCodeudor1" Style="width: 15%; background-color: #0099FF; color: #fff;">
                        <asp:Label ID="LblCodeudor1" runat="server" Text="Codeudor 1" Style="color: #FFF"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtCodeudor1" runat="server" TextMode="MultiLine" Height="35px" Width="95%" Style="margin-left: 5%;" CssClass="TextBox"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="cellLblCodeudor2">
                    <asp:TableCell Style="width: 15%; background-color: #0099FF; color: #FFF;">
                        <asp:Label ID="LblCodeudor2" runat="server" Text="Codeudor 2" Style="color: #FFF"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtCodeudor2" TextMode="MultiLine" Height="35px" runat="server" Width="95%" Style="margin-left: 5%;" CssClass="TextBox"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="cellLblCodeudor3">
                    <asp:TableCell Style="width: 15%; background-color: #0099FF; color: #FFF;">
                        <asp:Label ID="labelCodeudor3" runat="server" Text="Codeudor 3" Style="color: #FFF"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="textBoxCodeudor3" runat="server" TextMode="MultiLine" Width="95%" Height="35px" Style="margin-left: 5%;" CssClass="TextBox"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Panel ID="Ptitulo6" runat="server" Style="background-color: #0099FF; height: 20px; font-weight: bold; text-align: center;">
                <asp:Label ID="LblTeriminosAprobacion" runat="server" Text="TERMINOS APROBACIÓN" Style="color: #fff;"></asp:Label>
            </asp:Panel>
            <table id="TblConceptoAnalista" runat="server" style="width: 100%; margin-bottom: 1%; text-align: center;">
                <tr>
                    <td style="width: 100%; text-align: center; font-weight: bold;">
                        <asp:Label ID="LblConceptoAnalista" runat="server" Text="Concepto del Analista de Crédito"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="TblTermiosAprobacion" runat="server" style="display: block; width: 100%; margin: 1% 0px 1% 0px;">
                <tr style="text-align: center">
                    <td style="background-color: #0099FF; width: 25%; text-align: center;">
                        <asp:Label ID="LblViable" runat="server" Text="Viable" Style="color: #FFF;"></asp:Label>
                    </td>
                    <td style="margin-left: 10%; width: 25%">
                        <asp:RadioButton GroupName="Viabilidad" class="checkboxs" ID="RbtViable" runat="server" AutoPostBack="True" OnCheckedChanged="RbtViable_OnCheckedChanged" />
                    </td>
                    <td style="background-color: #0099FF; width: 25%; text-align: center;">
                        <asp:Label ID="LblNoViable" runat="server" Text="No viable" Style="color: #FFF;"></asp:Label>
                    </td>
                    <td style="margin-left: 10%; width: 25%">
                        <asp:RadioButton GroupName="Viabilidad" class="checkboxs" ID="RbtNoViable" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="margin-top: 1%; width: 25%;">
                        <asp:Label ID="LblJustificacion" runat="server" Text="Justificación:"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="TblJustificacion" runat="server" style="width: 100%; margin: 0px 0px 1% 0px;">
                <tr>
                    <td style="width: 100%">
                        <asp:TextBox ID="TxtJustificacion" runat="server" Width="95%" Style="margin-left: 2%; height: 35px;" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Modificación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vReportePlan" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnInforme5" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            OnClientClick="btnRegresarInforme_Click" Text="Regresar" OnClick="btnRegresarInforme_Click" />
                        &#160;&#160;
                        <asp:Button ID="BtnImprimir" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            Text="Imprimir" OnClick="btnImprimirInforme_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\Analisis\ReportAnalisisCredito.rdlc">
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

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
