using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    LiquidacionNominaService _liquidacionServices = new LiquidacionNominaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionServices.CodigoPrograma2, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoPrograma2, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosPrimeraColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosPrimeraColumna2);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosSegundaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosSegundaColumna2);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosTerceraColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosTerceraColumna2);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosCuartaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosCuartaColumna2);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosQuintaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosQuintaColumna2);

        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosSextaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosSextaColumna2);

        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosSeptimaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosSeptimaColumna2);

        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosOctavaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosOctavaColumna2);


        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosNovenaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosNovenaColumna2);



        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosDecimaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosDecimaColumna2);




        LlenarListasDesplegables(TipoLista.ConceptoNomina + "1", chkConceptosOnceavaColumna1);
        LlenarListasDesplegables(TipoLista.ConceptoNomina + "2", chkConceptosOnceavaColumna2);



        ParColumnasPlanillaLiq primeraColumna = _liquidacionServices.ConsultarParametrizacionColumnas(1, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosPrimeraColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(1, Usuario);
        LlenarInformacionColumna(primeraColumna, listaConceptosPrimeraColumna, txtNombrePrimeraColumna, chkVisiblePrimeraColumna, chkConceptosPrimeraColumna1);
        LlenarInformacionColumna(primeraColumna, listaConceptosPrimeraColumna, txtNombrePrimeraColumna, chkVisiblePrimeraColumna, chkConceptosPrimeraColumna2);

        ParColumnasPlanillaLiq segundaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(2, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosSegundaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(2, Usuario);
        LlenarInformacionColumna(segundaColumna, listaConceptosSegundaColumna, txtNombreSegundaColumna, chkVisibleSegundaColumna, chkConceptosSegundaColumna1);
        LlenarInformacionColumna(segundaColumna, listaConceptosSegundaColumna, txtNombreSegundaColumna, chkVisibleSegundaColumna, chkConceptosSegundaColumna2);

        ParColumnasPlanillaLiq terceraColumna = _liquidacionServices.ConsultarParametrizacionColumnas(3, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosTerceraColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(3, Usuario);
        LlenarInformacionColumna(terceraColumna, listaConceptosTerceraColumna, txtNombreTerceraColumna, chkVisibleTerceraColumna, chkConceptosTerceraColumna1);
        LlenarInformacionColumna(terceraColumna, listaConceptosTerceraColumna, txtNombreTerceraColumna, chkVisibleTerceraColumna, chkConceptosTerceraColumna2);

        ParColumnasPlanillaLiq cuartaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(4, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosCuartaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(4, Usuario);
        LlenarInformacionColumna(cuartaColumna, listaConceptosCuartaColumna, txtNombreCuartaColumna, chkVisibleCuartaColumna, chkConceptosCuartaColumna1);
        LlenarInformacionColumna(cuartaColumna, listaConceptosCuartaColumna, txtNombreCuartaColumna, chkVisibleCuartaColumna, chkConceptosCuartaColumna2);

        ParColumnasPlanillaLiq QuintaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(5, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosQuintaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(5, Usuario);
        LlenarInformacionColumna(QuintaColumna, listaConceptosQuintaColumna, txtNombreQuintaColumna, chkVisibleQuintaColumna, chkConceptosQuintaColumna1);
        LlenarInformacionColumna(QuintaColumna, listaConceptosQuintaColumna, txtNombreQuintaColumna, chkVisibleQuintaColumna, chkConceptosQuintaColumna2);

        ParColumnasPlanillaLiq SextaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(6, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosSextaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(6, Usuario);
        LlenarInformacionColumna(SextaColumna, listaConceptosSextaColumna, txtNombreSextaColumna, chkVisibleSextaColumna, chkConceptosSextaColumna1);
        LlenarInformacionColumna(SextaColumna, listaConceptosSextaColumna, txtNombreSextaColumna, chkVisibleSextaColumna, chkConceptosSextaColumna2);

        ParColumnasPlanillaLiq SeptimaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(7, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosSeptimaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(7, Usuario);
        LlenarInformacionColumna(SeptimaColumna, listaConceptosSeptimaColumna, txtNombreSeptimaColumna, chkVisibleSeptimaColumna, chkConceptosSeptimaColumna1);
        LlenarInformacionColumna(SeptimaColumna, listaConceptosSeptimaColumna, txtNombreSeptimaColumna, chkVisibleSeptimaColumna, chkConceptosSeptimaColumna2);


        ParColumnasPlanillaLiq OctavaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(8, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosOctavaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(8, Usuario);
        LlenarInformacionColumna(OctavaColumna, listaConceptosOctavaColumna, txtNombreOctavaColumna, chkVisibleOctavaColumna, chkConceptosOctavaColumna1);
        LlenarInformacionColumna(OctavaColumna, listaConceptosOctavaColumna, txtNombreOctavaColumna, chkVisibleOctavaColumna, chkConceptosOctavaColumna2);


        ParColumnasPlanillaLiq NovenaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(9, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosNovenaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(9, Usuario);
        LlenarInformacionColumna(NovenaColumna, listaConceptosNovenaColumna, txtNombreNovenaColumna, chkVisibleNovenaColumna, chkConceptosNovenaColumna1);
        LlenarInformacionColumna(NovenaColumna, listaConceptosNovenaColumna, txtNombreNovenaColumna, chkVisibleNovenaColumna, chkConceptosNovenaColumna2);



        ParColumnasPlanillaLiq DecimaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(10, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosDecimaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(10, Usuario);
        LlenarInformacionColumna(DecimaColumna, listaConceptosDecimaColumna, txtNombreDecimaColumna, chkVisibleDecimaColumna, chkConceptosDecimaColumna1);
        LlenarInformacionColumna(DecimaColumna, listaConceptosDecimaColumna, txtNombreDecimaColumna, chkVisibleDecimaColumna, chkConceptosDecimaColumna2);




        ParColumnasPlanillaLiq OnceavaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(11, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosOnceavaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(11, Usuario);
        LlenarInformacionColumna(OnceavaColumna, listaConceptosOnceavaColumna, txtNombreOnceavaColumna, chkVisibleOnceavaColumna, chkConceptosOnceavaColumna1);
        LlenarInformacionColumna(OnceavaColumna, listaConceptosOnceavaColumna, txtNombreOnceavaColumna, chkVisibleOnceavaColumna, chkConceptosOnceavaColumna2);



    }

    void LlenarInformacionColumna(ParColumnasPlanillaLiq informacionColumna, List<ParConceptosPlanillaLiq> listaConceptos, TextBox textBoxNombre, CheckBox checkBoxVisible, CheckBoxList checkBoxListConceptos)
    {
        if (informacionColumna != null)
        {
            textBoxNombre.Text = informacionColumna.nombrecolumna;
            checkBoxVisible.Checked = informacionColumna.esvisible == 1;
        }

        if (listaConceptos != null && listaConceptos.Count > 0)
        {
            foreach (ListItem item in checkBoxListConceptos.Items)
            {
                item.Selected = listaConceptos.Where(x => x.codigoconcepto == Convert.ToInt64(item.Value)).Any();
            }
        }
    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ParColumnasPlanillaLiq parPrimeraColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(1, chkVisiblePrimeraColumna, txtNombrePrimeraColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesPrimeraColumna = ObtenerListaConceptosParametrizacionColumnas2(1, chkConceptosPrimeraColumna1,chkConceptosPrimeraColumna2);
          
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parPrimeraColumnaPlanilla, listaConceptosVisiblesPrimeraColumna, Usuario);
          

            ParColumnasPlanillaLiq parSegundaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(2, chkVisibleSegundaColumna, txtNombreSegundaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesSegundaColumna = ObtenerListaConceptosParametrizacionColumnas2(2, chkConceptosSegundaColumna1, chkConceptosSegundaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parSegundaColumnaPlanilla, listaConceptosVisiblesSegundaColumna, Usuario);


            ParColumnasPlanillaLiq parTerceraColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(3, chkVisibleTerceraColumna, txtNombreTerceraColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesTerceraColumna = ObtenerListaConceptosParametrizacionColumnas2(3, chkConceptosTerceraColumna1, chkConceptosTerceraColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parTerceraColumnaPlanilla, listaConceptosVisiblesTerceraColumna, Usuario);

            ParColumnasPlanillaLiq parCuartaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(4, chkVisibleCuartaColumna, txtNombreCuartaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesCuartaColumna = ObtenerListaConceptosParametrizacionColumnas2(4, chkConceptosCuartaColumna1, chkConceptosCuartaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parCuartaColumnaPlanilla, listaConceptosVisiblesCuartaColumna, Usuario);

            ParColumnasPlanillaLiq parQuintaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(5, chkVisibleQuintaColumna, txtNombreQuintaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesQuintaColumna = ObtenerListaConceptosParametrizacionColumnas2(5, chkConceptosQuintaColumna1, chkConceptosQuintaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parQuintaColumnaPlanilla, listaConceptosVisiblesQuintaColumna, Usuario);

            ParColumnasPlanillaLiq parSextaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(6, chkVisibleSextaColumna, txtNombreSextaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesSextaColumna = ObtenerListaConceptosParametrizacionColumnas2(6, chkConceptosSextaColumna1, chkConceptosSextaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parSextaColumnaPlanilla, listaConceptosVisiblesSextaColumna, Usuario);

            ParColumnasPlanillaLiq parSeptimaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(7, chkVisibleSeptimaColumna, txtNombreSeptimaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesSeptimaColumna = ObtenerListaConceptosParametrizacionColumnas2(7, chkConceptosSeptimaColumna1, chkConceptosSeptimaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parSeptimaColumnaPlanilla, listaConceptosVisiblesSeptimaColumna, Usuario);

            ParColumnasPlanillaLiq parOctavaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(8, chkVisibleOctavaColumna, txtNombreOctavaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesOctavaColumna = ObtenerListaConceptosParametrizacionColumnas2(8, chkConceptosOctavaColumna1, chkConceptosOctavaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parOctavaColumnaPlanilla, listaConceptosVisiblesOctavaColumna, Usuario);


            ParColumnasPlanillaLiq parNovenaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(9, chkVisibleNovenaColumna, txtNombreNovenaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesNovenaColumna = ObtenerListaConceptosParametrizacionColumnas2(9, chkConceptosNovenaColumna1, chkConceptosNovenaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parNovenaColumnaPlanilla, listaConceptosVisiblesNovenaColumna, Usuario);



            ParColumnasPlanillaLiq parDecimaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(10, chkVisibleDecimaColumna, txtNombreDecimaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesDecimaColumna = ObtenerListaConceptosParametrizacionColumnas2(10, chkConceptosDecimaColumna1, chkConceptosDecimaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parDecimaColumnaPlanilla, listaConceptosVisiblesDecimaColumna, Usuario);

            ParColumnasPlanillaLiq parOnceavaColumnaPlanilla = ObtenerEntidadParametrizacionColumnas(11, chkVisibleOnceavaColumna, txtNombreOnceavaColumna);
            List<ParConceptosPlanillaLiq> listaConceptosVisiblesOnceavaColumna = ObtenerListaConceptosParametrizacionColumnas2(11, chkConceptosOnceavaColumna1, chkConceptosOnceavaColumna2);
            _liquidacionServices.CrearParametrizacionColumnasLiquidacion(parOnceavaColumnaPlanilla, listaConceptosVisiblesOnceavaColumna, Usuario);




            mvDatos.SetActiveView(vFinal);
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            VerError("No se pudo guardar los registros de parametrizacion, " + ex.Message);
        }
    }

    ParColumnasPlanillaLiq ObtenerEntidadParametrizacionColumnas(int codigoColumna, CheckBox checkBoxVisible, TextBox textBoxNombre)
    {
        ParColumnasPlanillaLiq parColumnasPlanilla = new ParColumnasPlanillaLiq
        {
            codigocolumna = codigoColumna,
            esvisible = Convert.ToInt64(checkBoxVisible.Checked),
            nombrecolumna = textBoxNombre.Text
        };

        return parColumnasPlanilla;
    }

    List<ParConceptosPlanillaLiq> ObtenerListaConceptosParametrizacionColumnas(int codigoColumna, CheckBoxList checkBoxConceptos)
    {
        List<ParConceptosPlanillaLiq> listaConceptos = new List<ParConceptosPlanillaLiq>();
        foreach (ListItem item in checkBoxConceptos.Items)
        {
            if (item.Selected)
            {
                listaConceptos.Add(new ParConceptosPlanillaLiq { codigocolumna = codigoColumna, codigoconcepto = Convert.ToInt64(item.Value) });
            }
        }

        return listaConceptos;
    }
    List<ParConceptosPlanillaLiq> ObtenerListaConceptosParametrizacionColumnas2(int codigoColumna, CheckBoxList checkBoxConceptos,CheckBoxList chek2)
    {
        List<ParConceptosPlanillaLiq> listaConceptos = new List<ParConceptosPlanillaLiq>();
        foreach (ListItem item in checkBoxConceptos.Items)
        {
            if (item.Selected)
            {
                listaConceptos.Add(new ParConceptosPlanillaLiq { codigocolumna = codigoColumna, codigoconcepto = Convert.ToInt64(item.Value) });
            }
        }
        foreach (ListItem item in chek2.Items)
        {
            if (item.Selected)
            {
                listaConceptos.Add(new ParConceptosPlanillaLiq { codigocolumna = codigoColumna, codigoconcepto = Convert.ToInt64(item.Value) });
            }
        }

        return listaConceptos;
    }
}