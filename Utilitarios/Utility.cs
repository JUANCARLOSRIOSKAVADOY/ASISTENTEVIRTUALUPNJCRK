using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Globalization;


namespace Utilitarios
{

    public static class Utility
    {
        public static string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }
            else
            {
                dec = " CON 00/100";
            }

            res = NumeroALetras(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public enum mensaje
        {
            [Description("Registro Satisfactorio. <br />Desea Generar una Boleta de Venta?")]
            registroSatisfactorioGenerarBoletaVenta,            
            [Description("Importacion Satisfactoria.")]
            exitoImportacion,
            [Description("Formato de archivo incorrecto")]
            errorFormatoIncorrecto,
            [Description("No se seleccionó ningun archivo")]
            errorExistenciaArchivo,
            [Description("¿Está seguro de eliminar este registro?")]
            eliminar,
            [Description("Solicitud en proceso...espere.")]
            espere,
            [Description("Registro satisfactorio ")]
            registroSatisfactorio,
            [Description("Actualización satisfactoria ")]
            actualizacionSatisfactoria,
            [Description("Cambio de Estado satisfactorio.")]
            cambioEstadoSatisfactorio,
            [Description("No se seleccionó al personal")]
            errorExistenciaPersonal,
            [Description("Anulación satisfactoria.")]
            anulacionSatisfactoria,
            [Description("Liquidación de Saldo satisfactoria.")]
            liquidacionSatisfactoria,
            [Description("No se pudo realizar la operación ")]
            operacionError,
            [Description("El dato que quiere insertar  ya existe ")]
            elementoExistente,
            [Description("Indique un precio")]
            preciotarifa,
            [Description("¿Está seguro que desea cambiar el estado a este registro?")]
            actualizarEstado,
            [Description("Esta seguro de anular este registro?")]
            anular,
            [Description("El registro seleccionado ha sido eliminado correctamente")]
            datoEliminado,
            [Description("El registro seleccionado no se puede eliminar")]
            errorEliminar,
            [Description("Ingrese un dato válido")]
            campovacio,
            [Description(" ha sido asignado")]
            asignado,
            [Description(" ha sido desasignado")]
            desasignado,
            [Description("Password Incorrecto")]
            pwdIncorrecto,
            [Description("El día elegido no tiene ningun horario")]
            horarioVacio,
            [Description("No se puede actualizar porque ya existe un horario")]
            actualizacionHorarioIncorrecto,
            [Description("EL horario ha sido ingresado correctamente")]
            horarioIngresadoCorrecto,
            [Description("EL horario no se puedo ingresar porque ya existe")]
            horarioIngresadoIncorrecto,
            [Description("Se ha iniciado correctamente")]
            iniciarOrdenServicio,
            [Description("Se ha detenido correctamente")]
            detenerOrdenServicio,
            [Description("Se ha indicado que la orden está para reprogramar")]
            porReprogramarOrdenServicio,
            [Description("Se ha finalizado correctamente")]
            finalizarOrdenServicio,
            [Description("La cita ha sido ingresada correctamente")]
            citaIngresadaCorrecto,
            [Description("La cita ha sido cancelada correctamente")]
            citaCanceladaCorrecto,
            
            [Description("La cita no se pudo ingresar")]
            citaIngresadaIncorrecto,
            
            [Description("La cita no se pudo actualizar")]
            citaActualizadaIncorrecto,

            [Description("La orden de servicio no se pudo ingresar")]
            ordenServicioIngresadaIncorrecto,

            [Description("La orden de servicio no puede reprogramarse, porque aun está a tiempo de atenderse.")]
            ordenServicioNoPuedeReprogramarse,


            [Description("La orden de servicio no se pudo actualizar")]
            ordenServicioActualizadaIncorrecto,

            [Description("La cita no se pudo cancelar")]
            citaCanceladaIncorrecto,
            [Description("Operación no válida.")]



            operacionNoValida,
            [Description("Agregue Examenes o Tipos de Examen")]
            examencampaña,
            [Description("No tiene permisos para Actualizar los datos en su estado actual. ¿Desea efectuar la operación como Administrador?")]
            sinPermisoActualizar,
            [Description("Operación no válida: La Fecha y hora de la Cita es menor que la hora actual.")]
            operacionNoValidaHoraCitaMenorHoraActual,
            [Description("El Tecnólogo no está disponible en ese horario o hay un cruce de horario con la fecha y hora de la cita editada.")]
            operacionNoValidaCruceHoraCita,


            [Description("No tiene permisos para Actualizar la Orden, porque ya esté informada o usted no tiene los permisos necesarios. ¿Desea efectuar la operación como Administrador?")]
            sinPermisoActualizarOrdenServicio,

            [Description("No tiene permisos para Registrar los datos. ¿Desea efectuar la operación como Administrador?")]
            sinPermisoInsertar,
            
            [Description("No tiene permisos para Anular los datos Registrados. ¿Desea efectuar la operación como Administrador?")]
            sinPermisoAnular,

            [Description("No tiene permisos para Leer los datos Registrados. ¿Desea efectuar la operación como Administrador?")]
            sinPermisoLeer,

            [Description("No tiene permisos para Auditar el Registro. ¿Desea efectuar la operación como Administrador?")]
            sinPermisoAuditar,
            
            
            [Description("¿Está seguro de desasignar el elemento?")]
            desasignar,
            [Description("La fecha u hora de ejecución de la orden de servicio es menor que la fecha u hora actual.")]
            fechaEjecucionOrdenServicioIncorrecta,
            [Description("La fecha u hora de la cita es menor que la fecha u hora actual.")]
            fechaHoraCitaIncorrecta,
            //[Description("El horario no se puede ingresar porque ya existe un ambiente en esa hora.")]
            [Description("El horario no se puede ingresar porque el ambiente ya está asignado para otro tecnólogo.")]
            HorarioIncorrectoPorAmbiente,
            //[Description("El horario no se puede ingresar porque ya existe un horario en esa hora.")]
            [Description("El horario no se puede ingresar porque el tecnólogo ya tiene un horario asignado para esa fecha.")]
            HorarioIncorrectoPorHora,
            [Description("El horario no se puede ingresar porque el ambiente no está disponible para esa fecha.")]
            HorarioIncorrectoPorConfiguracionAmbiente,
            [Description("Elija quién recepciono la llamada")]
            recepcion,
            [Description("Seleccione un paciente para ingresar la cita")]
            pacienteCita,
            [Description("Seleccione un Tipo de Cuenta")]
            tipoCuenta,
            [Description("El código del nodo hijo no puede ser igual al código del nodo padre")]
            codigoPlanCuentas,
            [Description("¿Esta seguro de desactivar el registro?")]
            confirmacion_desactivar            
        }







        public enum menus
        {
            [Description("Orden de Servicio")]
            OrdenServicio,
            [Description("TipoTarifa")]
            TipoTarifa,
        }








               

        public enum tipoMensaje
        {
            Confirmacion,
            Pregunta,
            Respuesta,
            Mensaje,
            Detalle,
            OK,
            ERROR,
            Acceso_Privilegiado
        }

        public static string rechazarGuiaRecepcion()
        {
            return "Rechazar Guia Recepción";
        }

        public static string rechazarSalidaInterna()
        {
            return "Rechazar Salida Interna";
        }

        public static string aprobarSalidaInterna()
        {
            return "Aprobar Salida Interna";
        }

        public static string aprobarGuiaRecepcion()
        {
            return "Aprobar Guia Recepción";
        }

        public static string aprobarRequerimientoInterno() {
            return "Aprobar Requerimiento Interno";
        }


        public static string editarInformacionUsuario()
        {
            return "Actualizar Foto y Contraseña";
        }

        public static string confirmarRequerimientoInterno()
        {
            return "Confirmar Requerimiento Interno";
        }

        public static string confirmarOrdenCompra()
        {
            return "Confirmar Orden Compra";
        }

        public static string editarCotizacion()
        {
            return "Editar Cotización";
        }

        public static string nuevaCotizacion()
        {
            return "Nueva Cotización";
        }

        public static string nuevoSolicitudCotizacion()
        {
            return "Nueva Solicitud Cotización";
        }


        public static string rechazarRequerimientoInterno() {
            return "Rechazar Requerimiento Interno";
        }

        public static string detalleSalidaInterna()
        {
            return "Detalle Salida Interna";
        }

        public static string detalleGuiaRecepcion()
        {
            return "Detalle Guia Recepcion";
        }

        public static string detallePedidoCompra()
        {
            return "Detalle Pedido Compra";
        }

        public static string detalleRequerimientoInterno(){
            return "Detalle Requerimiento Interno";
        }

        public static string detalleOrdenCompra()
        {
            return "Detalle Orden Compra";
        }

        public static string detalleOrdenPago()
        {
            return "Detalle Orden Pago";
        }

        public static string nuevoRequerimientoInterno(){
            return "Nuevo Requerimiento Interno";
        }

        public static string nuevoAsientoApertura()
        {
            return "Nuevo Asiento Apertura";
        }

        public static string editarAsientoApertura()
        {
            return "Editar Asiento Apertura";
        }

        public static string detalleAnalisisAsientoApertura()
        {
            return "Detalle Analisis";
        }

        public static string editarSalidaInterna()
        {
            return "Editar Salida Interna";
        }

        public static string editarGuiaRecepcion()
        {
            return "Editar Guia Recepcion";
        }

        public static string editarRequerimientoInterno() {
            return "Editar Requerimiento Interno";
        }

        public static string editarOrdenCompra()
        {
            return "Editar Orden Compra";
        }

        public static string editarOrdenPago()
        {
            return "Editar Orden Pago";
        }

        public static string nuevaSalidaIntera()
        {
            return "Nueva Salida Interna";
        }

        public static string nuevaGuiaRecepcion()
        {
            return "Nueva Guia Recepción";
        }

        public static string nuevoPedidoCompra()
        {
            return "Nuevo Pedido Compra";
        }

        public static string editarPedidoCompra()
        {
            return "Editar Pedido Compra";
        }

        public static string liquidarOrdenServicioCredito()
        {
            return "Liquidación de Orden de Servicio - CREDITO";
        }


        public static string nuevoLugarEntrega() {
            return "Nuevo Lugar Entrega";
        }

        public static string busquedaAvanzadaProducto() {
            return "Busqueda Avanzada de Productos";
        }

        public static string empresa()
        {
            return "Tomonorte";
        }
        public static string solicitantes()
        {
            return "Listado de Solicitantes";
        }
        public static string medicos()
        {
            return "Listado de Medicos";
        }
        public static string productos()
        {
            return "Listado de Productos";
        }
        public static string clientes()
        {
            return "Listado de Pacientes";
        }
        public static string valeCampania()
        {
            return "Generar Vales de Campaña";
        }

        public static string buscarEmpresa()
        {
            return "Selección de Empresa";
        }

        public static string buscarCita()
        {
            return "Selección de Cita";
        }

        public static String buscarCartaGarantia()
        {
            return "Selección de Carta de Garantía";
        }

        public static string buscarContactoEmpresa()
        {
            return "Selección de Contacto de Empresa";
        }

        public static string buscarPersona()
        {
            return "Buscar ";
        }

        public static string grafico()
        {
            return "Gráfico ";
        }

        public static string liquidarPagoOrdenServicio()
        {
            return "Liquidación de Saldo de Órdenes de Servicio.";
        }

        public static string tituloAuditoria()
        {
            return "Auditoria de Tablas.";
        }

        public static string observacionOrdenServicio()
        {
            return "Observación de Orden de Servicio";
        }

        public static string detalleOrdenServicio()
        {
            return "Detalle de Orden de Servicio";
        }

        public static string ordenServicio()
        {
            return "Orden de Servicio";
        }

        public static string ReporteCitasMedicosAnestesiologos()
        {
            return "Reporte de Médicos Anestesiólogos";
        }

        public static string historiaClinicaPaciente()
        {
            return "Historia Clínica de ";
        }

        public static string registrarDatosMedicos_PorTecnologo()
        {
            return "Datos Médicos para la orden: ";
        }

        public static string informeOrdenServicio()
        {
            return "Informe";
        }

        public static Int32 buscarContactoEmpresa_NroFilas()
        {
            return 5;
        }

        public static string modalMantenimiento()
        {

            return "Mantenimiento";
        }

        public static string nuevoProducto()
        {
            return "Nuevo Producto";
        }

        public static string editarProducto()
        {
            return "Editar Producto";
        }

        public static string nuevoVoucher()
        {
            return "Nuevo Voucher";
        }

        public static string editarVoucher()
        {
            return "Editar Voucher";
        }

        public static string AsignarTecnologoHorarioAtencionServicio()
        {
            return "Seleccionar Tecnológo / Horario de Atención para: ";
        }

        public static Int32 CalcularIntervaloAños(DateTime fechaInicial, DateTime? fechaFinal = null)
        {
            Int32 intervalo = 0;
            if (fechaInicial != DateTime.MinValue)
            {
                if (fechaFinal == null)
                    fechaFinal = DateTime.Now;
                intervalo = ((DateTime)fechaFinal).Year - fechaInicial.Year;
                if (fechaFinal < fechaInicial.AddYears(intervalo))
                    intervalo--;
            }
            return intervalo;
        }

        public static String ConvertirMinutosAHorasMinutos(Int32 Minutos, Boolean EsCompleto)
        {
            String cadena = "";
            if (EsCompleto) {
                cadena = (Minutos / 60).ToString() + "h " + (Minutos % 60).ToString() + " m";
            }
            return cadena;
        }

        public static String CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;
            return Convert.ToString(edad);
        }

