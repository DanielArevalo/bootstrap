<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Actualizacion Masiva de Datos :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwData" runat="server">
    <asp:Panel ID="pConsulta" runat="server">
        <div style="text-align:left;">
         Fecha de Actualizacion<br />
                    <ucFecha:fecha ID="txtfecha" runat="server" CssClass="textbox" />
            </div>
        <table width="100%">
            <tr>
                <td style="text-align: left;" colspan="5">
                    <strong>Filtros</strong>
                </td>
            </tr>
            <tr>
                <td class="logo" style="text-align: left">
                     <table width="260px">
                          <tr>
                            <td>
                                Codigo
                            </td>
                         </tr>
                         <tr>
                            <td>
                       <asp:TextBox ID="txtCodIni" runat="server" CssClass="textbox" Width="100px" />
                        <asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                        TargetControlID="txtCodIni" ValidChars="" />
                                &nbsp; a&nbsp;
                                <asp:TextBox ID="txtCodFin" runat="server" CssClass="textbox" Width="100px" />
                                <asp:FilteredTextBoxExtender ID="ftb3" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCodFin" ValidChars="" />
                            </td>
                         </tr>
                    </table>
                </td>
               
                <td style="text-align: left">
                   Empresa<br />
                    <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="250px" />
                </td>
                <td style="text-align: left">
                    Asesor<br />
                    <asp:DropDownList ID="ddlAsesor" runat="server" CssClass="textbox" Width="250px" />
                </td>
                <td style="text-align: left">
                    Ubicación/Zona<br />
                    <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox" Width="170px" />
                </td>
            </tr>
            <tr>
                 <td style="text-align: left">
                    Asesor Nuevo<br />
                    <asp:DropDownList ID="ddlAsesorNuevo" Enabled="false" OnSelectedIndexChanged="ddlAsesorNuevo_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="textbox" Width="250px" />
                </td>
                <td style="text-align: left">
                    Ubicación/Zona Nuevo<br />
                    <asp:DropDownList ID="ddlZonaNuevo"  Enabled="false" OnSelectedIndexChanged="ddlZonaNuevo_SelectedIndexChanged" AutoPostBack="true"  runat="server" CssClass="textbox" Width="170px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="5">
                    <hr style="width: 100%" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Panel ID="panelGrilla" Height="500px" runat="server">
                    <strong>Listado de Personas</strong>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" 
                        PagerStyle-CssClass="gridPager" RowStyle-Font-Size="X-Small"
                        RowStyle-CssClass="gridItem" DataKeyNames="cod_persona">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkpersona" Checked="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="cod_persona" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina_empleado" HeaderText="Cod. Nomina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_asesor" HeaderText="Cod. Asesor">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Asesor">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="zona" HeaderText="Cod. Zona">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_zona" HeaderText="Ubicación/Zona">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Nuevo Asesor">
                                <ItemTemplate>
                                  <asp:Label runat="server" ID="lblAsesor" Text='<%# Eval("cod_asesor") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nueva Zona">
                                  <ItemTemplate>
                                  <asp:Label  runat="server" ID="lblZona" Text='<%# Eval("zona") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
                    Visible="False" />
            </td>
        </tr>
    </table>
        </asp:View>
        <asp:View ID="vwImportacion" runat="server">
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
                        </asp:DropDownList>
                    </td>
                
                    <td style="text-align: left">
                        <asp:FileUpload ID="fupArchivoPersona" runat="server" />
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                        <asp:Panel ID="panelInfo" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: left; font-size: x-small">
                                        <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Codigo, Identificación, Nombre,
                                        Cod Nomina, Cod Asesor, Asesor, Cod. Zona, Ubicación/Zona, Salario, Cuota Aporte &nbsp;&nbsp;Nota: Debe incluir el código de la persona y/o identificación
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:Button ID="btnCargarPersonas" runat="server" CssClass="btn8" OnClick="btnCargarPersonas_Click"
                            Height="22px" Text="Cargar Personas" Width="200px" />
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
                        <asp:Panel ID="panelPersonas" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                              <asp:GridView ID="gvpersonas" runat="server" Width="100%" AutoGenerateColumns="False"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" 
                        PagerStyle-CssClass="gridPager" RowStyle-Font-Size="X-Small"
                        RowStyle-CssClass="gridItem" DataKeyNames="cod_persona">
                        <Columns>
                            <asp:BoundField DataField="cod_persona" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina_empleado" HeaderText="Cod. Nomina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_asesor" HeaderText="Cod. Asesor">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Asesor">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="zona" HeaderText="Cod. Zona">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_zona" HeaderText="Ubicación/Zona">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Salario" HeaderText="Salario de la persona">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="cuota" HeaderText="Cuota Aporte">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
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
