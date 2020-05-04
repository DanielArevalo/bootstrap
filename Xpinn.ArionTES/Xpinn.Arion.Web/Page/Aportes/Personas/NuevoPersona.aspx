<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="NuevoPersona.aspx.cs" Inherits="NuevoPersona" Title=".: Expinn - Personas :." %>

<%--<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }
    </script>
	<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp" crossorigin="anonymous">
    <link rel="stylesheet" href="../../../Styles/tabs.css" type="text/css"/>
    <script src="../../../Scripts/tabs.js" type="text/javascript"></script>
    <asp:Panel ID="panelDAtaGeneral" runat="server">
        <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwDetalleCliente" runat="server" EnableTheming="True">
                <asp:Panel ID="panelTabs" runat="server" Visible="true">
						<div class="wrap">
						<ul class="tabs">
							<li><a href="#ntab1"><span style="font-size:17px;" class="far fa-folder-open"></span><span class="tab-text">Datos personales</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size:17px;"></span></a></li>
							<li><a href="#ntab2"><span style="font-size:17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. laboral</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size:17px;"></span></a></li>
							<li><a href="#ntab3"><span style="font-size:17px;" class="far fa-folder-open"></span><span class="tab-text">Beneficiarios</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size:17px;"></span></a></li>
							<li><a href="#ntab4"><span style="font-size:17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. Económica</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size:17px;"></span></a></li>
							<li><a href="#ntab5"><span style="font-size:17px;" class="far fa-folder-open"></span><span class="tab-text">Inf. adicional</span><span class="fas fa-angle-right" style="margin-left: 7px; font-size:17px;"></span></a></li>
						</ul>

						<div class="secciones">
							<article id="ntab1">
								<table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-top: 10px;">
									<tr>
										<td colspan="9" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Datos Básicos</strong>
										</td>
									</tr>
								</table>
								<table cellpadding="0" cellspacing="3" width="100%" style="padding-top: 15px;">
									<tr>
										<td style="text-align: left; width: 60%;" colspan="2">
											<strong>Datos básicos</strong>
										</td>
									</tr>   
									<tr>
										<td style="text-align: left;" width="35%">Identificación*
											<br />
											<asp:DropDownList ID="ddlTipoE" runat="server" CssClass="textbox" Width="125px" TabIndex="1">
											</asp:DropDownList>
										</td>
										<td style="text-align: left;" width="35%">No.Identificación
											<br />
											<asp:TextBox ID="txtIdentificacionE" runat="server" CssClass="textbox" MaxLength="20"
												Width="120px" OnTextChanged="txtIdentificacionE_TextChanged" AutoPostBack="true"
												TabIndex="2" onkeypress="return ValidNum(event);" />

										</td>
										<td rowspan="5" style="text-align: center;" width="30%">
											<asp:FileUpload ID="fuFoto" runat="server" BorderWidth="0px" Font-Size="XX-Small"
												Height="20px" ToolTip="Seleccionar el archivo que contiene la foto" Width="200px" />
											<asp:HiddenField ID="hdFileName" runat="server" />
											<asp:HiddenField ID="hdFileNameThumb" runat="server" />
											<asp:LinkButton ID="linkBt" runat="server" OnClick="linkBt_Click" ClientIDMode="Static" />
											<br />
											<asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
											<br />
											<asp:Button ID="btnCargarImagen" runat="server" Text="Cargar Imagen" Font-Size="xx-Small"
												Height="20px" Width="100px" OnClick="btnCargarImagen_Click" ClientIDMode="Static" />
											<asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="False"
												MaxLength="80" Visible="False" Width="120px" />
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">Tipo Persona<br />
											<asp:UpdatePanel ID="UpdatePanel6" runat="server">
												<ContentTemplate>
													<asp:RadioButtonList ID="rblTipo_persona" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
														Width="155px" Enabled="False">
														<asp:ListItem Selected="True">Natural</asp:ListItem>
														<asp:ListItem>Jurídica</asp:ListItem>
													</asp:RadioButtonList>
												</ContentTemplate>
											</asp:UpdatePanel>
										</td>
										<td style="text-align: left;">Oficina<br />
											<asp:DropDownList ID="txtCod_oficina" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="150px" TabIndex="3">
												<asp:ListItem></asp:ListItem>
											</asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">F.Expedición<br />
											<uc1:fecha ID="txtFechaexpedicion" runat="server" Enabled="True" TabIndex="4" />
										</td>
										<td style="text-align: left;">Ciud.Expedición<br />
											<asp:DropDownList ID="ddlLugarExpedicion" runat="server" Width="128px" CssClass="textbox"
												AppendDataBoundItems="True" TabIndex="5">
												<asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
											</asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td style="text-align: left;" width="35%">Primer Nombre<br />
											<asp:TextBox ID="txtPrimer_nombreE" runat="server" CssClass="textbox" MaxLength="100"
												Style="text-transform: uppercase" Width="90%" TabIndex="6" />
											<asp:FilteredTextBoxExtender ID="fte50" runat="server" Enabled="True" TargetControlID="txtPrimer_nombreE"
												ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
										<td style="text-align: left;" width="35%">Segundo Nombre<br />
											<asp:TextBox ID="txtSegundo_nombreE" runat="server" CssClass="textbox" MaxLength="100"
												Style="text-transform: uppercase" Width="90%" TabIndex="7" />
											<asp:FilteredTextBoxExtender ID="fte51" runat="server" Enabled="True" TargetControlID="txtSegundo_nombreE"
												ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
									</tr>
									<tr>
										<td style="text-align: left;" width="35%">Primer Apellido<br />
											<asp:TextBox ID="txtPrimer_apellidoE" runat="server" CssClass="textbox" MaxLength="100"
												Width="90%" Style="text-transform: uppercase" TabIndex="8" />
											<asp:FilteredTextBoxExtender ID="fte52" runat="server" Enabled="True" TargetControlID="txtPrimer_apellidoE"
												ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
										<td style="text-align: left;" width="35%">Segundo Apellido
											<br />
											<asp:TextBox ID="txtSegundo_apellidoE" runat="server" CssClass="textbox" MaxLength="100"
												Width="90%" Style="text-transform: uppercase" TabIndex="9" />
											<asp:FilteredTextBoxExtender ID="fte53" runat="server" Enabled="True" TargetControlID="txtSegundo_apellidoE"
												ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
									</tr>
								</table>
								<table cellpadding="0" cellspacing="3" width="100%" style="padding-top: 15px;">
									<tr>
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Datos de Localización</strong>
										</td>
									</tr>
								</table>
								<table cellpadding="0" cellspacing="3" width="100%" style="padding-top: 10px;">
									<tr>
										<td style="text-align: left; width: 60%;" colspan="2">
											<strong>Información de Residencia</strong>
										</td>
									</tr>            
									<tr>
										<td style="text-align:left; Width:150px">Tipo de Ubicación<br/>
											<asp:DropDownList ID="ddlTipoUbic" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="130px" TabIndex="12" AutoPostBack="False" onkeypress="KeyBackspace();">
												<asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
												<asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
												<asp:ListItem Value="2" Text="Rural"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td style="text-align: left;" colspan="2">Dirección de Residencia
											<%--<div style="overflow: scroll; width: 100%;">--%>
											<%--<uc1:direccion ID="txtDireccionE" runat="server" />--%>
											<asp:TextBox ID="txtDireccionE" runat="server" Width="90%" CssClass="textbox" TabIndex="10"></asp:TextBox>
											<%--</div>--%>
										</td>
										<td style="text-align:left;">Ciudad de Residencia
											<asp:DropDownList ID="ddlCiudadResidencia" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="150px" TabIndex="12" AutoPostBack="False" onkeypress="KeyBackspace();">
												<asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td style="text-align:left;">Barrio de Residencia
											<asp:DropDownList ID="ddlBarrioResid" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="160px" TabIndex="12" AutoPostBack="False" onkeypress="KeyBackspace();">
												<asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<%--<td style="text-align: left;"></td>--%>
										<td style="text-align: left;">Tel. Residencia 
											<asp:TextBox ID="txtTelefonoE" runat="server" CssClass="textbox" MaxLength="128" Width="130px" TabIndex="17" />
											<asp:FilteredTextBoxExtender ID="txtTelefonoE_FilteredTextBoxExtender"
												runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTelefonoE">
											</asp:FilteredTextBoxExtender>
										</td>
									</tr>
									<tr>
										<td style="text-align: left; width: 60%" colspan="2">
											<strong>Información de Correspondencia</strong>
										</td>
									</tr>   
									<tr>
										<td colspan="6" style="text-align: left">
										<asp:CheckBox ID="chInfoResidencia" runat="server" TabIndex="44" Text="Utilizar los datos de la información de residencia"
											onchange="InfoCorrespondencia()"
											TextAlign="Left" /></td>
									</tr>         
									<tr>
										<td style="text-align:left; width:150px">Tipo de Ubicación<br/>
											<asp:DropDownList ID="ddlTipoUbicCorr" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="130px" TabIndex="12" AutoPostBack="False" onkeypress="KeyBackspace();">
												<asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
												<asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
												<asp:ListItem Value="2" Text="Rural"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td colspan="2" style="text-align: left;">Dirección de Correspondencia
											<%--<div style="overflow: scroll; width: 100%;">--%>
											<%--<uc1:direccion ID="txtDirCorrespondencia" runat="server" />--%>
											<asp:TextBox ID="txtDirCorrespondencia" runat="server" Width="90%" CssClass="textbox"
												TabIndex="11"></asp:TextBox>
											<%--</div>--%>
										</td>
										<td style="text-align: left;">Ciudad de Correspondencia
											<asp:DropDownList ID="ddlCiuCorrespondencia" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="150px" TabIndex="12" AutoPostBack="False" onkeypress="KeyBackspace();"
												ToolTip="Ciudad de Correspondencia">
												<asp:ListItem Value="Seleccione un item"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td style="text-align: left;">Barrio de Correspondencia
											<asp:DropDownList ID="ddlBarrioCorrespondencia" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="160px" TabIndex="13">
												<asp:ListItem Text="Seleccione un item" Value=""></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td style="text-align: left;">Tel. Correspondencia
											<asp:TextBox ID="txtTelCorrespondencia" runat="server" CssClass="textbox" MaxLength="20"
												Width="130px" TabIndex="14" />
											<asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtTelCorrespondencia" ValidChars="-()" />
										</td>
									</tr>
									<tr>
										<td style="text-align: left;" colspan="3">E-mail<br/>
											<asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="120" Width="280px" TabIndex="35">
											</asp:TextBox>
											<asp:RegularExpressionValidator ID="revTxtEmail" runat="server"
													ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="E-Mail no valido!"
													ForeColor="Red" Style="font-size: xx-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
											</asp:RegularExpressionValidator>
										</td>             
									</tr>
								</table>
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Datos Generales</strong>
										</td>
									</tr>
								</table>
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 20px;">
									<tr>
										<td colspan="2" style="text-align: left;"><strong>Datos Generales:</strong> </td>
									</tr>
									<tr>
										<td style="text-align: left;">Celular </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="12" Width="148px"
												TabIndex="15" /><asp:FilteredTextBoxExtender ID="fte55" runat="server" Enabled="True"
													FilterType="Numbers, Custom" TargetControlID="txtCelular" ValidChars="()-" />
										</td>
										<td style="text-align: left;">Actividad CIIU </td>
										<td colspan="3" style="text-align: left;">
											<%--<asp:UpdatePanel ID="upRecoger" runat="server" RenderMode="Inline" UpdateMode="Conditional">
												<ContentTemplate>--%>
													<%--<asp:HiddenField ID="hfValue" runat="server" Visible="false" />--%>
													<asp:TextBox ID="txtActividadCIIU" CssClass="textbox" runat="server" Width="145px" TabIndex="16"></asp:TextBox>
													<asp:PopupControlExtender ID="txtRecoger_PopupControlExtender" runat="server"
														Enabled="True" ExtenderControlID="" TargetControlID="txtActividadCIIU"
														PopupControlID="panelLista" OffsetY="22">
													</asp:PopupControlExtender>
													<asp:Panel ID="panelLista" runat="server" Height="200px" Width="400px"
														BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
														ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
														<table style="width: 100%">
															<tr>
																<td style="width: 30%">
																	<asp:TextBox ID="txtBuscarCodigo" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="90%" placeholder="Código"></asp:TextBox>
																</td>
																<td style="width: 45%">
																	<asp:TextBox ID="txtBuscarDescripcion" CssClass="textbox" runat="server" Width="90%" placeholder="Descripción"></asp:TextBox>
																</td>
																<td style="width: 15%">
																	<asp:ImageButton ID="imgBuscar" ImageUrl="~/Images/Lupa.jpg" Height="25px" runat="server" OnClick="imgBuscar_Click" CausesValidation="false" />
																</td>
															</tr>
														</table>
														<asp:GridView ID="gvActividadesCIIU" runat="server" Width="100%" AutoGenerateColumns="False"
															HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="ListaId" OnRowDataBound="gvActividadesCIIU_RowDataBound">
															<Columns>
																<asp:TemplateField HeaderText="Código">
																	<ItemTemplate>
																		<asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("ListaIdStr") %>'></asp:Label>
																	</ItemTemplate>
																	<ItemStyle HorizontalAlign="Left" />
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Descripción">
																	<ItemTemplate>
																		<asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("ListaDescripcion") %>'></asp:Label>
																	</ItemTemplate>
																	<ItemStyle HorizontalAlign="Left" />
																</asp:TemplateField>
																<asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal">
																	<ItemTemplate>
																		<cc1:CheckBoxGrid ID="chkPrincipal" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
																			AutoPostBack="false" />
																	</ItemTemplate>
																	<HeaderStyle CssClass="gridIco"></HeaderStyle>
																</asp:TemplateField>
																<asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Sel.">
																	<ItemTemplate>
																		<cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
																			AutoPostBack="false" ToolTip="Seleccionar" />
																	</ItemTemplate>
																	<HeaderStyle CssClass="gridIco"></HeaderStyle>
																</asp:TemplateField>

															</Columns>
														</asp:GridView>
													</asp:Panel>
												<%--</ContentTemplate>
												<Triggers>
													<asp:AsyncPostBackTrigger ControlID="gvActividadesCIIU" EventName="RowDataBound" />
												</Triggers>
											</asp:UpdatePanel>--%>
											<%--<asp:DropDownList ID="ddlActividadE" runat="server" AppendDataBoundItems="True" CssClass="textbox"
												Width="180px" TabIndex="16">
												<asp:ListItem Value="Seleccione un item"></asp:ListItem>
											</asp:DropDownList></td>--%>
										<td style="text-align: left;">Pais de Nacimiento</td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlPais" runat="server" TabIndex="19" AppendDataBoundItems="True"
												CssClass="textbox" Width="170px">
												<asp:ListItem Value="Seleccione un item"></asp:ListItem>
											</asp:DropDownList></td>                                                 
										<td style="text-align: left;"></td>
									</tr>
									<tr>
										<td style="text-align: left;">Fec.Nacimiento </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtFechanacimiento" runat="server" AutoPostBack="True" CssClass="textbox"
												MaxLength="10" TabIndex="18" OnTextChanged="txtFechanacimiento_TextChanged" ValidationGroup="vgGuardar"
												Width="148px"> </asp:TextBox><asp:MaskedEditExtender ID="MEEfecha" runat="server"
													TargetControlID="txtFechanacimiento" Mask="99/99/9999" MessageValidatorTip="true"
													MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />
											<asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" Enabled="True"
												OnClientDateSelectionChanged="checkDate" TargetControlID="txtFechanacimiento">
											</asp:CalendarExtender>
										</td>
										<td style="text-align: left;">Edad del Cliente </td>
										<td colspan="3" style="text-align: left">
											<asp:TextBox ID="txtEdadCliente" runat="server" CssClass="textbox" Enabled="False"
												Width="40px"></asp:TextBox></td>
										<td style="text-align: left;">Ciudad Nacimiento </td>
											<td style="text-align: left;">
												<asp:DropDownList ID="ddlLugarNacimiento" runat="server" TabIndex="19" AppendDataBoundItems="True"
													CssClass="textbox" Width="170px">
													<asp:ListItem Value="Seleccione un item"></asp:ListItem>
												</asp:DropDownList></td>     
										<td style="text-align: left;"></td>
										<td style="text-align: left;"></td>
									</tr>
									<tr>
										<td style="text-align: left;">Sexo<br style="font-size: x-small" />
										</td>
										<td style="text-align: left;">
											<asp:RadioButtonList ID="rblSexo" runat="server" AutoPostBack="true" CellPadding="0"
												CellSpacing="0" Height="22px" TabIndex="20" OnSelectedIndexChanged="rblSexo_SelectedIndexChanged"
												RepeatDirection="Horizontal" Style="font-size: xx-small; text-align: left;" Width="139px">
												<asp:ListItem Selected="True">F</asp:ListItem>
												<asp:ListItem>M</asp:ListItem>
											</asp:RadioButtonList></td>
										<td style="text-align: left;">Nivel Educativo<br style="font-size: x-small" />
										</td>
										<td colspan="3" style="text-align: left;">
											<asp:DropDownList ID="ddlNivelEscolaridad" runat="server" TabIndex="21" AppendDataBoundItems="True"
												CssClass="textbox" Width="180px">
												<asp:ListItem Value="Seleccione un item"></asp:ListItem>
											</asp:DropDownList></td>
										<td style="text-align: left;">Profesión </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtProfecion" runat="server" CssClass="textbox" TabIndex="22" MaxLength="100"
												Style="text-transform: uppercase" Visible="true" Width="160px"></asp:TextBox><asp:FilteredTextBoxExtender
													ID="fte56" runat="server" Enabled="True" TargetControlID="txtProfecion" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">Estado Civil<br />
										</td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlEstadoCivil" runat="server" TabIndex="23" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEstadoCivil_SelectedIndexChanged"
												CssClass="textbox" Width="156px" AutoPostBack="true">
												<asp:ListItem Value="Seleccione un item"></asp:ListItem>
											</asp:DropDownList></td>
										<td style="text-align: left;">Personas a Cargo </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtPersonasCargo" runat="server" CssClass="textbox" TabIndex="24"
												MaxLength="100" Visible="true" Width="40px"></asp:TextBox><asp:FilteredTextBoxExtender
													ID="ftb13" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtPersonasCargo"
													ValidChars="" />
										</td>
										<td style="text-align: left;">Estrato </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox" MaxLength="100" TabIndex="25"
												Visible="true" Width="40px"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte10"
													runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtEstrato"
													ValidChars="" />
										</td>
										<td style="text-align: left">Ocupación </td>
										<td style="text-align: left">
											<asp:DropDownList ID="ddlOcupacion" runat="server" CssClass="textbox" Width="170px" AutoPostBack="true"
													OnSelectedIndexChanged="ddlOcupacion_SelectedIndexChanged" TabIndex="26" /></td>
									</tr>
								</table>
								<hr />
								<table cellpadding="0" style="width: 100%; margin-right: 7px;">
									<tr>
										<td style="text-align: left;">Tipo Vivienda </td>
										<td style="text-align: left;">
											<asp:RadioButtonList ID="rblTipoVivienda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblTipoVivienda_SelectedIndexChanged"
												RepeatDirection="Horizontal" Width="370px" TabIndex="27">
												<asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
												<asp:ListItem Value="A">Arrendada</asp:ListItem>
												<asp:ListItem Value="F">Familiar</asp:ListItem>
											</asp:RadioButtonList></td>
										<td style="text-align: left;"></td>
										<td style="text-align: left;"></td>
									</tr>
									<tr>
										<td style="text-align: left;">
											<asp:Label ID="lblPropietario" runat="server" Text="Nombre Propietario"/>                                                          
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" MaxLength="128"
												Style="text-align: left; text-transform: uppercase" Width="299px" TabIndex="28" /></td>
										<td style="text-align: left">
											<asp:Label ID="lblTelefPropietario" runat="server" Text="Teléfono Propietario"/>                                                          
										</td>
										<td style="text-align: left">
											<asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" MaxLength="128"
												Style="margin-left: 0px" Width="148px" TabIndex="29" /><asp:FilteredTextBoxExtender
													ID="txtTelefonoarrendador_FilteredTextBoxExtender" runat="server" Enabled="True"
													FilterType="Numbers" TargetControlID="txtTelefonoarrendador">
												</asp:FilteredTextBoxExtender>
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">Antigüedad en la Vivienda (Meses) </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox" MaxLength="8"
												Style="text-align: left" Width="100px" TabIndex="30" /><asp:FilteredTextBoxExtender
													ID="txtAntiguedadlugar_FilteredTextBoxExtender" runat="server" Enabled="True"
													FilterType="Numbers" TargetControlID="txtAntiguedadlugar">
												</asp:FilteredTextBoxExtender>
										</td>
										<td style="text-align: left;"></td>
										<td style="text-align: left;">
											<asp:Label ID="lblValorArriendo" runat="server" Text="Valor Arriendo" Visible="false"/> 
											<uc1:decimales ID="txtValorArriendo" runat="server" TabIndex="31" Visible="false"/>
										</td>
									</tr>
								</table>
							</article>
							<article id="ntab2">
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Información Laboral</strong>
										</td>
									</tr>
								</table>
								<table cellpadding="0" style="width: 100%; padding-top: 10px;">
									<tr>
										<td colspan="2" style="text-align: left;"><strong>Información Laboral</strong> </td>
										<td style="text-align: left; width: 200px"></td>
										<td style="text-align: left"></td>
										<td style="text-align: left; width: 185px;"></td>
									</tr>
									<tr>
										<td style="text-align: left; width: 200px">Empresa </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" MaxLength="120" Style="text-transform: uppercase"
												Width="280px" TabIndex="32"></asp:TextBox></td>
										<td style="text-align: left; width: 200px">Nit Empresa </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtNitEmpresa" runat="server" CssClass="textbox" MaxLength="120" Style="text-transform: uppercase"
												Width="166px" TabIndex="32"></asp:TextBox>
										</td>
										<td style="text-align: left;">Actividad CIIU 
										<%--<asp:UpdatePanel ID="upActEmpresa" runat="server" RenderMode="Inline" UpdateMode="Conditional">
											<ContentTemplate>--%>
												<asp:TextBox ID="txtCIIUEmpresa" CssClass="textbox" runat="server" Width="145px" TabIndex="16"></asp:TextBox>
												<asp:HiddenField ID="hfActEmpresa" runat="server" ClientIDMode="Static" />
												<asp:PopupControlExtender ID="PopupControlExtender3" runat="server"
													Enabled="True" ExtenderControlID="" TargetControlID="txtCIIUEmpresa"
													PopupControlID="pActEmpresa" OffsetY="22">
												</asp:PopupControlExtender>
												<asp:Panel ID="pActEmpresa" runat="server" Height="200px" Width="400px"
													BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
													ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
													<table style="width: 100%">
														<tr>
															<td style="width: 30%">
																<asp:TextBox ID="txtBuscarCodigo2" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="90%" placeholder="Código"></asp:TextBox>
															</td>
															<td style="width: 45%">
																<asp:TextBox ID="txtBuscarDescripcion2" CssClass="textbox" runat="server" Width="90%" placeholder="Descripción"></asp:TextBox>
															</td>
															<td style="width: 15%">
																<asp:ImageButton ID="imgBuscar2" ImageUrl="~/Images/Lupa.jpg" Height="25px" runat="server" OnClick="imgBuscar2_Click" CausesValidation="false" />
															</td>
														</tr>
													</table>
													<asp:GridView ID="gvActEmpresa" runat="server" Width="100%" AutoGenerateColumns="False"
														HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="ListaId" OnRowDataBound="gvActEmpresa_RowDataBound">
														<Columns>
															<asp:TemplateField HeaderText="Código">
																<ItemTemplate>
																	<asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("ListaIdStr") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" />
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Descripción">
																<ItemTemplate>
																	<asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("ListaDescripcion") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" />
															</asp:TemplateField>
															<asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal">
																<ItemTemplate>
																	<cc1:CheckBoxGrid ID="chkPrincipal" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
																		AutoPostBack="false" />
																</ItemTemplate>
																<HeaderStyle CssClass="gridIco"></HeaderStyle>
															</asp:TemplateField>

														</Columns>
													</asp:GridView>
												</asp:Panel>
											<%--</ContentTemplate>
											<Triggers>
												<asp:AsyncPostBackTrigger ControlID="gvActEmpresa" EventName="RowDataBound" />
											</Triggers>
										</asp:UpdatePanel>--%>
										</td>
									</tr>
									<tr>
										<td style="text-align: left; width: 200px">Cargo </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlCargo" runat="server" CssClass="textbox" Width="166px" TabIndex="33"></asp:DropDownList></td>
										<td style="text-align: left; width: 200px">Tipo Empresa</td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlTipoEmpresa" runat="server" CssClass="textbox" Width="166px" TabIndex="33"></asp:DropDownList>
										</td>
											<td rowspan="8" style="text-align: left; vertical-align: sub">
											<div style="overflow: scroll; max-height: 260px;">
												<asp:Label Visible="false" runat="server" Text="Empresas para Recaudo:" ID="lblempresas"></asp:Label>
												<asp:GridView ID="gvEmpresaRecaudo" runat="server" AllowPaging="False" TabIndex="34"
													AutoGenerateColumns="false" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
													BorderWidth="1px" CellPadding="0" DataKeyNames="idempresarecaudo" ForeColor="Black"
													GridLines="Both" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
													Width="80%">
													<Columns>
														<asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
															<ItemTemplate>
																<asp:Label ID="lblidempresarecaudo" runat="server" Text='<%# Bind("idempresarecaudo") %>'></asp:Label>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" Width="120px" />
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
															<ItemTemplate>
																<asp:Label ID="lblcodempresa" runat="server" Text='<%# Bind("cod_empresa") %>'></asp:Label>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" Width="120px" />
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Empresa" ItemStyle-HorizontalAlign="Left">
															<ItemTemplate>
																<asp:Label ID="lblDescripcion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
																	Text='<%# Bind("descripcion") %>' Width="170px"></asp:Label>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Left" Width="200px" />
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Right">
															<ItemTemplate>
																<asp:UpdatePanel ID="updcheck" runat="server">
																	<ContentTemplate>
																		<cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("seleccionar")) %>'
																			CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnCheckedChanged="chkSeleccionar_CheckedChanged" />
																	</ContentTemplate>
																	<Triggers>
																		<asp:AsyncPostBackTrigger ControlID="chkSeleccionar" />
																	</Triggers>
																</asp:UpdatePanel>
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
											</div>
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">Tipo Contrato </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="textbox" TabIndex="37"
												Width="288px">
											</asp:DropDownList></td>
										<td style="text-align: left;">Fecha de Ingreso </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtFechaIngreso" runat="server" AutoPostBack="True" TabIndex="36"
												CssClass="textbox" MaxLength="10" OnTextChanged="txtFechaIngreso_TextChanged"
												ValidationGroup="vgGuardar" Width="158px"></asp:TextBox><asp:MaskedEditExtender ID="mkefechaIngreso"
													runat="server" TargetControlID="txtFechaIngreso" Mask="99/99/9999" MessageValidatorTip="true"
													MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />
											<asp:CalendarExtender ID="cefecha" runat="server" Enabled="True" Format="dd/MM/yyyy"
												OnClientDateSelectionChanged="checkDate" TargetControlID="txtFechaIngreso" />
										</td>
									</tr>

									<tr>
										<td style="text-align: left;">Actividad Económica </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlActividadE0" runat="server" CssClass="textbox" TabIndex="39"
												Width="288px">
											</asp:DropDownList></td>
                                                        
										<td style="text-align: left;">Antigüedad(Meses) </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtAntiguedadlugarEmpresa" runat="server" CssClass="textbox" TabIndex="38"
												Enabled="false" MaxLength="128" Width="158px"></asp:TextBox></td>
									</tr>
									<tr>                                                         
										<td style="text-align: left;">Sector &#160; </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlSector" runat="server" TabIndex="40"
												CssClass="textbox" Width="166px">
											</asp:DropDownList></td>
										<td style="text-align: left">&#160;&nbsp; </td>
										<td style="text-align: left">&#160;&nbsp; </td>
									</tr>
									<tr>      
										<td style="text-align: left; width: 200px">Codigo Nomina </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" MaxLength="120" Style="text-transform: uppercase"
												Width="280px" TabIndex="32"></asp:TextBox>
										</td>
										<td style="text-align: left;">Ubicación/Zona &#160; </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlZona" runat="server" TabIndex="40"
												CssClass="textbox"
												Width="166px">
											</asp:DropDownList></td>
									</tr>
									<tr>
										<td colspan="2" style="text-align: left">
											<asp:CheckBox ID="chkEmpleadoEntidad" runat="server" TabIndex="43" Text="Es empleado de la entidad solidaria?"
												TextAlign="Left" /></td>
										<td style="text-align: left;">Escalafón Salarial: </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlescalafon" runat="server" CssClass="textbox" Width="166px"></asp:DropDownList></td>
									</tr>
									<tr>
										<td style="text-align: left">Jornada Laboral </td>
										<td style="text-align: left">
											<asp:RadioButtonList ID="rblJornadaLaboral" runat="server" TabIndex="45" RepeatDirection="Horizontal">
												<asp:ListItem Selected="True" Value="1">Tiempo Total</asp:ListItem>
												<asp:ListItem Value="2">Tiempo Parcial</asp:ListItem>
											</asp:RadioButtonList></td>                                                        
										<td colspan="2" style="text-align: left">
											<asp:CheckBox ID="chkMujerCabeFami" runat="server" TabIndex="44" Text="Es mujer cabeza de familia?"
												TextAlign="Left" /></td>
									</tr>
									<tr>
										<td style="text-align: left;">Tiene Parentesco Con Empleados de la Entidad &#160; </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlparentesco" runat="server" TabIndex="40"
												CssClass="textbox" Width="166px">
												<asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
												<asp:ListItem Value="1">SI</asp:ListItem>
												<asp:ListItem Value="2">NO </asp:ListItem>
											</asp:DropDownList></td>                                                        
										<%--<td style="text-align: left">
											<asp:Label ID="lblnomFuncionario" runat="server" Text="Nombre del Funcionario"/></td>
										<td style="text-align: left">
											<asp:TextBox ID="txtnomFuncionario" runat="server" CssClass="textbox" TabIndex="42"
												Width="160px" /></td>--%>
										<td style="text-align: left;">¿Es familiar de una PEPS? &#160; </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlFamiliarPEPS" runat="server" AutoPostBack="false" TabIndex="40"
												CssClass="textbox" Width="166px">
												<asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
												<asp:ListItem Value="1">SI</asp:ListItem>
												<asp:ListItem Value="2">NO </asp:ListItem>
											</asp:DropDownList></td>   
									</tr>
									<tr>
										<td style="text-align: left;">¿Es familiar de un miembro de administración?&#160; </td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlFamiliarAdmin" runat="server" AutoPostBack="false" TabIndex="40" Visible="true"
												CssClass="textbox" Width="166px">
												<asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
												<asp:ListItem Value="1">SI</asp:ListItem>
												<asp:ListItem Value="2">NO </asp:ListItem>
											</asp:DropDownList></td>   
										<td style="text-align: left;">¿Es familiar de un miembro de control?&#160; </td> 
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlFamiliarControl" runat="server" AutoPostBack="false" TabIndex="40" Visible="true"
												CssClass="textbox" Width="166px">
												<asp:ListItem Value="0">Seleccione una Opción</asp:ListItem>
												<asp:ListItem Value="1">SI</asp:ListItem>
												<asp:ListItem Value="2">NO </asp:ListItem>
											</asp:DropDownList></td>                                                        
										<td style="text-align: left">       
									</tr>
									<tr>
										<td colspan="4" style="text-align: left"><strong>Ubicación de la Empresa</strong> </td>
									</tr>
									<tr>
										<td style="text-align: left">Tipo de Ubicación</td>
										<td style="text-align:left; Width:150px">
											<asp:DropDownList ID="ddlTipoUbicEmpresa" runat="server" AppendDataBoundItems="True"
												CssClass="textbox" Width="130px" TabIndex="12" AutoPostBack="False" onkeypress="KeyBackspace();">
												<asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
												<asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
												<asp:ListItem Value="2" Text="Rural"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td style="text-align: left">Dirección de la empresa</td>
										<td style="text-align: left">
											<asp:TextBox ID="txtDireccionEmpresa" runat="server" Width="166px" CssClass="textbox"
												TabIndex="46"></asp:TextBox></td>
										<td style="text-align: left">Ciudad <%--</td>
										<td style="text-align: left">--%>
											<asp:DropDownList ID="ddlCiu0" runat="server" CssClass="textbox" Width="150px" TabIndex="41"></asp:DropDownList></td> 
									</tr>
									<tr>
										<td style="text-align: left">Teléfono Empresa </td>
										<td style="text-align: left">
											<asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" TabIndex="47"
												MaxLength="128" Width="140px"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte58"
													runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelefonoempresa"
													ValidChars="-()" />
										</td>
										<td style="text-align: left">Celular </td>
										<td style="text-align: left">
											<asp:TextBox ID="txtTelCell0" runat="server" CssClass="textbox" MaxLength="128" TabIndex="48"
												Width="140px"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte60" runat="server"
													Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelCell0" ValidChars="-()" />
										</td>
										<td style="text-align: left; width: 250px;"></td>
									</tr>
								</table>
							</article>
							<article id="ntab3">
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Información del conyugue</strong>
										</td>
									</tr>
								</table>
								<table width="100%" style="padding-top:10px;">
									<tr>
										<td align="left" style="width: 25%">
											<asp:TextBox ID="txtcod_conyuge" runat="server" CssClass="textbox" TabIndex="54"
												MaxLength="128" Visible="false" Width="90%" />Primer Nombre
											<br />
											<asp:TextBox ID="txtnombre1_cony" runat="server" CssClass="textbox" TabIndex="55"
												MaxLength="128" Style="text-transform: uppercase" Width="90%" /><asp:FilteredTextBoxExtender
													ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtnombre1_cony"
													ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
										<td align="left" style="width: 25%">Segundo Nombre<br />
											<asp:TextBox ID="txtnombre2_cony" runat="server" CssClass="textbox" TabIndex="56"
												MaxLength="128" Style="text-transform: uppercase" Width="90%" /><asp:FilteredTextBoxExtender
													ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtnombre2_cony"
													ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
										<td align="left" style="width: 25%">Primer Apellido
											<br />
											<asp:TextBox ID="txtapellido1_cony" runat="server" CssClass="textbox" TabIndex="57"
												MaxLength="128" Style="text-transform: uppercase" Width="90%" /><asp:FilteredTextBoxExtender
													ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtapellido1_cony"
													ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
										<td align="left" style="width: 25%">Segundo Apellido&#160;&#160;<br />
											<asp:TextBox ID="txtapellido2_cony" runat="server" CssClass="textbox" TabIndex="58"
												MaxLength="128" Style="text-transform: uppercase" Width="90%" /><asp:FilteredTextBoxExtender
													ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtapellido2_cony"
													ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
										</td>
									</tr>
									<tr>
										<td align="left" style="width: 25%">Tipo Identificación
											<br />
											<asp:DropDownList ID="ddlTipo" runat="server" CssClass="textbox" TabIndex="59"></asp:DropDownList></td>
										<td align="left" style="width: 25%">Identificación *&#160;&#160;
											<br />
											<asp:TextBox ID="txtIdent_cony" runat="server" CssClass="textbox" TabIndex="60" MaxLength="20" /></td>
										<td style="text-align: left;">Fecha Expedición<br />
											<uc1:fecha ID="txtFechaExp_Cony" runat="server" Enabled="True" TabIndex="4" /></td>
										<td align="left" style="width: 25%">Ciudad Expedición<br />
											<asp:DropDownList ID="ddlcuidadExp_Cony" runat="server" CssClass="textbox" TabIndex="61"
												Width="90%">
											</asp:DropDownList></td>                                                        
									</tr>
									<tr>
										<td align="left">Sexo<asp:RadioButtonList ID="rbsexo_cony" runat="server" TabIndex="69" RepeatDirection="Horizontal">
											<asp:ListItem Selected="True">F</asp:ListItem>
											<asp:ListItem>M</asp:ListItem>
										</asp:RadioButtonList></td>
										<td align="left">Fecha Nacimiento<br />
											<asp:TextBox ID="txtfechaNac_Cony" runat="server" CssClass="textbox" TabIndex="70" OnTextChanged="txtfechaNac_Cony_TextChanged"
												MaxLength="10" Width="148px"> </asp:TextBox><asp:MaskedEditExtender ID="MaskedEditExtender1"
													runat="server" TargetControlID="txtfechaNac_Cony" Mask="99/99/9999" MessageValidatorTip="true"
													MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />
											<asp:CalendarExtender ID="ceFecha1" runat="server" Enabled="True" Format="dd/MM/yyyy"
												OnClientDateSelectionChanged="checkDate" TargetControlID="txtfechaNac_Cony">
											</asp:CalendarExtender>
											<br />
										</td>
										<td style="text-align: left;">Edad del Conyuge<br/>
											<asp:TextBox ID="txtEdad_Cony" runat="server" CssClass="textbox" Enabled="False"
												Width="40px"></asp:TextBox></td>                                                        
										<td align="left" style="width: 25%">Lugar de Nacimiento<br />
											<asp:DropDownList ID="ddlLugNacimiento_Cony" runat="server" CssClass="textbox" TabIndex="61" Width="90%">
											</asp:DropDownList></td>
										<td></td>
									</tr>
									<tr>                                                        
										<td align="left">Email<br />
											<asp:TextBox ID="txtemail_cony" runat="server" CssClass="textbox" TabIndex="68" MaxLength="128"
												Width="90%" /><asp:RegularExpressionValidator ID="rfvEmailConyuge" runat="server"
													ControlToValidate="txtemail_cony" Display="Dynamic" ErrorMessage="E-Mail no valido!"
													ForeColor="Red" Style="font-size: xx-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
										<td style="width: 25%; text-align:left">Celular&#160;<br />
											<asp:TextBox ID="txtCell_cony" runat="server" CssClass="textbox" TabIndex="62" MaxLength="50" /><asp:FilteredTextBoxExtender
												ID="fte62" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCell_cony"
												ValidChars="" />
										</td>
										<td align="left">Estrato<br />
											<asp:TextBox ID="txtEstrato_Cony" runat="server" CssClass="textbox" TabIndex="68" MaxLength="2"
												Width="50%" />
											<asp:FilteredTextBoxExtender
												ID="ft63" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtEstrato_Cony"
												ValidChars="" />
										</td>
										<td style="width: 25%; text-align:left">Ocupación<br />
											<asp:DropDownList ID="ddlOcupacion_Cony" runat="server" CssClass="textbox" TabIndex="61" Width="90%">
											</asp:DropDownList></td>
									</tr>
									<tr>
										<td align="left" colspan="4">
											<hr />
										</td>
									</tr>
									<tr>
										<td align="left"><strong>Información Laboral</strong></td>
									</tr>
									<tr>
										<td align="left" colspan="2" style="width: 25%">Empresa<br />
											<asp:TextBox ID="txtempresa_cony" runat="server" CssClass="textbox" TabIndex="63"
												MaxLength="128" Style="text-transform: uppercase" Width="85%" /></td>
										<td align="left">Antiguedad Cargo&#160;&#160;
											<br />
											<asp:TextBox ID="txtantiguedad_cony" runat="server" CssClass="textbox" TabIndex="64"
												MaxLength="8" /><asp:FilteredTextBoxExtender ID="fte63" runat="server" Enabled="True"
													FilterType="Numbers, Custom" TargetControlID="txtantiguedad_cony" ValidChars="" />
										</td>
										<td align="left">Tipo Contrato<br />
											<asp:DropDownList ID="ddlcontrato_cony" runat="server" CssClass="textbox" TabIndex="65">
												<asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
											</asp:DropDownList></td>
									</tr>
									<tr>
										<td align="left" style="width: 25%">Cargo<br />
											<asp:DropDownList ID="ddlCargo_cony" runat="server" CssClass="textbox" TabIndex="66"
												Width="90%">
											</asp:DropDownList></td>
										<td align="left">Telef Empresa
											<br />
											<asp:TextBox ID="txtTelefonoEmp_Cony" runat="server" CssClass="textbox" TabIndex="67" MaxLength="20" /><asp:FilteredTextBoxExtender
												ID="fte61" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelefonoEmp_Cony"
												ValidChars="" />
										</td>
									</tr>
								</table>
				
								<asp:AccordionPane ID="AccordionPane2" runat="server" Visible="true">
									<Header>
										<asp:Image ID="Image6" runat="server" DescriptionUrl="../../../Images/expand.png" />Información del Conyuge</Header>
									<Content>
										<asp:UpdatePanel ID="updConyuge" runat="server" RenderMode="Inline" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:CheckBox ID="chkConyuge" runat="server" AutoPostBack="true" TabIndex="53" ForeColor="Red"
													OnCheckedChanged="chkConyuge_CheckedChanged" Text="&lt;strong&gt;Desea Activar los Datos del Conyuge?&lt;/strong&gt;" /><br />
												<asp:Panel ID="PanelConyuge" runat="server">
													<table width="100%">
														<tr>
															<td style="text-align: left; width: 100%">Dirección
																<asp:TextBox ID="txtdirec_cony" runat="server" CssClass="textbox" Width="60%" TabIndex="71"></asp:TextBox></td>
														</tr>
													</table>
												</asp:Panel>
												<asp:Panel ID="Panel3" runat="server" Style="margin-right: 111px" Visible="False">
													<table border="0" cellpadding="5" cellspacing="0" width="100%">
														<tr>
															<td align="left">
																<br />
															</td>
															<td align="left">
																<asp:DropDownList ID="ddlActividad_cony" runat="server" Width="335px"></asp:DropDownList><br />
																<asp:DropDownList ID="DropDownList1" runat="server" Width="335px"></asp:DropDownList>Total Ingresos<br />
																<asp:TextBox ID="txtSalario_cony" runat="server" CssClass="textbox" MaxLength="128" /><br />
																Residente
																<asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
																	<asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
																	<asp:ListItem Value="N">No</asp:ListItem>
																</asp:RadioButtonList>Cod asesor
																<br />
																<asp:TextBox ID="txtCodasesor_cony" runat="server" CssClass="textbox" MaxLength="128">1</asp:TextBox><br />
																Fecha Residencia&#160;&#160;<br />
																<asp:TextBox ID="txtfecha_residen" runat="server" CssClass="textbox" MaxLength="128">01/01/2000</asp:TextBox><br />
																Teléfono Arrendador&#160;&#160;<br />
																<asp:TextBox ID="txttellArenda_cony" runat="server" CssClass="textbox" MaxLength="128" /><br />
																Arrendador&#160;&#160;<br />
																<asp:TextBox ID="txtarren_cony" runat="server" CssClass="textbox" MaxLength="128" /><br />
																Tratamiento&#160;&#160;<br />
																<asp:TextBox ID="txttratamiento_cony" runat="server" CssClass="textbox" MaxLength="128" /><br />
																Tipo Vivienda<asp:RadioButtonList ID="rbtipovivi_cony" runat="server" RepeatDirection="Horizontal">
																	<asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
																	<asp:ListItem Value="A">Arrendada</asp:ListItem>
																	<asp:ListItem Value="F">Familiar</asp:ListItem>
																</asp:RadioButtonList>Ciudad Nacimiento&#160;<br />
																<asp:DropDownList ID="ddlciudadNac_cony" runat="server"></asp:DropDownList><br />
																Escolaridad<br />
																<asp:DropDownList ID="ddlescolaridad_cony" runat="server"></asp:DropDownList><br />
																Estado Civil&#160;&#160;<br />
																&#160;<asp:DropDownList ID="ddlestadoCiv_cony" runat="server"></asp:DropDownList><br />
																<%--Fecha Expedición<br />
																<asp:TextBox ID="txtfechaExp_cony" runat="server" CssClass="textbox" MaxLength="128" /><asp:FilteredTextBoxExtender
																	ID="ftefechaCony" runat="server" Enabled="True" FilterType="Numbers, Custom"
																	TargetControlID="txtfechaExp_cony" ValidChars="/" />
																<br />--%>
																Tipo persona
																<asp:RadioButtonList ID="rblTipoPersona" runat="server" RepeatDirection="Horizontal">
																	<asp:ListItem Selected="True" Value="N">Natural</asp:ListItem>
																	<asp:ListItem Value="J">Juridica</asp:ListItem>
																</asp:RadioButtonList>Digito Verificación&#160;&#160;<br />
																<asp:TextBox ID="txtdigito_cony" runat="server" CssClass="textbox" MaxLength="128" /><br />
																Ciudad Residencia<br />
																<asp:DropDownList ID="ddlLugarResid_cony" runat="server"></asp:DropDownList><br />
																Teléfono Empresa&#160;
																<asp:CompareValidator ID="cvtxtTelefonoempresa" runat="server" ControlToValidate="txtTelefonoempresa"
																	Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
																	Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" /><br />
																<asp:TextBox ID="txttel_empre_cony" runat="server" CssClass="textbox" MaxLength="128" /><asp:Panel
																	ID="Panel2" runat="server">
																	Razon Social
																	<asp:TextBox ID="txtRazonSoc_cony" runat="server" CssClass="textbox" MaxLength="128" />
																</asp:Panel>
															</td>
														</tr>
													</table>
												</asp:Panel>
											</ContentTemplate>
											<Triggers>
												<asp:AsyncPostBackTrigger ControlID="chkConyuge" EventName="CheckedChanged" />
											</Triggers>
										</asp:UpdatePanel>
									</Content>
								</asp:AccordionPane>
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom:15px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Beneficiarios</strong>
										</td>
									</tr>
								</table>
								<asp:Button ID="btnAddRow" runat="server" CssClass="btn8" TabIndex="49" OnClick="btnAddRow_Click"
									OnClientClick="btnAddRow_Click" Text="+ Adicionar Detalle" />
								<asp:GridView ID="gvBeneficiarios"
									runat="server" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
									BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
									ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
										PageSize="10" ShowFooter="True" Style="font-size: xx-small; padding-top:10px;" Width="80%">
									<AlternatingRowStyle BackColor="White" />
									<Columns>
										<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
										<asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center">
											<ItemTemplate>
												<asp:Label ID="lblidbeneficiario" runat="server" Text='<%# Bind("idbeneficiario") %>'
													Visible="false"></asp:Label><asp:Label ID="lblParentezco" runat="server" Text='<%# Bind("parentesco") %>'
														Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlParentezco" runat="server"
															AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
															Style="font-size: xx-small; text-align: left" Width="120px">
														</cc1:DropDownListGrid>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="120px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<cc1:TextBoxGrid ID="txtIdentificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
													Text='<%# Bind("identificacion_ben") %>' Width="100px" OnTextChanged="TxtIde_TextChanged" AutoPostBack="true" CommandArgument="<%#Container.DataItemIndex %>"> </cc1:TextBoxGrid>
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
												<uc1:fecha ID="txtFechaNacimientoBen" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left"
													Text='<%# Eval("fecha_nacimiento_ben", "{0:" + FormatoFecha() + "}") %>' />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Left" Width="100px" />
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
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom:15px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Parentescos</strong>
										</td>
									</tr>
								</table>
								<asp:Button ID="btnAgregarFilaParentesco" runat="server" CssClass="btn8" TabIndex="51" OnClick="btnAgregarFilaParentesco_Click" Text="+ Adicionar Detalle" />
								<asp:GridView ID="gvParentescos"
									runat="server" AllowPaging="True" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
									BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="consecutivo"
									ForeColor="Black" GridLines="Both" OnRowDataBound="gvParentescos_RowDataBound"
									OnRowDeleting="gvParentescos_RowDeleting" PageSize="10" ShowFooter="True"
									Style="font-size: xx-small; padding-top:10px;" Width="80%">
									<AlternatingRowStyle BackColor="White" />
									<Columns>
										<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
										<asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:Label ID="lblCodParentesco" runat="server" Text='<%# Bind("codigoparentesco") %>'
													Visible="false"></asp:Label>
												<cc1:DropDownListGrid ID="ddlParentesco" runat="server"
													AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
													Style="font-size: xx-small; text-align: left" Width="120px" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="120px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Tipo Identificacion" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:Label ID="lblTipoIdentificacion" runat="server" Text='<%# Bind("codigotipoidentificacion") %>'
													Visible="false"></asp:Label>
												<cc1:DropDownListGrid ID="ddlTipoIdentificacion" runat="server"
													AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
													Style="font-size: xx-small; text-align: left" Width="120px" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="120px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Identificacion" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:TextBox ID="txtIdenficacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
													Text='<%# Bind("identificacion") %>' onkeypress="return isNumber(event)" Width="120px"></asp:TextBox>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="120px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Nombres" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
													Text='<%# Bind("nombresapellidos") %>' Width="200px"></asp:TextBox>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="200px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Act. Economica" ItemStyle-HorizontalAlign="Center">
											<ItemTemplate>
												<asp:Label ID="lblCodigoActividadEconomica" runat="server" Text='<%# Bind("codigoactividadnegocio") %>'
													Visible="false"></asp:Label>
												<cc1:DropDownListGrid ID="ddlActividadEconomica" runat="server"
													AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
													Style="font-size: xx-small; text-align: left" Width="120px" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="120px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Empresa" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
													Text='<%# Bind("empresa") %>' Width="200px"></asp:TextBox>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="200px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Cargo" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:TextBox ID="txtCargo" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
													Text='<%# Bind("cargo") %>' Width="200px"></asp:TextBox>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="200px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Ing. Mensual" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<asp:TextBox ID="txtIngresoMensual" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
													Text='<%# Bind("ingresomensual") %>' onkeypress="return isNumber(event)" Width="120px"></asp:TextBox>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="120px" />
										</asp:TemplateField>
										<asp:TemplateField  HeaderText="Estatus" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
											<ItemTemplate>
													<asp:CheckBoxList ID="chListaEstatus" runat="server" RepeatDirection="Horizontal" Width="280px"> 
														<asp:ListItem Value="0" Text="Empleado de la O.S."></asp:ListItem>
														<asp:ListItem Value="1" Text="Miembro de administración"></asp:ListItem>
														<asp:ListItem Value="2" Text="Miembro de control"></asp:ListItem>
														<asp:ListItem Value="3" Text="Es PEP"></asp:ListItem>
													</asp:CheckBoxList>
												</ItemTemplate>
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
							</article>
							<article id="ntab4">
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Información Económica</strong>
										</td>
									</tr>
								</table>
								<table cellpadding="0" style="width: 100%; padding-top: 15px;">
									<tr>
										<td colspan="6">
											<hr style="width: 100%" />
										</td>
									</tr>
										<tr>
											<td class="gridHeader" colspan="3" style="height: 20px; width: 100%;"><strong>Ingresos Mensuales</strong> </td>
											<td class="gridHeader" colspan="3" style="height: 20px; width: 100%;"><strong>Egresos</strong> </td>
										</tr>
										<tr>
											<td style="text-align: left; width: 20%;"></td>
											<td style="text-align: left; width: 15%;">Solicitante </td>
											<td style="text-align: left; width: 15%;">Cónyuge </td>
											<td style="text-align: left; width: 20%;"></td>
											<td style="text-align: left; width: 15%;">Solicitante </td>
											<td style="text-align: left; width: 15%;">Cónyuge </td>
										</tr>
										<tr>
											<td style="text-align: left;">Sueldo </td>
											<td style="text-align: left;">
												<asp:TextBox ID="cod_per" runat="server" Visible="false"></asp:TextBox>
												<asp:TextBox ID="cod_cony" runat="server" Visible="false"></asp:TextBox>
												<asp:TextBox ID="txtsueldo_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
													TabIndex="72"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte80" runat="server" Enabled="True" FilterType="Numbers, Custom"
														TargetControlID="txtsueldo_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtsueldo_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="73"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte81" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txtsueldo_cony" ValidChars="." />
											</td>
											<td style="text-align: left;">Cuota Hipoteca </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txthipoteca_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="74"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte82" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txthipoteca_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txthipoteca_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="75"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte83" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txthipoteca_cony" ValidChars="." />
											</td>
										</tr>
										<tr>
											<td style="text-align: left;">Honorarios </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txthonorario_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
													TabIndex="76"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte84" runat="server" Enabled="True" FilterType="Numbers, Custom"
														TargetControlID="txthonorario_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txthonorario_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="77"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte85" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txthonorario_cony" ValidChars="." />
											</td>
											<td style="text-align: left;">Cuota Tarjeta de Crédito </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txttarjeta_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="78"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte86" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txttarjeta_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txttarjeta_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="79"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte87" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txttarjeta_cony" ValidChars="." />
											</td>
										</tr>
										<tr>
											<td style="text-align: left;">Arrendamientos </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtarrenda_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
													TabIndex="80"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte88" runat="server" Enabled="True" FilterType="Numbers, Custom"
														TargetControlID="txtarrenda_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtarrenda_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="81"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte89" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txtarrenda_cony" ValidChars="." />
											</td>
											<td style="text-align: left;">Cuota Otros Préstamos </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtotrosPres_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="82"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte90" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txtotrosPres_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtotrosPres_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="83"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte91" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txtotrosPres_cony" ValidChars="." />
											</td>
										</tr>
										<tr>
											<td style="text-align: left;">Otros Ingresos </td>
											<td style="text-align: left;">
												<asp:UpdatePanel ID="UpdatePanel2" runat="server">
													<ContentTemplate>
														<asp:TextBox ID="txtotrosIng_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
															TabIndex="84"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte92" runat="server" Enabled="True" FilterType="Numbers, Custom"
																TargetControlID="txtotrosIng_soli" ValidChars="." />
														<asp:PopupControlExtender ID="PopupControlExtender2" runat="server"
															Enabled="True" ExtenderControlID="" TargetControlID="txtotrosIng_soli"
															PopupControlID="panelConceptoOtrosSoli" OffsetY="22">
														</asp:PopupControlExtender>
														<asp:Panel ID="panelConceptoOtrosSoli" runat="server" Height="70px" Width="250px"
															BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
															ScrollBars="Auto" BackColor="#CCCCCC">
															<table>
																<tr>
																	<td style="text-align: left;">Concepto otros</td>
																	<td style="text-align: left;">
																		<asp:TextBox ID="txtConceptoOtros_soli" runat="server" TabIndex="88" CssClass="textbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
																	</td>
																</tr>
															</table>
														</asp:Panel>
													</ContentTemplate>
												</asp:UpdatePanel>
											</td>
											<td style="text-align: left;">
												<asp:UpdatePanel ID="upConceptoOtrosCony" runat="server">
													<ContentTemplate>
														<asp:TextBox ID="txtotrosIng_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="85"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte93" runat="server" Enabled="True" FilterType="Numbers, Custom"
															TargetControlID="txtotrosIng_cony" ValidChars="." />
														<asp:PopupControlExtender ID="PopupControlExtender1" runat="server"
															Enabled="True" ExtenderControlID="" TargetControlID="txtotrosIng_cony"
															PopupControlID="panelConceptoOtrosCony" OffsetY="22">
														</asp:PopupControlExtender>
														<asp:Panel ID="panelConceptoOtrosCony" runat="server" Height="70px" Width="250px"
															BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
															ScrollBars="Auto" BackColor="#CCCCCC">
															<table>
																<tr>
																	<td style="text-align: left;">Concepto otros</td>
																	<td style="text-align: left;">
																		<asp:TextBox ID="txtConceptoOtros_cony" runat="server" TabIndex="89" CssClass="textbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
																	</td>
																</tr>
															</table>
														</asp:Panel>
													</ContentTemplate>
												</asp:UpdatePanel>
											</td>
											<td style="text-align: left;">Gastos Familiares </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtgastosFam_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="86"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte94" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txtgastosFam_soli" ValidChars="." />
											</td>
											<td style="text-align: left;">
												<asp:TextBox ID="txtgastosFam_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="87"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte95" runat="server" Enabled="True" FilterType="Numbers, Custom"
													TargetControlID="txtgastosFam_cony" ValidChars="." />
											</td>
										</tr>
										<tr>
											<td colspan="4"></td>
										</tr>
										<div>
											<tr>
												<td style="text-align: left;"></td>
												<td style="text-align: left;"></td>
												<td style="text-align: left;"></td>
												<td style="text-align: left;">Descuentos por Nomina </td>
												<td style="text-align: left;">
													<asp:TextBox ID="txtnomina_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="90"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte96" runat="server" Enabled="True" FilterType="Numbers, Custom"
														TargetControlID="txtnomina_soli" ValidChars="." />
												</td>
												<td style="text-align: left;">
													<asp:TextBox ID="txtnomina_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="91"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte97" runat="server" Enabled="True" FilterType="Numbers, Custom"
														TargetControlID="txtnomina_cony" ValidChars="." />
												</td>
											</tr>
										</div>
										<tr>
											<td colspan="6">
												<hr style="width: 100%" />
											</td>
										</tr>
										<tr>
											<td style="text-align: left;">Total Ingresos </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txttotalING_soli" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
											<td style="text-align: left;">
												<asp:TextBox ID="txttotalING_cony" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
											<td style="text-align: left;">Total Egresos </td>
											<td style="text-align: left;">
												<asp:TextBox ID="txttotalEGR_soli" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
											<td style="text-align: left;">
												<asp:TextBox ID="txttotalEGR_cony" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
											<asp:HiddenField ID="hdtotalING_soli" runat="server" ClientIDMode="Static" />
											<asp:HiddenField ID="hdtotalING_cony" runat="server" ClientIDMode="Static" />
											<asp:HiddenField ID="hdtotalEGR_soli" runat="server" ClientIDMode="Static" />
											<asp:HiddenField ID="hdtotalEGR_cony" runat="server" ClientIDMode="Static" />
										</tr>
									</tr>
									<tr>
										<td colspan="6">
											<hr style="width: 100%" />
										</td>
									</tr>
								</table>

								<table cellpadding="0" style="width: 100%;">
									<tr>
										<td style="text-align: left; width: 20%;"></td>
										<td style="text-align: left; width: 15%;">Solicitante </td>
										<td style="text-align: left; width: 15%;">Cónyuge </td>
										<td style="text-align: left; width: 50%;"></td>
									</tr>
									<tr>
										<td style="text-align: left;">Total Activos </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtactivos_soli" runat="server" CssClass="textbox numeric"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="fte6" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtactivos_soli" ValidChars="." />
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtactivos_conyuge" runat="server" CssClass="textbox numeric"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="fte7" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtactivos_conyuge" ValidChars="." />
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">Total Pasivos </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtpasivos_soli" runat="server" CssClass="textbox numeric"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="fte8" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtpasivos_soli" ValidChars="." />
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtpasivos_conyuge" runat="server" CssClass="textbox numeric"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="fte9" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtpasivos_conyuge" ValidChars="." />
										</td>
									</tr>
									<tr>
										<td style="text-align: left;">Total Patrimonio </td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtpatrimonio_soli" runat="server" CssClass="textbox numeric"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="fte12" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtpatrimonio_soli" ValidChars="." />
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtpatrimonio_conyuge" runat="server" CssClass="textbox numeric"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="fte13" runat="server" Enabled="True" FilterType="Numbers, Custom"
												TargetControlID="txtpatrimonio_conyuge" ValidChars="." />
										</td>
									</tr>
								</table>
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Información financiera</strong>
										</td>
									</tr>
								</table>
								<asp:Button ID="btnAgregarFila" runat="server" CssClass="btn8" TabIndex="51" OnClick="btnAgregarFila_Click"
									OnClientClick="btnAgregarFila_Click" Text="+ Adicionar Detalle" />
								<asp:GridView ID="gvCuentasBancarias"
										runat="server" AllowPaging="True" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
										BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idcuentabancaria"
										ForeColor="Black" GridLines="Both" OnRowDataBound="gvCuentasBancarias_RowDataBound"
										OnRowDeleting="gvCuentasBancarias_RowDeleting" PageSize="10" ShowFooter="True"
										Style="font-size: xx-small; padding-top:10px" Width="80%">
										<AlternatingRowStyle BackColor="White" />
										<Columns>
											<asp:CommandField ButtonType="Image" EditImageUrl="../../../Images/gr_edit.jpg" ShowEditButton="true"
												Visible="false" />
											<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
											<asp:TemplateField HeaderText="CodigoCuenta" ItemStyle-HorizontalAlign="Center" Visible="false">
												<ItemTemplate>
													<asp:Label ID="lblidcuentabancaria" runat="server" Text='<%# Bind("idcuentabancaria") %>'></asp:Label>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Tipo de Cuenta" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:Label ID="lbltipoProducto" runat="server" Text='<%# Bind("tipo_cuenta") %>'
														Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddltipoProducto" runat="server"
															AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
															Style="font-size: xx-small; text-align: left" Width="120px">
														</cc1:DropDownListGrid>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Número de Cuenta" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:TextBox ID="txtnum_Producto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
														Text='<%# Bind("numero_cuenta") %>' Width="140px"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="200px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Entidad Financiera" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:Label ID="lblentidad" runat="server" Text='<%# Bind("cod_banco") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
														ID="ddlentidad" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>"
														CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px">
													</cc1:DropDownListGrid>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:TextBox ID="txtsucursal" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
														Text='<%# Bind("sucursal") %>' Width="140px"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="200px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="F. Aprobación" ItemStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<uc1:fecha ID="txtfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
														style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha_apertura", "{0:" + FormatoFecha() + "}") %>'
														TipoLetra="XX-Small" Width_="80" />
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal" ItemStyle-CssClass="gridIco">
												<ItemTemplate>
													<asp:UpdatePanel ID="updcheck1" runat="server">
														<ContentTemplate>
															<cc1:CheckBoxGrid ID="cbSeleccionar" runat="server" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("principal"))%>'
																CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnCheckedChanged="cbSeleccionar_CheckedChanged" />
														</ContentTemplate>
														<Triggers>
															<asp:AsyncPostBackTrigger ControlID="cbSeleccionar" EventName="CheckedChanged" />
														</Triggers>
													</asp:UpdatePanel>
												</ItemTemplate>
												<HeaderStyle CssClass="gridIco"></HeaderStyle>
												<ItemStyle CssClass="gridIco"></ItemStyle>
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
									<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
										<tr style="text-align:center;">
											<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
												<strong>Información Transacciones Y Productos En El Exterior</strong>
											</td>
										</tr>
									</table>
									<asp:CheckBox ID="chkMonedaExtranjera" runat="server" AutoPostBack="true"
										OnCheckedChanged="chkMonedaExtranjera_CheckedChanged" Text="&lt;strong&gt;¿Maneja moneda extranjera?&lt;/strong&gt;" /><br />
									<br />
									<asp:Panel ID="panelMonedaExtranjera" runat="server" Visible="false">
										<asp:Button ID="btnAgregarFilaM" runat="server" CssClass="btn8" OnClick="btnAgregarFilaM_Click"
											OnClientClick="btnAgregarFilaM_Click" Text="+ Adicionar Detalle" /><br />
										<br />
										<asp:GridView ID="gvMonedaExtranjera" HorizontalAlign="Center" DataKeyNames="cod_moneda_ext"
											runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
											BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
											OnRowDeleting="gvMonedaExtranjera_RowDeleting" PageSize="10" ShowFooter="True"
											Style="font-size: xx-small" Width="100%">
											<AlternatingRowStyle BackColor="White" />
											<Columns>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
													</ItemTemplate>
													<ItemStyle Width="16px" />
												</asp:TemplateField>
												<%--<asp:TemplateField>
													<ItemTemplate>
														<asp:ImageButton runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Edit" />
													</ItemTemplate>
													<ItemStyle Width="16px" />
												</asp:TemplateField>    --%>
												<asp:TemplateField HeaderText="CodMoneda" ItemStyle-HorizontalAlign="Center" Visible="false">
													<ItemTemplate>
														<asp:Label ID="lblCodMoneda" runat="server" Text='<%# Bind("cod_moneda_ext") %>'></asp:Label>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Número de Cuenta" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNumCuentaExt" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" MaxLength="35"
															Text='<%# Bind("num_cuenta_ext") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Nombre del Banco" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomBancoExt" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("banco_ext") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Pais" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomPais" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("pais") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomCiudad" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("ciudad") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomMoneda" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("moneda") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Operación/Transacción que realiza" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtOperacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("desc_operacion") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
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
									</asp:Panel>

										<asp:CheckBox ID="chkTransaccionExterior" runat="server" OnCheckedChanged="chkTransaccionExterior_CheckedChanged" AutoPostBack="true"
											Text="&lt;strong&gt;¿Posee productos financieros en el exterior?&lt;/strong&gt;" /><br />
									<br />
									<asp:Panel ID="pProductosExt" runat="server" Visible="false">
										<asp:Button ID="btnProductoExt" runat="server" CssClass="btn8" OnClick="btnProductoExt_Click"
											OnClientClick="btnProductoExt_Click" Text="+ Adicionar Detalle" /><br />
										<br />
										<asp:GridView ID="gvProductosExterior" HorizontalAlign="Center" DataKeyNames="cod_moneda_ext"
											runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
											BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
											OnRowDeleting="gvProductosExterior_RowDeleting" PageSize="10" ShowFooter="True"
											Style="font-size: xx-small" Width="100%">
											<AlternatingRowStyle BackColor="White" />
											<Columns>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
													</ItemTemplate>
													<ItemStyle Width="16px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Cod Producto" ItemStyle-HorizontalAlign="Center" Visible="false">
													<ItemTemplate>
														<asp:Label ID="lblCodProducto" runat="server" Text='<%# Bind("cod_moneda_ext") %>'></asp:Label>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Tipo de Producto" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtTipoProducto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("tipo_producto") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="No. Producto" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNumProducto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" MaxLength="35"
															Text='<%# Bind("num_cuenta_ext") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Pais" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomPais" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("pais") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomCiudad" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("ciudad") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:TextBox ID="txtNomMoneda" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("moneda") %>' Width="140px"></asp:TextBox>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="120px" />
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
									</asp:Panel>
									<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
										<tr style="text-align:center;">
											<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
												<strong>Información Activos/Bienes</strong>
											</td>
										</tr>
									</table>
								<asp:Label ID="lblInfoBienesActivos" Text="Para poder agregar activos relacionados con el asociado deberá primero grabar la afiliación" runat="server" Style="color: red" />
									<br />
									<br />
								<asp:Button ID="btnBienesActivos" runat="server" CssClass="btn8" TabIndex="90" OnClick="InicializarModal" OnClientClick="javascript: LinkButton1.click()" Text="+ Adicionar Detalle" />
								<asp:GridView ID="gvBienesActivos" runat="server"
									AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White"
									BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
									ForeColor="Black" GridLines="Vertical" DataKeyNames="IdActivo"
									OnRowCommand="gvBienesActivos_OnRowCommand" OnRowEditing="gvBienesActivos_RowEditing"
									OnRowDeleting="gvBienesActivos_RowDeleting" Width="90%">
									<AlternatingRowStyle BackColor="White" />
									<Columns>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
											</ItemTemplate>
											<ItemStyle Width="16px" />
										</asp:TemplateField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Edit" />
											</ItemTemplate>
											<ItemStyle Width="16px" />
										</asp:TemplateField>
										<%--<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
											<ItemStyle Width="16px" />
										</asp:CommandField>
										<asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
											<ItemStyle Width="16px" />
										</asp:CommandField>--%>
										<asp:BoundField DataField="IdActivo" NullDisplayText=" " HeaderText="Código" />
										<asp:BoundField DataField="descripcion_activo" NullDisplayText=" " HeaderText="Tipo de Activo" />
										<asp:BoundField DataField="Descripcion" NullDisplayText=" " HeaderText="Descripción" />
										<asp:BoundField DataField="Fecha_adquisicionactivo" NullDisplayText=" " DataFormatString="{0:d}" HeaderText="Fecha de Adquisición" />
										<asp:BoundField DataField="valor_comercial" NullDisplayText=" " HeaderText="Valor Comercial" DataFormatString="{0:N0}" />
										<asp:BoundField DataField="estado_descripcion" NullDisplayText=" " HeaderText="Estado" />
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
							</article>
							<article id="ntab5">
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Documentos</strong>
										</td>
									</tr>
								</table>
								<asp:Button ID="btnAgregarDocumento" runat="server" CssClass="btn8" TabIndex="51" OnClick="btnAgregarFilaDocumento_Click" Text="+ Adicionar Detalle" />
								<br />        
								<asp:GridView ID="gvDocumentos"
									runat="server" AllowPaging="True" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
									BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idimagen"
									ForeColor="Black" GridLines="Both" OnRowDataBound="gvDocumentos_RowDataBound"
									OnRowDeleting="gvDocumentos_RowDeleting" PageSize="10" ShowFooter="True"
									Style="font-size: xx-small" Width="50%">
									<AlternatingRowStyle BackColor="White" />
									<Columns>
										<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True"/>
										<asp:TemplateField HeaderText="Archivo">
											<ItemTemplate>
												<asp:FileUpload ID="FluArchivo" runat="server" />
												<asp:Label ID="lblArchivo" runat="server" Visible="false"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Nombre">
											<ItemTemplate>
												<asp:Label ID="txtNombreArchivo" runat="server" Text='<%# Bind("idimagen") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField >
											<ItemTemplate>
												<asp:LinkButton ID="lnkView" runat="server" Text="Ver" OnClick="lnkView_Click" CommandArgument='<%#Eval("idimagen") %>'>
                                                              
												</asp:LinkButton>  
											</ItemTemplate>
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
								<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
									<tr style="text-align:center;">
										<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
											<strong>Actividades Culturales, Sociales y Deportivas</strong>
										</td>
									</tr>
								</table>
								<asp:Button ID="btnDetalle" runat="server" CssClass="btn8" TabIndex="90" OnClick="btnDetalle_Click"
									OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
								<asp:GridView ID="gvActividades"
										runat="server" AllowPaging="True" TabIndex="91" AutoGenerateColumns="false" BackColor="White"
										BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idactividad"
										ForeColor="Black" GridLines="Both" OnRowDataBound="gvActividades_RowDataBound"
										OnRowDeleting="gvActividades_RowDeleting" OnRowEditing="gvActividades_RowEditing"
										PageSize="10" ShowFooter="True" Style="font-size: xx-small" Width="80%">
										<AlternatingRowStyle BackColor="White" />
										<Columns>
											<asp:CommandField ButtonType="Image" EditImageUrl="../../../Images/gr_edit.jpg" ShowEditButton="true"
												Visible="false" />
											<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
											<asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
												<ItemTemplate>
													<asp:Label ID="lblactividad" runat="server" Text='<%# Bind("idactividad") %>'></asp:Label>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha" ItemStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<uc1:fecha ID="txtfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
														style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha_realizacion", "{0:" + FormatoFecha() + "}") %>'
														TipoLetra="XX-Small" Width_="80" />
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Actividad" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:Label ID="lblTipoActividad" runat="server" Text='<%# Bind("tipo_actividad") %>'
														Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlActividad" runat="server"
															AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
															Style="font-size: xx-small; text-align: left" Width="120px">
														</cc1:DropDownListGrid>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="120px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
														Text='<%# Bind("descripcion") %>' Width="170px"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="200px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Participante" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:Label ID="lblParticipante" runat="server" Text='<%# Bind("participante") %>'
														Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlParticipante" runat="server"
															AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
															Style="font-size: xx-small; text-align: left" Width="120px">
														</cc1:DropDownListGrid>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="100px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Right">
												<ItemTemplate>
													<asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
														Text='<%# Bind("calificacion") %>' Width="80px"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Right" Width="100px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Duración" ItemStyle-HorizontalAlign="Right">
												<ItemTemplate>
													<asp:TextBox ID="txtDuracion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
														Text='<%# Bind("duracion") %>' Width="100px"></asp:TextBox>
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
									<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
										<tr style="text-align:center;">
											<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
												<strong>Información adicional</strong>
											</td>
										</tr>
									</table>
									<asp:GridView ID="gvInfoAdicional" runat="server" AllowPaging="True" AutoGenerateColumns="false"
										BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="gvInfoAdicional_PageIndexChanging"
										CellPadding="0" DataKeyNames="" ForeColor="Black" GridLines="Both" OnRowDataBound="gvInfoAdicional_RowDataBound"
										PageSize="10" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="False"
										Style="font-size: xx-small" Width="80%">
										<AlternatingRowStyle BackColor="White" />
										<Columns>
											<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<asp:Label ID="lblidinfadicional" runat="server" Text='<%# Bind("idinfadicional") %>'></asp:Label>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="20px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="codigo" ItemStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<asp:Label ID="lblcod_infadicional" runat="server" Text='<%# Bind("cod_infadicional") %>'></asp:Label>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="20px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="left" Width="180px" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Control" ItemStyle-HorizontalAlign="Left">
												<ItemTemplate>
													<asp:Label ID="lblopcionaActivar" runat="server" Text='<%# Bind("tipo") %>' Visible="false"></asp:Label><asp:TextBox ID="txtCadena" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
														Text='<%# Bind("valor") %>' Visible="false" Width="240px"></asp:TextBox><asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
															Text='<%# Bind("valor") %>' Visible="false" Width="150px"> </asp:TextBox><asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
																TargetControlID="txtNumero" ValidChars="" />
													<uc1:fecha ID="txtctlfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
														style="font-size: xx-small; text-align: left" Text='<%# Eval("valor", "{0:" + FormatoFecha() + "}") %>'
														TipoLetra="xx-Small" Visible="false" />
													<asp:Label ID="lblValorDropdown" runat="server" Text='<%# Bind("valor") %>' Visible="false"></asp:Label><asp:Label ID="lblDropdown" runat="server" Text='<%# Bind("items_lista") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
														ID="ddlDropdown" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>"
														CssClass="textbox" Style="font-size: xx-small; text-align: left" Visible="false"
														Width="160px">
													</cc1:DropDownListGrid>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left" Width="180px" />
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
									<table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
										<tr style="text-align:center;">
											<td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
												<strong>Afiliación</strong>
											</td>
										</tr>
									</table>
									<div style="text-align: left">
										<strong>Tipo Cliente</strong> &#160;&#160;&#160;&#160;
										<asp:CheckBox ID="chkAsociado" runat="server" Text=" Asociado " AutoPostBack="true" Checked="true"
											OnCheckedChanged="chkAsociado_CheckedChanged" />
									</div>    
										<table>
											<tr>
												<td colspan="6">                                                        
													<asp:UpdatePanel ID="upPEPS" runat="server" ChildrenAsTriggers="true">
														<ContentTemplate>
															<table>
																<tr>
																	<td style="text-align:left">
																		<asp:CheckBox ID="chkPEPS" runat="server" Text=" PEPS " AutoPostBack="true" OnCheckedChanged="chkPEPS_CheckedChanged" />
																	</td>
																	<td style="text-align:left">
																		<asp:Panel ID="panelPEPS" runat="server">
																			<table>
																				<tr>
																					<td style="text-align: left;">
																						<asp:Label ID="lblCargoPEPS" runat="server" Text="Cargo" /><br />
																						<asp:DropDownList ID="txtCargoPEPS" runat="server" CssClass="textbox" Style="text-align: left" Visible="true" Width="250px" MaxLength="200" />
																					</td>
																					<td style="text-align: left;">
																						<asp:Label ID="lblInstitucion" runat="server" Text="Institución" /><br />
																						<asp:TextBox ID="txtInstitucion" runat="server" CssClass="textbox" Style="text-align: left" Visible="true" Width="200px" MaxLength="200" />
																					</td>
																					<td style="text-align: left;">
																						<asp:Label ID="lblFechaVinculacionPEPS" runat="server" Text="F.Vinculación" /><br />
																						<uc1:fecha ID="txtFechaVinculacionPEPS" runat="server" Enabled="True" style="width: 140px" />
																					</td>
																					<td style="text-align: left;">
																						<asp:Label ID="lblFechaDesvinculacionPEPS" runat="server" Text="F.Desvinculación" /><br />
																						<uc1:fecha ID="txtFechaDesVinculacionPEPS" runat="server" Enabled="True" style="width: 140px" />
																					</td>
																				</tr>
																			</table>
																		</asp:Panel>
																	</td>
																</tr>
																<tr>
																	<td style="text-align:left">
																		<asp:CheckBox ID="chkAdmiRecursosPublicos" runat="server" Text=" Administra Recursos Públicos " />
																	</td>
																</tr>
															</table>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>   
											</tr>
											<tr>
												<td style="text-align: left; width: 155px">Fecha de Afiliación<br />
													<asp:TextBox ID="txtcodAfiliacion" runat="server" CssClass="textbox" Style="text-align: right"
														Visible="false" Width="100px" /><uc1:fecha ID="txtFechaAfili" runat="server" Enabled="True"
															style="width: 140px" />
													<br></br>
												</td>
												<td style="text-align: left; width: 170px">Estado<br />
													<asp:DropDownList ID="ddlEstadoAfi" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
														CssClass="textbox" OnSelectedIndexChanged="ddlEstadoAfi_SelectedIndexChanged"
														Width="160px">
													</asp:DropDownList></td>
												<td style="text-align: left; width: 140px">Fecha de Rétiro<br />
													<asp:Panel ID="panelFecha" runat="server">
														<uc1:fecha ID="txtFechaRetiro" runat="server" Enabled="True" style="width: 140px" />
													</asp:Panel>
												</td>
												<td style="text-align: left; width: 170px">Forma de Pago<br />
													<asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
														OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Width="95%">
														<asp:ListItem Value="1">Caja</asp:ListItem>
														<asp:ListItem Value="2">Nomina</asp:ListItem>
													</asp:DropDownList></td>
												<td style="text-align: left;">
													<asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
													<asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" AutoPostBack="True"
														Width="180px" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
													</asp:DropDownList></td>
												<td style="text-align: left;">
													<asp:Label ID="lblAsociadosEspeciales" runat="server" Text="Asociados especiales" /><br />
													<asp:DropDownList ID="ddlAsociadosEspeciales" runat="server" CssClass="textbox"
														Width="180px">
													</asp:DropDownList>
												</td>
											</tr>
											<tr>
												<td style="text-align: left;">Valor<br />
													<uc1:decimales ID="txtValorAfili" runat="server" style="text-align: right;" AutoPostBack_="false" />
												</td>
												<td style="text-align: left;">Fecha de 1er Pago<br />
													<uc1:fecha ID="txtFecha1Pago" runat="server" style="width: 140px" />
												</td>
												<td style="text-align: left;">Nro Cuotas<br />
													<asp:TextBox ID="txtCuotasAfili" runat="server" CssClass="textbox" Style="text-align: right"
														Width="100px" /><asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
															runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCuotasAfili"
															ValidChars="" />
												</td>
												<td style="text-align: left;">Periodicidad<br />
													<asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="95%"></asp:DropDownList></td>
												<td style="text-align: left;">
													<asp:CheckBox ID="chkAsistioUltAsamblea" runat="server" Text="Asistio a la Ultima Asamblea"
														TextAlign="Left" /></td>
												<td style="text-align: left;">Asesor Comercial:
											<br />
													<asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true">
														<asp:ListItem Text="Seleccione un Item" Value=" " />
													</asp:DropDownList>
												</td>
												</tr>
												<tr>
													<td style="text-align: left;">
														<asp:CheckBox ID="ckAfiliadoOtraOS" runat="server" Text="¿Es asociado a otra O.S.?" OnCheckedChanged="ckAfiliadoOtraOS_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
														</td>
													<td style="text-align: left;">
														<asp:Label ID="lblOtraOS" runat="server" Text="Nombre de la Entidad" Visible="false" />
														<asp:TextBox ID="txtOtraOS" runat="server" CssClass="textbox" Style="text-align: right;" Visible="false" Width="100px" />
													</td>
													<td style="text-align: left;">
														<asp:CheckBox ID="ckCargosOS" runat="server" Text="¿Ha ocupado cargos directivos en O.S.?" OnCheckedChanged="ckCargosOS_CheckedChanged" AutoPostBack="true"/>                                                                
														</td>
													<td style="text-align: left;">
														<asp:Label ID="lblCargoOS" runat="server" Text="Cargo Ocupado" Visible="false"/>
														<asp:TextBox ID="txtCargosDirectivos" runat="server" CssClass="textbox" Style="text-align: right;" Visible="false" Width="100px" />
													</td>
													<%--<td style="text-align: left;"></td>
													<td style="text-align: left;"></td>--%>
													<td style="text-align: left;">No. de Asistencias a Asambleas
														<asp:TextBox ID="txtNoAsistencias" runat="server" CssClass="textbox" Style="text-align: right" Width="100px" />
														<asp:FilteredTextBoxExtender ID="fteAsistencias" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtNoAsistencias"
														ValidChars="" />
													</td>
													<td style="text-align: left;"></td>
												</tr>
										</table>
							</article>
						</div>
					</div>
                </asp:Panel>
                <asp:Panel ID="panelDatos" runat="server">
                    <%--<asp:Accordion ID="acoPersona" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContenido"
                        FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                        <Panes>
                           <asp:AccordionPane ID="acoDatosGenerales" runat="server" Visible="True" >
                                <Content>
                                    <asp:UpdatePanel ID="upTipoVivienda" runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtFechanacimiento" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rblSexo" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rblTipoVivienda" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </Content>
                            </asp:AccordionPane>--%>
                            <%--<asp:AccordionPane ID="acoAfiliacion" runat="server" Visible="True">
                                <Header>
                                    <asp:Image ID="Image2" runat="server" DescriptionUrl="../../../Images/expand.png" />Afiliación
                                </Header>
                                <Content>
                                    <asp:UpdatePanel ID="updAfiliacion" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="panelAfiliacion" runat="server">   
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlEstadoAfi" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkAsociado" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ckAfiliadoOtraOS" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ckCargosOS" EventName="CheckedChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>--%>
                    <div style="visibility: hidden">
                        <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" Text="Click here to change the paragraph style" />
                    </div>
                    <asp:ModalPopupExtender ID="mpeNuevoActividad" runat="server" PopupControlID="panelMostrarModal"
                        TargetControlID="LinkButton1" BackgroundCssClass="backgroundColor" CancelControlID="btnCancelarModal">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="panelMostrarModal" runat="server" BackColor="White" Style="overflow-y: scroll; text-align: left; max-height: 500px; padding: 20px; border: medium groove #0000FF; background-color: #FFFFFF;"
                        Width="700px">
                        <asp:UpdatePanel ID="upReclasificacion" runat="server">
                            <ContentTemplate>
                                <center><strong>ACTIVOS FIJOS</strong></center>
                                <table style="width: 100%">
                                    <tr style="text-align: right">
                                        <td></td>
                                        <td style="width: 120px">
                                            <asp:ImageButton runat="server" ID="btnCancelarModal" ImageUrl="~/Images/btnCancelar.jpg" ToolTip="Cancelar" OnClick="btnCancelarModal_click" />
                                        </td>
                                        <td style="width: 120px">
                                            <asp:ImageButton runat="server" ID="btnGuardarModal" ImageUrl="~/Images/btnGuardar.jpg" ToolTip="Guardar" OnClick="btnGuardarModal_click" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblErrorModal" runat="server" Style="text-align: center" Width="100%" ForeColor="Red"></asp:Label><br />
                                            <asp:Label ID="lblTipoProceso" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="3">&nbsp;
                                            <strong>Datos del Activo:  </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">Identificación<br />
                                            <asp:TextBox ID="txtModalIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="92%" />
                                        </td>
                                        <td style="width: 25%;">Tipo Identificación<br />
                                            <asp:DropDownList ID="ddlModalIdentificacion" Enabled="false" runat="server"
                                                CssClass="textbox" Width="95%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 50%;">Nombres y Apellidos<br />
                                            <asp:TextBox ID="txtModalNombres" Enabled="false" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                                MaxLength="128" Width="95%" /></td>
                                    </tr>
                                </table>
                                <table style="width: 100%" cellpadding="5" cellspacing="0">
                                    <tr>
                                        <td colspan="2" style="width: 148px;">Tipo de Activo<br />
                                            <asp:DropDownList ID="ddlModalTipoActivo" runat="server"
                                                CssClass="textbox" Width="199px" AutoPostBack="true" OnSelectedIndexChanged="ddlModalTipoActivo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">Estado<br />
                                            <asp:DropDownList ID="ddlEstadoModal" runat="server" CssClass="textbox" Width="95%">
                                                <asp:ListItem Value="0" Text="Inactivo" />
                                                <asp:ListItem Value="1" Text="Activo" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Descripción</td>
                                        <td colspan="2" style="text-align: left">
                                            <asp:TextBox ID="txtModalDescripcion" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                                MaxLength="128" Width="350px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Adquisición"></asp:Label>
                                            <uc1:fecha ID="txtModalFechaIni" runat="server" />
                                        </td>
                                        <td style="width: 35%; text-align: left">Valor Comercial:<br />
                                            <asp:TextBox ID="txtModalValorComercial" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 35%; text-align: left">Valor Comprometido:<br />
                                            <asp:TextBox ID="txtModalValorComprometido" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left; width: 684px;">
                                            <hr style="width: 99%" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="panelTipoActivoInmueble" Visible="false" runat="server">
                                    <table>
                                        <tr>
                                            <td class="tdD" style="height: 36px; width: 148px;">Dirección
                                                <asp:TextBox ID="txtModalDireccion" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td class="tdD" style="height: 36px; width: 148px;">Localización
                                                <asp:TextBox ID="txtModalLocalizacion" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td class="tdD" style="width: 148px;">VIS<br />
                                                <asp:DropDownList ID="ddlModalVIS" Width="180px" AutoPostBack="true" class="textbox" runat="server" OnSelectedIndexChanged="ddlModalVIS_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Sin VIS
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="1">Con VIS
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">Nro. Matricula
                                                <asp:TextBox ID="txtModalNoMatricula" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Escritura
                                                <asp:TextBox ID="txtModalEscritura" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Notaria
                                                <asp:TextBox ID="txtModalNotaria" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">Entidad Redescuento<br />
                                                <asp:DropDownList ID="ddlModalEntidadReDesc" runat="server"
                                                    CssClass="textbox" Width="199px">
                                                    <asp:ListItem Value="0">Ninguna</asp:ListItem>
                                                    <asp:ListItem Value="1">FINDETER</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 148px;">Margen Redescuento<br />
                                                <asp:TextBox ID="txtModalmargenReDesc" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtModalmargenReDesc" ValidChars=".," />
                                            </td>
                                            <td style="width: 148px;">Tipo Vivienda<br />
                                                <asp:DropDownList ID="ddlModalTipoVivienda" runat="server"
                                                    CssClass="textbox" Width="199px">
                                                    <asp:ListItem Value="1">Nueva
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="2">Usada
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="3">Mejoramiento
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="4">Lote con servicios
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="5">Construccion en sitio propio
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">Desembolso
                                                <asp:DropDownList ID="ddlModalDesembolso" runat="server"
                                                    CssClass="textbox" Width="199px">
                                                    <asp:ListItem Value="1">Desembolso Directo</asp:ListItem>
                                                    <asp:ListItem Value="2">Desembolso a Constructor</asp:ListItem>
                                                    <asp:ListItem Value="3">Subrogración</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 148px;">Desembolso Directo
                                                <asp:TextBox ID="txtModalDesembolsoDirecto" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Rango Vivienda<br />
                                                <asp:DropDownList ID="ddlModalRangoVivienda" runat="server"
                                                    CssClass="textbox" Width="199px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">
                                                <asp:CheckBox ID="chkHipoteca" runat="server" Text="Hipoteca" Width="199px">
                                                </asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlTipoActivoMaquinaria" Visible="false" runat="server">
                                    <table>
                                        <tr>
                                            <td style="width: 148px;">Marca
                                                <asp:TextBox ID="txtModalMarca" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Referencia
                                                <asp:TextBox ID="txtModalReferencia" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Modelo
                                                <asp:TextBox ID="txtModalModelo" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">Uso<br />
                                                <asp:DropDownList ID="ddlModalUso" Width="180px" class="textbox" runat="server">
                                                    <asp:ListItem Value="1">Particular
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="2">Publico
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 148px;">No.Chasis
                                                <asp:TextBox ID="txtModalNoChasis" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Capacidad
                                                <asp:TextBox ID="txtModalCapacidad" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">No.Serie Motor
                                                <asp:TextBox ID="txtModalNoSerieMotor" runat="server"
                                                    CssClass="textbox" Width="199px" />
                                            </td>
                                            <td style="width: 148px;">Placa
                                                <asp:TextBox ID="txtModalPlaca" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                            <td style="width: 148px;">Color
                                                <asp:TextBox ID="txtModalColor" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">Doc.Importación
                                                <asp:TextBox ID="txtModalDocImportacion" runat="server"
                                                    CssClass="textbox" Width="199px" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Fecha Importación"></asp:Label>
                                                <uc1:fecha ID="txtModalFechaImportacion" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px;">
                                                <asp:CheckBox ID="chkPignorado" runat="server" Text="Pignorado" Width="199px" OnCheckedChanged="chkPignorado_CheckedChanged" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPorcPignorado" runat="server" Text="Porcentaje" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtPorcPignorado" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Panel ID="panelDatosOcultos" runat="server" Visible="false">
                        <table style="width: 100%;" cellpadding="0">
                            <tr>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="rblResidente" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        Visible="False">
                                        <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCod_asesor" runat="server" CssClass="textbox" MaxLength="128"
                                        Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtTratamiento" runat="server" CssClass="textbox" MaxLength="128"
                                        Visible="False" />
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtFecha_residencia" runat="server" CssClass="textbox" MaxLength="128"
                                        Visible="False" />
                                    <asp:CalendarExtender ID="txtFecha_residencia_CalendarExtender" runat="server" DaysModeTitleFormat="<%=FormatoFecha()%>"
                                        Enabled="True" Format="<%=FormatoFecha()%>" TargetControlID="txtFecha_residencia"
                                        TodaysDateFormat="<%=FormatoFecha()%>">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlEstado" runat="server" Visible="False" Width="121px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDigito_verificacion" runat="server" CssClass="textbox" MaxLength="128"
                                        Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
        <table style="width: 80%">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelRiesgo" runat="server" Visible="false" Height="100%">
        <div style="text-align: left">
            <span<asp:Label ID="lblTitulo" runat="server" Text="GESTION DE RIESGO" Font-Bold="True"></asp:Label></span>
        </div>
        <br />
        <div style="text-align: left">
            <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación:"></asp:Label>
            <asp:Label ID="txtIdentificacionRiesgo" runat="server"></asp:Label><br />
            <asp:Label ID="txtNombre" runat="server" Text="Nombre:"></asp:Label>
            <asp:Label ID="txtNombreRiesgo" runat="server"></asp:Label><br />
        </div>
        <br />
              <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" Text="Imprimir" Width="280px" Height="30px" />
        <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle"
            OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 1px" Width="100%"> 
            <asp:TabPanel ID="tabProcuraduria" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    P R O C U R A D U R I A
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe id="Iframe1" name="IframeProcuraduria" width="100%" frameborder="0" src="http://www.procuraduria.gov.co/CertWEB/Certificado.aspx?tpo=1"
                        height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
                    <a href="" onClick="buscarProcuraduria()">Buscar</a> 
                </ContentTemplate>
            </asp:TabPanel>
            <%--<asp:TabPanel ID="tabRegistraduria" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    R E G I S T R A D U R I A
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Label ID="lbltit2" runat="server" Text="C O N S U L T A     R E G I S T R A D U R I A" Font-Bold="True"></asp:Label>
                    <iframe id="Iframe2" name="IframeRegistraduria" width="100%" frameborder="0" src="https://wsp.registraduria.gov.co/estadodocs/?cedula=79533413&buscar4=Buscar"
                        height="600px" runat="server" style="border-style: groove; float: left;" tabindex="2" allowfullscreen></iframe>
                    <a href="" onClick="buscarRegistraduria()">Buscar</a> 
                </ContentTemplate>
            </asp:TabPanel>--%>
            <asp:TabPanel ID="tabPolicia" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    P O L I C I A
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe id="Iframe3" name="IframeRegistraduria" width="100%" frameborder="0" src="https://antecedentes.policia.gov.co:7005/WebJudicial/antecedentes.xhtml"
                        height="600px" runat="server" style="border-style: groove; float: left;" tabindex="2" allowfullscreen></iframe>
                    <a href="" onClick="buscarRegistraduria()">Buscar</a> 
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabOFAQ" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    O F A Q 
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:TextBox ID="txtResultadoOFAQ" runat="server" style="width:100%; height: 100%; border: none" ReadOnly="true"></asp:TextBox>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </asp:Panel>
    <asp:Panel ID="panelFinal" runat="server" Visible="false" Height="600px">
        <div style="text-align: left">
            <asp:Button ID="btnVerData" runat="server" CssClass="btn8" Text="Cerrar Informe"
                OnClick="btnVerData_Click" Width="280px" Height="30px" />
        </div>
        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
            height="100%" runat="server" style="border-style: groove; float: left;"></iframe>
    </asp:Panel>

    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <%--<script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>--%>
</asp:Content>
