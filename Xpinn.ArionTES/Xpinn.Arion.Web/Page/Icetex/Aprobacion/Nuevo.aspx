<%@ Page Title=".: Aprobación Icetex :." MaintainScrollPositionOnPostback="true"
    Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <table width="100%">
            <tr>
                <td colspan="2" style="text-align: left">
                    Fecha de Aprobación<br />
                    <ucFecha:fecha ID="txtFecha" runat="server" Enabled="false" />
                    <asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblCod_convocatoria" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblCod_persona" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblEstado" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblTipoAprobacion" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="hdOperacionIcetex" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 50%">
                    <strong>Datos</strong>
                </td>
                <td style="text-align: center; width: 50%">
                    <strong>Vizualización de Documento</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 60%">
                    <div style="overflow: scroll; max-height: 350px;">
                        <asp:UpdatePanel ID="updBeneficiario" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDatos" runat="server" AllowPaging="False" AutoGenerateColumns="false"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    DataKeyNames="Tipo_Dato,Valor" ForeColor="Black" GridLines="Horizontal" Width="100%" OnRowDataBound="gvDatos_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Tipo_Dato" HeaderText="Datos">
                                            <ItemStyle HorizontalAlign="left" Width="180px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Aprobado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Bind("Valor") %>' Visible="false" CssClass="textbox" Width="180px"/>
                                                <ucFecha:fecha ID="txtctlfecha" runat="server" Enabled="True" habilitado="True"
                                                    style="font-size: xx-small; text-align: left" Text='<%# Eval("Valor", "{0:d}") %>'
                                                    tipoletra="xx-Small" Visible="false" />
                                                <asp:Label ID="lblDropdown" runat="server" Text='<%# Bind("Valor") %>' Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid
                                                    ID="ddlInfoDrop" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>"
                                                    CssClass="textbox" Style="font-size: xx-small; text-align: left" Visible="false" Width="190px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aprobado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkAprobado" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkAprobado_CheckedChanged" Checked="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aplazado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkAplazado" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkAplazado_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkNegado" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkNegado_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="130px"/> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td rowspan="2" style="vertical-align: top; text-align: center; width: 40%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltReport" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvDocumentos" EventName="RowEditing" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <strong>Documentos</strong>
                            <div style="overflow: scroll; max-height: 350px;">
                                <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" DataKeyNames="codigo,descripcion"
                                    ForeColor="Black" Width="100%" OnRowEditing="gvDocumentos_RowEditing">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_detall.jpg" ShowEditButton="True" />
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" Visible="false">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Datos">
                                            <ItemStyle HorizontalAlign="left" Width="200px"/>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Aprobado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkAprobadoDoc" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkAprobadoDoc_CheckedChanged" Checked="true"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="95px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aplazado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkAplazadoDoc" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkAplazadoDoc_CheckedChanged"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="95px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkNegadoDoc" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkNegadoDoc_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="95px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuArchivo" runat="server" Width="130px"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="130px"/> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table width="90%">
            <tr>
                <td colspan="2" style="text-align: left;">
                    <strong>Concepto</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 75%">
                    Observaciones<br />
                    <asp:TextBox ID="txtObservacion" runat="server" Width="95%" TextMode="MultiLine" Height="50px"/>
                </td>
                <td style="text-align: left; width: 25%; vertical-align: top">
                    Estado<br />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtEstado" runat="server" Width="90%" Enabled="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Documentos Soporte<br />
                    <asp:FileUpload ID="fuDocumento" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelFinal" runat="server">
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
                <td style="text-align: center; font-size: large; padding:15px">
                    Datos aprobados correctamente
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnCorreo" runat="server" Text="Enviar Correo al Asociado" 
                        onclick="btnCorreo_Click"  />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
