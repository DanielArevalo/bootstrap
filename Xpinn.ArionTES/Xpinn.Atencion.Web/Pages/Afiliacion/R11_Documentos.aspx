<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R11_Documentos.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
    <style>
.wrapper {
  margin: 0 auto;
  padding: 40px;
  max-width: 95%;
}

.table_noms {
  margin: 0 0 40px 0;
  width: 100%;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
  display: table;
}
@media screen and (max-width: 580px) {
  .table_noms {
    display: block;
  }
}

.row_noms {
  display: table-row;
  background: #f6f6f6;
}
.row_noms:nth-of-type(odd) {
  background: #e9e9e9;
}
.row_noms.header {
  font-weight: 900;
  color: #ffffff;
  background: #ea6153;
}
.row_noms.green {
  background: #27ae60;
}
.row_noms.blue {
  background: #2980b9;
}
@media screen and (max-width: 580px) {
  .row_noms {
    padding: 14px 0 7px;
    display: block;
  }
  .row_noms.header {
    padding: 0;
    height: 6px;
  }
  .row_noms.header .cell {
    display: none;
  }
  .row_noms .cell {
    margin-bottom: 10px;
  }
  .row_noms .cell:before {
    margin-bottom: 3px;
    content: attr(data-title);
    min-width: 98px;
    font-size: 10px;
    line-height: 10px;
    font-weight: bold;
    text-transform: uppercase;
    color: #969696;
    display: block;
  }
}

