using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TowerBuildingController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] private Animator animator;

        [Header("Settings")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private int maxTowersPerPhase;


        private List<TowerController> towerList = new List<TowerController>();
        private int towersPlaced;
        private bool canPlaceTowers = true;

        void Update()
        {
            if (!canPlaceTowers || towersPlaced >= maxTowersPerPhase) return;

            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
                {
                    var block = hit.collider;
                    var spawnPos = new Vector3(block.bounds.center.x, block.bounds.center.y + hit.collider.bounds.extents.y, block.bounds.center.z);
                    spawnPos.y += towerPrefab.GetComponent<BoxCollider>().size.y / 2 * towerPrefab.transform.localScale.y;
                    TowerController towerController = Instantiate(towerPrefab, spawnPos, Quaternion.identity, transform).GetComponent<TowerController>();
                    towerList.Add(towerController);
                    towersPlaced++;
                }
            }
        }

        public void EnterBuildingPhase()
        {
            towersPlaced = 0;
            canPlaceTowers = true;
            animator.Play("CloseDoors");
        }

        public void EnterEnemyPhase()
        {
            Debug.Log($"The number of towers in the list: {towerList.Count}");
            canPlaceTowers = false;
            animator.Play("OpenDoors");
        }
    }
}