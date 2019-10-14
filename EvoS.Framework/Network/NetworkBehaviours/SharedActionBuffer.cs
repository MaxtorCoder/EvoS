using System;
using System.Runtime.InteropServices;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("SharedActionBuffer")]
    public class SharedActionBuffer : NetworkBehaviour
    {
//        [SyncVar(hook = "HookSetActionPhase")]
        private ActionBufferPhase m_actionPhase;
//        [SyncVar(hook = "HookSetAbilityPhase")]
        private AbilityPriority m_abilityPhase;
        
        public SharedActionBuffer()
        {
            
        }
        
        public SharedActionBuffer(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        private void HookSetActionPhase(ActionBufferPhase value)
        {
            Networkm_actionPhase = value;
//            SynchronizeClientData();
        }

        private void HookSetAbilityPhase(AbilityPriority value)
        {
            Networkm_abilityPhase = value;
//            SynchronizeClientData();
        }

        public ActionBufferPhase Networkm_actionPhase
        {
            get => m_actionPhase;
            [param: In] set
            {
                int num = (int) value;
                ref ActionBufferPhase local = ref m_actionPhase;
                if (EvoSGameConfig.NetworkIsClient && !syncVarHookGuard)
                {
                    syncVarHookGuard = true;
                    HookSetActionPhase(value);
                    syncVarHookGuard = false;
                }
                SetSyncVar((ActionBufferPhase) num, ref local, 1U);
            }
        }

        public AbilityPriority Networkm_abilityPhase
        {
            get => m_abilityPhase;
            [param: In] set
            {
                int num = (int) value;
                ref AbilityPriority local = ref m_abilityPhase;
                if (EvoSGameConfig.NetworkIsClient && !syncVarHookGuard)
                {
                    syncVarHookGuard = true;
                    HookSetAbilityPhase(value);
                    syncVarHookGuard = false;
                }
                SetSyncVar((AbilityPriority) num, ref local, 2U);
            }
        }
        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                writer.Write((int) m_actionPhase);
                writer.Write((int) m_abilityPhase);
                return true;
            }
            bool flag = false;
            if (((int) syncVarDirtyBits & 1) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }
                writer.Write((int) m_actionPhase);
            }
            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }
                writer.Write((int) m_abilityPhase);
            }
            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                m_actionPhase = (ActionBufferPhase) reader.ReadInt32();
                m_abilityPhase = (AbilityPriority) reader.ReadInt32();
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    HookSetActionPhase((ActionBufferPhase) reader.ReadInt32());
                if ((num & 2) == 0)
                    return;
                HookSetAbilityPhase((AbilityPriority) reader.ReadInt32());
            }
        }
        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(SharedActionBuffer)}>(" +

                   ")";
        }
    }
}
