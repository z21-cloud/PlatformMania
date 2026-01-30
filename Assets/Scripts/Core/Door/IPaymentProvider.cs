using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface IPaymentProvider
    {
        public int Payment { get; }
    }
}
