using System;
using UnityEngine;

namespace Network.Extra
{
    [Serializable]
    public struct Header
    {
        [SerializeField] private string key;
        [SerializeField] private string value;

        public string Key => key;

        public string Value => value;
    }
}