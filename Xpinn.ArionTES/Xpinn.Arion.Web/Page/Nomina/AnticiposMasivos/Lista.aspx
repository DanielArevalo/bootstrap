<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Empleados :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" width="90%">
        <tr>
            <td>
                <table id="tbCriterios" border="0" width="100%">
                    <tr>
                        <td style="width: 20%">Código Liquidación<br />
                            <asp:TextBox ID="txtCodigoLiquidacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 20%">Fecha Inicio<br />
                            <asp:TextBox ID="txtFechaInicio" MaxLength="10" CssClass="textbox"
                                runat="server" Width="80%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                PopupButtonID="imagenCalendarioInicio"
                                TargetControlID="txtFechaInicio"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <img id="imagenCalendarioInicio" alt="Calendario"
                                src="../../../Images/iconCalendario.png" />
                        </td>
                        <td style="width: 20%">Fecha Fin<br />
                            <asp:TextBox ID="txtFechaFin" MaxLength="10" CssClass="textbox"
                                runat="server" Width="80%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                                PopupButtonID="imagenCalendarioFin"
                                TargetControlID="txtFechaFin"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <img id="imagenCalendarioFin" alt="Calendario"
                                src="../../../Images/iconCalendario.png" />
                        </td>
                        <td style="width: 20%">Tipo Nómina<br />
                            <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="dropdown" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">Centro Costo<br />
                            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="dropdown" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>

                     
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista"
                    runat="server"
                    AllowPaging="True"
                    AutoGenerateColumns="False"
                    GridLines="Horizontal"
                    PageSize="20"
                    HorizontalAlign="Center"
                    ShowHeaderWhenEmpty="True"
                    Width="100%"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    DataKeyNames="consecutivo"
                    OnRowCommand="OnRowCommandDeleting"
                    OnRowDeleting="gvLista_RowDeleting"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" visible="FALSE">
                            <ItemStyle Width="16px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="consecutivo" HeaderText="Cod.Anticipo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="codigonomina" HeaderText="Cod.Nómina" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="desc_nomina" HeaderText="Nómina" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="codigocentrocosto" HeaderText="Cod.Centro Costo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="desc_centro_costo" HeaderText="Centro Costo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fechainicio" HeaderText="Fecha Inicio" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fechaterminacion" HeaderText="Fecha Fin" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fechageneracion" HeaderText="Fecha Generación" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nom_usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="salario" HeaderText="Sumatoria Salarios" ItemStyle-HorizontalAlign="Center" />                         
                        <asp:BoundField DataField="porcentaje_anticipo" HeaderText="Porcentaje Anticipos" ItemStyle-HorizontalAlign="Center" />                       
                        <asp:BoundField DataField="valor_anticipo" HeaderText="Sumatoria Anticipos" ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField DataField="num_comp" HeaderText="Num. Comp." ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>

                            <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp." ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>

                       
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" />
            </td>
        </tr>
    </table>
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>
