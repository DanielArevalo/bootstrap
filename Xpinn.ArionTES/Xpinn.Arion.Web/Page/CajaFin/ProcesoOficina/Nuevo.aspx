<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () { 
        $("#chkNuevoEstado").change(function ()
        {
            if ($(this).is(":checked")) {
                $("#pDatos").show();
            }
            else
            {
                $("#pDatos").hide()show();
            }
        });
        });
     </script>
    <br /><br /><br />    
    <table cellpadding="5" cellspacing="0" style="width: 81%; text-align:left">
        <tr>
            <td colspan="3">
                <strong>Estado Actual de la Oficina</strong>
                <strong __designer:mapid="193a">
                <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td width="10%">
                Oficina &#160;&#160;<br/>
                <asp:DropDownList ID="ddlOficinas" CssClass="dropdown"  runat="server" 
                    Height="24px" Width="180px" AutoPostBack="True" Enabled="False"
                    onselectedindexchanged="ddlOficinas_SelectedIndexChanged">
                </asp:DropDownList> 
            </td>
            <td width="30%">
                Encargado <br/>
                <asp:TextBox ID="txtEncargado" runat="server" Enabled="False" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 227px">
                Fecha Real<br/>
                <asp:TextBox ID="txtFechaReal" enabled="false" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="158px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Estado<br/>  
                <asp:TextBox ID="txtEstadoAct" Width="172px" enabled="false" runat="server" 
                    CssClass="textbox"></asp:TextBox>
            </td>   
            <td>
                Fecha Estado<br/>
                <asp:TextBox ID="txtFechaProceso" enabled="false" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="158px"></asp:TextBox><br/>
                <asp:RequiredFieldValidator ID="rfvFechaProceso" 
                    runat="server" ErrorMessage="No se ha determinado fecha de proceso de la oficina" ControlToValidate="txtFechaProceso" 
                    Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" 
                    style="font-size: x-small"/>
            </td>   
            <td valign="top" style="width: 227px">
                Horario<br/>
                <asp:DropDownList ID="ddlTipoHorario" CssClass="dropdown"  runat="server" Enabled="false"
                    Height="24px" Width="182px">
                    <asp:ListItem Value="1">Normal</asp:ListItem>
                    <asp:ListItem Value="2">Adicional</asp:ListItem>
                </asp:DropDownList>    
            </td>
        </tr>
    </table>
    <hr />
    <table cellpadding="5" cellspacing="0" style="width: 70%; text-align:left">
        <tr>
            <td style="width: 180px">
                Nuevo Estado <br/>
                <asp:CheckBox ID="chkNuevoEstado" ClientIDMode="Static" runat="server" />
            </td>
             <td>
                 Fecha Nuevo Estado<br/>
                <asp:TextBox ID="tbxFechaNuevoProceso" enabled="false" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="120px"></asp:TextBox>
            </td>
            <td style="width: 192px">
                Horario Nuevo&nbsp; &#160;&#160;<br/>
                <asp:DropDownList ID="ddlTipoHorarioNuevo" CssClass="textbox"  runat="server" Width="182px">
                    <asp:ListItem Value="1">Normal</asp:ListItem>
                    <asp:ListItem Value="2">Adicional</asp:ListItem>
                </asp:DropDownList>    
            </td>
            <td>
                <asp:Label ID="lblAperturo" runat="server" Text="Usuario Aperturó"
                    Enabled="False" Width="200px"></asp:Label><br/>
                <asp:TextBox ID="txtAperturo" runat="server" 
                    Enabled="False" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
             <td>
                 &nbsp;</td>
            <td style="width: 192px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    <asp:Panel ClientIDMode="Static" ID="pDatos" runat="server">                                       
                <table style="width: 100%"> 
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="False"  OnRowDataBound="gvLista_RowDataBound"  Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"   >
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco" HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ClientIDMode="Static" ID="chkSeleccionar" runat="server" Checked="true"/>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        <ItemStyle CssClass="gridIco"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="cod_caja" HeaderText="Código" >
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_creacion" HeaderText="Fecha Creacion" DataFormatString="{0:d}" >
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>             
                                    <asp:BoundField DataField="estado" HeaderText="Estado" Visible="true">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

</asp:Content>