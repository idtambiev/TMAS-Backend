using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class HistoryService:IHistoryService
    {
        private readonly HistoryRepository _historyRepository;
        public HistoryService(HistoryRepository repository)
        {
            _historyRepository = repository;
        }
        public async Task<IEnumerable<History>> GetAll(Guid userId)
        {
            return await _historyRepository.GetAll(userId);
        }

        public async Task<History> Create(History history,Guid userId)
        {
            History newHistory = new History
            {
                ActionType = history.ActionType,
                AuthorId = userId,
                CreatedDate = DateTime.Now,
                ActionObject = history.ActionObject,
                SourceAction= history.SourceAction,
                DestinationAction=history.DestinationAction

            };
            return  await _historyRepository.Create(newHistory);
        }

    }
}
