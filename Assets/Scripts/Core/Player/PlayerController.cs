using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour, IPickableCollector
    {
        public int Coins { get; private set; }

        public void ResetCoins()
        {
            Coins = 0;
        }

        public void CollectCoint(int amount)
        {
            Coins += amount;
            Debug.Log($"PlayerCollector: count collected; Current coins: {Coins}");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<IPickable>(out var pickable) 
                && TryGetComponent<IPickableCollector>(out var collector))
            {
                pickable.PickUp(collector);
            }
        }
    }
}

