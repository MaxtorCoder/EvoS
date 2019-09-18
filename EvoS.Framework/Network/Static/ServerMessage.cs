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
            String_0 = value,
            String_1 = value,
            String_2 = value,
            String_3 = value,
            String_4 = value,
            String_5 = value,
            String_6 = value,
            String_7 = value,
            String_8 = value,
            String_9 = value
        };
    }

    public string String_0 { get; set; }
    public string String_1 { get; set; }
    public string String_2 { get; set; }
    public string String_3 { get; set; }
    public string String_4 { get; set; }
    public string String_5 { get; set; }
    public string String_6 { get; set; }
    public string String_7 { get; set; }
    public string String_8 { get; set; }
    public string String_9 { get; set; }

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
