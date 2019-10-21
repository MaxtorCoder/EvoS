using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("SinglePlayerScriptedChat")]
    public class SinglePlayerScriptedChat : ISerializedItem
    {
        public string m_text;
        public CharacterType m_sender;
        public float m_displaySeconds;
        public string m_audioEvent;

        public SinglePlayerScriptedChat()
        {
        }

        public SinglePlayerScriptedChat(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_text = stream.ReadString32();
            m_sender = (CharacterType) stream.ReadInt32();
            m_displaySeconds = stream.ReadSingle();
            m_audioEvent = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(SinglePlayerScriptedChat)}>(" +
                   $"{nameof(m_text)}: {m_text}, " +
                   $"{nameof(m_sender)}: {m_sender}, " +
                   $"{nameof(m_displaySeconds)}: {m_displaySeconds}, " +
                   $"{nameof(m_audioEvent)}: {m_audioEvent}, " +
                   ")";
        }
    }
}
