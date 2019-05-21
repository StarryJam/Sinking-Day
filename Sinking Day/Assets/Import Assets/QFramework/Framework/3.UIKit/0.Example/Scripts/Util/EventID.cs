using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.EventID
{
    public static class UIEventID
    {
        public enum MenuPanel
        {
            ChangeMenuColor = 3001,//ui事件ID为3000～5999，见QMsgSpan
        }
        
    }

    public static class GameEventID
    {

        public enum Turn
        {
            endTurn = QMgrID.Game + 1,
        }

        public enum Selectee
        {
            inRange = QMgrID.Game + 1,
        }

        
    }
}