using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InmoCierres.Models;

public class Expediente
{
    public string Id { get; set; } = string.Empty;
    public string ClienteNombre { get; set; } = string.Empty;
    public string PropiedadTitulo { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Avance { get; set; } = 0;
    public DateTime FechaInicio { get; set; } = DateTime.Now;

    // 🧬 NUEVO: El ADN del expediente. Por defecto es "Cierre" para proteger tus datos actuales
    public string TipoTramite { get; set; } = "Cierre"; 

    // Lista de actividades (Vendedor / Timeline de visitas)
    public List<Actividad> Bitacora { get; set; } = new List<Actividad>();

    // Lista de pasos del trámite (Checklist Legal)
    public List<Paso> PasosTramite { get; set; } = new List<Paso>();

    [JsonPropertyName("Colaboradores")]
    public List<string> Colaboradores { get; set; } = new List<string>();

    // 📈 NUEVO: La "mochila" con los datos de marketing para la V2
    public SeguimientoMarketing Marketing { get; set; } = new SeguimientoMarketing();
}

public class Actividad
{
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string Tipo { get; set; } = string.Empty;
    public string Comentarios { get; set; } = string.Empty;
    
    // 🏷️ NUEVO: Para los badges de colores en tu timeline de la V2
    public List<string> Etiquetas { get; set; } = new List<string>(); 
}

// NUEVO: Clase para los KPIs de la Fase 2 (Seguimiento de Venta)
public class SeguimientoMarketing
{
    public int VistasOnline { get; set; } = 0;
    public int Interesados { get; set; } = 0;
    public int CitasFisicas { get; set; } = 0;
    public bool CampanaActiva { get; set; } = false;
    public string DetalleCampana { get; set; } = "Preparando estrategia..."; // Ej: "Publicado en 4 portales"
}

public class Paso
{
    public string Nombre { get; set; } = string.Empty; // Ej. "Avalúo"
    public bool EstaCompletado { get; set; } = false;
    public DateTime? FechaCompletado { get; set; } // El '?' significa que puede estar vacío (nulo)
}

public class UsuarioLogueado 
{
    public string? email { get; set; } // El "?" elimina la advertencia amarilla
}