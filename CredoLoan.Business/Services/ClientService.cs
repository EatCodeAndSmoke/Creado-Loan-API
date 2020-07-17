using AutoMapper;
using CredoLoan.Business.Models;
using CredoLoan.DAL;
using CredoLoan.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CredoLoan.Business {
    internal class ClientService : IClientService {

        private readonly IClientRepository _clientRepository;
        private readonly IPasswordService _passwordService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, 
                             IPasswordService passwordService,
                             IRefreshTokenRepository refreshTokenRepository,
                             IMapper mapper) {

            _clientRepository = clientRepository;
            _passwordService = passwordService;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
        }

        public async Task<ClientReadModel> AuthenticateAsync(ClientAuthModel authModel) {
            var client = await _clientRepository.GetAsync(c => c.PersonalId == authModel.PersonalId);

            if (client == null)
                return null;
            if (!_passwordService.Match(authModel.Password, client.PasswordHash))
                return null;

            return _mapper.Map<ClientReadModel>(client);
        }

        public async Task<bool> ClientExists(Guid clientId) {
            return await _clientRepository.AnyAsync(c => c.Id == clientId);
        }

        public async Task<ClientReadModel> RegisterAsync(ClientAddOrUpdateModel client, string refreshToken) {
            var clientToSave = _mapper.Map<Client>(client);
            clientToSave.Id = Guid.NewGuid();
            clientToSave.Created = DateTime.Now;
            clientToSave.PasswordHash = _passwordService.ComputeHash(client.Password);
            await _clientRepository.BeginTransactionAsync();
            try {
                await _clientRepository.AddAsync(clientToSave);
                await AddRefreshTokenAsync(clientToSave.Id, refreshToken);
                _clientRepository.CommitTransaction();
                return _mapper.Map<ClientReadModel>(clientToSave);
            } catch (Exception) {
                _clientRepository.RollbackTransaction();
                throw;
            }         
        }

        public async Task<bool> UpdateClientAsync(ClientAddOrUpdateModel client) {
            if (!await ClientExists(client.Id))
                return false;

            var updatebleProps = new[] { nameof(Client.DateOfBirth), 
                                         nameof(Client.FirstName), 
                                         nameof(Client.LastName), 
                                         nameof(Client.PasswordHash) 
                                       };

            var clientToUpdate = _mapper.Map<Client>(client);
            clientToUpdate.PasswordHash = _passwordService.ComputeHash(client.Password);
            return await _clientRepository.UpdateAsync(clientToUpdate, updatebleProps);
        }

        public async Task<string> GetRefreshTokenAsync(Guid clientId) {
            var entity = await _refreshTokenRepository.GetByIdAsync(clientId);
            return entity?.RefreshToken;
        }

        private async Task AddRefreshTokenAsync(Guid clientId, string refreshToken) {
            await _refreshTokenRepository.AddAsync(new ClientRefreshToken { ClientId = clientId, RefreshToken = refreshToken });
        }
    }
}
