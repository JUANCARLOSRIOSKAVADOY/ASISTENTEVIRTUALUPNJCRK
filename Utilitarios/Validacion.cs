using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.ComponentModel;
namespace Utilitarios
{
   static public class Validacion
   {
       #region Validaciones para Tipo de dato 
       
       public static DateTime DBToDateTime(ref SqlDataReader reader, string ColumnName)
        {
            return (reader.IsDBNull(reader.GetOrdinal(ColumnName))) ? DateTime.MinValue : Convert.ToDateTime(reader[ColumnName]);
        }

       public static string DBToString(ref SqlDataReader reader, string ColumnName)
        {
            return Convert.ToString(reader[ColumnName]);
        }

       public static int DBToInt32(ref SqlDataReader reader, string ColumnName)
        {
            return (reader.IsDBNull(reader.GetOrdinal(ColumnName))) ? (Int32)0 : (Int32)reader[ColumnName];
        }

       public static decimal DBToDecimal(ref SqlDataReader reader, string ColumnName)
        {
            return (reader.IsDBNull(reader.GetOrdinal(ColumnName))) ? (decimal)0 : (decimal)reader[ColumnName];
        }

       public static short DBToInt16(ref SqlDataReader reader, string ColumnName)
        {
            return (reader.IsDBNull(reader.GetOrdinal(ColumnName))) ? (short)0 : (short)reader[ColumnName];
        }

       public static bool DBToBoolean(ref SqlDataReader reader, string ColumnName)
        {
            return (reader.IsDBNull(reader.GetOrdinal(ColumnName))) ? false : (bool)reader[ColumnName];
        }

       #endregion

       #region Validaciones para Controles de Formularios

       public static bool ValidarDecimales(string txtDato, CancelEventArgs e)
        {
            Regex isDecimal = new Regex(@"^-?[0-9]+(\.?[0-9]+)?$");
            if (isDecimal.IsMatch(txtDato))
                e.Cancel = false;
            else
                e.Cancel = true;
            return e.Cancel;
        }

       

       public static bool ValidarNumerosEnteros(string txtDato, CancelEventArgs e)
       {
           Regex isNumero = new Regex(@"^[0-9]+$");
           if (isNumero.IsMatch(txtDato))
               e.Cancel = false;
           else
               e.Cancel = true;
           return e.Cancel;
       }
       // VALIDACION RILOUDEDD xD
       public static bool ValidarEmail(string txtDato)
       {
           bool e;
           Regex isMail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
           if (isMail.IsMatch(txtDato))
               e = false;
           else
               e = true;
           return e;
       }
       public static bool ValidarNumeroEntero(string txtDato)
       {
           bool e;
           //Regex isEntero = new Regex(@"^\d*\.?\d*$");
           Regex isEntero = new Regex(@"^\d*$");
           if (isEntero.IsMatch(txtDato))
               e = false;
           else
               e = true;
           return e;
       }
       public static bool ValidarAlfaNumerico(string txtDato)
       {
           bool e;
           Regex isAlfanumerico = new Regex(@"^[a-zA-Z0-9]+$");
           
           if (isAlfanumerico.IsMatch(txtDato)) e = false;
           else e = true;

           return e;
       }
       public static bool ValidarObservaciones(string txtDato)
       {
           bool e;
           Regex isObservacion = new Regex(@"^(/w|/W|[^<>{}&])+$");

           if (isObservacion.IsMatch(txtDato)) e = false;
           else e = true;

           return e;
       }
       public static bool ValidarDireccion(string txtDato)
       {
           bool e;
           Regex isDireccion = new Regex(@"^(/w|/W|[^<>{}&'°#?;""])+$");

           if (isDireccion.IsMatch(txtDato)) e = false;
           else e = true;

           return e;
       }
       public static bool ValidarAlfabetico(string txtDato)
       {
           bool e;
           Regex isAlfabetico = new Regex(@"^[a-zA-Z\s.\-_ñÑ']+$");

           if (isAlfabetico.IsMatch(txtDato)) e = false;
           else e = true;

           return e;
       }
       public static bool ValidarTelefono(string txtDato)
       {
           bool e;
           Regex isTelefono = new Regex(@"^(\(?\d\d\d\)?)?( |\#|-|\.)?\d\d\d?\d( |\*|-|\.)?\d{3,4}(( |-|\.)?[ext\.]+ ?\d+)?$");

           if (isTelefono.IsMatch(txtDato)) e = false;
           else e = true;

           return e;
       }
       public static bool ValidarNumeroDecimal(string txtDato)
       {
           bool e;
           Regex isEntero = new Regex(@"^[0-9]*(\.)?[0-9]+$");
           if (isEntero.IsMatch(txtDato))
               e = false;
           else
               e = true;
           return e;
       }
       #endregion
   }

}