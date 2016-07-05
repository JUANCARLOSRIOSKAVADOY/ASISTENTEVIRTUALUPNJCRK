using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilitarios
{
    public class clsEscribirArchivoControl : Exception
    {
        private static string _ApplicationPath {set; get;}
        private static string _UID {set; get; }
        
        public clsEscribirArchivoControl()
        { 
        }

        public clsEscribirArchivoControl(Exception ex, string strLocation, String rutaArchivo)
        {
            DateTime now = DateTime.Now;
            string ErrorMessage = "ERROR -> " + now.ToShortDateString() + "-" + now.ToShortTimeString() + " @ " + strLocation + "-> " + ex.Message + " -> User:" + _UID + System.Environment.NewLine;
            string[] lines = Regex.Split(ex.StackTrace, "\r\n");
            for (int i = 0; i < lines.GetLength(0); i++)
            {
                ErrorMessage += lines[i] + System.Environment.NewLine;
            }


            System.IO.File.AppendAllText(rutaArchivo, ErrorMessage + "********************************************" + System.Environment.NewLine);
            
            //System.IO.File.AppendAllText(@"d:\exception.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
            //System.IO.File.AppendAllText(@"\\5.100.68.9\c$\BRItor.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
        }

        public clsEscribirArchivoControl(string mensaje, string localizacion, String rutaArchivo)
        {
            DateTime now = DateTime.Now;
            string ErrorMessage = "MENSAJE -> " + now.ToShortDateString() + "-" + now.ToShortTimeString() + " @ " + localizacion + "-> " + mensaje + " -> User:" + _UID + System.Environment.NewLine;
            string[] lines = Regex.Split(mensaje, "\r\n");
            for (int i = 0; i < lines.GetLength(0); i++)
            {
                ErrorMessage += lines[i] + System.Environment.NewLine;
            }


            System.IO.File.AppendAllText(rutaArchivo, ErrorMessage + "********************************************" + System.Environment.NewLine);

            //System.IO.File.AppendAllText(@"d:\exception.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
            //System.IO.File.AppendAllText(@"\\5.100.68.9\c$\BRItor.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
        }
    }
}
