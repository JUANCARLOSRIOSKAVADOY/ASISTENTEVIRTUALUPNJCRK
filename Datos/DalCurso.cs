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
    public class DalCurso
    {
        public List<BeCurso> ObtenerListadoCursoPorEstado(Boolean? Estado)
        {
            List<BeCurso> listado = new List<BeCurso>();
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_ESTADO", Estado);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoCursoPorEstado", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeCurso obj = new BeCurso();
                    obj.id = Validacion.DBToInt32(ref reader, "id");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcion");
                    obj.image = Validacion.DBToString(ref reader, "image");
                    obj.estado = Validacion.DBToBoolean(ref reader, "estado");
                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCurso -> ObtenerListadoCursoPorEstado()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return listado;
        }

        public Boolean InsertarCurso(BeCurso obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@descripcion", obj.descripcion);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarCurso", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCurso -> InsertarCurso()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public Boolean ActualizarCurso(BeCurso obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@cursoid", obj.id);
                Helper.AddParameter("@descripcion", obj.descripcion);
                Helper.AddParameter("@image", obj.image);
                Helper.AddParameter("@estado", obj.estado);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_ActualizarCurso", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCurso -> ActualizarCurso()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public List<BeCurso> ObtenerListadoCurso(Int32 TotalRegistros, Int32 RegistroActual, String Descripcion, Boolean? Estado)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            List<BeCurso> lst = new List<BeCurso>();

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@totalRegistros", TotalRegistros);
                helper.AddParameter("@registroActual", RegistroActual);
                helper.AddParameter("@descripcion", Descripcion);
                helper.AddParameter("@estado", Estado);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoCurso", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeCurso obj = new BeCurso();
                    obj.id = Validacion.DBToInt32(ref reader, "cursoid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcion");
                    obj.image = Validacion.DBToString(ref reader, "image");
                    obj.estado = Validacion.DBToBoolean(ref reader, "estado");

                    lst.Add(obj);
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCurso -> ObtenerListadoCurso()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return lst;
        }

        public Int32 ObtenerTotalListadoCurso(String Descripcion, Boolean? Estado)
        {
            Int32 total = 0;
            DatabaseHelper helper = null;
            SqlDataReader reader;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@descripcion", Descripcion);
                helper.AddParameter("@estado", Estado);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerTotalListadoCurso", System.Data.CommandType.StoredProcedure);

                while (reader.Read())
                {
                    total = Validacion.DBToInt32(ref reader, "Total");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCurso -> ObtenerTotalListadoCurso()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return total;
        }

        public BeCurso ObtenerCursolById(Int32 cursoid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            BeCurso obj = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@cursoid", cursoid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerCursoById", System.Data.CommandType.StoredProcedure);

                if (reader.Read())
                {
                    obj = new BeCurso();
                    obj.id = Validacion.DBToInt32(ref reader, "cursoid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcion");
                    obj.image = Validacion.DBToString(ref reader, "image");
                    obj.estado = Validacion.DBToBoolean(ref reader, "estado");

                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCurso -> ObtenerCursolById()");
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
