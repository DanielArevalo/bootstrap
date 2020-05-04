<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master"
    AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Vacaciones :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        var upload = '<%= avatarUpload.ClientID.ToLower()  %>';

    </script>
    <asp:Panel ID="pConsulta" runat="server">
        <asp:MultiView ActiveViewIndex="0" ID="mvPrincipal" runat="server">
            <asp:View runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td>
                            <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 25%">Código Persona<br />
                                        <asp:TextBox ID="txtCodPersona" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                                    </td>
                                    <td style="text-align: left; width: 25%" colspan="2">Empresa Recaudadora<br />
                                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="94%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 15%">
                                        <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha Periodo Novedad" /><br />
                                        <ucFecha:fecha ID="txtFecha" runat="server" Enabled="true" />
                                    </td>
                                    <td style="width: 25%">Identificación<br />
                                        <asp:TextBox ID="txtIdentificacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                                    </td>
                                    <td style="width: 25%">Código de nómina<br />
                                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="110px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGrid" runat="server" Visible="false" Width="100%">
                                <div style="overflow: scroll; max-height: 600px">
                                    <asp:GridView ID="gvLista" runat="server" Width="100%"
                                        AutoGenerateColumns="False" GridLines="Horizontal"
                                        HorizontalAlign="Center"
                                        ShowHeaderWhenEmpty="True"
                                        OnPageIndexChanging="gvLista_PageIndexChanging"
                                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                        DataKeyNames="idconsecutivo"
                                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvGarantias_RowDeleting"
                                        Style="font-size: x-small">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle Width="16px" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="idconsecutivo" HeaderText="No. Novedad" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nombres" HeaderText="Nombres" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="desc_tipo_identificacion" HeaderText="Tipo Ident." ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="identificacion" HeaderText="Identificacion" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="cod_nomina_empleado" HeaderText="Código de nómina" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="numero_cuotas" HeaderText="No. Cuotas" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="fecha_novedad" HeaderText="Fecha Novedad" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nom_empresa" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tipo_calculo" HeaderText="Tipo Calculo">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Label ID="lblTotalRegs" runat="server" />

                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwImportacion" runat="server">
                <br />
                <table cellpadding="2" style="width: 70%">
                    <tr>
                        <td style="text-align: left;" colspan="4">
                            <strong>Criterios de carga</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 20%">Fecha de Carga<br />
                            <ucFecha:fecha ID="ucFecha" runat="server" Enabled="false" />
                        </td>
                        <td style="width: 20%; text-align: left">Empresa Pagaduría
                        <br />
                            <asp:DropDownList ID="ddlPagaduria" runat="server" CssClass="dropdown" Width="94%">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 20%;">Archivo
                        <asp:DropDownList ID="ddlTipoCalculo" runat="server" CssClass="dropdown" Width="94%">
                            <asp:ListItem Text="Calculo en Generacion" Value="0" />
                            <asp:ListItem Text="Calculo en Carga" Value="1" />
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: left">Archivo
                        <br />
                            <input id="avatarUpload" type="file" name="file" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: left">
                            <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                            <table width="100%">
                                <tr>
                                    <td style="text-align: left; font-size: x-small">
                                        <strong>Estructura de archivo : SI = OBLIGATORIO , NO = NO OBLIGATORIO (Usar | |) </strong>
                                        <br />
                                        (Vacaciones)<br />
                                        IDENTIFICACION (Numérico - SI) FECHA_NOVEDAD (Numérico - SI), 
                                        NUMERO_CUOTAS (Numérico - SI)
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="4">
                            <asp:Button ID="btnCargarPersonas" runat="server" CssClass="btn8" OnClientClick="return TestInputFileToImportData(upload);" OnClick="btnCargarPersonas_Click"
                                Height="22px" Text="Cargar Datos" Width="150px" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
                <table cellpadding="2" width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pErrores" runat="server">
                                <asp:Panel ID="pEncBusqueda1" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                            <asp:Label ID="lblMostrarDetalles1" runat="server" />
                                            <asp:ImageButton ID="imgExpand1" runat="server" ImageUrl="~/Images/expand.jpg" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
                                    <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                        <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;">
                                            <Columns>
                                                <asp:BoundField DataField="numero_registro" HeaderText="No." ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="cpeDemo1" runat="Server" CollapseControlID="pEncBusqueda1"
                                    Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aquí para Mostrar Detalles...)"
                                    ExpandControlID="pEncBusqueda1" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                    ImageControlID="imgExpand1" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                    TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles1" />
                                <br />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlNotificacion" runat="server" Width="100%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center; font-size: large;">
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; font-size: large;">
                                            <asp:Label ID="lblImportacionCorrecta" runat="server" Visible="false" Text="Importación de datos generada correctamente" />
                                            <asp:Label ID="lblImportacionIncorrecta" runat="server" Visible="false" Text="Importación de datos incorrecta, revisa la tabla de errores!." />
                                            <br />
                                            <asp:Label ID="lblImportacionConErrores" runat="server" Visible="false" Text="Revisa la tabla de errores en caso de haber registros que no se guardaron correctamente!." />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="panelVacaciones" runat="server">
                                <div style="overflow: scroll; max-height: 550px; width: 100%;">
                                    <asp:GridView ID="gvDatos" runat="server" Width="70%" AutoGenerateColumns="False" GridLines="Horizontal"
                                        ShowHeaderWhenEmpty="True" HorizontalAlign="center" OnRowDeleting="gvDatos_RowDeleting"
                                        Style="font-size: x-small">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                        ToolTip="Eliminar" Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_novedad" DataFormatString="{0:d}" HeaderText="Fecha Cobrar">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="numero_cuotas" HeaderText="Número Cuotas">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>
