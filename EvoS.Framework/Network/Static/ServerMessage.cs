using System;
using System.Collections.Generic;
using System.Linq;
using EvoS.Framework.Network;
using Newtonsoft.Json;

// Token: 0x020009D7 RID: 2519
[Serializable]
[EvosMessage(770, typeof(ServerMessage))]
public class ServerMessage
{
    public static implicit operator ServerMessage(string value)
    {
        return new ServerMessage
        {
            EN = value,
            FR = value,
            DE = value,
            RU = value,
            ES = value,
            IT = value,
            PL = value,
            PT = value,
            KO = value,
            ZH = value
        };
    }

    public string EN { get; set; }
    public string FR { get; set; }
    public string DE { get; set; }
    public string RU { get; set; }
    public string ES { get; set; }
    public string IT { get; set; }
    public string PL { get; set; }
    public string PT { get; set; }
    public string KO { get; set; }
    public string ZH { get; set; }

    /*
	[JsonIgnore]
	public IEnumerable<string> Languages
	{
		get
		{
            
			foreach (ServerMessageLanguage language in Enum.GetValues(typeof(ServerMessageLanguage)).Cast<ServerMessageLanguage>())
			{
				if (!this.GetValue(language).IsNullOrEmpty())
				{
					yield return language.ToString();
				}
			}
			yield break;
		}
	}
    */
}
