using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace PlatfromMania.Services
{
    public class GroundCheckService
    {
        public bool Check(Vector2 position, float radius, LayerMask mask)
        {
            return Physics2D.OverlapCircle(position, radius, mask);
        }

        public static void DrawDebugCircle(Vector2 position, float radius, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(position, radius);
        }
    }
}

