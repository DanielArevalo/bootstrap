<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R05_Pep.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <%-- SECCION PEP --%>
                        <div onclick="ocultarMostrarPanel('pep')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">4. Declaraci&oacute;n de persona expuesta p&uacute;blicamente (PEP)<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="pep">
                            <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
                            <br />
                            Usted desempeña en la actualidad o ha desempeñado en los &uacute;ltimos veinticuatro (24) meses cargos o actividades en los cuales:<br />
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-8 col-lg-9">
                                    <label runat="server">¿Maneje recursos públicos o tengan poder de disposición sobre éstos?</label>
                                </div>
                                <div class="col-sm-12 col-lg-3 col-md-4">
                                    <asp:RadioButtonList ID="ChkRecursosPublicos" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <br />
                                <div class="col-sm-12 col-md-8 col-lg-9">
                                    <label runat="server">¿Tiene o goza de reconocimiento público?</label>
                                </div>
                                <div class="col-sm-12 col-lg-3 col-md-4">
                                    <asp:RadioButtonList ID="ChkPeps" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <br />
                                <div class="col-sm-12 col-md-8 col-lg-9">
                                    <label runat="server">¿tiene algun grado de poder público o desempeña una función pública prominente o destacada en el estado relacionada con alguno de los cargos descritos en el decreto 1674 de 2016?</label>
                                </div>
                                <div class="col-sm-12 col-lg-3 col-md-4">
                                    <asp:RadioButtonList ID="ChkFuncionPublica" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <br />
                                <div class="col-sm-12 col-md-8 col-lg-9">
                                    <label runat="server">¿Tiene familiares hasta el segundo grado de consanguinidad y afinidad que encajen en los escenarios descritos previamente?</label>
                                </div>
                                <div class="col-sm-12 col-lg-3 col-md-4">
                                    <asp:RadioButtonList ID="ChkFamiliares" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION PEP --%>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R04_Laboral.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

