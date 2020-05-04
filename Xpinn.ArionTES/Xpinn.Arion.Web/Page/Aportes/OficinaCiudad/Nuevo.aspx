<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%" cellspacing="2" cellpadding="2">
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 50%">
                        Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 48%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 40%">
                        Código Ciudad:<br />
                        <asp:TextBox ID="txtCodigo" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Width="140px" />
                        <asp:Label ID="lblMensaje" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        <br />
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 40%">
                        Ciudad:<br />
                        <asp:DropDownList ID="ddlCiudad" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente los datos ingresados
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" OnClick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <script type="text/javascript">
        var lista = [];

        $('#txtCodigo').on('blur', function () {
            var alertasi = false;

            if ($(this).length > 0) {
                var ddl = document.getElementById("ddlCiudad");
                var codigo = $(this).val();

                for (var i = 0; i < ddl.options.length; i++) {
                    if (ddl.options[i].value == codigo) {
                        $("#ddlCiudad").val(codigo);
                        alertasi = true;
                    }
                }
                if (alertasi) {
                    $('#lblMensaje').text("");
                    $('#lblMensaje').css('display', 'none');
                }
                else {
                    $('#lblMensaje').css('display', 'inline');
                    $('#lblMensaje').text("No se encontro Ciudad con ese codigo").css({ 'color': 'rgba(247, 94, 18, 0.99)', 'padding-left': '11px', 'font-size': '15px', 'font-weight': '400' });
                }
            }
        });

        $("#ddlCiudad").on('change', function (e) {
            $('#lblMensaje').text("");
            $('#lblMensaje').css('display', 'none');
            $('#txtCodigo').val($(this).val());
            console.log(lista);


        });


    </script>
</asp:Content>
