using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IPaymentConfirmationView
{
    public void ShowConfirmation(int amount, Action onConfirm, Action onCancel);
    public void Hide();
}
