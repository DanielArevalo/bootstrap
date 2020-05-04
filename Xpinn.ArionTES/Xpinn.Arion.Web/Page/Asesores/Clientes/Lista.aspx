<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="AseClienteLista" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>

    <asp:MultiView ID="mvClientes" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwConsulta" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <ucImprimir:imprimir ID="ucImprimir" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                            <table cellpadding="5" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="tdI">Primer Nombre del Cliente
                                        <br />
                                        <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                                    </td>
                                    <td class="tdD">Primer Apellido del Cliente<br />
                                        <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">Identificación del Cliente<br />
                                        <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                                    </td>
                                    <td class="tdD">Zona<br />
                                        <asp:DropDownList ID="ddlZona" runat="server" CssClass="dropdown" Width="50%"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr width="100%" noshade>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowDataBound="gvLista_RowDataBound" OnRowCommand="gvLista_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="IdCliente" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"></asp:BoundField>

                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NombreTipoIdentificacion" HeaderText="Tipo Identificación" />
                                <asp:BoundField DataField="NumeroDocumento" HeaderText="Número Identificación" />
                                <asp:BoundField DataField="PrimerNombre" HeaderText="Primer Nombre" />
                                <asp:BoundField DataField="SegundoNombre" HeaderText="Segundo Nombre" />
                                <asp:BoundField DataField="PrimerApellido" HeaderText="Primer Apellido" />
                                <asp:BoundField DataField="SegundoApellido" HeaderText="Segundo Apellido" />

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
            <br />
            <table>
                <tr>
                    <td style="text-align: left; font-size: xx-small">
                        <br />
                        <b>Tipo de Archivo : </b>&nbsp;&nbsp;&nbsp;Excel
                        <br />
                        <br />
                        <br />Estructura de archivo :  Primer nombre, Segundo nombre, Primer Apellido, Segundo Apellido, Tipo documento , Número documento,
                              Telefono, Correo electronico , Código de Zona , Códigoejecutivo ,Direccion,
                              Razón social de la empresa, Sigla de la empresa, Código de actividad,
                              Código Motivo afiliación , Observaciones
                        <br />
                            <br />
                            <b>Nombre de Pestaña excel : </b>&#160;&#160; Datos
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; font-size: xx-small">
                        Limpiar Clientes :
                      
                        <asp:CheckBox ID="chkLimpiar" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <br />
                        <asp:FileUpload ID="flpArchivo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <br />
                        <asp:Button ID="btnCargar" runat="server" Text="Cargar clientes" CssClass="btn8" Height="22px" Width="150px" OnClick="btnCargar_Click" Visible="false" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />

            <asp:Panel ID="panErrores" runat="server" Visible="false">
                <asp:Panel ID="pEncBusqueda1" runat="server" CssClass="collapsePanelHeader" Height="30px">
                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                            <asp:Label ID="lblMostrarDetalles1" runat="server" />
                            <asp:ImageButton ID="imgExpand1" runat="server" ImageUrl="~/Images/expand.jpg" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pConsultaErrores" runat="server" Width="100%">
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
                    TargetControlID="pConsultaErrores" TextLabelID="lblMostrarDetalles1" />
                <br />
            </asp:Panel>
            <br />
            <br />
            <br />
            <asp:Panel ID="panCargueExitoso" runat="server" Visible="false">
                <div style="overflow: scroll; max-height: 550px; width: 100%">
                    <asp:GridView ID="gvCarguesExitosos" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        OnRowDataBound="gvLista_RowDataBound" OnRowCommand="gvLista_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdCliente" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"></asp:BoundField>
                            <asp:BoundField DataField="NumeroDocumento" HeaderText="Número Identificación" />
                            <asp:BoundField DataField="PrimerNombre" HeaderText="Primer Nombre" />
                            <asp:BoundField DataField="SegundoNombre" HeaderText="Segundo Nombre" />
                            <asp:BoundField DataField="PrimerApellido" HeaderText="Primer Apellido" />
                            <asp:BoundField DataField="SegundoApellido" HeaderText="Segundo Apellido" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
                <asp:Label ID="lblTotalCarguesExitoso" runat="server" Visible="False" />
            </asp:Panel>
            <br />
            <asp:Label Id="lblMensajeExitoso" style="text-align: center; font-size: large;" text="Importación de datos generada correctamente." runat="server" Visible="false"></asp:Label>
        </asp:View>
    </asp:MultiView>
</asp:Content>
