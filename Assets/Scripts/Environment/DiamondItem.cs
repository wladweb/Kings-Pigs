using UnityEngine;

public class DiamondItem : CollectedItem
{
    protected override void ChargeItem(King king, int count)
    {
        king.CollectDiamond(count);
    }
}
