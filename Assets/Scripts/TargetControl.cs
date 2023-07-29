using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class TargetControl : MonoBehaviour {

        public Enemy Aimed_enemy { get; private set; } = null;

        public bool Has_aimed_enemy => (Aimed_enemy != null);


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

            Enemy enemy = collider.GetComponent<Enemy>();

            if( enemy != null ) {

                #if( UNITY_EDITOR || DEBUG_MODE )
                Debug.Log( "The player aimed an enemy " + enemy.name );
                #endif

                Aimed_enemy = enemy;
            }
		}


        /// <summary>
        /// Detects other collider exit.
        /// </summary>
		private void OnTriggerExit2D( Collider2D collider ) {

            if( collider == null ) { 

                return;
            }

            Enemy enemy = collider.GetComponent<Enemy>();

            if( enemy != null ) {

                #if( UNITY_EDITOR || DEBUG_MODE )
                Debug.Log( "The player shooting line out of the enemy zone " + enemy.name );
                #endif

                Aimed_enemy = null;
            }
		}
    }
}