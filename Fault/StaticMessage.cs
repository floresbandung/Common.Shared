using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.TataBuku.Shared.Fault
{
    public static class StaticMessage
    {
        public const string PROCESS_ACTIVITY_NOT_FOUND = "Process activity not found";
        public const string INVALID_PROCESS_ACTIVITY_INDEX = "Invalid process activity index";
        public const string INVALID_CONNECTION_STRING = "Connection to database server failed";
        public const string DEFAULT_ERROR_MESSAGE = "Something went wrong please try again later";
        public const string INVALID_USER_RIGHT_APPROVAL = "Current user does not have right no create approval";
        public const string INVALID_REQUEST_TYPE = "Invalid request type";
    }
}