        public static string LocalIPAddress()
        {
            System.Net.IPHostEntry host;
            string localIP = "";
            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public static string GetNombreSistemaOperativo()
        {
            System.OperatingSystem os = System.Environment.OSVersion;
            string osName = "Unknown";
            switch (os.Platform)
            {
                case System.PlatformID.Win32Windows:
                    switch (os.Version.Minor)
                    {
                        case 0: osName = "Windows 95"; break;
                        case 10: osName = "Windows 98"; break;
                        case 90: osName = "Windows ME"; break;
                    }
                    break;
                case System.PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3: osName = "Windws NT 3.51"; break;
                        case 4: osName = "Windows NT 4"; break;
                        case 5:
                            if (os.Version.Minor == 0)
                                osName = "Windows 2000";
                            else if (os.Version.Minor == 1)
                                osName = "Windows XP";
                            else if (os.Version.Minor == 2)
                                osName = "Windows Server 2003";
                            break;
                        case 6:
                            osName = "Otro";
                            if (os.Version.Minor == 0)
                                osName = "Windows Vista/Windows Server 2008";
                            else if (os.Version.Minor == 1)
                                osName = "Windows 7";
                            else if (os.Version.Minor == 2)
                                osName = "Windows 8";
                            break;
                    }
                    break;
            }
            return osName + " (" + os.ToString() + ")";
        }

