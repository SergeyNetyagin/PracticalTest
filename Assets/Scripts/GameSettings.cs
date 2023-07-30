using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public enum FireControlMode { 
    
        Auto,
        Manual
    }

    [CreateAssetMenu( fileName = "Game Settings", menuName = "NetyaginSergey Test/Game Settings" )]
    public class GameSettings : ScriptableObject {

        [Header( "PLAYER SETTINGS" ), SerializeField, Range( 0.1f, 1f )]
        private float player_starting_health = 1;
        public float Player_starting_health => player_starting_health;

        [SerializeField, Range( 0.5f, 5f )]
        private float player_motion_speed = 1;
        public float Player_motion_speed => player_motion_speed;

        [SerializeField, Range( 1, 10 ), Tooltip( "Shoots per a second" )]
        private int fire_speed = 2;
        public int Fire_speed => fire_speed;

        [SerializeField]
        private FireControlMode fire_control_mode = FireControlMode.Auto;
        public FireControlMode Fire_control_mode => fire_control_mode;

        [SerializeField]
        private KeyCode manual_fire_key = KeyCode.Space;
        public KeyCode Manual_fire_key => manual_fire_key;

        [SerializeField, Range( 0.5f, 1f )]
        private float fire_zone_radius = 0.75f;
        public float Fire_zone_radius => fire_zone_radius;

        [SerializeField, Range( 0.1f, 2f)]
        private float fire_collider_width = 1f;
        public float Fire_collider_width => fire_collider_width;

        [SerializeField, Range( 0.1f, 0.5f )]
        private float shoot_visualization_time = 0.25f;
        public float Shoot_visualization_time => shoot_visualization_time;

        [SerializeField]
        private bool show_fire_zone = false;
        public bool Show_fire_zone => show_fire_zone;

        [Header( "DAMAGE SETTINGS" ), SerializeField, Range( 1, 20 ), Tooltip( "Bullet motion speed" )]
        private float bullet_motion_speed = 7;
        public float Bullet_motion_speed => bullet_motion_speed;

        [SerializeField, Range( 0.5f, 2f ), Tooltip( "Bullet visible and its collider size" )]
        private float bullet_size = 1;
        public float Bullet_size => bullet_size;

        [SerializeField, Range( 0.05f, 0.5f ), Tooltip( "Bullet damage power by colliding with an enemy" )]
        private float bullet_damage = 0.1f;
        public float Bullet_damage => bullet_damage;

        [Header( "ENEMY SETTINGS" ), SerializeField]
        private Vector2Int enemy_session_count_range = new Vector2Int( 10, 20 );
        public Vector2Int Enemy_session_count_range => enemy_session_count_range;

        [SerializeField]
        private Vector2 enemy_spawning_timeout_range = new Vector2Int( 5, 10 );
        public Vector2 Enemy_spawning_timeout_range => enemy_spawning_timeout_range;

        [SerializeField]
        private Vector2 enemy_motion_speed_range = new Vector2Int( 1, 10 );
        public Vector2 Enemy_motion_speed_range => enemy_motion_speed_range;

        [SerializeField, Range( 0.1f, 1f )]
        private float enemy_starting_health = 1;
        public float Enemy_starting_health => enemy_starting_health;
    }
}