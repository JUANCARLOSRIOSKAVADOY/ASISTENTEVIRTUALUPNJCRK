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
    public class DalCursoMaterial
    {
        public BeCursoMaterial ObtenerListadoMaterialPorCurso(Int32 cursoid)
        {
            BeCursoMaterial obj = new BeCursoMaterial();
            obj.lstMaterial = new List<BeMaterial>();
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_CURSOID", cursoid);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoMaterialPorCurso", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeCurso objCurso = new BeCurso();
                    objCurso.id = Validacion.DBToInt32(ref reader, "cursoid");
                    objCurso.descripcion = Validacion.DBToString(ref reader, "desc_curso");

                    BeMaterial objMaterial = new BeMaterial();
                    objMaterial.id = Validacion.DBToInt32(ref reader, "materialid");
                    objMaterial.descripcion = Validacion.DBToString(ref reader, "desc_material");
                    objMaterial.curso = new BeCurso()
                    {
                        id = Validacion.DBToInt32(ref reader, "cursoid")
                    };

                    obj.curso = objCurso;
                    obj.lstMaterial.Add(objMaterial);
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCursoMaterial -> ObtenerListadoMaterialPorCurso()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return obj;
        }
        
        public Boolean EliminarCursoMaterial(Int32 cursoid, Int32 materialid)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@cursoid", cursoid);
                Helper.AddParameter("@materialid", materialid);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_EliminarCursoMaterial", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCursoMaterial -> EliminarCursoMaterial()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public Boolean InsertarCursoMaterial(BeCurso objCurso,BeMaterial objMaterial)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@cursoid", objCurso.id);
                Helper.AddParameter("@materialid", objMaterial.id);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarCursoMaterial", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCursoMaterial -> InsertarCursoMaterial()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public List<BeCursoMaterial> ObtenerListadoCursoMaterial(Int32 TotalRegistros, Int32 RegistroActual, Int32 cursoid, Int32 materialid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            List<BeCursoMaterial> lst = new List<BeCursoMaterial>();

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@totalRegistros", TotalRegistros);
                helper.AddParameter("@registroActual", RegistroActual);
                helper.AddParameter("@cursoid", cursoid);
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoCursoMaterial", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeCursoMaterial obj = new BeCursoMaterial();
                    //cursoid, descripcionCurso, materialid, descripcionMaterial
                    obj.curso = new BeCurso() {
                        id = Validacion.DBToInt32(ref reader, "cursoid"),
                        descripcion = Validacion.DBToString(ref reader, "descripcionCurso")
                    };

                    obj.material = new BeMaterial()
                    {
                        id = Validacion.DBToInt32(ref reader, "materialid"),
                        descripcion = Validacion.DBToString(ref reader, "descripcionMaterial")
                    };

                    lst.Add(obj);
                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCursoMaterial -> ObtenerListadoCursoMaterial()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return lst;
        }

        public Int32 ObtenerTotalListadoCursoMaterial(Int32 cursoid, Int32 materialid)
        {
            Int32 total = 0;
            DatabaseHelper helper = null;
            SqlDataReader reader;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@cursoid", cursoid);
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerTotalListadoCursoMaterial", System.Data.CommandType.StoredProcedure);

                while (reader.Read())
                {
                    total = Validacion.DBToInt32(ref reader, "Total");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCursoMaterial -> ObtenerTotalListadoCursoMaterial()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return total;
        }

        public BeCursoMaterial ObtenerCursoMaterialById(Int32 cursoid, Int32 materialid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            BeCursoMaterial obj = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@cursoid", cursoid);
                helper.AddParameter("@materialid", materialid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerCursoMaterialById", System.Data.CommandType.StoredProcedure);
                if (reader.Read())
                {
                    obj = new BeCursoMaterial();

                    obj.curso = new BeCurso()
                    {
                        id = Validacion.DBToInt32(ref reader, "cursoid")
                        //descripcion = Validacion.DBToString(ref reader, "descripcionCurso")
                    };

                    obj.material = new BeMaterial()
                    {
                        id = Validacion.DBToInt32(ref reader, "materialid")
                        //descripcion = Validacion.DBToString(ref reader, "descripcionMaterial")
                    };

                }
            }

            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCursoMaterial -> ObtenerCursoMaterialById()");
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
