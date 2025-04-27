using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.Utils
{
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarUtil : MonoBehaviour
    {
        private Scrollbar _scrollbar;

        private void Awake()
        {
            _scrollbar = GetComponent<Scrollbar>();
        }

        private IEnumerator Start()
        {
            yield return null;
            _scrollbar.value = 0f;
        }
    }
}
