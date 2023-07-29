using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class Enemy : MonoBehaviour, IPerson {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField, Range( 0f, 1f )]
        private float health = 1;
        public float Health => health;


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