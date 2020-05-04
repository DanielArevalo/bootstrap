<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton runat="server" ID="btnSiguiente" ImageUrl="../../../Images/btnGuardar.jpg" OnClick="btnSiguiente_Click" Visible="false" style="margin-left: 650px;" />
    <script src="../../../Scripts/PCLBryan.js"> </script>
    <script type="text/javascript">
        var imgControlID = '<%= imgDocumento.ClientID  %>';
        var uploadFileID = '<%= avatarUpload.ClientID  %>';
        var hiddenFieldID = '<%= hiddenFieldImageData.ClientID  %>';
    </script>
    <asp:Panel ID="pnlDatosPersona" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: left;">
        <label style="text-align: left; color: #FFF; font-weight: bold">Datos de la Persona:</label>
    </asp:Panel>
    <asp:HiddenField ID="hiddenFieldImageData" ClientIDMode="Static" runat="server" />
    <table id="TblAnalisisSolicitudes" class="tableNormal" style="width: 100%; margin: 2% 0px;">
        <tr>
            <td style="width: 20%; text-align: left; padding: 2px;">
                <asp:Label runat="server" Style="text-align: left; margin-left: 10%" Text="Identificación" />
            </td>
            <td style="width: 16%; text-align: left; padding: 2px;">
                <asp:Label Style="text-align: left; margin-left: 10%" runat="server" Text="Tipo Identificación" />
            </td>
            <td style="width: 32%; text-align: left; padding: 2px;">
                <asp:Label Style="text-align: left; margin-left: 10%" runat="server" Text="Apellidos" />
            </td>
            <td style="width: 32%; text-align: left; padding: 2px;">
                <asp:Label Style="text-align: left; margin-left: 10%" runat="server" Text="Nombres" />
            </td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtIdentificacion" Enabled="false" runat="server" Width="90%" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 16%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtTipoIdentificacion" Enabled="false" runat="server" Width="90%" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 32%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtApellidos" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 32%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtNombres" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; text-align: left; padding: 2px;" colspan="2">
                <asp:Label Style="text-align: left; margin-left: 10%" Text="Dirección" runat="server" />
            </td>
            <td style="width: 25%; text-align: left; padding: 2px;">
                <asp:Label Style="text-align: left; margin-left: 10%" Text="Telefono" runat="server" />
            </td>
            <td style="width: 25%; text-align: left; padding: 2px;">
                <asp:Label Style="text-align: left; margin-left: 10%" Text="Cod.Persona" runat="server" />
            </td>
            <td style="width: 25%; text-align: left; padding: 2px;">
                <asp:Label Style="text-align: left; margin-left: 10%" Text="Estado" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 25%; text-align: left; padding: 5px;" colspan="2">
                <asp:TextBox ID="txtDireccion" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 25%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtTelefono" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 25%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtCodPersona" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 25%; text-align: left; padding: 5px;">
                <asp:TextBox ID="txtEstado" Enabled="false" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlDocumentos" class="titulo" runat="server" Style="background-color: #0099FF; height: 20px; text-align: left;">
        <label style="text-align: left; color: #FFF; font-weight: bold">Datos de los documentos:</label>
    </asp:Panel>
    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table style="width: 100%; margin: 2% 0px">
                <tr>
                    <td style="width: 30%;">
                        <label style="text-align: left; color: #000; font-weight: bold;">Tipos de Documentos</label>
                    </td>
                    <td>

                        <table style="width: 100%">
                            <tr>
                                <td style="width: 70%">
                                    <label style="text-align: left; color: #000; font-weight: bold">Documentos</label>
                                </td>
                                <td>
                                    <asp:Button ID="btnGuardarImagen" runat="server" CssClass="btn8" OnClientClick="return TestInputFileForImagesAndPDF(uploadFileID);" OnClick="btnGuardarImagen_Click" Text="Guardar Imagen" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 100%">
                        <asp:ListBox ID="lstBoxTipoDocumentos" AutoPostBack="true" Style="padding: 5px; min-height: 150px;" Height="100%" Width="100%" Font-Bold="true" runat="server" OnSelectedIndexChanged="lstBoxTipoDocumentos_SelectedIndexChanged" />
                    </td>
                    <td>

                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; margin-bottom: 20px; width: 100%; margin: auto;">
                                    <asp:Panel runat="server" ID="pnlImagen">
                                        <asp:Image ID="imgDocumento" EnableViewState="true" runat="server" Width="70%" />
                                    </asp:Panel>
                                    <asp:Panel runat="server" Visible="false" ID="pnlPDF">
                                        <asp:Literal ID="ltrPDF" runat="server" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr style="width: 30%">
                                <td>
                                    <asp:Label ID="lblNotificacionGuardado" BorderColor="Red" BorderWidth="1px" Style="padding: 2px 20px" Visible="false" ForeColor="Red" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input id="avatarUpload" type="file" name="file" onchange="javascript:PrevisualizarArchivoCargadoYGuardarEnHidden(imgControlID, uploadFileID, hiddenFieldID);" runat="server" />
                                    <asp:Label ID="lblIdImagen" runat="server" Visible="false" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Size="XX-Small" Text="Archivo Máx. 5MB."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Size="XX-Small" Text="Extensiones Validas *.jpg, *.jpeg, *.bmp, *.png, *.pdf"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardarImagen" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
