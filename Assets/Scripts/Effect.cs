using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class Effect : MonoBehaviour, ICached {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        [Space( 10 ), SerializeField]
        private EventControl event_control;

        private Transform object_transform;
	public Transform Cached_transform { get { return object_transform; } set { object_transform = value; } }

        private bool is_free_in_cache = true;
        public bool Is_free_in_cache => is_free_in_cache;
	public void MakeFree() { is_free_in_cache = true; }
	public void MakeBusy() { is_free_in_cache = false; }


	/// <summary>
	/// Start is called before the first frame update.
	/// </summary>
	private void Start() {
        
            event_control.OnAnimationComplete += Deactivate;
        }


	private void OnDestroy() {
			
            event_control.OnAnimationComplete -= Deactivate;
	}


        /// <summary>
        /// Deactivates the effect.
        /// </summary>
        private void Deactivate() {

            Deactivate( PoolEffects.Instance.Pool_transform );
        }


	/// <summary>
	/// Starts running the bullet along its forward axle.
	/// </summary>
	public void Run() { 
		
		if( !Is_free_in_cache ) { 
		
			#if( UNITY_EDITOR || DEBUG_MODE )
			Debug.LogError( "An attempt to run a bullet busy in the pool!" );
			#endif

			return;
		}
	}


        /// <summary>
        /// Activates the specified object and put it under the activation parent.
        /// </summary>
        public void Activate( Transform parent_transform ) {

            object_transform.SetParent( parent_transform, false );
            object_transform.gameObject.SetActive( true );
            object_transform.localPosition = Vector3.zero;
            object_transform.localRotation = Quaternion.identity;
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
