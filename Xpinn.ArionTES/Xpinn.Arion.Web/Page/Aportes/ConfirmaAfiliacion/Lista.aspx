<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Confirmar Afiliación :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript">
        function Consultar(btnConsultar_Click) {
            var obj = document.getElementById("btnConsultar_Click");
            if (obj){
            obj.click();                  
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width: 80%;">
                    <tr>
                        <td style="text-align: left; font-size: x-small" colspan="5">
                            <strong>Criterios de Búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px"/>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtIdentificacion"
                                Display="Dynamic" ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Style="font-size: x-small" Type="Integer" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left">
                            Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="250px" />
                        </td>
                        <td style="text-align: left">
                            Apellidos<br />
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="250px" />
                        </td>
                        <td style="text-align: left">
                            Ciudad :<br />
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Width="180px"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">Solicitudes sin asesor<br />
                            <asp:CheckBox runat="server" ID="chkSinAsesor" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <asp:Panel ID="pDatos" runat="server">                                        
                <table style="width: 100%"> 
                    <tr>
                        <td style="font-size: x-small;text-align:left">
                            <strong>Listado de Personas pendientes por Afiliarse</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="id_persona, identificacion" 
                                OnRowDeleting="gvLista_RowDeleting" onrowediting="gvLista_RowEditing">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" 
                                        ShowDeleteButton="True" DeleteImageUrl="~/Images/gr_elim.jpg" />
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" 
                                        ShowEditButton="True" />                                   
                                    <asp:BoundField DataField="id_persona" HeaderText="Código">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_creacion" HeaderText="Fecha Creación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_tipo_identificacion" HeaderText="Tipo Identifi">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_ciudad" HeaderText="Nombre Ciudad">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion" HeaderText="Dirección" >
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_ciudadempresa" HeaderText="Ciudad Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion_empresa" HeaderText="Dirección Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefono_empresa" HeaderText="Telefono Laboral">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="email" HeaderText="Email">
                                        <ItemStyle HorizontalAlign="left" />
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
            <center>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado." /></center>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;" >                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Registros modificados correctamente"></asp:Label><br />                           
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
        </asp:View>    
    </asp:MultiView>    
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
