using AutoMapper;
using CredoLoan.Business.Models;
using CredoLoan.Domain.Entities;

namespace CredoLoan.Business.Mappers {
    public class ClientMapperProfile : Profile {

        public ClientMapperProfile() {
            CreateMap<Client, ClientReadModel>();
            CreateMap<ClientAddOrUpdateModel, Client>();
        }
    }
}
