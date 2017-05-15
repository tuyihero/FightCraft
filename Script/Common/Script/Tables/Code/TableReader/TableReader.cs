﻿using System.Collections;

namespace Tables
{
    public class TableReader
    {

        #region 唯一实例

        private TableReader() { }

        private TableReader _Instance = null;
        public TableReader Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new TableReader();

                return _Instance;
            }
        }

        #endregion
        #region Logic

//
        public static CommonItem CommonItem { get; internal set; }
//
        public static EquipItem EquipItem { get; internal set; }
//
        public static FightAttr FightAttr { get; internal set; }
//
        public static MonsterBase MonsterBase { get; internal set; }
//
        public static StageInfo StageInfo { get; internal set; }
//
        public static StrDictionary StrDictionary { get; internal set; }

        public static void ReadTables()
        {
            //读取所有表
            CommonItem = new CommonItem(TableReadBase.GetTableText("CommonItem"), false);
            EquipItem = new EquipItem(TableReadBase.GetTableText("EquipItem"), false);
            FightAttr = new FightAttr(TableReadBase.GetTableText("FightAttr"), false);
            MonsterBase = new MonsterBase(TableReadBase.GetTableText("MonsterBase"), false);
            StageInfo = new StageInfo(TableReadBase.GetTableText("StageInfo"), false);
            StrDictionary = new StrDictionary(TableReadBase.GetTableText("StrDictionary"), false);

            //初始化所有表
            CommonItem.CoverTableContent();
            EquipItem.CoverTableContent();
            FightAttr.CoverTableContent();
            MonsterBase.CoverTableContent();
            StageInfo.CoverTableContent();
            StrDictionary.CoverTableContent();
        }

        #endregion
    }
}
