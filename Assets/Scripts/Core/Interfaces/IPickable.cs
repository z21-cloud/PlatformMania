using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPickable
{
    public void PickUp(IPickableCollector collector) { }
}
