using System;
using System.Linq;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("BrushCoordinator")]
    public class BrushCoordinator : NetworkBehaviour
    {
        private static int kRpcRpcUpdateClientFog = -2062592578;
        private static int kListm_regionsLastDisruptionTurn = 1707345339;
        private SyncListInt m_regionsLastDisruptionTurn = new SyncListInt();
        private bool m_setupBrush;
        private bool m_cameraCreated;
        private bool m_brushVisible;
        public BrushRegion[] m_regions;

        static BrushCoordinator()
        {
//            RegisterRpcDelegate(typeof (BrushCoordinator), kRpcRpcUpdateClientFog, BrushCoordinator.InvokeRpcRpcUpdateClientFog);
            RegisterSyncListDelegate(typeof(BrushCoordinator), kListm_regionsLastDisruptionTurn,
                InvokeSyncListm_regionsLastDisruptionTurn);
        }

        public BrushCoordinator()
        {
        }

        public override void Awake()
        {
            m_regionsLastDisruptionTurn.InitializeBehaviour(this, kListm_regionsLastDisruptionTurn);
        }

        public BrushCoordinator(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        protected static void InvokeSyncListm_regionsLastDisruptionTurn(
            NetworkBehaviour obj,
            NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_regionsLastDisruptionTurn called on server.");
            else
                ((BrushCoordinator) obj).m_regionsLastDisruptionTurn.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListInt.WriteInstance(writer, m_regionsLastDisruptionTurn);
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

                SyncListInt.WriteInstance(writer, m_regionsLastDisruptionTurn);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListInt.ReadReference(reader, m_regionsLastDisruptionTurn);
            }
            else
            {
                if (((int) reader.ReadPackedUInt32() & 1) == 0)
                    return;
                SyncListInt.ReadReference(reader, m_regionsLastDisruptionTurn);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_regions = new SerializedArray<BrushRegion>(assetFile, stream).ToArray();
        }

        public override string ToString()
        {
            return $"{nameof(BrushCoordinator)}>(" +
                   $"{nameof(m_regions)}: [{string.Join(", ", m_regions.ToList())}]" +
                   ")";
        }
    }
}
