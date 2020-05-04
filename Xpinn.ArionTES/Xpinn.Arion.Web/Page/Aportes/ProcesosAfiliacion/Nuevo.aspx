<%@ Page Title=".: Proceso Afiliación :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton runat="server" ID="btnSiguiente" ImageUrl="../../../Images/btnGuardar.jpg" OnClick="btnSiguiente_Click" Visible="false" style="margin-left: 624px;" />

    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvProcesos"> 
        <asp:View ID="ViewAfilados" runat="server">
            <table>
                <tr>
                    <td>
                        <strong>Datos del Asociado</strong>
                    </td>
                </tr>
            </table>
            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
        </asp:View>
        <asp:View ID="ViewRegistro" runat="server">
            <asp:Panel runat="server" Enabled="true" ID="pDatosProceso">
                <table style="width: 100%">
                    
                    <tr>
                        <td style="text-align: left; width: 200px">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" Width="140px" CssClass="textbox" MaxLength="15" Enabled="false" />
                            <asp:HiddenField ID="hdCod_Persona" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdCod_Proceso" runat="server"></asp:HiddenField>
                        </td>
                        <td style="text-align: left; width: 150px">Tipo Identificación<br />
                            <asp:DropDownList ID="ddlTipo_ID" runat="server" CssClass="textbox" Width="140px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 350px">Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="300px" MaxLength="200" Enabled="false">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblLugar" runat="server" Text="Lugar Entrevista" /><br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="140px"></asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha Entrevista" /><br />
                            <uc1:fecha ID="txtFecha" runat="server"></uc1:fecha>
                        </td>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblFuncionario" runat="server" Text="Funcionario" /><br />
                            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textbox" Width="300px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 200px">Concepto<br />
                            <asp:RadioButtonList ID="rbConcepto" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                        </td>                        
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblActa" runat="server" Text="Número Acta" /><br />
                            <asp:TextBox ID="txtNumActa" runat="server" CssClass="textbox" Width="140px" MaxLength="15"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblFecha1" runat="server" Text="Fecha de Verificación" Visible="false" /><br />
                            <uc1:fecha ID="txtFecha1" runat="server" Visible="false"></uc1:fecha>
                        </td>                        
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblCoincidencias" runat="server" Text="Coincidencias" /><br />
                            <asp:RadioButtonList ID="rbCoincidencias" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                        </td>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblConsultaListas" runat="server" Text="El asociado fue consultado en listas restrictivas" /><br />
                            <asp:RadioButtonList ID="rbConsulta_Asociado" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                        </td>
                        <td style="text-align: left; width: 200px">
                            <asp:Label ID="lblDocumentos" runat="server" Text="El asociado anexó los documentos requeridos" /><br />
                            <asp:RadioButtonList ID="rbDocumentos_Asociado" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" /><br />
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Style="resize: none; overflow: hidden; height: 40px; width: 400px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />
</asp:Content>

