using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour, IPickableCollector, ICoinProvider
    {
        public int Coins { get; private set; }

        public event Action<int> OnCoinsChanged;

        public void ResetCoins()
        {
            Coins = 0;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void CollectCoint(int amount)
        {
            Coins += amount;
            OnCoinsChanged?.Invoke(Coins);
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

