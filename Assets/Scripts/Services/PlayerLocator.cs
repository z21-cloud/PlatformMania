using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.Services
{
    public static class PlayerLocator
    {
        private static GameObject player;
        public static GameObject Player
        {
            get
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    if (player == null)
                        Debug.LogError("Player Locator: Can't find the player!");
                }

                return player;
            }
        }

        public static void ClearCache() => player = null;
    }
}

