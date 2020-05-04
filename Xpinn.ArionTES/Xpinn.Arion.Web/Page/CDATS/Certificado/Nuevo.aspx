<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Apertura :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctltasa.ascx" TagName="tasa" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="700px">
                        <tr>

                          <td style="text-align: left; width: 200px">
                                Fecha<br />
                                <uc2:fecha ID="txtFecha" runat="server" CssClass="textbox" enabled="false"/>
                            </td>

                            </tr>
                            <tr>
                            <td style="text-align: left; width: 150px;" enabled="false">
                                Número CDAT<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" enabled="false"/>
                                <asp:TextBox ID="txtNumCDAT" runat="server" CssClass="textbox" Width="90%" enabled="false"/>
                                <asp:Label ID="lblNumDV" runat="server" Text ="Autogenerado" CssClass="textbox" Visible="false" enabled="false"/>
                            </td>
                             <td style="text-align: left; width: 200px">
                                Fecha Apertura<br />
                                <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" enabled="false" />
                            </td>
                        <td style="text-align: left; width: 280px" colspan="2">
                                Tipo/Linea de CDAT<br />
                                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" enabled="false"
                                    Width="90%" AppendDataBoundItems="True" />
                            </td>
                          
                          </tr>
                        <tr>
                            <td style="text-align: left; width: 160px">
                                Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" enabled="false" CssClass="textbox" Width="90%" AppendDataBoundItems="True" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Valor<br />
                                <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" enabled="false"/>
                            </td>

                            <td style="text-align: left; width: 140px">
                                Moneda<br />
                                <asp:DropDownList ID="ddlTipoMoneda"  enabled="false" runat="server" CssClass="textbox" Width="90%"/>
                            </td>

                             <td style="width: 180px; text-align: left">
                Plazo<br />
                <asp:TextBox ID="txtPlazo" runat="server"  enabled="false" CssClass="textbox" Width="100px" />dias
            </td>  
             <td style="text-align: left; width: 160px">
                                Tipo Calendario<br />
                                <asp:DropDownList ID="ddlTipoCalendario" enabled="false" runat="server" CssClass="textbox" Width="90%"/>
                            </td>
                        </tr>

                            
                       
                        <tr>
                            <td colspan="4">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                      
                            <td style="text-align: left;" colspan="4">
                                <strong>Titulares:</strong><br />
                                
                                <div style="overflow: scroll; width: 730px;">
                                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Style="font-size: x-small" OnRowDataBound="gvDetalle_RowDataBound"
                                        GridLines="Horizontal" DataKeyNames="codigo_cdat" OnRowDeleting="gvDetalle_RowDeleting">
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="Codigo" Visible="false"><ItemTemplate><asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("codigo_cdat") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación"><ItemTemplate><table><tr><td><asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("identificacion") %>'></asp:Label></td></tr></table></ItemTemplate><ItemStyle HorizontalAlign="left" Width="170px" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Persona"><ItemTemplate><asp:TextBox ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'
                                                        CssClass="textbox" Width="80px" Enabled="false" /></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres"><ItemTemplate><asp:TextBox ID="lblNombre" runat="server" Text='<%# Bind("nombres") %>' CssClass="textbox"
                                                        Width="160px" Enabled="false" /></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellidos"><ItemTemplate><asp:TextBox ID="lblApellidos" runat="server" Text='<%# Bind("apellidos") %>' CssClass="textbox"
                                                        Width="160px" Enabled="false" /></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ciudad"><ItemTemplate><asp:TextBox ID="lblCiudad" runat="server" Text='<%# Bind("ciudad") %>' CssClass="textbox"
                                                        Width="120px" Enabled="false" /></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dirección"><ItemTemplate><asp:TextBox ID="lblDireccion" runat="server" Text='<%# Bind("direccion") %>' CssClass="textbox"
                                                        Width="170px" Enabled="false" /></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Teléfono"><ItemTemplate><asp:TextBox ID="lbltelefono" runat="server" Text='<%# Bind("telefono") %>' CssClass="textbox"
                                                        Width="80px" Enabled="false" /></ItemTemplate><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Principal"><ItemTemplate><cc1:CheckBoxGrid ID="chkPrincipal" runat="server" Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                                        CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" OnCheckedChanged="chkPrincipal_CheckedChanged" /></ItemTemplate><ItemStyle HorizontalAlign="center" /></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conjunción"><ItemTemplate><asp:Label ID="lblConjuncion" runat="server" Text='<%# Eval("conjuncion")  %>' Visible="false" /><cc1:DropDownListGrid ID="ddlConjuncion" runat="server" CssClass="textbox" CommandArgument='<%#Container.DataItemIndex %>'
                                                        Width="120px" /></ItemTemplate><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: left; width:500px" colspan="2" Enabled="false">
                            <td style="text-align: left; width: 170px; vertical-align:top">
                                Tipo Tasa Interes<br />
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                    <asp:ListItem Value="1" Enabled="false">fijo</asp:ListItem>
                                    <asp:ListItem Value="2" Enabled="false">variable</asp:ListItem>
                                </asp:RadioButtonList>

                            <td style="text-align: left; width: 170px; vertical-align:top">
                                Modalidad Int<br />
                                <asp:RadioButtonList ID="rblModalidadInt" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                    <asp:ListItem Value="1" Enabled="false">Vencido</asp:ListItem>
                                    <asp:ListItem Value="2" Enabled="false">Anticipado</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            
                        </tr>                        
                        <tr>
                                                     <td style="width: 180px; text-align: left">
                Tasa Interes<br />
                <asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="90%"  enabled="false"/>
            </td>
                            <td style="text-align: left; width: 350px;">
                                Periodicidad Intereses<br />
                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="80%" Enabled="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" />
                                <asp:TextBox ID="txtDiasValida" runat="server" CssClass="textbox" Visible="False" />
                            </td>
                            
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers><asp:PostBackTrigger ControlID="ddlPeriodicidad" /></Triggers>
            </asp:UpdatePanel>                 
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Apertura del CDAT
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente 
                            <br />
                            <br />
                            <asp:Button ID="btnImprime" runat="server" Text="Desea Imprimir ?" 
                                CssClass="btn8" onclick="btnImprime_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                        </td>
                </tr>
                 <asp:Button ID="Button1" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                    Text="Imprimir" OnClick="btnImprime_Click" />
                                    </td>
                    <td> 
                <tr>
                <td>
                     
                                       
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%"><LocalReport ReportPath="Page\CDATS\Apertura\rptApertura.rdlc"><DataSources><rsweb:ReportDataSource /></DataSources></LocalReport></rsweb:ReportViewer>                    
                    </td>
                   
                                     <tr>
                        <td>
                            <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                                runat="server" style="border-style: dotted; float: left;"></iframe>
                        </td>
                    </tr>
                </tr>
            </table>
        </asp:View>
        </asp:MultiView>




    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
