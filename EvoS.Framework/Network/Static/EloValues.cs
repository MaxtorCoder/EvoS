using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [JsonConverter(typeof(JsonConverter))]
    [Serializable]
    [EvosMessage(516)]
    public class EloValues : ICloneable
    {
        public EloValues()
        {
            Values = new Dictionary<string, EloDatum>();
        }

        [EvosMessage(517)]
        public Dictionary<string, EloDatum> Values { get; private set; }

        public void UpdateElo(string key, float value, int countDelta)
        {
            EloDatum eloDatum;
            if (Values.TryGetValue(key, out eloDatum))
            {
                eloDatum.Elo = Math.Max(1f, value);
                eloDatum.Count = Math.Max(0, eloDatum.Count + countDelta);
            }
            else
            {
                countDelta = Math.Max(0, countDelta);
                Values.Add(key, new EloDatum
                {
                    Elo = value,
                    Count = countDelta
                });
            }
        }

        public void ApplyDelta(string key, float eloDelta, int countDelta)
        {
            EloDatum eloDatum;
            if (Values.TryGetValue(key, out eloDatum))
            {
                eloDatum.Elo = Math.Max(1f, eloDatum.Elo + eloDelta);
                eloDatum.Count = Math.Max(0, eloDatum.Count + countDelta);
            }
            else
            {
                countDelta = Math.Max(0, countDelta);
                Values.Add(key, new EloDatum
                {
                    Elo = 1500f + eloDelta,
                    Count = countDelta
                });
            }
        }

        public void GetElo(string key, out float elo, out int count)
        {
            EloDatum eloDatum;
            if (Values.TryGetValue(key, out eloDatum))
            {
                elo = eloDatum.Elo;
                count = eloDatum.Count;
            }
            else
            {
                elo = 1500f;
                count = 0;
            }
        }

//        [JsonIgnore]
//        public float PlayerFacingElo
//        {
//            get
//            {
//                float result;
//                int num;
//                GetElo(ELOPlayerKey.PublicFacingKey.KeyText, out result, out num);
//                return result;
//            }
//            set { UpdateElo(ELOPlayerKey.PublicFacingKey.KeyText, value, 0); }
//        }

//        [JsonIgnore]
//        public float InternalElo
//        {
//            get
//            {
//                float result;
//                int num;
//                GetElo(ELOPlayerKey.MatchmakingEloKey.KeyText, out result, out num);
//                return result;
//            }
//            set { UpdateElo(ELOPlayerKey.MatchmakingEloKey.KeyText, value, 0); }
//        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        private class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(EloValues);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                EloValues eloValues = (EloValues) value;
                serializer.Serialize(writer, eloValues.Values);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                EloValues eloValues = (existingValue == null) ? new EloValues() : ((EloValues) existingValue);
                serializer.Populate(reader, eloValues.Values);
                return eloValues;
            }
        }
    }

    [Serializable]
    [EvosMessage(520)]
    public class EloDatum : ICloneable
    {
        public EloDatum()
        {
            Elo = 1500f;
            Count = 0;
        }

        public float Elo { get; set; }
        public int Count { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
