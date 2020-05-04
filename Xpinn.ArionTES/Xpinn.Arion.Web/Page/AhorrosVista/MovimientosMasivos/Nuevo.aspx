<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlDestinacion.ascx" TagName="ddlDestinacion" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersona.ascx" TagName="ddlPersona" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server"> 


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
     <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">            
                <table cellpadding="5" cellspacing="0" style="width: 70%">
                    <tr>
                         <td style="height: 15px; text-align: left; width: 119px;">
                    Línea<br />
                    <asp:DropDownList ID="ddlLineaAhorro" runat="server" Width="358px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                    </tr>
                    <caption>
                  
                        <tr>
                            <td style="text-align: left;">
                                Fecha Operacion<br />
                                <ucFecha:fecha ID="Fecha1" runat="server" AutoPostBack="True" 
                                    CssClass="textbox" enable="false" MaxLength="1" ValidationGroup="vgGuardar" 
                                    Width="150px" />
                            </td>
                            <td style="text-align: left; width: 150px;">
                                Tipo Operacion<br />
                                <asp:RadioButtonList ID="rbTipoArchivo" runat="server" 
                                    RepeatDirection="Horizontal" Width="222px" 
                                    onselectedindexchanged="rbTipoArchivo_SelectedIndexChanged">
                                    <asp:ListItem Value=",">Depositos</asp:ListItem>
                                    <asp:ListItem Value="   ">Retiros</asp:ListItem>
                                </asp:RadioButtonList>

                                </td>
                                <td style="text-align: left; ">
                                    Valor<br />
                                    <asp:TextBox ID="TxtSaldo_Total" runat="server" CssClass="textbox" 
                                        DataFormatString="{0:n0}" enable="false" enabled="true" Width="100px"></asp:TextBox>
                                </td>
                            
                        </tr>
                    </caption>
                    
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" 
                                Text="+ Adicionar Detalle" OnClick="btnAgregar_Click" />
                            <br />
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                            <asp:GridView ID="gvDetMovs" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" AllowPaging="True"
                                BorderWidth="1px" CellPadding="0" ForeColor="Black" Style="font-size: xx-small"
                                OnRowDeleting="gvDetMovs_RowDeleting" DataKeyNames="numero_cuenta"
                                OnPageIndexChanging="gvDetMovs_PageIndexChanging" OnRowDataBound="gvDetMovs_RowDataBound"
                                OnSelectedIndexChanged="gvDetMovs_SelectedIndexChanged1">
                                <AlternatingRowStyle BackColor="" />
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                    <asp:BoundField DataField="numero_cuenta" HeaderText="Ncuenta" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Línea">
                                        <ItemTemplate>
                                            <asp:label ID="lblLinea"  AutoPostBack="True" runat="server" Text='<%# Eval("nom_linea") %>'
                                                Width="100px" Style="text-align: right;" ></asp:label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>    
                                           
                        
                                        <asp:TemplateField HeaderText="Oficina">
                                            <ItemTemplate>
                                                <asp:label ID="lbloficina"  AutoPostBack="True" runat="server" Text='<%# Eval("nom_oficina") %>'
                                                    Width="100px" Style="text-align: right;" ></asp:label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField> 

                                         <asp:TemplateField HeaderText="identificación">
                                        <ItemTemplate>
                                            <asp:label ID="lblidentificacion"  AutoPostBack="True" runat="server" Text='<%# Eval("identificacion") %>'
                                                Width="100px" Style="text-align: right;" ></asp:label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:label ID="lblnombre"  AutoPostBack="True" runat="server" Text='<%# Eval("nombres") %>'
                                                Width="100px" Style="text-align: right;" ></asp:label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField> 

                                         <asp:TemplateField HeaderText="Saldo Total">
                                            <ItemTemplate>
                                                <asp:label ID="lblsaldo_total" AutoPostBack="True" runat="server" Text='<%# Eval("saldo_total", "{0:n}") %>'
                                                    Width="100px" Style="text-align: right;"  ></asp:label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Center"  />
                                        </asp:TemplateField> 
                                               
                                        <asp:TemplateField HeaderText="Vr.Movimiento" >
                                            <ItemTemplate>
                                            <uc1:decimalesGridRow ID="vrTraslado" runat="server" Text='<%# Eval("V_Traslado") %>' style="text-align: right" 
                                            TipoLetra="XX-Small" Habilitado="True" AutoPostBack_="True"
                                            Enabled="True" Width_="80" />
                                                </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GMF" >
                                            <ItemTemplate>
                                            <uc1:decimalesGridRow ID="txtGMF"  runat="server" Text='<%# Eval("valor_gmf") %>'  style="text-align: right" 
                                            TipoLetra="XX-Small" Habilitado="True" AutoPostBack_="True"          
                                               Enabled="True" Width_="80" />

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>  
                                   
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>                               
                        <td style="text-align: right;" colspan="3">
                             <br /><asp:label Text="Valor Total"  style="text-align: right;" runat="server" CssClass="label" Enabled="false" enable="false"
                                Width="200px"></asp:label>
                        </td>
                         <td style="text-align: right;">
                             <br /><asp:label ID="lblvalor_total"  style="text-align: right;" runat="server" Text='<%# Eval("valor_total", "{0:n}") %>'
                             Width="100px"></asp:label>
                        </td>
                         <td style="text-align: right; ">
                             <br /><asp:label ID="txtsaldo_igual"   style="text-align: right;" 
                                 runat="server" 
                                Width="100px"></asp:label>
                        </td>
                    </tr>
                </table>       
            </asp:Panel>    
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
                                Text="Datos Grabados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>      
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>