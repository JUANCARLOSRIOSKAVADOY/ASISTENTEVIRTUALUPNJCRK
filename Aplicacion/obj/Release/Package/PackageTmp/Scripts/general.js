/**
 * Created by JONATHAN on 10/08/14.
 */ 
var root = location.protocol + '//' + location.host;

var General = new function () {
    return {
        init_botones : function(){
            $("#txtBusquedaCabecera").off("keyup");
            $("#txtBusquedaCabecera").on("keyup",function(e){
                //KeyCode 8 es el backspace
                if(e.keyCode != 116 || e.keyCode != 117 || e.keyCode != 122 || e.keyCode != 123){
                    var descripcion = $(this).val();
                    if (e.keyCode == 27 && $("#sectionBusquedaUsuario").length) {
                        $("#sectionBusquedaUsuario").remove();
                        $("#txtBusquedaCabecera").val("");
                    }
                    else { 
                    $.ajax({
                        url: root + "/Usuario/ObtenerListadoBusquedaUsuariaHeader",
                        data: "descripcion="+descripcion,
                        type: "POST",
                        success: function(data){
                            //alert(data);
                            if($("#sectionBusquedaUsuario").length) $("#sectionBusquedaUsuario").remove();
                            //alert(e.keyCode +" - "+ $("#txtBusquedaCabecera").val());
                            if(!(e.keyCode == 8 && $("#txtBusquedaCabecera").val() == "")){
                                var cantidadElementos = data.length;
                                if($("#sectionBusquedaUsuario").length) $("#sectionBusquedaUsuario").remove();

                                var contenedor = $("<section/>",{
                                    id: "sectionBusquedaUsuario",
                                    "class": "panel",
                                    css: {
                                        "z-index": 800,
                                        "position": "absolute",
                                        "border": "1px solid #eaeaea",
                                        "width": "100%"
                                    }
                                });
                                var grupo = $("<div/>",{"class":"panel-body active",css:{"padding":"0"}});

                                for(var i in data){
                                    //alert(data[i].Persona.NombreCompleto)
                                    //Creo cada fila que corresponde a una persona o empresa
                                    var row = $("<article/>",{"data-idusuario":data[i].id,"class":"media cursorPointer removeMargin",css:{"padding":"8px 15px"}});
                                    var imagen = $('<a class="pull-left thumb p-thumb"><img src="'+((data[i].Persona!=null)?"":data[i].Comerciante.logo)+'"></a>');
                                    var descripcion = $('<div class="media-body"><label class="cmt-head">'+((data[i].Persona!=null)?data[i].Persona.NombreCompleto:data[i].Comerciante.razonSocial)+'</label><p> <i class="icon-info-sign"></i> '+((data[i].Persona!=null)?data[i].Persona.Distrito.descripcion+" - "+data[i].Persona.Distrito.Provincia.descripcion:data[i].Comerciante.ruc+" - "+data[i].Comerciante.direccion)+'</p></div>');

                                    row.append(imagen).append(descripcion);
                                    grupo.append(row);
                                }

                                if(cantidadElementos == 0) {
                                    var row = $("<article/>",{"class":"media"});
                                    var descripcion = $('<div class="media-body"><p><i class="icon-frown"></i> No se encontraron resultados.</p></div>');
                                    row.append(descripcion);
                                    grupo.append(row);
                                }

                                contenedor.append(grupo);
                                $("#txtBusquedaCabecera").parent().append(contenedor);

                                contenedor.find("article").off("mouseover");
                                contenedor.find("article").on("mouseover",function(){
                                    $(this).addClass("azulFb");
                                }).on("mouseout",function(){
                                    $(this).removeClass("azulFb");
                                })
                                /*<section class = "panel" style="z-index:800; position:absolute; border: 1px solid #eaeaea">
                                    <header class="panel-heading">
                                    Personas
                                    </header>
                                    <div class="panel-body active">
                                        <article class="media">
                                            <a class="pull-left thumb p-thumb">
                                                <img src="img/avatar-mini.jpg">
                                                </a>
                                                <div class="media-body">
                                                    <a class="cmt-head" href="#">Lorem ipsum dolor sit amet, consectetur adipiscing elit.</a>
                                                    <p> <i class="icon-time"></i> 1 hours ago</p>
                                                </div>
                                            </article>
                                            </div>
                                        </section>*/
                            }

                            $("#sectionBusquedaUsuario article").off("click");
                            $("#sectionBusquedaUsuario article").on("click",function(){
                                var attr = $(this).attr('data-idusuario');
                                if (typeof attr !== typeof undefined && attr !== false) {
                                    //alert(attr)
                                    location.href = root + "/Acceso/Home/Profile?IdUsuario="+attr;
                                }
                            })
                        }
                    })
                    }
                }
            })
        },
        op_verificarExisteEventoCalendario: function(calendar, evento) {
            var array = calendar.fullCalendar('clientEvents');

            var start_nuevo_evento = new Date(evento.start);
            var end_nuevo_evento = new Date(evento.end);

            for (i in array) {
                if (array[i].id != evento.id) {

                    var start_evento_existente = new Date(array[i].start);
                    var end_evento_existente = new Date(array[i].end);
                                   
                    // start-time in between any of the events
                    if (start_nuevo_evento > start_evento_existente && start_nuevo_evento < end_evento_existente) {
                        return true;
                    }
                    //end-time in between any of the events
                    if (end_nuevo_evento > start_evento_existente && end_nuevo_evento < end_evento_existente) {
                        return true;
                    }
                    //any of the events in between/on the start-time and end-time
                    if (start_nuevo_evento <= start_evento_existente && end_nuevo_evento >= end_evento_existente) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}();

$(document).ready(function(){
    //General.init_botones();
    window.speechSynthesis.cancel();
})
