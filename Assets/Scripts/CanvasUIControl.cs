using UnityEngine;
using UnityEngine.UI;

namespace NetyaginSergey.TestFor1C {

    public class CanvasUIControl : MonoBehaviour {

        private static CanvasUIControl instance;
        public static CanvasUIControl Instance => (instance == null) ? (instance = FindObjectOfType<CanvasUIControl>( true )) : instance;

        [Space( 10 ), SerializeField]
        private RectTransform panel_success;

        [SerializeField]
        private RectTransform panel_failure;

        [SerializeField]
        private RectTransform panel_buttons;

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
        /// Shows/hides the success window.
        /// </summary>
        public void SetActiveSuccessWindow( bool activate ) {            

            panel_success.gameObject.SetActive( activate );
            panel_buttons.gameObject.SetActive( activate );
        }


        /// <summary>
        /// Shows/hides the failure window.
        /// </summary>
        public void SetActiveFailureWindow( bool activate ) { 

            panel_failure.gameObject.SetActive( activate );
            panel_buttons.gameObject.SetActive( activate );
        }


        /// <summary>
        /// Updates the health text value.
        /// </summary>
        public void UpdateHealth( float health ) { 
            
            text_health.text = ((int) (health * 100)).ToString() + "%";
        }


        /// <summary>
        /// Exits the game.
        /// </summary>
        public void OnClickExit() { 
         
            SetActiveSuccessWindow( false );
            SetActiveFailureWindow( false );

            GameplayManager.Instance.ExitGame();
        }


        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void OnClickRestart() { 

            SetActiveSuccessWindow( false );
            SetActiveFailureWindow( false );
            
            GameplayManager.Instance.RestartGame();
        }
    }
}