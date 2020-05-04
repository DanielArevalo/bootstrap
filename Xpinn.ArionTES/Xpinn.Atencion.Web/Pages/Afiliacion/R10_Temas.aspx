<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R10_Temas.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <%-- SECCION TEMAS --%>
                        <div onclick="ocultarMostrarPanel('temas')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">13. Temas de inter&eacute;s<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="temas">
                            <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <label for="cbTemas" style="margin: 25px 20px;">¿Sobre qué temas le gustaría recibir información de <strong runat="server" id="empresa"></strong>?</label>
                                    <br />                                    
                                    <asp:CheckBoxList ID="cbTemas" runat="server" CssClass="form-control" RepeatColumns="3" BorderStyle="None"></asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-sm-12">
                                    <label for="txtOtroTema">¿Otro?</label>
                                    <asp:TextBox ID="txtOtroTema" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION TEMAS --%>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R09_Informacion.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

