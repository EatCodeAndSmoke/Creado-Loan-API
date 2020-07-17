using AutoMapper;
using CredoLoan.Business.Models;
using CredoLoan.Domain.Entities;
using CredoLoan.Domain.Enums;

namespace CredoLoan.Business.Mappers {
    public class LoanAppMapperProfile : Profile {

        public LoanAppMapperProfile() {
            CreateMap<LoanApplication, LoanApplicationModel>()
                .ForMember(lr => lr.Currency, c => c.MapFrom(l => (byte)l.Currency))
                .ForMember(lr => lr.LoanType, c => c.MapFrom(l => (byte)l.Type))
                .ForMember(lr => lr.Status, c => c.MapFrom(l => (byte)l.Status));

            CreateMap<LoanApplicationModel, LoanApplication>()
                .ForMember(l => l.Currency, c => c.MapFrom(la => (LoanCurrency)la.Currency))
                .ForMember(l => l.Type, c => c.MapFrom(la => (LoanType)la.LoanType))
                .ForMember(l => l.Status, c => c.MapFrom(la => (LoanApplicationStatus)la.Status));
        }
    }
}
