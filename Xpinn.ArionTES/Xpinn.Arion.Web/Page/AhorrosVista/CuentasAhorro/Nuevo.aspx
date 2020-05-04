<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Ahorros :." %>

<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlDestinacion_ahorros.ascx" TagName="ddlDestinacion_Ahorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersonaEd.ascx" TagName="ddlPersona" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlModalidad.ascx" TagName="ddlModalidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="ddlFormaPago" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonass" TagPrefix="uc2" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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

    <script type="text/javascript">
function DisplayFullImage(ctrlimg) 
    { 
        txtCode = "<HTML><HEAD>" 
        +  "</HEAD><BODY TOPMARGIN=0 LEFTMARGIN=0 MARGINHEIGHT=0 MARGINWIDTH=0><CENTER>"   
        + "<IMG src='" + ctrlimg.src + "' BORDER=0 NAME=FullImage " 
        + "onload='window.resizeTo(document.FullImage.width,document.FullImage.height)'>"  
        + "</CENTER>"   
        + "</BODY></HTML>"; 
        mywindow= window.open  ('','image',  'toolbar=0,location=0,menuBar=0,scrollbars=0,resizable=0,width=1,height=1'); 
        mywindow.document.open(); 
        mywindow.document.write(txtCode); 
        mywindow.document.close();
    }
