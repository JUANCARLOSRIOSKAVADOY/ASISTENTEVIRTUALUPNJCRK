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
    public class DalMaterial
    {
        public Boolean InsertarMateria(BeMaterial obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@descripcion", obj.descripcion);
                Helper.AddParameter("@esEscritura", obj.esEscritura);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarMateria", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalMaterial -> InsertarMateria()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public Boolean ActualizarMateria(BeMaterial obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@materialid", obj.id);
                Helper.AddParameter("@descripcion", obj.descripcion);
                Helper.AddParameter("@image", obj.image);
                Helper.AddParameter("@esEscritura", obj.esEscritura);          
                Helper.AddParameter("@estado", obj.estado);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_ActualizarMaterial", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalMaterial -> ActualizarMateria()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public List<BeMaterial> ObtenerListadoMateria(Int32 TotalRegistros, Int32 RegistroActual,String Descripcion, Boolean? Estado)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            List<BeMaterial> lst = new List<BeMaterial>();

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@totalRegistros", TotalRegistros);
                helper.AddParameter("@registroActual", RegistroActual);
                helper.AddParameter("@descripcion", Descripcion);
                helper.AddParameter("@estado", Estado);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoMaterial", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeMaterial obj = new BeMaterial();
                    obj.id = Validacion.DBToInt32(ref reader, "materialid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcion");
                    obj.image = Validacion.DBToString(ref reader, "image");
                    obj.estado = Validacion.DBToBoolean(ref reader, "estado");

                    lst.Add(obj);
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalMaterial -> ObtenerListadoMateria()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return lst;
        }

        public Int32 ObtenerTotalListadoMateria(String Descripcion, Boolean? Estado)
        {
            Int32 total = 0;
            DatabaseHelper helper = null;
            SqlDataReader reader;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@descripcion", Descripcion);
                helper.AddParameter("@estado", Estado);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerTotalListadoMaterial", System.Data.CommandType.StoredProcedure);

                while (reader.Read())
                {
                    total = Validacion.DBToInt32(ref reader, "Total");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalMaterial -> ObtenerTotalListadoMateria()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return total;
        }

        public BeMaterial ObtenerMaterialById(Int32 materialid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            BeMaterial obj = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerMaterialById", System.Data.CommandType.StoredProcedure);

                if (reader.Read())
                {
                    obj = new BeMaterial();
                    obj.id = Validacion.DBToInt32(ref reader, "materialid");
                    obj.descripcion = Validacion.DBToString(ref reader, "descripcion");
                    obj.image = Validacion.DBToString(ref reader, "image");
                    obj.estado = Validacion.DBToBoolean(ref reader, "estado");
                    obj.esEscritura = Validacion.DBToBoolean(ref reader, "esEscritura");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalMaterial -> ObtenerMaterialById()");
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
