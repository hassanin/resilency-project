using System.ComponentModel.DataAnnotations;

namespace web_api.Config
{
    public class ResponseArray
    {
        [Required]
        [MinLength(1)]
        public required ResponseProfile[] ResponseProfiles { get; init; }

        /// <summary>
        /// TODOL Perform Real Validation
        /// The percentiles shuld be sorted and should be in increasing order. There shold be one entry with a percentile
        /// Value of 100 (representing the Max
        /// </summary>
        public static Func<ResponseArray, bool> GetValidator = (responseArray) =>
        {
            //SortedList<int,ResponseProfile> mySortedList = new System.Collections.Generic.SortedList<int,ResponseProfile>(
            //    (val1,val2) => { 
            //        return val1.
            //    };   
            return true;
        };
    }
}
