<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestablecerPassword.aspx.cs" Inherits="RestablecerPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.: Cambiar Contraseña :.</title>
    <link href="~/Css/bootstrap.css" rel="stylesheet" type="text/css" />
     <link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico" />
     <style type="text/css">
        .cssClass1
        {
            background: Red;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }
        .cssClass2
        {
            background: Gray;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }
        .cssClass3
        {
            background: orange;
            color: black;
            font-weight: bold;
            font-size: x-small;
        }
        .cssClass4
        {
            background: blue;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }
        .cssClass5
        {
            background: Green;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }
        .BarBorder
        {
            border-style: solid;
            border-width: 1px;
            width: 180px;
            padding: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="form-group">
        <div class="container">
            <div class="col-lg-12">
                <div class="col-lg-2 col-md-1">
                </div>
                <div class="col-lg-8 col-md-10 col-xs-12" style="padding-top: 8px;">
                    <div class="col-xs-12">
                        <div class="col-xs-12">
                            <asp:Image ID="imgEmpresa" runat="server" ImageUrl="~/Imagenes/LogoEmpresa.jpg" Width="85px" />
                            &nbsp;<asp:Label ID="lbltitulo" runat="server" Text="Restablecimiento de contraseña"
                                CssClass="text-primary" Style="font-size: 16px" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-1">
                </div>
            </div>
        </div>
        <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />
        <div class="container">
                <div class="col-lg-12">
                    <div class="col-lg-2 col-md-1">
                    </div>
                    <div class="col-lg-8 col-md-10 col-xs-12" style="padding-top: 8px;">
                        <div class="col-xs-12">
                            <asp:Label ID="lblError" runat="server" Style="text-align: left; font-size: small"
                                Visible="False" Width="100%" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-1">
                    </div>
                </div>
            </div>
            <asp:Label ID="lblcod_persona" runat="server" Visible="false" />
            <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwInfo1" runat="server">
                    <div class="container">
                        <div class="col-lg-12">
                            <div class="col-lg-2 col-md-1">
                            </div>
                            <div class="col-lg-8 col-md-10 col-xs-12">
                                <div class="col-xs-12" style="margin-top: 27px">
                                    <asp:Label ID="Label1" runat="server" Text="Restablece tu Contraseña"
                                        Style="color: #66757f; font-size: 28px;" />
                                </div>
                                <div class="col-xs-12">
                                    <p style="margin-top: 36px">
                                        Las Contraseñas fuertes incluyen números, letras y signos</p>
                                </div>
                                <div class="col-xs-12">
                                    <strong>Escribe tu Contraseña nueva</strong><br />
                                    <asp:TextBox ID="txtPasswordNew" namme="txtPasswordNew" runat="server" CssClass="form-control" Width="60%" TextMode="Password"/>
                                    <asp:RequiredFieldValidator ID="rfvcont" runat="server" ControlToValidate="txtPasswordNew"
                                        Display="Dynamic" ErrorMessage="Campo requerido."
                                        ForeColor="Red" SetFocusOnError="True" Style="font-size: x-small" ValidationGroup="vgGuardar" />
                                    <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtPasswordNew"
                                        DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="6"
                                        PrefixText="Nivel:" TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2"
                                        MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
                                        TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                                        CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1"></asp:PasswordStrength>
                                </div>
                                <div class="col-xs-12">
                                    <br />
                                </div>
                                <div class="col-xs-12">
                                    <strong>Escribe tu Contraseña nueva una vez más</strong><br />
                                    <asp:TextBox ID="txtConfirmaPassword" runat="server" CssClass="form-control" Width="60%" TextMode="Password"/>
                                    <asp:RequiredFieldValidator ID="rfvcontConf" runat="server" ControlToValidate="txtPasswordNew"
                                        Display="Dynamic" ErrorMessage="Campo requerido." ForeColor="Red" SetFocusOnError="True"
                                        Style="font-size: x-small" ValidationGroup="vgGuardar" />
                                    <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPasswordNew"
                                        Style="font-size: x-small" ControlToValidate="txtConfirmaPassword" Display="Dynamic"
                                        ErrorMessage="Las claves no son iguales" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="vgGuardar" Font-Bold="True">
                                    </asp:CompareValidator>
                                    <asp:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="txtPasswordNew"
                                        DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="6"
                                        PrefixText="Nivel:" TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2"
                                        MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
                                        TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                                        CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1">
                                    </asp:PasswordStrength>
                                </div>                                

                                <div class="col-xs-12">
                                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar" Width="150px" CssClass="btn btn-primary"
                                        Style="padding-bottom: 8px; padding-top: 8px; margin-top: 20px; border-radius: 3px"
                                        ValidationGroup="vgGuardar" onclick="btnEnviar_Click"/>
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
                            <div class="col-lg-2 col-md-1">
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwInfo2" runat="server">
                    <div class="container">
                        <div class="col-lg-12">
                            <div class="col-lg-2 col-md-1">
                            </div>
                            <div class="col-lg-8 col-md-10 col-xs-12">
                                <div class="col-xs-12" style="margin-top: 27px">
                                    <asp:Label ID="lblSub" runat="server" Text=" "
                                        Style="color: #66757f; font-size: 28px;" />
                                </div>
                                <div class="col-xs-12">
                                    <p style="margin-top: 36px">
                                        Su contraseña se restableció con éxito, haz clic en el enlace de abajo y vuelva a ingresar con los datos modificados.</p>
                                    <br />
                                </div>
                                <div class="col-xs-12">
                                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" Width="150px" CssClass="btn btn-primary"
                                        
                                        Style="padding-bottom: 8px; padding-top: 8px; margin-top: 20px; border-radius: 3px" 
                                        onclick="btnIngresar_Click" />
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-1">
                            </div>
                        </div>
                    </div>
                </asp:View>
                </asp:MultiView>
        <asp:TextBox ID="txtMayuscula" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox"  Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCaracter" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
    </form>

    <script type="text/javascript">
        document.getElementById("txtPasswordNew").addEventListener("change", validatePassword);
        document.getElementById("txtConfirmaPassword").addEventListener("change", validatePassword);        

        function validatePassword() {
            //variables
            var s = "\'\\'";
            var msg = "";
            var valRegex = "^(?=.*[a-z])";
            document.getElementById('btnEnviar').disabled = true;
            //obtiene restricciones de la contraseña
            var m =  "<%=txtMayuscula.Text%>";
            var n =  "<%=txtNumero.Text%>";
            var c = "<%=txtCaracter.Text%>";
            var l = <%=txtLongitud.Text%>;

            //Valida restricciones
            if (m == "True") { valRegex += "(?=.*[A-Z])"; msg += "- una mayuscula" + "\n"; }
            if (n == "True") { valRegex += "(?=.*[0-9])"; msg += "- un numero" + "\n"; }
            if (c == "True") { valRegex += "(?=.*[$@$!%*?&])"; msg += "- un caracter especial" + "\n"; }
            if (l != 0) { valRegex += "{" + l + ",}"; msg += "-una longitud de " + l + " caracteres" + "\n"; }

            var re = new RegExp(valRegex);
            var val = document.getElementById("txtPasswordNew").value;
            console.log(val);
            console.log(valRegex);

            if (re.test(val)) {
                console.log('entro');
                document.getElementById("btnEnviar").disabled = false;
                    document.getElementById("lblInfoV").value = "";
            } else {
                console.log('NO entro');
                document.getElementById("btnEnviar").disabled = true;
                document.getElementById("lblInfoV").value = "La contraseña debe tener al menos:" + "\n" + msg;
            }            
        }
    </script>
</body>
</html>
