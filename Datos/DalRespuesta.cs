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
    public class DalRespuesta
    {
        public List<BeRespuesta> ObtenerRespuestasByPregunta(Int32 preguntaid)
        {
            List<BeRespuesta> listado = new List<BeRespuesta>();
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_PREGUNTAID", preguntaid);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerRespuestasByPregunta", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeRespuesta obj = new BeRespuesta();
                    obj.id = Validacion.DBToInt32(ref reader, "id_respuesta");
                    obj.descripcion = Validacion.DBToString(ref reader, "desc_respuesta");
                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalRespuesta -> ObtenerRespuestasByPregunta()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return listado;
        }

        public List<BeRespuesta> ObtenerRespuestasEntrenamientoByPreg(Int32 cursoid, Int32 materialid, String descripcion)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            List<BeRespuesta> lst = new List<BeRespuesta>();

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@descripcion", descripcion);
                helper.AddParameter("@cursoid", cursoid);
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerRespuestasEntrenamientoByPreg", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeRespuesta obj = new BeRespuesta();

                    //obj.id = Validacion.DBToInt32(ref reader, "preguntaid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcionRespuesta");

                    lst.Add(obj);
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> ObtenerRespuestasEntrenamientoByPreg()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return lst;
        }

        public Boolean InsertarRespuesta(BeRespuesta obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@descripcion", obj.descripcion);
                Helper.AddParameter("@preguntaid", obj.pregunta.id);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarRespuesta", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalRespuesta -> InsertarRespuesta()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public Boolean EliminarRespuesta(Int32 respuestaid)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@respuestaid", respuestaid);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_EliminarRespuesta", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalRespuesta -> EliminarRespuesta()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }
    }
}
