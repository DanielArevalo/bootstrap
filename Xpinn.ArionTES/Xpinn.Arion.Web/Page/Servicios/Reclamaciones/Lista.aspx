<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <table>
            <tr>
                <td style="text-align: left" colspan="5">
                    <strong>Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Num. Servicio<br />
                    <asp:TextBox ID="txtNumServ" runat="server" CssClass="textbox" Width="80px" />
                </td>
                <td style="text-align: left;">
                    Linea de Servicio<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="150px" />
                </td>
                <td style="text-align: left;">
                    Identificación<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="80px" />
                </td>
                <td style="text-align: left;">
                    Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="200px" />
                </td>
                <td style="text-align: left;">
                    Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="100px" />
                </td>
                <td style="text-align: left;">
                    Ident. Fallecido<br />
                    <asp:TextBox ID="txtIdentFallecido" runat="server" CssClass="textbox" Width="100px" />
                </td>
                <td style="text-align: left;">
                    Fec. Reclamación<br />
                    <ucFecha:fecha ID="txtFechareclamacion" runat="server" CssClass="textbox" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr style="width:100%" />
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
            <td style="text-align:left">
                <strong>Listado de Servicios :</strong>                
            </td>
        </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"  OnRowDeleting="gvLista_RowDeleting"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio"  style="font-size: x-small">
                        <Columns>
                         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                        ToolTip="Detalle" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle VerticalAlign="Top" Width="20px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="numero_servicio" HeaderText="Id.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Linea Servicio">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_reclamacion" HeaderText="Fec. Reclamación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion_fallecido" HeaderText="Ident.Fallecido">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_fallecido" HeaderText="Nombre Fallecido">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_plan" HeaderText="Nom Usuario">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                              <asp:BoundField DataField="fechacreacion" HeaderText="Fec. Creación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>                          
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="False" />                    
                </td>
            </tr>
        </table>
    </asp:Panel>
    

</asp:Content>
