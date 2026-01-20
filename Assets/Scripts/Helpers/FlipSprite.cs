using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;

namespace PlatfromMania.Helpers
{
    public class FlipSprite : MonoBehaviour
    {
        [Header("Player Movement Checker")]
        [SerializeField] private PlayerMovement movement;
        private bool isFacingRight = true;

        private void OnEnable()
        {
            movement.OnSpriteFliped += UpdateSpriteFlip;
        }

        private void UpdateSpriteFlip(float value)
        {
            if (isFacingRight && value < 0 || !isFacingRight && value > 0)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
        }

        private void OnDisable()
        {
            movement.OnSpriteFliped -= UpdateSpriteFlip;
        }
    }
}

