using UnityEngine;

namespace Chatcloud.CodeBase.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Backend", menuName = "ScriptableObjects/Backend data", order = 1)]
    public class BackendData : ScriptableObject
    {
        public string endpoint;
        public string tenate;
    }
}