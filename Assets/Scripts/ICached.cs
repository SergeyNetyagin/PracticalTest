using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public interface ICached {


        /// <summary>
        /// Returns true if the object is free to using.
        /// </summary>
        bool Is_free_in_cache { get; }


        /// <summary>
        /// Returns a cached Transform of the object.
        /// </summary>
        Transform Cached_transform { get; set; }


        /// <summary>
        /// Activates the specified object and put it under a parent.
        /// </summary>
        void Activate( Transform parent_transform );


        /// <summary>
        /// Deactivates the specified object and put it under a parent.
        /// </summary>
        void Deactivate( Transform parent_transform );


        /// <summary>
        /// Makes the object free.
        /// </summary>
        void MakeFree();


        /// <summary>
        /// Makes the object busy.
        /// </summary>
        void MakeBusy();
    }
}