using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.UI
{
    public interface IMessageProvider
    {
        public event Action<string> OnMessageProvider;
    }
}

