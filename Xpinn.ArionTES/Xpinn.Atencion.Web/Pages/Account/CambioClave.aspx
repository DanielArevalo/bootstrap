<%@ Page Title=".: Cambiar Contraseña :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CambioClave.aspx.cs" Inherits="CambioClave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    
     <asp:TextBox ID="txtMayuscula" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox"  Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCaracter" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">            
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-6 text-left">
                        Contraseña Actual :
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" required />
                        <asp:RequiredFieldValidator ID="rfvClaveActual" runat="server" ErrorMessage="Campo Requerido"
                            Style="font-size: x-small" Display="Dynamic" ControlToValidate="txtPassword"
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" Font-Bold="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-6 text-left">
                        Nueva Contrase&ntilde;a:
                        <br />
                        <asp:TextBox ID="txtPasswordNew" runat="server" CssClass="form-control" TextMode="Password" required />
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Campo Requerido"
                            Style="font-size: x-small" Display="Dynamic" ControlToValidate="txtPasswordNew"
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" Font-Bold="True">
                        </asp:RequiredFieldValidator>
                        <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtPasswordNew"
                            DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="6"
                            PrefixText="Nivel:" TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2"
                            MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
                            TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                            CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1">
                        </asp:PasswordStrength>
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-6 text-left">
                        Vuelva a escribir la Contrase&ntilde;a:
                        <br />
                        <asp:TextBox ID="txtConfirmaPassword" runat="server" CssClass="form-control" TextMode="Password" required/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Requerido"
                            Style="font-size: x-small" Display="Dynamic" ControlToValidate="txtConfirmaPassword"
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" Font-Bold="True">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPasswordNew"
                            Style="font-size: x-small" ControlToValidate="txtConfirmaPassword" Display="Dynamic"
                            ErrorMessage="Las claves no son iguales" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgClave" Font-Bold="True">
                        </asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                            Operator="NotEqual" Style="font-size: x-small" ControlToValidate="txtPasswordNew"
                            Display="Dynamic" ErrorMessage="Las clave ingresada no puede ser igual a la anterior."
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgClave" Font-Bold="True">
                        </asp:CompareValidator>
                        <asp:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="txtConfirmaPassword"
                            DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="6"
                            PrefixText="Nivel:" TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2"
                            MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
                            TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                            CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1">
                        </asp:PasswordStrength>
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
            </div>
            <br />
            <br />
             <div class="form-group">
                 <div class="col-sm-12" style="display: flex;align-items: center;justify-content: center; padding-top:15px;">
                    <div class="col-xs-2">
                        <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-success col-xs-12"
                            Style="padding: 5px" Text="Continuar" OnClick="btnCambiarClave_Click" />
                        <asp:Label ID="lblEmailCooperativa" runat="server" Visible="false" />
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default col-xs-12"
                            Style="padding: 5px" Text="Cancelar" OnClick="btnCancelar_Click" />
                    </div>
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
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-6 text-center">
                        &nbsp;
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    &nbsp;
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
             <div class="form-group col-lg-12 text-center">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <strong><asp:Label ID="Label2" runat="server" Text="Se modifico correctamente la clave." style="color:Red"/></strong>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />                        
                    </div>
        </asp:View>
    </asp:MultiView>    
    <script type="text/javascript">
        document.getElementById("ContentPlaceHolder1_txtPasswordNew").addEventListener("change", validatePassword);
        document.getElementById("ContentPlaceHolder1_txtConfirmaPassword").addEventListener("change", validatePassword);

        function validatePassword() {
            //variables
            var s = "\'\\'";
            var msg = "";
            var valRegex = "^(?=.*[a-z])";
            document.getElementById('ContentPlaceHolder1_btnRegistrar').disabled = true;
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
            var val = document.getElementById("ContentPlaceHolder1_txtPasswordNew").value;
            var con = document.getElementById("ContentPlaceHolder1_txtConfirmaPassword").value;
            console.log(val);
            console.log(valRegex);

            if (re.test(val)) {
                console.log('entro');
                if (val === con) {
                    document.getElementById("ContentPlaceHolder1_btnRegistrar").disabled = false;
                    document.getElementById("ContentPlaceHolder1_lblInfoV").value = "";
                } else {
                    document.getElementById("ContentPlaceHolder1_btnRegistrar").disabled = true;
                    document.getElementById("ContentPlaceHolder1_lblInfoV").value = "La contraseñas no son iguales";
                }
            } else {
                console.log('NO entro');
                document.getElementById("ContentPlaceHolder1_btnRegistrar").disabled = true;
                document.getElementById("ContentPlaceHolder1_lblInfoV").value = "La contraseña debe tener al menos:" + "\n" + msg;
            }
        }
    </script>
</asp:Content>

