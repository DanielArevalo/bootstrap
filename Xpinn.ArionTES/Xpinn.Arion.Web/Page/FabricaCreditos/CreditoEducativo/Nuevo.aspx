<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="Numero" TagPrefix="ucn" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="ddlFormaPago" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvReestructuras" runat="server" ActiveViewIndex="0">
        <asp:View ID="mvSegunda" runat="server">
            <table style="width: 100%" id="tbInforma">
                <tr>
                    <td style="margin-left: 40px; text-align: left" colspan="3">
                        <strong>Datos del Deudor:</strong>
                    </td>
                    <td style="margin-left: 40px;">
                        &nbsp;
                    </td>
                    <td style="margin-left: 40px;">
                        &nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="text-align: left;" width="15%">
                        Identificacion:
                        <br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="95%"
                            Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left" width="10%">
                        Tipo.Iden
                        <br />
                        <asp:DropDownList ID="txtTipIdenT" runat="server" CssClass="textbox" Width="95%" Enabled="false"></asp:DropDownList>
                    </td>
                    <td style="text-align: left" width="25%">
                        Nombre:
                        <br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="95%" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left" width="25%">
                        Apellido:
                        <br />
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="textbox" Width="95%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="text-align: left" colspan="3" width="50%">
                        Direccion:
                        <br />
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Width="95%" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        Telefono:
                        <br />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="95%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <hr width="100%" />
            <table>
                <tr style="text-align: left">
                    <td style="text-align: left" width="20%">
                        Fecha Solicitud:
                        <br />
                        <uc1:fecha ID="ctlFecha" runat="server" width="95%" />
                    </td>
                    <td style="text-align: left" width="40%">
                        Universidad/Institucion Educativa:
                        <br />
                        <asp:TextBox ID="txtUniversidad" runat="server" CssClass="textbox" width="95%"></asp:TextBox>
                    </td>
                    <td style="text-align: left" width="20%">
                        Semestre/Curso:
                        <br />
                        <asp:TextBox ID="txtSemestre" runat="server" CssClass="textbox" width="95%"></asp:TextBox>
                    </td>
                    <td style="text-align: left" width="20%">
                        Valor Matrícula:
                        <br />
                        <ucn:Numero ID="ctlNumeroMatricul" runat="server" width="95%"/>
                    </td>
                </tr>
            </table>
            <hr width="100%" />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="margin-left: 40px; text-align: left" colspan="5">
                                <strong>Datos del Credito:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" width="25%">
                                Línea Crédito:
                                <br />
                                <asp:DropDownList ID="ddlLineaCredito" OnSelectedIndexChanged="ddlLineaCredito_SelectedIndexChanged"
                                    AutoPostBack="true" runat="server" CssClass="textbox" Width="95%">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left" width="15%">
                                Monto Máximo:
                                <br />
                                <ucn:Numero ID="txtMontoMaximo" Habilitado="false" width="95%" runat="server" />
                            </td>
                            <td style="text-align: left" width="22%">
                                Plazo Máximo:
                                <br />
                                <asp:TextBox ID="txtPlazoMaximo" runat="server" Enabled="false" CssClass="textbox"
                                    Width="130px"></asp:TextBox>
                            </td>
                            <td style="text-align: left" width="23%">
                                &nbsp;
                            </td>
                            <td style="text-align: left" width="15%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" width="25%">
                                Valor del Crédito:
                                <br />
                                <ucn:Numero ID="ctlCredito" Habilitado="false" runat="server" width="140px" />
                            </td>
                            <td style="text-align: left" width="15%">
                                Plazo:
                                <br />
                                <ucn:Numero ID="ddlPlazo" runat="server" Enabled="true" TextMode="Number" MinimumValue="1"
                                    CssClass="textbox" width="95%" />
                            </td>
                            <td style="text-align: left" width="22%">
                                Periodicidad:
                                <br />
                                <ctl:ddlPeriodicidad ID="ddlperiodicidad" runat="server" Width="95%" />
                            </td>
                            <td style="text-align: left" width="23%">
                                Fecha Primer Pago:
                                <br />
                                <uc1:fecha ID="ctlFechaPPago" Width="140px" runat="server" />
                            </td>
                            <td style="text-align: left" width="15%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="2">
                                Asesor Comercial:
                                <br />
                                <asp:DropDownList ID="ddlAsesorComercial" runat="server" CssClass="textbox" Width="95%">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left" width="22%">
                                Forma de Pago:
                                <br />
                                <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Width="95%">
                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left" width="23%">
                                <asp:Label ID="lblPagaduri" runat="server" Text="Pagaduria:"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="95%" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"
                                                 AutoPostBack="true" />
                            </td>
                            <td style="text-align: left" width="15%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelAuxilio" runat="server">
                <table>
                    <tr>
                        <td style="margin-left: 40px; text-align: left" colspan="3">
                            <strong>Datos del Auxilio:</strong>
                        </td>
                        <td style="margin-left: 40px;">
                            &nbsp;
                        </td>
                        <td style="margin-left: 40px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Linea Auxilio:
                            <br />
                            <asp:DropDownList ID="ddlLineaAuxilio" runat="server" CssClass="textbox" Width="160px"
                                OnSelectedIndexChanged="ddlLineaAuxilio_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            Valor Del Auxilio:
                            <br />
                            <ucn:Numero ID="txtValorAux" width="145px" runat="server" Habilitado="false" />
                        </td>
                        <td style="text-align: left">
                            Observaciones:
                            <br />
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Width="450px"></asp:TextBox>
                        </td>
                    </tr>                    
                </table>
            </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlLineaCredito" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlLineaAuxilio" EventName="SelectedIndexChanged" />                    
                </Triggers>
            </asp:UpdatePanel>

            <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
           
            <table>
                <tr>
                    <td style="text-align: left">
                        <strong>Beneficiarios</strong><br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                    OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Beneficiario" Height="22px" />
                                <asp:GridView ID="gvBeneficiarios" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                    margin-bottom: 0px;" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                                    DataKeyNames="codbeneficiarioaux" GridLines="Horizontal">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" CssClass="textbox" Text='<%# Bind("codbeneficiarioaux") %>' /></ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtidenti_grid" runat="server" CssClass="textbox" Text='<%# bind("identificacion") %>'
                                                    Width="110px"></cc1:TextBoxGrid></ItemTemplate>
                                            <%--OnTextChanged="txtidentificacion_TextChanged"--%>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtNombreComple" runat="server" CssClass="textbox" Text='<%# Bind("nombre") %>'
                                                    Width="250px" /></ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parentesco">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("cod_parentesco") %>'
                                                    Visible="false" /><cc1:DropDownListGrid ID="ddlParentesco" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" Width="170px">
                                                    </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%Beneficiario">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtPorcBene" runat="server" CssClass="textbox" Style="text-align: right"
                                                    Text='<%# Bind("porcentaje_beneficiario") %>' Width="100px" /><asp:FilteredTextBoxExtender
                                                        ID="fte3" runat="server" FilterType="Custom, Numbers" TargetControlID="txtPorcBene"
                                                        ValidChars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Codeudores</strong>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanelCodeudores" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvCodeudores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" PageSize="5"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                                    OnRowCommand="gvCodeudores_RowCommand" OnRowDeleting="gvCodeudores_RowDeleting"
                                    Style="font-size: x-small">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                                    ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" Height="16px" />
                                            </FooterTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                            <ItemStyle Width="16px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Codpersona" />
                                        <asp:TemplateField HeaderText="Identificación">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'
                                                    OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" AutoPostBack="True"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer Nombre" />
                                        <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo Nombre" />
                                        <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer Apellido" />
                                        <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo Apellido" />
                                        <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
                                        <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                                <div style="text-align: center">
                                    <asp:Label ID="lblTotReg" runat="server" Visible="False" />
                                    <asp:Label ID="lblTotalRegsCodeudores" runat="server" Text="No hay codeudores para este crédito."
                                        Visible="False" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:View >       
        <asp:View ID="Mensaje" runat="server">
            <br /><br /><br /><br />
            <table width="100%">
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="lblMensajeFin" runat="server" Text="" style="font-size:medium"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <asp:Button ID="btnFinal" runat="server" Text="Finalizar" OnClick="btnFinalClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPlanPagos" runat="server" Text="Ir a Plan de Pagos" OnClick="btnPlanPagosClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnAprobacion" runat="server" Text="ir a Aprobación" OnClick="btnAprobacionClick" />
                    </td>
                </tr>
            </table>                                   
        </asp:View>
    </asp:MultiView>
    <uc1:validarBiometria ID="ctlValidarBiometria" runat="server" />

    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
     <script type='text/javascript'>
         function Forzar() {
             __doPostBack('', '');
         }
    </script>

</asp:Content>
