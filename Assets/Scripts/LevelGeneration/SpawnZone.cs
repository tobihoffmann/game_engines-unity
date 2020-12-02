using UnityEngine;

namespace LevelGeneration
{
    public class SpawnZone : MonoBehaviour
    {
        [SerializeField] private int spawnRadius = 5;

        public int SpawnRadius => spawnRadius;

        /// <summary>
        /// Visualize spawn radius
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
