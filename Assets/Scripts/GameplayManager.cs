using System.Collections;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class GameplayManager : MonoBehaviour {

        private static GameplayManager instance;
        public static GameplayManager Instance => (instance == null) ? (instance = FindObjectOfType<GameplayManager>( true )) : instance;

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private Player player;

        [Space( 10 ), SerializeField]
        private Transform[] spawning_points;

        public bool Game_paused { get; private set; } = false;


        /// <summary>
        /// Awake is called before the first frame update.
        /// </summary>
        private void Awake() {
        
            instance = this;

            Random.InitState( System.DateTime.Now.Millisecond );
        }


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
            CanvasUIControl.Instance.UpdateHealth( player.Health );

            player.OnDamaged += CheckForGameState;
            player.OnKilled += CheckForGameState;

            StartCoroutine( SpawnControl() );
        }


        /// <summary>
        /// OnDestroy is called before the object destroying.
        /// </summary>
		private void OnDestroy() {

            player.OnDamaged -= CheckForGameState;
            player.OnKilled -= CheckForGameState;			
		}


        /// <summary>
        /// Damages the player because the finish border has been reached by an enemy.
        /// </summary>
        public void DamagePlayerOnEnemyFinishReached() { 
         
            player.Damaged( game_settings.Bullet_damage );
        }


        /// <summary>
        /// Checks for the game state.
        /// </summary>
        private void CheckForGameState( InteractableObject interactable_object ) { 
            
            if( interactable_object is Player ) { 
                
                CanvasUIControl.Instance.UpdateHealth( interactable_object.Health );
            }

            else if( interactable_object is Enemy ) { 
                
            }
        }


		/// <summary>
		/// Controls the spawning process.
		/// </summary>
		private IEnumerator SpawnControl() { 

            while( PoolEnemies.Instance.Objects_count == 0 ) { 
            
                yield return null;
            }

            while( enabled ) { 

                if( !Game_paused ) { 
                    
                    ICached enemy = PoolEnemies.Instance.GetFreeObject();

                    if( enemy is IPerson ) { 
                        
                        float motion_speed = Random.Range( game_settings.Enemy_motion_speed_range.x, game_settings.Enemy_motion_speed_range.y );

                        IPerson person = enemy as IPerson;
                        
                        person.Motion_speed = motion_speed;
                    }

                    int spawning_point_index = Random.Range( 0, spawning_points.Length );

                    try {
                    
                        Transform current_spawning_point = spawning_points[ spawning_point_index ];

                        enemy.MakeBusy();
                        enemy.Activate( PoolEnemies.Instance.Activation_parent_transform );
                        enemy.Cached_transform.position = current_spawning_point.position;
                    }

                    catch( System.Exception exception ) { 
                        
                        #if( UNITY_EDITOR || DEBUG_MODE )
                        Debug.LogException( exception );
                        #endif
                    }
                }
            
                float spawning_timeout = Random.Range( game_settings.Enemy_spawning_timeout_range.x, game_settings.Enemy_spawning_timeout_range.y );

                yield return new WaitForSeconds( spawning_timeout );
            }
            
            yield break;
        }
    }
}