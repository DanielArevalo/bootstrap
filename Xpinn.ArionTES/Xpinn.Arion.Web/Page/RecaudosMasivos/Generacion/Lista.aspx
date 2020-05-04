<%@ Page Title="Expinn - Estructura Archivos" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>  

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/css/select2.min.css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    

     <script type="text/javascript">
        $(document).ready(function () {
            $("#cphMain_ddlEmpresa").select2();
        });
    </script>
     

   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" Width="100%" >
        <div id="popupcontainer" style="width: 100%">
            <table style="width: 80%;">
                <tr>
                    <td style="width: 25%; text-align: left">
                        Empresa
                    </td>
                    <td style="width: 15%; text-align: left">
                        Tipo de lista
                    </td>
                    <td style="text-align: left; width: 15%">
                        Fecha Recaudo
                    </td>
                    <td style="text-align: left; width: 15%">
                        Estado
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="text-align: left">
                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="90%" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoLista" runat="server" AppendDataBoundItems="True" 
                            CssClass="textbox" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <ucFecha:fecha ID="txtfecha" runat="server" CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar"
                            Width="148px" />
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="148px"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <strong>Listado de Novedades Generadas:</strong>
                        </td>
                        <td>
                            <span style="font-size: x-small">Ordenar por</span> 
                            <asp:DropDownList ID="ddlOrdenar" runat="server" AppendDataBoundItems="True" 
                                CssClass="textbox" Width="120px" AutoPostBack="True" 
                                onselectedindexchanged="ddlOrdenar_SelectedIndexChanged">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="1" Text="Número Novedad"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tipo de Recaudo"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Empresa"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Tipo Lista"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Periodo"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Fecha Generacion"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Estado"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <span style="font-size: x-small">Luego por</span> 
                            <asp:DropDownList ID="ddlOrdenarLuego" runat="server" 
                                AppendDataBoundItems="True" CssClass="textbox" Width="120px" AutoPostBack="True" 
                                onselectedindexchanged="ddlOrdenarLuego_SelectedIndexChanged">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="1" Text="Número Novedad"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tipo de Recaudo"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Empresa"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Tipo Lista"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Periodo"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Fecha Generacion"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Estado"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="15" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                                margin-bottom: 0px;" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                                DataKeyNames="numero_novedad" OnRowEditing="gvLista_RowEditing" GridLines="Horizontal"
                                OnRowDeleting="gvLista_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
                                        <ItemStyle HorizontalAlign="center" Width="4%" />
                                    </asp:CommandField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="true">
                                        <ItemStyle HorizontalAlign="center" Width="4%" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="numero_novedad" HeaderText="Num Novedad" Visible="False" />
                                    <asp:BoundField HeaderText="Tipo Recaudo" DataField="nom_tipo_recaudo">
                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_empresa" HeaderText="Empresa">
                                        <ItemStyle HorizontalAlign="Left" Width="35%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Periodo" DataField="periodo_corte" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_generacion" HeaderText="Fecha Generacion" DataFormatString="{0:d}" />                                    
                                    <asp:BoundField DataField="nom_estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numero_novedad" HeaderText="Num Novedad" Visible="True" />
                                    <asp:BoundField DataField="nom_tipo_lista" HeaderText="Tipo Lista" Visible="True" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panel10" runat="server" Style="text-align: center;">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Style="text-align: center" />
                <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="false" Style="text-align: left" />
            </asp:Panel>
    </div>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeConfirmar" runat="server" PopupControlID="panelEliminar"
        TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelEliminar" runat="server" BackColor="White" Style="text-align: right"
        BorderWidth="1px" Width="500px">
        <div id="Div1" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        Esta Seguro de Eliminar el Registro Seleccionado?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="btn8" Width="182px"
                            OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" Width="182px"
                            OnClick="btnParar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
  
</asp:Content>
