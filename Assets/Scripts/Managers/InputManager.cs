using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatfromMania.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        private int leftMouseButton = 0;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public float GetHorizontalMovement() => Input.GetAxisRaw("Horizontal");
        public float GetVerticalMovement() => Input.GetAxisRaw("Vertical");
        public bool GetJump() => Input.GetKeyDown(KeyCode.Space);
        public bool GetMouseButton() => Input.GetMouseButtonDown(leftMouseButton);
    }
}
