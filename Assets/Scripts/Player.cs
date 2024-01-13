using System;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    [ExecuteInEditMode]
    public class Player : LivingPerson, IPerson, ICached {

        public Action OnDamaged;

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private FireZone fire_zone;

        [SerializeField]
        private FireCollider fire_collider;

        [SerializeField]
        private BulletHolder bullet_holder;

        [Space( 10 ), SerializeField]
        private Vector3 player_start_local_position = new Vector3( 0, (-3), 0 );

        private Transform object_transform;
		public Transform Cached_transform { get { return object_transform; } set { object_transform = value; } }

        private float motion_speed = 0;
        public float Motion_speed { get { return motion_speed; } set { motion_speed = value; } }

        private bool is_free_in_cache = true;
        public bool Is_free_in_cache => is_free_in_cache;
	public void MakeFree() { is_free_in_cache = true; }
	public void MakeBusy() { is_free_in_cache = false; }


        /// <summary>
        /// Awake is called before the first frame update.
        /// </summary>
        private void Awake() {

            if( !Application.isPlaying ) {

                return;
            }

            if( object_transform == null ) { 
                
                object_transform = transform;
            }
        }


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
            if( !Application.isPlaying ) {

                return;
            }

            object_transform.localPosition = player_start_local_position;

            motion_speed = game_settings.Player_motion_speed;

            CheckForFireZone();
        }


        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        #if( UNITY_EDITOR )
        private void Update() {
        
            if( !Application.isPlaying ) { 

                CheckForFireZone();
            
                return;
            }
        }
        #endif


        /// <summary>
        /// Controls the fire zone visualization.
        /// </summary>
        private void CheckForFireZone() { 
            
            if( game_settings.Show_fire_zone ) { 
                
                fire_zone.Show();
                fire_zone.SetZoneSize( game_settings.Fire_zone_radius, game_settings.Fire_collider_width );
            }

            else { 
                
                fire_zone.Hide();
            }
        }


        /// <summary>
        /// Checks for the inactive specified enemy and exclude it from target list.
        /// </summary>
        public void CheckForInactiveEnemy( Enemy enemy ) { 
            
            fire_collider.CheckForInactiveEnemy( enemy );
        }


        /// <summary>
        /// Attacks an opponent.
        /// </summary>
        public void Attacks() { 
                        
            bullet_holder.Fire();
        }


        /// <summary>
        /// Damages a pesron.
        /// </summary>
        public void Damaged( float damage ) { 

            health -= damage;

            if( health < game_settings.Bullet_damage ) { 
            
                health = 0;
            }

            if( health > 1 ) { 
            
                health = 1;
            }

            else if( health < 0 ) { 
            
                health = 0;
            }

            OnDamaged?.Invoke();

            if( health <= 0 ) { 
            
                Died();
            }

            #if( UNITY_EDITOR || DEBUG_MODE )
            //Debug.Log( "The player has been damaged with damage power " + damage + "; the health is " + health );
            #endif
        }


        /// <summary>
        /// Kills a person.
        /// </summary>
        public void Died() { 

            health = 0;

            CanvasUIControl.Instance.UpdateHealth( health );

            OnDamaged?.Invoke();

            #if( UNITY_EDITOR || DEBUG_MODE )
            Debug.Log( "The player has been KILLED" );
            #endif
        }


        /// <summary>
        /// Activates the specified object and put it under the activation parent.
        /// </summary>
        public void Activate( Transform parent_transform ) {

            object_transform.SetParent( parent_transform, false );
            object_transform.gameObject.SetActive( true );
            object_transform.localPosition = player_start_local_position;
            object_transform.localRotation = Quaternion.identity;

            health = game_settings.Player_starting_health;
        }


        /// <summary>
        /// Deactivates the specified object and put it under the pool parent.
        /// </summary>
        public void Deactivate( Transform parent_transform ) {

            object_transform.gameObject.SetActive( false );
        }


        /// <summary>
        /// Damages the player with damage value from settings.
        /// </summary>
        #if( UNITY_EDITOR )
        [ContextMenu( "Damage player" )]
        #endif
        private void Damage() { 
            
            Damaged( game_settings.Bullet_damage );
        }
    }
}
