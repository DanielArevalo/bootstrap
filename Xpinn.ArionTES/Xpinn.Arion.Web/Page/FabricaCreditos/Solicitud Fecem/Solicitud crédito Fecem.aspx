<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Solicitud crédito Fecem.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_Fecem_Solicitud_crédito_Fecem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Styles/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/materialize.css" rel="stylesheet" type="text/css" />
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
                        <asp:Label ID="LblTitulo" runat="server" CssClass="text-primary" Style="font-size: 2.5em; color:#0099CC;" Text="Solicitud de Crédito"></asp:Label>
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
                        <h4 style="color:#0099CC;">1. Datos del solicitante</h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtApellidos" type="text" class="validate"/>
                        <label for="TxtApellidos">Apellidos</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtNombres" type="text" class="validate"/>
                        <label for="TxtNombres">Nombres</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCodigoDecsis" type="number" class="validate"/>
                        <label for="TxtCodigoDecsis">Código Decsis</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtIdentificacion" type="number" class="validate"/>
                        <label for="TxtIdentificacion">N° de Identificación</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCargo" type="text" class="validate"/>
                        <label for="TxtCargo">Cargo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtUbicacion" type="text" class="validate"/>
                        <label for="TxtUbicacion">Ubicación</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtSueldoBasico" type="number" class="validate"/>
                        <label for="TxtSueldoBasico">Sueldo básico</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtFechaNacimiento" type="date" class="datepicker"/>
                        <label for="TxtFechaNacimiento">Fecha de nacimiento</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtPersonasCargo" type="text" class="validate"/>
                        <label for="TxtPersonasCargo">Personas a cargo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtEstadoCivil" type="text" class="validate"/>
                        <label for="TxtEstadoCivil">Estado civil</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtDireccionResidencia" type="text" class="validate"/>
                        <label for="TxtDireccionResidencia">Direccion de residencia</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtTelefonoFijo" type="number" class="validate"/>
                        <label for="TxtTelefonoFijo">Telefono fijo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtTelefonoOfi" type="number" class="validate"/>
                        <label for="TxtTelefonoOfi">Telefono oficina celular</label>
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
                        <h4 style="color:#0099CC;">2. Datos del codeudor</h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtApellidosCod" type="text" class="validate"/>
                        <label for="TxtApellidosCod">Apellidos</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtNombresCod" type="text" class="validate"/>
                        <label for="TxtNombresCod">Nombres</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCodigoDecsisCod" type="number" class="validate"/>
                        <label for="TxtCodigoDecsisCod">Código Decsis</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtIdentificacionCod" type="number" class="validate"/>
                        <label for="TxtIdentificacionCod">N° de Identificación</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCargoCod" type="text" class="validate"/>
                        <label for="TxtCargoCod">Cargo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtUbicacionCod" type="text" class="validate"/>
                        <label for="TxtUbicacionCod">Ubicación</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtSueldoBasicoCod" type="number" class="validate"/>
                        <label for="TxtSueldoBasicoCod">Sueldo básico</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtFechaNacimientoCod" type="date" class="datepicker"/>
                        <label for="TxtFechaNacimientoCod">Fecha de nacimiento</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtPersonasCargoCod" type="text" class="validate"/>
                        <label for="TxtPersonasCargoCod">Personas a cargo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtEstadoCivilCod" type="text" class="validate"/>
                        <label for="TxtEstadoCivilCod">Estado civil</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtDireccionResidenciaCod" type="text" class="validate"/>
                        <label for="TxtDireccionResidenciaCod">Direccion de residencia</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtTelefonoFijoCod" type="number" class="validate"/>
                        <label for="TxtTelefonoFijoCod">Telefono fijo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtTelefonoOfiCod" type="number" class="validate"/>
                        <label for="TxtTelefonoOfiCod">Telefono oficina celular</label>
                    </div>
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
                        <h4 style="color:#0099CC;">3. Datos de la solicitud</h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtLineaCredito" type="text" class="validate"/>
                        <label for="TxtLineaCredito">Línea de crédito</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtValor" type="text" class="validate"/>
                        <label for="TxtValor">Valor</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtPlazo" type="number" class="validate"/>
                        <label for="TxtPlazo">Plazo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="TxtCuentaGiro">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Si</option>
                          <option value="2">No</option>
                        </select>
                        <label for="TxtCuentaGiro">Giro a cuenta de nómina del Asociado</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCuenta" type="number" class="validate"/>
                        <label for="TxtCuenta"> ó girar en cuenta N°</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <select id="TxtTipoCuenta">
                          <option value="" disabled selected>Seleccione una opción</option>
                          <option value="1">Ahorros</option>
                          <option value="2">Corriente</option>
                        </select>
                        <label for="TxtTipoCuenta">Tipo de cuenta</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtBanco" type="text" class="validate"/>
                        <label for="TxtBanco">del banco</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtNombreDe" type="text" class="validate"/>
                        <label for="TxtNombreDe">A nombre de</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtCedulaONit" type="number" class="validate"/>
                        <label for="TxtCedulaONit">Cédula ó NIT</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtNombreCheque" type="text" class="validate"/>
                        <label for="TxtNombreCheque">Ó cheque a nombre de</label>
                    </div>
                    <div class="input-field col l6 m8 s12">
                         <textarea id="TxtObservaciones" class="materialize-textarea"></textarea>
                         <label for="TxtObservaciones">Observaciones</label>
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
                        <h4 style="color:#0099CC;">4. Autorización para la cosulta y reporte a las centrales de riesgo</h4>
                    </div>
                </div>
                <div class="col m12" style="margin-bottom:2%;">
                    <p style="text-align:justify; font-size:0.9em;">Autorizo(amos) al Fondo de Empleados de Cemex Colombia “FECEM”, o a quien represente sus derechos u ostente 
                        en el futuro la calidad de acreedor, en forma permanente e irrevocable para que con fines estadísticos y de 
                        información interbancaria o comercial informe, reporte, procese o divulgue, a las Centrales de Información y 
                        Riesgo, en especial a la CIFIN que administra la Asociación Bancaria, todo lo referente a mi(nuestro) 
                        comportamiento como cliente(s) en general, y en especial por el nacimiento, modificación, extinción de obligaciones
                         por mí(nosotros) contraídas ó que llegare a contraer con el Fondo de Empleados de Cemex Colombia “FECEM”, los 
                        saldos que a su favor resulten de todas las operaciones de crédito que bajo cualquier modalidad me(nos) hubiesen 
                        otorgado ó me(nos) otorguen en el futuro. Igualmente autorizo(amos) al Fondo de Empleados de Cemex Colombia “FECEM”,
                         o a quien represente sus derechos u ostente en el futuro la calidad de acreedor, en forma permanente e irrevocable 
                        para consultar ante la Asociación Bancaria ó frente a cualquier otra Central de Información, mi(nuestro) endeudamiento,
                         la información comercial disponible sobre el cumplimiento ó no de mis(nuestros) compromisos adquiridos, así como de 
                        su manejo.  Lo anterior implica que la información reportada permanecerá en la base de datos durante el tiempo que la 
                        misma ley establezca, de acuerdo con el momento y las condiciones en que se efectúen el pago de las obligaciones. 
                        Bajo la gravedad del juramento manifiesto(amos) que todos los datos aquí consignados con ciertos, que la información 
                        que adjunto(amos) es veraz y verificable, y autorizo(amos) su verificación ante cualquier persona natural o jurídica,
                         privada ó pública, sin limitación alguna, desde ahora y mientras subsista alguna relación comercial con el Fondo de 
                        Empleados de Cemex Colombia “FECEM” ó con quien represente sus derechos, y me(nos) comprometo(s) a actualizar la 
                        información y/o documentación cuando el Fondo de Empleados de Cemex Colombia “FECEM” lo requiera</p>
                </div>
                <div class="col m12">
                    <div class="input-field col l6"/>
                      <input id="TxtFirmaSolicitante" type="text" style="text-align:center;" class="validate">
                      <label for="TxtFirmaSolicitante" style="text-align:center;">Firma del solicitante</label>
                    </div>
                    <div class="input-field col l6"/>
                      <input id="TxtFirmaCodeudor" type="text" style="text-align:center;" class="validate">
                      <label for="TxtFirmaCodeudor" style="text-align:center;">Firma del Codeudor</label>
                    </div>
                    <div class="input-field col l6"/>
                      <input id="TxtCedulaSolicitante" type="number" style="text-align:center;" class="validate">
                      <label for="TxtCedulaSolicitante" style="text-align:center;">Cédula del solicitante</label>
                    </div>
                    <div class="input-field col l6"/>
                      <input id="TxtCedulaCodeudor" type="number" style="text-align:center;" class="validate">
                      <label for="TxtCedulaCodeudor" style="text-align:center;">Cédula del solicitante</label>
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
                        <h4 style="color:#0099CC;">5. Aprobación de gerencia</h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtFecha" type="date" class="datepicker"/>
                        <label for="TxtFecha">Fecha</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtValorAprobado" type="number" class="validate"/>
                        <label for="TxtValorAprobado">Valor aprobado</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtPlazoAprobado" type="text" class="validate"/>
                        <label for="TxtPlazoAprobado">Plazo</label>
                    </div>
                    <div class="input-field col l3 m4 s6">
                        <input id="TxtRechazadoPor" type="text" class="validate"/>
                        <label for="TxtRechazadoPor">Rechazado por</label>
                    </div>
                    <div class="input-field col l6"/>
                      <input id="TxtFirmaGerencia" type="text" style="text-align:center;" class="validate">
                      <label for="TxtFirmaGerencia" style="text-align:center;">Firma de Gerencia</label>
                    </div>
                    <div class="input-field col l6 m8 s12">
                         <textarea id="TxtObservacionesGerencia" class="materialize-textarea"></textarea>
                         <label for="TxtObservacionesGerencia">Observaciones</label>
                    </div>
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
                <div class="col m12" style="margin-bottom:2%;">
                    <div class="input-field col l12">
                        <div class="input-field col l3 m3 s6">
                          <input id="TxtCiudad" type="text" class="validate"/>
                          <label for="TxtCiudad">Ciudad</label>
                        </div>
                    </div>
                    <div class="input-field col l12">
                        <div class="input-field col l3 m3 s6">
                          <input id="TxtFechaDoc" type="text" class="validate"/>
                          <label for="TxtFechaDoc">Fecha</label>
                        </div>
                    </div>
                    <div class="input-field col l12">
                    <p class="col m12" style="text-align:justify; font-size:0.9em; font-weight:500">
                        Señores FONDO DE EMPLEADOS DE CEMEX COLOMBIA – FECEM -  E. S. M.
                    </p>
                    <p class="col m12">Ref.: Autorización e Instrucciones para llenar un pagaré (título valor) de contra-garantía firmado con
                         espacios en blanco.</p>
                    <p class="col m12">Yo (nosotros):</p>
                    <div class="input-field col l5 m5 s10">
                      <input id="TxtNosotros" type="text" class="validate"/>
                      <label for="TxtNosotros">Nombres</label>
                    </div>
                    <p class="col m12" style="text-align:justify;">
                        Identificados como aparece al pie de mi (nuestras) firma(s) y actuando en  Por medio de este documento y en los 
                        términos del artículo 622 del Código de Comercio autorizo (autorizamos) a ustedes en forma permanente e irrevocable
                         para llenar el documento pagaré que he (hemos) otorgado a su favor, para que el mismo, una vez llenado, constituya
                         un pagaré a la orden otorgado a su favor.  El documento pagaré tiene espacios en blanco que podrán ser llenados sin
                         previo aviso, de acuerdo con las siguientes instrucciones:
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">1.</strong> En el espacio reservado para el nombre de las personas a quienes deba hacerse 
                        el pago se indicará FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM - con la cual haya (hayamos) contraído cualquiera de las 
                        obligaciones a que se refiere el numeral 2 de la presente carta.
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">2.</strong> En los espacios reservados para un valor en pesos se indicará el monto de 
                        cualquier suma insoluta, presente o futura, que para la fecha en que sea llenado el pagaré, llegare a deber al FONDO DE 
                        EMPLEADOS DE CEMEX COLOMBIA - FECEM - directa o indirectamente y en forma conjunta o separada, el firmante o cualquiera 
                        de los firmantes del pagaré, por cualquier concepto tales como, deudas por concepto de préstamos en dinero, servicios, 
                        comisiones, impuesto de timbre y sus sanciones, intereses causados y no pagados, sanciones por devolución de cheques por 
                        causales imputables al librador, anticipos y en general cualquier obligación que aparezca registrada en los libros o conste 
                        en los archivos del FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM -. 
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">3.</strong> La fecha de vencimiento del pagaré será el día en que el pagaré sea llenado 
                        por el FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM -.
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">4.</strong> La fecha de emisión del pagaré será el día en que el pagaré sea llenado por 
                        el FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM -
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">5.</strong> El Acreedor queda facultado para dar válidamente por vencidos los plazos 
                        concedidos para el pago de mí (nuestras) obligaciones y para proceder a cobrar la totalidad de las obligaciones por vía 
                        judicial o extrajudicial, previo el diligenciamiento del pagaré de acuerdo con las presentes instrucciones, en cualquiera 
                        de los siguientes casos:  
                    </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(a)</strong> Cuando cualquiera de los firmantes del pagaré no pague en todo o en 
                            parte y en la oportunidad debida, cualquiera de las obligaciones de que trata el numeral 2 de la presente carta; 
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(b)</strong> Por mora o incumplimiento en el pago de cualquiera de las cuotas pactadas 
                            para amortizar el capital o pago de los intereses; 
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(c)</strong> Cuando sean embargados o perseguidos bienes de cualquiera de los firmantes 
                            del pagaré, por cualquier persona en ejercicio de cualquier clase de acción;
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(d)</strong> Cuando cualquiera de los firmantes del pagaré solicite ser admitido a la 
                            celebración de concordato con sus acreedores, acuerdo de reestructuración o cuando se inicie respecto de los firmantes 
                            del pagaré trámite de liquidación obligatoria, o cuando cualquiera de los firmantes del pagaré se disuelva por cualquier 
                            causa o inicie diligencias tendientes a su liquidación por cualquier causa;   
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(e)</strong> Cuando se produzca la muerte de cualquiera de los firmantes del pagaré.  
                            En este caso el Acreedor tendrá el derecho de exigir la totalidad del crédito a uno cualquiera de los herederos, sin 
                            necesidad de demandarlos a todos; 
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(f)</strong> Cuando las garantías constituidas a favor de la FONDO DE EMPLEADOS DE 
                            CEMEX COLOMBIA - FECEM - desaparecieren total o parcialmente, por cualquier causa, o cuando su valor, a juicios del 
                            Acreedor garantizado disminuyere en forma considerable, de modo que dejen de ser suficientes, o cuando los bienes sobre 
                            los cuales se constituyeron dichas garantías sean gravados o enajenados en todo o en parte y a cualquier título sin el 
                            consentimiento del Acreedor garantizado; 
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(g)</strong> Cuando cualquiera de los firmantes de los pagarés no suministre al Acreedor 
                            la información o documentación que se le solicite para establecer o actualizar su situación financiera o si cometiere 
                            inexactitudes en los estados financieros, informes, declaraciones o documentos que presente;   
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(h)</strong> Mala o difícil situación económica del deudor o uno cualquiera de los 
                            deudores calificada por el Acreedor;    
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(i)</strong> Si me (nos) retiro (retiramos) como afiliado (s) de FECEMo, dejo (dejamos) 
                            de ser empleado (s) por cualquier causa de la Empresa, en estos eventos FECEM podrá cubrir el importe de este título 
                            con los ahorros existentes a mi favor, y /o con el producto de los pagos que me  (nos) haga la Empresa, por concepto 
                            de liquidacion final de prestaciones sociales, incluidos salarios, prestacions sociales, indemnizacion y cualquier 
                            otra acreencia laboral;   
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(j)</strong> Cuando el destino del crédito sea diferente al tipo de obligación solicitada
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">(k)</strong> Cuando el crédito no tenga respaldo de un codeudor o si lo tuvo, y no ha 
                            sido reemplazado por otro deudor solidario dentro del mes siguiente
                        </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">6.</strong>El lugar de cumplimiento de las obligaciones cambiarias será el de cualquiera
                         de las oficinas del Acreedor en que el firmante (firmantes) haya (hayamos) contraído cualquiera de las deudas de que trata
                         el numeral 2 de la presente carta o el lugar de la oficina que se haya hecho cargo de sus negocios.
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">7.</strong> Autorizo(autorizamos) al pagador de la Empresa 
                        <input id="Empresa" type="text" style="width:250px;" class="validate"/>  a descontar el valor de las cuotas mensuales, 
                        que incluye capital, intereses corrientes, intereses de mora y demás gastos en que se incurra, por concepto del crédito 
                        solicitado hasta cancelar la deuda correspondiente, conociendo de antemano las condiciones del préstamo
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        <strong style="font-weight:700;">8.</strong> En el evento de ser aprobado el préstamo, el desembolso se hará de la siguiente manera: 
                    </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">a</strong>La suma de $
                            <input id="TxtValorDoc" type="number" style="width:200px;" class="validate"/> en los  
                            <input id="Txtdias" type="number" style="width:30px;" class="validate"/> primeros días de cada mes iniciando en el mes de  
                            <input id="TxtMes" type="text" style="width:150px;" class="validate"/>, los cuales se descontarán en la nómina del mismo mes 
                            y así sucesivamente hasta completar el monto aprobado. 
                        </p>
                        <p class="col m12" style="text-align:justify; margin-left:3%;">
                            <strong style="font-weight:700;">b</strong> Estos desembolsos y este documento tendrá validez  mientras subsista el contrato
                             de trabajo  con su empleador, pues desaparecido el vinculo laboral el Fondo no podrá hacer más desembolsos del saldo del 
                            préstamo. 
                        </p>
                    <p class="col m12" style="text-align:justify;">
                        Acompaño (acompañamos) el documento pagaré en referencia debidamente firmado y declaro (declaramos) haber recibido copia de 
                        la presente carta de instrucciones. 
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        Atentamente, 
                    </p>
                    <div class="input-field col l4 m4 s12">
                        <div class="input-field col l12">
                          <input id="TxtFirmaDoc1" type="text" class="validate">
                          <label for="TxtFirmaDoc1">Firma</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtNombreDoc1" type="text" class="validate">
                          <label for="TxtNombreDoc1">Nombre</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtccDoc1" type="number" style="text-align:center;" class="validate">
                          <label for="TxtccDoc1">C.c.</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtDeDoc1" type="text" style="text-align:center;" class="validate">
                          <label for="TxtDeDoc1">De:</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtCiudadDoc1" type="text" class="validate">
                          <label for="TxtCiudadDoc1">Ciudad actual</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtTelefonoDoc1" type="number" class="validate">
                          <label for="TxtTelefonoDoc1">Telefono actual</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtDirecciónDoc1" type="text" class="validate">
                          <label for="TxtDirecciónDoc1">Dirección actual</label>
                        </div>
                    </div>
                    <div class="input-field col l4 m4 s12">
                        <div class="input-field col l12">
                          <input id="TxtFirmaDoc2" type="text" class="validate">
                          <label for="TxtFirmaDoc2">Firma</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtNombreDoc2" type="text" class="validate">
                          <label for="TxtNombreDoc2">Nombre</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtccDoc2" type="number" style="text-align:center;" class="validate">
                          <label for="TxtccDoc2">C.c.</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtDeDoc2" type="text" style="text-align:center;" class="validate">
                          <label for="TxtDeDoc2">De:</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtCiudadDoc2" type="text" class="validate">
                          <label for="TxtCiudadDoc2">Ciudad actual</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtTelefonoDoc2" type="number" class="validate">
                          <label for="TxtTelefonoDoc2">Telefono actual</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtDirecciónDoc2" type="text" class="validate">
                          <label for="TxtDirecciónDoc2">Dirección actual</label>
                        </div>
                    </div>
                    <div class="input-field col l4 m4 s12">
                        <div class="input-field col l12"/>
                          <input id="TxtFirmaDoc3" type="text" class="validate">
                          <label for="TxtFirmaDoc3">Firma</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtNombreDoc3" type="text" class="validate">
                          <label for="TxtNombreDoc3">Nombre</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtccDoc3" type="number" style="text-align:center;" class="validate">
                          <label for="TxtccDoc3">C.c.</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtDeDoc3" type="text" style="text-align:center;" class="validate">
                          <label for="TxtDeDoc3">De:</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtCiudadDoc3" type="text" class="validate">
                          <label for="TxtCiudadDoc3">Ciudad actual</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtTelefonoDoc3" type="number" class="validate">
                          <label for="TxtTelefonoDoc3">Telefono actual</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtDirecciónDoc3" type="text" class="validate">
                          <label for="TxtDirecciónDoc3">Dirección actual</label>
                        </div>
                    </div>
                </div>
            </div>
            </div>
             
            <div class="row" style="margin-top:1%; margin-bottom:1%;">
                <div class="col-md-12">
                    <asp:Panel ID="Panel8" runat="server" Style="background-color: #0099CC; color: #fff; height:10px; text-align: center;"
                        Width="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="row" style="margin-bottom:5%;">
                <div class="col m12">
                    <div class="input-field col s12">
                        <h4 style="color:#0099CC;">Pagare</h4>
                    </div>
                </div>
                <div class="col m12">
                    <div class="input-field col l12">
                        <div class="input-field col l3 m3 s6">
                          <input id="TxtNumeroPagare" type="text" class="validate"/>
                          <label for="TxtNumeroPagare">Numero</label>
                        </div>
                        <div class="input-field col l3 m3 s6">
                          <input id="TxtValorPagare" type="text" class="validate"/>
                          <label for="TxtValorPagare">Valor</label>
                        </div>
                    </div>
                </div>
                <div class="input-field col l12">
                    <p class="col m12" style="text-align:justify; font-size:0.9em; font-weight:500">
                        Yo (nosotros):</p>
                    <div class="input-field col l5 m5 s10">
                      <input id="TxtNosotrosPagare" type="text" class="validate"/>
                      <label for="TxtNosotrosPagare">Nombres</label>
                    </div>
                    <p class="col m12" style="text-align:justify;">
                        Me (nos) obligo (obligamos) incondicionalmente y de manera solidaria a pagar en dinero 
                        efectivo al FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM - o a su orden, o a quien represente 
                        sus derechos, en la ciudad de <input id="TxtCiudadPagare" type="number" style="width:150px;" class="validate"/>,
                         el día <input id="TxtDiaPagare" type="number" style="width:30px;" class="validate"/> del mes 
                        <input id="TxtMesPagare" type="text" style="width:100px;" class="validate"/> del 
                        año <input id="TxtAñoPagare" type="number" style="width:50px;" class="validate"/>, la suma 
                        de <input id="TxtValorPagareLetra" type="text" style="width:200px;" class="validate"/>($ 
                        <input id="TxtValorPagareNumero" type="number" style="width:100px;" class="validate"/>).
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        A partir de la fecha de este título y sin perjuicio de las acciones legales del tenedor encaminadas a su cobro,
                         sobre el saldo de capital pendiente de pago que causarán intereses de mora a la tasa de interés de mora más alta
                         permitida por la ley.  Todos los gastos e impuestos que cause este título-valor, así como los honorarios del abogado 
                        y las costas del cobro, son de mi (nuestro) cargo. 
                    </p>
                    <p class="col m12" style="text-align:justify;">
                        El Acreedor está autorizado para debitar de cualquier suma a mi (nuestro) favor el importe total o parcial de este 
                        título-valor, por vía de compensación.  Renuncio (renunciamos) en forma expresa, a favor del Acreedor, los derechos 
                        de nombrar secuestre de bienes en caso de cobro judicial y de solicitar que los bienes embargados se dividan en lotes 
                        para la subasta pública.  Para todos los efectos legales, reconozco (reconocemos) que la obligación contraída tiene 
                        carácter de indivisible.  Hago (hacemos) constar que la solidaridad y la indivisibilidad subsisten en caso de prorroga 
                        o de cualquier modificación de lo estipulado, aunque se pacte con uno solo de los deudores.  Queda excusado el protesto 
                        de este pagaré. 
                    </p>
                    <p class="col m12" style="text-align:justify;">Se emite este pagaré a los 
                        <input id="TxtDiaPagareEmite" type="number" style="width:30px;" class="validate"/> días del mes de  
                        <input id="TxtMesPagareEmite" type="text" style="width:100px;" class="validate"/> del 
                        año <input id="TxtAñoPagareEmite" type="number" style="width:50px;" class="validate"/> y se entrega con la intención de 
                        hacerlo negociable
                </div>
                <div class="col m12">
                    <div class="input-field col l4 m4 s12">
                        <div class="input-field col l12"/>
                          <input id="TxtFirmaPagare1" type="text" class="validate"/>
                          <label for="TxtFirmaPagare1">Firma</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtNombrePagare1" type="text" class="validate"/>
                          <label for="TxtNombrePagare1">Nombre</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtccPagare1" type="number" style="text-align:center;" class="validate"/>
                          <label for="TxtccPagare1">C.c.</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtDePagare1" type="text" style="text-align:center;" class="validate"/>
                          <label for="TxtDePagare1">De:</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtCiudadPagare1" type="text" class="validate"/>
                          <label for="TxtCiudadPagare1">Ciudad actual</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtTelefonoPagare1" type="number" class="validate"/>
                          <label for="TxtTelefonoPagare1">Telefono actual</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtDirecciónPagare1" type="text" class="validate"/>
                          <label for="TxtDirecciónPagare1">Dirección actual</label>
                        </div>
                    </div>
                    <div class="input-field col l4 m4 s12">
                        <div class="input-field col l12"/>
                          <input id="TxtFirmaPagare2" type="text" class="validate"/>
                          <label for="TxtFirmaPagare2">Firma</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtNombrePagare2" type="text" class="validate"/>
                          <label for="TxtNombrePagare2">Nombre</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtccPagare2" type="number" style="text-align:center;" class="validate"/>
                          <label for="TxtccPagare2">C.c.</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtDePagare2" type="text" style="text-align:center;" class="validate"/>
                          <label for="TxtDePagare2">De:</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtCiudadPagare2" type="text" class="validate"/>
                          <label for="TxtCiudadPagare2">Ciudad actual</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtTelefonoPagare2" type="number" class="validate"/>
                          <label for="TxtTelefonoPagare2">Telefono actual</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtDirecciónPagare2" type="text" class="validate"/>
                          <label for="TxtDirecciónPagare2">Dirección actual</label>
                        </div>
                    </div>
                    <div class="input-field col l4 m4 s12">
                        <div class="input-field col l12"/>
                          <input id="TxtFirmaPagare3" type="text" class="validate"/>
                          <label for="TxtFirmaPagare3">Firma</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtNombrePagare3" type="text" class="validate"/>
                          <label for="TxtNombrePagare3">Nombre</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtccPagare3" type="number" style="text-align:center;" class="validate"/>
                          <label for="TxtccPagare3">C.c.</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtDePagare3" type="text" style="text-align:center;" class="validate"/>
                          <label for="TxtDePagare3">De:</label>
                        </div>
                        <div class="input-field col l7">
                          <input id="TxtCiudadPagare3" type="text" class="validate"/>
                          <label for="TxtCiudadPagare3">Ciudad actual</label>
                        </div>
                        <div class="input-field col l5">
                          <input id="TxtTelefonoPagare3" type="number" class="validate"/>
                          <label for="TxtTelefonoPagare3">Telefono actual</label>
                        </div>
                        <div class="input-field col l12">
                          <input id="TxtDirecciónPagare3" type="text" class="validate"/>
                          <label for="TxtDirecciónPagare3">Dirección actual</label>
                        </div>
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
