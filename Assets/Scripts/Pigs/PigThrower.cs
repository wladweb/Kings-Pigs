using UnityEngine;

public class PigThrower : Thrower
{
    protected override void FindItemPool()
    {
        Items = FindObjectOfType<ThrowedBoxes>();
    }
}
