using BinaryIntellect.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Utilitarios;

namespace Datos
{
    public class DalRpt
    {
        public List<BeRptResultado> ObtenerResultado(Int32? usuarioid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            List<BeRptResultado> lst = new List<BeRptResultado>();

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@userid", usuarioid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerResultado", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeRptResultado obj = new BeRptResultado();
                    obj.usuario = Validacion.DBToString(ref reader, "usuario");
                    obj.curso = Validacion.DBToString(ref reader, "curso");
                    obj.material = Validacion.DBToString(ref reader, "material");
                    obj.total = Validacion.DBToInt32(ref reader, "total");
                    obj.acertado = Validacion.DBToInt32(ref reader, "acertado");
                    obj.errado = Validacion.DBToInt32(ref reader, "errado");
                    obj.nota = Validacion.DBToInt32(ref reader, "nota");
                    obj.nivel = Validacion.DBToString(ref reader, "nivel");

                    lst.Add(obj);
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalRpt -> ObtenerResultado()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return lst;
        }  
    }
}
