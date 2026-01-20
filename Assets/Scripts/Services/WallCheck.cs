using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Services
{
    public class WallCheck : MonoBehaviour
    {
        [Header("Wall check")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
        [SerializeField] private LayerMask wallLayer;

        public bool IsWallSliding => isWallSliding;
        private bool isWallSliding;

        void Update()
        {
            CheckWallTouching();
        }

        private void CheckWallTouching()
        {
            isWallSliding = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);
        }

        private void OnDrawGizmosSelected()
        {
            if (wallCheck != null)
            {
                // —ин€€ лини€ - проверка стены сбоку (вправо и влево)
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
            }
        }
    }
}

