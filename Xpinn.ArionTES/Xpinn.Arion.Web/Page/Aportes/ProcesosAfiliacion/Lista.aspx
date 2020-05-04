<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br/>
    <asp:Panel runat="server"><br/>
        <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; color: #0066FF; font-size: small">
                    Búsqueda Avanzada
                </div>
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pBusqueda" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                        <strong>Criterios de Búsqueda:</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" width="14%">Tipo de Identificación<br />
                        <asp:DropDownList ID="ddlTipo_ID" runat="server" Width="43%" CssClass="textbox" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;" width="30%">Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" width="30%">Identificación<br />
                        <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" width="30%">Nombre<br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="true" Width="65%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" width="30%">Apellido<br />
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Enabled="true" Width="65%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" width="30%">Lugar del proceso<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Enabled="true" Width="65%"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" width="30%">Fecha<br />
                        <uc1:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="true" Width="50%"></uc1:fecha>
                    </td>
                    <td style="text-align: left;" width="30%">Funcionario<br />
                        <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textbox" Enabled="true" Width="65%"></asp:DropDownList>
                    </td>
                    <td style="text-align: left;" width="30%">Concepto<br />
                        <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="textbox" Enabled="true" Width="50%"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pBusqueda"
            ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="False"
            TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
            CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" ExpandedSize="150" />
        <br />
        <asp:Label ID="lblTituloGrid" Text="Listado de personas" runat="server"></asp:Label>
        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                        OnPageIndexChanging="gvLista_PageIndexChanging" HorizontalAlign="Center"
                        OnRowEditing="gvLista_RowEditing" DataKeyNames="cod_persona" Style="font-size: x-small">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="tipo_persona" HeaderText="Tipo Persona">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo Identificación">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="digito_verificacion" HeaderText="D.V.">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lugar_proceso" HeaderText="Lugar del Proceso">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha del Proceso" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_usuario" HeaderText="Funcionario">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_concepto" HeaderText="Concepto">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Visible="False" />
        <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
            Visible="False" />
    </asp:Panel>
</asp:Content>

