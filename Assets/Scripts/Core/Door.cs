using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;
using System;

namespace PlatfromMania.Core
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private int payment = 50;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDecreaseCoins>(out var coinProvider))
            {
                coinProvider.DecreaseCoins(payment);
            }
        }
    }
}

