<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo"  %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <strong>Seleccione el servicio sobre el cual se va a hacer la reclamación</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Num. Servicio<br />
                            <asp:TextBox ID="txtNumServ" runat="server" CssClass="textbox" Width="110px" />
                        </td>
                        <td style="text-align: left;">
                            Fec. Solicitud<br />
                            <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;">
                            Linea de Servicio<br />
                            <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="230px" />
                        </td>
                        <td style="text-align: left;">
                            Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px" />
                        </td>
                        <td style="text-align: left;">
                            Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="300px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr style="width: 100%" />
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Listado de Servicios :</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio" Style="font-size: x-small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                                    <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                                    <asp:BoundField HeaderText="F. 1erCuota" DataField="fecha_primera_cuota" DataFormatString="{0:d}" />
                                    <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                                    <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                    <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                    <asp:BoundField HeaderText="Ident. Titular" DataField="identificacion_titular" />
                                    <asp:BoundField HeaderText="Nom. Titular" DataField="nombre_titular" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <table style="width: 740px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <strong>Datos Del Servicio</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Fec. Reclamación<br />
                        <ucFecha:fecha ID="txtfechareclamacion" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        <br />
                        <ucFecha:fecha ID="txtfechacrea" runat="server" CssClass="textbox" />
                    </td>
                </tr>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Num. Servicio<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Linea de Servicio<br />
                        <asp:DropDownList ID="ddllineaservicio" runat="server" AutoPostBack="True" Enabled="false"
                            CssClass="textbox" Width="260px" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Fec. Solicitud<br />
                        <ucFecha:fecha ID="txtfechasolicitud" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 160px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 160px">
                        &nbsp;
                    </td>
                    <tr>
                        <td style="text-align: left; width: 140px">
                            Identificación<br />
                            <asp:TextBox ID="txtIdentificacionTitu" runat="server" CssClass="textbox" Enabled="false"
                                Width="90%" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            Tipo Identificación<br />
                            <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="textbox" Enabled="false"
                                Width="90%" />
                        </td>
                        <td colspan="3" style="text-align: left; width: 420px">
                            Nombres<br />
                            <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" Enabled="false"
                                ReadOnly="True" Width="350px" />
                            <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                Display="Dynamic" Enabled="false" ErrorMessage="Seleccione encargado" ForeColor="Red"
                                InitialValue="0" Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            &nbsp;
                        </td>
                    </tr>
                </tr>
            </table>
            <asp:Panel ID="panel1" runat="server">
                <table>
                    <tr>
                        <td>
                            <strong>Datos Del Beneficiario</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvBeneficiarios" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                margin-bottom: 0px;" DataKeyNames="identificacion" GridLines="Horizontal" 
                                Width="584px">
                                <Columns>
                                   
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Parentesco">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="porcentaje" HeaderText="%">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
              </asp:Panel>
              <asp:Panel ID="panel2" runat="server">
                <table>
                    <tr>
                        <td>
                            <strong>Datos Del Fallecido</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 140px">
                            Identificación<br />
                            <asp:TextBox ID="txtidentificacionfallecido" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            Nombres y Apellidos<br />
                            <asp:TextBox ID="txtNombresFallecido" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td colspan="2" style="text-align: left; width: 280px">
                            Parentesco<br />
                            <asp:DropDownList ID="ddlparentesco" runat="server" AutoPostBack="True" CssClass="textbox"
                                Width="260px" />
                        </td>
                    </tr>
                </table>
           </asp:Panel>
            <table style="width: 740px">
                <tr>
                    <td style="width: 740px; text-align: center">
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 740px; text-align: center">
                        <hr style="width: 100%" />
                    </td>
                </tr>
            </table>
            </ContentTemplate>
            <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
                </triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
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
                        <td style="text-align: center; font-size: large; color: Red">
                            Datos de la reclamación modificados correctamente.<br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
