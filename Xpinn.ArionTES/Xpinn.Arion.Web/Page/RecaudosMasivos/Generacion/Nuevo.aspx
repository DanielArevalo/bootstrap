<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Lista" Title=".: Xpinn - Generación Novedades :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimal" TagPrefix="c1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapidaVentEmergente.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/css/select2.min.css" />
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>

    <script type="text/javascript">
        function Consultar(btnConsultar_Click) {
            var obj = document.getElementById("btnConsultar_Click");
            if (obj) {
                obj.click();
            }
        }
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 950,
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

       <script type="text/javascript">
        $(document).ready(function () {
            $("#cphMain_ddlEmpresa").select2();
        });
    </script>

    <asp:MultiView ID="mtvGeneral" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: left">
                        <asp:ImageButton ID="btnGenerar" runat="server" ImageUrl="~/Images/btnGenerar.jpg"
                            OnClick="btnGenerar_Click" />
                    </td>

                    <td colspan="4" style="text-align: left">
                        <asp:Button ID="btnGenerarArchivos" runat="server" Text="Generar Archivos" CssClass="btn8"
                            Height="30px" OnClick="btnGenerarArchivos_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="PanelEncabezado" runat="server">
                <table>
                    <tr>
                        <td style="text-align: left">Empresa
                            <asp:TextBox ID="txtcodGeneracion" runat="server" CssClass="textbox" Width="80%"
                                ReadOnly="True" Visible="false" />
                        </td>
                        <td style="text-align: left">Tipo de Lista
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial" />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblFechaPeriodo" runat="server" Text="Periodo" />
                        </td>
                        <td style="text-align: left;">Fecha Recaudo
                        </td>
                        <td style="text-align: left;">Estado
                        </td>
                        <td style="text-align: left;">Numero Planilla
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="180px"
                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="lblManejaAtributo" runat="server" Visible="false" />
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlTipoLista" runat="server" CssClass="textbox" Width="150px"
                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoLista_SelectedIndexChanged">
                                <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <ucFecha:fecha ID="txtFechaInicial" runat="server" CssClass="textbox" MaxLength="1"
                                ValidationGroup="vgGuardar" Width="140px" />
                        </td>
                        <td style="text-align: left">
                            <ucFecha:fecha ID="txtfechaPeriodo" runat="server" CssClass="textbox" MaxLength="1"
                                ValidationGroup="vgGuardar" Width="140px" />
                            <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="textbox" Width="148px"
                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <ucFecha:fecha ID="txtfecha" runat="server" CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar"
                                Width="140px" />
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="150px"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblnum_planilla" runat="server" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <div style="text-align: left">
                <strong>Novedades:</strong>
                <br />
                <asp:Button ID="btnCargarDatos" runat="server" CssClass="btn8" Height="20px" Width="150px" OnClick="btnCargarDatos_Click"
                    OnClientClick="btnCargarDatos_Click" Text="Cargar Datos" />
                &#160;&#160;&#160;&#160;
                <br />
            </div>
            <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle"
                Style="margin-right: 30px; text-align: left;" Width="120%">
                <asp:TabPanel ID="tabTotales" runat="server">
                    <HeaderTemplate>
                        <div style="width: 140px">
                            TODAS
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                        <asp:UpdatePanel ID="panelGrilla" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left; width: 100%">
                                            <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" Width="150px" OnClick="btnDetalle_Click"
                                                OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                                            <asp:Label ID="lblConsulta" runat="server" Text="Consultar persona:" Style="text-align: left;"></asp:Label>
                                            <asp:TextBox type="text" ID="txtBuscar" runat="server" CssClass="textbox" />
                                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn8" Height="20px" OnClick="btnBuscar_Click" OnClientClick="btnBuscar_Click" Text="Buscar" />
                                            <asp:Panel ID="panelTotalRegs" runat="server" Style="text-align: center;">
                                                <br />
                                                <asp:Label ID="lblTotGenera" runat="server" />&#160;&#160;&#160;
                                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                                <asp:Label ID="Label2" runat="server" Style="text-align: Center" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                                            </asp:Panel>

                                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                                DataKeyNames="iddetalle" Style="font-size: x-small" 
                                                OnRowEditing="gvLista_RowEditing"
                                                OnRowDeleting="gvLista_RowDeleting" 
                                                OnRowDataBound="gvLista_RowDataBound"
                                                OnPageIndexChanging="gvLista_PageIndexChanging" >
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
                                                        <HeaderStyle CssClass="gridIco" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:CommandField>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                        <HeaderStyle CssClass="gridIco" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                                        <EditItemTemplate>
                                                            <span>
                                                                <asp:TextBox ID="txtiddetalle" runat="server" Text='<%# Bind("iddetalle") %>'></asp:TextBox>
                                                            </span>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbliddetalle" runat="server" Text='<%# Bind("iddetalle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo Producto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnom_tipo_producto" runat="server" Text='<%# Bind("nom_tipo_producto") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodTipoProducto" Text='<%# Bind("nom_tipo_producto") %>' Visible="false" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Linea">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllinea" runat="server" Text='<%# Bind("linea") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Núm. Producto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnumero_producto" runat="server" Text='<%# Bind("numero_producto") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cod. Cliente">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Identificación">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("identificacion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nombres">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnombres" runat="server" Text='<%# Bind("nombres") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apellidos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapellidos" runat="server" Text='<%# Bind("apellidos") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="F. Prox Pago">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfechaGrid" runat="server" Text='<%# Bind("fecha_proximo_pago", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvalor" runat="server" Text='<%# Bind("valor", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Saldo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsaldo" runat="server" Text='<%# Bind("saldo", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cod. Nomina">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigoNomina" runat="server" Text='<%# Bind("cod_nomina_empleado") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vr Capital">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrCapital" runat="server" Text='<%# Bind("capital", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vr Interes Cte">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrInteresCte" runat="server" Text='<%# Bind("intcte", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vr Interes Mora">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrInteresMora" runat="server" Text='<%# Bind("intmora", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor Seguro">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrSeguro" runat="server" Text='<%# Bind("seguro", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Otros Atributos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrOtros" runat="server" Text='<%# Bind("otros", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Fijos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrtotal_fijos" runat="server" Text='<%# Bind("total_fijos", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Prestamos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVrtotal_prestamos" runat="server" Text='<%# Bind("total_prestamos", "{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fec. Inicio Crédito">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecInicio" runat="server" Text='<%# Bind("fecha_inicio_producto", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fec. Vencimiento Crédito">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecVencimiento" runat="server" Text='<%# Bind("fecha_vencimiento_producto", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Vacaciones">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvacaciones" runat="server" Text='<%# Bind("vacaciones") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                                <PagerStyle CssClass="pagerstyle" />
                                                <RowStyle CssClass="gridItem"></RowStyle>
                                            </asp:GridView>
                                            
                                            <asp:GridView ID="gvExport" runat="server" Width="100%" GridLines="Horizontal"
                                                AutoGenerateColumns="False" DataKeyNames="iddetalle" Style="font-size: x-small">
                                                <Columns>
                                                    <asp:BoundField DataField="iddetalle" HeaderText="Id Detalle">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="linea" HeaderText="Linea">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="numero_producto" HeaderText="Núm. Producto">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod. Cliente">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombres" HeaderText="NombreS">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="apellidos" HeaderText="ApellidoS">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_nomina_empleado" HeaderText="Cod. Nomina">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="capital" HeaderText="Vr Capital" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="intcte" HeaderText="Vr Interes Cte" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="intmora" HeaderText="Vr Interes Mora" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="seguro" HeaderText="Valor Seguro" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="otros" HeaderText="Otros Atributos" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>

                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabNuevos" runat="server">
                    <HeaderTemplate>
                        <div style="width: 140px">
                            NUEVAS
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 100%">
                                    <asp:Button ID="btnDetalleNew" runat="server" CssClass="btn8" Width="150px" OnClick="btnDetalleNew_Click"
                                        OnClientClick="btnDetalleNew_Click" Text="+ Adicionar Detalle" />
                                    &#160;&#160;&#160;&#160;
                                    <asp:GridView ID="gvNovedadesNuevas" runat="server" Width="100%" GridLines="Horizontal"
                                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvNovedadesNuevas_PageIndexChanging"
                                        OnRowEditing="gvNovedadesNuevas_RowEditing" PageSize="20" OnRowDeleting="gvNovedadesNuevas_RowDeleting"
                                        DataKeyNames="iddetalle" Style="font-size: x-small" OnRowDataBound="gvNovedadesNuevas_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <EditItemTemplate>
                                                    <span>
                                                        <asp:TextBox ID="txtiddetalle" runat="server" Text='<%# Bind("iddetalle") %>'></asp:TextBox>
                                                    </span>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbliddetalle" runat="server" Text='<%# Bind("iddetalle") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Producto">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnom_tipo_producto" runat="server" Text='<%# Bind("nom_tipo_producto") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Linea">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllinea" runat="server" Text='<%# Bind("linea") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Número Producto">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnumero_producto" runat="server" Text='<%# Bind("numero_producto") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Codigo Cliente">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("identificacion") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnombres" runat="server" Text='<%# Bind("nombres") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblapellidos" runat="server" Text='<%# Bind("apellidos") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Novedad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoNovedad" runat="server" Text='<%# Bind("tipo_novedad") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="F. Novedad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFechaNovedad" runat="server" Text='<%# Bind("fecha_inicial", "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="F. Final Novedad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFecFinNovedad" runat="server" Text='<%# Bind("fecha_final", "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor Cuota">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalorCuota" runat="server" Text='<%# Bind("valor", "{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Número Cuota">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumCuotas" runat="server" Text='<%# Bind("numero_cuotas") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVrTotal" runat="server" Text='<%# Bind("valor_total", "{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cod_nomina_empleado" HeaderText="Codigo Nomina">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="pagerstyle" />
                                        <PagerTemplate>
                                            <asp:Label ID="lblFilas" runat="server" Text="Mostrar filas:" />
                                            <asp:DropDownList CssClass="letranormal" ID="RegsPag" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="RegsPagNew_SelectedIndexChanged" Enabled="False">
                                                <asp:ListItem Value="10" />
                                                <asp:ListItem Value="15" />
                                                <asp:ListItem Value="20" />
                                            </asp:DropDownList>
                                            &nbsp; Ir a
                                            <asp:TextBox ID="IraPag" runat="server" AutoPostBack="true" OnTextChanged="IraPag"
                                                CssClass="gopag" Enabled="False" />
                                            de
                                            <asp:Label ID="lblTotalNumberOfPages" runat="server" />
                                            &nbsp;
                                            <asp:Button ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                                CommandArgument="First" CssClass="pagfirst" />
                                            <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                                CssClass="paglast" />
                                        </PagerTemplate>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panel1" runat="server" Style="text-align: center;">
                            <asp:Label ID="lblTotalNew" runat="server" />&#160;&#160;&#160;
                            <asp:Label ID="lblTotalRegNuevos" runat="server" Visible="False" Style="text-align: center" />
                            <asp:Label ID="lblInfoNuevo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                Visible="False" Style="text-align: Center" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <br />
            <br />
        </asp:View>
        <asp:View ID="vReporte" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <rsweb:ReportViewer ID="ReportViewRecaudos" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Height="450px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="100%">
                            <LocalReport ReportPath="Page\RecaudosMasivos\Generacion\ReporteResumido.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
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
                    <td style="text-align: center; font-size: large;">Se<asp:Label ID="Label1" runat="server"></asp:Label>
                        correctamente los Datos
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwCargaDatos" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="2" style="text-align: center" class="gridHeader">
                        <strong>Seleccione el Archivo</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; font-size: xx-small; width: 100%">
                        <strong>Tipo de Archivo de Carga : &#160;&#160;&#160; .txt
                            <br />
                            Separador de Campos de Carga : &#160;&#160;&#160; |
                            <br />
                            Estructura de archivo a cargar: &#160;&#160;&#160; Código Tipo Producto, Código
                            de Línea, Número de Producto, Código Cliente, Identificación, Nombres, Apellidos,
                            Fecha Prox Pago, Valor</strong>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 40%">Archivo
                    </td>
                    <td style="text-align: left; width: 60%">
                        <asp:FileUpload ID="flpArchivo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblmsjCarga" runat="server" Style="color: Red; font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnAceptarCarga" runat="server" Height="30px" Text="Aceptar" Width="100px"
                            CssClass="btn8" OnClick="btnAceptarCarga_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" CssClass="btn8" Height="30px" Text="Cancelar"
                            Width="100px" OnClick="btnCancelarCarga_Click" />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="mvVentEmergente" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwGenerarArchivos" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="2" style="text-align: center" class="gridHeader">
                        <strong>Seleccione la Estructura</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 40%">Tipo de Estructura
                    </td>
                    <td style="text-align: left; width: 60%">
                        <asp:DropDownList ID="ddlEstructura" runat="server" CssClass="textbox" Width="300px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlEstructura_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 40%">Nombre del Archivo
                    </td>
                    <td style="text-align: left; width: 60%">
                        <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="textbox" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 40%">Archivo a Generar
                    </td>
                    <td style="text-align: left; width: 60%">
                        <asp:TextBox ID="txtArchivo" runat="server" CssClass="textbox" Width="200px" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblMensj" runat="server" Style="color: Red; font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnAceptarEstructura" runat="server" Height="30px" Text="Aceptar"
                            Width="100px" CssClass="btn8" OnClick="btnAceptarEstructura_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelarEstructura" runat="server" CssClass="btn8" Height="30px"
                            Text="Cancelar" Width="100px" OnClick="btnCancelarEstructura_Click" />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <asp:ModalPopupExtender ID="mpeActualizarNovedad" runat="server" PopupControlID="panelActualizarNovedad"
        TargetControlID="hfFechaIni" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="hfFechaIni" runat="server" />
    <div style="text-align: center">
        <asp:Panel ID="panelActualizarNovedad" runat="server" BackColor="White" Style="text-align: left" Width="550px">
            <div style="border: 2px groove #2E9AFE; width: 550px; background-color: #FFFFFF; padding: 10px">
                <asp:UpdatePanel ID="upModificacionTasa" runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table width="550px">
                            <tr>
                                <td class="gridHeader" style="text-align: center;" colspan="3">MODIFICACION DE DATOS
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;">Tipo de Producto :
                                </td>
                                <td style="text-align: left; width: 400px;" colspan="2">
                                    <asp:DropDownList ID="ddlTipoProducto" runat="server" Width="50%" CssClass="textbox"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged"
                                        AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtidDetalle" runat="server" CssClass="textbox" ReadOnly="True"
                                        Width="50px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;">Nro de Producto
                                </td>
                                <td style="text-align: left; width: 180px;">
                                    <asp:TextBox ID="txtCodProducto" runat="server" CssClass="textbox" Width="65%">
                                    </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="txtCodProducto" ValidChars="" />
                                    <asp:TextBox ID="txtIdProducto" runat="server" CssClass="textbox" Width="50px" ReadOnly="True"
                                        Visible="false" />
                                    <asp:Button ID="btnBuscaProductos" runat="server" CssClass="btn8" Height="26px" Text="..."
                                        Visible="false" />
                                    <uc1:ListadoPersonas ID="ctlBusquedaProducto" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ErrorMessage="Seleccione encargado"
                                        Display="Dynamic" ControlToValidate="txtNomProducto" ValidationGroup="guardar"
                                        InitialValue="0" ForeColor="Red" Style="font-size: xx-small" />
                                </td>
                                <td style="text-align: left; width: 220px;">
                                    <asp:TextBox ID="txtNomProducto" runat="server" CssClass="textbox" Width="90%" PlaceHolder="Cod Linea-Nombre Linea">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;">Persona
                                </td>
                                <td style="text-align: left; width: 180px;">
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="65%"
                                        AutoPostBack="true" OnTextChanged="txtIdentificacion_TextChanged" />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Width="50px" ReadOnly="True"
                                        Visible="false" />
                                    <asp:Button ID="btnBuscarPersona" runat="server" CssClass="btn8" Height="26px" Text="..."
                                        OnClick="btnBuscarPersona_Click" />
                                    <uc1:ListadoPersonas ID="ctlBusquedaPersona" runat="server" style="position: fixed; top: 0px; left: 0px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Seleccione encargado"
                                        Display="Dynamic" ControlToValidate="txtNomPersona" ValidationGroup="guardar"
                                        InitialValue="0" ForeColor="Red" Style="font-size: xx-small" />
                                </td>
                                <td style="text-align: left; width: 220px;">
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" Width="90%" ReadOnly="True">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;">Valor
                                </td>
                                <td style="text-align: left; width: 180px;">
                                    <asp:TextBox ID="txtvalor" runat="server" CssClass="textbox" Width="80%">
                                    </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtvalor" ValidChars=",." />
                                </td>
                                <td style="text-align: left; width: 220px;">
                                    <asp:Label ID="lblFecEmerg" runat="server" Text="F. Prox Pago" />&nbsp;
                                <ucFecha:fecha ID="txtProxPago" runat="server" CssClass="textbox" MaxLength="1" Width="50%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="panelEdNovNew" runat="server">
                                        <table>
                                            <tr>
                                                <td style="text-align: left; width: 150px;">Tipo de Novedad
                                                </td>
                                                <td style="text-align: left; width: 180px;">
                                                    <asp:DropDownList ID="ddlTipoNovedad" runat="server" CssClass="textbox" Width="150px">
                                                        <asp:ListItem Value="I">Inclusión</asp:ListItem>
                                                        <asp:ListItem Value="R">Retiro</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 220px;">F. Final Nov.&nbsp;
                                                <ucFecha:fecha ID="txtFecFinNov" runat="server" CssClass="textbox" MaxLength="1" Width="50%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 150px;">Valor Total
                                                </td>
                                                <td style="text-align: left; width: 180px;">
                                                    <asp:TextBox ID="txtVrTotal" runat="server" CssClass="textbox" Width="80%" />
                                                    <asp:FilteredTextBoxExtender ID="fte123" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtVrTotal" ValidChars=",." />
                                                </td>
                                                <td style="text-align: left; width: 220px;">Num. Cuotas&nbsp;
                                                <asp:TextBox ID="txtNumCuotas" runat="server" CssClass="textbox" Width="50%" />
                                                    <asp:FilteredTextBoxExtender ID="fte145" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="txtNumCuotas" ValidChars="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Label ID="lblmsj" runat="server" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnModificar" runat="server" Height="30px" Text="Guardar" Width="100px"
                                        OnClick="btnModificar_Click" ValidationGroup="grabar" />
                                    &#160;&#160;&#160;&#160;
                        <asp:Button ID="btnCloseReg1" runat="server" CausesValidation="false" CssClass="button"
                            Height="30px" Text="Cancelar" Width="100px" OnClick="btnCloseReg1_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField ID="HF2" runat="server" />
    <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando"
        TargetControlID="HF2" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;"
        CssClass="pnlBackGround">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loading.gif" />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Espere un momento mientras se ejecuta el Proceso."></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="text-align: center; width: 100%">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="300" OnTick="Timer1_Tick">
            </asp:Timer>
            <br />
            <asp:Label ID="lblError" runat="server" Text=""
                Style="text-align: left; color: #FF3300"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>

</asp:Content>
