<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDireccion.ascx.cs" Inherits="ctlDireccion" %>

<script src="//code.jquery.com/jquery-1.8.0.min.js" type="text/javascript"></script>

<style type="text/css">
    .required:invalid {
        border: 1px solid #CC0000; /*ff6a00  F28C00*/
    }
    .requiredD:invalid{
        border: 1px solid red;
        padding: 4px 3px 0px;
    }
</style>
<table cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <asp:HiddenField ID="hfControlText" runat="server" />
            <asp:RadioButtonList ID="rbtnDetalleZonaGeo" runat="server" CssClass="componente required" Width="180px"
                RepeatDirection="Horizontal" Onchange="mensaje()">
                <asp:ListItem Value="R">Rural</asp:ListItem>
                <asp:ListItem Value="U">Urbana</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvtxtValorArriendo2" runat="server"
                ControlToValidate="txtDireccion" Display="Dynamic" ErrorMessage="Campo Requerido"
                ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"
                Style="font-size: small" />
            &nbsp;
        </td>
    </tr>
</table>
<table border="0">
    <tr>
        <td>

            <asp:Panel ID="pTipoDireccion" runat="server">
                <asp:DropDownList ID="ddlVia" runat="server"
                    CssClass="comboDireccionManzana" Width="80px">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="AU">Autopista</asp:ListItem>
                    <asp:ListItem Value="AV">Avenida</asp:ListItem>
                    <asp:ListItem Value="AC">Avenida Calle</asp:ListItem>
                    <asp:ListItem Value="AK">Avenida Carrera</asp:ListItem>
                    <asp:ListItem Value="BL">Bulevar</asp:ListItem>
                    <asp:ListItem Value="CL">Calle</asp:ListItem>
                    <asp:ListItem Value="KR">Carrera</asp:ListItem>
                    <asp:ListItem Value="CT">Carretera</asp:ListItem>
                    <asp:ListItem Value="CQ">Circular</asp:ListItem>
                    <asp:ListItem Value="CC">Cuentas Corridas</asp:ListItem>
                    <asp:ListItem Value="DG">Diagonal</asp:ListItem>
                    <asp:ListItem Value="PJ">Pasaje</asp:ListItem>
                    <asp:ListItem Value="PS">Paseo</asp:ListItem>
                    <asp:ListItem Value="PT">Peatonal</asp:ListItem>
                    <asp:ListItem Value="TV">Transversal</asp:ListItem>
                    <asp:ListItem Value="TC">Troncal</asp:ListItem>
                    <asp:ListItem Value="VT">Variante</asp:ListItem>
                    <asp:ListItem Value="VI">Vía</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlManzana" runat="server"
                    CssClass="comboDireccionManzana" Width="80px">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="MZ">Manzana</asp:ListItem>
                    <asp:ListItem Value="IN">Interior</asp:ListItem>
                    <asp:ListItem Value="SC">Sector</asp:ListItem>
                    <asp:ListItem Value="ET">Etapa</asp:ListItem>
                    <asp:ListItem Value="ED">Edificio</asp:ListItem>
                    <asp:ListItem Value="MD">Modulo</asp:ListItem>
                    <asp:ListItem Value="TO">Torre</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
        </td>
        <td>
            <asp:Panel ID="pVia" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNombreVia" runat="server" CssClass="cajaTextoGen" MaxLength="100" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTxtNombreVia" runat="server"
                                ControlToValidate="txtNombreVia" Display="Dynamic" Enabled="False"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLetra" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBis" runat="server" CssClass="comboBis">
                                <asp:ListItem Value=""> </asp:ListItem>
                                <asp:ListItem Value="BIS">BIS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLetra2" runat="server" CssClass="comboLetras" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSentido" runat="server" Width="80px">
                            </asp:DropDownList>
                        </td>
                        <td id="td1">#</td>
                        <td>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="cajaTextoNumero" MaxLength="3" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTxtNumero" runat="server"
                                ControlToValidate="txtNumero" Display="Dynamic" Enabled="False"
                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLetra3" runat="server" CssClass="comboLetras">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBis2" runat="server" CssClass="comboBis">
                                <asp:ListItem Value=""> </asp:ListItem>
                                <asp:ListItem Value="BIS">BIS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLetra4" runat="server" CssClass="comboLetras" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td id="td2">-</td>
                        <td>
                            <asp:TextBox ID="txtPlaca" runat="server" CssClass="cajaTextoNumero" MaxLength="3" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTxtPlaca" runat="server"
                                ControlToValidate="txtPlaca" Display="Dynamic" Enabled="False" ErrorMessage="*"
                                SetFocusOnError="true" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSentido2" runat="server" Width="80px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td>
            <asp:Panel ID="pManzana" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNombreManzana" runat="server" CssClass="cajaTextoGen" MaxLength="100" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTxtNombreManzana" SetFocusOnError="true" runat="server"
                                ErrorMessage="*" ControlToValidate="txtNombreManzana" Display="Dynamic" ValidationGroup="vgGuardar" Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="comboDireccionVia" Width="80px">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="AL">Altillo</asp:ListItem>
                                <asp:ListItem Value="AP">Apartamento</asp:ListItem>
                                <asp:ListItem Value="BG">Bodega</asp:ListItem>
                                <asp:ListItem Value="CS">Casa</asp:ListItem>
                                <asp:ListItem Value="CN">Consultorio</asp:ListItem>
                                <asp:ListItem Value="DP">Depósito</asp:ListItem>
                                <asp:ListItem Value="DS">Depósito Sótano</asp:ListItem>
                                <asp:ListItem Value="GA">Garaje</asp:ListItem>
                                <asp:ListItem Value="GS">Garaje Sótano</asp:ListItem>
                                <asp:ListItem Value="LC">Local</asp:ListItem>
                                <asp:ListItem Value="LM">Local Mezzanine</asp:ListItem>
                                <asp:ListItem Value="LT">Lote</asp:ListItem>
                                <asp:ListItem Value="MN">Mezzanine</asp:ListItem>
                                <asp:ListItem Value="OF">Oficina</asp:ListItem>
                                <asp:ListItem Value="PA">Parqueadero</asp:ListItem>
                                <asp:ListItem Value="PN">Pent-House</asp:ListItem>
                                <asp:ListItem Value="PL">Planta</asp:ListItem>
                                <asp:ListItem Value="PD">Predio</asp:ListItem>
                                <asp:ListItem Value="SS">Semisótano</asp:ListItem>
                                <asp:ListItem Value="SO">Sótano</asp:ListItem>
                                <asp:ListItem Value="ST">Suite</asp:ListItem>
                                <asp:ListItem Value="TZ">Terraza</asp:ListItem>
                                <asp:ListItem Value="UN">Unidad</asp:ListItem>
                                <asp:ListItem Value="UL">Unidad Residencial</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvDdlUnidad" SetFocusOnError="true" runat="server"
                                ErrorMessage="*" ControlToValidate="ddlUnidad" Display="Dynamic" ValidationGroup="vgGuardar"
                                Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreUnidad" runat="server" CssClass="cajaTextoGen"
                                MaxLength="100" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTxtNombreUnidad" SetFocusOnError="true" runat="server"
                                ErrorMessage="*" ControlToValidate="txtNombreUnidad" ValidationGroup="vgGuardar"
                                Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td>
            <asp:Panel ID="pComplemento" runat="server">
                <asp:TextBox ID="txtComplemento" runat="server" CssClass="cajaTextoGen"
                    MaxLength="100" Width="50px"></asp:TextBox><label id="lblAlerta" for=""></label>
            </asp:Panel>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="cajaTextoDireccion textbox"
                Enabled="false" Width="400px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtDireccion" SetFocusOnError="true" runat="server"
                ErrorMessage="*" ValidationGroup="vgGuardar" ControlToValidate="txtDireccion"
                Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:HyperLink ID="hplLimpiarDireccion" runat="server" NavigateUrl="#">Limpiar</asp:HyperLink>
            <%--<asp:LinkButton ID="btnLimpiarDireccion" runat="server"  CssClass="vinculo">Limpiar</asp:LinkButton>--%>
        </td>
    </tr>
