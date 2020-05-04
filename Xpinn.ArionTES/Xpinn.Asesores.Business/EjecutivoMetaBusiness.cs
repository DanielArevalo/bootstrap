using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class EjecutivoMetaBusiness : GlobalData
    {
        private const String HEADER = "CodigoEjecutivo;Oficina;Mes;codmeta;vlrmeta";
        private const int LENGTH_FORMAT = 5;

        private StreamReader strReader;
        private MemoryStream memoryStream;
        private StreamWriter strWriterMemory;

        private EjecutivoMetaData dataEjecMeta;

        public EjecutivoMetaBusiness()
        {
            dataEjecMeta = new EjecutivoMetaData();
        }

        private Boolean isFormatHeader(String pline)
        {
            String[] header = HEADER.Split(';');
            String[] line = pline.Split(';');
            Boolean output = true;

            for (int i = 0; i < header.Length; i++)
            {
                if (!header[i].Equals(line[i]))
                {
                    output = false;
                    break;
                }
            }
            return output;
        }

        private void CargarEjecutivoMetaPorLineaArchivo(String lineFile, EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                String[] arrayline = lineFile.Split(';');

                pEntityEjecutivoMeta.IdEjecutivo = Convert.ToInt64(arrayline[0].ToString());
                pEntityEjecutivoMeta.NombreOficina = arrayline[1].ToString();
                pEntityEjecutivoMeta.Mes = arrayline[2].ToString();
                pEntityEjecutivoMeta.IdMeta = Convert.ToInt64(arrayline[3].ToString().Trim());
                pEntityEjecutivoMeta.VlrMeta = Convert.ToInt64(arrayline[4].ToString().Trim());
                pEntityEjecutivoMeta.Vigencia = DateTime.Now.Year.ToString();

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    dataEjecMeta.CrearEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
                    ts.Complete();
                }

            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "CargarEjecutivoMetaPorLineaArchivo", ex);
            }
        }

        public Boolean CargarArchivoEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            Boolean header = true;
            Boolean output = true;
            String readLine;

            try
            {
                using (strReader = new StreamReader(pEntityEjecutivoMeta.stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        readLine = strReader.ReadLine();

                        if (header)
                        {
                            header = false;
                            if (!isFormatHeader(readLine))
                            {
                                output = false;
                                break;
                            }
                        }
                        else
                        {
                            if (readLine.Split(';').Length.Equals(HEADER.Split(';').Length))
                                CargarEjecutivoMetaPorLineaArchivo(readLine, pEntityEjecutivoMeta, pUsuario);
                            else
                            {
                                output = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "CrearCliente", ex);
            }
            return output;
        }//end function CargarArchivoEjecutivoMeta

        public List<EjecutivoMeta> ListarEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return dataEjecMeta.ListarEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ListarEjecutivoMeta", ex);
                return null;
            }
        }

        public List<EjecutivoMeta> ListarEjecutivos(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return dataEjecMeta.ListarEjecutivos(pEntityEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ListarEjecutivos", ex);
                return null;
            }
        }
        public EjecutivoMeta ConsultarMeta( Usuario pUsuario,String filtro)
        {
            try
            {
                return dataEjecMeta.ConsultarMeta(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ConsultarMeta", ex);
                return null;
            }
        }
        public EjecutivoMeta ConsultarMetas(Usuario pUsuario, String idobjeto)
        {
            try
            {
                return dataEjecMeta.ConsultarMetas(pUsuario, idobjeto);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ConsultarMetas", ex);
                return null;
            }
        }
      
        public  List<EjecutivoMeta> ListarMeta(Usuario pUsuario)
        {
            try
            {
                return dataEjecMeta.ListarMeta(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ListarMeta", ex);
                return null;
            }
        }
        public List<EjecutivoMeta> ListarMetas(EjecutivoMeta pEntityMeta, Usuario pUsuario)
        {
            try
            {
                return dataEjecMeta.ListarMetas(pEntityMeta,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ListarMeta", ex);
                return null;
            }
        }

        public List<EjecutivoMeta> ListarMetasFiltro(String filtro, Usuario pUsuario)
        {
            try
            {
                return dataEjecMeta.ListarMetasFiltro(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ListarMetasFiltro", ex);
                return null;
            }
        }
        public List<EjecutivoMeta> ListarPeriodicidad(Usuario pUsuario)
        {
            try
            {
                return dataEjecMeta.ListarPeriodicidad(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "ListarPeriodicidad", ex);
                return null;
            }
        }

        public MemoryStream DescargarArchivoEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            List<EjecutivoMeta> list = new List<EjecutivoMeta>();
            memoryStream = new MemoryStream();
            bool flag = false;

            try
            {
                list = dataEjecMeta.ListarEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
                using (strWriterMemory = new StreamWriter(memoryStream))
                {
                    strWriterMemory.WriteLine(HEADER);
                    foreach (var node in list)
                    {
                        if (node.PrimerNombre == pEntityEjecutivoMeta.PrimerNombre || node.Mes == pEntityEjecutivoMeta.Mes)
                        {
                            strWriterMemory.WriteLine(node.IdEjecutivo.ToString() + ";" + node.NombreOficina + ";" + node.Mes + ";" + node.IdMeta.ToString() + ";" + node.VlrMeta);
                            flag = true;
                        }
                    }
                    if (!flag) strWriterMemory.WriteLine("LA CONSULTA NO OBTUVO RESULTADOS, REVISAR LOS FILTROS");
                    strWriterMemory.Flush();
                }
                return memoryStream;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessEjecutivoMeta", "DescargarArchivoEjecutivoMeta", ex);
                return null;
            }
        }

        public void EliminarEjecutivoMeta(Int64 pIdEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    dataEjecMeta.Eliminar(pIdEjecutivoMeta, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "EliminarCliente", ex);
            }
        }

        public void EliminarMeta(Int64 pMeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    dataEjecMeta.EliminarMeta(pMeta, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "EliminarMeta", ex);
            }
        }

        public EjecutivoMeta ActualizarEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntityEjecutivoMeta = dataEjecMeta.Actualizar(pEntityEjecutivoMeta, pUsuario);
                    ts.Complete();
                }
                return pEntityEjecutivoMeta;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("EjecutivoMetaBusiness", "ActualizarEjecutivoMeta", ex);
                return null;
            }
        }
        public EjecutivoMeta CrearEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntityEjecutivoMeta = dataEjecMeta.CrearEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
                    ts.Complete();
                }
                return pEntityEjecutivoMeta;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("EjecutivoMetaBusiness", "CrearEjecutivoMeta", ex);
                return null;
            }
        }


        public EjecutivoMeta ModificarMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntityEjecutivoMeta = dataEjecMeta.ModificarMeta(pEntityEjecutivoMeta, pUsuario);
                    ts.Complete();
                }
                return pEntityEjecutivoMeta;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("EjecutivoMetaBusiness", "ModificarMeta", ex);
                return null;
            }
        }
        public EjecutivoMeta CrearMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntityEjecutivoMeta = dataEjecMeta.CrearMeta(pEntityEjecutivoMeta, pUsuario);
                    ts.Complete();
                }
                return pEntityEjecutivoMeta;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("EjecutivoMetaBusiness", "CrearMeta", ex);
                return null;
            }
        }

    }//end class
}