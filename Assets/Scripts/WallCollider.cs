using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public enum WallType { 
    
        Unknown,
        BorderLeft,
        BorderRight,
        BorderTop,
        BorderBottom,
        BorderFinish
    }

    public class WallCollider : InteractableCollider {

        [Space( 10 ), SerializeField]
        private WallType wall_type = WallType.Unknown;
        public WallType Wall_type => wall_type;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
        }
    }
}