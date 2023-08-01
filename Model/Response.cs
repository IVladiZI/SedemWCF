using Newtonsoft.Json;

namespace Model
{
    public class Response<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
