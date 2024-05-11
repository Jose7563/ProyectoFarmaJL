using Microsoft.AspNetCore.DataProtection.KeyManagement;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFarmacia.Models
{
    public class Imagen
    {
        public int IdArchivo { get; set; }
        public string? Nombre { get; set; }
        public byte[] Archivo { get; set; }
        public string? Extension { get; set; }

 
    }
}
