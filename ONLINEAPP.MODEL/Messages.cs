using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class Messages
    {
        /// <summary>
        /// An error occured while reading data. GUID: {0}
        /// </summary>
        public const string MsgExceptionOccured = "An error occured while reading data. GUID: {0}";
        /// <summary>
        /// Details added successfully
        /// </summary>
        public const string MsgSuccessAdd = "Details added successfully";
        /// <summary>
        /// Details updated successfully
        /// </summary>
        public const string MsgSuccessUpdate = "Details updated successfully";
        /// <summary>
        /// Details deleted successfully
        /// </summary>
        public const string MsgSuccessDelete = "Details deleted successfully";
        /// <summary>
        /// Something went wrong
        /// </summary>
        public const string MsgSomethingWentWrong = "Something went wrong";

        /**Transport*/
        public const string MsgTransportAddedSuccessfully = "Your Registration Mark data has been saved successfully";
        public const string MsgTransportUpdatedSuccessfully = "Your Registration Mark data has beenupdated successfully";
        public const string MsgTransportDeletedSuccessfully = "Your Registration Mark data has been deleted successfully";

        public const string MsgTransportMarkAvailableSuccessfully = "Selected Mark is available!";
        public const string MsgTransportMarkNoAvailableSuccessfully = "Oops! Selected Mark is not available!";

        public const string MsgTransportMarkwithinLimit = "You have not reached the limit for number applications !";
        public const string MsgTransportMarkOutLimit = "Oops! You have exceed the limit of number applications!";

        /*MVD*/
        public const string MsgMVDValid = "The Motor Vehicle Dealer ID is Valid!";
        public const string MsgMVDNotValid = "Oops! This Motor Vehicle Dealer ID is not Valid!";



        /**Reserved Mark*/
        public const string MsgReservedMarkAddedSuccessfully = "Reserved Mark data added successfully";
        public const string MsgReservedMarkUpdatedSuccessfully = "Reserved Mark data updated successfully";
        public const string MsgReservedMarkDeletedSuccessfully = "Selected Reserved Mark data deleted successfully";

        /**Registration Rules*/
        public const string MsgRegistrationRuleAddedSuccessfully = "Registration rule added successfully";
        public const string MsgRegistrationRuleUpdatedSuccessfully = "Registration rule updated successfully";
        public const string MsgRegistrationRuleDeletedSuccessfully = "Selected registration rule deleted successfully";

        /**Events */
        public const string MsgEventAddedSuccessfully = "Event added successfully";
        public const string MsgEventUpdatedSuccessfully = "Event updated successfully";
        public const string MsgEventDeletedSuccessfully = "Selected Event deleted successfully";

        /**News */
        public const string MsgNewsAddedSuccessfully = "News added successfully";
        public const string MsgNewsUpdatedSuccessfully = "News updated successfully";
        public const string MsgNewsDeletedSuccessfully = "Selected News deleted successfully";

    }
}
