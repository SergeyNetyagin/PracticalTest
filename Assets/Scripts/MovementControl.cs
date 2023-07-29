using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class MovementControl : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private Transform control_transform;

        [SerializeField, Range( 0.5f, 5f )]
        private float movement_speed_ratio = 1;

        [Space( 10 ), SerializeField]
        private KeyCode move_left_key = KeyCode.A;

        [SerializeField]
        private KeyCode move_right_key = KeyCode.D;

        [SerializeField]
        private KeyCode move_up_key = KeyCode.W;

        [SerializeField]
        private KeyCode move_down_key = KeyCode.S;

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
        /// LateUpdate is called once per frame.
        /// </summary>
        private void LateUpdate() {
 
            float movement = Time.deltaTime * movement_speed_ratio;

            if( Input.GetKey( move_left_key ) && Is_movable_left ) { 
                
                control_transform.Translate( (- movement), 0, 0, Space.Self );
            }

            else if( Input.GetKey( move_right_key ) && Is_movable_right ) { 
                
                control_transform.Translate( movement, 0, 0, Space.Self );
            }

            if( Input.GetKey( move_up_key ) && Is_movable_up ) { 
                
                control_transform.Translate( 0, movement, 0, Space.Self );
            }

            else if( Input.GetKey( move_down_key ) && Is_movable_down ) { 
                
                control_transform.Translate( 0, (- movement), 0, Space.Self );
            }
        }


        /// <summary>
        /// Detects other collider enter.
        /// </summary>
		private void OnTriggerEnter2D( Collider2D collider ) {

            if( collider == null ) { 

                return;
            }

            Wall wall = collider.GetComponent<Wall>();

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

            Wall wall = collider.GetComponent<Wall>();

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