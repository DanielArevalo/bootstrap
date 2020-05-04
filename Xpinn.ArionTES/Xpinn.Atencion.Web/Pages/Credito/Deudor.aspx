<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Deudor.aspx.cs" Inherits="Deudor" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deudor</title>
    <link href="~/Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/materialize.min.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
     
     .tableNormal
     {
         border-collapse : separate;
         border-spacing:4px;
             width: 103%;
         }
         * 
         {margin: 0px auto;
         }
     </style>

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

        function radioMeEstCivil(e) {
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
        
    </script>
     
</head>
<body>
    <form id="form1" runat="server" style="width:95%; margin:auto;">
    <div class="col-xs-12" style="margin-top:20px;">
        <asp:Panel ID="Pinformacion" runat="server" Style="background-color: #0099FF; color: #fff;
            text-align: center; padding-bottom: 5px; padding-top: 5px" Width="100%">
            INFORMACIÓN DEUDOR SOLIDARIO
        </asp:Panel>
    </div>
    <div class="col-md-12">
        <section class="main row" style="width:100%">
            <div style="width:85%; margin:10px auto;">
                <div class="row">
                    <div class="row" style="margin:5px 0px;">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtApellido1" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="txtApellido1">Primer Apellido</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtApellido2" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="txtApellido2">Segunda Apellido</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtNombresDeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtNombresDeudor">Nombre(s)</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <label class="col s6 m3" for="cblSexo" style="margin-top:-9%;">
                            Sexo</label>
                            <asp:CheckBoxList ID="cblSexo" runat="server" RepeatDirection="Horizontal" 
                                style="margin-top:-3px;">
                                <asp:ListItem Value="1">Hombre</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">Mujer</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="DDLDocumento" runat="server" class="drop">
                                <asp:ListItem disabled="" selected="" value="">Indique su tipo de Documento</asp:ListItem>
                                <asp:ListItem value="1">Cédula de ciudadania</asp:ListItem>
                                <asp:ListItem value="2">Cédula Extranjera</asp:ListItem>
                                <asp:ListItem value="3">Nit</asp:ListItem>
                                <asp:ListItem Value="4">Nit de Extranjeria</asp:ListItem>
                                <asp:ListItem value="5">Rup</asp:ListItem>
                            </asp:DropDownList>
                            <label for="DDLDocumento">Tipo de documento</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="txtNumero">Numero de identificación</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtFechaExpedicion" runat="server" class="datepicker" type="date" />
                            <label for="txtFechaExpedicion">Fecha de Expedición</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="DDLciudadExpedición" runat="server" Class="drop">
                                <asp:ListItem Value="" Selected="" disabled="">Indique su ciudad de expedición</asp:ListItem>
                            </asp:DropDownList>
                            <label for="DDLciudadExpedición">Ciudad de Expedición</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtFechaNacimiento" runat="server" class="datepicker" type="date" />
                            <label for="TxtFechaNacimiento">Fecha de Nacimiento</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="Ddlnacimiento" runat="server" Class="drop">
                            <asp:ListItem Value="" Selected="" disabled="">Indique su ciudad de nacimiento</asp:ListItem>
                            </asp:DropDownList>
                            <label for="Ddlnacimiento">Ciudad de Nacimiento</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="DropDownList2" runat="server" class="drop">
                                <asp:ListItem disabled="" selected="" Value="">Indique su Estado civil</asp:ListItem>
                            </asp:DropDownList>
                            <label ID="Label1" runat="server" for="fs">
                            Estado Civil</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="cbNivelAcademico" runat="server" class="drop">
                                <asp:ListItem value="" Selected="" disabled="">Indique su nivel academico</asp:ListItem>
                            </asp:DropDownList>
                            <label id="Label4" runat="server" for="cbNivelAcademico">Nivel Academico</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtPersonaCargo" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="txtPersonaCargo">Personas a cargo</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <label id="Label5" runat="server" for="cblCabezaFamilia" style="margin-top:-10%;">Cabeza de Familia</label>
                            <asp:CheckBoxList ID="cblCabezaFamilia" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtProfesion" runat="server" CssClass="validate"></asp:TextBox>
                            <label runat="server" for="txtProfesion">Profesión</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="validate"></asp:TextBox>
                            <label id="Label2" runat="server" for="txtDireccion">Direción</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="cblEstrato" runat="server" class="drop">
                                <asp:ListItem disabled="" selected="" Value="">Indique su estrato social</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                            <label runat="server" for="cblEstrato">Estrato social</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="drop">
                                <asp:ListItem disabled="" selected="" Value="">Indique su departamento</asp:ListItem>
                            </asp:DropDownList>
                            <label id="Label3" runat="server" for="ddlDepartamento">Departamento</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="drop">
                                <asp:ListItem disabled="" selected="" Value="">Indique su Ciudad</asp:ListItem>
                            </asp:DropDownList>
                            <label runat="server" for="ddlCiudad">Ciudad</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlBarrio" runat="server" CssClass="drop">
                                <asp:ListItem disabled="" selected="" Value="">Indique su Barrio</asp:ListItem>
                            </asp:DropDownList>
                            <label runat="server" for="ddlBarrio">Barrio</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txttelefonodeudor" runat="server" Class="validate"></asp:TextBox>
                            <label runat="server" for="ddlBarrio">Telefono</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtTelCelular" runat="server" CssClass="textbox"></asp:TextBox>
                            <label runat="server" for="TxtTelCelular">Telefono Celular</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtEmail" runat="server" type="email" Class="validate"></asp:TextBox>
                            <label runat="server" for="TxtEmail" data-error="Email no valido" data-success="Correcto">Email</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="DDLestadoDeudor" runat="server" CssClass="drop">
                                <asp:ListItem>Empleado</asp:ListItem>
                                <asp:ListItem>Contratista</asp:ListItem>
                                <asp:ListItem>Independiente</asp:ListItem>
                                <asp:ListItem>Pensionado</asp:ListItem>
                            </asp:DropDownList>
                            <label runat="server" for="DDLestadoDeudor">Estado laboral</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtempresadeudor" runat="server" Class="validate"></asp:TextBox>
                            <label runat="server" for="Txtempresadeudor">Empresa donde labora</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtnitdeudor" runat="server" Class="validate"></asp:TextBox>
                            <label runat="server" for="Txtnitdeudor">Nit empresa donde labora</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtDireccionEmpresa" runat="server" CssClass="validate"></asp:TextBox>
                            <label runat="server" for="TxtDireccionEmpresa">Dirección donde labora</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txttelefonolaboraldeudor" runat="server" Class="validate"></asp:TextBox>
                            <label runat="server" for="Txttelefonolaboraldeudor">Telefono donde labora</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlDepartamentoLaboral" runat="server" CssClass="drop">
                                <asp:ListItem disabled="" selected="" Value="">Indique su departamento</asp:ListItem>
                            </asp:DropDownList>
                            <label runat="server" for="ddlDepartamentoLaboral">Departamento donde labora</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="Ddltipocontratodeudor" runat="server" Class="drop">
                            </asp:DropDownList>
                            <label runat="server" for="Ddltipocontratodeudor">Tipo de contrato</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtcargodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label runat="server" for="Txtcargodeudor">Cargo que se le otrogaba</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtInicioLaboral" runat="server" class="datepicker" type="date" />
                            <label for="TxtInicioLaboral">Fecha de inicio</label>
                        </div>
                    </div>
                    <div class="row">
                            <div style="margin-bottom:5px; margin-top:-10px;">
                                <div class="input-field col s6 m3">
                                <label></label>
                                </div>
                                <div class="input-field col s6 m3">
                                    <label>Ingreso salario mensual</label>
                                </div>
                                <div class="input-field col s6 m3">
                                    <label>Otros ingresos</label> 
                                </div>
                                <div class="input-field col s6 m3">
                                    <label>Deducciones</label>
                                </div>
                            </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlPeriodicidadPago" runat="server" CssClass="drop">
                            </asp:DropDownList>
                            <label for="ddlPeriodicidadPago" runat="server">Frecuencia de ingresos</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtIngsalariomensual" class="validate" runat="server"></asp:TextBox> 
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtOtrosing" class="validate" runat="server"></asp:TextBox>  
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtDeducciones" CssClass="validate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="txtTotalIng" runat="server" CssClass="validate" Enabled="false" placeholder="$" Style="text-align: right"></asp:TextBox>
                            <label for="txtTotalIng">Total de ingresos</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtempresaAnteriordeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtempresaAnteriordeudor">Empresa anterior</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtFechaLiquidacíon" runat="server" class="datepicker" type="date" />
                            <label for="TxtFechaLiquidacíon">Fecha retiro de la empresa</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtcargoanteriordeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtcargoanteriordeudor">Cargo anterior</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtContactoEmpresa" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtContactoEmpresa">Contacto en la empresa</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtTelefonoContacto" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtTelefonoContacto">Telefono de contacto</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtCargoContacto" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtCargoContacto">Cargo del contacto</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtemailcontactodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtemailcontactodeudor" data-error="Email no valido" data-success="Correcto">Email del contacto</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtelefonoanteriordeudor" runat="server" Class="validate"></asp:TextBox>
                            <label for="Txtelefonoanteriordeudor">Telefono anterior</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlVivienda" runat="server" RepeatDirection="Horizontal" class="drop">
                                <asp:ListItem Value="" Selected="" disabled="">Indique el tipo de vivienda</asp:ListItem>
                                <asp:ListItem Value="1">Propia</asp:ListItem>
                                <asp:ListItem Value="2">Arrendada</asp:ListItem>
                                <asp:ListItem Value="3">Familiar</asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlVivienda">Vivienda</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtnombrearrendatariodeudor" runat="server" Class="validate"></asp:TextBox>
                            <label for="Txtnombrearrendatariodeudor">Nombre arrendatario</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txttelarrendatariodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txttelarrendatariodeudor">Telefono arrendatario</label>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <div class="col-xs-12">
        <asp:Panel ID="Prelacionbienesdeudor" runat="server" Style="text-align:center; background-color: #0099FF;
            color: #fff; height: 33px;" Width="100%">
            <div style="margin:0px auto; text-align:center;">
                <h6 style="color:#fff;">Relacion de bienes de su propiedad</h6>
            </div>
        </asp:Panel>
    </div>
    <div class="col-md-12">
        <section class="main row" style="width:100%">
            <div style="width:85%;">
                <div class="row">
                    <div class="row" style="margin:5px 0px;">
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="ddlPropiedad" runat="server" class="drop" >
                                <asp:ListItem Value="Casa">Casa</asp:ListItem>
                                <asp:ListItem Value="Apartamento">Apartamento;</asp:ListItem>
                                <asp:ListItem Value="Finca">Finca</asp:ListItem>
                                <asp:ListItem Value="Lote">Lote</asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlPropiedad">Concepto de la propiedad</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtdireccionpropiedaddeudor" runat="server" Class="validate"></asp:TextBox>
                            <label for="Txtdireccionpropiedaddeudor">Dirección de la propiedad</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:DropDownList ID="DDLCiudadDeudor" runat="server" CssClass="drop">
                            </asp:DropDownList>
                            <label for="DDLCiudadDeudor">Ciudad de la propiedad</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtescrituranumerodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtescrituranumerodeudor">Numero de la escritura</label>
                        </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtnotariadeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtnotariadeudor">Notaria</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:CheckBoxList ID="ChkHipoteca" runat="server" class="validate" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:CheckBoxList>
                            <label style="margin-top:-20px;" for="ChkHipoteca">Hipoteca</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtmatriculainmoviliariadeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtmatriculainmoviliariadeudor">Matricula inmoviliaria</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtvalorcomercialdeudor" runat="server" CssClass="validate" Placeholder="$"></asp:TextBox>
                            <label for="Txtvalorcomercialdeudor">Valor comercial</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtvalorhipotecadeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtvalorhipotecadeudor">Valor Hipoteca</label>
                        </div>
                        <div class="input-field col s6 m3">
                        </div>
                        <div class="input-field col s6 m3">
                        </div>
                        <div class="input-field col s6 m3">
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top:-30px;">
            <div class="input-field col s12 m6"  style="border:1px solid #0099FF; border-radius:5px; margin-left:-10px; margin-right:10px;">
                <br />
                <div style="text-align: center; background-color: #0099FF; color: #fff; border-radius:5px;">
                    Gastos mensuales generales
                </div>
                <div class="container">
                    <br />
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txtarriendoviviendadeudor" runat="server" CssClass="validate" Placeholder="$"></asp:TextBox>
                        <label for="Txtarriendoviviendadeudor">Arriendo/cuota Vivienda</label>
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txtgastosdeudor" runat="server" CssClass="validate" Placeholder="$"></asp:TextBox>
                        <label for="Txtgastosdeudor">Gastos de sostenimiento</label>
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txtotrosgastosdeudor" runat="server" CssClass="validate"  Placeholder="$"></asp:TextBox>
                        <label for="Txtotrosgastosdeudor">Otros Gastos</label>
                    </div>
                    <div class="input-field col s12" style="background-color:#0099FF; margin:1px 0px; width:125%; margin-left:-12%;">
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txttotalgastosdeudor" runat="server" CssClass="validate"></asp:TextBox>
                        <label for="Txttotalgastosdeudor">Total de Gastos</label>
                    </div>
                </div>
            </div>
            <div class="input-field col s12 m6" style="border:1px solid #0099FF; border-radius:5px; margin-left:10px; margin-right:-10px;">
            <br />
                <div style="text-align: center;background-color: #0099FF; color: #fff; border-radius:5px;">
                    Vehiculos
                </div>
                
                <div class="container">
                    <br />
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txtmarcamodelodeudor" runat="server" CssClass="validate"></asp:TextBox>
                        <label for="Txtmarcamodelodeudor">Marca/Modelo</label>
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txtvalorcomercialvehiculodeudor" runat="server" CssClass="validate"></asp:TextBox>
                        <label for="Txtvalorcomercialvehiculodeudor">Valor comercial</label>
                    </div>
                    <div class="input-field col s12">
                        <asp:DropDownList ID="cbPignorado" runat="server" class="drop" RepeatDirection="Horizontal">
                            <asp:ListItem Value="" selected="" disabled="">Indique si esta pignorado</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                        <label for="Txtotrosgastosdeudor">Pignorado</label>
                    </div>
                    <div class="input-field col s12" style="background-color:#0099FF; margin:1px 0px; width:125%; margin-left:-12%;">
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="Txtvalorpignoradodeudor" runat="server" CssClass="validate" placeholder="$"></asp:TextBox>
                        <label for="Txtvalorpignoradodeudor">Valor pignorado</label>
                    </div>
                </div
            </div>
            </div>
         </div>
    </section>
</div>
    
<div class="col-md-12">
    <asp:Panel ID="Pcreditosdeudor" runat="server" Style="text-align: center; background-color: #0099FF;
        color: #fff;" Height="26px">
        Creditos
    </asp:Panel>
</div>
    <div class="col-md-12">
        <section class="main row" style="width:100%">
            <div style="width:85%;">
                <div class="row">
                    <div class="row" style="margin:5px 0px;">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtCreditoNumero" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtCreditoNumero">Numero de credito</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtentidadcreditodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtentidadcreditodeudor">Entidad del crédito</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtDiaVencimiento" type="date" class="datepicker" runat="server"></asp:TextBox>
                            <label for="TxtDiaVencimiento">Fecha de vencimiento</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtsaldofechadeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtsaldofechadeudor">Saldo a la fecha</label>
                        </div>
                    </div>
                    <div class="row" style="margin:5px 0px;">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtvalorcuotadeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtvalorcuotadeudor">Valor de cuota</label>
                        </div>
                        <div class="input-field col s1 m9">
                            <asp:TextBox ID="TxtObservaciones2" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtObservaciones2">Observaciones</label>
                        </div>
                    </div>
                    <div style="background-color:#0099FF; height:5px;">
                    </div>
                    <div class="row" style="margin:5px 0px;">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtCreditoNumero2" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtCreditoNumero2">Numero de credito</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtentidadcreditodeudor2" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtentidadcreditodeudor2">Entidad del crédito</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtDiaVencimiento2" type="date" class="datepicker" runat="server"></asp:TextBox>
                            <label for="TxtDiaVencimiento2">Fecha de vencimiento</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtsaldofechadeudor2" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtsaldofechadeudor2">Saldo a la fecha</label>
                        </div>
                    </div>
                    <div class="row" style="margin:5px 0px;">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtvalorcuotadeudor2" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtvalorcuotadeudor2">Valor de cuota</label>
                        </div>
                        <div class="input-field col s12 m9">
                            <asp:TextBox ID="TxtObservaciones" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtObservaciones">Observaciones</label>
                        </div>
                    </div>
                </div>
            </div>
    <asp:Panel ID="Pconsolidadodeudor" runat="server" Style="background-color: #0099FF;
        text-align: center; color: #fff;" Height="26px">Consolidado obligaciones financieras
    </asp:Panel>
            <div style="width:85%;">
                <div class="row">
                    <div class="row">
                        <div class="input-field col s6 m6">
                            <asp:TextBox ID="Txtsaldoconsolidadodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtsaldoconsolidadodeudor">Saldo consolidado</label>
                        </div>
                        <div class="input-field col s6 m6">
                            <asp:TextBox ID="Txtcuotapormesdeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtcuotapormesdeudor">Cuota por mes</label>
                        </div>
                    </div>
                    <asp:Panel ID="Panelcierredeudor" runat="server" Style="background-color: #0099FF;
                        text-align: center; color: #fff;" Height="2px">
                    </asp:Panel>
                    <div class="row">
                        <div class="input-field col s12">
                        <p style="font-size:11px; text-align:justify;"><strong>DECLARACION DE INFORMACION:</strong> Declaro (amos) que la información suministrada
            en el presente formulario corresponde a la realidad y asumo(imos) plena responsabilidad
            por la veracidad de la misma, en caso de comprobarse alguna inexactitud, será causal
            suficiente para el rechazo a cualquier clase de servicio que pretenda(mos) acceder.
            Reconozco(cemos) la obligación legal de actualizar esta información por lo menos
            una vez al año y en caso de suceder algún cambio durante el lapso referido, me(nos)
            comprometo(emos) a actualizar el cambio de manera inmediata ante Coofinanciamos
            Ltda., en caso de incumplimiento autorizo(amos) me sean bloqueados los servicios
            a los cuales pretenda acceder y tenga dentro de la Cooperativa en calidad de Afiliado,
            Deudor y/o Codeudor. Certifico(amos) que todas mis(nuestras) actividades las realizo(amos)
            dentro de las normas Legales, por lo tanto no he(mos) entregado ni entregare(mos)
            dinero a Coofinanciamos Ltda., producto de actividades ilícitas o al margen de la
            Ley. En caso de infracción que se derive de información errónea, falsa o inexacta
            eximo(imos) a Coofinanciamos Ltda., de cualquier responsabilidad. <strong>DECLARACION
                DE RECEPCIÓN Y CONOCIMIENTO DE LA INFORMACION:</strong> Dejo(amos) constancia
            que me(nos) han suministrado información comprensible, legible sobre las normas
            que rigen la Cooperativa y sobre los términos y condiciones de los créditos que
            ofrece Coofinanciamos Ltda., especialmente lo siguiente: Estatutos de la Cooperativa
            Multiactiva Coofinanciamos Ltda., Derechos y obligaciones que adquiero como Asociado.
            Alcance sobre los aportes que realizare a la Coofinanciamos Ltda., compensaciones,
            devolución, etc. Condiciones y Términos de los créditos: 1. Plazos. 2. Tasas de
            Interés. 3. Base del Capital sobre la cual se aplica la tasa de interés. 4. Tasa
            de Interés moratoria. 5 Comisiones y costos administrativos. 6. Recargos. 7. Condiciones
            de Prepago y abonos extraordinarios. 8. Derechos, procedimientos y costos en caso
            de incumplimiento. 9 toda la información relevante y necesaria para mí(nuestra)
            adecuada comprensión. De acuerdo a lo anterior, manifiesto(estamos) que conocozco(cemos)
            las Obligaciones y derechos que adquiero(mos) al ser aceptado(s) como Asociado(s).
            Igualmente conozco(cemos) las condiciones de los créditos, las cuales acepto(amos)
            con pleno conocimiento. <strong>AUTORIZACION PARA CONSULTA, CONOCIMIENTO Y REPORTE INFORMACIÓN:</strong>
            Autorizo(amos) y doy(amos) consentimiento expreso e irrevocable a la Cooperativa
            Multiactiva Coofinanciamos Ltda., para que con fines estadísticos y de información,
            en cualquier tiempo consulte, reporte, circule e incluya información a las Centrales
            de Riesgo y entidades que manejen bases de datos con los mismos fines, relacionadas
            con mi(nuestro) nombre, mí (nuestro) comportamiento comercial, hábitos de pago,
            manejo de crédito y cuentas, con mis obligaciones crediticias, tiempos de mora en
            el pago de dichas obligaciones, lo mismo que el suministro de tal información a
            quien tenga interés legitimo en ella.<strong>
                <br />
                DOCUMENTOS REQUERIDOS:</strong>
            <br />
            - Certificación laboral no superior a un mes de expedición
            <br />
            -Desprendible de pago de los dos últimos meses
            <br />
            - Copia de la Cedula
            <br />
            - Copia de un recibo publico del lugar de residencia.
            <br />
            - Formulario diligenciado completamente
        </p>
                        </div>
                    </div>
        <asp:Panel ID="Panel3" runat="server" Style="background-color: #0099FF;
            text-align: center; color: #fff;" Height="2px">
        </asp:Panel>
                    <div class="row">
                        <div class="input-field col s6 m6">
                            <asp:DropDownList ID="ddlTipoSolicitud" class="drop" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="" Selected="" disabled="">Indique su tipo de solicitud</asp:ListItem>
                                <asp:ListItem Value="Afiliacion">Afiliación</asp:ListItem>
                                <asp:ListItem Value="Credito">Crédito</asp:ListItem>
                                <asp:ListItem Value="AfiliacionYCredito">Afiliación y Crédito</asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlTipoSolicitud">Tipo de solicitud</label>
                        </div>
                        <div class="input-field col s6 m6">
                            <asp:DropDownList ID="ddlEnvioCorrespondencia" class="drop" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Casa">Casa</asp:ListItem>
                                <asp:ListItem Value="Oficina">Oficina</asp:ListItem>
                                <asp:ListItem Value="Correo">Correo Electrónico Personal</asp:ListItem>
                            </asp:DropDownList>
                            <label for="ddlEnvioCorrespondencia">Envio de correpondencia</label>
                        </div>
                    </div>
                    <asp:Panel ID="Pfirmas" runat="server" Height="26px" Style="background-color: #0099FF; color: #fff; text-align: center;">Firmas
                    </asp:Panel>
                    <div class="row">
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtNombresolicitante" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtNombresolicitante">Nombre del solicitante</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="TxtCedulaSolicitante" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="TxtCedulaSolicitante">Cedula del solicitante</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtnombrecodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtnombrecodeudor">Nombre del codeudor</label>
                        </div>
                        <div class="input-field col s6 m3">
                            <asp:TextBox ID="Txtcedulacodeudor" runat="server" CssClass="validate"></asp:TextBox>
                            <label for="Txtcedulacodeudor">Cedula del codeudor</label>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <asp:Panel ID="Panel1" runat="server" Style="background-color: #0099FF; color: #fff; text-align: center;">Espacio exclusivo de la Cooperativa.</asp:Panel>
        <section class="main row" style="width:100%">
            <div style="width:85%;">
                <div class="row">
                    <div class="input-field col s6 m3">
                        <asp:DropDownList ID="ddpEstadoSolicitud" runat="server" RepeatDirection="Horizontal" class="drop">
                            <asp:ListItem Value="" Selected="" disabled="">Estado de la solicitud</asp:ListItem>
                            <asp:ListItem Value="1">Aprovada</asp:ListItem>
                            <asp:ListItem Value="2">En Estudio</asp:ListItem>
                            <asp:ListItem Value="3">Negada</asp:ListItem>
                        </asp:DropDownList>
                        <label for="ddpEstadoSolicitud">Estado de la solicitud</label>
                    </div>
                    <div class="input-field col s6 m3">
                        <asp:TextBox ID="Txtestudiadopor" runat="server" CssClass="validate"></asp:TextBox>
                        <label for="Txtestudiadopor">Estudiado y Aprobado por:</label>
                    </div>
                    <div class="input-field col s6 m3">
                        <asp:TextBox ID="TxtCargoQuienAprobo" runat="server" CssClass="validate"></asp:TextBox>
                        <label for="TxtCargoQuienAprobo">Cargo de quien aprobo</label>
                    </div>
                    <div class="input-field col s6 m3">
                        <asp:TextBox ID="TxtFechaAprobacion" runat="server" type="date" class="datepicker"></asp:TextBox>
                        <label for="TxtFechaAprobacion">Fecha de aprobación</label>
                    </div>
                </div>
            </div>
    <asp:Panel ID="Panel2" runat="server" Height="16px" Style="background-color: #0099FF;">
    </asp:Panel>
        </section>
    </div>
    </form>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script src="../../Scripts/materialize.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.drop').material_select();
        });
        $('.datepicker').pickadate({
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15 // Creates a dropdown of 15 years to control year
        });
        $(document).ready(function () {
            $('.collapsible').collapsible();
        });
    </script>
</body>
</html>
