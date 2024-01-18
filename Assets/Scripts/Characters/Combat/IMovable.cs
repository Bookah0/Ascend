using UnityEngine;

namespace Assets.Scripts.Characters.Combat
{
    public interface IMovable
    {
        public GameObject GetPosition();
        public void SetPosition(GameObject gameObject);

    }
}
