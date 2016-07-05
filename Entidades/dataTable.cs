using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class dynamicDataTable
    {

        /// <summary>
        /// Request sequence number sent by DataTable, same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }


        /// <summary>
        /// Selected col for ordering
        /// </summary>
        public Int32 iSortCol_0 { get; set; }
        /// <summary>
        /// Selected col order asc or desc
        /// </summary>
        public string sSortDir_0 { get; set; }

        public Int32 sIdPaquete { get; set; }
        public Int32 sIdAmbiente { get; set; }
        public Int32 sIdSubAdicional { get; set; }
        public Int32 sIdPersona { get; set; }
        public String sNombre { get; set; }
        public Int32 sIdTipoExamen { get; set; }
        public Int32 sIdTipoTarifa { get; set; }
        public Int32 sIdSolicitante { get; set; }
        public Int32 sIdCalendarioVisitaMedica { get; set; }
        public Int32 sIdCampania { get; set; }
        public Int32 sSucursal { get; set; }
        public String sNombres { get; set; }
        public String sDni { get; set; }
        public String sCodigo { get; set; }
        public Int32 sIdMotivoCampania { get; set; }
        public Int32 sIdSucursal { get; set; }
        public Int32 sArea { get; set; }
        public Int32 sTipoPersonal { get; set; }
        public Int32 sEspecialidad { get; set; }
        public Int32 sEmpresa { get; set; }
        public Int32 sZona { get; set; }
        public String sNombreExamen { get; set; }
        public Int32 sTipoExamen { get; set; }
        public Int32 sIdTipoEmpresa { get; set; }
        public Int32 sIdGrupo { get; set; }
        public Int32 sIdPeriodo { get; set; }
        public Int32 sIdPuntoEmision { get; set; }
        public String sEstadoPeriodo { get; set; }
        public Int32 sEsVerdadero { get; set; }

        //vvv Documento
        public Int32 sIdConfigDocumento { get; set; }
        public Int32 sIdDocumento { get; set; }

        //vvvConfigTipoVentaDocumento
        public Int32 sIdTipoVenta { get; set; }

        //vvvConfigGPDocumento
        public Int32 sIdCargo { get; set; }

        

        /// <summary>
        /// Filtros
        /// </summary>
        
        public String sNroOT { get; set; }       
        public String sPaciente { get; set; }        
        public DateTime sInicio { get; set; }
        public DateTime sFin { get; set; }

        public String ssId { get; set; }
        public Int32 sId { get; set; }
        public Int32 sIdModulo { get; set; }       
        public String sNombre2 { get; set; }
        public String sNombre3 { get; set; }
        public String sFInicio { get; set; }
        public String sFFin { get; set; }
        public String sTipo { get; set; }
        public Boolean sEstadoBooleano { get; set; }

        public Int32 sMes { get; set; }

        public String sApellido { get; set; }
        public String sRUC { get; set; }
        public String sRazonSocial { get; set; }

        public Int32 sIdTipoAdi { get; set; }
        public Int32 sIdCita { get; set; }

        public Int32 sEstadoEntero {get;set;}
        public String sFecha {get; set; }
    }
}
