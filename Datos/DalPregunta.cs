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
    public class DalPregunta
    {

        public List<BePregunta> ObtenerListadoPreguntaPorCurMat(Int32 cursoid, Int32 materialid)
        {
            List<BePregunta> listado = new List<BePregunta>();
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_CURSOID", cursoid);
                helper.AddParameter("@P_MATERIALID", materialid);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoPreguntaPorCurMat", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BePregunta obj = new BePregunta();
                    obj.id = Validacion.DBToInt32(ref reader, "preguntaid");
                    obj.descripcion = Validacion.DBToString(ref reader, "desc_pregunta");
                    obj.material = new BeMaterial()
                    {
                        esEscritura = Validacion.DBToBoolean(ref reader, "esEscritura")
                    };
                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> ObtenerListadoPreguntaPorCurMat()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return listado;
        }

        public List<BePregunta> ObtenerListadoPregunta(Int32 TotalRegistros, Int32 RegistroActual, Int32 cursoid, Int32 materialid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            List<BePregunta> lst = new List<BePregunta>();

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@totalRegistros", TotalRegistros);
                helper.AddParameter("@registroActual", RegistroActual);
                helper.AddParameter("@cursoid", cursoid);
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoPregunta", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BePregunta obj = new BePregunta();
                    //cursoid, descripcionCurso, materialid, descripcionMaterial
                    obj.curso = new BeCurso()
                    {
                        id = Validacion.DBToInt32(ref reader, "cursoid"),
                        descripcion = Validacion.DBToString(ref reader, "descripcionCurso")
                    };

                    obj.material = new BeMaterial()
                    {
                        id = Validacion.DBToInt32(ref reader, "materialid"),
                        descripcion = Validacion.DBToString(ref reader, "descripcionMaterial")
                    };
                    obj.id = Validacion.DBToInt32(ref reader, "preguntaid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcionPregunta");

                    lst.Add(obj);
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> ObtenerListadoPregunta()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return lst;
        }

        public Int32 ObtenerTotalListadoPregunta(Int32 cursoid, Int32 materialid)
        {
            Int32 total = 0;
            DatabaseHelper helper = null;
            SqlDataReader reader;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@cursoid", cursoid);
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerTotalListadoPregunta", System.Data.CommandType.StoredProcedure);

                while (reader.Read())
                {
                    total = Validacion.DBToInt32(ref reader, "Total");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> ObtenerTotalListadoPregunta()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return total;
        }

        public Boolean InsertarPregunta(BePregunta obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@cursoid", obj.curso.id);
                Helper.AddParameter("@materialid", obj.material.id);
                Helper.AddParameter("@descripcion", obj.descripcion);
                Helper.AddParameter("@esEntrenamiento", obj.esEntrenamiento);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarPregunta", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> InsertarPregunta()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public BePregunta ObtenerPreguntaById(Int32 preguntaid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            BePregunta obj = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@preguntaid", preguntaid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerPreguntaById", System.Data.CommandType.StoredProcedure);
                if (reader.Read())
                {
                    obj = new BePregunta();

                    obj.curso = new BeCurso()
                    {
                        id = Validacion.DBToInt32(ref reader, "cursoid"),
                        descripcion = Validacion.DBToString(ref reader, "descripcionCurso")
                    };

                    obj.material = new BeMaterial()
                    {
                        id = Validacion.DBToInt32(ref reader, "materialid"),
                        descripcion = Validacion.DBToString(ref reader, "descripcionMaterial")
                    };

                    obj.id = Validacion.DBToInt32(ref reader, "preguntaid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcionPregunta");
                    obj.esEntrenamiento = Validacion.DBToBoolean(ref reader, "esEntrenamiento");
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> ObtenerPreguntaById()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return obj;
        }

        public Boolean ActualizarPregunta(BePregunta obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@cursoid", obj.curso.id);
                Helper.AddParameter("@materialid", obj.material.id);
                Helper.AddParameter("@descripcion", obj.descripcion);
                Helper.AddParameter("@image", obj.image);
                Helper.AddParameter("@preguntaid", obj.id);
                Helper.AddParameter("@esEntrenamiento", obj.esEntrenamiento);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_ActualizarPregunta", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalPregunta -> ActualizarPregunta()");
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
