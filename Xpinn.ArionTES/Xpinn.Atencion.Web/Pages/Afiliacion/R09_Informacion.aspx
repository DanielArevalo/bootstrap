<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R09_Informacion.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <%-- SECCION ENTREVISTAS --%>
                        <div onclick="ocultarMostrarPanel('entrevista')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">7. Informaci&oacute;n entrevista (espacio exclusivo para la entidad)<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="entrevista">
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtLugar">Lugar de la entrevista</label>
                                    <asp:TextBox runat="server" id="txtLugar" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtFechaEntrevista">Fecha realización entrevista</label>
                                    <asp:TextBox ID="TxtFechaEntrevista" ReadOnly="true" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtHora">Hora</label>
                                    <asp:TextBox runat="server" ReadOnly="true" id="txtHora" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Resultado</label>
                                    <asp:RadioButtonList ID="cbResultadoEntrevista" Enabled="false" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160Aprobado&#160</asp:ListItem>
                                        <asp:ListItem Value="0" style="margin-top: 2%;">&#160Rechazado</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <label for="txtObservaciones">Observaciones</label>
                                    <textarea id="txtObservaciones" readonly="readonly" class="form-control"></textarea>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <label for="txtNombreRealizoEntrevista">Nombre y firma ejecutivo(a) comercial</label>
                                    <asp:TextBox ID="txtNombreRealizoEntrevista" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION ENTREVISTAS --%>


                        <%-- SECCION VERIFICACION --%>
                        <div onclick="ocultarMostrarPanel('verificacion')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">8. Verificaci&oacute;n de la informaci&oacute;n (espacio exclusivo para la entidad)<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="verificacion">
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtNombreVerificacion">Nombre del oficial de cumplimiento</label>
                                    <asp:TextBox ID="txtNombreVerificacion" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtFechaVerificacion">Fecha</label>
                                    <asp:TextBox ID="TxtFechaVerificacion" runat="server" ReadOnly="true" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtHoraVerifica">Hora</label>
                                    <asp:TextBox runat="server" id="txtHoraVerifica" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Resultado</label>
                                    <asp:RadioButtonList ID="cbResultadoVerifica" Enabled="false" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160Aprobado&#160</asp:ListItem>
                                        <asp:ListItem Value="0" style="margin-top: 2%;">&#160Rechazado</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <label for="txtObservacionesVerifica">Observaciones</label>
                                    <textarea id="txtObservacionesVerifica" readonly="readonly" class="form-control"></textarea>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <label for="txFirmaVerificacion">Nombre y firma oficial de cumplimiento</label>
                                    <asp:TextBox ID="txtOficialCumplimiento" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION VERIFICACION --%>

                        <%-- SECCION CONSULTAS --%>
                        <div onclick="ocultarMostrarPanel('consultas')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">9. Consultas y validaciones (espacio exclusivo para la entidad)<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="consultas">
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-4">
                                    <label>¿Se realizaron consultas y validaciones en la lista ONU y la lista OFAC?</label>
                                    <asp:RadioButtonList ID="rbRelizoConsultas" Enabled="false" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160Si&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Value="0" style="margin-top: 2%;">&#160No&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <label>Fecha de consulta</label>
                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtFechaConsulta" class="datepicker form-control" ClientIDMode="Static" type="text"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <label>Persona responsable de la consulta</label>
                                    <asp:TextBox ID="txtResponsableConsulta" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION CONSULTAS --%>

    <%-- SECCION DECLARACION --%>
                        <div onclick="ocultarMostrarPanel('declaracion')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">11. Declaraci&oacute;n de descuento <span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="declaracion">
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    Bajo la gravedad de juramento y actuando en nombre propio realizo la siguiente declaración de origen y destinación de recursos a Fecem, con el fin de cumplir con las disposiciones señaladas en su Sistema de Administración del Riesgo de Lavado de Activos y de la Financiación del Terrorismo:<br />
                                    1. Declaro que los activos, ingresos, bienes y demás recursos provienen de actividades legales conforme a lo descrito en mi actividad y ocupación.<br />
                                    2. No admitiré que terceros vinculen mi actividad con dineros, recursos o activos relacionadas con el delito de lavado de activos o destinados a la financiación del terrorismo.<br />
                                    3. Eximo a FECEM, de toda responsabilidad que se derive del comportamiento o el que se ocasione por la información falsa ó errónea suministrada en la presente declaración y en los documentos que respaldan o soporten mis afirmaciones.<br />
                                    4. Autorizo a FECEM, para que verifique y realice las consultas que estime necesarias con el propósito de confirmar la información registrada en este formulario.<br />
                                    5. Los recursos que utilizo para realizar los pagos e inversiones en FECEM tienen procedencia lícita y están soportados con el desarrollo de actividades legítimas.<br />
                                    6. No he sido, ni me encuentro incluido en investigaciones relacionadas con Lavado de Activos o Financiación del Terrorismo.<br />
                                    7. Estoy informado de mi obligación de actualizar anualmente la información que solicite la entidad por cada producto o servicio que utilice, suministrando la información documental exigida por FECEM para dar cumplimiento a la normatividad vigente.<br />
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION DECLARACION --%>


                        <%-- SECCION AUTORIZA COMPARTIR --%>
                        <div onclick="ocultarMostrarPanel('autoriza')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">12. Autorizaci&oacute;n para consultar, reportar y compartir informaci&oacute;n <span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="autoriza">
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    Autorizo al FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM, o a quien represente sus derechos
                                    u ostente en el futuro la calidad de acreedor, en forma permanente e irrevocable para que con fines
                                    estadísticos y de información interbancaria o comercial; informe, consulte, reporte, procese o divulgue,
                                    a las Centrales de Información y Riesgo, todo lo referente a mi comportamiento como cliente en
                                    general, y en especial por el nacimiento, modificación, extinción, de obligaciones por mí contraídas o
                                    que llegare a contraer con FECEM, los saldos que a su favor resulten de todas las operaciones de crédito
                                    que bajo cualquier modalidad me hubiesen otorgado o me otorguen en el futuro. Igualmente Autorizo
                                    a FECEM, o a quien represente sus derechos u ostente en el futuro la calidad de acreedor, en forma
                                    permanente e irrevocable para consultar ante la Asociación Bancaria o frente a cualquier otra Central
                                    de Información, mi endeudamiento, la información Comercial disponible sobre el cumplimiento o no
                                    de mis compromisos adquiridos, así como de su manejo. Lo anterior implica que la información
                                    reportada permanecerá en la Base de Datos durante el tiempo que la misma Ley establezca, de acuerdo
                                    con el momento y las condiciones en que se efectúen el pago de las obligaciones. Bajo la gravedad de
                                    juramento manifiesto que todos los datos aquí consignados son ciertos, que la información que adjunto
                                    es veraz y verificable, y autorizo su verificación ante cualquier persona natural o jurídica, privada o
                                    pública, sin limitación alguna, desde ahora y mientras subsista alguna relación comercial con FECEM, o
                                    con cualquiera que represente sus derechos, y me comprometo a actualizar la información y/o
                                    documentación cuando FECEM, lo requiera. En concordancia con la Ley 1266 de 2008 de Habeas Data.
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION AUTORIZA COMPARTIR --%>


                        <%-- SECCION AUTORIZA COMPARTIR --%>
                        <div onclick="ocultarMostrarPanel('proteccion')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">13. Protecci&oacute;n de datos <span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="proteccion">
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    De acuerdo con la Ley Estatutaria 1581 de 2012 de Protección de Datos y con el Decreto 1377 de 2013,
                                    autorizo expresamente, como Titular de los datos, que éstos sean recolectados, transferidos, usados,
                                    suprimidos, compartidos, actualizados y transmitidos, bajo la responsabilidad del FONDO DE
                                    EMPLEADOS DE CEMEX COLOMBIA -FECEM, siendo tratados los datos personales, datos sensibles
                                    (Huella dactilar, imágenes, video y estados médicos), con la finalidad de (1) validar la información en
                                    cumplimiento de la exigencia legal de conocimiento del cliente aplicable al FONDO DE EMPLEADOS DE
                                    CEMEX COLOMBIA - FECEM, (2) adelantar las acciones de cobro y de recuperación de cartera, en virtud
                                    del Objeto social de la organización y que sean incorporados en distintas bases o bancos de datos, o en
                                    repositorios electrónicos de todo tipo con que cuenta la entidad. Esta información es y será utilizada
                                    en el desarrollo de las funciones propias del FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM,
                                    en su condición de entidad sin ánimo de lucro que presta los servicios de ahorro y crédito, así como
                                    para fines administrativos, comerciales, de publicidad y contrato frente a los titulares de los mismos. El
                                    alcance de la autorización comprende la facultad para que el FONDO DE EMPLEADOS DE CEMEX
                                    COLOMBIA - FECEM, le envíe mensajes con contenidos institucionales, notificaciones, información del
                                    estado de cuenta, saldos, cuotas pendientes de pago y demás información relativa al portafolio de
                                    servicios de la Entidad, a través de correo electrónico y/o mensajes de texto/chat al teléfono móvil, de
                                    acuerdo con las medidas de seguridad definidas en la política de tratamiento desarrollada por el FONDO
                                    DE EMPLEADOS DE CEMEX COLOMBIA - FECEM, a la cual se puede tener acceso mediante la página
                                    web www.fecem.com. De igual modo, declaro haber sido informado de que puedo ejercitar los
                                    derechos de acceso, corrección, supresión, revocación o reclamo por infracción sobre mis datos,
                                    mediante escrito dirigido al FONDO DE EMPLEADOS DE CEMEX COLOMBIA - FECEM a la dirección de
                                    correo electrónico fecem.colombia@cemex.com, indicando en el asunto el derecho que desea
                                    ejercitar, o mediante documento escrito radicado o enviado por correo físico a la dirección; Calle 98 #
                                    14-17, Piso 2 Oficina 201 en Bogotá.
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION AUTORIZA COMPARTIR --%>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R08_Autorizacion.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

