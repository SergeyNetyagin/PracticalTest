using System.Collections;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class GunControl : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private Player player;

        [SerializeField]
        private FireCollider fire_collider;

        [Space( 10 ), SerializeField]
        private SpriteRenderer sprite_renderer;

        [SerializeField]
        private Animator animator;

        public bool Is_ready_to_fire { get; private set; } = true;

        private float fire_ready_timer = 0;
        private float fire_delta_time = 0;

        private Coroutine shoot_coroutine = null;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
            sprite_renderer.enabled = false;
            animator.enabled = false;

            fire_delta_time = 1f / game_settings.Fire_speed;

            StartCoroutine( FireControl() );
        }


        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update() {
        
            if( game_settings.Fire_control_mode == FireControlMode.Auto ) { 
                
                if( Is_ready_to_fire && fire_collider.Has_aimed_enemy ) { 
                
                    Fire();
                }
            }

            else if( game_settings.Fire_control_mode == FireControlMode.Manual ) { 
                
                if( Input.GetKeyDown( game_settings.Manual_fire_key ) ) { 
                    
                    Fire();
                }
            }
        }


        /// <summary>
        /// Make gun fire and run a bullet.
        /// </summary>
        public void Fire() {

            if( !Is_ready_to_fire ) {

                return;
            }
                
            fire_ready_timer = 0;

            Is_ready_to_fire = false;

            player.Attacks();

            if( shoot_coroutine != null ) { 
            
                StopCoroutine( shoot_coroutine );
            }

            StartCoroutine( ShootVisualization() );

            IEnumerator ShootVisualization() {
            
                sprite_renderer.enabled = true;
                animator.enabled = true;

                yield return new WaitForSeconds( game_settings.Shoot_visualization_time );

                sprite_renderer.enabled = false;
                animator.enabled = false;

                yield break;
            }
        }


        /// <summary>
        /// Controls the fire using the fire speed.
        /// </summary>
        private IEnumerator FireControl() { 
        
            while( enabled ) { 

                fire_ready_timer += Time.deltaTime;

                if( fire_ready_timer >= fire_delta_time ) { 
                    
                    Is_ready_to_fire = true;
                }
           
                yield return null;
            }

            yield break;
        }
    }
}