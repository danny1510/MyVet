using MyVet.Common.Models;
using MyVet.Web.Data;
using MyVet.Web.Data.Entities;
using MyVet.Web.Models;
using System.Threading.Tasks;

namespace MyVet.Web.Helper
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext datacontext,
            ICombosHelper combosHelper)
        {
            _dataContext = datacontext;
            _combosHelper = combosHelper;
        }

        public async Task<History> ToHistoryAsync(HistoryViewModel model)
        {
            return new History
            {
                Date = model.Date.ToUniversalTime(),
                Description = model.Description,
                Id = (model.Id != 0) ? model.Id : 0,
                Pet = await _dataContext.Pets.FindAsync(model.PetId),
                Remarks = model.Remarks,
                ServiceType = await _dataContext.ServiceTypes.FindAsync(model.ServiceTypeId)
            };


        }

        public HistoryViewModel ToHistoryViewModel(History history)
        {
            return new HistoryViewModel
            {
                Date = history.Date,
                Description = history.Description,
                Id = history.Id,
                PetId = history.Pet.Id,
                Remarks = history.Remarks,
                ServiceTypeId = history.ServiceType.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes()
            };

        }

        public async Task<Pet> ToPetAsync(PetViewModel model, string path)
        {
            return new Pet
            {
                Agendas = model.Agendas,
                Born = model.Born,
                Histories = model.Histories,
                Id = (model.Id != 0) ? model.Id : 0,
                ImageUrl = path,
                Name = model.Name,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                PetType = await _dataContext.PetTypes.FindAsync(model.PetTypeId),
                Race = model.Race,
                Remarks = model.Remarks
            };

        }

        public PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Agendas = pet.Agendas,
                Born = pet.Born,
                Histories = pet.Histories,
                ImageUrl = pet.ImageUrl,
                Name = pet.Name,
                Owner = pet.Owner,
                PetType = pet.PetType,
                Race = pet.Race,
                Remarks = pet.Remarks,
                Id = pet.Id,
                OwnerId = pet.Owner.Id,
                PetTypeId = pet.PetType.Id,
                PetTypes = _combosHelper.GetComboPetTypes()
            };
        }

        public PetResponse ToPetResponse(Pet pet)
        {
            if (pet == null)
            {
                return null;
            }

            return new PetResponse
            {
                Born = pet.Born,
                Id = pet.Id,
                ImageUrl = pet.ImageFullPath,
                Name = pet.Name,
                PetType = pet.PetType.Name,
                Race = pet.Race,
                Remarks = pet.Remarks
            };
        }

        public OwnerResponse ToOwnerResposne(Owner owner)
        {
            if (owner == null)
            {
                return null;
            }

            return new OwnerResponse
            {
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                PhoneNumber = owner.User.PhoneNumber
            };
        }




    }
}
