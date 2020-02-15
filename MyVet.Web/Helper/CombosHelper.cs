using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Helper
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            /* // POCO OPTIMO
           var list = new List<SelectListItem>();
           foreach (var petType in _dataContext.PetTypes)
           {
               list.Add(new SelectListItem { 
               Text=petType.Name,
               Value=$"{petType.Id}"
               });
           }
           */
            var list = _dataContext.PetTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Select A Pet Type...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboServiceTypes()
        {
            var list = _dataContext.ServiceTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
              .OrderBy(pt => pt.Text)
              .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Select A Service Type...]",
                Value = "0"
            });

            return list;
        }




    }
}
