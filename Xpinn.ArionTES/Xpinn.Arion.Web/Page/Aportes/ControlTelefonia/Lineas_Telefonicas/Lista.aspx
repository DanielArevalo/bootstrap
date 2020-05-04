<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Lineas Telefónicas :." EnableEventValidation="false" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <table style="width: 780px">
                <tr>
                    <td style="width: 15%; text-align: left">Num. Linea Telefónica<br />
                        <asp:TextBox ID="txtNumLinea" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 15%">Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 25%">Tipo Plan<br />
                        <asp:DropDownList ID="ddlPlan" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="num_linea_telefonica,Estado"
                                OnRowDeleting="gvLista_RowDeleting" Style="font-size: x-small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg"
                                        ShowEditButton="True" ControlStyle-CssClass="gridIco"/>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
                                        ShowDeleteButton="True" Visible="false" ControlStyle-CssClass="gridIco"/>
                                    <asp:BoundField DataField="num_linea_telefonica" HeaderText="Num. Linea Telefonica">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_serv_fijo" HeaderText="Num. Sevicio Fijo"
                                        DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_serv_adicional" HeaderText="Num. Sevicio Adicional"
                                        DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion_titular" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre_titular" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota"
                                        DataFormatString="{0:n0}" />
                                    <asp:BoundField HeaderText="Estado" DataField="estado" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwImportacion" runat="server">
            <br />
            <table cellpadding="2">
                <tr>
                    <td style="text-align: left;" colspan="3">
                        <strong>Criterios de carga</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <div id="divfecha" runat="server">
                            Fecha de carga <span style="color:red">*</span><br />
                            <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                        </div>
                    </td>
                    <td style="text-align: left">
                        <asp:FileUpload ID="fupArchivoPersona" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left; font-size: x-small">
                        <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Nro. Identificación, Número Linea Telefónica, Nuevo Valor de Adicionales, Fecha próximo pago.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:Button ID="btnCargarAdicionales" runat="server" CssClass="btn8" OnClick="btnCargarAdicionales_Click"
                            Height="22px" Text="Cargar Adicionales" Width="150px" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <table cellpadding="2" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pErrores" runat="server">
                            <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                        <asp:Label ID="lblMostrarDetalles" runat="server" />
                                        <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
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
                            <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
                                Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles" />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelData" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvDatos" runat="server" Width="90%" AutoGenerateColumns="False"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnRowDeleting="gvDatos_RowDeleting"
                                    DataKeyNames="" Style="font-size: x-small">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:BoundField DataField="identificacion_titular" HeaderText="Nro. Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_linea_telefonica" HeaderText="Nro. Linea Telefonica">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota Adicionales" DataFormatString="{0:n2}">
                                            <ItemStyle HorizontalAlign="Right" />
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
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;" id="tbnew">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">Importación de datos generada correctamente.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td id="infApor_no" runat="server" visible="false">
                        <asp:GridView ID="GridView1" runat="server" Width="90%" AutoGenerateColumns="true"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True">
                            <Columns>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lab_infApor_no" runat="server" Text="Números de lineas con adicionales no exitosos en la importación, ERROR EN LOS DATOS SUMINISTRADOS." />
                    </td>
                </tr>
            </table>
        </asp:View>


    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />


</asp:Content>
