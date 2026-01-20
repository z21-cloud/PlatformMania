using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Managers;

namespace PlatfromMania.Core
{
    public class PlayerController : MonoBehaviour
    {
        private bool isFacingRight = true;

        // Update is called once per frame
        void Update()
        {
            UpdateSpriteFlip();
        }

        private void UpdateSpriteFlip()
        {
            float movement = InputManager.Instance.GetHorizontalMovement();
            if (movement == 0) return;
            if(isFacingRight && movement < 0 || !isFacingRight && movement > 0)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }
    }
}

