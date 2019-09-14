using System;
using System.Collections.Generic;

namespace Hbm2Code
{
    public interface IHbmCustomizer
    {
        void Customize(ClassInfo classInfo);
    }

    public class HbmCustomizer : IHbmCustomizer
    {
        private readonly List<Action<Property>> customizers = new List<Action<Property>>();

        public void Register(Action<Property> customizer)
        {
            customizers.Add(customizer);
        }

        public void Customize(ClassInfo classInfo)
        {
            foreach (var prop in classInfo.GetChildProperties())
                customizers.ForEach(customize => customize(prop));
        }
    }
}