using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalContainer : Container
{
    [SerializeField] private Container _container;

    private void Update()
    {
        TryTake(_container);
    }
}
