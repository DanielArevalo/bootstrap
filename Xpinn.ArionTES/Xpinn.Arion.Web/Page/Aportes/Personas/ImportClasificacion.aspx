<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="ImportClasificacion.aspx.cs" Inherits="Page_Aportes_Personas_ImportClasificacion" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
        <script src="../../../../Scripts/PCLBryan.js"></script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <br />
       <table>
                <tr>
                    <td style="text-align: left; font-size: xx-small">
                        <br />
                        <b>Tipo de Archivo : </b>&nbsp;&nbsp;&nbsp;Excel
                        <br />
                        <br />
                        <br />Estructura de archivo :  Documento,Clasificacion
                        <br />
                            <br />
                            <b>Nombre de Pestaña excel : </b>&#160;&#160; Datos
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
                        <asp:Button ID="btnCargar" runat="server" Text="Cargar clientes" CssClass="btn8" Height="22px" Width="150px" OnClick="btnCargarPersonas_Click" />
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
                       >
                        <Columns>

                            <asp:BoundField DataField="Identificacion" HeaderText="Número Identificación" />
                            <asp:BoundField DataField="valor" HeaderText="Clasificación" />
                           
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
</asp:Content>

