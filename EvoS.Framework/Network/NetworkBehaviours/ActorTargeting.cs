using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorTargeting")]
    public class ActorTargeting : NetworkBehaviour
    {
        public ActorTargeting()
        {
        }

        public ActorTargeting(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            return false;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorTargeting)}>(" +
                   ")";
        }

        public class AbilityRequestData : IComparable
        {
            public AbilityData.ActionType _actionType;
            public List<AbilityTarget> _targets;

            public AbilityRequestData(AbilityData.ActionType actionType, List<AbilityTarget> targets)
            {
                _actionType = actionType;
                _targets = targets;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                if (!(obj is AbilityRequestData abilityRequestData))
                    throw new ArgumentException("Object is not an AbilityRequestData");

                return _actionType.CompareTo(abilityRequestData._actionType);
            }
        }
    }
}
