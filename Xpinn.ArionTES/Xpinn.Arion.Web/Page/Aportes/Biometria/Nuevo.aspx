<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListaPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.css" type="text/css" />
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <table cellpadding="1" cellspacing="0" width="80%">
                <tr>
                    <td style="text-align: left; color: #FFFFFF; background-color: #0066FF;" colspan="4">
                        <strong>Datos de la Persona:</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 10%">Identificación<br />
                        <asp:TextBox name="min" ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 20%">Tipo Identificación<br />
                        <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="textbox" Width="100%" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 30%">Apellidos<br />
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="95%" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 30%">Nombres<br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="95%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 30%" colspan="2">Direccion<br />
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Width="100%" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 30%">Telefono<br />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="95%" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 30%">Cod.Persona<br />
                        <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Width="95%" Enabled="false" />
                    </td>
                </tr>
            </table>

            <table border="0" cellpadding="1" cellspacing="0" style="width: 80%; margin-top: 0px;">
                <tr>
                    <td style="text-align: left; color: #FFFFFF; background-color: #0066FF;" colspan="4">
                        <strong>Registros Biometricos:</strong>
                    </td>
                </tr>
                <tr>
                    <td style="float: left; vertical-align: top;">
                        <strong>Fotografía</strong>
                    </td>
                    <td></td>
                    <td style="float: right; width: 80%; vertical-align: top;">
                        <strong>Huella Digital</strong>
                        <br />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="float: left; width: 80%; vertical-align: top;" rowspan="2">
                        <div class="container">
                            <div id="results" style="display: none"></div>
                            <div id="Camera"></div>
                            <input runat="server" style="display: none" id="Base64" />
                        </div>
                        <div class="controls">
                        <%--    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <Triggers>
                                </Triggers>
                                <ContentTemplate>--%>
                                    &nbsp;
                                    <input type="button" value="Iniciar Camara" onclick="startCaptures()" style="width: 100px; margin-bottom: 0px; background-color: #359af2; color: #FFFFFF;" />
                                    <input type="button" value="Tomar Foto" onclick="take_snapshot(); Webcam.reset()" style="background-color: #359af2; color: #FFFFFF;" />
                                    <input type="button" runat="server" id="UpdateButton2" value="Guardar" style="background-color: #359af2; color: #FFFFFF;" onserverclick="OnServerClick" />
                              <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </td>
                    <td></td>
                    <td style="float: right; vertical-align: top;">
                        <asp:Image ID="imgHuella" runat="server" AlternateText="Image text" ImageAlign="left"
                            ImageUrl="huellas2.png" Style="height: 260px; width: 190px; visibility: Visible;" />
                        <br />
                        <asp:Button ID="btnTomarHuella" runat="server" CssClass="btn8" OnClick="validar_persona" Text="Tomar Huella" />
                        <br />
                        <br />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>

        </asp:View>
    </asp:MultiView>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/webcamjs/1.0.25/webcam.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/webcamjs/1.0.25/webcam.min.js" type="text/javascript"></script>


    <script type="text/javascript">
        var video = document.querySelector('video');
        var canvas = document.querySelector('canvas');
        var localMediaStream = null;
        var pageUrl = '<%=ResolveUrl("~/page/Aportes/Biometria/Nuevo.aspx")%>';

        function startCaptures() {
            $('#Camera').show();
            $("#results").hide();
            Webcam.set({
                width: 420,
                height: 340,
                image_format: 'jpeg',
                jpeg_quality: 90,
                force_flash: false
            });
            Webcam.attach('#Camera');
        }

        function take_snapshot() {
            $('#Camera').hide();
            $("#results").show();
            // take snapshot and get image data
            Webcam.snap(function (dataUri) {
                var html = '<img runat ="server" id="imgs" />';
                $("#results").append(html);
                // display results in page
                $("#cphMain_imgs").attr('Src', dataUri);
                $("#cphMain_Base64").val(dataUri);
            });
        }
        function resultado(result) {
            debugger;
            //if (result == "") {
                toastr.success(result, 'Foto Guardada', { timeOut: 4000 });
            //} else {
            //    toastr.error('Ha generado un error.' + result, 'Upss! Error', { timeOut: 4000 });
            //}
        }

    </script>

</asp:Content>
