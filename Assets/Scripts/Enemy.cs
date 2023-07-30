using System.Collections;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class Enemy : InteractableObject, IPerson, ICached {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        private Transform object_transform;
		public Transform Cached_transform { get { return object_transform; } set { object_transform = value; } }

        private float motion_speed = 0;
        public float Motion_speed { get { return motion_speed; } set { motion_speed = value; } }

        private bool is_free_in_cache = true;
        public bool Is_free_in_cache => is_free_in_cache;
		public void MakeFree() { is_free_in_cache = true; }
		public void MakeBusy() { is_free_in_cache = false; }


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
        }


        /// <summary>
        /// Attacks an opponent.
        /// </summary>
        public void Attacks() { 
            
        }


        /// <summary>
        /// Damages a pesron.
        /// </summary>
        public void Damaged( float damage ) { 
        
            #if( UNITY_EDITOR || DEBUG_MODE )
            //Debug.Log( "The enemy " + name + " has been damaged" );
            #endif

            health -= damage;

            if( health > game_settings.Enemy_starting_health ) { 
            
                health = game_settings.Enemy_starting_health;
            }

            else if( health < 0 ) { 
            
                health = 0;
            }

            OnDamaged?.Invoke( this );

            if( health <= 0 ) { 
            
                Killed();
            }            
        }


        /// <summary>
        /// Kills a person.
        /// </summary>
        public void Killed() { 

            #if( UNITY_EDITOR || DEBUG_MODE )
            Debug.Log( "The enemy " + name + " has been damaged" );
            #endif

            health = 0;

            OnKilled?.Invoke( this );
        }


        /// <summary>
        /// Activates the specified object and put it under the activation parent.
        /// </summary>
        public void Activate( Transform parent_transform ) {

            object_transform.SetParent( parent_transform, false );
            object_transform.gameObject.SetActive( true );
            object_transform.localPosition = Vector3.zero;
            object_transform.localRotation = Quaternion.identity;

            health = game_settings.Enemy_starting_health;

            StartCoroutine( MoveEnemy() );

            IEnumerator MoveEnemy() { 
            
                while( enabled ) { 
                
                    Cached_transform.Translate( 0, Motion_speed * Time.deltaTime * (-1), 0, Space.Self );

                    yield return null;
                }

                yield break;
            }
        }


        /// <summary>
        /// Deactivates the specified object and put it under the pool parent.
        /// </summary>
        public void Deactivate( Transform parent_transform ) {

            object_transform.SetParent( parent_transform, true );
            object_transform.gameObject.SetActive( false );
        }
	}
}