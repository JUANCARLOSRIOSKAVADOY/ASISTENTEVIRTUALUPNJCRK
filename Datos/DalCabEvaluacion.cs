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
    public class DalCabEvaluacion
    {
        public Boolean InsertarCabEvaluacion(BeCabEvaluacion obj, List<BeEvaluacion> detalle)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                MemoryStream objMS = new MemoryStream();
                using (XmlWriter objXMLW = XmlWriter.Create(objMS))
                {
                    //TotalDetalle
                    objXMLW.WriteStartElement("r");
                    objXMLW.WriteStartElement("dc");
                    foreach (BeEvaluacion det in detalle)
                    {
                        objXMLW.WriteStartElement("d");
                        objXMLW.WriteAttributeString("pregunta", det.pregunta);
                        objXMLW.WriteAttributeString("respuesta", det.respuesta);
                        objXMLW.WriteAttributeString("esCorrecta", det.esCorrecta.ToString());
                        objXMLW.WriteEndElement();
                    }
                    objXMLW.WriteEndElement();

                    objXMLW.WriteEndElement();
                    objMS.Position = 0;

                    Helper.AddParameter("@XMLdata", new SqlXml(objMS));
                    Helper.AddParameter("@cursoid", obj.curso.id);
                    Helper.AddParameter("@materialid", obj.material.id);
                    Helper.AddParameter("@userid", obj.usuario.usuarioid);
                    Helper.AddParameter("@esEscritura", obj.esEscritura);
                }

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_InsertarCabEvaluacion", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCabEvaluacion -> InsertarCabEvaluacion()");
            }
            finally
            {
                if (Helper != null)
                    Helper.Dispose();
            }
            return Resultado;
        }

        public List<BeCabEvaluacion> ObtenerListadoCalificacion(Int32 usuarioid)
        {
            List<BeCabEvaluacion> listado = new List<BeCabEvaluacion>();
            DatabaseHelper helper = null;
            SqlDataReader reader = null;

            try
            {
                helper = new DatabaseHelper(DalConexion.getConexion());
                helper.AddParameter("@usuarioid", usuarioid);
                reader = (SqlDataReader)helper.ExecuteReader("spr_ObtenerListadoCalificacion", System.Data.CommandType.StoredProcedure);
                while (reader.Read())
                {
                    BeCabEvaluacion obj = new BeCabEvaluacion();
                    obj.cabevaluacionid = Validacion.DBToInt32(ref reader, "cabevaluacionid");
                    obj.nota = Validacion.DBToDecimal(ref reader, "nota");
                    obj.evaluacion = new BeEvaluacion()
                    {
                        evaluacionid = Validacion.DBToInt32(ref reader, "evaluacionid"),
                        pregunta = Validacion.DBToString(ref reader, "pregunta"),
                        respuesta = Validacion.DBToString(ref reader, "respuesta")
                    };
                    obj.curso = new BeCurso()
                    {
                        descripcion = Validacion.DBToString(ref reader, "descripcionCurso")
                    };
                    obj.material = new BeMaterial()
                    {
                        descripcion = Validacion.DBToString(ref reader, "descripcionMaterial")
                    };
                    obj.usuario = new BeUsuario()
                    {
                        nombres = Validacion.DBToString(ref reader, "nombres"),
                        ap_paterno = Validacion.DBToString(ref reader, "ap_paterno"),
                        ap_materno = Validacion.DBToString(ref reader, "ap_materno")
                    };

                    listado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCabEvaluacion -> ObtenerListadoCalificacion()");
            }
            finally
            {
                if (helper != null)
                    helper.Dispose();
            }
            return listado;
        }

        public Boolean CalificarNota(Int32 cabevaluacionid, Decimal nota)
        {
            DatabaseHelper Helper = null;
            Boolean Resultado = false;

            try
            {
                Helper = new DatabaseHelper(DalConexion.getConexion());

                Helper.AddParameter("@cabevaluacionid", cabevaluacionid);
                Helper.AddParameter("@nota", nota);

                Resultado = Convert.ToBoolean(Helper.ExecuteNonQuery("spr_CalificarNota", System.Data.CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                clsException localException = new clsException(ex, "DalCabEvaluacion -> CalificarNota()");
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
