using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class Coin : MonoBehaviour, IPickable
    {
        [SerializeField] private int value = 10;

        public void PickUp(IPickableCollector collector)
        {
            collector.CollectCoint(value);
            gameObject.SetActive(false);
        }
    }
}


