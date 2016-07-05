using BinaryIntellect.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Utilitarios;
using System.Web;

namespace Datos
{
    public class DalUsuario
    {
        public BeUsuario Login (String dni, String psw)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            BeUsuario obj = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_DNI", dni);
                helper.AddParameter("@P_PSW", Utility.EncryptMD5(psw));

                reader = (SqlDataReader)helper.ExecuteReader("spr_Login", System.Data.CommandType.StoredProcedure);

                if (reader.Read())
                {
                    obj = new BeUsuario();
                    obj.usuarioid = Validacion.DBToInt32(ref reader, "usuarioid");
                    obj.nombres = Validacion.DBToString(ref reader, "nombres");
                    obj.ap_paterno = Validacion.DBToString(ref reader, "ap_paterno");
                    obj.ap_materno = Validacion.DBToString(ref reader, "ap_materno");
                    obj.esAdmin = Validacion.DBToBoolean(ref reader, "esAdmin");
                    obj.esDocente = Validacion.DBToBoolean(ref reader, "esDocente");
                    obj.esEstudiante = Validacion.DBToBoolean(ref reader, "esEstudiante");
                    obj.dni = Validacion.DBToString(ref reader, "dni");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalUsuario -> Login()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return obj;
        }

        public BeUsuario ObtenerUsuarioId(Int32 usuarioid)
        {
            DatabaseHelper helper = null;
            SqlDataReader reader;
            BeUsuario obj = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@P_USUARIOID", usuarioid);

                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerUsuarioId", System.Data.CommandType.StoredProcedure);

                if (reader.Read())
                {
                    obj = new BeUsuario();
                    obj.usuarioid = Validacion.DBToInt32(ref reader, "usuarioid");
                    obj.nombres = Validacion.DBToString(ref reader, "nombres");
                    obj.ap_paterno = Validacion.DBToString(ref reader, "ap_paterno");
                    obj.ap_materno = Validacion.DBToString(ref reader, "ap_materno");
                    obj.esAdmin = Validacion.DBToBoolean(ref reader, "esAdmin");
                    obj.esDocente = Validacion.DBToBoolean(ref reader, "esDocente");
                    obj.esEstudiante = Validacion.DBToBoolean(ref reader, "esEstudiante");
                    obj.dni = Validacion.DBToString(ref reader, "dni");
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalUsuario -> ObtenerUsuarioId()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return obj;
        }

        public Boolean InsertarUsuario(BeUsuario obj)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@nombres", obj.nombres);
                Helper.AddParameter("@ap_paterno", obj.ap_paterno);
                Helper.AddParameter("@ap_materno", obj.ap_materno);
                Helper.AddParameter("@dni", obj.dni);
                Helper.AddParameter("@password", Utility.EncryptMD5(obj.password));
                Helper.AddParameter("@esEstudiante", obj.esEstudiante);
                Helper.AddParameter("@esDocente", obj.esDocente);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarUsuario", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalUsuario -> InsertarUsuario()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public BeUsuario ObtenerUsuarioLogueado()
        {
            // Obtener la cookie 
            HttpCookie Cookie = HttpContext.Current.Request.Cookies.Get("IdUsuario");
            BeUsuario obj = ObtenerUsuarioId(Convert.ToInt32(Cookie.Value));
            return obj;
        }
    }
}
