using BinaryIntellect.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Utilitarios;
using System.IO;
using System.Xml;
using System.Data.SqlTypes;

namespace Datos
{
    public class DalEvaluacion
    {
        public BeEvaluacion ObtenerEvaluacionById(Int32 evaluacionid)
        {
            BeEvaluacion obj = null;
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@evaluacionid", evaluacionid);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerEvaluacionById", System.Data.CommandType.StoredProcedure);
                if (reader.Read())
                {
                    obj = new BeEvaluacion();
                    obj.evaluacionid = Validacion.DBToInt32(ref reader, "evaluacionid");
                    obj.pregunta = Validacion.DBToString(ref reader, "pregunta");
                    obj.respuesta = Validacion.DBToString(ref reader, "respuesta");

                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalEvaluacion -> ObtenerEvaluacionById()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return obj;
        }
    }
}
