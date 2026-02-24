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

    // Lista de actividades (Vendedor)
    public List<Actividad> Bitacora { get; set; } = new List<Actividad>();

    // NUEVO: Lista de pasos del trámite (Checklist)
    public List<Paso> PasosTramite { get; set; } = new List<Paso>();

    [JsonPropertyName("Colaboradores")]
    public List<string> Colaboradores { get; set; } = new List<string>();
}

public class Actividad
{
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string Tipo { get; set; } = string.Empty;
    public string Comentarios { get; set; } = string.Empty;
}

// NUEVO: Definición de un "Paso"
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