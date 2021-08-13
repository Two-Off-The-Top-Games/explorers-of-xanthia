using Events.Entity;
using System;

public class HealthPotion : Item
{
    public int HealAmount;

    protected override void ItemEffect(int targetInstanceId)
    {
        new EntityGainHealthEvent(targetInstanceId, HealAmount);
        Destroy(gameObject);
    }
}
