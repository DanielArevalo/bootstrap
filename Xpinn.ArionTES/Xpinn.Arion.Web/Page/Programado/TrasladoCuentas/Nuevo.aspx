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
                        <td style="text-align: left; " colspan= "5">
                            Fecha Traslado<br />
                            <ucFecha:fecha ID="txtFechaCambio" runat="server" AutoPostBack="True" 
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; ">
                            Numero De Cuenta<br />
                            <asp:TextBox ID="TxtNumeroCuenta" runat="server" CssClass="textbox" Width="150px" 
                                  enable="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            Fecha Apertura<br />
                            <ucFecha:fecha ID="Fecha1" runat="server" AutoPostBack="True" 
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="150px" enable="false"/>
                        </td>
                        <td style="text-align: left; ">
                            Línea<br />
                            <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" enabled="false" enable="false" 
                                Width="100px" ></asp:TextBox>                   
                       </td>
                        <td style="text-align: left; ">
                            Nombre Línea<br />
                            <asp:TextBox ID="txtNombreLinea" runat="server" CssClass="textbox" Enabled="false" enable="false"
                                Width="180px"></asp:TextBox>                    
                        </td>
                         <td style="text-align: left; ">
                           Saldo Total<br />
                            <asp:TextBox ID="TxtSaldo_Total"  runat="server" CssClass="textbox" enabled="false" enable="false"
                                Width="100px" DataFormatString="{0:n0}" ></asp:TextBox>                   
                       </td>
                    </tr>               
                    <tr>
                        <td style="text-align: left;">
                            Identificacion<br /> 
                            <asp:TextBox ID="Txtiden" runat="server" 
                                CssClass="textbox" Enabled="false" enable="false"
                                Width="150px"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            Tipo de Ident.<br />
                            <asp:TextBox ID="TxtIdentif" runat="server" CssClass="textbox" Enabled="false" venable="false"
                                Width="150px"></asp:TextBox>                    
                        </td>
                        <td style="text-align: left; " colspan="2">
                            Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" 
                                Width="305px" enable="false"></asp:TextBox>                    
                        </td>
                        <td style="text-align: left; " >
                           Plazo<br />
                            <asp:TextBox   ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" Width="100px" enable="false" DataFormatString="{0:n0}" ></asp:TextBox>                    
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" 
                                Text="+ Adicionar Detalle" OnClick="btnAgregar_Click" />
                            <br />
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                            <asp:GridView ID="gvDetMovs" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                BorderWidth="1px" CellPadding="0" ForeColor="Black" Style="font-size: xx-small"
                                OnRowDeleting="gvDetMovs_RowDeleting" DataKeyNames="numero_cuenta"
                                OnRowDataBound="gvDetMovs_RowDataBound"
                                OnSelectedIndexChanged="gvDetMovs_SelectedIndexChanged1">
                                <AlternatingRowStyle BackColor="" />
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
        
                                    <asp:TemplateField HeaderText="N.Cuenta">
                                        <ItemTemplate>
                                            <cc1:TextBoxGrid ID="txtNumeroCuenta"  AutoPostBack="True" runat="server" Text='<%# Eval("numero_cuenta") %>'
                                                Width="100px" OnTextChanged="txtNumCuenta_TextChanged" CommandArgument='<%#Container.DataItemIndex %>'
                                                Style="text-align: right;" />
                                                 <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtNumeroCuenta" ValidChars="" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>     

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
                                               
                                        <asp:TemplateField HeaderText="Vr.Traslado" >
                                            <ItemTemplate>
                                            <uc1:decimalesGridRow ID="txtTraslado" runat="server" Text='<%# Eval("V_Traslado") %>' style="text-align: right" 
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
                    <tr> 
                        <td style="text-align: left;" colspan="5">
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
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
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>

                     <tr>  
            <td>

            
                               
                 
            </td>
        </tr>        
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>