using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Xpinn.Util
{
    public class DbExpinnCommand : DbCommand
    {
        public event EventHandler<AuditoriaStoredProceduresArgs> SpEjecutado;
        DbCommand _command;
        bool _seAudita;

        #region Constructores


        public DbExpinnCommand(DbCommand command, bool seAudita)
        {
            _command = command;
            _seAudita = seAudita;
        }


        #endregion


        // Si investigas en el codigo fuente del .NET, te das cuenta que estas propiedades publicas se usan de accesor para las propiedades protected
        // Se sigue la misma logica y se encapsula igual, se ocultan las propiedades base por si acaso en un futuro se quierne personalizar igual
        #region Propiedades Que Ocultan al Base


        public new DbParameterCollection Parameters
        {
            get
            {
                return DbParameterCollection;
            }
        }

        public new DbConnection Connection
        {
            get
            {
                return DbConnection;
            }
            set
            {
                DbConnection = value;
            }
        }

        public new DbTransaction Transaction
        {
            get
            {
                return DbTransaction;
            }
            set
            {
                DbTransaction = value;
            }
        }


        #endregion


        #region Propiedades Que Sobreescriben al Base


        public override string CommandText
        {
            get
            {
                return _command.CommandText;
            }
            set
            {
                _command.CommandText = value;
            }
        }

        public override int CommandTimeout
        {
            get
            {
                return _command.CommandTimeout;
            }
            set
            {
                _command.CommandTimeout = value;
            }
        }

        public override CommandType CommandType
        {
            get
            {
                return _command.CommandType;
            }
            set
            {
                _command.CommandType = value;
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                return _command.DesignTimeVisible;
            }
            set
            {
                _command.DesignTimeVisible = value;
            }
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get
            {
                return _command.UpdatedRowSource;
            }
            set
            {
                _command.UpdatedRowSource = value;
            }
        }

        protected override DbConnection DbConnection
        {
            get
            {
                return _command.Connection;
            }
            set
            {
                _command.Connection = value;
            }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get
            {
                return _command.Parameters;
            }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                return _command.Transaction;
            }
            set
            {
                _command.Transaction = value;
            }
        }


        #endregion


        // Si investigas en el codigo fuente del .NET, te das cuenta que estas propiedades publicas se usan de accesor para las propiedades protected
        // Se sigue la misma logica y se encapsula igual, se ocultan las propiedades base por si acaso en un futuro se quierne personalizar igual
        #region Metodos que Ocultan al Base


        public new DbParameter CreateParameter()
        {
            return CreateDbParameter();
        }

        public new DbDataReader ExecuteReader()
        {
            return ExecuteDbDataReader(CommandBehavior.Default);
        }

        public new DbDataReader ExecuteReader(CommandBehavior behavior)
        {
            return ExecuteDbDataReader(behavior);
        }


        #endregion


        #region Propiedades que Sobreescriben al Base


        public override int ExecuteNonQuery()
        {
            try
            {
                int result = _command.ExecuteNonQuery();

                if (_seAudita)
                {
                    AuditoriaStoredProcedures auditoria = ArmarEntidadParaAuditar(true);

                    if (SpEjecutado != null)
                    {
                        SpEjecutado(this, new AuditoriaStoredProceduresArgs(auditoria));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (_seAudita)
                {
                    AuditoriaStoredProcedures auditoria = ArmarEntidadParaAuditar(false, ex);

                    if (SpEjecutado != null)
                    {
                        SpEjecutado(this, new AuditoriaStoredProceduresArgs(auditoria));
                    }
                }

                throw;
            }
        }

        public override object ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }

        public override void Prepare()
        {
            _command.Prepare();
        }

        protected override DbParameter CreateDbParameter()
        {
            return _command.CreateParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return _command.ExecuteReader(behavior);
        }

        public override void Cancel()
        {
            _command.Cancel();
        }


        #endregion


        #region Metodos de Ayuda


        AuditoriaStoredProcedures ArmarEntidadParaAuditar(bool exitoso, Exception ex = null)
        {
            AuditoriaStoredProcedures auditoria = new AuditoriaStoredProcedures
            {
                fechaejecucion = DateTime.Now,
                exitoso = exitoso,
                nombresp = _command.CommandText,
                informacionenviada = ArmarJsonConParametrosEjecutados(),
                mensaje_error = ex != null ? ex.Message : null
            };

            return auditoria;
        }

        string ArmarJsonConParametrosEjecutados()
        {
            List<AuditoriaParametro> lista = new List<AuditoriaParametro>();
            DbParameterCollection colleccionParametros = DbParameterCollection;

            foreach (DbParameter parametro in colleccionParametros)
            {
                AuditoriaParametro parametroAuditoria = new AuditoriaParametro();
                try
                {
                    parametroAuditoria.NombreParametro = parametro.ParameterName;
                    parametroAuditoria.ValorParametro = parametro.Value != DBNull.Value ? (parametro.Value.ToString() != null ? parametro.Value.ToString() : default(string)) : default(string);
                }
                catch { }

            lista.Add(parametroAuditoria);
            }

            return JsonConvert.SerializeObject(lista);
        }


        #endregion


    }
}
