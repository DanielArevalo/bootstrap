using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Xpinn.Util
{
    public class DbExpinnProviderFactory : DbProviderFactory
    {
        public event EventHandler<AuditoriaStoredProceduresArgs> SpEjecutado;
        DbProviderFactory _provider;
        bool _seAudita;

        public DbExpinnProviderFactory(DbProviderFactory provider, bool seAudita)
        {
            _provider = provider;
            _seAudita = seAudita;
        }

        public static bool GeneraAuditoria { get; set; }

        //
        // Resumen:
        //     Especifica si el objeto System.Data.Common.DbProviderFactory concreto admite
        //     la clase System.Data.Common.DbDataSourceEnumerator.
        //
        // Devuelve:
        //     true si la instancia de System.Data.Common.DbProviderFactory admite la clase
        //     System.Data.Common.DbDataSourceEnumerator; de lo contrario, false.
        public override bool CanCreateDataSourceEnumerator { get { return _provider.CanCreateDataSourceEnumerator; } }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbCommand.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbCommand.
        public override DbCommand CreateCommand()
        {
            DbExpinnCommand command = new DbExpinnCommand(_provider.CreateCommand(), _seAudita);

            if (_seAudita)
            {
                command.SpEjecutado += Command_SpEjecutado;
            }

            return command;
        }

        void Command_SpEjecutado(object sender, AuditoriaStoredProceduresArgs e)
        {
            if (SpEjecutado != null)
            {
                SpEjecutado(sender, e);
            }
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbCommandBuilder.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbCommandBuilder.
        public override DbCommandBuilder CreateCommandBuilder()
        {
            return _provider.CreateCommandBuilder();
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbConnection.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbConnection.
        public override DbConnection CreateConnection()
        {
            DbConnection conexion = _provider.CreateConnection();

            return conexion;
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbConnectionStringBuilder.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbConnectionStringBuilder.
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return _provider.CreateConnectionStringBuilder();
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbDataAdapter.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbDataAdapter.
        public override DbDataAdapter CreateDataAdapter()
        {
            return _provider.CreateDataAdapter();
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbDataSourceEnumerator.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbDataSourceEnumerator.
        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return _provider.CreateDataSourceEnumerator();
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la clase
        //     System.Data.Common.DbParameter.
        //
        // Devuelve:
        //     Una nueva instancia de System.Data.Common.DbParameter.
        public override DbParameter CreateParameter()
        {
            return _provider.CreateParameter();
        }

        //
        // Resumen:
        //     Devuelve una nueva instancia de la clase del proveedor que implementa la versión
        //     del proveedor de la clase System.Security.CodeAccessPermission.
        //
        // Parámetros:
        //   state:
        //     Uno de los valores de System.Security.Permissions.PermissionState.
        //
        // Devuelve:
        //     Un objeto System.Security.CodeAccessPermission para el objeto System.Security.Permissions.PermissionState
        //     especificado.
        public override CodeAccessPermission CreatePermission(PermissionState state)
        {
            return _provider.CreatePermission(state);
        }
    }
}
