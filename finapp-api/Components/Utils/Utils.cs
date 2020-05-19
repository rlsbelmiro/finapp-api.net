using FinAppApi.Components.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Utils
{
    public static class Utils
    {
        public static List<EnumViewModel> GetListOfEnum(Enum type)
        {
            var types = Enum.GetValues(type.GetType());
            List<EnumViewModel> lista = new List<EnumViewModel>();
            if(type != null)
            {
                foreach(var t in types)
                {
                    lista.Add(new EnumViewModel()
                    {
                        Name = Enum.GetName(type.GetType(), t),
                        Value = Convert.ToInt32(t)
                    });
                }
            }

            return lista;
        }
    }
}