</script>
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewTitular" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <strong>Datos de la Linea</strong>
                        <asp:Panel ID="pEncLinea" runat="server">
                            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                    Linea de Ahorro a la Vista<br />
                                    <asp:DropDownList ID="ddlLineaFiltro" runat="server" Style="margin-bottom: 0px" Width="450px"
                                        CssClass="textbox">
                                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong style="text-align: left">Datos del Titular</strong>
                        <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pCuenta" runat="server" Width="106%">

             
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0"
                    style="width: 779px; height: 452px;">
                    <tr>
                        <td style="text-align: left" colspan="4">
                            <strong>Datos Principales</strong>
                            <hr style="text-align: left; width: 765px;" />
                        </td>

                    </tr>

                    <tr>
                        <td style="text-align: left; width: 201px;">Línea de Ahorros<ctl:ddlLineaAhorro ID="ddlLineaAhorro" runat="server"
                            Width="350px" AutoPostBack="true" />
                        </td>
                        <td style="text-align: left; width: 150px;">Fecha de Apertura<br />
                            <uc1:fecha ID="txtFechaApertura" runat="server" Enabled="True" Habilitado="True"
                                style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                        </td>

                        <td style="text-align: left; margin-left: 40px;" class="logo">Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                                Width="200px" />
                        </td>


                    </tr>
                    <tr>
                        <td style="text-align: left; width: 360px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    Número de Cuenta<br />
                                    <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="textbox" AutoPostBack="true"
                                        OnTextChanged="txtNumeroCuenta_TextChanged" />
                                    <asp:Label ID="lblNumAuto" runat="server" Text="Autogenerado" CssClass="textbox" Visible="false" />
                                    <asp:TextBox ID="txtDigVer" runat="server" CssClass="textbox" Width="20px" Visible="false" />
                                    <asp:CheckBox ID="ChkExentaGmf" runat="server" Text="Exenta Gmf" />
                                     <asp:Label ID="lblEntTerritorial" runat="server" Visible="false" style="color:red;"></asp:Label>
                                    <asp:Label ID="IdExcenta" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="FechaExcenta" runat="server" Visible="false"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtNumeroCuenta" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </td>
                        <td style="text-align: left; width: 150px;">Modalidad<br />
                            <asp:DropDownList ID="ddlModalidad" runat="server" AutoPostBack="True"
                                CssClass="textbox" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged"
                                Width="150px" />
                        </td>
                        <td style="text-align: left;" class="logo">Destinación<br />
                            <ctl:ddlDestinacion_Ahorro ID="ddlDestinacion" runat="server" Width="200px" />
                        </td>


                    </tr>
                    <tr>
                        <td style="text-align: left;">Asesor Comercial:
                                                    <br />
                            <asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="3">
                            <strong>Titulares</strong>
                            &nbsp;<hr style="text-align: left; width: 765px;" />

                            <asp:Button ID="btnAddRow" runat="server" CssClass="btn8"
                                OnClick="btnAddRow_Click" OnClientClick="btnAddRow_Click"
                                Text="+ Adicionar Titular" />
                        </td>


                    </tr>

                    <tr>

                        <td style="text-align: left" colspan="4">
                            <asp:Panel ID="panelPersona" runat="server">
                                <asp:GridView ID="gvDetalle" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="idcuenta_habiente"
                                    GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                                    OnRowDataBound="gvDetalle_RowDataBound" OnRowDeleting="gvDetalle_RowDeleting"
                                    OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged"
                                    PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
                                            ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Codigo" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblcod_cuentahabiente" runat="server" CssClass="textbox"
                                                    Enabled="false" Text='<%# Bind("idcuenta_habiente") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Identificación">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 309px">
                                                            <cc2:TextBoxGrid ID="txtIdentificacion" runat="server" AutoPostBack="True"
                                                                CommandArgument="<%#Container.DataItemIndex %>"
                                                                OnTextChanged="txtIdentificacion_TextChanged"
                                                                Text='<%# Bind("identificacion") %>' Width="90px" /></td>
                                                        <td>
                                                            <cc2:ButtonGrid ID="btnListadoPersonas" runat="server"
                                                                CommandArgument="<%#Container.DataItemIndex %>" CssClass="btnListado"
                                                                OnClick="btnListadoPersona_Click" Text="..." /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 309px">
                                                            <uc2:ListadoPersonass ID="ctlListadoPersonas" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Persona">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblcodigo" runat="server"
                                                    Text='<%# Bind("cod_persona") %>' Width="45" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombres">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblNombre" runat="server" CssClass="textbox" Enabled="false"
                                                    Text='<%# Bind("nombres") %>' Width="100px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apellidos">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblApellidos" runat="server" CssClass="textbox"
                                                    Enabled="false" Text='<%# Bind("apellidos") %>' Width="100px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ciudad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblCiudad" runat="server" CssClass="textbox" Enabled="false"
                                                    Text='<%# Bind("ciudad") %>' Width="90px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dirección">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblDireccion" runat="server" CssClass="textbox"
                                                    Enabled="false" Text='<%# Bind("direccion") %>' Width="100px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Teléfono">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbltelefono" runat="server" CssClass="textbox" Enabled="false"
                                                    Text='<%# Bind("telefono") %>' Width="90px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Principal">
                                            <ItemTemplate>
                                                <cc2:CheckBoxGrid ID="chkPrincipal" runat="server" AutoPostBack="true"
                                                    Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                                    CommandArgument="<%#Container.DataItemIndex %>"
                                                    OnCheckedChanged="chkPrincipal_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="gridPager" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>

                            </asp:Panel>
                        </td>

                    </tr>

                    <tr>
                        <td style="text-align: left; width: 350px;" colspan="1">Observaciones<br />
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox"
                                Width="350px" />
                        </td>
                        <td style="text-align: left; width: 150px;">&nbsp;</td>
                        <td style="text-align: left;" class="logo">
                            <asp:Label ID="lblEstado" runat="server" Text="Estado de la Cuenta"
                                Width="150px"></asp:Label><br />
                            <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                Width="165px">
                                <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                <asp:ListItem Value="0">Apertura</asp:ListItem>
                                <asp:ListItem Value="1">Activa</asp:ListItem>
                                <asp:ListItem Value="2">Inactiva</asp:ListItem>
                                <asp:ListItem Value="3">Cerrada</asp:ListItem>
                                <asp:ListItem Value="4">Bloqueada</asp:ListItem>
                                <asp:ListItem Value="5">Embargada</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="4">
                            <hr style="text-align: left" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelLibreta" runat="server" Width="62%">

                    <table id="Table1" border="0" cellpadding="1" cellspacing="0">

                        <tr>
                            <td colspan="3" style="text-align: left">
                                <strong>Libreta</strong></td>


                            <tr>
                                <td colspan="3" style="text-align: left">&nbsp;</td>
                                <td style="text-align: left; width: 201px;">Número Libreta<br />
                                    <asp:TextBox ID="txtNumLibreta" runat="server" Enabled="False" Width="120px"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 212px;">Desprendible Desde<br />
                                    <asp:TextBox ID="txtDesprendibleDesde" runat="server" Enabled="False"
                                        Width="120px"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 199px;">Desprendible Hasta<br />
                                    <asp:TextBox ID="txtDesprendibleHasta" runat="server" Enabled="False"
                                        Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                        </tr>

                    </table>
                </asp:Panel>

                <table id="Table2" border="0" cellpadding="1" cellspacing="0" width="60%">
                    <tr>
                        <td style="text-align: left; margin-left: 40px;" colspan="6">

                            <asp:Accordion ID="AccordionNomina" runat="server" SelectedIndex="-1" FadeTransitions="false" SuppressHeaderPostbacks="true"
                                FramesPerSecond="50" Width="779px" TransitionDuration="200" HeaderCssClass="accordionCabecera"
                                ContentCssClass="accordionContenido" Height="731px" Style="margin-right: 6px; font-size: xx-small;">
                                <Panes>
                                    <asp:AccordionPane ID="AccordionPane4" runat="server" ContentCssClass=""
                                        HeaderCssClass="">
                                        <Header>
                                            <center>TASA DE INTERES</center>
                                        </Header>
                                        <Content>
                                            <table>
                                                <tr>
                                                    <td style="text-align: left" colspan="4"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 350px" class="columnForm50">
                                                        <asp:CheckBox ID="cbInteresCuenta" runat="server" Text="Interés por Cuenta" /></td>
                                                    <td style="width: 150px">&#160;</td>
                                                    <td class="logo">
                                                        <asp:CheckBox ID="cbRetencion" runat="server" Text="Cobra Retención Por Cuenta" /></td>
                                                    <td style="width: 71px"></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdD" colspan="4">
                                                        <asp:Panel ID="panelTasa" runat="server">
                                                            <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <caption>
                                                    <hr style="text-align: left" />
                                                </caption>
                                            </table>
                                        </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="AccordionPane3" runat="server" ContentCssClass=""
                                        HeaderCssClass="">
                                        <Header>
                                            <center>FORMA DE PAGO</center>
                                        </Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td
                                                        style="text-align: left">Forma de Pago<br />
                                                        <asp:DropDownList ID="ddlFormaPago" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" runat="server" CssClass="textbox" Width="230px" />
                                                    </td>
                                                    <td>
                                                        <asp:Panel runat="server" Visible="false" ID="pnlEmpresaRecaudo">
                                                            Empresa Recaudo
                                                            <br />
                                                            <asp:DropDownList ID="ddlEmpresaRecaudo" runat="server" CssClass="textbox" Width="230px" />
                                                        </asp:Panel>
                                                    </td>
                                                    <tr>
                                                        <td>Valor de la Cuota<br />
                                                            <uc1:decimales
                                                                ID="txtCuota" runat="server" Enabled="True" Habilitado="True" style="font-size: xx-small; text-align: right"
                                                                TipoLetra="XX-Small" Width_="80" />
                                                        </td>
                                                        <td>Periodicidad<br />
                                                            <ctl:ddlPeriodicidad
                                                                ID="ddlPeriodicidad" runat="server" Requerido="True" />
                                                        </td>
                                                        <td>Fecha Próximo Pago<br />
                                                            <uc1:fecha
                                                                ID="txtProximoPago" runat="server" Enabled="True" Habilitado="True" style="font-size: xx-small; text-align: right"
                                                                TipoLetra="XX-Small" Width_="80" />
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="AccordionPane2" runat="server" ContentCssClass=""
                                        HeaderCssClass="">
                                        <Header>
                                            <center>TARJETAS DE FIRMAS</center>
                                        </Header>
                                        <Content>
                                            <asp:Panel ID="PanelImagenes" runat="server" Width="62%">
                                                <table style="width: 494px" align="left">
                                                    <tr>
                                                      
                                                        <td>
                              
                               </td>


                                                        <td>
                                                            Formatos permitidos  .png", ".jpeg", ".jpg", ".gif", ".bmp"
                                                            <asp:FileUpload ID="fuFoto" runat="server" BorderWidth="0px"
                                                                Font-Size="XX-Small" Height="20px"
                                                                ToolTip="Seleccionar el archivo que contiene la foto" Width="200px" /><asp:HiddenField ID="hdFileName" runat="server" />
                                                            <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                                                            <asp:LinkButton ID="linkBt" runat="server" ClientIDMode="Static"
                                                                OnClick="linkBt_Click" /><br />
                                                            
                                                            <asp:Image ID="imgFoto" runat="server"  Height="151px"
                                                                Width="374px" BorderStyle="Ridge" ImageAlign="Top" onclick="DisplayFullImage(this);"/> 
                                                            
                                                           
                                                                <br />
                                                            <asp:Button ID="btnCargarImagen" runat="server" ClientIDMode="Static"
                                                                Font-Size="xx-Small" Height="20px" OnClick="btnCargarImagen_Click"
                                                                Text="Cargar Imagen" Width="100px" /><asp:TextBox ID="txtNumero_cuenta" runat="server" CssClass="textbox"
                                                                    Enabled="False" MaxLength="80" Visible="False" Width="120px" /></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass=""
                                        HeaderCssClass="">
                                        <Header>
                                            <center>BENEFICIARIOS</center>
                                        </Header>
                                        <Content>
                                            <table
                                                cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left">

                                                        <asp:CheckBox Text="Desea tener beneficiarios" ID="chkBeneficiario" AutoPostBack="true" OnCheckedChanged="chkBeneficiario_CheckedChanged" runat="server" /><br />
                                                        <br />
                                                        <asp:UpdatePanel ID="upBeneficiarios" Visible="false" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnAddRowBeneficio" AutoPostBack="true" runat="server" CssClass="btn8" TabIndex="49" OnClick="btnAddRowBeneficio_Click" Text="+ Adicionar Detalle" />
                                                                <asp:GridView ID="gvBeneficiarios"
                                                                    runat="server" AllowPaging="True" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
                                                                    ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound"
                                                                    OnRowDeleting="gvBeneficiarios_RowDeleting" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
                                                                    Width="80%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                                                        <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("identificacion_ben") %>' Width="100px"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("nombre_ben") %>' Width="260px"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="260px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <uc1:fecha OneventoCambiar="FechaNacimiento_Changed" ID="txtFechaNacimientoBen" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Eval("fecha_nacimiento_ben", "{0:" + FormatoFecha() + "}") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Edad" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtEdadBen" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("edad") %>' Width="100px"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sexo" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList runat="server" ID="ddlsexo">
                                                                                    <asp:ListItem Value="F" Text="Femenino" />
                                                                                    <asp:ListItem Value="M" Text="Masculino" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblidbeneficiario" runat="server" Text='<%# Bind("idbeneficiario") %>'
                                                                                    Visible="false"></asp:Label><asp:Label ID="lblParentezco" runat="server" Text='<%# Bind("parentesco") %>'
                                                                                        Visible="false"></asp:Label><asp:Label ID="lblSexo" runat="server" Text='<%# Bind("sexo") %>'
                                                                                            Visible="false"></asp:Label><cc2:DropDownListGrid ID="ddlParentezco" runat="server"
                                                                                                AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                                                                                Style="font-size: xx-small; text-align: left" Width="120px"></cc2:DropDownListGrid>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="% Ben." ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <uc1:decimalesGridRow ID="txtPorcentaje" runat="server" AutoPostBack_="True" CssClass="textbox"
                                                                                    Enabled="True" Habilitado="True" Text='<%# Eval("porcentaje_ben") %>' Width_="80" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="gridHeader" />
                                                                    <HeaderStyle CssClass="gridHeader" />
                                                                    <RowStyle CssClass="gridItem" />
                                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="pDatos" runat="server" Width="100%">
                    <table id="Table3" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaCierre" runat="server" Text="Fecha Cierre"></asp:Label><br />
                                <uc1:fecha ID="txtFechaCierre" runat="server" Enabled="False" Habilitado="True" style="font-size: xx-small; text-align: right"
                                    TipoLetra="XX-Small" Width_="80" />
                            </td>
                            <td>
                                <asp:Label ID="lblFechaInteres" runat="server" Text="Fecha Interes"></asp:Label><br />
                                <uc1:fecha ID="txtFechaInteres" runat="server" Enabled="False" Habilitado="True"
                                    style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                            </td>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSaldoTotal" runat="server" Text="Saldo Total"></asp:Label><br />
                                <uc1:decimales ID="txtSaldoTotal" runat="server" Enabled="False" />
                            </td>
                            <td>
                                <asp:Label ID="lblSaldoCanje" runat="server" Text="Saldo Canje"></asp:Label><br />
                                <uc1:decimales ID="txtSaldoCanje" runat="server" Enabled="False" />
                            </td>
                            <td>
                                <asp:Label ID="lblSaldoInteres" runat="server" Text="Saldo Interés"></asp:Label><br />
                                <uc1:decimales ID="txtSaldoInteres" runat="server" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>

                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
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
                        <td style="text-align: center; font-size: large;">Apertura del Ahorro
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente
                            <br />
                            Número de Ahorro&nbsp;<asp:Label ID="lblgenerado" runat="server"></asp:Label>
                            <br />
                            <asp:Button ID="btnImprime" runat="server" CssClass="btn8"
                                Text="Desea Imprimir ?" Visible="False" />
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
                          
                        <td style="text-align: left;" colspan="8">
                            &nbsp;</td>

                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
         
    </asp:MultiView>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />

 

</asp:Content>
