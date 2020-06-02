using UnityEngine;

public class HeartItem : CollectedItem
{
    protected override void ChargeItem(King king, int count)
    {
        king.ApplyHeal(count);
    }
}
