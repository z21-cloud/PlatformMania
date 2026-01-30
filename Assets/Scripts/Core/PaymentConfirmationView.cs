using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PaymentConfirmationView : MonoBehaviour, IPaymentConfirmationView
{
    [Header("UI")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action currentOnConfirm;
    private Action currentOnCancel;

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);

        Hide();
    }

    private void OnDestroy()
    {
        confirmButton.onClick.RemoveListener(OnConfirmClicked);
        cancelButton.onClick.RemoveListener(OnCancelClicked);
    }

    public void ShowConfirmation(int amount, Action onConfirm, Action onCancel)
    {
        currentOnConfirm = onConfirm;
        currentOnCancel= onCancel;

        popupPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Hide()
    {
        popupPanel.SetActive(false);

        Time.timeScale = 1f;

        currentOnConfirm = null;
        currentOnCancel = null;
    }

    private void OnCancelClicked()
    {
        currentOnCancel?.Invoke();
        Hide();
    }

    private void OnConfirmClicked()
    {
        currentOnConfirm?.Invoke();
        Hide();
    }
}
