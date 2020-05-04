<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Devoluciones Aplicacion :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

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
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:UpdatePanel ID="updFormaPago" runat="server">
            <ContentTemplate>                                    
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Devoluciones</strong><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Panel ID="pConsulta" runat="server">
                                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0">
                                    <tr>
                                          <td class="tdD" style="text-align: left; width: 140px">Fecha Aplicacion<br />
                                            <ucFecha:fecha ID="txtFec_Apli" runat="server" />
                                        </td>
                                        <td class="tdD" style="width: 160px; text-align: left">Número Recaudo<br />
                                            <asp:TextBox ID="txtNumeroRecaudo" runat="server" CssClass="textbox" Width="90%" />
                                        </td>
                                        <td class="tdD" style="text-align: left; width: 140px">Fecha Periodo<br />
                                            <ucFecha:fecha ID="txtFechaPeriodo" runat="server" />
                                        </td>
                                        <td class="tdD" style="text-align: left; width: 300px">Empresa<br />
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="dropdown"
                                                Width="250px" Height="24px" />
                                        </td>
                                        <td class="tdD" style="text-align: left; width: 300px">Tipo Devolucion<br />
                                              <asp:DropDownList ID="ddlTipoComp" runat="server" CssClass="dropdown"
                                                Width="250px" Height="24px" >
                                                <asp:ListItem Enabled="true" Text="Seleccionar" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="DEVOLUCIONES" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="EGRESOS EN EFECTIVO" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="APLICACION SALDOS A FAVOR" Value="12"></asp:ListItem>
                                                 <asp:ListItem Text="DEDUCCIONES POR RECIBIR" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="APORTES FALLECIDOS" Value="12"></asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td class="tdD">&nbsp;
                                        </td>
                                    </tr>
                                </table>               
                            </asp:Panel>                 
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px">
                            <asp:Panel ID="panelGrilla" runat="server">
                            <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel" Visible ="False"/>
                            <div id="divLista" runat="server" style="overflow: scroll; height: 500px; width: 100%">
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="False" OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="cod_persona"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        <ItemStyle CssClass="gridIco"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona">
                                        <ItemStyle HorizontalAlign="center" />
                                            
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUM_DEVOLUCION" HeaderText="Num. Devolucion">
                                        <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                        <asp:BoundField DataField="fecha" HeaderText="Fec. Recaudo" DataFormatString="{0:dd-MM-yyyy}">
                                        <ItemStyle HorizontalAlign="center"  />
                                            </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-Width="400px">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            </div>
                            </asp:Panel>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>                    
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
