using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(575)]
    public class QuestProgress
    {
        public int Id { get; set; }
        public Dictionary<int, int> ObjectiveProgress { get; set; }
        [EvosMessage(576)]
        public Dictionary<int, DateTime> ObjectiveProgressLastDate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
