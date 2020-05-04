<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cruce Cuentas :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlListaSaldos.ascx" TagName="ctlListaSaldos" TagPrefix="ctl1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="MvAfiliados" runat="server">
            <asp:View ID="vwTitular" runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwDatos" runat="server">
                <asp:Panel ID="panelDatos" runat="server">
                    <table>
                        <tr>
                            <td colspan="4" style="text-align: left;">
                                <strong>Datos de la Persona</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtIdRetiro" runat="server" CssClass="textbox" Width="20px" Visible="False" />
                                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Width="20px" Visible="False" />
                                Identificación<br />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="120px" />
                            </td>
                            <td style="text-align: left;">Tipo Identificación<br />
                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" Height="26px" CssClass="textbox"
                                    Width="180px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Nombres<br />
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="False" Width="300px" />
                            </td>
                            <td style="text-align: left;">Apellidos<br />
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Enabled="False" Width="300px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left;">
                                <hr style="width: 750px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>


                <table width="80%">
                    <tr>
                        <td colspan="4" style="text-align: left;">
                            <strong>Datos del Retiro </strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Fecha Retiro
                            <br />
                            <ucFecha:fecha ID="txtFecharetiro" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;">Motivo del Retiro<br />
                            <asp:DropDownList ID="DdlMotRetiro" runat="server" Width="300px" Height="26px" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;">Número Acta<br />
                            <asp:TextBox ID="txtActa" runat="server" Width="100px" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;">
                            <br />
                            <asp:CheckBox ID="cbTipoCruce" runat="server" Text="Cruzar hasta Donde Alcance"
                                AutoPostBack="True" OnCheckedChanged="cbTipoCruce_CheckedChanged" Checked="True" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left;" width="100%">Observaciones<br />
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" CssClass="textbox" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4" style="text-align: left;">Tipo Pago<br />
                            <asp:DropDownList ID="ddltipo_cruce" runat="server" AutoPostBack ="true" Width="300px" Height="26px" CssClass="textbox" OnSelectedIndexChanged ="ddltipo_cruce_CheckedChanged" >
                                <asp:ListItem Text="" Value="0" />
                                <asp:ListItem Text="Aplicar Moras- Prioridad" Value="1" />
                                <asp:ListItem Text="Aplicar Producto" Value="2" />
                                <asp:ListItem Text="Abono a Cuotas" Value="3" />
                            </asp:DropDownList>                   
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left;">
                            <hr style="width: 750px" />
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="panelGrilla" runat="server" Width="100%">
                    <table>
                        <tr>
                            <td colspan="3" style="text-align: left;">
                                <strong>Cruce de Cuentas </strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left;">
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="100%"
                                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDataBound="gvLista_RowDataBound"
                                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-Height="25px" ShowHeaderWhenEmpty="True" Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDistPagos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg" ToolTip="Dist Pagos" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numproducto" HeaderText="Num. Producto" />
                                        <asp:BoundField HeaderText="Linea Producto" DataField="linea_producto" />
                                        <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                        <asp:BoundField DataField="capital" HeaderText="Capital" DataFormatString="{0:C}" SortExpression="capital">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="interes" HeaderText="Int. Rendimiento" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="intmora" HeaderText="Int. Mora" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="otros" DataFormatString="{0:n0}" HeaderText="Otros">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="retencion" DataFormatString="{0:n0}" HeaderText="Retención">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="signo">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="interes_causado" HeaderText="Int. Causado" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="rentecioncausada" HeaderText="Ret. Causado" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_cruce" DataFormatString="{0:n0}" HeaderText="Saldo Pendiente">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" DataFormatString="{0:n0}" HeaderText="Total a Cruzar">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Codeudor" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Panel Width="70%" runat="server">
                                                    <ctl:ctlListarCodigo ID="ctlListarCodeudores" Visible="false" runat="server" />
                                                </asp:Panel>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" colspan="3">
                                <asp:Label ID="lblSaldo" runat="server" Text="Saldo"></asp:Label>&#160;&#160;&#160;&#160;
                                <asp:TextBox ID="txtsaldo" runat="server" Enabled="false" CssClass="textbox" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" colspan="3">Vr.Aseguradora&#160;(+)&#160;&#160;&#160;&#160;<asp:TextBox ID="txtAseguradora"
                                runat="server" Enabled="true" CssClass="textbox" AutoPostBack="True"
                                OnTextChanged="txtAseguradora_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" colspan="3">Total&#160;&#160;&#160;&#160;<asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="textbox" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="3">
                                <hr style="width: 750px" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>

                <table>
                    <tr>
                        <td style="text-align: left;">
                            <strong>Datos para el Giro</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <uc3:ctlgiro ID="ctlGiro" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="updDistribucion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <strong>
                                        <asp:CheckBox ID="chkDistribuir" runat="server" Text="Distribución de Giros" AutoPostBack="true"
                                            OnCheckedChanged="chkDistribuir_CheckedChanged" />
                                    </strong>
                                    <asp:Panel ID="panelDistribucion" runat="server">
                                        <div style="text-align: left">
                                            <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                                OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Detalle" Height="25px" /><br />
                                        </div>
                                        <asp:GridView ID="gvDistribucion" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                            HeaderStyle-Height="30px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                            ShowFooter="True" DataKeyNames="idgiro" ForeColor="Black" GridLines="Horizontal"
                                            Style="font-size: x-small" OnRowDeleting="gvDistribucion_RowDeleting" OnRowDataBound="gvDistribucion_RowDataBound">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Código" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("idgiro") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCod_persona" runat="server" Text='<%# Bind("cod_persona") %>' Visible="false" />
                                                        <cc1:TextBoxGrid ID="txtIdentificacionD" runat="server" Width="120px" CssClass="textbox"
                                                            CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" Text='<%# Bind("identificacion") %>'
                                                            OnTextChanged="txtIdentificacionD_TextChanged" />
                                                        <asp:FilteredTextBoxExtender ID="ftb120" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                            TargetControlID="txtIdentificacionD" ValidChars="-" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:TextBoxGrid ID="txtNombre" runat="server" Width="300px" CssClass="textbox" Text='<%# Bind("nombre") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipo" runat="server" Visible="false" Text='<%# Bind("tipo") %>' />
                                                        <cc1:DropDownListGrid ID="ddlTipo" runat="server" CssClass="textbox" Width="130px" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Total :
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <uc1:decimalesGridRow ID="txtValor" runat="server" Text='<%# Eval("valor") %>' style="text-align: right"
                                                            Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="100" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalVr" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="gridHeader" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                        <asp:Label ID="lblErrorDist" runat="server" Style="font-size: x-small; color: Red" />
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chkDistribuir" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarFila" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="gvDistribucion" EventName="RowDeleting" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                 <tr><td style="text-align:left;">
                        <ctl1:ctlListaSaldos ID="ListaSaldos" runat="server" />
                    </td>
                 </tr>
                </table>
            </asp:View>
            <asp:View ID="vwImpresion" runat="server">
                <br />
                <br />
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="25px" Width="130px"
                                Text="Visualizar Datos" OnClick="btnDatos_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                                Width="100%">
                                <LocalReport ReportPath="Page\Aportes\CruceCuentas\rptCruceCuenta.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltReport" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
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
                            <td style="text-align: center; font-size: large; color: Red">Cruce de cuentas grabado Correctamente
                                <br />
                                <br />
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
        </asp:MultiView>
    </asp:Panel>

    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>


    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
