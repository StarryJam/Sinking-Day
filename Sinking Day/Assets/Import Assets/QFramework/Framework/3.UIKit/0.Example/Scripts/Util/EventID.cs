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

        public enum TurnManager
        {
            turnStart = QMgrID.Game + 1,
            playerEnd = QMgrID.Game + 5,
            turnEnd = QMgrID.Game + 6
        }

        public enum Selectee
        {
            inRange = QMgrID.Game + 2,
        }

        public enum Guider
        {
            compeletStep = QMgrID.Game + 3,
        }
        
        public enum Unit
        {
            enemyDie = QMgrID.Game + 4,
        }
    }
}