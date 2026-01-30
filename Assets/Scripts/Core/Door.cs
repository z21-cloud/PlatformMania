using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;
using System;

namespace PlatfromMania.Core
{
    public class Door : MonoBehaviour, IPaymentProvider
    {
        [SerializeField] private int payment = 50;

        [SerializeField] private PaymentConfirmationView confirmationView;

        public int Payment { get; private set; }
        private IDecreaseCoins currentPlayer;
        private void Start()
        {
            Payment = payment;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDecreaseCoins>(out var coinProvider) && collision.TryGetComponent<ICoinProvider>(out var coinsCheck))
            {
                if (coinsCheck.Coins >= payment)
                {
                    ShowPaymentConfirmation();
                    currentPlayer = coinProvider;
                }
            }
        }

        private void ShowPaymentConfirmation()
        {
            if(confirmationView != null)
            {
                confirmationView.ShowConfirmation(
                    payment,
                    onConfirm: OnPaymentConfirmed,
                    onCancel: OnPaymentCancelled
                    );
            }
        }

        private void OnPaymentConfirmed()
        {
            currentPlayer?.DecreaseCoins(payment);
        }

        private void OnPaymentCancelled() { }
    }
}

