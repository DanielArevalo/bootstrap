<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 92%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;" colspan="2">
                    Número Aporte
                    <br />
                    <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="151px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Identificación<br />
                    <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" 
                        Width="157px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; ">
                    Fecha Apertura&nbsp;&nbsp;<br />
                    <asp:TextBox ID="txtFecha_apertura" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" 
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_apertura">
                    </asp:CalendarExtender>
                    <br />
                    <asp:CompareValidator ID="cvFecha_apertura" runat="server" 
                        ControlToValidate="txtFecha_apertura" Display="Dynamic" 
                        ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" style="font-size: xx-small" 
                        ToolTip="Formato fecha" ValidationGroup="vgGuardar" Width="200px" />
                </td>
                <td style="text-align: left">
                    Fecha Vencimiento&nbsp;<br />
                    <asp:TextBox ID="txtFecha_vencimiento" runat="server" CssClass="textbox" 
                        MaxLength="128" />
                    <asp:CalendarExtender ID="txtFecha_vencimiento_CalendarExtender" runat="server" 
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_vencimiento">
                    </asp:CalendarExtender>
                    <br />
                    <asp:CompareValidator ID="cvFecha_vencimiento" runat="server" 
                        ControlToValidate="txtFecha_vencimiento" Display="Dynamic" 
                        ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" style="font-size: xx-small" 
                        ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Width="200px" />
                </td>       
                <td class="tdI" style="text-align: left">
                    Estado<br />
                    <asp:DropDownList ID="DdlEstado" runat="server" CssClass="textbox" Width="154px">
                        <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                        <asp:ListItem Value="1">Activo</asp:ListItem>
                        <asp:ListItem Value="2">Inactivo</asp:ListItem>
                        <asp:ListItem Value="3">Cerrado</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdI" style="text-align: left">
                    Ordenado Por
                    <br />
                    <asp:DropDownList ID="DdlOrdenadorpor" runat="server" CssClass="textbox" 
                        Width="154px" AutoPostBack="True" 
                        onselectedindexchanged="DdlOrdenadorpor_SelectedIndexChanged">
                        <asp:ListItem Value="Numero_Aporte">NUMEROCUENTA</asp:ListItem>
                        <asp:ListItem Value="NOM_LINEA_APORTE">LINEA</asp:ListItem>
                        <asp:ListItem>IDENTIFICACION</asp:ListItem>
                        <asp:ListItem Value="NOMBRE">NOMBRES</asp:ListItem>
                        <asp:ListItem Value="FECHA_PROXIMO_PAGO">FECHAVENCIMIENTO</asp:ListItem>
                        <asp:ListItem>ESTADO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_aporte">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" 
                                    ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_aporte" HeaderText="Num. Aporte" >
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_apertura" HeaderText="Fec. Apertura" 
                            DataFormatString="{0:d}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo" HeaderText="Saldo Total" />
                        <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" />
                        <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx. Pago" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
</asp:Content>
