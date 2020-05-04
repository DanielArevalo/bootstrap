<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvafiliacion" runat="server" >
        <asp:View ID="vconsulta" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 56%">
                    <tr>
                        <td style="height: 15px; text-align: left; font-size: x-small;">
                            <strong>
                                <asp:Label ID="lblInfo" runat="server" Style="font-size: small; color: Red; height: 45px;
                                    text-align: left;" Visible="False" />
                                <br />
                                Criterios de Búsqueda:</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px; text-align: left; width: 150px;">
                            Fecha de Corte<br />
                            <ucFecha:fecha ID="txtfechacorte" runat="server" AutoPostBack="True" CssClass="textbox"
                                MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                         <td style="height: 15px; text-align: left; width: 150px;">
                            <asp:TextBox ID="txtcod_persona" runat="server" CssClass="textbox" Visible="false"
                                Width="148px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 37px; text-align: left; width: 230px;">
                            <strong>listado De Asociados a Causar: </strong>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:UpdatePanel ID="panelactualizar" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panelgrilla" runat="server">
                        <table style="width: 120%">
                            <tr>
                                <td style="width: 483px">
                                    <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                                        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                        OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_persona">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkafilia" runat="server" OnCheckedChanged="checkeado_oncheckedchanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="idafiliacion" HeaderText="ID">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cod_persona" HeaderText="Codigo">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="identificacion" HeaderText="identificación">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fec. Afiliación" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor" HeaderText="Valor Afiliación" DataFormatString="{0:N0}" />
                                            <asp:BoundField DataField="cuotas" HeaderText="Cuota" />
                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx. Pago" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblvalortotal" runat="server" Text="Valor Total a Causar: " Visible="false"></asp:Label>
                                    <uc2:decimales ID="txtvalortotal" runat="server" Visible="false" CssClass="textbox"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Datos Guardados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View1" runat="server">
        <table style="width: 100%;">
         <tr >  
            <td >
                <asp:GridView ID="gvimpresion" runat="server" Width="80%" AutoGenerateColumns="False" 
                    AllowPaging="False" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_persona">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkafilia" runat="server"  />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idafiliacion" HeaderText="ID" >
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_persona" HeaderText="Codigo" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="identificación" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombres" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fec. Afiliación" 
                            DataFormatString="{0:d}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor" HeaderText="Valor Afiliación" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="cuotas" HeaderText="Cuota" />
                        <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx. Pago" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="Label1" runat="server" Visible="False" />
                
            </td>
        </tr>  
        </table>
        </asp:View>      
    </asp:MultiView>
        <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
 
</asp:Content>
