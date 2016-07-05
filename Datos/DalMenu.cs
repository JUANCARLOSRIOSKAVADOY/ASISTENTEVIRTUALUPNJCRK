using BinaryIntellect.DataAccess;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace Datos
{
    public class DalMenu
    {
        public List<BeMenu> ObtenerListadoMenuByRol(Int32 usuarioid)
        {
            List<BeMenu> listado = new List<BeMenu>();
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_USUARIOID", usuarioid);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerMenuByRol", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeMenu obj = new BeMenu();
                    obj.menuid = Validacion.DBToInt32(ref reader, "menuid");
                    obj.icon = Validacion.DBToString(ref reader, "icon");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcion");
                    obj.url = Validacion.DBToString(ref reader, "url");
                    obj.padreid = Validacion.DBToInt32(ref reader, "padreid");
                    obj.enBienvenida = Validacion.DBToBoolean(ref reader, "enBienvenida");
                    obj.image = Validacion.DBToString(ref reader, "image");
                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalMenu -> ObtenerListadoMenuByRol()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return listado;
        }
    }
}
