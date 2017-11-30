using Core.JsonConverter.Contracts;
using Newtonsoft.Json;

namespace Core.JsonConverter
{
    public class JsonConverter<T> : IJsonConverter<T>
    {
        public T DeserializeJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string SerializeObject(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
