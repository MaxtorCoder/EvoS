using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("SharedEffectBarrierManager")]
    public class SharedEffectBarrierManager : NetworkBehaviour
    {
        public int m_numTurnsInMemory = 3;
        private List<int> m_endedEffectGuidsSync;
        private List<int> m_endedBarrierGuidsSync;

        public SharedEffectBarrierManager()
        {
        }

        public SharedEffectBarrierManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            m_endedEffectGuidsSync = new List<int>();
            m_endedBarrierGuidsSync = new List<int>();
        }

        private void SetDirtyBit(DirtyBit bit)
        {
            SetDirtyBit((uint) bit);
        }

        private bool IsBitDirty(uint setBits, DirtyBit bitToTest)
        {
            return ((DirtyBit) setBits & bitToTest) != ~DirtyBit.All;
        }

        private void OnEndedEffectGuidsSync()
        {
//            if (m_endedEffectGuidsSync.Count > 100)
//                Log.Error("Remembering more than 100 effects?");
//            if (!(ClientEffectBarrierManager.Get() != null))
//                return;
//            for (int index = 0; index < m_endedEffectGuidsSync.Count; ++index)
//                ClientEffectBarrierManager.Get().EndEffect(m_endedEffectGuidsSync[index]);
        }

        private void OnEndedBarrierGuidsSync()
        {
//            if (m_endedBarrierGuidsSync.Count > 50)
//                Log.Error("Remembering more than 50 barriers?");
//            if (!(ClientEffectBarrierManager.Get() != null))
//                return;
//            for (int index = 0; index < m_endedBarrierGuidsSync.Count; ++index)
//                ClientEffectBarrierManager.Get().EndBarrier(m_endedBarrierGuidsSync[index]);
        }

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            if (!initialState)
                writer.WritePackedUInt32(syncVarDirtyBits);
            uint setBits = !initialState ? syncVarDirtyBits : uint.MaxValue;
            if (IsBitDirty(setBits, DirtyBit.EndedEffects))
            {
                short count = (short) m_endedEffectGuidsSync.Count;
                writer.Write(count);
                foreach (int num in m_endedEffectGuidsSync)
                    writer.Write(num);
            }

            if (IsBitDirty(setBits, DirtyBit.EndedBarriers))
            {
                short count = (short) m_endedBarrierGuidsSync.Count;
                writer.Write(count);
                foreach (int num in m_endedBarrierGuidsSync)
                    writer.Write(num);
            }

            return setBits != 0U;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            uint setBits = uint.MaxValue;
            if (!initialState)
                setBits = reader.ReadPackedUInt32();
            if (IsBitDirty(setBits, DirtyBit.EndedEffects))
            {
                m_endedEffectGuidsSync.Clear();
                short num = reader.ReadInt16();
                for (short index = 0; (int) index < (int) num; ++index)
                    m_endedEffectGuidsSync.Add(reader.ReadInt32());
            }

            if (IsBitDirty(setBits, DirtyBit.EndedBarriers))
            {
                m_endedBarrierGuidsSync.Clear();
                short num = reader.ReadInt16();
                for (short index = 0; (int) index < (int) num; ++index)
                    m_endedBarrierGuidsSync.Add(reader.ReadInt32());
            }

            OnEndedEffectGuidsSync();
            OnEndedBarrierGuidsSync();
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_numTurnsInMemory = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(SharedEffectBarrierManager)}>(" +
                   $"{nameof(m_numTurnsInMemory)}: {m_numTurnsInMemory}, " +
                   ")";
        }

        private enum DirtyBit : uint
        {
            EndedEffects = 1,
            EndedBarriers = 2,
            All = 4294967295 // 0xFFFFFFFF
        }
    }
}
