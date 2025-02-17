﻿namespace PersonasAPI.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string Dui { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}
