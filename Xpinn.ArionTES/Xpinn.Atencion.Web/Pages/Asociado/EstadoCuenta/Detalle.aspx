<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="ctlFecha" TagPrefix="ctl" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .thcenter {
            text-align: center;
        }

        .paddingCero {
            padding: 1px;
        }

        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }

        .btn-circle, .btn-circle-3d {
            border-radius: 50% !important;
        }

        .btn-primary.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #245580;
            -moz-box-shadow: 0px 0px 3px 1px #245580;
            box-shadow: 0px 0px 3px 1px #245580;
        }

        .btn-info.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #28a4c9;
            -moz-box-shadow: 0px 0px 3px 1px #28a4c9;
            box-shadow: 0px 0px 3px 1px #28a4c9;
        }

        .btn-success.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #3e8f3e;
            -moz-box-shadow: 0px 0px 3px 1px #3e8f3e;
            box-shadow: 0px 0px 3px 1px #3e8f3e;
        }

        .btn-danger.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #b92c28;
            -moz-box-shadow: 0px 0px 3px 1px #b92c28;
            box-shadow: 0px 0px 3px 1px #b92c28;
        }

        .btn-warning.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #e38d13;
            -moz-box-shadow: 0px 0px 3px 1px #e38d13;
            box-shadow: 0px 0px 3px 1px #e38d13;
        }

        .btn-default.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #ccc;
            -moz-box-shadow: 0px 0px 3px 1px #ccc;
            box-shadow: 0px 0px 3px 1px #ccc;
        }
        .resumen{
            background-color: rgba(178, 190, 195,.4);
            padding: 10px;
            margin-bottom: 50px;
        }
        .text-center{
            margin:0 auto;
        }
        .table{
            border: 1px;
            border: ghostwhite;
        }

        .table .table{
            border: gray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <%--Script para filtro de estado de credito--%>
    <script type="text/javascript">
                            function filtrar() {
                                let tableReg = document.getElementById('ContentPlaceHolder1_gvCreditos')
                                let valor0 = document.getElementById('FiltroEstado').value.toUpperCase()
                                let filas = ''
                                let found = false
                                let Col0 = ''
                                // Recorre las filas con contenido de la tabla
                                for (let i = 1; i < tableReg.rows.length; i++) {
                                    filas = tableReg.rows[i].getElementsByTagName('td')
                                    found = false
                                    // toma los datos de la fila
                                    Col0 = filas[4].innerHTML.toUpperCase()
                                    //Compara si no hay entrada de filtro o si el texto esta contenido
                                    if (valor0.length == 0 || Col0.indexOf(valor0) > -1) {
                                        found = true;
                                    } else { found = false }

                                    //Si hay coincidencias muestra la fila
                                    if (found) {
                                        tableReg.rows[i].style.display = ''
                                    } else {
                                        // si no hay coincidencia, esconde la fila
                                        tableReg.rows[i].style.display = 'none'
                                    }
                                }
                            }
                        </script>  

    <div class="form-group">        
        <asp:Panel ID="panelGeneral" runat="server" Visible="false">            
            <asp:Button ID="btnRegresar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Regresar" OnClick="btnRegresar_Click" Visible="false" />
            <br />
            <br />
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
                                        Tipo Cliente
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
            <!--Inicio Panel Consulta: con todos los datos de consulta-->
            <asp:Panel ID="panelConsulta" runat="server" Visible="true">
                <asp:Button ID="btnConsolidado" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Consolidado" OnClick="btnImprimir_Click" />                
                <br />
                <br />            
            <asp:Label ID="lblTituloAhorroVista" runat="server" Visible="false"/>
            <div class="">                
                <div style="background-color: #FFFFFF">
                    <div class="">
                          
                        <asp:Panel ID="panelAporte" runat="server">
                        <div id="aportes" class="resumen">
                            <h3 class="text-primary">Mis aportes</h3>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvAportes" runat="server" AutoGenerateColumns="False" CssClass="table"
                                        GridLines="Horizontal" RowStyle-CssClass="table" OnRowEditing="gvAportes_RowEditing"
                                        OnSelectedIndexChanged="gvAportes_SelectedIndexChanged" OnRowDataBound="gvAportes_RowDataBound"
                                        DataKeyNames="Saldo,cuota,valor_a_pagar, tipo_registro, valor_acumulado,principal" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Edit"
                                                        ToolTip="Realizar pago">
                                                    <div style="color:white"><span class="fa fa-money fa-lg"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Detalle" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Número" DataField="numero_aporte">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Estado" DataField="estado_Linea" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Línea" DataField="cod_linea_aporte" Visible="false"/>                                            
                                            <asp:BoundField HeaderText="Nombre" DataField="nom_linea_aporte" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Oficina" DataField="nom_oficina" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                            <asp:BoundField HeaderText="Fec apertura" DataField="fecha_apertura" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" Visible="false" />                                            
                                            <asp:BoundField HeaderText="Cuota" DataField="cuota" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Saldo" DataField="Saldo" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Próx pago" DataField="fecha_proximo_pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Valor a pagar" DataField="valor_a_pagar" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Rendimientos" DataField="valor_acumulado" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="Total" DataField="valor_total_acumu"   DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombre") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <br />
                                <div style="overflow: auto; width: 100%; max-height: 550px;">                                
                                    <asp:Label ID="lblMsj1" runat="server" CssClass="text-center" ForeColor="Red" Visible="false" />
                                <div class="form-group col-sm-12">                                    
                                    <div class="row">
                                            <div class="col-sm-12 col-md-4">
                                                Saldo total <br />
                                                <asp:TextBox ID="txtTotalAportes" runat="server" CssClass="form-control text-center" DataFormatString="{0:c0}"
                                                    Enabled="False" Width="85%" />
                                            </div>
                                            <div class="col-sm-12 col-md-4">                                                
                                                Total por pagar  <br />
                                                <asp:TextBox ID="txtAPortesPendientesporPagar" runat="server" CssClass="form-control text-center" DataFormatString="{0:c0}"
                                                    Enabled="False" Width="85%" />
                                            </div>
                                            <div class="col-sm-12 col-md-4">                                                
                                                Total cuotas <br /> 
                                                <asp:TextBox ID="txtTotalCuotasAportes" runat="server" CssClass="form-control text-center" DataFormatString="{0:c0}"
                                                    Enabled="False" Width="85%" />
                                            </div>                                                                                                                                                                                               
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 col-md-3">                                                
                                            </div>                                                                                                                                              
                                            <div class="col-sm-12 col-md-3">
                                                <asp:TextBox ID="txtTotalCausadosApo" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="85%" Visible="false"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 col-md-3">
                                                <asp:TextBox ID="txtTotalmasRendCausados" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="85%" Visible="false" />
                                            </div>
                                            <div class="col-sm-12 col-md-6">    
                                                &nbsp;                                            
                                            </div>
                                        </div>                                    
                                </div>
                                    </div>
                        </div>                                              
                            </asp:Panel>
                            
                        <asp:Panel ID="panelCredito" runat="server">
                        <div id="creditos" class="resumen">
                            <h3 class="text-primary">Mis créditos</h3>
                                <div class="form-group col-sm-12">                                  
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12">                                                
                                            Estado
                                            <select id="FiltroEstado" class="form-control text-center" style="width:100px;" onchange="filtrar()">
                                            <option value="">Todos</option>
                                            <option value="ATRASADO">Atrasado</option>
                                            <option value="SOLICITADO">Solicitado</option>
                                            <option value="APROBADO">Aprobado</option>
                                            <option value="ESTA AL DIA">Al día</option>
                                            <option value="ANALIZADO">Analizado</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvCreditos" runat="server" AutoGenerateColumns="False" OnRowEditing="gvCreditos_RowEditing"
                                        RowStyle-CssClass="table" CssClass="table" GridLines="Horizontal"
                                        OnSelectedIndexChanged="gvCreditos_SelectedIndexChanged" OnRowDataBound="gvCreditos_RowDataBound"
                                        OnRowDeleting="gvCreditos_RowDeleting" DataKeyNames="saldo,valorcuota,valorapagar, tipo_registro" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                              <ItemTemplate>
                                                  <asp:LinkButton Visible="false" ID="btnPagar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Edit"
                                                        ToolTip="Pagar"><div style="color:white">
                                                        <span class="fa fa-money fa-lg"></div></asp:LinkButton>                                                  
                                              </ItemTemplate>                                              
                                              <ItemStyle HorizontalAlign="Center" />
                                          </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Detalle" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnGarantias" runat="server" CommandName="Delete"
                                                        Visible='<%# Eval("estado").ToString().Trim() == "APROBADO" ? true:false %>'                                                        
                                                        ToolTip="Garantias y Desembolsos" ImageUrl="~/Imagenes/gr_general.jpg"  Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Número" DataField="numero_producto" />
                                            <asp:BoundField HeaderText="Estado" DataField="estado">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" DataField="linea" />
                                            <asp:BoundField HeaderText="Monto aprobado" DataField="monto" DataFormatString="{0:c0}" Visible="false">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Fecha aprobada" DataField="fechaapertura" DataFormatString="{0:d}" Visible="false"/>
                                            <asp:BoundField HeaderText="Plazo" DataField="Plazo">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Cuotas pagadas" DataField="CuotasPagadas">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Cuota" DataField="valorcuota" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Saldo" DataField="saldo" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Fec próx pago" DataField="fechaproximopago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Valor en mora" DataField="valorapagar" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Tasa interés" DataField="Tasainteres" DataFormatString="{0:N}%" >
                                                <ItemStyle HorizontalAlign="Right" />
                                                  
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="Oficina" DataField="NombreOficina" Visible="false">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="Pagaduria" DataField="pagadurias" Visible="false">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                               <asp:BoundField HeaderText="Fec terminación" DataField="fecha_vencimiento" DataFormatString="{0:d}">
                                                <HeaderStyle Width="50px" />
                                            </asp:BoundField>

                                             <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombre") %>' Width="160px"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                <br />
                                <div style="overflow: auto; width: 100%; max-height: 550px">
                                    <asp:Label ID="lblMsj2" runat="server" CssClass="text-center" ForeColor="Red" Visible="false" /></center>
                                <div class="form-group col-sm-12">                                  
                                    <div class="row">   
                                            <div class="col-sm-12 col-md-4">                                                
                                                Total saldo capital<br />
                                                <asp:TextBox ID="txtTotalSaldos" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                            </div>
                                            <div class="col-sm-12 col-md-4">                                                
                                                Total en mora<br />
                                                <asp:TextBox ID="txtVlrPendienteApagar" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                            </div>                                            
                                            <div class="col-sm-12 col-md-4">                                                
                                                Total cuotas crédito<br />
                                                <asp:TextBox ID="txtTotalCoutasCreditos" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                        </div>
                                    <div class="row">                                               
                                        <div class="col-sm-12 col-md-3 text-right">
                                            &nbsp;                                                
                                            </div>
                                            <div class="col-sm-12 col-md-3">
                                                &nbsp;
                                            </div>
                                    </div>                                    
                                </div>
                                </div>
                        </div>
                            </asp:Panel>
                           
                        <asp:Panel ID="panelAcodeudado" runat="server">
                        <div id="acodeudados" class="resumen" style="display: none">
                            <h3 class="text-primary">Mis créditos acodeudados</h3>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvAcodeudados" runat="server" Width="100%" GridLines="Horizontal"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="table table-striped"
                                        ShowFooter="true" RowStyle-CssClass="table">
                                        <Columns>
                                            <asp:BoundField DataField="identificacion" HeaderText="Cedula">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombres" HeaderText="Nombre deudor">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaProxPago" HeaderText="Próximo pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Valor_apagar" HeaderText="Valor en mora" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                <br />
                                    <div style="overflow: auto; width: 100%; max-height: 550px">
                                    <asp:Label ID="lblMsj3" runat="server" CssClass="text-center" ForeColor="Red" Visible="false" />
                                </div>
                            <br />
                        </div>
                            </asp:Panel>
                            
                        <asp:Panel ID="panelAhorros" runat="server">
                        <div id="ahorros" class="resumen">
                            <h3 class="text-primary"><span id="spnTitleVista">Ahorros a la vista</span></h3>
                            <%--<h3 class="text-primary"><span id="spnTitleVista">Mis club de ahorradores</span></h3>--%>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvAhorros" runat="server" AllowPaging="false" AutoGenerateColumns="False" ShowFooter="true"
                                        GridLines="Horizontal" CssClass="table" RowStyle-CssClass="table" OnSelectedIndexChanged="gvAhorros_SelectedIndexChanged"  OnRowEditing="gvAhorros_RowEditing" OnRowCommand="gvAhorros_RowCommand"
                                        ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="saldo_total,valor_cuota,saldo_canje,valor_acumulado,numero_cuenta, tipo_registro">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible="true" ID="btnRetirar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Select"
                                                        ToolTip="Hacer retiro"><div style="color:white">
                                                        <span class="fa fa-arrow-circle-down fa-lg"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible="true" ID="btnPagar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Pagar" ToolTip="Realizar pago" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'>
                                                        <div style="color:white" ><span class="fa fa-money fa-lg"></div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton Visible="true" ID="btnInfo" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Detalle" Height="20px" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_cuenta" HeaderText="Número">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Beneficiario" DataField="nombres_ben" ItemStyle-HorizontalAlign="Center" />                                            
                                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fec. Apertura" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo total" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_canje" HeaderText="Saldo en canje" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_formapago" HeaderText="Forma pago" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="Próximo Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Rendimientos" DataField="valor_acumulado" DataFormatString="{0:c0}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                             <asp:BoundField HeaderText="Total con rendimientos" DataField="valor_total_acumu"   DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombres") %>' Width="160px"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                 </div>
                                <br />
                                <div style="overflow: auto; width: 100%; max-height: 550px">
                                    <table style="width: 100%">
                                    <tr>
                                        <td class="col-sm-12 text-center">
                                            <asp:Label ID="lblTotRegAhorro" runat="server" Visible="false" CssClass="text-center" ForeColor="Red"/>
                                            <asp:Label ID="lblInfoAhorro" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="form-group col-sm-12">
                                    <div class="col-lg-12 col-sm-12">
                                        <div class="row">
                                                <div class="col-sm-12 col-md-4">
                                                    Total saldos <br />
                                                <asp:TextBox ID="txtTotalAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                                </div>
                                                <div class="col-sm-12 col-md-4">
                                                    Total rendimientos <br />
                                                <asp:TextBox ID="txtTotalCausacionAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%"/>
                                                </div>
                                                <div class="col-sm-12 col-md-4">
                                                    Total saldo + rendimiento <br />

                                                <asp:TextBox ID="txtTotalAhmasRendCausados" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />                                                
                                                </div>                                                
                                            </div>
                                    <div class="row">
                                            <asp:TextBox ID="txtTotalCanjeAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" Visible="false"/>
                                            <asp:TextBox ID="txtTotalCuotasAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" Visible="false" />                                                                                        
                                    </div>                                   
                                </div>                                
                                </div>
                        </div>
                        </div>                                                
                            </asp:Panel>                            
                           
                        <asp:Panel ID="panelProgra" runat="server">
                        <div id="programado" class="resumen">
                            <h3 class="text-primary">Mis ahorros programados</h3>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvAhoProgra" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="true"
                                        AllowPaging="false" CssClass="table" GridLines="Horizontal" RowStyle-CssClass="table"
                                        DataKeyNames="numero_programado,saldo,valor_cuota,valor_acumulado" OnSelectedIndexChanged="gvAhoProgra_SelectedIndexChanged" OnRowEditing="gvAhoProgra_RowEditing">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible="false" ID="btnRetirar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Select"
                                                        ToolTip="Hacer retiro"><div style="color:white">
                                                        <span class="fa fa-arrow-circle-down fa-lg"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible="false" ID="btnPagar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Edit"
                                                        ToolTip="Realizar pago"><div style="color:white">
                                                        <span class="fa fa-money fa-lg"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Detalle" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_programado" HeaderText="Número">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>    
                                                 <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>                                       
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo total" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult Mov" DataFormatString="{0:d}" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomforma_pago" HeaderText="Forma pago" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F.Vencimie." DataFormatString="{0:d}">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>                                                                                                                                
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas pagadas">
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa interés" DataFormatString="{0:N}%">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>                                           
                                            
                                              <asp:BoundField HeaderText="Rendimientos" DataField="valor_acumulado" DataFormatString="{0:c0}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                               <asp:BoundField HeaderText="Valor + Rendimientos" DataField="valor_total_acumu"   DataFormatString="{0:c0}" Visible="false">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                    <br />
                                    <div style="overflow: auto; width: 100%; max-height: 550px">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="col-sm-12 text-center">
                                            <asp:Label ID="lblTotRegProg" runat="server" Visible="false" CssClass="text-center" ForeColor="Red" />
                                            <asp:Label ID="lblInfoProg" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                                               
                                         <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-12 col-md-4">
                                                    Total saldos <br />
                                                <asp:TextBox ID="txtTotalAhoProgramado" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="90%" />
                                                </div>
                                                <div class="col-sm-12 col-md-4">
                                                    Total rendimientos <br />
                                                <asp:TextBox ID="txtTotalCausacionProgramado" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="90%" />
                                                </div>
                                                <div class="col-sm-12 col-md-4">
                                                    Total saldo + rendimiento <br />   
                                                <asp:TextBox ID="txtTotalProgmasRendCausados" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="90%" />                                             
                                                </div>                                                
                                            </div>
                                             <div class="row">                                                
                                                <div class="col-sm-12 col-md-3 text-right">
                                                    &nbsp;
                                                </div>
                                                <div class="col-sm-12 col-md-3">                                                
                                                <asp:TextBox ID="txtTotalCuotasAhoProgra" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="90%" visible="false"/>
                                                </div>
                                            </div>
                                            
                                         <div class="col-sm-12 col-md-3 text-right">
                                                                                                
                                            </div>
                                         <div class="col-sm-12 col-md-3">
                                                
                                            </div>
                                       <div class="col-sm-12 col-md-3 text-right">
                                                
                                            </div>
                                              <div class="col-sm-12 col-md-3">
                                                
                                            </div>
                                        </div>
                                </div>
                        </div>
                            </asp:Panel>
                                                    
                        <asp:Panel ID="panelCdat" runat="server">
                        <div id="cdats" class="resumen">
                            <h3 class="text-primary">Mis CDAT's</h3>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvCDATS" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="true"
                                        AllowPaging="false" DataKeyNames="codigo_cdat,valor,valor_acumulado" GridLines="Horizontal"
                                        CssClass="table" RowStyle-CssClass="table" OnSelectedIndexChanged="gvCDATS_SelectedIndexChanged"  OnRowCommand="gvCDATS_RowCommand"
                                        OnRowEditing="gvCDATS_RowEditing" OnRowDeleting="gvCDATS_RowDeleting" >
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible="false" ID="btnRetirar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Select"
                                                        ToolTip="Hacer retiro"><div style="color:white">
                                                        <span class="fa fa-arrow-circle-down fa-lg"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Pagar" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Realizar pago" >
                                                        <div style="color:white"><span class="fa fa-money fa-lg"></div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Detalle" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnPrint" runat="server" CommandName="Delete"
                                                        ToolTip="Descargar CDAT" ImageUrl="~/Imagenes/print.png"  Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_cdat" HeaderText="Número">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_inicio" HeaderText="F. Inicio" DataFormatString="{0:d}" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F. Vencimiento" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Plazo" visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlazocdat" runat="server" Text='<%# Convert.ToInt32(Eval("plazo"))/30 %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tasa_interes" DataFormatString="{0:n}%" HeaderText="Tasa interés E.A.">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Rendimientos" DataField="valor_acumulado" DataFormatString="{0:c0}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                               <asp:BoundField HeaderText="Total" DataField="valor_total_acumu"   DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="codigo_cdat" HeaderText="código" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                 <br />
                                    <div style="overflow: auto; width: 100%; max-height: 550px">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="col-sm-12 text-center">
                                            <asp:Label ID="lblTotRegCdat" runat="server" Visible="false" CssClass="text-center" ForeColor="Red"/>                                        
                                            <asp:Label ID="lblInfoCdat" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="form-group col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-12 col-md-4">
                                                    Total saldos<br />
                                                    <asp:TextBox ID="txtTotalCdats" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="90%" />
                                                </div>
                                                <div class="col-sm-12 col-md-4">
                                                    Rendimientos<br />
                                                    <asp:TextBox ID="txtTotalCausacionCdat" runat="server" CssClass="form-control text-center"
                                                    Enabled="False" Width="90%" />
                                                </div>
                                                <div class="col-sm-12 col-md-4">
                                                    Total saldo + rendimiento<br />
                                                    <asp:TextBox ID="txtTotalCdatRendCausados" runat="server" CssClass="form-control text-center"
                                                    Enabled="false" Width="90%" />  
                                                </div>
                                        </div>

                                </div>
                                                                              
                                    </div>
                        </div>
                            </asp:Panel>
                                                    
                        <asp:Panel ID="panelServicio" runat="server">
                        <div id="servicios" class="resumen">
                            <h3 class="text-primary">Seguros / Servicios</h3>
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvServicio" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                        OnSelectedIndexChanged="gvServicio_SelectedIndexChanged" OnRowEditing="gvServicio_RowEditing"
                                        DataKeyNames="numero_servicio,saldo,valor_cuota,valor_total" ShowFooter="true"
                                        GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="100%" CssClass="table"
                                        RowStyle-CssClass="table">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible="false" ID="btnPagar" runat="server" CssClass="btn btn-sm btn-primary btn-circle" CommandName="Edit"
                                                        ToolTip="Realizar Pago"><div style="color:white">
                                                        <span class="fa fa-money"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Imagenes/gr_general.png"
                                                        ToolTip="Detalle" Height="20px" Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_servicio" HeaderText="Número">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="nom_plan" HeaderText="Plan" Visible="false">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="valor_total" HeaderText="Saldo inicial" DataFormatString="{0:c0}" visible="false">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_solicitud" DataFormatString="{0:d}" HeaderText="Fec. Solicitud" visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_activacion" DataFormatString="{0:d}" HeaderText="Fec. Desembolso" visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="Fec.Próx. Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_final_vigencia" DataFormatString="{0:d}" HeaderText="Fec terminación">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="estado" HeaderText="Estado" visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor cuota" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c0}">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="numero_cuotas" HeaderText="Plazo">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                                 <asp:BoundField DataField="nom_forma_pago" HeaderText="Forma pago" Visible="false">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>


                                             <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombre") %>' Width="160px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>                                            
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                <br />
                                    <div style="overflow: auto; width: 100%; max-height: 550px">
                                        <table style="width: 100%">
                                <tr>
                                    <td class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotRegServ" runat="server" Visible="false" CssClass="text-center" ForeColor="Red"/>
                                        <asp:Label ID="lblInfoServ" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                                 <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            Saldos iniciales <br />
                                            <asp:TextBox ID="txtTotalValorInicialServicios" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                      <div class="col-sm-4">
                                            Total saldos<br />
                                            <asp:TextBox ID="txtTotalServicios" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                        <div class="col-sm-4">
                                            Total cuotas servicios<br />
                                            <asp:TextBox ID="txtTotalCuotasServicios" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                      
                                    </div>                            
                                </div>                           
                        </div>
                            </asp:Panel>


                        <div id="devolucion" class="resumen" style="display: none">
                            <h3 class="text-primary">Mis devoluciones</h3>                            
                                    <div class="col-md-12 text-left">
                                        <asp:CheckBox ID="chekSaldo" runat="server" Text="Mostrar Saldos en cero" OnCheckedChanged="chekSaldo_CheckedChanged"
                                            AutoPostBack="True" />
                                    </div>
                                    <asp:Panel ID="pnlDevolucion" runat="server">
                                        <div style="overflow: auto; width: 100%;">
                                            <asp:GridView ID="gvDevolucion" runat="server" AutoGenerateColumns="False" CssClass="table"
                                                GridLines="Horizontal" RowStyle-CssClass="table" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="num_devolucion" HeaderText="Número">
                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="concepto" HeaderText="Nombre">
                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_devolucion" DataFormatString="{0:d}" HeaderText="Fec. Apertura">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c0}">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c0}">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                                </Columns>
                                            </asp:GridView>
                                            </div>
                                        <br />
                                            <div style="overflow: auto; width: 100%; max-height: 550px">
                                            <div class="col-md-12 text-center">
                                                <asp:Label ID="lblMsjDevolucion" runat="server" CssClass="text-center" ForeColor="Red" Visible="false"/>
                                            </div>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chekSaldo" EventName="CheckedChanged" />
                                        </Triggers>
                                        </div>                                            
                                    </asp:Panel>
                                                     
                        </div>
                        <div id="comentarios" class="resumen" style="display: none">
                            <h3 class="text-primary">Comentarios</h3>
                            <asp:Panel ID="pnlComentarios" runat="server">
                                <div style="overflow: scroll; width: 100%; max-height: 350px">
                                    <asp:GridView ID="gvComentarios" runat="server" Width="100%" GridLines="Horizontal" ShowFooter="true"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="table table-striped"
                                        RowStyle-CssClass="table">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Imagenes/gr_delete.png" ShowDeleteButton="True" ControlStyle-Height="20px"/>
                                            <asp:BoundField DataField="idComentario" HeaderText="IdComentario">
                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hora" HeaderText="Hora">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion" HeaderText="Comentario">
                                                <ItemStyle HorizontalAlign="Left" Width="70%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                <br />
                                    <div style="overflow: auto; width: 100%; max-height: 550px">
                            <br />
                                    <div class="col-md-12 text-center">
                                                <asp:Label ID="lblMensajeComentarios" runat="server" CssClass="text-center" ForeColor="Red" />
                                     </div>                                
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>                
            </div>
            </asp:Panel>
            <!--Fin Panel Consulta -->

            <!--Inicio Panel Retiro: datos para solicitar retiro -->
            <div style="text-align: center">
                <asp:Panel ID="pnlHacerRetiro" Visible="false" runat="server" BackColor="White" Style="text-align: left; margin: auto auto" Width="50%">
                    <div class="modal-content">
                        <div class="col-sm-12 container">
                            <br />
                            <center>
                    <b><h4 class="modal-title text-primary">
                            Retiro de Ahorros</h4>
                        </b>
                </center>
                            <center>
                    <asp:Label runat="server" ID="lblError" Font-Size="Small" Visible="false" Font-Bold="true"  class="modal-title text-primary" style="color:red" ></asp:Label>
                    </center>
                            <hr style="width: 100%" />
                        </div>

                        <asp:Panel ID="pnlDatosRetiro" runat="server" >                        
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Fecha solicitud
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtFechaSolicitud" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Num. Producto
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtNumeroProducto" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Valor disponible
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDisponible" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Valor retiro
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtValorRetiro" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtValorRetiro_TextChanged" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)"></asp:TextBox>
                                <br />
                            </div>
                        </div>
                            <asp:Label runat="server" ID="lblprueba"></asp:Label>
                        <asp:Panel ID="pnlCierre" runat="server" Visible="false">
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Cerrar cuenta
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCierre" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0">Seleccione un Item</asp:ListItem>
                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                </asp:DropDownList>
                                    <br />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-sm-12">                            
                            <div class="col-sm-4 text-left">
                                Comentario
                            </div>
                            <div class="col-sm-8">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtObsRetiro" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Forma de giro
                            </div>
                            <div class="col-sm-8">                                
                                <asp:DropDownList ID="ddlFormaDesembolso" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="3" Selected="True">Transferencia</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </div>
                        </div>
                        <asp:Panel ID="panelTransferencia" runat="server" Visible="true">
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Banco
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlBancos" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <br />
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Tipo Cuenta
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlTipoCuenta" runat="server" Enabled="false" CssClass="form-control">
                                        <asp:ListItem Value="0">Ahorros</asp:ListItem>
                                        <asp:ListItem Value="1">Corriente</asp:ListItem>                                        
                                        <asp:ListItem Value="2">Ahorros</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <br />
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-left">
                                    Num. Cuenta
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtNumCuenta" Enabled="false" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />                                    
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <br />
                                <asp:TextBox Enabled="false" runat="server" Visible="false" ID="tipo_prod_retiro"></asp:TextBox>
                            </div>
                        </asp:Panel>

                        </asp:Panel>
                        <div class="modal-footer">
                            <br />
                            <asp:Button ID="btnRetiro" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar" OnClick="btnRetiro_Click" />
                            &nbsp &nbsp
                            <asp:Button ID="btnCerrarRetiro" CssClass="btn btn-primary" runat="server" Text="Regresar" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClick="btnCerrarRetiro_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <!--Fin Panel Retiro: datos para solicitar retiro -->
        </asp:Panel>
        <!--Inicio Panel Impresion -->
        <asp:Panel ID="panelImpresion" runat="server">
            <div class="col-sm-12">
                <rsweb:ReportViewer ID="rptEstadoCuenta" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="10pt" Width="100%">
                    <LocalReport ReportPath="Pages\Asociado\EstadoCuenta\rptEstadoCuenta.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
            <div class="col-sm-12">
                <asp:Literal ID="ltReport" runat="server" />
            </div>
        </asp:Panel>
        <!--Fin Panel Impresion -->
    </div>

</asp:Content>