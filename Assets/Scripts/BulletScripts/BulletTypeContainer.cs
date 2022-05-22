using UnityEngine;

namespace BulletScripts
{
    [CreateAssetMenu(fileName = "Canon Block Blast", menuName = "Bulelt Container")]
    public class BulletTypeContainer : ScriptableObject
    {
        public BulletType[] Container;
    }
}