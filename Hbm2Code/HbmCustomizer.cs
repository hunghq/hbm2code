using System;
using System.Collections.Generic;

namespace Hbm2Code
{
    public class HbmCustomizer
    {
        private List<Action<Property>> customizers = new List<Action<Property>>();

        public void Register(Action<Property> customizer)
        {
            customizers.Add(customizer);
        }

        public void Customize(params ClassInfo[] classInfos)
        {
            foreach (var clazz in classInfos)
                foreach (var prop in clazz.GetChildProperties())
                    customizers.ForEach(customize => customize(prop));
        }
    }
}