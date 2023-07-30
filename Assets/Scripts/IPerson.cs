using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public interface IPerson {


        /// <summary>
        /// Motion speed of the person.
        /// </summary>
        float Motion_speed { get; set; }


        /// <summary>
        /// Attacks an opponent.
        /// </summary>
        void Attacks();


        /// <summary>
        /// Damages a pesron.
        /// </summary>
        void Damaged( float damage );


        /// <summary>
        /// Kills a person.
        /// </summary>
        void Died();
    }
}