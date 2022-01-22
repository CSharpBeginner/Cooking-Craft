using UnityEngine;

public class ReceivingContainer : Container
{
    private void OnTriggerStay(Collider other)
    {
        TryTake(other);
    }
}
