<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Afiliacion.aspx.cs" Inherits="Afiliacion" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" lang="sp">
    <title>Solicitud de Afiliación</title>
    <link href="~/Css/bootstrap2.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PCLBryan.js")%>"></script>




    <script type="text/javascript">

        function radioSex(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= cblSexo.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

        function TipPersona(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= ChkPersona.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= cblEstrato.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

        function radioMeCbz(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBoxCabeza = document.getElementById('<%= cblCabezaFamilia.ClientID %>');
            var chksCbz = chkBoxCabeza.getElementsByTagName('INPUT');
            for (i = 0; i < chksCbz.length; i++) {
                if (chksCbz[i] != checker)
                    chksCbz[i].checked = false;
            }
        }

        function radioMeDocu(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

        function radioMeEstadoCi(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= cblEstadoCivil.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

        function radioMeNivEsc(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= cbNivelAcademico.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

        function radioMeDocuCony(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= cblDocumentoCony.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

    </script>
    <style>
        * {
            text-align: left;
        }

        small {
            color: Red;
        }

        label {
            font-weight: 500;
        }

        #container-main {
            margin: 0px auto;
            width: 100%;
        }

        .accordion-container {
            width: 100%;
            margin: 0 0 1px;
            text-align: center;
            clear: both;
        }

        .accordion-titulo {
            position: relative;
            display: block;
            padding: 4px;
            font-size: 16px;
            font-weight: 300;
            background: #0099FF;
            color: #fff;
            text-decoration: none;
        }

            .accordion-titulo.open {
                background: #005bff;
                color: #fff;
            }

            .accordion-titulo:hover {
                background: #005bff;
            }

        .accordion-content {
            display: none;
            padding: 20px;
            width: 100%;
            overflow: auto;
        }

        @media (max-width: 767px) {
            .accordion-content {
                padding: 10px 0;
            }
        }

        .mensaje{
            background-color: rgba(184, 233, 148,1.0);
            padding: 15px;
            margin: 0 50px;
            font-weight: 400;
        }

        .centrar{
            text-align: center;
            margin-left: auto;
            margin-right: auto;
            align-content: center;
            align-items: center;
            align-self: center;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <form id="formulario" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Panel ID="panelData" runat="server">
                <div class="form-group">
                    <div class="col-md-12" style="height: 10px; background-color: #0099FF;"></div>
                    <div class="col-sm-12">
                        <div class="col-lg-12 col-md-12 col-xs-12" style="padding-top: 8px; margin: 7px auto;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 70%; text-align: left; padding-left: 15px">
                                        <asp:Image ID="imgEmpresa" runat="server" ImageUrl="~/Imagenes/LogoEmpresa.jpg" Width="90px" />
                                        &nbsp;<asp:Label ID="Lbltitulo" runat="server" CssClass="text-primary" Style="font-size: 30px"
                                            Text="Solicitud de Afiliación"></asp:Label>
                                    </td>
                                    <td style="width: 15%; text-align: right">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" Width="160px" ToolTip="Save"
                                            Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnGuardar_Click">
                                        Guardar Información
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%; text-align: right">
                                        <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-primary" Width="120px" ToolTip="Cancel"
                                            Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnCancelar_Click">
                                        Cancelar
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <hr style="width: 100%; margin-top: 4px" />
                    <div class="col-sm-12" style="margin-bottom: 20px;">
                        <div class="col-lg-12 col-md-12 col-xs-12">
                            <div class="row mensaje">
                                <div class="col-sm-12 col-md-9">
                                    “Después de diligenciada la afiliación por favor imprímala, fírmela, coloque la huella del índice derecho, y envíela en original a la comercial asignada junto a la copia de la cédula de ciudadanía, y el formato de designación de beneficiarios MetLife”
                                </div>
                                <div class="col-sm-12 col-md-3 centrar">                                    
                                    <a id="download" class="btn btn-default navbar-btn" style="background-color: white;" href="./../../files/formato_Metlife.pdf" download="formato_Metlife.pdf">Descargar formato MetLife</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr style="width: 100%; margin-top: 4px" />
                    <div class="col-sm-12" style="margin-bottom: 20px;">
                        <div class="col-lg-12 col-md-12 col-xs-12">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 13px" />
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <%--<input id="datepicker" class="datepicker" type="text"(>--%>
                                    <table>
                                        <tr>
                                            <td style="max-width: 100px">
                                                <label>Día</label>
                                                <asp:TextBox ID="txtDiaEncabezado" Style="max-width: 100%;" runat="server" CssClass="validate text-center form-control" Enabled="false"></asp:TextBox></td>
                                            <td style="max-width: 200px">
                                                <label>Mes</label>
                                                <asp:DropDownList ID="ddlMesEncabezado" Style="max-width: 100%;" runat="server" CssClass="browser-default form-control" Enabled="false">
                                                </asp:DropDownList></td>
                                            <td style="max-width: 100px">
                                                <label>Año</label>
                                                <asp:TextBox ID="txtAnioEncabezado" Style="max-width: 100%;" runat="server" CssClass="validate text-center form-control" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="col-md-12">
                                        <label>Tipo de persona a afiliar</label>
                                        <asp:CheckBoxList ID="ChkPersona" runat="server" RepeatDirection="Horizontal"
                                            Style="margin-top: -5px; width: 100%;" AutoPostBack="true" type="checkbox" class="form-check-input" OnSelectedIndexChanged="ChkPersona_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1" style="margin-right: 3%;">&#160&#160&#160Persona Natural</asp:ListItem>
                                            <asp:ListItem Value="0">&#160&#160&#160Persona Juridica</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <br />
                                <div class="col-xs-12">
                                    <label class="text-danger" style="font-size:small">Los datos a ingresar deben corresponder a los registrados en su entidad</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div id="">
                            <div class="Natural" id="Div1" runat="server">
                                <div class="accordion-container open">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Identificación<span class="toggle-icon"></span></a>
                                        <div class="row">           
                                            <div class="col-sm-12 col-md-6">
                                                <label for="inputState">Tipo de documento *</label>
                                                <asp:DropDownList ID="ddlDocumento" runat="server" class="form-control"
                                                    ClientIDMode="static" OnSelectedIndexChanged="ddlDocumento_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                </asp:DropDownList>
                                                <small style="color: Red;" id="LblTipoElemento" class="form-text text-muted">Seleccione un tipo de documento</small>
                                            </div>
                                            <div class="col-sm-12 col-md-6">
                                                <label>Numero de identificación *</label>
                                                <asp:TextBox ID="txtNumero" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su N° Identificación" OnTextChanged="txtNumero_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True"
                                                    FilterType="Numbers, Custom" TargetControlID="txtNumero" ValidChars="-." />
                                                <small style="color: Red;" id="LblErrorNumeroIdentificacion" class="form-text text-muted">Ingrese su numero de identificación</small>
                                            </div>
                                        </div>
                                </div>                
                            </div>
                        </div>
                    </div>                    

                    <div class="col-md-12">
                        <div id="">
                            <div class="Natural" id="Natural" runat="server">
                                <div class="accordion-container open">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Persona Natural<span class="toggle-icon"></span></a>
                                    <div class="accordion-content" style="display: block; height: auto; overflow: hidden;">
                                        <div class="container-fluid">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Primer apellido *</label>
                                                        <asp:TextBox ID="txtApellido1" runat="server" ClientIDMode="Static"
                                                            class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorApellido1" class="form-text text-muted">Ingrese un Apellido</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Segundo Apellido</label>
                                                        <asp:TextBox ID="txtApellido2" runat="server" ClientIDMode="Static"
                                                            class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Primer Nombre *</label>
                                                        <asp:TextBox ID="txtNombre1" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorNombre1" class="form-text text-muted">Ingrese un Nombre</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Segundo Nombre</label>
                                                        <asp:TextBox ID="txtNombre2" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Sexo</label>
                                                        <asp:CheckBoxList ID="cblSexo" runat="server" RepeatDirection="Horizontal" Style="width: 100%;">
                                                            <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160&#160Hombre</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160&#160&#160Mujer</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>                                                    
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Fecha de expedición*</label>
                                                        <asp:TextBox ID="txtDia" runat="server" class="datepicker form-control" type="text" placeholder="Ingrese aquí su nombre" />
                                                        <small style="color: Red;" id="LblErrorFechaExpedicion" class="form-text text-muted">Indique una fecha de expedición valida</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Ciudad de Expedicón *</label>
                                                        <asp:DropDownList ID="ddlCiudadExpedicion" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorCiudadExpedicion" class="form-text text-muted">Seleccione su ciudad de expedición</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Nacionalidad *</label>
                                                        <asp:TextBox ID="TxtNacionalidad" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorNacionalidad" class="form-text text-muted">Indique su nacionalidad</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Fecha de Nacimiento *</label>
                                                        <asp:TextBox ID="txtDianacimiento" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                                        <small style="color: Red;" id="LblErrorFechaNacimiento" class="form-text text-muted">Indique una fecha de nacimiento valida</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Ciudad de Nacimiento *</label>
                                                        <asp:DropDownList ID="ddlCiudadNacimiento" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorCiudadNacimiento" class="form-text text-muted">Seleccione su ciudad de nacimiento</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Direción de residencia *</label>
                                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorDireccionResidencia" class="form-text text-muted">Indiquie su dirección de residencia</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Departamento de residencia *</label>
                                                        <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorDepartamento" class="form-text text-muted">Departamento de residencia</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Ciudad de residencia *</label>
                                                        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorCiudadResidencia" class="form-text text-muted">Seleccione una ciudad</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Barrio</label>
                                                        <asp:DropDownList ID="ddlBarrio" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Telefono fijo de residencia</label>
                                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtTelefono" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorTelefonoReisdencia" class="form-text text-muted">Indique su telefono de residencia</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtCelular">Celular personal</label>
                                                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtCelular" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorCelularPersonal" class="form-text text-muted">Indique su numero de celular</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label runat="server">Estado Civil</label>
                                                        <asp:DropDownList ID="cblEstadoCivil" runat="server" class="form-control">
                                                            <asp:ListItem Selected="true" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="cblEstadoCivils" class="form-text text-muted">Indique su estado civil</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Cabeza de Familia</label>
                                                        <asp:CheckBoxList ID="cblCabezaFamilia" runat="server" RepeatDirection="Horizontal" Style="width: 100%;">
                                                            <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160&#160Si</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160&#160&#160No</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server">Numero de hijos</label>
                                                        <asp:TextBox ID="txthijos" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txthijos" ValidChars="-." />
                                                        <small style="color: Red;" id="txthijos2" class="form-text text-muted">Numero de hijos</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Nivel Academico</label>
                                                        <asp:DropDownList ID="cbNivelAcademico" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Estrato social</label>
                                                        <asp:DropDownList ID="cblEstrato" runat="server" class="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label>Email</label>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                                            placeholder="alguien@example.com" type="email"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="color: Red; font-weight: 200; font-size: 12px;" runat="server" ControlToValidate="txtEmail"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="email no valido"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-container">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Información laboral<span class="toggle-icon"></span></a>
                                    <div class="accordion-content">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Estado Laboral</label>
                                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="" Selected="True">Seleccione un item</asp:ListItem>
                                                        <asp:ListItem Value="0">Empleado</asp:ListItem>
                                                        <asp:ListItem Value="1">Contratista</asp:ListItem>
                                                        <asp:ListItem Value="2">Independiente</asp:ListItem>
                                                        <asp:ListItem Value="3">Pensionado</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtEmpresa" runat="server">Empresa en la que labora</label>
                                                    <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label runat="server">Cargo que se le otorga</label>
                                                    <asp:DropDownList ID="ddlCargo" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <%--	<asp:TextBox ID="txtCargo" runat="server" CssClass="validate"></asp:TextBox>--%>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtNit" runat="server">Nit de la empresa en la que labora</label>
                                                    <asp:TextBox ID="txtNit" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte9" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="txtNit" ValidChars="-." />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtDireccionLaboral" runat="server">Dirección empresa en la que laboral</label>
                                                    <asp:TextBox ID="txtDireccionLaboral" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Departamento en el que Labora</label>
                                                    <asp:DropDownList ID="ddlDepartamentoLaboral" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Ciudad empresa en la que laboral</label>
                                                    <asp:DropDownList ID="ddlCiudadLaboral" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label>Telefono laboral</label>
                                                    <asp:TextBox ID="txtTelefonolaboral" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte10" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelefonolaboral" ValidChars="-()" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label runat="server" for="txtProfesion">Profesión</label>
                                                    <asp:TextBox ID="txtProfesion" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Administra recursos públicos</label>
                                                    <asp:CheckBoxList ID="ChkRecursosPublicos" runat="server" RepeatDirection="Horizontal"
                                                        Style="width: 100%;">
                                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160&#160Si</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160&#160&#160No</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>PEPS (Persona Expuesta Públicamente)</label>
                                                    <asp:CheckBoxList ID="ChkPeps" runat="server" RepeatDirection="Horizontal"
                                                        Style="width: 100%;">
                                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160&#160Si</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160&#160&#160No</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label runat="server" for="TxtDescripcionEconomica">Descripción actividad económica</label>
                                                    <asp:TextBox ID="TxtDescripcionEconomica" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label runat="server" for="TxtCiiu">CIIU</label>
                                                    <asp:TextBox ID="TxtCiiu" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Frecuencia de ingresos</label>
                                                    <asp:DropDownList ID="ddlPeriodicidadPago" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtPersonaCargo">Personas a cargo</label>
                                                    <asp:TextBox ID="txtPersonaCargo" runat="server" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtDiaInicio" runat="server">Fecha de inicio laboral</label>
                                                    <asp:TextBox ID="txtDiaInicio" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upCalculos" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="input-field col-xs-6 col-md-3 form-group">
                                                            <label class="active" for="txtIngsalariomensual">Ingreso salario mensual</label>
                                                            <uc1:decimales ID="txtIngsalariomensual" class="form-control" runat="server" AutoPostBack_="true" Width_="100%" />
                                                        </div>
                                                        <div class="input-field col-xs-6 col-md-3 form-group">
                                                            <label class="active" for="txtOtrosing">Otros ingresos</label>
                                                            <uc1:decimales ID="txtOtrosing" class="form-control" runat="server" AutoPostBack_="true" Width_="100%" />
                                                        </div>
                                                        <div class="input-field col-xs-6 col-md-3 form-group">
                                                            <label class="active" for="txtDeducciones">Deducciones</label>
                                                            <uc1:decimales ID="txtDeducciones" CssClass="form-control" runat="server" AutoPostBack_="true" Width_="100%" />
                                                        </div>
                                                        <div class="input-field col-xs-6 col-md-3 form-group">
                                                            <label for="txtTotalIng">Total Ingresos</label>
                                                            <asp:TextBox ID="txtTotalIng" runat="server" CssClass="form-control" Enabled="false" placeholder="$" Style="text-align: right"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtTotalActivos">Total Activos</label>
                                                    <asp:TextBox ID="TxtTotalActivos" runat="server" CssClass="form-control" placeholder="$" Style="text-align: right"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="TxtTotalActivos"
                                                        ValidChars="-()" />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtTotalPasivos">Total Pasivos</label>
                                                    <asp:TextBox ID="TxtTotalPasivos" runat="server" CssClass="form-control" placeholder="$" Style="text-align: right"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="TxtTotalPasivos"
                                                        ValidChars="-()" />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtTotalPatrimonio">Total Patrimonio</label>
                                                    <asp:TextBox ID="TxtTotalPatrimonio" runat="server" CssClass="form-control" placeholder="$" Style="text-align: right"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="TxtTotalPatrimonio"
                                                        ValidChars="-()" />
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Tipo de contrato</label>
                                                    <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="form-control">
                                                        <%--<asp:ListItem>Indefinido</asp:ListItem>
											<asp:ListItem>Termino</asp:ListItem>
											<asp:ListItem>Obra Labor</asp:ListItem>
											<asp:ListItem>Servicios</asp:ListItem>
											<asp:ListItem>Pensionado</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtEmpresaAnterior">Empresa anterior</label>
                                                    <asp:TextBox ID="txtEmpresaAnterior" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtCargoanterior">Cargo que se le otorgaba</label>
                                                    <asp:TextBox ID="txtCargoanterior" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtTelefonoanterior">Telefono de la empresa anterior</label>
                                                    <asp:TextBox ID="txtTelefonoanterior" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte12" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="txtTelefonoanterior"
                                                        ValidChars="-()" />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtDialiquidacion">Fecha retiro de la empresa</label>
                                                    <asp:TextBox ID="txtDialiquidacion" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="Txtcontacto">Contacto empresa anterior</label>
                                                    <asp:TextBox ID="Txtcontacto" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtTelcontacto">Telefono del contacto</label>
                                                    <asp:TextBox ID="txtTelcontacto" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte11" runat="server" Enabled="True"
                                                        FilterType="Numbers, Custom" TargetControlID="txtTelcontacto"
                                                        ValidChars="-()" />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtCargocontacto">Cargo del contacto</label>
                                                    <asp:TextBox ID="txtCargocontacto" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtEmailcontacto" data-error="email no valido" data-success="right">Correo del contacto</label>
                                                    <asp:TextBox ID="txtEmailcontacto" runat="server" CssClass="form-control" type="email" placeholder="alguien@example.com"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="color: Red; font-weight: 200; font-size: 12px;" runat="server" ControlToValidate="txtEmailcontacto"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="email no valido"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-container">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Información del conyugue o familiar en primer grado de consanguinidad<span class="toggle-icon"></span></a>
                                    <div class="accordion-content">
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-xs-12">
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtApellido1conyugue">Primer Apellido</label>
                                                    <asp:TextBox ID="txtApellido1conyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtApellido2conyugue">Segundo Apellido</label>
                                                    <asp:TextBox ID="txtApellido2conyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtNombre1conyugue">Primer Nombre</label>
                                                    <asp:TextBox ID="txtNombre1conyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtNombre2conyugue">Segundo Nombre</label>
                                                    <asp:TextBox ID="txtNombre2conyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Parentesco</label>
                                                    <asp:DropDownList ID="ddlParentesco" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Tipo de Documento</label>
                                                    <asp:DropDownList ID="cblDocumentoCony" runat="server" class="form-control" ClientIDMode="static">
                                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtNumerodocumentoconyugue">Numero de identificación</label>
                                                    <asp:TextBox ID="txtNumerodocumentoconyugue" runat="server"
                                                        CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte13" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtNumerodocumentoconyugue" ValidChars="-." />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtDireccionconyugue">Dirección del familiar</label>
                                                    <asp:TextBox ID="txtDireccionconyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtTelefonoconyugue">Telefono del familiar</label>
                                                    <asp:TextBox ID="txtTelefonoconyugue" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte14" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtTelefonoconyugue" ValidChars="-()" />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label>Email del familiar</label>
                                                    <asp:TextBox ID="txtEmailconyugue" runat="server" CssClass="form-control" type="email" placeholder="alguien@example.com"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="color: Red; font-weight: 200; font-size: 12px;" runat="server" ControlToValidate="txtEmailconyugue"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="email no valido"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label>Estado laboral del familiar</label>
                                                    <asp:DropDownList ID="ddlEstadoconyugue" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Empleado</asp:ListItem>
                                                        <asp:ListItem Value="1">Contratista</asp:ListItem>
                                                        <asp:ListItem Value="2">Independiente</asp:ListItem>
                                                        <asp:ListItem Value="3">Pensionado</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label for="txtEmpresaconyugue">Empresa donde labora</label>
                                                    <asp:TextBox ID="txtEmpresaconyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label for="txtIngconyugue">Ingresos del familiar</label>
                                                    <uc1:decimales ID="txtIngconyugue" runat="server" Width="100%" clas="form-control" />
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label for="txtCargolaboralconyugue">Cargo del familiar</label>
                                                    <asp:TextBox ID="txtCargolaboralconyugue" runat="server" Class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label for="txtDireccionlabConyugue">Dirección donde labora</label>
                                                    <asp:TextBox ID="txtDireccionlabConyugue" runat="server" Class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 form-group">
                                                    <label for="txtTelefonolabconyugue">Telefono donde labora</label>
                                                    <asp:TextBox ID="txtTelefonolabconyugue" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte15" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelefonolabconyugue"
                                                        ValidChars="-()" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-container">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Referencia personal o comercial<span class="toggle-icon"></span></a>
                                    <div class="accordion-content">
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-xs-12">
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtApellido1refencia">Primer Apellido</label>
                                                    <asp:TextBox ID="txtApellido1refencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtApellido2refencia">Segundo Apellido</label>
                                                    <asp:TextBox ID="txtApellido2refencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtNombre1referencia">Primer Nombre</label>
                                                    <asp:TextBox ID="txtNombre1referencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtNombre2referencia">Segundo Nombre</label>
                                                    <asp:TextBox ID="txtNombre2referencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtRelacionreferencia">Relación</label>
                                                    <asp:TextBox ID="txtRelacionreferencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtDireccionresidencia">Dirección residencia</label>
                                                    <asp:TextBox ID="txtDireccionresidencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtCelreferencia">Celular referencia</label>
                                                    <asp:TextBox ID="txtCelreferencia" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte16" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtCelreferencia" ValidChars="-()" />
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtTelfijoRef">Telefono referencia</label>
                                                    <asp:TextBox ID="txtTelfijoRef" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="ftr17" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtTelfijoRef" ValidChars="-()" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtEmailreferencia">Email referencia</label>
                                                    <asp:TextBox ID="txtEmailreferencia" runat="server" CssClass="form-control" placeholder="alguien@example.com"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="color: Red; font-weight: 200; font-size: 12px;" runat="server" ControlToValidate="txtEmailreferencia"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="email no valido"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3">
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3">
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-container">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Beneficiarios<span class="toggle-icon"></span></a>
                                    <div class="accordion-content">
                                        <div class="col-sm-12">
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <label>Primer Beneficiario</label>
                                                <div class="row">
                                                    <div class="input-field col-xs-12 col-md-6 form-group">
                                                        <label for="txtNombre1benef">Nombre(s)</label>
                                                        <asp:TextBox ID="txtNombre1benef" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="input-field col-xs-12 col-md-6 form-group">
                                                        <label for="txtApellido1benef">Apellidos(s)</label>
                                                        <asp:TextBox ID="txtApellido1benef" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-4 form-group">
                                                        <label for="txtDocumentobenef1">Numero de identificación</label>
                                                        <asp:TextBox ID="txtDocumentobenef1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="fte18" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                            TargetControlID="txtDocumentobenef1" ValidChars="-." />
                                                    </div>
                                                    <div class="col-xs-6 col-md-4 form-group">
                                                        <label>Parentesco</label>
                                                        <asp:DropDownList ID="ddlParentescoBenef1" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-4 form-group">
                                                        <label for="TxtPorcBenef1">Porcentaje</label>
                                                        <asp:TextBox ID="TxtPorcBenef1" runat="server" CssClass="form-control" placeholder="%"
                                                            Style="text-align: right" MaxLength="8"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="fte19" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                            TargetControlID="TxtPorcBenef1" ValidChars=",." />
                                                    </div>
                                                </div>
                                                <hr style="width: 100%; color: #0099CC;" />
                                                <label>Segundo Beneficiario</label>
                                                <div class="row">
                                                    <div class="input-field col-xs-12 col-md-6 form-group">
                                                        <label for="txtNombre2benef">Nombre(s)</label>
                                                        <asp:TextBox ID="txtNombre2benef" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="input-field col-xs-12 col-md-6 form-group">
                                                        <label for="txtApellido2benef">Apellidos(s)</label>
                                                        <asp:TextBox ID="txtApellido2benef" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-4 form-group">
                                                        <label for="txtDocumentobenef2">Numero de identificación</label>
                                                        <asp:TextBox ID="txtDocumentobenef2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="fte20" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                            TargetControlID="txtDocumentobenef2" ValidChars="-." />
                                                    </div>
                                                    <div class="col-xs-6 col-md-4 form-group">
                                                        <label>Parentesco</label>
                                                        <asp:DropDownList ID="ddlParentescoBenef2" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-4 form-group">
                                                        <label for="TxtPorcBenef2">Porcentaje</label>
                                                        <asp:TextBox ID="TxtPorcBenef2" runat="server" CssClass="form-control" placeholder="%"
                                                            Style="text-align: right"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="fte21" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                            TargetControlID="TxtPorcBenef2" ValidChars=",." />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlTemas" Visible="true">
                                <div class="accordion-container">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Temas de Interes<span class="toggle-icon"></span></a>
                                    <div class="accordion-content" style="height: auto; overflow: hidden;">
                                    <div class="container-fluid">
                                        <div class="col-md-12">
                                            <div class="row">                                                
                                                <div class="input-field col-xs-12 col-md-12 form-group">
                                                    <label for="txtTelcontacto">Temas de interés</label>
                                                    <asp:CheckBoxList ID="cbTemas" runat="server" CssClass="form-control" RepeatColumns="3"></asp:CheckBoxList>                                        
                                                </div>                                                
                                            </div>                                            
                                        </div>
                                         <div class="col-md-12">
                                            <div class="row">                                                
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="txtTelcontacto">Otro</label>
                                                    <asp:TextBox ID="txtOtroTema" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>                                                
                                            </div>                                            
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                </asp:Panel>
                            </div>                                                           
                            

                            <div class="Juridica" id="Juridica" runat="server">
                                <div class="accordion-container">
                                    <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Persona Juridica<span class="toggle-icon"></span></a>
                                    <div class="accordion-content" style="display: block; height: auto; overflow: hidden;">
                                        <div class="container-fluid">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label for="txtRazonSoial">Razón Social *</label>
                                                        <asp:TextBox ID="txtRazonSoial" runat="server" ClientIDMode="Static"
                                                            CssClass="form-control" data-error="wrong" EnableTheming="True"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorRazonSocial" class="form-text text-muted">Ingrese una Razón Social</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label for="txtNitJuridica">Nit *</label>
                                                        <asp:TextBox ID="txtNitJuridica" runat="server" ClientIDMode="Static"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorNit" class="form-text text-muted">Ingrese el Nit</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label for="txtCamaraComercio">Camara de comercio *</label>
                                                        <asp:TextBox ID="txtCamaraComercio" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorCamaraComercio" class="form-text text-muted">Ingrese la camara de comercio</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label for="txtPaisConstitución">País de constitución persona juridica *</label>
                                                        <asp:TextBox ID="txtPaisConstitución" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorNacionalidadRepresentante" class="form-text text-muted">Ingrese la nacionalidad</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label for="txtDirecciónDomicilio">Dirección de domicilio *</label>
                                                        <asp:TextBox ID="txtDirecciónDomicilio" runat="server" ClientIDMode="Static"
                                                            CssClass="form-control" data-error="wrong" EnableTheming="True"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorDirecciinDomicilio" class="form-text text-muted">Ingrese una dirección</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Departamento de residencia *</label>
                                                        <asp:DropDownList ID="DdlDepartamentoResidecia" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorDepartamentoResidencia" class="form-text text-muted">Seleccione un departamento</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Ciudad de residencia *</label>
                                                        <asp:DropDownList ID="DdlCiudadResidencia" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorCiudadResidenciaJuridica" class="form-text text-muted">Seleccione una ciudad</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Barrio *</label>
                                                        <asp:DropDownList ID="DdlBarrioResidencia" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorBarrioResidenciaJuridica" class="form-text text-muted">Seleccione un barrio</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtTelefono">Telefono fijo de residencia *</label>
                                                        <asp:TextBox ID="TxtTelefonoResidencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="TxtTelefonoResidencia" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorTelefonoJuridica" class="form-text text-muted">Indique su número teléfono</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label for="txtRepresentanteLegal">Nombre representante legal *</label>
                                                        <asp:TextBox ID="txtRepresentanteLegal" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorNombreRepresentante" class="form-text text-muted">Ingrese el nombre del representante</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtTelefono">Identificación representante *</label>
                                                        <asp:TextBox ID="TxtDocumentoJuridica" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtDocumentoJuridica_TextChanged"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="TxtDocumentoJuridica" ValidChars="-." />
                                                        <asp:Label ID="lblCod_repre" runat="server" Visible="false"></asp:Label>
                                                        <small style="color: Red;" id="lblCodrepre" class="form-text text-muted">Indique su número de identificación</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Tipo de documento *</label>
                                                        <asp:DropDownList ID="DdlTipoDocumentoRepresentante" runat="server" class="form-control"
                                                            ClientIDMode="static">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorTipoDocumentoRepresentante" class="form-text text-muted">Seleccione un tipo de documento</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtDomicilioJuridica">Dirección domicilio representante *</label>
                                                        <asp:TextBox ID="TxtDireccionJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorDireccionJuridica" class="form-text text-muted">Indique su dirección representante</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Departamento representante *</label>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorDepartamentojuridica" class="form-text text-muted">Seleccione un departamento</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Ciudad residencia representante *</label>
                                                        <asp:DropDownList ID="DdlCiudadJuridica" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorCiudadjuridica" class="form-text text-muted">Seleccione una ciudad</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Barrio domicilio representante *</label>
                                                        <asp:DropDownList ID="DdlBarrioJuridica" runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small style="color: Red;" id="LblErrorBarrioRepresentante" class="form-text text-muted">Indique el barrio de residencia</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtTelefono">Telefono(s) de representante *</label>
                                                        <asp:TextBox ID="TxtTelefonoRepresentante" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="TxtTelefonoRepresentante" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorTelefonoRepresentante" class="form-text text-muted">Indique su número teléfono</small>
                                                    </div>
                                                    <div class="col-xs-6 col-md-3 form-group">
                                                        <label>Tipo de empresa *</label>
                                                        <asp:CheckBoxList ID="ChkTipoEmpresa" runat="server" CssClass="" RepeatDirection="Horizontal"
                                                            Style="margin-top: 0px; margin-left: -10px;">
                                                            <asp:ListItem Selected="True" Value="2" style="margin-left: 10px;">Privada</asp:ListItem>
                                                            <asp:ListItem Value="1" style="margin-left: 20px;">Pública</asp:ListItem>
                                                            <asp:ListItem Value="0" style="margin-left: 20px;">Mixta</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                        <small style="color: Red;" id="LblErrorTipoEmpresa" class="form-text text-muted">Indique el tipo de empresa</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="TxtActividadEconomica">Actividad económica *</label>
                                                        <asp:TextBox ID="TxtActividadEconomica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorActividadEconomica" class="form-text text-muted">Indique su actividad economica</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 from-group">
                                                        <label runat="server" for="TxtCiiuJuridica">CIIU</label>
                                                        <asp:TextBox ID="TxtCiiuJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="input-field col-xs-12">
                                                        <br />
                                                        <p>
                                                            Nombre o Razón social de los accionistas que tengan una participación mayor al 5%
                                                        </p>
                                                        <p>
                                                            Número de identificación de los accionistas tengan una participación mayor al 5%
                                                        </p>
                                                        <br />
                                                        <h5 style="text-align: center;">Información financiera
                                                        </h5>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtIngresosJuridica">Ingresos mensuales *</label>
                                                        <asp:TextBox ID="txtIngresosJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtIngresosJuridica" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorIngresosMensualesJuridica" class="form-text text-muted">Ingrese una cantidad valida</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="TxtDetalleOtros">Detalle otros ingresos *</label>
                                                        <asp:TextBox ID="TxtDetalleOtros" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <small style="color: Red;" id="LblErrorDetalleOtros" class="form-text text-muted">Ingrese una cantidad valida</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtIngresosJuridica">Egresos mensuales *</label>
                                                        <asp:TextBox ID="txtEgresosJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtEgresosJuridica" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorEgresosMensualesJuridica" class="form-text text-muted">Ingrese una cantidad valida</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 from-group">
                                                        <label runat="server" for="txtTotalActivosJuridica">Total Activos *</label>
                                                        <asp:TextBox ID="txtTotalActivosJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtTotalActivosJuridica" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorTotalActivosJuridica" class="form-text text-muted">Ingrese una cantidad valida</small>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                                        <label runat="server" for="txtTotalPasivosJuridica">Total Pasivos *</label>
                                                        <asp:TextBox ID="txtTotalPasivosJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtTotalPasivosJuridica" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorTotalPasivosJuridica" class="form-text text-muted">Ingrese una cantidad valida</small>
                                                    </div>
                                                    <div class="input-field col-xs-6 col-md-3 from-group">
                                                        <label runat="server" for="txtTotalPatrimonioJuridica">Total Patrimonio *</label>
                                                        <asp:TextBox ID="txtTotalPatrimonioJuridica" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtTotalPatrimonioJuridica" ValidChars="-." />
                                                        <small style="color: Red;" id="LblErrorTotalPatrimonioJuridica" class="form-text text-muted">Ingrese una cantidad valida</small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="accordion-container">
                                <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Operaciones en moneda extranjera<span class="toggle-icon"></span></a>
                                <div class="accordion-content" style="height: auto; overflow: hidden;">
                                    <div class="container-fluid">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-xs-6 col-md-3 from-group">
                                                    <label>Operaciones en moneda extrajera *</label>
                                                    <asp:CheckBoxList ID="ChkOperaciones" runat="server" RepeatDirection="Horizontal"
                                                        Style="margin-top: -5px;">
                                                        <asp:ListItem Value="1" style="margin-left: 20px;">Si</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="0" style="margin-left: 20px;">No</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtPorcBenef2">Cuáles</label>
                                                    <asp:TextBox ID="TxtCuales" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-6 col-md-3 from-group">
                                                    <label>Posee cuentas en moneda extrajera *</label>
                                                    <asp:CheckBoxList ID="ChkPoseeCuentas" runat="server" RepeatDirection="Horizontal"
                                                        Style="margin-top: -5px;">
                                                        <asp:ListItem Value="1" style="margin-left: 20px;">Si</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="0" style="margin-left: 20px;">No</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtPorcBenef2">N° de cuenta</label>
                                                    <asp:TextBox ID="TxtNumeroCuenta" runat="server" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="TxtNumeroCuenta" ValidChars=",." />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtBanco">Banco</label>
                                                    <asp:TextBox ID="TxtBanco" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtCiudad">Ciudad</label>
                                                    <asp:TextBox ID="TxtCiudad" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtMoneda">Moneda</label>
                                                    <asp:TextBox ID="TxtMoneda" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                                </div>
                                                <div class="input-field col-xs-6 col-md-3 form-group">
                                                    <label for="TxtPaís">País</label>
                                                    <asp:TextBox ID="TxtPaís" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                


                                <div class="accordion-content">
                                    <div class="col-sm-12">
                                        <p style="font-size: 11px; text-align: left;">
                                            CERTIFICO QUE LA INFORMACIÓN SUMINISTRADA ES VERIDICA Y AUTORIZO A LA COOPERATIVA PARA QUE LA VERIFIQUE.<br />
                                            ESTOY INFORMADO DE MI OBLIGACIÓN DE ACTUALIZAR ANUALMENTE LA INFORMACIÓN QUE SOLICITE LA ENTIDAD POR CADA PRODUCTO
										O SERVICIO QUE UTILICE.<br />
                                            AUTORIZO A LA COOPERATIVA PARA QUE CONSULTE Y REPORTE INFORMACIÓN A LAS CENTRALES DE RIESGO.<br />
                                            DECLARO QUE MIS INGRESOS Y BIENES PROVIENEN DEL DESARROLLO DE MI ACTIVIDAD ECONÓMICA PRINCIPAL.<br />
                                            "DECLARO QUE EL ORIGEN DE LOS RECURSOS Y DEMAS ACTIVOS PROCEDEN DEL GIRO ORDINARIO DE ACTIVIDADES LICITAS Y QUE LOS RECURSOS 
										QUE ENTREGO PROVIENEN DE LAS SIGUIETES FUENTES ESPECIFICADAS".
                                        </p>
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="fuentes:"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                                <div class="accordion-content">
                                    <div class="col-sm-12">
                                        <p style="font-size: 11px; text-align: left;">
                                            CERTIFICO QUE LA INFORMACIÓN SUMINISTRADA ES VERIDICA Y AUTORIZO A LA COOPERATIVA PARA QUE LA VERIFIQUE.<br />
                                            ESTOY INFORMADO DE MI OBLIGACIÓN DE ACTUALIZAR ANUALMENTE LA INFORMACIÓN QUE SOLICITE LA ENTIDAD POR CADA PRODUCTO
										O SERVICIO QUE UTILICE.<br />
                                            AUTORIZO A LA COOPERATIVA PARA QUE CONSULTE Y REPORTE INFORMACIÓN A LAS CENTRALES DE RIESGO.<br />
                                            DECLARO QUE MIS INGRESOS Y BIENES PROVIENEN DEL DESARROLLO DE MI ACTIVIDAD ECONÓMICA PRINCIPAL.<br />
                                            "DECLARO QUE EL ORIGEN DE LOS RECURSOS Y DEMAS ACTIVOS PROCEDEN DEL GIRO ORDINARIO DE ACTIVIDADES LICITAS Y QUE LOS RECURSOS 
										QUE ENTREGO PROVIENEN DE LAS SIGUIETES FUENTES ESPECIFICADAS".
                                        </p>
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="TxtFuentes" runat="server" CssClass="form-control" placeholder="fuentes:"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="accordion-container">
                                <a href="#" class="accordion-titulo" style="color: #fff; text-decoration: none; text-align: center;">Uso exclusivo de la entidad<span class="toggle-icon"></span></a>
                                <div class="accordion-content" style="overflow: hidden;">
                                    <div class="input-field col-xs-12 form-group">
                                        <label for="txtObservaciones">Observaciones</label>
                                        <textarea id="txtObservaciones" class="form-control"></textarea>
                                    </div>
                                    <div class="input-field col-xs-6 col-md-3 from-group">
                                        <label for="TxtFechaEntrevista">Fecha realización entrevista</label>
                                        <asp:TextBox ID="TxtFechaEntrevista" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                    </div>
                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                        <label for="txtNombreRealizoEntrevista">Nombre de quien realizo entrevista</label>
                                        <asp:TextBox ID="txtNombreRealizoEntrevista" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                        <label for="txtNombreVerificacion">Nombre de quien verifica información</label>
                                        <asp:TextBox ID="txtNombreVerificacion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-field col-xs-6 col-md-3 form-group">
                                        <label for="TxtFechaVerificacion">Fecha verificación información</label>
                                        <asp:TextBox ID="TxtFechaVerificacion" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-sm-11">
                            <h5 class="centrar">Documentos a anexar</h5>
                            <ul>
                                <li style="list-style: disc;">Fotocopia del documento de identificación</li>
                                <li style="list-style: disc;">Constancia de ingresos(honorarios, laborales, certificación de ingresos y retenciones)</li>
                                <li style="list-style: disc;">Declaración de renta del último período gravable disponible</li>
                                <li style="list-style: disc;">Original del certificacido de existencia y representación legal con vigencia no superior a 3 meses</li>
                                <li style="list-style: disc;">Fotocopia del Número de identificación de identificación tributaria NIT</li>
                                <li style="list-style: disc;">Fotocopia del documento del representante legal</li>
                            </ul>
                            <br />
                            <br />
                            <br />
                        </div>
                        <div class="col-sm-1" style="text-align: center;">
                        </div>
                        <div class="col-sm-10" style="text-align: center;">
                            <div class="form-group">
                                <label for="exampleFormControlFile1">
                                Anexe los documentos que solicite la entidad</label>
                                <input type="file" multiple class="form-control-file" id="exampleFormControlFile1" />
                            </div>
                        </div>
                        <div class="col-md-12" style="height: 20px;"></div>
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-6">
                            <div id="dvCaptcha">
                            </div>
                            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                            <%--<asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="The CAPTCHA field is required." ControlToValidate="txtCaptcha" style="font-size:x-small; font-weight:600"
                                runat="server" ForeColor="Red" Display="Dynamic" />--%>
                        </div>

                        <div class="col-sm-12">
                            <asp:Panel ID="panelFooter" runat="server" Style="background-color: #fff; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px"
                                Width="100%" Height="40px">
                            </asp:Panel>
                            <asp:Panel ID="panel1" runat="server" Style="background-color: #0099FF; color: #fff; text-align: center; padding-bottom: 15px; padding-top: 5px"
                                Width="100%" Height="7px">
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="panelFinal" runat="server" Visible="false">
                <div class="col-xs-12">
                    <div class="col-lg-1">
                    </div>
                    <div style="width: 88%; margin: auto; margin-top: 27px">
                        <div class="col-xs-12">
                            <asp:Label ID="Label2" runat="server" Text="Su solicitud de Afiliación se generó correctamente."
                                Style="color: #66757f; font-size: 28px; padding: 0px 200px 0px 0px;" />
                            <asp:Button ID="BtnInicioSesion" runat="server" class="btn btn-primary" Style="border-radius: 2px; padding: 5px 10px; top: 0px; left: 0px;"
                                Text="Inicio de Sesión" OnClick="btnInicioSesion_Click" />
                        </div>
                        <div class="col-xs-12">
                            <p style="margin-top: 36px">
                                Gracias por afiliarse con nosotros, para mayor seguridad su código de solicitud
                            es el
                            <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red" />. Tenga en
                            cuenta el código para cualquier inconveniente o acérquese a nuestras oficinas para
                            mayor información.
                            </p>
                            <p style="margin-top: 36px">
                                Usted puede solicitar un crédito si lo desea, haz clic en el enlace de abajo por
                            si desea realizarlo ahora.
                            </p>
                        </div>
                        <div class="col-xs-12">
                            <asp:Button ID="btnSolicitudCred" runat="server" CssClass="btn btn-success" Style="border-radius: 0; padding: 5px 10px; top: 0px; left: 0px;"
                                Text="Solicitar Cr&eacute;dito" OnClick="btnSolicitudCred_Click" />
                        </div>
                    </div>
                    <div class="col-lg-1">
                    </div>
                </div>
            </asp:Panel>
            <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />
        </form>
    </div>

    <script type="text/javascript" src="//code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="//www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"></script>

    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>


    <script type="text/javascript">

        $(".datepicker").datepicker();

        $(".accordion-titulo").click(function () {

            var contenido = $(this).next(".accordion-content");

            if (contenido.css("display") == "none") { //open
                $(".accordion-titulo").removeClass("open");
                $(".accordion-content").slideUp(250);
                contenido.slideDown(250);
                $(this).addClass("open");
            }
            else { //close		
                contenido.slideUp(250);
                $(this).removeClass("open");
            }
        });

        var onloadCallback = function () {
            //var baseUrl = '<%= baseUrl %>';
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%= ReCaptcha_Key %>',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "Afiliacion.aspx/VerifyCaptcha",
                        data: "{response: '" + response + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            ResponseCaptcha(r);
                        }
                    });
                }
            });
        };

        function ResponseCaptcha(r) {
            var captchaResponse = jQuery.parseJSON(r.d);
            if (captchaResponse.success) {
                $("[id*=txtCaptcha]").val(captchaResponse.success);
                $("[id*=rfvCaptcha]").hide();
            } else {
                $("[id*=txtCaptcha]").val("");
                $("[id*=rfvCaptcha]").show();
                var error = captchaResponse["error-codes"][0];
                $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
            }
        }

    </script>
</body>
</html>
