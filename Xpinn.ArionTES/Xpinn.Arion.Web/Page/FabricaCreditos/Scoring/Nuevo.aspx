<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Fabrica de Creditos :." %>

<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js"></script>
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
    <br />
    <br />
    <asp:MultiView ActiveViewIndex="0" ID="mvPrincipal" runat="server">
        <asp:View runat="server">
            <asp:Panel runat="server" ID="pnlPrincipal">
                <div style="background-color: #0099FF; text-align: center; margin: 3% auto; height: 30px;">
                    <label style="color: #fff;">SCORE CREDITICIO</label>
                </div>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div style="width: 50%; display: block; float: left;">
                            <div style="width: 95%; margin: 0px auto; border: 1px solid #0099FF;">

                                <div style="width: 90%; margin: 1% auto;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <label style="width: 100%;">Código de nomina</label>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtCodigoNomina" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;" colspan="2">
                                                <table>
                                                    <tr>
                                                        <td style="width: 20%;">
                                                            <label>Tipo Ident.</label>
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <asp:TextBox Class="col-md-6" ReadOnly="true" Style="width: 90%; margin: 0px auto;" CssClass="textbox" ID="txtTipoIdentificacion" runat="server"></asp:TextBox>
                                                            <asp:TextBox Visible="false" ID="txtCodTipoIdentificacion" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <label>Identificación</label>
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <asp:TextBox Class="col-md-6" ReadOnly="true" Style="width: 90%; margin: 0px auto;" CssClass="textbox" ID="txtCedula" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Nombres:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="TxtNombres" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Apellidos:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtApellidos" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Código de afiliacion:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtCodigoAsociado" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Código de persona:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtCodPersona" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Aportes + Ah. Permanente:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtMontoAportes" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; margin-top: 5%;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Salario básico:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" onkeypress="return isNumber(event)" CssClass="textbox" ID="txtSalarioBasico" AutoPostBack="true" runat="server" OnTextChanged="txtSalarioBasico_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Salario Variable y/o Horas Extras:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtSalarioVariable" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Otros Ingresos:</label>
                                            </td>
                                            <td>

                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtOtrosIngresos" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 30%;">
                                                <label>Total Ingresos:</label>
                                            </td>
                                            <td style="width: 30%;">
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtTotalIngresos" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSalarioBasico" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <div style="width: 50%; display: block; float: left; margin: 0% 0px;">
                    <div style="width: 95%; margin: 0px auto; border: 1px solid #0099FF;">
                        <div style="width: 90%; margin: 1% auto;">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%;">
                                        <label>Empresa:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtEmpresa" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;">
                                        <label>Cargo:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtCargo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;">
                                        <label>Ubicación:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtUbicacion" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;">
                                        <label>Fecha de ingreso:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtFechaIngreso" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;">
                                        <label>Fecha de afiliación:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtFechaAfiliacion" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div style="width: 50%; display: block; float: left;">
                            <div style="width: 95%; margin: 0px auto; margin-top: 1%; border: 1px solid #0099FF;">
                                <div style="width: 90%; margin: 1% auto;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Aportes de Salud y Pensión:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtParafiscales" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Retención:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" CssClass="textbox" AutoPostBack="true" OnTextChanged="txtGrupoQueNecesitaCalcularDeducciones_TextChanged" onkeypress="return isNumber(event)" ID="txtRetencion" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Cuota aporte obligatorio:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtCuotaAporteObligatorio" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Servicios:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" onkeypress="return isNumber(event)" OnTextChanged="txtGrupoQueNecesitaCalcularDeducciones_TextChanged" AutoPostBack="true" CssClass="textbox" ID="txtServicios" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Ahorros voluntarios:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtAhorrosVoluntarios" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Pensiones voluntarias:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" CssClass="textbox" onkeypress="return isNumber(event)" OnTextChanged="txtGrupoQueNecesitaCalcularDeducciones_TextChanged" AutoPostBack="true" ID="txtPensionesVoluntarias" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Cuentas AFC:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" CssClass="textbox" onkeypress="return isNumber(event)" OnTextChanged="txtGrupoQueNecesitaCalcularDeducciones_TextChanged" AutoPostBack="true" ID="txtCuentasAFC" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <label>Total:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" ID="txtTotalDeducciones" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                    <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                        <table style="width: 100%;">
                            <tr style="text-align: center; color: #fff;">
                                <td>Análisis Información Crediticia</td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="col-md-12" style="margin: 2% 0px;">
                    <asp:UpdatePanel ID="upAnalisis" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table id="tblAnalisisInformacionCrediticia" runat="server" style="width: 100%; margin: 0px auto;">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvEstadoCuenta" runat="server" CellPadding="4"
                                            GridLines="None" Width="100%" AutoGenerateColumns="False" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxgv" runat="server" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Fecha Desembolso" DataFormatString="{0:d}" DataField="fecha_desembolso_nullable" />
                                                <asp:BoundField HeaderText="Linea" DataField="linea_credito" />
                                                <asp:BoundField HeaderText="No.Crédito" DataField="numero_radicacion" />
                                                <asp:TemplateField HeaderText="Valor Inicial">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorInicial" runat="server" Text='<%# Eval("monto_aprobado", "${0:#,##0.00}") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTotalValorInicialFooter" Style="text-align: right" Width="112px" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Saldo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSaldo" runat="server" Text='<%# Eval("saldo_capital", "${0:#,##0.00}") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTotalSaldoFooter" Width="112px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Intereses">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInt" runat="server" Text='<%# Eval("intcoriente", "${0:#,##0.00}") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTotalInteresesFooter" Width="112px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valor cuota">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorCuota" runat="server" Text='<%# Eval("valor_cuota", "${0:#,##0.00}") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTotalValorCuotaFooter" Width="112px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" />
                                            <RowStyle HorizontalAlign="Center" CssClass="gridItem" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>



                <div style="width: 100%; display: block; float: left;">
                    <div style="width: 100%; margin: 0px auto">
                        <div style="width: 100%; margin: 0px auto; text-align: left;">
                            <asp:ImageButton ImageUrl="~/Images/btnConsultar.jpg" ID="btnConsultarCifin" runat="server" OnClick="btnConsultarCifin_Click" />
                            <asp:TextBox runat="server" ID="txtValidarConsulta" Visible="false" />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvCifin" Visible="false" runat="server" CellPadding="3"
                                                    GridLines="None" Width="100%" AutoGenerateColumns="False" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Obligaciones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblObligaciones" Width="180" runat="server" Text='<%# Eval("PaqueteInformacion") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <label><b>Total Obligaciones</b></label>
                                                            </FooterTemplate>
                                                            <HeaderStyle Width="180" HorizontalAlign="Center" />
                                                            <ItemStyle Width="180" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre Entidad">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreEntidad" Width="180" runat="server" Text='<%# Eval("NombreEntidad") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="180" />
                                                            <ItemStyle Width="180" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Num. Obligacion">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNumeroObligacion" runat="server" Text='<%# Eval("NumeroObligacion") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="70" />
                                                            <ItemStyle Width="70" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reestruc.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReestrucutado" runat="server" Text='<%# Eval("Reestructurado") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="70" />
                                                            <ItemStyle Width="70" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Calidad">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCalidad" runat="server" Text='<%# Eval("Calidad") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="70" />
                                                            <ItemStyle Width="70" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estado">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstadoObligacion" runat="server" Text='<%# Eval("EstadoObligacion") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="70" />
                                                            <ItemStyle Width="70" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor Inicial">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValorInicial" runat="server" Text='<%# Eval("ValorInicial") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtValorInicial" Width="85px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                            </FooterTemplate>
                                                            <HeaderStyle Width="85" />
                                                            <ItemStyle Width="85" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Saldo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSaldo" runat="server" Text='<%# Eval("SaldoObligacion") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtSaldo" Width="85px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                            </FooterTemplate>
                                                            <HeaderStyle Width="85" />
                                                            <ItemStyle Width="85" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor Mora">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValorMora" runat="server" Text='<%# Eval("ValorMora") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtValorMora" Width="85px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                            </FooterTemplate>
                                                            <HeaderStyle Width="85" />
                                                            <ItemStyle Width="85" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cuota">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCuota" runat="server" Text='<%# Eval("ValorCuota") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtCuota" Width="85px" Style="text-align: right" runat="server" CssClass="textbox" Enabled='false'></asp:TextBox>
                                                            </FooterTemplate>
                                                            <HeaderStyle Width="85" />
                                                            <ItemStyle Width="85" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" AutoPostBack="true" ID="chkCifin" OnCheckedChanged="chkCifin_CheckedChanged" Checked="true" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40" />
                                                            <ItemStyle Width="40" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="gridHeader" />
                                                    <PagerStyle CssClass="gridHeader" />
                                                    <RowStyle HorizontalAlign="Center" CssClass="gridItem" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Panel runat="server" Visible="false" ID="pnlInformacionCifin">
                                                    <table style="width: 100%; margin-top: 15px; text-align: center">
                                                        <tr>
                                                            <td colspan="8">
                                                                <strong>Información Consulta</strong>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 13%">
                                                                <strong>Estado Persona</strong>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <asp:TextBox ID="txtEstadoPersona" Style="width: 100%; text-align: center; margin: 0px auto;" ReadOnly="true" Font-Bold="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 13%">
                                                                <strong>Fecha Consulta</strong>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <asp:TextBox ID="txtFechaConsulta" Style="width: 90%; text-align: center; margin: 0px auto;" ReadOnly="true" Font-Bold="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 13%">
                                                                <strong>Puntaje Score</strong>
                                                            </td>
                                                            <td style="width: 6%">
                                                                <asp:TextBox ID="txtPuntajeScore" Style="width: 90%; text-align: center; margin: 0px auto;" ReadOnly="true" Font-Bold="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 13%">
                                                                <strong>Prob Mora (%)</strong>
                                                            </td>
                                                            <td style="width: 6%">
                                                                <asp:TextBox ID="txtProbMora" Style="width: 90%; text-align: center; margin: 0px auto;" ReadOnly="true" Font-Bold="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <table style="width: 100%; margin-top: 15px; text-align: center">
                                                    <tr>
                                                        <td colspan="7">
                                                            <strong>Total Resumen Cartera</strong>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%"></td>
                                                        <td style="width: 15%">
                                                            <strong>Total Cuota</strong>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox ID="txtTotalCuota" Style="width: 90%; text-align: right; margin: 0px auto;" ReadOnly="true" Font-Bold="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <strong>Total Saldo</strong>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox ID="txtTotalSaldo" Style="width: 90%; text-align: right; margin: 0px auto;" ReadOnly="true" Font-Bold="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%"></td>
                                                        <td style="width: 15%"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-md-12" style="margin-top: 2%;">
                            <div style="width: 97%; margin: 1px auto;">
                                <div style="width: 100%; margin: 1px auto;">
                                    <table style="width: 90%; margin: 0px auto;">
                                        <tr>
                                            <td style="width: 100%; text-align: center;">
                                                <label style="text-align: center;">Observaciones:</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: center;">
                                                <asp:TextBox Style="width: 90%; margin: 0px auto; height: 30px;" TextMode="Multiline" ID="txtObservaciones" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="width: 50%; display: block; float: left; margin-top: 3%;">
                                    <div style="width: 95%; margin: 0px auto; border: 1px solid #0099FF;">
                                        <div style="width: 90%; margin: 1% auto;">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Línea de crédito:</label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLineaCredito" Style="width: 92%; margin: 0px auto;" CssClass="textbox" AutoPostBack="true"
                                                            runat="server" OnSelectedIndexChanged="ddlLineaCredito_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;" colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 40%;">
                                                                    <label>Ingresos </label>
                                                                    <br />
                                                                    <label>(% castigo línea): </label>
                                                                </td>
                                                                <td style="width: 35%;">
                                                                    <asp:TextBox ID="txtIngresoCalculoCastigo" Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <label>%</label>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:TextBox Class="col-md-6" ReadOnly="true" Style="width: 90%; margin: 0px auto;" CssClass="textbox" ID="txtPorcentajeCastigo" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Monto solicitado:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMontoSolicitado" Style="width: 90%; margin: 0px auto;" CssClass="textbox" runat="server" onkeypress="return isNumber(event)" AutoPostBack="true" OnTextChanged="txtGrupoQueNecesitaCalcularCuota_TextChanged"></asp:TextBox>
                                                        <asp:Label ID="lblMensajeMonto" runat="server" Font-Size="XX-Small" Visible="false" Text="Monto solicitado no puede superar el monto maximo" Style="color: #FF3300;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Plazo:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPlazo" Style="width: 90%; margin: 0px auto;" AutoPostBack="true" CssClass="textbox" runat="server" onkeypress="return isNumber(event)" OnTextChanged="txtGrupoQueNecesitaCalcularCuota_TextChanged"></asp:TextBox>
                                                        <asp:Label ID="lblMensajePlazo" runat="server" Font-Size="XX-Small" Visible="false" Text="Plazo solicitado no puede superar al plazo maximo" Style="color: #FF3300;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Periodicidad:</label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPeriodicidad" Style="width: 92%; margin: 0px auto;" CssClass="textbox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Tasa:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTasa" Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Tipo Tasa:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTipoTasa" Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtTipoLiquidacion" Visible="false" Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Cuota aproximada:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCuotaMensualAprox" Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Fecha Primer Pago:</label>
                                                    </td>
                                                    <td>
                                                        <ucFecha:fecha ID="FechaPrimPag" runat="server" style="width: 80%; margin: 0px auto;" AutoPostBack="true" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>
                                    <div style="width: 95%; margin: 0px auto; border: 1px solid #0099FF;">
                                        <div style="width: 90%; margin: 1% auto;">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <label>Capacidad mensual:</label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txtCapacidadMensualScore" ReadOnly="true" Style="width: 90%; margin: 0px auto;" CssClass="textbox" runat="server"></asp:TextBox>
                                                    </td>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>
                                                                % Salario Comprometido:</label>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtPorcentajeSalarioComprometido" runat="server" CssClass="textbox" ReadOnly="true" Style="width: 90%; margin: 0px auto;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>
                                                                Compromisos Extras:</label>
                                                        </td>


                                                        <td colspan="4">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 25%;">
                                                                        <asp:TextBox ID="Primas" runat="server" CssClass="textbox" MaxLength="2" onkeypress="return isNumber(event)" Style="width: 80%; margin: 0px auto;"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <label>
                                                                            Primas</label>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <asp:TextBox ID="Cesantias" runat="server" CssClass="textbox" MaxLength="2" onkeypress="return isNumber(event)" Style="width: 80%; margin: 0px auto;"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <label>Cesantias</label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>Valor Extra prima:</label>
                                                        </td>

                                                        <td colspan="4">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 33.3%;">
                                                                        <asp:TextBox ID="ValorPrima" runat="server" CssClass="textbox" OnTextChanged="Formatear_TextChanged" AutoPostBack="true" onkeypress="return isNumber(event)" Style="width: 80%; margin: 0px auto;"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 33.3%;">
                                                                        <label>Fecha:</label>
                                                                    </td>
                                                                    <td style="width: 33.3%;">
                                                                        <ucFecha:fecha ID="txtFechaPrima" runat="server" Width="80%" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>
                                                                Valor Extra Cesantias:</label>
                                                        </td>

                                                        <td colspan="4">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 33.3%;">
                                                                        <asp:TextBox ID="ValorCesantias" runat="server" CssClass="textbox" OnTextChanged="Formatear_TextChanged" AutoPostBack="true" onkeypress="return isNumber(event)" Style="width: 80%; margin: 0px auto;"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 33.3%;">
                                                                        <label>
                                                                            Fecha:</label>
                                                                    </td>
                                                                    <td style="width: 33.3%;">
                                                                        <ucFecha:fecha ID="txtFechaCesan" runat="server" Width="80%" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>
                                                                Valor cuota con compromiso:</label>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtValorCuotaCompromiso" runat="server" CssClass="textbox" OnTextChanged="Formatear_TextChanged" AutoPostBack="true" onkeypress="return isNumber(event)" Style="width: 90%; margin: 0px auto;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>
                                                                Plazo Maximo:</label>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtPlazoMaximo" runat="server" CssClass="textbox" ReadOnly="true" Style="width: 90%; margin: 0px auto;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <label>
                                                                Máximo a prestar:</label>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtMaximoAPrestar" runat="server" CssClass="textbox" ReadOnly="true" Style="width: 90%; margin: 0px auto;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div style="width: 50%; display: block; float: left; margin-top: 3%;">
                                <div style="width: 95%; margin: 0px auto; border: 1px solid #0099FF;">
                                    <div style="width: 90%; margin: 1% auto;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 100%; text-align: center; color: #fff; background-color: #0099FF;">
                                                    <label>ALERTA</label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <label>Máximo con codeudor:</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaximoCodeudor" Style="width: 90%; margin: 0px auto;" ReadOnly="true" CssClass="textbox" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <label>Antigüedad De 3-5 Años</label>
                                                </td>
                                                <td style="width: 50%;">
                                                    <asp:Label ID="lblAntiguedad" Font-Bold="true" ForeColor="Red" ReadOnly="true" Style="text-align: center; margin: 0px auto; margin-top: 30%;" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <label>Máximo sin codeudor:</label>
                                                </td>
                                                <td style="width: 50%;">
                                                    <asp:TextBox ID="txtMaximoSinCodeudor" ReadOnly="true" CssClass="textbox" Style="width: 90%; margin: 0px auto;" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <label>Antigüedad 5+ Años</label>
                                                </td>
                                                <td style="width: 50%;">
                                                    <asp:Label ID="lblAntiguedad5" Font-Bold="true" ForeColor="Red" ReadOnly="true" Style="text-align: center; margin: 0px auto; margin-top: 30%;" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <label>Máximo sin codeudor:</label>
                                                </td>
                                                <td style="width: 50%;">
                                                    <asp:TextBox ID="txtMaximoSinCodeudor5" ReadOnly="true" CssClass="textbox" Style="width: 90%; margin: 0px auto;" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; margin-top: 2%;">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <label>Tipo de garantía:</label>
                                                </td>
                                                <td style="width: 50%;">
                                                    <asp:Label ID="lblTipoGarantia" Visible="true" Font-Bold="true" ForeColor="Red" ReadOnly="true" Style="text-align: center; margin: 0px auto; margin-top: 30%;" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 100%; text-align: center;">
                                                    <label>Estado</label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="border: 2px solid #0099FF; height: 80px;">
                                            <div style="height: 40%"></div>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblViable" Font-Bold="true" ForeColor="Red" Style="text-align: center; margin: 0px auto; margin-top: 30%;" runat="server"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtMontoSolicitado" EventName="TextChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="txtPlazo" EventName="TextChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="txtSalarioBasico" EventName="TextChanged" />

                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <label class="col-md-6"></label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtMontoSolicitado" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtPlazo" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtSalarioBasico" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>

                        <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                            <div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center" colspan="2">
                                            <asp:Button ID="btnCupos" runat="server" CssClass="btn8" OnClick="btnConsultarCupos_Click" TabIndex="12"
                                                OnClientClick="btnConsultarCupos_Click" Text="Calcular Cupos" Height="22px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div style="width: 100%; display: block; float: left; margin-top: 2%;">
                            <div style="width: 100%; margin: 0px auto; background-color: #0099FF;">
                                <table style="width: 100%;" id="TituloCupos" runat="server">
                                    <tr style="text-align: center; color: #fff;">
                                        <td>Cupos por línea de créditos</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin: 2% 0px;">
                            <table id="tbCuposLineas" runat="server" style="width: 100%; margin: 0px auto;">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvCreditos" runat="server" AllowPaging="true" PageSize="5" Font-Size="Small"
                                            GridLines="None" Width="100%" AutoGenerateColumns="False" ShowFooter="true" OnPageIndexChanging="gvCreditos_PageIndexChanging"
                                            OnRowDataBound="gvCreditos_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Línea de Crédito">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinea_credito" Text='<%#Eval("cod_linea_credito")%>' runat="server"
                                                            Width="30px" />
                                                        <asp:Label ID="lbnomlinea_credito" Text='<%#Eval("nom_linea_credito")%>'
                                                            runat="server" Width="150px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Monto Máximo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbmontomaximo" Text='<%# String.Format("{0:N}", Eval("monto_maximo")) %>'
                                                            runat="server" Width="90px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Plazo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Plazo" Text='<%# String.Format("{0:N0}", Eval("plazo")) %>' runat="server"
                                                            Width="60px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Cupo Dispónible">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbmonto" Text='<%# String.Format("{0:N}", Eval("monto")) %>' runat="server"
                                                            Width="90px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Cuota Estimada">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cuota" Text='<%# String.Format("{0:N}", Eval("cuota_credito")) %>'
                                                            runat="server" Width="80px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Tasa Int.Cte.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Tasa" Text='<%# String.Format("{0:N}", Eval("tasa")) %>' runat="server"
                                                            Width="60px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Reciprocidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblreciprocidad" Text='<%# Eval("reciprocidad") %>' runat="server"
                                                            Width="30px" Visible="false" /><asp:CheckBox ID="cbreciprocidad" runat="server" Enabled="false"
                                                                Checked='<%#Convert.ToBoolean(Eval("reciprocidad")) %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Refinanciar">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefinancia" Text='<%# Eval("check") %>' runat="server" Width="30px"
                                                            Visible="false" /><asp:CheckBox ID="cbrefinancia" runat="server" Enabled="false"
                                                                Checked='<%#Convert.ToBoolean(Eval("check")) %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Saldo Actual">
                                                    <ItemTemplate>
                                                        <asp:Label ID="saldo" Text='<%# String.Format("{0:N}", Eval("saldo_capital")) %>'
                                                            runat="server" Width="90px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" />
                                            <RowStyle HorizontalAlign="Center" CssClass="gridItem" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vReportePlan" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            Text="Imprimir" OnClick="btnImprimir_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewerPlan" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\Scoring\ReportScoring.rdlc">
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
</asp:Content>
