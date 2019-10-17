using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [SerializedMonoBehaviour("NPCBrain_StateMachine")]
    public class NPCBrain_StateMachine : NPCBrain
    {
        public NPCBrain_StateMachine()
        {
        }


        public NPCBrain_StateMachine(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override NPCBrain Create(BotController bot, Transform destination)
        {
            return bot.gameObject.AddComponent<NPCBrain_StateMachine>();
        }

        public override string ToString()
        {
            return $"{nameof(NPCBrain_StateMachine)}(" +
                   ")";
        }
    }
}
