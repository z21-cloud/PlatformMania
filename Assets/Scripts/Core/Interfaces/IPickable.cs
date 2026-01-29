using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface IPickable
    {
        public void PickUp(IPickableCollector collector) { }
    }
}

