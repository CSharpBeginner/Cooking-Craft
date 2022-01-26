using UnityEngine;

public class GivingContainer : Container
{
    private void OnTriggerStay(Collider other)
    {
        TryGive(other);
    }
}
