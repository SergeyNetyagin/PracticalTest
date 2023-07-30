using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class BulletCollider : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private Bullet bullet;


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

            ICached effect = PoolEffects.Instance.GetFreeObject();

            if( effect == null ) { 
            
                #if( UNITY_EDITOR || DEBUG_MODE )
                Debug.LogError( "Cannot make an effect because an effect returned from the pool is NULL!" );
                #endif

                return;
            }

            effect.MakeBusy();
            effect.Activate( bullet.Cached_transform );
            effect.Cached_transform.SetParent( bullet.Cached_transform.parent, true );

            bullet.Deactivate( PoolBullets.Instance.Pool_transform );
            bullet.MakeFree();

            if( interactable_collider is EnemyCollider ) { 
                
                Enemy enemy = interactable_collider.Living_person as Enemy;

                if( enemy == null ) { 

                    #if( UNITY_EDITOR || DEBUG_MODE )
                    Debug.LogError( "Cannot applay Damage() from the bullet " + bullet.name + " to the enemy because the enemy is NULL!" );
                    #endif

                    return;
                }

                enemy.Damaged( bullet.Damage );
            }

            #if( UNITY_EDITOR || DEBUG_MODE )
            //Debug.Log( "The bullet " + bullet.name + " collided with " + collider.name );
            #endif
        }
    }
}