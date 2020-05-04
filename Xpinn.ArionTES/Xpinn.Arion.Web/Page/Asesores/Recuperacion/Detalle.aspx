<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle"
    MasterPageFile="~/General/Master/site.master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; margin-right: 10px;">
        <tr>
            <td colspan="6" style="text-align: center; height: 9px;">
                <span style="color: #0099FF;">
                    <strong style="text-align: center; background-color: #FFFFFF;">
                        <asp:Label ID="LblReportes" runat="server" Style="color: #0099FF; font-weight: 700;"></asp:Label>
                        <asp:Button runat="server" ID="BtnAgenda" Text="Agenda" OnClick="BtnAgenda_Click"
                            OnClientClick="NewWindow();" CssClass="btn8" Style="text-align: right" />
                    </strong>
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <div style="float: left; color: #FFFFFF; font-size: small">
                    DATOS DEL CLIENTE:
                </div>
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="Label3" runat="server">(Mostrar Detalles...)</asp:Label>
                </div>
                <div style="float: right; vertical-align: middle;">
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">Código<br />
                <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" Width="89px"
                    Enabled="False" Height="15px"></asp:TextBox>
            </td>
            <td style="text-align: left;">No. Identificación<br />
                <asp:TextBox ID="txtIdentificacionCliente" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
            </td>
            <td style="text-align: left;" colspan="3">Nombres y Apellidos<br />
                <asp:TextBox ID="txtPrimerNombreCliente" runat="server" CssClass="textbox" Width="325px"
                    Enabled="False" Height="16px"></asp:TextBox>
            </td>
            <td style="text-align: left;">Ciudad<br />
                <asp:TextBox ID="txtCiudad" runat="server" CssClass="textbox" Width="220px"
                    Enabled="False" Height="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" colspan="2">Dirección<br />
                <asp:TextBox ID="txtDireccionCliente" runat="server" CssClass="textbox" Width="220px"
                    Enabled="False" Height="15px"></asp:TextBox>
            </td>
            <td style="text-align: left;">Teléfono<br />
                <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="textbox" Width="158px"
                    Enabled="False" Height="15px"></asp:TextBox>
            </td>
            <td style="text-align: left;">E-mail<br />
                <asp:TextBox ID="txtEmailCliente" runat="server" CssClass="textbox" Width="216px"
                    Enabled="False" Height="15px"></asp:TextBox>
            </td>
            <td style="text-align: left;" colspan="2">Barrio<br />
                <asp:TextBox ID="txtBarrioCliente" runat="server" CssClass="textbox" Width="220px"
                    Enabled="False" Height="15px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <asp:Panel ID="pEncCredito" runat="server" CssClass="collapsePanelHeader">
                    <div style="float: left; color: #FFFFFF; font-size: small">
                        DATOS DEL CREDITO:
                    </div>
                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    </div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="imgExpandCredito" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Panel ID="pDetCredito" runat="server" Width="100%">
                    <table style="width: 100%; margin-right: 0px;">
                        <tr style="padding: 5px; margin: 5px auto;">
                            <td style="text-align: left;">No. Crédito<br />
                                <asp:TextBox ID="txtIdCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Pagaré<br />
                                <asp:TextBox ID="txtpagare" runat="server" CssClass="textbox" Width="120px" />
                            </td>
                            <td style="text-align: left;" colspan="2">Línea<br />
                                <asp:TextBox ID="txtLineaCredito" runat="server" CssClass="textbox" Width="240px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Fecha solicitud<br />
                                <asp:TextBox ID="txtFechaSolicitudCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Monto crédito<br />
                                <asp:TextBox ID="txtMontoCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="2">Ejecutivo
                                <asp:DropDownList ID="ddlUsuarios" runat="server" Enabled="False" CssClass="textbox" Width="16px"
                                    Visible="False">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox ID="txtAsesor" runat="server" CssClass="textbox" Width="240px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Plazo<br />
                                <asp:TextBox ID="txtPlazoCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                                <asp:TextBox ID="txtdiapago" runat="server" CssClass="textbox" Width="45px" Enabled="False" Visible="False" />
                            </td>
                            <td style="text-align: left;">Cuota<br />
                                <asp:TextBox ID="txtCuotaCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Cuotas pagadas<br />
                                <asp:TextBox ID="txtCuotasPagadasCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Calificación<br />
                                <asp:TextBox ID="txtCalificacionCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Dias Mora<br />
                                <asp:TextBox ID="txtdiasmora" runat="server" CssClass="textbox" Width="120px" Enabled="False"
                                    Height="15px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">Fecha terminación<br />
                                <asp:TextBox ID="txtFechaTerminacionCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Fecha último pago<br />
                                <asp:TextBox ID="txtFechaUltimoPagoCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Último valor pagado<br />
                                <asp:TextBox ID="txtUltimoValorPagadoCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Fecha próximo pago<br />
                                <asp:TextBox ID="txtFechaProximoPagoCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Saldo<br />
                                <asp:TextBox ID="txtSaldoCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Saldo en mora<br />
                                <asp:TextBox ID="txtSaldoMoraCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                                <asp:Button ID="btnInfo" runat="server" Text=".." CssClass="btn8" Height="23px" />
                                <asp:BalloonPopupExtender ID="btnInfo_BalloonPopupExtender" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="btnInfo" BalloonPopupControlID="Panel2"
                                    Position="TopRight" BalloonStyle="Rectangle" BalloonSize="Medium" UseShadow="true"
                                    ScrollBars="Auto" DisplayOnMouseOver="true" DisplayOnFocus="false" DisplayOnClick="false">
                                </asp:BalloonPopupExtender>
                            </td>
                            <td style="text-align: left;">Valor a pagar<br />
                                <asp:TextBox ID="txtValorPagarCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;">Valor total a pagar<br />
                                <asp:TextBox ID="txtValorTotalPagarCredito" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                            </td>
                            <td style="text-align: left;"></td>
                            <asp:Image ID="imagen1" runat="server" Visible="false" src="#" />
                        </tr>
                    </table>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pDetCredito"
                    ExpandControlID="pEncCredito" CollapseControlID="pEncCredito" Collapsed="False"
                    ImageControlID="imgExpandCredito" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                    CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <asp:Panel ID="pEncProceso" runat="server" CssClass="collapsePanelHeader">
                    <div style="float: left; color: #FFFFFF; font-size: small">
                        DATOS DEL PROCESO:
                    </div>
                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                        <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                    </div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Panel ID="pDetProceso" runat="server" Width="100%">
                    <table style="width: 100%; margin-right: 0px;">
                        <tr>
                            <td style="text-align: center; font-size: small;" width="25%">Proceso<br />
                                <asp:TextBox ID="txtProceso" runat="server" CssClass="textbox" Width="97%" Enabled="False" />
                            </td>
                            <td style="text-align: center;" width="20%">Negociador<br />
                                <asp:DropDownList ID="ddlAbogado" runat="server" CssClass="textbox" Width="97%" Enabled="False" />
                            </td>
                            <td style="text-align: center;" width="20%">Motivo<br />
                                <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" Width="97%" Enabled="False" />
                            </td>
                            <td style="text-align: center;" width="20%">Ciudad Juzgado<br />
                                <asp:DropDownList ID="ddlCiudadJuzg" runat="server" CssClass="textbox" Width="97%" Enabled="False" />
                            </td>
                            <td style="text-align: center;" width="15%">
                                <br />
                                <asp:Button ID="BntCambiar" runat="server" Text="Cambiar Neg" CssClass="btn8" OnClick="btnCambiar_Click" Height="23px" />
                            </td>
                            <td style="text-align: center;" width="15%">
                                <br />
                                <asp:Button ID="btnInfo1" runat="server" Text="Gestionar Proceso" CssClass="btn8" OnClick="btnInfo1_Click" Height="23px" />
                                <asp:ModalPopupExtender ID="mpeNuevo" runat="server" PopupControlID="panelActividadReg"
                                    TargetControlID="btnInfo1" BackgroundCssClass="backgroundColor">
                                </asp:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblCambio" runat="server" Style="color: blue; font-weight: 700;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; clear: both; margin: auto;" colspan="6">Observaciones<br />
                                <asp:TextBox ID="txtobseprocesos" runat="server" CssClass="textbox" Width="98%" Enabled="False" /><br />
                                <asp:Label ID="Label1" runat="server" Style="color: #0099FF; font-weight: bold;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpeProceso" runat="Server" TargetControlID="pDetProceso"
                    ExpandControlID="pEncProceso" CollapseControlID="pEncProceso" Collapsed="False"
                    TextLabelID="lblMostrarDetalles" ImageControlID="ImageButton3" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                    CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <div style="float: left; color: #FFFFFF; font-size: small">
                    CODEUDORES
                </div>
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                </div>
                <div style="float: right; vertical-align: middle;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="gvListaCodeudores" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="IdCliente" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                    OnPageIndexChanging="gvListaCodeudores_PageIndexChanging" PagerStyle-CssClass="gridPager"
                    PageSize="3" RowStyle-CssClass="gridItem" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="IdCliente" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo identificación" />
                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Identificación" />
                        <asp:BoundField DataField="PrimerNombre" HeaderText=" Primer nombre" />
                        <asp:BoundField DataField="SegundoNombre" HeaderText=" Segundo nombre" />
                        <asp:BoundField DataField="PrimerApellido" HeaderText=" Primer apellido" />
                        <asp:BoundField DataField="SegundoApellido" HeaderText=" Segundo apellido" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Email" HeaderText="E-mail" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs0" runat="server" Visible="False" Style="font-size: x-small" />
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <div style="float: left; color: #FFFFFF; font-size: small">
                    DILIGENCIAS:
                </div>
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                </div>
                <div style="float: right; vertical-align: middle;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" Text="Agregar diligencia"
                    OnClick="btnAgregar_Click1" Width="140px" Height="22px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnActualizar" runat="server" CssClass="btn8" Text="Actualizar Diligencias"
                    OnClick="btnActualizar_Click" Width="140px" Height="22px" />
                <br />
                <asp:GridView ID="gvListaDiligencias" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="codigo_diligencia" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                    OnPageIndexChanging="gvListaDiligencias_PageIndexChanging" OnSelectedIndexChanged="gvListaDiligencias_SelectedIndexChanged"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="97%" ViewStateMode="Enabled"
                    AllowPaging="True" PageSize="8">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo2" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Height="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fecha_diligencia" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="tipo_diligencia_consulta" HeaderText="Tipo Diligencia" />
                        <asp:BoundField DataField="tipo_contacto_consulta" HeaderText="Tipo Contacto" />
                        <asp:BoundField DataField="atendio" HeaderText="Quien atendió?" />
                        <asp:BoundField DataField="respuesta" HeaderText="Respuesta" />
                        <asp:BoundField DataField="acuerdo_consulta" HeaderText="Acuerdo" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="observacion" HeaderText="Observacion" />
                        <asp:BoundField DataField="Num_Celular" HeaderText="Num Celular" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Style="font-size: x-small" />&nbsp;
                <asp:Label ID="LblMensajeConsulta" runat="server" Style="color: #0099FF; font-weight: 700; font-size: x-small;" />
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <div style="float: left; color: #FFFFFF; font-size: small">
                    CITACIONES:
                </div>
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                </div>
                <div style="float: right; vertical-align: middle;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; margin: auto;">
                <asp:Panel ID="Panel5" runat="server" HorizontalAlign="Center" Width="97%">
                    <div style="text-align: Center; margin: auto;">
                        <asp:Label ID="LabelFecha_citacion" runat="server" Text="Fecha Citación"></asp:Label>
                        &nbsp;(dia/mes/año)&nbsp;&nbsp;
                        <asp:TextBox ID="txtFechaCitacion" runat="server" CssClass="textbox" MaxLength="10" Width="106px">01/01/0001</asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                            TargetControlID="txtFechaCitacion">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnGuardarCitacion" runat="server" CssClass="button" Height="20px"
                            OnClick="btnGuardarCitacion_Click" Style="margin: auto;" Text="Guardar"
                            EnableTheming="True" OnClientClick="deshabilitar(this.id)" />
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; height: 9px; background-color: #0099FF">
                <div style="float: left; color: #FFFFFF; font-size: small">
                    INFORMES Y CARTAS:
                </div>
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                </div>
                <div style="float: right; vertical-align: middle;">
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="6" class="style6" style="display: block; margin: auto; text-align: center; width: 100%; background-color: #FFFFFF">



                <asp:DropDownList ID="ddlTipoDoc" runat="server" AutoPostBack="True" CssClass="dropdown"
                    OnSelectedIndexChanged="ddlTipoDoc_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="LblMensaje" runat="server" Style="color: #0099FF; font-weight: 700; font-size: x-small;"></asp:Label>
                <asp:Label ID="LblCartas" runat="server" Style="color: #0099FF; font-weight: 700;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 437px">
                <br />
                <asp:MultiView ID="mvReporte" runat="server">
                    <asp:View ID="vReporte" runat="server">
                        <asp:Button ID="BtnCerrarInforme" runat="server" CssClass="btn8"
                            OnClick="BtnCerrarInforme_Click" Text="Cerrar Informe" BackColor="#0099FF"
                            ForeColor="white" Height="24px" />
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" Width="100%" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="361px">
                            <LocalReport ReportPath="Page\Asesores\Recuperacion\ReporteDetalle.rdlc"></LocalReport>
                        </rsweb:ReportViewer>
                        <br />
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="5" style="height: 267px">
                <asp:ModalPopupExtender ID="mpeNuevoActividad" runat="server" PopupControlID="PanelActividadRegistro"
                    TargetControlID="btnAgregar" BackgroundCssClass="backgroundColor">
                </asp:ModalPopupExtender>
                <asp:ModalPopupExtender ID="mpeConsultarActividad" runat="server" PopupControlID="PanelActividadConsulta"
                    TargetControlID="HiddenField3" BackgroundCssClass="backgroundColor">
                </asp:ModalPopupExtender>
               <%-- <asp:AnimationExtender ID="popUpAnimationConsultaActividad" runat="server" TargetControlID="HiddenField3">
                    <Animations>
                 <OnClick>
                  <Parallel AnimationTarget="PanelActividadConsulta" 
                    Duration=".3" Fps="25">
                    <Move Horizontal="800" Vertical="800" />
                    <Resize Width="800" Height="800" />
                      <Color PropertyKey="backgroundColor" 
                      StartValue="#FFFFFF" 
                      EndValue="#FFFF00" />
                  </Parallel>                    
                </OnClick>
                    </Animations>
                </asp:AnimationExtender>--%>
                <asp:Panel ID="Panel3" runat="server" Style="margin-bottom: 0px">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <br />
                    <table style="width: 100%; height: 231px; margin-bottom: 0px;">
                        <tr>
                            <td style="height: 205px">
                                <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: left">
                                    <asp:UpdatePanel ID="upActividadReg" runat="server">
                                        <ContentTemplate>
                                            <div id="popupcontainer" cssclass="clasdiv" style="border: medium groove #0000FF; width: 312px; background-color: #FFFFFF; padding: 22px">
                                                <div class="row popupcontainertitle">
                                                    <div class="gridHeader" style="text-align: center">
                                                        Registro de&nbsp; Actividad&nbsp; al Proceso
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell popupcontainercell">
                                                        <div id="ordereditcontainer">
                                                            <div class="row">
                                                                <div class="cell ordereditcell" style="height: 44px; width: 304px">
                                                                    Radicación<br />
                                                                    <asp:TextBox ID="txtRadicado" runat="server" CssClass="textbox" Enabled="False" MaxLength="128"
                                                                        Width="112px" />
                                                                    <br />
                                                                    <div class="cell ordereditcell" style="height: 7px; width: 304px">
                                                                    </div>
                                                                </div>
                                                                <div class="cell" style="width: 258px">
                                                                    <strong><span style="font-size: medium">Proceso</span></strong><br />
                                                                    <asp:DropDownList ID="ddlProceso" runat="server" AutoPostBack="True" CssClass="dropdown"
                                                                        OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged" Width="282px">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell ordereditcell">
                                                                    Negociador&nbsp; Asignado
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:DropDownList ID="ddlUsuario" runat="server" AutoPostBack="True" CssClass="dropdown"
                                                                        Width="282px" Enabled="False">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="cell ordereditcell">
                                                                    Motivo Cambio
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="dropdown" Width="282px">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    No. juzgado
                                                                </div>
                                                                <div class="cell" style="text-align: left">
                                                                    <asp:TextBox ID="txtJuzgado" runat="server" CssClass="textbox" Enabled="False" MaxLength="128"
                                                                        Width="279px" />
                                                                    <div class="cell">
                                                                        Ciudad Proceso Judicial<asp:DropDownList ID="ddlCiudadJuzgado" runat="server" CssClass="dropdown"
                                                                            Width="286px">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="cell" style="text-align: left; padding-left: 0px;">
                                                                    Observaciones<div class="cell">
                                                                        <asp:TextBox ID="txtobservaciones" runat="server" CssClass="textbox" Enabled="True" Width="90%"
                                                                            Height="111px" MaxLength="128">0</asp:TextBox>
                                                                        <br />
                                                                        <asp:Button ID="btnGuardarRegProce" runat="server" CssClass="button" Height="20px"
                                                                            OnClick="btnGuardarRegProce_Click" PostBackUrl="~/Page/Asesores/Recuperacion/Detalle.aspx"
                                                                            Style="margin-right: 10px;" Text="Guardar" ValidationGroup="vgActividadReg" />
                                                                        <asp:Button ID="btnCloseReg1" runat="server" CausesValidation="false" CssClass="button"
                                                                            Height="20px" OnClick="btnCloseReg1_Click" Text="Cerrar" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelActividadConsulta" runat="server">
                        <asp:HiddenField ID="HiddenField3" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanelConsulta" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAbrirAnexo" />
                            </Triggers>
                            <ContentTemplate>
                                <table border="0" cellpadding="5" cellspacing="0" style="border: medium groove #0000FF; width: 400px; background-color: #FFFFFF; height: 386px; margin-right: 5px;">
                                    <tr>
                                        <td class="tdI" colspan="3" style="text-align: left">
                                            <div class="gridHeader" style="height: 22px; width: 100%; text-align: center;">
                                                CONSULTA ACTIVIDAD
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; margin-left: 40px;">Código<br />
                                            <asp:TextBox ID="txtCodigo_diligencia" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="128" Width="60px" />
                                        </td>
                                        <td style="text-align: left; margin-left: 40px;">Número Radicación<br />
                                            <asp:TextBox ID="txtNumero_radconsulta" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="128" Width="140px" />
                                        </td>
                                        <td style="text-align: left">Fecha Diligencia<br />
                                            <asp:TextBox ID="txtFecha_diliConsulta" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="10" Width="140px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">Tipo diligencia<br />
                                            <asp:DropDownList ID="ddlTipoDiligenciaConsulta" runat="server" AutoPostBack="True"
                                                CssClass="textbox diligencia" Width="200px" OnSelectedIndexChanged="ddlTipoDiligenciaConsulta_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CompareValidator ID="cvTipoDiligencia3" runat="server" ControlToValidate="ddlTipoDiligenciaConsulta"
                                                Display="Dynamic" ErrorMessage="Seleccione un tipo de diligencia" ForeColor="Red"
                                                Operator="GreaterThan" SetFocusOnError="true" Style="font-size: xx-small" Type="Integer"
                                                ValidationGroup="vgGuardar" ValueToCompare="0" Width="200px"></asp:CompareValidator>
                                        </td>
                                        <td style="text-align: left; height: 30px;">Tipo Contacto<br />
                                            <asp:DropDownList ID="ddlTipoContactoConsulta" runat="server" AutoPostBack="True"
                                                CssClass="textbox" Width="200px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CompareValidator ID="cvTipoContactoConsulta" runat="server" ControlToValidate="ddlTipoContactoConsulta"
                                                Display="Dynamic" ErrorMessage="Seleccione un tipo de contacto" ForeColor="Red"
                                                Operator="GreaterThan" SetFocusOnError="true" Style="font-size: xx-small" Type="Integer"
                                                ValidationGroup="vgGuardar" ValueToCompare="0" Width="200px">
                                            </asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">Atendió<br />
                                            <asp:TextBox ID="txtAtendioConsulta" runat="server" CssClass="textbox" MaxLength="40"
                                                Width="220px" />
                                        </td>
                                        <td style="text-align: left;">Respuesta:<br />
                                            <asp:TextBox ID="txtRespuestaConsulta" runat="server" CssClass="textbox" Height="27px"
                                                MaxLength="250" TextMode="multiline" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:CheckBox ID="chkApruebaConsulta" runat="server" Text="Acuerdo" TextAlign="Right"
                                                AutoPostBack="True" OnCheckedChanged="chkApruebaConsulta_CheckedChanged" Style="font-size: x-small; text-align: left;"
                                                Width="60px" />
                                        </td>
                                        <td style="text-align: left;">Fecha acuerdo&nbsp;&nbsp;<br />
                                            <asp:TextBox ID="txtFecha_acuerdoConsulta" runat="server" CssClass="textbox" MaxLength="128"
                                                Width="140px" Enabled="False" />
                                            <asp:CalendarExtender ID="txtFecha_acuerdoConsulta_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_acuerdoConsulta">
                                            </asp:CalendarExtender>
                                            <asp:CompareValidator ID="cvFecha_acuerdo2" runat="server" ControlToValidate="txtFecha_acuerdoConsulta"
                                                Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small" ToolTip="Formato fecha"
                                                Type="Date" ValidationGroup="vgGuardar" />
                                        </td>
                                        <td style="text-align: left;">Valor Acuerdo<br />
                                            <asp:TextBox ID="txtValor_acuerdoConsulta" runat="server" CssClass="textbox" DataFormatString="{0:n0}"
                                                MaxLength="38" Width="200px" Style="text-align: right" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999"
                                                MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtValor_acuerdoConsulta" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 22px;" colspan="2">
                                            <asp:TextBox ID="txtAnexo" runat="server" CssClass="textbox" Enabled="false" Width="220px" />
                                            <br />
                                            <asp:Button ID="btnAbrirAnexo" runat="server" CssClass="btn8" OnClick="btnAbrirAnexo_Click"
                                                Text="Abrir anexo" Visible="False" Height="22px" Width="200px" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:CheckBox ID="chkCambiarArchivoConsulta" runat="server" Style="font-size: xx-small" /><br />
                                            <asp:FileUpload ID="FileUpload2" runat="server" Style="font-size: x-small" Height="20px" Width="180px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 31px;" colspan="3">Observación<br />
                                            <asp:TextBox ID="txtObservacionConsulta" runat="server" CssClass="textbox" MaxLength="250"
                                                Width="90%" />
                                            <asp:TextBox ID="txtCodigo_usuario_regis" runat="server" CssClass="textbox" Height="15px"
                                                Width="144px" Enabled="False" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center;">
                                            <asp:Button ID="btnCloseRegConsulta" runat="server" CausesValidation="false" CssClass="btn8"
                                                Height="25px" Width="100px" OnClick="btnCloseRegConsulta_Click1" Text="Cerrar" />
                                            &#160;&#160;&#160;
                                            <asp:Button ID="btnModificarDiligencia" runat="server" CssClass="btn8"
                                                Height="25px" OnClick="btnModificarDiligencia_Click" PostBackUrl="~/Page/Asesores/Recuperacion/Detalle.aspx"
                                                Style="margin-right: 10px;" Text="Modificar" ValidationGroup="vgActividadReg"
                                                Width="100px" />
                                        </td>
                                    </tr>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                    <asp:Panel ID="PanelActividadRegistro" runat="server" BackColor="White" Style="text-align: left; padding: 15px">
                                <table border="0" cellpadding="5" cellspacing="0" style="border: medium groove #0000FF; background-color: #FFFFFF; margin-right: 5px;"
                                    width="500px" align="center">
                                    <tr>
                                        <td class="tdI" style="text-align: center" colspan="3">
                                            <div class="gridHeader" style="width: 100%">
                                                REGISTRO DE&nbsp; ACTIVIDAD
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 34px;" colspan="2">Número radicación&nbsp;&nbsp;<br />
                                            <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="128" Width="140px" OnTextChanged="txtNumero_radicacion_TextChanged" />
                                        </td>
                                        <td style="text-align: left; height: 34px;">Fecha Diligencia<br />
                                            <asp:TextBox ID="txtFecha_diligencia" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="10" Width="140px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdI" style="height: 30px;" colspan="2">Tipo Diligencia<br />
                                            <asp:DropDownList ID="ddlTipoDiligencia" runat="server" CssClass="textbox tipdili" Width="200px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CompareValidator ID="cvTipoDiligencia" runat="server" ControlToValidate="ddlTipoDiligencia"
                                                Display="Dynamic" ErrorMessage="Seleccione un tipo de diligencia" ForeColor="Red" Style="font-size: x-small"
                                                Operator="GreaterThan" SetFocusOnError="true" Type="Integer" ValidationGroup="vgGuardar"
                                                ValueToCompare="0">
                                            </asp:CompareValidator>
                                        </td>
                                        <td class="tdI" style="height: 30px;">Tipo Contacto<br />
                                            <asp:DropDownList ID="ddlTipoContacto" runat="server" CssClass="textbox" Width="200px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CompareValidator ID="cvTipoContacto" runat="server" ControlToValidate="ddlTipoContacto"
                                                Display="Dynamic" ErrorMessage="Seleccione un tipo de contacto" ForeColor="Red" Style="font-size: x-small"
                                                Operator="GreaterThan" SetFocusOnError="true" Type="Integer" ValidationGroup="vgGuardar"
                                                ValueToCompare="0">
                                            </asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 38px;" colspan="2">Atendió&nbsp;<br />
                                            <asp:TextBox ID="txtAtendio" runat="server" CssClass="textbox" MaxLength="40" Width="200px" onkeydown="return (event.keyCode!=13);" />
                                        </td>
                                        <td style="text-align: left; height: 38px;">Respuesta&nbsp;&nbsp;<br />
                                            <asp:TextBox ID="txtRespuesta" runat="server" CssClass="textbox" MaxLength="250" onkeydown="return (event.keyCode!=13);"
                                                Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 15px;">
                                            <asp:CheckBox ID="chkAprueba" runat="server" OnChange="eneable()"
                                                Style="font-size: x-small" Text="Acuerdo" TextAlign="Right" />
                                        </td>
                                        <td style="text-align: left; height: 15px;">Fecha acuerdo<br />
                                            <asp:TextBox ID="txtFecha_acuerdo" runat="server" CssClass="textbox" MaxLength="128"
                                                Enabled="False" Width="140px" onkeydown="return (event.keyCode!=13);" />
                                            <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtFecha_acuerdo">
                                            </asp:CalendarExtender>
                                            <br />
                                            <asp:CompareValidator ID="cvFecha_acuerdo" runat="server" ControlToValidate="txtFecha_acuerdo"
                                                Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                                Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date"
                                                ValidationGroup="vgGuardar" Style="font-size: xx-small" />
                                        </td>
                                        <td style="text-align: left; height: 15px;">Valor acuerdo<br />
                                            <asp:TextBox ID="txtValor_acuerdo" runat="server" CssClass="textbox" Enabled="False"
                                                MaxLength="38" Width="200px" onkeydown="return (event.keyCode!=13);" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999"
                                                MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtValor_acuerdo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:Label ID="lblCambiarArchivo" runat="server" Text="Cargar archivo:" Visible="False"
                                                Width="150px" />
                                            <br />
                                            <asp:FileUpload ID="FileUpload1" runat="server" Style="font-size: x-small" Height="20px" Width="180px" />
                                        </td>

                                        <td style="text-align: left;">Observación&nbsp;&nbsp;<br />
                                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" MaxLength="250"
                                                UseSubmitBehavior="false" onkeydown="return (event.keyCode!=13);" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 31px;" colspan="3">Número de Celular<br />
                                            <asp:TextBox ID="txtNumCel" runat="server" CssClass="textbox" MaxLength="250"
                                                UseSubmitBehavior="false" onkeydown="return (event.keyCode!=13);" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 100%" colspan="3">
                                            <asp:Panel ID="panelCreditos" runat="server">
                                                <div style="overflow: scroll; max-height: 350px">
                                                    <asp:GridView ID="gvCreditos" runat="server" AutoGenerateColumns="False" DataKeyNames="numero_radicacion"
                                                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                                        RowStyle-Font-Size="X-Small" RowStyle-CssClass="gridItem" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Incluir">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSeleccionar" runat="server" Checked='<%#Convert.ToBoolean(Eval("check"))%>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="numero_radicacion" HeaderText="Nro Radicación" />
                                                            <asp:BoundField DataField="fecha_prox_pago" HeaderText="Fec. Prox Pago" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="valor_a_pagar" HeaderText="Vr a Pagar" DataFormatString="{0:c0}">
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; width: 100%" colspan="3">
                                            <asp:Label ID="lblMsj" runat="server" Style="color: Red; font-size: x-small" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdI" style="text-align: center; height: 23px;" colspan="3"><%--PostBackUrl="~/Page/Asesores/Recuperacion/Detalle.aspx"--%>
                                            <asp:Button ID="btnGuardarReg" runat="server" CssClass="btn8" Height="25px" OnClick="btnGuardarReg_Click"
                                                Style="margin-right: 10px;" Text="Guardar" ValidationGroup="vgActividadReg" Width="100px" />&#160;&#160;&#160;
                                                
                                                <asp:Button ID="btnCloseReg" runat="server" CausesValidation="false" CssClass="btn8"
                                                    Height="25px" OnClick="btnCloseReg2_Click" Text="Cerrar" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                    </asp:Panel>
                </asp:Panel>

            </td>
            <td class="style9">
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 70px">
                <asp:Panel ID="Panel2" runat="server" Style="margin-bottom: 0px" Height="100%">
                    <asp:GridView ID="gvListaAtributos" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                        DataKeyNames="cod_atr" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        OnPageIndexChanging="gvListaAtributos_PageIndexChanging" PagerStyle-CssClass="gridPager"
                        PageSize="7" RowStyle-CssClass="gridItem" Width="100%" Height="127px">
                        <Columns>
                            <asp:BoundField DataField="cod_atr" />
                            <asp:BoundField DataField="nombre" HeaderText="Atributo" />
                            <asp:BoundField DataField="saldo_atributo" HeaderText="Valor" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs1" runat="server" Visible="False" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <span id="txtOutput" style="font: 8pt verdana;" runat="server" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {

            var options = $(".diligencia option");
            options.detach().sort(function (a, b) {
                var at = $(a).val();
                var bt = $(b).val();
                return bt - at;
            });
            options.appendTo(".diligencia");

            var option = $(".tipdili option");
            option.detach().sort(function (a, b) {
                var at = $(a).val();
                var bt = $(b).val();
                return bt - at;
            });
            option.appendTo(".tipdili");
            console.log(option);


        });

        function NewWindow() {
            document.forms[0].target = "_blank";
        }
    </script>
    <script type="text/javascript">
        function deshabilitar(boton) {
            document.getElementById(boton).style.visibility = 'hidden';
        }
        function Page_Load() {
            $('#popupcontainer').addClass('clasdiv');
        }
        Page_Load()
        function Forzar() {
            __doPostBack('', '');
        }
        function eneable() {
            if ($("#cphMain_chkAprueba").is(":checked")) {
                $("#cphMain_txtFecha_acuerdo").removeAttr("disabled");
                $("#cphMain_txtValor_acuerdo").removeAttr('disabled');
            } else {
                $("#cphMain_txtFecha_acuerdo").val('');
                $("#cphMain_txtValor_acuerdo").val('');
            }
            document.getElementById("cphMain_txtFecha_acuerdo").disabled = !document.getElementById("cphMain_chkAprueba").checked;
            document.getElementById("cphMain_txtValor_acuerdo").disabled = !document.getElementById("cphMain_chkAprueba").checked;
        }

        function Check(e) {
            debugger;
            if (!$("#" + e).is(":checked")) {
                $("#" + e).attr("checked", false);
            }
        }


    </script>

</asp:Content>
