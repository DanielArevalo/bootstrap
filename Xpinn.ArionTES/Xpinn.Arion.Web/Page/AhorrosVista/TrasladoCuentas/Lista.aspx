<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
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
        <table cellpadding="5" cellspacing="0" style="width: 80%; margin-right: 0px;">
            <tr>
                <td style="height: 15px; text-align: left;">
                    Número Cuenta<br />
                    <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left; width: 150px;">
                    Fecha Apertura<br />
                    <ucFecha:fecha ID="txtAprobacion_fin" runat="server" AutoPostBack="True" CssClass="textbox"
                        MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="height: 15px; text-align: left; width: 119px;">
                    Línea<br />
                    <asp:DropDownList ID="ddlLineaAhorro" runat="server" Width="300px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 15px; text-align: left;">
                    Identificacion<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="text-align: left; height: 15px; width: 99px;">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" Width="180px " CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; height: 15px;" colspan="2">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="height: 26px">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                    Text="Exportar a Excel" />
            </td>
        </tr>
        </table>
                <br />

       <asp:Panel ID="panelGrilla" runat="server">
        <table>
            <tr>
                <td style="text-align:left;">
                <strong>Listado de Cuentas :</strong> <br />
                <div style="overflow:scroll; "> 
                  <asp:GridView ID="gvLista" runat="server" Width="665px" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowDeleting="gvLista_RowDeleting" 
        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" 
        DataKeyNames="numero_cuenta" Style="font-size: xx-small" PageSize="100">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_cuenta" HeaderText="No.Cuenta">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_apertura" HeaderText="F.Apertura" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_linea_ahorro" HeaderText="Línea">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
