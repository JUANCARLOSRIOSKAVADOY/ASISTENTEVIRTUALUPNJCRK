using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilitarios
{
    public class clsException : Exception
    {
        private static string _ApplicationPath {set; get;}
        private static string _UID {set; get; }
        
        public clsException()
        { 
        }

        public clsException(Exception ex, string strLocation)
        {
            DateTime now = DateTime.Now;
            string ErrorMessage = "ERROR -> " + now.ToShortDateString() + "-" + now.ToShortTimeString() + " @ " + strLocation + "-> " + ex.Message + " -> User:" + _UID + System.Environment.NewLine;
            string[] lines = Regex.Split(ex.StackTrace, "\r\n");
            for (int i = 0; i < lines.GetLength(0); i++)
            {
                ErrorMessage += lines[i] + System.Environment.NewLine;
            }


            //System.IO.File.AppendAllText("C:"+"\\cassiopeia\\exception2.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
            
            //System.IO.File.AppendAllText(@"d:\exception.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
            //System.IO.File.AppendAllText(@"\\5.100.68.9\c$\BRItor.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
        }

        public clsException(string mensaje, string localizacion)
        {
            DateTime now = DateTime.Now;
            string ErrorMessage = "ERROR -> " + now.ToShortDateString() + "-" + now.ToShortTimeString() + " @ " + localizacion + "-> " + mensaje + " -> User:" + _UID + System.Environment.NewLine;
            string[] lines = Regex.Split(mensaje, "\r\n");
            for (int i = 0; i < lines.GetLength(0); i++)
            {
                ErrorMessage += lines[i] + System.Environment.NewLine;
            }


            System.IO.File.AppendAllText("C:" + "\\cassiopeia\\exception.log", ErrorMessage + "********************************************" + System.Environment.NewLine);

            //System.IO.File.AppendAllText(@"d:\exception.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
            //System.IO.File.AppendAllText(@"\\5.100.68.9\c$\BRItor.log", ErrorMessage + "********************************************" + System.Environment.NewLine);
        }
    }
}
