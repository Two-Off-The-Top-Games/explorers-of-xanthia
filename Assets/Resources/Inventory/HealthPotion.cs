using Entities.Events;
using System.Collections.Generic;

public class HealthPotion : Item
{
    public int HealAmount;
    protected override List<ItemAction> Actions => new List<ItemAction>() { new ItemAction("Use", HealthPotionUseEffect) };

    private void HealthPotionUseEffect()
    {
        // TODO: Get instance id somehow.
        new CharacterGainHealthEvent(OwnerId, HealAmount);
    }
}
