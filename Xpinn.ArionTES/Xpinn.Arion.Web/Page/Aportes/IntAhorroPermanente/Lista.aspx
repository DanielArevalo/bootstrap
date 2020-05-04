<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 950,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="70%">
            <tr>
                <td style="text-align: left;"><br/></td>
            </tr>
            <tr>
                <td style="text-align: left; width: 300px">Empresa<br />
                    <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="dropdown" Width="250px" Height="25px"></asp:DropDownList>
                </td>
                <td style="text-align: left;">Número Recaudo<br />
                    <asp:TextBox ID="txtNumRecaudo" runat="server" CssClass="textbox" Width="150px"  />
                </td>
                <td style="text-align: left;">Periodo<br />
                    <uc:fecha ID="txtFecPeriodo" runat="server" CssClass="textbox" Width="150px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br/>
    <asp:Panel ID="pLista" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="3" style="width: 100%"><strong>Listado de empresas</strong>
                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" OnRowEditing="gvLista_RowEditing"
                        HeaderStyle-CssClass="gridHeader" DataKeyNames="cod_persona" HorizontalAlign="Center"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: small; width: 90%"
                        ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Gestionar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                            <asp:BoundField DataField="nom_persona" HeaderText="Nombre" />
                            <asp:BoundField DataField="numero_aporte" HeaderText="Número Ahorro" />
                            <asp:BoundField DataField="valor" HeaderText="Valor Interés" />--%>
                            <asp:BoundField DataField="numero_recaudo" HeaderText="No.Recaudo" />
                            <asp:BoundField DataField="nom_tipo_recaudo" HeaderText="Tipo Recaudo" />
                            <asp:BoundField DataField="nom_empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="periodo_corte" HeaderText="Período" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="fecha_recaudo" HeaderText="Fecha Recaudo" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="fecha_aplicacion" HeaderText="Fecha Aplicacion" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="nom_estado" HeaderText="Estado" />
                            <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="numero_producto" HeaderText="Nro. Comp" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>                    
                </td>
        </table>
    </asp:Panel>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
</asp:Content>

