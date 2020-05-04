<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Confirmación :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js"></script>
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
                        <td style="text-align: left">Producto<br />
                            <asp:DropDownList ID="ddlTipoProducto" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:Panel ID="Panel1" runat="server" Width="130px">
                                <asp:Label ID="lblFechaNovedad" runat="server" Text="Fecha Novedad"></asp:Label>
                                <asp:TextBox ID="txtFechaNovedad" MaxLength="10" CssClass="textbox"
                                    runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                    PopupButtonID="Image1"
                                    TargetControlID="txtFechaNovedad"
                                    Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                <img id="Image1" alt="Calendario"
                                    src="../../../Images/iconCalendario.png" />
                            </asp:Panel>
                        </td>
                        <td style="text-align: left">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="110px" />
                        </td>
                        <td style="text-align: left">Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="200px" />
                        </td>                        
                        <td style="text-align: left">Estado<br />
                            <asp:DropDownList ID="ddlEstado" runat="server">
                                <asp:ListItem Text="Solicitado" Value="0" />
                                <asp:ListItem Text="Aprobado" Value="1" />
                                <asp:ListItem Text="Rechazado" Value="2" />
                                <asp:ListItem Text="Todos" Value="3" />
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
                    <tr>
                         <td style="font-size: x-small; text-align: left">
                             <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
                        </td>
                    </tr>

                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <asp:Panel ID="pDatos" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="font-size: x-small; text-align: left">
                            <strong>Listado de Novedades para actualizar</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="id_novedad_cambio" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                OnRowEditing="gvLista_RowEditing" OnRowDeleting="gvLista_RowDeleting">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Visible='<%# Eval("nom_estado").ToString().Trim() != "Solicitado" ? false:true %>' ID="imgRechazar" CommandName="Edit" ImageUrl="~/Images/gr_elim.jpg" />                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" Visible='<%# Eval("nom_estado").ToString().Trim() != "Solicitado" ? false:true %>'
                                            ToolTip="Confirmación de producto" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>                                
                                    <asp:BoundField HeaderText="ID Solicitud" DataField="id_solicitud" />
                                    <asp:BoundField HeaderText="Fec. Solicitud" DataField="fecha" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                    <asp:BoundField HeaderText="Cod. Persona" DataField="cod_persona" />
                                    <asp:BoundField HeaderText="Identificación" DataField="identificacion" />
                                    <asp:BoundField HeaderText="Nombre" DataField="nombres" />
                                    <asp:BoundField HeaderText="Valor" DataField="valor_cuota" DataFormatString="${0:#,##0.00}"/>                                                                                                                                                                                                                          
                                    <asp:BoundField HeaderText="Estado Solicitud" DataField="nom_estado">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDocs" runat="server" CommandName="Delete"
                                            Visible='<%# Eval("tipo_producto").ToString().Trim() == "5" ? true:false %>'
                                            ToolTip="Documentos anexos" ImageUrl="~/Images/gr_detall.jpg"  Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Asesor" DataField="descripcion" />                                    
                                    <asp:BoundField HeaderText="Identificación PSE" DataField="IdPayment"/>
                                    <asp:BoundField HeaderText="Pago Realizado Por" DataField="PagoRealizadoPor" />
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
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />

</asp:Content>

