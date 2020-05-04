<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc5" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <script type="text/javascript">
        function deshabilitar() {
            //Esta es una variable. Su valor es true cuando todos los campos 
            //son válidos y false cuando hay algún error.
            var camposValidos = false;

            //En esta variable guardamos el objeto que representa al elemento
            //que tenga como ID = boton dentro de nuestro HTML. Asumimos que ese fue
            //el ID que se le dio al botón de enviar. 
            var botonEnviar = document.getElementById('btnGuardar2');

            if (camposValidos == false) {
                botonEnviar.disabled = true;
            }
            else {
                botonEnviar.disabled = false;
            }
        }

        function deshabilitar(boton) {
            document.getElementById(boton).style.visibility = 'hidden';
        }
    </script>
    <script type="text/javascript">
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
    </script>
    <br />
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:Panel ID="panelEncabezado" runat="server">
            <table style="width:85%;">
                <tr>
                    <td style="text-align: left;font-size:x-small" colspan="8">
                        <asp:Label ID="Lblerror" runat="server" 
                            style="color: #FF0000; font-weight: 700" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px; text-align: left;">
                        <span style="font-size: x-small">Oficina</span>
                    </td>
                    <td style="width: 180px; text-align: left;">
                        <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False" 
                            Width="151px"></asp:TextBox>
                    </td>
                    <td style="font-size: x-small; width: 140px; text-align: left">
                        Fecha y Hora de Transacción</td>
                    <td style="font-size: x-small; width: 120px; text-align: left">
                        <asp:TextBox ID="txtFechaReal" runat="server" CssClass="textbox" 
                            Enabled="false" MaxLength="10" Width="120px"></asp:TextBox>
                    </td>
                    <td style="font-size: x-small; width: 100px; text-align: right">
                        Fecha Nota</td>
                    <td style="font-size: x-small; width: 100px; text-align: left">
                    <uc3:fecha ID="txtFechaNota" runat="server" />
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width:80%;">
                <tr>
                    <td style="text-align: left" colspan="2">
                        <strong>Datos del Cliente</strong>&nbsp;</td>
                    <td style="text-align: right">
                        &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Tipo Identificación
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right">
                        Identificación</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                            MaxLength="12" Width="124px"></asp:TextBox>
                        <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." 
                            Height="26px" onclick="btnConsultaPersonas_Click" />
                        <uc5:ListadoPersonas id="ctlBusquedaPersonas" runat="server" />
                    </td>
                    <td style="text-align: left">
                        <asp:Button ID="btnConsultar" runat="server" onclick="btnConsultar_Click" 
                            Text="Consultar" ValidationGroup="vgGuardar" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Nombres y Apellidos</td>
                    <td colspan="4" style="text-align: left">
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" 
                            Enabled="false" Width="582px"></asp:TextBox>
                    </td>
                </tr>
        
                <tr>
                    <td style="text-align: left">
                        Tipo de Producto
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="True" CssClass="textbox" 
                            onselectedindexchanged="ddlTipoTipoProducto_SelectedIndexChanged" Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">                
                    </td>
                    <td style="text-align: left">
                        &nbsp;</td>
                </tr>
            </table>
            <table width="80%">
                <tr>
                    <td>
                        <div id="divDatos" runat="server" style="overflow: scroll; max-height: 200px">                
                            <asp:GridView ID="gvConsultaDatos" runat="server" Width="99%" AutoGenerateColumns="False"
                                AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                OnRowCommand="gvConsultaDatos_RowCommand" style="font-size: x-small">                   
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Estado Cuenta" CommandName='<%#Eval("numero_radicacion")%>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>                    
                                    <asp:BoundField DataField="cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" >
                                        <HeaderStyle CssClass="gridColNo" />
                                        <ItemStyle CssClass="gridColNo" />
                                    </asp:BoundField>                        
                                    <asp:BoundField DataField="numero_radicacion" HeaderText="Radicado" />
                                    <asp:BoundField DataField="linea_credito" HeaderText="Línea Crédito" />
                                    <asp:BoundField DataField="Dias_mora" HeaderText="Dias de Mora" />
                                        <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="{0:n0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:n0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:n0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="garantias_comunitarias" HeaderText="G.Comunitaria" DataFormatString="{0:n0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_proxima_pago" HeaderText="Fec. Próx.Pago" 
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="valor_a_pagar" HeaderText="Valor a Pagar" 
                                        DataFormatString="{0:n0}"  >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="total_a_pagar" HeaderText="Valor Total a Pagar" 
                                        DataFormatString="{0:n0}" >
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
                
                             <asp:GridView ID="gvDatosAfiliacion" runat="server" Width="99%"
                                AutoGenerateColumns="False" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                DataKeyNames="idafiliacion" style="font-size: x-small" onrowediting="gvDatosAfiliacion_RowEditing">                   
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>                      
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" 
                                        ShowEditButton="True" />          
                                    <asp:BoundField DataField="idafiliacion" HeaderText="Código" />
                                    <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fecha Afiliación" 
                                        DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="valor" HeaderText="Valor Afiliación" 
                                        DataFormatString="{0:n}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Periodicidad" />                  
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
                            <%--AHORRO VISTA--%>
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
                                RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio" style="font-size: x-small">
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
                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <%--CDAT--%>
                            <asp:GridView ID="gvCdat" runat="server" Width="99%" AutoGenerateColumns="False"
                                AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                Style="font-size: x-small" DataKeyNames="codigo_cdat" OnRowEditing="gvCdat_RowEditing">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                    <asp:BoundField DataField="codigo_cdat" HeaderText="Num Producto">
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
                                </Columns>
                               <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <%--AHORRO PROGRAMADO--%>
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
                             <%--OBLIGACIONES FINANCIERAS--%>
                            <asp:GridView ID="GvObligaciones" runat="server" Width="99%" AutoGenerateColumns="False"
                                AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                Style="font-size: x-small" DataKeyNames="codobligacion" OnRowEditing="GvObligaciones_RowEditing">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                    <asp:BoundField DataField="codobligacion" HeaderText="Num. Obligación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomlineaobligacion" HeaderText="Línea">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>                                    
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_ultpago" HeaderText="F. Ult Mov" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaproximopago" HeaderText="F. Prox Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                   
                                    <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldocapital" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cuota" HeaderText="Cuota" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                   
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>

                        </div>
               
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <asp:MultiView  ID="mvOperacion" runat="server">
            <asp:View ID="vwOperacion" runat="server">
                <asp:UpdatePanel ID="updOperacion" runat="server">
                <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td style="font-size: small; text-align: left; width: 75%" colspan="7" >
                            <strong>Datos de la Nota</strong>
                        </td>
                        <td>&nbsp;</td>
                    </tr>                
                    <tr>
                        <td style="width: 10%; text-align: left">
                            <asp:CheckBox ID="chkGeneraDev" runat="server" Text="Genera Devolución" TextAlign="Right" />
                        </td>
                        <td style="width: 10%">
                            <asp:CheckBox ID="chkPendiente" runat="server" Text="Genera Pendientes" TextAlign="Right" Checked ="true" />
                        </td>
                        <td style="text-align: left" colspan="5">
                            Tipo de Nota:
                            <asp:RadioButtonList ID="rblTipoNota" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="rblTipoNota_SelectedIndexChanged">
                                <asp:ListItem Value="D" Selected="True">Débito</asp:ListItem>
                                <asp:ListItem Value="C">Crédito</asp:ListItem>
                            </asp:RadioButtonList>    
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small;">
                            Número de Producto<br />
                            <asp:TextBox ID="txtNumProducto" runat="server" CssClass="textbox" Width="134px" MaxLength="12" ></asp:TextBox>
                        </td>
                        <td style="font-size: x-small;">
                            <asp:Label ID="Label1" runat="server" Text="Valor de la Transacción"></asp:Label><br />
                             <uc1:decimales ID="txtValTransac" runat="server" CssClass="textbox" Width="150px" MaxLength="17" style="text-align: right"></uc1:decimales>
                        </td>
                        <td style="font-size: x-small; width: 10%">
                            Moneda<br />
                            <asp:DropDownList ID="ddlMonedas" runat="server" CssClass="textbox"  Width="138px"></asp:DropDownList>
                        </td>
                        <td style="font-size: x-small; width: 10%">
                            <asp:Label ID="lblAtributo" runat="server" Text="Atributo"/><br />
                            <asp:DropDownList ID="ddlAtributo" runat="server" CssClass="textbox" Width="145px"></asp:DropDownList>
                        </td>
                        <td style="font-size: x-small; width: 10%;  text-align: left"><br />
                            <asp:Button ID="btnGoTran" runat="server" onclick="btnGoTran_Click" Text="&gt;&gt;" />
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>              
                    <tr>
                        <td colspan="5" style="font-size:x-small">
                            <asp:Label ID="lblMsgNroProducto" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>            
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblTipoNota" EventName="SelectedIndexChanged" />                    
                        <asp:PostBackTrigger ControlID="btnGoTran" />
                    </Triggers>            
                </asp:UpdatePanel>
            
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="text-align:left">
                            Observaciones&nbsp;
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="600px" MaxLength="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:left">
                            <strong>Transacciones a Realizar</strong></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvTransacciones" runat="server" AllowPaging="False" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" 
                                Width="60%" onrowdeleting="gvTransacciones_RowDeleting"
                                style="font-size: x-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns> 
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDistPagos" runat="server" CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Dist Pagos" />
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
                                    <asp:BoundField DataField="nroRef" HeaderText="# Producto" />
                                    <asp:BoundField DataField="valor" DataFormatString="{0:N}" HeaderText="Valor">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />                                
                                    <asp:BoundField DataField="atributo" HeaderText="Atributo" />
                                    <asp:BoundField DataField="codtipopago" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                        <HeaderStyle CssClass="gridColNo" />
                                        <ItemStyle CssClass="gridColNo" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_atr" HeaderText="Cod.Atr." />
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
                        <td style="text-align: left; width: 70px">
                            Valor Neto
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtValorTran" runat="server" CssClass="textbox" 
                                Enabled="false" Width="170px" style="text-align: right"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTran" Mask="999,999,999,999" MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" 
                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"></asp:MaskedEditExtender>
                        </td>
                    </tr>
                </table>                         
            </asp:View>        
            <asp:View ID="mvFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br /><br /><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <asp:Label ID="lblMensajeGrabar" runat="server" Text="Notas generadas correctamente"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <asp:Button ID="btnContinuar" runat="server" Text="Continuar" 
                                    onclick="btnContinuar_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
        <asp:ModalPopupExtender ID="MpeDetallePagoAportes" runat="server" Enabled="True" PopupDragHandleControlID="PanelFinal"
            PopupControlID="PanelDetallePagoAportes" TargetControlID="HiddenField1" CancelControlID="btnCloseAct2">
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
        
        <asp:ModalPopupExtender ID="MpeDetalleAvances" runat="server" Enabled="True" PopupDragHandleControlID="PanelFinal"
        PopupControlID="PanelDetalleAvance" TargetControlID="HiddenField1" CancelControlID="Bnt1">
        <Animations>
            <OnHiding>
                <Sequence>                            
                    <StyleAction AnimationTarget="Bnt1" Attribute="display" Value="none" />
                    <Parallel>
                        <FadeOut />
                        <Scale ScaleFactor="5" />
                    </Parallel>
                </Sequence>
            </OnHiding>            
        </Animations>
    </asp:ModalPopupExtender>

           <asp:HiddenField ID="HiddenField1" runat="server" />       
        <asp:Panel ID="PanelDetallePagoAportes" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup"  >
            <asp:UpdatePanel ID="UpDetallePagoApo" runat="server" >
            <ContentTemplate>  
                <table>
                    <tr>
                        <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panel9" runat="server" Width="475px" style="cursor: move">
                                <strong>Detalle del Pago</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; margin-left: 120px;">
                           <asp:GridView ID="GvPagosAPortes" runat="server" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                GridLines="Vertical" PageSize="20" Width="475px" 
                                style="text-align: left; font-size: xx-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>                            
                                    <asp:BoundField DataField="FechaCuota" HeaderText="F.Pago" 
                                        DataFormatString="{0:d}" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Capital" HeaderText="ValorPago" DataFormatString="{0:N0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N0}" >
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
                       <asp:Button ID="btnCloseAct2" runat="server" Text="Cerrar" CssClass="button" onclick="btnCloseAct2_Click" CausesValidation="false" Height="20px" />
                        </td>
                   </tr>
               </table>
           </ContentTemplate>
           </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="PanelDetalleAvance" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpDetalleAvances" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panel3" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle Avances </strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="background-color: #0066FF">
                       <td style=" background-color: #0066FF">
                            <table style="background-color: #0066FF">
                            <tr background-color: #0066FF">
                                <td style="width: 120px; background-color: #0066FF">
                                 <strong>Total Capital: </strong>
                                 <asp:TextBox ID="TxtTotalCap" runat="server" CssClass="textbox"
                                  Width="56px"></asp:TextBox>
                                  </td>
                                 <td style="width: 120px; background-color: #0066FF">
                                 <strong>Total Intereses: </strong>
                                 <asp:TextBox ID="TxtTotalInt" runat="server" CssClass="textbox"
                                  Width="56px"></asp:TextBox>
                                 </td>
                                 <td style="width: 120px; background-color: #0066FF">
                                 <strong>Total Avances: </strong>
                                 <asp:TextBox ID="TxtTotalAvances" runat="server" CssClass="textbox"
                                  Width="56px"></asp:TextBox>
                                  </td>
                       
                         </tr></table>
                          </td>
                    <tr>
                        <td style="width: 475px; margin-left: 120px; background-color: #0066FF">
                            <div class="scrolling-table-container" style= "height: 378px;  overflow-y: scroll; overflow-x: hidden;">
                                 <asp:CheckBox ID="chkAvances" runat="server"   AutoPostBack="true" OnCheckedChanged="chkAvances_CheckedChanged"/>
                                  <asp:Label ID="Label4" runat="server" Text=" ¿Seleccionar Todos los Avances?"></asp:Label>
                                <br />
                            <asp:GridView ID="gvAvances" runat="server" Width="99%" AutoGenerateColumns="False"
                                AllowPaging="false" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                Style="font-size: x-small" DataKeyNames="NumAvance" >
                                <Columns>
                                    <%-- IDAVANCE,FECHA_DESEMBOLSO,VALOR_DESEMBOLSADO,VALOR_CUOTA,PLAZO,SALDO_AVANCE--%>

                                  <asp:TemplateField HeaderText="Sel">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="chkAvance" runat="server"  Checked ="true"
                                                AutoPostBack="true" OnCheckedChanged="chkAvance_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NumAvance" HeaderText="Id Avance">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaDesembolsi" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValDesembolso" HeaderText="Valor Desembolso">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValorCuota" HeaderText="Valor Cuota">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SaldoAvance" HeaderText="Saldo Avance">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Intereses" HeaderText="Intereses">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="ValorTotal" HeaderText="Total Pagar">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>


                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" BackColor="#CCCC99" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                                </div>
                        </td>
                    </tr>
                     
                    
                </table>
                    </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="Bnt1" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseAct2_Click" CausesValidation="False" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

       
    </asp:Panel>


  
    
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
