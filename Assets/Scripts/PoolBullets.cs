using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class PoolBullets : Pool {

        private static PoolBullets instance;
        public static PoolBullets Instance => (instance == null) ? (instance = FindObjectOfType<PoolBullets>( true )) : instance;

        [Space( 10 ), SerializeField]
        private Bullet bullet_prefab;


        /// <summary>
        /// Awake is called before the first frame update.
        /// </summary>
        protected override void Awake() {

            instance = this;
        
            base.Awake();
        }


        /// <summary>
        /// Creates a new object and puts it into the pool.
        /// </summary>
        protected override ICached CreateObject() { 
            
            Bullet bullet = Instantiate( bullet_prefab, pool_transform );

            bullet.name = bullet_prefab.name + " #" + pool_transform.childCount;
            bullet.Cached_transform = bullet.transform;
            bullet.Deactivate( Pool_transform );

            return bullet;
        }
    }
}