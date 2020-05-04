<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registro.aspx.cs" Inherits="Pages_Account_Registro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.: Registro de Usuario :.</title>
    <link href="~/Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/JaLoAdmin.min.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico" />
</head>
<body>

    <div class="container">
        <form role="form" class="form-horizontal" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <fieldset>
                <div class="col-lg-12">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-8">
                        <div class="form-group col-lg-12">
                            <br />
                            <br />
                            <asp:Image ID="imgEmpresa" runat="server" ImageUrl="~/Imagenes/LogoEmpresa.jpg" style="max-height:210px; max-width: 310px"/>
                            <br />
                            <asp:Label ID="lblError" runat="server" Style="text-align: left; font-size: small"
                                Visible="False" Width="100%" ForeColor="Red" />
                        </div>
                    </div>
                </div>
                <asp:MultiView ID="mtvPrincipal" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vwData" runat="server">
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-8">
                                <div class="form-group col-lg-12">
                                    <h3><strong>Regístrese</strong></h3>
                                    <br />
                                </div>
                                <div class="form-group col-md-12">
                                    <h5>Sí está afiliado con nosotros ingrese su identificación y regístrese</h5>
                                </div>

                                <asp:Panel ID="panelData" runat="server">
                                    <div class="form-group col-lg-12 text-primary">
                                        <h4>Datos básicos del Afiliado</h4>
                                        <asp:TextBox ID="txtCodPersona" runat="server"
                                            CssClass="form-control" Visible="false" />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-xs-2 text-left">
                                            <label>
                                                Identificación</label><br />
                                        </div>
                                        <div class="col-xs-4 text-left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-xs-6 text-left">
                                            &nbsp;
                                        <%--<asp:Button ID="btnConsultar" runat="server" CssClass="btn btn-primary col-xs-6"
                                            Style="padding: 3px; top: 0px; left: 0px;" Text="Consultar"
                                            OnClick="btnConsultar_Click" />--%>
                                        </div>
                                    </div>
                                    <%--<hr style="width: 100%" />--%>
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" Visible="false" />
                                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" Visible="false" />
                                    <asp:TextBox ID="txtFecNacimiento" runat="server" placeholder="alguien@example.com" CssClass="form-control" Visible="false" />
                                    <%--<div class="form-group">
                                        <div class="col-xs-12">
                                            <label>
                                                Fecha de Nacimiento</label>
                                        </div>
                                        <div class="col-xs-4 text-left">
                                            <asp:DropDownList ID="ddlDia" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-xs-4 text-left">
                                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-xs-4 text-left">
                                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>--%>
                                </asp:Panel>
                                <hr style="width: 100%" />
                                <asp:Panel ID="panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label>
                                                Contraseña</label><br />
                                            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" oncopy="return false;" onpaste="return false;" oncut="return false;" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label>
                                                Vuelva a escribir la contraseña</label><br />
                                            <asp:TextBox ID="txtConfirmaContra" runat="server" CssClass="form-control" TextMode="Password" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="form-group">
                                    <div class="col-xs-8">
                                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-success col-xs-12"
                                            Style="padding: 5px" Text="Continuar" OnClick="btnRegistrar_Click" />
                                        <asp:Label ID="lblEmailCooperativa" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default col-xs-12"
                                            Style="padding: 5px" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>
                                </div>
                                <div class="clear">
                                    <asp:Label ID="lblInfo"  runat="server" ForeColor="Red" Visible="False" 
                                        Font-Bold="True"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:TextBox runat="server" ID="lblInfoV" TextMode="MultiLine" style="background-color: transparent;border: none;width: 100%;text-align: center; height: 200px;" ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwSeguridad" runat="server">
                        <div class="form-group col-lg-12">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-8">
                                <div class="form-group col-lg-12">
                                    <h3><strong>Regístrese</strong></h3>
                                    <br />
                                </div>
                                <div class="form-group col-md-12">
                                     <asp:Label ID="lblPassword" runat="server" Style="text-align: left; font-size: small"
                                        Visible="False" Width="100%" />
                                    <h5>Antes de generar el registro, queremos asegurarnos de que es realmente una persona afiliada con nosotros.</h5>
                                </div>
                                <!--
                            <asp:Panel ID="panelExtra" runat="server">
                                <div class="form-group">
                                    <div class="col-md-12 text-left">
                                        <div class="col-md-4 text-left">
                                           Ingrese su número de celular
                                        </div>
                                        <div class="col-md-3 text-left">
                                            <asp:TextBox ID="txtCelular" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5">
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                                -->
                                <asp:Panel ID="panelCreditos" runat="server">
                                    <div class="form-group col-md-12 text-left">
                                        <asp:Label ID="lblTituloSeg" runat="server" CssClass="text-primary" Text="Seleccione dentro de las opciones algún número de Crédito que le pertenezca."></asp:Label>
                                        <hr style="width: 100%" class="text-primary" />
                                        <asp:RadioButtonList ID="rbPreguntas" runat="server" RepeatDirection="Vertical">
                                        </asp:RadioButtonList>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="panelOtros" runat="server">
                                    <div class="form-group col-md-12 text-left">
                                        <asp:Label ID="Label1" runat="server" class="text-primary" Text="Seleccione dentro de las opciones algún número de Aporte que le pertenezca."></asp:Label>
                                        <hr style="width: 100%" class="text-primary" />
                                        <asp:RadioButtonList ID="rbAportes" runat="server" RepeatDirection="Vertical">
                                        </asp:RadioButtonList>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="panelCaptcha" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 text-left">
                                            <div class="col-md-12 text-left">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-left">
                                            <h5>Ingrese correctamente la imagen que se encuntra en la parte superior.</h5>
                                        </div>
                                        <div class="col-md-12 text-left">
                                            <div class="col-md-3 text-left">
                                                <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 text-left">
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Imagenes/refresh.png" runat="server"
                                                    CausesValidation="false" />
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-left">
                                            <div class="col-md-12 text-left">
                                                <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Datos incorrectos, intentelo otra vez." Style="font-size: x-small"
                                                    ForeColor="Red" OnServerValidate="ValidateCaptcha" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="form-group col-md-12">
                                    <div class="col-xs-8">
                                        <asp:Button ID="btnGrabar" runat="server" CssClass="btn btn-success col-xs-12" Style="padding: 5px; top: -1px; left: 0px;"
                                            Text="Registrar" OnClick="btnGrabar_Click"  /><%--OnClick="btnGrabar_Click"--%>
                                    </div>
                                    <div class="col-xs-4">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="form-group col-md-12">
                                    <hr style="width: 100%" />
                                </div>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        <%--</div>--%>
                    </asp:View>
                    <asp:View ID="vwConfirmar" runat="server">
                        <div class="form-group col-lg-12">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-8">
                                <div class="form-group col-lg-12">
                                    <h3><strong>Regístrese</strong></h3>
                                    <br />
                                </div>
                                <div class="form-group col-md-12">
                                    <p><asp:Label ID="lblContenido" runat="server" /></p><br />
                                    <h5>Ingrese el código que se le envio a su correo para realizar el registro de su cuenta.&nbsp;</h5><br />
                                    <p>
                                        Si no ves el correo electrónico en tu bandeja de entrada, revisa otros lugares donde podría estar, como tus carpetas de correo no deseado, sociales u otras. 
                                    </p>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 text-left">
                                        <div class="col-md-4 text-left">
                                           <h3 style="margin:0">Ingrese el código</h3>
                                        </div>
                                        <div class="col-md-3 text-left">
                                            <asp:Label ID="lblAleatorio" runat="server" Visible="false"/>
                                            <asp:TextBox ID="txtCodigoConf" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-xs-8">
                                            <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-success col-xs-12" Style="padding: 5px; top: 0px; left: 0px;"
                                                Text="Confirmar Código" OnClick="btnConfirmar_Click" />
                                        </div>
                                        <div class="col-xs-4">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwFinal" runat="server">
                        <div class="form-group col-lg-12">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-8">
                                <div class="form-group col-lg-12">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="form-group col-lg-12 text-center">
                                    <asp:Label ID="lblMessageFinal" runat="server" Text=" " />
                                    <strong>
                                        <asp:Label ID="lblIdentificacion" runat="server" /></strong>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-default text-center" Style="padding: 5px; width: 130px"
                                        Text="Acceder" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </fieldset>

            <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />
        <asp:TextBox ID="txtMayuscula" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox"  Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCaracter" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        </form>
    </div>
    <script type="text/javascript">
        document.getElementById("txtContrasena").addEventListener("change", validatePassword);
        document.getElementById("txtConfirmaContra").addEventListener("change", validatePassword);                
        function validatePassword() {
            //variables
            var s = "\'\\'";
            var msg = "";
            var valRegex = "^(?=.*[a-z])";
            document.getElementById('btnRegistrar').disabled = true;
            //obtiene restricciones de la contraseña
            var m =  "<%=txtMayuscula.Text%>";
            var n =  "<%=txtNumero.Text%>";
            var c = "<%=txtCaracter.Text%>";
            var l = "<%=txtLongitud.Text%>";

            //Valida restricciones
            if (m == "True") { valRegex += "(?=.*[A-Z])"; msg += "- una mayuscula" + "\n"; }
            if (n == "True") { valRegex += "(?=.*[0-9])"; msg += "- un numero" + "\n"; }
            if (c == "True") { valRegex += "(?=.*[$@$!%*?&])"; msg += "- un caracter especial" + "\n"; }
            if (l != 0) { valRegex += "{" + l + ",}"; msg += "-una longitud de " + l + " caracteres alfanumericos" + "\n"; }
            console.log(valRegex);
            var re = new RegExp(valRegex);
            var val = document.getElementById("txtContrasena").value;
            console.log(val);
            console.log(valRegex);

            if (re.test(val)) {
                console.log('entro');
                document.getElementById("btnRegistrar").disabled = false;
                    document.getElementById("lblInfoV").value = "";
            } else {
                console.log('NO entro');
                document.getElementById("btnRegistrar").disabled = true;
                document.getElementById("lblInfoV").value = "La contraseña debe tener al menos:" + "\n" + msg;
            }            
        }
    </script>
</body>
</html>
