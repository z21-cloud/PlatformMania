using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Core
{
    public interface ICheckpoint
    {
        Transform RespawnPoint { get; }
    }
}

