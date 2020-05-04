<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewPrincipal">
            <table border="0" style="height: 84px; width: 1100px" cellspan="1">
                <tr>
                    <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                        <label>Código Empleado</label>
                        <br />
                        <asp:TextBox ID="txtCodigoEmpleado" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
                        </asp:TextBox>
                    </td>
                    <td style="width: 209px; height: 51px;" class="logo">
                        <label>Identificación</label>
                        <br />
                        <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                        </asp:TextBox>
                    </td>
                    <td style="width: 209px; height: 51px;" class="logo">&nbsp;
                <label>Nombre</label>
                        <br />
                        <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server" Width="180px">
                        </asp:TextBox>
                    </td>
                    <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                        <label>Nómina</label>
                        <br />
                        <asp:DropDownList runat="server" ID="ddlNomina" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div style="overflow-x: scroll; max-width: 1205px;">
                <table border="0" style="width: 1200px">
                    <tr>
                        <td>
                            <asp:GridView ID="gvHoraExtras" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvHoraExtras_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvHoraExtras_SelectedIndexChanged"
                                OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvHoraExtras_RowDeleting"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                        <ItemStyle Width="16px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="consecutivo" HeaderText="Cod. Hora">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigoempleado" HeaderText="Cod. Empleado">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion_empleado" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="desc_nomina" HeaderText="Nómina">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cantidadhoras" HeaderText="Cantidad Horas">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="desc_concepto_hora" HeaderText="Concepto Horas">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>

                            <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View runat="server" ID="viewImportar">
            <br />
            <table cellpadding="2">
                <tr>
                    <td style="text-align: left;" colspan="4">
                        <strong>Criterios de carga</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Fecha de Carga<br />
                        <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                    </td>
                    <td style="text-align: left">Formato de fecha<br />
                        <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="160px">
                            <asp:ListItem Text="dd/MM/yyyy" Value="dd/MM/yyyy" />
                            <asp:ListItem Text="yyyy/MM/dd" Value="yyyy/MM/dd" />
                            <asp:ListItem Text="MM/dd/yyyy" Value="MM/dd/yyyy" />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <input id="avatarUpload" type="file" name="file" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                        <table width="100%">
                            <tr>
                                <td style="text-align: left; font-size: x-small">
                                    <strong>Estructura de archivo : </strong>
                                    <br />
                                    (Horas Extras)<br />
                                    Fecha | Identificacion Empleado (Numerico)&nbsp; |&nbsp; Cantidad Horas (Numerico)&nbsp; | Codigo Concepto Horas (Numerico)&nbsp; | Codigo Nomina (Numerico)
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:Button ID="btnCargarHoras" runat="server" CssClass="btn8" OnClientClick="return TestInputFileToImportData(upload);" OnClick="btnCargarHoras_Click"
                            Height="22px" Text="Cargar Horas" Width="150px" />
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
                                Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
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
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-size: large;">Importación de datos generada
                                    <br />
                                        Revisa la tabla de errores en caso de haber registros que no se guardaron correctamente!.
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelCreditos" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True" OnRowDeleting="gvDatos_RowDeleting" Width="600px"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                    ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion_empleado" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidadhoras" HeaderText="Cantidad Horas">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigoconceptohoras" HeaderText="Cod. Concepto Horas">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigonomina" HeaderText="Cod. Nomina">
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
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
