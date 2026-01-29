using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;
using System;

namespace PlatfromMania.UI
{
    public class PlayerUIService : MonoBehaviour
    {
        [Header("Data Providers")]
        [SerializeField] private GameObject healthProviderObject;
        [SerializeField] private GameObject coinsProviderObject;
        [SerializeField] private GameObject messageProviderObject;

        [Header("UI Views")]
        [SerializeField] private GameObject healthViewObject;
        [SerializeField] private GameObject coinsViewObject;
        [SerializeField] private GameObject messageViewObject;

        [Header("Settings")]
        [SerializeField] private float messageDuration = 2f;

        private IHealth healthProvider;
        private ICoinProvider coinsProvider;
        private IMessageProvider messageProvider;

        private IHealthView healthView;
        private ICoinsView coinsView;
        private IMessageView messageView;

        private void Awake()
        {
            InitializeProviders();
            InitializeViews();
        }

        private void InitializeProviders()
        {
            if (healthProviderObject != null)
                healthProvider = healthProviderObject.GetComponent<IHealth>();

            if (coinsProviderObject != null)
                coinsProvider = coinsProviderObject.GetComponent<ICoinProvider>();

            if (messageProviderObject != null)
                messageProvider = messageProviderObject.GetComponent<IMessageProvider>();
        }

        private void InitializeViews()
        {
            if (healthViewObject != null)
                healthView = healthViewObject.GetComponent<IHealthView>();

            if (coinsViewObject != null)
                coinsView = coinsViewObject.GetComponent<ICoinsView>();

            if (messageViewObject != null)
                messageView = messageViewObject.GetComponent<IMessageView>();
        }

        private void OnEnable()
        {
            SubscribeToProviders();
        }

        private void OnDisable()
        {
            UnsubscribeFromProviders();
        }

        private void SubscribeToProviders()
        {
            if (healthProvider != null)
            {
                healthProvider.OnHealthChanged += OnHealthChanged;
                // Инициализируем начальное значение
                healthView?.UpdateHealth(healthProvider.CurrentHealth);
            }

            if (coinsProvider != null)
            {
                coinsProvider.OnCoinsChanged += OnCoinsChanged;
                // Инициализируем начальное значение
                coinsView?.UpdateCoins(coinsProvider.Coins);
            }

            if (messageProvider != null)
            {
                messageProvider.OnMessageProvider += OnMessageRequested;
            }
        }

        private void UnsubscribeFromProviders()
        {
            if (healthProvider != null)
                healthProvider.OnHealthChanged -= OnHealthChanged;

            if (coinsProvider != null)
                coinsProvider.OnCoinsChanged -= OnCoinsChanged;

            if (messageProvider != null)
                messageProvider.OnMessageProvider -= OnMessageRequested;
        }

        private void OnHealthChanged(float current)
        {
            healthView?.UpdateHealth(current);
        }

        private void OnCoinsChanged(int amount)
        {
            coinsView?.UpdateCoins(amount);
        }

        private void OnMessageRequested(string message)
        {
            messageView?.ShowMessage(message, messageDuration);
        }
    }
}
