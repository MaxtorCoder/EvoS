using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(560)]
    public enum PersistedAccountDataSnapshotReason
    {
        Unknown,
        LastLogoutSession,
        BackupOnRestore,
        Admin
    }
}
