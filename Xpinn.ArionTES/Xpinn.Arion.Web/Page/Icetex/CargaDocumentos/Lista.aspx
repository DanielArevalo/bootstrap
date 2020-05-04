<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width:100%">
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <strong>Criterios de búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Num Crédito<br />
                            <asp:TextBox ID="txtNumCredito" runat="server" Width="100px" CssClass="textbox" />
                            <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txtNumCredito" ValidChars="-()" />
                        </td>
                        <td style="text-align: left">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" Width="100px" CssClass="textbox" />
                            <asp:FilteredTextBoxExtender ID="fte2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtIdentificacion" ValidChars="-()" />
                        </td>
                        <td style="text-align: left">Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" Width="270px" CssClass="textbox" />
                        </td>
                        <td style="text-align: left">Fecha Solicitud<br />
                            <ucFecha:fecha ID="txtFecIni" runat="server" />
                            &nbsp;a&nbsp;
                            <ucFecha:fecha ID="txtFecFin" runat="server" />
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="140px">
                                <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                <asp:ListItem Value="S">Pre-Inscrito</asp:ListItem>
                                <asp:ListItem Value="A">Aprobado</asp:ListItem>
                                <asp:ListItem Value="Z">Aplazado</asp:ListItem>
                                <asp:ListItem Value="N">Negado</asp:ListItem>
                                <asp:ListItem Value="I">Inscrito</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Listado de Créditos</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <div style="overflow: scroll; max-width: 100%;">
                                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                                    Style="font-size: x-small" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" DataKeyNames="numero_credito, cod_convocatoria, tipo_beneficiario">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                                        <asp:BoundField DataField="numero_credito" HeaderText="Num Credito">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec Solicitud" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_convocatoria" HeaderText="Convocatoria">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_beneficiario" HeaderText="Tipo">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_nombre" HeaderText="1er Nombre">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_nombre" HeaderText="2do Nombre">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_apellido" HeaderText="1er Apellido">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_apellido" HeaderText="2do Apellido">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="email" HeaderText="E-Mail">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estrato" HeaderText="Estrato" />
                                        <asp:BoundField DataField="nom_universidad" HeaderText="Universidad">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_programa_univ" HeaderText="Programa">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_programa" HeaderText="Tipo">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="Valor">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodos" HeaderText="Periodo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_beneficiario" HeaderText="CodBeneficiario">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </div>
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
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwData" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left;font-weight:600">Num Crédito
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumCredito" runat="server" Width="120px" />
                    </td>
                    <td style="text-align: left;font-weight:600">Identificación
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblIdentificacion" runat="server" Width="120px" />
                    </td>
                    <td style="text-align: left;font-weight:600">Nombre</td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNombre" runat="server" Width="220px" />
                    </td>
                    <td style="text-align: left;font-weight:600;">Tipo Beneficiario</td>
                    <td style="text-align: left">
                        <asp:Label ID="lblTipoBene" runat="server" Width="300px" />
                        <asp:Label ID="lblCodBene" runat="server" Visible="false"/>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrid" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Listado de Documentos</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <div style="overflow: scroll; max-width: 100%;">
                                <asp:GridView ID="gvDocumentosReq" runat="server" AutoGenerateColumns="False"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="cod_tipo_doc"
                                    Width="100%" OnRowDataBound="gvDocumentosReq_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="cod_tipo_doc" HeaderText="Cod Tipo Doc" Visible="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Tipo Documento">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Pregunta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPegrunta" runat="server" Text='<%# Bind("pregunta") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Respuesta">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRespuesta" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Archivo">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuArchivo" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                         <asp:Label ID="lblInfoDocu" runat="server" Text="Actualmente no existen documentos requeridos para la convocatoria actual."
                                    Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align:center">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label id="lblMsj" runat="server" style="color:red"/>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    
</asp:Content>

