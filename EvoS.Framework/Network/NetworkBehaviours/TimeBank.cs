using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("TimeBank")]
    public class TimeBank : NetworkBehaviour
    {
        private static int kCmdCmdConsumableUsed = -1923431383;

//        [SyncVar]
        private float m_reserveRemaining;

//        [SyncVar]
        private int m_consumablesRemaining;
        private float m_reserveUsed;
        private bool m_confirmed;

        private bool m_clientConsumableUsed;

//        [SyncVar]
        private bool m_resolved;
        private bool m_clientEndTurnRequested;

        public float ReserveRemaining => m_reserveRemaining;
        public int ConsumablesRemaining => m_consumablesRemaining;
        public float ReserveUsed => m_reserveUsed;
        public bool Confirmed => m_confirmed;
        public bool ClientConsumableUsed => m_clientConsumableUsed;
        public bool Resolved => m_resolved;
        public bool ClientEndTurnRequested => m_clientEndTurnRequested;

        static TimeBank()
        {
//            NetworkBehaviour.RegisterCommandDelegate(typeof (TimeBank), TimeBank.kCmdCmdConsumableUsed, new NetworkBehaviour.CmdDelegate(TimeBank.InvokeCmdCmdConsumableUsed));
        }

        public TimeBank()
        {
        }

        public TimeBank(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }


        public override void Awake()
        {
//            int tbConsumables = GameWideData.Get().m_tbConsumables;
//         
//            this.Networkm_reserveRemaining = GameWideData.Get().m_tbInitial;
//            this.Networkm_consumablesRemaining = tbConsumables;
//            this.ResetTurn();
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                writer.Write(m_reserveRemaining);
                writer.WritePackedUInt32((uint) m_consumablesRemaining);
                writer.Write(m_resolved);
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

                writer.Write(m_reserveRemaining);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_consumablesRemaining);
            }

            if (((int) syncVarDirtyBits & 4) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write(m_resolved);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                m_reserveRemaining = reader.ReadSingle();
                m_consumablesRemaining = (int) reader.ReadPackedUInt32();
                m_resolved = reader.ReadBoolean();
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    m_reserveRemaining = reader.ReadSingle();
                if ((num & 2) != 0)
                    m_consumablesRemaining = (int) reader.ReadPackedUInt32();
                if ((num & 4) == 0)
                    return;
                m_resolved = reader.ReadBoolean();
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(TimeBank)}>(" +
                   ")";
        }
    }
}
