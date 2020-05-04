<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Confirmar Actualización :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script type="text/javascript">
        function Consultar(btnConsultar_Click) {
            var obj = document.getElementById("btnConsultar_Click");
            if (obj) {
                obj.click();
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width: 80%;">
                    <tr>
                        <td style="text-align: left; font-size: x-small" colspan="5">
                            <strong>Criterios de Búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Fecha de Solicitud<br />
                            <ucFecha:fecha ID="txtFechaSoli" runat="server" CssClass="textbox" Width="100px" />
                        </td>
                        <td style="text-align: left">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtIdentificacion"
                                Display="Dynamic" ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Style="font-size: x-small" Type="Integer" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left">Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left">Apellidos<br />
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Código de nómina<br />
                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td style="text-align: left">Oficina :<br />
                            <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">Zonas<br />
                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox">                                
                            </asp:DropDownList>
                        </td>
                        <asp:Panel runat="server" ID="pnlAse" Visible="false">
                            <td style="text-align: left">Asesor<br />
                                <asp:DropDownList ID="ddlAsesores" runat="server" CssClass="textbox">                                
                                </asp:DropDownList>
                            </td>
                        </asp:Panel>
                        <td style="text-align: left;display: flex;align-items: center;width: 150px;">&nbsp<br />
                            <br /><br />
                            <asp:CheckBox runat="server" ID="chkSinAsesor" Text="Incluir sin asesor" />
                        </td>              
                    </tr>
                </table>
                <hr style="width: 100%" />
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">Fecha de Aprobación<br />
                            <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar"
                                Width="100px" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pDatos" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="font-size: x-small; text-align: left">
                            <strong>Listado de Personas para actualizar</strong>
                        </td>
                    </tr>
                    <tr>
                         <td style="font-size: x-small; text-align: left">
                             <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="idconsecutivo" OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                    <asp:CommandField ButtonType="Image"
                                        ShowDeleteButton="True" DeleteImageUrl="~/Images/gr_elim.jpg" />
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle CssClass="gridIco" HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="idconsecutivo" HeaderText="Código" Visible="false">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codciudadresidencia" HeaderText="Ciudad">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomciudadresidencia" HeaderText="Nombre Ciudad">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="celular" HeaderText="Celular">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ciudadempresa" HeaderText="Ciudad Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomciudadempresa" HeaderText="Nombre Ciudad Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccionempresa" HeaderText="Dirección Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefonoempresa" HeaderText="Telefono Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="email" HeaderText="Email">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_solicitud" DataFormatString="{0:d}" HeaderText="Fecha Solicitud">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="asesor"  HeaderText="Asesor">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>--%>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <center>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado." /></center>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <table style="width: 100%; text-align: center">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;"></td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <asp:Label ID="lblMensajeGrabar" runat="server" Text="Registros modificados correctamente"></asp:Label><br />

                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

    <asp:ModalPopupExtender ID="mpeShowDetail" runat="server" PopupControlID="pnlInformationRequest"
        TargetControlID="hfInfoDetail" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="hfInfoDetail" runat="server" />
    <asp:Panel ID="pnlInformationRequest" runat="server" BackColor="White" CssClass="pnlBackGround" style="padding: 18px" Width="550px">
        <table style="width: 100%;">
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:ImageButton ID="imgCloseModal" runat="server" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Cerrar" />
                </td>
            </tr>
        </table>
        <asp:FormView ID="frvData" runat="server" Width="100%" OnDataBound="frvData_DataBound">
            <ItemTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align: center; font-weight: 600">
                            <h4 style="color: #359af2">DETALLE DE SOLICITUD</h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: right">
                            <b>Fecha de Solicitud:&nbsp;&nbsp;&nbsp;&nbsp;</b><br />

                        </td>
                        <td style="width: 50%; text-align: left">
                            <asp:Label ID="lblFecSoliModal" runat="server" Text='<%# Eval("fecha_solicitud", "{0:d}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>Código</b><br />
                            <asp:Label ID="lblCodigoMdl" runat="server" Text='<%# Eval("cod_persona") %>' />
                        </td>
                        <td style="width: 50%; text-align: left">
                            <b>Identificación</b><br />
                            <asp:Label ID="lblIdentificacionMdl" runat="server" Text='<%# Eval("identificacion") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>Nombres</b><br />
                            <asp:Label ID="lblNombresMdl" runat="server" Text='<%# Eval("Nombres") %>' />
                        </td>
                        <td style="width: 50%; text-align: left">
                            <b>Apellidos</b><br />
                            <asp:Label ID="lblApellidosMdl" runat="server" Text='<%# Eval("Apellidos") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; font-weight: 600">
                            <br />
                            <h4 style="color: #359af2">DATOS POR MODIFICAR</h4><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>Ciudad Residencia</b><br />
                            <asp:Label ID="lblNomCiudadMdl" runat="server" Text='<%# Eval("nomciudadresidencia") %>' />
                        </td>
                        <td style="width: 50%; text-align: left">
                            <b>Dirección</b><br />
                            <asp:Label ID="lblDireccionMdl" runat="server" Text='<%# Eval("direccion") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>Teléfono</b><br />
                            <asp:Label ID="lblTelefonoMdl" runat="server" Text='<%# Eval("telefono") %>' />
                        </td>
                        <td style="width: 50%; text-align: left">
                            <b>Celular</b><br />
                            <asp:Label ID="lblCelularMdl" runat="server" Text='<%# Eval("celular") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>Ciudad laboral</b><br />
                            <asp:Label ID="lblCiudadLabMdl" runat="server" Text='<%# Eval("nomciudadempresa") %>' />
                        </td>
                        <td style="width: 50%; text-align: left">
                            <b>Dirección laboral</b><br />
                            <asp:Label ID="lblDirecLabMdl" runat="server" Text='<%# Eval("direccionempresa") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>Teléfono laboral</b><br />
                            <asp:Label ID="lblTelefLabMdl" runat="server" Text='<%# Eval("telefonoempresa") %>' />
                        </td>
                        <td style="width: 50%; text-align: left">
                            <b>Email</b><br />
                            <asp:Label ID="lblEmailMdl" runat="server" Text='<%# Eval("email") %>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        
    </asp:Panel>

</asp:Content>
