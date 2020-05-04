<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Width="606px">
                <table cellpadding="5" cellspacing="0" style="width: 99%">

                    <tr>
                        <td style="height: 15px; text-align: left; font-size: x-small;" colspan="5">
                            <strong>Criterios de Búsqueda:</strong></td>
                    </tr>
                    <tr>
                        <td style="height: 15px; text-align: left;">Linea Aporte
                    <br />
                            <asp:TextBox ID="txtCodLinea" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                            <br />
                        </td>
                        <td style="height: 15px; text-align: left;">Nombre Linea<br />
                            <asp:TextBox ID="txtNombreLinea" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                        </td>
                        <td style="height: 15px; text-align: left;">Estado<br />
                            <asp:DropDownList ID="DdlEstado" runat="server" Width="154px" CssClass="textbox">
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                <asp:ListItem Value="1">Activo</asp:ListItem>
                                <asp:ListItem Value="2">Inactivo</asp:ListItem>
                                <asp:ListItem Value="3">Cerrado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 15px; text-align: left; width: 185px;"></td>
                        <td style="height: 15px; text-align: left;">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 80%">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="98%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_linea_aporte"
                            Style="text-align: left">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit"
                                            ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado_Linea" HeaderText="Estado" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                    </td>
                </tr>
            </table>
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
                            Fecha de Carga<br />
                            <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                        </div>
                    </td>
                    <td style="text-align: left">
                        <div id="divffecha" runat="server">
                            Formato de fecha<br />
                            <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="160px">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="text-align: left">Linea Aporte<br />
                        <asp:DropDownList ID="DdlLineaAporte" runat="server" AutoPostBack="True" Style="margin-bottom: 0px"
                            Width="250px" CssClass="textbox" OnSelectedIndexChanged="DdlLineaAporte_SelectedIndexChanged">
                            <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                        </asp:DropDownList>
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
                        <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Nro. Identificación, Número Aporte, Nuevo Valor del Aporte.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:Button ID="btnCargarAportes" runat="server" CssClass="btn8" OnClick="btnCargarAportes_Click"
                            Height="22px" Text="Cargar Aportes" Width="150px" />
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
                                    DataKeyNames="cod_linea_aporte" Style="font-size: x-small">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:BoundField DataField="identificacion" HeaderText="Nro. Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_aporte" HeaderText="Nro. Aporte">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" DataFormatString="{0:n2}">
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
                    <td id="infApor_no" runat="server" Visible="false">
                        <asp:GridView ID="GridView1" runat="server" Width="90%" AutoGenerateColumns="true"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True">
                            <Columns>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <br />     
                        <asp:Label ID="lab_infApor_no" runat="server" Text="Números de aporte no exitosos en la importación, ERROR EN LOS DATOS SUMINISTRADOS."  />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
