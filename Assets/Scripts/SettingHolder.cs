using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SettingHolder
{
    public static bool IsTranslateRevers;
    public static bool IsAutoMode;
    private static int _quantityRepit = 1;

    public static int QuantityRepit
    {
        get
        {
            return _quantityRepit;
        }
        set
        {
            if (0 < value && value < 999)
                _quantityRepit = value;
            else
                _quantityRepit = 1;
        }
    }
}

