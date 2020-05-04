<%@ Page Title="Expinn - Programado Cierre Cuentas" Language="C#" MasterPageFile="~/General/Master/site.master"
    AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
   

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="80%">
        <table cellspacing="5">
            <tr>
                <td colspan="2" style="font-size:x-small">
                    <strong>Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Cuenta
                    <br />
                    <asp:TextBox ID="txtCuenta" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                </td>
                  <td style="text-align: left">
                      Línea<br />
                   <asp:DropDownList ID="ddlLinea" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Oficina<br />
                   <asp:DropDownList ID="ddlOficina" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:DropDownList>
                </td>
              <td style="text-align: left">
                    Fecha Prorroga<br />
                   <uc1:fecha ClientIDMode="Static" runat="server" ID="txtFecha"/>
                </td>

                   <td style="text-align: left">
                       Identificación
                    <br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                </td>
                 <td style="text-align: left">
                    Nombre
                    <br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de las cuentas Prorrogadas:</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="15" ShowHeaderWhenEmpty="True"
                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                        margin-bottom: 0px;" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                        DataKeyNames="Cuenta" GridLines="Horizontal" >
                        <Columns>
                          
                            <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Linea" HeaderText="Línea" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Oficina" HeaderText="Oficina" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_Apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Identificacion" HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_Prorroga" HeaderText="Fecha Prroroga">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha Inicial Prorroga">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Fecha_Final" HeaderText="Fecha Vencimiento Prorroga">
                                <HeaderStyle HorizontalAlign="Center" />
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
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Style="text-align: center" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
