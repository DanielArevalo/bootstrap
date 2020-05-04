<%@ Page Title="" Language="C#" MasterPageFile="../../../../General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function buscarProcuraduria() {
            var frame = document.getElementById('cphMain_frmPrint');
            alert(frame);
            var form = window.frames["cphMain_frmPrint"].document.forms[0];
            alert(form);
        }
       function buscarRegistraduria() {
            var frame = document.getElementById('cphMain_frmPrint');
            alert(frame);
            var form = window.frames["cphMain_frmPrint"].document.forms[0];
            alert(form);
        }
        function ActiveTabChanged(sender, e) {
        }
    </script>
    <script>
        function iprint(ptarget) {

            var mywindow = window.open('', 'ptarget', 'height=500,width=500');
            mywindow.document.write('<html><head><title>title</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write($(ptarget).html()); //iFrame data
            mywindow.document.write('</body></html>');

            mywindow.print();
            mywindow.close();
        }
    </script>
        <script type="text/javascript" src="../../../Scripts/jquery-1.8.3.min.js"></script>
        <script type="text/javascript">
        $(document).ready(function() {
	        //$(".boton").click(function(event) {
            //$("#caja").aviso();
	        //});
        });
        setTimeout(function () {
            var num = $("#cphMain_txtIdentificacion").val();
            $("#txtSearchNIT").val(num);
            $("#ContentPlaceHolder1_TextBox1").val(num);
        }, 2000);
        function aviso(){
            $.ajax ({ 
                type : "GET" , 
                encabezados : { "Access-Control-Allow-Origin" : "*" }, 
                url : "https://www.rues.org.co/" });     
                }
        </script>
    <br /><br />
    <asp:MultiView ID="mvPreAnalisiCliente" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwPreAnalisis" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 90%">
                <tr style="text-align: center">
                    <td class="tdI" style="font-weight: 700; text-align: left; height: 24px;"
                        colspan="7">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr style="text-align: center">
                    <td class="tdI" style="font-weight: 700; text-align: center; height: 24px;"
                        colspan="7">
                        <span style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tipo de Solicitud de Crédito</span><br />
                    </td>
                </tr>
                <tr style="text-align: center">
                    <td align="center" class="tdI" style="text-align: center;">&nbsp;</td>
                    <td align="center" class="tdI" style="text-align: center; width: 126px;">&nbsp;</td>
                    <td align="center" class="tdI" style="text-align: center; width: 138px;">&nbsp;</td>
                    <td align="center" style="text-align: center; width: 234px;">
                        <asp:RadioButtonList ID="rblTipoCredito" runat="server"
                            RepeatDirection="Horizontal" Style="text-align: center">
                            <asp:ListItem Selected="True">Consumo</asp:ListItem>
                            <asp:ListItem>Microcrédito</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="center" class="tdI" style="text-align: center;">&nbsp;</td>
                    <td align="center" class="tdI" style="text-align: center;">&nbsp;</td>
                    <td align="center" class="tdI" style="text-align: center;">&nbsp;</td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td class="tdI" style="font-weight: 700; text-align: left; height: 24px;"
                        colspan="7">
                        <hr style="width: 100%" />
                    </td>
                    <td class="tdD" style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdI" style="height: 27px; text-align: center;" colspan="2">&nbsp;</td>
                    <td class="tdI" style="height: 27px; text-align: center;" colspan="2"
                        valign="bottom">
                        <strong>Datos del Cliente  </strong></td>                    
                    <td class="tdI" colspan="2" style="height: 27px; text-align: right;">
                        <asp:ImageButton ID="btnListasrestrictivas" runat="server"
                            ImageUrl="~/Images/btnConsultar.jpg" OnClick="btnListasRestrictivas_Click"
                            ValidationGroup="vgGuardar" />
                    </td>
                    <td class="tdI" style="height: 27px; text-align: center;"></td>
                </tr>
                <tr>
                    <td class="tdD" style="height: 36px; width: 148px;" valign="top">Tipo Identificación<br />
                        <asp:DropDownList ID="ddlIdentificacion" runat="server"
                            CssClass="textbox" Width="199px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdD" style="height: 36px; width: 148px;" valign="top">Identificación *<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                            MaxLength="128" Width="196px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtIdentificacion" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            Style="font-size: x-small" ValidationGroup="vgGuardar" />
                    </td>
                    <td class="tdD" style="height: 36px;" colspan="2"></td>
                </tr>
                <tr>
                    <td class="tdI" valign="top" style="width: 148px; text-align: left">Primer Nombre *<br />
                        <asp:TextBox ID="txtPrimer_nombre" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                            MaxLength="128" Width="200px" />

                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtPrimer_nombre" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: x-small" />
                    </td>
                    <td class="tdI" style="width: 171px; text-align: left" valign="top" colspan="2">Segundo Nombre<br />
                        <asp:TextBox ID="txtSegundo_nombre" runat="server" CssClass="textbox" MaxLength="128" Width="196px" Style="text-transform: uppercase" />

                    </td>
                    <td class="tdD" valign="top" style="width: 171px; text-align: left" colspan="2">Primer Apellido *<br />
                        <asp:TextBox ID="txtPrimer_apellido" runat="server" Style="text-transform: uppercase"
                            CssClass="textbox" MaxLength="128" Width="230px"
                            OnTextChanged="txtPrimer_apellido_TextChanged" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="txtPrimer_apellido" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: x-small" />
                    </td>
                    <td class="tdD" valign="top" width="171px" style="text-align: left">Segundo Apellido<br />
                        <asp:TextBox ID="txtSegundo_apellido" runat="server" Style="text-transform: uppercase"
                            CssClass="textbox" MaxLength="128" Width="200px" />
                        <br />
                    </td>
                    <td class="tdD">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3" width="342px" style="text-align: left">E-mail<br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                            MaxLength="136" Width="410px" />
                    </td>
                    <td class="tdD" colspan="3" width="342px" style="text-align: left">
                        <br />
                    </td>
                    <td class="tdD" style="height: 31px"></td>
                </tr>
                <tr>
                    <td class="tdI" colspan="7" style="text-align: left; width: 684px;"
                        width="342px">
                        <hr style="width: 100%" />
                    </td>
                    <td class="tdD" style="height: 31px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdI" style="height: 27px; text-align: center;" colspan="2">&nbsp;</td>
                    <td class="tdI" style="height: 27px; text-align: center;" colspan="2"
                        valign="bottom">
                        <strong>Datos del Negocio/Empresa</strong></td>
                    <td class="tdI" colspan="2" style="height: 27px; text-align: right;">&nbsp;</td>
                    <td class="tdI" style="height: 27px; text-align: center;"></td>
                </tr>
                <tr>
                    <td class="tdD" colspan="3" style="text-align: left">Razón Social<br />
                        <asp:TextBox ID="txtRazon_social" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                            MaxLength="128" Width="410px" />
                    </td>
                    <td class="tdD" colspan="2" style="text-align: left">Celular<br />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox"
                            MaxLength="128" Width="230px" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txtTelefono" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: x-small" InitialValue="Ingrese un numero de celular"
                            ToolTip="Campo Requerido" />
                    </td>
                    <td class="tdD" style="text-align: left">Actividad
                        <asp:DropDownList ID="ddlActividad" runat="server" CssClass="textbox"
                            Width="200px" AppendDataBoundItems="True">
                            <asp:ListItem>Seleccione un Item</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvActividad" runat="server"
                            ControlToValidate="ddlActividad" Display="Dynamic"
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: x-small" InitialValue="Seleccione un Item"
                            ToolTip="Campo Requerido" />
                    </td>
                    <td class="tdD">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdI" colspan="7" style="text-align: left">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>                    
                    <td class="tdI" colspan="7" style="text-align: left">Dirección
                        <asp:panel runat="server">
                            <uc1:direccion ID="txtDireccion" runat="server" />
                        </asp:panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="7" style="text-align: left; width: 684px;"
                        width="342px">
                        <hr style="width: 100%" />
                    </td>
                    <td class="tdD" style="height: 31px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdI" style="height: 27px; text-align: center;" colspan="1">&nbsp;</td>
                    <td class="tdI" style="height: 27px; text-align: center;" colspan="4"
                        valign="bottom">
                        <strong>Datos del producto  </strong></td>
                    <td class="tdI" colspan="1" style="height: 27px; text-align: right;">&nbsp;</td>
                    <td class="tdI" style="height: 27px; text-align: center;"></td>
                </tr>
                <tr>
                    <td class="tdD" style="height: 36px; width: 148px;" valign="top">Categoría<br />
                        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true"
                            CssClass="textbox" Width="199px" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                            <asp:ListItem>Seleccione un Item</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdD" style="height: 36px; width: 148px;" valign="top">Producto *<br />
                        <asp:DropDownList ID="ddlProducto" runat="server"
                            CssClass="textbox" Width="199px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdD" style="height: 36px;" colspan="2"></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwConfirmacion" runat="server" EnableViewState="False">
            <table align="right" style="width: 100%">
                <tr>
                    <td style="text-align: center"></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTituloConfirmacion" runat="server" Text="Confirmación"
                            Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center"></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTextoConfirmacion" runat="server" Width="600px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center"></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:ImageButton ID="btnCancelarConfirmacion" runat="server"
                            ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelarConfirmacion_Click" />
                        <asp:ImageButton ID="btnAceptarConfirmacion" runat="server"
                            ImageUrl="~/Images/btnAceptar.jpg" OnClick="btnAceptarConfirmacionPago_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwConfirmacionPago" runat="server" EnableViewState="False">
            <table align="right" style="width: 100%">
                <tr>
                    <td colspan="3" style="text-align: center"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="lblTituloConfirmacionPago" runat="server"
                            Text="Confirmación Pago" Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center"></td>
                </tr>
                <tr>
                    <td style="text-align: center">Primer Nombre
                        <asp:TextBox ID="txtPrimer_nombre0" runat="server" CssClass="textbox"
                            MaxLength="128" Width="187px" />
                    </td>
                    <td style="text-align: center">Primer Apellido
                        <asp:TextBox ID="txtPrimer_apellido0" runat="server" CssClass="textbox"
                            MaxLength="128" Width="192px" />
                    </td>
                    <td style="text-align: center">Identificación<asp:TextBox ID="txtIdentificacion0" runat="server"
                        CssClass="textbox" MaxLength="128" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Label ID="lblTextoConfirmacionPago" runat="server" Width="600px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:ImageButton ID="btnCancelarConfirmacionPago" runat="server"
                            ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelarConfirmacion_Click" />
                        <asp:ImageButton ID="btnAceptarConfirmacionPago" runat="server"
                            ImageUrl="~/Images/btnAceptar.jpg"
                            OnClick="btnAceptarConfirmacionCentrales" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReciboPago" runat="server">
            <asp:Panel ID="Panel2" runat="server" Visible="false">
                <table width="500px" align="center">
                    <tr>
                        <td class="style1">
                            <asp:GridView ID="gvInforme" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:GridView ID="gvNaturalNacional" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:GridView ID="gvIdentificacion" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:GridView ID="gvEdad" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:GridView ID="gvScore" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:GridView ID="gvRazon" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <rsweb:ReportViewer ID="rvReciboPago" runat="server" Font-Names="Verdana"
                            Font-Size="8pt" Height="550px" InteractiveDeviceInfos="(Colección)"
                            ShowBackButton="False" ShowFindControls="False"
                            ShowPageNavigationControls="False" ShowRefreshButton="False"
                            ShowZoomControl="False" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="690px">
                            <LocalReport ReportPath="Page\FabricaCreditos\ConsultaDatacredito\DatosCliente\ReciboPago.rdlc"></LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="btnConsultarResultado" runat="server"
                            ImageUrl="~/Images/btnConsultar.jpg" OnClick="btnConsultarResultado_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwResultadoCentrales" runat="server">
            <table align="right" style="width: 100%">
                <tr>
                    <td colspan="2" style="text-align: center"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblResultadoCentrales" runat="server" Text="RESULTADO CENTRALES RIESGO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblTextoResultadoCentrales" runat="server" Width="400px"
                            Height="24px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:ImageButton ID="CancelarResultadoCentrales0" runat="server"
                            ImageUrl="~/Images/btnCancelar.jpg"
                            OnClick="CancelarResultadoCentrales_Click" />
                        <asp:ImageButton ID="btnAceptarResultadoCentrales0" runat="server"
                            ImageUrl="~/Images/btnAceptar.jpg"
                            OnClick="btnAceptarResultadoCentrales_Click" />

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="MensajesError" runat="server">
            <table align="right" style="width: 100%">
                <tr>
                    <td style="text-align: center">


                        <asp:Label ID="lblMensajeError" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:ImageButton ID="btnAceptarMensaje" runat="server"
                            ImageUrl="~/Images/btnAceptar.jpg" OnClick="btnAceptarMensaje_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwListasRestrictivas" runat="server">
            <asp:Panel ID="panelRiesgo" runat="server" Visible="true" Height="100%">
                <br />
                <table>
                    <tr>
                        <td style="text-align: left">Identificación
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="30px" Visible="false" Style="text-align: left" />
                            <asp:TextBox ID="txtIdentificacion2" runat="server" CssClass="textbox" Width="110px" Style="text-align: left" Enabled="False" />
                        </td>
                        <td style="text-align: left">Tipo de Identificación
                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="150px" Style="text-align: left" Enabled="false" />
                        </td>
                        <td style="text-align: left">Tipo de Persona
                    <asp:TextBox ID="txtTipoPersona" runat="server" CssClass="textbox" Width="150px" Style="text-align: left" Enabled="false" />
                        </td>
                        <td style="text-align: left">Nombres
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="300px" Style="text-align: left" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right">   &nbsp;&nbsp;
                            <asp:ImageButton ID="btnGuardar" runat="server" Style="padding: 0 10px 0 0"
                            ImageUrl="~/Images/btnConsultarDatacredito.jpg" OnClick="btnGuardar_Click"
                            ValidationGroup="vgGuardar" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle"
                    OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 1px" Width="100%">
                    <asp:TabPanel ID="tabRegistraduria" runat="server" HeaderText="Datos" >
                        <HeaderTemplate>
                            Registraduria
                            <asp:Image ID="btnCheckR" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('R', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisR" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('R', 'Y')"/>
                            <asp:CheckBox ID="chkR" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe2" name="IframeRegistraduria" width="100%" frameborder="0" src="https://wsp.registraduria.gov.co/certificado/Datos.aspx" target="_top"
                                height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
                           <%-- <div id="caja">
                                </div>
                            <a href="" onclick="buscarRegistraduria()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabProcuraduria" runat="server" HeaderText="Datos" >
                        <HeaderTemplate>
                            Procuraduria
                            <asp:Image ID="btnCheckP" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('P', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisP" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('P', 'Y')"/>
                            <asp:CheckBox ID="chkP" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe5" name="IframeProcuraduria" width="100%" frameborder="0" src="https://www.procuraduria.gov.co/portal/consulta_antecedentes.page" target="_top"
                                height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
                           <%-- <div id="caja">
                                </div>
                            <a href="" onclick="buscarRegistraduria()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabContraloria" runat="server" HeaderText="Datos" >
                        <HeaderTemplate>
                            <a style="text-decoration:none;" target="_blank" href="https://www.contraloria.gov.co/control-fiscal/responsabilidad-fiscal/control-fiscal/responsabilidad-fiscal/certificado-de-antecedentes-fiscales/persona-natural">Contraloria</a>
                            <asp:Image ID="btnCheckC" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('C', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisC" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('C', 'Y')"/>
                            <asp:CheckBox ID="chkC" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                           <%-- <div id="caja">
                                </div>
                            <a href="" onclick="buscarRegistraduria()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabRues" runat="server" HeaderText="Datos">
                        <HeaderTemplate>
                            RUES
                            <asp:Image ID="btnCheckU" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('U', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisU" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('U', 'Y')"/>
                            <asp:CheckBox ID="chkU" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe1" name="IframeRues" width="100%" frameborder="0" src="https://www.rues.org.co/RUES_Web/Consultas/Consultasext"
                                height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
 <%--                           <a href="" onclick="buscarRues()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabPolicia" runat="server" HeaderText="Datos">
                        <HeaderTemplate>
                            POLICIA
                            <asp:Image ID="btnCheckL" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('L', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisL" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('L', 'Y')"/>
                            <asp:CheckBox ID="chkL" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe3" name="Iframepolicia" width="100%" frameborder="0" src="https://antecedentes.policia.gov.co:7005/WebJudicial/antecedentes.xhtml"
                                height="600px" runat="server" style="border-style: groove; float: left;" tabindex="2" allowfullscreen="true"></iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabOFAQ" runat="server" HeaderText="Datos">
                        <HeaderTemplate>OFAC</HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="lblOFAC" runat="server" Text="Listas reportadas" Style="text-align: center; font-weight: bold" Visible="false" /><br />
                            <asp:GridView ID="gvListaOFAC" runat="server" AllowPaging="True" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" OnRowDataBound="gvLista_RowDataBound"
                                ForeColor="Black" PageSize="10" ShowFooter="True" OnPageIndexChanging="gvListaOFAC_PageIndexChanging"
                                Style="font-size: x-small" Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="source" HeaderText="Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="name" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="alt_name" HeaderText="Alias" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="address" HeaderText="Direcciones" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="date_of_birth" HeaderText="Fechas de nacimiento" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="nationality" HeaderText="Nacionalidades" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="place_of_birth" HeaderText="Lugares de Nacimiento" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="citizenship" HeaderText="Ciudadanias" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="program" HeaderText="Programas" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Coincidencia">
                                        <ItemTemplate>
                                            <asp:Image ID="btnCheck" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" Visible="false" />
                                            <asp:Image ID="btnEquis" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
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
                            <asp:Label ID="lblResultadosOFAC" runat="server" Text="" Style="text-align: center;" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabONU" runat="server" HeaderText="Datos">
                        <HeaderTemplate>ONU</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <asp:GridView ID="gvONUIndividual" runat="server" AllowPaging="false" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black" ShowFooter="false" HorizontalAlign="Center"
                                Style="font-size: x-small" Width="95%" OnRowDataBound="gvLista_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="dataid" HeaderText="Código" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="first_name" HeaderText="Nombres" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="un_list_type" HeaderText="Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="listed_on" HeaderText="Fecha de Ingreso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="comments1" HeaderText="Comentarios" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="designation" HeaderText="Designación" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="nationality" HeaderText="Nacionalidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="list_type" HeaderText="Tipo de Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Coincidencia">
                                        <ItemTemplate>
                                            <asp:Image ID="btnCheck" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" />
                                            <asp:Image ID="btnEquis" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
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
                            <asp:GridView ID="gvONUEntidad" runat="server" AllowPaging="false" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black" ShowFooter="True"
                                Style="font-size: x-small" Width="90%" OnRowDataBound="gvLista_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="dataid" HeaderText="Código" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="first_name" HeaderText="Nombres" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="un_list_type" HeaderText="Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="listed_on" HeaderText="Fecha de Ingreso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="comments1" HeaderText="Comentarios" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="country" HeaderText="País" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="city" HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Coincidencia">
                                        <ItemTemplate>
                                            <asp:Image ID="btnCheck" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" />
                                            <asp:Image ID="btnEquis" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
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
                            <br />
                            <asp:Label ID="lblOnu" runat="server" Text="" Style="text-align: center;" />
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>


    <uc4:mensajegrabar ID="ctlCentrales" runat="server" />

    <script>
        function checkList(tab, visible) {
            var iconY = "";
            var iconN = "";
            var p1 = tab;
            var p2 = "";
            switch(tab){
                case "R":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_tabRegistraduria_btnCheckR.id;
                        iconN = cphMain_Tabs_tabRegistraduria_btnEquisR.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_tabRegistraduria_btnEquisR.id;
                        iconN = cphMain_Tabs_tabRegistraduria_btnCheckR.id;
                    }
                    var v = $("#cphMain_Tabs_tabRegistraduria_btnCheckR").is(":visible");
                    if(v){
                        $("#<%=chkR.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkR.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "P":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_TabProcuraduria_btnCheckP.id;
                        iconN = cphMain_Tabs_TabProcuraduria_btnEquisP.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_TabProcuraduria_btnEquisP.id;
                        iconN = cphMain_Tabs_TabProcuraduria_btnCheckP.id;
                    }
                    var v = $("#cphMain_Tabs_TabProcuraduria_btnCheckP").is(":visible");
                    if(v){
                        $("#<%=chkP.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkP.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "U":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_tabRues_btnCheckU.id;
                        iconN = cphMain_Tabs_tabRues_btnEquisU.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_tabRues_btnEquisU.id;
                        iconN = cphMain_Tabs_tabRues_btnCheckU.id;
                    }
                    var v = $("#cphMain_Tabs_tabRues_btnCheckU").is(":visible");
                    if(v){
                        $("#<%=chkU.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkU.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "L":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_tabPolicia_btnCheckL.id;
                        iconN = cphMain_Tabs_tabPolicia_btnEquisL.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_tabPolicia_btnEquisL.id;
                        iconN = cphMain_Tabs_tabPolicia_btnCheckL.id;
                    }
                    var v = $("#cphMain_Tabs_tabPolicia_btnCheckL").is(":visible");
                    if(v){
                        $("#<%=chkL.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkL.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "C":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_TabContraloria_btnCheckC.id;
                        iconN = cphMain_Tabs_TabContraloria_btnEquisC.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_TabContraloria_btnEquisC.id;
                        iconN = cphMain_Tabs_TabContraloria_btnCheckC.id;
                    }
                    var v = $("#cphMain_Tabs_TabContraloria_btnCheckC").is(":visible");
                    if(v){
                        $("#<%=chkC.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkC.ClientID%>").attr('checked', 'checked'); }
                    break;
            }
            $("#" + iconY).show();
            $("#" + iconY).attr('onclick', 'checkList("'+p1+'", "'+p2+'")');
            $("#" + iconN).hide();

        }
    </script>
</asp:Content>
