using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public enum ColliderOffset { 
    
        Zero     = 0,
        Positive = 1,
        Negative = (-1)
    }

    public class ColliderAdapter : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private ColliderOffset vertical_offset = ColliderOffset.Zero;

        [SerializeField]
        private ColliderOffset horizontal_offset = ColliderOffset.Zero;

        [SerializeField]
        private Vector2 additional_offset = Vector2.zero;


        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private IEnumerator Start() {
        
            yield return null;

            AdaptCollider();

            yield break;
        }


        /// <summary>
        /// Adapts the collider using its RectTransform size.
        /// </summary>
        #if( UNITY_EDITOR )
        [ContextMenu( "Adapt the collider" )]
        #endif
        public void AdaptCollider() { 
            
            RectTransform rect_transform = GetComponent<RectTransform>();

            BoxCollider2D box_collider = GetComponent<BoxCollider2D>();

            if( rect_transform == null ) { 
            
                #if( UNITY_EDITOR || DEBUG_MODE )
                Debug.LogError( name + ": cannot adapt collider because RectTransform component is NULL!" );
                #endif

                return;
            }

            if( box_collider == null ) { 
            
                #if( UNITY_EDITOR || DEBUG_MODE )
                Debug.LogError( name + ": cannot adapt collider because BoxCollider2D component is NULL!" );
                #endif

                return;
            }

            Vector2 offset_factor = new Vector2(
                
                (horizontal_offset == ColliderOffset.Zero) ? 0 : (float) horizontal_offset * 0.5f,
                (vertical_offset == ColliderOffset.Zero) ? 0 : (float) vertical_offset * 0.5f
            );

            box_collider.size = new Vector2( 
                
                rect_transform.rect.width, 
                rect_transform.rect.height 
            );

            box_collider.offset = new Vector2( 
                
                box_collider.size.x * offset_factor.x + additional_offset.x, 
                box_collider.size.y * offset_factor.y + additional_offset.y 
            );

            #if( UNITY_EDITOR )
            if( !Application.isPlaying ) { 
                
                EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene() );
            }
            #endif

            #if( UNITY_EDITOR || DEBUG_MODE )
            //Debug.Log( name + " collider has been adapted" );
            #endif
        }
    }
}