</table>

<script type="text/javascript">
    $(document).ready(function () {

        // array para llenar opciones de combo de letras
        var arrayLetras = ["A", "B", "C", "D", "E", "F", "G",
                                "H", "I", "J", "K", "L", "M", "N",
                                "O", "P", "Q", "R", "S", "T", "U",
                                "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG",
                                "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                                "AO", "AP", "AQ", "AR", "AS", "AT", "AU",
                                "AV", "AW", "AX", "AY", "AZ"];

        // Configurando RadioButtons
        $('#<%= ddlLetra2.ClientID %>').prop("disabled", true);
        $('#<%= txtDireccion.ClientID %>').prop("disabled", true);

        // CARGA DE COMBOS
        $('#<%= ddlLetra.ClientID %>').append('<option value=""></option>');
        $('#<%= ddlLetra2.ClientID %>').append('<option value=""></option>');
        $('#<%= ddlLetra3.ClientID %>').append('<option value=""></option>');
        $('#<%= ddlLetra4.ClientID %>').append('<option value=""></option>');

        for (var i = 0; i < arrayLetras.length; i++) {
            $('#<%= ddlLetra.ClientID %>').append('<option value="' + arrayLetras[i] + '">' + arrayLetras[i] + '</option>');
            $('#<%= ddlLetra2.ClientID %>').append('<option value="' + arrayLetras[i] + '">' + arrayLetras[i] + '</option>');
            $('#<%= ddlLetra3.ClientID %>').append('<option value="' + arrayLetras[i] + '">' + arrayLetras[i] + '</option>');
            $('#<%= ddlLetra4.ClientID %>').append('<option value="' + arrayLetras[i] + '">' + arrayLetras[i] + '</option>');
        }

        cargarListaSentido();

        $('#<%= pTipoDireccion.ClientID %>').hide();
        $('#<%= pVia.ClientID %>').hide();
        $('#<%= pManzana.ClientID %>').hide();
        $('#<%= pComplemento.ClientID %>').hide();
        $('#<%= hplLimpiarDireccion.ClientID %>').hide();



        //#region FUNCIONES GENERALES

        function cargarListaSentido() {// esta funcion carga la lista de sentido en el evento ready de js

            $('#<%= ddlSentido.ClientID %>').append($('<option value=""></option>'));
            $('#<%= ddlSentido.ClientID %>').append($('<option value="NORTE">NORTE</option>'));
            $('#<%= ddlSentido.ClientID %>').append('<option value="SUR">SUR</option>');
            $('#<%= ddlSentido.ClientID %>').append('<option value="ESTE">ESTE</option>');
            $('#<%= ddlSentido.ClientID %>').append('<option value="OESTE">OESTE</option>');

            $('#<%= ddlSentido2.ClientID %>').append('<option value=""></option>');
            $('#<%= ddlSentido2.ClientID %>').append('<option value="NORTE">NORTE</option>');
            $('#<%= ddlSentido2.ClientID %>').append('<option value="SUR">SUR</option>');
            $('#<%= ddlSentido2.ClientID %>').append('<option value="ESTE">ESTE</option>');
            $('#<%= ddlSentido2.ClientID %>').append('<option value="OESTE">OESTE</option>');
        }

        function Limpiar() {
            $('#<%= ddlVia.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlManzana.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlBis.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlLetra.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlLetra2.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlSentido.ClientID %>').prop('selectedIndex', -1);
            $('#<%= txtNumero.ClientID %>').val("");
            $('#<%= ddlLetra3.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlBis2.ClientID %>').prop('selectedIndex', -1);
            $('#<%= ddlLetra4.ClientID %>').prop('selectedIndex', -1);
            $('#<%= txtPlaca.ClientID %>').val("");
            $('#<%= ddlSentido2.ClientID %>').prop('selectedIndex', -1);
            $('#<%= txtComplemento.ClientID %>').val("");
        }


        function getDireccion() {
            var direc = "";
            if ($('#<%= pVia.ClientID %>').is(':hidden')) {
                direc = $('#<%= ddlVia.ClientID %>').val();
            }
            else {
                direc = $('#<%= ddlVia.ClientID %>').val();
                direc += $('#<%= txtNombreVia.ClientID %>').val() != "" || $('#<%= txtNombreVia.ClientID %>').val() != "undefined" || $('#<%= txtNombreVia.ClientID %>').val() != null ? " " + $('#<%= txtNombreVia.ClientID %>').val() : "";
                direc += $('#<%= ddlLetra.ClientID %>').val() != "" || $('#<%= ddlLetra.ClientID %>').val() != "undefined" || $('#<%= ddlLetra.ClientID %>').val() != null ? " " + $('#<%= ddlLetra.ClientID %>').val() : "";
                direc += $('#<%= ddlBis.ClientID %>').val() != "" || $('#<%= ddlBis.ClientID %>').val() != "undefined" || $('#<%= ddlBis.ClientID %>').val() != null ? " " + $('#<%= ddlBis.ClientID %>').val() : "";
                direc += $('#<%= ddlLetra2.ClientID %>').val() != "" || $('#<%= ddlLetra2.ClientID %>').val() != "undefined" || $('#<%= ddlLetra2.ClientID %>').val() != null ? " " + $('#<%= ddlLetra2.ClientID %>').val() : "";
                direc += $('#<%= ddlSentido.ClientID %>').val() != "" || $('#<%= ddlSentido.ClientID %>').val() != "undefined" || $('#<%= ddlSentido.ClientID %>').val() != null ? " " + $('#<%= ddlSentido.ClientID %>').val() : "";
                direc += $('#<%= txtNumero.ClientID %>').val() != "" || $('#<%= txtNumero.ClientID %>').val() != "undefined" || $('#<%= txtNumero.ClientID %>').val() != null ? " " + $('#<%= txtNumero.ClientID %>').val() : "";
                direc += $('#<%= ddlLetra3.ClientID %>').val() != "" || $('#<%= ddlLetra3.ClientID %>').val() != "undefined" || $('#<%= ddlLetra3.ClientID %>').val() != null ? " " + $('#<%= ddlLetra3.ClientID %>').val() : "";
                direc += $('#<%= ddlBis2.ClientID %>').val() != "" || $('#<%= ddlBis2.ClientID %>').val() != "undefined" || $('#<%= ddlBis2.ClientID %>').val() != null ? " " + $('#<%= ddlBis2.ClientID %>').val() : "";
                direc += $('#<%= ddlLetra4.ClientID %>').val() != "" || $('#<%= ddlLetra4.ClientID %>').val() != "undefined" || $('#<%= ddlLetra4.ClientID %>').val() != null ? " " + $('#<%= ddlLetra4.ClientID %>').val() : "";
                direc += $('#<%= txtPlaca.ClientID %>').val() != "" || $('#<%= txtPlaca.ClientID %>').val() != "undefined" || $('#<%= txtPlaca.ClientID %>').val() != null ? " " + $('#<%= txtPlaca.ClientID %>').val() : "";
                direc += $('#<%= ddlSentido2.ClientID %>').val() != "" || $('#<%= ddlSentido2.ClientID %>').val() != "undefined" || $('#<%= ddlSentido2.ClientID %>').val() != null ? " " + $('#<%= ddlSentido2.ClientID %>').val() : "";
                direc += $('#<%= txtComplemento.ClientID %>').val() != "" || $('#<%= txtComplemento.ClientID %>').val() != "undefined" || $('#<%= txtComplemento.ClientID %>').val() != null ? " " + $('#<%= txtComplemento.ClientID %>').val() : "";
            }
            return direc;
        }

        function getdireccion2() {
            var direccion = "";
            if ($('#<%= pManzana.ClientID %>').is(':hidden')) {
                direccion = $('#<%= ddlManzana.ClientID %>').val();
            }
            else {
                direccion += $('#<%= ddlManzana.ClientID %>').val();
                direccion += $('#<%= txtNombreManzana.ClientID %>').val() != "" || $('#<%= txtNombreManzana.ClientID %>').val() != "undefined" || $('#<%= txtNombreManzana.ClientID %>').val() != null ? " " + $('#<%= txtNombreManzana.ClientID %>').val() : "";
                direccion += $('#<%= ddlUnidad.ClientID %>').val() != "" || $('#<%= ddlUnidad.ClientID %>').val() != "undefined" || $('#<%= ddlUnidad.ClientID %>').val() != null ? " " + $('#<%= ddlUnidad.ClientID %>').val() : "";
                direccion += $('#<%= txtNombreUnidad.ClientID %>').val() != "" || $('#<%= txtNombreUnidad.ClientID %>').val() != "undefined" || $('#<%= txtNombreUnidad.ClientID %>').val() != null ? " " + $('#<%= txtNombreUnidad.ClientID %>').val() : "";
                direccion += $('#<%= txtComplemento.ClientID %>').val() != "" || $('#<%= txtComplemento.ClientID %>').val() != "undefined" || $('#<%= txtComplemento.ClientID %>').val() != null ? " " + $('#<%= txtComplemento.ClientID %>').val() : "";
            }
            return direccion;
        }

        //#enregion


        //#region EVENTO RADIO BUTTON

        $('#<%=rbtnDetalleZonaGeo.ClientID %> input').change(function () {

            $('#<%= pVia.ClientID %>').hide();
            $('#<%= pManzana.ClientID %>').hide();
            $('#<%= pComplemento.ClientID %>').hide();

            $('#<%= ddlVia.ClientID %>').show();
            $('#<%= ddlManzana.ClientID %>').show();
            $('#<%= hplLimpiarDireccion.ClientID %>').hide();
            if ($(this).val() == "R") {

                // LIMPIAR FILA
                $('#<%= pTipoDireccion.ClientID %>').hide();

                $('#<%= txtDireccion.ClientID %>').prop("disabled", false);
                $('#<%= hplLimpiarDireccion.ClientID %>').show();
                //se ocultan las demas opciones
            } else if ($(this).val() == "U") {

                $('#<%= pTipoDireccion.ClientID %>').show();
                $('#<%= hplLimpiarDireccion.ClientID %>').hide();

                $('#<%= ddlVia.ClientID %>').prop('selectedIndex', -1);
                $('#<%= ddlManzana.ClientID %>').prop('selectedIndex', -1);

                $('#<%= txtDireccion.ClientID %>').prop("disabled", true);
            }
        });

        //#endregion 


        //#region EVENTS CONTROL VIA

        $('#<%= ddlVia.ClientID %>').change(function (e) {
            e.preventDefault();

            $('#<%= txtDireccion.ClientID %>').val(getDireccion()); //cadenadireccion
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
            $('#<%= pVia.ClientID %>').show();
            $('#<%= pManzana.ClientID %>').hide();
            $('#<%= ddlManzana.ClientID %>').hide();
            $('#<%= ddlVia.ClientID %>').show();
            $('#<%= pComplemento.ClientID %>').show();
        });


        $('#<%=txtNombreVia.ClientID %>').blur(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlBis.ClientID %>').change(function (e) {
            e.preventDefault();
            if ($(this).val() != "") {
                $('#<%= ddlLetra2.ClientID %>').prop("disabled", false);
            } else if ($(this).val() !== "BIS") {
                $('#<%= ddlLetra2.ClientID %>').prop("disabled", true);
            }
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlBis2.ClientID %>').change(function (e) {
            e.preventDefault();
            if ($(this).val() != "") {
                $('#<%= ddlLetra4.ClientID %>').prop("disabled", false);
            } else if ($(this).val() !== "BIS") {
                $('#<%= ddlLetra4.ClientID %>').prop("disabled", true);
            }
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlLetra.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlLetra2.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= txtNumero.ClientID %>').blur(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlLetra3.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlSentido.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlLetra4.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= txtPlaca.ClientID %>').blur(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        $('#<%= ddlSentido2.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });

        //#endregion EVENT CONTROL MANZANA


         $('#<%= ddlManzana.ClientID %>').change(function (e) {
            e.preventDefault();

            $('#<%= txtDireccion.ClientID %>').val(getdireccion2());
             $('#<%= hfControlText.ClientID %>').val(getdireccion2());

             $('#<%= pManzana.ClientID %>').show();
             $('#<%= pVia.ClientID %>').hide();
             $('#<%= ddlVia.ClientID %>').hide();
             $('#<%= ddlManzana.ClientID %>').show();
             $('#<%= pComplemento.ClientID %>').show();
         });


        $('#<%= txtNombreManzana.ClientID %>, #<%= txtNombreUnidad.ClientID %>').blur(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getdireccion2());
            $('#<%= hfControlText.ClientID %>').val(getdireccion2());
        });


        $('#<%= ddlUnidad.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getdireccion2());
            $('#<%= hfControlText.ClientID %>').val(getdireccion2());
        });

        //#region

       
        //#endregion

        //#region OTROS EVENTOS

        $('#<%= hplLimpiarDireccion.ClientID %>').click(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val("");
            $('#<%= hfControlText.ClientID %>').val("");
            Limpiar();
        });

        $('#<%= txtComplemento.ClientID %>').change(function (e) {
            e.preventDefault();
            $('#<%= txtDireccion.ClientID %>').val(getDireccion());
            $('#<%= hfControlText.ClientID %>').val(getDireccion());
        });


        $('#<%= txtComplemento.ClientID %>').blur(function (e) {
            e.preventDefault();
            var RadioSelected = $('#<%=rbtnDetalleZonaGeo.ClientID %> input').val();
            if (RadioSelected != null) {
                if (RadioSelected == "R") {
                    if (!$('#<%= ddlManzana.ClientID %>').is(":hidden")) {
                        $('#<%= txtDireccion.ClientID %>').val(getdireccion2());
                        $('#<%= hfControlText.ClientID %>').val(getdireccion2());
                    }
                    else {
                        $('#<%= txtDireccion.ClientID %>').val(getdireccion());
                        $('#<%= hfControlText.ClientID %>').val(getdireccion());
                    }
                }
            }
        });

        //#endregion

        $('#<%= txtDireccion.ClientID %>').blur(function (e) {
            e.preventDefault();
            var RadioSelected = $('#<%=rbtnDetalleZonaGeo.ClientID %> input').val();
            if (RadioSelected != null) {
                if (RadioSelected == "R") {
                    $('#<%= hfControlText.ClientID %>').val($('#<%= txtDireccion.ClientID %>').val());
                }
            }
        });

    });



</script>