.cell {
  padding: 6px 12px;
  display: table-cell;
}
@media screen and (max-width: 580px) {
  .cell {
    padding: 2px 16px;
    display: block;
  }
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:Panel runat="server" ID="pnlData">    
    <%-- SECCION DOCUMENTOS --%>
                        <div onclick="ocultarMostrarPanel('documentos')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">14. Confirmar<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="documentos">
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
                            <br />
                                <asp:Panel runat="server" ID="dataFormulario" Visible="false">
                                    <div class="col-sm-12" style="margin-bottom: 20px;">
                                    <div class="col-lg-12 col-md-12 col-xs-12">
                                        <div class="row mensaje">
                                            <div class="col-sm-12 col-md-9">
                                                “Una vez diligencie el formulario de afiliación descárguelo, imprímalo, fírmelo, ponga huella y entréguelo junto a la fotocopia de su documento de identidad (2 caras) y formato MetLife a su comercial, teniendo en cuenta la zona en que esté ubicado/a.”
                                            </div>
                                            <div class="col-sm-12 col-md-3 centrar">
                                                <a id="download" class="btn btn-default navbar-btn" style="background-color: white;" href="./../../files/formato_Metlife.pdf" download="formato_Metlife.pdf">Descargar formato MetLife</a>
                                                <br />
                                                <br />
                                                <a id="doc_afilia" runat="server" class="btn btn-default navbar-btn" style="background-color: white;" download="formato_afiliacion.pdf">formulario de afiliación</a>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    </div>
                                </asp:Panel>                        
                                <asp:Panel runat="server" ID="dataOtros" Visible="true">
                                    <div class="col-sm-12" style="margin-bottom: 20px;">
                                    <div class="col-lg-12 col-md-12 col-xs-12">
                                        <div class="row mensaje">
                                            <div class="col-sm-12 col-md-9">
                                                “Confirmación de afiliación”
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </asp:Panel>                        
                            <h5 class="centrar"></h5>                                                                                                                       
                            <div class="col-xs-12" style="background-color: rgba(230,230,250,.5);">
                                <div class="col-lg-1">
                                </div>
                                <div style="width: 88%; margin: auto; margin-top: 27px">                                    
                                    <div class="col-xs-12">
                                        <p style="margin-top: 36px; text-align: justify; font-size: small;">
                                            <strong>Autorización Tratamiento de Datos Personales</strong><br /><br />
                                            Declaro que autorizo a <strong runat="server" id="ent"></strong> para  la recolección y tratamiento de mis datos personales, conforme a la política de datos definida por la entidad y que es de mi conocimiento,  entiendo que los datos serán objeto de recolección, almacenamiento, uso, circulación, supresión, transferencia, transmisión, cesión y todo el tratamiento, con fines contractuales, comerciales, de atención al cliente y marketing.<br />
                                            Declaro que se me ha informado que como Titular de la información tengo derecho a conocer, actualizar y rectificar mis datos personales, solicitar prueba de la autorización otorgada para su tratamiento y  revocar el consentimiento otorgado para el tratamiento de datos personales en forma gratuita.     Esto lo puedo realizar a través de los canales dispuestos por la entidad para este fin y  observando las Políticas y Procedimientos de Protección de Datos Personales disponibles en la pagina web de la entidad, declaro que conozco que es voluntario responder preguntas que eventualmente me sean hechas sobre datos sensibles o datos de menores de edad, sin perder de vista que éstos serán tratados respetando los derechos fundamentales e intereses superiores. Autorizo  efectuar las gestiones pertinentes para llevar a cabo el procedimiento de asociación y colocación de los productos del portafolio de la Entidad Solidaria, respecto de cualquiera de los productos o servicios que pueda adquirir o proveer, en mi relación negocial con ésta, así como dar cumplimiento a la ley colombiana o extranjera y las órdenes de autoridades judiciales o administrativas; autorizo realizar invitaciones a eventos, mejorar productos y servicios u ofertar nuevos productos, y todas aquellas actividades asociadas a la relación comercial o vínculo existente  o aquel que llegare a tener;  Autorizo gestionar trámites como solicitudes, quejas, reclamos, sugerencias y comentarios positivos; realización de análisis de riesgos, estudios de perfil del asociado y de mercadeo, encuestas de satisfacción respecto de los productos y servicios ofrecidos por la entidad solidaria o empresas vinculadas  a través de convenios y aliados comerciales. La información de la presente autorización, la he suministrado de forma voluntaria y es verídica.
                                        </p>
                                    </div>
                                    <input type="checkbox" name="acept" required="required"> Aceptar<br/>
                                    <br />
                                </div>
                                <div class="col-lg-1">
                                </div>
                            </div>
                        </div>
                        <%-- FIN SECCION DOCUMENTOS --%>
                    <div class="col-xs-12">
                        <div id="dvCaptcha"  style="width: 88%; margin: auto; margin-top: 27px">
                        </div>
                        <div  style="width: 88%; margin: auto; margin-top: 27px">
                            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                        </div>
                        <%--<asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="The CAPTCHA field is required." ControlToValidate="txtCaptcha" style="font-size:x-small; font-weight:600"
                                runat="server" ForeColor="Red" Display="Dynamic" />--%>
                    </div>
    <br />
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnVolver" CssClass="btn btn-danger" Style="padding: 3px 15px; width: 110px" runat="server" Text="volver" OnClick="btnVolver_Click" />
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
        </asp:Panel>    
     <%-- PANEL FINAL --%>
        <asp:Panel ID="panelFinal" runat="server" Visible="false">
            <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div style="width: 88%; margin: auto; margin-top: 27px">
                    <div class="col-xs-12">
                        <asp:Label ID="Label2" runat="server" Text="" Style="color: #66757f; font-size: 28px; padding: 0px 200px 0px 0px;" />                        
                    </div>
                    <div class="col-xs-12">
                        <p style="margin-top: 36px;font-size: x-large;">
                            Su solicitud de afiliación se generó correctamente con el código: 
                            <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red; font-size: x-large;" />
                        </p>
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
        </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="infoAsesores" Visible="false">                                
                                <div class="wrapper">
                                  <div class="table_noms">    
                                    <div class="row_noms header blue">
                                      <div class="cell">
                                        Nombre
                                      </div>
                                      <div class="cell">
                                        Zona
                                      </div>
                                      <div class="cell">
                                        Celular
                                      </div>
                                      <div class="cell">
                                        Email
                                      </div>
                                    </div>
                                    <div class="row_noms">
                                      <div class="cell" data-title="Nombre"><strong>ALEXANDRA MURILLO</strong></div>
                                      <div class="cell" data-title="Zona">Corporativo</div>
                                      <div class="cell" data-title="Celular">3112200221</div>
                                      <div class="cell" data-title="Email">alexandra.murillo@ext.cemex.com</div>
                                    </div> 

                                    <div class="row_noms">
                                      <div class="cell" data-title="Nombre"><strong>ERIKA BARRAGÁNO</strong></div>
                                      <div class="cell" data-title="Zona">Bogotá plantas norte y sur</div>
                                      <div class="cell" data-title="Celular">3112209232</div>
                                      <div class="cell" data-title="Email">erika.barraganc@ext.cemex.com</div>
                                    </div> 

                                    <div class="row_noms">
                                      <div class="cell" data-title="Nombre"><strong>ANDREA PERÉZ</strong></div>
                                      <div class="cell" data-title="Zona">La Calera</div>
                                      <div class="cell" data-title="Celular">3183482018</div>
                                      <div class="cell" data-title="Email">johana.perez@ext.cemex.com</div>
                                    </div>
                                      			
                                    <div class="row_noms">
                                      <div class="cell" data-title="Nombre"><strong>PATRICIA CARTAGENA</strong></div>
                                      <div class="cell" data-title="Zona">Ibagué y Caracolito</div>
                                      <div class="cell" data-title="Celular">3112209631</div>
                                      <div class="cell" data-title="Email">gloriapatricia.cartagenag@ext.cemex.com</div>
                                    </div>

                                    <div class="row_noms">
                                      <div class="cell" data-title="Nombre"><strong>NATALIA TOBÓN</strong></div>
                                      <div class="cell" data-title="Zona">Bucaramanga, Cúcuta y Costa</div>
                                      <div class="cell" data-title="Celular">3112203391</div>
                                      <div class="cell" data-title="Email">natalia.tobondecastro@ext.cemex.com</div>
                                    </div>

                                    <div class="row_noms">
                                      <div class="cell" data-title="Nombre"><strong>MANUELA ZAPATA</strong></div>
                                      <div class="cell" data-title="Zona">Maceo, Medellín, Pereira, Cali y Tuluá</div>
                                      <div class="cell" data-title="Celular">3168785069</div>
                                      <div class="cell" data-title="Email">manuelaandrea.zapatamartinez@ext.cemex.com</div>
                                    </div>                                    			
                                </div>
                                    </div>
                            </asp:Panel>    
        <%-- FIN PANEL FINAL --%>
        <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />

    <script type="text/javascript" src="//code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="//www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(".datepicker").datepicker({
            maxDate: "0d"
        });

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
</asp:Content>

