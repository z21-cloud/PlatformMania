using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface ICheckpoint
    {
        public Transform CheckpointTransform { get; }
    }
}

