using System;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.TouchInput
{
    public class ScriptOrder : Attribute
    {
        public int order;

        public ScriptOrder(int order)
        {
            this.order = order;
        }
    }
}