using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Services
{
    public class WallCheckService
    {
        public bool Check(Vector2 position, Vector2 size, float angle, LayerMask mask)
        {
            return Physics2D.OverlapBox(position, size, angle, mask);
        }

        public static void DrawDebugCircle(Vector2 position, Vector2 size, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(position, size);
        }
    }
}

