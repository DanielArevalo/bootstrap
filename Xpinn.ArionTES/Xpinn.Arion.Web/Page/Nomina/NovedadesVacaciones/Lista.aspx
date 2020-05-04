<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewPrincipal">
            <table border="0" style="height: 84px; width: 1100px" cellspan="1">
                <tr>
                    <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                        <label>Código Empleado</label>
                        <br />
                        <asp:TextBox ID="txtCodigoEmpleado" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
                        </asp:TextBox>
                    </td>
                    <td style="width: 209px; height: 51px;" class="logo">
                        <label>Identificación</label>
                        <br />
                        <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                        </asp:TextBox>
                    </td>
                    <td style="width: 209px; height: 51px;" class="logo">&nbsp;
                <label>Nombre</label>
                        <br />
                        <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server" Width="180px">
                        </asp:TextBox>
                    </td>
                    <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                        <label>Nómina</label>
                        <br />
                        <asp:DropDownList runat="server" ID="ddlNomina" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div style="overflow-x: scroll; max-width: 1205px;">
                <table border="0" style="width: 1200px">
                    <tr>
                        <td>
                            <asp:GridView ID="gvDiasVacaciones" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvDiasVacaciones_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvDiasVacaciones_SelectedIndexChanged"
                                OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvDiasVacaciones_RowDeleting"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="False">
                                        <ItemStyle Width="16px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                        <ItemStyle CssClass="gridIco" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigoempleado" HeaderText="Cod. Empleado">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="desc_nomina" HeaderText="Nómina">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="fechainicio" DataFormatString="{0:d}" HeaderText="Fecha Inicio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                         <asp:BoundField DataField="fechaterminacion" DataFormatString="{0:d}" HeaderText="Fecha Terminación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                     <asp:BoundField DataField="fechainicioperiodo" DataFormatString="{0:d}" HeaderText="Fecha Inicio Periodo">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                         <asp:BoundField DataField="fechaterminacionperiodo" DataFormatString="{0:d}" HeaderText="Fecha Terminación Periodo">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cantidaddias" HeaderText="Cantidad Dias">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                   
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>

                            <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
