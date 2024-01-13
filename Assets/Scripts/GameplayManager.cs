using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public enum SessionStopReason { 
    
        OnPlayerDied,
        OnEnemyDied
    }

    public class GameplayManager : MonoBehaviour {

        private static GameplayManager instance;
        public static GameplayManager Instance => (instance == null) ? (instance = FindObjectOfType<GameplayManager>( true )) : instance;

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private Player player;

        [Space( 10 ), SerializeField]
        private Transform[] spawning_points;

        public bool Session_is_complete { get; private set; } = false;
        public int Enemy_count { get; private set; } = 0;

        private List<ICached> spawned_enemies = new List<ICached>();


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

            PoolEnemies.Instance.OnCreateEnemy += CheckForEnemyCreation;

            player.OnDamaged += CheckForPlayerState;

            StartSession();
        }


        /// <summary>
        /// OnDestroy is called before the object destroying.
        /// </summary>
	private void OnDestroy() {

            PoolEnemies.Instance.OnCreateEnemy -= CheckForEnemyCreation;

            player.OnDamaged -= CheckForPlayerState;
	}


        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame() { 
            
            #if( UNITY_EDITOR )
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }


        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame() { 
            
            StartSession();
        }


        /// <summary>
        /// Starts a new game session.
        /// </summary>
        private void StartSession() { 

            Time.timeScale = 1;

            Session_is_complete = false;

            player.Activate( transform );

            CanvasUIControl.Instance.UpdateHealth( player.Health );

            Enemy_count = Random.Range( 
                
                game_settings.Enemy_session_count_range.x, 
                game_settings.Enemy_session_count_range.y + 1 
            );

            PoolEnemies.Instance.DeactivateAll();
            PoolEnemies.Instance.MakeAllFree();

            spawned_enemies.Clear();

            StartCoroutine( SpawnControl() );            
        }


        /// <summary>
        /// Stops a current game session.
        /// </summary>
        private void StopSession( SessionStopReason reason ) { 

            Time.timeScale = 0;

            Session_is_complete = true;

            CanvasUIControl.Instance.UpdateHealth( player.Health );

            if( reason == SessionStopReason.OnPlayerDied ) {
            
                CanvasUIControl.Instance.SetActiveFailureWindow( true );
            }

            else if( reason == SessionStopReason.OnEnemyDied ) {
            
                CanvasUIControl.Instance.SetActiveSuccessWindow( true );
            }
        }


        /// <summary>
        /// Damages the player because the finish border has been reached by an enemy.
        /// </summary>
        public void DamagePlayerOnEnemyFinishReached() { 
         
            player.Damaged( game_settings.Bullet_damage );
        }


        /// <summary>
        /// Checks for the player state.
        /// </summary>
        private void CheckForPlayerState() { 
            
            CanvasUIControl.Instance.UpdateHealth( player.Health );

            if( player.Health <= 0 ) { 
                
                StopSession( SessionStopReason.OnPlayerDied );
            }
        }


        /// <summary>
        /// Checks for an enemy creation.
        /// </summary>
        private void CheckForEnemyCreation( Enemy enemy ) { 

            enemy.OnDied += CheckForEnemyDied;
            enemy.OnDeactivated += CheckForEnemyDeactivated;
        }


	/// <summary>
	/// Checks for the enemy died.
	/// </summary>
	private void CheckForEnemyDied( Enemy enemy ) {

            player.CheckForInactiveEnemy( enemy );
        }


	/// <summary>
	/// Checks for the enemy deactivated.
	/// </summary>
	private void CheckForEnemyDeactivated( Enemy enemy ) {

            player.CheckForInactiveEnemy( enemy );
        }


	/// <summary>
	/// Controls the spawning process.
	/// </summary>
	private IEnumerator SpawnControl() { 

            while( PoolEnemies.Instance.Objects_count == 0 ) { 
            
                yield return null;
            }

            while( PoolBullets.Instance.Objects_count == 0 ) { 
            
                yield return null;
            }

            while( PoolEffects.Instance.Objects_count == 0 ) { 
            
                yield return null;
            }

            while( !Session_is_complete ) { 

                if( spawned_enemies.Count < Enemy_count ) {

                    ICached enemy = PoolEnemies.Instance.GetFreeObject();

                    if( enemy is IPerson ) { 
                        
                        float motion_speed = Random.Range( 
                        
                            game_settings.Enemy_motion_speed_range.x, 
                            game_settings.Enemy_motion_speed_range.y 
                        );

                        IPerson person = enemy as IPerson;
                        
                        person.Motion_speed = motion_speed;
                    }

                    int spawning_point_index = Random.Range( 0, spawning_points.Length );

                    try {
                    
                        Transform current_spawning_point = spawning_points[ spawning_point_index ];

                        enemy.MakeBusy();
                        enemy.Activate( PoolEnemies.Instance.Activation_parent_transform );
                        enemy.Cached_transform.position = current_spawning_point.position;

                        spawned_enemies.Add( enemy );
                    }

                    catch( System.Exception exception ) { 
                        
                        #if( UNITY_EDITOR || DEBUG_MODE )
                        Debug.LogException( exception );
                        #endif
                    }
                }

                else { 
                    
                    if( PoolEnemies.Instance.AllAreFree() == true ) {

                        if( player.Health > 0 ) {
                    
                            StopSession( SessionStopReason.OnEnemyDied );

                            yield break;
                        }
                    }
                }
            
                float spawning_timeout = Random.Range( 
                    
                    game_settings.Enemy_spawning_timeout_range.x, 
                    game_settings.Enemy_spawning_timeout_range.y 
                );

                #if( UNITY_EDITOR || DEBUG_MODE )
                //Debug.Log( "Random spawning timeout: " + spawning_timeout );
                #endif

                yield return new WaitForSeconds( spawning_timeout );
            }
            
            yield break;
        }
    }
}
