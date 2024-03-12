using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.UserSystem.IUserPromote;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.UserSystem.UserPromote
{
    public class UserPromoteService : IUserPromoteService
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly ILogger<UserPromoteService> _logger;
        
        public UserPromoteService(UserManager<UserInfo> userManager,
            ILogger<UserPromoteService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<bool> RequestPromotion(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogError("Usuário não encontrado.");
                    return false;
                }
                if(user.PromoteStatus == PromoteStatus.Requested || user.PromoteStatus == PromoteStatus.Promoted)
                {
                    _logger.LogError("Usuário já solicitou promoção.");
                    return false;
                }
                user.PromoteStatus = PromoteStatus.Requested;
                await _userManager.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao solicitar promoção: {ex}");
                return false;
            }
        }
        public async Task<bool> PromoteUser(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogError("Usuário não encontrado.");
                    return false;
                }
                if (user.PromoteStatus == PromoteStatus.None|| user.PromoteStatus == PromoteStatus.Promoted)
                {
                    _logger.LogError("Usuário não solicitou promoção.");
                    return false;
                }
                user.PromoteStatus = PromoteStatus.Promoted;
                await _userManager.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao solicitar promoção: {ex}");
                return false;
            }
        }
    }
}
