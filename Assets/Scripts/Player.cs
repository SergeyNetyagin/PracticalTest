using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    [ExecuteInEditMode]
    public class Player : MonoBehaviour, IPerson {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private FireZone fire_zone;

        [SerializeField]
        private GunControl gun_control;

        [Space( 10 ), SerializeField, Range( 0f, 1f )]
        private float health = 1;
        public float Health => health;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
            if( Application.isPlaying ) {            

                CheckForFireZone();

                return;
            }
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
        /// Attacks an opponent.
        /// </summary>
        public void Attacks() { 
                        
        }


        /// <summary>
        /// Damages a pesron.
        /// </summary>
        public void Damaged() { 

            if( health <= 0 ) { 
            
                Killed();
            }                        
        }


        /// <summary>
        /// Kills a person.
        /// </summary>
        public void Killed() { 
            
        }
    }
}