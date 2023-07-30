using System;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class PoolEnemies : Pool {

        public Action<Enemy> OnCreateEnemy;

        private static PoolEnemies instance;
        public static PoolEnemies Instance => (instance == null) ? (instance = FindObjectOfType<PoolEnemies>( true )) : instance;

        [Space( 10 ), SerializeField]
        private Enemy enemy_prefab;


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
            
            Enemy enemy = Instantiate( enemy_prefab, pool_transform );

            enemy.name = enemy_prefab.name + " #" + pool_transform.childCount;
            enemy.Cached_transform = enemy.transform;
            enemy.Deactivate( Pool_transform );          

            OnCreateEnemy?.Invoke( enemy );

            base.CreateObject();

            return enemy;
        }
    }
}