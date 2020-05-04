<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AfiliacionFecem.aspx.cs" Inherits="Page_Aportes_Afiliaciones_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../../Styles/materialize.css"/>
    <link href="../../../Styles/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link  href="../../../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <style>
        .row {
            width:95%;
            margin:0px auto;
        }
        .ir-arriba {
	        display:none;
	        padding:20px 25px;
	        font-size:40px;
	        color:#0099CC;
            background-color:#fff;
            border:2px solid #0099CC;
            border-radius:50px;
	        cursor:pointer;
	        position: fixed;
	        bottom:20px;
	        right:20px;
        }
        .division {
            border-right:1px solid #0099cc;
                border-bottom:1px solid #0099cc;
        }
        .division2 {
            border-right: 1px solid #0099cc;
            border-bottom:1px solid #0099cc;
        }
        .division3 {
            border-right: none;
            border-bottom:1px solid #0099cc;
        }

        @media (max-width:600px) {
            .division {
                border-right: none;
                border-bottom:1px solid #0099cc;
            }
            .division2 {
                border-right: none;
                border-bottom:1px solid #0099cc;
            }
        }
    </style>
</head>
    <body style="border-top:6px solid #0099CC; border-bottom:15px solid #1a79b1;">
         <form action="#" id="form1" runat="server">
        <div style="width:95%; margin:0px auto; margin-top:10px;">
            <div class="row"">
                <div class="col m12" style="margin:10px auto;">
                    <div class="col m5 col s6" style="text-align:center;">
                        <asp:Image ID="imgEmpresa" runat="server" ImageUrl="~/Images/logoInterna.jpg" style="width:30%;"/>
                    </div>
                    <div class="col m7 col s6"><div style="height:10px;"></div>
                        <asp:Label ID="Label4" runat="server" CssClass="text-primary" Style="font-size: 2.5em; color:#0099CC;" Text="Solicitud de Afiliación"></asp:Label>
                    </div>
                </div>
            </div>
            <hr style="width: 100%; margin: 4px auto; color:#0099CC
