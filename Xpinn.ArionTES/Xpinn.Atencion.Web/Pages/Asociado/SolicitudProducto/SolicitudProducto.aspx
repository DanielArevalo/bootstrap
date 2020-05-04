<%@ Page Title=".: Solicitud de Producto :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SolicitudProducto.aspx.cs" Inherits="SolicitudProducto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .cssClass1 {
            background: Red;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass2 {
            background: Gray;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass3 {
            background: orange;
            color: black;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass4 {
            background: blue;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass5 {
            background: Green;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .BarBorder {
            border-style: solid;
            border-width: 1px;
            width: 180px;
            padding: 2px;
        }

        .auto-style1 {
            left: -446px;
            top: -870px;
        }
    </style>
    <script type="text/javascript">
        // Solo permite ingresar numeros.
        function isNumber(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }

        function EvitarClickeoLocos() {
            if (contadorClickGuardar == 0) {
                contadorClickGuardar += 1;
                return true;
            }
            return false;
        }

        var contadorClickGuardar = 0;
        $(document).ready(function () {
            $("#btnPse").click(EvitarClickeoLocos);
        });
    </script>
    <div class="form-group">
        <asp:Panel ID="panelGeneral" runat="server">
            <asp:Label ID="txtCodPersona" runat="server" Visible="false" />
            <asp:Label ID="txtCambio" runat="server" Visible="false" />
            <asp:FormView ID="frvData" runat="server" Width="100%" OnDataBound="frvData_DataBound" Visible="false">
                <ItemTemplate>
                    <table class="col-sm-12 tableNormal">
                        <tr>
                            <td>
                                <h5 class="text-primary text-left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Información del Asociado</h5>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Identificación
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="txtIdentificacion" runat="server" Text='<%# Eval("NumeroDocumento") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Nombre
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="txtNombre" runat="server" Text='<%# Eval("PrimerNombre") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Ciudad
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="txtCiudad" runat="server" Text='<%# Eval("SegundoNombre") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Dirección
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="txtDireccion" runat="server" Text='<%# Eval("Direccion") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Fecha Afiliación
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="lblFechaAfiliacion" runat="server" Text='<%# Eval("FechaAfiliacion", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Tipo cliente
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblTipoCliente" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                        <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%# Eval("Email") %>' />
                                        <asp:Label ID="lblMotivo" Visible="false" runat="server" Text='<%# Eval("nommotivo") %>' />
                                        <asp:Label ID="Ciudad" Visible="false" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                        <asp:Label ID="lblEstado" Visible="false" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="border-color: #2780e3;" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <div style="text-align: center">
                <asp:Panel ID="pnlSolicitudProducto" Visible="true" runat="server" BackColor="White" Style="text-align: left; margin: auto auto" Width="50%">
                    <div class="modal-content">
                        <div class="col-sm-12 container">
                            <br />
                            <center>
                    <b><h4 class="modal-title text-primary">
                                    Solicitud de ahorros</h4>
                        </b>
                </center>
                            <hr style="width: 100%" />
                        </div>
                        <asp:Panel runat="server" ID="pnlDatosSolicitud">
                            <center>
                    <asp:Label runat="server" ID="lblError" Font-Size="Small" Visible="false" Font-Bold="true"  class="modal-title text-primary" style="color:red" ></asp:Label>
                    </center>
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Fecha solicitud
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFechaSolicitud" Enabled="false" runat="server" CssClass="form-control" />
                                    <br />
                                </div>
                            </div>
                            <div class="col-sm-12" style="display: none">
                                <div class="col-sm-4 text-left">
                                    Asesor
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtAsesor" Enabled="false" runat="server" CssClass="form-control" />
                                    <br />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Producto
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="3">Ahorros</asp:ListItem>
                                        <%--<asp:ListItem Value="3">Club de ahorradores</asp:ListItem>--%>
                                        <asp:ListItem Value="5">CDAT</asp:ListItem>
                                        <asp:ListItem Value="9">Ahorro Programado</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>
                            <asp:Panel ID="pnlCDATS" runat="server" Visible="false">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Línea
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlCLinea" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCLinea_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        plazo
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlCPlazo" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="txtCDAT_TextChanged">
                                            <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Valor CDAT
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtCValor" onkeypress="return ValidNum(event);" onblur="valorCambio(event)" Width="100%" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtCDAT_TextChanged"></asp:TextBox>
                                        <br />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlConsignacion" runat="server" Visible="true">
                                    <div class="col-sm-3" style="margin-left: 11px;">
                                        <asp:RadioButton runat="server" ID="rbConsignacion" GroupName="radio1" OnCheckedChanged="ValidarMedioPago" AutoPostBack="true" />
                                    </div>Pagos Por Consignación
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Anexar consignación
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:FileUpload ID="fuDocCDAT1" runat="server" />
                                            <br />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlPse" runat="server" Visible="true">
                                    <div class="col-sm-3" style="margin-left: 11px;">
                                        <asp:RadioButton runat="server" ID="rbPse" GroupName="radio1" OnCheckedChanged="ValidarMedioPago" AutoPostBack="true" />
                                    </div>Transferencia Desde Su Banco
                                    <div class="col-sm-12">
                                        <asp:ImageButton runat="server" ID="btnPse" ClientIDMode="Static" ImageUrl="~/Imagenes/LogoPSE.png" Width="70px" Height="55px" OnClick="btnPse_Click" />
                                        <br />
                                        <br />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlCDoc" runat="server" Visible="false">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Anexar declaración de origen de fondos
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:FileUpload ID="fuDocCDAT2" runat="server" />
                                            <br />
                                        </div>
                                    </div>


                                    <asp:Panel runat="server" ID="pnlDescarga" Visible="false">
                                        <div class="col-sm-12" style="margin-bottom: 20px;">
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <div class="row mensaje">
                                                    <div class="col-sm-12 col-md-3 centrar">
                                                        <a id="download" class="btn btn-default navbar-btn" style="background-color: white;" href="./../../../files/declaracion_origen_fecem.pdf" download="declaracion_origen.pdf">Descargar</a>
                                                    </div>
                                                    <div class="col-sm-12 col-md-9" style="padding-top: 5px;">
                                                        “Descargue el formato de declaración de origen de fondos”
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </asp:Panel>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Tasa (NA)<br />
                                        &nbsp;
                                    <br />
                                        <asp:TextBox ID="txtCTasa" Enabled="false" runat="server" CssClass="form-control noEditable" />
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        Tasa (EA)<br />
                                        &nbsp;
                                    <br />
                                        <asp:TextBox ID="txtCTasaEA" Enabled="false" runat="server" CssClass="form-control noEditable" />
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        Rendimiento total aprox.
                                    <br />
                                        <asp:TextBox ID="txtCRendimiento" Enabled="false" runat="server" CssClass="form-control noEditable" Width_="100%" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="col-sm-12">
                                    &nbsp;<br />
                                    <hr />
                                </div>

                                <%--<div class="col-sm-12">
                                <center>
                                    <b><h4 class="modal-title text-primary">
                                        Beneficiarios</h4>
                                    </b>
                                </center>
                            </div>
                            <br />                            
                            <asp:UpdatePanel ID="upBeneficiarios" Visible="true" runat="server">
                                <ContentTemplate>                                    
                                    <asp:GridView ID="gvBeneficiarios"
                                        runat="server" AllowPaging="True" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
                                        ForeColor="Black" GridLines="Both"
                                        OnSelectedIndexChanged="gvBeneficiario_SelectedIndexChanged"
                                        OnRowDeleting="gvBeneficiarios_RowDeleting" PageSize="10"
                                        Style="font-size: xx-small; margin: 0 auto;" Width="80%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Agregar" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnGarantias" runat="server" CommandName="Delete"                                                       
                                                        ToolTip="Eliminar" ImageUrl="~/Imagenes/gr_general.jpg"  Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Identificación" DataField="identificacion_ben">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" DataField="nombre">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>      
                                            <asp:BoundField HeaderText="Porcentaje" DataField="porcentaje_ben">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>                                                  
                                        </Columns>
                                        <FooterStyle CssClass="gridHeader" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <asp:Panel ID="pnlAddBenef" runat="server" Visible="true">
                                <div class="col-sm-12">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="btnAddRowBeneficio" AutoPostBack="true" runat="server" CssClass="btn btn-secondary" Style="padding: 3px 15px;" OnClick="btnAddRowBeneficio_Click" Text="+ Adicionar" />                                    
                                        <br />
                                    </div>
                                </div>   
                                <div class="col-sm-12">
                                    &nbsp;<br />
                                    <hr />                                
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlCBenef" Visible="false">
                                <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Identificación
                                </div>
                                <div class="col-sm-8">                                    
                                    <asp:TextBox ID="txtCidentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                                    <br />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Nombre
                                </div>
                                <div class="col-sm-8">                                    
                                    <asp:TextBox ID="txtCNombre" runat="server" CssClass="form-control" />
                                    <br />
                                </div>
                            </div>                            
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Parentesco
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCParentezco" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                    </asp:DropDownList> 
                                    <br />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Porcentaje
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtCporcentaje" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                                    <br />
                                </div>
                            </div>                                
                            <div class="col-sm-12">
                                <div class="col-sm-6 text-center">
                                    <asp:Button ID="Button1" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar" OnClick="btnGuardarBenef_Click" />
                                </div>
                                <div class="col-sm-6 text-center">              
                                    <asp:Button ID="Button2" CssClass="btn btn-danger"  Style="padding: 3px 15px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="cancelar" OnClick="btnCancelarBenef_Click"   />
                                </div>
                            </div>  
                            <asp:TextBox ID="txtPorcentajeDisponible" runat="server" Visible="false" ReadOnly="true" />
                            </asp:Panel>    --%>
                            </asp:Panel>
                            <asp:Panel ID="pnlAhoProgra" runat="server" Visible="false">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Línea
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlALinea" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlALinea_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Número cuotas
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlANumerocuotas" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="txtAValorCuota_TextChanged">
                                            <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-12">
                                        <asp:DropDownList ID="ddlADestinacion" runat="server" CssClass="form-control" Visible="false">
                                            <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Valor cuota
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtAValorCuota" onkeypress="return ValidNum(event);" onblur="valorCambio(event)" runat="server" CssClass="form-control" AutoPostBack="True" Width="100%" OnTextChanged="txtAValorCuota_TextChanged"></asp:TextBox>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Tasa (NA)
                                    <br />
                                        <br />
                                        <asp:TextBox ID="txtATasa" Enabled="false" runat="server" CssClass="form-control noEditable" />
                                        <br />
                                    </div>
                                    <div class="col-sm-4">
                                        Tasa (EA)
                                    <br />
                                        <br />
                                        <asp:TextBox ID="txtATasaEA" Enabled="false" runat="server" CssClass="form-control noEditable" />
                                        <br />
                                    </div>
                                    <div class="col-sm-4">
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txtARendimiento" Visible="false" Enabled="false" runat="server" CssClass="form-control noEditable" Width_="100%" />
                                        <br />
                                    </div>
                                </div>
                                <div>
                                    <br />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlVista" runat="server" Visible="false">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Línea
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlVLinea" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlVLinea_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Valor cuota
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtVValorCuota" onkeypress="return ValidNum(event);" onblur="valorCambio(event)" runat="server" Width="100%" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtVValorCuota_TextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtVAperturaMin" runat="server" Width="100%" CssClass="form-control" Visible="false"></asp:TextBox>
                                        <br />
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlBenef" Visible="false">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Nombre beneficiario
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtVNombre" runat="server" CssClass="form-control" />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Parentesco
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlparentesco" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Tipo identificación
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Número documento
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtVidentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtVValorCuota_TextChanged" />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            Fecha nacimiento
                                        </div>
                                        <div class="col-sm-8">
                                            <uc2:fecha runat="server" ID="txtVfecha" CssClass="form-control" />
                                            <br />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        Tasa (NA)
                                    <br />
                                        <br />
                                        <asp:TextBox ID="txtVTasa" Enabled="false" runat="server" CssClass="form-control noEditable" />
                                        <br />
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        Tasa (EA)
                                    <br />
                                        <br />
                                        <asp:TextBox ID="txtVTasaEA" Enabled="false" runat="server" CssClass="form-control noEditable" />
                                        <br />
                                    </div>
                                    <div class="col-sm-4">
                                        <br />
                                        <asp:TextBox ID="txtVRendimiento" Visible="false" Enabled="false" runat="server" CssClass="form-control noEditable" Width_="100%" />
                                        <br />
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txtVMinima" Visible="false"></asp:TextBox>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlAlmacenar">
                                <div class="modal-footer text-center" style="border-top: none">
                                    <div class="col-sm-12 text-center">
                                        &nbsp;<br />
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <div class="col-sm-6 text-right">
                                            <asp:Button ID="btnGuardarSolicitud" CssClass="btn btn-primary" Style="padding: 3px 10px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar" OnClick="btnGuardarSolicitud_Click" />
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:Button ID="btnCancelarSolicitud" CssClass="btn btn-primary" OnClick="btnCancelar_Click" runat="server" Text="Regresar" Style="padding: 3px 10px; width: 110px" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>

                        <%-- Seccion de datos ocultos fecem --%>
                        <asp:Panel ID="pnlComunAhorros" runat="server" Visible="false">
                            <div class="col-sm-12">
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlComunPeriodicidad" runat="server" CssClass="form-control" Visible="false">
                                        <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlComunFormaPago" runat="server" CssClass="form-control" Visible="false">
                                        <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="1">Caja</asp:ListItem>
                                        <asp:ListItem Value="2">Nómina</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="panelFinal" runat="server" Visible="false">
            <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                    <div class="col-xs-12">
                        <asp:Label ID="Label2" runat="server" Text="Su solicitud se generó correctamente con el código:"
                            Style="color: #66757f; font-size: 28px;" />
                        <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red; font-size: 28px;" />
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        <asp:Button ID="btnVolver" CssClass="btn btn-primary" runat="server" Text="Volver" Style="padding: 3px 15px; width: 110px; margin: 0 auto" OnClick="btnCancelar_Click" />
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
        </asp:Panel>
        <%-- Texto para almacenar datos parciales --%>
        <asp:TextBox ID="txtaccion" runat="server" Visible="false" Enabled="false" />
        <script>

</script>
    </div>

</asp:Content>

