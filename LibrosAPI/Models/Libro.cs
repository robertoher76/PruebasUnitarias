﻿namespace LibrosAPI.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor {  get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
