using UnityEngine;
using UnityEngine.UI;

namespace NetyaginSergey.TestFor1C {

    public class CanvasUIControl : MonoBehaviour {

        private static CanvasUIControl instance;
        public static CanvasUIControl Instance => (instance == null) ? (instance = FindObjectOfType<CanvasUIControl>( true )) : instance;

        [Space( 10 ), SerializeField]
        private Text text_health;


        /// <summary>
        /// Awake is called before the first frame update.
        /// </summary>
        private void Awake() {
        
            instance = this;
        }


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {
        
        }


        /// <summary>
        /// Updates the health text value.
        /// </summary>
        public void UpdateHealth( float health ) { 
            
            text_health.text = ((int) (health * 100)).ToString() + "%";
        }
    }
}