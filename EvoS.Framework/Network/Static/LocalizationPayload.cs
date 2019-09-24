using System;
using System.Collections.Generic;
using System.IO;
using EvoS.Framework.Network;

//using NetSerializer;

// Token: 0x020009EE RID: 2542
[Serializable]
[EvosMessage(25)]
public class LocalizationPayload
{
    public byte[][] ArgumentsAsBinaryData;
    public string Context;
    public string Term = "unset";


    /*
    [NonSerialized]
    private static Serializer s_serializer;
    
    public static Serializer Serializer
	{
		get
		{
			if (LocalizationPayload.s_serializer == null)
			{
				LocalizationPayload.s_serializer = new Serializer();
				Type[] rootTypes = new Type[]
				{
					typeof(LocalizationArg_AccessLevel),
					typeof(LocalizationArg_BroadcastMessage),
					typeof(LocalizationArg_ChatChannel),
					typeof(LocalizationArg_Faction),
					typeof(LocalizationArg_FirstRank),
					typeof(LocalizationArg_FirstType),
					typeof(LocalizationArg_Freelancer),
					typeof(LocalizationArg_GameType),
					typeof(LocalizationArg_Handle),
					typeof(LocalizationArg_Int32),
					typeof(LocalizationArg_LocalizationPayload),
					typeof(LocalizationArg_TimeSpan)
				};
				LocalizationPayload.s_serializer.AddTypes(rootTypes);
			}
			return LocalizationPayload.s_serializer;
		}
	}

	// Token: 0x060047F8 RID: 18424 RVA: 0x00171BE8 File Offset: 0x0016FDE8
	private List<LocalizationArg> ExtractArguments()
	{
		List<LocalizationArg> list = null;
		try
		{
			if (!this.ArgumentsAsBinaryData.IsNullOrEmpty<byte[]>())
			{
				list = new List<LocalizationArg>();
				foreach (byte buffer in this.ArgumentsAsBinaryData)
				{
					MemoryStream stream = new MemoryStream(buffer);
					object obj;
					LocalizationPayload.Serializer.Deserialize(stream, out obj);
					if (obj != null && obj is LocalizationArg)
					{
						list.Add(obj as LocalizationArg);
					}
				}
			}
		}
		catch (Exception exception)
		{
			Log.Exception(exception);
		}
		return list;
	}

	// Token: 0x060047F9 RID: 18425 RVA: 0x00171C74 File Offset: 0x0016FE74
	public override string ToString()
	{
		string text = StringUtil.smethod_0(this.Term, this.Context);
		List<LocalizationArg> list = this.ExtractArguments();
		if (list.IsNullOrEmpty<LocalizationArg>())
		{
			return text;
		}
		if (list.Count == 1)
		{
			return string.Format(text, list[0].vmethod_0());
		}
		if (list.Count == 2)
		{
			return string.Format(text, list[0].vmethod_0(), list[1].vmethod_0());
		}
		if (list.Count == 3)
		{
			return string.Format(text, list[0].vmethod_0(), list[1].vmethod_0(), list[2].vmethod_0());
		}
		if (list.Count > 4)
		{
			Log.Error("We do not support more than four arguments to localized payloads: {0}@{1}", new object[]
			{
				this.Term,
				this.Context
			});
		}
		return string.Format(text, new object[]
		{
			list[0].vmethod_0(),
			list[1].vmethod_0(),
			list[2].vmethod_0(),
			list[3].vmethod_0()
		});
	}

	// Token: 0x060047FA RID: 18426 RVA: 0x0003AD3B File Offset: 0x00038F3B
	public string ToDebugString()
	{
		return this.ToString();
	}

	// Token: 0x060047FB RID: 18427 RVA: 0x00171D90 File Offset: 0x0016FF90
	public static LocalizationPayload Create(string attedLocIdentifier)
	{
		string[] array = attedLocIdentifier.Split("@".ToCharArray(), 2);
		if (array.Length == 2)
		{
			return new LocalizationPayload
			{
				Term = array[0],
				Context = array[1]
			};
		}
		throw new Exception(string.Format("Bad argument ({0}) to LocalizationPayload, expected string with an @ in it.", attedLocIdentifier));
	}

	// Token: 0x060047FC RID: 18428 RVA: 0x00171DE4 File Offset: 0x0016FFE4
	public static LocalizationPayload Create(string term, string context)
	{
		return new LocalizationPayload
		{
			Term = term,
			Context = context
		};
	}

	// Token: 0x060047FD RID: 18429 RVA: 0x00171E08 File Offset: 0x00170008
	public static LocalizationPayload Create(string term, string context, params LocalizationArg[] arguments)
	{
		List<byte[]> list = null;
		if (!arguments.IsNullOrEmpty<LocalizationArg>())
		{
			list = new List<byte[]>();
			foreach (LocalizationArg ob in arguments)
			{
				MemoryStream memoryStream = new MemoryStream();
				LocalizationPayload.Serializer.Serialize(memoryStream, ob);
				list.Add(memoryStream.ToArray());
			}
		}
		return new LocalizationPayload
		{
			Term = term,
			Context = context,
			ArgumentsAsBinaryData = list.ToArray()
		};
	}
    */
}
