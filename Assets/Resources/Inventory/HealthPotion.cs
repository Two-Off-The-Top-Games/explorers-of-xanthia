using Entities.Events;
using System;

public class HealthPotion : Item
{
    public int HealAmount;
    protected override Action UseAction => HealthPotionUseEffect;

    private void HealthPotionUseEffect()
    {
        new CharacterGainHealthEvent(OwnerId, HealAmount);
        Destroy(gameObject);
    }
}
