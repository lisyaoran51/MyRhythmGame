
using Newtonsoft.Json;

namespace Base.IO.Serialization {
    public interface IJsonSerializable {
    }

    public static class JsonSerializableExtensions {
        public static string Serialize(this IJsonSerializable obj) {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public static T Deserialize<T>(this string objString) {
            return JsonConvert.DeserializeObject<T>(objString);
        }

        public static T DeepClone<T>(this T obj)
            where T : IJsonSerializable {
            return Deserialize<T>(Serialize(obj));
        }
    }
}
