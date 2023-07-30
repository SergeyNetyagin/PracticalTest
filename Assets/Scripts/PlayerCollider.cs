using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class PlayerCollider : InteractableCollider {

        public bool Is_movable_left { get; private set; } = true;
        public bool Is_movable_right { get; private set; } = true;
        public bool Is_movable_up { get; private set; } = true;
        public bool Is_movable_down { get; private set; } = true;


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

            WallCollider wall = collider.GetComponent<WallCollider>();

            if( wall != null ) {

                #if( UNITY_EDITOR || DEBUG_MODE )
                //Debug.Log( "The player enter to " + wall.name );
                #endif

                if( (wall.Wall_type == WallType.BorderTop) || (wall.Wall_type == WallType.BorderFinish) ) { 
                    
                    Is_movable_up = false;
                }

                if( wall.Wall_type == WallType.BorderBottom ) { 
                    
                    Is_movable_down = false;
                }

                if( wall.Wall_type == WallType.BorderLeft ) { 
                    
                    Is_movable_left = false;
                }

                if( wall.Wall_type == WallType.BorderRight ) { 
                    
                    Is_movable_right = false;
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

            WallCollider wall = collider.GetComponent<WallCollider>();

            if( wall != null ) {

                #if( UNITY_EDITOR || DEBUG_MODE )
                //Debug.Log( "The player EXIT from " + wall.name );
                #endif

                if( (wall.Wall_type == WallType.BorderTop) || (wall.Wall_type == WallType.BorderFinish) ) { 
                    
                    Is_movable_up = true;
                }

                if( wall.Wall_type == WallType.BorderBottom ) { 
                    
                    Is_movable_down = true;
                }

                if( wall.Wall_type == WallType.BorderLeft ) { 
                    
                    Is_movable_left = true;
                }

                if( wall.Wall_type == WallType.BorderRight ) { 
                    
                    Is_movable_right = true;
                }
            }
		}
	}
}