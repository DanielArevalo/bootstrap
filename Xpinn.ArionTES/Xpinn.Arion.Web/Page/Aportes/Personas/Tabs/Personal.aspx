<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Personal.aspx.cs" Inherits="Page_Aportes_Personas_Tabs_Personal" CodeBehind="Personal.aspx.cs" EnableEventValidation="false" %>

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
<%@ Register Src="~/General/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="Server">
    <style>
        * {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

        .btn8 {
            background-color: #0099ff;
            color: #fff;
            border: 2px solid #0099ff;
            margin: 10px auto;
            font-size: 12px;
        }

        .required:invalid {
            border: 1px solid #FF3333; /*ff6a00  F28C00*/
            box-shadow: 0 0 3px 0px #FF3333;
        }

        .required:valid {
            border: 1px solid #669900;
            box-shadow: 0 0 1px 0px #669900;
        }

        .Buscarbutton {
            background-image: url(../../../../Images/Lupa.jpg);
        }
        .clsInactivos {
            display: none;
        }
    </style>
    <title></title>
    <script>
        function mostrarDatosArriendo() {
        if ($("#txtArrendador").val() == $("#txtPrimer_nombreE").val() + " " + $("#txtPrimer_apellidoE").val()) {
            ocultarDatosArriendo();
        }
        //MUESTRO LOS DATOS DEL ARRENDAROR
        $("#lblPropietario").show();
        $("#txtArrendador").show();
        if($("#txtArrendador").val() == ""){$("#txtArrendador").val("");}
        $("#lblTelefPropietario").show();
        $("#txtTelefonoarrendador").show();
        if ($("#txtTelefonoarrendador").val() == "") { $("#txtTelefonoarrendador").val(""); }
        $("#lblValorArriendo").show();
        if ($("#txtValorArriendo").val() == "") { $("#txtValorArriendo").val(""); }
        $("#txtValorArriendo").show();
        document.getElementById('<%= txtArrendador.ClientID %>').required = true;
        document.getElementById('<%= txtTelefonoarrendador.ClientID %>').required = true;
        
    }
    function ocultarDatosArriendo() {
        //OCULTO LO DATOS DEL ARRENDADOR 
        $("#lblPropietario").hide();
        $("#txtArrendador").hide(); $("#txtArrendador").val($("#txtPrimer_nombreE").val() + " " + $("#txtPrimer_apellidoE").val());
        $("#lblTelefPropietario").hide();
        $("#txtTelefonoarrendador").hide(); $("#txtTelefonoarrendador").val($("#txtTelefonoE").val());
        $("#lblValorArriendo").hide(); $("#txtValorArriendo").val(0);
        $("#txtValorArriendo").hide(); 
    }
    function validar_vivienda(valor) {
        document.getElementById('<%= txtArrendador.ClientID %>').required = true;
        document.getElementById('<%= txtTelefonoarrendador.ClientID %>').required = true;
        if (valor == "A") {
            mostrarDatosArriendo();
            document.getElementById('lblPropietario').innerHTML = "Nombre Arrendador *";
            document.getElementById('lblTelefPropietario').innerHTML = "Teléfono Arrendador *";
            document.getElementById('lblValorArriendo').innerHTML = "Valor Arriendo *";
        }
        else if (valor == "P" || valor == "F") {
            mostrarDatosArriendo();
            document.getElementById('lblPropietario').innerHTML = "Nombre Propietario *";
            document.getElementById('lblTelefPropietario').innerHTML = "Teléfono Propietario *";
            document.getElementById('lblValorArriendo').innerHTML = "Gastos Vivienda *";
            if (valor == "P") {
                document.getElementById('<%= txtArrendador.ClientID %>').required = false;
                document.getElementById('<%= txtTelefonoarrendador.ClientID %>').required = false;
                ocultarDatosArriendo();
            }
        }
    }
    </script>
</head>


<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:Label ID="lblerror" Text="" runat="server" />
        </div>
        <div id="Contenido">
            <table style="text-align: left; padding-top: 0px; width: 100%;">
                <tr>
                    <td style="text-align: left; width: 35%">
                        <asp:CheckBox ID="CheckAct" runat="server" Text="Actualizacion de datos" />
                    </td>
                    <td style="text-align: left; width: 65%">
                        <asp:Label ID="lblfechaAct" Text="Fecha de actualizacon" runat="server" />
                        <br />
                        <asp:TextBox ID="txtfechaAct" type="date" runat="server" Width="170px" name="txtfechaAct" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblhora" Text="" runat="server" />
                        <asp:TextBox ID="txtmayoredad" runat="server" CssClass="textbox" MaxLength="12" Width="170px" Style="visibility: hidden" TabIndex="22" />
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 5px; width: 100%;">
                <tr>
                    <td colspan="9" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                        <strong>Datos Básicos</strong>
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td style="text-align: left; width: 60%;" colspan="2">
                        <strong>Datos básicos</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 35%">Tipo Identificación *
					    <br />
                        <asp:DropDownList ID="ddlTipoE" runat="server" CssClass="textbox required" Width="170px" TabIndex="1" required="required">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 35%">No. Identificación *
                         <br />
                        <asp:TextBox ID="txtIdentificacionE" runat="server" CssClass="textbox required" MaxLength="20" OnTextChanged="txtIdentificacionE_TextChanged" AutoPostBack="true" TabIndex="2" Width="170px" required="required" />
                    </td>
                    <td rowspan="5" style="text-align: center; width: 30%;">
                        <asp:FileUpload ID="fuFoto" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                            Height="20px" ToolTip="Seleccionar el archivo que contiene la foto" Width="200px" />
                        <asp:HiddenField ID="hdFileName" runat="server" />
                        <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                        <asp:LinkButton ID="linkBt" runat="server" OnClick="linkBt_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                        <br />
                        <asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
                        <br />
                        <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar Imagen" Font-Size="xx-Small"
                            Height="20px" Width="100px" OnClick="btnCargarImagen_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                        <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="False"
                            MaxLength="80" Visible="False" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Tipo Persona *<br />
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rblTipo_persona" runat="server" RepeatDirection="Horizontal"
                                    Width="170px" Enabled="False">
                                    <asp:ListItem Selected="True">Natural</asp:ListItem>
                                    <asp:ListItem>Jurídica</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left;">Oficina *<br />
                        <asp:DropDownList ID="ddloficina" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="3" required="required" Enabled="True">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">F. Expedición *<br />
                        <asp:TextBox ID="txtFechaexpedicion" class="required" type="date" runat="server" name="user_date" TabIndex="4" required="required" />
                    </td>
                    <td style="text-align: left;">Lugar Expedición *<br />
                        <asp:DropDownList ID="ddlLugarExpedicion" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="5" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 35%">Primer Nombre *<br />
                        <asp:TextBox ID="txtPrimer_nombreE" runat="server" CssClass="textbox required" MaxLength="100"
                            Style="text-transform: uppercase" Width="90%" TabIndex="6" required="required"/>
                        <asp:FilteredTextBoxExtender ID="fte50" runat="server" Enabled="True" TargetControlID="txtPrimer_nombreE"
                            ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                    </td>
                    <td style="text-align: left; width: 35%;">Segundo Nombre<br />
                        <asp:TextBox ID="txtSegundo_nombreE" runat="server" CssClass="textbox" MaxLength="100"
                            Style="text-transform: uppercase" Width="90%" TabIndex="7" />
                        <asp:FilteredTextBoxExtender ID="fte51" runat="server" Enabled="True" TargetControlID="txtSegundo_nombreE"
                            ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 35%">Primer Apellido *<br />
                        <asp:TextBox ID="txtPrimer_apellidoE" runat="server" CssClass="textbox required" MaxLength="100"
                            Width="90%" Style="text-transform: uppercase" TabIndex="8" required="required" />
                        <asp:FilteredTextBoxExtender ID="fte52" runat="server" Enabled="True" TargetControlID="txtPrimer_apellidoE"
                            ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                    </td>
                    <td style="text-align: left; width: 35%">Segundo Apellido<br />
                        <asp:TextBox ID="txtSegundo_apellidoE" runat="server" CssClass="textbox" MaxLength="100"
                            Width="90%" Style="text-transform: uppercase" TabIndex="9" />
                        <asp:FilteredTextBoxExtender ID="fte53" runat="server" Enabled="True" TargetControlID="txtSegundo_apellidoE"
                            ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                        <strong>Datos de Localización</strong>
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td style="text-align: left; width: 60%;" colspan="4">
                        <strong>Información de Residencia</strong>
                    </td>
                </tr>
                <tr>
                    <%--<td style="text-align: left; width: 170px">Tipo de Ubicación *<br />
                        <asp:DropDownList ID="ddlTipoUbic" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="10" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Rural"></asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                    <td style="text-align: left;" colspan="4">Dirección de Residencia *	<br />
                        <label id="msgTd" style="color:#FF3333;box-shadow: 0 0 1px 0px #FF3333;">Por favor seleccione un tipo de residencia</label>		
                        <%--<asp:TextBox ID="txtDireccionE" runat="server" Width="90%" CssClass="textbox required" TabIndex="11" required="required"></asp:TextBox>--%>
                        <uc4:Direccion ID="txtDireccionE" runat="server" Width="90%" CssClass="textbox required"
                            TabIndex="11" required="required"></uc4:Direccion>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 250px">Ciudad de Residencia *<br />
                        <asp:DropDownList ID="ddlCiudadResidencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="buscarBarrioResidencia" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="90%" TabIndex="12" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 250px">Barrio de Residencia *<br />
                        <asp:DropDownList ID="ddlBarrioResid" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="90%" TabIndex="13" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 150px">Tel. de Residencia
                        <br />
                        <asp:TextBox ID="txtTelefonoE" runat="server" CssClass="textbox" MaxLength="128" Width="135px" TabIndex="14" />
                        <asp:FilteredTextBoxExtender ID="txtTelefonoE_FilteredTextBoxExtender"
                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTelefonoE">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 60%" colspan="2">
                        <strong>Información de Correspondencia</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left">
                        <asp:CheckBox ID="chInfoResidencia" runat="server" TabIndex="15" Text="Utilizar los datos de la información de residencia"
                            onclick="InfoResi_Corr()"
                            TextAlign="Left" />
                    </td>
                </tr>
                <tr>
                    <%--<td style="text-align: left; width: 170px; vertical-align: top;">Tipo de Ubicación *<br />
                        <asp:DropDownList ID="ddlTipoUbicCorr" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="16" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Rural"></asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                    <td colspan="4" style="text-align: left;">Dirección de Correspondencia * <br />
                        <label id="msgTd02" style="color:#FF3333;box-shadow: 0 0 1px 0px #FF3333;">Por favor seleccione un tipo de residencia</label>		
                        <uc4:Direccion ID="txtDirCorrespondencia" runat="server" Width="90%" CssClass="textbox required"
                            TabIndex="17" required="required"></uc4:Direccion>
                        <%--<asp:TextBox ID="txtDirCorrespondencia" runat="server" Width="90%" CssClass="textbox required"
                            TabIndex="17" required="required"></asp:TextBox>--%>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Ciudad de Correspondencia	*
			            <asp:DropDownList ID="ddlCiuCorrespondencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="buscarBarrioCorrespondencia" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="90%" TabIndex="18" onkeypress="KeyBackspace();" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Barrio de Correspondencia	*
						<asp:DropDownList ID="ddlBarrioCorrespondencia" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="90%" TabIndex="19" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Tel. de Correspondencia<br />
                        <asp:TextBox ID="txtTelCorrespondencia" runat="server" CssClass="textbox" MaxLength="20"
                            Width="135px" TabIndex="20" />
                        <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                            TargetControlID="txtTelCorrespondencia" ValidChars="-()" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="1">E-mail<br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="120" Width="300px" TabIndex="21">
                        </asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTxtEmail" runat="server"
                            ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="E-Mail no valido!"
                            ForeColor="Red" Style="font-size: xx-small" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td style="text-align: left;">Ubicación/Zona *
                        <br />
                        <asp:DropDownList ID="ddlZona" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="11" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                        <strong>Datos Generales</strong>
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td colspan="2" style="text-align: left;"><strong>Datos Generales:</strong> </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Celular </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox required" MaxLength="12" Width="170px"
                            TabIndex="22" required="required" />
                        <asp:FilteredTextBoxExtender ID="fte55" runat="server" Enabled="True"
                            FilterType="Numbers, Custom" TargetControlID="txtCelular" ValidChars="()-" />
                    </td>
                    <td style="text-align: left;">Actividad CIIU *</td>
                    <td colspan="3" style="text-align: left;">
                        <asp:TextBox ID="txtActividadCIIU" CssClass="textbox required" runat="server" Width="170px" TabIndex="23" required="required"></asp:TextBox>
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
                                        <asp:Button ID="imgBuscar" CssClass="Buscarbutton" runat="server" Height="25px" Width="25px" Text="" OnClick="imgBuscar_Click" CausesValidation="false" UseSubmitBehavior="false" />
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
                    </td>
                    <td style="text-align: left;">Pais de Nacimiento *</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlPais" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="24" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Fec.Nacimiento *</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtFechanacimiento" class="required" type="date" runat="server" name="user_date" onblur="edad(this.value)" TabIndex="25" required="required" />
                    </td>
                    <td style="text-align: left;">Edad del Cliente *</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtEdadCliente" runat="server" CssClass="textbox required" Enabled="False"
                            Width="40px" required="required"></asp:TextBox>
                        Años</td>
                    <td style="text-align: left;">Ciudad Nacimiento *</td>
                    <td style="text-align: left;">

                        <asp:DropDownList ID="ddlLugarNacimiento" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="26" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Sexo *<br style="font-size: x-small" />
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlsexo" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="27" required="required" onChange="Mujer_cabeza(this.value)">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            <asp:ListItem Value="F" Text="FEMENINO"></asp:ListItem>
                            <asp:ListItem Value="M" Text="MASCULINO"></asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td style="text-align: left;">Nivel Educativo *<br style="font-size: x-small" />
                    </td>
                    <td colspan="3" style="text-align: left;">
                        <asp:DropDownList ID="ddlNivelEscolaridad" runat="server" TabIndex="28" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="text-align: left;">Profesión *</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtProfesion" runat="server" CssClass="textbox required" TabIndex="29" MaxLength="100"
                            Style="text-transform: uppercase" Visible="true" Width="170px" required="required"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="fte56" runat="server" Enabled="True" TargetControlID="txtProfesion" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Estado Civil *<br />
                    </td>
                    <td style="text-align: left;">

                        <asp:DropDownList ID="ddlEstadoCivil" runat="server" TabIndex="30" AppendDataBoundItems="True" onChange="EstadoCivil(this.value)"
                            CssClass="textbox required" Width="170px" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="text-align: left;">Personas a Cargo </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtPersonasCargo" runat="server" CssClass="textbox" TabIndex="31"
                            MaxLength="100" Visible="true" Width="40px"></asp:TextBox><asp:FilteredTextBoxExtender
                                ID="ftb13" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtPersonasCargo"
                                ValidChars="" />
                    </td>
                    <td style="text-align: left;">Estrato *</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox required" MaxLength="100" TabIndex="32"
                            Visible="true" Width="40px" required="required"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte10"
                                runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtEstrato"
                                ValidChars="" />
                    </td>
                    <td style="text-align: left">Ocupación *</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlOcupacion" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="33" onchange="Alertando(this.value);" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                            <asp:ListItem Value="1">Empleado</asp:ListItem>
                            <asp:ListItem Value="2">Independiente</asp:ListItem>
                            <asp:ListItem Value="3">Pensionado</asp:ListItem>
                            <asp:ListItem Value="4">Estudiante</asp:ListItem>
                            <asp:ListItem Value="5">Hogar</asp:ListItem>
                            <asp:ListItem Value="6">Cesante</asp:ListItem>
                            <asp:ListItem Value="7">Desempleado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td style="text-align: left;">Tipo Vivienda *</td>
                    <td style="text-align: left;">
                        <input type="radio" id="ViviendaP" name="vivienda" value="P" onclick="validar_vivienda(this.value)" runat="server" />Propia
                        <input type="radio" id="ViviendaA" name="vivienda" value="A" onclick="validar_vivienda(this.value)" runat="server" />Arrendada
                        <input type="radio" id="ViviendaF" name="vivienda" value="F" onclick="validar_vivienda(this.value)" runat="server" />Familiar 
                    </td>
                    <td style="text-align: left;"></td>
                    <td style="text-align: left;"></td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:Label ID="lblPropietario" runat="server" Text="Nombre Propietario *" />
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox required" MaxLength="128"
                            Style="text-align: left; text-transform: uppercase" Width="300px" TabIndex="35" /></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblTelefPropietario" runat="server" Text="Teléfono Propietario *" />
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox required" MaxLength="128"
                            Style="margin-left: 0px" Width="130px" TabIndex="36" /><asp:FilteredTextBoxExtender
                                ID="txtTelefonoarrendador_FilteredTextBoxExtender" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txtTelefonoarrendador">
                            </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Antigüedad en la Vivienda (Meses) * </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtAntiguedadlugar" runat="server" CssClass="textbox required" MaxLength="8"
                            Style="text-align: left" Width="100px" TabIndex="37" required="required" />
                        <asp:FilteredTextBoxExtender
                                ID="txtAntiguedadlugar_FilteredTextBoxExtender" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txtAntiguedadlugar">
                            </asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblValorArriendo" runat="server" Text="Gastos Vivienda *" /></td>
                    <td style="text-align: left;">
                        <input type="text" id="txtValorArriendo" class="required" style="width: 130px;" onkeypress="return isNumber(event)" runat="server" tabindex="38" required="required" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblEstatus" runat="server" Font-Bold="true" Style="text-align: left">Estatus: </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">¿Tiene Parentesco Con Empleados de la Entidad? *&#160; 
                        <br />
                        <asp:DropDownList ID="ddlparentesco" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="39" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="2">NO </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">¿Es familiar de una PEPS? *&#160;
                        <br />
                        <asp:DropDownList ID="ddlFamiliarPEPS" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="40" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="2">NO </asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:Label ID="lblMujerCabeFami" runat="server" Text="¿Es mujer cabeza de familia?" TabIndex="43" />
                        <asp:CheckBox ID="chkMujerCabeFami" runat="server" TextAlign="Left" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">¿Es familiar de un miembro de administración? *&#160;
                    <br />
                        <asp:DropDownList ID="ddlFamiliarAdmin" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="41" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="2">NO </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" style="text-align: left;">¿Es familiar de un miembro de control? *&#160; 
                        <br />
                        <asp:DropDownList ID="ddlFamiliarControl" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="42" required="required">
                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="2">NO </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divguardar" style="text-align: right; display: none;">
            <asp:Button runat="server" ID="GuardarPersonal" Text="Guardar Inf.Personal  >" OnClick="btnGuardarPersonal_click" OnClientClick="FamiliarStatus();" UseSubmitBehavior="false" />
        </div>
        <%--     </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtIdentificacionE" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </form>
</body>    
<script type="text/javascript">
    $(document).ready(function () {
        mensaje();
        var dateDefault = today = new Date();
        var dateAc = dateDefault.getFullYear() + "-" + ((dateDefault.getMonth() < 9) ? "0" + (dateDefault.getMonth() + 1) : dateDefault.getMonth() + 1) + "-" + ((dateDefault.getDate() < 9) ? "0" + (dateDefault.getDate()) : dateDefault.getDate());
        document.getElementById("txtfechaAct").value = dateAc;        
    });
    function mensaje() {
        var contenidoRadio01 = getRadio("txtDireccionE$rbtnDetalleZonaGeo");
        if (contenidoRadio01 == "R" || contenidoRadio01 == "U") {
            $("#msgTd").hide();
        }
        var contenidoRadio02 = getRadio("txtDirCorrespondencia$rbtnDetalleZonaGeo");
        if (contenidoRadio02 == "R" || contenidoRadio02 == "U") {
            $("#msgTd02").hide();
        }
    }
    function getRadio(name) {
        var radioObjs = document.getElementsByName(name);
        var radioLength = radioObjs.length;
        var result = null;
        for (var i = 0; i < radioLength; i++) {
            if (radioObjs[i].checked) {
                result = radioObjs[i].value;
            }
        }
        return result;
    }

    function setRadio(name, newValue) {
        var radioObjs = document.getElementsByName(name);
        var radioLength = radioObjs.length;
        for (var i = 0; i < radioLength; i++) {
            radioObjs[i].checked = false;
            if (radioObjs[i].value == newValue) {
                radioObjs[i].checked = true;
            }
        }
    }

    function InfoResi_Corr() {
        var ChkInfoRes = document.getElementById('chInfoResidencia');
        if (ChkInfoRes.checked == true) {
            // VALIDACION
            var contenidoRadio = getRadio("txtDireccionE$rbtnDetalleZonaGeo");
            if (contenidoRadio == null) {
                alert("No selecciono el tipo de ubicación de su dirección de residencia.");
                ChkInfoRes.checked = false;
                return;
            }
            if (contenidoRadio == "R")
                document.getElementById('txtDirCorrespondencia_rbtnDetalleZonaGeo_0').checked = true;
            else
                document.getElementById('txtDirCorrespondencia_rbtnDetalleZonaGeo_1').checked = true;
            mensaje();
            document.getElementById('txtDirCorrespondencia_txtDireccion').value = document.getElementById('txtDireccionE_txtDireccion').value;
            document.getElementById('txtDirCorrespondencia_hfControlText').value = document.getElementById('txtDireccionE_txtDireccion').value;
            document.getElementById('ddlCiuCorrespondencia').value = document.getElementById('ddlCiudadResidencia').value;
            document.getElementById('ddlBarrioCorrespondencia').value = document.getElementById('ddlBarrioResid').value;
            document.getElementById('txtTelCorrespondencia').value = document.getElementById('txtTelefonoE').value;

        } else {
            setRadio("txtDirCorrespondencia$rbtnDetalleZonaGeo", null);
            document.getElementById('txtDirCorrespondencia_txtDireccion').value = "";
            document.getElementById('txtDirCorrespondencia_hfControlText').value = "";
            document.getElementById('ddlCiuCorrespondencia').value = "";
            document.getElementById('ddlBarrioCorrespondencia').value = "";
            document.getElementById('txtTelCorrespondencia').value = "";
        }
    }

    function edad(Fecha) {
        fecha = new Date(Fecha)
        hoy = new Date()
        ed = parseInt((hoy - fecha) / 365 / 24 / 60 / 60 / 1000)
        edmayoredad = document.getElementById('txtmayoredad').value;
        edmayoredad = parseInt(edmayoredad);
        if (ed < edmayoredad) {
            alert("La persona debe ser mayor de edad");
            document.getElementById('txtEdadCliente').value = "";
            document.getElementById('txtFechanacimiento').value = "";
        } else {
            document.getElementById('txtEdadCliente').value = ed;
        }
    }    

    function Mujer_cabeza(valor) {
        if (valor == "F") {
            document.getElementById('chkMujerCabeFami').style.display = "inline";
            document.getElementById('lblMujerCabeFami').style.display = "inline";
        }
        else {
            document.getElementById('chkMujerCabeFami').style.display = "none";
            document.getElementById('lblMujerCabeFami').style.display = "none";
        }
    }

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    function MostrarCIIUPrincipal(pDescripcion) {
        document.getElementById('<%= txtActividadCIIU.ClientID %>').value = pDescripcion;
    }

    function Alertando(valor) {
        if (valor == "1" || valor == "2" || valor == "3") {
            window.parent.$('#lilaboral').removeClass("clsInactivos");
        } else {
            window.parent.$('#lilaboral').removeClass("clsInactivos").addClass("clsInactivos");
        }

        if (valor == "3") {
            var elements = window.parent.document.formularios.iframeLaboral.contentDocument.getElementsByClassName('datoEmpleado');
            for (var i = 0; i < elements.length; i++) {
                elements[i].style.display = 'none';
            }
        } else {
            var elements = window.parent.document.formularios.iframeLaboral.contentDocument.getElementsByClassName('datoEmpleado');
            for (var i = 0; i < elements.length; i++) {
                elements[i].style.display = 'table-cell';
            }            
        }
    }

    function FamiliarStatus() {
        if (document.getElementById('ddlparentesco').value == "1" || document.getElementById('ddlFamiliarPEPS').value == "1" || document.getElementById('ddlFamiliarAdmin').value == "1" || document.getElementById('ddlFamiliarControl').value == "1") {
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('lblestatus').textContent = "Debe ingresar el parentesco para familiares, con algun estatus";
        } else {
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('lblestatus').textContent = " ";
        }
    }

    function EstadoCivil(valor) {
        if (valor == 1 || valor == 3) {
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').style.display = "block";
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').val = "A"
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').CurrentSite = "A";
        } else {
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').style.display = "none";
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').CurrentSite = "";
            window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').value = "";
        }
    }

</script>
</html>
