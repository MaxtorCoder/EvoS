using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(218)]
    public enum ConsumeInventoryItemResult
    {
        None,
        Success,
        ItemNotFound,
        InventroyIsFull,
        ItemTemplateNotFound,
        ItemTemplateDisabled,
        ItemTemplateNotConsumable,
        ItemTemplateNotValid,
        CannotConsumeItem
    }
}
