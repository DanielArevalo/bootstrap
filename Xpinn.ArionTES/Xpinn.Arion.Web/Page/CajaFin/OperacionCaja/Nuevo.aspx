<%@ Page Title=".: Registro de Operaciones :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlAvances.ascx" TagName="avances" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script src="../../../Scripts/PCLBryan.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                    <strong>Registro de Operaciones de Caja</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 60px; text-align: left;">
                                    <span style="font-size: x-small">Oficina</span>&nbsp;
                                </td>
                                <td class="logo" style="width: 181px">
                                    <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False" Width="151px"></asp:TextBox>
                                </td>
                                <td style="width: 50px; font-size: x-small;">
                                    Caja
                                </td>
                                <td class="logo" style="width: 186px">
                                    <asp:TextBox ID="txtCaja" runat="server" CssClass="textbox" Enabled="False" Width="185px"></asp:TextBox>
                                </td>
                                <td style="width: 60px; font-size: x-small;">
                                    Cajero
                                </td>
                                <td style="width: 113px">
                                    <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" Enabled="False" Width="173px"></asp:TextBox>
                                </td>
                                <td style="font-size: x-small; width: 150px; text-align: right">
                                    Fecha y Hora de Transacción
                                </td>
                                <td style="margin-left: 240px">
                                    <asp:TextBox ID="txtFechaTransaccion" runat="server" CssClass="textbox" Enabled="false"
                                        MaxLength="10" Width="132px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDia" runat="server" CssClass="textbox" Enabled="False" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />

    <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <table cellpadding="5" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="text-align: left">                                    
                                    <asp:RadioButtonList ID="rblOpcRegistro" runat="server" RepeatDirection="Horizontal"
                                        Width="300px" AutoPostBack="true" OnSelectedIndexChanged="rblOpcRegistro_SelectedIndexChanged">                                        
                                        <asp:ListItem Selected="True" Text="Cooperativa" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Convenios" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:ImageButton ID="ImgBntCod" ImageUrl="~/Images/codbarras.jpg" Height="20" Width="100" runat="server" OnClick ="ImgBntCod_Click" />
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="BntVer" runat="server" OnClick="btnConsultarEstado_Click" Text="Estado Cuenta" />
                                </td>
                            </tr>
                        </table>
                </tr>
                <tr>
                    <asp:Panel ID="PnlCod" runat="server" Width="450px" Style="cursor: move" Visible ="false">
                            <table>
                                <tr>
                                    <td colspan="2" style="font-size: x-small; color: #FFFFFF;  width: 434px">
                                        <asp:Panel ID="PanelCodBarras2" runat="server" Width="450px" Style="cursor: move">
                                            <strong>Lectura Codigo de Barras</strong>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCodBarras" runat="server" Width="350" OnTextChanged="txtCodBarras_KeyUp" />
                                        <asp:Button ID="BtnValidar" runat="server" Text="Validar" CssClass="button" CausesValidation="False" Height="30" Width="80" OnClick="btnValidar_Click" Style="margin: 0 auto" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel></tr>
                <tr>
                    <td style="text-align: left">
                        <strong>
                            <asp:Label ID="lblTitulo" runat="server" /><br />
                            <asp:TextBox ID="txttransacciondia" runat="server" CssClass="textbox" Enabled="false"
                                MaxLength="10" Width="132px" Visible="False"></asp:TextBox>
                        </strong>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelCooperativa" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 250px; text-align: left">
                            <asp:Label ID="lblTipoConvenio" runat="server" Text="Convenio" />
                        </td>
                        <td colspan="3" style="text-align: left; width: 250px">
                            <asp:DropDownList ID="ddlTipoConvenio" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="ddlTipoConvenio_SelectedIndexChanged" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; text-align: left">Tipo de Producto
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged" Width="182px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px; text-align: left">Tipo Movimiento
                        </td>
                        <td style="width: 260px; text-align: left">
                            <asp:DropDownList ID="ddlTipoMovimiento" runat="server" CssClass="textbox"
                                Width="160px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlTipoMovimiento_SelectedIndexChanged">
                                <asp:ListItem Value="2">INGRESO</asp:ListItem>
                                <asp:ListItem Value="1">EGRESO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 631px; text-align: left">&nbsp;
                        </td>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; text-align: left">Tipo Identificación
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Height="27px"
                                Width="182px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px; text-align: left">Identificación
                        </td>
                        <td style="width: 260px; text-align: left">
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px"
                                MaxLength="12"></asp:TextBox>
                            <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                                OnClick="btnConsultaPersonas_Click" />
                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />


                        </td>
                        <td style="width: 631px; text-align: left">
                            <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar" />
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCodCliente" runat="server" CssClass="textbox"
                                Enabled="False" MaxLength="12" Visible="False" Width="10px"></asp:TextBox>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 250px; text-align: left">Nombres y Apellidos
                        </td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false"
                                Width="542px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" colspan="4">
                            <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" colspan="4">
                            <asp:TextBox ID="txtObservaciones" runat="server" Width="90%" Style="height: 35px;" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="panelGrids" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <div id="divDatos" runat="server" style="overflow: scroll; height: 200px">
                                    <asp:GridView ID="gvConsultaDatos" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="50" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        OnRowCommand="gvConsultaDatos_RowCommand" Style="font-size: x-small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_info.jpg"
                                                        ToolTip="Estado Cuenta" CommandName='<%#Eval("numero_radicacion")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="numero_radicacion" HeaderText="Radicado" />
                                            <asp:BoundField DataField="linea_credito" HeaderText="Línea Crédito" />
                                            <asp:BoundField DataField="Dias_mora" HeaderText="Dias de Mora" />
                                            <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="garantias_comunitarias" HeaderText="G.Comunitaria" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proxima_pago" HeaderText="Fec. Próx.Pago" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="valor_a_pagar" HeaderText="Valor a Pagar" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_CE" HeaderText="Valor C.Extras"
                                                DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="total_a_pagar" HeaderText="Valor Total a Pagar" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                    <%--AFILIACION--%>
                                    <asp:GridView ID="gvDatosAfiliacion" runat="server" Width="99%" AutoGenerateColumns="False"
                                        PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="idafiliacion"
                                        Style="font-size: x-small" OnRowEditing="gvDatosAfiliacion_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                            <asp:BoundField DataField="idafiliacion" HeaderText="Código" />
                                            <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fecha Afiliación" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="valor" HeaderText="Valor Afiliación" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Periodicidad" />
                                            <%--<asp:BoundField DataField="saldo" HeaderText="Saldo Afiliación" 
                                                DataFormatString="{0:n}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>   --%>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>

                                    <asp:GridView ID="gvAhorroVista" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        Style="font-size: x-small" DataKeyNames="numero_cuenta" OnRowEditing="gvAhorroVista_RowEditing">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                            <asp:BoundField DataField="numero_cuenta" HeaderText="Num Producto">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea" HeaderText="Línea">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fec Próx Pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_formapago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_canje" HeaderText="Saldo Canje" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Vr. Cuota" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>

                                    <%--SERVICIOS--%>
                                    <asp:GridView ID="gvServicios" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                        AllowPaging="false" OnRowEditing="gvServicios_RowEditing"
                                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio" Style="font-size: x-small">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                                            <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="F. 1erCuota" DataField="fecha_primera_cuota" DataFormatString="{0:d}" />
                                            <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                            <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                            <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                            <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:n0}" />
                                            <asp:BoundField DataField="total_calculado" HeaderText="Valor a Pagar" DataFormatString="{0:n0}" />
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>

                                    <asp:GridView ID="gvProgramado" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        Style="font-size: x-small" DataKeyNames="numero_programado" OnRowEditing="gvProgramado_RowEditing">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                            <asp:BoundField DataField="numero_programado" HeaderText="Num Producto">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult Mov" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomforma_pago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_total" HeaderText="Valor Total a Pagar" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>

                                    <asp:GridView ID="gvCdat" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        Style="font-size: x-small" DataKeyNames="codigo_cdat" OnRowEditing="gvCdat_RowEditing">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                            <asp:BoundField DataField="codigo_cdat" HeaderText="Num Producto">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="numero_cdat" HeaderText="Num Cdat">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha de Emisión" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="modalidad" HeaderText="Modalidad">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="retencion" HeaderText="Cobra Interés"></asp:BoundField>
                                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>

                                               <asp:BoundField DataField="valor_parcial" HeaderText="Valor_Parcial" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>

                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                    <asp:GridView ID="gvGiros" runat="server" AutoGenerateColumns="False" Width="100%"
                                        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        OnRowEditing="gvGiros_RowEditing" DataKeyNames="idgiro" Style="font-size: x-small" OnPageIndexChanging="gvGiros_PageIndexChanging">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                            <asp:BoundField DataField="idgiro" HeaderText="Num Giro">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fec_reg" HeaderText="Fecha Registro" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="num_comp" HeaderText="No.Comp">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_tipo_comp" HeaderText="Tipo Comp" HeaderStyle-Width="120px">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_forma_pago" HeaderText="Forma Pago" HeaderStyle-Width="80px">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_generado" HeaderText="Generado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado" HeaderStyle-Width="120px">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Distribución">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkDistribuir" runat="server" Style="text-align: right" Checked='<%# Convert.ToBoolean(Eval("distribuir")) %>'
                                                        Enabled="false" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                                <ItemStyle CssClass="gridIco"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="identif_bene" HeaderText="Identific. Beneficiario">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="nombre_bene" HeaderText="Nombre Beneficiario">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                    <%--TOTAL ADEUDADO--%>
                                    <asp:GridView ID="gvtotal" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        OnPageIndexChanging="gvLista_PageIndexChanging"
                                        OnRowCommand="gvLista_RowCommand" Style="font-size: x-small"
                                        OnSelectedIndexChanged="gvConsultaDatos_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxgv" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto" />
                                            <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Producto" />
                                            <asp:BoundField DataField="linea_credito" HeaderText="Línea Producto" />
                                            <asp:BoundField DataField="Dias_mora" HeaderText="Días de Mora" />
                                            <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Apertura" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_capital" HeaderText="Saldo" DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proxima_pago" HeaderText="Fec. Próx. Pago" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="total_a_pagar" HeaderText="Total a Pagar a la fecha"
                                                DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_CE" HeaderText="Vr.Cuotas Extras"
                                                DataFormatString="{0:n0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </div>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelTransaccion" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-size: small;" colspan="6">
                                <strong>Datos de la Transacción</strong>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px; font-size: x-small;">Número de Producto
                            </td>
                            <td style="width: 145px; font-size: x-small;">Tipo de Pago
                            </td>
                            <%--<td style="width: 145px; font-size: x-small;">--%>
                            <%--<asp:Label ID="lblTipoValorTransaccion" runat="server" Text="Valor de la Transacción"></asp:Label>--%>
                            <%--</td>--%>
                            <td style="width: 160px; font-size: x-small;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNumeroCuotas" runat="server" Text="#Cuo" Visible="false" /></td>
                                        <td>
                                            <asp:Label ID="lblTipoValorTransaccion" runat="server" Text="Valor de la Transacción"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 140px; font-size: x-small;">Moneda
                            </td>
                            <td style="width: 145px; font-size: x-small;">
                                <asp:Label ID="LblReferencia" runat="server" Text="Referencia" Visible="False"></asp:Label>
                            </td>
                            <td style="width: 140px; font-size: x-small;">
                                <asp:Label ID="LblIdAvances" runat="server" Text="Id.Avances" Visible="False" /></td>
                            <td style="width: 35px">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtNumProducto" Enabled="false" runat="server" AutoPostBack="True" CssClass="textbox"
                                    MaxLength="12" OnTextChanged="txtNumProducto_TextChanged" Width="134px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlTipoPago" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="ddlTipoPago_SelectedIndexChanged" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                <%--<uc1:decimales ID="txtValTransac" runat="server" CssClass="textbox" MaxLength="17"
                                    style="text-align: right" Width="150px" />--%>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNumCuotas" runat="server" AutoPostBack="True" Visible="false"
                                                CssClass="textbox" MaxLength="3" OnTextChanged="txtNumCuotas_TextChanged" Width="20px" />
                                            <asp:FilteredTextBoxExtender ID="txtNumCuotas_FilteredTextBoxExtender"
                                                runat="server" Enabled="True" FilterType="Custom" TargetControlID="txtNumCuotas"
                                                ValidChars="0123456789" />
                                        </td>
                                        <td>
                                            <uc1:decimales ID="txtValTransac" runat="server" CssClass="textbox" MaxLength="17"
                                                style="text-align: right" Width="150px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlMonedas" runat="server" CssClass="textbox" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtReferencia" runat="server" AutoPostBack="True" CssClass="textbox"
                                    MaxLength="12" Visible="False" Width="134px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Panel ID="upAvances" runat="server" Visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAvances" CssClass="textbox" runat="server" Width="100%" ReadOnly="True" Style="text-align: left" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAvances" CssClass="btn8" runat="server" Text="..." Height="26px" Width="100%" OnClick="btnConsultaAvances_Click" />
                                                <uc3:avances ID="listadoavances" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width: 35px">
                                <asp:Button ID="btnGoTran" runat="server" OnClick="btnGoTran_Click" Text="&gt;&gt;" />
                            </td>
                            <td style="width: 35px">
                                <asp:Button ID="btnTotalTran" runat="server" OnClick="Cargar_trans" Visible="false"
                                    Text="Pago Total &gt;&gt;" Width="99px" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkMora" runat="server" Visible="false"
                                    Text="Aplicar por Mora" Width="99px" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="lblComentario" runat="server" Text="Estado Crédito" />
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtlinea" runat="server" CssClass="textbox" MaxLength="12" Visible="False"
                                    Width="16px"></asp:TextBox>
                                <asp:TextBox ID="txtlinea2" runat="server" CssClass="textbox" MaxLength="12" Visible="False"
                                    Width="16px"></asp:TextBox>
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="false" ForeColor="Red"
                                    Width="640px"></asp:TextBox>
                            </td>
                            <td style="width: 35px">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblMsgNroProducto" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="Lblerror" runat="server" Style="color: #FF0000; font-weight: 700"
                                    Text=""></asp:Label>
                            </td>
                            <td style="width: 35px">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelGridTran" runat="server" BorderColor="#3333FF">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    <strong>Transacciones a Aplicar</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvTransacciones" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="50" Width="91%"
                                        OnRowDeleting="gvTransacciones_RowDeleting" OnRowCommand="gvTransacciones_RowCommand"
                                        Style="font-size: x-small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDistPagos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg" ToolTip="Dist Pagos" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipo" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tproducto" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipomov" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomtipo" HeaderText="Tipo" />
                                            <asp:BoundField DataField="nomtproducto" HeaderText="T.Producto" />
                                            <asp:BoundField DataField="nroRef" HeaderText="# Ref" />
                                            <asp:BoundField DataField="valor" DataFormatString="{0:N}" HeaderText="Valor">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
                                            <asp:BoundField DataField="tipopago" HeaderText="Tipo Pago" />
                                            <asp:BoundField DataField="codtipopago" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                                            <asp:BoundField DataField="idavance" HeaderText="Id.Avance" />
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 170px">Valor Total
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtValorTran" runat="server" CssClass="textbox" Enabled="false"
                                        Width="171px" Style="text-align: right"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTran"
                                        Mask="999,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>

                        </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblOpcRegistro" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTipoConvenio" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTipoProducto" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:MultiView ID="mvOperacion" runat="server">
        <asp:View ID="vwFormaPago" runat="server">
            <table cellpadding="5" cellspacing="0" >
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel7" runat="server">
                            <table>
                                <tr>
                                    <td class="logo" style="width: 120px">
                                        <strong>Forma de Pago</strong>
                                    </td>
                                    <td style="width: 124px">&nbsp;
                                    </td>
                                    <td class="logo" style="width: 131px">&nbsp;
                                    </td>
                                    <td style="width: 33px">&nbsp;
                                    </td>
                                    <td rowspan="5">
                                        <asp:Panel ID="Panel8" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 87px">
                                                        <strong>Cheques</strong>
                                                    </td>
                                                    <td style="width: 106px">&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 87px; font-size: x-small;">Núm. Cheque
                                                    </td>
                                                    <td style="width: 106px">E<span style="font-size: x-small">ntidad Bancaria</span>
                                                    </td>
                                                    <td style="font-size: x-small">Valor Cheque
                                                    </td>
                                                    <td style="font-size: x-small">Moneda
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 87px">
                                                        <asp:TextBox ID="txtNumCheque" runat="server" CssClass="textbox" Width="94px" MaxLength="20"></asp:TextBox>
                                                        <asp:Label ID="numchequevacio" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                                                    </td>
                                                    <td style="width: 106px">
                                                        <asp:DropDownList ID="ddlBancos" runat="server" CssClass="textbox" Width="123px">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="bancochquevacio" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <uc1:decimales ID="txtValCheque" runat="server" CssClass="textbox" Width="100px"></uc1:decimales>
                                                        <asp:Label ID="valorchequevacio" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMonCheque" runat="server" CssClass="textbox" Width="82px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGoCheque" runat="server" OnClick="btnGoCheque_Click" Text="&gt;&gt;"
                                                            Width="22px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:GridView ID="gvCheques" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                            CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" Style="font-size: small"
                                                            Width="79%" ShowHeaderWhenEmpty="True" OnRowDeleting="gvCheques_RowDeleting">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                                            ToolTip="Eliminar" Width="16px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="entidad" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                                                <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                                                <asp:BoundField DataField="numcheque" HeaderText="Núm. Cheque" />
                                                                <asp:BoundField DataField="nomentidad" HeaderText="Entidad" />
                                                                <asp:BoundField DataField="valor" DataFormatString="{0:N0}" HeaderText="Valor">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                            <RowStyle CssClass="gridItem" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                            <SortedAscendingHeaderStyle BackColor="#848384" />
                                                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                            <SortedDescendingHeaderStyle BackColor="#575357" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: right">Valor Total Cheques
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtValTotalCheque" runat="server" CssClass="textbox" Enabled="false"
                                                            Width="171px" Style="text-align: right"></asp:TextBox>
                                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtValTotalCheque"
                                                            Mask="999,999,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                                        </asp:MaskedEditExtender>
                                                                                   <asp:LinkButton ID="btnGuardar"  runat="server" CssClass="btn btn-small btn-outline-success btn-master">
                                                                        <asp:ImageButton ID="ImageButton1" runat="Server" OnClick="btnGuardarCod_Click" ImageUrl="~/Images/btnGuardar.jpg"></asp:ImageButton>
                                                            </asp:LinkButton>

                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>

                                               <%--  <tr>
                                                    <td colspan="2" style="text-align: right">
                                                    </td>
                                                    <td colspan="2" style="margin : 5px auto">
                                                            
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 120px">Moneda
                                    </td>
                                    <td style="width: 124px">Forma de Pago
                                    </td>
                                    <td class="logo" style="width: 131px; text-align: center">Valor
                                    </td>
                                    <td style="width: 33px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 120px">
                                        <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="textbox" Width="114px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 124px">
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="true" CssClass="textbox" Width="133px" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="logo" style="width: 131px">
                                        <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Width="120px"></uc1:decimales>
                                    </td>
                                    <td style="width: 33px">
                                        <asp:Label ID="lblboucher" runat="server" Style="color: #FF0000; font-size: xx-small;" Text="N. Baucher" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtBaucher" runat="server" CssClass="textbox" MaxLength="20" Visible="False" Width="50px"></asp:TextBox>
                                        <asp:Button ID="btnGoFormaPago" runat="server" OnClick="btnGoFormaPago_Click" Text="&gt;&gt;"
                                            Width="21px" Style="height: 26px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" colspan="3">
                                        <asp:Panel ID="PanelForPago" runat="server">
                                            <asp:GridView ID="gvFormaPago" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                GridLines="Vertical" PageSize="20" Width="98%" Style="text-align: left; font-size: small">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fpago" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                        <HeaderStyle CssClass="gridColNo" HorizontalAlign="Center" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
                                                    <asp:BoundField DataField="nomfpago" HeaderText="F.Pago">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipomov" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                    <td class="logo">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 120px; text-align: right;">Valor Total
                                    </td>
                                    <td style="width: 124px">
                                        <asp:TextBox ID="txtValTotalFormaPago" runat="server" CssClass="textbox" Enabled="false"
                                            Width="171px" Style="text-align: right"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtValTotalFormaPago"
                                            Mask="999,999,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                        </asp:MaskedEditExtender>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button runat="server" ID="mpePopUp" class="enlace" role="link" Text="Ver Documento" OnClick="mpePopUp_OnClick" Visible="False" />
                                    </td>
                                    <td class="logo" style="width: 131px">&nbsp;
                                    </td>
                                    <td style="width: 33px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 120px; text-align: right;">Valor Recibido
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="valoring" runat="server" CssClass="textbox" Width="120px"
                                            Enabled="true" Style="text-align: right;" onChange="devueltas()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 120px; text-align: right;">Valor A Devolver
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="valord" runat="server" CssClass="textbox" Width="120px"
                                            Enabled="false" Style="text-align: right;"></asp:TextBox>
             
                                    </td>
                                      
                                </tr>


                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFormatoObligatorio" runat="server">
            <asp:Panel runat="server" ClientIDMode="Static" ID="pnlReporte">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="RpviewInfo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                Height="300px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                                WaitMessageFont-Size="14pt" Width="100%" ShowBackButton="False"
                                ShowCredentialPrompts="False" ShowDocumentMapButton="False"
                                ShowFindControls="False" ShowPageNavigationControls="False"
                                ShowParameterPrompts="False" ShowPromptAreaButton="False"
                                ShowRefreshButton="False" ShowToolBar="False" ShowWaitControlCancelLink="False"
                                ShowZoomControl="False" SizeToReportContent="True">
                                <LocalReport ReportPath="Page\CajaFin\OperacionCaja\rptDeclaracion.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <%--  <asp:Button ID="btnImprimirRep" runat="server" Text="Imprimir Informe" Visible="true"
                                        Style="width: 115px; text-align: left;" OnClick="btnImprimiendose_Click" />--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<input id="Button1" onclick="javascript: imprimir();" type="button" value="Imprimir Factura." style="width: 130px;" />--%>
                        
                        </td>

                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="HiddenField3" runat="server" />
      <asp:HiddenField ID="HiddenField4" runat="server" />
    <asp:ModalPopupExtender ID="MpeDetallePago" runat="server" Enabled="True" PopupDragHandleControlID="Panelf"
        PopupControlID="PanelDetallePago" TargetControlID="HiddenField1" CancelControlID="btnCloseAct">
        <Animations>
        <OnHiding>
            <Sequence>                            
                <StyleAction AnimationTarget="btnCloseAct" Attribute="display" Value="none" />
                <Parallel>
                    <FadeOut />
                    <Scale ScaleFactor="5" />
                </Parallel>
            </Sequence>
        </OnHiding>            
        </Animations>
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="MpeDetallePagoAportes" runat="server" Enabled="True"
        PopupDragHandleControlID="Panelg" PopupControlID="PanelDetallePagoAportes" TargetControlID="HiddenField2"
        CancelControlID="btnCloseAct2">
        <Animations>
        <OnHiding>
            <Sequence>                            
                <StyleAction AnimationTarget="btnCloseAct2" Attribute="display" Value="none" />
                <Parallel>
                    <FadeOut />
                    <Scale ScaleFactor="5" />
                </Parallel>
            </Sequence>
        </OnHiding>            
        </Animations>
    </asp:ModalPopupExtender>     



    <asp:ModalPopupExtender ID="GeneradorDocumento" runat="server" Enabled="True" PopupDragHandleControlID="Panelsw"
        PopupControlID="PanelDocumentos" TargetControlID="HiddenField3" CancelControlID="btnCloseAct2">
    </asp:ModalPopupExtender>
    
    <asp:Panel ID="PanelDetallePago" runat="server" Width="480px" Style="border: solid 2px Gray"
        CssClass="modalPopup">
        <asp:UpdatePanel ID="upDetallePago" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panelf" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle del Pago</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; margin-left: 120px;">
                            <asp:GridView ID="GVDetallePago" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                GridLines="Vertical" PageSize="20" Width="475px" Style="text-align: left; font-size: xx-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="NumCuota" HeaderText="No." DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaCuota" HeaderText="F.Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Capital" HeaderText="Capital" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IntCte" HeaderText="Int.Cte" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IntMora" HeaderText="Int.Mora" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ivaLeyMiPyme" HeaderText="Iva Ley MiPyme" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Otros" HeaderText="Otros" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Poliza" HeaderText="Póliza" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="btnCloseAct" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseAct_Click"
                                CausesValidation="false" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvTransacciones" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="PanelDetallePagoAportes" runat="server" Width="480px" Style="border: solid 2px Gray"
        CssClass="modalPopup">
        <asp:UpdatePanel ID="UpDetallePagoApo" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panelg" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle del Pago</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; margin-left: 120px;">
                            <asp:GridView ID="GvPagosAPortes" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                GridLines="Vertical" PageSize="20" Width="475px" Style="text-align: left; font-size: xx-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="FechaCuota" HeaderText="F.Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Capital" HeaderText="ValorPago" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="btnCloseAct2" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseAct2_Click"
                                CausesValidation="false" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvTransacciones" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="PanelDocumentos" runat="server" Width="47pc" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="PanelDocumento" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small; color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panelsw" runat="server" Width="748px" Style="cursor: move">
                                <strong>Documento Declaracion</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="LiteralDcl" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="cerrar" CssClass="button" CausesValidation="False" Height="20px" OnClick="btneCancelar_Click" Style="margin: 0 auto;" />
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }

        var contadorClickGuardarNoMaster = 0
        $(document).ready(function () {
            $('#<%= ((LinkButton)Master.FindControl("btnGuardar")).ClientID %>').off("click", EvitarClickeoLocos).click(function () {
            ).off("click", EvitarClickeoLocos).click(function () {
                if (contadorClickGuardarNoMaster == 0) {
                    if (confirm('Desea Aplicar los Pagos?')) {
                        contadorClickGuardarNoMaster += 1;
                        return true;
                    }
                }
                if (contadorClickGuardarNoMaster == 0) {
                    var imgLoading = document.getElementById("imgLoading");
                    imgLoading.style.visibility = "hidden";
                }
                return false;
            });
        });
    </script>


    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function validar() {


            var statusConfirm = confirm("¿Desea Ver Estado de Cuenta del Asosiado?");
            if (statusConfirm == true) {
                window.open("../../Asesores/EstadoCuenta/Detalle.aspx", 'Estado de Cuenta', "resizable=yes ,width=500, height=450 align=center");
            }



        }

        function devueltas() {
            debugger;
            if ($("#cphMain_valord").val() !== null) {
                $("#cphMain_valord").val('');
            }
            var s = $("#cphMain_txtValTotalFormaPago").val().split(".").join("").replace(",", ".");
            var q = $("#cphMain_valoring").val();
            var validar = 0;
            validar = (parseFloat(q) - parseFloat(s));
            if (validar > 0)
            {
                $("#cphMain_valord").val(new Intl.NumberFormat('es-MX').format(validar));
            }
            else
            {
                $("#cphMain_valord").val(new Intl.NumberFormat('es-MX').format(0));
            }
        }

    </script>
</asp:Content>
