<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="AseEjecutivosNuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register Src="../../../General/Controles/direccion.ascx" TagName="direccion"
    TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="width: 212px; text-align:left">
                Tipo Documento<br />
                <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="dropdown" 
                    OnSelectedIndexChanged="ddlTipoDoc_SelectedIndexChanged" Width="85%" 
                    Height="25px">
                </asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvDdlTipoDoc" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="ddlTipoDoc" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" Width="307px" /><br /><br />
            </td>
            <td style="text-align:left; width: 282px;">
                Número Documento<br />
                <asp:TextBox ID="txtNumeDoc" runat="server" CssClass="textbox" MaxLength="12" 
                    Width="50%" AutoPostBack="True" ontextchanged="txtNumeDoc_TextChanged"></asp:TextBox><br />
                <asp:Label ID="Labelerror" runat="server" 
                    style="color: #FF3300; font-weight: 700" Font-Size="X-Small"></asp:Label><br />
                <asp:RequiredFieldValidator ID="rfvNumeDoc" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtNumeDoc" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" Font-Size="X-Small" />
                <asp:RegularExpressionValidator ID="revNumeDoc" runat="server" ValidationExpression="^\d+$"
                    ErrorMessage="Solo Numeros" ControlToValidate="txtNumeDoc" ValidationGroup="vgGuardar"
                    ForeColor="Red" Font-Size="X-Small"></asp:RegularExpressionValidator>
            </td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left">
                Primer Nombre<br />
                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPrimerNombre" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtPrimerNombre" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
            </td>
            <td style="text-align:left" colspan="2">
                Segundo Nombre<br />
                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Primer Apellido<br />
                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPrimerApellido" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtPrimerApellido" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
            </td>
            <td style="text-align:left" colspan="2">
                Segundo Apellido<br />
                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Teléfono Residencia<br />
                <asp:TextBox ID="txtTeleResi" runat="server" CssClass="textbox" MaxLength="12" 
                    Width="300px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revTeleResi" runat="server" ValidationExpression="^\d+$"
                    Display="Dynamic" ErrorMessage="Solo Números" ControlToValidate="txtTeleResi"
                    ValidationGroup="vgGuardar" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
            <td style="text-align:left" colspan="2">
                Teléfono Celular<br />
                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="12" 
                    Width="300px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revTeleCel" runat="server" ValidationExpression="^\d+$"
                    Display="Dynamic" ErrorMessage="Solo Números" ControlToValidate="txtCelular"
                    ValidationGroup="vgGuardar" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align:left">
                Dirección de Residencia
                <uc1:direccion ID="txtDirCorrespondencia" runat="server" aling="center" Width="50%" />
            </td>
        </tr>
    </table>
    <table style="text-align:left">
        <tr>
            <td style="text-align:left;vertical-align:top">
                Zona<br />
                <asp:TextBox ID="txtZonas" CssClass="textbox" runat="server"  Width="293px" ReadOnly="True" Style="text-align: right" TEXTMODE="SingleLine"></asp:TextBox>
                <asp:PopupControlExtender ID="txtZonas_PopupControlExtender" runat="server"
                    Enabled="True" ExtenderControlID="" TargetControlID="txtZonas"
                    PopupControlID="panelZonas" OffsetY="22">
                </asp:PopupControlExtender>
                <asp:Panel ID="panelZonas" runat="server" Height="200px" Width="300px"
                    BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                    ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                    <asp:GridView ID="gvZonas" runat="server" Width="100%" AutoGenerateColumns="False"
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="CODCIUDAD">
                        <Columns>
                            <asp:TemplateField HeaderText="Código">
                                <itemtemplate>
                            <asp:Label ID="lbl_zona" runat="server" Text='<%# Bind("CODCIUDAD") %>'></asp:Label>
                            </itemtemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción">
                                    <itemtemplate>
                            <asp:Label ID="lbl_nom_zona" runat="server" Text='<%# Bind("NOMCIUDAD") %>'></asp:Label>
                            </itemtemplate>                                                        
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">   
                                <ItemTemplate>
                                    <cc1:CheckBoxGrid ID="cbListadoZ" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                    AutoPostBack="false" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>  
                    </asp:GridView>
                </asp:Panel>
            </td>
            <td style="text-align:left;vertical-align:top">
                Barrio<br />
                        <asp:TextBox ID="txtBarrios" CssClass="textbox" runat="server"  Width="293px" ReadOnly="True" Style="text-align: right" TEXTMODE="SingleLine"></asp:TextBox>
                        <asp:PopupControlExtender ID="txtBarrios_PopupControlExtender" runat="server"
                            Enabled="True" ExtenderControlID="" TargetControlID="txtBarrios"
                            PopupControlID="panelLista" OffsetY="22">
                        </asp:PopupControlExtender>
                        <asp:Panel ID="panelLista" runat="server" Height="200px" Width="300px"
                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                            ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                            <asp:GridView ID="gvBarrios" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="CODCIUDAD">
                                <Columns>
                                    <asp:TemplateField HeaderText="Código">
                                        <itemtemplate>
                                    <asp:Label ID="lbl_barrio" runat="server" Text='<%# Bind("CODCIUDAD") %>'></asp:Label>
                                    </itemtemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                            <itemtemplate>
                                    <asp:Label ID="lbl_nom_ciudad" runat="server" Text='<%# Bind("NOMCIUDAD") %>'></asp:Label>
                                    </itemtemplate>                                                        
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">   
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                            AutoPostBack="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>  
                            </asp:GridView>
                        </asp:Panel>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align:left" >
                Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="dropdown"  
                    Width="330px" Height="25px">
                </asp:DropDownList><br />
                <asp:Label ID="Labelerroroficina" runat="server" style="color: #FF3300; font-weight: 700" 
                    Text=""></asp:Label>
            </td>
            <td style="text-align:left">
                Correo Electrónico<br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="330px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Correo Invalido"
                    ValidationGroup="vgGuardar" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic"
                    ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|co|es|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\b"> </asp:RegularExpressionValidator> 
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Fecha Ingreso<br />
                <ucFecha:fecha ID="ucFecha" runat="server" />
            </td>
            <td style="text-align:left">
                Estado<br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown" Width="330px" 
                    Height="25px">
                </asp:DropDownList><br />
            </td>
        </tr>
    </table>
</asp:Content>