        ///// <summary>
        ///// convierte de DateTime a UNIX Timestamp
        ///// summary>
        ///// <param name="value">Date to convertparam>
        ///// <returns>returns>
        public static double ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

        public static DateTime ConvertToDateTime(double value)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(value);
        }

        public static string ObtenerNombreMesNumero(int numeroMes)
        {
            try
            {
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                string nombreMes = formatoFecha.GetMonthName(numeroMes);
                return nombreMes[0].ToString().ToUpper() + nombreMes.Substring(1);
            }
            catch
            {
                return "Desconocido";
            }
        } 

        public static String EncryptMD5(String password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

        public enum FormatoIntervaloFechas
        {
            UNA_FILA_TOTAL,
            SEPARADO_FILAS_OPCIONAL_DIAS,
            UNA_FILA_OPCIONAL_DIAS
        }

        public static String ObtenerDiferenciaIntervaloFechas(DateTime fecha1, DateTime fecha2, FormatoIntervaloFechas formatoIntervaloFechas)
        { 
            String strDiferenciaIntervaloFechas = "";
            if (fecha1 != DateTime.MinValue && fecha2 != DateTime.MinValue){
                TimeSpan diferencia;
                if (fecha1 < fecha2)
                {
                    diferencia = (fecha2 - fecha1);
                }
                else {
                    diferencia = (fecha1 - fecha2);
                }
                if (GetEnumDescription(formatoIntervaloFechas) == FormatoIntervaloFechas.UNA_FILA_TOTAL.ToString())
                {
                    strDiferenciaIntervaloFechas = diferencia.Days + " d " + diferencia.Hours.ToString("0#") + " h " + diferencia.Minutes.ToString("0#") + " m " + diferencia.Seconds.ToString("0#") + " s";
                }
                else if (GetEnumDescription(formatoIntervaloFechas) == FormatoIntervaloFechas.SEPARADO_FILAS_OPCIONAL_DIAS.ToString())
                {
                    if (diferencia.Days != 0)
                        strDiferenciaIntervaloFechas = diferencia.Days.ToString(" #") + " d" + "<br/>";
                    if (strDiferenciaIntervaloFechas != "" && diferencia.Hours != 0) {
                        strDiferenciaIntervaloFechas = strDiferenciaIntervaloFechas + diferencia.Hours.ToString("0#") + " h<br/>";    
                    }
                    if (strDiferenciaIntervaloFechas != "" && diferencia.Minutes != 0) {
                        strDiferenciaIntervaloFechas = strDiferenciaIntervaloFechas + diferencia.Minutes.ToString("0#") + " m</br>";
                    }
                    strDiferenciaIntervaloFechas = strDiferenciaIntervaloFechas + diferencia.Seconds.ToString("0#") + " s";
                }
                else if (GetEnumDescription(formatoIntervaloFechas) == FormatoIntervaloFechas.UNA_FILA_OPCIONAL_DIAS.ToString())
                {
                    if (diferencia.Days != 0)
                        strDiferenciaIntervaloFechas = diferencia.Days.ToString(" #") + " d ";
                    
                    if (diferencia.Hours != 0)
                    {
                        strDiferenciaIntervaloFechas = strDiferenciaIntervaloFechas + diferencia.Hours.ToString("0#") + " h ";
                    }
                    
                    if (diferencia.Minutes != 0)
                    {
                        strDiferenciaIntervaloFechas = strDiferenciaIntervaloFechas + diferencia.Minutes.ToString("0#") + " m ";
                    }
                    strDiferenciaIntervaloFechas = strDiferenciaIntervaloFechas + diferencia.Seconds.ToString("0#") + " s";
                }
                return strDiferenciaIntervaloFechas;
            }    
            else
                return "";
            
        }

        public static String obtenerDiferenciaFechas_DMY(DateTime fecha1, DateTime fecha2, String parte) {

            Int32 anios;
            Int32 meses;
            Int32 dias;
            String str = "";

            if (fecha1 == DateTime.MinValue)
                return "- años";

            anios = (fecha2.Year - fecha1.Year);
            meses = (fecha2.Month - fecha1.Month);
            dias = (fecha2.Day - fecha1.Day);

            Int32 intervalo = ((DateTime)fecha2).Year - fecha1.Year;
            if (fecha2 < fecha1.AddYears(intervalo))
                anios--;

            if (meses < 0)
            {
                //anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(fecha2.Year, fecha2.Month);
            }

            if (anios == 0 && meses == 0)
                dias++;

            if (anios < 0)
            {
                return "Fecha Invalida";
            }

            if (parte == "TODO_CONCATENADO")
            {
                if (anios > 0)
                    str = str + anios.ToString() + " años ";
                if (meses > 0)
                    str = str + meses.ToString() + " meses ";
                if (dias > 0)
                    str = str + dias.ToString() + " dias ";
            }
            else if (parte == "AÑOS")
            {
                if (anios > 1)
                    str = anios.ToString() + " años";
                else
                    str = anios.ToString() + " año";
            }
            else if (parte == "REDONDEADO")
            {   // o años o meses o dias
                if (anios > 1)
                    str = anios.ToString() + " años";
                else if (anios == 1)
                    str = anios.ToString() + " año";
                else if (meses > 1)
                    str = meses.ToString() + " meses";
                else if (meses == 1)
                    str = meses.ToString() + " mes";
                else if (dias > 1)
                    str = dias.ToString() + " días";
                else if (dias == 1)
                    str = dias.ToString() + " día";
            }
            else if (parte == "REDONDEADO_CONCATENADO_COMPLETO")
            {   // o años y meses // o meses  o dias
                if (anios > 1)
                    str = anios.ToString() + " años";
                else if (anios == 1)
                    str = anios.ToString() + " año";
                if (str != "") {
                    str += " ";
                    if (meses > 1)
                        str += meses.ToString() + " meses";
                    else if (meses == 1)
                        str += meses.ToString() + " mes";
                }
                else{
                    if (meses > 1)
                        str = meses.ToString() + " meses";
                    else if (meses == 1)
                        str = meses.ToString() + " mes";
                    else if (dias > 1)
                        str = dias.ToString() + " días";
                    else if (dias == 1)
                        str = dias.ToString() + " día";    
                }
                
            }
            else if (parte == "REDONDEADO_CONCATENADO_ABREVIADO")
            {   // o años y meses // o meses  o dias
                if (anios > 1)
                    str = anios.ToString() + " años";
                else if (anios == 1)
                    str = anios.ToString() + " años";
                if (str != "")
                {   
                    if (meses > 1)
                        str += ", " + meses.ToString() + " m.";
                    else if (meses == 1)
                        str += ", " + meses.ToString() + " m.";
                }
                else
                {
                    if (meses > 1)
                        str = meses.ToString() + " m";
                    else if (meses == 1)
                        str = meses.ToString() + " m";
                    else if (dias > 1)
                        str = dias.ToString() + " d";
                    else if (dias == 1)
                        str = dias.ToString() + " d";
                }

            }

            return str;
        }

        public static string detalleSolicitudCotizacion()
        {
            return "Detalle Solicitud Cotizacion";
        }

        public static string editarSolicitudCotizacion()
        {
            return "Editar Solicitud Cotizacion";
        }

        public static string confirmarEnvioEmailProveedores()
        {
            return "Confirmar Envío de Email a Proveedores";
        }

        public enum TipoEtiqueta_EGP
        { 
            Titulo = 1,
            Detalle = 2,
            SubtotalDetalle = 3,
            Subtotal = 4      
        }

        public static string nuevaOrdenCompra()
        {
            return "Nueva Orden Compra";
        }

        public static string nuevaOrdenPago()
        {
            return "Nueva Orden Pago";
        }
    }

}
