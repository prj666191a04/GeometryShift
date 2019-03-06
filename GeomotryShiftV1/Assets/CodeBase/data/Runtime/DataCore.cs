using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCore 
{
    public class GroupedData
    {
        public class PlayerData {
            public string name;
            //In secconds
            public long playTime;
        }
        public class WorldState {
            Leveldata level1Data;
            Leveldata level2Data;
            Leveldata level3Data;
            Leveldata level4Data;
            Leveldata level5Data;
            Leveldata level6Data;
            Leveldata level7Data;
            Leveldata level8Data;
            Leveldata level10Data;
            Leveldata level11Data;
            Leveldata level12Data;
            Leveldata level13Data;
            Leveldata level14Data;
            Leveldata level15Data;
        }





    }

}

public class Leveldata {
    int levelId;
    int compleeteCode;    
}




