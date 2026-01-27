using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Services
{
    public class ResetService
    {
        private readonly List<IResettable> resettables = new List<IResettable>();

        public void Register(IResettable resettable)
        {
            if (!resettables.Contains(resettable)) resettables.Add(resettable);
        }

        public void Unregister(IResettable resettable)
        {
            resettables.Remove(resettable);
        }

        public void ResetAll()
        {
            foreach (var r in resettables)
                r.ResetState();
        }
    }
}

