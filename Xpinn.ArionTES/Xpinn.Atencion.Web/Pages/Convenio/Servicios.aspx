<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Servicios.aspx.cs" Inherits="Servicios" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="ctlFecha" TagPrefix="ctl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>

        .accordion-titulo {
            position: relative;
            display: block;
            padding: 4px;
            font-size: 16px;
            font-weight: 300;
            /* background: #0099FF; */
            color: #7b7b7b;
            text-decoration: none;
            text-align: center;
            margin-bottom: 2px;
            /*border-top: 1px solid #b5b0b0;*/
            border-bottom: 1px solid #b5b0b0;
        }

        .accordion-titulo:hover, .accordion-titulo:active,.accordion-titulo:focus {
            color: #808080;
        }

        .centrar {
            text-align: center;
            margin-left: auto;
            margin-right: auto;
            align-content: center;
            align-items: center;
            align-self: center;
        }

    </style>
    <script type="text/javascript">
        function ocultarMostrarPanel(id) {
            var panel = document.getElementById(id);
            //if ($("#" + id).is(":visible")) {
            //    $("#" + id).hide();
            //} else {
            //    $("#" + id).show();
            //}
            panel.style.display = (panel.style.display == 'none') ? 'block' : 'none';
        }
    </script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }

        function ocultarMostrarPanel(prueba) {
            var panel = document.getElementById(prueba);
            panel.style.display = (panel.style.display == 'none') ? 'block' : 'none';
        }        
    </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Panel runat="server" ID="pnlCards" Visible="true">

        <div style="text-align: center">
            <div style="text-align: left; margin: auto auto;">
                <div class="row justify-content-md-center">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-3">
                        <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-control">
                            <asp:ListItem Value="0" Selected="True">Todas</asp:ListItem>
                            <asp:ListItem Value="1">Medicina prepagada</asp:ListItem>
                            <asp:ListItem Value="2">Planes exequiales</asp:ListItem>
                            <asp:ListItem Value="3">Seguros</asp:ListItem>
                            <asp:ListItem Value="4">Otros</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-3">
                        <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-info" style="height: 30px;" Text="consultar" OnClick="btnConsultar_Click" />
                    </div>
                    <div class="col-sm-1"></div>
                </div>
            </div>
        </div>
        <hr />
    </asp:Panel>

    <div style="text-align: center">
        <asp:Panel runat="server" ID="pnlinfo">
            
        </asp:Panel>

        <asp:Panel ID="pnlDatos" Visible="false" runat="server" Style="text-align: left; margin: auto auto" Width="50%">
            <div class="modal-content">
                <div class="col-sm-12 container">
                    <br />
                    <center>
                    <asp:Label runat="server" ID="lblError" Font-Size="Small" Visible="false" Font-Bold="true"  class="modal-title text-primary" style="color:red" ></asp:Label>
                    </center>
                    <hr style="width: 100%" />
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtLinea" Enabled="false" Visible="false" runat="server" CssClass="form-control" />
                        <br />
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-4 text-left">
                        Línea de servicio
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtServicio" Enabled="false" runat="server" CssClass="form-control" />
                        <br />
                    </div>
                </div>
                <div class="col-sm-12" style="display: none;">
                    <div class="col-sm-4 text-left">
                        Fecha servicio
                    </div>
                    <div class="col-sm-8">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <ctl:ctlfecha runat="server" id="txtFechaServicio" Width_="100%" Enabled="false" />
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-4 text-left">
                        Valor
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtValor" onkeypress="return ValidNum(event);" onblur="valorCambio(event)" CssClass="form-control" runat="server"></asp:TextBox>
                        <br />
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-4 text-left">
                        Valor Máximo
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtValorMax" runat="server" Width_="100%" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <br />
                    </div>
                </div>
                <div class="col-sm-12" style="display: none;">
                    <div class="col-sm-4 text-left">
                        Fecha primera cuota
                    </div>
                    <div class="col-sm-8">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <ctl:ctlfecha runat="server" id="txtFechaPago" Width_="100%" />
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-4 text-left">
                        Numero cuotas
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtNumeroCuotas" onkeypress="return ValidNum(event);" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                </div> 
                <div class="col-sm-12">
                    <div class="col-sm-4 text-left">
                        Plazo máximo (meses)
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPlazoMax" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                </div>                
                <div class="col-sm-12">
                    <div class="col-sm-4 text-left">
                        Descripción
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" CssClass="form-control" Rows="2" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                </div>                
                <div class="modal-footer">
                    <br />
                    <asp:Button ID="btnGuardar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                    &nbsp &nbsp
                    <asp:Button ID="btnCerrarCambioCuota" CssClass="btn btn-primary" runat="server" Text="Cancelar" Style="padding: 3px 15px; width: 110px" OnClick="btnCerrarCambioCuota_Click" />
                </div>
            </div>            
        </asp:Panel>        
        <asp:Panel runat="server" ID="pnlFinal" Visible="false">
            <div class="row">
                <div class="col-sm-12 text-center">
                    <asp:Label ID="lblFinal" runat="server" Text="Su solicitud se generó correctamente con el código: "
                            Style="color: #66757f; font-size: 28px;" />
                        <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red; font-size: 28px;" />
                    
                    <br />
                    <br />
                    <asp:Button ID="btnVolver" CssClass="btn btn-primary" runat="server" Text="Volver" Style="padding: 3px 15px; width: 110px; margin: 0 auto" OnClick="btnCerrarCambioCuota_Click" />
                </div>
            </div>
        </asp:Panel>
            </div>
</asp:Content>

<%-- "~/Imagenes/LogoInterna.png --%>