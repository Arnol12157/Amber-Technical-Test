using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public bool CanMove = true;
        private float _currentSpeed;
        private GameObject _base;

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }

        public void Setup(GameObject baseRef)
        {
            _base = baseRef;
        }
    
        private void Update()
        {
            if (!CanMove)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _base.transform.position,
                CurrentSpeed * Time.deltaTime);
            transform.LookAt(_base.transform);
        }
    }
}
