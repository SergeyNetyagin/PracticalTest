using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class EnemyCollider : InteractableCollider {

        [SerializeField]
        private Enemy enemy;
        public Enemy Enemy => enemy;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
        }


        /// <summary>
        /// Detects other collider enter.
        /// </summary>
	private void OnTriggerEnter2D( Collider2D collider ) {

            if( collider == null ) { 

                return;
            }

            InteractableCollider interactable_collider = collider.GetComponent<InteractableCollider>();

            if( interactable_collider == null ) {

                return;
            }

            if( interactable_collider is WallCollider ) { 
                
                WallCollider wall = interactable_collider as WallCollider;

                if( wall.Wall_type == WallType.BorderFinish ) { 

                    GameplayManager.Instance.DamagePlayerOnEnemyFinishReached();

                    enemy.Deactivate( PoolEnemies.Instance.Pool_transform );
                    enemy.MakeFree();

                    #if( UNITY_EDITOR || DEBUG_MODE )
                    //Debug.Log( enemy.name + " reached the finish border and damaged the player; the enemy has been deactivated" );
                    #endif
                }
            }
        }
    }
}
