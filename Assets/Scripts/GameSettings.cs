using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public enum FireControlMode { 
    
        Auto,
        Manual
    }

    [CreateAssetMenu( fileName = "Game Settings", menuName = "NetyaginSergey Test/Game Settings" )]
    public class GameSettings : ScriptableObject {

        [Space( 10 ), SerializeField, Range( 1, 10 ), Tooltip( "Shoots per a second" )]
        private int fire_speed = 2;
        public int Fire_speed => fire_speed;

        [SerializeField, Range( 10, 100 )]
        private int bullets_buffer_size = 20;
        public int Bullets_buffer_size => bullets_buffer_size;

        [SerializeField]
        private Bullet bullet_prefab;
        public Bullet Bullet_prefab => bullet_prefab;

        [Space( 10 ), SerializeField]
        private FireControlMode fire_control_mode = FireControlMode.Auto;
        public FireControlMode Fire_control_mode => fire_control_mode;

        [SerializeField]
        private KeyCode manual_fire_key = KeyCode.Space;
        public KeyCode Manual_fire_key => manual_fire_key;

        [Space( 10 ), SerializeField, Range( 0.5f, 1f )]
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
    }
}