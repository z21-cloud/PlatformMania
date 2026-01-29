using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour, IPickableCollector, ICoinProvider, IDecreaseCoins
    {
        public int Coins { get; private set; }

        public event Action<int> OnCoinsChanged;

        private const int COINS_THRESHOLD = 0;

        public void ResetCoins()
        {
            Coins = COINS_THRESHOLD;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void DecreaseCoins(int value)
        {
            Coins -= value;
            Coins = Mathf.Max(COINS_THRESHOLD, Coins - value);
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

