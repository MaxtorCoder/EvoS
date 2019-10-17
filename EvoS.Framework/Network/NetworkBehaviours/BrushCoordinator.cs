using System;
using System.Linq;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
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
            Start();
        }

        private void Start()
        {
            foreach (var region in m_regions)
            {
                region.Initialize(this);
                if (EvoSGameConfig.NetworkIsServer)
                    m_regionsLastDisruptionTurn.Add(-1);
            }

            TrySetupBrushSquares();
        }

        private void TrySetupBrushSquares()
        {
            if (m_setupBrush || Board == null || (GameFlowData == null || !m_cameraCreated))
                return;
            SetupBrushSquares();
//            FogOfWar.CalculateFogOfWarForTeam(Team.TeamA);
//            FogOfWar.CalculateFogOfWarForTeam(Team.TeamB);
            m_setupBrush = true;
        }

        private void SetupBrushSquares()
        {
            for (var index = 0; index < m_regions.Length; ++index)
            {
                if (m_regions[index] == null)
                {
                    Log.Print(LogType.Error,$"Null brush region ({index}); fix brush coordinator's data.");
                }
                else
                {
                    foreach (var boardSquare in m_regions[index].method_0())
                    {
                        if (boardSquare.BrushRegion == -1)
                            boardSquare.BrushRegion = index;
                        else
                            Log.Print(LogType.Error,
                                $"Two brush regions ({boardSquare.BrushRegion} and {index}) are claiming the same boardSquare ({boardSquare.name})");
                    }
                }
            }
        }
        public bool DisableAllBrush()
        {
//            if (SinglePlayerManager.Get() != null && !SinglePlayerManager.Get().EnableBrush())
//                return true;
            if (DebugParameters.Get() != null)
                return DebugParameters.Get().GetParameterAsBool("DisableBrush");
            return false;
        }

        public bool IsRegionFunctioning(int regionIndex)
        {
            if (DisableAllBrush())
                return false;
            bool flag;
            if (regionIndex >= 0 && regionIndex < m_regions.Length)
            {
                int num = m_regionsLastDisruptionTurn[regionIndex];
                flag = num <= 0 || GameFlowData.CurrentTurn - num >= GameplayData.m_brushDisruptionTurns;
            }
            else
                flag = false;
            return flag;
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
