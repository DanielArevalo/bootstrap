<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipos de Correo:." ValidateRequest="False" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScritpManager1" runat="server" EnablePageMethods="true" />
    <script type="text/javascript">
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#blah')
                        .attr('src', e.target.result)
                        .width(30)
                        .height(30);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <br />
    <br />
    <asp:MultiView ID="mvTipoDopcumento" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <div>
                <table border="0" style="text-align: left; width: 54%;">
                    <tr style="text-align: left;">
                        <td>
                            <strong align="left">Tipo De Documento:<br />
                            </strong>
                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="ddltipo_documento_OnselectedIndexChanged"
                                Width="300">
                                <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                                <asp:ListItem Value="1">Credito Aprobado</asp:ListItem>
                                <asp:ListItem Value="2">Credito Negado</asp:ListItem>
                                <asp:ListItem Value="3">Credito Aplazado</asp:ListItem>
                                <asp:ListItem Value="4">Control de Documentos</asp:ListItem>
                                <asp:ListItem Value="5">Hoja de Ruta</asp:ListItem>
                                <asp:ListItem Value="6">Formato Cumpleaños</asp:ListItem>
                                <asp:ListItem Value="7">Icetex Aprobado</asp:ListItem>
                                <asp:ListItem Value="8">Icetex Negado</asp:ListItem>
                                <asp:ListItem Value="9">Icetex Aplazado</asp:ListItem>                                
                                <asp:ListItem Value="10">Solicitud Credito Atencion Web</asp:ListItem>
                                <asp:ListItem Value="11">Icetex Aprobado Inscrito</asp:ListItem>
                                <asp:ListItem Value="12">Icetex Negado Inscrito</asp:ListItem>
                                <asp:ListItem Value="13">Icetex Aplazado Inscrito</asp:ListItem>
                                <asp:ListItem Value="14">Pago Por Ventanilla</asp:ListItem>
                                <asp:ListItem Value="15">Estado de Cuenta</asp:ListItem>
                                <asp:ListItem Value="16">Extracto ahorros</asp:ListItem>
                                <asp:ListItem Value="17">Confirmación producto web</asp:ListItem>
                                <asp:ListItem Value="18">Rechazo producto web</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>
                    <tr style="text-align: left;">
                        <td>
                            <br />
                            <strong align="left">Lista de parametros a sustituir en el formato:<br />
                            </strong>
                            <asp:DropDownList ID="ddlParametroFormato" runat="server" CssClass="textbox"
                                Width="300">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <strong>Seleccione una imagen a cargar</strong>
                        </td>
                        <td rowspan="3">
                            <img src="#" id='blah' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type='file' value="Seleccione su imagen" onchange="readURL(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small">
                            <em>Arrastre la imagen hacia el campo de texto </em>
                        </td>
                    </tr>
                    

                </table>
            </div>
            <div>
                <FTB:FreeTextBox ID="FreeTextBox1" runat="Server" Text="" Width="900px" OnSaveClick="btnGuardar_Click" ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell" />
            </div>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Documento Grabado Correctamente"></asp:Label> <br />
                            <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" Text="Regresar" OnClick="btnRegresar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
