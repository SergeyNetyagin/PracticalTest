using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class FireZone : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private Transform fire_zone_transform;

        [SerializeField]
        private SpriteRenderer sprite_renderer;

        [SerializeField]
        private BoxCollider2D fire_collider;

        [SerializeField, Range( 1f, 100f )]
        private float collider_height_ratio = 10;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
        }


        /// <summary>
        /// Assigns a radius for the fire zone.
        /// </summary>
        public void SetZoneSize( float zone_radius, float collider_width ) { 
            
            fire_zone_transform.localScale = new Vector3( 
                
                zone_radius, 
                zone_radius, 
                zone_radius 
            );

            fire_collider.size = new Vector2( 
                
                collider_width, 
                zone_radius * collider_height_ratio
            );

            fire_collider.offset = new Vector2(

                fire_collider.offset.x,
                fire_collider.size.y * 0.5f
            );
        }


        /// <summary>
        /// Shows the fire zone.
        /// </summary>
        public void Show() { 
            
            if( sprite_renderer != null ) {
            
                sprite_renderer.enabled = true;
            }
        }


        /// <summary>
        /// Hides the fire zone.
        /// </summary>
        public void Hide() { 
            
            if( sprite_renderer != null ) {
            
                sprite_renderer.enabled = false;
            }
        }
    }
}