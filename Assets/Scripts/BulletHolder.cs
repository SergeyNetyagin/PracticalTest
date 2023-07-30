using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class BulletHolder : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private Transform bullet_holder_transform;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() {

        }


        /// <summary>
        /// Creates and start a bullet.
        /// </summary>
        public void Fire() { 
            
            ICached bullet = PoolBullets.Instance.GetFreeObject();

            if( bullet == null ) { 
            
                #if( UNITY_EDITOR || DEBUG_MODE )
                Debug.LogError( "Cannot shoot because a bullet returned from the pool is NULL!" );
                #endif

                return;
            }

            bullet.MakeBusy();
            bullet.Activate( bullet_holder_transform );
        }
    }
}