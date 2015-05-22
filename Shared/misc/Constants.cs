namespace Shared.misc
{
    public class Constants
    {
        public const string JOB_CHECKS = "JobChecks";        
        public const string SMTP_ADDRESS = "SMTP_ADDRESS";        
        public const string WEBSITES_TO_CHECK = "WEBSITES_TO_CHECK";
        public const string DRIVES_TO_CHECK_FOR_SPACE = "DRIVES_TO_CHECK_FOR_SPACE";
        public const string DIRECTORIES_TO_GET_COUNTS = "DIRECTORIES_TO_GET_COUNTS";
        public const string APPLICATION_NAME = "WebsiteCheck";
        public const string SYSTEMS = "SYSTEMS";

        public const string NOTIFICATION_EMAIL = "NOTIFICATION_EMAIL";
        public const string CLASS_PREFIX = "BLL.implementations.";
        
        public const string WEB_SITE_CHECK_JOB = "Website check";
        public const string DRIVE_SPACE_CHECK_JOB = "Drive space check";
        public const string DIRECTORY_FILE_CHECKS_JOB = "Directory file counts";

        public const string ERROR = "Error";
        public const string PROCESSING = "Processing system checks...";
        public const string SYSTEM_CHECKES = "System Checks - ";

        public const string EMAIL_NO_RESPONSE = "NoResponse@somesite.com";
    }
    public class Errors
    {
        public const string SMTP_NOT_SET = "SMTP server not set";
        public const string NOTIFICATION_EMAIL_NOT_SET = "Notification email is not set";
        public const string CANNOT_GET_DRIVE_SPECIFICATION = "Can't get specifications for ";
    }
    public class ServiceConstants
    {
        public const string STARTING = "Starting...";
        public const string STARTING_EXCLAMATION = "Start!";
        public const string STOPPING = "Stopping...";
        public const string STOPPING_EXCLAMATION = "Stopped!";
        public const string TIMER_SETUP = "Setting up timer...";
        public const string TIMER_EXCLAMATION = "Timer Setup!";
        public const string RUN_START = "Timer stopped...starting run()";
        public const string RUN_STOP = "Timer starting...run() complete!";
        public const string ERROR = "ERROR: ";
    }
    public class JobConstants
    {
        public const string ERROR = "ERROR: ";
        public const string STACKTRACE = " - Stacktrace ";
        public const string KERNAL32 = "kernel32.dll";
        public const string NEW_LINE = "\r\n";
        public const string FORWARD_SLASH = "/";
        public const string DASH = " - ";
    }
    public class Messages
    {
        public const string GETTING_DRIVE_SPECS = "Getting drive specifications (path): ";
        public const string SPACE_LABEL = "Free/Total Mega Bytes ";
        public const string GETTING_COUNTS = "Getting counts for ";
        public const string FILE_COUNT_LABEL = "File Count: ";
        public const string DIRECTORY_COUNT_LABEL = "Directory Count: ";
        public const string TESTING_WEBSITE = "Testing website ";
        public const string SEARCH_TERM = " for search term '";
        public const string END_SINGLE_QUOTE = "'";
        public const string END_SINGLE_QUOTE_DASH = "' - ";
        public const string ERROR_WEBSITE_RESPONSE = "ERROR: Website Response - ";
        public const string SEARCH_TERM_NOT_FOUND = " does not contain the expected search term '";
        public const string SEARCH_TERM_FOUND = " contains the search term '";
        public const string WEBSITE = "Website ";
        public const string ERROR_SEARCH_TERM = " has error while searching for term '";
        public const string OK_RESPONSE = " returned 'OK' response";
        public const string NOT_RESPONDING = " is not responding";
    }
}
