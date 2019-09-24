using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [JsonConverter(typeof(JsonConverter))]
    [Serializable]
    [EvosMessage(157)]
    public struct Rate
    {
        public Rate(double amount, TimeSpan period)
        {
            Amount = amount;
            Period = period;
        }

        public static implicit operator Rate(string rate)
        {
            string[] array = rate.Split(new[]
            {
                " per "
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length != 2)
            {
                throw new Exception("Failed to parse rate");
            }

            return new Rate(double.Parse(array[0]), TimeSpan.Parse(array[1]));
        }

        public override string ToString()
        {
            return string.Format("{0} per {1}", Amount, Period);
        }

        public double AmountPerSecond
        {
            get
            {
                if (Period == TimeSpan.Zero)
                {
                    return 0.0;
                }

                return Amount / Period.TotalSeconds;
            }
        }

        public double Amount;

        public TimeSpan Period;

        private class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Rate);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((Rate) value).ToString());
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                return reader.Value.ToString();
            }
        }
    }
}
