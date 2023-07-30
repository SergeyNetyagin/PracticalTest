using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public abstract class Pool : MonoBehaviour {

        [Space( 10 ), SerializeField]
        protected Transform pool_transform;
        public Transform Pool_transform => pool_transform;

        [SerializeField]
        protected Transform activation_parent_transform;
        public Transform Activation_parent_transform => activation_parent_transform;

        [Space( 10 ), SerializeField, Range( 10, 100 )]
		protected int cache_size = 10;

        public int Objects_count => (objects == null) ? list.Count : objects.Length;

        protected List<ICached> list = new List<ICached>();
        
        protected ICached[] objects;


        /// <summary>
        /// Creates a new object and puts it into the pool.
        /// </summary>
        protected abstract ICached CreateObject();


        /// <summary>
        /// Awake is called before the first frame update.
        /// </summary>
        protected virtual void Awake() {
        
            pool_transform = transform;

            CreateObject();
        }


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private IEnumerator Start() {

            yield return null;
        
            while( list.Count < cache_size ) { 
            
                ICached the_new_object = CreateObject();
                                
                if( the_new_object != null ) {
                
                    list.Add( the_new_object );
                }

                yield return null;
            }

            objects = list.ToArray();

            list.Clear();

            yield break;
        }


        /// <summary>
        /// Returns a free object from cache.
        /// </summary>
        public ICached GetFreeObject() { 
            
            ICached the_free_object = null;

            if( objects != null ) { 
                
                for( int i = 0; i < objects.Length; i++ ) { 
                
                    ICached checking_object = objects[i];

                    if( checking_object.Is_free_in_cache ) { 
                    
                        the_free_object = checking_object;

                        break;
                    }
                }
            }

            else { 
            
                for( int i = 0; i < list.Count; i++ ) { 
                
                    ICached checking_object = list[i];

                    if( checking_object.Is_free_in_cache ) { 
                    
                        the_free_object = checking_object;

                        break;
                    }
                }
            }

            return the_free_object;
        }
    }
}