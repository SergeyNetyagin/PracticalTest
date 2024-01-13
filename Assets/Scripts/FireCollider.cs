using System.Collections.Generic;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class FireCollider : MonoBehaviour {

        public bool Has_aimed_enemy => (Aimed_enemies.Count != 0);

        private List<Enemy> Aimed_enemies = new List<Enemy>();


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

            Enemy enemy = collider.GetComponent<EnemyCollider>()?.Enemy;

            if( enemy != null ) {

                #if( UNITY_EDITOR || DEBUG_MODE )
                //Debug.Log( "The player aimed an enemy " + enemy.name );
                #endif

                if( !Aimed_enemies.Contains( enemy ) ) { 
                
                    Aimed_enemies.Add( enemy );
                }
            }
	}


        /// <summary>
        /// Detects other collider exit.
        /// </summary>
	private void OnTriggerExit2D( Collider2D collider ) {

            if( collider == null ) { 

                return;
            }

            Enemy enemy = collider.GetComponent<EnemyCollider>()?.Enemy;

            if( enemy != null ) {

                #if( UNITY_EDITOR || DEBUG_MODE )
                //Debug.Log( "The player shooting line out of the enemy zone; enemy: " + enemy.name );
                #endif

                if( Aimed_enemies.Contains( enemy ) ) { 
                
                    Aimed_enemies.Remove( enemy );
                }
            }
	}


        /// <summary>
        /// Checks for the inactive specified enemy and exclude it from target list.
        /// </summary>
        public void CheckForInactiveEnemy( Enemy enemy ) { 
            
            if( Aimed_enemies.Contains( enemy ) ) { 
                
                Aimed_enemies.Remove( enemy );
            }
        }
    }
}
