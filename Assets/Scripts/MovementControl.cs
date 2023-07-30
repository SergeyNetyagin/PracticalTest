using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class MovementControl : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private Player player;

        [SerializeField]
        private PlayerCollider player_collider;

        [Space( 10 ), SerializeField]
        private KeyCode move_left_key = KeyCode.A;

        [SerializeField]
        private KeyCode move_right_key = KeyCode.D;

        [SerializeField]
        private KeyCode move_up_key = KeyCode.W;

        [SerializeField]
        private KeyCode move_down_key = KeyCode.S;

        private Transform player_transform;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {

            player_transform = player.transform;
        }


        /// <summary>
        /// LateUpdate is called once per frame.
        /// </summary>
        private void LateUpdate() {
 
            float movement = player.Motion_speed * Time.deltaTime;

            if( Input.GetKey( move_left_key ) && player_collider.Is_movable_left ) { 
                
                player_transform.Translate( (- movement), 0, 0, Space.Self );
            }

            else if( Input.GetKey( move_right_key ) && player_collider.Is_movable_right ) { 
                
                player_transform.Translate( movement, 0, 0, Space.Self );
            }

            if( Input.GetKey( move_up_key ) && player_collider.Is_movable_up ) { 
                
                player_transform.Translate( 0, movement, 0, Space.Self );
            }

            else if( Input.GetKey( move_down_key ) && player_collider.Is_movable_down ) { 
                
                player_transform.Translate( 0, (- movement), 0, Space.Self );
            }
        }
	}
}