" />
            <div class="row">
                <div class="col-lg-10 col-md-12 col-sm-12">
                    <div class="col-xs-12">
                        <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 13px" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">SOLICITUD DE AFILIACIÓN</h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="Txtfecha" type="date" class="datepicker"/>
                        <label for="Txtfecha">Fecha</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCiudadSolicitud" type="text" class="validate"/>
                        <label for="TxtCiudadSolicitud">Ciudad de solicitud</label>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtIdentificacion" type="number" class="validate"/>
                        <label for="TxtIdentificacion">N° de Identificación</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="TipoDocumento">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">C.C</option>
                          <option value="2">C.E</option>
                        </select>
                        <label for="TipoDocumento">Tipo de documento</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCiudadExpedicion" type="text" class="validate"/>
                        <label for="TxtCiudadExpedicion">Lugar de expedición</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtFechaExpedicion" type="date" class="datepicker"/>
                        <label for="TxtFechaExpedicion">Fecha de expedición</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtApellidos" type="text" class="validate"/>
                        <label for="TxtApellidosCod">Apellidos</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtNombres" type="text" class="validate"/>
                        <label for="TxtNombresCod">Nombres</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="Sexo">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Masculino</option>
                          <option value="2">Femenino</option>
                        </select>
                        <label for="Sexo">Sexo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="EstadoCivil">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Soltero</option>
                          <option value="2">Casado</option>
                          <option value="3">Otro</option>
                        </select>
                        <label for="EstadoCivil">Estado civil</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtFechaNacimientoCod" type="date" class="datepicker"/>
                        <label for="TxtFechaNacimientoCod">Fecha de nacimiento</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="Vivienda">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Si</option>
                          <option value="2">No</option>
                        </select>
                        <label for="Vivienda">Vivienda</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtFechaIngresoEmpresa" type="date" class="datepicker"/>
                        <label for="TxtFechaIngresoEmpresa">Fecha de ingreso a la empresa</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtTipoContrato" type="text" class="validate"/>
                        <label for="TxtTipoContrato">Tipo de contrato</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtSalarioBasico" type="number" class="validate" placeholder="$"/>
                        <label for="TxtSalarioBasico">Salario Básico</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="NivelEducativo">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Primaria</option>
                          <option value="2">Bachiller</option>
                          <option value="3">Técnico/Tecnólogo</option>
                          <option value="4">Profesional</option>
                        </select>
                        <label for="NivelEducativo">Nivel educativo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtEstudiosEnCurso" type="text" class="validate"/>
                        <label for="TxtEstudiosEnCurso">Estudios en curso</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtEmpresaLaborar" type="text" class="validate"/>
                        <label for="TxtEmpresaLaborar">Empresa en la que labora</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCodigoNomina" type="text" class="validate"/>
                        <label for="TxtCodigoNomina">Código de Nómina</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtUbicacionLaboral" type="text" class="validate"/>
                        <label for="TxtUbicacionLaboral">Ubicación laboral</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtProfesion" type="text" class="validate"/>
                        <label for="TxtProfesion">Profesión u oficio</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCargoActual" type="text" class="validate"/>
                        <label for="TxtCargoActual">Cargo actual</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtTelefonoResidencia" type="number" class="validate"/>
                        <label for="TxtTelefonoResidencia">Telefono de residencia</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCelularPersonal" type="number" class="validate"/>
                        <label for="TxtCelularPersonal">Celular Personal</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCelularCorporativo" type="number" class="validate"/>
                        <label for="TxtCelularCorporativo">Celular corporativo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtDireccionResidencia" type="text" class="validate"/>
                        <label for="TxtDireccionResidencia">Dirección de residencia</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtBarrio" type="text" class="validate"/>
                        <label for="TxtBarrio">Barrio</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtEstrato" type="number" class="validate"/>
                        <label for="TxtEstrato">Estrato</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtE-mailPersonal" type="email" class="validate"/>
                        <label for="TxtE-mailPersonal">E-mail Personal</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtE-mailCorporativo" type="email" class="validate"/>
                        <label for="TxtE-mailCorporativo">E-mail corporativo</label>
                    </div>
                </div>
            </div>
            <span class="ir-arriba fa fa-angle-double-up"></span>
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">Familiares ó personas dependientes</h4>
                    </div>
                </div>
                <div class="col m6 s12 division">
                    <div class="input-field col m6 s12">
                        <input id="TxtNombresDependiente1" type="text" class="validate"/>
                        <label for="TxtNombresDependiente1">Nombres</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtApellidosDependiente1" type="text" class="validate"/>
                        <label for="TxtApellidosDependiente1">Apellidos</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtFechaNacimientoDependiente1" type="date" class="datepicker"/>
                        <label for="TxtFechaNacimientoDependiente1">Fecha de nacimiento</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <select id="SexoDependiente1">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Masculino</option>
                          <option value="2">Femenino</option>
                        </select>
                        <label for="SexoDependiente1">Sexo</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtNumeroIdentificacionDependiente1" type="number" class="validate"/>
                        <label for="TxtNumeroIdentificacionDependiente1">Numero de identificación</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtOcupacionDependiente1" type="text" class="validate"/>
                        <label for="TxtOcupacionDependiente1">Ocupación</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtNivelEducativoDependiente1" type="text" class="validate"/>
                        <label for="TxtNivelEducativoDependiente1">Nivel Educativo</label>
                    </div>
                </div>
                <div class="col m6 x12 division3">
                    <div class="input-field col m6 s12">
                        <input id="TxtNombresDependiente2" type="text" class="validate"/>
                        <label for="TxtNombresDependiente2">Nombres</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtApellidosDependiente2" type="text" class="validate"/>
                        <label for="TxtApellidosDependiente2">Apellidos</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtFechaNacimientoDependiente2" type="date" class="datepicker"/>
                        <label for="TxtFechaNacimientoDependiente2">Fecha de nacimiento</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <select id="SexoDependiente2">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Masculino</option>
                          <option value="2">Femenino</option>
                        </select>
                        <label for="SexoDependiente2">Sexo</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtNumeroIdentificacionDependiente2" type="number" class="validate"/>
                        <label for="TxtNumeroIdentificacionDependiente2">Numero de identificación</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtOcupacionDependiente2" type="text" class="validate"/>
                        <label for="TxtOcupacionDependiente2">Ocupación</label>
                    </div>
                    <div class="input-field col m6 s12">
                        <input id="TxtNivelEducativoDependiente2" type="text" class="validate"/>
                        <label for="TxtNivelEducativoDependiente2">Nivel Educativo</label>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel2" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">Información financiera
                        </h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtIngresosMensuales" type="number" class="validate"/>
                        <label for="TxtIngresosMensuales">Ingresos mensuales (Pesos)</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtActivos" type="number" class="validate"/>
                        <label for="TxtActivos">Activos (Pesos)</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtEgresosMensuales" type="number" class="validate"/>
                        <label for="TxtEgresosMensuales">Egresos mensuales (Pesos)</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtPasivos" type="number" class="validate"/>
                        <label for="TxtPasivos">Pasivos (Pesos)</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtOtrosIngresos" type="number" class="validate"/>
                        <label for="TxtOtrosIngresos">Otros ingresos (Pesos)</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtConceptoOtrosIngresos" type="number" class="validate"/>
                        <label for="TxtConceptoOtrosIngresos">Concepto de otros ingresos (Pesos)</label>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel3" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col m12">
                    <p class="col m12" style="text-align:justify;">
                        Por su cargo o actividad maneja Recursos Públicos? 
                        <input type="checkbox" class="filled-in" id="ChkPregunta1Si" style="margin-left:10%;"/>
                        <label for="ChkPregunta1Si">Si</label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="checkbox" class="filled-in" id="ChkPregunta1No"/>
                        <label for="ChkPregunta1No">No</label>
                    </p>
                </div>
                <div class="col m12">
                    <p class="col m12" style="text-align:justify;">
                        Por su actividad u oficio, goza usted de reconocimiento Púbico? 
                        <input type="checkbox" class="filled-in" id="ChkPregunta2Si"  style="margin-left:10%;"/>
                        <label for="ChkPregunta2Si">Si</label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="checkbox" class="filled-in" id="ChkPregunta2No"/>
                        <label for="ChkPregunta2No">No</label>
                    </p>
                </div>
                <div class="col m12">
                    <p class="col m12" style="text-align:justify;">
                        Existe algunas de las preguntas anteriores es afirmativa por favor especifique: <input id="Pregunta3" type="text" 
                            style="width:250px;" class="validate"/>
                    </p>
                </div>
            </div>

            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel4" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">Declaración de orígen de fondos
                        </h4>
                    </div>
                </div>
                <div class="col m12">
                    <p class="col m12" style="text-align:justify;">
                        Declaro expresamente que:<br />
                        <strong style="font-weight:700;">1.</strong> Los recursos que poseo provienen de las siguientes fuentes 
                        (detalle ocupación, actividad o negocio):  <input id="TxtFuentesIngresos" type="text" style="width:250px;" class="validate"/>
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">2.</strong> Tanto mi actividad, profesión u oficio es lícita y la ejerzo dentro del marco 
                        legal y los recursos que poseo no provienen de actividades ilícitas de las contempladas en el Código Penal Colombiano.
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">3.</strong> La información que he suministrado en la solicitud y en este documento es veraz 
                        y verificable y me obligo a actualizarla anualmente
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">4.</strong> Los recursos que se deriven del desarrollo de este contrato no se destinarán a 
                        la financiación del terrorismo, grupos terroristas o actividades terroristas.
                    </p>
                </div><br />
            </div>
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel9" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">¿Cómo se enteró de FECEM?</h4>
                    </div>
                    <div class="input-field col m3 s12">
                      <input type="checkbox" class="filled-in" id="TxtRecursosHumanos"/>
                      <label for="TxtRecursosHumanos">Recursos humanos</label>
                    </div>
                    <div class="input-field col m3 s12">
                      <input type="checkbox" class="filled-in" id="TxtCompañeros"/>
                      <label for="TxtCompañeros">Compañeros de trabajo</label>
                    </div>
                    <div class="input-field col m3 s12">
                      <input type="checkbox" class="filled-in" id="TxtEmpleadoFondo"/>
                      <label for="TxtEmpleadoFondo">Empleado del fondo</label>
                    </div>
                    <div class="input-field col m3 s12">
                        <input id="TxtOtros" type="text" class="validate"/>
                        <label for="TxtOtros">Otros</label>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel5" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">Autorización de descuento</h4>
                    </div>
                </div>
                <div class="col m12" style="margin-bottom:2%;">
                    <p class="col m12" style="text-align:justify;">
                        Yo, <input id="TxtNombresApellidos" type="text" style="width:250px;" class="validate"/> identificado con la cedula No.
                         <input id="TxtCedula" type="number" style="width:150px;" class="validate"/> de  <input id="TxtCiudadCedula" type="text" style="width:100px;" 
                         class="validate"/>declaro conocer los estatutos y reglamentos que rigen a FECEM por lo cual solicito mi afiliación de forma libre
                        y voluntaria, autorizando al pagador de <input id="TxtAutorizacion" type="text" style="width:150px;" class="validate"/> para descontar de
                         mi salario y/ó pensión la suma de $ en forma mensual por concepto de aportes más ahorros como asociado al <strong>Fondo de Empleados de
                         <input id="TxtValor" type="number" style="width:100px;" class="validate"/>.</p>
                </div>                    
                <div class="col m6 s12">
                    <div class="input-field col m12">
                          <input id="TxtFirmaCodeudor" type="text" style="text-align:center;" class="validate"/>
                          <label for="TxtFirmaCodeudor" style="text-align:center;">Firma del Codeudor</label>
                    </div>
                    <div class="input-field col s12">
                          <input id="TxtCedulaSolicitante" type="number" style="text-align:center;" class="validate"/>
                          <label for="TxtCedulaSolicitante" style="text-align:center;">Cédula del solicitante</label>
                    </div>
                </div>
                <div class="col m6 s12">
                    <div class="input-field col m12">
                          <input id="TxtAprobado" type="text" style="text-align:center;" class="validate"/>
                          <label for="TxtAprobado" style="text-align:center;">Aprobado</label>
                    </div>
                    <div class="input-field col s12">
                          <input id="TxtActa" type="number" style="text-align:center;" class="validate"/>
                          <label for="TxtActa" style="text-align:center;">Acta</label>
                    </div>
                </div>
            </div>
        <div class="row" style="margin-top:1%; margin-bottom:1%;">
            <div class="col-md-12">
                <asp:Panel ID="Panel6" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                    Width="100%">
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col m12">
                <div class="input-field col s12">
                    <h4 style="color:#0099CC;">Información legal</h4>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col m12" style="margin-bottom:2%;">
                <div class="input-field col l12">
                    <p class="col m12" style="text-align:justify; font-size:0.9em; font-weight:500">
                        Autorizo la recepción de Información relacionada a las Actividades propias de FECEM. A través de los medios:
                    </p>
                </div>
                <div class="input-field col m3 s12">
                    <input type="checkbox" class="filled-in" id="TxtEmailPersonal"/>
                    <label for="TxtEmailPersonal">E-mail Personal</label>
                </div>
                <div class="input-field col m3 s12">
                    <input type="checkbox" class="filled-in" id="TxtEmailCorporativo"/>
                    <label for="TxtEmailCorporativo">E-mail corporativo</label>
                </div>
                <div class="input-field col m3 s12">
                    <input type="checkbox" class="filled-in" id="TxtCelularPersonalAutorizacion"/>
                    <label for="TxtCelularPersonalAutorizacion">Celular personal</label>
                </div>
                <div class="input-field col m3 s12">
                    <input type="checkbox" class="filled-in" id="TxtCelularCorporativoAutorizacion"/>
                    <label for="TxtCelularCorporativoAutorizacion">Celular Corporativo</label>
                </div>
            </div>
            <div class="input-field col l12">
                <p class="col m12" style="text-align:justify; font-size:1em;">
                <strong>Autorización para Consultar, Reportar y Compartir Información:</strong> Autorizo al <strong>FONDO DE EMPLEADOS DE CEMEX COLOMBIA
                 - FECEM,</strong> o a quien represente sus derechos u ostente en el futuro la calidad de acreedor, en forma permanente e irrevocable para 
                que con fines estadísticos y de información interbancaria o comercial; informe, consulte, reporte, procese o divulgue, a las Centrales de 
                Información y Riesgo, en especial a Transunión - CIFIN, todo lo referente a mi comportamiento como cliente en general, y en especial por 
                el nacimiento, modificación, extinción, de obligaciones por mí contraídas o que llegare a contraer con el <strong>FONDO DE EMPLEADOS DE 
                CEMEX COLOMBIA - FECEM,</strong> los saldos que a su favor resulten de todas las operaciones de crédito que bajo cualquier modalidad me 
                hubiesen otorgado o me otorguen en el futuro. Igualmente Autorizo al <strong>FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM,</strong> o a 
                quien represente sus derechos u ostente en el futuro la calidad de acreedor, en forma permanente e irrevocable para consultar ante la 
                Asociación Bancaria o frente a cualquier otra Central de Información, mi endeudamiento, la información Comercial disponible sobre el 
                cumplimiento o no de mis compromisos adquiridos, así como de su manejo. Lo anterior implica que la información reportada permanecerá 
                en la Base de Datos durante el tiempo que la misma Ley establezca, de acuerdo con el momento y las condiciones en que se efectúen el
                pago de las obligaciones. Bajo la gravedad de juramento manifiesto que todos los datos aquí consignados son ciertos, que la información 
                que adjunto es veraz y verificable, y autorizo su verificación ante cualquier persona natural o jurídica, privada o pública, sin limitación 
                alguna, desde ahora y mientras subsista alguna relación comercial con el de <strong>FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM,</strong> 
                o con cualquiera que represente sus derechos, y me comprometo a actualizar la información y/o documentación cuando el <strong>FONDO DE 
                EMPLEADOS DE CEMEX COLOMBIA - FECEM,</strong> lo requiera. En concordancia con la Ley 1266 de 2008 de Habeas Data. 
                </p><br />
                <div class="input-field col m6 s12">
                    <input id="TxtFirma" style="text-align:center;" type="text" class="validate"/>
                    <label for="TxtFirma" style="text-align:center;">Firma</label>
                </div>
                <div class="input-field col m6 s12">
                    <input id="TxtCedulaFirma" type="text" class="validate"/>
                    <label for="TxtCedulaFirma">Cedula</label>
                </div>
                <p class="col m12" style="text-align:justify; margin-bottom:3%;">
                    Los datos incluidos en este documento y/o formulario serán incorporados a una base de datos responsabilidad de <strong>FONDO DE 
                    EMPLEADOS DE CEMEX COLOMBIA FECEM,</strong> con finalidades legítimas, en cumplimiento de la Constitución y la Ley. Para fines 
                    administrativos, comerciales, de publicidad y contrato frente a los titulares de los mismos. Acorde a la <strong>Ley Estatutaria 1581 de                     2012</strong> para el tratamiento de datos personales.
                </p>
            </div>
        </div>
        <div class="row" style="margin-top:1%; margin-bottom:1%;">
            <div class="col-md-12">
                <asp:Panel ID="Panel7" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                    Width="100%">
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col m12">
                <div class="input-field col s12">
                    <h4 style="color:#0099CC;">Verificación de la información (Espacio de uso exclusivo de FECEM)</h4>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col m12" style="margin-bottom:2%;">
                <div class="input-field col m6 s12">
                    <input type="date" class="datepicker" id="TxtFechaFecem"/>
                    <label for="TxtFechaFecem">Fecha</label>
                </div>
                <div class="input-field col m6 s12">
                    <input type="number" class="validate" id="TxtHora"/>
                    <label for="TxtHora">Hora</label>
                </div>
            </div>
            <div class="col m12" style="margin-bottom:2%;">
                <div class="input-field col m6 s12">
                    <input type="text" class="validate" id="TxtNombreVerifica"/>
                    <label for="TxtNombreVerifica">Nombre de quien verifica</label>
                </div>
                <div class="input-field col m6 s12">
                    <input type="text" class="validate" id="TxtCargoVerifica"/>
                    <label for="TxtCargoVerifica">Cargo de quien verifica</label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col m12" style="margin-bottom:2%;">
                <div class="input-field col m6 s12" style="text-align:center;">
                    <input type="text" style="text-align:center;" class="validate" id="TxtFirmaVerifica"/>
                    <label for="TxtFirmaVerifica" style="text-align:center;">Firma</label>
                </div>
                <div class="input-field col m6 s12" style="text-align:center;">
                    <input type="number" class="validate" id="TxtCedulaFirmaVerifica"/>
                    <label for="TxtCedulaFirmaVerifica">Cedula</label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col m12" style="margin-bottom:2%;">
                <div class="input-field col m12" style="text-align:center;">
                    <textarea id="TxtObservaciones" class="materialize-textarea"></textarea>
                    <label for="TxtObservaciones" style="text-align:center;">Observaciones</label>
                </div>
            </div>
        </div>
    </div>
    <div class="container-full" style="margin-top:1%; margin-bottom:0%;">
        <div class="col-md-12">
            <asp:Panel ID="Panel1" runat="server" Style="background-color: #0099CC; color: #fff; height:50px; text-align: center;"
                Width="100%">
            </asp:Panel>
        </div>
    </div>
    </form>
        

    <script src="../../../Scripts/materialize.min.js"></script>
        <script src="../../../Scripts/VolverArriba.js"></script>
        <script>
            $('.datepicker').pickadate({
                selectMonths: true, // Creates a dropdown to control month
                selectYears: 100 // Creates a dropdown of 15 years to control year
            });
            $(document).ready(function () {
                $('select').material_select();
            });
        </script>
</body>
</html>
