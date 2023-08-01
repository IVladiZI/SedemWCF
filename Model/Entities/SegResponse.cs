using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Model.Entities
{
    public class SegResponse
    {
        private HttpStatusCode statusCode;
        public bool Succeeded { get; set; }
        public ObjectEntity Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get => statusCode; set => statusCode = value; }
    }
    public class SegUsuarioDto
    {
        [JsonProperty("idsegUsuarioSistema")]
        public int IdsegUsuarioSistema { get; set; }
        [JsonProperty("nombreCompleto")]
        public string NombreCompleto { get; set; }
        [JsonProperty("nroCi")]
        public string NroCi { get; set; }
        [JsonProperty("espedido")]
        public string Espedido { get; set; }
        [JsonProperty("idsegPerfil")]
        public int IdsegPerfil { get; set; }
        [JsonProperty("perfil")]
        public string Perfil { get; set; }
        [JsonProperty("loginUsuario")]
        public string LoginUsuario { get; set; }
        [JsonProperty("idgenInstitucionsucursal")]
        public int IdgenInstitucionsucursal { get; set; }
        [JsonProperty("idgenInstitucion")]
        public int IdgenInstitucion { get; set; }
        [JsonProperty("institucion")]
        public string Institucion { get; set; }
        [JsonProperty("sucursal")]
        public string Sucursal { get; set; }
        [JsonProperty("estado")]
        public string Estado { get; set; }
        [JsonProperty("roles")]
        public object Roles { get; set; }
        [JsonProperty("jwToken")]
        public string JwToken { get; set; }

    }
    public class ObjectEntity
    {
        public int IdsegUsuarioSistema { get; set; }
        public string NombreCompleto { get; set; }
        public string NroCi { get; set; }
        public string Espedido { get; set; }
        public int IdsegPerfil { get; set; }
        public string Perfil { get; set; }
        public string LoginUsuario { get; set; }
        public int IdgenInstitucionsucursal { get; set; }
        public int IdgenInstitucion { get; set; }
        public string Institucion { get; set; }
        public string Sucursal { get; set; }
        public string Estado { get; set; }
        public List<string> Roles { get; set; }
        public string JwToken { get; set; }

    }
}
