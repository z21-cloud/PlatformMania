using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Core;

namespace PlatfromMania.Helpers
{
    public class FlipSprite : MonoBehaviour
    {
        [Header("Sprite flip setup")]
        [SerializeField, Tooltip("Component Implementing IDirectionProvider")]
        private MonoBehaviour directionProvider;
        private IDirectionProvider provider;
        private bool isFacingRight = true;
        private void Awake()
        {
            provider = directionProvider as IDirectionProvider;

            if(provider == null)
            {
                Debug.LogError($"{name} : direction provider does not implement IDirectionProvdier", this);
                enabled = false;
            }
        }

        private void Update()
        {
            UpdateSpriteFlip(provider.Direction);
        }

        private void UpdateSpriteFlip(float value)
        {
            if (value == 0) return;

            if (isFacingRight && value < 0 || !isFacingRight && value > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
}

