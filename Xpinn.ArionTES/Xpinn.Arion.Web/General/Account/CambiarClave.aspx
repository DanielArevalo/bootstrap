<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CambiarClave.aspx.cs" Inherits="General_Account_CambiarClave" %>
<%@ Register Src="../Controles/header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE HTML>
<html >
<head runat="server">
    <title>Expinn Technology</title>
<link rel="stylesheet" href="../../Styles/Styles.css"/>
    <link rel="stylesheet" href="../../Styles/StyleHeader.css"/>
        <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--ESTILOS BOOTSTRAP-->
    <link rel="stylesheet" href="../../Styles/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>



<script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("img.rollover").hover(
            function () {
                this.src = this.src.replace("_off", "_on");
            },
            function () {
                this.src = this.src.replace("_on", "_off");
            });
    });
</script>
<style type="text/css">
    .cssClass1
    {
    background: Red;
    color:White;
    font-weight:bold;
    font-size:x-small;
    }
    .cssClass2
    {
    background: Gray;
    color:White;
    font-weight:bold;
    font-size:x-small;
    }
    .cssClass3
    {
    background: orange;
    color:black;
    font-weight:bold;
    font-size:x-small;
    }
    .cssClass4

    {
    background: blue;
    color:White;
    font-weight:bold;
    font-size:x-small;
    }
    .cssClass5

    {
    background: Green;
    color:White;
    font-weight:bold;
    font-size:x-small;
    }
    .BarBorder
    {
    border-style: solid;
    border-width: 1px;
    width: 180px;
    padding:2px;
    }

    .contentModulos{
            text-align: center;
    }
</style>


<script type="text/javascript" language="javascript">
    function getPasswordStrengthState() {
        if ($find("PasswordStrength1")._getPasswordStrength() > 50) {
            $get("<%=btnCambiarClave.ClientID%>").style.display = 'error';
        }
    }
</script>

</head>
<body class="modulo">
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" />
    <div class="container">
        <div class="utilitario">
            <asp:Panel runat="server" ID="pnlVolver">
                <img alt="Utilitarios" src="../../Images/iconUtilitarios.jpg" valign="bottom" />
                <asp:LinkButton ID="hlkModulos" runat="server" onclick="hlkModulos_Click">Volver a Modulos</asp:LinkButton>
            </asp:Panel>
        </div>
    </div>
    <div class="clear">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </div>
    <div class="contentModulos">
        <!--validacion contraseña-->
        <asp:TextBox ID="txtMayuscula" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox"  Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCaracter" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <!--fin validacion contraseña-->
        <h1><span style="color: #FFFFFF; font-weight: bold">CAMBIAR CONTRASE&Ntilde;A</span></h1><br />
        <asp:Label ID="txtClaveActual" runat="server" AssociatedControlID="CurrentPassword">Contrase&ntilde;a Actual:</asp:Label>&nbsp;
        <asp:RequiredFieldValidator
            ID="rfvClaveActual" runat="server" ErrorMessage="Campo Requerido" 
                Display="Dynamic" ControlToValidate="CurrentPassword" 
                ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" 
            Font-Bold="True"></asp:RequiredFieldValidator>
            <br />
        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
           
        <br />
        <asp:Label ID="txtNuevaClave" runat="server" AssociatedControlID="NewPassword">Nueva Contrase&ntilde;a:</asp:Label>&nbsp;
        <asp:RequiredFieldValidator
            ID="rfvNewPassword" runat="server" ErrorMessage="Campo Requerido" 
                Display="Dynamic" ControlToValidate="NewPassword" 
                ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" 
            Font-Bold="True"></asp:RequiredFieldValidator>
            <br />
        <asp:TextBox ID="NewPassword" runat="server" CssClass="textbox" 
            TextMode="Password" ValidationGroup="vgClave"></asp:TextBox>
       
        <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="NewPassword" DisplayPosition="RightSide"
            StrengthIndicatorType="Text" PreferredPasswordLength="6" PrefixText="Nivel:"
            TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2" MinimumSymbolCharacters="1"
            RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
            TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
            CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" 
                    MinimumUpperCaseCharacters="1">
        </asp:PasswordStrength>
        
        <br />
        <asp:Label ID="txtConfNuevaClave" runat="server" AssociatedControlID="ConfirmNewPassword">Confirmar Nueva Contrase&ntilde;a:</asp:Label>&nbsp;
        <asp:RequiredFieldValidator
            ID="rfvConfirmPassword" runat="server" ErrorMessage="Campo Requerido" 
            Display="Dynamic" ControlToValidate="ConfirmNewPassword" 
            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" 
            Font-Bold="True">
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="NewPassword" 
            ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Las claves no son iguales" 
            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" Font-Bold="True">
        </asp:CompareValidator>
            <br />
        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="textbox" TextMode="Password" ValidationGroup="vgClave"></asp:TextBox>
         
         <asp:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="ConfirmNewPassword" DisplayPosition="RightSide"
            StrengthIndicatorType="Text" PreferredPasswordLength="6" PrefixText="Nivel:"
            TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2" MinimumSymbolCharacters="1"
            RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
            TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
            CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1">
        </asp:PasswordStrength>
        
        <br />
        <br />
        <br />
        <div class="fl-rt">
        </div>
            <asp:Button ID="btnCambiarClave" runat="server" Text="Cambiar Contrase&ntilde;a" 
                ValidationGroup="vgClave" onclick="btnCambiarClave_Click" 
                CssClass="cambiarClave"/>
        <br />
        <br />
        <div class="clear">
            <asp:Label ID="lblInfo"  runat="server" ForeColor="Red" Visible="False" 
                Font-Bold="True"></asp:Label>
            <br />
            <br />
            <asp:TextBox runat="server" ID="lblInfoV" TextMode="MultiLine" style="background-color: transparent;border: none;width: 100%;text-align: center; height: -webkit-fill-available;">
            </asp:TextBox>            
        </div>
        <br />
        <br />
     </div>
    <footer>
    ©2014 Expinn Technology
    </footer>
    </form>
</body>
    <script>
        document.getElementById("NewPassword").addEventListener("change", validatePassword);


        function validatePassword() {
            //variables
            var s = "\'\\'";
            var msg = "";
            var valRegex = "^";

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
            var val = $("#NewPassword").val();
            console.log(val);
            console.log(valRegex);

            if (re.test(val)) {
                console.log('entro');
                $("#btnCambiarClave").removeAttr('disabled');
                $("#lblInfoV").text("");
            } else {
                console.log('NO entro');
                $("#lblInfoV").text("La contraseña debe tener al menos:" + "\n" + msg);
                $("#btnCambiarClave").attr('disabled', 'disabled');
            }
        }
    </script>
</html>
