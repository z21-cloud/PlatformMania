using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IMessageProvider
{
    public event Action<string> OnMessageProvider; 
}
