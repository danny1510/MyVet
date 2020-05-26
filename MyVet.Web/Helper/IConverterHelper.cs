using MyVet.Common.Models;
using MyVet.Web.Data.Entities;
using MyVet.Web.Models;
using System.Threading.Tasks;

namespace MyVet.Web.Helper
{
    public interface IConverterHelper
    {
        Task<Pet> ToPetAsync(PetViewModel model, string path);
        PetViewModel ToPetViewModel(Pet pet);
        Task<History> ToHistoryAsync(HistoryViewModel model);
        HistoryViewModel ToHistoryViewModel(History history);
        PetResponse ToPetResponse(Pet pet);
        OwnerResponse ToOwnerResposne(Owner owner);




    }
}