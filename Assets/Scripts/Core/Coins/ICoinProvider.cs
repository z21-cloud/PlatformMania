using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Core
{
    public interface ICoinProvider
    {
        public int Coins { get; }

        public event Action<int> OnCoinsChanged;
    }
}

