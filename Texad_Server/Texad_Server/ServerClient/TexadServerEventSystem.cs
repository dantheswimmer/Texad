using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadServerEventSystem
    {
        //Recieve Codes
        public const char CLIENT_JOINED = 'c';
        public const char CLIENT_LOGIN_INFO = '+';

        public const char ACTION_NOTIFY = 'a';

        public const char ITEM_UPDATE_REQUEST = 'i';
        public const char STAT_UPDATE_REQUEST = 'u';
        public const char COMBAT_UPDATE_REQUEST = 'b';
        public const char POS_UPDATE_REQUEST = 'p';
        public const char COMPANION_UPDATE_REQUEST = 'c';

        public const char COMMAND_PROCESS_RERQUEST = 'l';

        //Send Codes
        public const char STORY_UPDATE_RESPONSE = 's';
        public const char STAT_UPDATE_RESPONSE = 'u';
        public const char COMBAT_UPDATE_RESPONSE = 'c';
        public const char POS_UPDATE_RESPONSE = 'p';
        public const char PROG_BAR_UPDATE = 'g';
    }
}
