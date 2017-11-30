using System;

namespace Core.JsonConverter.Contracts
{
    public interface IJsonConverter<T>
    {
        T DeserializeJson(string json);

        string SerializeObject(T obj);
    }
}
