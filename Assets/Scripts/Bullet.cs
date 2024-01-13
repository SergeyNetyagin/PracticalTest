using System.Collections;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class Bullet : MonoBehaviour, ICached {

        [Space( 10 ), SerializeField]
        private GameSettings game_settings;

        private Transform object_transform;
	public Transform Cached_transform { get { return object_transform; } set { object_transform = value; } }

        private bool is_free_in_cache = true;
        public bool Is_free_in_cache => is_free_in_cache;
	public void MakeFree() { is_free_in_cache = true; }
	public void MakeBusy() { is_free_in_cache = false; }

        public float Damage { get; private set; } = 0;


	/// <summary>
	/// Start is called before the first frame update.
	/// </summary>
	private void Start() {
        
        }


        /// <summary>
        /// Activates the specified object and put it under the activation parent.
        /// </summary>
        public void Activate( Transform parent_transform ) {

            Damage = game_settings.Bullet_damage;

            float bullet_size = game_settings.Bullet_size;

            object_transform.SetParent( parent_transform, false );
            object_transform.gameObject.SetActive( true );
            object_transform.localPosition = Vector3.zero;
            object_transform.localRotation = Quaternion.identity;
            object_transform.localScale = new Vector3( bullet_size, bullet_size, bullet_size );

            StartCoroutine( MoveBullet() );

            IEnumerator MoveBullet() { 
            
                float bullet_speed = game_settings.Bullet_motion_speed;

                while( enabled ) { 
                
                    Cached_transform.Translate( 0, bullet_speed * Time.deltaTime, 0, Space.Self );

                    yield return null;
                }

                yield break;
            }
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
