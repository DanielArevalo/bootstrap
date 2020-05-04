<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Servicio Carga :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pErrores" runat="server" Visible="false">
        <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="lblMostrarDetalles" runat="server" />
                    <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
            <div style="border-style: none; border-width: medium; overflow: scroll; max-height: 500px; background-color: #f5f5f5">
                <table width="100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/Images/btnExportar.jpg"
                                OnClick="btnExportar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvInconsistencia" runat="server" Width="100%" GridLines="Horizontal"
                                AutoGenerateColumns="False" PageSize="20" CssClass="table"
                                DataKeyNames="numero_registro" Style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField DataField="numero_registro" HeaderText="No. Fila" ItemStyle-Width="50"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblTotalIncon" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
            Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
            ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
            TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles" />
        <br />
    </asp:Panel>

    <asp:MultiView ID="mvServicio" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="pConsulta" runat="server">

                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <b>Separador del archivo : </b>&nbsp;&nbsp;Coma
                        </td>
                        <td style="text-align: left">
                            <b>Estructura de archivo: (Identificación, Clave)</b>
                        </td>
                        <td style="text-align: left">
                            <asp:FileUpload ID="flpArchivo" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr style="color: black; width: 100%; padding: 5px;" />

            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="80%" GridLines="Horizontal"
                                AutoGenerateColumns="False" PageSize="20" CssClass="table"
                                Style="font-size: x-small" DataKeyNames="Identificación, Clave  ">
                                <Columns>
                                    <asp:BoundField DataField="Identificación" HeaderText="Identificacion"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblTotReg" runat="server" /></td>
                    </tr>
                </table>
                
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
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
                    <td style="text-align: center; font-size: large; color: Red">Se grabaron correctamente los servicios cargados<br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">&nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
