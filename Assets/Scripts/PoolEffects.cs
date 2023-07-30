using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class PoolEffects : Pool {

        private static PoolEffects instance;
        public static PoolEffects Instance => (instance == null) ? (instance = FindObjectOfType<PoolEffects>( true )) : instance;

        [Space( 10 ), SerializeField]
        private Effect effect_prefab;


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
            
            Effect effect = Instantiate( effect_prefab, pool_transform );

            effect.name = effect_prefab.name + " #" + pool_transform.childCount;
            effect.Cached_transform = effect.transform;
            effect.Deactivate( Pool_transform );

            return effect;
        }
    }
}