using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPaymentProvider
{
    public int Payment { get; }
}