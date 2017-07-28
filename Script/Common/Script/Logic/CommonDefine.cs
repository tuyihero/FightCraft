﻿using UnityEngine;
using System.Collections;

using Tables;
namespace GameLogic
{
    public class CommonDefine
    {
        public static string GetQualityColorStr(ITEM_QUALITY quality)
        {
            switch (quality)
            {
                case ITEM_QUALITY.WHITE:
                    return "<color=#ffffff>";
                case ITEM_QUALITY.BLUE:
                    return "<color=#3ba0ff>";
                case ITEM_QUALITY.PURPER:
                    return "<color=#ca40e7>";
                case ITEM_QUALITY.ORIGIN:
                    return "<color=#ffe400>";
            }
            return "<color=#ffffff>";
        }

        public static string GetEnableColorStr(int isEnable)
        {
            switch (isEnable)
            {
                case 1:
                    return "<color=#00ff00>";
                case 0:
                    return "<color=#ff0000>";
            }
            return "<color=#ffffff>";
        }
    }
}
