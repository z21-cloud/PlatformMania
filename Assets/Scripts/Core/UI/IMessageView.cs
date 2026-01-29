using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatfromMania.UI
{
    public interface IMessageView
    {
        public void ShowMessage(string message, float dureation);
    }
}

